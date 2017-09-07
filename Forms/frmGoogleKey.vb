Public Class frmGoogleKey
    Public Event SaveGoogleID(ByVal data As String)
    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        theGoogleMapKey = txtGoogleKey.Text
        RaiseEvent SaveGoogleID(theGoogleMapKey)
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        theGoogleMapKey = "No Valid Google Key"
        RaiseEvent SaveGoogleID(theGoogleMapKey)
        Me.Close()
    End Sub
End Class