Module modMSTGoogle_MapsDBChecks
    Public Sub CreateSavePolyonGeofenceSp()
        Dim spExists As Boolean = False

        Try
            Dim sqlstr As String = "Select * from sys.objects where type_desc = 'SQL_STORED_PROCEDURE' AND name = 'MST_SavePolygonGeofence'"
            Dim myConnection As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
            myConnection.Open()
            Dim mySqlCommand As New SqlClient.SqlCommand(sqlstr, myConnection)
            Dim myReader As SqlClient.SqlDataReader

            Try
                myReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
                If Not mySqlCommand Is Nothing Then
                    mySqlCommand.Dispose()
                End If

                While myReader.Read
                    If Not myReader.IsDBNull(0) Then
                        spExists = True
                    End If
                End While

                myReader.Close()
                myReader.Dispose()

            Catch ex As Exception

            End Try
            Try
                If spExists = False Then
                    Dim sqlstr1 As String = "CREATE PROCEDURE [dbo].[MST_SavePolygonGeofence]" & Chr(13) & _
                                "@PolyName nvarchar(255), " & Chr(13) & _
                                "@UserId nvarchar(50), " & Chr(13) & _
                                "@GeoCoords nvarchar(max)" & Chr(13) & _
                    "AS " & Chr(13) & _
                    "BEGIN " & Chr(13) & _
                    "SET NOCOUNT ON;" & Chr(13) & _
                    "DECLARE @g geography; " & Chr(13) & _
                    "SET @g = geography::STPolyFromText('POLYGON(('+ @GeoCoords +'))', 4326); " & Chr(13) & _
                    "INSERT INTO dbo.GeoPolygons (PolyName, UserID, GeoPolygon) " & Chr(13) & _
                    "VALUES (@PolyName, @UserId, @g)" & Chr(13) & _
                    "END" & Chr(13)
                    Dim myConnection1 As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
                    myConnection1.Open()
                    Dim mySqlCommand1 As New SqlClient.SqlCommand(sqlstr1, myConnection1)
                    Dim myReader1 As SqlClient.SqlDataReader = Nothing
                    Try
                        myReader1 = mySqlCommand1.ExecuteReader(CommandBehavior.CloseConnection)
                    Catch exc As Exception
                        MsgBox("Unable to create SP - MST_SavePolygonGeofence. Please contact support", vbCritical, "MS Track Pro 8 Create Stored Procedure.")
                    End Try
                    myReader1.Close()
                    myReader1.Dispose()
                End If
               
            Catch exc As Exception
                'MsgBox(exc.Message)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Sub CreateUpdatePolyonGeofenceSp()
        Dim spExists As Boolean = False

        Try
            Dim sqlstr As String = "Select * from sys.objects where type_desc = 'SQL_STORED_PROCEDURE' AND name = 'MST_UpdatePolygonGeofence'"
            Dim myConnection As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
            myConnection.Open()
            Dim mySqlCommand As New SqlClient.SqlCommand(sqlstr, myConnection)
            Dim myReader As SqlClient.SqlDataReader

            Try
                myReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
                If Not mySqlCommand Is Nothing Then
                    mySqlCommand.Dispose()
                End If

                While myReader.Read
                    If Not myReader.IsDBNull(0) Then
                        spExists = True
                    End If
                End While

                myReader.Close()
                myReader.Dispose()

            Catch ex As Exception

            End Try
            Try
                If spExists = False Then
                    'Dim sqlstr1 As String = "GO " & _
                    '            "SET ANSI_NULLS ON " & _
                    '            "GO " & _
                    '            "SET QUOTED_IDENTIFIER ON " & _
                    '            "GO " & _
                    Dim sqlstr1 As String = "CREATE PROCEDURE [dbo].[MST_UpdatePolygonGeofence]" & Chr(13) & _
                                "@GeoId int, " & Chr(13) & _
                                "@GeoCoords nvarchar(max)" & Chr(13) & _
                    "AS " & Chr(13) & _
                    "BEGIN " & Chr(13) & _
                    "SET NOCOUNT ON;" & Chr(13) & _
                    "DECLARE @g geography; " & Chr(13) & _
                    "SET @g = geography::STPolyFromText('POLYGON(('+ @GeoCoords +'))', 4326); " & Chr(13) & _
                    "	Update dbo.GeoPolygons Set GeoPolygon = @g" & Chr(13) & _
                    "   Where GeoId = @GeoId" & Chr(13) & _
                    "END" & Chr(13)
                    Dim myConnection1 As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
                    myConnection1.Open()
                    Dim mySqlCommand1 As New SqlClient.SqlCommand(sqlstr1, myConnection1)
                    Dim myReader1 As SqlClient.SqlDataReader = Nothing
                    Try
                        myReader1 = mySqlCommand1.ExecuteReader(CommandBehavior.CloseConnection)

                    Catch exc As Exception
                        MsgBox("Unable to create SP - MST_UpdatePolygonGeofence. Please contact support", vbCritical, "MS Track Pro 8 Create Stored Procedure.")

                    End Try
                    myReader1.Close()
                    myReader1.Dispose()
                End If

            Catch exc As Exception
                'MsgBox(exc.Message)
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Public Sub CreateDeletePolyonGeofenceSp()
        Dim spExists As Boolean = False

        Try
            Dim sqlstr As String = "Select * from sys.objects where type_desc = 'SQL_STORED_PROCEDURE' AND name = 'MST_DeletePolygonGeofence'"
            Dim myConnection As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
            myConnection.Open()
            Dim mySqlCommand As New SqlClient.SqlCommand(sqlstr, myConnection)
            Dim myReader As SqlClient.SqlDataReader

            Try
                myReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
                If Not mySqlCommand Is Nothing Then
                    mySqlCommand.Dispose()
                End If

                While myReader.Read
                    If Not myReader.IsDBNull(0) Then
                        spExists = True
                    End If
                End While

                myReader.Close()
                myReader.Dispose()

            Catch ex As Exception

            End Try
            Try
                If spExists = False Then
                    'Dim sqlstr1 As String = "GO " & _
                    '            "SET ANSI_NULLS ON " & _
                    '            "GO " & _
                    '            "SET QUOTED_IDENTIFIER ON " & _
                    '            "GO " & _
                    Dim sqlstr1 As String = "CREATE PROCEDURE [dbo].[MST_DeletePolygonGeofence]" & Chr(13) & _
                                "@GeoId int " & Chr(13) & _
                    "AS " & Chr(13) & _
                    "BEGIN " & Chr(13) & _
                    "SET NOCOUNT ON;" & Chr(13) & _
                    "Delete From  dbo.GeoPolygons" & Chr(13) & _
                    "   Where GeoId = @GeoId" & Chr(13) & _
                    "END" & Chr(13)
                    Dim myConnection1 As New SqlClient.SqlConnection(MapInfoFleetConnectionString)
                    myConnection1.Open()
                    Dim mySqlCommand1 As New SqlClient.SqlCommand(sqlstr1, myConnection1)
                    Dim myReader1 As SqlClient.SqlDataReader = Nothing
                    Try
                        myReader1 = mySqlCommand1.ExecuteReader(CommandBehavior.CloseConnection)

                    Catch exc As Exception
                        MsgBox("Unable to create SP - MST_DeletePolygonGeofence. Please contact support", vbCritical, "MS Track Pro 8 Create Stored Procedure.")

                    End Try
                    myReader1.Close()
                    myReader1.Dispose()
                End If

            Catch exc As Exception
                'MsgBox(exc.Message)
            End Try
        Catch ex As Exception

        End Try
    End Sub

End Module
