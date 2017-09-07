Public Class clsHistoryMapCallback
    Public Shared Event evtHistory_CreateSite(message)
    Public Shared Event evtHistory_ClearModifyRoute()
    Public Shared Event evtHistory_SaveMapBounds(centerLat, centerLng, mapZoom)
    Public Shared Event evtHistory_UpdateGoogleMapType(NewMapType)
    Public Shared Event evtHistory_DeletePolyGeofence(polyName)
    Public Shared Event evtHistory_SaveAddressWaypoint(address, lat, lng)
    Public Shared Event evtHistory_SaveWaypointFromMap(lat, lng)
    Public Shared Event evtHistory_UnableToDeletePolygon(thisPolygon)
    Public Shared Event evtHistory_SavePolygonGeofence(message)
    Public Shared Event evtHistory_NoRoutesCreated(routeMsg)
    Public Shared Event evtHistory_History_ShowRoutes()
    Public Shared Event evtHistory_UpdateTurns(turnsList, totalPath, wayPointLatLngs)
    Public Sub createSite(ByVal message As String)
            RaiseEvent evtHistory_CreateSite(message)
    End Sub
    Public Sub clearModifyRoute()
        RaiseEvent evtHistory_ClearModifyRoute()
    End Sub
    Public Sub saveMapBounds(ByVal centerLat As Double, ByVal centerLng As Double, ByVal mapZoom As Integer)
        RaiseEvent evtHistory_SaveMapBounds(centerLat, centerLng, mapZoom)
    End Sub
    Public Sub mapTypeChanged(ByVal NewMapType As String)
        RaiseEvent evtHistory_UpdateGoogleMapType(NewMapType)
    End Sub
    Public Sub deletePolyGeofence(ByVal polyName As String)
        RaiseEvent evtHistory_DeletePolyGeofence(polyName)
    End Sub
    Public Sub saveAddressWaypoint(ByVal address As String, ByVal lat As Double, lng As Double)
        RaiseEvent evtHistory_SaveAddressWaypoint(address, lat, lng)
    End Sub
    Public Sub saveWaypointFromMap(ByVal lat As Double, ByVal lng As Double)
        RaiseEvent evtHistory_SaveWaypointFromMap(lat, lng)
    End Sub
    Public Sub unableToDeletePolygon(ByVal thisPolygon As String)
        RaiseEvent evtHistory_UnableToDeletePolygon(thisPolygon)
    End Sub
    Public Sub savePolygonGeofence(ByVal message As String)
        RaiseEvent evtHistory_SavePolygonGeofence(message)
    End Sub
    Public Sub noRoutesCreated(ByVal routeMsg As String)
        RaiseEvent evtHistory_NoRoutesCreated(routeMsg)
    End Sub
    Public Sub history_ShowRoutes()
        RaiseEvent evtHistory_History_ShowRoutes()
    End Sub
    Public Sub updateTurns(ByVal turnsList As String, ByVal totalPath As String, ByVal wayPointLatLngs As String)
        RaiseEvent evtHistory_UpdateTurns(turnsList, totalPath, wayPointLatLngs)
    End Sub
End Class
