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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTGoogleHistoryMap))
        Me.tvRoutes = New System.Windows.Forms.TreeView()
        Me.cmbSites = New System.Windows.Forms.ComboBox()
        Me.pnlSitesRight = New System.Windows.Forms.Panel()
        Me.pnlSites = New System.Windows.Forms.Panel()
        Me.pnlSitesLeft = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnFindAddress = New System.Windows.Forms.Button()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlAddress = New System.Windows.Forms.Panel()
        Me.btnWayPointFromMap = New System.Windows.Forms.Button()
        Me.pnlClickMap = New System.Windows.Forms.Panel()
        Me.btnClearRoute = New System.Windows.Forms.Button()
        Me.btnSaveRoute = New System.Windows.Forms.Button()
        Me.btnCreateRoute = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlWaypointLabel = New System.Windows.Forms.Panel()
        Me.WayPoints = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgWayPoints = New System.Windows.Forms.DataGridView()
        Me.pnlWaypointGrid = New System.Windows.Forms.Panel()
        Me.pnlWaypoints = New System.Windows.Forms.Panel()
        Me.pnlCreateRoutes = New System.Windows.Forms.Panel()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.btnEmail = New System.Windows.Forms.Button()
        Me.pnlTurnsHeader = New System.Windows.Forms.Panel()
        Me.Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Distance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TurnLocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Turn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgTurns = New System.Windows.Forms.DataGridView()
        Me.pnlTurnsGrid = New System.Windows.Forms.Panel()
        Me.turnTabPage = New System.Windows.Forms.TabPage()
        Me.colResults = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mapTabPage = New System.Windows.Forms.TabPage()
        Me.pnlWebBrowser = New System.Windows.Forms.Panel()
        Me.dgLocationResults = New System.Windows.Forms.DataGridView()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbVehicles = New System.Windows.Forms.ComboBox()
        Me.pnlVehRight = New System.Windows.Forms.Panel()
        Me.pnlVehLeft = New System.Windows.Forms.Panel()
        Me.pnlVehicles = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlGeoLeft = New System.Windows.Forms.Panel()
        Me.cmbGeofences = New System.Windows.Forms.ComboBox()
        Me.pnlGeofences = New System.Windows.Forms.Panel()
        Me.pnlGeoRight = New System.Windows.Forms.Panel()
        Me.pnlRoutesView = New System.Windows.Forms.Panel()
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
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
        Me.mnuRoutes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCreateRoute = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShowRoutes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImportKML = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.pnlShowRoutes = New System.Windows.Forms.Panel()
        Me.mainContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MapRefreshTimer = New System.Windows.Forms.Timer(Me.components)
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlSitesRight.SuspendLayout
        Me.pnlSites.SuspendLayout
        Me.pnlSitesLeft.SuspendLayout
        Me.pnlAddress.SuspendLayout
        Me.pnlClickMap.SuspendLayout
        Me.pnlTop.SuspendLayout
        Me.pnlWaypointLabel.SuspendLayout
        CType(Me.dgWayPoints,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlWaypointGrid.SuspendLayout
        Me.pnlWaypoints.SuspendLayout
        Me.pnlCreateRoutes.SuspendLayout
        Me.pnlTurnsHeader.SuspendLayout
        CType(Me.dgTurns,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTurnsGrid.SuspendLayout
        Me.turnTabPage.SuspendLayout
        Me.mapTabPage.SuspendLayout
        CType(Me.dgLocationResults,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TabControl1.SuspendLayout
        Me.pnlVehLeft.SuspendLayout
        Me.pnlVehicles.SuspendLayout
        Me.pnlGeoLeft.SuspendLayout
        Me.pnlGeofences.SuspendLayout
        Me.pnlGeoRight.SuspendLayout
        Me.pnlRoutesView.SuspendLayout
        Me.MenuStrip1.SuspendLayout
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer2.Panel1.SuspendLayout
        Me.SplitContainer2.Panel2.SuspendLayout
        Me.SplitContainer2.SuspendLayout
        Me.pnlShowRoutes.SuspendLayout
        CType(Me.mainContainer1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.mainContainer1.Panel1.SuspendLayout
        Me.mainContainer1.Panel2.SuspendLayout
        Me.mainContainer1.SuspendLayout
        Me.SuspendLayout
        '
        'tvRoutes
        '
        Me.tvRoutes.CheckBoxes = true
        Me.tvRoutes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvRoutes.FullRowSelect = true
        Me.tvRoutes.Location = New System.Drawing.Point(0, 0)
        Me.tvRoutes.Name = "tvRoutes"
        Me.tvRoutes.ShowLines = false
        Me.tvRoutes.Size = New System.Drawing.Size(188, 356)
        Me.tvRoutes.TabIndex = 11
        '
        'cmbSites
        '
        Me.cmbSites.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSites.FormattingEnabled = true
        Me.cmbSites.Location = New System.Drawing.Point(0, 0)
        Me.cmbSites.Name = "cmbSites"
        Me.cmbSites.Size = New System.Drawing.Size(100, 21)
        Me.cmbSites.TabIndex = 19
        '
        'pnlSitesRight
        '
        Me.pnlSitesRight.AutoSize = true
        Me.pnlSitesRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlSitesRight.Controls.Add(Me.cmbSites)
        Me.pnlSitesRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSitesRight.Location = New System.Drawing.Point(90, 0)
        Me.pnlSitesRight.MinimumSize = New System.Drawing.Size(100, 23)
        Me.pnlSitesRight.Name = "pnlSitesRight"
        Me.pnlSitesRight.Size = New System.Drawing.Size(100, 23)
        Me.pnlSitesRight.TabIndex = 1
        '
        'pnlSites
        '
        Me.pnlSites.AutoSize = true
        Me.pnlSites.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlSites.Controls.Add(Me.pnlSitesRight)
        Me.pnlSites.Controls.Add(Me.pnlSitesLeft)
        Me.pnlSites.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSites.Location = New System.Drawing.Point(0, 46)
        Me.pnlSites.MinimumSize = New System.Drawing.Size(90, 23)
        Me.pnlSites.Name = "pnlSites"
        Me.pnlSites.Size = New System.Drawing.Size(188, 23)
        Me.pnlSites.TabIndex = 14
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
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 23)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Sites"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnFindAddress
        '
        Me.btnFindAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnFindAddress.Location = New System.Drawing.Point(0, 58)
        Me.btnFindAddress.Name = "btnFindAddress"
        Me.btnFindAddress.Size = New System.Drawing.Size(188, 23)
        Me.btnFindAddress.TabIndex = 11
        Me.btnFindAddress.UseVisualStyleBackColor = true
        '
        'txtAddress
        '
        Me.txtAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtAddress.Location = New System.Drawing.Point(0, 38)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(188, 20)
        Me.txtAddress.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(188, 38)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Enter Address or Latitude, Longitude"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlAddress
        '
        Me.pnlAddress.Controls.Add(Me.btnFindAddress)
        Me.pnlAddress.Controls.Add(Me.txtAddress)
        Me.pnlAddress.Controls.Add(Me.Label5)
        Me.pnlAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAddress.Location = New System.Drawing.Point(0, 69)
        Me.pnlAddress.Name = "pnlAddress"
        Me.pnlAddress.Size = New System.Drawing.Size(188, 83)
        Me.pnlAddress.TabIndex = 15
        '
        'btnWayPointFromMap
        '
        Me.btnWayPointFromMap.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnWayPointFromMap.Location = New System.Drawing.Point(0, 0)
        Me.btnWayPointFromMap.Name = "btnWayPointFromMap"
        Me.btnWayPointFromMap.Size = New System.Drawing.Size(188, 23)
        Me.btnWayPointFromMap.TabIndex = 12
        Me.btnWayPointFromMap.Text = "Select Way Point From Map"
        Me.btnWayPointFromMap.UseVisualStyleBackColor = true
        '
        'pnlClickMap
        '
        Me.pnlClickMap.Controls.Add(Me.btnWayPointFromMap)
        Me.pnlClickMap.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlClickMap.Location = New System.Drawing.Point(0, 152)
        Me.pnlClickMap.Name = "pnlClickMap"
        Me.pnlClickMap.Size = New System.Drawing.Size(188, 24)
        Me.pnlClickMap.TabIndex = 16
        '
        'btnClearRoute
        '
        Me.btnClearRoute.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnClearRoute.BackColor = System.Drawing.Color.Orange
        Me.btnClearRoute.ForeColor = System.Drawing.Color.White
        Me.btnClearRoute.Location = New System.Drawing.Point(99, -1)
        Me.btnClearRoute.Name = "btnClearRoute"
        Me.btnClearRoute.Size = New System.Drawing.Size(96, 23)
        Me.btnClearRoute.TabIndex = 15
        Me.btnClearRoute.Text = "Clear Route"
        Me.btnClearRoute.UseVisualStyleBackColor = false
        '
        'btnSaveRoute
        '
        Me.btnSaveRoute.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnSaveRoute.BackColor = System.Drawing.Color.OrangeRed
        Me.btnSaveRoute.ForeColor = System.Drawing.Color.White
        Me.btnSaveRoute.Location = New System.Drawing.Point(43, 24)
        Me.btnSaveRoute.Name = "btnSaveRoute"
        Me.btnSaveRoute.Size = New System.Drawing.Size(96, 23)
        Me.btnSaveRoute.TabIndex = 14
        Me.btnSaveRoute.Text = "Save Route"
        Me.btnSaveRoute.UseVisualStyleBackColor = false
        Me.btnSaveRoute.Visible = false
        '
        'btnCreateRoute
        '
        Me.btnCreateRoute.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnCreateRoute.BackColor = System.Drawing.Color.ForestGreen
        Me.btnCreateRoute.ForeColor = System.Drawing.Color.White
        Me.btnCreateRoute.Location = New System.Drawing.Point(-11, -1)
        Me.btnCreateRoute.Name = "btnCreateRoute"
        Me.btnCreateRoute.Size = New System.Drawing.Size(87, 23)
        Me.btnCreateRoute.TabIndex = 13
        Me.btnCreateRoute.Text = "Create Route"
        Me.btnCreateRoute.UseVisualStyleBackColor = false
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.SystemColors.Control
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.btnClearRoute)
        Me.pnlTop.Controls.Add(Me.btnSaveRoute)
        Me.pnlTop.Controls.Add(Me.btnCreateRoute)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(188, 49)
        Me.pnlTop.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(208, 23)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Way Points"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        'WayPoints
        '
        Me.WayPoints.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.WayPoints.DefaultCellStyle = DataGridViewCellStyle1
        Me.WayPoints.HeaderText = "WayPoints"
        Me.WayPoints.Name = "WayPoints"
        '
        'dgWayPoints
        '
        Me.dgWayPoints.AccessibleRole = System.Windows.Forms.AccessibleRole.Caret
        Me.dgWayPoints.AllowDrop = true
        Me.dgWayPoints.AllowUserToAddRows = false
        Me.dgWayPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgWayPoints.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgWayPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgWayPoints.ColumnHeadersVisible = false
        Me.dgWayPoints.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.WayPoints})
        Me.dgWayPoints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgWayPoints.Location = New System.Drawing.Point(0, 0)
        Me.dgWayPoints.Name = "dgWayPoints"
        Me.dgWayPoints.RowHeadersVisible = false
        Me.dgWayPoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgWayPoints.Size = New System.Drawing.Size(208, 284)
        Me.dgWayPoints.TabIndex = 12
        '
        'pnlWaypointGrid
        '
        Me.pnlWaypointGrid.AutoSize = true
        Me.pnlWaypointGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlWaypointGrid.Controls.Add(Me.dgWayPoints)
        Me.pnlWaypointGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWaypointGrid.Location = New System.Drawing.Point(0, 23)
        Me.pnlWaypointGrid.MinimumSize = New System.Drawing.Size(100, 150)
        Me.pnlWaypointGrid.Name = "pnlWaypointGrid"
        Me.pnlWaypointGrid.Size = New System.Drawing.Size(208, 284)
        Me.pnlWaypointGrid.TabIndex = 24
        '
        'pnlWaypoints
        '
        Me.pnlWaypoints.AutoSize = true
        Me.pnlWaypoints.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlWaypoints.BackColor = System.Drawing.SystemColors.Control
        Me.pnlWaypoints.Controls.Add(Me.pnlWaypointGrid)
        Me.pnlWaypoints.Controls.Add(Me.pnlWaypointLabel)
        Me.pnlWaypoints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWaypoints.Location = New System.Drawing.Point(0, 49)
        Me.pnlWaypoints.MinimumSize = New System.Drawing.Size(208, 100)
        Me.pnlWaypoints.Name = "pnlWaypoints"
        Me.pnlWaypoints.Size = New System.Drawing.Size(208, 307)
        Me.pnlWaypoints.TabIndex = 24
        '
        'pnlCreateRoutes
        '
        Me.pnlCreateRoutes.Controls.Add(Me.pnlWaypoints)
        Me.pnlCreateRoutes.Controls.Add(Me.pnlTop)
        Me.pnlCreateRoutes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCreateRoutes.Location = New System.Drawing.Point(0, 0)
        Me.pnlCreateRoutes.Name = "pnlCreateRoutes"
        Me.pnlCreateRoutes.Size = New System.Drawing.Size(188, 356)
        Me.pnlCreateRoutes.TabIndex = 28
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(3, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(87, 23)
        Me.btnPrint.TabIndex = 14
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = true
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = true
        '
        'btnEmail
        '
        Me.btnEmail.Location = New System.Drawing.Point(107, 5)
        Me.btnEmail.Name = "btnEmail"
        Me.btnEmail.Size = New System.Drawing.Size(87, 23)
        Me.btnEmail.TabIndex = 15
        Me.btnEmail.Text = "Email"
        Me.btnEmail.UseVisualStyleBackColor = true
        '
        'pnlTurnsHeader
        '
        Me.pnlTurnsHeader.Controls.Add(Me.btnEmail)
        Me.pnlTurnsHeader.Controls.Add(Me.btnPrint)
        Me.pnlTurnsHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTurnsHeader.Location = New System.Drawing.Point(3, 3)
        Me.pnlTurnsHeader.Name = "pnlTurnsHeader"
        Me.pnlTurnsHeader.Size = New System.Drawing.Size(750, 31)
        Me.pnlTurnsHeader.TabIndex = 1
        '
        'Time
        '
        Me.Time.HeaderText = "Time"
        Me.Time.Name = "Time"
        '
        'Distance
        '
        Me.Distance.HeaderText = "Distance"
        Me.Distance.Name = "Distance"
        '
        'TurnLocation
        '
        Me.TurnLocation.HeaderText = "Location"
        Me.TurnLocation.Name = "TurnLocation"
        '
        'Turn
        '
        Me.Turn.HeaderText = "Turn"
        Me.Turn.Name = "Turn"
        '
        'dgTurns
        '
        Me.dgTurns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTurns.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Turn, Me.TurnLocation, Me.Distance, Me.Time})
        Me.dgTurns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgTurns.Location = New System.Drawing.Point(0, 0)
        Me.dgTurns.MaximumSize = New System.Drawing.Size(700, 0)
        Me.dgTurns.Name = "dgTurns"
        Me.dgTurns.Size = New System.Drawing.Size(700, 643)
        Me.dgTurns.TabIndex = 1
        '
        'pnlTurnsGrid
        '
        Me.pnlTurnsGrid.Controls.Add(Me.dgTurns)
        Me.pnlTurnsGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTurnsGrid.Location = New System.Drawing.Point(3, 34)
        Me.pnlTurnsGrid.Name = "pnlTurnsGrid"
        Me.pnlTurnsGrid.Size = New System.Drawing.Size(750, 643)
        Me.pnlTurnsGrid.TabIndex = 2
        '
        'turnTabPage
        '
        Me.turnTabPage.Controls.Add(Me.pnlTurnsGrid)
        Me.turnTabPage.Controls.Add(Me.pnlTurnsHeader)
        Me.turnTabPage.Location = New System.Drawing.Point(4, 22)
        Me.turnTabPage.Name = "turnTabPage"
        Me.turnTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.turnTabPage.Size = New System.Drawing.Size(756, 680)
        Me.turnTabPage.TabIndex = 1
        Me.turnTabPage.Text = "Turns"
        Me.turnTabPage.UseVisualStyleBackColor = true
        '
        'colResults
        '
        Me.colResults.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colResults.FillWeight = 450!
        Me.colResults.HeaderText = "Please select the required location from the list below"
        Me.colResults.Name = "colResults"
        Me.colResults.ReadOnly = true
        Me.colResults.Width = 450
        '
        'mapTabPage
        '
        Me.mapTabPage.Controls.Add(Me.pnlWebBrowser)
        Me.mapTabPage.Controls.Add(Me.dgLocationResults)
        Me.mapTabPage.Location = New System.Drawing.Point(4, 22)
        Me.mapTabPage.Name = "mapTabPage"
        Me.mapTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.mapTabPage.Size = New System.Drawing.Size(756, 680)
        Me.mapTabPage.TabIndex = 0
        Me.mapTabPage.Text = "Map"
        Me.mapTabPage.UseVisualStyleBackColor = true
        '
        'pnlWebBrowser
        '
        Me.pnlWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWebBrowser.Location = New System.Drawing.Point(3, 3)
        Me.pnlWebBrowser.Name = "pnlWebBrowser"
        Me.pnlWebBrowser.Size = New System.Drawing.Size(750, 674)
        Me.pnlWebBrowser.TabIndex = 10
        '
        'dgLocationResults
        '
        Me.dgLocationResults.AllowUserToAddRows = false
        Me.dgLocationResults.AllowUserToDeleteRows = false
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgLocationResults.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgLocationResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgLocationResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colResults})
        Me.dgLocationResults.Location = New System.Drawing.Point(0, 6)
        Me.dgLocationResults.MultiSelect = false
        Me.dgLocationResults.Name = "dgLocationResults"
        Me.dgLocationResults.ReadOnly = true
        Me.dgLocationResults.RowHeadersVisible = false
        Me.dgLocationResults.Size = New System.Drawing.Size(454, 405)
        Me.dgLocationResults.TabIndex = 9
        Me.dgLocationResults.Visible = false
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.mapTabPage)
        Me.TabControl1.Controls.Add(Me.turnTabPage)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(764, 706)
        Me.TabControl1.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 23)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Vehicles"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbVehicles
        '
        Me.cmbVehicles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbVehicles.FormattingEnabled = true
        Me.cmbVehicles.Location = New System.Drawing.Point(90, 0)
        Me.cmbVehicles.Name = "cmbVehicles"
        Me.cmbVehicles.Size = New System.Drawing.Size(98, 21)
        Me.cmbVehicles.TabIndex = 18
        '
        'pnlVehRight
        '
        Me.pnlVehRight.AutoSize = true
        Me.pnlVehRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlVehRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlVehRight.Location = New System.Drawing.Point(90, 0)
        Me.pnlVehRight.MinimumSize = New System.Drawing.Size(100, 23)
        Me.pnlVehRight.Name = "pnlVehRight"
        Me.pnlVehRight.Size = New System.Drawing.Size(100, 23)
        Me.pnlVehRight.TabIndex = 1
        '
        'pnlVehLeft
        '
        Me.pnlVehLeft.AutoSize = true
        Me.pnlVehLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlVehLeft.Controls.Add(Me.Label2)
        Me.pnlVehLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlVehLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlVehLeft.MinimumSize = New System.Drawing.Size(90, 23)
        Me.pnlVehLeft.Name = "pnlVehLeft"
        Me.pnlVehLeft.Size = New System.Drawing.Size(90, 23)
        Me.pnlVehLeft.TabIndex = 0
        '
        'pnlVehicles
        '
        Me.pnlVehicles.Controls.Add(Me.cmbVehicles)
        Me.pnlVehicles.Controls.Add(Me.pnlVehRight)
        Me.pnlVehicles.Controls.Add(Me.pnlVehLeft)
        Me.pnlVehicles.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlVehicles.Location = New System.Drawing.Point(0, 0)
        Me.pnlVehicles.Name = "pnlVehicles"
        Me.pnlVehicles.Size = New System.Drawing.Size(188, 23)
        Me.pnlVehicles.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 23)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Geofences"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlGeoLeft
        '
        Me.pnlGeoLeft.AutoSize = true
        Me.pnlGeoLeft.Controls.Add(Me.Label3)
        Me.pnlGeoLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGeoLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlGeoLeft.MinimumSize = New System.Drawing.Size(90, 23)
        Me.pnlGeoLeft.Name = "pnlGeoLeft"
        Me.pnlGeoLeft.Size = New System.Drawing.Size(90, 23)
        Me.pnlGeoLeft.TabIndex = 23
        '
        'cmbGeofences
        '
        Me.cmbGeofences.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbGeofences.FormattingEnabled = true
        Me.cmbGeofences.Location = New System.Drawing.Point(0, 0)
        Me.cmbGeofences.Name = "cmbGeofences"
        Me.cmbGeofences.Size = New System.Drawing.Size(98, 21)
        Me.cmbGeofences.TabIndex = 18
        '
        'pnlGeofences
        '
        Me.pnlGeofences.AutoSize = true
        Me.pnlGeofences.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlGeofences.Controls.Add(Me.pnlGeoRight)
        Me.pnlGeofences.Controls.Add(Me.pnlGeoLeft)
        Me.pnlGeofences.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlGeofences.Location = New System.Drawing.Point(0, 23)
        Me.pnlGeofences.MinimumSize = New System.Drawing.Size(90, 23)
        Me.pnlGeofences.Name = "pnlGeofences"
        Me.pnlGeofences.Size = New System.Drawing.Size(188, 23)
        Me.pnlGeofences.TabIndex = 13
        '
        'pnlGeoRight
        '
        Me.pnlGeoRight.AutoSize = true
        Me.pnlGeoRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlGeoRight.Controls.Add(Me.cmbGeofences)
        Me.pnlGeoRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGeoRight.Location = New System.Drawing.Point(90, 0)
        Me.pnlGeoRight.Name = "pnlGeoRight"
        Me.pnlGeoRight.Size = New System.Drawing.Size(98, 23)
        Me.pnlGeoRight.TabIndex = 24
        '
        'pnlRoutesView
        '
        Me.pnlRoutesView.BackColor = System.Drawing.SystemColors.Control
        Me.pnlRoutesView.Controls.Add(Me.tvRoutes)
        Me.pnlRoutesView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRoutesView.Location = New System.Drawing.Point(0, 0)
        Me.pnlRoutesView.Name = "pnlRoutesView"
        Me.pnlRoutesView.Size = New System.Drawing.Size(188, 356)
        Me.pnlRoutesView.TabIndex = 2
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(189, 6)
        Me.ToolStripSeparator1.Visible = false
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
        Me.mnuPolygonGeofence.Enabled = false
        Me.mnuPolygonGeofence.Name = "mnuPolygonGeofence"
        Me.mnuPolygonGeofence.Size = New System.Drawing.Size(192, 22)
        Me.mnuPolygonGeofence.Text = "Polygon Geofence"
        Me.mnuPolygonGeofence.Visible = false
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
        Me.mnuBufferZones.Enabled = false
        Me.mnuBufferZones.Name = "mnuBufferZones"
        Me.mnuBufferZones.Size = New System.Drawing.Size(192, 22)
        Me.mnuBufferZones.Text = "Buffer Zones"
        Me.mnuBufferZones.Visible = false
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBufferZones, Me.mnuPolygonGeofence, Me.mnuDeselectAll, Me.ToolStripSeparator1, Me.mnuCreateSite, Me.mnuDisplayGeofence})
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(47, 20)
        Me.mnuTools.Text = "Tools"
        Me.mnuTools.Visible = false
        '
        'mnuDeselectAll
        '
        Me.mnuDeselectAll.Enabled = false
        Me.mnuDeselectAll.Name = "mnuDeselectAll"
        Me.mnuDeselectAll.Size = New System.Drawing.Size(192, 22)
        Me.mnuDeselectAll.Text = "Deselect All"
        Me.mnuDeselectAll.Visible = false
        '
        'mnuCreateSite
        '
        Me.mnuCreateSite.Enabled = false
        Me.mnuCreateSite.Name = "mnuCreateSite"
        Me.mnuCreateSite.Size = New System.Drawing.Size(192, 22)
        Me.mnuCreateSite.Text = "Create Site "
        Me.mnuCreateSite.Visible = false
        '
        'mnuDisplayGeofence
        '
        Me.mnuDisplayGeofence.Enabled = false
        Me.mnuDisplayGeofence.Name = "mnuDisplayGeofence"
        Me.mnuDisplayGeofence.Size = New System.Drawing.Size(192, 22)
        Me.mnuDisplayGeofence.Text = "Display Site Geofences"
        Me.mnuDisplayGeofence.Visible = false
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
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuTools, Me.mnuPrintMap, Me.LablesToolStripMenuItem, Me.mnuClearMap, Me.mnuDraw, Me.mnuDistanceCalculator, Me.mnuRoutes, Me.mnuImportKML})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(988, 24)
        Me.MenuStrip1.TabIndex = 44
        Me.MenuStrip1.Text = "MenuStrip1"
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
        Me.LablesToolStripMenuItem.Visible = false
        '
        'mnuDisplayDate
        '
        Me.mnuDisplayDate.CheckOnClick = true
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
        Me.mnuDraw.Visible = false
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
        Me.mnuDrawPolygon.Visible = false
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
        Me.mnuRoutes.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCreateRoute, Me.mnuShowRoutes})
        Me.mnuRoutes.Name = "mnuRoutes"
        Me.mnuRoutes.Size = New System.Drawing.Size(124, 20)
        Me.mnuRoutes.Text = "Route Management"
        Me.mnuRoutes.Visible = false
        '
        'mnuCreateRoute
        '
        Me.mnuCreateRoute.Name = "mnuCreateRoute"
        Me.mnuCreateRoute.Size = New System.Drawing.Size(142, 22)
        Me.mnuCreateRoute.Text = "Create Route"
        '
        'mnuShowRoutes
        '
        Me.mnuShowRoutes.Name = "mnuShowRoutes"
        Me.mnuShowRoutes.Size = New System.Drawing.Size(142, 22)
        Me.mnuShowRoutes.Text = "Show Routes"
        '
        'mnuImportKML
        '
        Me.mnuImportKML.Name = "mnuImportKML"
        Me.mnuImportKML.Size = New System.Drawing.Size(82, 20)
        Me.mnuImportKML.Text = "Import KML"
        Me.mnuImportKML.Visible = false
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnlShowRoutes)
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnlCreateRoutes)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlClickMap)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlAddress)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlSites)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlGeofences)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlVehicles)
        Me.SplitContainer2.Size = New System.Drawing.Size(188, 706)
        Me.SplitContainer2.SplitterDistance = 356
        Me.SplitContainer2.TabIndex = 0
        '
        'pnlShowRoutes
        '
        Me.pnlShowRoutes.Controls.Add(Me.pnlRoutesView)
        Me.pnlShowRoutes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlShowRoutes.Location = New System.Drawing.Point(0, 0)
        Me.pnlShowRoutes.Name = "pnlShowRoutes"
        Me.pnlShowRoutes.Size = New System.Drawing.Size(188, 356)
        Me.pnlShowRoutes.TabIndex = 29
        '
        'mainContainer1
        '
        Me.mainContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
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
        Me.mainContainer1.Size = New System.Drawing.Size(956, 706)
        Me.mainContainer1.SplitterDistance = 188
        Me.mainContainer1.TabIndex = 46
        '
        'MapRefreshTimer
        '
        Me.MapRefreshTimer.Enabled = true
        Me.MapRefreshTimer.Interval = 300000
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.AutoSize = true
        Me.btnRefresh.BackColor = System.Drawing.SystemColors.MenuBar
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"),System.Drawing.Image)
        Me.btnRefresh.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRefresh.Location = New System.Drawing.Point(928, 0)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(36, 24)
        Me.btnRefresh.TabIndex = 45
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Click to refresh map")
        Me.btnRefresh.UseVisualStyleBackColor = false
        '
        'frmMSTGoogleHistoryMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(988, 730)
        Me.Controls.Add(Me.mainContainer1)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = true
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmMSTGoogleHistoryMap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmMSTGoogleHistoryMap"
        Me.pnlSitesRight.ResumeLayout(false)
        Me.pnlSites.ResumeLayout(false)
        Me.pnlSites.PerformLayout
        Me.pnlSitesLeft.ResumeLayout(false)
        Me.pnlAddress.ResumeLayout(false)
        Me.pnlAddress.PerformLayout
        Me.pnlClickMap.ResumeLayout(false)
        Me.pnlTop.ResumeLayout(false)
        Me.pnlWaypointLabel.ResumeLayout(false)
        CType(Me.dgWayPoints,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlWaypointGrid.ResumeLayout(false)
        Me.pnlWaypoints.ResumeLayout(false)
        Me.pnlWaypoints.PerformLayout
        Me.pnlCreateRoutes.ResumeLayout(false)
        Me.pnlCreateRoutes.PerformLayout
        Me.pnlTurnsHeader.ResumeLayout(false)
        CType(Me.dgTurns,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTurnsGrid.ResumeLayout(false)
        Me.turnTabPage.ResumeLayout(false)
        Me.mapTabPage.ResumeLayout(false)
        CType(Me.dgLocationResults,System.ComponentModel.ISupportInitialize).EndInit
        Me.TabControl1.ResumeLayout(false)
        Me.pnlVehLeft.ResumeLayout(false)
        Me.pnlVehicles.ResumeLayout(false)
        Me.pnlVehicles.PerformLayout
        Me.pnlGeoLeft.ResumeLayout(false)
        Me.pnlGeofences.ResumeLayout(false)
        Me.pnlGeofences.PerformLayout
        Me.pnlGeoRight.ResumeLayout(false)
        Me.pnlRoutesView.ResumeLayout(false)
        Me.MenuStrip1.ResumeLayout(false)
        Me.MenuStrip1.PerformLayout
        Me.SplitContainer2.Panel1.ResumeLayout(false)
        Me.SplitContainer2.Panel2.ResumeLayout(false)
        Me.SplitContainer2.Panel2.PerformLayout
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer2.ResumeLayout(false)
        Me.pnlShowRoutes.ResumeLayout(false)
        Me.mainContainer1.Panel1.ResumeLayout(false)
        Me.mainContainer1.Panel2.ResumeLayout(false)
        CType(Me.mainContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.mainContainer1.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents tvRoutes As Windows.Forms.TreeView
    Friend WithEvents cmbSites As Windows.Forms.ComboBox
    Friend WithEvents pnlSitesRight As Windows.Forms.Panel
    Friend WithEvents pnlSites As Windows.Forms.Panel
    Friend WithEvents pnlSitesLeft As Windows.Forms.Panel
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents btnFindAddress As Windows.Forms.Button
    Friend WithEvents txtAddress As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents pnlAddress As Windows.Forms.Panel
    Friend WithEvents btnWayPointFromMap As Windows.Forms.Button
    Friend WithEvents pnlClickMap As Windows.Forms.Panel
    Friend WithEvents btnClearRoute As Windows.Forms.Button
    Friend WithEvents btnSaveRoute As Windows.Forms.Button
    Friend WithEvents btnCreateRoute As Windows.Forms.Button
    Friend WithEvents pnlTop As Windows.Forms.Panel
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents pnlWaypointLabel As Windows.Forms.Panel
    Friend WithEvents WayPoints As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgWayPoints As Windows.Forms.DataGridView
    Friend WithEvents pnlWaypointGrid As Windows.Forms.Panel
    Friend WithEvents pnlWaypoints As Windows.Forms.Panel
    Friend WithEvents pnlCreateRoutes As Windows.Forms.Panel
    Friend WithEvents btnPrint As Windows.Forms.Button
    Friend WithEvents PrintDialog1 As Windows.Forms.PrintDialog
    Friend WithEvents btnEmail As Windows.Forms.Button
    Friend WithEvents pnlTurnsHeader As Windows.Forms.Panel
    Friend WithEvents Time As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Distance As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TurnLocation As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Turn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgTurns As Windows.Forms.DataGridView
    Friend WithEvents pnlTurnsGrid As Windows.Forms.Panel
    Friend WithEvents turnTabPage As Windows.Forms.TabPage
    Friend WithEvents colResults As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mapTabPage As Windows.Forms.TabPage
    Friend WithEvents dgLocationResults As Windows.Forms.DataGridView
    Friend WithEvents TabControl1 As Windows.Forms.TabControl
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents cmbVehicles As Windows.Forms.ComboBox
    Friend WithEvents pnlVehRight As Windows.Forms.Panel
    Friend WithEvents pnlVehLeft As Windows.Forms.Panel
    Friend WithEvents pnlVehicles As Windows.Forms.Panel
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents pnlGeoLeft As Windows.Forms.Panel
    Friend WithEvents cmbGeofences As Windows.Forms.ComboBox
    Friend WithEvents pnlGeofences As Windows.Forms.Panel
    Friend WithEvents pnlGeoRight As Windows.Forms.Panel
    Friend WithEvents pnlRoutesView As Windows.Forms.Panel
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
    Friend WithEvents MenuStrip1 As Windows.Forms.MenuStrip
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
    Friend WithEvents mnuRoutes As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCreateRoute As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShowRoutes As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImportKML As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer2 As Windows.Forms.SplitContainer
    Friend WithEvents pnlShowRoutes As Windows.Forms.Panel
    Friend WithEvents mainContainer1 As Windows.Forms.SplitContainer
    Friend WithEvents MapRefreshTimer As Windows.Forms.Timer
    Friend WithEvents btnRefresh As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents pnlWebBrowser As Windows.Forms.Panel
    Friend WithEvents mnuPrintMap As Windows.Forms.ToolStripMenuItem
End Class
