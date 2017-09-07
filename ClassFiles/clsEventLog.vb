Imports System
Imports System.Diagnostics
Public Class clsEventLog
    Private Shared sSource As String = "MSTGoogleMap"
    Private Shared sLog As String = "Application"
    Private Shared sEvent As String = "Sample Event"
    Private Shared sMachine As String = "."

    Public Shared Sub initEventLog()
        If Not EventLog.SourceExists(sSource, sMachine) Then
            Dim mySourceData As EventSourceCreationData = New EventSourceCreationData(sSource, sLog)
            EventLog.CreateEventSource(mySourceData)
        End If
    End Sub
    Public Shared Sub writeEventLog(message As String, strLevel As String)
        initEventLog()
        If ClsMstGoogleMaps.IsMSTrackFlag = True Then
            sSource = "MSTGoogleMap - MSTrack Pro"
            Else
            sSource = "MSTGoogleMap - MST Viewer"
        End If
        Dim logEntryType As EventLogEntryType
        Select Case strLevel
            Case "Info"
                logEntryType = EventLogEntryType.Information
            Case "Warning"
                logEntryType = EventLogEntryType.Warning
            Case "Error"
                logEntryType = EventLogEntryType.Error
        End Select
        Dim ELog As New EventLog(sLog, sMachine, sSource)
        ELog.WriteEntry(message, logEntryType, 234, CType(3, Short))
    End Sub

End Class
