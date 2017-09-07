Public Class clsMapCallback
    Public Shared Event evt_CreateSite(message)
    Public Shared Event evt_ClearModifyRoute()
    Public Shared Event evt_SaveMapBounds(centerLat, centerLng, mapZoom)
    Public Shared Event evt_UpdateGoogleMapType(NewMapType)
    Public Shared Event evt_DeletePolyGeofence(polyName)
    Public Shared Event evt_SaveAddressWaypoint(address, lat, lng)
    Public Shared Event evt_SaveWaypointFromMap(lat, lng)
    Public Shared Event evt_UnableToDeletePolygon(thisPolygon)
    Public Shared Event evt_SavePolygonGeofence(message)
    Public Shared Event evt_NoRoutesCreated(routeMsg)
    Public Shared Event evt_History_ShowRoutes()
    Public Shared Event evt_UpdateTurns(turnsList, totalPath, wayPointLatLngs)
    Public Sub createSite(ByVal message As String)
            RaiseEvent evt_CreateSite(message)
    End Sub
    Public Sub clearModifyRoute()
        RaiseEvent evt_ClearModifyRoute()
    End Sub
    Public Sub saveMapBounds(ByVal centerLat As Double, ByVal centerLng As Double, ByVal mapZoom As Integer)
        RaiseEvent evt_SaveMapBounds(centerLat, centerLng, mapZoom)
    End Sub
    Public Sub mapTypeChanged(ByVal NewMapType As String)
        RaiseEvent evt_UpdateGoogleMapType(NewMapType)
    End Sub
    Public Sub deletePolyGeofence(ByVal polyName As String)
        RaiseEvent evt_DeletePolyGeofence(polyName)
    End Sub
    Public Sub saveAddressWaypoint(ByVal address As String, ByVal lat As Double, lng As Double)
        RaiseEvent evt_SaveAddressWaypoint(address, lat, lng)
    End Sub
    Public Sub saveWaypointFromMap(ByVal lat As Double, ByVal lng As Double)
        RaiseEvent evt_SaveWaypointFromMap(lat, lng)
    End Sub
    Public Sub unableToDeletePolygon(ByVal thisPolygon As String)
        RaiseEvent evt_UnableToDeletePolygon(thisPolygon)
    End Sub
    Public Sub savePolygonGeofence(ByVal message As String)
        RaiseEvent evt_SavePolygonGeofence(message)
    End Sub
    Public Sub noRoutesCreated(ByVal routeMsg As String)
        RaiseEvent evt_NoRoutesCreated(routeMsg)
    End Sub
    Public Sub history_ShowRoutes()
        RaiseEvent evt_History_ShowRoutes()
    End Sub
    Public Sub updateTurns(ByVal turnsList As String, ByVal totalPath As String, ByVal wayPointLatLngs As String)
        RaiseEvent evt_UpdateTurns(turnsList, totalPath, wayPointLatLngs)
    End Sub
End Class

