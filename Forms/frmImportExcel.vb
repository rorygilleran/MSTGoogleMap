Imports System.Data
Imports System.Windows.Forms
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class frmImportExcel
    Private Sub frmImportExcel_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim hasHeaders As Boolean = True
        Dim WorkSheetName As String = "Sheet1$"
        With OpenFileDialog1
            .DefaultExt = ".xlsx"
            .AddExtension = True
            .Filter = "Excel Files|*.xls;*.xlsx"
        End With

        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        Try
            If result = vbOK Then
                Try
                    Dim thisFile As String = OpenFileDialog1.FileName
                    Dim tmpArray() As String = thisFile.Split(".")
                    Dim thisFileExt As String = "." & UCase(Trim(tmpArray(1)))
                    Try
                        Dim connString As String = Nothing
                        If thisFileExt = ".XLS" Then
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & thisFile & ";Extended Properties=Excel 8.0"
                        ElseIf thisFileExt = ".XLSX" Then
                            If hasHeaders = True Then
                                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & thisFile & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1"""
                            Else
                                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & thisFile & ";Extended Properties=""Excel 12.0 Xml;HDR=NO;IMEX=1"""
                            End If
                        ElseIf thisFileExt = ".CSV" Then
                            'connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + StoredPath + ";Extended Properties=""text;FMT=Delimited(,);HDR=YES"""
                        End If
                        Dim oledbConn As OleDb.OleDbConnection = New OleDb.OleDbConnection(connString)
                        Dim myCommand As String = "SELECT * FROM [" & WorkSheetName & "]"
                        '***********************
                        Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(myCommand, oledbConn)
                        Dim oleda As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter()
                        dsExcelRoutes = New DataSet()
                        oleda.SelectCommand = cmd
                        oleda.Fill(dsExcelRoutes)

                    Catch ex As Exception

                        MsgBox("Unable to load excel file : " & ex.Message, MsgBoxStyle.Critical, "MST Viewer - Import Route.")

                    End Try
                Catch ex As Exception

                End Try
            End If
            Me.Dispose()
        Catch ex As Exception

        End Try
    End Sub
End Class