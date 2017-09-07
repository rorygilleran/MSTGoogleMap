Imports System.Windows.Forms

Public Class frmPolyGeofenceName
    Private Sub frmPolyGeofenceName_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If PolyType = "POLYGON" Then
            gbPoly.Visible = True
        Else
            If PolyType = "POLYLINE" Then
                gbPoly.Visible = False
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            Dim tmp As String = txtGeoFenceName.Text
            If txtGeoFenceName.Text = "" Then
                Exit Sub
            End If
            If PolyType = "POLYGON" Then
                PolygonGeofenceName = txtGeoFenceName.Text
                Me.DialogResult = DialogResult.OK
            End If

        Catch ex As Exception

        End Try
        Me.Dispose()
    End Sub

    Private Sub bntCancel_Click(sender As Object, e As EventArgs) Handles bntCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Dispose()
    End Sub

End Class