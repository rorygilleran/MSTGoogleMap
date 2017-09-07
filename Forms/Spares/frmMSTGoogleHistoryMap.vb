
Imports System
Imports System.Windows.Forms
Imports System.Security.Permissions
Imports MSTGoogleMap.ClsMstGoogleMaps
Imports MSTGoogleMap.clsHistoryMapCallback
Imports System.Threading

<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
<Runtime.InteropServices.ComVisibleAttribute(True)>
Public Class frmMSTGoogleHistoryMap
    Inherits System.Windows.Forms.Form

    Public Event myHistoryFormSettings(ByVal myX As Integer, ByVal myY As Integer, ByVal myW As Integer, ByVal myH As Integer)
    Public Event AppClosing(ByVal data As Boolean)
    Public Event CloseMe(ByVal data As Boolean)
    Public Event GoogleCreateSite(ByVal ptLat As Double, ByVal ptLong As Double)
    Public Event VehiclesCloseToPoint(ByVal ptLat As Double, ByVal ptLong As Double)
    Public Event UpdateGoogleMapType(ByVal gMapType As String)
    Public Event ClearGoogleMap(ByVal value As Boolean)
    Public Event GoogleMapId(ByVal data As String)
    Public Shared FormExists As Boolean = False
    Public HistoryArrayStr As String = Nothing
    Private myPointsObj As clsBIS_GoogleMaps.clsPlotPoints
    Private WithEvents GM_HistoryBrowser As ChromiumWebBrowser

    Public Shared ReadOnly Property InstanceExists() As Boolean
        Get
            ' Access shared members through the Class name, not an instance.
            Return FormExists
        End Get
    End Property

    Public WriteOnly Property FormSize() As clsBIS_GoogleMaps.clsHistoryFormSettings
        Set(ByVal Value As clsBIS_GoogleMaps.clsHistoryFormSettings)
            Try
                Me.Location = New System.Drawing.Point(Value.FormX, Value.FormY)
                Me.Width = Value.FormWidth
                Me.Height = Value.FormHeight
                Me.Text = String.Format("MST Google-Map ({0})", Me.ProductVersion)
            Catch ex As Exception
            End Try

        End Set

    End Property
    Public Property StartMapForm() As Boolean
        Set(ByVal Value As Boolean)
            If Value = True Then
                Try
                    If historyMapHandlersAdded = False Then
                        'Add the event handlers

                        'RouteEvents
                        AddHandler clsHistoryMapCallback.evtHistory_ClearModifyRoute, AddressOf ClearModifyRouteFlag
                        AddHandler clsHistoryMapCallback.evtHistory_NoRoutesCreated, AddressOf NoRoutesCreated
                        'AddHandler clsHistoryMapCallback.evtHistory_History_ShowRoutes, AddressOf xxxxxxx!!!!

                        'Polygon Events
                        AddHandler clsHistoryMapCallback.evtHistory_DeletePolyGeofence, AddressOf DeletePolyGeofence
                        AddHandler clsHistoryMapCallback.evtHistory_UnableToDeletePolygon, AddressOf UnableToDeletePolygon
                        AddHandler clsHistoryMapCallback.evtHistory_SavePolygonGeofence, AddressOf SavePolygonGeofence

                        'General Events
                        AddHandler clsHistoryMapCallback.evtHistory_SaveMapBounds, AddressOf SaveMapBounds
                        AddHandler clsHistoryMapCallback.evtHistory_UpdateGoogleMapType, AddressOf MapTypeChanged
                        AddHandler clsHistoryMapCallback.evtHistory_CreateSite, AddressOf CreateSite
                        historyMapHandlersAdded = True
                    End If
                    'Start the map display
                    Google_DisplayMap()

                Catch ex As Exception
                    'clsEventLog.writeEventLog("StartMapForm: Error 97: " & ex.Message, "Error")
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
                    RaiseEvent myHistoryFormSettings(Me.Left, Me.Top, Me.Width, Me.Height)
                Catch exc As Exception
                End Try
            End If

        End Set

    End Property        ' GetParameters
    Public WriteOnly Property ClearMap() As Boolean
        Set(ByVal Value As Boolean)
            GM_HistoryBrowser.ExecuteScriptAsync("clear_Google_Overlays();")
        End Set
    End Property
    Public WriteOnly Property CloseForm() As Boolean
        Set(ByVal Value As Boolean)
            If Value = True Then
                Me.Dispose()
            End If
        End Set
    End Property


    Private Sub frmMSTGoogleMap_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Kill(HistoryHTML)
        Catch ex As Exception
        End Try
        CloseApp()
    End Sub


    Private Async Sub Google_DisplayMap()
        Dim result As JavascriptResponse
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf Google_DisplayMap)
            Me.Invoke(d, New Object)
        Else
            If Me.WindowState = FormWindowState.Minimized Then
                Exit Sub
            End If
            Try
                'If IsMSTrackFlag = True Then
                mnuRoutes.Visible = False
                'Else
                '    mnuRoutes.Visible = True
                'End If
                TabControl1.TabPages.Remove(turnTabPage)
                mainContainer1.Panel1Collapsed = True
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

                Select Case Google_MapType
                    Case "Map"
                    Case "Satellite"
                    Case "Hybrid"
                    Case "Terrain"
                    Case Else
                End Select
                HistoryArrayStr = ""
                If Not thisGoogleHistoryObj Is Nothing Or Google_IsBufferZone = True Then
                    TopMost = False
                    For i = 0 To NumofPlots - 1
                        Try
                            With thisGoogleHistoryObj(i)
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
                                ' End Try
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
                                HistoryArrayStr = HistoryArrayStr & .RegNo & "|" & .VehDesc & "|" & .Latitude & "|" & .Longitude & "|" & tmpDate & "|" & (CLng(.Speed)) & " " & tmpDistType & " " & .Heading &
                                    "|" & thisLocation & "|" & IgnStatus & "|" & Trim(.Heading) & "|" & thisEventDesc & "|" & thisOdometer & "|" & thisDriver & "|" & .VehicleType & "|" & .Speed
                                If i < NumofPlots - 1 Then
                                    HistoryArrayStr = HistoryArrayStr & "~"
                                End If
                            End With

                        Catch ex As Exception
                            MsgBox("Error processing record " & i & "  : " & ex.Message, MsgBoxStyle.Critical, "Display Google Map")
                        End Try
                    Next
                    LoadGeofenceArray()
                    If IsMSTrackFlag = False Then
                        PlotGeoFences()
                        PlotPolygonRoutes(MapCompany, "*")
                        GetPolygonRoutes(MapCompany, "*")
                    End If
                    PlotSites()
                    If NumofPlots = 1 Then
                        Me.Text = "MST Google-Map (" & Me.ProductVersion & ")" & " - Vehicle " & thisGoogleHistoryObj(0).RegNo & " - " & thisGoogleHistoryObj(0).VehDesc
                    Else
                        Me.Text = String.Format("MST Google-Map ({0})", Me.ProductVersion)
                    End If
                    If Google_IsHistory = True Then
                        'LablesToolStripMenuItem.Visible = True
                        Google_UseLabels = True
                        Me.Text = "MST Google-Map (" & Me.ProductVersion & ")" & " - Plotting History for Vehicle " & thisGoogleHistoryObj(0).RegNo & " - " & thisGoogleHistoryObj(0).VehDesc
                    Else
                        'LablesToolStripMenuItem.Visible = True
                    End If
                    If frmMSTGoogleHistoryMap.FormExists = True Then
                        ' ***************  Changed 20160906 RJG - CEFSharp
                        result = Await GM_HistoryBrowser.EvaluateScriptAsync("SetMapOptions", {HistoryArrayStr, 1, 1, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})

                    Else
                        Dim str_HTML As String = CreateMapHTML(thisGoogleMapKey)
                        FileOpen(1, HistoryHTML, OpenMode.Output, OpenAccess.Write, OpenShare.Shared)
                        Print(1, str_HTML)
                        FileClose(1)
                        Try
                            If CefSharp.Cef.IsInitialized = False Then
                                Dim settings As New CefSharp.CefSettings()
#If DEBUG Then
                                settings.RemoteDebuggingPort = 8088
#End If
                                CefSharp.Cef.Initialize(settings)
                            End If
                        Catch ex As Exception
                            MsgBox("Cef.Initialize: Error 323: " & ex.Message, MsgBoxStyle.Critical, "MST Google Map - History")
                            Exit Sub
                        End Try
                        GM_HistoryBrowser = New ChromiumWebBrowser(HistoryHTML) With {
                            .Dock = DockStyle.Fill}
                        GM_HistoryBrowser.RegisterJsObject("external", New clsHistoryMapCallback())
                        pnlWebBrowser.Controls.Add(GM_HistoryBrowser)
                        frmMSTGoogleHistoryMap.FormExists = True
                        Me.Show()
                    End If
                Else
                    Me.Hide()
                End If
            Catch ex As Exception
                MsgBox("Error 242: Display History Map - " & ex.Message, vbCritical, "MST Google Map - History Map")
            End Try
        End If

    End Sub

    Private Sub CreateSite(ByVal message As String)
        Try
            Dim locArray() As String = message.Split(",")
            Dim MapLat As Double = CDbl(locArray(0))
            Dim MapLong As Double = CDbl(locArray(1))
            RaiseEvent GoogleCreateSite(MapLat, MapLong)
        Catch ex As Exception

        End Try

    End Sub
    Public Sub SaveMapBounds(ByVal centerLat As Double, ByVal centerLng As Double, ByVal mapZoom As Integer)
        'Added 20160105 RJG

    End Sub
    Private Sub MapTypeChanged(ByVal NewMapType As String)
        RaiseEvent UpdateGoogleMapType(NewMapType)
    End Sub
    Private Sub CloseApp()
        Try
            If Not Me.WindowState = Windows.Forms.FormWindowState.Minimized Then
                RaiseEvent myHistoryFormSettings(Me.Left, Me.Top, Me.Width, Me.Height)
            End If
            Me.Hide()

            'Try
            '    GM_HistoryBrowser.Dispose()
            'Catch ex As Exception

            'End Try
            frmMSTGoogleHistoryMap.FormExists = False
            Application.DoEvents()
            Me.Dispose()
        Catch ex As Exception
        End Try
    End Sub     ' CloseApp

    Private Sub mnuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClose.Click
        Try
            Kill(HistoryHTML)

            CloseApp()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub mnuStartDistCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStartDistCalc.Click
        mnuEndDistCalc.Enabled = True
        mnuClearDistCalc.Enabled = True
        GM_HistoryBrowser.ExecuteScriptAsync("addDistanceListeners();")
    End Sub

    Private Sub mnuEndDistCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEndDistCalc.Click
        mnuEndDistCalc.Enabled = False
        mnuClearDistCalc.Enabled = True
        GM_HistoryBrowser.ExecuteScriptAsync("endDistanceListeners();")
    End Sub

    Public Sub CreateBufferZoneFromPoints(ByVal pointData As clsBIS_GoogleMaps.clsPlotPoints)
        myPointsObj = pointData

        Dim NumberPoints As Integer = myPointsObj.NumberOfPoints

        Dim i As Integer

        str_BufferZoneArray = ""
        For i = 0 To NumberPoints - 1
            myPointsObj.PointNumber = i
            str_BufferZoneArray &= myPointsObj.PtLatitude & ","
            str_BufferZoneArray &= myPointsObj.PtLongitude & "~"
        Next
        Google_IsBufferZone = True
        Google_DisplayMap()
        'GM_Browser.Document.InvokeScript("BuildBufferZoneArray", New String() {str_BufferZoneArray})

    End Sub        ' CreateBufferZoneFromPoints
    Private Sub mnuClearMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClearMap.Click
        mnuDisplayGeofence.Text = "Display Site Geofences"
        mnuEndDistCalc.Enabled = False
        mnuClearDistCalc.Enabled = False
        mainContainer1.Panel1Collapsed = True
        If GM_HistoryBrowser.IsBrowserInitialized = True Then
            GM_HistoryBrowser.GetMainFrame.ExecuteJavaScriptAsync("ClearMap();")
        End If
        RaiseEvent ClearGoogleMap(True)
    End Sub


    Private Sub mnuDrawLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDrawLine.Click
    End Sub

    Private Sub mnuClearDistCalc_Click(sender As System.Object, e As System.EventArgs) Handles mnuClearDistCalc.Click
        GM_HistoryBrowser.ExecuteScriptAsync("clearDistanceListeners();")
    End Sub

    Private Sub CreateSiteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles mnuCreateSite.Click
        GM_HistoryBrowser.ExecuteScriptAsync("AllowCreateGeofence();")
    End Sub

    Private Sub frmMSTGoogleHistoryMap_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If CefSharp.Cef.IsInitialized = False Then
                Dim settings As New CefSharp.CefSettings()
#If DEBUG Then
                settings.RemoteDebuggingPort = 8088
#End If
                CefSharp.Cef.Initialize(settings)
            End If
        Catch ex As Exception
            MsgBox("Cef.Initialize: Error 361: " & ex.Message, MsgBoxStyle.Critical, "MST Google Map - History")
            Exit Sub
        End Try
    End Sub
    Private Sub frmMSTGoogleMap_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        If Not Me.WindowState = Windows.Forms.FormWindowState.Minimized Then
            RaiseEvent myHistoryFormSettings(Me.Left, Me.Top, Me.Width, Me.Height)
        End If
    End Sub
    Private Sub frmMSTGoogleMap_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        If Not Me.WindowState = Windows.Forms.FormWindowState.Minimized Then
            RaiseEvent myHistoryFormSettings(Me.Left, Me.Top, Me.Width, Me.Height)
        End If
    End Sub
    Private Sub frmMSTGoogleMap_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        If Not Me.WindowState = Windows.Forms.FormWindowState.Minimized Then
            ' GM_Browser.Refresh()
        End If
    End Sub



    Private Sub mnuDisplayDate_Click(sender As Object, e As EventArgs) Handles mnuDisplayDate.Click

        'Display_Date
        If mnuDisplayDate.Checked = True Then
            Display_Date = True
        Else
            Display_Date = False
        End If
        Display_RegNo = False
        Display_Desc = False
        Display_Both = False
        Google_IsHistory = True
        GM_HistoryBrowser.ExecuteScriptAsync("clear_Google_Overlays();")
        GM_HistoryBrowser.ExecuteScriptAsync("SetMapOptions", {Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, IsMSTrackFlag})
        GM_HistoryBrowser.ExecuteScriptAsync("AddGoogleMarkers();")

    End Sub
    Private Sub SavePolygonGeofence(ByVal message As String)
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

                        .Parameters.Add("@PolyName", SqlDbType.NVarChar, 255).Value = PolygonGeofenceName
                        .Parameters.Add("@UserId", SqlDbType.NVarChar, 50).Value = MapUserID
                        .Parameters.Add("@GeoCoords", SqlDbType.NVarChar, -1).Value = tmpArray(0)
                        .Parameters.Add("@Company", SqlDbType.NVarChar, 50).Value = MapCompany
                    End With
                    myConnection.Open()
                    Dim mySqlDataReader As SqlClient.SqlDataReader
                    mySqlDataReader = mySqlCommand.ExecuteReader(CommandBehavior.Default)
                    mySqlDataReader.Close()
                    myConnection.Close()
                    myConnection.Dispose()

                    MsgBox(PolygonGeofenceName & " saved.", MsgBoxStyle.Information, "MS Track Pro 8 - Save Geofence")
                    'LoadPolygonGeofences()
                Catch ex As Exception
                    MsgBox(PolygonGeofenceName & " was not saved. Error: " & ex.Message, MsgBoxStyle.Critical, "MS Track Pro 8 - Save Geofence Failed")
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

                MsgBox(PolygonGeofenceName & " updated.", MsgBoxStyle.Information, "MS Track Pro 8 - Save Geofence")
                'LoadPolygonGeofences()
            Catch ex As Exception
                MsgBox(PolygonGeofenceName & " was not updated. Error: " & ex.Message, MsgBoxStyle.Critical, "MS Track Pro 8 - Save Geofence Failed")
            End Try
        End If
        'RefreshHistoryMap()
        'Dim result As JavascriptResponse
        'result = Await GM_HistoryBrowser.EvaluateScriptAsync("SetMapOptions", {HistoryArrayStr, 1, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})
        'result = Await GM_HistoryBrowser.EvaluateScriptAsync("reloadGeofences();")
    End Sub
    Private Sub DeletePolyGeofence(data As String)
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
            'RefreshHistoryMap()
            'Dim result As JavascriptResponse
            'result = Await GM_HistoryBrowser.EvaluateScriptAsync("SetMapOptions", {HistoryArrayStr, 1, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})
            'result = Await GM_HistoryBrowser.EvaluateScriptAsync("reloadGeofences();")
        Catch ex As Exception
            MsgBox(thisGeoName & " was not deleted. Error: " & ex.Message, MsgBoxStyle.Critical, "MS Track Pro 8 - Delete Geofence Failed")

        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        RefreshHistoryMap()
    End Sub
    Private Async Sub RefreshHistoryMap()
        ReloadAllGeofences_History()
        Dim result As JavascriptResponse
        result = Await GM_HistoryBrowser.EvaluateScriptAsync("SetMapOptions", {HistoryArrayStr, 1, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})
        result = Await GM_HistoryBrowser.EvaluateScriptAsync("reloadGeofences();")
    End Sub
    Public Sub ReloadAllGeofences_History()
        PlotSites()
        LoadGeofenceArray()
        If IsMSTrackFlag = False Then
            PlotGeoFences()
            GetPolygonGeofences()
            GetPolygonRoutes(MapCompany, "*")
        End If

    End Sub
    Public Sub History_ShowRoutes()
        Dim colorIndex As Integer
        Dim AllocatedNode As TreeNode
        Dim hasAllocatedRoutes As Boolean = False
        pnlAddress.Visible = False
        pnlClickMap.Visible = False
        pnlGeofences.Visible = False
        pnlSites.Visible = False
        pnlTop.Visible = False
        pnlVehicles.Visible = False
        pnlWaypointGrid.Visible = False
        pnlWaypointLabel.Visible = False
        tvRoutes.Nodes.Clear()
        AllocatedNode = tvRoutes.Nodes.Add("Allocated")
        If PolyGeoFences.Count > 0 Then
            For Each obj In PolyGeoFences
                If obj.GeoFenceSource = "4" And Not IsNothing(obj.RegNo) Then
                    hasAllocatedRoutes = True
                    Dim customNode As TreeNode = New TreeNode()
                    customNode.Text = obj.GeoFenceName
                    If colorIndex > 5 Then
                        colorIndex = 0
                    End If
                    customNode.ForeColor = Colors(colorIndex)
                    'customNode.NodeFont = New Font("Microsoft Sans Serif", 8.25, FontStyle.Bold)
                    AllocatedNode.Nodes.Add(customNode)
                    colorIndex += 1
                End If
            Next
        Else
            NoRoutesCreated("NoRoutesAllocated")
        End If
        If hasAllocatedRoutes = True Then
            pnlShowRoutes.Visible = True
            pnlCreateRoutes.Visible = False
            mainContainer1.Panel1Collapsed = False
        Else
            NoRoutesCreated("NoRoutesAllocated")
        End If
    End Sub
    Private Sub NoRoutesCreated(ByVal routeMsg As String)
        If routeMsg = "NoRoutesCreated" Then
            MsgBox("Unable to show routes, no routes created.", MsgBoxStyle.Information, "MST Viewer - Show Routes")
        ElseIf routeMsg = "NoRoutesAllocated" Then
            MsgBox("Unable to show routes, no routes allocated by this Company.", MsgBoxStyle.Information, "MST Viewer - Show Routes")
        End If
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
                                    nodeName = Trim(childNode.Text)
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
                GM_HistoryBrowser.ExecuteScriptAsync("showSelectedRoutes", New String() {selectedRoutes})
            Else
                GM_HistoryBrowser.ExecuteScriptAsync("ClearRoutes();")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub tvRoutes_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles tvRoutes.AfterCheck
        ShowSelectedRoutes()
    End Sub
    Private Sub ClearModifyRouteFlag()
        btnSaveRoute.Visible = False
        IsModifyRoute = False
    End Sub

    Private Sub mnuPrintMap_Click(sender As Object, e As EventArgs) Handles mnuPrintMap.Click
        GM_HistoryBrowser.Print()
    End Sub

    Private Async Sub GM_Browser_FrameLoadEnd(sender As Object, e As FrameLoadEndEventArgs) Handles GM_HistoryBrowser.FrameLoadEnd
        Try
            Dim result As JavascriptResponse
            result = Await GM_HistoryBrowser.EvaluateScriptAsync("SetMapOptions", {HistoryArrayStr, 0, 1, Google_UseLabels, Google_JoinPoints, Google_IsSites, str_GeofenceArray, str_SitesArray, str_BufferZoneArray, Display_RegNo, Display_Desc, Display_Both, Display_Date, ClsMstGoogleMaps.Google_IsHistory, str_PolygonGeo, str_PolygonRoutes, str_IconBase64, str_IconArray, str_PolygonRoutesWayPoints, Google_IsRouteOptimized, IsMSTrackFlag, mapStartLat, mapStartLng, mapStartZoom})

            If HistoryArrayStr.Length > 0 Then
                result = Await GM_HistoryBrowser.EvaluateScriptAsync("Load_Google_Map", New String() {HistoryArrayStr})
            Else
                result = Await GM_HistoryBrowser.EvaluateScriptAsync("SetMapCenter", {mapStartLat, mapStartLng, mapStartZoom})
            End If
        Catch ex As Exception
            MsgBox("Error 637: " & ex.Message, MsgBoxStyle.Critical, "GM_HistoryBrowser_FrameLoadEnd")
        End Try
    End Sub
    Private Sub UnableToDeletePolygon(ByVal thisPolygon As String)
        MsgBox("Geofence " & thisPolygon & " has Company and Fleet Allocations. It cannot be deleted.", MsgBoxStyle.Information, "MST Viewer - Delete polygon geofence")
    End Sub
End Class