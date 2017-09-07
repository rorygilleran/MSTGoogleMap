//*****************************************************************************************
// File Name:       MST_Routes.js
// Author:          Rory Gilleran 
// Copyright:       Business Information System Ltd 2011
// Last Updated:    02nd January 2016
// Notes:           Copied from MSTW_Routes.js in MS Track Web 8
//*****************************************************************************************

var map = null;
var MapEng;
var newRoutesArray = new Array();
var tmpRoutesArray = new Array();
var NoofWayPoints = 0;
var TurnsArray = new Array(3);
var shape = null;
var routes = new Array();
var CurrentVehicleArray = new Array();
var NumofRoutes = 0;
var VE_Map_height;
var VE_Map_width;
var zoomLevel = 15;
var thisaddress = null;
var dgRoutes;
var dgNewRoutes;
var dgNewRoutes_contextMenu;
var routes_TabBar;
var lastActiveTab;
var RoutesMap_contextMenu;
var MsgWin;
var DisplayMsgWin;
var dgNewGeofence_Routes;
var dgRouteSetup;
var newLat = 0;
var newLong = 0;
var DisplayType;
var screen_avail_height;
var screen_avail_width;
var browserName = "Unknown";
var dhxWins;
var DisplayWin;
var Company;
var Fleet_Code;
var SiteArray = [];
var distUnits;
var latlngbounds;
var myRoute;
var directionsDisplay;
var directionsService;
var isModifyRoute = false;

function initRoutesTabbar() {
    routes_TabBar = dhxRouteLayout.cells("b").attachTabbar();
    routes_TabBar.setImagePath("codebase/imgs/");
    routes_TabBar.setStyle("modern");
    var clientID = document.getElementById("hiddenClientID").value;
    var tabbar_xmlFile = "xmlFiles/" + clientID + "_RoutesTabbar.xml?etc=" + new Date().getTime();
    routes_TabBar.loadXML(tabbar_xmlFile, DisplayTabbar);
}
function DisplayTabbar() {
    routes_TabBar.setTabActive("Routes_Map", true);
    routes_TabBar.hideTab("Routes_Geofence");
    routes_TabBar.enableAutoReSize(true, true);
    routes_TabBar.enableScroll(false);
    routes_TabBar.attachEvent("onSelect", CheckRoutesTabActive);
}
function doInitGrid() {
    try {
        dgRoutes = new dhtmlXGridObject('routes_Turns_Container');
        dgRoutes.setImagePath("codebase/imgs/");
        var tmpHeader = document.getElementById("hiddenTurnGridHeader").value;
        dgRoutes.setHeader(tmpHeader, null, ["text-align:center;"]);
        dgRoutes.setInitWidths("150,*,100,100");
        dgRoutes.enableTooltips("false");
        dgRoutes.setColAlign("center,left,center,center");
        dgRoutes.setSkin("modern");
        dgRoutes.attachEvent("onRowSelect", GetLocationFromGrid);
        dgRoutes.attachEvent("onMouseOver", MouseOver);
        dgRoutes.init();
        dgRoutes.enableSmartRendering(true); //
    } catch (error) {
        window.alert("Error 65: " + error.message);
    }
}
function doinitNewRouteGrid() {
    try {

        dgNewRoutes_contextMenu = new dhtmlXMenuObject(null, "dhx_blue");
        dgNewRoutes_contextMenu.setImagePath("../../../dhtmlxMenu/codebase/imgs/");
        dgNewRoutes_contextMenu.setIconsPath("../images/");
        dgNewRoutes_contextMenu.renderAsContextMenu();
        dgNewRoutes_contextMenu.setOpenMode("web");
        dgNewRoutes_contextMenu.attachEvent("onClick", ShowRouteGridContextMenu);
        var clientID = document.getElementById("hiddenClientID").value;
        var RouteGrid_xmlFile = "xmlFiles/" + clientID + "_RouteGridcontextMenu.xml?etc=" + new Date().getTime();
        dgNewRoutes_contextMenu.loadXML(RouteGrid_xmlFile);

        dgNewRoutes = dhxRouteLayout1.cells("b").attachGrid();
        dgNewRoutes.setImagePath("codebase/imgs/");
        dgNewRoutes.setHeader("");
        dgNewRoutes.setNoHeader(true);
        dgNewRoutes.setInitWidthsP("100");
        dgNewRoutes.enableDragAndDrop(true);
        //dgNewRoutes.enableLightMouseNavigation(true);
        dgNewRoutes.enableTooltips("false");
        dgNewRoutes.setColAlign("center");
        dgNewRoutes.setSkin("modern");
        dgNewRoutes.enableContextMenu(dgNewRoutes_contextMenu);
        dgNewRoutes.init();
        dgNewRoutes.preventIECaching(true);
        //dgNewRoutes.addRow(dgNewRoutes.uid(), "Routes",1);
        var clientID = document.getElementById("hiddenClientID").value;
        var settings_xmlFile = "xmlFiles/" + clientID + "_NewRouteGrid.xml?etc=" + new Date().getTime();
        dgNewRoutes.loadXML(settings_xmlFile);
    }
    catch (Error) {
        window.alert("Error 89: " + Error.message);
    }
}
function CheckRoutesTabActive(idn, ido) {
    if (idn == "Routes_Turns") {
        DisplayRouteTurns();
    }
    else if (idn == "Routes_Help") {
        LoadHelpPage();
    }

    return true;
}
function ShowRouteGridContextMenu(id) {
    switch (id) {
        case 'RouteUp':
            cMenu_gridRoutesUp();
            break;
        case 'RouteDown':
            cMenu_gridRoutesDown();
            break;
        case 'RouteDelete':
            cMenu_gridRoutesDelete();
            break;
        case 'createGeofence':
            cMenu_gridRoutesCreateGeofence();
            break;
        case 'sendToGarmin':
            cMenu_gridRoutesSendLocToGarmin();
            break;
    }

}
function CheckSelectedValue(stage, rowid, cellid, newvalue) {
    if (stage == 2) {
        if (rowid == 6 && cellid == 1) {
            if (newvalue == 11) {
                GetGeoFenceValue();
            }
        }
        if (rowid == 7 && cellid == 1) {
            if (newvalue == 3 || newvalue == 4) {
                window.setTimeout(function () {
                    dgNewGeofence_Routes.setRowHidden(8, false);   //make the row visib;e
                    dgNewGeofence_Routes.selectCell(7, 1); //rowid, collid - select the cell
                    dgNewGeofence_Routes.editCell();        // make edit mode.
                }, 1);
            }
        }
        return newvalue;
    }
}
function GetGeoFenceValue() {
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("codebase/imgs/");
    MsgWin.setSkin("dhx_skyblue");
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 365, 100);
    //MsgWin.window("DisplayMessage").center();
    MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();

    DisplayMsgWin.setText("Enter new Geofence radius.");
    DisplayMsgWin.attachObject("Message_Container_1");
    DisplayMsgWin.centerOnScreen()
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();
    document.getElementById("txtNewRadius").focus();
}
function EndMsgDisplay_Radius() {
    try {
        if (document.getElementById("txtNewRadius").value > 0) {
            var newRadius = document.getElementById("txtNewRadius").value;
            document.getElementById("hiddenNewGeofenceRadius").value = newRadius;
            var combo = dgNewGeofence_Routes.getCustomCombo(7, 1);
            combo.put(12, newRadius);
            dgNewGeofence_Routes.cells(6, 1).setValue(newRadius);
        }
        DisplayMsgWin.setModal(false);
        DisplayMsgWin.hide();
    }
    catch (Error) {
        window.alert("Unable to close message - " + Error.message);
    }
}
function LoadHelpPage() {
    //  This function gets the requested Help page and displays it in the right hand td
    try {
        var FullPath = "Help/Routes.htm";
        document.getElementById('HelpContent').src = FullPath;
    }
    catch (Error) {
        window.alert("Error 128: " + Error.message);
    }
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
function ShowEmailResults(msg, emailAddress) {
    var dialog = document.getElementById("dialog_Emailcontent");
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("codebase/imgs/");
    MsgWin.setSkin("dhx_skyblue");
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 250, 110);
    //MsgWin.window("DisplayMessage").center();
    MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();
    var tmpWinText = document.getElementById("hiddenEmailSent").value;
    DisplayMsgWin.setText(tmpWinText);
    DisplayMsgWin.attachObject("Email_Sent_Container");
    DisplayMsgWin.centerOnScreen()
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();
    if (msg == "Sent") {
        lblEmailSent.value = document.getElementById("hiddenEmailSentto").value + " " + emailAddress;
    }
    else {
        lblEmailSent.value = document.getElementById("hiddenSendEmailFailed").value + " " + emailAddress;
    }
    return false;
}
function EmailSent() {
    DisplayMsgWin.setModal(false);
    DisplayMsgWin.hide();
}
function MouseOver(rowID, cell) {
    dgRoutes.selectRow(rowID - 1, false, false, false);
}
function GetLocationFromGrid(rowID, cell) {
    //  This function gets the details for the selected vehicle
    //  in the current vehicle grid and displays them on the map
    if (MapEng == "MS_VirtualEarth") {
        try {
            map.DeleteShape(shape);;
        } catch (Error) {
        }
        routes_TabBar.setTabActive("Routes_Map", true);
        j = rowID;
        var pinLatLong = new VELatLong(TurnsArray[j][1], TurnsArray[j][2]);
        shape = new VEShape(VEShapeType.Pushpin, pinLatLong);
        var sessFolderArrows = "Images/Arrows/";
        var icon = new VECustomIconSpecification();
        icon.Image = sessFolderArrows + "MovingCircle.gif";
        icon.ImageOffset = new VEPixel(-7, -7);
        icon.TextContent = " ";
        shape.SetCustomIcon(icon);
        map.AddShape(shape);
    } else if (MapEng == "Google_Maps") {
        routes_TabBar.setTabActive("Routes_Map", true);
        j = rowID - 1;
        var loc = new google.maps.LatLng(parseFloat(TurnsArray[j][1]), parseFloat(TurnsArray[j][2]));
        var sessFolderArrows = "Images/Arrows/";
        var myIcon = sessFolderArrows + "HisBlueSouth.gif";
        var marker = new google.maps.Marker({
            position: loc,
            map: map,
            icon: myIcon
        });
        marker.setMap(map);
    }
}
function cMenu_gridRoutesUp() {
    try {
        var thisIndex = dgNewRoutes.getSelectedId();
        dgNewRoutes.moveRow(thisIndex, "up");
        tmpRoutesArray = moveUp(tmpRoutesArray, thisIndex);
    }
    catch (Error) {
        window.alert("WayPoint Up :" + Error.message);
    }
}
function cMenu_gridRoutesDown() {
    try {

        dgNewRoutes.moveRow(thisIndex, "down");
        tmpRoutesArray = moveUp(tmpRoutesArray, thisIndex);
    }
    catch (Error) {
        window.alert("WayPoint Down :" + Error.message);
    }
}
function moveDown(modArray, index) {

    if (index == 0) { return modArray; }

    modArray.splice(index - 1, 2, modArray[index], modArray[index - 1]);

    return modArray;
}
function moveUp(modArray, index) {

    if (index > modArray.length - 2) { return modArray; }

    modArray.splice(index, 2, modArray[index + 1], modArray[index]);

    return modArray;
}
function cMenu_gridRoutesDelete() {
    try {
        var thisIndex = dgNewRoutes.getSelectedId();
        if (thisIndex != -1) {
            tmpRoutesArray.splice(thisIndex, 1);
            dgNewRoutes.deleteRow(thisIndex);
            NoofWayPoints = NoofWayPoints - 1;
        }
        else {
            window.alert('Please select (left click) location to delete');
        }
    }
    catch (Error) {
        window.alert("WayPoint Delete :" + Error.message);
    }
}
function listbox_move(listID, direction) {

    var listbox = document.getElementById(listID);
    var selIndex = listbox.selectedIndex;

    if (-1 == selIndex) {
        alert("Please select (left click) location to move.");
        return;
    }

    var increment = -1;
    if (direction == 'up')
        increment = -1;
    else
        increment = 1;

    if ((selIndex + increment) < 0 ||
		(selIndex + increment) > (listbox.options.length - 1)) {
        return;
    }

    var selValue = listbox.options[selIndex].value;
    var selText = listbox.options[selIndex].text;
    listbox.options[selIndex].value = listbox.options[selIndex + increment].value
    listbox.options[selIndex].text = listbox.options[selIndex + increment].text

    listbox.options[selIndex + increment].value = selValue;
    listbox.options[selIndex + increment].text = selText;

    listbox.selectedIndex = selIndex + increment;

}
function cMenu_gridRoutesSendLocToGarmin() {
    try {
        var thisIndex = dgNewRoutes.getSelectedId();
        if (thisIndex != -1) {
            var thisRoute = tmpRoutesArray[thisIndex].split("|");
            document.getElementById("txtSite").value = thisRoute[0];
            document.getElementById("txtLat").value = parseFloat(thisRoute[1]).toFixed(5);
            document.getElementById("txtLong").value = parseFloat(thisRoute[2]).toFixed(5);
            dhxWins = new dhtmlXWindows();
            dhxWins.setImagePath("../../../dhtmlxMenu/codebase/imgs/");
            DisplayWin = null;
            DisplayWin = dhxWins.createWindow("w1", 55, 55, 275, 175);
            dhxWins.window("w1").button("close").hide();
            dhxWins.window("w1").button("park").hide();
            dhxWins.window("w1").button("minmax1").hide();
            DisplayWin.setText("Send location to Garmin PND");
            DisplayWin.attachObject("SendLocToGarminPND_container");
            DisplayWin.centerOnScreen()
            DisplayWin.setModal(true);
            DisplayWin.show();
        }
        else {
            window.alert('Please select (left click) location to send to Garmin PND');
        }
    }
    catch (Error) {

    }

}
function SendGarminLocation() {
    var IndexValue = document.getElementById("ddlGarminPNDS").selectedIndex;
    var SelectedVal = document.getElementById("ddlGarminPNDS").options[IndexValue].value;
    var tmpSite = document.getElementById("txtSite").value;
    var tmpLat = document.getElementById("txtLat").value;
    var tmpLong = document.getElementById("txtLong").value;
    document.getElementById("hiddenSelectedGarminPND").value = SelectedVal + "/" + tmpSite + "/" + tmpLat + "/" + tmpLong;
    document.getElementById("hiddenButtonSendLoctoGarminPND").click();
    DisplayWin.setModal(false);
    DisplayWin.hide();
}
function CancelSendGarminLocation() {
    DisplayWin.setModal(false);
    DisplayWin.hide();
}
function cMenu_gridRoutesCreateGeofence() {
    try {
        var thisIndex = dgNewRoutes.getSelectedId();
        if (thisIndex != -1) {
            var thisRoute = tmpRoutesArray[thisIndex].split('|');
            if (tmpRoutesArray.length > 0) {
                lastActiveTab = routes_TabBar.getActiveTab();
                routes_TabBar.showTab("Routes_Geofence");
                routes_TabBar.setTabActive("Routes_Geofence");
                routes_TabBar.showTab("Routes_Geofence");
                routes_TabBar.setTabActive("Routes_Geofence");
                CreateNewGeofence(thisRoute[1], thisRoute[2], thisRoute[0]);
            }
        }
        else {
            window.alert('Please select (left click) location to add a geofence');
        }
    }
    catch (Error) {
        window.alert("WayPoint Create geofence :" + Error.message);
    }
}
function CreateNewGeofence(geoLat, geoLong, geoLoc) {
    try {
        document.getElementById("hiddenNewGeofenceRadius").value = 0;
        newLat = geoLat;
        newLong = geoLong;
        SetUpCells();
    }
    catch (Error) {
        window.alert("Error 227: CreateNewGeofence:-  " + Error.message);
    }
}
function SetUpCells() {
    try {
        dgNewGeofence_Routes.cells(1, 1).setValue("");
        dgNewGeofence_Routes.cells(2, 1).setValue("");
        dgNewGeofence_Routes.cells(3, 1).setValue(1);
        dgNewGeofence_Routes.cells(4, 1).setValue(newLat);
        dgNewGeofence_Routes.cells(5, 1).setValue(newLong);
        dgNewGeofence_Routes.cells(6, 1).setValue(1);
        dgNewGeofence_Routes.cells(7, 1).setValue(1);
        dgNewGeofence_Routes.cells(8, 1).setValue("");
        dgNewGeofence_Routes.selectCell(0, 1);
        dgNewGeofence_Routes.editCell();
        dgNewGeofence_Routes.setRowHidden(8, "true");
    }
    catch (Error) {
        window.alert("Error 470: CreateNewGeofence:-  " + Error.message);
    }
} function SaveGeofence() {
    if (dgNewGeofence_Routes.cells(1, 1).getValue() == "") {
        window.alert(document.getElementById("hiddenGeoCodeBlank").value);
        return false;
    }
    if (dgNewGeofence_Routes.cells(2, 1).getValue() == "") {
        window.alert(document.getElementById("hiddenGeoNameBlank").value);
        return false;
    }
    if (dgNewGeofence_Routes.cells(3, 1).getValue() == "") {
        window.alert(document.getElementById("hiddenGeoFleetBlank").value);
        return false;
    }
    if (dgNewGeofence_Routes.cells(7, 1).getValue() == 3) {
        if (dgNewGeofence_Routes.cells(8, 1).getValue() <= "0") {
            window.alert(document.getElementById("hiddenGeoMaxTimeInvalid").value);
            return false;
        }
    }
    dgNewGeofence_Routes.setCSVDelimiter("|");
    dgNewGeofence_Routes.csv.row = "~";
    dgNewGeofence_Routes.enableCSVAutoID(true);
    document.getElementById("hiddenNewGeofence").value = dgNewGeofence_Routes.serializeToCSV();
    document.getElementById("hiddenButtonSaveGeofence").click();
    routes_TabBar.hideTab("Routes_Geofence");
    routes_TabBar.setTabActive(lastActiveTab);


}
function CancelGeofences() {
    dgNewGeofence_Routes.clearAll();
    routes_TabBar.hideTab("Routes_Geofence");
    routes_TabBar.setTabActive(lastActiveTab);
}
function CallPrint(strid) {
    dgRoutes.printView();
}
function CallSendEmail() {
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("codebase/imgs/");
    MsgWin.setSkin("dhx_skyblue");
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 365, 110);
    //MsgWin.window("DisplayMessage").center();
    MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();
    var tmpWinText = document.getElementById("hiddenWinText").value;
    DisplayMsgWin.setText(tmpWinText);
    DisplayMsgWin.attachObject("Email_Address_Container");
    DisplayMsgWin.centerOnScreen();
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();
    document.getElementById("txtEmailAddress").focus();
    return false;
}
function GetMapLanguage() {
    try {
        var currentLang = document.getElementById("hiddenLangPref").value;
        var tag = document.createElement("script");
        tag.type = "text/javascript";
        switch (currentLang) {
            case 'es-MX':
                tag.src = "http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2&mkt=es-MX";
                break;
            case 'pt-PT':
                tag.src = "http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2&mkt=pt-PT";
                break;
            default:
                tag.src = "http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2";
                break;
        }
        document.body.appendChild(tag);
    }
    catch (Error) {
        window.alert("Error 381: SetupMap:-  " + Error.message);
    }
}
function waitPreloadPage() { //DOM
    GetThisBrowserType();
    GetMapLanguage();
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
    SetupRouteLayout();
    initRoutesTabbar();
    doInitGrid();
    MapEng = document.getElementById("hiddenMapEng").value;
    if (MapEng == "Google_Maps") {
        createGoogleScript();
    }
    Company = document.getElementById("hidden_Company").value;
    Fleet_Code = document.getElementById("hidden_FleetCode").value;
    DisplayType = document.getElementById("hiddenDisplayType").value;
    var tmpGeoFenceArrayStr = document.getElementById("hiddenGeofenceArray").value;
    BuildGeoFenceArray(tmpGeoFenceArrayStr);
    loadSiteArray();
    var tmpInArrayStr = document.getElementById("hiddenCurrentVehArray").value;
    BuildCurrentVehiclesArray(tmpInArrayStr);
    LoadRouteSetupGrid();
    LoadComplete();
    dhxRouteLayout.attachEvent("onPanelResizeFinish", function () {
        var a_height = dhxRouteLayout.cells("a").getHeight();
        var a_width = dhxRouteLayout.cells("a").getWidth();
        var b_height = dhxRouteLayout.cells("b").getHeight();
        var b_width = dhxRouteLayout.cells("b").getWidth();
        var c_height = dhxRouteLayout.cells("c").getHeight();
        var c_width = dhxRouteLayout.cells("c").getWidth();
        var RoutesPanels = a_height + "," + a_width + "," + b_height + "," + b_width + "," + c_height + "," + c_width;
        createCookie("MSTW8_RouteLayout", RoutesPanels, 30);
    });
    dhxRouteLayout1.attachEvent("onPanelResizeFinish", function () {
        var a_height = dhxRouteLayout1.cells("a").getHeight();
        var a_width = dhxRouteLayout1.cells("a").getWidth();
        var b_height = dhxRouteLayout1.cells("b").getHeight();
        var b_width = dhxRouteLayout1.cells("b").getWidth();
        var RoutesPanels = a_width + "," + a_height + "," + b_width + "," + b_height;
        createCookie("MSTW8_RouteLayout_1", RoutesPanels, 30);
    });
    doinitNewRouteGrid();
    document.getElementById("loading").className = "loading-invisible";
}
function loadSiteArray() {
    var tmpSiteArrayStr = document.getElementById("hiddenSitesArray").value;
    BuildSiteArray(tmpSiteArrayStr);
}
function BuildSiteArray(tmpArrayStr) {
    try {
        var tmpArray = new Array();
        tmpArray = tmpArrayStr.split('~');
        SiteArray = [];
        for (var i = 0; i < tmpArray.length - 1; i++) {
            SiteArray[i] = tmpArray[i].split(',');
        }
    }
    catch (Error) {
        window.alert("Build Site Array Error: " + Error.message);
    }
}
function SetupRouteLayout() {
    eraseCookie("MSTW8_RouteLayout");
    dhxRouteLayout = null;
    dhxRouteLayout1 = null;
    dhxRouteLayout = new dhtmlXLayoutObject("Route_Parent", "3J", "dhx_blue");

    var tmpstr = readCookie("MSTW8_RouteLayout");
    if (tmpstr != "") {
        var RoutesLayoutSizes = readCookie("MSTW8_RouteLayout").split(",");
        dhxRouteLayout.cells("a").setHeight(RoutesLayoutSizes[0]);
        dhxRouteLayout.cells("a").setWidth(RoutesLayoutSizes[1]);
        dhxRouteLayout.cells("a").hideHeader();
        dhxRouteLayout.cells("b").hideHeader();
        dhxRouteLayout.cells("b").setHeight(RoutesLayoutSizes[2]);
        dhxRouteLayout.cells("b").setWidth(RoutesLayoutSizes[3]);
        dhxRouteLayout.cells("c").setHeight(RoutesLayoutSizes[4]);
        dhxRouteLayout.cells("c").setWidth(RoutesLayoutSizes[5]);
        dhxRouteLayout.cells("c").hideHeader();
    }
    else {
        dhxRouteLayout.cells("a").setWidth(screen_avail_width * 0.2);
        dhxRouteLayout.cells("a").setHeight(screen_avail_height * 0.5);
        dhxRouteLayout.cells("a").hideHeader();
        dhxRouteLayout.cells("b").hideHeader();
        dhxRouteLayout.cells("b").setWidth(screen_avail_width * 0.8);
        dhxRouteLayout.cells("b").setHeight(screen_avail_height * 0.9);
        dhxRouteLayout.cells("c").setWidth(screen_avail_width * 0.2);
        dhxRouteLayout.cells("c").setHeight(screen_avail_height * 0.5);
        dhxRouteLayout.cells("c").hideHeader();
    }
    dhxRouteLayout1 = new dhtmlXLayoutObject(dhxRouteLayout.cells("a"), "2E");
    var tmpstr1 = readCookie("MSTW8_RouteLayout_1");
    if (tmpstr1 != "") {
        var Routes1_LayoutSizes = readCookie("MSTW8_RouteLayout_1").split(",");
        dhxRouteLayout1.cells("a").attachObject("TableLeft_Top");
        dhxRouteLayout1.cells("a").setHeight(Routes1_LayoutSizes[1]);
        dhxRouteLayout1.cells("a").hideHeader();
        dhxRouteLayout1.cells("a").fixSize(true, false);
        dhxRouteLayout1.cells("b").setHeight(Routes1_LayoutSizes[3]);
        dhxRouteLayout1.cells("b").hideHeader();
    }
    else {
        dhxRouteLayout1.cells("a").attachObject("TableLeft_Top");
        dhxRouteLayout1.cells("a").setWidth(screen_avail_width * 0.2);
        dhxRouteLayout1.cells("a").setHeight(screen_avail_height * 0.02);
        dhxRouteLayout1.cells("a").hideHeader();
        dhxRouteLayout1.cells("a").fixSize(true, false);
        dhxRouteLayout1.cells("b").setWidth(screen_avail_width * 0.2);
        dhxRouteLayout1.cells("b").setHeight(screen_avail_height * 0.5);
        dhxRouteLayout1.cells("b").hideHeader();
    }

}
function createCookie(c_name, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) +
((expiredays == null) ? "" : ";expires=" + exdate.toUTCString());
}
function readCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) c_end = document.cookie.length;
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}
function eraseCookie(name) {
    createCookie(name, "", -1);
}
function BuildCurrentVehiclesArray(tmpArrayStr) {
    CurrentVehicleArray = [];
    CurrentVehicleArray = $.parseJSON(tmpArrayStr);
}
function PageRefresh() {
    dgRouteSetup.clearAll();
    var tmpGeoFenceArrayStr = document.getElementById("hiddenGeofenceArray").value;
    BuildGeoFenceArray(tmpGeoFenceArrayStr);
    LoadRouteSetupGrid();
}
function LoadComplete() {
    dgNewGeofence_Routes = new dhtmlXGridObject("New_Geofence_Container_1");
    dgNewGeofence_Routes.setImagePath("codebase/imgs/");
    dgNewGeofence_Routes.setHeader("Option,Value", null, ["text-align:center;"]);
    dgNewGeofence_Routes.setInitWidths("150,190");
    dgNewGeofence_Routes.enableTooltips("true");
    dgNewGeofence_Routes.setColAlign("left,center");
    dgNewGeofence_Routes.setSkin("modern");
    dgNewGeofence_Routes.enableLightMouseNavigation("true");
    dgNewGeofence_Routes.attachEvent("onEditCell", CheckSelectedValue);
    dgNewGeofence_Routes.init();
    dgNewGeofence_Routes.preventIECaching(true);
    IsSetupGeofenceGrid = true;
    var clientID = document.getElementById("hiddenClientID").value;
    var settings_xmlFile = "xmlFiles/" + clientID + "_NewGeofence.xml?etc=" + new Date().getTime();
    dgNewGeofence_Routes.loadXML(settings_xmlFile);
}
function LoadRouteSetupGrid() {
    //dgRouteSetup = new dhtmlXGridObject('RouteSettings_container');
    dgRouteSetup = dhxRouteLayout.cells("c").attachGrid();
    dgRouteSetup.setImagePath("codebase/imgs/");
    var tmpHeader = document.getElementById("hiddenRoutesHeader").value;
    dgRouteSetup.setHeader(tmpHeader + ",#cspan", null, ["text-align:center;"]);
    dgRouteSetup.setInitWidthsP("45,55");
    dgRouteSetup.enableLightMouseNavigation("true");
    dgRouteSetup.enableTooltips("false,true");
    dgRouteSetup.enableMultiline(true);
    dgRouteSetup.enableCollSpan(true);
    dgRouteSetup.setColAlign("left,center");
    dgRouteSetup.setSkin("modern");
    dgRouteSetup.attachEvent("onEditCell", GetItemForRoute);
    //dhtmlxError.catchError("ALL", myErrorHandler);
    dgRouteSetup.init();
    dgRouteSetup.preventIECaching(true);
    var clientID = document.getElementById("hiddenClientID").value;
    var settings_xmlFile = "xmlFiles/" + clientID + "_Routes.xml?etc=" + new Date().getTime();
    dgRouteSetup.loadXML(settings_xmlFile);

}
function GetItemForRoute(stage, rowid, cellid, newvalue, oldvalue) {
    if (rowid == 6 && cellid == 0) {
        return false;
    }
    if (stage == 2) {
        var Route_waypoint;
        var Route_lat;
        var Route_long;
        if (rowid == 1 && cellid == 1) {
            if (DisplayType == "ByDesc" || DisplayType == "BothByDesc") {
                Route_waypoint = CurrentVehicleArray[newvalue - 1]["RegNo"];
            }
            else {
                Route_waypoint = CurrentVehicleArray[newvalue - 1][0];
            }
            Route_lat = CurrentVehicleArray[newvalue - 1]["Latitude"];
            Route_long = CurrentVehicleArray[newvalue - 1]["Longitude"];
            // Create an Option object
            var opt = document.createElement("option");
            //document.getElementById("lstRoutes").options.add(opt);
            dgNewRoutes.addRow(NoofWayPoints, Route_waypoint);
            opt.text = Route_waypoint;
            opt.value = NoofWayPoints;
            tmpRoutesArray[NoofWayPoints] = Route_waypoint + "|" + Route_lat + "|" + Route_long;
            NoofWayPoints = NoofWayPoints + 1;
        }
        else if (rowid == 2 && cellid == 1) {
            if (GeofenceArray.length > 0) {
                Route_waypoint = GeofenceArray[newvalue - 1]["GeoFenceName"];
                if (GeofenceArray[newvalue - 1]["GeoFenceSource"] == 0) {
                    Route_lat = GeofenceArray[newvalue - 1]["Latitude"];
                    Route_long = GeofenceArray[newvalue - 1]["Longitude"];
                }
                else if (GeofenceArray[newvalue - 1]["GeoFenceSource"] == 3) {
                    // calculate the bounds of the polygon	
                    var bounds = new google.maps.LatLngBounds();
                    var thisPoly = GeofenceArray[newvalue - 1].Polygon.split('#');
                    var polygonCoords = [];
                    for (var j = 0; j < thisPoly.length ; j++) {
                        var tmpPoly = thisPoly[j].split(",");
                        polygonCoords[j] = new google.maps.LatLng(parseFloat(tmpPoly[0]), parseFloat(tmpPoly[1]));
                    }
                    for (var x = 0; x < polygonCoords.length; x++) {
                        bounds.extend(polygonCoords[x]);
                    }
                    Route_lat = bounds.getCenter().lat();
                    Route_long = bounds.getCenter().lng();
                }
                var opt = document.createElement("option");
                //document.getElementById("lstRoutes").options.add(opt);
                dgNewRoutes.addRow(NoofWayPoints, Route_waypoint);
                opt.text = Route_waypoint;
                opt.value = NoofWayPoints;
                tmpRoutesArray[NoofWayPoints] = Route_waypoint + "|" + Route_lat + "|" + Route_long;
                NoofWayPoints = NoofWayPoints + 1;
            }
        }
        else if (rowid == 3 && cellid == 1) {
            if (SiteArray.length > 0) {
                Route_waypoint = SiteArray[newvalue - 1][0];
                Route_lat = SiteArray[newvalue - 1][1];
                Route_long = SiteArray[newvalue - 1][2];
                var opt = document.createElement("option");
                //document.getElementById("lstRoutes").options.add(opt);
                dgNewRoutes.addRow(NoofWayPoints, Route_waypoint);
                opt.text = Route_waypoint;
                opt.value = NoofWayPoints;
                tmpRoutesArray[NoofWayPoints] = Route_waypoint + "|" + Route_lat + "|" + Route_long;
                NoofWayPoints = NoofWayPoints + 1;
            }
        }
    }
    return newvalue;

}
function FindLoc() {

    var thisLoc = dgRouteSetup.cells(5, 0).getValue();
    if (thisLoc.length > 0) {
        routes_TabBar.setTabActive("Routes_DisplayAddressList", true);
        document.getElementById("hiddenWantedLocation").value = thisLoc;
        document.getElementById("hiddenButtonFindLocation").click();
    }
    else {
        setUpRows();
    }
}
function ShowLocationResults() {

    document.getElementById("loading").className = "loading-invisible";
    LoadLocationSearchResultsGrid();
}
function AddToRoute(wantedAddress) {
    var addressArray = $("#hiddenthisAddress").val().split("|");
    Route_waypoint = addressArray[0];
    Route_lat = addressArray[1];
    Route_long = addressArray[2];
    var opt = document.createElement("option");
    //document.getElementById("lstRoutes").options.add(opt);
    dgNewRoutes.addRow(NoofWayPoints, Route_waypoint);
    opt.text = Route_waypoint;
    opt.value = NoofWayPoints;
    tmpRoutesArray[NoofWayPoints] = Route_waypoint + "|" + Route_lat + "|" + Route_long;
    NoofWayPoints = NoofWayPoints + 1;
}
function LoadLocationSearchResultsGrid() {
    document.getElementById("FindAddressResults_container").style.width = "500px";
    dgLocationResults = new dhtmlXGridObject('FindAddressResults_container');
    dgLocationResults.setImagePath("codebase/imgs/");
    var tmpHeader = document.getElementById("hiddenSearchLocationGridHeader").value;
    dgLocationResults.setHeader(tmpHeader);
    dgLocationResults.setColTypes("link");
    dgLocationResults.setInitWidths("515");
    dgLocationResults.setColAlign("center");
    dgLocationResults.setSkin("modern");
    dgLocationResults.enableMultiselect(false);
    //dgLocationResults.attachEvent("onMouseOver", MouseOver);
    dgLocationResults.init();
    dgLocationResults.preventIECaching(true);
    var clientID = document.getElementById("hiddenClientID").value;
    var Results_xmlFile = "xmlFiles/" + clientID + "_LocationResults.xml?etc=" + new Date().getTime();
    dgLocationResults.loadXML(Results_xmlFile);
}
function FindLocFromResults() {
    var rId = dgLocationResults.getSelectedRowId();
    var cInd = dgLocationResults.getSelectedCellIndex();
    var thisNewLoc = dgLocationResults.cells(rId, cInd).getValue();
    //dgLocationResults.clearAll();
    document.getElementById("hiddenWantedLocation").value = GetInnerText(thisNewLoc);
    document.getElementById("hiddenGetThisLocation").value = "True";
    document.getElementById("hiddenButtonFindLocation").click();
}
function GetInnerText(strLoc) {
    //"<A href="javascript:FindLocFromResults();">Lakeview Dr, Swords, County Fingal, Ireland</A>"
    var tmpAddressArray = strLoc.split('>');
    var tmpAddressArray_1 = tmpAddressArray[1].split('<');
    return tmpAddressArray_1[0];

}
function FindLoc_OLD() {

    var thisLoc = dgRouteSetup.cells(5, 0).getValue();

    if (thisLoc != null && thisLoc != "") {
        routes_TabBar.setTabActive("Routes_Map", true);
        distUnits = document.getElementById("hiddenDistUnits").value;
        var displayDistunits;
        var results = null;
        if (distUnits == "Miles") {                             // Show as miles or kilometres
            displayDistunits = VEDistanceUnit.Miles;
        }
        else {
            displayDistunits = VEDistanceUnit.Kilometers;
        }
        if (!map) {
            map = null;
            map = new VEMap('myMap');
            if (browserName != "msie" && browserName != "firefox" && browserName != "safari") {
                document.getElementById('myMap').style.width = VE_Map_width;
            }
            var options = new VEMapOptions();
            var AllowBirdsEye = document.getElementById('hiddenAllowBirdsEye').value;
            if (AllowBirdsEye == "True") {
                options.EnableBirdseye = true;
                options.EnableDashboardLabels = true;
            }
            else {
                options.EnableBirdseye = false;
                options.EnableDashboardLabels = false;
            }
        }
        map.LoadMap(null, null, null, null, null, false, null, options);
        map.AttachEvent("onclick", PixelClick);
        map.SetScaleBarDistanceUnit(displayDistunits);
        map.Find(null,                                                 //what
					  thisLoc,                                          //where
					  null,                                             //findType
					  null,                                             //shapeLayer
					  null,                                             //startIndex
					  1,                                                //numberOfResults
					  true,                                             //showResults
					  true,                                             //createResults
					  true,                                             //useDefaultDisambiguation
					  true,                                             //setBestMapView
					  showLocation);                                            //callback

    }
    return true;
}
function showLocation(theLayer, resulstArray, places, hasMore, veErrorMessage) {
    try {
        if (places != null && places.length > 0) {
            var thisLat = places[0].LatLong.Latitude;
            var thislong = places[0].LatLong.Longitude;
            var shape = new VEShape(VEShapeType.Pushpin, new VELatLong(thisLat, thislong));
            shape.SetTitle(places[0].Name);
            shape.SetDescription(document.getElementById("hiddenRightClickMapSelectAddress").value);
            map.AddShape(shape);
        }
        else {
            window.alert(veErrorMessage);
        }
    }
    catch (Error) {
        window.alert("Find Address - 133: " + Error.message);
    }
}
function PixelClick(e) {
    if (e.eventName == "onclick") {
        if (e.rightMouseButton) {
            var x = e.mapX;
            var y = e.mapY;
            pixel = new VEPixel(x, y);
            LL = map.PixelToLatLong(pixel);
            map.FindLocations(LL, GetResults);
        }
    }
}
function PostBackAddress() {
    var tmpArray = new Array();
    tmpArray = thisaddress.split('|');
    Route_waypoint = tmpArray[0];
    Route_lat = tmpArray[1];
    Route_long = tmpArray[2];
    var opt = document.createElement("option");
    dgNewRoutes.addRow(NoofWayPoints, Route_waypoint);
    opt.text = Route_waypoint;
    opt.value = NoofWayPoints
    tmpRoutesArray[NoofWayPoints] = Route_waypoint + "|" + Route_lat + "|" + Route_long
    NoofWayPoints = NoofWayPoints + 1;
    End_PostBackAddress();
}
function PostBackEmailAddress() {
    var EmailAddress = document.getElementById("txtEmailAddress").value;
    document.getElementById("hiddenEmailAddress").value = EmailAddress;
    document.getElementById("hiddenNoofWayPoints").value = NoofWayPoints;
    document.getElementById("hiddentmpRoutesArray").value = tmpRoutesArray;
    document.getElementById("hiddenButtonPostBackEmailAddress").click();
    DisplayMsgWin.setModal(false);
    DisplayMsgWin.hide();
    DisplayMsgWin = null;
}
function CancelEmailAddress() {
    DisplayMsgWin.setModal(false);
    DisplayMsgWin.hide();
    DisplayMsgWin = null;
}
function GetResults(locations) {
    var s = "Selected location is at ";
    if (locations != null) {
        s += locations[0].Name;
        thisaddress = locations[0].Name + '|' + locations[0].LatLong.Latitude + '|' + locations[0].LatLong.Longitude;
    }
    else {
        s += "No Result found.";
    }
    document.getElementById("lblFoundAddress").value = s;
    MsgWin = new dhtmlXWindows();
    MsgWin.setImagePath("codebase/imgs/");
    MsgWin.setSkin("dhx_skyblue");
    DisplayMsgWin = null;
    DisplayMsgWin = MsgWin.createWindow("DisplayMessage", 55, 55, 380, 110);
    //MsgWin.window("DisplayMessage").center();
    MsgWin.window("DisplayMessage").button("close").hide();
    MsgWin.window("DisplayMessage").button("park").hide();
    MsgWin.window("DisplayMessage").button("minmax1").hide();
    DisplayMsgWin.setText(document.getElementById("hiddenAddAddressToRoute").value);
    DisplayMsgWin.attachObject("Container_AddAddress");
    DisplayMsgWin.centerOnScreen()
    DisplayMsgWin.setModal(true);
    DisplayMsgWin.show();


    // eo_GetObject('DialogAddress').show(true, document.getElementById("hiddenAddAddressToRoute").value);
    // var span = document.getElementById("dialog_content");
    // span.innerHTML = s;
    // var thisline = null;
}
function End_PostBackAddress() {
    DisplayMsgWin.setModal(false);
    DisplayMsgWin.hide();
    DisplayMsgWin = null;
}
function AddtoRoute() {
    eo_GetObject('DialogAddress').show(true, document.getElementById("hiddenAddAddressToRoute").value);
    var span = document.getElementById("dialog_content");
    span.innerHTML = s;
    var thisline = null;

}
function ModifyRoute(tmpRoutesArray) {
    isModifyRoute = true;
    ClearRoutes();
    BuildNewRouteArray(tmpRoutesArray.split("~"));
    GetMap();

}

function CalculateNewRoute(tmpRoutesArray) {
    isModifyRoute = false;
    ClearRoutes();
    BuildNewRouteArray(tmpRoutesArray.split("~"));
    GetMap();
}
function ClearRoutes() {
    try {

        //Clear the routes
        if (RouteMapArray) {
            for (i in RouteMapArray) {
                RouteMapArray[i].setMap(null);
            }
        }
        RouteMapArray = [];
        if (waypointMarkersArray) {
            for (i in waypointMarkersArray) {
                waypointMarkersArray[i].setMap(null);
            }
        }
        waypointMarkersArray = [];

        newRoutesArray = [];
        if (directionsDisplay != null) {
            directionsDisplay.setMap(null);
            directionsDisplay = null;
        }
    }
    catch (error) {
        alert(error.message);
    }
}
function BuildNewRouteArray(tmpArrayStr) {
    try {
        newRoutesArray = [];
        routes = [];
        for (var i = 0; i < tmpArrayStr.length; i++) {
            newRoutesArray[i] = tmpArrayStr[i].split('|');
            NumofRoutes = i + 1;
        }
    }
    catch (Error) {
        window.alert("Build New Route Array Error: " + Error.message);
    }
}
function GetMap() {
    try {
        directionsService = new google.maps.DirectionsService;
        directionsDisplay = new google.maps.DirectionsRenderer({
            draggable: true,
            suppressMarkers: true
        });

        var googleMapOptions =
        {
            zoom: zoomLevel,
            scaleControl: true,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            mapTypeControlOptions: {
                style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                position: google.maps.ControlPosition.TOP_LEFT
            }
        };
        directionsDisplay.setMap(map);
        //directionsDisplay.setPanel(document.getElementById("routes_Turns_Container"));
        calculateAndDisplayRoute(directionsService, directionsDisplay);
        map.setCenter(latlngbounds.getCenter(), zoomLevel);


    } catch (error) {
        window.alert("Get Route Error: " + error.message);
    }
}
function DisplayRouteTurns() {
    var clientId = document.getElementById("hiddenClientID").value;
    var routesXmlFile = "xmlFiles/" + clientId + "_routeTurnsgrid.xml?etc=" + new Date().getTime();
    dgRoutes.clearAndLoad(routesXmlFile);
}
function calculateAndDisplayRoute(directionsService, directionsDisplay) {
    var distUnit = google.maps.DirectionsUnitSystem.METRIC;
    latlngbounds = new google.maps.LatLngBounds();
    var waypts = [];
    for (var i = 0; i < newRoutesArray.length ; i++) {
        var loc;
        if (newRoutesArray[i].length > 1) {
            loc = new google.maps.LatLng(newRoutesArray[i][1], newRoutesArray[i][2]);
            latlngbounds.extend(loc);
        }
        if (i == 0 || i == newRoutesArray.length - 1) {
            //waypts.push({
            //    location: new google.maps.LatLng(parseFloat(newRoutesArray[i][1]), parseFloat(newRoutesArray[i][2])),
            //    stopover: false
            //});
        } else {
            waypts.push({
                location: new google.maps.LatLng(parseFloat(newRoutesArray[i][1]), parseFloat(newRoutesArray[i][2])),
                stopover: true
            });
        }
    }
    if (distUnits == "Miles") {
        distUnit = google.maps.DirectionsUnitSystem.IMPERIAL;
    }
    myRoute = directionsService.route({
        origin: new google.maps.LatLng(parseFloat(newRoutesArray[0][1]), parseFloat(newRoutesArray[0][2])),
        destination: new google.maps.LatLng(parseFloat(newRoutesArray[newRoutesArray.length - 1][1]), parseFloat(newRoutesArray[newRoutesArray.length - 1][2])),
        waypoints: waypts,
        optimizeWaypoints: isOptimizedRoute,
        travelMode: google.maps.TravelMode.DRIVING,
        unitSystem: distUnit        //Change this to IMPERIAL
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
            var dir = response.routes[0];
            myGoogleRouteHandler(response);
            directionsDisplay.addListener('directions_changed', function () {
                myGoogleRouteHandler(response);
            });

        }
        else {
            window.alert('Directions request failed due to ' + status);
        }
    });

}
function createGoogleScript() {
    try {
        var thisKey = document.getElementById("hiddenGoogleKey").value;
        var script = document.createElement("script");
        script.type = "text/javascript";
        script.src = "http://maps.googleapis.com/maps/api/js?v=3.22&" + thisKey + "&sensor=true&libraries=drawing,geometry&callback=initalizeGoogleMap";
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

    var script2 = document.createElement("script");
    script2.type = "text/javascript";
    script2.src = "./Scripts/markerwithlabel.js";
    document.body.appendChild(script2);

    var script3 = document.createElement("script");
    script3.type = "text/javascript";
    script3.src = "./Scripts/ProjectedOverlay.js";
    document.body.appendChild(script3);

    var script4 = document.createElement("script");
    script4.type = "text/javascript";
    script4.src = "./Scripts/geoXML3.js";
    document.body.appendChild(script4);

    google.maps.Polygon.prototype.my_getBounds = function () {
        var bounds = new google.maps.LatLngBounds();
        this.getPath().forEach(function (element, index) { bounds.extend(element); });
        return bounds;
    }
}
//function to display an icon on the route stop points
function AddRoutePins(routes) {
    try {
        map.DeleteAllPushpins();
    }
    catch (Error) {
    }
    try {
        map.DeleteAllShapes();
    }
    catch (Error) {
    }
    pinid = 0;
    try {
        var icon = 'Images/Scotland-Flag.gif';
        var layer = null;
        for (i = 1; i < routes.length - 1; i++) {
            var thisLabel = newRoutesArray[i][0];
            var pin = new VEPushpin(pinid, routes[i], icon, thisLabel, newRoutesArray[i][0]);
            var shape = new VEShape(VEShapeType.Pushpin, routes[i]);
            var thisicon = "<div style='color:#212121;font-family:Verdana;font-style:italic;font-size:8.25pt;font-weight: bold;background-color:transparent;width:auto;white-space:pre;text-indent:2em;padding-top:5;'><label style='background-color:white;border:solid 1px Black;text-decoration:underline;'>" + newRoutesArray[i][0] + "</label></div>";
            shape.SetTitle(newRoutesArray[i][0]);
            shape.SetTitle(thisicon);
            shape.SetDescription(newRoutesArray[i][0]);
            shape.SetCustomIcon(icon);
            map.AddShape(shape);
            map.AddPushpin(pin);
            pinid++;
        }
    }
    catch (Error) {
        window.alert("Add Layer Error: " + Error.message);
    }
}
function GetTime(totalSec) {
    // time is an integer representing seconds
    // returns a formatted string
    var hours = parseInt(totalSec / 3600) % 24;
    var minutes = parseInt(totalSec / 60) % 60;
    var seconds = totalSec % 60;
    var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);
    return result;
}
function myBingRouteHandler(route) {
    // Unroll route and populate alert text
    var legs = route.RouteLegs;
    var turns = "Turn-by-Turn Directions\n";
    var turnsList = "";
    var leg = null;
    var turnNum = 0;  // The turn #
    var totalDistance = 0;  // The sum of all leg distances
    var totalTime = 0;      // The sum of all leg times
    for (i = 0; i < 1001; ++i) {
        TurnsArray[i] = new Array(3);
    }
    // Get intermediate legs
    for (var i = 0; i < legs.length; i++) {
        // Get this leg so we don't have to derefernce multiple times
        leg = legs[i];  // Leg is a VERouteLeg object

        // Unroll each intermediate leg
        var turn = null;  // The itinerary leg
        var legDistance = null;  // The distance for this leg
        var legTime = null; //The time for this leg

        for (var j = 0; j < leg.Itinerary.Items.length; j++) {
            // turn is a VERouteItineraryItem object
            turn = leg.Itinerary.Items[j];
            turns += turnNum + ":  " + turn.Text;
            legDistance = turn.Distance;
            // Round distances to 1/10ths
            // Note that miles is the default
            var turnDistance = 0;
            turnDistance = legDistance.toFixed(1);
            legTime = GetTime(turn.Time);
            turnsList += turnNum + "|" + turn.Text + "|" + turnDistance + "|" + legTime + "~";
            TurnsArray[j][0] = turnNum;
            TurnsArray[j][1] = turn.LatLong.Latitude;
            TurnsArray[j][2] = turn.LatLong.Longitude;
            totalDistance += legDistance;
            totalTime += turn.Time;
            turnNum++;
        }
    }
    var tripTime = GetTime(totalTime);
    if (distUnits == "Miles") {
        turnsList += "|Route Totals|" + totalDistance.toFixed(1) + " miles " + "|" + tripTime + "~";
    }
    else {
        turnsList += "|Route Totals|" + totalDistance.toFixed(1) + " kilometres " + "|" + tripTime + "~";
    }

    // Show directions
    document.getElementById("hiddenTurnsList").value = turnsList;

}
function myGoogleRouteHandler(route) {

    //First set the custom markers for the start location of all legs
    var thisRoute = route.routes[0];
    for (i = 0; i <= thisRoute.legs.length - 1; i++) {
        var wayPointName = newRoutesArray[i][0];
        var wayPointLat = parseFloat(newRoutesArray[i][1]).toFixed(5);
        var wayPointLng = parseFloat(newRoutesArray[i][2]).toFixed(5);
        var pos = new google.maps.LatLng(wayPointLat, wayPointLng);
        waypointMarkersArray.push(new MarkerWithLabel({
            position: pos,
            map: map,
            icon: 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + (i + 1) + '|FE6256|000000',
            zIndex: 2,
            labelContent: wayPointName,
            labelAnchor: new google.maps.Point(-5, 20),
            labelClass: "labels", // the CSS class for the label	 
            labelStyle: { opacity: 1.0 },
            labelZIndex: 1
        }));
    }

    var lastWayPointName = newRoutesArray[newRoutesArray.length - 1][0];
    waypointMarkersArray.push(new MarkerWithLabel({
        position: thisRoute.legs[thisRoute.legs.length - 1].end_location,
        map: map,
        zIndex: 2,
        icon: 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + (i + 1) + '|FE6256|000000',
        labelContent: lastWayPointName,
        labelAnchor: new google.maps.Point(-5, 20),
        labelClass: "labels", // the CSS class for the label	 
        labelStyle: { opacity: 1.0 },
        labelZIndex: 1
    }));

    // Unroll route and populate alert text
    var legs = route.routes[0].legs.length;
    var turns = "Turn-by-Turn Directions\n";
    var turnsList = "";
    var leg = null;
    var turnNum = 0;  // The turn #
    var totalDistance = 0;  // The sum of all leg distances
    var totalTime = 0;      // The sum of all leg times
    var totalPath = '';          // string of all the paths in the route
    var routeWayPoints = '';
    for (i = 0; i < 10001; ++i) {
        TurnsArray[i] = new Array(3);
    }
    // Get intermediate legs
    for (var i = 0; i < legs; i++) {
        // Get this leg so we don't have to derefernce multiple times
        leg = route.routes[0].legs[i];
        // Unroll each intermediate leg
        var turn = null;  // The itinerary leg
        var legDistance = null;  // The distance for this leg
        var legTime = null; //The time for this leg
        for (var j = 0; j < leg.steps.length; j++) {
            //totalPath += '(' + leg.start_location.lat().toFixed(5) + ', ' + leg.start_location.lng().toFixed(5) + '),';
            turn = leg.steps[j];
            turns += turnNum + ":  " + turn.instructions;
            legDistance = turn.distance.value;
            // Round distances to 1/10ths
            // Note that miles is the default
            var turnDistance = 0;
            turnDistance = legDistance.toFixed(1) / 1000;
            legTime = GetTime(turn.duration.value);
            var turnHtml = turn.instructions.replace("<div", ". <div");
            var turnName = stripHTML(turnHtml);

            turnsList += turnNum + "|" + turnName + "|" + turnDistance + "|" + legTime + "~";
            TurnsArray[j][0] = turnNum;
            TurnsArray[j][1] = turn.end_location.G;
            TurnsArray[j][2] = turn.end_location.K;
            totalDistance += turnDistance;
            totalTime += turn.duration.value;
            totalPath += turn.path;
            turnNum++;

        }
        if (i < legs - 1) {
            routeWayPoints += 'RouteStart' + '|' + leg.start_location.lat().toFixed(5) + '|' + leg.start_location.lng().toFixed(5) + "~";
        } else {
            if (i == legs - 1) {
                routeWayPoints += 'RouteWayPoint' + '|' + leg.start_location.lat().toFixed(5) + '|' + leg.start_location.lng().toFixed(5) + "~";
                routeWayPoints += 'RouteEnd' + '|' + leg.end_location.lat().toFixed(5) + '|' + leg.end_location.lng().toFixed(5);
            }
        }
    }
    var tripTime = GetTime(totalTime);
    if (distUnits == "Miles") {
        turnsList += "|Route Totals|" + totalDistance.toFixed(1) + " miles " + "|" + tripTime + "~";
    }
    else {
        turnsList += "|Route Totals|" + totalDistance.toFixed(1) + " kilometres " + "|" + tripTime + "~";
    }

    // Show directions
    window.external.UpdateTurns(turnsList, totalPath, routeWayPoints);

}
function stripHTML(html) {
    var tmp = document.createElement("DIV");
    tmp.innerHTML = html;
    return tmp.textContent || tmp.innerText || "";
}
function ShowPolygonRoutes() {
    try {
        var dupPolyId = -1; //Variable to store GeoPolyID to strip out duplicates when drawing polygons.
        for (i in GeofenceArray) {
            if (GeofenceArray[i].Polygon != null) {
                // Define the LatLng coordinates for the polygon's path.	
                var polyCoords = [];
                var polyCoords1 = [];
                var polyCoords2 = [];
                polyCoords = GeofenceArray[i].Polygon.split('#');
                for (j in polyCoords) {
                    polyCoords1 = polyCoords[j].split(',');
                    polyCoords2[j] = new google.maps.LatLng(parseFloat(polyCoords1[0]), parseFloat(polyCoords1[1]));
                }

                // Set the polygon colours based upon the Company allocation
                var polystrokeColor = "#045FB4";
                var polyfillColor = "#0080FF";
                var isAllocated = false;
                if (GeofenceArray[i].Company != null) {
                    if (GeofenceArray[i].Company.length > 1) {
                        isAllocated = true;
                        polystrokeColor = '#8A0808';
                        polyfillColor = '#FF0000';
                    }
                } else {
                    isAllocated = false;
                    polystrokeColor = '#045FB4';
                    polyfillColor = '#0080FF';
                }
                var thisPoly;
                if (GeofenceArray[i].GeoFenceSource == "4") {
                    // Construct the polyline.	
                    thisPoly = new google.maps.Polyline({
                        path: polyCoords2,
                        //geodesic: true,
                        strokeColor: polyfillColor,
                        strokeOpacity: 0.8,
                        strokeWeight: 7.5,
                        indexID: i,
                        ID: GeofenceArray[i].GeoId,
                        PName: GeofenceArray[i].GeoFenceName,
                        PAllocated: isAllocated,
                        PType: 4
                    });
                    if (thisPoly) {
                        var geoDetails = 'Route: ' + GeofenceArray[i].GeoFenceName + ' (Unallocated)';
                        google.maps.event.addListener(thisPoly, 'rightclick', function (e) {
                            selectedLine = null;
                            for (var x = 0; x < PolylineMapArray.length; x++) {
                                if (PolylineMapArray[x].indexID == this.indexID) {
                                    selectedLine = PolylineMapArray[x];
                                    poly_point = convertPoint(e.latLng);
                                    PolylineContextMenu.showContextMenu(poly_point.x + 400, poly_point.y + 25);
                                }
                            }
                        });
                        if (dupPolyId != GeofenceArray[i].GeoId) {
                            thisPoly.setMap(map);
                        }
                        PolylineMapArray.push(thisPoly);
                    }
                }
                dupPolyId = GeofenceArray[i].GeoId;
            }
        }
    } catch (error) {
        window.alert("Error 1540: " + error.message);
    }
}