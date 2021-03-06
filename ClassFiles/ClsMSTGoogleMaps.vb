﻿Imports System.Threading
Imports Microsoft.VisualBasic.CompilerServices
Imports MSTGoogleMap.modMSTGoogle_MapUtils

Public Class ClsMstGoogleMaps
    Implements IDisposable
    Private disposed As Boolean = False

    Private MapFormSettings As New clsBIS_GoogleMaps.clsFormSettings
    Private HistoryMapFormSettings As New clsBIS_GoogleMaps.clsHistoryFormSettings
    Private mapLeft, mapRight, mapTop, mapBottom As Double

    Private MapPath As String

    Public Event FormSettings(ByVal myX As Integer, ByVal myY As Integer, ByVal myW As Integer, ByVal myH As Integer)
    Public Event HistoryFormSettings(ByVal myX As Integer, ByVal myY As Integer, ByVal myW As Integer, ByVal myH As Integer)
    Public Event theMapExtent(ByVal xLeft As Double, ByVal xRight As Double, ByVal xTop As Double, ByVal xBottom As Double)
    Public Event MapPathChange(ByVal data As String)
    Public Event UpdateDisplayIgnition(ByVal data As Boolean)
    Public Event UpdateDisplayInformationBalloons(ByVal data As Boolean)
    Public Event UpdateDisplayFences(ByVal data As Boolean)
    Public Event UpdateAutoZoomToLayer(ByVal data As Boolean)
    Public Event UpdateDisplayDirection(ByVal data As Boolean)
    Public Event BufferZoneCreated(ByVal data As String)
    Public Event BufferZoneDeleted(ByVal data As String)
    Public Event AppClosing(ByVal data As Boolean)
    Public Event InfoBalloonTypeChange(ByVal data As String)
    Public Event UseHint(ByVal data As Boolean)
    Public Event theHintField(ByVal data As String)
    Public Event theHintLayer(ByVal data As String)
    Public Event CreateSiteFromPoint(ByVal SiteLat As Double, ByVal SiteLong As Double)
    Public Event SaveMapBounds(ByVal ptLat As Double, ByVal ptLong As Double, ByVal mapZoom As Integer)          'Added 20160105 RJG
    Public Event WriteErrorFile(ByVal data As String)                                                           'Added 20170718 rjg
    Public Event UpdatePlotDBSites(ByVal data As Boolean)
    Public Event VehiclesCloseToPoint(ByVal PointLat As Double, ByVal PointLong As Double)
    Public Event UpdateMapType(ByVal MapType As String)
    Public Event ClearGoogleMap(ByVal Value As Boolean)
    Public Event UpdateGoogleMapID(ByVal Data As String)


    Public Shared thisGoogleObj As Object
    Public Shared Google_NoOfPoints As Integer

    Public Shared thisGoogleHistoryObj As Object

    Public Shared Google_MapType As String
    Public Shared Google_ZoomLevel As Integer
    Public Shared Google_JoinPoints As Boolean
    Public Shared Google_UseLabels As Boolean
    Public Shared Google_IsSites As Boolean
    Public Shared Google_IsHistory As Boolean
    Public Shared Google_IsBufferZone As Boolean
    Public Shared Google_IsRouteManagement As Boolean
    Public Shared Google_IsRouteOptimized As Boolean

    Public Shared RouteVehicles As New List(Of ClsRouteVehicles)
    Public Shared MapCompany As String
    Public Shared MapFleet_Code As String
    Public Shared Company As String
    Public Shared Fleet_Code As String
    Public Shared MapUserID As String
    Public Shared reloadGeofences As Boolean = True
    Public Shared reloadSites As Boolean = True

    Private myGoogleMap_Form As New frmMSTGoogleMap
    Private myGoogleHistoryMap_Form As New frmMSTGoogleHistoryMap
    Public Shared HistoryFormExists As Boolean = False
    Public Shared FleetFormExists As Boolean = False
    'History Map

    Public Shared IsMSTrackFlag As Boolean                'Added 20160905 RJG
    Public WriteOnly Property GoogleReloadGeofences As Boolean
        Set(ByVal Value As Boolean)
            reloadGeofences = Value
        End Set
    End Property
    Public WriteOnly Property GoogleReloadSites As Boolean
        Set(ByVal Value As Boolean)
            reloadSites = Value
        End Set
    End Property
    Public WriteOnly Property IsMSTrack As Boolean
        Set(ByVal Value As Boolean)
            IsMSTrackFlag = Value
        End Set
    End Property
    Public WriteOnly Property GoogleHistoryMapObj() As Object
        Set(ByVal Value As Object)
            thisGoogleHistoryObj = Value
        End Set
    End Property
    'Fleet Map
    Public WriteOnly Property GoogleCompany() As String
        Set(ByVal Value As String)
            Company = Value
        End Set
    End Property
    Public WriteOnly Property GoogleFleet_Code() As String
        Set(ByVal Value As String)
            Fleet_Code = Value
        End Set
    End Property
    Public WriteOnly Property GoogleRouteVehicles As List(Of ClsRouteVehicles)
        Set(ByVal value As List(Of ClsRouteVehicles))
            RouteVehicles = value
        End Set
    End Property
    Public ReadOnly Property FormSettingsEventProperty As FormSettingsEventHandler
        Get
            Return FormSettingsEvent
        End Get
    End Property

    Public WriteOnly Property GoogleMapObj() As Object
        Set(ByVal Value As Object)
            thisGoogleObj = Value
        End Set
    End Property
    Public WriteOnly Property NoOfPoints() As Integer
        Set(ByVal Value As Integer)
            Google_NoOfPoints = Value
        End Set
    End Property
    Public WriteOnly Property SetGoogleZoomLevel() As Integer
        Set(ByVal Value As Integer)
            Google_ZoomLevel = Value
        End Set
    End Property
    Public WriteOnly Property SetGoogleIsSites() As Boolean
        Set(ByVal value As Boolean)
            Google_IsSites = value
        End Set
    End Property
    Public WriteOnly Property SetGoogleIsHistory() As Boolean
        Set(ByVal value As Boolean)
            Google_IsHistory = value
        End Set
    End Property
    Public WriteOnly Property SetGoogleIsBufferZone() As Boolean
        Set(ByVal value As Boolean)
            Google_IsBufferZone = value
        End Set
    End Property
    Public WriteOnly Property SetGoogleUseLabels() As Boolean
        Set(ByVal value As Boolean)
            Google_UseLabels = value
        End Set
    End Property
    Public WriteOnly Property SetGoogleJoinPoints() As Boolean
        Set(ByVal Value As Boolean)
            Google_JoinPoints = Value
        End Set
    End Property
    Public Property SetGoogleMapType() As String
        Set(ByVal Value As String)
            Google_MapType = Value
        End Set
        Get
            SetGoogleMapType = Google_MapType
        End Get
    End Property
    Public WriteOnly Property StartHistory() As Boolean
        Set(ByVal Value As Boolean)
            If Value = True And GoogleMapKey <> "" And GoogleMapKey <> "No Valid Google Key" Then
                ShowGoogleHistoryMapForm()
            Else
                Dim myForm As New frmGoogleKey
                AddHandler myForm.SaveGoogleID, AddressOf SaveGoogleKey
                myForm.ShowDialog()
            End If
        End Set
    End Property
    Public WriteOnly Property ReloadAllGeofences() As Boolean
        Set(value As Boolean)
            If value = True And frmMSTGoogleMap.InstanceExists Then
                myGoogleMap_Form.ReloadAllGeofences()
            End If
            If value = True And frmMSTGoogleHistoryMap.InstanceExists Then
                myGoogleHistoryMap_Form.ReloadAllGeofences_History()
            End If
        End Set
    End Property

    Public WriteOnly Property Start() As Boolean
        Set(ByVal Value As Boolean)
            If Value = True And GoogleMapKey <> "" And GoogleMapKey <> "No Valid Google Key" Then
                ShowGoogleMapForm()
            Else
                Dim myForm As New frmGoogleKey
                AddHandler myForm.SaveGoogleID, AddressOf SaveGoogleKey
                myForm.ShowDialog()
            End If
        End Set
    End Property

    Public ReadOnly Property FormSettingsEvent1 As FormSettingsEventHandler
        Get
            Return FormSettingsEvent

        End Get
    End Property

    Public WriteOnly Property CloseMe() As Boolean
        Set(ByVal Value As Boolean)

            If Value = True Then
                myGoogleMap_Form.CloseApp()
                CloseForm(True)
            End If

        End Set
    End Property

    Public WriteOnly Property MapFormSize() As clsBIS_GoogleMaps.clsFormSettings
        Set(ByVal Value As clsBIS_GoogleMaps.clsFormSettings)
            MapFormSettings = Value
        End Set
    End Property    ' MapFormSize
    Public WriteOnly Property HistoryMapFormSize() As clsBIS_GoogleMaps.clsHistoryFormSettings
        Set(ByVal Value As clsBIS_GoogleMaps.clsHistoryFormSettings)
            HistoryMapFormSettings = Value
        End Set
    End Property    ' MapFormSize
    Public WriteOnly Property ClearHistoryMap() As Boolean

        Set(ByVal Value As Boolean)
            myGoogleHistoryMap_Form.ClearMap = Value
        End Set

    End Property        ' ClearMap
    Public WriteOnly Property CloseHistoryMap() As Boolean

        Set(ByVal Value As Boolean)
            HistoryFormExists = False
            myGoogleHistoryMap_Form.CloseForm = Value
        End Set

    End Property        ' ClearMap
    Public WriteOnly Property ClearMap() As Boolean

        Set(ByVal Value As Boolean)
            myGoogleMap_Form.ClearMap = Value
        End Set

    End Property        ' ClearMap

    Private Sub onMyHistoryFormSettings(ByVal myX As Integer, ByVal myY As Integer, ByVal myW As Integer, ByVal myH As Integer)

        Try
            RaiseEvent HistoryFormSettings(myX, myY, myW, myH)
        Catch ex As Exception
        End Try

    End Sub     ' myFormSettings
    Private Sub onMyFormSettings(ByVal myX As Integer, ByVal myY As Integer, ByVal myW As Integer, ByVal myH As Integer)

        Try
            RaiseEvent FormSettings(myX, myY, myW, myH)
        Catch ex As Exception
        End Try

    End Sub     ' myFormSettings
    Private Sub theMapSettings(ByVal xLeft As Double, ByVal xTop As Double, ByVal xRight As Double, ByVal xBottom As Double)

        Try
            RaiseEvent theMapExtent(xLeft, xTop, xRight, xBottom)
        Catch ex As Exception
        End Try


    End Sub     ' theMapSettings
    Public Property theCompany() As String
        Get
            theCompany = MapCompany
        End Get
        Set(ByVal Value As String)
            MapCompany = Value
        End Set
    End Property
    Public Property theFleet_Code() As String
        Get
            theFleet_Code = MapFleet_Code
        End Get
        Set(ByVal Value As String)
            MapFleet_Code = Value
        End Set
    End Property
    Public WriteOnly Property theUserID() As String
        Set(value As String)
            MapUserID = value
        End Set
    End Property


    Public WriteOnly Property theInfoFleetConnectionString() As String
        Set(ByVal Value As String)
            MapInfoFleetConnectionString = Value
        End Set
    End Property

    Public WriteOnly Property theGeoPlacesConnectionString() As String
        Set(ByVal value As String)
            MapGeoPlacesConnectionString = value
        End Set
    End Property
    Public Property GoogleMapKey() As String

        Set(ByVal value As String)
            theGoogleMapKey = value
            Dim TDES As New clsTripleDES
            If theGoogleMapKey <> "" Then
                'theGoogleMapKey = TDES.DecryptFromHexStr(theGoogleMapKey)
            Else
                Dim myForm As New frmGoogleKey
                AddHandler myForm.SaveGoogleID, AddressOf SaveGoogleKey
                myForm.ShowDialog()
            End If
        End Set
        Get
            GoogleMapKey = theGoogleMapKey
        End Get
    End Property
    Private Sub SaveGoogleKey(ByVal data As String)

        RaiseEvent UpdateGoogleMapID(data)
    End Sub
    Public WriteOnly Property DistanceUnits() As String
        Set(ByVal value As String)
            DistUnits = value
            SetDistConversionFactor(DistUnits)
        End Set
    End Property

    Private Sub SetDistConversionFactor(ByVal data As String)

        Try
            Select Case data
                Case "Kilometres"
                    MapDistMultiplier = 1.0

                Case "Miles"
                    MapDistMultiplier = 0.621371

                Case "Knots"
                    MapDistMultiplier = 0.539957701907 ' 0.539876
                    ' Value adopted from the Swiss Institute of Metrology
                    'http://www.metas.ch/en/index.html
            End Select

        Catch ex As Exception

        End Try

    End Sub     ' SetDistConversionFactor
    Private Sub onMapPathChange(ByVal data As String)

        Try
            RaiseEvent MapPathChange(data)
        Catch ex As Exception

        End Try

    End Sub     ' onMapPathChange

    Private Sub onUpdateDisplayIgnition(ByVal data As Boolean)

        Try
            RaiseEvent UpdateDisplayIgnition(data)
        Catch ex As Exception
        End Try

    End Sub     ' onUpdateDisplayIgnition

    Private Sub onUpdateDisplayInfoBalloons(ByVal data As Boolean)

        Try
            RaiseEvent UpdateDisplayInformationBalloons(data)
        Catch ex As Exception
        End Try

    End Sub     ' onUpdateDisplayInfoBalloons

    Private Sub onUpdateDisplayFences(ByVal data As Boolean)

        Try
            RaiseEvent UpdateDisplayFences(data)
        Catch ex As Exception

        End Try

    End Sub     ' onUpdateDisplayFences

    Private Sub onUpdateTravelDirection(ByVal data As Boolean)

        Try
            RaiseEvent UpdateDisplayDirection(data)
        Catch ex As Exception

        End Try
    End Sub     ' onUpdateTravelDirection


    Private Sub onUpdateZoomToLayer(ByVal data As Boolean)

        Try
            RaiseEvent UpdateAutoZoomToLayer(data)
        Catch ex As Exception

        End Try
    End Sub     ' onUpdateZoomToLayer


    Private Sub OnBufferZoneCreated(ByVal data As String)

        RaiseEvent BufferZoneCreated(data)

    End Sub     ' OnBufferZoneCreated
    ' onGISCreateSite

    Private Sub onVehiclesCloseToPoint(ByVal ptLat As Double, ByVal ptLong As Double)

        Try
            RaiseEvent VehiclesCloseToPoint(ptLat, ptLong)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub onUpdateDisplayDBSites(ByVal data As Boolean)
        Try
            RaiseEvent UpdatePlotDBSites(data)
        Catch ex As Exception

        End Try
    End Sub         ' onUpdateDisplayDBSites
    Private Sub ShowGoogleHistoryMapForm()
        'Called by clsMSTGoogleMaps.StartHistory() As Boolean

        If Not frmMSTGoogleHistoryMap.InstanceExists Then
            myGoogleHistoryMap_Form = New frmMSTGoogleHistoryMap
            AddHandler myGoogleHistoryMap_Form.GoogleMapId, AddressOf onGoogleMapIDUpdate
            AddHandler myGoogleHistoryMap_Form.myHistoryFormSettings, AddressOf onMyHistoryFormSettings
            AddHandler myGoogleHistoryMap_Form.AppClosing, AddressOf CloseForm
            AddHandler myGoogleHistoryMap_Form.UpdateGoogleMapType, AddressOf GoogleUpdateGoogleMapType
            AddHandler myGoogleHistoryMap_Form.GoogleCreateSite, AddressOf onGoogleCreateSite
            AddHandler myGoogleHistoryMap_Form.ClearGoogleMap, AddressOf onGoogleClearMap

            Try
                myGoogleHistoryMap_Form.FormSize = HistoryMapFormSettings
                myGoogleHistoryMap_Form.StartMapForm = True
            Catch ex As Exception
            End Try
        Else
            myGoogleMap_Form.FormSize = MapFormSettings
            'myGoogleHistoryMap_Form.StartMapForm = True
        End If
    End Sub
    Private Sub ShowAllGeofences()

    End Sub
    Private Sub ShowGoogleMapForm()

        If Not frmMSTGoogleMap.InstanceExists Then
            myGoogleMap_Form = New frmMSTGoogleMap
            AddHandler myGoogleMap_Form.GoogleMapId, AddressOf onGoogleMapIDUpdate
            AddHandler myGoogleMap_Form.myFormSettings, AddressOf onMyFormSettings
            AddHandler myGoogleMap_Form.AppClosing, AddressOf CloseForm
            AddHandler myGoogleMap_Form.UpdateGoogleMapType, AddressOf GoogleUpdateGoogleMapType
            AddHandler myGoogleMap_Form.GoogleCreateSite, AddressOf onGoogleCreateSite
            AddHandler myGoogleMap_Form.ClearGoogleMap, AddressOf onGoogleClearMap
            AddHandler myGoogleMap_Form.GoogleSaveMapBounds, AddressOf onGoogleSaveMapBounds     'Added 20160105 RJG
            AddHandler myGoogleMap_Form.GoogleWriteErrorFile, AddressOf onGoogleWriteErrorFile      'Added 20170718 RJG

            Try
                myGoogleMap_Form.FormSize = MapFormSettings
                myGoogleMap_Form.StartMapForm = True
            Catch ex As Exception
            End Try
        Else
            'If GoogleMapStarted = True Then
            'myGoogleMap_Form.FormSize = MapFormSettings
            myGoogleMap_Form.StartMapForm = True
            'End If
            'GoogleMapStarted = True
        End If
    End Sub

    ' This routine used if the File Close menu option was clicked
    Public Sub FormClosed(ByVal data As Boolean)

        RaiseEvent AppClosing(True)
        'Me.Dispose()

    End Sub     ' FormClosed
    Private Sub onGoogleMapIDUpdate(ByVal data As String)
        RaiseEvent UpdateGoogleMapID(data)
    End Sub
    Private Sub onGoogleCreateSite(ByVal ptLat As Double, ByVal ptLong As Double)

        Try
            RaiseEvent CreateSiteFromPoint(ptLat, ptLong)
        Catch ex As Exception

        End Try
    End Sub     ' onGISCreateSit
    Private Sub onGoogleSaveMapBounds(ByVal ptLat As Double, ByVal ptLong As Double, mapZoom As Integer)
        'Added 20160105 RJG
        Try
            RaiseEvent SaveMapBounds(ptLat, ptLong, mapZoom)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub onGoogleWriteErrorFile(ByVal data As String)
        Try
            RaiseEvent WriteErrorFile(data)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub onGoogleClearMap(ByVal value As Boolean)
        Try
            RaiseEvent ClearGoogleMap(value)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CloseForm(ByVal data As Boolean)

        Try
            myGoogleMap_Form.GetParameters = data
            myGoogleMap_Form.Dispose()
            myGoogleMap_Form = Nothing

            RaiseEvent AppClosing(True)
        Catch ex As Exception
        End Try

    End Sub         'CloseForm

    Private Sub GoogleUpdateGoogleMapType(ByVal MapType As String)
        RaiseEvent UpdateMapType(MapType)
    End Sub

    Private Sub OnCloseDll(ByVal data As Boolean)
        Try
            Kill(FleetHTML)
        Catch ex As Exception
        End Try
        Try
            Kill(HistoryHTML)
        Catch ex As Exception
        End Try
        Me.Finalize()
    End Sub     ' OnCloseDll
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        'GC.SuppressFinalize(Me)
    End Sub 'Dispose
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        Try
            CloseForm(True)
            OnCloseDll(True)
            myGoogleMap_Form = Nothing
        Catch ex As Exception
        End Try
    End Sub


End Class
