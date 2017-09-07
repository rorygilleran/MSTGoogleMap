<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMSTGoogleHistoryMap
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTGoogleHistoryMap))
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDeleteGeofenceM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCancelGeofenceM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCompleteGeofenceM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStartGeofenceM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCreateGeofenceM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPolygonGeofence = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteBufferZoneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCancelBufferZone = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCompleteBufferZone = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStartBufferZone = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateBufferZoneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBufferZones = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDeselectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCreateSite = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDisplayGeofence = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.HistoryMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.mnuPrintMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.LablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDisplayDate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDraw = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDrawLine = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDrawPolygon = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDistanceCalculator = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStartDistCalc = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearDistCalc = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEndDistCalc = New System.Windows.Forms.ToolStripMenuItem()
        Me.MapRefreshTimer = New System.Windows.Forms.Timer(Me.components)
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GM_History_Browser = New System.Windows.Forms.WebBrowser()
        Me.HistoryMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(189, 6)
        Me.ToolStripSeparator1.Visible = False
        '
        'mnuDeleteGeofenceM
        '
        Me.mnuDeleteGeofenceM.Name = "mnuDeleteGeofenceM"
        Me.mnuDeleteGeofenceM.Size = New System.Drawing.Size(161, 22)
        Me.mnuDeleteGeofenceM.Text = "Delete Geofence"
        '
        'mnuCancelGeofenceM
        '
        Me.mnuCancelGeofenceM.Name = "mnuCancelGeofenceM"
        Me.mnuCancelGeofenceM.Size = New System.Drawing.Size(126, 22)
        Me.mnuCancelGeofenceM.Text = "Cancel"
        '
        'mnuCompleteGeofenceM
        '
        Me.mnuCompleteGeofenceM.Name = "mnuCompleteGeofenceM"
        Me.mnuCompleteGeofenceM.Size = New System.Drawing.Size(126, 22)
        Me.mnuCompleteGeofenceM.Text = "Complete"
        '
        'mnuStartGeofenceM
        '
        Me.mnuStartGeofenceM.Name = "mnuStartGeofenceM"
        Me.mnuStartGeofenceM.Size = New System.Drawing.Size(126, 22)
        Me.mnuStartGeofenceM.Text = "Start"
        '
        'mnuCreateGeofenceM
        '
        Me.mnuCreateGeofenceM.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuStartGeofenceM, Me.mnuCompleteGeofenceM, Me.mnuCancelGeofenceM})
        Me.mnuCreateGeofenceM.Name = "mnuCreateGeofenceM"
        Me.mnuCreateGeofenceM.Size = New System.Drawing.Size(161, 22)
        Me.mnuCreateGeofenceM.Text = "Create Geofence"
        '
        'mnuPolygonGeofence
        '
        Me.mnuPolygonGeofence.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCreateGeofenceM, Me.mnuDeleteGeofenceM})
        Me.mnuPolygonGeofence.Enabled = False
        Me.mnuPolygonGeofence.Name = "mnuPolygonGeofence"
        Me.mnuPolygonGeofence.Size = New System.Drawing.Size(192, 22)
        Me.mnuPolygonGeofence.Text = "Polygon Geofence"
        Me.mnuPolygonGeofence.Visible = False
        '
        'DeleteBufferZoneToolStripMenuItem
        '
        Me.DeleteBufferZoneToolStripMenuItem.Name = "DeleteBufferZoneToolStripMenuItem"
        Me.DeleteBufferZoneToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.DeleteBufferZoneToolStripMenuItem.Text = "Delete Buffer Zone"
        '
        'mnuCancelBufferZone
        '
        Me.mnuCancelBufferZone.Name = "mnuCancelBufferZone"
        Me.mnuCancelBufferZone.Size = New System.Drawing.Size(126, 22)
        Me.mnuCancelBufferZone.Text = "Cancel"
        '
        'mnuCompleteBufferZone
        '
        Me.mnuCompleteBufferZone.Name = "mnuCompleteBufferZone"
        Me.mnuCompleteBufferZone.Size = New System.Drawing.Size(126, 22)
        Me.mnuCompleteBufferZone.Text = "Complete"
        '
        'mnuStartBufferZone
        '
        Me.mnuStartBufferZone.Name = "mnuStartBufferZone"
        Me.mnuStartBufferZone.Size = New System.Drawing.Size(126, 22)
        Me.mnuStartBufferZone.Text = "Start"
        '
        'CreateBufferZoneToolStripMenuItem
        '
        Me.CreateBufferZoneToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuStartBufferZone, Me.mnuCompleteBufferZone, Me.mnuCancelBufferZone})
        Me.CreateBufferZoneToolStripMenuItem.Name = "CreateBufferZoneToolStripMenuItem"
        Me.CreateBufferZoneToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.CreateBufferZoneToolStripMenuItem.Text = "Create Buffer Zone"
        '
        'mnuBufferZones
        '
        Me.mnuBufferZones.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateBufferZoneToolStripMenuItem, Me.DeleteBufferZoneToolStripMenuItem})
        Me.mnuBufferZones.Enabled = False
        Me.mnuBufferZones.Name = "mnuBufferZones"
        Me.mnuBufferZones.Size = New System.Drawing.Size(192, 22)
        Me.mnuBufferZones.Text = "Buffer Zones"
        Me.mnuBufferZones.Visible = False
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBufferZones, Me.mnuPolygonGeofence, Me.mnuDeselectAll, Me.ToolStripSeparator1, Me.mnuCreateSite, Me.mnuDisplayGeofence})
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(47, 20)
        Me.mnuTools.Text = "Tools"
        Me.mnuTools.Visible = False
        '
        'mnuDeselectAll
        '
        Me.mnuDeselectAll.Enabled = False
        Me.mnuDeselectAll.Name = "mnuDeselectAll"
        Me.mnuDeselectAll.Size = New System.Drawing.Size(192, 22)
        Me.mnuDeselectAll.Text = "Deselect All"
        Me.mnuDeselectAll.Visible = False
        '
        'mnuCreateSite
        '
        Me.mnuCreateSite.Enabled = False
        Me.mnuCreateSite.Name = "mnuCreateSite"
        Me.mnuCreateSite.Size = New System.Drawing.Size(192, 22)
        Me.mnuCreateSite.Text = "Create Site "
        Me.mnuCreateSite.Visible = False
        '
        'mnuDisplayGeofence
        '
        Me.mnuDisplayGeofence.Enabled = False
        Me.mnuDisplayGeofence.Name = "mnuDisplayGeofence"
        Me.mnuDisplayGeofence.Size = New System.Drawing.Size(192, 22)
        Me.mnuDisplayGeofence.Text = "Display Site Geofences"
        Me.mnuDisplayGeofence.Visible = False
        '
        'mnuClose
        '
        Me.mnuClose.Name = "mnuClose"
        Me.mnuClose.Size = New System.Drawing.Size(198, 22)
        Me.mnuClose.Text = "&Close History Map Forn"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuClose})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "&File"
        '
        'HistoryMenuStrip
        '
        Me.HistoryMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuTools, Me.mnuPrintMap, Me.LablesToolStripMenuItem, Me.mnuClearMap, Me.mnuDraw, Me.mnuDistanceCalculator})
        Me.HistoryMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.HistoryMenuStrip.Name = "HistoryMenuStrip"
        Me.HistoryMenuStrip.Size = New System.Drawing.Size(988, 24)
        Me.HistoryMenuStrip.TabIndex = 44
        Me.HistoryMenuStrip.Text = "MenuStrip1"
        '
        'mnuPrintMap
        '
        Me.mnuPrintMap.Name = "mnuPrintMap"
        Me.mnuPrintMap.Size = New System.Drawing.Size(71, 20)
        Me.mnuPrintMap.Text = "Print Map"
        '
        'LablesToolStripMenuItem
        '
        Me.LablesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDisplayDate})
        Me.LablesToolStripMenuItem.Name = "LablesToolStripMenuItem"
        Me.LablesToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.LablesToolStripMenuItem.Text = "Labels"
        Me.LablesToolStripMenuItem.Visible = False
        '
        'mnuDisplayDate
        '
        Me.mnuDisplayDate.CheckOnClick = True
        Me.mnuDisplayDate.Name = "mnuDisplayDate"
        Me.mnuDisplayDate.Size = New System.Drawing.Size(139, 22)
        Me.mnuDisplayDate.Text = "Display Date"
        '
        'mnuClearMap
        '
        Me.mnuClearMap.Name = "mnuClearMap"
        Me.mnuClearMap.Size = New System.Drawing.Size(73, 20)
        Me.mnuClearMap.Text = "Clear Map"
        '
        'mnuDraw
        '
        Me.mnuDraw.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDrawLine, Me.mnuDrawPolygon})
        Me.mnuDraw.Name = "mnuDraw"
        Me.mnuDraw.Size = New System.Drawing.Size(46, 20)
        Me.mnuDraw.Text = "Draw"
        Me.mnuDraw.Visible = False
        '
        'mnuDrawLine
        '
        Me.mnuDrawLine.Name = "mnuDrawLine"
        Me.mnuDrawLine.Size = New System.Drawing.Size(118, 22)
        Me.mnuDrawLine.Text = "Line"
        '
        'mnuDrawPolygon
        '
        Me.mnuDrawPolygon.Name = "mnuDrawPolygon"
        Me.mnuDrawPolygon.Size = New System.Drawing.Size(118, 22)
        Me.mnuDrawPolygon.Text = "Polygon"
        Me.mnuDrawPolygon.Visible = False
        '
        'mnuDistanceCalculator
        '
        Me.mnuDistanceCalculator.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuStartDistCalc, Me.mnuClearDistCalc, Me.mnuEndDistCalc})
        Me.mnuDistanceCalculator.Name = "mnuDistanceCalculator"
        Me.mnuDistanceCalculator.Size = New System.Drawing.Size(121, 20)
        Me.mnuDistanceCalculator.Text = "Distance Calculator"
        '
        'mnuStartDistCalc
        '
        Me.mnuStartDistCalc.Name = "mnuStartDistCalc"
        Me.mnuStartDistCalc.Size = New System.Drawing.Size(101, 22)
        Me.mnuStartDistCalc.Text = "Start"
        '
        'mnuClearDistCalc
        '
        Me.mnuClearDistCalc.Name = "mnuClearDistCalc"
        Me.mnuClearDistCalc.Size = New System.Drawing.Size(101, 22)
        Me.mnuClearDistCalc.Text = "Clear"
        '
        'mnuEndDistCalc
        '
        Me.mnuEndDistCalc.Name = "mnuEndDistCalc"
        Me.mnuEndDistCalc.Size = New System.Drawing.Size(101, 22)
        Me.mnuEndDistCalc.Text = "End"
        '
        'MapRefreshTimer
        '
        Me.MapRefreshTimer.Enabled = True
        Me.MapRefreshTimer.Interval = 300000
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.AutoSize = True
        Me.btnRefresh.BackColor = System.Drawing.SystemColors.MenuBar
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRefresh.Location = New System.Drawing.Point(928, 0)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(36, 24)
        Me.btnRefresh.TabIndex = 45
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Click to refresh map")
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'GM_History_Browser
        '
        Me.GM_History_Browser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GM_History_Browser.Location = New System.Drawing.Point(0, 24)
        Me.GM_History_Browser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.GM_History_Browser.Name = "GM_History_Browser"
        Me.GM_History_Browser.Size = New System.Drawing.Size(988, 706)
        Me.GM_History_Browser.TabIndex = 46
        '
        'frmMSTGoogleHistoryMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(988, 730)
        Me.Controls.Add(Me.GM_History_Browser)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.HistoryMenuStrip)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMSTGoogleHistoryMap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmMSTGoogleHistoryMap"
        Me.HistoryMenuStrip.ResumeLayout(False)
        Me.HistoryMenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents PrintDialog1 As Windows.Forms.PrintDialog
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuDeleteGeofenceM As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCancelGeofenceM As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCompleteGeofenceM As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStartGeofenceM As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCreateGeofenceM As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPolygonGeofence As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteBufferZoneToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCancelBufferZone As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCompleteBufferZone As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStartBufferZone As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateBufferZoneToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBufferZones As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDeselectAll As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCreateSite As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDisplayGeofence As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClose As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFile As Windows.Forms.ToolStripMenuItem
    Friend WithEvents HistoryMenuStrip As Windows.Forms.MenuStrip
    Friend WithEvents mnuClearMap As Windows.Forms.ToolStripMenuItem
    Friend WithEvents LablesToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDisplayDate As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDraw As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDrawLine As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDrawPolygon As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDistanceCalculator As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStartDistCalc As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClearDistCalc As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEndDistCalc As Windows.Forms.ToolStripMenuItem
    Friend WithEvents MapRefreshTimer As Windows.Forms.Timer
    Friend WithEvents btnRefresh As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents mnuPrintMap As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GM_History_Browser As Windows.Forms.WebBrowser
End Class
