var AllowBirdsEye = false;
var IsQuickHistory = false;
var IsFirstQuickHistory = false;
var PlottedVehicle = "none_zzID";
var CurrenttvTreeNodeLevel = 0;
var PageRefreshTimer = 30000;
var IsPlottingQuickHistory = false;
var CurrentZoomLevel = 15;
var zoomLevel;
var ShowMapFirst;
var clientID;
var selStyle;
var selMode;
var map = null;
var icon;
var pinid = 0;
var MapEng;
var VE_Map_height;
var VE_Map_width;
var browserName = "Unknown";
var NumofPlots = 0;
var VehicleArray = [];
var Markers = [];
var Markers_Detail = [];
var VehLabels = [];
var CurrentVehicleArray = [];
var QuickHistoryArray = [];
var myVehArray = [];
var DisplayOption = "";
var DisplayWin;
var DisplayMsgWin;
var GeofenceArray = [];
var SiteArray = [];
var CreatingGeofence = false;
var CreatingSite = false;
var IsDisplayingGeofence = false;
var IsDisplayingSites = false;
var IsDisplayingSiteLabels = false;
var Fleet_Code = null;
var Company = null;
var useDrivers = "";
var dgVehicles;
var dgVehicles_contextMenu;
var dgVehicleDetails;
var dgVehicleDetails_contextMenu;
var dgMessages;
var dgMessages_contextMenu;
var dgLocationResults;
var dgClosestSite;
var tvVehicles;
var tvVehicles_contextMenu;
var fleets_TabBar;
var lastActiveTab;
var IsRefresh = false;
var AllowMessaging;
var UseGeofences = false;
var MsgWin;
var MessageSelected = false;
var MsgTermType = null;
var LatLongFormat = 1;
var newSiteName;
var newLat = 0;
var newLong = 0;
var dgNewGeofence_Fleets;
var dgNewSite_Fleets;
var dhxLayout;
var dhxLayout1;
var dhxLayout2;
var screen_avail_height;
var screen_avail_width;
var opacity = 0.4;
var circle = [];
var centerMarker;
var circleUnits;
var circleRadius;
var circleCount = 0;
var UseIcons;
var Not_Fleet_Colours = true;
var infowindow;
var SkinColor;
var IsDisplayingVehicleLabels = true;
var ShowTraffic = false;
var showGeofences = "False";
var showSites = "False";
var trafficLayer;
var geoFenceLayer = null;
var fixedMapBounds = 0;
var fixedMapRectangle;
var fixedMapZoom = 0;
var ctrl_VE_Address;
var map_contextMenu;
var map_SiteContextMenu;
var thisLatLong;
var mapOptionsMenu;
var geoCenter;
var distunits;
var geofenceMarkersArray = [];
var siteMarkers = [];
var autoLocate;
var connectLine;
var connectLineID;
var connectLineColour;
var connectLineWeight;
var connectLineOpacity;
var connectLineArray = [];
var currentCusorY;
var connectLineStartPoint = null;
var UseSites = false;
var VehicleShapeLayer = null;
var VehiclePinLayer = null;
var VehicleQuickHistoryPinLayer = null;
var SitesShapeLayer;
var SitesPinLayer;
var siteLabels = [];
var siteMarkers_Detail = [];
var SelectedSite;
var thisSelectedSite = "";
var NoofSites = 0;
var frmSite;
var dgSiteSearchResults;
var p;
var SiteLabelColour;
var Site_Select_Combo, SiteToSend;
var IsLoading_Site_Select_Combo;
var Send_Site_Select_Combo;
var Send_Vehicle_Select_Combo;
var Days_Select_Combo;
var VehicleToSend;
var IsFromClosestSite = false;
var IsSendingSite = false;
var connectSiteLineArray = [];
var w1;
var dhxWinSite;
var dhxWins;
var cmbNumMessage;
var selectedVehicles = "";
var hasSelectedVehicles = false;

	function BuildArray(tmpArrayStr) {	
	debugger;
   if(isBufferZone == true){
	        var tmpBufferZoneArray = tmpArrayStr.split('~');	
           BufferZoneArray = []; 
           BufferPath = "["; 
	        var k = 0;	
	        for (var i = 0; i < tmpArray.length; i++) {	
	            BufferZoneArray[k] = tmpArray[i].split('|');	
               if( i == tmpArray.length){  
                BufferPath += new google.maps.LatLng(BufferZoneArray[k][0],BufferZoneArray[k][1])
                BufferPath += "];"            }
       else {
                BufferPath += new google.maps.LatLng(BufferZoneArray[k][0],BufferZoneArray[k][1]) +","
               }
            }
            }
       else {
	        try {	
	        var tmpArray = tmpArrayStr.split('~');	
	        VehicleArray = [];	
	        var k = 0;	
	        for (var i = 0; i < tmpArray.length; i++) {	
	            VehicleArray[k] = tmpArray[i].split('|');	
	            if (VehicleArray[k][2] !== "") {	
	                k++;	
	            }	
	        }	
	    }	
	    catch (Error) {	
	        //window.alert("Build Array Error: " + Error.message);	
	        }	
	    }	
   }



function map_contextMenuCheckboxClicked(id) {
    var checkedItem = id;
    var state = map_contextMenu.getCheckboxState(checkedItem);
    switch (id) {

        case 'showLabels':
            IsDisplayingVehicleLabels = true;
            if (state == true) {
                IsDisplayingVehicleLabels = false;

            }
            SaveMapOptions();
            if (MapEng == "MS_VirtualEarth") {
                Load_VE_Map();
            }
            else if (MapEng == "Google_Maps") {
                Load_Google_Map();
            }
            break;
        case 'showSiteLabels':
            if (state == true) {
                IsDisplayingSiteLabels = false;
                if (MapEng == "MS_VirtualEarth") {
                    clearSiteLayers();
                    Load_VE_Map();
                }
                if (MapEng == "Google_Maps") {
                    if (siteLabels) {
                        for (i in siteLabels) {
                            siteLabels[i].setMap(null);
                        }

                    }
                }
            }
            else {
                IsDisplayingSiteLabels = true;
                if (MapEng == "MS_VirtualEarth") {
                    Load_VE_Map();
                }
                else if (MapEng == "Google_Maps") {
                    Load_Google_Map();
                }
            }
            SaveMapOptions();

            break;
        case 'showTraffic':
            if (MapEng == "Google_Maps") {
                ShowTraffic = true;
                if (state == true) {
                    ShowTraffic = false;
                }
                if (ShowTraffic == true) {
                    trafficLayer.setMap(map);
                }
                else {
                    trafficLayer.setMap();
                }
            }
            break;
        case 'showGeofences':
            if (state == true) {
                ShowGeofences(false);
            }
            else {
                ShowGeofences(true);
            }
            break;
        case 'showSites':
            if (state == true) {
                ShowSites(false);
            }
            else {
                ShowSites(true);
            }
            break;

        case 'freezeMapExtent':
            fixedMapBounds = 0;
            fixedMapZoom = 0;
            if (MapEng == "MS_VirtualEarth") {
                if (state == false) {
                    fixedMapBounds = map.GetCenter();
                    fixedMapZoom = map.GetZoomLevel();
                }
            }
            else if (MapEng == "Google_Maps") {
                if (state == false) {
                    fixedMapBounds = map.getBounds();
                    fixedMapZoom = map.getZoom();
                }
            }
            break;
    }
    SaveMapOptions();
    return true;
}

function SaveMapOptions() {
    var tmpMapOptions;
    if (IsDisplayingVehicleLabels == true) {
        tmpMapOptions = "1|";
    }
    else {
        tmpMapOptions = "0|";
    }
    if (ShowTraffic == true) {
        tmpMapOptions = tmpMapOptions + "1|";
    }
    else {
        tmpMapOptions = tmpMapOptions + "0|";
    }
    if (IsDisplayingGeofence == true) {
        tmpMapOptions = tmpMapOptions + "1|";
    }
    else {
        tmpMapOptions = tmpMapOptions + "0|";
    }
    tmpMapOptions = tmpMapOptions + fixedMapBounds + "|" + fixedMapZoom + "|";
    if (IsDisplayingSites == true) {
        tmpMapOptions = tmpMapOptions + "1|";
    }
    else {
        tmpMapOptions = tmpMapOptions + "0|";
    }
    if (IsDisplayingSiteLabels == true) {
        tmpMapOptions = tmpMapOptions + "1";
    }
    else {
        tmpMapOptions = tmpMapOptions + "0";
    }
    document.getElementById("hiddenMapOptions").value = tmpMapOptions;
    document.getElementById("hiddenButtonSaveMapOptions").click();
}
function ShowMapSiteContextMenu(id) {
    lastActiveTab = fleets_TabBar.getActiveTab();
    switch (id) {
        case 'ClosestVehicle':
            ShowClosestWindow();

            break;
        case 'lastHour':
            break;
        case 'last4Hours':
            break;
        case 'last8Hours':
            break;
    }
}
function UnloadClosestWindow() {
    try {
        if (w1) {
            w1.hide();
            w1 = null;
        }
        if (frmSite) {
            frmSite.unload();
        }
    } catch (e) {
    }

}
function ShowClosestWindow() {
    try {
        UnloadClosestWindow();
        //document.getElementById("FindClosest_Container").style.display = "inline";
        //dhxWins = null;
        dhxWinSite = new dhtmlXWindows();
        dhxWinSite.setImagePath("codebase/imgs/");
        dhxWinSite.attachEvent("onClose", function () {
            w1.hide();
            w1 = null;
            UnloadClosestWindow();
        });
        w1 = dhxWinSite.createWindow("w1", 30, 40, 500, 400);
        w1.setText("Find Closest to Site");
        dhxWinSite.window("w1").button("park").hide();
        dhxWinSite.window("w1").button("minmax1").hide();
        w1.attachObject("FindClosest_Container");
        frmSite = new dhtmlXForm("SiteForm_Container");
        thisSelectedSite = "";
        if (MapEng == "Google_Maps") {
            thisSelectedSite = SelectedSite.title;
        }
        else if (MapEng == "MS_VirtualEarth") {
            thisSelectedSite = SelectedSite;
        }
        frmSite.attachEvent("onButtonClick", function (name, command) {
            SubmitSearch(name, command, thisSelectedSite);
        });
        frmSite.loadStruct("Scripts/FindClosest.xml?e=" + new Date().getTime(), function () {
            frmSite.setItemValue("txtSelectedSite", thisSelectedSite);
            if (distunits == "Miles") {
                frmSite.setItemLabel("lblRadius", "Radius (mls)");
            }
            else {
                frmSite.setItemLabel("lblRadius", "Radius (kms)");
            }
        });
        if (dgClosestSite) {
            dgClosestSite.destructor();
            dgClosestSite = null;
        }
        dgClosestSite = new dhtmlXGridObject('FindClosest_Grid');
        dgClosestSite.setImagePath("codebase/imgs/");
        dgClosestSite.setInitWidths("125,75,125,47.5,0,0,0,0,0,0,0,0,0");
        if (DisplayOption == "ByDesc") {
            dgClosestSite.setHeader("Description, Distance, Date/Time, Ign,,,,,,,,,");
        }
        else {
            dgClosestSite.setHeader("Reg No,Distance, Date/Time, Ign,,,,,,,,,");
        }
        dgClosestSite.enableTooltips("false");
        dgClosestSite.setColAlign("left,center,center,center");
        dgClosestSite.setSkin(SkinColor);
        dgClosestSite.init();
        dgClosestSite.enableSmartRendering(true);
        dgClosestSite.attachEvent("onRowSelect", GetCurrentLocationToSite);
        dgClosestSite.setColSorting("str,int,str,na");
        dgClosestSite.preventIECaching(true);
    }
    catch (Error) {
        alert("Error 552: " + Error.Message);
    }
}

function SubmitSearch(name, command, thisSelectedSite) {
    var selectedOption = frmSite.getCheckedValue("type");
    document.getElementById("hiddenSearchForSite").value = command;
    document.getElementById("hiddenSiteSearchRadius").value = frmSite.getItemValue("txtRadius");
    document.getElementById("hiddenSiteSearchOption").value = selectedOption;
    document.getElementById("hiddenSelectedSite").value = thisSelectedSite;
    document.getElementById("hiddenButtonSiteSearch").click();

}
function ShowSiteSearchResutls() {
    try {
        clientID = document.getElementById("hiddenClientID").value;
        dgClosestSite.clearAll();
        var Results_xmlFile = "xmlFiles/" + clientID + "_SiteSearchResultsgrid.xml?etc=" + new Date().getTime();
        dgClosestSite.loadXML(Results_xmlFile, function () {
            dgClosestSite.sortRows(1, "int", "asc");
            dgClosestSite.setSizes();

            document.getElementById("btnExportToExcel").style.visibility = "visible";
        });
    } catch (e) {
        alert("Error 596: " + e.Message);
    }
}
function GetCurrentLocationToSite(rowID, cell) {
    var thisVehicle = dgClosestSite.cells(rowID, cell).getValue();
    var startLat = 0;
    var startLong = 0;
    for (var i = 0; i <= SiteArray.length - 1; i++) {
        if (SiteArray[i][0] == thisSelectedSite) {
            startLat = SiteArray[i][6];
            startLong = SiteArray[i][7];
        }
    }

    if (MapEng !== "") {
        IsFromClosestSite = false;
        var selectedOption = frmSite.getCheckedValue("type");
        var tmpArrayStr;
        var thisReg;
        thisLat = 0;
        thisLong = 0;
        switch (selectedOption) {

            case "current":
                for (i = 0; i <= CurrentVehicleArray.length - 1; i++) {
                    if (thisVehicle == CurrentVehicleArray[i][0] || thisVehicle == CurrentVehicleArray[i][1]) {
                        thisReg = CurrentVehicleArray[i][0];
                        thisDesc = CurrentVehicleArray[i][1];
                        PlottedVehicle = thisDesc;
                        thisLat = CurrentVehicleArray[i][2];
                        thisLong = CurrentVehicleArray[i][3];
                        thisDateTime = CurrentVehicleArray[i][4];
                        thisIgnStatus = CurrentVehicleArray[i][7];
                        thisLoc = CurrentVehicleArray[i][6];
                        thisSpdDir = CurrentVehicleArray[i][5];
                        thisType = CurrentVehicleArray[i][9];
                        thisOdo = addCommas(CurrentVehicleArray[i][11]) + ' ';
                        thisVehType = CurrentVehicleArray[i][10];
                        thisHeading = CurrentVehicleArray[i][8];
                        thisDriver = CurrentVehicleArray[i][36];
                        tmpArrayStr = thisReg + '|' + thisDesc + '|' + thisLat + '|' + thisLong + '|' + thisDateTime + '|' + thisSpdDir + '|' + thisLoc + '|' + thisIgnStatus + '|' + thisHeading + '|' + thisType + '|' + thisVehType + '|' + thisOdo + '|' + thisDriver;
                        if (CurrentVehicleArray[i][37] === "LMU") {
                            tmpArrayStr = tmpArrayStr + '|' + CurrentVehicleArray[i][43] + '|' + CurrentVehicleArray[i][44] + '|' + CurrentVehicleArray[i][45] + '|' + CurrentVehicleArray[i][46] + '|' + CurrentVehicleArray[i][47] + '|' + CurrentVehicleArray[i][48];
                        }
                        myVehArray = tmpArrayStr;
                        NumofPlots = 1;
                        //i = CurrentVehicleArray.length + 1;
                        if (thisLat !== 0 && thisLong !== 0 && IsValidLat(thisLat)) {
                            IsFromClosestSite = true;
                            if (MapEng == "MS_VirtualEarth") {
                                Load_VE_Map();
                            }
                            if (MapEng == "Google_Maps") {
                                Load_Google_Map();
                            }
                            if (MapEng == "MSTMap") {
                                try {
                                    fleets_TabBar.setTabActive("FT_Map", true);
                                }
                                catch (Error) {
                                }
                                wantedVehicle = "MSTMap" + "|" + PlottedVehicle + "|" + 2;
                                document.getElementById("hiddenMSTWantedVehicle").value = wantedVehicle;
                                document.getElementById("btnMSTWantedVehicle").click();
                                document.getElementById("loading").className = "loading-visible";
                                tmpMsg = document.getElementById("hiddenLoadingMap").value;
                                document.getElementById("loadingLabel").innerHTML = tmpMsg;
                            }
                            IsFromClosestSite = false;
                        }
                    }
                }
                break;
            default:
                try {
                    thisReg = dgClosestSite.cells(rowID, 0).getValue();
                    var thisDesc = dgClosestSite.cells(rowID, 0).getValue();
                    PlottedVehicle = thisDesc;
                    var thisLat = dgClosestSite.cells(rowID, 8).getValue();
                    var thisLong = dgClosestSite.cells(rowID, 9).getValue();
                    var thisDateTime = dgClosestSite.cells(rowID, 2).getValue();
                    var thisIgnStatus = dgClosestSite.cells(rowID, 3).getValue();
                    var thisLoc = dgClosestSite.cells(rowID, 4).getValue();
                    var thisSpdDir = dgClosestSite.cells(rowID, 5).getValue();
                    var thisType = dgClosestSite.cells(rowID, 6).getValue();
                    var thisOdo = addCommas(dgClosestSite.cells(rowID, 10).getValue()) + ' ';
                    var thisVehType = dgClosestSite.cells(rowID, 11).getValue();
                    var thisHeading = dgClosestSite.cells(rowID, 12).getValue();
                    var thisDriver = dgClosestSite.cells(rowID, 7).getValue();
                    tmpArrayStr = thisReg + '|' + thisDesc + '|' + thisLat + '|' + thisLong + '|' + thisDateTime + '|' + thisSpdDir + '|' + thisLoc + '|' + thisIgnStatus + '|' + thisHeading + '|' + thisType + '|' + thisVehType + '|' + thisOdo + '|' + thisDriver;
                    myVehArray = tmpArrayStr;
                    NumofPlots = 1;

                    if (thisLat !== 0 && thisLong !== 0 && IsValidLat(thisLat)) {
                        if (MapEng == "MS_VirtualEarth") {
                            Load_VE_Map();
                        }
                        if (MapEng == "Google_Maps") {
                            Load_Google_Map();
                        }
                        if (MapEng == "MSTMap") {
                            try {
                                fleets_TabBar.setTabActive("FT_Map", true);
                            }
                            catch (Error) {
                            }
                            var wantedVehicle = "MSTMap" + "|" + PlottedVehicle + "|" + 2;
                            document.getElementById("hiddenMSTWantedVehicle").value = wantedVehicle;
                            document.getElementById("btnMSTWantedVehicle").click();
                            document.getElementById("loading").className = "loading-visible";
                            var tmpMsg = document.getElementById("hiddenLoadingMap").value;
                            document.getElementById("loadingLabel").innerHTML = tmpMsg;
                        }
                    }

                    break;
                } catch (e) {
                    alert("Error 689: " + e.Message);
                }
        }
        ConnectSiteToVehicle(startLat, startLong, thisLat, thisLong);
    }
    else {
        window.alert("Unable to display this location - Incorrect Map setting - Contact your distributor");
    }
}
function ConnectSiteToVehicle(startLat, startLong, endLat, endLong) {
    var startPoint;
    var endPoint;
    var lineCoordinates;
    if (MapEng == "MS_VirtualEarth") {
        try {
            if (connectSiteLineArray) {
                for (i in connectSiteLineArray) {
                    map.DeletePolyline(i);
                }
                connectSiteLineArray = [];
            }
        }
        catch (Error) {
        }
        endPoint = new VELatLong(endLat, endLong);
        startPoint = new VELatLong(startLat, startLong);
        lineCoordinates = [
            startPoint,
            endPoint];
        var linecolor = connectLineColour.substring(connectLineColour.length, connectLineColour.length - 6);
        var converter = new VEHexStringToColor();
        var veColor = converter.Convert(linecolor);
        veColor.A = connectLineOpacity;
        var width = connectLineWeight;
        connectLineID = Math.floor(Math.random() * 20);
        var connectLine = new VEPolyline(connectLineID, lineCoordinates, veColor, width);
        map.AddPolyline(connectLine);

        connectSiteLineArray.push(connectLine);
    }
    else if (MapEng == "Google_Maps") {
        try {
            if (connectSiteLineArray) {
                for (i in connectSiteLineArray) {
                    connectSiteLineArray[i].setMap(null);
                }
                connectSiteLineArray = [];
            }
            zoomLevel = map.getZoom();
            var overlay = new google.maps.OverlayView();
            overlay.draw = function () { };
            overlay.setMap(map);
            startPoint = new google.maps.LatLng(startLat, startLong);
            endPoint = new google.maps.LatLng(endLat, endLong);
            lineCoordinates = [
                startPoint,
                endPoint];
            connectLine = new google.maps.Polyline({
                path: lineCoordinates,
                strokeColor: connectLineColour,
                strokeOpacity: connectLineOpacity,
                strokeWeight: connectLineWeight
            });
            connectLine.setMap(map);
            connectSiteLineArray.push(connectLine);
        } catch (Error) {
            alert("Error 840: " + Error.Message);
        }
    }
}

function ExportSiteGridToExcel() {
    document.getElementById("btnHiddenExportToExcel").click();
}
function ShowMapContextMenu(id) {
    lastActiveTab = fleets_TabBar.getActiveTab();
    var tmpArray;
    switch (id) {
        case 'createGeofence':
            thisZoomLevel = 15;
            if (CreatingGeofence === false) {
                CreatingGeofence = true;
                if (MapEng == "Google_Maps") {
                    thisZoomLevel = map.getZoom();
                } else if (MapEng == "MS_VirtualEarth")
                    thisZoomLevel = map.GetZoomLevel();
            }
            if (thisZoomLevel > 13) {
                tmpArray = thisLatLong.split(',');
                document.getElementById("hiddenNewGeofenceRadius").value = 0;
                CreateNewGeofence(tmpArray[0], tmpArray[1], thisLatLong);
            }
            else {
                cantMsg = document.getElementById("hiddenCannotCreateGeo").value;
                window.alert(cantMsg);
            }
            CreatingGeofence = false;
            break;
        case 'createSite':
            var thisZoomLevel = 15;
            if (CreatingSite === false) {
                CreatingSite = true;
                if (document.getElementById("hiddenHasCategories").value == "True") {
                    if (MapEng == "Google_Maps") {
                        thisZoomLevel = map.getZoom();
                    }
                    else if (MapEng == "MS_VirtualEarth") {
                        thisZoomLevel = map.GetZoomLevel();
                    }
                    if (thisZoomLevel > 13) {
                        tmpArray = thisLatLong.split(',');
                        CreateNewSite(tmpArray[0], tmpArray[1], null);
                    }
                    else {
                        cantMsg = document.getElementById("hiddenCannotCreateSite").value;
                        window.alert(cantMsg);
                    }
                }
                else {
                    var cantMsg = "No categories created, cannot create site.";
                    window.alert(cantMsg);
                }
                CreatingSite = false;
            }
            break;
        case 'sendLoc':
            try {
                document.getElementById("combo_SendSite").style.display = "none";
                document.getElementById("txtSite").style.display = "inline";
                document.getElementById("lblCreateSite").style.display = "inline";
                if (!Send_Vehicle_Select_Combo) {
                    Send_Vehicle_Select_Combo = new dhtmlXCombo("combo_SendVehicle", "Sendvehicle_Combo", 250);
                }
                else {
                    Send_Vehicle_Select_Combo.clearAll();
                }
                for (i = 0; i < CurrentVehicleArray.length; i++) {
                    if (CurrentVehicleArray[i][38] == "3") {
                        if (DisplayOption == "ByDesc") {
                            Send_Vehicle_Select_Combo.addOption([[i, CurrentVehicleArray[i][1]]]);
                        }
                        else {
                            Send_Vehicle_Select_Combo.addOption([[i, CurrentVehicleArray[i][0]]]);
                        }
                    }
                }
                Send_Vehicle_Select_Combo.enableFilteringMode(true);
                //Send_Vehicle_Select_Combo.attachEvent("onChange", SelectedVehicleChanged)
                var tmpSiteArrayStr = document.getElementById("hiddenSitesArray").value;
                if (tmpSiteArrayStr.length > 0) {
                    BuildSiteArray(tmpSiteArrayStr);
                }
                if (UseSites == "True") {
                    window.dhx_globalImgPath = "codebase/imgs/";
                    if (!Send_Site_Select_Combo) {
                        Send_Site_Select_Combo = new dhtmlXCombo("combo_SendSite", "SendSite_Combo", 250);
                    }
                    else {
                        Send_Site_Select_Combo.clearAll();
                    }
                    for (var i = 0; i < SiteArray.length; i++) {
                        Send_Site_Select_Combo.addOption([[i, SiteArray[i][0]]]);
                    }
                    Send_Site_Select_Combo.enableFilteringMode(true);
                    Send_Site_Select_Combo.attachEvent("onChange", SelectedSiteChanged);
                }
                else {
                    document.getElementById("chkCreateSite").style.display = "none";
                    document.getElementById("lblCreateSite").style.display = "none";
                }
                IsSendingSite = false;
                tmpArray = thisLatLong.split(',');
                document.getElementById("txtSite").value = "";
                document.getElementById("txtLat").value = parseFloat(tmpArray[0]).toFixed(5);
                document.getElementById("txtLong").value = parseFloat(tmpArray[1]).toFixed(5);
                dhxWins = new dhtmlXWindows();
                dhxWins.setImagePath("../../../dhtmlxMenu/codebase/imgs/");
                DisplayWin = null;
                DisplayWin = dhxWins.createWindow("w1", 55, 55, 365, 175);
                dhxWins.window("w1").button("close").hide();
                dhxWins.window("w1").button("park").hide();
                dhxWins.window("w1").button("minmax1").hide();
                DisplayWin.setText("Send location to Garmin PND");
                DisplayWin.attachObject("SendLocToGarminPND_container");
                DisplayWin.centerOnScreen();
                DisplayWin.setModal(true);
                DisplayWin.show();
                document.getElementById("txtSite").focus();

            }
            catch (Error) {
                window.alert("Error Sending location to Garmin PDN: " + Error.Message);
            }
            break;
        default:
            break;
    }
}
function SelectedSiteChanged() {
    SiteToSend = null;
    SiteToSend = Send_Site_Select_Combo.getSelectedText();
    document.getElementById("txtSite").value = SiteToSend;
    toggleddlSites();
}
function SendGarminLocation() {
    var SelectedVal = Send_Vehicle_Select_Combo.getSelectedText();
    if (SelectedVal == "") {
        return false;
    }
    var tmpSite;
    if (IsSendingSite == false) {
        tmpSite = document.getElementById("txtSite").value;
    }
    else if (IsSendingSite == true) {
        tmpSite = Send_Site_Select_Combo.getSelectedText();
    }
    if (tmpSite == "") {
        // return false;
    }
    var tmpLat = document.getElementById("txtLat").value;
    var tmpLong = document.getElementById("txtLong").value;
    document.getElementById("hiddenSelectedGarminPND").value = SelectedVal + "/" + tmpSite + "/" + tmpLat + "/" + tmpLong;
    document.getElementById("combo_SendSite").style.display = "none";
    document.getElementById("txtSite").style.display = "inline";
    document.getElementById("lblSite").innerHTML = "Name:";
    document.getElementById("hiddenButtonSendLoctoGarminPND").click();
    if (IsSendingSite == false) {
        if (document.getElementById("chkCreateSite").checked == true) {
            CreateNewSite(tmpLat, tmpLong, tmpSite);
        }
    }
    document.getElementById("combo_SendSite").style.display = "none";
    document.getElementById("txtSite").style.display = "inline";
    document.getElementById("lblSite").innerHTML = "Name:";
    Send_Vehicle_Select_Combo = null;
    DisplayWin.setModal(false);
    DisplayWin.hide();
    dhxWins.unload();
    return false;
}
function CancelSendGarminLocation() {
    DisplayWin.setModal(false);
    DisplayWin.hide();
    document.getElementById("combo_SendSite").style.display = "none";
    document.getElementById("txtSite").style.display = "inline";
    document.getElementById("chkCreateSite").checked = false;
    document.getElementById("lblSite").innerHTML = "Name:";
}
function toggletxtSite() {
    if (UseSites == "True") {
        document.getElementById("txtSite").style.display = "none";
        document.getElementById("combo_SendSite").style.display = "inline";
        Send_Site_Select_Combo.setComboText("");
        document.getElementById("lblSite").innerHTML = "Select Site:";
        document.getElementById("chkCreateSite").style.display = "none";
        document.getElementById("lblCreateSite").style.display = "none";
        document.getElementById("combo_SendSite").focus();
    }
}
function toggleddlSites() {
    document.getElementById("combo_SendSite").style.display = "none";
    document.getElementById("txtSite").style.display = "inline";
    document.getElementById("lblSite").innerHTML = "Name:";
    document.getElementById("chkCreateSite").style.display = "inline";
    document.getElementById("lblCreateSite").style.display = "inline";
    document.getElementById("txtSite").focus();
}
function ddlSitesIndexChanged() {
    var PNDSite = Send_Site_Select_Combo.getSelectedText();
    for (var i = 0; i < SiteArray.length; i++) {
        if (SiteArray[i][0] == PNDSite) {
            document.getElementById("txtLat").value = parseFloat(SiteArray[i][6]).toFixed(5);
            document.getElementById("txtLong").value = parseFloat(SiteArray[i][7]).toFixed(5);
            IsSendingSite = true;
        }
        else {
            if (PNDSite == "") {
                document.getElementById("txtSite").style.display = "inline";
                document.getElementById("combo_SendSite").style.display = "none";
                document.getElementById("lblSite").innerHTML = "Name:";
                IsSendingSite = false;
            }
        }
    }
}
function ShowMsgGridContextMenu(id) {
    if (MessageSelected) {
        var rowID = dgMessages.getSelectedRowId();
        var ToThisTerminal;
        var thisMessage = dgMessages.cells(rowID, 6).getValue();
        document.getElementById('hiddenSelectedMessage').value = id + "," + thisMessage;
        if (DisplayOption == "ByReg") {
            ToThisTerminal = dgMessages.cells(rowID, 0).getValue();
        }
        else {
            ToThisTerminal = dgMessages.cells(rowID, 1).getValue();
        }
        switch (id) {
            case 'reply':
                SendMessage(ToThisTerminal);
                break;
            case 'read':
                dgMessages.setCellTextStyle(rowID, 6, "background-color: transparent");
                document.getElementById("hiddenButtonMsgGridClick").click();
                //document.getElementById("btnRefresh").click();
                break;
            case 'delete':
                dgMessages.deleteSelectedRows();
                document.getElementById("hiddenButtonMsgGridClick").click();
                //document.getElementById("btnRefresh").click();
                break;
            default:
                break;

        }
    }
    else {
        window.alert("No message selected, Left Click the required message.");
    }
}
function ShowGridContextMenu(id) {
    var rowID = dgVehicles.getSelectedRowId();
    var ToThisTerminal;
    if (DisplayOption == "ByReg" || DisplayOption == "BothByReg") {
        ToThisTerminal = dgVehicles.cells(rowID, 0).getValue();
    }
    else {
        ToThisTerminal = dgVehicles.cells(rowID, 1).getValue();
    }
    switch (id) {
        case 'poll':
            dgVehPollVehicle();
            break;
        case 'createGeofence':
            document.getElementById("hiddenNewGeofenceRadius").value = 0;
            dgVehCreateGeofence();
            break;
        case 'createSite':
            if (CreatingSite === false) {
                CreatingSite = true;
                if (document.getElementById("hiddenHasCategories").value == "True") {
                    dgVehCreateSite();
                }
                else {
                    var cantMsg = "No categories created, cannot create site.";
                    window.alert(cantMsg);
                }
                CreatingSite = false;
            }
            break;
        case 'displayGPS':
            dgVehDispLatLong();
            break;
        case 'sendMessage':
            SendMessage(ToThisTerminal);
            break;
        default:
            break;
    }
}
function ShowTreeContextMenu(id) {
    IsQuickHistory = false;
    switch (id) {
        case 'poll':
            document.getElementById("hiddenSelectedVehicles").value = "";
            cMenu_PollVehicle();
            break;
        case 'createGeofence':
            cMenu_CreateGeoFence();
            break;
        case 'createSite':
            cMenu_CreateSite();
            break;
        case 'displayQuickHistory':
            cMenu_QuickHistory();
            break;
        case 'sendMessage':
            SendMessage(document.getElementById('hiddenSelectedVehicle').value);
            break;
        case 'sendAll':
            document.getElementById("hiddenSelectedVehicles").value = selectedVehicles;
            SendMessage("ALL");
            break;
        case 'plotAll':
            Load_Initial_VE_Map();
            break;
        case "pollAll":
            document.getElementById("hiddenSelectedVehicles").value = selectedVehicles;
            cMenu_PollVehicle("");
            break;
        case 'toggleOutput':
            document.getElementById("hiddenButtonToggleOutput1").click();
            break;
        default:
            break;
    }
}

function addCommas(nStr) {
    var x;
    var x1;
    var x2;
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
function CallPrint(strid) {
    var prtContent = document.getElementById(strid);
    var WinPrint = window.open('', '', 'left=0,top=0,width=1000,height=600,toolbar=0,scrollbars=0,status=0');
    WinPrint.document.write(prtContent.innerHTML);
    WinPrint.document.close();
    WinPrint.focus();
    WinPrint.print();
    WinPrint.close();
}
function GetThisBrowserType() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.indexOf("opera") != -1) {
        browserName = "opera";
    } else if (ua.indexOf("msie") != -1) {
        browserName = "msie";
    } else if (ua.indexOf("safari") != -1) {
        browserName = "safari";
    } else if (ua.indexOf("mozilla") != -1) {
        if (ua.indexOf("firefox") != -1) {
            browserName = "firefox";
        } else {
            browserName = "mozilla";
        }
    }
}
function Fleets_AbandonSession() {
    definitelyClose();
}
function Fleets_CloseSession() {
    document.getElementById("loading").className = "loading-invisible";
    //document.body.style.cursor = "default";
    document.getElementById("Fleets_Message_Container_Timeout").style.display = "inline";
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("codebase/imgs/");
    MsgWin.setSkin("dhx_skyblue");
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 365, 135);
    //MsgWin.window("DisplayMessage").center();
    MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();
    DisplayMsgWin.setText("MS Track Web 8 Session Timeout.");
    DisplayMsgWin.attachObject("Fleets_Message_Container_Timeout");
    DisplayMsgWin.centerOnScreen();
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();
}
function RefreshPage() {                // Page RefreshTimer
    if (IsPlottingQuickHistory === false) {
        try {
            //createCookie("MSTW_NoClick", true, 1);
            document.getElementById("btnRefresh").click();
            resetTimeout();
        }
        catch (Error) {
            window.alert(Error.message);
        }
    }

}
function OLD_RefreshPage() {
    if (readCookie("MSTW8_ClientTimeout") !== "") {
        var nextTimeout = new Date(readCookie("MSTW8_ClientTimeout"));
        var currentTime = new Date();
        if (nextTimeout - currentTime > 0) {
            if (RefreshPage != 999999) {
                if (IsPlottingQuickHistory === false) {
                    try {
                        //createCookie("MSTW_NoClick", true, 1);
                        document.getElementById("btnRefresh").click();
                        resetTimeout();
                    }
                    catch (Error) {
                        window.alert(Error.message);
                    }
                }
                PageRefreshTimer = document.getElementById("hiddenPageRefresh").value * 1000;
                if (browserName == "firefox") {
                    var thisTimeOut = this;
                    setTimeout(function () { thisTimeOut.RefreshPage(); }, parseInt(PageRefreshTimer, 10), thisTimeOut);
                } else {
                    setTimeout("RefreshPage()", parseInt(PageRefreshTimer, 10));
                }
                document.getElementById("lblRefreshTimer").innerHTML = PageRefreshTimer;
            }
        }
    }
}
function sleep(delay) {
    /**
    * Delay for a number of milliseconds
    */
    var start = new Date().getTime();
    while (new Date().getTime() < start + delay);
}
function LoadMapOptions() {
    try {
        if (document.getElementById("hiddenMapOptions").value !== '') {
            var strMapOptions = document.getElementById("hiddenMapOptions").value;
            var tmpMapOptions = strMapOptions.split('|');
            if (tmpMapOptions[0] == "0") {
                IsDisplayingVehicleLabels = false;
            }
            if (MapEng == "Google_Maps") {
                if (tmpMapOptions[1] == "1") {
                    ShowTraffic = true;
                }
            }
            if (tmpMapOptions[2] == "1") {
                IsDisplayingGeofence = true;
            }
            fixedMapBounds = tmpMapOptions[3];
            fixedMapZoom = tmpMapOptions[4];
            if (tmpMapOptions[5] == "1") {
                IsDisplayingSites = true;
            }
            if (tmpMapOptions[6] == "1") {
                IsDisplayingSiteLabels = true;
            }
        }
    } catch (e) {

    }
}
function waitPreloadPage() { //DOM
    document.getElementById("loading").className = "loading-visible";
    MapEng = document.getElementById("hiddenMapEng").value;
    UseIcons = document.getElementById("hiddenUseIcons").value;
    SkinColor = document.getElementById("hiddenSkinColor").value;
    GetThisBrowserType();
    if (document.getElementById("hiddenAllow_Google").value == "True" && MapEng == "Google_Maps") {
        createGoogleScript();
    }
    else {
        waitPreloadPage2();
    }
}
function waitPreloadPage2() {
    if (self.innerHeight) {
        screen_avail_height = self.innerHeight;
        screen_avail_width = self.innerWidth;
    }
    else if (document.documentElement && document.documentElement.clientHeight) {
        screen_avail_height = document.documentElement.clientHeight;
        screen_avail_width = document.documentElement.clientWidth;
    }
    else if (document.body) {
        screen_avail_height = document.body.clientHeight;
        screen_avail_width = document.body.clientWidth;
    }
    SetupFleetLayout();
    initFleetsTabbar();
    LoadVehicleDetailsGrid();
    LoadMapOptions();
    dhxLayout.attachEvent("onPanelResizeFinish", function () {
        eraseCookie("MSTW8_FleetLayout");
        var a_height = dhxLayout.cells("a").getHeight();
        var a1_height = dhxLayout1.cells("a").getHeight();
        var a_width = dhxLayout.cells("a").getWidth();
        var b_height = dhxLayout.cells("b").getHeight();
        var b_width = dhxLayout.cells("b").getWidth();
        var c_height = dhxLayout.cells("c").getHeight();
        var c_width = dhxLayout.cells("c").getWidth();
        var FleetPanels = a_height + "," + a_width + "," + b_height + "," + b_width + "," + c_height + "," + c_width + "," + a1_height;
        createCookie("MSTW8_FleetLayout", FleetPanels, 30);
    });

    dhxLayout1.attachEvent("onPanelResizeFinish", function () {
        eraseCookie("MSTW8_FleetLayout");
        var a_height = dhxLayout.cells("a").getHeight();
        var a1_height = dhxLayout1.cells("a").getHeight();
        var a_width = dhxLayout.cells("a").getWidth();
        var b_height = dhxLayout.cells("b").getHeight();
        var b_width = dhxLayout.cells("b").getWidth();
        var c_height = dhxLayout.cells("c").getHeight();
        var c_width = dhxLayout.cells("c").getWidth();
        var FleetPanels = a_height + "," + a_width + "," + b_height + "," + b_width + "," + c_height + "," + c_width + "," + a1_height;
        createCookie("MSTW8_FleetLayout", FleetPanels, 30);
    });
    if (document.getElementById) {
        PageRefreshTimer = document.getElementById("hiddenPageRefresh").value * 1000;
        if (browserName == "firefox") {
            var thisObj = this;
            setTimeout(function () { thisObj.RefreshPage(); }, 10, thisObj);
        } else {
            setTimeout("RefreshPage()", 10);
        }
        document.getElementById("hiddenMSTDisplayGeofence").value = false;
    }

    document.getElementById("Fleet_Parent").style.visibility = "visible";
    SetupMapContextMenu();
    distunits = document.getElementById("hiddenDistUnits").value;
    autoLocate = $("#hiddenAutoLocate").val();
    connectLineColour = $("#hiddenLocateLineColour").val();
    connectLineWeight = $("#hiddenLocateLineWeight").val();
    connectLineOpacity = $("#hiddenLocateLineOpacity").val();
    SiteLabelColour = $("#hiddenSiteLabelColour").val();
    $('#Fleet_Parent').mousemove(function (e) {
        currentCusorY = e.pageY;
    });

    return;
}
function SetupMapContextMenu() {
    try {
        map_contextMenu = new dhtmlXMenuObject();
        map_contextMenu = new dhtmlXMenuObject(null, "dhx_blue");
        map_contextMenu.setImagePath("../../../dhtmlxMenu/codebase/imgs/");
        map_contextMenu.setIconsPath("../images/");
        map_contextMenu.renderAsContextMenu();
        map_contextMenu.setOpenMode("web");
        map_contextMenu.attachEvent("onClick", ShowMapContextMenu);
        map_contextMenu.attachEvent("onCheckboxClick", map_contextMenuCheckboxClicked);
        clientID = document.getElementById("hiddenClientID").value;
        var msgGrid_xmlFile = "xmlFiles/" + clientID + "_MapcontextMenu.xml?etc=" + new Date().getTime();
        map_contextMenu.loadXML(msgGrid_xmlFile);
        //map_contextMenu.addContextZone("myMap");
    }
    catch (Error) {
        window.alert("SetMapContextMenu Error 816: " + Error.message);
    }
}
function SetupMapSiteContextMenu() {
    try {
        map_SiteContextMenu = new dhtmlXMenuObject();
        map_SiteContextMenu = new dhtmlXMenuObject(null, "dhx_blue");
        map_SiteContextMenu.setImagePath("../../../dhtmlxMenu/codebase/imgs/");
        map_SiteContextMenu.setIconsPath("../images/");
        map_SiteContextMenu.renderAsContextMenu();
        map_SiteContextMenu.setOpenMode("web");
        map_SiteContextMenu.attachEvent("onClick", ShowMapSiteContextMenu);
        clientID = document.getElementById("hiddenClientID").value;
        var Grid_xmlFile = "xmlFiles/" + clientID + "_MapSiteContextMenu.xml?etc=" + new Date().getTime();
        map_SiteContextMenu.loadXML(Grid_xmlFile);
        //map_SiteContextMenu.addContextZone("myMap");
    }
    catch (Error) {
        window.alert("SetMapContextMenu Error 834: " + Error.message);
    }
}
function cell_Collapse() {
    var state = dhxLayout.cells("c").isCollapsed();
    eraseCookie("MSTW8_FleetDetailExpanded");
    createCookie("MSTW8_FleetDetailExpanded", state, 30);
}
function cell_Expand() {
    var state = dhxLayout.cells("c").isCollapsed();
    eraseCookie("MSTW8_FleetDetailExpanded");
    createCookie("MSTW8_FleetDetailExpanded", state, 30);
}
function SetupFleetLayout() {
    //eraseCookie("MSTW8_FleetLayout");
    dhxLayout = new dhtmlXLayoutObject("Fleet_Parent", "3J", "dhx_blue");
    var tmpstr = readCookie("MSTW8_FleetLayout");
    var state = readCookie("MSTW8_FleetDetailExpanded");
    if (tmpstr !== "") {
        var FleetLayoutSizes = readCookie("MSTW8_FleetLayout").split(",");
        dhxLayout.cells("a").setHeight(FleetLayoutSizes[0]);
        dhxLayout.cells("a").setWidth(FleetLayoutSizes[1]);
        dhxLayout.cells("a").hideHeader();
        dhxLayout.cells("b").hideHeader();
        dhxLayout.cells("b").setHeight(FleetLayoutSizes[2]);
        dhxLayout.cells("b").setWidth(FleetLayoutSizes[3]);
        dhxLayout.cells("c").setHeight(FleetLayoutSizes[4]);
        dhxLayout.cells("c").setWidth(FleetLayoutSizes[5]);

        dhxLayout.cells("c").setText("");       //document.getElementById("hiddenVehicleDetailHeader").value);
        if (state == "true") {
            dhxLayout.cells("c").collapse();
        }
        else {
            dhxLayout.cells("c").expand();
        }
        dhxLayout.attachEvent("onCollapse", cell_Collapse);
        dhxLayout.attachEvent("onExpand", cell_Expand);
        dhxLayout1 = new dhtmlXLayoutObject(dhxLayout.cells("a"), "2E");
        dhxLayout1.cells("a").attachObject("TableLeft_Top");
        dhxLayout1.cells("a").setWidth(FleetLayoutSizes[0]);
        dhxLayout1.cells("a").setHeight(FleetLayoutSizes[6]);
        dhxLayout1.cells("a").hideHeader();
        dhxLayout1.cells("b").setWidth(FleetLayoutSizes[0]);
        dhxLayout1.cells("b").hideHeader();
        dhxLayout.attachEvent("onCollapse", cell_Collapse);
        dhxLayout.attachEvent("onExpand", cell_Expand);
    }
    else {
        dhxLayout.cells("a").setWidth(screen_avail_width * 0.2);
        dhxLayout.cells("a").setHeight(screen_avail_height * 0.5);
        dhxLayout.cells("a").hideHeader();
        dhxLayout.cells("b").hideHeader();
        dhxLayout.cells("b").setWidth(screen_avail_width * 0.8);
        dhxLayout.cells("b").setHeight(screen_avail_height * 0.9);
        dhxLayout.cells("c").setWidth(screen_avail_width * 0.2);
        dhxLayout.cells("c").setHeight(screen_avail_height * 0.5);
        dhxLayout.cells("c").setText("");       //document.getElementById("hiddenVehicleDetailHeader").value);
        if (state == "true") {
            dhxLayout.cells("c").collapse();
        }
        else {
            dhxLayout.cells("c").expand();
        }
        dhxLayout.attachEvent("onCollapse", cell_Collapse);
        dhxLayout.attachEvent("onExpand", cell_Expand);
        dhxLayout1 = new dhtmlXLayoutObject(dhxLayout.cells("a"), "2E");
        dhxLayout1.cells("a").attachObject("TableLeft_Top");
        dhxLayout1.cells("a").setWidth(screen_avail_width * 0.2);
        dhxLayout1.cells("a").setHeight(screen_avail_height * 0.02);
        dhxLayout1.cells("a").hideHeader();
        //dhxLayout1.cells("a").fixSize(true, true);
        dhxLayout1.cells("b").setWidth(screen_avail_width * 0.2);
        dhxLayout1.cells("b").setHeight(screen_avail_height * 0.5);
        dhxLayout1.cells("b").hideHeader();
    }
}

function ShowGeofences(state) {
    if (MapEng == "MSTMap") {
        document.getElementById("hiddenMSTDisplayGeofence").value = true;
        //document.getElementById("hiddenButtonMSTDisplayGeoFences").click();
    }
    switch (state) {
        case true:
            name.src = document.getElementById("hiddenHideGeofenceButton").value;
            document.getElementById("loading").className = "loading-visible";
            document.getElementById("loadingLabel").innerHTML = "Loading Geofences .... please wait";
            IsDisplayingGeofence = true;
            break;
        case false:
            name.src = document.getElementById("hiddenShowGeofenceButton").value;
            document.getElementById("hiddenMSTDisplayGeofence").value = false;
            document.getElementById("loading").className = "loading-visible";
            document.getElementById("loadingLabel").innerHTML = "Clearing Geofences .... please wait";
            IsDisplayingGeofence = false;
            break;

    }
    if (MapEng == "MS_VirtualEarth" || MapEng == "Google_Maps") {
        CreateGeofenceLayer(state);
    }
    document.getElementById("hiddenIsDisplayingGeofences").value = IsDisplayingGeofence;
}
function ShowSites(state) {
    if (MapEng == "MSTMap") {
        document.getElementById("hiddenMSTDisplaySites").value = true;
        //document.getElementById("hiddenButtonMSTDisplayGeoFences").click();
    }
    if (MapEng == "MS_VirtualEarth" || MapEng == "Google_Maps") {
        CreateSitesLayer(state);
    }
    document.getElementById("hiddenIsDisplayingSites").value = IsDisplayingSites;

}

function RefreshEvImageOverChange(name, direction) {
    var tabStrip = eo_GetObject('TabStrip2');
    tabStrip.setSelectedIndex(0);
    switch (direction) {
        case 'in':
            //loadCurrentVehicles();
            break;
        case 'out':
            name.src = "Buttons/MSTW_Refresh.gif";
            document.getElementById("btnShowGeofences").style.display = 'visible';
            break;
        default:
            break;
    }
}
function RedisplayButtons() {
    //document.getElementById("btnRefresh").src = "Buttons/MSTW_Refresh.gif";
    document.getElementById("btnShowGeofences").style.visibility = 'visible';

}
function MST_Map_Loaded() {
    document.getElementById("loading").className = "loading-invisible";
}
function loadQuickHistory() {
    try {
        IsQuickHistory = true;
        var tmpInArrayStr = document.getElementById('hiddenQuickHistoryArray').value;
        BuildQuickHistoryArray(tmpInArrayStr);
        PlotQuickHistory();
        DisplayVehicleInfo(document.getElementById('hiddenSelectedVehicle').value);
    }
    catch (Error) {
        window.alert("Error 104: " + Error.message);
    }
}
function ButtonRefresh() {
    selectedVehicles = tvVehicles.getAllChecked();
    document.getElementById("loading").className = "loading-visible";
    document.getElementById("loadingLabel").innerHTML = document.getElementById("hiddenRefreshMessage").value;
    // Set cursor to hourglass
    //document.body.style.cursor = "wait";
    if (dgVehicles) {
        dgVehicles.clearAll();
    }
    if (MapEng == "MS_VirtualEarth") {
        clearSiteLayers();
    }
    else {
        if (MapEng == "Google_Maps") {
            try {
                clearSiteOverlays();
            } catch (e) {
            }
        }
    }
    IsRefresh = "True";
    lastActiveTab = fleets_TabBar.getActiveTab();
}
function loadCurrentVehicles() {
    try {
        if (AllowMessaging == "True") {
            try {
                dgMessages.clearAll();
            }
            catch (Error) {
            }
        }

        Fleet_Code = document.getElementById("hidden_FleetCode").value;
        useDrivers = document.getElementById("hiddenUseDrivers").value;
        document.getElementById("hiddenMapRequest").value = "AllVeh";
        var tmpInArrayStr = document.getElementById("hiddenCurrentVehArray").value;
        BuildCurrentVehiclesArray(tmpInArrayStr);
        DisplayOption = document.getElementById("hiddenDisplayOption").value;
        doInitGrid();
        clientID = document.getElementById("hiddenClientID").value;
        var vehicle_xmlFile = "xmlFiles/" + clientID + "_vehiclegrid.xml?etc=" + new Date().getTime();
        var message_xmlFile = "xmlFiles/" + clientID + "_messagegrid.xml?etc=" + new Date().getTime();
        AllowMessaging = document.getElementById('hiddenAllowMessaging').value;
        dgVehicles.loadXML(vehicle_xmlFile);

        if (DisplayOption == "ByReg") {
            dgVehicles.setColumnHidden(1, true);
            dgVehicles.setColumnHidden(0, false);
            if (AllowMessaging == "True") {
                MsgWin = new dhtmlXWindows();
                dgMessages.setColumnHidden(1, true);
                dgMessages.setColumnHidden(0, false);
            }
        }
        else if (DisplayOption == "ByDesc") {
            dgVehicles.setColumnHidden(1, false);
            dgVehicles.setColumnHidden(0, true);
            if (AllowMessaging == "True") {
                MsgWin = new dhtmlXWindows();
                dgMessages.setColumnHidden(1, false);
                dgMessages.setColumnHidden(0, true);
            }
        }
        else if (DisplayOption == "BothByReg") {
            dgVehicles.setColumnHidden(1, false);
            dgVehicles.setColumnHidden(0, false);
            if (AllowMessaging == "True") {
                MsgWin = new dhtmlXWindows();
                dgMessages.setColumnHidden(1, false);
                dgMessages.setColumnHidden(0, false);
            }
        }
        else if (DisplayOption == "BothByDesc") {
            dgVehicles.setColumnHidden(1, false);
            dgVehicles.setColumnHidden(0, false);
            if (AllowMessaging == "True") {
                MsgWin = new dhtmlXWindows();
                dgMessages.setColumnHidden(1, false);
                dgMessages.setColumnHidden(0, false);
            }
        }
        if (useDrivers == "True") {
            dgVehicles.setColumnHidden(7, false);
        }
        else {
            dgVehicles.setColumnHidden(7, true);
        }


        if (AllowMessaging == "True") {
            dgMessages.loadXML(message_xmlFile);
            var msgLabel = document.getElementById("hiddenMessagesLabel").value;
            if (document.getElementById("hiddenTotalUnReadMessages").value > 0) {
                fleets_TabBar.setLabel("Messaging", msgLabel + "<span style='color:blue;font-weight: bold;'> (" + document.getElementById("hiddenTotalUnReadMessages").value + ")</span>");
                var sound_file_url = "Images/UnreadMessages.WAV";
                document.getElementById("sound_element").innerHTML = "<embed src='" + sound_file_url + "' hidden=true autostart=true loop=false>";
            }
            else {
                fleets_TabBar.setLabel("Messaging", msgLabel);
            }
        }
        //doInitTree();

        document.getElementById("TableLeft_Top").style.visibility = "visible";
        // Reset cursor
        document.body.style.cursor = "default";
    }
    catch (Error) {
        //window.alert("Error 743:" + Error.message);
        //window.location.reload();
    }
} IsSetupGeofenceGrid = true;

function UpdateMessageGrid() {
    if (AllowMessaging == "True") {
        dgMessages.clearAll();
        clientID = document.getElementById("hiddenClientID").value;
        var message_xmlFile = "xmlFiles/" + clientID + "_messagegrid.xml?etc=" + new Date().getTime();
        dgMessages.loadXML(message_xmlFile);
        var msgLabel = document.getElementById("hiddenMessagesLabel").value;
        if (document.getElementById("hiddenTotalUnReadMessages").value > 0) {
            fleets_TabBar.setLabel("Messaging", msgLabel + "<span style='color:blue;font-weight: bold;'> (" + document.getElementById("hiddenTotalUnReadMessages").value + ")</span>");
            var sound_file_url = "Images/UnreadMessages.WAV";
            document.getElementById("sound_element").innerHTML = "<embed src='" + sound_file_url + "' hidden=true autostart=true loop=false>";
        }
        else {
            fleets_TabBar.setLabel("Messaging", msgLabel);
        }
    }

}
function PlaySound(soundObj) {
    var sound = document.getElementById(soundObj);
    sound.Play();
}

function LoadNewGeofenceGrid() {
    dgNewGeofence_Fleets = null;
    dgNewGeofence_Fleets = new dhtmlXGridObject("New_Geofence_Container_1");
    dgNewGeofence_Fleets.setImagePath("codebase/imgs/");
    dgNewGeofence_Fleets.setHeader(document.getElementById("hiddenGeofenceGridHeader").value);
    dgNewGeofence_Fleets.setInitWidths("245,230");
    dgNewGeofence_Fleets.setNumberFormat("000", 1);
    dgNewGeofence_Fleets.enableTooltips("true");
    dgNewGeofence_Fleets.setColAlign("left,center");
    dgNewGeofence_Fleets.setSkin(SkinColor);
    dgNewGeofence_Fleets.enableLightMouseNavigation("true");
    dgNewGeofence_Fleets.attachEvent("onCellChanged", CheckSelectedValue);
    dgNewGeofence_Fleets.init();

    clientID = document.getElementById("hiddenClientID").value;
    var NewGeofence_xmlFile = "xmlFiles/" + clientID + "_NewGeofence.xml?etc=" + new Date().getTime();
    dgNewGeofence_Fleets.loadXML(NewGeofence_xmlFile, SetUpGeofenceGridCells);
}
function SetUpGeofenceGridCells() {
    try {
        dgNewGeofence_Fleets.cells(1, 1).setValue("");
        dgNewGeofence_Fleets.cells(2, 1).setValue("");
        dgNewGeofence_Fleets.cells(3, 1).setValue(1);
        dgNewGeofence_Fleets.cells(4, 1).setValue(newLat);
        dgNewGeofence_Fleets.cells(5, 1).setValue(newLong);
        dgNewGeofence_Fleets.cells(6, 1).setValue(1);
        dgNewGeofence_Fleets.cells(7, 1).setValue(1);
        dgNewGeofence_Fleets.cells(8, 1).setValue("");
        dgNewGeofence_Fleets.selectCell(0, 1);
        dgNewGeofence_Fleets.editCell();
        dgNewGeofence_Fleets.setRowHidden(8, "true");
    }
    catch (Error) {
        alert(Error.message);
        // window.alert("Unable to verify latitude and Longitude. Please press Refresh and recreate new Geofence");
    }
}
function CheckSelectedValue(rowid, cellid, newvalue) {
    if (rowid == 6 && cellid == 1) {
        if (newvalue == 11) {
            GetGeoFenceValue();
        }
    }
    if (rowid == 7 && cellid == 1) {
        if (newvalue == 3) {
            window.setTimeout(function () {
                dgNewGeofence_Fleets.setRowHidden(8, false);   //make the row visib;e
                dgNewGeofence_Fleets.selectCell(7, 1); //rowid, collid - select the cell
                dgNewGeofence_Fleets.editCell();        // make edit mode.
            }, 1);
        }
    }
}
function LoadNewSiteGrid() {
    try {
        fleets_TabBar.showTab("FT_Site");
        fleets_TabBar.setTabActive("FT_Site");
        dgNewSite_Fleets = new dhtmlXGridObject("New_Site_Container_1");
        dgNewSite_Fleets.setImagePath("codebase/imgs/");
        dgNewSite_Fleets.setHeader(document.getElementById("hiddenGeofenceGridHeader").value);
        dgNewSite_Fleets.setInitWidths("245,230");
        dgNewSite_Fleets.setNumberFormat("000", 1);
        dgNewSite_Fleets.enableTooltips("true");
        dgNewSite_Fleets.setColAlign("left,center");
        dgNewSite_Fleets.enableEditEvents(false, true);
        dgNewSite_Fleets.setSkin("modern");
        dgNewSite_Fleets.enableLightMouseNavigation("true");
        //dgNewSite_Fleets.attachEvent("onCellChanged", CheckSelectedValue);
        dgNewSite_Fleets.init();
        dgNewSite_Fleets.preventIECaching(true);
        clientID = document.getElementById("hiddenClientID").value;
        var NewSite_xmlFile = "xmlFiles/" + clientID + "_NewSite.xml?etc=" + new Date().getTime();
        dgNewSite_Fleets.loadXML(NewSite_xmlFile, SetUpSiteGridCells);
    } catch (e) {
        alert(e.Message);
    }
}

function SetUpSiteGridCells() {
    try {
        fleets_TabBar.showTab("FT_Site");
        fleets_TabBar.setTabActive("FT_Site");
        dgNewSite_Fleets.cells(1, 1).setValue(newSiteName);
        //dgNewSite_Fleets.cells(2, 1).setValue(1);
        dgNewSite_Fleets.cells(3, 1).setValue("");
        dgNewSite_Fleets.cells(4, 1).setValue("");
        dgNewSite_Fleets.cells(5, 1).setValue("");
        //dgNewSite_Fleets.cells(6, 1).setValue("");
        dgNewSite_Fleets.cells(7, 1).setValue(newLat);
        dgNewSite_Fleets.cells(8, 1).setValue(newLong);
        dgNewSite_Fleets.selectCell(0, 1);
        dgNewSite_Fleets.editCell();
    }
    catch (Error) {
        alert(Error.message);
        // window.alert("Unable to verify latitude and Longitude. Please press Refresh and recreate new Site");
    }
}
function SaveSite() {
    dgNewSite_Fleets.setCSVDelimiter("|");
    dgNewSite_Fleets.csv.row = "~";
    dgNewSite_Fleets.enableCSVAutoID(true);
    document.getElementById("hiddenNewSite").value = dgNewSite_Fleets.serializeToCSV();
    document.getElementById("hiddenButtonSaveSite").click();
    if (document.getElementById("chkCreateGeoFromSite").checked == true) {
        fleets_TabBar.hideTab("FT_Site");
        CreateNewGeofence(newLat, newLong, dgNewSite_Fleets.cells(2, 1).getValue());
    }
    else {
        fleets_TabBar.hideTab("FT_Site");
        fleets_TabBar.setTabActive(lastActiveTab);
    }
}
function CancelSites() {
    CreatingSite = false;
    fleets_TabBar.setTabActive(lastActiveTab);
}
function RefreshSites() {

    ShowSites(IsDisplayingSites);

}
function VE_Map_MouseHandler(e) {
    thisLatLong = map.PixelToLatLong(new VEPixel(e.mapX, e.mapY)).toString();
    if (e.rightMouseButton && e.elementID) {
        var tmpArray = map.GetShapeByID(e.elementID).Title.split(':');
        if (tmpArray[0] == "Site") {
            SelectedSite = tmpArray[1];
            ShowClosestWindow();
            //map_SiteContextMenu.showContextMenu(e.clientX - 20, e.clientY - 20);
        }
        else {
            map_contextMenu.showContextMenu(e.clientX - 20, e.clientY - 20);
        }
    }
    else {
        if (e.rightMouseButton && !e.elementID) {
            map_contextMenu.showContextMenu(e.clientX - 20, e.clientY - 20);
        }
    }
}
function cMenu_PollVehicle() {
    document.getElementById("btnPollFromVehGrid").click();
}
function Poll_Success(thisTerminal) {

    document.getElementById("Poll_Success_Container").style.display = "inline";
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("codebase/imgs/");
    MsgWin.setSkin("dhx_skyblue");
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 175, 100);
    MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();
    DisplayMsgWin.setText("<div style='text-align:center; padding-left:10px;'>" + thisTerminal + "</div>");
    DisplayMsgWin.attachObject("Poll_Success_Container");
    DisplayMsgWin.centerOnScreen();
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();
}
function Close_Poll_Success_Container() {
    DisplayMsgWin.setModal(false);
    DisplayMsgWin.hide();
    DisplayMsgWin = null;
}
function Poll_Failed(thisTerminal) {
    document.getElementById("Poll_Failed_Container").style.display = "inline";
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("../../../dhtmlxMenu/codebase/imgs/");
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 225, 100);
    MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();
    DisplayMsgWin.setText("<div style='text-align:left; padding-left:20px;'>" + thisTerminal + "</div>");
    DisplayMsgWin.attachObject("Poll_Failed_Container");
    DisplayMsgWin.centerOnScreen();
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();
}
function Close_Poll_Failed_Container() {
    DisplayMsgWin.setModal(false);
    DisplayMsgWin.hide();
    DisplayMsgWin = null;
}
function cMenu_QuickHistory() {
    document.getElementById("loading").className = "loading-visible";
    var tmpMsg = document.getElementById("hiddenPlottingQuickHistory").value;
    document.getElementById("loadingLabel").innerHTML = tmpMsg;
    IsPlottingQuickHistory = true;
    document.getElementById("hiddenMapRequest").value = "QuickHistory";
    document.getElementById("hiddenButtonQuickHistory").click();
}
function cMenu_CreateSite() {
    var thisVehicle = document.getElementById('hiddenSelectedVehicle').value;
    for (var i = 0; i < CurrentVehicleArray.length; i++) {
        if (CurrentVehicleArray[i][0] == thisVehicle || CurrentVehicleArray[i][1] == thisVehicle) {
            var thisLat = CurrentVehicleArray[i][2];
            var thisLong = CurrentVehicleArray[i][3];
            CreateNewSite(thisLat, thisLong, null);
        }
    }
}
function cMenu_CreateGeoFence() {
    var thisVehicle = document.getElementById('hiddenSelectedVehicle').value;
    for (var i = 0; i < CurrentVehicleArray.length; i++) {
        if (CurrentVehicleArray[i][0] == thisVehicle || CurrentVehicleArray[i][1] == thisVehicle) {
            var thisLat = CurrentVehicleArray[i][2];
            var thisLong = CurrentVehicleArray[i][3];
            var thisLoc = CurrentVehicleArray[i][6];
            document.getElementById("hiddenNewGeofenceRadius").value = 0;
            CreateNewGeofence(thisLat, thisLong, thisLoc);
        }
    }
}
function BuildQuickHistoryArray(tmpArrayStr) {
    var tmpArray = tmpArrayStr.split('~');
    QuickHistoryArray = [];
    for (var i = 0; i < tmpArray.length; i++) {
        QuickHistoryArray[i] = tmpArray[i].split('|');
    }
}
function BuildCurrentVehiclesArray(tmpArrayStr) {
    var tmpArray = tmpArrayStr.split('~');
    CurrentVehicleArray = [];
    for (var i = 0; i < tmpArray.length; i++) {
        CurrentVehicleArray[i] = tmpArray[i].split('|');
    }
}
function BuildArray(tmpArrayStr) {
    try {
        var tmpArray = tmpArrayStr.split('~');
        VehicleArray = [];
        var k = 0;
        for (var i = 0; i < tmpArray.length; i++) {
            VehicleArray[k] = tmpArray[i].split('|');
            if (VehicleArray[k][2] !== "") {
                k++;
            }
        }
    }
    catch (Error) {
        //window.alert("Build Array Error: " + Error.message);
    }
}
function AddPin(locs) {
    pinid = 0;
    VehicleShapeLayer = new VEShapeLayer();
    VehiclePinLayer = new VEShapeLayer();
    icon = 'Images/LorryGreen.png';
    for (var i = 0; i < locs.length; i++) {
        var sessIgnStat = VehicleArray[i][7];
        var sessHeading = parseInt(VehicleArray[i][8], 10);
        var sessEvent = VehicleArray[i][9];
        var sessFolder = "Images/Vehicles/";
        var sessOdo = VehicleArray[i][11];
        //var sessOdo = str.substring(0, str.length - 1);
        var sessVehicleType = VehicleArray[i][10];
        //if (sessVehicleType != "Bus" && sessVehicleType != 'Car' && sessVehicleType != 'Lorry' && sessVehicleType != 'Truck') {
        //    sessVehicleType = "Car";
        //}
        if (document.getElementById("hiddenUseFleetColours").value == "True" && Not_Fleet_Colours == false) {
            icon = "Images/FleetColours/Lorry" + VehicleArray[i][13] + ".png";
        }
        else if (sessVehicleType != "") {
            switch (sessIgnStat) {
                case "OFF":
                    icon = sessFolder + sessVehicleType + "RedEast.png";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolder + sessVehicleType + "RedEast.png";
                            break;
                        case 2:
                            icon = sessFolder + sessVehicleType + "RedEast.png";
                            break;
                        case 3:
                            icon = sessFolder + sessVehicleType + "RedEast.png";
                            break;
                        case 4:
                            icon = sessFolder + sessVehicleType + "RedEast.png";
                            break;
                        case 5:
                            icon = sessFolder + sessVehicleType + "RedWest.png";
                            break;
                        case 6:
                            icon = sessFolder + sessVehicleType + "RedWest.png";
                            break;
                        case 7:
                            icon = sessFolder + sessVehicleType + "RedWest.png";
                            break;
                        case 8:
                            icon = sessFolder + sessVehicleType + "RedWest.png";
                            break;
                        default:
                            icon = sessFolder + sessVehicleType + "RedEast.png";
                            break;
                    }
                    break;

                case "ON":
                    icon = sessFolder + sessVehicleType + "GreenEast.png";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolder + sessVehicleType + "GreenEast.png";
                            break;
                        case 2:
                            icon = sessFolder + sessVehicleType + "GreenEast.png";
                            break;
                        case 3:
                            icon = sessFolder + sessVehicleType + "GreenEast.png";
                            break;
                        case 4:
                            icon = sessFolder + sessVehicleType + "GreenEast.png";
                            break;
                        case 5:
                            icon = sessFolder + sessVehicleType + "GreenWest.png";
                            break;
                        case 6:
                            icon = sessFolder + sessVehicleType + "GreenWest.png";
                            break;
                        case 7:
                            icon = sessFolder + sessVehicleType + "GreenWest.png";
                            break;
                        case 8:
                            icon = sessFolder + sessVehicleType + "GreenWest.png";
                            break;
                        default:
                            icon = sessFolder + sessVehicleType + "GreenEast.png";
                            break;
                    }
                    break;
                default:
                    icon = sessFolder + sessVehicleType + "BlueEast.png";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolder + sessVehicleType + "BlueEast.png";
                            break;
                        case 2:
                            icon = sessFolder + sessVehicleType + "BlueEast.png";
                            break;
                        case 3:
                            icon = sessFolder + sessVehicleType + "BlueEast.png";
                            break;
                        case 4:
                            icon = sessFolder + sessVehicleType + "BlueEast.png";
                            break;
                        case 5:
                            icon = sessFolder + sessVehicleType + "BlueWest.png";
                            break;
                        case 6:
                            icon = sessFolder + sessVehicleType + "BlueWest.png";
                            break;
                        case 7:
                            icon = sessFolder + sessVehicleType + "BlueWest.png";
                            break;
                        case 8:
                            icon = sessFolder + sessVehicleType + "BlueWest.png";
                            break;
                        default:
                            icon = sessFolder + sessVehicleType + "BlueEast.png";
                            break;
                    }
            }
        }

        else if (sessEvent != document.getElementById("hiddenTranslatePosition").value) {
            switch (sessIgnStat) {

                case "OFF":
                    icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                            break;
                        case 2:
                            icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                            break;
                        case 3:
                            icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                            break;
                        case 4:
                            icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                            break;
                        case 5:
                            icon = sessFolder + sessVehicleType + "RedEventWest.gif";
                            break;
                        case 6:
                            icon = sessFolder + sessVehicleType + "RedEventWest.gif";
                            break;
                        case 7:
                            icon = sessFolder + sessVehicleType + "RedEventWest.gif";
                            break;
                        case 8:
                            icon = sessFolder + sessVehicleType + "RedEventWest.gif";
                            break;
                        default:
                            icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                            break;
                    }
                    break;
                case "ON":
                    icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                            break;
                        case 2:
                            icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                            break;
                        case 3:
                            icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                            break;
                        case 4:
                            icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                            break;
                        case 5:
                            icon = sessFolder + sessVehicleType + "GreenEventWest.gif";
                            break;
                        case 6:
                            icon = sessFolder + sessVehicleType + "GreenEventWest.gif";
                            break;
                        case 7:
                            icon = sessFolder + sessVehicleType + "GreenEventWest.gif";
                            break;
                        case 8:
                            icon = sessFolder + sessVehicleType + "GreenEventWest.gif";
                            break;
                        default:
                            icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                            break;
                    }
                    break;
                default:
                    icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                            break;
                        case 2:
                            icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                            break;
                        case 3:
                            icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                            break;
                        case 4:
                            icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                            break;
                        case 5:
                            icon = sessFolder + sessVehicleType + "BlueEventWest.gif";
                            break;
                        case 6:
                            icon = sessFolder + sessVehicleType + "BlueEventWest.gif";
                            break;
                        case 7:
                            icon = sessFolder + sessVehicleType + "BlueEventWest.gif";
                            break;
                        case 8:
                            icon = sessFolder + sessVehicleType + "BlueEventWest.gif";
                            break;
                        default:
                            icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                            break;
                    }
            }
        }
        if (sessVehicleType == "") {
            switch (sessIgnStat) {
                case "OFF":
                    icon = sessFolder + "red.gif";
                    break;
                case "ON":
                    icon = sessFolder + "green.gif";
                    break;
                default:
                    icon = sessFolder + "purple.gif";
                    break;
            }
        }
        if (sessEvent == document.getElementById("hiddenTranslatePosition").value) {
            sessEvent = "";
        }
        var sessDetails = 'Date: ' + VehicleArray[i][4] + '<br>Location: ' + VehicleArray[i][6] + '<br>Speed:   ' + VehicleArray[i][5] + '<br>Odometer:   ' + sessOdo + '<br><B>' + sessEvent + '</B>';
        if (useDrivers == "True") {
            sessDetails = 'Date: ' + VehicleArray[i][4] + '<br>Location: ' + VehicleArray[i][6] + '<br>Speed:   ' + VehicleArray[i][5] + '<br>Odometer:   ' + sessOdo + '<br>Driver:   ' + VehicleArray[i][12] + '<br><B>' + sessEvent + '</B>';
        }

        var thisLabel;
        if (DisplayOption == "ByReg") {
            thisLabel = VehicleArray[i][0];
        }
        else {
            thisLabel = VehicleArray[i][1];
        }
        //var pin = new VEPushpin(pinid, locs[i], icon, thisLabel, sessDetails);
        var pin = new VEShape(VEShapeType.Pushpin, locs[i]);
        pin.SetCustomIcon(icon);
        pin.SetTitle(thisLabel);
        pin.SetDescription(sessDetails);
        var shape = new VEShape(VEShapeType.Pushpin, locs[i]);
        var thisicon;
        if (DisplayOption == "ByReg") {
            thisicon = "<div style='color:#212121;font-family:Tahoma;font-style:italic;font-size:8.25pt;font-weight: bold;background-color:transparent;width:auto;white-space:nowrap;text-indent:2em;padding-top:5;'><label style='background-color:white;border:solid 1px Black;text-decoration:underline;'>" + VehicleArray[i][0] + "</label></div>";
        }
        else {
            thisicon = "<div style='color:#212121;font-family:Tahoma;font-style:italic;font-size:8.25pt;font-weight: bold;background-color:transparent;width:auto;white-space:nowrap;text-indent:2em;padding-top:5;'><label style='background-color:white;border:solid 1px Black;text-decoration:underline;'>" + VehicleArray[i][1] + "</label></div>";
        }
        shape.SetTitle(VehicleArray[i][1]);
        shape.SetDescription(sessDetails);
        shape.SetZIndex(100, 100);
        if (IsDisplayingVehicleLabels == true) {
            shape.SetCustomIcon(thisicon);
            VehicleShapeLayer.AddShape(shape);
        }
        VehiclePinLayer.AddShape(pin);
        pinid++;
    }
    //Delete_VE_Map_Layers();
    //Add_VE_Map_Layers();
    map.AddShapeLayer(VehicleShapeLayer);
    map.AddShapeLayer(VehiclePinLayer);
}
function BuildGeoFenceArray(tmpArrayStr) {
    try {
        var tmpArray = tmpArrayStr.split('~');
        GeofenceArray = [];
        for (var i = 0; i < tmpArray.length - 1; i++) {
            GeofenceArray[i] = tmpArray[i].split(',');
        }
    }
    catch (Error) {
        window.alert("Build GeoFence Array Error: " + Error.message);
    }
}
function QuickHistoryAddPin(quickhistory_locs) {
    pinid = 0;
    VehicleQuickHistoryPinLayer = new VEShapeLayer();
    icon = 'Images/LorryGreen.png';

    for (var i = 0; i < quickhistory_locs.length; i++) {
        var sessIgnStat = VehicleArray[i][7];
        var sessHeading = parseInt(VehicleArray[i][8], 10);
        var sessEvent = VehicleArray[i][9];
        var sessFolder = "Images/Vehicles/";
        var sessFolderArrows = "Images/Arrows/";
        var str = VehicleArray[i][11];
        var sessOdo = str.substring(0, str.length - 1);
        var sessVehicleType = VehicleArray[i][10];
        //if (sessVehicleType != "Bus" && sessVehicleType != 'Car' && sessVehicleType != 'Lorry' && sessVehicleType != 'Truck') {
        //    sessVehicleType = "Car";
        //}
        if (sessVehicleType != "") {
            switch (sessIgnStat) {
                case "OFF":
                    if (i == 1 || i == quickhistory_locs.length) {
                        icon = sessFolder + sessVehicleType + "RedEast.png";
                    }
                    else {
                        icon = sessFolder + "Red_Dot.gif";
                    }
                    break;
                case "ON":
                    icon = sessFolderArrows + "HisGreenNorth.gif";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolderArrows + "HisGreenNorth.gif";
                            break;
                        case 2:
                            icon = sessFolderArrows + "HisGreenNEast.gif";
                            break;
                        case 3:
                            icon = sessFolderArrows + "HisGreenEast.gif";
                            break;
                        case 4:
                            icon = sessFolderArrows + "HisGreenSEast.gif";
                            break;
                        case 5:
                            icon = sessFolderArrows + "HisGreenSouth.gif";
                            break;
                        case 6:
                            icon = sessFolderArrows + "HisGreenSWest.gif";
                            break;
                        case 7:
                            icon = sessFolderArrows + "HisGreenWest.gif";
                            break;
                        case 8:
                            icon = sessFolderArrows + "HisGreenNWest.gif";
                            break;
                        default:
                            icon = sessFolderArrows + sessVehicleType + "HisGreenNorth.png";
                            break;
                    }
                    break;
                default:
                    icon = sessFolderArrows + "HisBlueNorth.gif";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolderArrows + "HisBlueNorth.gif";
                            break;
                        case 2:
                            icon = sessFolderArrows + "HisBlueNEast.gif";
                            break;
                        case 3:
                            icon = sessFolderArrows + "HisBlueEast.gif";
                            break;
                        case 4:
                            icon = sessFolderArrows + "HisBlueSEast.gif";
                            break;
                        case 5:
                            icon = sessFolderArrows + "HisBlueSouth.gif";
                            break;
                        case 6:
                            icon = sessFolderArrows + "HisBlueSWest.gif";
                            break;
                        case 7:
                            icon = sessFolderArrows + "HisBlueWest.gif";
                            break;
                        case 8:
                            icon = sessFolderArrows + "HisBlueNWest.gif";
                            break;
                        default:
                            icon = sessFolderArrows + "HisBlueNorth.gif";
                            break;
                    }
            }
            if (sessEvent != document.getElementById("hiddenTranslatePosition").value) {
                switch (sessIgnStat) {
                    case "OFF":
                        if (sessEvent.toUpperCase() == "IGNITION OFF") {
                            icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                        }
                        else {
                            icon = sessFolder + "Red_Dot_Event.gif";
                        }
                        break;
                    case "ON":
                        if (sessEvent.toUpperCase() == "IGNITION ON") {
                            icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                        }
                        else {
                            icon = sessFolderArrows + "HisGreenEventEast.gif";
                            switch (sessHeading) {
                                case 1:
                                    icon = sessFolderArrows + "HisGreenEventNorth.gif";
                                    break;
                                case 2:
                                    icon = sessFolderArrows + "HisGreenEventNEast.gif";
                                    break;
                                case 3:
                                    icon = sessFolderArrows + "HisGreenEventEast.gif";
                                    break;
                                case 4:
                                    icon = sessFolderArrows + "HisGreenEventSEast.gif";
                                    break;
                                case 5:
                                    icon = sessFolderArrows + "HisGreenEventSouth.gif";
                                    break;
                                case 6:
                                    icon = sessFolderArrows + "HisGreenEventWest.gif";
                                    break;
                                case 7:
                                    icon = sessFolderArrows + "HisGreenEventSWest.gif";
                                    break;
                                case 8:
                                    icon = sessFolderArrows + "HisGreenEventWest.gif";
                                    break;
                                default:
                                    icon = sessFolderArrows + "HisGreenEventNWest.gif";
                                    break;
                            }
                        }
                        break;
                    default:
                        icon = sessFolderArrows + "HisBlueEventNorth.gif";
                        switch (sessHeading) {
                            case 1:
                                icon = sessFolderArrows + "HisBlueEventNEast.gif";
                                break;
                            case 2:
                                icon = sessFolderArrows + "HisBlueEventEast.gif";
                                break;
                            case 3:
                                icon = sessFolderArrows + "HisBlueEventSEast.gif";
                                break;
                            case 4:
                                icon = sessFolderArrows + "HisBlueEventSouth.gif";
                                break;
                            case 5:
                                icon = sessFolderArrows + "HisBlueEventSWest.gif";
                                break;
                            case 6:
                                icon = sessFolderArrows + "HisBlueEventWest.gif";
                                break;
                            case 7:
                                icon = sessFolderArrows + "HisBlueEventNWest.gif";
                                break;
                            case 8:
                                icon = sessFolderArrows + "HisBlueEventNorth.gif";
                                break;
                            default:
                                icon = sessFolderArrows + "HisBlueEventEast.gif";
                                break;
                        }
                }
            }
        }
        else {
            switch (sessIgnStat) {
                case "OFF":
                    icon = sessFolder + "red.gif";
                    break;
                case "ON":
                    icon = sessFolder + "green.gif";
                    break;
                default:
                    icon = sessFolder + "purple.gif";
                    break;
            }

        }
        if (sessEvent == document.getElementById("hiddenTranslatePosition").value) {
            sessEvent = "";
        }
        var sessDetails = '<br>Date: ' + VehicleArray[i][4] + '<br>Location: ' + VehicleArray[i][6] + '<br>Speed:   ' + VehicleArray[i][5] + '<br>Odometer:   ' + sessOdo + '<br><br><B>' + sessEvent + '</B>';
        if (useDrivers == "True") {
            sessDetails = '<br>Date: ' + VehicleArray[i][4] + '<br>Location: ' + VehicleArray[i][6] + '<br>Speed:   ' + VehicleArray[i][5] + '<br>Odometer:   ' + sessOdo + '<br>Driver:   ' + VehicleArray[i][12] + '<br><br><B>' + sessEvent + '</B>';
        }
        if (i == quickhistory_locs.length - 1) {
            var iconsp = sessFolderArrows + "MovingCircle.gif";
            var pinsp = new VEPushpin(999999, quickhistory_locs[i], iconsp);
            map.AddPushpin(pinsp);
        }
        // var pin = new VEPushpin(pinid, quickhistory_locs[i], icon, VehicleArray[i][1], sessDetails);
        var pin = new VEShape(VEShapeType.Pushpin, quickhistory_locs[i]);
        pin.SetCustomIcon(icon);
        pin.SetTitle(VehicleArray[i][1]);
        pin.SetDescription(sessDetails);
        //map.AddPushpin(pin);
        VehicleQuickHistoryPinLayer.AddShape(pin);
        pinid++;
    }
    map.AddShapeLayer(VehicleQuickHistoryPinLayer);
}
function PlotQuickHistory() {
    var j = 0;
    Not_Fleet_Colours = true;
    for (var i = 0; i < QuickHistoryArray.length; i++) {
        var thisReg = QuickHistoryArray[i][1];
        var thisDesc = QuickHistoryArray[i][0];
        var thisLat = QuickHistoryArray[i][2];
        var thisLong = QuickHistoryArray[i][3];
        var thisDateTime = QuickHistoryArray[i][4];
        var thisIgnStatus = QuickHistoryArray[i][7];
        var thisLoc = QuickHistoryArray[i][6];
        var thisSpdDir = QuickHistoryArray[i][5];
        var thisType = QuickHistoryArray[i][9];
        var thisOdo = addCommas(QuickHistoryArray[i][11]) + ' ';
        var thisVehType = QuickHistoryArray[i][10];
        var thisHeading = QuickHistoryArray[i][8];
        var thisDriver = " ";
        var tmpArrayStr = thisReg + '|' + thisDesc + '|' + thisLat + '|' + thisLong + '|' + thisDateTime + '|' + thisSpdDir + '|' + thisLoc + '|' + thisIgnStatus + '|' + thisHeading + '|' + thisType + '|' + thisVehType + '|' + thisOdo + '|' + thisDriver;
        if (thisLat !== 0 && thisLong !== 0 && IsValidLat(thisLat)) {
            if (j === 0) {
                myVehArray = tmpArrayStr;
                j++;
            }
            else {
                myVehArray = myVehArray + "~" + tmpArrayStr;
                j++;
            }
        }
    }
    if (j > 0) {
        NumofPlots = j;
        document.getElementById("loading").className = "loading-invisible";
        if (MapEng == "MS_VirtualEarth") {
            Load_VE_Map();
        }
        if (MapEng == "Google_Maps") {
            Load_Google_Map();
        }
    }
}
function MouseOver(rowID) {
    dgVehicles.selectRow(rowID - 1, false, false, false);
}
function GetGeoFenceValue() {
    document.getElementById("Message_Container_1").style.visibility = "visible";
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("codebase/imgs/");
    MsgWin.setSkin("dhx_skyblue");
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 365, 100);
    //MsgWin.window("DisplayMessage").center();
    MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();
    DisplayMsgWin.setText(document.getElementById("hiddenNewGeoRadiusHeader").value);
    DisplayMsgWin.attachObject("Message_Container_1");
    DisplayMsgWin.centerOnScreen();
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();
    document.getElementById("txtNewRadius").focus();
}
function EndMsgDisplay_Radius() {
    try {
        if (document.getElementById("txtNewRadius").value > 0) {
            var newRadius = document.getElementById("txtNewRadius").value;
            document.getElementById("hiddenNewGeofenceRadius").value = newRadius;
            var combo = dgNewGeofence_Fleets.getCustomCombo(7, 1);
            combo.put(12, newRadius);
            dgNewGeofence_Fleets.cells(6, 1).setValue(newRadius);
        }
        DisplayMsgWin.setModal(false);
        DisplayMsgWin.hide();
        document.getElementById("Message_Container_1").style.visibility = "hidden";
    }
    catch (Error) {
        window.alert("Unable to close message - " + Error.message);
    }
}
function dgMessages_MouseOver(rowID) {
    dgMessages.selectRow(rowID, false, false, false);
}
function ShowMessage(rowID, cell) {
    if (cell !== 0) {
        document.getElementById("Message_Container").style.display = "inline";
        MsgWin.setImagePath("../../../dhtmlxMenu/codebase/imgs/");
        DisplayMsgWin = null;
        DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 375, 200);
        MsgWin.window("DisplayMessage").center();
        MsgWin.window("DisplayMessage").button("close").hide();
        MsgWin.window("DisplayMessage").button("park").hide();
        MsgWin.window("DisplayMessage").button("minmax1").hide();

        if (DisplayOption == "ByReg") {
            DisplayMsgWin.setText("Message from " + dgMessages.cells(rowID, 0).getValue());
            document.getElementById("txtMsgFrom").value = dgMessages.cells(rowID, 0).getValue();
        }
        else {
            DisplayMsgWin.setText("Message from " + dgMessages.cells(rowID, 1).getValue());
            document.getElementById("txtMsgFrom").value = dgMessages.cells(rowID, 1).getValue();
        }
        document.getElementById("txtMsgDate").value = dgMessages.cells(rowID, 2).getValue();
        document.getElementById("txtMessage").value = dgMessages.cells(rowID, 4).getValue();
        DisplayMsgWin.attachObject("Message_Container");
        DisplayMsgWin.centerOnScreen();
        //DisplayMsgWin.setModal(true);
        DisplayMsgWin.show();
    }
}
function SendMessage(ToThisTerminal) {
    var NoMDTmsg = document.getElementById("hiddenNoDataTerminal").value;
    if (ToThisTerminal !== "ALL") {
        for (var i = 0; i < CurrentVehicleArray.length; i++) {
            if ($.inArray(CurrentVehicleArray[i][0], selectedVehiclesArray > -1) || $.inArray(CurrentVehicleArray[i][1], selectedVehiclesArray) > -1) {
                if (CurrentVehicleArray[i][0] == ToThisTerminal || CurrentVehicleArray[i][1] == ToThisTerminal) {
                    var thisUnitType = CurrentVehicleArray[i][37];
                    switch (thisUnitType) {
                        case 'SPIDER MTG': case 'SPIDER MTGL': case 'SPIDER MTGUL':
                            MsgTermType = CurrentVehicleArray[i][38];
                            if (MsgTermType != "0") {
                                OkToSendMessage(ToThisTerminal);
                            }
                            else {
                                window.alert(NoMDTmsg);
                            }
                            break;
                        case 'MT2000':
                            OkToSendMessage(ToThisTerminal);
                            break;
                        default:
                            window.alert(NoMDTmsg);
                            break;
                    }
                }
            }
        }
    }
    else {
        OkToSendMessage(ToThisTerminal);
    }


}
function OkToSendMessage(ToThisTerminal) {
    if (MsgWin) {
        MsgWin = null;
    }
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("../../../dhtmlxMenu/codebase/imgs/");
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 365, 250);
    MsgWin.window("DisplayMessage").center();
    MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();
    var MsgTitle = document.getElementById("hiddenSendMsgTitle").value;
    DisplayMsgWin.setText(MsgTitle + ToThisTerminal);
    DisplayMsgWin.attachObject("Message_Container");
    DisplayMsgWin.centerOnScreen();
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();
    $("#txtMsgFrom").val(ToThisTerminal);
    $("#txtMsgDate").val(Date());
}
function EndMsgDisplay() {
    try {
        document.getElementById("txtMsgFrom").value = null;
        document.getElementById("Message_Container").style.display = "none";
        DisplayMsgWin.setModal(false);
        DisplayMsgWin.hide("DisplayMessage");
    }
    catch (Error) {
        window.alert("Unable to close message - " + Error.message);
    }
}
function SendMsgToTerminal() {
    var thisMsg;
    thisMsg = document.getElementById("txtMsgFrom").value + "~" + document.getElementById("txtMsgDate").value + "~" + document.getElementById("txtMessage").value + "~" + MsgTermType;
    document.getElementById("hiddenMsgToSend").value = thisMsg;
    document.getElementById("hiddenButtonSendMsgToTerminal").click();
    EndMsgDisplay();
}
function GetCurrentLocation(rowID, cell) {
    //  This function gets the details for the selected vehicle
    //  in the current vehicle grid and displays them on the map
    if (MapEng !== "") {
        CurrenttvTreeNodeLevel = 2;
        document.getElementById("hiddenMapRequest").value = "AllVeh";
        if (cell == 4) {
            var i = rowID - 1;
            var thisReg = CurrentVehicleArray[i][0];
            var thisDesc = CurrentVehicleArray[i][1];
            PlottedVehicle = thisDesc;
            var thisLat = CurrentVehicleArray[i][2];
            var thisLong = CurrentVehicleArray[i][3];
            var thisDateTime = CurrentVehicleArray[i][4];
            var thisIgnStatus = CurrentVehicleArray[i][7];
            var thisLoc = CurrentVehicleArray[i][6];
            var thisSpdDir = CurrentVehicleArray[i][5];
            var thisType = CurrentVehicleArray[i][9];
            var thisOdo = addCommas(CurrentVehicleArray[i][11]) + ' ';
            var thisVehType = CurrentVehicleArray[i][10];
            var thisHeading = CurrentVehicleArray[i][8];
            var thisDriver = CurrentVehicleArray[i][36];
            var tmpArrayStr = thisReg + '|' + thisDesc + '|' + thisLat + '|' + thisLong + '|' + thisDateTime + '|' + thisSpdDir + '|' + thisLoc + '|' + thisIgnStatus + '|' + thisHeading + '|' + thisType + '|' + thisVehType + '|' + thisOdo + '|' + thisDriver;
            if (CurrentVehicleArray[i][37] === "LMU") {
                tmpArrayStr = tmpArrayStr + '|' + CurrentVehicleArray[i][43] + '|' + CurrentVehicleArray[i][44] + '|' + CurrentVehicleArray[i][45] + '|' + CurrentVehicleArray[i][46] + '|' + CurrentVehicleArray[i][47] + '|' + CurrentVehicleArray[i][48];
            }
            myVehArray = tmpArrayStr;
            NumofPlots = 1;
            if (thisLat !== 0 && thisLong !== 0 && IsValidLat(thisLat)) {
                if (MapEng == "MS_VirtualEarth") {
                    Load_VE_Map();
                }
                if (MapEng == "Google_Maps") {
                    Load_Google_Map();
                }
                if (MapEng == "MSTMap") {
                    try {
                        fleets_TabBar.setTabActive("FT_Map", true);
                    }

                    catch (Error) {
                    }
                    var wantedVehicle = "MSTMap" + "|" + PlottedVehicle + "|" + 2;
                    document.getElementById("hiddenMSTWantedVehicle").value = wantedVehicle;
                    document.getElementById("btnMSTWantedVehicle").click();
                    document.getElementById("loading").className = "loading-visible";
                    var tmpMsg = document.getElementById("hiddenLoadingMap").value;
                    document.getElementById("loadingLabel").innerHTML = tmpMsg;
                }

            }
            else {
                window.alert("Unable to display this location - invalid GPS position");
            }
        }
    }
    else {
        window.alert("Unable to display this location - Incorrect Map setting - Contact your distributor");
    }
}
function IsValidLat(thisLat) {
    if (thisLat > 89 || thisLat === "") {
        return false;
    }
    else {
        return true;
    }
}
function RemoveFleetColour(fleet) {
    var startpos = fleet.indexOf("(");
    if (startpos == -1) {
        return fleet;
    }
    else {
        return fleet.substring(0, startpos - 1);
    }
}
function Fleets_UpdateDashboard(nodeID) {
    //fleets_TabBar.setTabActive("FT_Dashboard", true);
    PlottedVehicle = nodeID;
    CurrenttvTreeNodeLevel = tvVehicles.getLevel(nodeID);

    if (CurrenttvTreeNodeLevel == 1) {
        $("#hiddenSelectedFleet").val(RemoveFleetColour(tvVehicles.getItemText(nodeID)));
        $("#hiddenSelectedVehicle").val("");             //tvVehicles.getItemText(nodeID);
        $("#hiddenSelectedVehicles").val(tvVehicles.getAllChecked());
        $("#hiddenButtonUpdateDashboard").click();
    }

    if (CurrenttvTreeNodeLevel == 2) {
        $("#hiddenSelectedFleet").val("");
        $("#hiddenSelectedVehicle").val(PlottedVehicle);             //tvVehicles.getItemText(nodeID);
        $("#hiddenButtonUpdateDashboard").click();
    }
}
function Fleets_ReloadDashboard() {
    fleets_TabBar.forceLoad("FT_Dashboard");
}
function GetCurrentLocationFromTree(nodeID) {
    //  This function gets the details for the selected vehicle
    //  in the current vehicle tree and displays them on the map
    //  If the selected tab is the Dashboard then the vehicle daskboard
    //  data will be displayed.
    var currentTab = fleets_TabBar.getActiveTab();
    if (currentTab == "FT_Dashboard") {
        Fleets_UpdateDashboard(nodeID);
        return;
    }

    IsQuickHistory = false;
    document.getElementById("hiddenMapRequest").value = "AllVeh";
    IsPlottingQuickHistory = false;
    //PlottedVehicle = tvVehicles.getSelectedItemText();
    PlottedVehicle = nodeID;
    CurrenttvTreeNodeLevel = tvVehicles.getLevel(nodeID);

    if (CurrenttvTreeNodeLevel == 1) {
        PlottedVehicle = tvVehicles.getItemText(nodeID);
    }
    if (MapEng == "MS_VirtualEarth" || MapEng == "Google_Maps") {
        PlotFromVehicleTree(PlottedVehicle, CurrenttvTreeNodeLevel);
    }
    if (MapEng == "MSTMap") {
        try {
            fleets_TabBar.setTabActive("FT_Map", true);
        }
        catch (Error) {
        }
        document.getElementById("loading").className = "loading-visible";
        var tmpmsg = document.getElementById("hiddenLoadingMap").value;
        document.getElementById("loadingLabel").innerHTML = tmpmsg;
        var wantedVehicle = "MSTMap" + "|" + PlottedVehicle + "|" + CurrenttvTreeNodeLevel;
        document.getElementById("hiddenMSTWantedVehicle").value = wantedVehicle;
        document.getElementById("btnMSTWantedVehicle").click();

    }
}
function BuildInitialVehicleArray(selectedVehicles) {
    var j = 0;
    for (var i = 0; i < CurrentVehicleArray.length; i++) {
        if (selectedVehicles.indexOf(CurrentVehicleArray[i][0]) > -1 || selectedVehicles.indexOf(CurrentVehicleArray[i][1]) > -1 || hasSelectedVehicles == false) {
            var thisReg = CurrentVehicleArray[i][0];
            var thisDesc = CurrentVehicleArray[i][1];
            var thisLat = CurrentVehicleArray[i][2];
            var thisLong = CurrentVehicleArray[i][3];
            var thisDateTime = CurrentVehicleArray[i][4];
            var thisIgnStatus = CurrentVehicleArray[i][7];
            var thisLoc = CurrentVehicleArray[i][6];
            var thisSpdDir = CurrentVehicleArray[i][5];
            var thisType = CurrentVehicleArray[i][9];
            var thisOdo = addCommas(CurrentVehicleArray[i][11]) + ' ';
            var thisVehType = CurrentVehicleArray[i][10];
            var thisHeading = CurrentVehicleArray[i][8];
            var thisDriver = CurrentVehicleArray[i][36];
            var thisFleetColour = CurrentVehicleArray[i][39];
            var tmpArrayStr = thisReg + '|' + thisDesc + '|' + thisLat + '|' + thisLong + '|' + thisDateTime + '|' + thisSpdDir + '|' + thisLoc + '|' + thisIgnStatus + '|' + thisHeading + '|' + thisType + '|' + thisVehType + '|' + thisOdo + '|' + thisDriver + '|' + thisFleetColour;
            if (CurrentVehicleArray[i][37] === "LMU") {
                tmpArrayStr = tmpArrayStr + '|' + CurrentVehicleArray[i][43] + '|' + CurrentVehicleArray[i][44] + '|' + CurrentVehicleArray[i][45] + '|' + CurrentVehicleArray[i][46] + '|' + CurrentVehicleArray[i][47] + '|' + CurrentVehicleArray[i][48];
            }
            if (thisLat !== 0 && thisLong !== 0 && IsValidLat(thisLat)) {
                if (j === 0) {
                    myVehArray = tmpArrayStr;
                    j++;
                }
                else {
                    myVehArray = myVehArray + "~" + tmpArrayStr;
                    j++;
                }
            }
        }
    }
    NumofPlots = j;
}
function PlotFromVehicleTree(thisreg, thisTreeLevel) {
    var selectedVehiclesArray = selectedVehicles.split(",");
    myVehArray = [];
    NumofPlots = 0;
    var j = 0;
    var thisFleetColour;
    if (thisTreeLevel === 0) {
        Not_Fleet_Colours = false;
        for (var i = 0; i < CurrentVehicleArray.length; i++) {
            if ($.inArray(CurrentVehicleArray[i][0], selectedVehiclesArray > -1) || $.inArray(CurrentVehicleArray[i][1], selectedVehiclesArray) > -1) {
                var thisReg = CurrentVehicleArray[i][0];
                var thisDesc = CurrentVehicleArray[i][1];
                var thisLat = CurrentVehicleArray[i][2];
                var thisLong = CurrentVehicleArray[i][3];
                var thisDateTime = CurrentVehicleArray[i][4];
                var thisIgnStatus = CurrentVehicleArray[i][7];
                var thisLoc = CurrentVehicleArray[i][6];
                var thisSpdDir = CurrentVehicleArray[i][5];
                var thisType = CurrentVehicleArray[i][9];
                var thisOdo = addCommas(CurrentVehicleArray[i][11]) + ' ';
                var thisVehType = CurrentVehicleArray[i][10];
                var thisHeading = CurrentVehicleArray[i][8];
                var thisDriver = CurrentVehicleArray[i][36];
                thisFleetColour = CurrentVehicleArray[i][39];
                var tmpArrayStr = thisReg + '|' + thisDesc + '|' + thisLat + '|' + thisLong + '|' + thisDateTime + '|' + thisSpdDir + '|' + thisLoc + '|' + thisIgnStatus + '|' + thisHeading + '|' + thisType + '|' + thisVehType + '|' + thisOdo + '|' + thisDriver + '|' + thisFleetColour;
                if (CurrentVehicleArray[i][37] === "LMU") {
                    tmpArrayStr = tmpArrayStr + '|' + CurrentVehicleArray[i][43] + '|' + CurrentVehicleArray[i][44] + '|' + CurrentVehicleArray[i][45] + '|' + CurrentVehicleArray[i][46] + '|' + CurrentVehicleArray[i][47] + '|' + CurrentVehicleArray[i][48];
                }
                if (thisLat !== 0 && thisLong !== 0 && IsValidLat(thisLat)) {
                    if (j === 0) {
                        myVehArray = tmpArrayStr;
                        j++;
                    }
                    else {
                        myVehArray = myVehArray + "~" + tmpArrayStr;
                        j++;
                    }
                }
            }
        }
        if (j > 0) {
            NumofPlots = j;
            if (MapEng == "MS_VirtualEarth") {
                Load_VE_Map();
            }
            if (MapEng == "Google_Maps") {
                Load_Google_Map();
            }

        }
    }

    //    var items = selectedVehicles.split(',');
    //    $.each(items, function (Index, value) {
    //        tvVehicles.setCheck(value, true);
    //    });

    if (thisTreeLevel == 1) {
        Not_Fleet_Colours = false;

        for (i = 0; i < CurrentVehicleArray.length; i++) {
            if ($.inArray(CurrentVehicleArray[i][0], selectedVehiclesArray) > -1 || $.inArray(CurrentVehicleArray[i][1], selectedVehiclesArray) > -1) {
                var tmpArray = thisreg.split("(");
                thisreg = trim(tmpArray[0]);
                if (CurrentVehicleArray[i][12] == thisreg) {
                    thisReg = CurrentVehicleArray[i][0];
                    thisDesc = CurrentVehicleArray[i][1];
                    thisLat = CurrentVehicleArray[i][2];
                    thisLong = CurrentVehicleArray[i][3];
                    thisDateTime = CurrentVehicleArray[i][4];
                    thisIgnStatus = CurrentVehicleArray[i][7];
                    thisLoc = CurrentVehicleArray[i][6];
                    thisSpdDir = CurrentVehicleArray[i][5];
                    thisType = CurrentVehicleArray[i][9];
                    thisOdo = addCommas(CurrentVehicleArray[i][11]) + ' ';
                    thisVehType = CurrentVehicleArray[i][10];
                    thisHeading = CurrentVehicleArray[i][8];
                    thisDriver = CurrentVehicleArray[i][36];
                    thisFleetColour = CurrentVehicleArray[i][39];
                    tmpArrayStr = thisReg + '|' + thisDesc + '|' + thisLat + '|' + thisLong + '|' + thisDateTime + '|' + thisSpdDir + '|' + thisLoc + '|' + thisIgnStatus + '|' + thisHeading + '|' + thisType + '|' + thisVehType + '|' + thisOdo + '|' + thisDriver + '|' + thisFleetColour;
                    if (CurrentVehicleArray[i][37] === "LMU") {
                        tmpArrayStr = tmpArrayStr + '|' + CurrentVehicleArray[i][43] + '|' + CurrentVehicleArray[i][44] + '|' + CurrentVehicleArray[i][45] + '|' + CurrentVehicleArray[i][46] + '|' + CurrentVehicleArray[i][47] + '|' + CurrentVehicleArray[i][48];
                    }
                    if (thisLat !== 0 && thisLong !== 0 && IsValidLat(thisLat)) {
                        if (j === 0) {
                            myVehArray = tmpArrayStr;
                            j++;
                        }
                        else {
                            myVehArray = myVehArray + "~" + tmpArrayStr;
                            j++;
                        }
                    }
                }
            }
        }
        if (j > 0) {
            NumofPlots = j;
            if (MapEng == "MS_VirtualEarth") {
                Load_VE_Map();
            }
            if (MapEng == "Google_Maps") {
                Load_Google_Map();
            }
        }
    }

    if (thisTreeLevel == 2) {
        Not_Fleet_Colours = true;
        for (i = 0; i < CurrentVehicleArray.length; i++) {
            if (CurrentVehicleArray[i][0] == thisreg || CurrentVehicleArray[i][1] == thisreg) {
                thisReg = CurrentVehicleArray[i][0];
                thisDesc = CurrentVehicleArray[i][1];
                thisLat = CurrentVehicleArray[i][2];
                thisLong = CurrentVehicleArray[i][3];
                thisDateTime = CurrentVehicleArray[i][4];
                thisIgnStatus = CurrentVehicleArray[i][7];
                thisLoc = CurrentVehicleArray[i][6];
                thisSpdDir = CurrentVehicleArray[i][5];
                thisType = CurrentVehicleArray[i][9];
                thisOdo = addCommas(CurrentVehicleArray[i][11]) + ' ';
                thisVehType = CurrentVehicleArray[i][10];
                thisHeading = CurrentVehicleArray[i][8];
                thisDriver = CurrentVehicleArray[i][36];
                tmpArrayStr = thisReg + '|' + thisDesc + '|' + thisLat + '|' + thisLong + '|' + thisDateTime + '|' + thisSpdDir + '|' + thisLoc + '|' + thisIgnStatus + '|' + thisHeading + '|' + thisType + '|' + thisVehType + '|' + thisOdo + '|' + thisDriver;
                if (CurrentVehicleArray[i][37] === "LMU") {
                    tmpArrayStr = tmpArrayStr + '|' + CurrentVehicleArray[i][43] + '|' + CurrentVehicleArray[i][44] + '|' + CurrentVehicleArray[i][45] + '|' + CurrentVehicleArray[i][46] + '|' + CurrentVehicleArray[i][47] + '|' + CurrentVehicleArray[i][48];
                }
                myVehArray = tmpArrayStr;
                NumofPlots = 1;
                if (thisLat !== 0 && thisLong !== 0 && IsValidLat(thisLat)) {
                    if (MapEng == "MS_VirtualEarth") {
                        Load_VE_Map();
                    }
                    if (MapEng == "Google_Maps") {
                        Load_Google_Map();
                    }
                }
                else {
                    window.alert("Unable to display this location - invalid GPS position");
                }
            }
        }
    }
}
function AddFilledCircle(pgID, latin, lonin, radius) {
    var locs = new Array();
    var R; // earth's mean radius in km
    if (distunits == "Miles") {
        R = 3956;
    }
    else {
        R = 6371;
    }
    var lat1 = latin * Math.PI / 180.0;
    var lon1 = lonin * Math.PI / 180.0;
    var d = radius / R;
    var x;
    for (x = 0; x <= 360; x += 5)         //x+=10
    {
        var tc = (x / 90) * Math.PI / 2;
        var lat = Math.asin(Math.sin(lat1) * Math.cos(d) + Math.cos(lat1) * Math.sin(d) * Math.cos(tc));
        lat = 180.0 * lat / Math.PI;
        var lon;
        if (Math.cos(lat1) === 0) {
            lon = lonin; // endpoint a pole
        }
        else {
            lon = ((lon1 - Math.asin(Math.sin(tc) * Math.sin(d) / Math.cos(lat1)) + Math.PI) % (2 * Math.PI)) - Math.PI;
        }
        lon = 180.0 * lon / Math.PI;
        var loc = new VELatLong(lat, lon);
        locs.push(loc);
    }
    //var poly = new VEPolygon(pgID, locs, new VEColor(255, 204, 255, 0.5), new VEColor(0, 0, 0, 0.5), 1);
    var circle = new VEShape(VEShapeType.Polygon, locs);
    circle.SetFillColor(new VEColor(255, 204, 255, 0.5));
    circle.SetLineColor(new VEColor(0, 0, 0, 0.5));
    circle.SetLineWidth(1);
    circle.HideIcon();
    circle.SetZIndex(5, 5);
    return circle;
}
function CreateNewGeofence(geoLat, geoLong) {
    try {
        if (document.getElementById("chkCreateGeoFromSite").checked == true) {
            document.getElementById("chkCreateGeoFromSite").checked = false;
            document.getElementById("chkCreateSiteFromGeo").style.display = "none";
            document.getElementById("lblCreateSiteFromGeo").style.display = "none";
        }
        else {
            lastActiveTab = fleets_TabBar.getActiveTab();
            document.getElementById("chkCreateSiteFromGeo").style.display = "inline";
            document.getElementById("lblCreateSiteFromGeo").style.display = "inline";
        }
        fleets_TabBar.showTab("FT_Geofence");
        fleets_TabBar.setTabActive("FT_Geofence");
        document.getElementById("New_Geofence_Container").style.display = "block";
        newLat = geoLat;
        newLong = geoLong;
        LoadNewGeofenceGrid();
    }
    catch (Error) {

    }
}
function CreateNewSite(siteLat, siteLong, siteLoc) {
    try {
        if (document.getElementById("chkCreateSiteFromGeo").checked == true) {
            document.getElementById("chkCreateSiteFromGeo").checked = false;
            document.getElementById("chkCreateGeoFromSite").style.display = "none";
            document.getElementById("lblCreateGeoFromSite").style.display = "none";
        }
        else {
            lastActiveTab = fleets_TabBar.getActiveTab();
            document.getElementById("chkCreateGeoFromSite").style.display = "inline";
            document.getElementById("lblCreateGeoFromSite").style.display = "inline";
        }
        fleets_TabBar.showTab("FT_Site");
        fleets_TabBar.setTabActive("FT_Site");
        document.getElementById("New_Site_Container").style.display = "block";
        newSiteName = siteLoc;
        newLat = siteLat;
        newLong = siteLong;
        LoadNewSiteGrid();
    }
    catch (Error) {

    }
}
function SaveGeofence() {
    if (dgNewGeofence_Fleets.cells(1, 1).getValue() === "") {
        window.alert(document.getElementById("hiddenGeoCodeBlank").value);
        return false;
    }
    if (dgNewGeofence_Fleets.cells(2, 1).getValue() === "") {
        window.alert(document.getElementById("hiddenGeoNameBlank").value);
        return false;
    }
    if (dgNewGeofence_Fleets.cells(3, 1).getValue() === "") {
        window.alert(document.getElementById("hiddenGeoFleetBlank").value);
        return false;
    }
    if (dgNewGeofence_Fleets.cells(7, 1).getValue() === 3) {
        if (dgNewGeofence_Fleets.cells(8, 1).getValue() <= "0") {
            window.alert(document.getElementById("hiddenGeoMaxTimeInvalid").value);
            return false;
        }
    }
    dgNewGeofence_Fleets.setCSVDelimiter("|");
    dgNewGeofence_Fleets.csv.row = "~";
    dgNewGeofence_Fleets.enableCSVAutoID(true);
    document.getElementById("hiddenNewGeofence").value = dgNewGeofence_Fleets.serializeToCSV();
    document.getElementById("hiddenButtonSaveGeofence").click();
    document.getElementById("New_Geofence_Container").style.display = "none";
    if (document.getElementById("chkCreateSiteFromGeo").checked == true) {
        fleets_TabBar.hideTab("FT_Geofence");
        CreateNewSite(newLat, newLong, dgNewGeofence_Fleets.cells(2, 1).getValue());
    }
    else {
        fleets_TabBar.hideTab("FT_Geofence");
        fleets_TabBar.setTabActive(lastActiveTab);
    }
    return true;
}
function ddlstNewAlertChanged() {
    var thisSelectedAlert = document.getElementById("ddlstNewAlertType");
    if (thisSelectedAlert[2].selected !== true) {
        document.getElementById("txtMaxTime").style.visibility = 'hidden';
        document.getElementById("lblMaxTime").style.visibility = "hidden";
    }
    else {
        document.getElementById("txtMaxTime").style.visibility = "visible";
        document.getElementById("lblMaxTime").style.visibility = "visible";
    }
}
function CancelGeofences() {
    fleets_TabBar.hideTab("FT_Geofence");
    if (ShowMapFirst) {
        fleets_TabBar.setTabActive("FT_Map");
    }
    else {
        fleets_TabBar.setTabActive("FT_Grid");
    }
}
function CancelSites() {
    fleets_TabBar.hideTab("FT_Site");
    if (ShowMapFirst) {
        fleets_TabBar.setTabActive("FT_Map");
    }
    else {
        fleets_TabBar.setTabActive("FT_Grid");
    }
}

function dgVehDispLatLong() {
    try {
        var thisrow = dgVehicles.getSelectedRowId() - 1;
        var thisLat = CurrentVehicleArray[thisrow][2];
        var thisLong = CurrentVehicleArray[thisrow][3];
        var thisLoc = CurrentVehicleArray[thisrow][6];
        if (parseInt(LatLongFormat, 10) > 1) {
            thisLat = FormatLatLong(thisLat, 0, parseInt(LatLongFormat, 10));
            thisLong = FormatLatLong(thisLong, 1, parseInt(LatLongFormat, 10));
        }
        dgVehicles.selectCell(thisrow, 8);
        if (CurrentVehicleArray[thisrow][37] != "1") {
            dgVehicles.cells(thisrow + 1, 4).setValue("<a href='#'>" + thisLat + ", " + thisLong + "</a>");
            CurrentVehicleArray[thisrow][37] = "1";
        }
        else {
            if (CurrentVehicleArray[thisrow][37] == "1") {
                dgVehicles.cells(thisrow + 1, 4).setValue("<a href='#'>" + thisLoc + "</a>");
                CurrentVehicleArray[thisrow][37] = "0";
            }
        }
    }
    catch (Error) {
        DisplayNoRowSelectedDialog();
    }
}
function DisplayNoRowSelectedDialog() {
    eo_GetObject('DialogNoRowSelected').show(true);
}
function dgVehCreateGeofence() {
    try {
        var thisrow = dgVehicles.getSelectedRowId() - 1;
        var thisLat = CurrentVehicleArray[thisrow][2];
        var thisLong = CurrentVehicleArray[thisrow][3];
        var thisLoc = CurrentVehicleArray[thisrow][6];
        document.getElementById("hiddenNewGeofenceRadius").value = 0;
        CreateNewGeofence(thisLat, thisLong, thisLoc);
    }
    catch (Error) {
        DisplayNoRowSelectedDialog();
    }
}
function dgVehCreateSite() {
    try {
        var thisrow = dgVehicles.getSelectedRowId() - 1;
        var thisLat = CurrentVehicleArray[thisrow][2];
        var thisLong = CurrentVehicleArray[thisrow][3];
        var thisLoc = CurrentVehicleArray[thisrow][6];
        CreateNewSite(thisLat, thisLong, thisLoc);
    }
    catch (Error) {
        DisplayNoRowSelectedDialog();
    }
}
function DoPageLoad() {
    AllowMessaging = document.getElementById('hiddenAllowMessaging').value;
    if (MapEng == "MS_VirtualEarth") {
        clearSiteLayers();
    }
    if (MapEng == "Google_Maps") {
        clearSiteOverlays();
    }
    doInitTree();
    loadCurrentVehicles();
    DisplayOption = document.getElementById("hiddenDisplayOption").value;
    ShowMapFirst = document.getElementById("hiddenShowMapFirst").value;
    LatLongFormat = document.getElementById("hiddenLatLongFormat").value;
    MapEng = document.getElementById("hiddenMapEng").value;
    if (PlottedVehicle != "none_zzID" && fleets_TabBar.getActiveTab() == "FT_Map") {
        if (MapEng == "MS_VirtualEarth" || MapEng == "Google_Maps") {
            PlotFromVehicleTree(PlottedVehicle, CurrenttvTreeNodeLevel);
        }
        if (MapEng == "MSTMap") {
            fleets_TabBar.setTabActive("FT_Map", true);
            wantedVehicle = "MSTMap" + "|" + PlottedVehicle + "|" + CurrenttvTreeNodeLevel;
            document.getElementById("hiddenMSTWantedVehicle").value = wantedVehicle;
            document.getElementById("btnMSTWantedVehicle").click();
        }
    }
    if (ShowMapFirst == 'True' && PlottedVehicle == "none_zzID") {
        if (MapEng == "MS_VirtualEarth" || MapEng == "Google_Maps") {
            Load_Initial_VE_Map();
        }
        if (MapEng == "MSTMap") {
            fleets_TabBar.setTabActive("FT_Map", true);
            var wantedVehicle = "MSTMap" + "|" + PlottedVehicle + "|" + CurrenttvTreeNodeLevel;
            document.getElementById("hiddenMSTWantedVehicle").value = wantedVehicle;
            document.getElementById("btnMSTWantedVehicle").click();
        }
    }
    if (ShowMapFirst == 'False' && IsRefresh != "True") {
        fleets_TabBar.setTabActive("FT_Grid", true);
    }
    if (UseSites == "True") {
        if (IsDisplayingSites == true) {
            ShowSites(true);
        }
    }
    document.getElementById("loading").className = "loading-invisible";
    document.getElementById("NextRefresh").innerHTML = document.getElementById("hiddenNextRefresh").value;
    PageRefreshTimer = document.getElementById("hiddenPageRefresh").value;
    if (PageRefreshTimer == "999999") {
        document.getElementById("NextRefresh").innerHTML = document.getElementById("hiddenRefreshDisabled").value;
    }
    else {
        ActivateCountDown("CountDownPanel1", PageRefreshTimer, "RefreshPage");
    }
}
function Load_Initial_VE_Map() {
    BuildInitialVehicleArray(selectedVehicles);
    Not_Fleet_Colours = false;
    Fleet_Code = document.getElementById("hidden_FleetCode").value;
    Company = document.getElementById("hidden_Company").value;
    if (Fleet_Code != "*") {
        PlottedVehicle = Company;
    }
    else {
        PlottedVehicle = Fleet_Code;
    }
    try {
        if (MapEng == "MS_VirtualEarth") {
            Load_VE_Map();
        }
        if (MapEng == "Google_Maps") {
            Load_Google_Map();
        }
    }
    catch (Error2) {
        window.alert("Error 2365 Activating Web Map: " + Error2.message + ". Please contact your dealer");
    }
}
function createGoogleScript() {
    try {

        var thisKey = document.getElementById("hiddenGoogleKey").value;
        var script = document.createElement("script");
        script.type = "text/javascript";
        script.src = "http://maps.googleapis.com/maps/api/js?v=3&" + thisKey + "&sensor=true&callback=initalizeGoogleMap";
        document.body.appendChild(script);
    }
    catch (Error) {
        window.alert(Error.message);
    }
}
function initalizeGoogleMap() {
    var script1 = document.createElement("script");
    script1.type = "text/javascript";
    script1.src = "./Scripts/label_v3.js";
    document.body.appendChild(script1);
    waitPreloadPage2();
}
function clear_Google_Overlays() {
    try {
        if (Markers) {
            for (i in Markers) {
                Markers[i].setMap(null);
            }
        }
        if (VehLabels) {
            for (i in VehLabels) {
                VehLabels[i].setMap(null);
            }
            VehLabels = [];
        }
        if (circle) {
            for (i in circle) {
                circle[i].setMap(null);
            }
        }
        if (connectSiteLineArray) {
            for (i in connectSiteLineArray) {
                connectSiteLineArray[i].setMap(null);
            }

        }
        //    if (siteMarkers) {
        //        for (i in siteMarkers) {
        //            siteMarkers[i].setMap(null);
        //        }
        //    }
        //    if (siteLabels) {
        //        for (i in siteLabels) {
        //            siteLabels[i].setMap(null);
        //        }
        //    }
    } catch (e) {
        alert("Error 2992: " + e.Message);
    }
}
function getInternetExplorerVersion() {
    var rv = -1; // Return value assumes failure.
    if (navigator.appName == 'Microsoft Internet Explorer') {
        var ua = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    }
    return rv;
}

function Load_Google_Map() {
    try {
        BuildArray(myVehArray);
        fleets_TabBar.setTabActive("FT_Map", true);
        if (!map) {
            zoomLevel = 15;
            var googleMapOptions = {
                zoom: 15,
                scaleControl: true,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                mapTypeControlOptions: {
                    style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                    position: google.maps.ControlPosition.TOP_LEFT
                }
            };
            map = new google.maps.Map(document.getElementById("myMap"), googleMapOptions);
            trafficLayer = new google.maps.TrafficLayer();
            google.maps.event.addListener(map, 'rightclick', function (point) {
                try {
                    infowindow.close();
                }
                catch (Error) {
                }
                Google_Map_Click(point);
            });
        }
        else {
            zoomLevel = map.getZoom();
            clear_Google_Overlays();
        }
        try {
            if (IsQuickHistory === true) {
                AddGoogleQuickHistoryMarkers();
                //IsQuickHistory = false;
            }
            else {
                AddGoogleMarkers();
            }
        }
        catch (Error3) {
            // window.alert("Error 87: " + Error3.message);
        }
        var latlngbounds = new google.maps.LatLngBounds();
        for (var i = 0; i < VehicleArray.length; i++) {
            var loc = new google.maps.LatLng(VehicleArray[i][2], VehicleArray[i][3]);
            latlngbounds.extend(loc);
        }

        if (ShowTraffic == true) {
            trafficLayer.setMap(map);
        }
        else {
            trafficLayer.setMap();
        }
        if (IsDisplayingGeofence == true) {
            ShowGeofences(IsDisplayingGeofence);
        }
        if (IsDisplayingSites == true && IsFromClosestSite == false) {                     //SiteArray.length > 0 &&
            ShowSites(IsDisplayingSites);
        }
        if (VehicleArray.length > 1) {
            if (fixedMapBounds != 0) {
                var tmpArray = fixedMapBounds.split(')');
                var southWest = Mid(tmpArray[0], 2, tmpArray[0].length);
                var northEast = Mid(tmpArray[1], 3, tmpArray[1].length);
                var sWest = southWest.split(',');
                var nEast = northEast.split(',');
                sWest = new google.maps.LatLng(parseFloat(sWest[0]), parseFloat(sWest[1]));
                nEast = new google.maps.LatLng(parseFloat(nEast[0]), parseFloat(nEast[1]));
                latlngbounds = new google.maps.LatLngBounds(sWest, nEast);
                map.setCenter(latlngbounds.getCenter());            //, map.fitBounds(latlngbounds));
                map.setZoom(parseInt(fixedMapZoom));
            }
            else {
                //fixedMapRectangle.setBounds(null);
                map.setCenter(latlngbounds.getCenter(), map.fitBounds(latlngbounds));
            }
        }
        else {
            map.setCenter(latlngbounds.getCenter(), zoomLevel);
        }
    }
    catch (Error2) {
        window.alert("Error 2515 Loading Google Web Map: " + Error2.message + " Please notify your dealer if error persists.");
    }
}

function Mid(str, start, len)
/***
IN: str - the string we are LEFTing
start - our string's starting position (0 based!!)
len - how many characters from start we want to get

RETVAL: The substring from start to start+len
***/
{
    // Make sure start and len are within proper bounds
    if (start < 0 || len < 0) return "";

    var iEnd, iLen = String(str).length;
    if (start + len > iLen)
        iEnd = iLen;
    else
        iEnd = start + len;

    return String(str).substring(start, iEnd);
}

function AddGoogleMarkers() {
    pinid = 0;
    icon = 'Images/LorryGreen.png';
    Markers = [];
    Markers_Detail = [];
    for (var i = 0; i < VehicleArray.length; i++) {
        var sessIgnStat = VehicleArray[i][7];
        var sessHeading = parseInt(VehicleArray[i][8], 10);
        var sessEvent = VehicleArray[i][9];
        var sessFolder = "Images/Vehicles/";
        var str = VehicleArray[i][11];
        var sessOdo = str.substring(0, str.length - 1);
        var sessVehicleType = VehicleArray[i][10];
        //if (sessVehicleType != "Bus" && sessVehicleType != 'Car' && sessVehicleType != 'Lorry' && sessVehicleType != 'Truck') {
        //    sessVehicleType = "Car";
        //}
        if (document.getElementById("hiddenUseFleetColours").value == "True" && Not_Fleet_Colours == false) {
            if (VehicleArray[i][13] != "") {
                icon = "Images/FleetColours/Lorry" + VehicleArray[i][13] + ".png";
            }
            else {
                icon = "Images/FleetColours/LorryRed.png";
            }
        }
        else if (UseIcons == "True" && sessVehicleType != "") {
            switch (sessIgnStat) {
                case "OFF":
                    icon = sessFolder + sessVehicleType + "RedEast.png";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolder + sessVehicleType + "RedEast.png";
                            break;
                        case 2:
                            icon = sessFolder + sessVehicleType + "RedEast.png";
                            break;
                        case 3:
                            icon = sessFolder + sessVehicleType + "RedEast.png";
                            break;
                        case 4:
                            icon = sessFolder + sessVehicleType + "RedEast.png";
                            break;
                        case 5:
                            icon = sessFolder + sessVehicleType + "RedWest.png";
                            break;
                        case 6:
                            icon = sessFolder + sessVehicleType + "RedWest.png";
                            break;
                        case 7:
                            icon = sessFolder + sessVehicleType + "RedWest.png";
                            break;
                        case 8:
                            icon = sessFolder + sessVehicleType + "RedWest.png";
                            break;
                        default:
                            icon = sessFolder + sessVehicleType + "RedEast.png";
                            break;
                    }
                    break;

                case "ON":
                    icon = sessFolder + sessVehicleType + "GreenEast.png";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolder + sessVehicleType + "GreenEast.png";
                            break;
                        case 2:
                            icon = sessFolder + sessVehicleType + "GreenEast.png";
                            break;
                        case 3:
                            icon = sessFolder + sessVehicleType + "GreenEast.png";
                            break;
                        case 4:
                            icon = sessFolder + sessVehicleType + "GreenEast.png";
                            break;
                        case 5:
                            icon = sessFolder + sessVehicleType + "GreenWest.png";
                            break;
                        case 6:
                            icon = sessFolder + sessVehicleType + "GreenWest.png";
                            break;
                        case 7:
                            icon = sessFolder + sessVehicleType + "GreenWest.png";
                            break;
                        case 8:
                            icon = sessFolder + sessVehicleType + "GreenWest.png";
                            break;
                        default:
                            icon = sessFolder + sessVehicleType + "GreenEast.png";
                            break;
                    }
                    break;
                default:
                    icon = sessFolder + sessVehicleType + "BlueEast.png";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolder + sessVehicleType + "BlueEast.png";
                            break;
                        case 2:
                            icon = sessFolder + sessVehicleType + "BlueEast.png";
                            break;
                        case 3:
                            icon = sessFolder + sessVehicleType + "BlueEast.png";
                            break;
                        case 4:
                            icon = sessFolder + sessVehicleType + "BlueEast.png";
                            break;
                        case 5:
                            icon = sessFolder + sessVehicleType + "BlueWest.png";
                            break;
                        case 6:
                            icon = sessFolder + sessVehicleType + "BlueWest.png";
                            break;
                        case 7:
                            icon = sessFolder + sessVehicleType + "BlueWest.png";
                            break;
                        case 8:
                            icon = sessFolder + sessVehicleType + "BlueWest.png";
                            break;
                        default:
                            icon = sessFolder + sessVehicleType + "BlueEast.png";
                            break;
                    }
            }

            if (sessEvent != document.getElementById("hiddenTranslatePosition").value) {
                switch (sessIgnStat) {

                    case "OFF":
                        icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                        switch (sessHeading) {
                            case 1:
                                icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                                break;
                            case 2:
                                icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                                break;
                            case 3:
                                icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                                break;
                            case 4:
                                icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                                break;
                            case 5:
                                icon = sessFolder + sessVehicleType + "RedEventWest.gif";
                                break;
                            case 6:
                                icon = sessFolder + sessVehicleType + "RedEventWest.gif";
                                break;
                            case 7:
                                icon = sessFolder + sessVehicleType + "RedEventWest.gif";
                                break;
                            case 8:
                                icon = sessFolder + sessVehicleType + "RedEventWest.gif";
                                break;
                            default:
                                icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                                break;
                        }
                        break;
                    case "ON":
                        icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                        switch (sessHeading) {
                            case 1:
                                icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                                break;
                            case 2:
                                icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                                break;
                            case 3:
                                icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                                break;
                            case 4:
                                icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                                break;
                            case 5:
                                icon = sessFolder + sessVehicleType + "GreenEventWest.gif";
                                break;
                            case 6:
                                icon = sessFolder + sessVehicleType + "GreenEventWest.gif";
                                break;
                            case 7:
                                icon = sessFolder + sessVehicleType + "GreenEventWest.gif";
                                break;
                            case 8:
                                icon = sessFolder + sessVehicleType + "GreenEventWest.gif";
                                break;
                            default:
                                icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                                break;
                        }
                        break;
                    default:
                        icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                        switch (sessHeading) {
                            case 1:
                                icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                                break;
                            case 2:
                                icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                                break;
                            case 3:
                                icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                                break;
                            case 4:
                                icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                                break;
                            case 5:
                                icon = sessFolder + sessVehicleType + "BlueEventWest.gif";
                                break;
                            case 6:
                                icon = sessFolder + sessVehicleType + "BlueEventWest.gif";
                                break;
                            case 7:
                                icon = sessFolder + sessVehicleType + "BlueEventWest.gif";
                                break;
                            case 8:
                                icon = sessFolder + sessVehicleType + "BlueEventWest.gif";
                                break;
                            default:
                                icon = sessFolder + sessVehicleType + "BlueEventEast.gif";
                                break;
                        }
                }
            }
        }
        else {
            switch (sessIgnStat) {
                case "OFF":
                    icon = sessFolder + "red.gif";
                    break;
                case "ON":
                    icon = sessFolder + "green.gif";
                    break;
                default:
                    icon = sessFolder + "purple.gif";
                    break;
            }

        }
        if (sessEvent == document.getElementById("hiddenTranslatePosition").value) {
            sessEvent = "";
        }
        var thisLabel;
        if (DisplayOption == "ByReg") {
            thisLabel = VehicleArray[i][0];
        }
        else {
            thisLabel = VehicleArray[i][1];
        }
        var sessDetails = '<div class=infodescription><B>' + thisLabel + ' </B><br>Date: ' + VehicleArray[i][4] + '<br>Location: ' + VehicleArray[i][6] + '<br>Speed:   ' + VehicleArray[i][5] + '<br>Odometer:   ' + sessOdo + '<br><B>' + sessEvent + '</B></div>';
        if (useDrivers == "True") {
            sessDetails = '<div class=infodescription><B>' + thisLabel + ' </B><br>Date: ' + VehicleArray[i][4] + '<br>Location: ' + VehicleArray[i][6] + '<br>Speed:   ' + VehicleArray[i][5] + '<br>Odometer:   ' + sessOdo + '<br>Driver:   ' + VehicleArray[i][12] + '<br><B>' + sessEvent + '</B></div>';
        }
        Markers_Detail.push(sessDetails);
        var myIcon = new google.maps.MarkerImage(icon,
            null,
            null,
            new google.maps.Point(12, 12),
            new google.maps.Size(24, 24));
        //        if (icon.indexOf("Event") >= 0 && browserName == "firefox") {
        //            alert("firefox: " + myIcon.size.width + myIcon.size.height + myIcon.anchor.x + myIcon.anchor.x + myIcon.scaledSize.width + myIcon.scaledSize.height);
        //            myIcon.size = new google.maps.Size(24, 36);
        //            alert("firefox: " + myIcon.size.width + myIcon.size.height + myIcon.anchor.x + myIcon.anchor.x + myIcon.scaledSize.width + myIcon.scaledSize.height);
        //        }
        var loc = new google.maps.LatLng(VehicleArray[i][2], VehicleArray[i][3]);
        Markers.push(new google.maps.Marker({ position: loc, map: map, title: thisLabel, icon: myIcon, zIndex: 100 }));
        Markers[Markers.length - 1].setMap(map);
        if (IsDisplayingVehicleLabels == true) {
            var VehLabel = new Label({ map: map }, '#F2F2F2', 9, 0);
            VehLabel.set('position', loc);
            VehLabel.set('text', thisLabel);
            VehLabels.push(VehLabel);
        }
        google.maps.event.addListener(Markers[Markers.length - 1], "mouseover", function () {
            for (i = 0; i < Markers.length; i++) {
                if (Markers[i].position == this.position) {
                    try {
                        infowindow.close();
                    }
                    catch (Error) {
                    }

                    infowindow = new google.maps.InfoWindow({
                        content: Markers_Detail[i],
                        size: new google.maps.Size(50, 50),
                        position: this.position
                    });
                    infowindow.open(map);
                }
            }
        });
        google.maps.event.addListener(Markers[Markers.length - 1], "mouseout", function () {
            for (i = 0; i < Markers.length; i++) {
                if (Markers[i].position == this.position) {
                    try {
                        infowindow.close();
                    }
                    catch (Error) {
                    }
                }
            }
        });

        pinid++;
    }
    RedisplayButtons();
}
function AddGoogleQuickHistoryMarkers() {
    pinid = 0;
    icon = 'Images/LorryGreen.png';

    for (var i = 0; i < VehicleArray.length; i++) {
        var sessIgnStat = VehicleArray[i][7];
        var sessHeading = parseInt(VehicleArray[i][8], 10);
        var sessEvent = VehicleArray[i][9];
        var sessFolder = "Images/Vehicles/";
        var sessFolderArrows = "Images/Arrows/";
        var str = VehicleArray[i][11];
        var sessOdo = str.substring(0, str.length - 1);
        var sessVehicleType = VehicleArray[i][10];
        if (document.getElementById("hiddenUseFleetColours").value == "True" && Not_Fleet_Colours == false) {
            if (VehicleArray[i][13] != "") {
                icon = "Images/FleetColours/Lorry" + VehicleArray[i][13] + ".png";
            }
            else {
                icon = "Images/FleetColours/LorryRed.png";
            }
        }
        else if (sessVehicleType != "") {
            switch (sessIgnStat) {
                case "OFF":
                    icon = sessFolder + "Red_Dot.gif";
                    break;
                case "ON":
                    icon = sessFolderArrows + "HisGreenNorth.gif";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolderArrows + "HisGreenNorth.gif";
                            break;
                        case 2:
                            icon = sessFolderArrows + "HisGreenNEast.gif";
                            break;
                        case 3:
                            icon = sessFolderArrows + "HisGreenEast.gif";
                            break;
                        case 4:
                            icon = sessFolderArrows + "HisGreenSEast.gif";
                            break;
                        case 5:
                            icon = sessFolderArrows + "HisGreenSouth.gif";
                            break;
                        case 6:
                            icon = sessFolderArrows + "HisGreenSWest.gif";
                            break;
                        case 7:
                            icon = sessFolderArrows + "HisGreenWest.gif";
                            break;
                        case 8:
                            icon = sessFolderArrows + "HisGreenNWest.gif";
                            break;
                        default:
                            icon = sessFolderArrows + sessVehicleType + "HisGreenNorth.png";
                            break;
                    }
                    break;
                default:
                    icon = sessFolderArrows + "HisBlueNorth.gif";
                    switch (sessHeading) {
                        case 1:
                            icon = sessFolderArrows + "HisBlueNorth.gif";
                            break;
                        case 2:
                            icon = sessFolderArrows + "HisBlueNEast.gif";
                            break;
                        case 3:
                            icon = sessFolderArrows + "HisBlueEast.gif";
                            break;
                        case 4:
                            icon = sessFolderArrows + "HisBlueSEast.gif";
                            break;
                        case 5:
                            icon = sessFolderArrows + "HisBlueSouth.gif";
                            break;
                        case 6:
                            icon = sessFolderArrows + "HisBlueSWest.gif";
                            break;
                        case 7:
                            icon = sessFolderArrows + "HisBlueWest.gif";
                            break;
                        case 8:
                            icon = sessFolderArrows + "HisBlueNWest.gif";
                            break;
                        default:
                            icon = sessFolderArrows + "HisBlueNorth.gif";
                            break;
                    }
            }
            if (sessEvent != document.getElementById("hiddenTranslatePosition").value) {
                switch (sessIgnStat) {
                    case "OFF":
                        if (sessEvent.toUpperCase() == "IGNITION OFF") {
                            icon = sessFolder + sessVehicleType + "RedEventEast.gif";
                        }
                        else {
                            icon = sessFolder + "Red_Dot_Event.gif";
                        }
                        break;
                    case "ON":
                        if (sessEvent.toUpperCase() == "IGNITION ON") {
                            icon = sessFolder + sessVehicleType + "GreenEventEast.gif";
                        }
                        else {
                            icon = sessFolderArrows + "HisGreenEventEast.gif";
                            switch (sessHeading) {
                                case 1:
                                    icon = sessFolderArrows + "HisGreenEventNorth.gif";
                                    break;
                                case 2:
                                    icon = sessFolderArrows + "HisGreenEventNEast.gif";
                                    break;
                                case 3:
                                    icon = sessFolderArrows + "HisGreenEventEast.gif";
                                    break;
                                case 4:
                                    icon = sessFolderArrows + "HisGreenEventSEast.gif";
                                    break;
                                case 5:
                                    icon = sessFolderArrows + "HisGreenEventSouth.gif";
                                    break;
                                case 6:
                                    icon = sessFolderArrows + "HisGreenEventWest.gif";
                                    break;
                                case 7:
                                    icon = sessFolderArrows + "HisGreenEventSWest.gif";
                                    break;
                                case 8:
                                    icon = sessFolderArrows + "HisGreenEventWest.gif";
                                    break;
                                default:
                                    icon = sessFolderArrows + "HisGreenEventNWest.gif";
                                    break;
                            }
                        }
                        break;
                    default:
                        icon = sessFolderArrows + "HisBlueEventNorth.gif";
                        switch (sessHeading) {
                            case 1:
                                icon = sessFolderArrows + "HisBlueEventNEast.gif";
                                break;
                            case 2:
                                icon = sessFolderArrows + "HisBlueEventEast.gif";
                                break;
                            case 3:
                                icon = sessFolderArrows + "HisBlueEventSEast.gif";
                                break;
                            case 4:
                                icon = sessFolderArrows + "HisBlueEventSouth.gif";
                                break;
                            case 5:
                                icon = sessFolderArrows + "HisBlueEventSWest.gif";
                                break;
                            case 6:
                                icon = sessFolderArrows + "HisBlueEventWest.gif";
                                break;
                            case 7:
                                icon = sessFolderArrows + "HisBlueEventNWest.gif";
                                break;
                            case 8:
                                icon = sessFolderArrows + "HisBlueEventNorth.gif";
                                break;
                            default:
                                icon = sessFolderArrows + "HisBlueEventEast.gif";
                                break;
                        }
                }
            }
        }
        else {
            switch (sessIgnStat) {
                case "OFF":
                    icon = sessFolder + "red.gif";
                    break;
                case "ON":
                    icon = sessFolder + "green.gif";
                    break;
                default:
                    icon = sessFolder + "purple.gif";
                    break;
            }

        }
        if (sessEvent == document.getElementById("hiddenTranslatePosition").value) {
            sessEvent = "";
        }
        var thisLabel;
        if (DisplayOption == "ByReg") {
            thisLabel = VehicleArray[i][0];
        }
        else {
            thisLabel = VehicleArray[i][1];
        }
        var sessDetails = '<div class=infodescription><B>' + thisLabel + ' </B><br><br>Date: ' + VehicleArray[i][4] + '<br>Location: ' + VehicleArray[i][6] + '<br>Speed:   ' + VehicleArray[i][5] + '<br>Odometer:   ' + sessOdo + '<br><br><B>' + sessEvent + '</B></div>';
        if (useDrivers == "True") {
            sessDetails = '<div class=infodescription><B>' + thisLabel + ' </B><br><br>Date: ' + VehicleArray[i][4] + '<br>Location: ' + VehicleArray[i][6] + '<br>Speed:   ' + VehicleArray[i][5] + '<br>Odometer:   ' + sessOdo + '<br>Driver:   ' + VehicleArray[i][12] + '<br><br><B>' + sessEvent + '</B></div>';
        }
        Markers_Detail.push(sessDetails);
        var myIcon;
        if (icon == "Images/Vehicles/Red_Dot.gif" || icon == "Images/Vehicles/Red_Dot_Event.gif") {
            myIcon = new google.maps.MarkerImage(icon,
                null,
                null,
                new google.maps.Point(5.5, 5.5),
                new google.maps.Size(11, 11));
        } else {
            myIcon = new google.maps.MarkerImage(icon,
                null,
                null,
                new google.maps.Point(12, 12),
                new google.maps.Size(24, 24));
        }
        var loc = new google.maps.LatLng(VehicleArray[i][2], VehicleArray[i][3]);
        Markers.push(new google.maps.Marker({ position: loc, map: map, title: thisLabel, icon: myIcon }));
        Markers[Markers.length - 1].setMap(map);
        google.maps.event.addListener(Markers[Markers.length - 1], "mouseover", function () {
            for (i = 0; i < Markers.length; i++) {
                if (Markers[i].position == this.position) {
                    try {
                        infowindow.close();
                    }
                    catch (Error) {
                    }

                    infowindow = new google.maps.InfoWindow({
                        content: Markers_Detail[i],
                        size: new google.maps.Size(50, 75),
                        position: this.position
                    });
                    infowindow.open(map);
                }
            }
        });
        google.maps.event.addListener(Markers[Markers.length - 1], "mouseout", function () {
            for (i = 0; i < Markers.length; i++) {
                if (Markers[i].position == this.position) {
                    try {
                        infowindow.close();
                    }
                    catch (Error) {
                    }
                }
            }
        });

        pinid++;
    }
    RedisplayButtons();
}
function jsAppend(js_file) {
    var js_script = document.createElement('script');
    js_script.type = "text/javascript";
    js_script.src = js_file;
    document.getElementsByTagName('head')[0].appendChild(js_script);
}
function Google_Map_Click(clickedPoint) {
    var thisLat = clickedPoint.latLng.lat();
    var thisLong = clickedPoint.latLng.lng();
    thisLatLong = thisLat + "," + thisLong;
    map_contextMenu.showContextMenu(clickedPoint.pixel.x + 150, clickedPoint.pixel.y + 20);
}
function Load_VE_Map() {
    try {
        //Delete_VE_Map_Layers();
        if (myVehArray !== null) {
            try {
                for (var i = 0; i < GeofenceArray.length; i++) {
                    var geoID = GeofenceArray[i][0];
                    map.DeletePolygon(geoID);
                    showGeofences = "False";
                }
            }
            catch (Error) {
            }
            fleets_TabBar.setTabActive("FT_Map", true);
            try {
                CurrentZoomLevel = map.GetZoomLevel();
                selStyle = map.GetMapStyle();
                selMode = map.GetMapMode();
            }
            catch (Error1) {
                CurrentZoomLevel = 15;
                selStyle = VEMapStyle.Road;
                selMode = VEMapMode.Mode2D;
            }
            var locs = [];
            BuildArray(myVehArray);
            if (!map) {
                map = null;
                try {
                    // If the browser is Firefox get the version number
                    var ffv = 0;
                    var ffn = "Firefox/";
                    var ffp = navigator.userAgent.indexOf(ffn);
                    if (ffp != -1) {
                        ffv = parseFloat(navigator.userAgent.substring(ffp + ffn.length));
                    }
                    // If we're using Firefox 1.5 or above override the Virtual Earth drawing functions to use SVG
                    if (ffv >= 1.5) {

                        Msn.Drawing.Graphic.CreateGraphic = function (f, b) { return new Msn.Drawing.SVGGraphic(f, b); };
                    }
                    map = new VEMap('myMap');
                    map.SetDashboardSize(VEDashboardSize.Normal);
                    var options = new VEMapOptions();
                    AllowBirdsEye = document.getElementById('hiddenAllowBirdsEye').value;
                    if (AllowBirdsEye == "True") {
                        options.EnableBirdseye = true;
                        options.EnableDashboardLabels = true;
                    }
                    else {
                        options.EnableBirdseye = false;
                        options.EnableDashboardLabels = false;
                    }
                    map.LoadMap(null, null, null, null, null, false, null, options);
                    map.SetMapStyle(selStyle);
                    map.SetMapMode(selMode);
                    map.AttachEvent("onclick", VE_Map_MouseHandler);
                    var map_distunits;

                    if (distunits == "Miles") {
                        map_distunits = VEDistanceUnit.Miles;
                    }
                    else {
                        map_distunits = VEDistanceUnit.Kilometers;
                    }
                    map.SetScaleBarDistanceUnit(map_distunits);
                }
                catch (Error2) {
                    window.alert("Error 79: " + Error2.message);
                }
            }
            clearVehicleLayers();

            //if (NumofPlots == 1) {
            //CurrentZoomLevel = 15;
            //}
            try {
                for (var j = 0; j < NumofPlots; j++) {
                    //if (VehicleArray[j][2] !== 0 && VehicleArray[j][3] !== 0) {
                    if (VehicleArray[j][2].length > 0 && VehicleArray[j][3].length > 0) {
                        var loc = new VELatLong(VehicleArray[j][2], VehicleArray[j][3]);
                        locs.push(loc);
                    }
                }
            }
            catch (Error) {
                window.alert("Error 2009: " + Error.message);
            }

            if (IsDisplayingGeofence == true) {
                ShowGeofences(IsDisplayingGeofence);
            }
            if (IsDisplayingSites == true && SiteArray.length > 0 && IsFromClosestSite == false) {
                ShowSites(IsDisplayingSites);
            }

            try {
                if (IsQuickHistory === true) {
                    QuickHistoryAddPin(locs);
                    //IsQuickHistory = false;
                }
                else {
                    AddPin(locs);
                }
            }
            catch (Error3) {
                window.alert("Error 87: " + Error3.message);
            }
            //            try {
            //                if (document.getElementById("hiddenIsDisplayingGeofences").value == "true") {
            //                    CreateGeofenceLayer();
            //                }
            //            }
            //            catch (Error4) {
            //                window.alert("Error 524: " + Error4.message);
            //            }
            try {

                if ((fixedMapBounds != 0) && (NumofPlots > 1)) {
                    var tmpLoc = fixedMapBounds.split(",");
                    map.SetCenterAndZoom(new VELatLong(tmpLoc[0], tmpLoc[1]), fixedMapZoom);
                }
                else {
                    DoBestMap(locs);
                }

            }
            catch (Error5) {
                window.alert("Error 530: " + Error5.message);
            }
        }
        //document.body.style.cursor = "default";

    }
    catch (Error6) {
        window.alert("Error 327: " + Error6.message);
    }

    RedisplayButtons();
}
function Add_VE_Map_Layers() {
    if (IsDisplayingGeofence == true) {
        try {
            map.DeleteShapeLayer(geoFenceLayer);
            map.AddShapeLayer(geoFenceLayer);
        } catch (e) {

        }
    }
    if (IsDisplayingSiteLabels == true) {
        try {
            map.AddShapeLayer(SitesShapeLayer);
        } catch (e) {

        }
    }
    if (IsDisplayingSites == true) {
        try {
            map.AddShapeLayer(SitesPinLayer);
        } catch (e) {

        }
    }
    if (IsDisplayingVehicleLabels == true) {
        try {
            map.AddShapeLayer(VehicleShapeLayer);
        } catch (e) {

        }
    }
    try {
        map.AddShapeLayer(VehiclePinLayer);
    } catch (e) {

    }
}

function Delete_VE_Map_Layers() {

    try {
        map.DeleteShapeLayer(geoFenceLayer);
    } catch (e) {

    }
    try {
        map.DeleteShapeLayer(SitesShapeLayer);
    } catch (e) {

    }
    try {
        map.DeleteShapeLayer(SitesPinLayer);
    } catch (e) {

    }
    try {
        map.DeleteShapeLayer(VehicleShapeLayer);
    } catch (e) {

    }
    try {
        map.DeleteShapeLayer(VehiclePinLayer);

    } catch (e) {

    }
}
function FindAddress() {
    document.getElementById("FindAddress_Container_1").style.visibility = "visible";
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("codebase/imgs/");
    MsgWin.setSkin("dhx_skyblue");
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 400, 100);
    //MsgWin.window("DisplayMessage").center();
    MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();
    DisplayMsgWin.setText(document.getElementById("hiddenFindAddressHeader").value);
    DisplayMsgWin.attachObject("FindAddress_Container_1");
    DisplayMsgWin.centerOnScreen();
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();
    document.getElementById("txtFindAddress").value = "";
    document.getElementById("txtFindAddress").focus();

}

function FindSite() {
    try {
        try {
            Site_Select_Combo.setComboText("");
            Site_Select_Combo.clearAll();
            if (MapEng == "MS_VirtualEarth") {
                clearSiteLayers();
            }
            if (MapEng == "Google_Maps") {
                clearSiteOverlays();
            }
        } catch (e) {

        }
        IsLoading_Site_Select_Combo = true;
        var tmpSiteArrayStr = document.getElementById("hiddenSitesArray").value;
        if (tmpSiteArrayStr.length > 0) {
            BuildSiteArray(tmpSiteArrayStr);
        }
        window.dhx_globalImgPath = "codebase/imgs/";
        if (!Site_Select_Combo) {
            Site_Select_Combo = new dhtmlXCombo("combo_FindSite", "FindSite_Combo", 200);
        }
        for (var i = 0; i < SiteArray.length; i++) {
            Site_Select_Combo.addOption([[i, SiteArray[i][0]]]);
        }
        Site_Select_Combo.enableFilteringMode(true);
    } catch (e) {
        alert(e.Message);
    }
    document.getElementById("FindSite_Container_1").style.display = "inline";
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("codebase/imgs/");
    MsgWin.setSkin("dhx_skyblue");
    MsgWin.attachEvent("onClose", function () {
        DisplayMsgWin.setModal(false);
        DisplayMsgWin.hide();
        DisplayMsgWin = null;
        UnloadFindSiteWindow();
    });
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 225, 75);
    //MsgWin.window("DisplayMessage").center();
    //MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();
    DisplayMsgWin.setText(document.getElementById("hiddenFindSiteHeader").value);
    DisplayMsgWin.attachObject("FindSite_Container_1");
    DisplayMsgWin.centerOnScreen();
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();
    Site_Select_Combo.attachEvent("onChange", FindSelectedSite);
    IsLoading_Site_Select_Combo = false;

}
function UnloadFindSiteWindow() {
    try {
        if (DisplayMsgWin) {
            DisplayMsgWin.hide();
            DisplayMsgWin = null;
        }
    } catch (e) {
    }
}
function FindSelectedSite() {

    if (IsLoading_Site_Select_Combo == false) {
        var thisSite = Site_Select_Combo.getSelectedText();
        if (DisplayMsgWin) {
            DisplayMsgWin.setModal(false);
            DisplayMsgWin.hide();
            DisplayMsgWin = null;
            MsgWin = null;
        }
        SitesShapeLayer = new VEShapeLayer();
        SitesPinLayer = new VEShapeLayer();
        //        siteMarkers = [];
        //        siteLabels = [];
        //        siteMarkers_Detail = [];
        if (MapEng == "MS_VirtualEarth") {
            clearSiteLayers();
            clearVehicleLayers();
        }
        if (MapEng == "Google_Maps") {
            clearSiteOverlays();
            clear_Google_Overlays();
        }
        for (var i = 0; i < SiteArray.length; i++) {
            if (thisSite == SiteArray[i][0]) {
                var thisSiteName = SiteArray[i][0];
                var thisSiteCatDesc = SiteArray[i][3];
                var thisSiteIconPath = SiteArray[i][4];
                var thisSiteComments = SiteArray[i][5];
                var thisSiteLat = SiteArray[i][6];
                var thisSiteLong = SiteArray[i][7];
                var thisSiteContact = SiteArray[i][8];
                var thisSiteContactNo = SiteArray[i][9];
                var icon;
                var sessDetails;
                var thisLabel;
                var loc;
                if (MapEng == "MS_VirtualEarth") {
                    icon = thisSiteIconPath;
                    sessDetails = 'Category: ' + thisSiteCatDesc + '<br>Contact:   ' + thisSiteContact + '<br>Contact Number: ' + thisSiteContactNo + '<br>Comments: ' + thisSiteComments + '<br><br><br>Latitude: ' + thisSiteLat + '<br>Longitude: ' + thisSiteLong;
                    thisLabel = "Site:" + thisSiteName;
                    loc = new VELatLong(thisSiteLat, thisSiteLong);
                    var pin = new VEShape(VEShapeType.Pushpin, loc);
                    pin.SetCustomIcon(icon);
                    pin.SetTitle(thisLabel);
                    pin.SetDescription(sessDetails);
                    var shape = new VEShape(VEShapeType.Pushpin, loc);
                    var thisicon;
                    thisicon = "<div style='color:#212121;font-family:Tahoma;font-style:italic;font-size:8.25pt;font-weight: bold;background-color:transparent;width:auto;white-space:nowrap;text-indent:2em;padding-top:5;'><label style='background-color:" + SiteLabelColour + ";border:solid 1px Black;text-decoration:underline;'>" + thisSiteName + "</label></div>";
                    shape.SetTitle(thisSiteName);
                    shape.SetDescription(sessDetails);
                    shape.SetZIndex(100, 100);
                    shape.SetCustomIcon(thisicon);
                    pinid++;
                    SitesPinLayer.AddShape(pin);
                    // SitesShapeLayer.AddShape(shape);
                    CurrentZoomLevel = map.GetZoomLevel();
                    //map.AddShapeLayer(SitesShapeLayer);
                    map.AddShapeLayer(SitesPinLayer);
                    map.SetCenterAndZoom(loc, CurrentZoomLevel);
                }
                else {
                    try {
                        if (MapEng == "Google_Maps") {
                            icon = thisSiteIconPath;
                            thisLabel = thisSiteName;
                            sessDetails = '<div class=infodescription><B>Site: ' + thisSiteName + '</B><br><br>Category: ' + thisSiteCatDesc + '<br>Contact:   ' + thisSiteContact + '<br>Contact Number: ' + thisSiteContactNo + '<br>Comments: ' + thisSiteComments + '<br><br><br>Latitude: ' + thisSiteLat + '<br>Longitude: ' + thisSiteLong + '</div>';
                            siteMarkers_Detail.push(sessDetails);
                            var myIcon = new google.maps.MarkerImage(icon,
                            null,
                            null,
                            null,
                            new google.maps.Size(32, 32));
                            loc = new google.maps.LatLng(thisSiteLat, thisSiteLong);
                            siteMarkers.push(new google.maps.Marker({ position: loc, map: map, title: thisLabel, icon: myIcon, zIndex: 100 }));
                            siteMarkers[siteMarkers.length - 1].setMap(map);
                            if (IsDisplayingSiteLabels == true) {
                                var SiteLabel = new Label({ map: map }, SiteLabelColour, 0, 0);
                                SiteLabel.set('position', loc);
                                SiteLabel.set('text', thisLabel);
                                siteLabels.push(SiteLabel);
                            }
                            google.maps.event.addListener(siteMarkers[siteMarkers.length - 1], "mouseover", function () {
                                for (i = 0; i < siteMarkers.length; i++) {
                                    if (siteMarkers[i].position == this.position) {
                                        try {
                                            infowindow.close();
                                        }
                                        catch (Error) {
                                        }

                                        infowindow = new google.maps.InfoWindow({
                                            content: siteMarkers_Detail[i],
                                            size: new google.maps.Size(50, 75),
                                            position: this.position
                                        });
                                        infowindow.open(map);
                                    }
                                }
                            });
                            google.maps.event.addListener(siteMarkers[siteMarkers.length - 1], "mouseout", function () {
                                for (i = 0; i < siteMarkers.length; i++) {
                                    if (siteMarkers[i].position == this.position) {
                                        try {
                                            infowindow.close();
                                        }
                                        catch (Error) {
                                        }
                                    }
                                }
                            });
                            google.maps.event.addListener(siteMarkers[siteMarkers.length - 1], "rightclick", function (evt) {
                                for (i = 0; i < siteMarkers.length; i++) {
                                    if (siteMarkers[i].position == this.position) {
                                        try {
                                            SiteClicked(evt, siteMarkers[i]);
                                        }
                                        catch (Error) {
                                        }
                                    }
                                }
                            });
                            var latlngbounds = new google.maps.LatLngBounds();
                            latlngbounds.extend(loc);
                            zoomLevel = map.getZoom();
                            map.setCenter(latlngbounds.getCenter(), zoomLevel);
                            pinid++;
                        }
                    } catch (e) {
                        alert("Error 3821: " + e.Message);
                    }
                }
                //IsDisplayingSites = false;
            }
        }
    }
}
function FindLocFromBtn() {
    document.getElementById("hiddenGetThisLocation").value = "False";
    var thisLoc = document.getElementById("txtFindAddress").value;
    if (thisLoc.length < 1) {
        return false;
    }
    if (dgLocationResults) {
        dgLocationResults.clearAll();
    }
    document.getElementById("loading").className = "loading-visible";
    document.getElementById("loadingLabel").innerHTML = "Searching for location .... please wait";
    fleets_TabBar.setTabActive("FT_AddressDetails", true);
    document.getElementById("hiddenWantedLocation").value = thisLoc;
    document.getElementById("hiddenButtonFindLocation").click();
    return false;
}
function New_FindLocFromBtn() {
    document.getElementById("hiddenGetThisLocation").value = "False";
    var thisLoc = document.getElementById("txtFindAddress").value;
    if (thisLoc.length < 1) {
        return false;
    }

    var geocoder = new google.maps.Geocoder;
    geocoder.geocode({ 'address': thisLoc }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
        }
        else {
            alert("Geocode was not successful for the following reason: " + status);
        }
    });
    return false;
}
function EndFindAddress() {
    fleets_TabBar.hideTab("FT_AddressDetails");
    fleets_TabBar.setTabActive("FT_Map", true);
}
function LoadLocationSearchResultsGrid() {
    document.getElementById("loading").className = "loading-invisible";
    fleets_TabBar.showTab("FT_AddressDetails");
    fleets_TabBar.setTabActive("FT_AddressDetails", true);

    dgLocationResults = new dhtmlXGridObject("FindAddress_container");
    dgLocationResults.setImagePath("codebase/imgs/");
    var tmpHeader = document.getElementById("hiddenSearchLocationGridHeader").value;
    dgLocationResults.setHeader(tmpHeader);
    dgLocationResults.setColTypes("link");
    dgLocationResults.setInitWidths("500");
    dgLocationResults.setColAlign("center");
    dgLocationResults.setSkin(SkinColor);
    dgLocationResults.enableMultiselect(false);
    //dgLocationResults.attachEvent("onMouseOver", MouseOver);
    dgLocationResults.init();
    dgLocationResults.preventIECaching(true);
    clientID = document.getElementById("hiddenClientID").value;
    var Results_xmlFile = "xmlFiles/" + clientID + "_FindAddressResults.xml?etc=" + new Date().getTime();
    dgLocationResults.loadXML(Results_xmlFile);
    EndMsgDisplay();
}
function FindLocFromResults() {
    var rId = dgLocationResults.getSelectedRowId();
    var cInd = dgLocationResults.getSelectedCellIndex();
    var thisNewLoc = dgLocationResults.cells(rId, cInd).getValue();
    //dgLocationResults.clearAll();
    document.getElementById("hiddenWantedLocation").value = GetInnerText(thisNewLoc);
    document.getElementById("hiddenGetThisLocation").value = "True";
    document.getElementById("hiddenButtonFindLocation").click();
    fleets_TabBar.setTabActive("FT_Map", true);
    fleets_TabBar.hideTab("FT_AddressDetails");
}
function GetInnerText(strLoc) {
    //"<A href="javascript:FindLocFromResults();">Lakeview Dr, Swords, County Fingal, Ireland</A>"
    var tmpAddressArray = strLoc.split('>');
    var tmpAddressArray_1 = tmpAddressArray[1].split('<');
    return tmpAddressArray_1[0];

}
function NoLocationResults() {
    document.getElementById("loading").className = "loading-invisible";
    window.alert("No location data found, please try again.");
}
function PlotFoundAddress() {
    var strAddress = document.getElementById("hiddenAddressFound").value;
    var tmpAddressArray = strAddress.split('|');
    var addrLat = tmpAddressArray[0];
    var addrLong = tmpAddressArray[1];
    var addrLoc = tmpAddressArray[2];
    var sessFolder = "Images/Vehicles/";
    var loc;
    var sessDetails;
    if (MapEng == "MS_VirtualEarth") {
        icon = sessFolder + "iconb.png";
        loc = new VELatLong(addrLat, addrLong);
        sessDetails = '<div class=infodescription><B>' + addrLoc + ' </B><br><br>Latitude: ' + addrLat + '<br><br>Longitude: ' + addrLong + '</div>';
        var pin = new VEPushpin(pinid, loc, icon, sessDetails, null);
        map.AddPushpin(pin);
        CurrentZoomLevel = 15;
        map.SetCenterAndZoom(loc, CurrentZoomLevel);
        pinid++;
    }
    if (MapEng == "Google_Maps") {
        icon = sessFolder + "iconb.png";
        var thisLabel = "";
        loc = new google.maps.LatLng(addrLat, addrLong);
        var latlngbounds = new google.maps.LatLngBounds();
        latlngbounds.extend(loc);
        CurrentZoomLevel = 15;
        map.setCenter(latlngbounds.getCenter());
        map.setZoom(CurrentZoomLevel);
        Markers.push(new google.maps.Marker({ position: loc, map: map, title: thisLabel, icon: icon, zIndex: 20 }));
        Markers[Markers.length - 1].setMap(map);
        sessDetails = '<div class=infodescription><B>' + addrLoc + ' </B><br><br>Latitude: ' + addrLat + '<br><br>Longitude: ' + addrLong + '</div>';
        Markers_Detail.push(sessDetails);
        google.maps.event.addListener(Markers[Markers.length - 1], "rightclick", function (evt) {
            for (var i = 0; i < Markers.length; i++) {
                if (Markers[i].position == this.position) {
                    try {
                        Markers[i].setTitle(addrLoc);
                        SiteClicked(evt, Markers[i]);
                    }
                    catch (Error) {
                    }
                }
            }
        });
        google.maps.event.addListener(Markers[Markers.length - 1], "mouseover", function () {
            for (var i = 0; i < Markers.length; i++) {
                if (Markers[i].position == this.position) {
                    try {
                        infowindow.close();
                    }
                    catch (Error) {
                    }

                    infowindow = new google.maps.InfoWindow({
                        content: Markers_Detail[i],
                        size: new google.maps.Size(50, 25),
                        position: this.position
                    });
                    infowindow.open(map);
                }
            }
        });
    }

}
function DoBestMap(locs) {
    if (NumofPlots > 0) {
        if (NumofPlots == 1) {
            map.SetCenterAndZoom(new VELatLong(VehicleArray[0][2], VehicleArray[0][3]), CurrentZoomLevel);
        }
        else {
            map.SetMapView(locs);
        }
    }
    else {
        fleets_TabBar.setTabActive("FT_Grid", true);
    }
}
function GeofenceOnMouseOver(e) {
    if (e.elementID != null) {
        for (var i = 0; i < GeofenceArray.length; i++) {
            if (map.GetShapeByID(e.elementID).GetTitle().match(GeofenceArray[i][1])) {
                window.alert(GeofenceArray[i][1]);
            }
        }
    }
}
function CreateGeofenceLayer(state) {
    try {
        if (state == true) {
            var tmpGeoFenceArrayStr = document.getElementById("hiddenGeofenceArray").value;
            BuildGeoFenceArray(tmpGeoFenceArrayStr);
            //if (MapEng == "MS_VirtualEarth") {
            //    map.AttachEvent("onmouseover", GeofenceOnMouseOver);
            //}
            geoFenceLayer = new VEShapeLayer();
            for (var i = 0; i < GeofenceArray.length; i++) {
                var geoID = GeofenceArray[i][0];
                var geoName = GeofenceArray[i][1];
                var geoLat = parseFloat(GeofenceArray[i][2]);
                var geoLong = parseFloat(GeofenceArray[i][3]);
                var geoRadius = GeofenceArray[i][4] / 1000;
                if (geoLat != "0" && geoLong != "0") {
                    if (MapEng == "MS_VirtualEarth") {
                        var polygon = AddFilledCircle(geoID, geoLat, geoLong, geoRadius);
                        geoFenceLayer.AddShape(polygon);
                        var labelloc;
                        labelloc = new VELatLong(geoLat, geoLong);
                        var shape = new VEShape(VEShapeType.Pushpin, labelloc);
                        var thisicon;
                        thisicon = "<span><img src='Images/geofence.png' width='24px' height='24px'></span>";
                        shape.SetCustomIcon(thisicon);
                        shape.SetZIndex(10, 10);
                        shape.SetTitle('Geofence: ' + geoName);
                        geoFenceLayer.AddShape(shape);
                    }
                    else {
                        if (MapEng == "Google_Maps") {
                            drawCircle(geoID, geoLat, geoLong, geoRadius);
                            var geoDetails = 'Geofence: ' + geoName;
                            var image = 'Images/geofence.png';
                            var myLatLng = new google.maps.LatLng(geoLat, geoLong);
                            var geoMarker = new google.maps.Marker({
                                position: myLatLng,
                                map: map,
                                title: geoDetails,
                                icon: image,
                                zIndex: 10
                            });
                            geofenceMarkersArray.push(geoMarker);
                        }
                    }
                }
            }
            if (MapEng == "MS_VirtualEarth") {
                //Delete_VE_Map_Layers();
                //Add_VE_Map_Layers();
                map.AddShapeLayer(geoFenceLayer);
            }
            showGeofences = "True";
        }
        else {
            try {
                if (MapEng == "MS_VirtualEarth") {
                    map.DeleteShapeLayer(geoFenceLayer);
                }
                else {
                    if (MapEng == "Google_Maps") {
                        for (var gid = 0; gid < GeofenceArray.length; gid++) {
                            var geoID_1 = GeofenceArray[gid][0];
                            if (circle[geoID_1]) {
                                circle[geoID_1].setMap(null);
                            }
                        }
                    }
                    clearGeofenceOverlays();
                }
                showGeofences = "False";
            }
            catch (Error) {
                window.alert("Error 799: " + Error.message);
            }
        }
        document.getElementById("loading").className = "loading-invisible";
    }
    catch (Error) {
        window.alert("Error 3503: " + Error.message);
    }
}
function clearGeofenceOverlays() {
    if (geofenceMarkersArray) {
        for (i in geofenceMarkersArray) {
            geofenceMarkersArray[i].setMap(null);
        }
    }
}
function BuildSiteArray(tmpArrayStr) {
    try {
        var tmpArray = tmpArrayStr.split('~');
        SiteArray = [];
        for (var i = 0; i < tmpArray.length - 1; i++) {
            SiteArray[i] = tmpArray[i].split(',');
        }
        NoofSites = i;
    }
    catch (Error) {
        window.alert("Build Site Array Error: " + Error.message);
    }
}
function PreLoadSites() {

}
function CreateSitesLayer(state) {
    try {
        var tmpSiteArrayStr = document.getElementById("hiddenSitesArray").value;
        if (tmpSiteArrayStr.length > 0) {
            BuildSiteArray(tmpSiteArrayStr);
        }
        try {
            if (MapEng == "MS_VirtualEarth") {
                clearSiteLayers();
            }
            if (MapEng == "Google_Maps") {
                clearSiteOverlays();
            }
        } catch (e) {

        }
        if (state == true) {
            if (SiteArray.length > 0) {
                SitesShapeLayer = new VEShapeLayer();
                SitesPinLayer = new VEShapeLayer();
                siteMarkers = [];
                siteLabels = [];
                siteMarkers_Detail = [];

                for (var i = 0; i < NoofSites; i++) {
                    var thisSiteName = SiteArray[i][0];
                    var thisSiteCatDesc = SiteArray[i][3];
                    var thisSiteIconPath = SiteArray[i][4];
                    var thisSiteComments = SiteArray[i][5];
                    var thisSiteLat = SiteArray[i][6];
                    var thisSiteLong = SiteArray[i][7];
                    var thisSiteContact = SiteArray[i][8];
                    var thisSiteContactNo = SiteArray[i][9];
                    var thisLabel;
                    var loc;
                    var sessDetails;
                    var icon;
                    if (MapEng == "MS_VirtualEarth") {
                        icon = thisSiteIconPath;
                        sessDetails = 'Category: ' + thisSiteCatDesc + '<br>Contact:   ' + thisSiteContact + '<br>Contact Number: ' + thisSiteContactNo + '<br>Comments: ' + thisSiteComments + '<br><br><br>Latitude: ' + thisSiteLat + '<br>Longitude: ' + thisSiteLong;
                        thisLabel = "Site:" + thisSiteName;
                        // ReSharper disable UseOfImplicitGlobalInFunctionScope
                        loc = new VELatLong(thisSiteLat, thisSiteLong); // ReSharper restore UseOfImplicitGlobalInFunctionScope
                        var pin = new VEShape(VEShapeType.Pushpin, loc);
                        pin.SetCustomIcon(icon);
                        pin.SetTitle(thisLabel);
                        pin.SetDescription(sessDetails);
                        var shape = new VEShape(VEShapeType.Pushpin, loc);
                        if (IsDisplayingSiteLabels == true) {
                            var thisicon;
                            thisicon = "<div style='color:#212121;font-family:Tahoma;font-style:italic;font-size:8.25pt;font-weight: bold;background-color:transparent;width:auto;white-space:nowrap;text-indent:2em;padding-top:5;'><label style='background-color:" + SiteLabelColour + ";border:solid 1px Black;text-decoration:underline;'>" + thisSiteName + "</label></div>";
                            shape.SetTitle(thisSiteName);
                            shape.SetDescription(sessDetails);
                            shape.SetZIndex(100, 100);
                            shape.SetCustomIcon(thisicon);
                            SitesShapeLayer.AddShape(shape);
                        }
                        SitesPinLayer.AddShape(pin);

                    }
                    else {
                        try {
                            if (MapEng == "Google_Maps") {
                                icon = thisSiteIconPath;
                                thisLabel = thisSiteName;
                                sessDetails = '<div class=infodescription><B>Site: ' + thisSiteName + '</B><br>Category: ' + thisSiteCatDesc + '<br>Contact:   ' + thisSiteContact + '<br>Contact Number: ' + thisSiteContactNo + '<br>Comments: ' + thisSiteComments + '<br>Latitude: ' + thisSiteLat + '<br>Longitude: ' + thisSiteLong + '</div>';
                                siteMarkers_Detail.push(sessDetails);
                                var myIcon = new google.maps.MarkerImage(icon,
                            null,
                            null,
                            null,
                            new google.maps.Size(32, 32));
                                loc = new google.maps.LatLng(thisSiteLat, thisSiteLong);
                                siteMarkers.push(new google.maps.Marker({ position: loc, map: map, title: thisLabel, icon: myIcon, zIndex: 100 }));
                                siteMarkers[siteMarkers.length - 1].setMap(map);
                                if (IsDisplayingSiteLabels == true) {
                                    var SiteLabel = new Label({ map: map }, SiteLabelColour, 0, 0);
                                    SiteLabel.set('position', loc);
                                    SiteLabel.set('text', thisLabel);
                                    siteLabels.push(SiteLabel);
                                }
                                google.maps.event.addListener(siteMarkers[siteMarkers.length - 1], "mouseover", function () {
                                    for (i = 0; i < siteMarkers.length; i++) {
                                        if (siteMarkers[i].position == this.position) {
                                            try {
                                                infowindow.close();
                                            }
                                            catch (Error1) {
                                            }

                                            infowindow = new google.maps.InfoWindow({
                                                content: siteMarkers_Detail[i],
                                                size: new google.maps.Size(50, 75),
                                                position: this.position
                                            });
                                            infowindow.open(map);
                                        }
                                    }
                                });
                                google.maps.event.addListener(siteMarkers[siteMarkers.length - 1], "mouseout", function () {
                                    for (i = 0; i < siteMarkers.length; i++) {
                                        if (siteMarkers[i].position == this.position) {
                                            try {
                                                infowindow.close();
                                            }
                                            catch (Error1) {
                                            }
                                        }
                                    }
                                });
                                google.maps.event.addListener(siteMarkers[siteMarkers.length - 1], "rightclick", function (evt) {
                                    for (i = 0; i < siteMarkers.length; i++) {
                                        if (siteMarkers[i].position == this.position) {
                                            try {
                                                SiteClicked(evt, siteMarkers[i]);
                                            }
                                            catch (Error11) {
                                            }
                                        }
                                    }
                                });
                                pinid++;
                            }
                        } catch (e) {
                            alert("Error 3821: " + e.Message);
                        }
                    }
                    IsDisplayingSites = true;
                }
                if (MapEng == "MS_VirtualEarth") {
                    if (IsDisplayingSiteLabels == true) {
                        map.AddShapeLayer(SitesShapeLayer);
                    }
                    //Delete_VE_Map_Layers();
                    //Add_VE_Map_Layers();
                    map.AddShapeLayer(SitesPinLayer);
                }
                showSites = "True";
                IsDisplayingSites = true;
            }
            else {
                DisplayMsg("There are no sites created for this Company and Fleet.", 320);
            }
        }
        else {
            try {
                showSites = "False";
                IsDisplayingSites = false;
            }
            catch (Error) {
                window.alert("Error 799: " + Error.message);
            }
        }
        document.getElementById("loading").className = "loading-invisible";
    }
    catch (Error) {
        window.alert("Error 3652: " + Error.message);
    }
}
function getImgSize(imgSrc) {
    p = null;
    var newImg = new Image();
    newImg.src = imgSrc;
    p = $(newImg).ready(function () {
        return { width: newImg.width, height: newImg.height };
    });
    do {

    }
    while (p == null);
    return newImg;
}
function clearVehicleLayers() {
    try {
        map.DeleteShapeLayer(VehiclePinLayer);
    } catch (e) {
    }
    try {
        map.DeleteShapeLayer(VehicleShapeLayer);
    } catch (e) {
    }
    try {
        map.DeleteShapeLayer(VehicleQuickHistoryPinLayer);
    } catch (e) {
    }
}

function clearSiteLayers() {
    try {
        map.DeleteShapeLayer(SitesPinLayer);
    } catch (e) {
    }
    try {
        map.DeleteShapeLayer(SitesShapeLayer);
    } catch (e) {
    }
}
function clearSiteOverlays() {
    if (siteMarkers) {
        for (i in siteMarkers) {
            siteMarkers[i].setMap(null);
        }
        siteMarkers.length = 0;
    }
    if (siteLabels) {
        for (i in siteLabels) {
            siteLabels[i].setMap(null);
        }
        siteLabels.length = 0;
    }
    if (connectSiteLineArray) {
        for (i in connectSiteLineArray) {
            connectSiteLineArray[i].setMap(null);
        }

    }
}

function SiteClicked(clickedPoint, thisSite) {
    SelectedSite = thisSite;
    ShowClosestWindow();
    //map_SiteContextMenu.showContextMenu(clickedPoint.pixel.x + 150, clickedPoint.pixel.y + 20);
}
function drawCircle(geoID, geoLat, geoLong, geoRadius) {
    try {
        var geoFenceCircle;
        var circleOptions = {
            strokeColor: "#FF0000",
            strokeOpacity: 0.25, strokeWeight: 1,
            fillColor: "#FFCCFF",
            fillOpacity: 0.5,
            map: map,
            center: new google.maps.LatLng(geoLat, geoLong),
            radius: geoRadius * 1000

        };
        geoFenceCircle = new google.maps.Circle(circleOptions);
        circle[geoID] = geoFenceCircle;
        geoFenceCircle.setMap(map);
        //        google.maps.event.addListener(circle[geoID], "mouseover", function (evt) {
        //            for (var n in GeofenceArray) {
        //                if (parseFloat(GeofenceArray[n][2]).toFixed(10) === this.center.Ja.toFixed(10) && parseFloat(GeofenceArray[n][3]).toFixed(10) === this.center.Ka.toFixed(10)) {
        //                    try {
        //                        infowindow.close();
        //                    }
        //                    catch (Error) {
        //                    }
        //                    var geoID = GeofenceArray[n][0];
        //                    var geoName = GeofenceArray[n][1];
        //                    var geoLat = parseFloat(GeofenceArray[n][2]);
        //                    var geoLong = parseFloat(GeofenceArray[n][3]);
        //                    var geoRadius = GeofenceArray[n][4] / 1000;
        //                    var distType;
        //                    if (distunits == "Miles") {
        //                        distType = " mls";
        //                    }
        //                    else {
        //                        distType = " kms";
        //                    }
        //                    var geoDetails = '<center><B><u>Geofence Details</u></B></center><br><br><B>Geofence: ' + geoName + '</B><br>Location: ' + geoLat.toFixed(5) + ', ' + geoLong.toFixed(5) + '<br>Radius: ' + geoRadius + distType;
        //                    infowindow = new google.maps.InfoWindow({
        //                        content: geoDetails,
        //                        size: new google.maps.Size(50, 75),
        //                        position: this.center
        //                    });
        //                    infowindow.open(map);
        //                }
        //            }
        //        });
        //        google.maps.event.addListener(circle[geoID], "mouseout", function (evt) {
        //            for (var n in GeofenceArray) {
        //                if (parseFloat(GeofenceArray[n][2]).toFixed(10) === this.center.Ja.toFixed(10) && parseFloat(GeofenceArray[n][3]).toFixed(10) === this.center.Ka.toFixed(10)) {
        //                    try {
        //                        infowindow.close();
        //                    }
        //                    catch (Error) {
        //                    }
        //                }
        //            }
        //        });
    }
    catch (Error) {
        alert("Error 2755: " + Error.message);
    }
}
function doDrawCircle(geoID, geoLat, geoLong) {
    try {
        var d;
        var bounds = new google.maps.LatLngBounds();
        var circlePoints = Array();
        with (Math) {
            if (circleUnits == 'KM') {
                d = circleRadius / 6378.8; // radians
            }
            else { //miles
                d = circleRadius / 3963.189; // radians
            }

            var lat1 = (PI / 180) * geoLat; // radians
            var lng1 = (PI / 180) * geoLong; // radians

            for (var a = 0; a < 361; a++) {
                var tc = (PI / 180) * a;
                var y = asin(sin(lat1) * cos(d) + cos(lat1) * sin(d) * cos(tc));
                var dlng = atan2(sin(tc) * sin(d) * cos(lat1), cos(d) - sin(lat1) * sin(y));
                var x = ((lng1 - dlng + PI) % (2 * PI)) - PI; // MOD function
                var point = new google.maps.LatLng(parseFloat(y * (180 / PI)), parseFloat(x * (180 / PI)));
                circlePoints.push(point);
                bounds.extend(point);
            }
            if (d < 1.5678565720686044) {
                circle[geoID] = new google.maps.Polygon(circlePoints, '#000000', 1, 0.25, '#FFCCFF', 0.5);
            }
            else {
                circle[geoID] = new google.maps.Polygon(circlePoints, '#FFCCFF', 1, 0.5);
            }
            map.addOverlay(circle[geoID]);
        }
    }
    catch (Error) {
        window.alert("Error 2477: " + Error.message);
    }
}
function LoadVehicleDetailsGrid() {
    try {
        dgVehicleDetails = dhxLayout.cells("c").attachGrid();
        dgVehicleDetails.setImagePath("codebase/imgs/");
        dgVehicleDetails.setHeader(document.getElementById("hiddenVehicleDetailHeader").value + ",#cspan", null, ["text-align:center;"]);

        //dgVehicleDetails.setNoHeader(true);
        //dgVehicleDetails.setStyle("text-align:center;font-family:Tahoma;font-size:13px;font-weight:bold;color:#055A78;background-image:url(codebase/imgs/skin_modern_header.png");
        dgVehicleDetails.setInitWidthsP("40,60");
        dgVehicleDetails.enableLightMouseNavigation("true");
        dgVehicleDetails.enableTooltips("false,true");
        dgVehicleDetails.enableCollSpan(true);
        dgVehicleDetails.enableMultiline(true);
        dgVehicleDetails.setColAlign("left,center");
        dgVehicleDetails.setSkin(SkinColor);
        dgVehicleDetails.init();
        dgVehicleDetails.preventIECaching(true);
        clientID = document.getElementById("hiddenClientID").value;
        var settings_xmlFile = "xmlFiles/" + clientID + "_VehicleDetails.xml?etc=" + new Date().getTime();
        dgVehicleDetails.loadXML(settings_xmlFile);
    }
    catch (Error) {
        window.alert("Error 2115: " + Error.message);
    }
}
function getPosition() {
    connectLineStartPoint = currentCusorY;
}
function tvVehiclesItemMouseIn(id) {

    if (id.indexOf("~") < 0) {
        tvVehicles.selectItem(id, false, false);
        var thisreg = id;
        getPosition();
        document.getElementById('hiddenSelectedVehicle').value = thisreg;
        DisplayVehicleInfo(thisreg);
        if (autoLocate == "True" && fleets_TabBar.getActiveTab() == "FT_Map" && IsQuickHistory !== true) {
            var endPoint;
            var startpix;
            var startPoint;
            var lineCoordinates;
            if (MapEng == "MS_VirtualEarth") {
                try {
                    if (connectLineArray) {
                        for (i in connectLineArray) {
                            map.DeletePolyline(i);
                        }
                        connectLineArray = [];
                    }
                }
                catch (Error) {
                }
                for (i = 0; i < VehicleArray.length; i++) {
                    if (VehicleArray[i][0] == thisreg || VehicleArray[i][1] == thisreg) {
                        endPoint = new VELatLong(VehicleArray[i][2], VehicleArray[i][3]);
                        startpix = connectLineStartPoint;
                        var pixel = new VEPixel(0, startpix);
                        startPoint = map.PixelToLatLong(pixel);
                        lineCoordinates = [
                            startPoint,
                            endPoint];
                        var linecolor = connectLineColour.substring(connectLineColour.length, connectLineColour.length - 6);
                        var converter = new VEHexStringToColor();
                        var veColor = converter.Convert(linecolor);
                        veColor.A = connectLineOpacity;
                        var width = connectLineWeight;
                        connectLineID = Math.floor(Math.random() * 20);
                        var connectLine = new VEPolyline(connectLineID, lineCoordinates, veColor, width);
                        map.AddPolyline(connectLine);
                        connectLineArray.push(connectLine);
                    }
                }
            }
            else if (MapEng == "Google_Maps") {
                try {
                    if (connectLineArray) {
                        for (i in connectLineArray) {
                            connectLineArray[i].setMap(null);
                        }
                        connectLineArray = [];
                    }
                    zoomLevel = map.getZoom();
                    var overlay = new google.maps.OverlayView();
                    overlay.draw = function () { };
                    overlay.setMap(map);
                    startpix = connectLineStartPoint;
                    startPoint = overlay.getProjection().fromContainerPixelToLatLng(new google.maps.Point(0, startpix));
                    for (var i = 0; i < Markers.length; i++) {
                        if (Markers[i].title == thisreg) {
                            endPoint = Markers[i].position;
                            lineCoordinates = [
                                startPoint,
                                endPoint];
                            connectLine = new google.maps.Polyline({
                                path: lineCoordinates,
                                strokeColor: connectLineColour,
                                strokeOpacity: connectLineOpacity,
                                strokeWeight: connectLineWeight
                            });
                            connectLine.setMap(map);
                            connectLineArray.push(connectLine);
                        }
                    }
                } catch (Error) {
                }
            }
        }
    }
}

function DisplayVehicleInfo(thisreg) {
    try {
        tvVehicles_contextMenu.removeItem("toggleOutput");
        tvVehicles_contextMenu.removeItem("sep_5");
    }
    catch (Error) {
    }
    if (thisreg !== "") {
        for (var i = 0; i < CurrentVehicleArray.length; i++) {
            if (CurrentVehicleArray[i][0] == thisreg || CurrentVehicleArray[i][1] == thisreg) {
                if (DisplayOption == "ByReg") {
                    dgVehicleDetails.cells(1, 1).setValue(CurrentVehicleArray[i][0]);
                }
                else if (DisplayOption == "BothByReg") {
                    dgVehicleDetails.cells(1, 1).setValue(CurrentVehicleArray[i][0] + " / " + CurrentVehicleArray[i][1]);
                }
                else if (DisplayOption == "ByDesc") {
                    dgVehicleDetails.cells(1, 1).setValue(CurrentVehicleArray[i][1]);
                }
                else if (DisplayOption == "BothByDesc") {
                    dgVehicleDetails.cells(1, 1).setValue(CurrentVehicleArray[i][1] + " / " + CurrentVehicleArray[i][0]);
                }
                dgVehicleDetails.cells(2, 1).setValue(CurrentVehicleArray[i][4]);
                dgVehicleDetails.cells(3, 1).setValue(CurrentVehicleArray[i][5]);
                dgVehicleDetails.cells(4, 1).setValue(addCommas(CurrentVehicleArray[i][11]));
                dgVehicleDetails.cells(5, 1).setValue(CurrentVehicleArray[i][9]);
                dgVehicleDetails.cells(6, 1).setValue(CurrentVehicleArray[i][36]);
                dgVehicleDetails.cells(8, 1).setValue(CurrentVehicleArray[i][6]);
                try {
                    dgVehicleDetails.deleteRow("Outp1");
                }
                catch (Error) {
                }
                try {
                    if (CurrentVehicleArray[i][40] == "True") {
                        dgVehicleDetails.addRow("Outp1", CurrentVehicleArray[i][41]);
                        dgVehicleDetails.cells("Outp1", 0).setFont("Tahoma-bold-8pt");
                        dgVehicleDetails.cells("Outp1", 0).setValue(CurrentVehicleArray[i][41]);
                        dgVehicleDetails.cells("Outp1", 1).setHorAlign("center");
                        if (CurrentVehicleArray[i][42] == "1") {
                            dgVehicleDetails.cells("Outp1", 1).setValue("Off");
                        }
                        else {
                            dgVehicleDetails.cells("Outp1", 1).setValue("On");
                        }
                        tvVehicles_contextMenu.addNewSeparator("pollAll", "sep_5");
                        if (CurrentVehicleArray[i][42] == "1") {
                            tvVehicles_contextMenu.addNewSibling("sep_5", "toggleOutput", "Set " + CurrentVehicleArray[i][41] + " On", false);
                        }
                        else {
                            tvVehicles_contextMenu.addNewSibling("sep_5", "toggleOutput", "Set " + CurrentVehicleArray[i][41] + " Off", false);
                        }
                    }
                    else {
                        dgVehicleDetails.deleteRow("Outp1");
                        tvVehicles_contextMenu.removeItem("sep_5");
                    }
                }
                catch (Error) {
                    window.alert(Error.Message);
                }
                try {
                    dgVehicleDetails.deleteRow("TempProbe1");
                }
                catch (Error) {
                }
                try {
                    if (CurrentVehicleArray[i][43] == "True") {
                        dgVehicleDetails.addRow("TempProbe1", CurrentVehicleArray[i][44]);
                        dgVehicleDetails.cells("TempProbe1", 0).setFont("Tahoma-bold-8pt");
                        dgVehicleDetails.cells("TempProbe1", 0).setValue(CurrentVehicleArray[i][44]);
                        dgVehicleDetails.cells("TempProbe1", 1).setHorAlign("center");
                        if (CurrentVehicleArray[i][45] >= CurrentVehicleArray[i][46] || CurrentVehicleArray[i][45] <= CurrentVehicleArray[i][47]) {
                            dgVehicleDetails.cells("TempProbe1", 1).setTextColor("red");
                        }
                        else {
                            dgVehicleDetails.cells("TempProbe1", 1).setTextColor("green");
                        }
                        dgVehicleDetails.cells("TempProbe1", 1).setValue(CurrentVehicleArray[i][45]);
                    }
                } catch (e) {

                }
                try {
                    dgVehicleDetails.deleteRow("TempProbe2");
                }
                catch (Error) {
                }
                try {
                    if (CurrentVehicleArray[i][48] == "True") {
                        dgVehicleDetails.addRow("TempProbe2", CurrentVehicleArray[i][49]);
                        dgVehicleDetails.cells("TempProbe2", 0).setFont("Tahoma-bold-8pt");
                        dgVehicleDetails.cells("TempProbe2", 0).setValue(CurrentVehicleArray[i][49]);
                        dgVehicleDetails.cells("TempProbe2", 1).setHorAlign("center");
                        if (CurrentVehicleArray[i][50] >= CurrentVehicleArray[i][51] || CurrentVehicleArray[i][50] <= CurrentVehicleArray[i][52]) {
                            dgVehicleDetails.cells("TempProbe2", 1).setTextColor("red");
                        }
                        else {
                            dgVehicleDetails.cells("TempProbe2", 1).setTextColor("green");
                        }

                        dgVehicleDetails.cells("TempProbe2", 1).setValue(CurrentVehicleArray[i][50]);
                    }
                } catch (e) {

                }
            }
        }
    }
}
function tvVehiclesItemMouseOut() {
    if (MapEng == "Google_Maps") {
        try {
            if (connectLineArray) {
                for (i in connectLineArray) {
                    connectLineArray[i].setMap(null);
                }
                connectLineArray = [];
            }
        }
        catch (Error) {
            alert("Error 3904: " + Error.Message);
        }
    }
    else {
        try {
            if (connectLineID) {
                map.DeletePolyline(connectLineID);
                connectLineID = null;
            }
        }
        catch (Error) {
        }
    }
}
function dgVehPollVehicle() {
    try {
        var thisrow = dgVehicles.getSelectedRowId() - 1;
        var thisVehicle;
        if (DisplayOption == "ByDesc") {
            thisVehicle = CurrentVehicleArray[thisrow][1];
        }
        else {
            thisVehicle = CurrentVehicleArray[thisrow][0];
        }
        document.getElementById('hiddenSelectedVehicle').value = thisVehicle;
        document.getElementById("btnPollFromVehGrid").click();
    }
    catch (Error) {
        DisplayNoRowSelectedDialog();
    }
}
function FormatLatLong(TheNumber, LatLong, MsgType) {
    //   The Number is either Latitude or Longitude in decimal degrees
    //   LatLong:    0 => Latitude, 1 => Longitude
    //   MsgType:    1 => Decimal degrees
    //               2 => Degrees, minutes, seconds
    //               3 => Degress and decimal minutes
    try {
        var TheSign;
        var tmpstr;
        var TheDeg;
        var MinInt;
        var SecInt;
        var tmp;
        var tmp1;
        //  var tmp2;
        var DecDeg;
        var MinDec;
        var ChrDeg = String.fromCharCode(176);
        var ChrMin = String.fromCharCode(39);
        var ChrSec = String.fromCharCode(34);
        if (LatLong === 0) {
            TheSign = "N";
            if (TheNumber < 0) {
                TheSign = "S";
            }
        }
        else {
            TheSign = "E";
            if (TheNumber < 0) {
                TheSign = "W";
            }
        }
        TheNumber = Math.abs(TheNumber);
        TheDeg = parseInt(Math.floor(TheNumber), 10);             // Degrees
        MinDec = (TheNumber - TheDeg) * 60;         // Minutes - decimal
        MinInt = parseInt(Math.floor(MinDec), 10);      // Minute integer
        tmp = (MinDec - MinInt) * 60;
        SecInt = parseInt(tmp, 10);                               // Seconds integer

        //Now format the result
        switch (MsgType) {
            case 1:             // Decimal Degrees
                tmp = TheNumber * 100000;
                tmp1 = parseInt(tmp, 10);
                DecDeg = tmp1 / 100000;
                tmpstr = DecDeg;

                return tmpstr + " " + TheSign + " ";
                break;

            case 2:             // Degrees Minutes & Seconds
                var strDeg = TheDeg.toString();
                var strMin = MinInt.toString();
                var strSec = SecInt.toString();
                if (strDeg.length < 2) {
                    strDeg = "0" + strDeg;
                }
                if (strMin.length < 2) {
                    strMin = "0" + strMin;
                }
                if (strSec.length < 2) {
                    strSec = "0" + strSec;
                }

                return (strDeg + ChrDeg + " " + strMin + ChrMin + " " + strSec + ChrSec + " " + TheSign + " ");
                break;

            case 3:             //Degrees, Minutes and decimal minutes
                strDeg = TheDeg.toString();
                strMin = MinInt.toString();
                if (strDeg.length < 2) {
                    strDeg = "0" + strDeg;
                }
                if (strMin.length < 2) {
                    strMin = "0" + strMin;
                }
                //                tmp = (MinDec - MinInt) * 100;
                //                tmp1 = parseInt(tmp, 10);
                //                tmp2 = tmp1 / 100;

                return (strDeg + ChrDeg + " " + strMin + " " + TheSign + " ");
                break;
            default:
                break;
                return false;
        }
    }
    catch (Error) {
        window.alert("Error 2711: " + Error.message);
    }
}

function trim(str) {
    if (!str || typeof str != 'string')
        return null;
    return str.replace(/^[\s]+/, '').replace(/[\s]+$/, '').replace(/[\s]{2,}/, ' ');
}

