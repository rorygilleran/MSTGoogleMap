Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports MSTGoogleMap.ClsMstGoogleMaps
Imports Newtonsoft.Json

Module modMSTGoogle_MapUtils
    Public MapInfoFleetConnectionString As String
    Public MapGeoPlacesConnectionString As String
    '20130815 RJG Added two lines
    'Public MapCompany As String
    Public MapFleet_Code As String

    Public theDistanceUnits As String = "Kilometres"
    Public theGoogleMapKey As String = ""
    Public MapDistMultiplier As Double = 1.0
    Public str_GeofenceArray As String = ""
    Public str_IconArray As String
    Public str_IconBase64 As String
    Public NumofPlots As Integer
    Public DistUnits As String
    Public thisDisplayType As String
    Public str_SitesArray As String = ""
    Public str_BufferZoneArray As String = ""
    Public BufferZoneWidth As Integer
    Public str_PolygonGeo As String = String.Empty
    Public str_PolygonRoutesWayPoints As String = String.Empty
    Public str_PolygonRoutes As String = String.Empty
    Public IsCreatingPolygon As Boolean
    Public PolygonGeofenceName As String = ""
    Public PolygonGeofenceCode As String = ""
    Public PolygonGeofenceAlertType As Integer
    Public PolyType As String = ""
    Public PolyCoords As String = ""
    Public Display_Date As Boolean = False
    Public Display_RegNo As Boolean
    Public Display_Desc As Boolean
    Public Display_Both As Boolean
    Public FleetHTML As String = Application.StartupPath & "\GoogleMap.html"
    Public HistoryHTML As String = Application.StartupPath & "\GoogleHistoryMap.html"
    Public RouteGeofences As List(Of ClsGeofences)
    Public RouteSites As List(Of ClsSites)
    Public WaypointsObj As New List(Of ClsWayPoints)
    Public WaypointIndex As Integer
    Public UseThisLocation As Boolean
    Public TotalRoutePath As String
    Public PolyGeoFences As List(Of ClsPolyGeofence)
    Public PolyRoutes As List(Of ClsPolyRoutes)
    Public myRouteList As List(Of ClsTurns)
    Public AllGeofences As List(Of ClsGeofences)
    Public AllPolygonGeofences As List(Of ClsGeofences)
    Public GoogleMapStarted As Boolean
    Public mapStartLat As Double
    Public mapStartLng As Double
    Public mapStartZoom As Integer
    Public IsModifyRoute As Boolean
    Public ModifyRouteName As String
    Public ModifyRouteID As Integer
    Public kmlRouteUri As String
    Public selectedRoute As String
    Public dsExcelRoutes As DataSet
    Public EmailFileName As String
    Public NoGeofences As Integer
    Public mapHandlersAdded As Boolean
    Public historyMapHandlersAdded As Boolean



    Public Function CreateMapHTML(ByVal thisGoogleMapKey As String) As String
        CreateMapHTML = ""
        Try
            Dim kmlPolygonFileName As String = Application.StartupPath & "\Polygons\" & MapCompany & "_" & MapFleet_Code & ".KML"
            kmlPolygonFileName = kmlPolygonFileName.Replace("\", "/")
            Dim strHtml As String
            strHtml = "<html><head><title></title>" & vbCrLf
            'str_HTML &= "<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"" > " & vbCrLf
            strHtml &= "<!DOCTYPE html > " & vbCrLf
            strHtml &= "<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">" & vbCrLf
            strHtml &= "<style type=""text/css"">" & vbCrLf
            strHtml &= My.Resources.MST_GoogleMapCSS & vbCrLf
            strHtml &= "</style>" & vbCrLf
            strHtml &= "<script type=""text/javascript"" src=""https://maps.google.com/maps/api/js?v=3&client=" & thisGoogleMapKey & "&libraries=drawing,geometry""></script>" & vbCrLf
            'strHtml &= "<script src=""https://cdnjs.cloudflare.com/ajax/libs/json2/20150503/json2.min.js""></script>" & vbCrLf
            strHtml &= "<script type=""text/javascript"" language=""javascript"">" & vbCrLf
            strHtml &= My.Resources.jquery_1_11_1_min & vbCrLf
            strHtml &= My.Resources.label_v3 & vbCrLf
            strHtml &= My.Resources.ContextMenu & vbCrLf
            strHtml &= My.Resources.Arrowheads & vbCrLf
            strHtml &= My.Resources.markerclusterer & vbCrLf
            strHtml &= My.Resources.markerwithlabel & vbCrLf
            strHtml &= My.Resources.GPolyLine & vbCrLf
            strHtml &= My.Resources.ProjectedOverlay & vbCrLf
            strHtml &= My.Resources.MST_RoutesJS & vbCrLf
            strHtml &= My.Resources.MST_GoogleMapJS & vbCrLf

            strHtml &= "</script>" & vbCrLf
            strHtml &= "</head>" & vbCrLf
            strHtml &= "<body>" & vbCrLf
            strHtml &= "<div id=""panel"" style=""display: none;"" >	" & vbCrLf
            strHtml &= "	      <div id=""color-palette""></div>	" & vbCrLf
            strHtml &= "</div>	" & vbCrLf
            strHtml &= "<div id='myMap' style=""width:100%; height:100%;""></div>" & vbCrLf
            'strHtml &= "<div class='alert' id='alertNoGeofences'> " & vbCrLf
            'strHtml &= "<span class='closebtn' onclick='this.parentElement.style.display='none';'>&times;</span> " & vbCrLf
            'strHtml &= "<strong>Show / Hide Geofences:</strong> No geofence found for this Company." & vbCrLf
            'strHtml &= "</div>" & vbCrLf
            strHtml &= "</body>" & vbCrLf
            strHtml &= "</html>" & vbCrLf
            CreateMapHTML = strHtml
        Catch ex As Exception
            Dim errMsg As String = ex.Message
            MsgBox("Display_Map_Google Error: 1826 - " & errMsg)

        End Try
    End Function

    Public Sub GetPolygonRoutes(ByVal strCompany As String, ByVal strFleetCode As String)
        Dim i, j As Integer
        Dim noPolygonGeofences As Integer
        Dim crit As String = String.Empty
        Dim sqlStr As String = String.Empty
        Dim myConnection As New SqlConnection(MapInfoFleetConnectionString)
        myConnection.Open()
        Try

            If strCompany = "*" Then
                crit = ""
            ElseIf strFleetCode = "*" Then
                crit = "AND GeoFences.Company = '" & strCompany & "'"
            Else
                crit = "AND GeoFences.Company = '" & strCompany & "' AND GeoFences.FleetCode ='" & strFleetCode & "'"
            End If

            sqlStr = "SELECT GeoPolygons.UserId, GeoFences.Company, GeoFences.FleetCode, GeoFences.Reg_No, GeoPolygons.PolyName, GeoPolygons.PolyType, " &
                "GeoPolygons.GeoPolygonNative.STAsText() AS Polygon, GeoPolygons.GeoId " &
                "FROM GeoFences RIGHT OUTER JOIN GeoPolygons ON GeoFences.GeoFenceCode = Cast(GeoPolygons.GeoId as nvarchar(100)) " &
                "Where GeoPolygons.PolyType = '4' AND GeoFences.Company IS NOT NULL " & crit


            sqlStr = sqlStr & " Order by GeoPolygons.GeoID"

            Dim mySda As New SqlDataAdapter(sqlStr, myConnection)
            mySda.SelectCommand.CommandTimeout = 300
            Dim myDs As New DataSet
            mySda.Fill(myDs)
            myConnection.Close()
            i = 0
            PolyGeoFences = New List(Of ClsPolyGeofence)
            Dim dr As DataRow
            For Each dr In myDs.Tables(0).Rows
                Dim geoObj As ClsPolyGeofence = New ClsPolyGeofence()
                If Not IsDBNull(dr("UserId")) Then
                    geoObj.UserId = Trim(dr("UserId"))
                End If
                If Not IsDBNull(dr("Company")) Then
                    geoObj.Company = Trim(dr("Company"))
                End If
                If Not IsDBNull(dr("FleetCode")) Then
                    geoObj.FleetCode = Trim(dr("FleetCode"))
                End If
                If Not IsDBNull(dr("Reg_No")) Then
                    geoObj.RegNo = Trim(dr("Reg_No"))
                End If
                If Not IsDBNull(dr("PolyName")) Then
                    geoObj.GeoFenceName = Trim(dr("PolyName"))
                End If
                If Not IsDBNull(dr("PolyType")) Then
                    geoObj.GeoFenceSource = Trim(dr("PolyType"))
                End If
                If Not IsDBNull(dr("GeoId")) Then
                    geoObj.GeoId = Trim(dr("GeoId"))
                End If
                If Not IsDBNull(dr("Polygon")) Then
                    Dim coords As String = dr("Polygon")
                    If geoObj.GeoFenceSource = "3" Then
                        Dim parts As String() = coords.Split(New String() {"(("}, StringSplitOptions.RemoveEmptyEntries)
                        coords = Microsoft.VisualBasic.Left(parts(1), parts(1).Length - 2)
                        If coords.Contains("(") Then
                            coords = coords.Replace("(", "")
                        End If
                        Dim tmpArray() As String = coords.Split(",")
                        If tmpArray.Length > 0 Then
                            For j = 0 To tmpArray.Length - 1
                                Dim tmpArray1() As String = Trim(tmpArray(j)).Split(" ")
                                If tmpArray1.Length > 0 Then
                                    If tmpArray1(0).Contains("(") Then
                                        tmpArray1(0) = tmpArray1(0).Replace("(", "")
                                    End If
                                    geoObj.Polygon += tmpArray1(1) & ", " & tmpArray1(0)
                                    If j < tmpArray.Length - 1 Then
                                        geoObj.Polygon += "#"
                                    End If
                                End If
                            Next
                        End If
                    ElseIf geoObj.GeoFenceSource = "4" Then
                        If coords.Contains("MULTILINESTRING") Then
                            coords = coords.Replace("MULTILINESTRING ((", "")
                        End If
                        If coords.Contains("LINESTRING") Then
                            coords = coords.Replace("LINESTRING (", "")
                        End If
                        coords = coords.Replace("))", "")
                        coords = coords.Replace(")", "")
                        coords = coords.Replace("(", "")

                        Dim tmpArray() As String = coords.Split(",")
                        Try
                            If tmpArray.Count > 0 Then
                                For j = 0 To tmpArray.Count - 1
                                    Dim tmpArray1() As String = Trim(tmpArray(j)).Split(" ")
                                    geoObj.Polygon += tmpArray1(1) & ", " & tmpArray1(0)
                                    If j < tmpArray.Count - 1 Then
                                        geoObj.Polygon += "#"
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                            MsgBox("Error 216 " & ex.Message)
                        End Try
                    End If
                    PolyGeoFences.Add((geoObj))
                    i += 1
                End If
            Next

        Catch ex As Exception
            Dim errMsg As String = ex.Message
        End Try
        Try
            If strCompany = "*" Then
                crit = ""
            ElseIf strFleetCode = "*" Then
                crit = "WHERE GeoPolygons.Company = '" & strCompany & "'"
            Else
                crit = "WHERE GeoPolygons.Company = '" & strCompany & "' AND GeoPolygons.FleetCode ='" & strFleetCode & "'"
            End If
            sqlStr = "SELECT GeoPolygons.GeoId, GeoPolygons.PolyName, GeoPolygons.DateChanged, GeoPolygons.UserId, GeoPolygons.GeoPolygonNative.STAsText() AS Polygon, GeoPolygons.PolyType, GeoPolygons.Company, GeoPolygons.FleetCode " &
                        "FROM GeoPolygons " & crit
            Dim mySda1 As New SqlDataAdapter(sqlStr, myConnection)
            mySda1.SelectCommand.CommandTimeout = 300
            Dim myDs1 As New DataSet
            Dim geoPolyName As String = String.Empty
            Dim geoPolygonExists As Boolean = False
            mySda1.Fill(myDs1)
            myConnection.Close()

            For Each dr In myDs1.Tables(0).Rows
                Dim geoObj As ClsPolyGeofence = New ClsPolyGeofence()
                geoPolygonExists = False
                If Not IsDBNull(dr("PolyName")) Then
                    geoPolyName = Trim(dr("PolyName"))
                End If
                For Each obj In PolyGeoFences
                    If obj.GeoFenceName = geoPolyName Then
                        geoPolygonExists = True
                    End If
                Next
                If geoPolygonExists = False Then
                    If Not IsDBNull(dr("UserId")) Then
                        geoObj.UserId = Trim(dr("UserId"))
                    End If
                    geoObj.Company = ""
                    geoObj.FleetCode = ""
                    If Not IsDBNull(dr("PolyName")) Then
                        geoObj.GeoFenceName = Trim(dr("PolyName"))
                    End If
                    If Not IsDBNull(dr("PolyType")) Then
                        geoObj.GeoFenceSource = dr("PolyType")
                    Else
                        geoObj.GeoFenceSource = "3"
                    End If
                    If Not IsDBNull(dr("Company")) Then
                        geoObj.Company = Trim(dr("Company"))
                    End If
                    If Not IsDBNull(dr("FleetCode")) Then
                        geoObj.FleetCode = Trim(dr("FleetCode"))
                    End If
                    If Not IsDBNull(dr("GeoId")) Then
                        geoObj.GeoId = Trim(dr("GeoId"))
                    End If
                    If Not IsDBNull(dr("Polygon")) Then
                        Dim coords As String = dr("Polygon")
                        If geoObj.GeoFenceSource = "3" Then
                            Dim parts As String() = coords.Split(New String() {"(("}, StringSplitOptions.RemoveEmptyEntries)
                            coords = Microsoft.VisualBasic.Left(parts(1), parts(1).Length - 2)
                            If coords.Contains("(") Then
                                coords = coords.Replace("(", "")
                            End If
                            Dim tmpArray() As String = coords.Split(",")
                            If tmpArray.Length > 0 Then
                                For j = 0 To tmpArray.Length - 1
                                    Dim tmpArray1() As String = Trim(tmpArray(j)).Split(" ")
                                    If tmpArray1.Length > 0 Then
                                        If tmpArray1(0).Contains("(") Then
                                            tmpArray1(0) = tmpArray1(0).Replace("(", "")
                                        End If
                                        geoObj.Polygon += tmpArray1(1) & ", " & tmpArray1(0)
                                        If j < tmpArray.Length - 1 Then
                                            geoObj.Polygon += "#"
                                        End If
                                    End If
                                Next
                            End If
                        ElseIf geoObj.GeoFenceSource = "4" Then
                            If coords.Contains("MULTILINESTRING") Then
                                coords = coords.Replace("MULTILINESTRING ((", "")
                            End If
                            If coords.Contains("LINESTRING") Then
                                coords = coords.Replace("LINESTRING (", "")
                            End If
                            coords = coords.Replace("))", "")
                            coords = coords.Replace(")", "")
                            coords = coords.Replace("(", "")
                            Dim tmpArray() As String = coords.Split(",")
                            If tmpArray.Length > 0 Then
                                For j = 0 To tmpArray.Length - 1
                                    Dim tmpArray1() As String = Trim(tmpArray(j)).Split(" ")
                                    geoObj.Polygon += tmpArray1(1) & ", " & tmpArray1(0)
                                    If j < tmpArray.Length - 1 Then
                                        geoObj.Polygon += "#"
                                    End If
                                Next
                            End If
                        End If
                        PolyGeoFences.Add((geoObj))
                        i += 1
                    End If
                End If
            Next
            noPolygonGeofences = i

        Catch ex As Exception
            Dim errMsg As String = ex.Message
        End Try


    End Sub
    Public Sub PlotPolygonGeofences()
        str_PolygonGeo = ""
        Dim i, j As Integer
        Dim sqlstr As String
        Dim noPolygonGeofences As Integer
        Dim polygonGeofenceInfo(1001, 7) As String
        Dim crit As String = ""
        If MapCompany <> "*" Then
            crit = "AND DT.Company = '" & MapCompany & "' "
            If MapFleet_Code <> "*" Then
                crit = "AND DT.Company = '" & MapCompany & "' AND DT.FleetCode = '" & MapFleet_Code & "'"
            End If
        End If

        Try
            'sqlstr = "SELECT GeoPolygons.UserId, GeoFences.Company, GeoFences.FleetCode, GeoPolygons.PolyName, GeoFences.GeoFenceSource, " & _
            '    "GeoPolygons.GeoPolygon.STAsText() AS Polygon, GeoPolygons.GeoId " & _
            '    "FROM GeoFences FULL OUTER JOIN GeoPolygons ON GeoFences.GeoFenceCode = Cast(GeoPolygons.GeoId as nvarchar(100)) " & _
            '    "Where GeoFences.GeoFenceSource IS NULL or GeoFences.GeoFenceSource = '3'"

            'From KSM 25/11/2014
            'This will get allocated GeoPolygons
            sqlstr = "SELECT dbo.GeoPolygons.UserId, dbo.GeoPolygons.PolyName, dbo.GeoPolygons.GeoId, DT.Company, DT.FleetCode, GeoPolygons.GeoPolygon.STAsText() AS Polygon, DT.SeqNo " &
                          "FROM  dbo.GeoPolygons LEFT JOIN  " &
                          "(SELECT dbo.GeoFences.GeoFenceCode , dbo.GeoFences.Company, dbo.GeoFences.FleetCode, dbo.GeoFences.SeqNo, dbo.GeoFences.GeoFenceSource " &
                          " FROM dbo.GeoFences " &
                          "WHERE dbo.GeoFences.GeoFenceSource = '3') DT " &
                          "ON CAST(dbo.GeoPolygons.GeoId AS nvarchar(100)) = DT.GeoFenceCode " &
                          "WHERE (GeoPolygons.PolyType = '3' OR GeoPolygons.PolyType = '6') " & crit

            Dim myConnection As New SqlConnection(MapInfoFleetConnectionString)
            Dim mySda As New SqlDataAdapter(sqlstr, myConnection)
            mySda.SelectCommand.CommandTimeout = 300
            Dim myDs As New DataSet
            mySda.Fill(myDs)
            myConnection.Close()
            i = 0
            Dim dr As DataRow
            For Each dr In myDs.Tables(0).Rows
                If Not IsDBNull(dr("UserId")) Then
                    polygonGeofenceInfo(i, 0) = Trim(dr("UserId"))
                End If
                If Not IsDBNull(dr("Company")) Then
                    polygonGeofenceInfo(i, 1) = Trim(dr("Company"))
                End If
                If Not IsDBNull(dr("FleetCode")) Then
                    polygonGeofenceInfo(i, 2) = Trim(dr("FleetCode"))
                End If
                If Not IsDBNull(dr("PolyName")) Then
                    polygonGeofenceInfo(i, 3) = Trim(dr("PolyName"))
                End If
                'If Not IsDBNull(dr("GeoFenceSource")) Then
                '    polygonGeofenceInfo(i, 4) = Trim(dr("GeoFenceSource"))
                'End If
                'If Not IsDBNull(dr("SeqNo")) Then
                '    polygonGeofenceInfo(i, 5) = dr("SeqNo")
                'End If
                If Not IsDBNull(dr("GeoId")) Then
                    polygonGeofenceInfo(i, 6) = Trim(dr("GeoId"))
                End If
                If Not IsDBNull(dr("Polygon")) Then
                    Dim coords As String = dr("Polygon")
                    Dim parts As String() = coords.Split(New String() {"(("}, StringSplitOptions.RemoveEmptyEntries)
                    coords = Microsoft.VisualBasic.Left(parts(1), parts(1).Length - 2)
                    Dim tmpArray() As String = coords.Split(",")
                    If tmpArray.Length > 0 Then
                        For j = 0 To tmpArray.Length - 1
                            Dim tmpArray1() As String = Trim(tmpArray(j)).Split(" ")
                            If tmpArray1.Length > 0 Then
                                polygonGeofenceInfo(i, 7) += tmpArray1(1) & ", " & tmpArray1(0)
                                If j < tmpArray.Length - 1 Then
                                    polygonGeofenceInfo(i, 7) += "#"
                                End If
                            End If
                        Next
                    End If
                End If
                i += 1
            Next

            'Now get the unallocated polygons
            sqlstr = "SELECT dbo.GeoPolygons.Company, dbo.GeoPolygons.FleetCode, dbo.GeoPolygons.UserId, dbo.GeoPolygons.PolyName, dbo.GeoPolygons.GeoId, GeoPolygons.GeoPolygon.STAsText() AS Polygon " &
              "FROM  dbo.GeoPolygons " &
              "WHERE (GeoPolygons.PolyType = '3' OR GeoPolygons.PolyType = '6') AND GeoPolygons.Company ='" & MapCompany & "'"

            Dim myConnection1 As New SqlConnection(MapInfoFleetConnectionString)
            Dim mySda1 As New SqlDataAdapter(sqlstr, myConnection1)
            mySda1.SelectCommand.CommandTimeout = 300
            Dim myDs1 As New DataSet
            mySda1.Fill(myDs1)
            myConnection1.Close()
            i = 0
            Dim dr1 As DataRow
            For Each dr1 In myDs1.Tables(0).Rows
                If Not IsDBNull(dr1("UserId")) Then
                    polygonGeofenceInfo(i, 0) = Trim(dr1("UserId"))
                End If
                If Not IsDBNull(dr1("Company")) Then
                    polygonGeofenceInfo(i, 1) = Trim(dr1("Company"))
                End If
                If Not IsDBNull(dr1("FleetCode")) Then
                    polygonGeofenceInfo(i, 2) = Trim(dr1("FleetCode"))
                End If
                If Not IsDBNull(dr1("PolyName")) Then
                    polygonGeofenceInfo(i, 3) = Trim(dr1("PolyName"))
                End If
                'If Not IsDBNull(dr("GeoFenceSource")) Then
                '    polygonGeofenceInfo(i, 4) = Trim(dr("GeoFenceSource"))
                'End If
                'If Not IsDBNull(dr("SeqNo")) Then
                '    polygonGeofenceInfo(i, 5) = dr("SeqNo")
                'End If
                If Not IsDBNull(dr1("GeoId")) Then
                    polygonGeofenceInfo(i, 6) = Trim(dr1("GeoId"))
                End If
                If Not IsDBNull(dr1("Polygon")) Then
                    Dim coords As String = dr1("Polygon")
                    Dim parts As String() = coords.Split(New String() {"(("}, StringSplitOptions.RemoveEmptyEntries)
                    coords = Microsoft.VisualBasic.Left(parts(1), parts(1).Length - 2)
                    Dim tmpArray() As String = coords.Split(",")
                    If tmpArray.Length > 0 Then
                        For j = 0 To tmpArray.Length - 1
                            Dim tmpArray1() As String = Trim(tmpArray(j)).Split(" ")
                            If tmpArray1.Length > 0 Then
                                polygonGeofenceInfo(i, 7) += tmpArray1(1) & ", " & tmpArray1(0)
                                If j < tmpArray.Length - 1 Then
                                    polygonGeofenceInfo(i, 7) += "#"
                                End If
                            End If
                        Next
                    End If
                End If
                i += 1
            Next

            noPolygonGeofences = i
            For i = 0 To noPolygonGeofences - 1
                str_PolygonGeo += polygonGeofenceInfo(i, 0) & "|" & polygonGeofenceInfo(i, 1) & "|" & polygonGeofenceInfo(i, 2) & "|" & polygonGeofenceInfo(i, 3) &
                    "|" & polygonGeofenceInfo(i, 4) & "|" & polygonGeofenceInfo(i, 5) & "|" & polygonGeofenceInfo(i, 6) & "|" & polygonGeofenceInfo(i, 7) & "~"
            Next
        Catch ex As Exception
        End Try

    End Sub

    Public Sub PlotPolygonRoutes(ByVal strCompany As String, ByVal strFleetCode As String)

        str_PolygonRoutes = ""
        Dim i, j As Integer
        Dim sqlstr As String
        Dim noPolygonRoutes As Integer
        Dim polygonRoutesInfo(1001, 8) As String
        Dim crit As String = String.Empty
        Dim crit2 As String = String.Empty
        If strCompany = "*" Then
            crit = ""
            crit2 = ""
        ElseIf strFleetCode = "*" Then
            crit = "AND GeoPolygons.Company = '" & strCompany & "'"
            crit2 = "AND GeoPolygons.Company = '" & strCompany & "'"
        Else
            crit = "AND GeoPolygons.Company = '" & strCompany & "' AND GeoPolygons.FleetCode ='" & strFleetCode & "'"
            crit2 = "AND GeoPolygons.Company = '" & strCompany & "' AND GeoPolygons.FleetCode ='" & strFleetCode & "'"
        End If


        Try
            sqlstr = "SELECT dbo.GeoPolygons.UserId, dbo.GeoPolygons.PolyName, dbo.GeoPolygons.GeoId, DT.Company, DT.FleetCode, GeoPolygons.GeoPolygonNative.STAsText() AS Polygon, DT.SeqNo, dbo.GeoPolygons.WayPoints " &
                          "FROM  dbo.GeoPolygons LEFT JOIN  " &
                          "(SELECT dbo.GeoFences.GeoFenceCode , dbo.GeoFences.Company, dbo.GeoFences.FleetCode, dbo.GeoFences.SeqNo, dbo.GeoFences.GeoFenceSource " &
                          " FROM dbo.GeoFences " &
                          "WHERE dbo.GeoFences.GeoFenceSource = '4') DT " &
                          "ON CAST(dbo.GeoPolygons.GeoId AS nvarchar(100)) = DT.GeoFenceCode " &
                          "WHERE (GeoPolygons.PolyType = '4') " & crit

            Dim myConnection As New SqlConnection(MapInfoFleetConnectionString)
            Dim mySda As New SqlDataAdapter(sqlstr, myConnection)
            mySda.SelectCommand.CommandTimeout = 300
            Dim myDs As New DataSet
            mySda.Fill(myDs)
            myConnection.Close()
            i = 0
            Dim dr As DataRow
            For Each dr In myDs.Tables(0).Rows
                If Not IsDBNull(dr("UserId")) Then
                    polygonRoutesInfo(i, 0) = Trim(dr("UserId"))
                End If
                If Not IsDBNull(dr("Company")) Then
                    polygonRoutesInfo(i, 1) = Trim(dr("Company"))
                End If
                If Not IsDBNull(dr("FleetCode")) Then
                    polygonRoutesInfo(i, 2) = Trim(dr("FleetCode"))
                End If
                If Not IsDBNull(dr("PolyName")) Then
                    polygonRoutesInfo(i, 3) = Trim(dr("PolyName"))
                End If
                'If Not IsDBNull(dr("GeoFenceSource")) Then
                '    polygonGeofenceInfo(i, 4) = Trim(dr("GeoFenceSource"))
                'End If
                'If Not IsDBNull(dr("SeqNo")) Then
                '    polygonGeofenceInfo(i, 5) = dr("SeqNo")
                'End If
                If Not IsDBNull(dr("GeoId")) Then
                    polygonRoutesInfo(i, 6) = Trim(dr("GeoId"))
                End If
                If Not IsDBNull(dr("Polygon")) Then
                    Dim coords As String = dr("Polygon")
                    If coords.Contains("MULTILINESTRING") Then
                        coords = coords.Replace("MULTILINESTRING ((", "")
                    End If
                    If coords.Contains("LINESTRING") Then
                        coords = coords.Replace("LINESTRING (", "")
                    End If
                    coords = coords.Replace("))", "")
                    coords = coords.Replace(")", "")
                    coords = coords.Replace("(", "")
                    Dim tmpArray() As String = coords.Split(",")
                    If tmpArray.Length > 0 Then
                        For j = 0 To tmpArray.Length - 1
                            Dim tmpArray1() As String = Trim(tmpArray(j)).Split(" ")
                            If tmpArray1.Length > 0 Then
                                polygonRoutesInfo(i, 7) += tmpArray1(1) & ", " & tmpArray1(0)
                                If j < tmpArray.Length - 1 Then
                                    polygonRoutesInfo(i, 7) += "#"
                                End If
                            End If
                        Next
                    End If
                End If
                If Not IsDBNull(dr("WayPoints")) Then
                    polygonRoutesInfo(i, 8) = Trim(dr("WayPoints"))
                End If
                i += 1
            Next
            noPolygonRoutes = i
            str_PolygonRoutesWayPoints = ""
            str_PolygonRoutes = ""
            For i = 0 To noPolygonRoutes - 1
                str_PolygonRoutes += polygonRoutesInfo(i, 0) & "|" & polygonRoutesInfo(i, 1) & "|" & polygonRoutesInfo(i, 2) & "|" & polygonRoutesInfo(i, 3) &
                    "|" & polygonRoutesInfo(i, 4) & "|" & polygonRoutesInfo(i, 5) & "|" & polygonRoutesInfo(i, 6) & "|" & polygonRoutesInfo(i, 7) & "~"
                Dim tmpWayPoints() As String = polygonRoutesInfo(i, 8).Split("[")
                For j = 1 To tmpWayPoints.Length - 1
                    tmpWayPoints(j) = tmpWayPoints(j).Replace("]", "")
                    str_PolygonRoutesWayPoints += polygonRoutesInfo(i, 3) & "|" & polygonRoutesInfo(i, 6) & "|" & tmpWayPoints(j) & "~"
                Next
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Function TimeStringToSeconds(ByVal timeStr As String) As Integer
        Dim timeArray() As String = timeStr.Split(":")
        Dim tmpHours = timeArray(0) * 3600
        Dim tmpMins = timeArray(1) * 60
        Dim tmpSecs = timeArray(2)
        TimeStringToSeconds = CInt(tmpHours) + CInt(tmpMins) + CInt(tmpSecs)

    End Function
    Public Sub LoadGeofenceArray()
        Dim sqlStr As String = ""
        Dim i As Integer

        Dim crit As String = String.Empty

        Dim myReader As SqlDataReader = Nothing
        Try
            Dim myConnection As New SqlConnection(MapInfoFleetConnectionString)
            myConnection.Open()
            If MapCompany <> "*" Then
                crit = "AND GeoFences.Company = '" & MapCompany & "'"
            End If
            If MapCompany <> "*" And MapFleet_Code <> "*" Then
                crit = crit & " AND GeoFences.FleetCode = '" & MapFleet_Code & "'"
            End If

            sqlStr = "SELECT Company, FleetCode, GeoFenceCode, GeoFenceName, Radius, MaxTimeAtSite, AlertType, Latitude, Longitude, SpeedLimit, GeoFenceSource " &
                "FROM GeoFences WHERE GeoFenceSource = '1' " & crit & " Order by GeoFenceName"

            Dim mySDA As New SqlDataAdapter(sqlStr, myConnection)
            mySDA.SelectCommand.CommandTimeout = 300
            Dim myDS As New DataSet
            mySDA.Fill(myDS)
            mySDA.Dispose()
            myConnection.Close()
            i = 0
            Try

                AllGeofences = New List(Of ClsGeofences)
                For Each dr As DataRow In myDS.Tables(0).Rows
                    Dim geoObj As ClsGeofences = New ClsGeofences()
                    If Not IsDBNull(dr("Company")) Then
                        geoObj.GeofenceCompany = Trim(dr("Company"))                     'Company
                    End If
                    If Not IsDBNull(dr("FleetCode")) Then
                        geoObj.GeofenceFleet = Trim(dr("FleetCode"))                            'FleetCode
                    End If
                    If Not IsDBNull(dr("GeoFenceCode")) Then
                        geoObj.GeofenceGeoCode = dr("GeoFenceCode")                          'GeoFenceCode
                    End If

                    If Not IsDBNull(dr("GeoFenceName")) Then
                        geoObj.GeofenceGeoName = dr("GeoFenceName")                            'GeoFenceName
                    End If

                    If Not IsDBNull(dr("Radius")) Then
                        geoObj.GeofenceRadius = dr("Radius")                                       'Radius
                    End If

                    If Not IsDBNull(dr("MaxTimeAtSite")) Then                                                'MaxTimeAtSite
                        Dim myMaxTime As Int32
                        myMaxTime = dr("MaxTimeAtSite")
                        If myMaxTime > 0 Then
                            geoObj.MaxTimeAtGeoFence = myMaxTime
                        Else
                            geoObj.MaxTimeAtGeoFence = 0
                        End If
                    End If
                    If Not IsDBNull(dr("GeoFenceSource")) Then
                        geoObj.GeoFenceSource = dr("GeoFenceSource")
                    End If
                    If Not IsDBNull(dr("AlertType")) Then
                        geoObj.GeofenceAlertType = dr("AlertType")                           'AlertType
                    End If

                    If Not IsDBNull(dr("Latitude")) Then
                        geoObj.GeofenceLatitude = dr("Latitude")                       'Latitude
                    End If

                    If Not IsDBNull(dr("Longitude")) Then
                        geoObj.GeofenceLongitude = dr("Longitude")                      'Longitude
                    End If

                    If Not IsDBNull(dr("SpeedLimit")) Then                      'SpeedLimit
                        geoObj.SpeedInGeoFence = dr("SpeedLimit")
                    End If
                    AllGeofences.Add((geoObj))
                    i = i + 1
                Next
            Catch ex As Exception
            End Try

        Catch exc As Exception
        End Try
        NoGeofences = i
        Try
            If IsMSTrackFlag = False Then
                ' GetPolygonGeofences()
            Else
                str_GeofenceArray = JsonConvert.SerializeObject(AllGeofences)
            End If

        Catch exc As Exception
        End Try
    End Sub

    Public Sub GetPolygonGeofences()
        Dim i, j, k As Integer
        Dim noPolygonGeofences As Integer
        Dim crit As String = String.Empty
        Dim sqlStr As String = String.Empty
        Dim myConnection As New SqlConnection(MapInfoFleetConnectionString)
        myConnection.Open()
        Try

            If MapCompany = "*" Then
                crit = ""
            ElseIf MapFleet_Code = "*" Then
                crit = "AND GeoFences.Company = '" & MapCompany & "'"
            Else
                crit = "AND GeoFences.Company = '" & MapCompany & "'"           ' AND GeoFences.FleetCode ='" & strFleetCode & "'"
            End If

            sqlStr = "SELECT GeoPolygons.UserId, GeoFences.Company, GeoFences.FleetCode, GeoPolygons.PolyName, GeoFences.GeoFenceSource, " &
                "GeoPolygons.GeoPolygon.STAsText() AS Polygon, GeoPolygons.GeoId, GeoFences.Radius " &
                "FROM GeoFences LEFT OUTER JOIN GeoPolygons ON GeoFences.GeoFenceCode = Cast(GeoPolygons.GeoId as nvarchar(100)) " &
                "Where (GeoFences.GeoFenceSource IS NULL OR GeoFences.GeoFenceSource = '3' OR GeoFences.GeoFenceSource = '6') " & crit


            sqlStr = sqlStr & " Order by GeoPolygons.GeoID"

            Dim mySda As New SqlDataAdapter(sqlStr, myConnection)
            mySda.SelectCommand.CommandTimeout = 300
            Dim myDs As New DataSet
            mySda.Fill(myDs)
            myConnection.Close()
            i = 0

            Dim dr As DataRow
            For Each dr In myDs.Tables(0).Rows
                If Not IsDBNull(dr("GeoId")) Then
                    If dr("GeoId") = "2879" Then
                        dr("GeoId") = dr("GeoId")
                    End If
                End If
                Dim geoObj As ClsGeofences = New ClsGeofences()
                If Not IsDBNull(dr("UserId")) Then
                    geoObj.UserId = Trim(dr("UserId"))
                End If
                If Not IsDBNull(dr("Company")) Then
                    geoObj.GeofenceCompany = Trim(dr("Company"))
                End If
                If Not IsDBNull(dr("FleetCode")) Then
                    geoObj.GeofenceFleet = Trim(dr("FleetCode"))
                End If
                If Not IsDBNull(dr("PolyName")) Then
                    geoObj.GeofenceGeoName = Trim(dr("PolyName"))
                End If
                If Not IsDBNull(dr("GeoFenceSource")) Then
                    geoObj.GeoFenceSource = Trim(dr("GeoFenceSource"))
                End If
                If Not IsDBNull(dr("GeoId")) Then
                    geoObj.GeoId = Trim(dr("GeoId"))
                End If
                If Not IsDBNull(dr("Radius")) Then
                    geoObj.GeofenceRadius = dr("Radius")
                End If
                If Not IsDBNull(dr("Polygon")) Then
                    Try
                        Dim polytypeArray() As String = dr("Polygon").ToString.Split("(")
                        If Trim(polytypeArray(0)) = "POLYGON" Then
                            Dim coords As String = dr("Polygon")
                            Dim parts As String() = coords.Split(New String() {"(("}, StringSplitOptions.RemoveEmptyEntries)
                            coords = Microsoft.VisualBasic.Left(parts(1), parts(1).Length - 2)
                            Dim tmpArray() As String = coords.Split(",")
                            If tmpArray.Length > 0 Then
                                For j = 0 To tmpArray.Length - 1
                                    Dim tmpArray1() As String = Trim(tmpArray(j)).Split(" ")
                                    If tmpArray1.Length > 0 Then
                                        geoObj.Polygon += tmpArray1(1) & ", " & tmpArray1(0)
                                        If j < tmpArray.Length - 1 Then
                                            geoObj.Polygon += "#"
                                        End If
                                    End If
                                Next
                            End If
                        ElseIf Trim(polytypeArray(0)) = "POINT" Then
                            Dim tmpArray1() As String = Trim(polytypeArray(1)).Split(" ")
                            geoObj.Polygon += tmpArray1(1).TrimEnd(")") & ", " & tmpArray1(0)
                        End If
                        AllGeofences.Add((geoObj))

                    Catch ex As Exception
                        MsgBox("Error in polygon: " & dr("GeoId") & "," & dr("Polygon") & ", " & ex.Message, MsgBoxStyle.Critical, "MST GoogleMaps - Load Allocated Polygon Geofences")
                        Exit For
                    End Try
                End If
                i += 1
            Next




            sqlStr = "SELECT GeoPolygons.GeoId, GeoPolygons.PolyName, GeoPolygons.DateChanged, GeoPolygons.UserId, GeoPolygons.GeoPolygon.STAsText() AS Polygon, GeoFences.Company, GeoPolygons.PolyType " &
                        "FROM GeoPolygons LEFT OUTER JOIN GeoFences ON CAST(GeoPolygons.GeoId AS nvarchar(100)) = GeoFences.GeoFenceCode " &
                        "WHERE (GeoPolygons.PolyType = '3' OR GeoPolygons.PolyType = '6') AND GeoPolygons.Company = '" & MapCompany & "'"
            Dim mySda1 As New SqlDataAdapter(sqlStr, myConnection)
            mySda1.SelectCommand.CommandTimeout = 300
            Dim myDs1 As New DataSet
            Dim geoPolyName As String = String.Empty
            Dim geofenceExists As Boolean = False
            mySda1.Fill(myDs1)
            myConnection.Close()
            'AllPolygonGeofences = New List(Of ClsGeofences)
            For Each dr In myDs1.Tables(0).Rows
                If Not IsDBNull(dr("GeoId")) Then
                    If dr("GeoId") = "2879" Then
                        dr("GeoId") = dr("GeoId")
                    End If
                End If
                Dim geoObj As ClsGeofences = New ClsGeofences()
                geofenceExists = False
                If Not IsDBNull(dr("PolyName")) Then
                    geoPolyName = Trim(dr("PolyName"))
                End If
                For k = 0 To AllGeofences.Count - 1
                    If AllGeofences(k).GeofenceGeoName = geoPolyName Then
                        geofenceExists = True
                    End If
                Next
                If geofenceExists = False Then
                    If Not IsDBNull(dr("UserId")) Then
                        geoObj.UserId = Trim(dr("UserId"))
                    End If
                    geoObj.GeofenceCompany = ""
                    geoObj.GeofenceFleet = ""
                    If Not IsDBNull(dr("PolyName")) Then
                        geoObj.GeofenceGeoName = Trim(dr("PolyName"))
                    End If
                    If Not IsDBNull(dr("PolyType")) Then
                        geoObj.GeoFenceSource = Trim(dr("PolyType"))
                    End If
                    If Not IsDBNull(dr("GeoId")) Then
                        geoObj.GeoId = Trim(dr("GeoId"))
                    End If
                    If Not IsDBNull(dr("Polygon")) Then
                        Try
                            Dim polytypeArray() As String = dr("Polygon").ToString.Split("(")
                            If Trim(polytypeArray(0)) = "POLYGON" Then
                                Dim coords As String = dr("Polygon")
                                Dim parts As String() = coords.Split(New String() {"(("}, StringSplitOptions.RemoveEmptyEntries)
                                coords = Microsoft.VisualBasic.Left(parts(1), parts(1).Length - 2)
                                If coords.Contains("(") Then
                                    coords = coords.Replace("(", "")
                                End If
                                Dim tmpArray() As String = coords.Split(",")
                                If tmpArray.Length > 0 Then
                                    For j = 0 To tmpArray.Length - 1
                                        Dim tmpArray1() As String = Trim(tmpArray(j)).Split(" ")
                                        If tmpArray1.Length > 0 Then
                                            If tmpArray1(0).Contains("(") Then
                                                tmpArray1(0) = tmpArray1(0).Replace("(", "")
                                            End If
                                            geoObj.Polygon += tmpArray1(1) & ", " & tmpArray1(0)
                                            If j < tmpArray.Length - 1 Then
                                                geoObj.Polygon += "#"
                                            End If
                                        End If
                                    Next
                                End If
                            ElseIf Trim(polytypeArray(0)) = "POINT" Then
                                Dim tmpArray1() As String = Trim(polytypeArray(1)).Split(" ")
                                geoObj.Polygon += tmpArray1(1).TrimEnd(")") & ", " & tmpArray1(0)
                            End If
                            AllGeofences.Add((geoObj))

                        Catch ex As Exception
                            MsgBox("Error in polygon: " & dr("GeoId") & "," & dr("Polygon") & ", " & ex.Message, MsgBoxStyle.Critical, "MST GoogleMaps - Load Unallocated Polygon Geofences")
                            Exit For
                        End Try
                    End If

                    i += 1
                End If
            Next
            noPolygonGeofences = i
            str_GeofenceArray = JsonConvert.SerializeObject(AllGeofences)
            ' str_PolygonGeo = JsonConvert.SerializeObject(AllPolygonGeofences)

        Catch ex As Exception
            Dim errMsg As String = ex.Message
        End Try


    End Sub

    Public Sub PlotGeoFences()

        Dim NoGeoFences As Integer

        Dim myInOutInt As Int16

        Dim tmpRadius As Double
        Dim tmpLatitude As Double
        Dim tmpLongitude As Double

        Dim sqlstr As String = ""
        Dim tmpCompany As String = ""
        Dim tmpFleet As String = ""
        Dim tmpGeoCode As String = ""
        Dim tmpGeoName As String = ""
        Dim tmpradiusStr As String = ""
        RouteGeofences = New List(Of ClsGeofences)
        ' GeoFences
        Dim crit As String = ""
        If MapCompany <> "*" Then
            crit = "dbo.GeoFences.Company = '" & MapCompany & "' "
            If MapFleet_Code <> "*" Then
                crit = "dbo.GeoFences.Company = '" & MapCompany & "' AND dbo.GeoFences.FleetCode = '" & MapFleet_Code & "'"
            End If
        End If
        ' First Load the database information
        'MsgBox("InfoFleetConnectionString " & InfoFleetConnectionString)
        Dim myConnection As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
        Dim myReader As SqlClient.SqlDataReader

        sqlstr = "SELECT dbo.GeoFences.Company, dbo.GeoFences.FleetCode, dbo.GeoFences.GeoFenceCode, " &
        "dbo.GeoFences.GeoFenceName, dbo.GeoFences.Radius, dbo.GeoFences.AlertType, " &
        "dbo.GeoFences.Latitude, dbo.GeoFences.Longitude FROM dbo.GeoFences " &
        "WHERE " & crit & " AND GeoFenceSource = '1'"

        Try
            NoGeoFences = 0

            myConnection.Open()

            Dim mySQLCommand As New SqlClient.SqlCommand(sqlstr, myConnection)

            myReader = mySQLCommand.ExecuteReader(CommandBehavior.Default)

            With myReader

                While .Read = True
                    Try
                        NoGeoFences = NoGeoFences + 1

                        If Not .IsDBNull(0) Then
                            tmpCompany = .GetString(0)
                        End If

                        If Not .IsDBNull(1) Then
                            tmpFleet = .GetString(1)
                        End If

                        If Not .IsDBNull(2) Then
                            tmpGeoCode = .GetString(2)
                        End If

                        If Not .IsDBNull(3) Then
                            tmpGeoName = .GetString(3)
                        End If

                        If Not .IsDBNull(4) Then
                            tmpRadius = .GetDouble(4)
                            ' Convert to Units being used

                            tmpradiusStr = CStr(tmpRadius)
                        End If

                        If Not .IsDBNull(5) Then
                            myInOutInt = .GetInt16(5)
                        End If

                        If Not .IsDBNull(6) Then
                            tmpLatitude = .GetDouble(6)
                        End If

                        If Not .IsDBNull(7) Then
                            tmpLongitude = .GetDouble(7)
                        End If
                        Dim geoObj As New ClsGeofences
                        geoObj.GeofenceCompany = tmpCompany
                        geoObj.GeofenceFleet = tmpFleet
                        geoObj.GeofenceGeoCode = tmpGeoCode
                        geoObj.GeofenceAlertType = CStr(myInOutInt)
                        geoObj.GeofenceGeoName = tmpGeoName
                        geoObj.GeofenceRadius = tmpRadius
                        geoObj.GeofenceLatitude = tmpLatitude
                        geoObj.GeofenceLongitude = tmpLongitude
                        RouteGeofences.Add(geoObj)

                    Catch exc As Exception
                        'MsgBox("PlotGeoFences - Get Database Info " & exc.Message)
                    End Try

                End While
            End With

        Catch ex As Exception
            'MsgBox("Loading Database " & ex.Message)
        End Try

        Try
            myConnection.Close()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub PlotSites()
        Dim sqlstr As String
        Dim tmp As String = ""
        Dim SiteInfo(10001, 14) As String
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
            crit = "SiteInfo.Company = '" & MapCompany & "'"
        End If
        Try

            sqlstr = "SELECT SiteInfo.Name, SiteInfo.Code, SiteInfo.Category, Categories.CatDesc, Categories.CatIcon, SiteInfo.Comments, SiteInfo.Latitude, SiteInfo.Longitude, " &
                "SiteInfo.SiteSeqNo, SiteInfo.Company, SiteInfo.Fleet_Code, SiteInfo.ClientID, SiteInfo.Surname, SiteInfo.FirstName, SiteInfo.Tel " &
                "FROM SiteInfo LEFT OUTER JOIN Categories ON SiteInfo.Category = Categories.CatCode Where " & crit &
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
                            End If

                            If Not .IsDBNull(4) Then 'CatIcon
                                SiteInfo(i, 4) = Trim(.GetString(4))
                            End If

                            If Not .IsDBNull(5) Then 'Comments
                                SiteInfo(i, 5) = Trim(.GetString(5))
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
                                'FirstName
                            End If
                            If Not .IsDBNull(14) Then
                                SiteInfo(i, 14) = Trim(.GetString(14))
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
                str_SitesArray = ""
                Try
                    For i = 0 To NoSites - 1
                        If SiteInfo(i, 6) <> "0" And SiteInfo(i, 7) <> "0" Then
                            str_SitesArray = str_SitesArray & SiteInfo(i, 0) & ","
                            str_SitesArray = str_SitesArray & SiteInfo(i, 8) & ","
                            str_SitesArray = str_SitesArray & SiteInfo(i, 2) & ","
                            str_SitesArray = str_SitesArray & SiteInfo(i, 3) & ","
                            str_SitesArray = str_SitesArray & SiteInfo(i, 4) & ","
                            str_SitesArray = str_SitesArray & SiteInfo(i, 5) & ","
                            str_SitesArray = str_SitesArray & SiteInfo(i, 6) & ","
                            str_SitesArray = str_SitesArray & SiteInfo(i, 7) & ","
                            str_SitesArray = str_SitesArray & SiteInfo(i, 12) & ","
                            str_SitesArray = str_SitesArray & SiteInfo(i, 14) & ","
                            str_SitesArray = str_SitesArray & SiteInfo(i, 10) & "~"
                        End If
                    Next i
                Catch exc As Exception
                End Try

            Catch ex As Exception
                'MsgBox(ex.Message)
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

    End Sub     ' DisplaySitesOnMa
    '    Public Sub WriteToMapDebugLog(ByVal data As String)

    '    '   Routine to write to a text / log file
    '    Dim FileName As String

    '    FileName = Application.StartupPath & "\" & Format(Now, "yyyyMMdd") & "_MapDebug.log"

    '    data = Format(Now, "HH:mm:ss.f") & "  " & data

    '    Try
    '        FileOpen(1, FileName, OpenMode.Append, OpenAccess.Write, OpenShare.Shared)
    '        Print(1, data)
    '        Print(1, Chr(13) & Chr(10))
    '        FileClose(1)
    '    Catch exc As Exception
    '        'MsgBox(data, MsgBoxStyle.Critical, "Write file error " & exc.Message)
    '    End Try


    'End Sub ' WriteToErrorFile
End Module
