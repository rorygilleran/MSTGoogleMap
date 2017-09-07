<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPolyGeofenceName
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.gbPoly = New System.Windows.Forms.GroupBox()
        Me.txtGeoFenceName = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.bntCancel = New System.Windows.Forms.Button()
        Me.gbPoly.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(54, 75)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(73, 21)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'gbPoly
        '
        Me.gbPoly.Controls.Add(Me.txtGeoFenceName)
        Me.gbPoly.Controls.Add(Me.lblName)
        Me.gbPoly.Location = New System.Drawing.Point(12, 12)
        Me.gbPoly.Name = "gbPoly"
        Me.gbPoly.Size = New System.Drawing.Size(267, 57)
        Me.gbPoly.TabIndex = 5
        Me.gbPoly.TabStop = False
        Me.gbPoly.Text = "Save Polygon Geofence"
        '
        'txtGeoFenceName
        '
        Me.txtGeoFenceName.Location = New System.Drawing.Point(106, 20)
        Me.txtGeoFenceName.MaxLength = 30
        Me.txtGeoFenceName.Name = "txtGeoFenceName"
        Me.txtGeoFenceName.Size = New System.Drawing.Size(136, 20)
        Me.txtGeoFenceName.TabIndex = 2
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(15, 23)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(85, 13)
        Me.lblName.TabIndex = 2
        Me.lblName.Text = "Geofence Name"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'bntCancel
        '
        Me.bntCancel.Location = New System.Drawing.Point(181, 75)
        Me.bntCancel.Name = "bntCancel"
        Me.bntCancel.Size = New System.Drawing.Size(73, 21)
        Me.bntCancel.TabIndex = 6
        Me.bntCancel.Text = "Cancel"
        Me.bntCancel.UseVisualStyleBackColor = True
        '
        'frmPolyGeofenceName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(290, 101)
        Me.ControlBox = False
        Me.Controls.Add(Me.gbPoly)
        Me.Controls.Add(Me.bntCancel)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPolyGeofenceName"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.gbPoly.ResumeLayout(False)
        Me.gbPoly.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents gbPoly As System.Windows.Forms.GroupBox
    Friend WithEvents txtGeoFenceName As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents bntCancel As System.Windows.Forms.Button
End Class
