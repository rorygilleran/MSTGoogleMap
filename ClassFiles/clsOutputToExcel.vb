Imports System.Data
Imports System.IO
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Outlook = Microsoft.Office.Interop.Outlook

Public Class clsOutputToExcel
    Private oExcel As Excel.Application
    Public Sub ExportTurnsToExcel(ByVal turns As List(Of ClsTurns), Optional ByVal IsEmail As Boolean = False)
        Dim i As Integer = 2
        Dim savedFilePath As String
        'Make sure the reports folder
        Try
            MkDir(System.Windows.Forms.Application.StartupPath & "\Routes\")
        Catch ex As Exception
        End Try
        'CleanUpReportsFolder()
        Dim oExcel As Excel.Application
        oExcel = New Microsoft.Office.Interop.Excel.Application
        Dim excelBook As Excel.Workbook     ' = oExcel.Workbooks.Add(System.Reflection.Missing.Value)
        excelBook = oExcel.Workbooks.Add
        savedFilePath = System.Windows.Forms.Application.StartupPath & "\Routes\" & selectedRoute & ".xlsx"
        Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets(1), Excel.Worksheet)
        excelBook.Worksheets(1).Select()
        excelWorksheet.Name = "Turns"
        With excelWorksheet
            .Range("A1").Value = "Turn"
            .Range("A1").Font.Bold = True
            .Range("A1").ColumnWidth = 5
            .Range("B1").Value = "Location"
            .Range("B1").Font.Bold = True
            .Range("B1").ColumnWidth = 75
            .Range("C1").Value = "Distance"
            .Range("C1").Font.Bold = True
            .Range("C1").ColumnWidth = 15
            .Range("D1").Value = "Time"
            .Range("D1").Font.Bold = True
            .Range("D1").ColumnWidth = 10
            .Range("C:C").HorizontalAlignment = Excel.Constants.xlRight

            For Each obj As ClsTurns In turns
                .Range("A" & i.ToString).Value = obj.Turn
                .Range("B" & i.ToString).Value = obj.Location
                .Range("C" & i.ToString).Value = obj.Distance
                .Range("D" & i.ToString).Value = obj.Time
                i += 1
            Next
            Try
                Kill(savedFilePath)
            Catch ex As Exception
            End Try
            Try

                ' Make sure that you release object references. 
                excelBook.SaveAs(savedFilePath)
                excelBook.Close()

                releaseObject(excelWorksheet)
                excelWorksheet = Nothing
                releaseObject(excelBook)
                oExcel.Quit()
                releaseObject(oExcel)
                oExcel = Nothing

                If IsEmail = False Then
                    MsgBox("Route '" & selectedRoute & "' has been saved to the Routes folder. (" & savedFilePath & ")", MsgBoxStyle.Information, "MST Viewer - Export Route")
                Else
                    'sendOutLookEmail(savedFilePath)
                End If
            Catch ex As Exception
                MsgBox("Error Saving Excel  " & selectedRoute & " in Reports Folder - " & ex.Message, MsgBoxStyle.Critical, "Export " & selectedRoute & " To Excel")
            End Try

        End With

    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub CleanUpReportsFolder()

        Dim files() As String
        files = Directory.GetFileSystemEntries(System.Windows.Forms.Application.StartupPath & "\Routes\")

        For Each element As String In files
            If (Not Directory.Exists(element)) Then
                File.Delete(Path.Combine(System.Windows.Forms.Application.StartupPath & "\Routes\", Path.GetFileName(element)))
            End If
        Next
    End Sub
    Private Sub sendOutLookEmail(ByVal savedFilePath)
        'Try


        '    Dim Outl As New Outlook.Application
        '    Outl = CreateObject("Outlook.Application")
        '    If Outl IsNot Nothing Then
        '        Dim omsg As Object
        '        omsg = Outl.CreateItem(0) '=Outlook.OlItemType.olMailItem'
        '        'set message properties here...'

        '        omsg.Display(True) 'will display message to user
        '    End If
        'Catch ex As Exception
        '     MsgBox("Error Creating Enail for " & selectedRoute &": " & ex.Message, MsgBoxStyle.Critical, "Email " & selectedRoute)
        'End Try
    End Sub

End Class
