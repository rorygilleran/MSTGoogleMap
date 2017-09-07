<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMSTGoogleMap
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTGoogleMap))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBufferZones = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateBufferZoneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStartBufferZone = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCompleteBufferZone = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCancelBufferZone = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteBufferZoneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPolygonGeofence = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCreateGeofenceM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStartGeofenceM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCompleteGeofenceM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCancelGeofenceM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDeleteGeofenceM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDeselectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCreateSite = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDisplayGeofence = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.LablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VehicleLabelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDisplayRegNo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDisplayDesc = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDisplayBoth = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDraw = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDrawLine = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDrawPolygon = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDistanceCalculator = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStartDistCalc = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearDistCalc = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEndDistCalc = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRoutes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCreateRoute = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShowRoutes = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportRoutesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImportRouteExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImportKML = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClearRoutes = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.MapRefreshTimer = New System.Windows.Forms.Timer(Me.components)
        Me.mainContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.pnlRouteHolder = New System.Windows.Forms.Panel()
        Me.pnlCreateRoutes = New System.Windows.Forms.Panel()
        Me.pnlWaypoints = New System.Windows.Forms.Panel()
        Me.pnlWaypointGrid = New System.Windows.Forms.Panel()
        Me.dgWayPoints = New System.Windows.Forms.DataGridView()
        Me.WayPoints = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlWaypointLabel = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.chkOptimizedRoute = New System.Windows.Forms.CheckBox()
        Me.btnClearRoute = New System.Windows.Forms.Button()
        Me.btnSaveRoute = New System.Windows.Forms.Button()
        Me.btnCreateRoute = New System.Windows.Forms.Button()
        Me.pnlShowRoutes = New System.Windows.Forms.Panel()
        Me.pnlRoutesView = New System.Windows.Forms.Panel()
        Me.tvRoutes = New System.Windows.Forms.TreeView()
        Me.pnlFiller = New System.Windows.Forms.Panel()
        Me.pnlClickMap = New System.Windows.Forms.Panel()
        Me.btnWayPointFromMap = New System.Windows.Forms.Button()
        Me.pnlAddress = New System.Windows.Forms.Panel()
        Me.btnFindAddress = New System.Windows.Forms.Button()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlSites = New System.Windows.Forms.Panel()
        Me.pnlSitesRight = New System.Windows.Forms.Panel()
        Me.cmbSites = New System.Windows.Forms.ComboBox()
        Me.pnlSitesLeft = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlGeofences = New System.Windows.Forms.Panel()
        Me.pnlGeoRight = New System.Windows.Forms.Panel()
        Me.cmbGeofences = New System.Windows.Forms.ComboBox()
        Me.pnlGeoLeft = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlVehicles = New System.Windows.Forms.Panel()
        Me.cmbVehicles = New System.Windows.Forms.ComboBox()
        Me.pnlVehRight = New System.Windows.Forms.Panel()
        Me.pnlVehLeft = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.mapTabPage = New System.Windows.Forms.TabPage()
        Me.GM_Browser = New System.Windows.Forms.WebBrowser()
        Me.dgLocationResults = New System.Windows.Forms.DataGridView()
        Me.colResults = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.turnTabPage = New System.Windows.Forms.TabPage()
        Me.pnlTurnsGrid = New System.Windows.Forms.Panel()
        Me.dgTurns = New System.Windows.Forms.DataGridView()
        Me.Turn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TurnLocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Distance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlTurnsHeader = New System.Windows.Forms.Panel()
        Me.btnEmail = New System.Windows.Forms.Button()
        Me.btnExportToExcel = New System.Windows.Forms.Button()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.mnuDeleteRoute = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextMnuTvRoutesDelMod = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuModifyRoute = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShowTurnsUnallocated = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextMnuTvRoutesMod = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuModifyAllocatedRoute = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShowTurnsAllocated = New System.Windows.Forms.ToolStripMenuItem()
        Me.bw1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.mainContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mainContainer1.Panel1.SuspendLayout()
        Me.mainContainer1.Panel2.SuspendLayout()
        Me.mainContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.pnlRouteHolder.SuspendLayout()
        Me.pnlCreateRoutes.SuspendLayout()
        Me.pnlWaypoints.SuspendLayout()
        Me.pnlWaypointGrid.SuspendLayout()
        CType(Me.dgWayPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlWaypointLabel.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlShowRoutes.SuspendLayout()
        Me.pnlRoutesView.SuspendLayout()
        Me.pnlClickMap.SuspendLayout()
        Me.pnlAddress.SuspendLayout()
        Me.pnlSites.SuspendLayout()
        Me.pnlSitesRight.SuspendLayout()
        Me.pnlSitesLeft.SuspendLayout()
        Me.pnlGeofences.SuspendLayout()
        Me.pnlGeoRight.SuspendLayout()
        Me.pnlGeoLeft.SuspendLayout()
        Me.pnlVehicles.SuspendLayout()
        Me.pnlVehLeft.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.mapTabPage.SuspendLayout()
        CType(Me.dgLocationResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.turnTabPage.SuspendLayout()
        Me.pnlTurnsGrid.SuspendLayout()
        CType(Me.dgTurns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTurnsHeader.SuspendLayout()
        Me.contextMnuTvRoutesDelMod.SuspendLayout()
        Me.contextMnuTvRoutesMod.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuTools, Me.mnuPrint, Me.mnuClearMap, Me.LablesToolStripMenuItem, Me.mnuDraw, Me.mnuDistanceCalculator, Me.mnuRoutes, Me.mnuClearRoutes})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1040, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuClose})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "&File"
        '
        'mnuClose
        '
        Me.mnuClose.Name = "mnuClose"
        Me.mnuClose.Size = New System.Drawing.Size(161, 22)
        Me.mnuClose.Text = "&Close Map Form"
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBufferZones, Me.mnuPolygonGeofence, Me.mnuDeselectAll, Me.ToolStripSeparator1, Me.mnuCreateSite, Me.mnuDisplayGeofence})
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(47, 20)
        Me.mnuTools.Text = "Tools"
        Me.mnuTools.Visible = False
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
        'CreateBufferZoneToolStripMenuItem
        '
        Me.CreateBufferZoneToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuStartBufferZone, Me.mnuCompleteBufferZone, Me.mnuCancelBufferZone})
        Me.CreateBufferZoneToolStripMenuItem.Name = "CreateBufferZoneToolStripMenuItem"
        Me.CreateBufferZoneToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.CreateBufferZoneToolStripMenuItem.Text = "Create Buffer Zone"
        '
        'mnuStartBufferZone
        '
        Me.mnuStartBufferZone.Name = "mnuStartBufferZone"
        Me.mnuStartBufferZone.Size = New System.Drawing.Size(126, 22)
        Me.mnuStartBufferZone.Text = "Start"
        '
        'mnuCompleteBufferZone
        '
        Me.mnuCompleteBufferZone.Name = "mnuCompleteBufferZone"
        Me.mnuCompleteBufferZone.Size = New System.Drawing.Size(126, 22)
        Me.mnuCompleteBufferZone.Text = "Complete"
        '
        'mnuCancelBufferZone
        '
        Me.mnuCancelBufferZone.Name = "mnuCancelBufferZone"
        Me.mnuCancelBufferZone.Size = New System.Drawing.Size(126, 22)
        Me.mnuCancelBufferZone.Text = "Cancel"
        '
        'DeleteBufferZoneToolStripMenuItem
        '
        Me.DeleteBufferZoneToolStripMenuItem.Name = "DeleteBufferZoneToolStripMenuItem"
        Me.DeleteBufferZoneToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.DeleteBufferZoneToolStripMenuItem.Text = "Delete Buffer Zone"
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
        'mnuCreateGeofenceM
        '
        Me.mnuCreateGeofenceM.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuStartGeofenceM, Me.mnuCompleteGeofenceM, Me.mnuCancelGeofenceM})
        Me.mnuCreateGeofenceM.Name = "mnuCreateGeofenceM"
        Me.mnuCreateGeofenceM.Size = New System.Drawing.Size(161, 22)
        Me.mnuCreateGeofenceM.Text = "Create Geofence"
        '
        'mnuStartGeofenceM
        '
        Me.mnuStartGeofenceM.Name = "mnuStartGeofenceM"
        Me.mnuStartGeofenceM.Size = New System.Drawing.Size(126, 22)
        Me.mnuStartGeofenceM.Text = "Start"
        '
        'mnuCompleteGeofenceM
        '
        Me.mnuCompleteGeofenceM.Name = "mnuCompleteGeofenceM"
        Me.mnuCompleteGeofenceM.Size = New System.Drawing.Size(126, 22)
        Me.mnuCompleteGeofenceM.Text = "Complete"
        '
        'mnuCancelGeofenceM
        '
        Me.mnuCancelGeofenceM.Name = "mnuCancelGeofenceM"
        Me.mnuCancelGeofenceM.Size = New System.Drawing.Size(126, 22)
        Me.mnuCancelGeofenceM.Text = "Cancel"
        '
        'mnuDeleteGeofenceM
        '
        Me.mnuDeleteGeofenceM.Name = "mnuDeleteGeofenceM"
        Me.mnuDeleteGeofenceM.Size = New System.Drawing.Size(161, 22)
        Me.mnuDeleteGeofenceM.Text = "Delete Geofence"
        '
        'mnuDeselectAll
        '
        Me.mnuDeselectAll.Enabled = False
        Me.mnuDeselectAll.Name = "mnuDeselectAll"
        Me.mnuDeselectAll.Size = New System.Drawing.Size(192, 22)
        Me.mnuDeselectAll.Text = "Deselect All"
        Me.mnuDeselectAll.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(189, 6)
        Me.ToolStripSeparator1.Visible = False
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
        'mnuPrint
        '
        Me.mnuPrint.Name = "mnuPrint"
        Me.mnuPrint.Size = New System.Drawing.Size(71, 20)
        Me.mnuPrint.Text = "Print Map"
        '
        'mnuClearMap
        '
        Me.mnuClearMap.Name = "mnuClearMap"
        Me.mnuClearMap.Size = New System.Drawing.Size(73, 20)
        Me.mnuClearMap.Text = "Clear Map"
        '
        'LablesToolStripMenuItem
        '
        Me.LablesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VehicleLabelsToolStripMenuItem})
        Me.LablesToolStripMenuItem.Name = "LablesToolStripMenuItem"
        Me.LablesToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.LablesToolStripMenuItem.Text = "Labels"
        '
        'VehicleLabelsToolStripMenuItem
        '
        Me.VehicleLabelsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDisplayRegNo, Me.mnuDisplayDesc, Me.mnuDisplayBoth})
        Me.VehicleLabelsToolStripMenuItem.Name = "VehicleLabelsToolStripMenuItem"
        Me.VehicleLabelsToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.VehicleLabelsToolStripMenuItem.Text = "Vehicle Labels"
        '
        'mnuDisplayRegNo
        '
        Me.mnuDisplayRegNo.Checked = True
        Me.mnuDisplayRegNo.CheckOnClick = True
        Me.mnuDisplayRegNo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuDisplayRegNo.Name = "mnuDisplayRegNo"
        Me.mnuDisplayRegNo.Size = New System.Drawing.Size(175, 22)
        Me.mnuDisplayRegNo.Text = "Display Reg No."
        '
        'mnuDisplayDesc
        '
        Me.mnuDisplayDesc.CheckOnClick = True
        Me.mnuDisplayDesc.Name = "mnuDisplayDesc"
        Me.mnuDisplayDesc.Size = New System.Drawing.Size(175, 22)
        Me.mnuDisplayDesc.Text = "Display Description"
        '
        'mnuDisplayBoth
        '
        Me.mnuDisplayBoth.CheckOnClick = True
        Me.mnuDisplayBoth.Name = "mnuDisplayBoth"
        Me.mnuDisplayBoth.Size = New System.Drawing.Size(175, 22)
        Me.mnuDisplayBoth.Text = "Display Both"
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
        'mnuRoutes
        '
        Me.mnuRoutes.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCreateRoute, Me.mnuShowRoutes, Me.ImportRoutesToolStripMenuItem})
        Me.mnuRoutes.Name = "mnuRoutes"
        Me.mnuRoutes.Size = New System.Drawing.Size(124, 20)
        Me.mnuRoutes.Text = "Route Management"
        '
        'mnuCreateRoute
        '
        Me.mnuCreateRoute.Name = "mnuCreateRoute"
        Me.mnuCreateRoute.Size = New System.Drawing.Size(159, 22)
        Me.mnuCreateRoute.Text = "Create Route"
        '
        'mnuShowRoutes
        '
        Me.mnuShowRoutes.Name = "mnuShowRoutes"
        Me.mnuShowRoutes.Size = New System.Drawing.Size(159, 22)
        Me.mnuShowRoutes.Text = "Show All Routes"
        '
        'ImportRoutesToolStripMenuItem
        '
        Me.ImportRoutesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuImportRouteExcel, Me.mnuImportKML})
        Me.ImportRoutesToolStripMenuItem.Name = "ImportRoutesToolStripMenuItem"
        Me.ImportRoutesToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.ImportRoutesToolStripMenuItem.Text = "Import Routes"
        Me.ImportRoutesToolStripMenuItem.Visible = False
        '
        'mnuImportRouteExcel
        '
        Me.mnuImportRouteExcel.Name = "mnuImportRouteExcel"
        Me.mnuImportRouteExcel.Size = New System.Drawing.Size(168, 22)
        Me.mnuImportRouteExcel.Text = "Import from Excel"
        '
        'mnuImportKML
        '
        Me.mnuImportKML.Name = "mnuImportKML"
        Me.mnuImportKML.Size = New System.Drawing.Size(168, 22)
        Me.mnuImportKML.Text = "Import from KML"
        Me.mnuImportKML.Visible = False
        '
        'mnuClearRoutes
        '
        Me.mnuClearRoutes.Name = "mnuClearRoutes"
        Me.mnuClearRoutes.Size = New System.Drawing.Size(85, 20)
        Me.mnuClearRoutes.Text = "Clear Routes"
        Me.mnuClearRoutes.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.AutoSize = True
        Me.btnRefresh.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRefresh.Location = New System.Drawing.Point(985, 0)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(36, 24)
        Me.btnRefresh.TabIndex = 42
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Click to refresh map")
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'MapRefreshTimer
        '
        Me.MapRefreshTimer.Enabled = True
        Me.MapRefreshTimer.Interval = 300000
        '
        'mainContainer1
        '
        Me.mainContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mainContainer1.Location = New System.Drawing.Point(12, 24)
        Me.mainContainer1.Name = "mainContainer1"
        '
        'mainContainer1.Panel1
        '
        Me.mainContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'mainContainer1.Panel2
        '
        Me.mainContainer1.Panel2.Controls.Add(Me.TabControl1)
        Me.mainContainer1.Size = New System.Drawing.Size(1016, 706)
        Me.mainContainer1.SplitterDistance = 203
        Me.mainContainer1.TabIndex = 43
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Highlight
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnlRouteHolder)
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnlFiller)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlClickMap)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlAddress)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlSites)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlGeofences)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlVehicles)
        Me.SplitContainer2.Size = New System.Drawing.Size(203, 706)
        Me.SplitContainer2.SplitterDistance = 356
        Me.SplitContainer2.TabIndex = 0
        '
        'pnlRouteHolder
        '
        Me.pnlRouteHolder.Controls.Add(Me.pnlCreateRoutes)
        Me.pnlRouteHolder.Controls.Add(Me.pnlShowRoutes)
        Me.pnlRouteHolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRouteHolder.Location = New System.Drawing.Point(0, 27)
        Me.pnlRouteHolder.Name = "pnlRouteHolder"
        Me.pnlRouteHolder.Size = New System.Drawing.Size(203, 329)
        Me.pnlRouteHolder.TabIndex = 1
        '
        'pnlCreateRoutes
        '
        Me.pnlCreateRoutes.Controls.Add(Me.pnlWaypoints)
        Me.pnlCreateRoutes.Controls.Add(Me.pnlTop)
        Me.pnlCreateRoutes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCreateRoutes.Location = New System.Drawing.Point(0, 0)
        Me.pnlCreateRoutes.Name = "pnlCreateRoutes"
        Me.pnlCreateRoutes.Size = New System.Drawing.Size(203, 329)
        Me.pnlCreateRoutes.TabIndex = 32
        '
        'pnlWaypoints
        '
        Me.pnlWaypoints.AutoSize = True
        Me.pnlWaypoints.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlWaypoints.BackColor = System.Drawing.SystemColors.Control
        Me.pnlWaypoints.Controls.Add(Me.pnlWaypointGrid)
        Me.pnlWaypoints.Controls.Add(Me.pnlWaypointLabel)
        Me.pnlWaypoints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWaypoints.Location = New System.Drawing.Point(0, 76)
        Me.pnlWaypoints.MinimumSize = New System.Drawing.Size(208, 100)
        Me.pnlWaypoints.Name = "pnlWaypoints"
        Me.pnlWaypoints.Size = New System.Drawing.Size(208, 253)
        Me.pnlWaypoints.TabIndex = 24
        '
        'pnlWaypointGrid
        '
        Me.pnlWaypointGrid.AutoSize = True
        Me.pnlWaypointGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlWaypointGrid.Controls.Add(Me.dgWayPoints)
        Me.pnlWaypointGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWaypointGrid.Location = New System.Drawing.Point(0, 23)
        Me.pnlWaypointGrid.MinimumSize = New System.Drawing.Size(100, 150)
        Me.pnlWaypointGrid.Name = "pnlWaypointGrid"
        Me.pnlWaypointGrid.Size = New System.Drawing.Size(208, 230)
        Me.pnlWaypointGrid.TabIndex = 24
        '
        'dgWayPoints
        '
        Me.dgWayPoints.AccessibleRole = System.Windows.Forms.AccessibleRole.Caret
        Me.dgWayPoints.AllowDrop = True
        Me.dgWayPoints.AllowUserToAddRows = False
        Me.dgWayPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgWayPoints.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgWayPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgWayPoints.ColumnHeadersVisible = False
        Me.dgWayPoints.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.WayPoints})
        Me.dgWayPoints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgWayPoints.Location = New System.Drawing.Point(0, 0)
        Me.dgWayPoints.Name = "dgWayPoints"
        Me.dgWayPoints.RowHeadersVisible = False
        Me.dgWayPoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgWayPoints.Size = New System.Drawing.Size(208, 230)
        Me.dgWayPoints.TabIndex = 12
        '
        'WayPoints
        '
        Me.WayPoints.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.WayPoints.DefaultCellStyle = DataGridViewCellStyle5
        Me.WayPoints.HeaderText = "WayPoints"
        Me.WayPoints.Name = "WayPoints"
        '
        'pnlWaypointLabel
        '
        Me.pnlWaypointLabel.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.pnlWaypointLabel.Controls.Add(Me.Label6)
        Me.pnlWaypointLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlWaypointLabel.Location = New System.Drawing.Point(0, 0)
        Me.pnlWaypointLabel.Name = "pnlWaypointLabel"
        Me.pnlWaypointLabel.Size = New System.Drawing.Size(208, 23)
        Me.pnlWaypointLabel.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(208, 23)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Way Points"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.SystemColors.Control
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.chkOptimizedRoute)
        Me.pnlTop.Controls.Add(Me.btnClearRoute)
        Me.pnlTop.Controls.Add(Me.btnSaveRoute)
        Me.pnlTop.Controls.Add(Me.btnCreateRoute)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(203, 76)
        Me.pnlTop.TabIndex = 23
        '
        'chkOptimizedRoute
        '
        Me.chkOptimizedRoute.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkOptimizedRoute.AutoSize = True
        Me.chkOptimizedRoute.Location = New System.Drawing.Point(52, 5)
        Me.chkOptimizedRoute.Name = "chkOptimizedRoute"
        Me.chkOptimizedRoute.Size = New System.Drawing.Size(98, 17)
        Me.chkOptimizedRoute.TabIndex = 16
        Me.chkOptimizedRoute.Text = "Optimize Route"
        Me.chkOptimizedRoute.UseVisualStyleBackColor = True
        Me.chkOptimizedRoute.Visible = False
        '
        'btnClearRoute
        '
        Me.btnClearRoute.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnClearRoute.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearRoute.ForeColor = System.Drawing.Color.Black
        Me.btnClearRoute.Location = New System.Drawing.Point(102, 23)
        Me.btnClearRoute.Name = "btnClearRoute"
        Me.btnClearRoute.Size = New System.Drawing.Size(96, 23)
        Me.btnClearRoute.TabIndex = 15
        Me.btnClearRoute.Text = "Cancel Route"
        Me.btnClearRoute.UseVisualStyleBackColor = False
        '
        'btnSaveRoute
        '
        Me.btnSaveRoute.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnSaveRoute.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveRoute.ForeColor = System.Drawing.Color.Black
        Me.btnSaveRoute.Location = New System.Drawing.Point(52, 48)
        Me.btnSaveRoute.Name = "btnSaveRoute"
        Me.btnSaveRoute.Size = New System.Drawing.Size(96, 23)
        Me.btnSaveRoute.TabIndex = 14
        Me.btnSaveRoute.Text = "Save Route"
        Me.btnSaveRoute.UseVisualStyleBackColor = False
        Me.btnSaveRoute.Visible = False
        '
        'btnCreateRoute
        '
        Me.btnCreateRoute.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnCreateRoute.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnCreateRoute.ForeColor = System.Drawing.Color.Black
        Me.btnCreateRoute.Location = New System.Drawing.Point(3, 23)
        Me.btnCreateRoute.Name = "btnCreateRoute"
        Me.btnCreateRoute.Size = New System.Drawing.Size(87, 23)
        Me.btnCreateRoute.TabIndex = 13
        Me.btnCreateRoute.Text = "Create Route"
        Me.btnCreateRoute.UseVisualStyleBackColor = False
        '
        'pnlShowRoutes
        '
        Me.pnlShowRoutes.Controls.Add(Me.pnlRoutesView)
        Me.pnlShowRoutes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlShowRoutes.Location = New System.Drawing.Point(0, 0)
        Me.pnlShowRoutes.Name = "pnlShowRoutes"
        Me.pnlShowRoutes.Size = New System.Drawing.Size(203, 329)
        Me.pnlShowRoutes.TabIndex = 31
        '
        'pnlRoutesView
        '
        Me.pnlRoutesView.BackColor = System.Drawing.SystemColors.Control
        Me.pnlRoutesView.Controls.Add(Me.tvRoutes)
        Me.pnlRoutesView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRoutesView.Location = New System.Drawing.Point(0, 0)
        Me.pnlRoutesView.Name = "pnlRoutesView"
        Me.pnlRoutesView.Size = New System.Drawing.Size(203, 329)
        Me.pnlRoutesView.TabIndex = 2
        '
        'tvRoutes
        '
        Me.tvRoutes.CheckBoxes = True
        Me.tvRoutes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvRoutes.FullRowSelect = True
        Me.tvRoutes.Location = New System.Drawing.Point(0, 0)
        Me.tvRoutes.Name = "tvRoutes"
        Me.tvRoutes.ShowLines = False
        Me.tvRoutes.Size = New System.Drawing.Size(203, 329)
        Me.tvRoutes.TabIndex = 11
        '
        'pnlFiller
        '
        Me.pnlFiller.BackColor = System.Drawing.SystemColors.Control
        Me.pnlFiller.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFiller.Location = New System.Drawing.Point(0, 0)
        Me.pnlFiller.Name = "pnlFiller"
        Me.pnlFiller.Size = New System.Drawing.Size(203, 27)
        Me.pnlFiller.TabIndex = 0
        '
        'pnlClickMap
        '
        Me.pnlClickMap.Controls.Add(Me.btnWayPointFromMap)
        Me.pnlClickMap.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlClickMap.Location = New System.Drawing.Point(0, 152)
        Me.pnlClickMap.Name = "pnlClickMap"
        Me.pnlClickMap.Size = New System.Drawing.Size(203, 24)
        Me.pnlClickMap.TabIndex = 16
        '
        'btnWayPointFromMap
        '
        Me.btnWayPointFromMap.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnWayPointFromMap.Location = New System.Drawing.Point(0, 0)
        Me.btnWayPointFromMap.Name = "btnWayPointFromMap"
        Me.btnWayPointFromMap.Size = New System.Drawing.Size(203, 23)
        Me.btnWayPointFromMap.TabIndex = 12
        Me.btnWayPointFromMap.Text = "Select Way Point From Map"
        Me.btnWayPointFromMap.UseVisualStyleBackColor = True
        '
        'pnlAddress
        '
        Me.pnlAddress.Controls.Add(Me.btnFindAddress)
        Me.pnlAddress.Controls.Add(Me.txtAddress)
        Me.pnlAddress.Controls.Add(Me.Label5)
        Me.pnlAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAddress.Location = New System.Drawing.Point(0, 69)
        Me.pnlAddress.Name = "pnlAddress"
        Me.pnlAddress.Size = New System.Drawing.Size(203, 83)
        Me.pnlAddress.TabIndex = 15
        '
        'btnFindAddress
        '
        Me.btnFindAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnFindAddress.Enabled = False
        Me.btnFindAddress.Location = New System.Drawing.Point(0, 58)
        Me.btnFindAddress.Name = "btnFindAddress"
        Me.btnFindAddress.Size = New System.Drawing.Size(203, 23)
        Me.btnFindAddress.TabIndex = 11
        Me.btnFindAddress.Text = "Click to find address"
        Me.btnFindAddress.UseVisualStyleBackColor = True
        '
        'txtAddress
        '
        Me.txtAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtAddress.Location = New System.Drawing.Point(0, 38)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(203, 20)
        Me.txtAddress.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(203, 38)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Enter Address or Latitude, Longitude"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlSites
        '
        Me.pnlSites.AutoSize = True
        Me.pnlSites.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlSites.Controls.Add(Me.pnlSitesRight)
        Me.pnlSites.Controls.Add(Me.pnlSitesLeft)
        Me.pnlSites.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSites.Location = New System.Drawing.Point(0, 46)
        Me.pnlSites.MinimumSize = New System.Drawing.Size(90, 23)
        Me.pnlSites.Name = "pnlSites"
        Me.pnlSites.Size = New System.Drawing.Size(203, 23)
        Me.pnlSites.TabIndex = 14
        '
        'pnlSitesRight
        '
        Me.pnlSitesRight.AutoSize = True
        Me.pnlSitesRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlSitesRight.Controls.Add(Me.cmbSites)
        Me.pnlSitesRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSitesRight.Location = New System.Drawing.Point(90, 0)
        Me.pnlSitesRight.MinimumSize = New System.Drawing.Size(100, 23)
        Me.pnlSitesRight.Name = "pnlSitesRight"
        Me.pnlSitesRight.Size = New System.Drawing.Size(113, 23)
        Me.pnlSitesRight.TabIndex = 1
        '
        'cmbSites
        '
        Me.cmbSites.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSites.FormattingEnabled = True
        Me.cmbSites.Location = New System.Drawing.Point(0, 0)
        Me.cmbSites.Name = "cmbSites"
        Me.cmbSites.Size = New System.Drawing.Size(113, 21)
        Me.cmbSites.Sorted = True
        Me.cmbSites.TabIndex = 19
        '
        'pnlSitesLeft
        '
        Me.pnlSitesLeft.Controls.Add(Me.Label4)
        Me.pnlSitesLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSitesLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlSitesLeft.MinimumSize = New System.Drawing.Size(90, 23)
        Me.pnlSitesLeft.Name = "pnlSitesLeft"
        Me.pnlSitesLeft.Size = New System.Drawing.Size(90, 23)
        Me.pnlSitesLeft.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 23)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Sites"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlGeofences
        '
        Me.pnlGeofences.AutoSize = True
        Me.pnlGeofences.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlGeofences.Controls.Add(Me.pnlGeoRight)
        Me.pnlGeofences.Controls.Add(Me.pnlGeoLeft)
        Me.pnlGeofences.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlGeofences.Location = New System.Drawing.Point(0, 23)
        Me.pnlGeofences.MinimumSize = New System.Drawing.Size(90, 23)
        Me.pnlGeofences.Name = "pnlGeofences"
        Me.pnlGeofences.Size = New System.Drawing.Size(203, 23)
        Me.pnlGeofences.TabIndex = 13
        '
        'pnlGeoRight
        '
        Me.pnlGeoRight.AutoSize = True
        Me.pnlGeoRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlGeoRight.Controls.Add(Me.cmbGeofences)
        Me.pnlGeoRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGeoRight.Location = New System.Drawing.Point(90, 0)
        Me.pnlGeoRight.Name = "pnlGeoRight"
        Me.pnlGeoRight.Size = New System.Drawing.Size(113, 23)
        Me.pnlGeoRight.TabIndex = 24
        '
        'cmbGeofences
        '
        Me.cmbGeofences.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbGeofences.FormattingEnabled = True
        Me.cmbGeofences.Location = New System.Drawing.Point(0, 0)
        Me.cmbGeofences.Name = "cmbGeofences"
        Me.cmbGeofences.Size = New System.Drawing.Size(113, 21)
        Me.cmbGeofences.Sorted = True
        Me.cmbGeofences.TabIndex = 18
        '
        'pnlGeoLeft
        '
        Me.pnlGeoLeft.AutoSize = True
        Me.pnlGeoLeft.Controls.Add(Me.Label3)
        Me.pnlGeoLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGeoLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlGeoLeft.MinimumSize = New System.Drawing.Size(90, 23)
        Me.pnlGeoLeft.Name = "pnlGeoLeft"
        Me.pnlGeoLeft.Size = New System.Drawing.Size(90, 23)
        Me.pnlGeoLeft.TabIndex = 23
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 23)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Geofences"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlVehicles
        '
        Me.pnlVehicles.Controls.Add(Me.cmbVehicles)
        Me.pnlVehicles.Controls.Add(Me.pnlVehRight)
        Me.pnlVehicles.Controls.Add(Me.pnlVehLeft)
        Me.pnlVehicles.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlVehicles.Location = New System.Drawing.Point(0, 0)
        Me.pnlVehicles.Name = "pnlVehicles"
        Me.pnlVehicles.Size = New System.Drawing.Size(203, 23)
        Me.pnlVehicles.TabIndex = 12
        '
        'cmbVehicles
        '
        Me.cmbVehicles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbVehicles.FormattingEnabled = True
        Me.cmbVehicles.Location = New System.Drawing.Point(90, 0)
        Me.cmbVehicles.Name = "cmbVehicles"
        Me.cmbVehicles.Size = New System.Drawing.Size(113, 21)
        Me.cmbVehicles.Sorted = True
        Me.cmbVehicles.TabIndex = 18
        '
        'pnlVehRight
        '
        Me.pnlVehRight.AutoSize = True
        Me.pnlVehRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlVehRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlVehRight.Location = New System.Drawing.Point(90, 0)
        Me.pnlVehRight.MinimumSize = New System.Drawing.Size(100, 23)
        Me.pnlVehRight.Name = "pnlVehRight"
        Me.pnlVehRight.Size = New System.Drawing.Size(113, 23)
        Me.pnlVehRight.TabIndex = 1
        '
        'pnlVehLeft
        '
        Me.pnlVehLeft.AutoSize = True
        Me.pnlVehLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlVehLeft.Controls.Add(Me.Label2)
        Me.pnlVehLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlVehLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlVehLeft.MinimumSize = New System.Drawing.Size(90, 23)
        Me.pnlVehLeft.Name = "pnlVehLeft"
        Me.pnlVehLeft.Size = New System.Drawing.Size(90, 23)
        Me.pnlVehLeft.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 23)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Vehicles"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.mapTabPage)
        Me.TabControl1.Controls.Add(Me.turnTabPage)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(809, 706)
        Me.TabControl1.TabIndex = 9
        '
        'mapTabPage
        '
        Me.mapTabPage.Controls.Add(Me.GM_Browser)
        Me.mapTabPage.Controls.Add(Me.dgLocationResults)
        Me.mapTabPage.Location = New System.Drawing.Point(4, 22)
        Me.mapTabPage.Name = "mapTabPage"
        Me.mapTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.mapTabPage.Size = New System.Drawing.Size(801, 680)
        Me.mapTabPage.TabIndex = 0
        Me.mapTabPage.Text = "Map"
        Me.mapTabPage.UseVisualStyleBackColor = True
        '
        'GM_Browser
        '
        Me.GM_Browser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GM_Browser.Location = New System.Drawing.Point(3, 3)
        Me.GM_Browser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.GM_Browser.Name = "GM_Browser"
        Me.GM_Browser.Size = New System.Drawing.Size(795, 674)
        Me.GM_Browser.TabIndex = 10
        '
        'dgLocationResults
        '
        Me.dgLocationResults.AllowUserToAddRows = False
        Me.dgLocationResults.AllowUserToDeleteRows = False
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgLocationResults.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgLocationResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgLocationResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colResults})
        Me.dgLocationResults.Location = New System.Drawing.Point(0, 6)
        Me.dgLocationResults.MultiSelect = False
        Me.dgLocationResults.Name = "dgLocationResults"
        Me.dgLocationResults.ReadOnly = True
        Me.dgLocationResults.RowHeadersVisible = False
        Me.dgLocationResults.Size = New System.Drawing.Size(454, 405)
        Me.dgLocationResults.TabIndex = 9
        Me.dgLocationResults.Visible = False
        '
        'colResults
        '
        Me.colResults.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colResults.FillWeight = 450.0!
        Me.colResults.HeaderText = "Please select the required location from the list below"
        Me.colResults.Name = "colResults"
        Me.colResults.ReadOnly = True
        Me.colResults.Width = 450
        '
        'turnTabPage
        '
        Me.turnTabPage.Controls.Add(Me.pnlTurnsGrid)
        Me.turnTabPage.Controls.Add(Me.pnlTurnsHeader)
        Me.turnTabPage.Location = New System.Drawing.Point(4, 22)
        Me.turnTabPage.Name = "turnTabPage"
        Me.turnTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.turnTabPage.Size = New System.Drawing.Size(801, 680)
        Me.turnTabPage.TabIndex = 1
        Me.turnTabPage.Text = "Turns"
        Me.turnTabPage.UseVisualStyleBackColor = True
        '
        'pnlTurnsGrid
        '
        Me.pnlTurnsGrid.Controls.Add(Me.dgTurns)
        Me.pnlTurnsGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTurnsGrid.Location = New System.Drawing.Point(3, 34)
        Me.pnlTurnsGrid.Name = "pnlTurnsGrid"
        Me.pnlTurnsGrid.Size = New System.Drawing.Size(795, 643)
        Me.pnlTurnsGrid.TabIndex = 2
        '
        'dgTurns
        '
        Me.dgTurns.AllowUserToAddRows = False
        Me.dgTurns.AllowUserToDeleteRows = False
        Me.dgTurns.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgTurns.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgTurns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTurns.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Turn, Me.TurnLocation, Me.Distance, Me.Time})
        Me.dgTurns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgTurns.Location = New System.Drawing.Point(0, 0)
        Me.dgTurns.MaximumSize = New System.Drawing.Size(700, 0)
        Me.dgTurns.Name = "dgTurns"
        Me.dgTurns.ReadOnly = True
        Me.dgTurns.Size = New System.Drawing.Size(700, 643)
        Me.dgTurns.TabIndex = 1
        '
        'Turn
        '
        Me.Turn.HeaderText = "Turn"
        Me.Turn.Name = "Turn"
        Me.Turn.ReadOnly = True
        '
        'TurnLocation
        '
        Me.TurnLocation.HeaderText = "Location"
        Me.TurnLocation.Name = "TurnLocation"
        Me.TurnLocation.ReadOnly = True
        '
        'Distance
        '
        Me.Distance.HeaderText = "Distance"
        Me.Distance.Name = "Distance"
        Me.Distance.ReadOnly = True
        '
        'Time
        '
        Me.Time.HeaderText = "Time"
        Me.Time.Name = "Time"
        Me.Time.ReadOnly = True
        '
        'pnlTurnsHeader
        '
        Me.pnlTurnsHeader.Controls.Add(Me.btnEmail)
        Me.pnlTurnsHeader.Controls.Add(Me.btnExportToExcel)
        Me.pnlTurnsHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTurnsHeader.Location = New System.Drawing.Point(3, 3)
        Me.pnlTurnsHeader.Name = "pnlTurnsHeader"
        Me.pnlTurnsHeader.Size = New System.Drawing.Size(795, 31)
        Me.pnlTurnsHeader.TabIndex = 1
        '
        'btnEmail
        '
        Me.btnEmail.Location = New System.Drawing.Point(107, 5)
        Me.btnEmail.Name = "btnEmail"
        Me.btnEmail.Size = New System.Drawing.Size(87, 23)
        Me.btnEmail.TabIndex = 15
        Me.btnEmail.Text = "Email"
        Me.btnEmail.UseVisualStyleBackColor = True
        Me.btnEmail.Visible = False
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(3, 5)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(87, 23)
        Me.btnExportToExcel.TabIndex = 14
        Me.btnExportToExcel.Text = "Export to Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'mnuDeleteRoute
        '
        Me.mnuDeleteRoute.Name = "mnuDeleteRoute"
        Me.mnuDeleteRoute.Size = New System.Drawing.Size(146, 22)
        Me.mnuDeleteRoute.Text = "Delete Route"
        '
        'contextMnuTvRoutesDelMod
        '
        Me.contextMnuTvRoutesDelMod.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuModifyRoute, Me.mnuDeleteRoute, Me.mnuShowTurnsUnallocated})
        Me.contextMnuTvRoutesDelMod.Name = "contextMnuTvRoutes"
        Me.contextMnuTvRoutesDelMod.Size = New System.Drawing.Size(147, 70)
        '
        'mnuModifyRoute
        '
        Me.mnuModifyRoute.Name = "mnuModifyRoute"
        Me.mnuModifyRoute.Size = New System.Drawing.Size(146, 22)
        Me.mnuModifyRoute.Text = "Modify Route"
        '
        'mnuShowTurnsUnallocated
        '
        Me.mnuShowTurnsUnallocated.Name = "mnuShowTurnsUnallocated"
        Me.mnuShowTurnsUnallocated.Size = New System.Drawing.Size(146, 22)
        Me.mnuShowTurnsUnallocated.Text = "Show Turns"
        '
        'contextMnuTvRoutesMod
        '
        Me.contextMnuTvRoutesMod.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuModifyAllocatedRoute, Me.mnuShowTurnsAllocated})
        Me.contextMnuTvRoutesMod.Name = "contextMnuTvRoutesMod"
        Me.contextMnuTvRoutesMod.Size = New System.Drawing.Size(147, 48)
        '
        'mnuModifyAllocatedRoute
        '
        Me.mnuModifyAllocatedRoute.Name = "mnuModifyAllocatedRoute"
        Me.mnuModifyAllocatedRoute.Size = New System.Drawing.Size(146, 22)
        Me.mnuModifyAllocatedRoute.Text = "Modify Route"
        '
        'mnuShowTurnsAllocated
        '
        Me.mnuShowTurnsAllocated.Name = "mnuShowTurnsAllocated"
        Me.mnuShowTurnsAllocated.Size = New System.Drawing.Size(146, 22)
        Me.mnuShowTurnsAllocated.Text = "Show Turns"
        '
        'frmMSTGoogleMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1040, 730)
        Me.Controls.Add(Me.mainContainer1)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMSTGoogleMap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmMSTGoogleMap"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.mainContainer1.Panel1.ResumeLayout(False)
        Me.mainContainer1.Panel2.ResumeLayout(False)
        CType(Me.mainContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mainContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.pnlRouteHolder.ResumeLayout(False)
        Me.pnlCreateRoutes.ResumeLayout(False)
        Me.pnlCreateRoutes.PerformLayout()
        Me.pnlWaypoints.ResumeLayout(False)
        Me.pnlWaypoints.PerformLayout()
        Me.pnlWaypointGrid.ResumeLayout(False)
        CType(Me.dgWayPoints, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlWaypointLabel.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlShowRoutes.ResumeLayout(False)
        Me.pnlRoutesView.ResumeLayout(False)
        Me.pnlClickMap.ResumeLayout(False)
        Me.pnlAddress.ResumeLayout(False)
        Me.pnlAddress.PerformLayout()
        Me.pnlSites.ResumeLayout(False)
        Me.pnlSites.PerformLayout()
        Me.pnlSitesRight.ResumeLayout(False)
        Me.pnlSitesLeft.ResumeLayout(False)
        Me.pnlGeofences.ResumeLayout(False)
        Me.pnlGeofences.PerformLayout()
        Me.pnlGeoRight.ResumeLayout(False)
        Me.pnlGeoLeft.ResumeLayout(False)
        Me.pnlVehicles.ResumeLayout(False)
        Me.pnlVehicles.PerformLayout()
        Me.pnlVehLeft.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.mapTabPage.ResumeLayout(False)
        CType(Me.dgLocationResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.turnTabPage.ResumeLayout(False)
        Me.pnlTurnsGrid.ResumeLayout(False)
        CType(Me.dgTurns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTurnsHeader.ResumeLayout(False)
        Me.contextMnuTvRoutesDelMod.ResumeLayout(False)
        Me.contextMnuTvRoutesMod.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBufferZones As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateBufferZoneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStartBufferZone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCompleteBufferZone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCancelBufferZone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteBufferZoneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPolygonGeofence As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCreateGeofenceM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStartGeofenceM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCompleteGeofenceM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCancelGeofenceM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDeleteGeofenceM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDeselectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClearMap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDraw As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDrawLine As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDrawPolygon As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents mnuCreateSite As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDisplayGeofence As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents LablesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VehicleLabelsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDisplayRegNo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDisplayDesc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDisplayBoth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDistanceCalculator As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStartDistCalc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClearDistCalc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEndDistCalc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MapRefreshTimer As System.Windows.Forms.Timer
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents mnuRoutes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShowRoutes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCreateRoute As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mainContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlClickMap As Windows.Forms.Panel
    Friend WithEvents btnWayPointFromMap As Windows.Forms.Button
    Friend WithEvents pnlAddress As Windows.Forms.Panel
    Friend WithEvents btnFindAddress As Windows.Forms.Button
    Friend WithEvents txtAddress As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents pnlSites As Windows.Forms.Panel
    Friend WithEvents pnlGeofences As Windows.Forms.Panel
    Friend WithEvents pnlVehicles As Windows.Forms.Panel
    Friend WithEvents cmbVehicles As Windows.Forms.ComboBox
    Friend WithEvents pnlVehRight As Windows.Forms.Panel
    Friend WithEvents pnlVehLeft As Windows.Forms.Panel
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents pnlGeoRight As Windows.Forms.Panel
    Friend WithEvents cmbGeofences As Windows.Forms.ComboBox
    Friend WithEvents pnlGeoLeft As Windows.Forms.Panel
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents pnlSitesRight As Windows.Forms.Panel
    Friend WithEvents cmbSites As Windows.Forms.ComboBox
    Friend WithEvents pnlSitesLeft As Windows.Forms.Panel
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents TabControl1 As Windows.Forms.TabControl
    Friend WithEvents mapTabPage As Windows.Forms.TabPage
    Friend WithEvents dgLocationResults As Windows.Forms.DataGridView
    Friend WithEvents colResults As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents turnTabPage As Windows.Forms.TabPage
    Friend WithEvents pnlTurnsGrid As Windows.Forms.Panel
    Friend WithEvents dgTurns As Windows.Forms.DataGridView
    Friend WithEvents Turn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TurnLocation As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Distance As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Time As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnlTurnsHeader As Windows.Forms.Panel
    Friend WithEvents btnEmail As Windows.Forms.Button
    Friend WithEvents btnExportToExcel As Windows.Forms.Button
    Friend WithEvents PrintDialog1 As Windows.Forms.PrintDialog
    Friend WithEvents mnuClearRoutes As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDeleteRoute As Windows.Forms.ToolStripMenuItem
    Friend WithEvents contextMnuTvRoutesDelMod As Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuModifyRoute As Windows.Forms.ToolStripMenuItem
    Friend WithEvents contextMnuTvRoutesMod As Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuModifyAllocatedRoute As Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlRouteHolder As Windows.Forms.Panel
    Friend WithEvents pnlCreateRoutes As Windows.Forms.Panel
    Friend WithEvents pnlWaypoints As Windows.Forms.Panel
    Friend WithEvents pnlWaypointGrid As Windows.Forms.Panel
    Friend WithEvents dgWayPoints As Windows.Forms.DataGridView
    Friend WithEvents WayPoints As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnlWaypointLabel As Windows.Forms.Panel
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents pnlTop As Windows.Forms.Panel
    Friend WithEvents btnClearRoute As Windows.Forms.Button
    Friend WithEvents btnSaveRoute As Windows.Forms.Button
    Friend WithEvents btnCreateRoute As Windows.Forms.Button
    Friend WithEvents pnlShowRoutes As Windows.Forms.Panel
    Friend WithEvents pnlRoutesView As Windows.Forms.Panel
    Friend WithEvents tvRoutes As Windows.Forms.TreeView
    Friend WithEvents pnlFiller As Windows.Forms.Panel
    Friend WithEvents ImportRoutesToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImportRouteExcel As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImportKML As Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkOptimizedRoute As Windows.Forms.CheckBox
    Friend WithEvents mnuShowTurnsUnallocated As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShowTurnsAllocated As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPrint As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GM_Browser As Windows.Forms.WebBrowser
    Friend WithEvents bw1 As ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker1 As ComponentModel.BackgroundWorker
End Class
