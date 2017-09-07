Imports System.Windows.Forms
Imports System.Xml
Imports System.Data.SqlClient

Public Class frmImportKML

    Private Sub frmImportKML_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim shapeName As String = ""
        Dim shapeType As String = ""
        Dim shapeCoords As String = ""
        With OpenFileDialog1
            .DefaultExt = ".kml"
            .AddExtension = True
            .Filter = "kml Files|*.kml"
        End With

        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        If result = vbOK Then
            Try
                Dim thisfile As String = OpenFileDialog1.FileName
                kmlRouteUri = "file:///" & thisfile
            Catch ex As Exception

            End Try
        End If
        Me.Dispose()
    End Sub
End Class