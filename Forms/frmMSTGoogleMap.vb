
Imports System
Imports System.Windows.Forms
Imports System.Security.Permissions
Imports System.IO
Imports MSTGoogleMap.ClsMstGoogleMaps
Imports MSTGoogleMap.modMSTGoogle_MapUtils
Imports MapUtils = MSTGoogleMap.modMSTGoogle_MapUtils
Imports MSTGoogleMap.clsOutputToExcel
Imports System.Threading
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Drawing
Imports System.Net
Imports Microsoft.Office.Interop.Excel
Imports Newtonsoft.Json
Imports System.ComponentModel
Imports Microsoft.Win32

<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
<Runtime.InteropServices.ComVisibleAttribute(True)>
Public Class frmMSTGoogleMap
    Inherits System.Windows.Forms.Form
    Private fromIndex As Integer
    Private dragIndex As Integer
    Private dragRect As System.Drawing.Rectangle
    Public Event myFormSettings(ByVal myX As Integer, ByVal myY As Integer, ByVal myW As Integer, ByVal myH As Integer)
    Public Event AppClosing(ByVal data As Boolean)
    Public Event CloseMe(ByVal data As Boolean)
    Public Event GoogleCreateSite(ByVal ptLat As Double, ByVal ptLong As Double)

    Public Event GoogleSaveMapBounds(ByVal ptLat As Double, ByVal ptLong As Double, ByVal zoomLevel As Integer)  'Added 20160105 RJG

    Public Event GoogleWriteErrorFile(ByVal data As String)                                                         'Added 20170718 RJG

    Public Event VehiclesCloseToPoint(ByVal ptLat As Double, ByVal ptLong As Double)
    Public Event UpdateGoogleMapType(ByVal gMapType As String)
    Public Event ClearGoogleMap(ByVal value As Boolean)
    Public Event GoogleMapId(ByVal data As String)

    Public Shared VehicleArrayStr As String = Nothing
    Private myPointsObj As clsBIS_GoogleMaps.clsPlotPoints
    Private WithEvents docToPrint As New Printing.PrintDocument
    Delegate Sub SetdgTurnsCallBack(ByVal data As List(Of ClsTurns))
    Delegate Sub SetdgWayPointsCallBack(ByVal address As String, ByVal lat As Double, ByVal lng As Double)
    Delegate Sub SetdgWayPointsCallBackWayPoint(ByVal lat As Double, ByVal lng As Double)
    Delegate Sub SetUpdateTurnsCallBack(ByVal turnsList As String, ByVal totalPath As String, ByVal wayPointLatLngs As String)

    Public Shared ReadOnly Property InstanceExists() As Boolean
        Get
            ' Access shared members through the Class name, not an instance.
            Return FleetFormExists
        End Get
    End Property

    Public WriteOnly Property FormSize() As clsBIS_GoogleMaps.clsFormSettings
        Set(ByVal Value As clsBIS_GoogleMaps.clsFormSettings)
            Try
                Dim tempRect As New System.Drawing.Rectangle(Value.FormX, Value.FormY, Value.FormWidth, Value.FormHeight)
                DesktopBounds = tempRect
                Me.Text = String.Format("MST Google-Map ({0})", Me.ProductVersion)
                mapStartLat = Value.MapLat
                mapStartLng = Value.MapLng
                mapStartZoom = Value.MapZoomLevel
            Catch ex As Exception
                RaiseEvent GoogleWriteErrorFile("GoogleMapFormSize - Error 71: " & ex.Message)
            End Try

        End Set

    End Property
    Public Property StartMapForm() As Boolean
        Set(ByVal Value As Boolean)
            If Value = True Then
                Try
                    If mapHandlersAdded = False Then
                        'Add the event handlers

                        'RouteEvents
                        AddHandler clsMapCallback.evt_ClearModifyRoute, AddressOf ClearModifyRoute
                        AddHandler clsMapCallback.evt_SaveAddressWaypoint, AddressOf SaveAddressWaypoint
                        AddHandler clsMapCallback.evt_SaveWaypointFromMap, AddressOf SaveWaypointFromMap
                        AddHandler clsMapCallback.evt_NoRoutesCreated, AddressOf NoRoutesCreated
                        AddHandler clsMapCallback.evt_UpdateTurns, AddressOf UpdateTurns

                        'Polygon Events
                        AddHandler clsMapCallback.evt_DeletePolyGeofence, AddressOf DeletePolyGeofence
                        AddHandler clsMapCallback.evt_UnableToDeletePolygon, AddressOf UnableToDeletePolygon
                        AddHandler clsMapCallback.evt_SavePolygonGeofence, AddressOf SavePolygonGeofence

                        'General Events
                        AddHandler clsMapCallback.evt_SaveMapBounds, AddressOf SaveMapBounds
                        AddHandler clsMapCallback.evt_UpdateGoogleMapType, AddressOf MapTypeChanged
                        AddHandler clsMapCallback.evt_CreateSite, AddressOf CreateSite
                        mapHandlersAdded = True
                        bw1 = New BackgroundWorker
                    End If
                    'Start the map display
                    Google_DisplayMap()
                Catch ex As Exception
                    MsgBox("Error 103: " & ex.Message, MsgBoxStyle.Critical, "MSTGoogleMap - StartMapForm")
                End Try
            End If
        End Set
        Get
            StartMapForm = False
        End Get
    End Property    ' StartMapForm
    Public Property GetParameters() As Boolean
        Get
            GetParameters = False
        End Get
        Set(ByVal Value As Boolean)
            If Value = True Then
                Try
                    If Me.Left = 0 Then Me.Left = 1
                    If Me.Top = 0 Then Me.Top = 1
                    RaiseEvent myFormSettings(Me.Left, Me.Top, Me.Width, Me.Height)
                Catch exc As Exception
                    RaiseEvent GoogleWriteErrorFile("GoogleMap_GetParameters - Error 122: " & exc.Message)
                End Try
            End If

        End Set

    End Property        ' GetParameters
    Public WriteOnly Property ClearMap() As Boolean
        Set(ByVal Value As Boolean)
            GM_Browser.Document.InvokeScript("clear_Google_Overlays")
        End Set
    End Property
    Public WriteOnly Property CloseForm() As Boolean
        Set(ByVal Value As Boolean)
            If Value = True Then
                Me.Dispose()
            End If
        End Set
    End Property
    <STAThread>
    Private Sub Google_DisplayMap()
        CheckInterExplorerVersion()

        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf Google_DisplayMap)
            Me.Invoke(d, New Object)
        Else
            If Me.WindowState = FormWindowState.Minimized Then
                Exit Sub
            End If
            Try
                TabControl1.TabPages.Remove(turnTabPage)

                If IsMSTrackFlag = True Then
                    mnuRoutes.Visible = False
                Else
                    mnuRoutes.Visible = True
                End If
                TabControl1.TabPages.Remove(turnTabPage)
                mainContainer1.Panel1Collapsed = True
                turnTabPage.Visible = False
                GM_Browser.AllowWebBrowserDrop = False
                GM_Browser.IsWebBrowserContextMenuEnabled = False
                GM_Browser.WebBrowserShortcutsEnabled = False
                GM_Browser.ObjectForScripting = Me
                Dim UseDrivers As String = "True"
                Dim DisplayOption As String = "ByReg"
                Dim UseLabels As String = Google_UseLabels
                Dim zoomLevel As Integer = Google_ZoomLevel
                Dim joinPoints As String = Google_JoinPoints
                Dim TranslatePosition As String = "Position"

                Dim NumofPlots As Integer = Google_NoOfPoints
                Dim tmpDate As String = Nothing
                Dim VehicleArray(10000, 18) As String
                Dim IgnStatus As String
                Dim thisDriver As String = ""
                Dim thisLocation As String = ""
                Dim thisEventDesc As String = ""
                Dim thisOdometer As String = ""
                Dim TDES As New clsTripleDES
                Dim thisGoogleMapKey = TDES.DecryptFromHexStr(theGoogleMapKey)
                str_IconBase64 = Airport & "~" & Attraction & "~" & Bank & "~" & Bicycling & "~" & Boatramp & "~" & BoomGateClosed & "~" & BoomGateOpened & "~" & BouyBlack & "~" & BouyBlue & "~" & BouyGray & "~" & BouyGreen & "~" & BouyRed & "~" & BouyYellow & "~" & Building & "~" & Building2 & "~" & BusStation & "~" & BusStop & "~" & CarRentalService & "~" & CarRepair & "~" & Castle & "~" & CatholicTemple & "~" & Caution & "~" & Cinema & "~" & City & "~" & City2 & "~" & CityBuilding & "~" & CityBuilding2 & "~" & Consulate & "~" & ContainerPort & "~" & Customs & "~" & Entertainment & "~" & Etoll & "~" & ExhibitionCenter & "~" & Factory & "~" & Factory2 & "~" & FireStation & "~" & FoodShop & "~" & Garage & "~" & Garage2 & "~" & GasStation & "~" & Gate & "~" & GeneralStore & "~" & Government & "~" & Hospital & "~" & Hotel & "~" & Hotel2 & "~" & House & "~" & House2 & "~" & Jetty & "~" & Library & "~" & McDonalds & "~" & Memorial & "~" & MilitaryArea & "~" & Museum & "~" & OilRefinery & "~" & OrthodoxyTemple & "~" & PetrolStation & "~" & Pharmacy & "~" & PoliceStation & "~" & Port & "~" & RailwayStation & "~" & RailwayStation2 & "~" & Satellite & "~" & School & "~" & School2 & "~" & ShoppingCenter & "~" & SportCenter & "~" & Stadium & "~" & SwimmingPool & "~" & Tent & "~" & Tent2 & "~" & Theater & "~" & Village & "~" & Warehouse & "~" & Zoo & "~" & RouteStart & "~" & RouteEnd & "~" & RouteWayPoint
                str_IconArray = String.Join("~", IconArray)
                Select Case Google_MapType
                    Case "Map"
                    Case "Satellite"
                    Case "Hybrid"
                    Case "Terrain"
                    Case Else
                End Select
                VehicleArrayStr = ""
                If Not thisGoogleObj Is Nothing Or Google_IsBufferZone = True Or Google_IsRouteManagement = False Then
                    TopMost = False
                    For i = 0 To NumofPlots - 1
                        Try
                            With thisGoogleObj(i)
                                IgnStatus = "N/A"
                                Select Case .IgnitionStatus
                                    Case 0
                                        IgnStatus = "N/A"
                                    Case 1
                                        IgnStatus = "OFF"
                                    Case 2
                                        IgnStatus = "ON"
                                    Case 99
                                        IgnStatus = "SITE"
                                End Select

                                Try
                                    thisDriver = .DriverName
                                Catch ex As Exception
                                    thisDriver = .DriverId
                                End Try
                                Try
                                    thisLocation = .Location
                                Catch ex As Exception
                                    thisLocation = ""
                                End Try
                                thisLocation = thisLocation.Replace("'", "")
                                Try
                                    thisEventDesc = .EventDesc
                                Catch ex As Exception
                                    thisEventDesc = .EventDescription
                                End Try
                                If Not IsDBNull(.Odometer) Then
                                    thisOdometer = Format(.Odometer, "#,###,##0")
                                Else
                                    thisOdometer = "0"
                                End If
                                Dim tmpDistType As String
                                If DistUnits = "Miles" Then
                                    tmpDistType = " mph"
                                Else
                                    tmpDistType = " kph"
                                End If
                                tmpDate = Format(.TransDate, "yyyy-MM-dd HH:mm:ss")
                                VehicleArrayStr = VehicleArrayStr & .RegNo & "|" & .VehDesc & "|" & .Latitude & "|" & .Longitude & "|" & tmpDate & "|" & (CLng(.Speed)) & " " & tmpDistType & " " & .Heading &
                                    "|" & thisLocation & "|" & IgnStatus & "|" & Trim(.Heading) & "|" & thisEventDesc & "|" & thisOdometer & "|" & thisDriver & "|" & .VehicleType & "|" & .Speed
                                If i < NumofPlots - 1 Then
                                    VehicleArrayStr = VehicleArrayStr & "~"
                                End If
                            End With

                        Catch ex As Exception
                            MsgBox("Error processing record " & i & "  : " & ex.Message, MsgBoxStyle.Critical, "Display Google Map")

                        End Try
                    Next
                    If ClsMstGoogleMaps.reloadGeofences = True Then
                        LoadGeofenceArray()
                        If IsMSTrackFlag = False Then
                            PlotGeoFences()
                        End If
                        If IsMSTrackFlag = False Then
                            GetPolygonGeofences()
                            PlotPolygonRoutes(MapCompany, "*")
                            GetPolygonRoutes(MapCompany, "*")
                        End If
                        reloadGeofences = False
                    End If
                    If ClsMstGoogleMaps.reloadSites = True Then
                        PlotSites()
                        reloadSites = False
                    End If

                    If NumofPlots = 1 Then
                        Me.Text = "MST Google-Map (" & Me.ProductVersion & ")" & " - Vehicle " & thisGoogleObj(0).RegNo & " - " & thisGoogleObj(0).VehDesc
                    Else
                        Me.Text = String.Format("MST Google-Map ({0})", Me.ProductVersion)
                    End If
                    If Google_IsHistory = True Then
                        LablesToolStripMenuItem.Visible = False
                        Google_UseLabels = True
                        Me.Text = "MST Google-Map (" & Me.ProductVersion & ")" & " - Plotting History for Vehicle " & thisGoogleObj(0).RegNo & " - " & thisGoogleObj(0).VehDesc
                    Else
                        LablesToolStripMenuItem.Visible = True
                    End If
#If DEBUG Then
                    RaiseEvent GoogleWriteErrorFile("Google_DisplayMap")
#End If

                    If FleetFormExists = True Then
                        'GM_Browser.Document.InvokeScript("ClearRoutes")
                        GM_Browser.Document.InvokeScript("BuildArray", {VehicleArrayStr, 1})
                        GM_Browser.Document.InvokeScript("SetupMapValues")
                        GM_Browser.Document.InvokeScript("SetupMap")
                        GM_Browser.Document.InvokeScript("SetMapOptions", {VehicleArrayStr, 1, 1, ClsMstGoogleMaps.Google_UseLabels, ClsMstGoogleMaps.Google_JoinPoints, ClsMstGoogleMaps.Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, ClsMstGoogleMaps.Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, ClsMstGoogleMaps.Google_IsRouteOptimized, ClsMstGoogleMaps.IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})
                        GM_Browser.Document.InvokeScript("AddGoogleMarkers")
                        GM_Browser.Document.InvokeScript("SetMapBounds")
                    Else
                        Dim strHtml As String = CreateMapHTML(thisGoogleMapKey)
                        FileOpen(1, FleetHTML, OpenMode.Output, OpenAccess.Write, OpenShare.Shared)
                        Print(1, strHtml)
                        FileClose(1)
                        GM_Browser.Visible = True
                        'GM_Browser.Navigate("www.chisono.it")
                        GM_Browser.Navigate(FleetHTML)
                        FleetFormExists = True
                        Me.Show()
                    End If
                Else
                    LoadGeofenceArray()
                    If IsMSTrackFlag = False Then
                        PlotGeoFences()
                    End If
                    If IsMSTrackFlag = False Then
                        GetPolygonGeofences()
                        PlotPolygonRoutes(MapCompany, "*")
                        GetPolygonRoutes(MapCompany, "*")
                    End If
                    PlotSites()
                    Dim strHtml As String = CreateMapHTML(thisGoogleMapKey)
                    FileOpen(1, FleetHTML, OpenMode.Output, OpenAccess.Write, OpenShare.Shared)
                    Print(1, strHtml)
                    FileClose(1)
                    GM_Browser.Navigate(FleetHTML)
                    FleetFormExists = True
                    Me.Show()


                End If
            Catch ex As Exception
                RaiseEvent GoogleWriteErrorFile("Google_DisplayMap - Error 323: " & ex.Message)

            End Try
        End If
    End Sub

    Public Sub SaveMapBounds(ByVal centerLat As Double, ByVal centerLng As Double, ByVal mapZoom As Integer)
        'Added 20160105 RJG

        If Not IsNumeric(centerLat) Then
            centerLat = 0
        End If
        If System.Double.IsNaN(centerLat) = True Then
            centerLat = 0
        End If

        If Not IsNumeric(centerLng) Then
            centerLng = 0
        End If
        If Not IsNumeric(mapZoom) Or mapZoom = 0 Then
            mapZoom = 15
        End If
        RaiseEvent GoogleSaveMapBounds(centerLat, centerLng, mapZoom)
    End Sub
    Public Sub MapTypeChanged(ByVal NewMapType As String)
        RaiseEvent UpdateGoogleMapType(NewMapType)
    End Sub
    Public Sub CreateSite(ByVal message As String)
        Try
            Dim locArray() As String = message.Split(",")
            Dim MapLat As Double = CDbl(locArray(0))
            Dim MapLong As Double = CDbl(locArray(1))
            RaiseEvent GoogleCreateSite(MapLat, MapLong)
            ClsMstGoogleMaps.reloadGeofences = True
        Catch ex As Exception

        End Try

    End Sub
    Public Sub NoRoutesCreated(ByVal routeMsg As String)
        If routeMsg = "NoRoutesCreated" Then
            MsgBox("Unable to show routes, no routes created.", MsgBoxStyle.Information, "MST Viewer - Show Routes")
        ElseIf routeMsg = "NoRoutesAllocated" Then
            MsgBox("Unable to show routes, no routes allocated by this Company.", MsgBoxStyle.Information, "MST Viewer - Show Routes")
        End If
    End Sub
    Public Sub CloseApp()
        Try
            If Not Me.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
                If Me.Left = 0 Then Me.Left = 1
                If Me.Top = 0 Then Me.Top = 1
                RaiseEvent myFormSettings(Me.Left, Me.Top, Me.Width, Me.Height)
            End If
            RaiseEvent AppClosing(True)

            FleetFormExists = False
            System.Windows.Forms.Application.DoEvents()
            Me.Dispose()
        Catch ex As Exception
        End Try
    End Sub     ' CloseApp

    Private Sub mnuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClose.Click
        FleetFormExists = False
        Me.Hide()
        'CloseApp()
    End Sub
    Private Sub mnuClearMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClearMap.Click
        IsModifyRoute = False
        mainContainer1.Panel1Collapsed = True
        TabControl1.TabPages.Remove(turnTabPage)
        mnuDisplayGeofence.Text = "Display Site Geofences"
        mnuEndDistCalc.Enabled = False
        mnuClearDistCalc.Enabled = False
        Google_IsRouteManagement = False
        GM_Browser.Document.InvokeScript("ClearMap")
        'GM_Browser.Document.InvokeScript("clear_Google_Overlays")
        'GM_Browser.Document.InvokeScript("endDistanceListeners")
        'GM_Browser.Document.InvokeScript("setShowRoutes")
        RaiseEvent ClearGoogleMap(True)
    End Sub
    Private Sub mnuStartDistCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStartDistCalc.Click
        mnuEndDistCalc.Enabled = True
        mnuClearDistCalc.Enabled = True
        GM_Browser.Document.InvokeScript("addDistanceListeners")
    End Sub

    Private Sub mnuEndDistCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEndDistCalc.Click
        mnuEndDistCalc.Enabled = False
        mnuClearDistCalc.Enabled = True
        GM_Browser.Document.InvokeScript("endDistanceListeners")
    End Sub
    Private Sub PlotSites()

        Dim sqlstr As String
        Dim tmp As String = ""
        Dim SiteInfo(100001, 14) As String
        Dim ErrorFlag As Boolean = False
        Dim myConnection As New SqlClient.SqlConnection(MapGeoPlacesConnectionString)
        Dim myReader As SqlClient.SqlDataReader
        Dim i As Integer
        Dim NoSites As Integer
        RouteSites = New List(Of ClsSites)
        Dim crit As String = String.Empty
        If MapCompany = "*" Then
            crit = ""
        Else
            crit = " Where SiteInfo.Company = '" & MapCompany & "'"
        End If
        Try

            sqlstr = "SELECT SiteInfo.Name, SiteInfo.Code, SiteInfo.Category, Categories.CatDesc, Categories.CatIcon, SiteInfo.Comments, SiteInfo.Latitude, SiteInfo.Longitude, " &
                "SiteInfo.SiteSeqNo, SiteInfo.Company, SiteInfo.Fleet_Code, SiteInfo.ClientID, SiteInfo.Surname, SiteInfo.FirstName, SiteInfo.Tel " &
                "FROM SiteInfo LEFT OUTER JOIN Categories ON SiteInfo.Category = Categories.CatCode  " & crit &
                "Order By SiteInfo.Fleet_Code, SiteInfo.Name"

            Dim mySQLCommand As New SqlClient.SqlCommand(sqlstr, myConnection)

            Try
                myConnection.Open()
                myReader = mySQLCommand.ExecuteReader(CommandBehavior.Default)

                With myReader

                    While .Read = True
                        Try
                            Dim siteObj As New ClsSites
                            If Not .IsDBNull(0) Then
                                SiteInfo(i, 0) = Trim(.GetString(0))
                                siteObj.SiteName = Trim(.GetString(0))
                                'SiteName
                            End If

                            If Not .IsDBNull(1) Then
                                SiteInfo(i, 1) = Trim(.GetString(1))
                                siteObj.SiteCode = Trim(.GetString(1))
                                'SiteCode
                            End If

                            If Not .IsDBNull(2) Then
                                SiteInfo(i, 2) = Trim(.GetInt32(2))
                                'Category
                            End If

                            If Not .IsDBNull(3) Then 'CatDesc
                                SiteInfo(i, 3) = Trim(.GetString(3))
                                siteObj.SiteCatDesc = Trim(.GetString(3))
                            End If

                            If Not .IsDBNull(4) Then 'CatIcon
                                SiteInfo(i, 4) = Trim(.GetString(4))
                                siteObj.SiteIconPath = Trim(.GetString(4))
                            End If

                            If Not .IsDBNull(5) Then 'Comments
                                SiteInfo(i, 5) = Trim(.GetString(5))
                                siteObj.SiteComments = Trim(.GetString(5))
                            End If

                            If Not .IsDBNull(6) Then
                                SiteInfo(i, 6) = Trim(.GetDouble(6)).ToString
                                siteObj.SiteLatitude = Trim(.GetDouble(6))
                                'Latitude
                            End If

                            If Not .IsDBNull(7) Then
                                SiteInfo(i, 7) = Trim(.GetDouble(7)).ToString
                                siteObj.SiteLongitude = Trim(.GetDouble(7))
                                'Longitude
                            End If
                            If Not .IsDBNull(8) Then
                                SiteInfo(i, 8) = .GetInt32(8)
                                'SiteSeqNo
                            End If
                            If Not .IsDBNull(9) Then
                                SiteInfo(i, 9) = Trim(.GetString(9))
                                siteObj.SiteCompany = Trim(.GetString(9))
                                'Company
                            End If

                            If Not .IsDBNull(10) Then
                                SiteInfo(i, 10) = Trim(.GetString(10))
                                siteObj.SiteFleetCode = Trim(.GetString(10))
                                'FleetCode
                            End If

                            If Not .IsDBNull(11) Then
                                SiteInfo(i, 11) = Trim(.GetString(11))
                                'ClientID
                            End If

                            If Not .IsDBNull(12) Then
                                SiteInfo(i, 12) = Trim(.GetString(12))
                                'Surname
                            End If
                            If Not .IsDBNull(13) Then
                                SiteInfo(i, 12) = Trim(.GetString(13)) & " " & SiteInfo(i, 12)
                                siteObj.SiteContact = Trim(.GetString(13)) & " " & Trim(SiteInfo(i, 12))
                                'FirstName
                            End If
                            If Not .IsDBNull(14) Then
                                SiteInfo(i, 14) = Trim(.GetString(14))
                                siteObj.SiteContactNumber = Trim(.GetString(14))
                                'Contact Number
                            End If
                            i = i + 1
                            RouteSites.Add(siteObj)
                        Catch exc As Exception
                            MsgBox("DisplaySitesOnMap - 2: " & exc.Message)
                        End Try
                    End While
                End With
                NoSites = i
                str_SitesArray = JsonConvert.SerializeObject(RouteSites)
                'Try
                '    For i = 0 To NoSites - 1
                '        If SiteInfo(i, 6) <> "0" And SiteInfo(i, 7) <> "0" Then
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 0) & ","
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 8) & ","
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 2) & ","
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 3) & ","
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 4) & ","
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 5) & ","
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 6) & ","
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 7) & ","
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 12) & ","
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 14) & ","
                '            str_SitesArray = str_SitesArray & SiteInfo(i, 10) & "~"
                '        End If
                '    Next i
                'Catch exc As Exception
                '    RaiseEvent GoogleWriteErrorFile("Google_Plot Sites - Error 534: " & exc.Message)
                'End Try

            Catch ex As Exception
                MsgBox("Error 661: " & ex.Message, MsgBoxStyle.Critical, "MSTGoogle Map - Plot Sites")
            End Try

        Catch ex As Exception
            MsgBox("DisplaySitesOnMap - 3: " & ex.Message)
        End Try


        Try
            myConnection.Close()
            myConnection = Nothing
        Catch ex As Exception

        End Try

        Try
            ' 20110407  Removed KSM
            'GIS.Update()
        Catch ex As Exception
        End Try
    End Sub     ' DisplaySitesOnMap




    Private Sub mnuDrawLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDrawLine.Click
    End Sub

    Private Sub mnuClearDistCalc_Click(sender As System.Object, e As System.EventArgs) Handles mnuClearDistCalc.Click
        GM_Browser.Document.InvokeScript("clearDistanceListeners")
    End Sub

    Private Sub CreateSiteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles mnuCreateSite.Click
        GM_Browser.Document.InvokeScript("AllowCreateGeofence")
    End Sub
    Private Sub frmMSTGoogleMap_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        If Not Me.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
            If Me.Left = 0 Then Me.Left = 1
            If Me.Top = 0 Then Me.Top = 1
            RaiseEvent myFormSettings(Me.Left, Me.Top, Me.Width, Me.Height)
        End If
    End Sub
    Private Sub frmMSTGoogleMap_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        If Not Me.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
            If Me.Left = 0 Then Me.Left = 1
            If Me.Top = 0 Then Me.Top = 1
            RaiseEvent myFormSettings(Me.Left, Me.Top, Me.Width, Me.Height)
        End If
    End Sub
    Private Sub frmMSTGoogleMap_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        If Not Me.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
            If Me.Left = 0 Then Me.Left = 1
            If Me.Top = 0 Then Me.Top = 1
            RaiseEvent myFormSettings(Me.Left, Me.Top, Me.Width, Me.Height)
        End If
    End Sub
    Private Sub frmMSTGoogleMap_Move(sender As Object, e As EventArgs) Handles Me.Move
        If Not Me.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
            If Me.Left = 0 Then Me.Left = 1
            If Me.Top = 0 Then Me.Top = 1
            RaiseEvent myFormSettings(Me.Left, Me.Top, Me.Width, Me.Height)
        End If
    End Sub
    Private Sub mnuBufferZones_Click(sender As System.Object, e As System.EventArgs) Handles mnuBufferZones.Click

    End Sub

    Private Sub mnuDisplayRegNo_Click(sender As System.Object, e As System.EventArgs) Handles mnuDisplayRegNo.Click
        If mnuDisplayRegNo.Checked = True Then
            Display_RegNo = True
            Display_Desc = False
            Display_Both = False
            mnuDisplayDesc.Checked = False
            mnuDisplayBoth.Checked = False
            Google_UseLabels = True
        Else
            Display_RegNo = False
            Display_Desc = False
            Display_Both = False
            mnuDisplayDesc.Checked = False
            mnuDisplayBoth.Checked = False
            Google_UseLabels = False
        End If
        GM_Browser.Document.InvokeScript("clear_Google_Overlays")
        GM_Browser.Document.InvokeScript("SetMapOptions", {VehicleArrayStr, 0, 0, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})
        GM_Browser.Document.InvokeScript("AddGoogleMarkers")

    End Sub

    Private Sub mnuDisplayDesc_Click(sender As System.Object, e As System.EventArgs) Handles mnuDisplayDesc.Click
        If mnuDisplayDesc.Checked = True Then
            Display_RegNo = False
            Display_Desc = True
            Display_Both = False
            mnuDisplayRegNo.Checked = False
            mnuDisplayBoth.Checked = False
            Google_UseLabels = True
        Else
            Display_RegNo = False
            Display_Desc = False
            Display_Both = False
            mnuDisplayRegNo.Checked = False
            mnuDisplayBoth.Checked = False
            Google_UseLabels = False
        End If
        GM_Browser.Document.InvokeScript("clear_Google_Overlays")
        GM_Browser.Document.InvokeScript("SetMapOptions", {VehicleArrayStr, 0, 0, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})
        GM_Browser.Document.InvokeScript("AddGoogleMarkers")

    End Sub

    Private Sub mnuDisplayBoth_Click(sender As System.Object, e As System.EventArgs) Handles mnuDisplayBoth.Click
        If mnuDisplayBoth.Checked = True Then
            Display_RegNo = False
            Display_Desc = False
            Display_Both = True
            mnuDisplayRegNo.Checked = False
            mnuDisplayDesc.Checked = False
            Google_UseLabels = True
        Else
            Display_RegNo = False
            Display_Desc = False
            Display_Both = False
            mnuDisplayRegNo.Checked = False
            mnuDisplayDesc.Checked = False
            Google_UseLabels = False
        End If
        GM_Browser.Document.InvokeScript("clear_Google_Overlays")
        GM_Browser.Document.InvokeScript("SetMapOptions", {VehicleArrayStr, 0, 0, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})
        GM_Browser.Document.InvokeScript("AddGoogleMarkers")

    End Sub

    Private Sub MapRefreshTimer_Tick(sender As System.Object, e As System.EventArgs) Handles MapRefreshTimer.Tick
        'GM_Browser.Refresh()
    End Sub
    Public Sub SavePolygonGeofence(ByVal message As String)
        Dim tmpArray() As String = message.Split("~")
        Dim myConnection As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
        Dim mySqlCommand As New SqlClient.SqlCommand
        If tmpArray(1) = True Then
            PolyType = tmpArray(2)
            Dim myForm As New frmPolyGeofenceName
            myForm.ShowDialog()
            If myForm.DialogResult = DialogResult.OK Then
                Try
                    mySqlCommand = New SqlClient.SqlCommand("MST_SavePolygonGeofence", myConnection)
                    With mySqlCommand
                        .CommandTimeout = 300
                        .CommandType = CommandType.StoredProcedure

                        .Parameters.Add("@PolyName", SqlDbType.NVarChar, 255).Value = Trim(PolygonGeofenceName)
                        .Parameters.Add("@UserId", SqlDbType.NVarChar, 50).Value = Trim(MapUserID)
                        .Parameters.Add("@GeoCoords", SqlDbType.NVarChar, -1).Value = tmpArray(0)
                        .Parameters.Add("@Company", SqlDbType.NVarChar, 50).Value = MapCompany
                    End With
                    myConnection.Open()
                    Dim mySqlDataReader As SqlClient.SqlDataReader
                    mySqlDataReader = mySqlCommand.ExecuteReader(CommandBehavior.Default)
                    mySqlDataReader.Close()
                    myConnection.Close()
                    myConnection.Dispose()

                    MsgBox(PolygonGeofenceName & " saved.", MsgBoxStyle.Information, "MST Viewer - Save Geofence")
                Catch ex As Exception
                    MsgBox(PolygonGeofenceName & " was not saved. Error: " & ex.Message, MsgBoxStyle.Critical, "MST Viewer - Save Geofence Failed")
                End Try
            End If
        Else
            Try
                PolygonGeofenceName = tmpArray(4)
                mySqlCommand = New SqlClient.SqlCommand("MST_UpdatePolygonGeofence", myConnection)
                With mySqlCommand
                    .CommandTimeout = 300
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add("@GeoId", SqlDbType.Int).Value = tmpArray(3)
                    .Parameters.Add("@GeoCoords", SqlDbType.NVarChar, -1).Value = tmpArray(0)
                End With
                myConnection.Open()
                Dim mySqlDataReader As SqlClient.SqlDataReader
                mySqlDataReader = mySqlCommand.ExecuteReader(CommandBehavior.Default)
                mySqlDataReader.Close()
                myConnection.Close()
                myConnection.Dispose()

                MsgBox(PolygonGeofenceName & " updated.", MsgBoxStyle.Information, "MST Viewer - Save Geofence")
            Catch ex As Exception
                MsgBox(PolygonGeofenceName & " was not updated. Error: " & ex.Message, MsgBoxStyle.Critical, "MST Viewer - Save Geofence Failed")
            End Try
        End If
        ReloadAllGeofences()
    End Sub
    Public Sub UnableToDeletePolygon(ByVal thisPolygon As String)
        MsgBox("Geofence " & thisPolygon & " has Company and Fleet Allocations. It cannot be deleted.", MsgBoxStyle.Information, "MST Viewer - Delete polygon geofence")
    End Sub
    Public Sub DeletePolyGeofence(data As String)
        ClsMstGoogleMaps.reloadGeofences = True
        Dim myConnection As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
        Dim tmpArray() As String = data.Split("~")
        Dim thisGeoName As String = tmpArray(0)
        Dim thisGeoID As Integer = tmpArray(1)
        Dim sqlStr As String = "Delete From  dbo.GeoPolygons Where GeoId = " & thisGeoID
        Dim myReader As SqlClient.SqlDataReader
        Try
            NoGeofences = 0
            myConnection.Open()
            Dim mySqlCommand As New SqlClient.SqlCommand(sqlStr, myConnection)
            myReader = mySqlCommand.ExecuteReader(CommandBehavior.Default)
            myConnection.Close()
            myConnection.Dispose()
            If myReader.RecordsAffected > 0 Then
                MsgBox(thisGeoName & " deleted.", MsgBoxStyle.Information, "MS Track Pro 8 - Delete Geofence")
            End If
            ReloadAllGeofences()
        Catch ex As Exception
            MsgBox(thisGeoName & " was not deleted. Error: " & ex.Message, MsgBoxStyle.Critical, "MS Track Pro 8 - Delete Geofence Failed")

        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        ReloadAllGeofences()
        GM_Browser.Document.InvokeScript("AddGoogleMarkers")
        GM_Browser.Document.InvokeScript("SetMapBounds")

    End Sub
    Public Sub ReloadAllGeofences()
        PlotSites()
        LoadGeofenceArray()
        If IsMSTrackFlag = False Then
            PlotGeoFences()
            GetPolygonGeofences()
            GetPolygonRoutes(MapCompany, "*")
        End If
        GM_Browser.Document.InvokeScript("SetMapOptions", {VehicleArrayStr, 1, 0, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})

    End Sub

    Private Sub mnuImportKML_Click(sender As Object, e As EventArgs) Handles mnuImportKML.Click
        Dim myForm As New frmImportKML
        myForm.ShowDialog()
        GM_Browser.Document.InvokeScript("loadKMLRoute", {kmlRouteUri})
        'ReloadAllGeofences()
        'GM_Browser.Document.InvokeScript("AddGoogleMarkers")
        'GM_Browser.Document.InvokeScript("SetMapBounds")
        'GM_Browser.Document.InvokeScript("reloadGeofences")
    End Sub

    Private Sub GM_Browser_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles GM_Browser.DocumentCompleted
        If VehicleArrayStr.Length > 0 Then
            GM_Browser.Document.InvokeScript("Load_Google_Map", New String() {VehicleArrayStr})
        Else
            GM_Browser.Document.InvokeScript("SetMapCenter", {mapStartLat, mapStartLng, mapStartZoom})
        End If
        'GM_Browser.Document.InvokeScript("SetMapOptions", {Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})
        GM_Browser.Document.InvokeScript("SetMapOptions", {VehicleArrayStr, 0, 1, ClsMstGoogleMaps.Google_UseLabels, ClsMstGoogleMaps.Google_JoinPoints, ClsMstGoogleMaps.Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, ClsMstGoogleMaps.Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, ClsMstGoogleMaps.Google_IsRouteOptimized, ClsMstGoogleMaps.IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})

    End Sub
    Private Sub mnuShowRoutes_Click(sender As Object, e As EventArgs) Handles mnuShowRoutes.Click
        Dim colorIndex As Integer
        Dim hasAllocatedRoutes As Boolean
        Dim hasUnallocatedRoutes As Boolean
        Dim AllocatedNode As TreeNode = Nothing
        Dim UnallocatedNode As TreeNode = Nothing
        mainContainer1.Panel1Collapsed = False
        pnlAddress.Visible = False
        pnlClickMap.Visible = False
        pnlGeofences.Visible = False
        pnlSites.Visible = False
        pnlTop.Visible = False
        pnlVehicles.Visible = False
        pnlWaypointGrid.Visible = False
        pnlWaypointLabel.Visible = False
        tvRoutes.Nodes.Clear()

        If PolyGeoFences.Count > 0 Then
            For Each obj In PolyGeoFences
                If obj.RegNo <> "" Then
                    hasAllocatedRoutes = True
                Else
                    hasUnallocatedRoutes = True
                End If
            Next
            If hasAllocatedRoutes = True Then
                AllocatedNode = tvRoutes.Nodes.Add("Allocated")
            End If
            If hasUnallocatedRoutes = True Then
                UnallocatedNode = tvRoutes.Nodes.Add("Unallocated")
            End If
            For Each obj In PolyGeoFences
                If obj.GeoFenceSource = "4" Then
                    Dim customNode As TreeNode = New TreeNode()

                    customNode.Text = obj.GeoFenceName
                    If obj.RegNo <> "" Then
                        customNode.Text = obj.GeoFenceName & " (" & obj.RegNo & ")"
                        AllocatedNode.Nodes.Add(customNode)
                        customNode.ContextMenuStrip = contextMnuTvRoutesMod
                    Else
                        customNode.Text = obj.GeoFenceName
                        UnallocatedNode.Nodes.Add(customNode)
                        customNode.ContextMenuStrip = contextMnuTvRoutesDelMod
                    End If

                    colorIndex += 1
                End If
            Next
        Else
            tvRoutes.Nodes.Add("No Routes Created")
        End If
        pnlShowRoutes.Visible = True
        pnlCreateRoutes.Visible = False
    End Sub

    Private Sub mnuCreateRoute_Click(sender As Object, e As EventArgs) Handles mnuCreateRoute.Click
        dgWayPoints.Rows.Clear()
        ' WaypointsObj = New List(Of ClsWayPoints)
        WaypointIndex = 0
        WaypointsObj.Clear()
        LoadWayPointCombos()
        mainContainer1.Panel1Collapsed = False
        pnlAddress.Visible = True
        pnlClickMap.Visible = True
        pnlGeofences.Visible = True
        pnlSites.Visible = True
        pnlTop.Visible = True
        pnlVehicles.Visible = True
        pnlWaypointGrid.Visible = True
        pnlWaypointLabel.Visible = True
        pnlShowRoutes.Visible = False
        pnlCreateRoutes.Visible = True
        Google_IsRouteManagement = True
        dgWayPoints.Columns.Item(0).Width = dgWayPoints.Width
        GM_Browser.Document.InvokeScript("ClearRoutes")
    End Sub
    Private Sub dgWayPoints_Resize(sender As Object, e As EventArgs) Handles dgWayPoints.Resize
        dgWayPoints.Columns.Item(0).Width = dgWayPoints.Width
    End Sub

    Private Sub LoadWayPointCombos()
        cmbVehicles.Items.Clear()
        cmbGeofences.Items.Clear()
        cmbSites.Items.Clear()
        For Each tmpObj In RouteVehicles
            If Display_RegNo = True Then
                cmbVehicles.Items.Add(tmpObj.RouteVehRegNo)
            ElseIf Display_Desc = True Then
                cmbVehicles.Items.Add(tmpObj.RouteVehVehDesc)
            Else
                cmbVehicles.Items.Add(tmpObj.RouteVehRegNo)
            End If
        Next
        For Each geoObj In RouteGeofences
            cmbGeofences.Items.Add(geoObj.GeofenceGeoName)
        Next
        For Each siteObj In RouteSites
            cmbSites.Items.Add(siteObj.SiteName)
        Next
    End Sub
    Private Sub cmbVehicles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVehicles.SelectedIndexChanged
        dgWayPoints.Rows.Add(sender.Text)
        For Each tmpObj In RouteVehicles
            If tmpObj.RouteVehRegNo = sender.Text Or tmpObj.RouteVehVehDesc = sender.text Then
                Dim newWaypoint As New ClsWayPoints
                newWaypoint.WaypointNumber = WaypointIndex
                WaypointIndex += 1
                newWaypoint.WaypointName = sender.text
                newWaypoint.WaypointLatitude = tmpObj.RouteVehLatitude
                newWaypoint.WaypointLongitude = tmpObj.RouteVehLongitude
                newWaypoint.WaypointType = "Vehicle"
                WaypointsObj.Add(newWaypoint)
                Exit For
            End If
        Next
    End Sub

    Private Sub cmbSites_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSites.SelectedIndexChanged
        dgWayPoints.Rows.Add(sender.Text)
        For Each tmpObj In RouteSites
            If tmpObj.SiteName = sender.Text Then
                Dim newWaypoint As New ClsWayPoints
                newWaypoint.WaypointNumber = WaypointIndex
                WaypointIndex += 1
                newWaypoint.WaypointName = sender.text
                newWaypoint.WaypointLatitude = tmpObj.SiteLatitude
                newWaypoint.WaypointLongitude = tmpObj.SiteLongitude
                newWaypoint.WaypointType = "Site"
                WaypointsObj.Add(newWaypoint)
                Exit For
            End If
        Next
    End Sub

    Private Sub cmbGeofences_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGeofences.SelectedIndexChanged
        dgWayPoints.Rows.Add(sender.Text)
        For Each tmpObj In RouteGeofences
            If tmpObj.GeofenceGeoName = sender.Text Then
                Dim newWaypoint As New ClsWayPoints
                newWaypoint.WaypointNumber = WaypointIndex
                WaypointIndex += 1
                newWaypoint.WaypointName = sender.text
                newWaypoint.WaypointLatitude = tmpObj.GeofenceLatitude
                newWaypoint.WaypointLongitude = tmpObj.GeofenceLongitude
                newWaypoint.WaypointType = "Geofence"
                WaypointsObj.Add(newWaypoint)
                Exit For
            End If
        Next
    End Sub
    Private Sub btnClearRoute_Click(sender As Object, e As EventArgs) Handles btnClearRoute.Click
        turnTabPage.Visible = False
        TabControl1.SelectedTab = mapTabPage
        dgWayPoints.Rows.Clear()
        WaypointsObj.Clear()
        btnSaveRoute.Visible = False
        GM_Browser.Document.InvokeScript("ClearRoutes")
    End Sub

    Private Sub dgWayPoints_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles dgWayPoints.DragDrop
        Try
            Dim p As System.Drawing.Point = dgWayPoints.PointToClient(New System.Drawing.Point(e.X, e.Y))
            dragIndex = dgWayPoints.HitTest(p.X, p.Y).RowIndex
            If (e.Effect = DragDropEffects.Move) Then
                Dim dragRow As DataGridViewRow = e.Data.GetData(GetType(DataGridViewRow))
                If dragRow.Index < 0 Then Exit Sub
                If dragIndex < 0 Then dragIndex = dgWayPoints.RowCount - 1
                dgWayPoints.Rows.RemoveAt(fromIndex)
                dgWayPoints.Rows.Insert(dragIndex, dragRow)
                dgWayPoints_ReOrder()
            End If
        Catch ex As ArgumentException

        End Try

    End Sub

    Private Sub dgWayPoints_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles dgWayPoints.DragOver
        e.Effect = DragDropEffects.Move
    End Sub


    Private Sub dgWayPoints_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgWayPoints.MouseDown
        fromIndex = dgWayPoints.HitTest(e.X, e.Y).RowIndex
        If fromIndex > -1 Then
            Dim dragSize As Size = SystemInformation.DragSize
            dragRect = New System.Drawing.Rectangle(New System.Drawing.Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize)
        Else
            dragRect = System.Drawing.Rectangle.Empty
        End If
    End Sub

    Private Sub dgWayPoints_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgWayPoints.MouseMove
        If (e.Button And MouseButtons.Left) = MouseButtons.Left Then
            If (dragRect <> System.Drawing.Rectangle.Empty _
            AndAlso Not dragRect.Contains(e.X, e.Y)) Then
                dgWayPoints.DoDragDrop(dgWayPoints.Rows(fromIndex), DragDropEffects.Move)
            End If
        End If
    End Sub
    Private Sub dgWayPoints_ReOrder()
        For Each row In dgWayPoints.Rows
            For Each tmpObj In WaypointsObj
                If tmpObj.WaypointName = row.Cells(0).Value Then
                    tmpObj.WaypointNumber = row.index
                End If
            Next
        Next
        WaypointsObj = WaypointsObj.OrderBy(Function(x) x.WaypointNumber).ToList()
    End Sub

    Private Sub btnCreateRoute_Click(sender As Object, e As EventArgs) Handles btnCreateRoute.Click
        Dim strRoutesArray As String = String.Empty
        Dim i As Integer
        If WaypointsObj.Count = 0 Then Exit Sub
        For Each tmpObj In WaypointsObj
            strRoutesArray += tmpObj.WaypointName & "|" & tmpObj.WaypointLatitude & "|" & tmpObj.WaypointLongitude
            If i < WaypointsObj.Count - 1 Then
                strRoutesArray += "~"
            End If
            i += 1
        Next
        GM_Browser.Document.InvokeScript("CalculateNewRoute", New String() {strRoutesArray})
        btnSaveRoute.Visible = True
    End Sub

    Private Sub txtAddress_KeyUp(sender As Object, e As KeyEventArgs) Handles txtAddress.KeyUp
        If txtAddress.Text.Length > 0 Then
            btnFindAddress.Enabled = True
        Else
            btnFindAddress.Enabled = False
        End If
    End Sub

    Private Sub btnFindAddress_Click(sender As Object, e As EventArgs) Handles btnFindAddress.Click
        GetGecodeData(txtAddress.Text)
    End Sub

    Private Sub GetGecodeData(ByVal strAddress As String)
        Dim locations As XmlNodeList
        Dim lng As Double
        Dim lat As Double
        Dim addresses As XmlNodeList
        Dim address As String
        Dim mystream As Stream = Nothing
        Dim mydoc As XmlDocument
        Dim myreq As HttpWebRequest
        Dim i, j, k As Integer
        Try
            If strAddress <> "" Then
                Dim urlString As String = "http://maps.googleapis.com/maps/api/geocode/xml?address=" & strAddress & ",&sensor=false" '&client=" & clsUtils.gme_ID
                myreq = HttpWebRequest.Create(urlString)
                Dim myresponse As HttpWebResponse = myreq.GetResponse
                mystream = myresponse.GetResponseStream
                Dim sr As New StreamReader(mystream)
                Dim mytext As String = ""
                mytext = sr.ReadToEnd
                sr.Close()
                If mytext.Contains("(1 - 10)") Then
                    MsgBox("Unable to find address, please modify your entry and try again", vbInformation, "MST Viewer - Find address")
                    Exit Sub
                End If
                mydoc = New XmlDocument
                mydoc.LoadXml(mytext)
                addresses = mydoc.GetElementsByTagName("formatted_address")
                locations = mydoc.GetElementsByTagName("location")
                address = addresses(0).InnerText
                lat = locations(0).SelectSingleNode("lat").InnerText
                lng = locations(0).SelectSingleNode("lng").InnerText
                If UseThisLocation = True Then
                    GM_Browser.Document.InvokeScript("AddAddressMarker", {address, lat, lng})
                    dgLocationResults.Visible = False
                    GM_Browser.Visible = True
                    txtAddress.Clear()
                    Exit Sub
                Else
                    Try
                        GM_Browser.Visible = False
                        dgLocationResults.Visible = True
                        dgLocationResults.Rows.Clear()
                        For i = 0 To addresses.Count - 1
                            Dim tmpResults(10, 10)
                            Dim tmpArray As String() = addresses(i).InnerText.Split(">")
                            If tmpArray.Length < 2 Then
                                dgLocationResults.Rows.Add(tmpArray(0))
                                k = k + 1
                            Else
                                For j = 0 To tmpArray.Length - 1
                                    If tmpArray.Length > 1 Then
                                        Dim tmpArray2 As String() = tmpArray(j).Split("<")
                                        If tmpArray2(0) <> "" And tmpArray2(0) <> "Did you mean:" Then
                                            dgLocationResults.Rows.Add(tmpArray2(0))
                                            k = k + 1
                                        End If
                                    Else
                                        dgLocationResults.Rows.Add(tmpArray(0))
                                        k = k + 1
                                    End If
                                Next
                            End If
                        Next

                    Catch ex As Exception
                        MsgBox("GetGeocodeData Error 1052 -- " & ex.Message, vbCritical, "MST Viewer - Find address")
                    End Try

                End If
            End If
        Catch ex As Exception
            MsgBox("GetGeocodeData Error 1058 -- " & ex.Message, vbCritical, "MST Viewer - Find address")
        End Try
    End Sub
    Private Sub SaveAddressWaypoint(ByVal address As String, ByVal lat As Double, lng As Double)
        If dgWayPoints.InvokeRequired Then
            Dim d As New SetdgWayPointsCallBack(AddressOf SaveAddressWaypoint)
            Me.Invoke(d, New Object() {address, lat, lng})
        Else
            dgWayPoints.Rows.Add(address)
            Dim newWaypoint As New ClsWayPoints
            newWaypoint.WaypointNumber = WaypointIndex
            WaypointIndex += 1
            newWaypoint.WaypointName = address
            newWaypoint.WaypointLatitude = lat
            newWaypoint.WaypointLongitude = lng
            newWaypoint.WaypointType = "Address"
            WaypointsObj.Add(newWaypoint)
        End If
    End Sub
    Private Sub dgLocationResults_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgLocationResults.CellDoubleClick
        UseThisLocation = True
        Dim thisLocation As String = dgLocationResults.CurrentCell.Value
        GetGecodeData(thisLocation)
    End Sub
    Public Sub UpdateTurns(ByVal turnsList As String, ByVal totalPath As String, ByVal wayPointLatLngs As String)
        If TabControl1.InvokeRequired Then
            Dim d As New SetUpdateTurnsCallBack(AddressOf UpdateTurns)
            Me.Invoke(d, New Object() {turnsList, totalPath, wayPointLatLngs})
        Else
            Dim tmpTurnsArray() As String = turnsList.Split("~")
            Dim i As Integer
            Dim j As Integer
            Dim tmpWayPointsArray() As String = wayPointLatLngs.Split("~")
            Dim routeDistance As Double
            Dim routeTime As Integer
            Try
                For i = 0 To tmpWayPointsArray.Length - 1
                    Dim tmpPointsArray() As String = tmpWayPointsArray(i).Split("|")
                    WaypointsObj(i).WaypointIcon = tmpPointsArray(0)
                    WaypointsObj(i).WaypointLatitude = tmpPointsArray(1)
                    WaypointsObj(i).WaypointLongitude = tmpPointsArray(2)
                Next
            Catch ex As Exception
            End Try
            myRouteList = New List(Of ClsTurns)
            TotalRoutePath = totalPath
            For i = 0 To tmpTurnsArray.Length - 1
                Dim tmpTurnDetails() As String = tmpTurnsArray(i).Split("|")
                Try
                    Dim turnObj As New ClsTurns
                    If IsNumeric(tmpTurnDetails(0)) Then
                        turnObj.Turn = tmpTurnDetails(0)
                    End If
                    If i = 0 Then
                        turnObj.Location = "Depart " & WaypointsObj(j).WaypointName & ". " & tmpTurnDetails(1)
                        WaypointsObj(j).WaypointDistance = routeDistance
                        WaypointsObj(j).WaypointTime = routeTime
                        j = j + 1
                    ElseIf tmpTurnDetails(1).Contains("Depart") Then
                        Dim departLoc As String = Mid(tmpTurnDetails(1), 7)
                        turnObj.Location = "Depart " & WaypointsObj(j).WaypointName & departLoc
                        WaypointsObj(j).WaypointDistance = routeDistance
                        WaypointsObj(j).WaypointTime = routeTime
                        j = j + 1
                    ElseIf tmpTurnDetails(1).Contains("Destination will be on") Then
                        turnObj.Location = tmpTurnDetails(1).Replace("Destination", WaypointsObj(j).WaypointName)
                        WaypointsObj(j).WaypointDistance = routeDistance
                        WaypointsObj(j).WaypointTime = routeTime
                        j = j + 1
                        'turnObj.Location = "Arrive at " & WaypointsObj(j).WaypointName
                    Else
                        turnObj.Location = tmpTurnDetails(1)
                    End If
                    If IsNumeric(tmpTurnDetails(2)) Then
                        routeDistance = routeDistance + tmpTurnDetails(2)
                        If tmpTurnDetails(2) < 1 Then
                            If DistUnits = "Miles" Then
                                turnObj.Distance = (tmpTurnDetails(2) * 1000) & " yards"
                            Else
                                turnObj.Distance = (tmpTurnDetails(2) * 1000) & " metres"
                            End If
                        Else
                            turnObj.Distance = tmpTurnDetails(2) & " " & DistUnits
                        End If
                    Else
                        turnObj.Distance = tmpTurnDetails(2)
                    End If
                    If i = 50 Then
                        i = 50
                    End If
                    turnObj.Time = tmpTurnDetails(3)
                    routeTime = routeTime + TimeStringToSeconds(tmpTurnDetails(3))
                    myRouteList.Add(turnObj)


                Catch ex As Exception
                End Try
            Next
            Try
                TabControl1.TabPages.Remove(turnTabPage)
            Catch ex As Exception

            End Try
            TabControl1.TabPages.Insert(1, turnTabPage)
            turnTabPage.Visible = True
            BindStatsGrid(myRouteList)
        End If
    End Sub
    Private Sub BindStatsGrid(ByVal myTurns As List(Of ClsTurns))
        Try
            Dim i As Integer
            If dgTurns.InvokeRequired Then
                Dim d As New SetdgTurnsCallBack(AddressOf BindStatsGrid)
                Me.Invoke(d, New Object() {myTurns})
            Else
                With dgTurns
                    'Turn
                    .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    'Location
                    .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    'Distance
                    .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    'Time
                    .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                End With

                dgTurns.Rows.Clear()
                i = 0
                For Each turnObj In myTurns
                    dgTurns.Rows.Add(New String() {turnObj.Turn, turnObj.Location, turnObj.Distance, turnObj.Time})
                    i += 1
                Next
            End If
        Catch ex As Exception
            MsgBox("Error: BindTurnsGrid - " & ex.Message, vbCritical, "MST Viewer - BindTurnsGrid")
        End Try
    End Sub
    Private Sub dgWayPoints_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgWayPoints.CellDoubleClick

        Dim thisWayPoint As String = dgWayPoints.CurrentCell.Value
        For Each obj In WaypointsObj
            If obj.WaypointName = thisWayPoint Then
                WaypointsObj.Remove(obj)
                Exit For
            End If
        Next

        dgWayPoints.Rows.Remove(dgWayPoints.CurrentRow)
    End Sub
    Private Sub btnSaveRoute_Click(sender As Object, e As EventArgs) Handles btnSaveRoute.Click

        Dim polyCoords As String = String.Empty
        Dim strWaypoints As String = String.Empty
        Dim routeName As String = String.Empty
        For Each obj In WaypointsObj
            strWaypoints += "[" & obj.WaypointName & "|" & FormatNumber(obj.WaypointLatitude, 5) & "|" & FormatNumber(obj.WaypointLongitude, 5) & "|" & obj.WaypointType & "|" & obj.WaypointDistance & "|" & obj.WaypointTime & "]"
        Next
        If IsModifyRoute = False Then
            routeName = InputBox("Please enter Route Name", "MST Viewer - Save Route")
            If routeName.Length < 1 Then
                MsgBox("Route Name cannot be blank.", MsgBoxStyle.Information, "MST Viewer - Save Route")
                Exit Sub
            End If
        Else
            routeName = ModifyRouteName
        End If
        Dim routePath As String = TotalRoutePath.Replace(",", "")
        If routePath.Length > 0 Then
            Dim routeArray1 As String() = routePath.Split(New String() {")("}, StringSplitOptions.RemoveEmptyEntries)
            For i = 0 To routeArray1.Length - 1
                Dim tmpArray As String() = routeArray1(i).Split(" ")
                Dim strLat As String = tmpArray(0).Replace("(", "")
                Dim strLng As String = tmpArray(1).Replace(")", "")
                Dim dblLat As Double = FormatNumber((strLat), 5)
                Dim dblLng As Double = FormatNumber((strLng), 5)
                If i < routeArray1.Length - 1 Then
                    polyCoords += dblLng & " " & dblLat & ","
                Else
                    polyCoords += dblLng & " " & dblLat
                End If
            Next
        End If


        If routeName <> "" And polyCoords <> "" Then
            'Create the polyline in GeoPolygons
            Try
                Dim myConnection As New SqlConnection(MapInfoFleetConnectionString)
                Dim mySQLCommand As SqlClient.SqlCommand
                myConnection.Open()
                If IsModifyRoute = False Then
                    mySQLCommand = New SqlClient.SqlCommand("MST_SaveRoutePolyline", myConnection)
                    With mySQLCommand
                        .CommandTimeout = 300
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add("@PolyName", SqlDbType.NVarChar, 255).Value = routeName
                        .Parameters.Add("@UserId", SqlDbType.NVarChar, 50).Value = "MSTViewer"
                        .Parameters.Add("@GeoCoords", SqlDbType.NVarChar, -1).Value = polyCoords
                        .Parameters.Add("@WayPoints", SqlDbType.NVarChar, -1).Value = strWaypoints
                        .Parameters.Add("@Company", SqlDbType.NVarChar, 50).Value = MapCompany
                    End With
                Else
                    mySQLCommand = New SqlClient.SqlCommand("MST_UpdateRoutePolyline", myConnection)
                    With mySQLCommand
                        .CommandTimeout = 300
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add("@PolyName", SqlDbType.NVarChar, 255).Value = routeName
                        .Parameters.Add("@UserId", SqlDbType.NVarChar, 50).Value = "MSTViewer"
                        .Parameters.Add("@GeoCoords", SqlDbType.NVarChar, -1).Value = polyCoords
                        .Parameters.Add("@WayPoints", SqlDbType.NVarChar, -1).Value = strWaypoints
                        .Parameters.Add("@GeoId", SqlDbType.Int).Value = ModifyRouteID
                    End With
                End If


                Dim mySqlDataReader As SqlClient.SqlDataReader
                mySqlDataReader = mySQLCommand.ExecuteReader(CommandBehavior.Default)
                mySqlDataReader.Close()
                myConnection.Close()
                MsgBox("Route: " & routeName & " saved.", vbInformation, "MST Viewer - Save Route")
                ReloadAllGeofences()
            Catch ex As Exception
                MsgBox("Route: " & routeName & " was not saved. Error: " & ex.Message, vbCritical, "MST Viewer - Save Route Failed")
            End Try
        End If
        ModifyRouteID = 0
        ModifyRouteName = String.Empty
        ReloadAllGeofences()
    End Sub
    Private Sub btnShowSelectedRoutes_Click(sender As Object, e As EventArgs)
        ShowSelectedRoutes()
    End Sub
    Private Sub ShowSelectedRoutes()
        Dim selectedRoutes As String = String.Empty
        Dim nodeName As String = String.Empty
        Try
            For Each parentNode In tvRoutes.Nodes
                If parentNode.Nodes.Count > 0 Then
                    For Each childNode As TreeNode In parentNode.Nodes
                        If childNode.Checked = True Then
                            If parentNode.Text = "Allocated" Then
                                Try
                                    nodeName = String.Empty
                                    Dim tmpText() As String = childNode.Text.Split("(")
                                    If tmpText.Length > 1 Then
                                        nodeName = Trim(tmpText(0))
                                    End If
                                Catch ex As Exception

                                End Try
                            Else
                                nodeName = Trim(childNode.Text)
                            End If

                            selectedRoutes += parentNode.Text & "~" & nodeName & "|"
                        End If

                    Next

                End If
            Next

            If selectedRoutes.Length > 1 Then
                selectedRoutes = Strings.Left(selectedRoutes, Len(selectedRoutes) - 1)
                GM_Browser.Document.InvokeScript("showSelectedRoutes", New String() {selectedRoutes})
            Else
                GM_Browser.Document.InvokeScript("ClearRoutes")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub tvRoutes_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles tvRoutes.AfterCheck
        ShowSelectedRoutes()
    End Sub
    Private Sub mnuClearRoutes_Click(sender As Object, e As EventArgs) Handles mnuClearRoutes.Click
        IsModifyRoute = False
        GM_Browser.Document.InvokeScript("ClearRoutes")
    End Sub
    Private Sub mnuDeleteRoute_Click(sender As Object, e As EventArgs) Handles mnuDeleteRoute.Click
        Try
            Dim selectedRoute As String = tvRoutes.SelectedNode.Text
            If selectedRoute = "" Then
                MsgBox("Please select route to delete.", MsgBoxStyle.Information, "Delete Route")
                Exit Sub
            End If

            selectedRoute = tvRoutes.SelectedNode.Text

            Dim result As Integer = MessageBox.Show("Delete route " & selectedRoute & vbCrLf & vbCrLf & ". Do you wish to continue ?", "MS Track Fleet Viewer - Delete Route", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then
                Exit Sub
            ElseIf result = DialogResult.Yes Then
                DeletePolyRoute(selectedRoute)
            End If
        Catch ex As Exception

        End Try
        ReloadAllGeofences()
        GM_Browser.Document.InvokeScript("ClearRoutes")
    End Sub
    Private Sub DeletePolyRoute(data As String)
        ClsMstGoogleMaps.reloadGeofences = True
        Dim myConnection As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
        Dim mySqlCommand As New SqlClient.SqlCommand
        Dim thisRouteName As String = data
        Dim thisRouteId As Integer
        For Each obj In PolyGeoFences
            If obj.GeoFenceName = thisRouteName Then
                thisRouteId = obj.GeoId
                Exit For
            End If
        Next
        Try
            mySqlCommand = New SqlClient.SqlCommand("MST_DeletePolygonGeofence", myConnection)
            With mySqlCommand
                .CommandTimeout = 300
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@GeoId", SqlDbType.Int).Value = thisRouteId
            End With
            myConnection.Open()
            Dim mySqlDataReader As SqlClient.SqlDataReader
            mySqlDataReader = mySqlCommand.ExecuteReader(CommandBehavior.Default)
            mySqlDataReader.Close()
            myConnection.Close()
            myConnection.Dispose()
            MsgBox(thisRouteName & " deleted.", MsgBoxStyle.Information, "MS Track Fleet Viewer - Delete Route")
            GetPolygonGeofences()
            GM_Browser.Document.InvokeScript("SetMapOptions", {VehicleArrayStr, 1, 0, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})
            GM_Browser.Document.InvokeScript("reloadGeofences")
        Catch ex As Exception
            MsgBox(thisRouteName & " was not deleted. Error: " & ex.Message, MsgBoxStyle.Critical, "MS Track Fleet Viewer - Delete Route Failed")

        End Try
    End Sub
    Private Sub mnuModifyAllocatedRoute_Click(sender As Object, e As EventArgs) Handles mnuModifyAllocatedRoute.Click
        ModifyRoute()
    End Sub
    Private Sub mnuModifyRoute_Click(sender As Object, e As EventArgs) Handles mnuModifyRoute.Click
        ModifyRoute()
    End Sub
    Private Sub ModifyRoute()
        Try
            WaypointsObj.Clear()
            selectedRoute = tvRoutes.SelectedNode.Text
            Dim wayPoints As String = String.Empty
            ModifyRouteName = String.Empty
            Dim strRoutesArray As String = String.Empty

            Dim i As Integer
            If selectedRoute = "" Then
                MsgBox("Please select route to modify.", MsgBoxStyle.Information, "Modify Route")
                Exit Sub
            End If
            selectedRoute = tvRoutes.SelectedNode.Text
            Try
                Dim tmpText() As String = selectedRoute.Split("(")
                If tmpText.Length > 1 Then
                    ModifyRouteName = Trim(tmpText(0))
                Else
                    ModifyRouteName = selectedRoute
                End If
            Catch ex As Exception
                ModifyRouteName = selectedRoute
            End Try

            For Each obj In PolyGeoFences
                If obj.GeoFenceName = ModifyRouteName Then
                    ModifyRouteID = obj.GeoId
                    Exit For
                End If
            Next
            Dim myConnection As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
            Dim myReader As SqlClient.SqlDataReader
            Dim sqlstr As String = "Select Waypoints from Geopolygons Where GeoId = " & ModifyRouteID
            myConnection.Open()
            Dim mySQLCommand As New SqlClient.SqlCommand(sqlstr, myConnection)
            myReader = mySQLCommand.ExecuteReader(CommandBehavior.Default)
            wayPoints = ""
            With myReader
                While .Read = True
                    If Not .IsDBNull(0) Then
                        wayPoints = .GetString(0)
                    End If
                End While
            End With
            myConnection.Close()
            myReader.Close()
            dgWayPoints.Rows.Clear()

            Try

                Dim strWayPoints() As String = wayPoints.Split("]")
                If strWayPoints.Length > 1 Then
                    For i = 0 To strWayPoints.Length - 2
                        strWayPoints(i) = strWayPoints(i).Replace("[", "")
                        strWayPoints(i) = strWayPoints(i).Replace("]", "")
                        Dim obj As New ClsWayPoints
                        Dim tmpWayPointArray() As String = strWayPoints(i).Split("|")
                        obj.WaypointNumber = i
                        obj.WaypointName = tmpWayPointArray(0)
                        dgWayPoints.Rows.Add(obj.WaypointName)
                        obj.WaypointLatitude = CDbl(tmpWayPointArray(1))
                        obj.WaypointLongitude = CDbl(tmpWayPointArray(2))
                        obj.WaypointType = tmpWayPointArray(3)
                        obj.WaypointDistance = tmpWayPointArray(4)
                        obj.WaypointTime = tmpWayPointArray(5)
                        WaypointsObj.Add(obj)
                    Next
                End If
                'now display the create route screen
                WaypointIndex = 0
                LoadWayPointCombos()
                mainContainer1.Panel1Collapsed = False
                pnlAddress.Visible = True
                pnlClickMap.Visible = True
                pnlGeofences.Visible = True
                pnlSites.Visible = True
                pnlTop.Visible = True
                pnlVehicles.Visible = True
                pnlWaypointGrid.Visible = True
                pnlWaypointLabel.Visible = True
                pnlShowRoutes.Visible = False
                pnlCreateRoutes.Visible = True
                dgWayPoints.Columns.Item(0).Width = dgWayPoints.Width
            Catch ex As Exception

            End Try
            Try
                strRoutesArray = ""
                i = 0
                If WaypointsObj.Count = 0 Then Exit Sub
                For Each tmpObj In WaypointsObj
                    strRoutesArray += tmpObj.WaypointName & "|" & tmpObj.WaypointLatitude & "|" & tmpObj.WaypointLongitude
                    If i < WaypointsObj.Count - 1 Then
                        strRoutesArray += "~"
                    End If
                    i += 1
                Next
                IsModifyRoute = True
                GM_Browser.Document.InvokeScript("ModifyRoute", New String() {strRoutesArray})
                btnSaveRoute.Visible = True
            Catch ex As Exception

            End Try


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnWayPointFromMap_Click(sender As Object, e As EventArgs) Handles btnWayPointFromMap.Click
        GM_Browser.Document.InvokeScript("GetWayPointFromMap")
    End Sub
    Public Sub SaveWaypointFromMap(ByVal lat As Double, ByVal lng As Double)
        If dgWayPoints.InvokeRequired Then
            Dim d As New SetdgWayPointsCallBackWayPoint(AddressOf SaveWaypointFromMap)
            Me.Invoke(d, New Object() {lat, lng})
        Else
            Try
                Dim waypointName As String
ReEnterName:
                waypointName = InputBox("Please enter Waypoint Name", "MST Viewer - Save Route")
                If waypointName.Length < 1 Then
                    Exit Sub
                End If
                dgWayPoints.Rows.Add(waypointName)
                Dim newWaypoint As New ClsWayPoints
                newWaypoint.WaypointNumber = WaypointIndex
                WaypointIndex += 1
                newWaypoint.WaypointName = waypointName
                newWaypoint.WaypointLatitude = lat
                newWaypoint.WaypointLongitude = lng
                newWaypoint.WaypointType = "WayPoint"
                WaypointsObj.Add(newWaypoint)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub mnuImportRouteExcel_Click(sender As Object, e As EventArgs) Handles mnuImportRouteExcel.Click
        Dim myForm As New frmImportExcel
        myForm.ShowDialog()

        Try
            If dsExcelRoutes.Tables(0).Rows.Count > 0 Then
                WaypointsObj.Clear()
                WaypointIndex = 0
                dgWayPoints.Rows.Clear()

                For Each row As DataRow In dsExcelRoutes.Tables(0).Rows
                    dgWayPoints.Rows.Add(row(0))
                    Dim newWaypoint As New ClsWayPoints
                    newWaypoint.WaypointNumber = WaypointIndex
                    WaypointIndex += 1
                    newWaypoint.WaypointName = row(0)
                    newWaypoint.WaypointLatitude = row(1)
                    newWaypoint.WaypointLongitude = row(2)
                    newWaypoint.WaypointType = "ImportExcel"
                    WaypointsObj.Add(newWaypoint)
                Next
                mainContainer1.Panel1Collapsed = False
                pnlAddress.Visible = True
                pnlClickMap.Visible = True
                pnlGeofences.Visible = True
                pnlSites.Visible = True
                pnlTop.Visible = True
                pnlVehicles.Visible = True
                pnlWaypointGrid.Visible = True
                pnlWaypointLabel.Visible = True
                pnlShowRoutes.Visible = False
                pnlCreateRoutes.Visible = True
                Google_IsRouteManagement = True
                dgWayPoints.Columns.Item(0).Width = dgWayPoints.Width
                GM_Browser.Document.InvokeScript("ClearRoutes")
            Else

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkOptimizedRoute_CheckedChanged(sender As Object, e As EventArgs) Handles chkOptimizedRoute.CheckedChanged
        Google_IsRouteOptimized = False
        If chkOptimizedRoute.Checked = True Then
            Google_IsRouteOptimized = True
        End If
        GM_Browser.Document.InvokeScript("SetMapOptions", {VehicleArrayStr, 1, 0, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})

    End Sub

    Private Sub tvRoutes_MouseDown(sender As Object, e As MouseEventArgs) Handles tvRoutes.MouseDown
        Dim NodeClicked As TreeNode
        NodeClicked = tvRoutes.GetNodeAt(e.X, e.Y)
        tvRoutes.SelectedNode = NodeClicked
    End Sub

    Private Sub mnuShowTurnsAllocated_Click(sender As Object, e As EventArgs) Handles mnuShowTurnsAllocated.Click
        ModifyRoute()
        TabControl1.SelectedTab = turnTabPage
    End Sub

    Private Sub mnuShowTurnsUnallocated_Click(sender As Object, e As EventArgs) Handles mnuShowTurnsUnallocated.Click
        ModifyRoute()
        TabControl1.SelectedTab = turnTabPage
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Dim export = New clsOutputToExcel
        export.ExportTurnsToExcel(myRouteList)
    End Sub

    Public Sub ClearModifyRoute()
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf ClearModifyRoute)
            Me.Invoke(d, New Object)
        Else
            btnSaveRoute.Visible = False
            IsModifyRoute = False
        End If
    End Sub

    Private Sub frmMSTGoogleMap_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        FleetFormExists = False
        Me.Hide()
        'CloseApp()
    End Sub

    Private Sub mnuPrint_Click_1(sender As Object, e As EventArgs) Handles mnuPrint.Click
        GM_Browser.ShowPrintPreviewDialog()
    End Sub

    Private Sub frmMSTGoogleMap_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason <> CloseReason.WindowsShutDown And e.CloseReason <> CloseReason.ApplicationExitCall Then
            e.Cancel = True
        End If
        FleetFormExists = False
        ' MyBase.OnFormClosing(e)
    End Sub

    Private Sub CheckInterExplorerVersion()
        Dim version = FileVersionInfo.GetVersionInfo("c:\windows\system32\ieframe.dll")
        Dim executablePath As String = Environment.GetCommandLineArgs()(0)
        Dim executableName As String = System.IO.Path.GetFileName(executablePath)
        If version.ProductMajorPart <> 11 Then
            MsgBox("The installed version of Internet Explorer is " & version.ProductVersion & " - Please update to Internet Explorer to IE 11.", MsgBoxStyle.Information, "")
            Exit Sub
        End If
        Dim readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\Feature_Browser_Emulation", executableName, Nothing)

        'Remove this
        ' MsgBox("Browser Version : " & readValue, MsgBoxStyle.Information, "MSTGoogleMaps - Browser Emulation")
        Try


            If IsNothing(readValue) Then
                Dim key As String = "SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"
                Dim registrybrowser As RegistryKey = Registry.CurrentUser.OpenSubKey(key, True)
                If registrybrowser Is Nothing Then
                    Dim registryFolder As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl", True)

                    If registryFolder Is Nothing Then
                        Dim registryFolderParent As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\Main", True)
                        registryFolderParent.CreateSubKey("FeatureControl")
                        registryFolder = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl", True)
                    End If
                    registrybrowser = registryFolder.CreateSubKey("FEATURE_BROWSER_EMULATION")
                End If
                registrybrowser.SetValue(executableName, &H2AF8, RegistryValueKind.DWord)
                registrybrowser.Close()
                readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\Feature_Browser_Emulation", executableName, Nothing)
                'MsgBox("Browser Version : " & readValue, MsgBoxStyle.Information, "MSTGoogleMaps - Browser Emulation")
            End If

        Catch ex As Exception
            MsgBox("Set Browser Version Error : " & ex.Message, MsgBoxStyle.Critical, "MSTGoogleMaps - Browser Emulation")
        End Try
    End Sub
End Class