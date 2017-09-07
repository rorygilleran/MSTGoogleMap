//*****************************************************************************************
// File Name:       MST_GoogleMapJS.js
// Author:          Rory Gilleran 
// Copyright:       Business Information System Ltd 2011
// Last Updated:    16th December 2015
//*****************************************************************************************


var bounds;
var BufferZoneArray = [];
var BufferZoneIsEditable = "False";
var BufferZoneWidth = "0";
var centerMarker;
var changedMapType;
var circleRadius;
var circleUnits;
var clickedPoint;
var colorButtons = {};
//var colors = ['#FFADB0', '#FF1493', '#32CD32', '#FF8C00', '#4B0082'];
var colors = ["#FF0000", "#008000", "#FFA500", "#800080", "#00FFFF", "#FF1493"];
var connectSiteLineArray = [];
var contextMenu;
var contextMenuListener;
var contextMenuListener1;
var contextMenuOptions = {};
var contextPolyMenu;
var contextPolyMenuListener;
var contextPolyMenuOptions = {};
var CreatingGeofence = false;
var currentZoomLevel = 15;
var Display_Both = "False";
var Display_Date = "False";
var Display_Desc = "False";
var Display_RegNo = "False";
var displayOption = "ByReg";
var distanceOverlay;
var DistLabels = [];
var distpolyline;
var distpolylineDistance;
var distUnitMultiplier = "1";
var distUnits = "Kilometres";
var drawingManager;
var fixedMapBounds = 0;
var fixedMapZoom = 0;
var GeofenceArray = [];
var GeofenceCircle = [];
var geoFenceListener;
var geofenceMarkersArray = [];
var geofenceTitleArray = [];
var geoMarker;
var hideHistoryIcons;
var HistoryArrowHeads = [];
var HistoryLines = [];
var IconArray = [];
var IconBase64Array = [];
var idMapClick;
var idMapRightClick;
var idMouseMove;
var infowindow;
var IsAutoHistory;
var isBufferZone = "False";
var IsCreatingPolygon = false;
var isHistory = "False";
var isSites = "False";
var isOptimizedRoute = false;
var joinPoints = "False";
var isMSTrack = false;
var kml_polyline_data = [];
var lastPoint;
var lineColor0 = "#000000";
var lineColor1 = "#ff0000";
var lineColor2 = "#00ff00";
var lineColor3 = "#0000ff";
var lineColor4 = "#ffff00";
var lineColor5 = "#ff00ff";
var lineColor6 = "#00ffff";
var lineCordinates = [];
var lineCount;
var lineCounter = 0;
var map;
var markerCount = 0;
var Markers = [];
var Markers_Detail = [];
var Markers_Routes = [];
var measureDistance = "False";
var menuItems = [];
var numOfIcons = "75";
var numOfPlots = 0;
var opacity = 0.4;
var options = {};
var pBufferZone;
var pin;
var PolygonMapArray = [];
var polyline;
var polyline_coordinates = [];
var polyline_count = 0;
var polylineData = '';
var polylineListener;
var PolyLines = [];
var polyMarkers = [];
var polyMenuItems = [];
var polypath = [];
var polyType;
var RouteMapArray = [];
var routeOverlay;
var routePoints = Array();
var RoutesArray = [];
var waypointMarkersArray = [];
var waypointMarkersDetails = [];
var selectedColor;
var selectedShape;
var setArrows;
var showGeofences = "False";
var showRoutes = "False";
var showSiteLabels = "False";
var showSites = "False";
var showTraffic = "False";
var SiteArray = [];
var siteLabels = [];
var siteMarkers = [];
var siteMarkers_Detail = [];
var storedArrayString;
var str_BufferZoneArray = "";
var str_GeofenceArray = "";
var str_IconArray;
var str_IconBase64;
var str_PolygonGeo = "";
var str_PolylineRoutes = "";
var str_PolyRoutesWayPoints;
var str_SitesArray = [];
var thisDistance = 0;
var tmpDist;
var tmpZIndex = 100;
var useDrivers = "True";
var useLabels = Boolean("True");
var VehicleArray = [];
var VehLabels = [];
var WayPointsArray = [];

function history_HideVehicleIcons(state) {
    if (state === true) {
        hideHistoryIcons = "True";
        lineCordinates = [];
        lineCount = 0;
        IsAutoHistory = false;
        try {
            history_line.setMap(null);

        } catch (e) {
        }
        try {
            if (PolyLines.length > 0) {
                for (i = 0; i < PolyLines.length; i++) {
                    PolyLines[i].setMap(null);
                }
                PolyLines = [];
            }
        } catch (e) {
        }
        AddGoogleMarkers();

    } else {
        hideHistoryIcons = "False";
        try {
            if (Markers) {
                for (var i = 1; i < Markers.length - 1; i++) {
                    Markers[i].setMap(null);
                }
            }

        } catch (e) {
        }
        try {
            if (PolyLines.length > 0) {
                for (i = 0; i < PolyLines.length; i++) {
                    PolyLines[i].setMap(null);
                }
                PolyLines = [];
            }
        } catch (e) {
        }
        AddGoogleMarkers();
    }
}
function MapTypeChanged() {
    var newMap = map.getCurrentMapType().getName();
    window.external.MapTypeChanged(newMap);
}
function SetMapValues() {
    numOfPlots = 0;
    useDrivers = "True";
    displayOption = "ByReg";
}
function addRoutePoint(point) {
    var zoom = map.getZoom();
    routePoints.push(point);
    if (routePoints.length > 1) {
        plotRoute();
    }
    else {
        startMarker = createMarker(point, 'Start');
    }
}
function addIcon(file) {
    var g = google.maps;
    var icon = {
        url: "http://www.google.com/mapfiles/" + file,
        size: new g.Size(24, 24), anchor: new g.Point(12, 12)
    };
    return icon;
}
function JoinHistoryPoints() {
    try {
        var history_Points = new Array();
        for (var i = 0; i < VehicleArray.length; i++) {
            history_Points.push(new google.maps.LatLng(parseFloat(VehicleArray[i][2]), parseFloat(VehicleArray[i][3])));
        }
        setArrows = new ArrowHandler();
        var thisLine = createPoly(history_Points, 'midline');
        HistoryLines.push(thisLine);
        IsShowingHistoryPoints = true;
    }
    catch (Error) {
    }
}
function LoadContextMenu(isMSTrack) {
    menuItems = [];
    contextMenuOptions.classNames = { menu: 'context_menu', menuSeparator: 'context_menu_separator' };
    if (isHistory != "True") {
        menuItems.push({ className: 'context_menu_item', eventName: 'showLabels_click', id: 'showLabelsItem', label: 'Show / Hide Vehicle Labels' });
    } else {
        menuItems.push({ className: 'context_menu_item', eventName: 'showIcons_click', id: 'showIconsItem', label: 'Show Vehicle Icons' });
    }
    menuItems.push({ className: 'context_menu_item', eventName: 'showGeofences_click', id: 'showGeofencesItem', label: 'Show / Hide Geofences' });
    if (isMSTrack == false) {
        menuItems.push({ className: 'context_menu_item', eventName: 'showRoutes_click', id: 'showRoutesItem', label: 'Show / Hide Allocated Routes' });
    }
    menuItems.push({ className: 'context_menu_item', eventName: 'showSites_click', id: 'showSitesItem', label: 'Show / Hide Sites' });
    menuItems.push({ className: 'context_menu_item', eventName: 'showSiteLabels_click', id: 'showSiteLabelsItem', label: 'Show / Hide Site Labels' });
    menuItems.push({ className: 'context_menu_item', eventName: 'showTraffic_click', id: 'showTraffic', label: 'Show / Hide Traffic' });
    menuItems.push({ className: 'context_menu_item', eventName: 'createSite_click', id: 'createSiteItem', label: 'Create Site' });
    if (isMSTrack == false || isMSTrack == "False") {
        menuItems.push({ className: 'context_menu_item', eventName: 'createPolygon_click', id: 'createPolygonItem', label: 'Create Polygon Geofence' });
    };
    contextMenuOptions.menuItems = menuItems;
    contextMenu = new ContextMenu(map, contextMenuOptions);
    contextMenuListener = google.maps.event.addListener(map, 'rightclick', function (mouseEvent) {
        clickedPoint = mouseEvent.latLng;
        contextMenu.show(mouseEvent.latLng);
    });
    contextMenuListener1 = google.maps.event.addListener(contextMenu, 'menu_item_selected', function (clickedPoint, eventName) {
        polyType = "";
        switch (eventName) {
            case 'showLabels_click':
                if (useLabels === true) {
                    useLabels = false;
                    if (isHistory === true) {
                        Display_Date = false;
                    }
                    AddGoogleMarkers();

                } else {
                    useLabels = true;
                    if (isHistory === true) {
                        Display_Date = true;
                    }
                    AddGoogleMarkers();
                }
                break;
            case 'showIcons_click':
                if (hideHistoryIcons === true || hideHistoryIcons == "True") {
                    hideHistoryIcons = false;
                    history_HideVehicleIcons(false);
                } else {
                    hideHistoryIcons = true;
                    history_HideVehicleIcons(true);
                }
                AddGoogleMarkers();
                break;
            case 'showGeofences_click':
                if (showGeofences == "True") {
                    showGeofences = "False";
                } else {
                    showGeofences = "True";
                }
                CreateGeofenceLayer();
                break;
            case 'showSites_click':
                if (showSites == "True") {
                    showSites = "False";
                } else {
                    showSites = "True";
                }
                CreateSitesLayer();
                break;
            case 'showSiteLabels_click':
                if (showSiteLabels == "True") {
                    showSiteLabels = "False";
                    clear_SitesLabels();
                } else {
                    showSiteLabels = "True";
                }
                CreateSitesLayer();
                break;
            case 'showRoutes_click':
                if (showRoutes == "True") {
                    showRoutes = "False";
                } else {
                    showRoutes = "True";
                }
                //'if (isHistory == "True" || isHistory === true) {
                //  window.external.history_ShowRoutes();
                //} else {
                CreateRoutesLayer();
                //}
                break;
            case 'createSite_click':
                contextMenu.hide();
                CreateSite(clickedPoint);
                break;
            case 'showTraffic_click':
                if (showTraffic == "True") {
                    showTraffic = "False";
                    trafficLayer.setMap(null);
                } else {
                    trafficLayer.setMap(map);
                    showTraffic = "True";
                }
                break;
            case 'createPolygon_click':
                IsCreatingPolygon = true;
                if (drawingManager === null) {
                    SetupDrawingManager();
                }
                drawingManager.setDrawingMode(google.maps.drawing.OverlayType.POLYGON);
                polyType = 'POLYGON';
                contextMenu.hide();
                SetupPolygonContextMenu();
                break;
            case 'createPolyLine_click':
                drawingManager.setDrawingMode(google.maps.drawing.OverlayType.POLYLINE);
                drawingManager.setOptions({
                    drawingControl: true
                });
                polyType = 'POLYLINE';
                contextMenu.hide();
                google.maps.event.clearListeners(map, 'contextMenuListener');
                contextMenu = null;
                SetupPolygonContextMenu();
                break;

            default:
                return;

        }
    });

}
function setShowRoutes() {
    showRoutes = "True";
}
function clearSelection() {
    contextMenuListener = google.maps.event.addListener(map, 'rightclick', function (mouseEvent) {
        contextMenu.show(mouseEvent.latLng);
    });
    if (selectedShape) {
        selectedShape.setEditable(false);
        selectedShape = null;
    }
}

function setSelection(shape) {
    clearSelection();
    selectedShape = shape;
    shape.setEditable(true);
    selectColor(shape.get('fillColor') || shape.get('strokeColor'));
}

function deleteSelectedShape() {
    if (selectedShape.IsNew === false) {
        if (selectedShape.IsAllocated === false) {
            window.external.DeletePolyGeofence(selectedShape.PName + '~' + selectedShape.ID);
            selectedShape.setMap(null);
        }
        else {
            window.external.UnableToDeletePolygon(selectedShape.PName);
        }
    }
    else {
        selectedShape.setMap(null);
    }
}

function selectColor(color) {
    selectedColor = color;
    for (var i = 0; i < colors.length; ++i) {
        var currColor = colors[i];
        colorButtons[currColor].style.border = currColor == color ? '2px solid #789' : '2px solid #fff';
    }

    // Retrieves the current options from the drawing manager and replaces the	
    // stroke or fill color as appropriate.	
    var polylineOptions = drawingManager.get('polylineOptions');
    polylineOptions.strokeColor = color;
    drawingManager.set('polylineOptions', polylineOptions);

    var rectangleOptions = drawingManager.get('rectangleOptions');
    rectangleOptions.fillColor = color;
    drawingManager.set('rectangleOptions', rectangleOptions);

    var circleOptions = drawingManager.get('circleOptions');
    circleOptions.fillColor = color;
    drawingManager.set('circleOptions', circleOptions);

    var polygonOptions = drawingManager.get('polygonOptions');
    polygonOptions.strokeColor = '#045FB4';
    polygonOptions.fillColor = '#0080FF';
    drawingManager.set('polygonOptions', polygonOptions);
}

function setSelectedShapeColor(color) {
    if (selectedShape) {
        if (selectedShape.type == google.maps.drawing.OverlayType.POLYLINE) {
            selectedShape.set('strokeColor', color);
        } else {
            selectedShape.set('fillColor', color);
        }
    }
}
function makeColorButton(color) {
    var button = document.createElement('span');
    button.className = 'color-button';
    button.style.backgroundColor = color;
    google.maps.event.addDomListener(button, 'click', function () {
        selectColor(color);
        setSelectedShapeColor(color);
    });

    return button;
}

function buildColorPalette() {
    var colorPalette = document.getElementById('color-palette');
    for (var i = 0; i < colors.length; ++i) {
        var currColor = colors[i];
        var colorButton = makeColorButton(currColor);
        colorPalette.appendChild(colorButton);
        colorButtons[currColor] = colorButton;
    }
    selectColor(colors[0]);
}
function SetupDrawingManager() {
    try {
        var polyOptions = {
            strokeWeight: 0,
            fillOpacity: 0.45,
            strokeColor: '#045FB4',
            fillColor: '#0080FF',
            editable: true
        };
        // Creates a drawing manager attached to the map that allows the user to draw	
        // markers, lines, and shapes.	
        drawingManager = new google.maps.drawing.DrawingManager({
            drawingControlOptions: {
                drawingModes: [
                    google.maps.drawing.OverlayType.POLYGON
                ]
            },
            markerOptions: {
                draggable: true
            },
            polylineOptions: {
                editable: true
            },
            rectangleOptions: polyOptions,
            circleOptions: polyOptions,
            polygonOptions: polyOptions,
            map: map
        });
        drawingManager.setOptions({
            drawingControl: false
        });
        google.maps.event.addListener(drawingManager, 'overlaycomplete', function (e) {
            if (e.type != google.maps.drawing.OverlayType.MARKER) {
                // Switch back to non-drawing mode after drawing a shape.	
                drawingManager.setDrawingMode(null);
                // To hide:	
                clearSelection();
                drawingManager.setOptions({
                    drawingControl: false
                });

                // Add an event listener that selects the newly-drawn shape when the user	
                // mouses down on it.	
                var newShape = e.overlay;
                newShape.type = e.type;
                newShape.setEditable(false);
                google.maps.event.addListener(newShape, 'click', function () {
                    setSelection(newShape);
                    selectedShape.IsNew = true;
                    selectedShape.IsAllocated = true;
                });
                google.maps.event.addListener(newShape, 'rightclick', function (mouseEvent) {
                    contextPolyMenu.show(mouseEvent.latLng);
                });
            }
        });
        // Clear the current selection when the drawing mode is changed, or when the	
        // map is clicked.	
        google.maps.event.addListener(drawingManager, 'drawingmode_changed', clearSelection);
        google.maps.event.addListener(map, 'click', clearSelection);
        buildColorPalette();
    }
    catch (Error) {
        window.alert("Load Drawing Manager Error: " + Error.message);
    }
}
function SetupPolygonContextMenu() {
    polyMenuItems = [];
    contextPolyMenuOptions.classNames = { menu: 'context_menu', menuSeparator: 'context_menu_separator' };
    polyMenuItems.push({ className: 'context_menu_item', eventName: 'savePoly_click', id: 'shoeLabelsItem', label: 'Save Geofence' });
    polyMenuItems.push({ className: 'context_menu_item', eventName: 'deletePoly_click', id: 'showGeofencesItem', label: 'Delete Geofence' });
    contextPolyMenuOptions.menuItems = polyMenuItems;
    contextPolyMenu = new ContextMenu(map, contextPolyMenuOptions);
    contextPolyMenuListener = google.maps.event.addListener(contextPolyMenu, 'menu_item_selected', function (latLng, eventName) {
        switch (eventName) {
            case 'savePoly_click':
                if (selectedShape) {
                    if (IsCreatingPolygon === true) {
                        selectedShape.IsNew = false;
                        SaveCoords();
                    } else {
                        SaveCoords();
                    }
                }
                break;
            case 'deletePoly_click':
                deleteSelectedShape();
                contextPolyMenu.hide();

                break;
            default:
                return;
        }
    });
}
function SaveCoords() {
    getPolyCoords();
    window.external.SavePolygonGeofence(polylineData + '~' + IsCreatingPolygon + '~' + polyType + '~' + selectedShape.ID + '~' + selectedShape.PName);
    selectedShape.setMap(null);
    contextPolyMenu.hide();
    contextMenuListener = google.maps.event.addListener(map, 'rightclick', function (mouseEvent) {
        clickedPoint = mouseEvent.latLng;
        contextMenu.show(mouseEvent.latLng);
    });

}
function reloadGeofences() {
    IsCreatingPolygon = false;
    clear_Geofences();
    CreateGeofenceLayer();
}
function getPolyCoords() {

    if (selectedShape) {
        polylineData = '';
        var i = 0;
        var firstLat;
        var firstLng;
        for (i = 0; i < selectedShape.getPath().length; ++i) {
            var thisLat = selectedShape.getPath().getAt(i).lat().toFixed(5);
            var thisLng = selectedShape.getPath().getAt(i).lng().toFixed(5);
            if (i === 0) {
                firstLat = thisLat;
                firstLng = thisLng;
            }
            polylineData += thisLng + ' ' + thisLat + ',';
        }
        polylineData += firstLng + ' ' + firstLat;
    }
}
function Load_Google_Map(tmpArrayStr) {
    try {
        window.onerror = function () { return true; };
        BuildArray(tmpArrayStr, 0);
        ClearRoutes();
        CreateGeofenceLayer();
        SetupMap();
        LoadContextMenu(isMSTrack);
        SetupDrawingManager();
        SetMapBounds();
        if (isBufferZone == "True") {
            BuildBufferZoneArray(str_BufferZoneArray);
        }
        else {
            history_HideVehicleIcons(false);
            AddGoogleMarkers();
            SetMapBounds();
        }
    } catch (Error) {
    }
}
function BuildArray(tmpArrayStr, flag) {
    if (flag == 1) {
        storedArrayString = tmpArrayStr;
    }
    if (storedArrayString) {
        tmpArrayStr = storedArrayString;
    }
    try {
        var tmpArray = tmpArrayStr.split('~');
        VehicleArray = [];
        var k = 0;
        for (var i = 0; i <= tmpArray.length - 1; i++) {
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
function SetupMap() {
    if (!map) {
        zoomLevel = 15;
        var googleMapOptions = {
            zoom: 15,
            scaleControl: true,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            streetViewControl: true,
            mapTypeControl: true,
            mapTypeControlOptions: {
                style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                position: google.maps.ControlPosition.TOP_LEFT
            }
        };
        map = new google.maps.Map(document.getElementById("myMap"), googleMapOptions);
        trafficLayer = new google.maps.TrafficLayer();
        map.addListener('bounds_changed', function () {
            saveFleetMapBounds();
        });

    } else {
        zoomLevel = map.getZoom();
        clear_Google_Overlays();
    }
}
function ClearMap() {
    ClearRoutes();
    clear_Google_Overlays();
    endDistanceListeners();
    if (isMSTrack == false) {
        setShowRoutes();
    }
    return ("Map Cleared");
}
function clear_Geofences() {
    if (GeofenceCircle.length > 0) {
        for (i in GeofenceCircle) {
            GeofenceCircle[i].setMap(null);
        }
    }
    GeofenceCircle = [];
    if (geofenceMarkersArray.length > 0) {
        for (i in geofenceMarkersArray) {
            geofenceMarkersArray[i].setMap(null);
        }
    }
    geofenceMarkersArray = [];

    if (PolygonMapArray.length > 0) {
        for (i in PolygonMapArray) {
            PolygonMapArray[i].setMap(null);
        }
        PolygonMapArray = [];
    }
}
function clear_Sites() {
    if (siteMarkers) {
        for (i in siteMarkers) {
            siteMarkers[i].setMap(null);
        }
        siteMarkers = [];
    }
}
function clear_SitesLabels() {
    if (siteLabels) {
        for (i in siteLabels) {
            siteLabels[i].setMap(null);
        }
        siteLabels = [];
    }
}
function clear_VehicleLabels() {
    if (Markers) {
        for (i in Markers) {
            Markers[i].setMap(null);
        }
        Markers = [];
    }
}

function ClearRoutes() {
    try {
        isModifyRoute = false;
        window.external.ClearModifyRoute();
        if (RouteMapArray.length > 0) {
            for (i in RouteMapArray) {
                RouteMapArray[i].setMap(null);
            }
        }
    } catch (e) {

    }
    RouteMapArray = [];
    newRoutesArray = [];
    try {
        if (waypointMarkersArray) {
            for (i in waypointMarkersArray) {
                waypointMarkersArray[i].setMap(null);
            }
        }
        waypointMarkersArray = [];
        waypointMarkersDetails = [];
    } catch (e) {
        //alert("702: " + e.message);
    }
    try {
        if (newRoutesArray) {
            for (i in newRoutesArray) {
                newRoutesArray.setMap(null);
            }
            newRoutesArray = [];
        }
    } catch (e) {
        //alert("712: " + e.message);
    }
    try {
        if (directionsDisplay !== null) {
            directionsDisplay.setMap(null);
            directionsDisplay = null;
        }
    } catch (e) {
        //alert("720: " + e.message);
    }
    //showRoutes = "True";
}
function clear_Google_Overlays() {
    //ClearRoute();
    fixedMapZoom = 0;
    fixedMapBounds = 0;
    showRoutes = "False";
    showGeofences = "False";
    CreateGeofenceLayer();
    showSites = "False";
    CreateSitesLayer();

    try {
        endDistanceListeners();
    } catch (e) {
    }
    try {
        if (markerCluster) {
            markerCluster.clearMarkers();
            markerCluster = null;
        }
    } catch (e) {
    }
    try {
        if (Markers) {
            for (i in Markers) {
                Markers[i].setMap(null);
            }
            Markers = [];
        }
    } catch (e) {
    }
    try {
        if (HistoryLines) {
            for (i in HistoryLines) {
                HistoryLines[i].setMap(null);
            }
            HistoryLines = [];
            clearArrowHeads();
            setArrows = null;
        }
    } catch (e) {
    }
    try {
        if (DistLabels) {
            for (i in DistLabels) {
                DistLabels[i].setMap(null);
            }
            DistLabels = [];
        }
    } catch (e) {
    }
    try {
        if (polyMarkers) {
            for (i in polyMarkers) {
                polyMarkers[i].setMap(null);
            }
            polyMarkers = [];
        }
    } catch (e) {
    }
    try {
        if (distpolylineArray) {
            for (i in distpolylineArray) {
                distpolylineArray[i].setMap(null);
            }
            distpolylineArray = [];
            distpolylineDistance = 0;
        }
    } catch (e) {
    }
    try {
        if (pBufferZone) {
            pBufferZone.setMap(null);
        }
    } catch (e) {
    }
    try {
        if (connectSiteLineArray) {
            for (i in connectSiteLineArray) {
                connectSiteLineArray[i].setMap(null);
            }
        }
    } catch (e) {
    }
}
function GetWayPointFromMap() {
    map.setOptions({ draggableCursor: 'crosshair' });
    var listenerHandle = map.addListener('dblclick', function (event) {
        var lat = event.latLng.lat();
        var lng = event.latLng.lng();
        window.external.SaveWaypointFromMap(lat, lng);
        map.setOptions({ draggableCursor: 'default' });
        google.maps.event.removeListener(listenerHandle);
    });
}
function AddAddressMarker(address, lat, lng) {
    var pos = new google.maps.LatLng(lat, lng);
    Markers.push(new MarkerWithLabel({
        position: pos,
        map: map,
        zIndex: 2,
        labelContent: address,
        labelAnchor: new google.maps.Point(-5, 20),
        labelClass: "labels", // the CSS class for the label	 
        labelStyle: { opacity: 1.0 },
        labelZIndex: 1
    }));
    bounds = map.getBounds();
    bounds.extend(pos);
    map.setCenter(bounds.getCenter(), map.fitBounds(bounds));
    google.maps.event.addListener(Markers[Markers.length - 1], "dblclick", function () {
        for (i = 0; i < Markers.length; i++) {
            if (Markers[i].position == this.position) {
                window.external.saveAddressWaypoint(address, lat, lng);
                if (Markers) {
                    for (i in Markers) {
                        Markers[i].setMap(null);
                    }
                    Markers = [];
                }
            }
        }
    });
}
function AddGoogleMarkers() {
    icon = 'Images/LorryGreen.png';
    clear_VehicleLabels();
    Markers = [];
    Markers_Detail = [];
    if (isHistory == "True" || isHistory === true) {
        try {
            clear_Google_Overlays();
            history_line.setMap(null);
        } catch (e) {
        }
    }
    if (hideHistoryIcons === true || hideHistoryIcons == "True") {
        var historyLinecolor = "#FF66FF";
        if (IsAutoHistory === true && lineCount === 0) {
            latlngbounds = new google.maps.LatLngBounds();
            lineCordinates = [];
            lineCordinates[lineCount] = new google.maps.LatLng(VehicleArray[0][2], VehicleArray[0][3]);
            latlngbounds.extend(lineCordinates[lineCount]);
            lineCount++;
        }
        else if (IsAutoHistory === true && lineCount > 0) {
            lineCordinates[lineCount] = new google.maps.LatLng(VehicleArray[0][2], VehicleArray[0][3]);
            latlngbounds.extend(lineCordinates[lineCount]);
            lineCount++;
            PolyLines = [];
            history_line = new GPolylineWithArrows({
                path: lineCordinates,
                strokeColor: historyLinecolor,
                strokeOpacity: 1,
                strokeWeight: 2,
                arrowStrokeColor: historyLinecolor,
                arrowStrokeOpacity: 1,
                arrowStrokeWeight: 1,
                fillColor: historyLinecolor,
                fillOpacity: 1,
                arrowSize: 15,
                middleArrow: true
            });
            PolyLines.push(polyline);
            history_line.setMap(history_map);
        }
        else if (IsAutoHistory !== true) {
            lineCordinates = [];
            var dateStr = VehicleArray[0][4].substring(0, 19);
            var a = dateStr.split(" ");
            var d = a[0].split("-");
            var t = a[1].split(":");
            var date = new Date(d[0], (d[1] - 1), d[2], t[0], t[1], t[2]);
            var currentDay = date.getDay();
            for (i = 0; i < VehicleArray.length - 1; i++) {
                //var thisDay = new Date(VehicleArray[i][4].substring(0, 10)).getDay();	
                dateStr = VehicleArray[i][4].substring(0, 19);
                a = dateStr.split(" ");
                d = a[0].split("-");
                t = a[1].split(":");
                date = new Date(d[0], (d[1] - 1), d[2], t[0], t[1], t[2]);
                var thisDay = date.getDay();
                while (thisDay == currentDay) {
                    lineCordinates.push(new google.maps.LatLng(parseFloat(VehicleArray[i][2]), parseFloat(VehicleArray[i][3])));
                    if (i < VehicleArray.length - 1) {
                        //currentDay = new Date(VehicleArray[i + 1][4].substring(0, 10)).getDay();	
                        dateStr = VehicleArray[i + 1][4].substring(0, 19);
                        a = dateStr.split(" ");
                        d = a[0].split("-");
                        t = a[1].split(":");
                        date = new Date(d[0], (d[1] - 1), d[2], t[0], t[1], t[2]);
                        currentDay = date.getDay();
                        i = i + 1;
                    } else {
                        currentDay = 99;
                    }
                }
                switch (thisDay) {
                    case 0:
                        historyLinecolor = lineColor0;
                        break;
                    case 1:
                        historyLinecolor = lineColor1;
                        break;
                    case 2:
                        historyLinecolor = lineColor2;
                        break;
                    case 3:
                        historyLinecolor = lineColor3;
                        break;
                    case 4:
                        historyLinecolor = lineColor4;
                        break;
                    case 5:
                        historyLinecolor = lineColor5;
                        break;
                    case 6:
                        historyLinecolor = lineColor6;
                        break;
                    default:
                        return;
                }
                polyline = new GPolylineWithArrows({
                    path: lineCordinates,
                    strokeColor: historyLinecolor,
                    strokeOpacity: 1,
                    strokeWeight: 2.5,
                    arrowStrokeColor: historyLinecolor,
                    arrowStrokeOpacity: 1,
                    arrowStrokeWeight: 1,
                    fillColor: historyLinecolor,
                    fillOpacity: 1,
                    arrowSize: 15,
                    middleArrow: true
                });
                PolyLines.push(polyline);
                polyline.setMap(map);
                currentDay = thisDay;
                lineCordinates = [];
            }
        }
    } else {
        for (var i = 0; i < VehicleArray.length; i++) {
            var sessIgnStat = VehicleArray[i][7];
            var sessHeading = VehicleArray[i][8];
            var sessEvent = VehicleArray[i][9];
            var sessOdo = VehicleArray[i][10];
            var sessDriver = VehicleArray[i][11];
            var sessVehicleType = VehicleArray[i][12];
            var sessSpeed = VehicleArray[i][13];
            if (isSites == "True") {
                icon = 'http://ire.mstrackweb.com/Images/SiteIcons/factory.png';
            } else {
                switch (sessIgnStat) {
                    case "OFF":
                        tmpZIndex = 100;
                        switch (sessVehicleType) {
                            case "Bus":
                                icon = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAATCAYAAACKsM07AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAbvAAAG7wBureguwAAAB90RVh0U29mdHdhcmUATWFjcm9tZWRpYSBGaXJld29ya3MgOLVo0ngAAATfSURBVDiNtVRJTBtnGH3/bPYMNow3jA0EgwM2EMISsiiJCoVGghyaqmRRpFZRVaWKukmVol5z66VppeaQRu2pPbW9RUmkqkuqULcioRAHAiYBbLDZbDDgbewZz3h6Spul7al50nd4h+97ek/6HtF1Hc8T1HO9DoBJrMWhltBtEcW2rJR1aFrJVlSLuUwmdz+eWJ/s6zk0+28uieCFLs3/pwCZjSzbx0bv3JQVuc3va9IrHXbC84JOM3SJECqVLxQ2JEl6IOXz41IuP6YUlVChqG0M9h1O6U8pf/HVt3jrzKknBRRZsX966XJgNhzx1dTWQinIMJsEeL1eiKIId7Ub5aZysCwDKS/phUJBV5TifFFVJ1RVmyzp+pSUyy+EFxYi5944nXwmIhACQjNaLl9ANptHMrmO2bk0ZmZmsBAJo7GxEXWeBggmM5r8fmKz2ojAC420TjWWSsqQXirp5oqKbFdX10JgNLTAC3zIajEP17ttNwCABFeXbLHYxq2H98Ot6UQCwdFhZLPbEPgy3B75DQ3enTAYDHgYmsIOjwcHD/dC1Sn4WtpQU10NjtIhyxJymTTW1xOIRRdBgGJf/0snjg4cucqIFzqIqetFqq69F4l9ndjR2op4ZAUr4SCmJ8YhSzlk0imsrq6AYRhM35/A6EgADqcbp8+chZFSERj+BbKsQMrnkc1mQRGKtTsqTx0dOHKd/mCMMpm+v3fWFPjB6or+iGpbAq7WKph9++HvHES9txEcQ5BMxKGpChwOJ+LxNeglFTwvwCCU4e74XZR0gGU5OJ2VsFis4HnetLPJ9x2Dcit0SRCsIQXfhJL4cvgnXHDeQH+3C9Hug4j69sPR9Cqae15GPDIPZWsVa6tLYFkaomgGzXAQKqzQNA0ZSUJyOwWxwoIWQ7mYTOkectO/h7GurH7IZLNvf7K3vbr2/XPY/vgKzgfHQIs6SLsBmc4diDZ1IWbrRlx1YzOuAvlttHidWAzPYGlpDY5KN8xlNtidVXA1uFBeadienh4fYvoejJdRjB7gS7AfMpAzeylY8+UseGJAPsOjcAvghqPwu+fQ5rsGeVc1HjTtwb2yFoRWOLS39+JgPw+rg4VFlGEimzCsjYIEfif2kRBHOJajlZJaAa1Uzwn8oK+lZci8lc6eWt6qeIE2eCt1ImgaoMtAFBquMVkcd6nw7BWxeKAD1j3tsOQkaLENsBPzYP8Ig1nKI93ak1Q2t48TjuWglFRAK4ExcERTih5d1/cRltptoY2eN2mTdxCcp1ElznftHML9B9AxuYjzd+dgrciB6tkNOlqEOXgHeQAMgIJRxM/HhpTrKLzOyIr8+OPphJAIISTCgg1sKlLHRUpq+4w21veqdHW+dnfP5XfeM136/ArCkzOoTCmQ+E5sthSA5UWYXjmGQnMrrs7OIhhbiOkO2xR5VCeEEDxeLQbOiKKmgCNMhYziLqLBX1VT89qJoaHe4ExIOnkruJ1hDFW33QIlehuAOg9qm5uTc7Ho1HR4/kb5zrqvretba+TppjRwRshK4S9uZDjIKAIaOEJIk81mG1A1tSqVShPBWOYye2ocDS7X3PLKykjs4WyQpulI+8kTaWNtFX796CKg6/o/joFmn+CPIIoiIYRYaY5pBdBAKMrcMzhAdvh9f+f82N4zDv5v/AnqrkPK1B3U5gAAAABJRU5ErkJggg==';
                                break;
                            case "Car":
                                icon = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAALEgAACxIB0t1+/AAAAB90RVh0U29mdHdhcmUATWFjcm9tZWRpYSBGaXJld29ya3MgOLVo0ngAAAP4SURBVEiJ7dVdiJRlFAfw/znP+873zO7Mzuy8M+7Ojh8ru84mu6JGFLp9WGIGGlJbRngRRdJdEEYfEAR1E8RCgmJFaEVEhBdh4SLGiuWiZqur6Cy6u6bs9+7szruzzrzvc7qQljK704vAc/vA+XEe+PMnEcHdHL6r2+8B94A7MgYAnM5ksWpo4B8PB7PxbLS69oH6aDodCETAicR1TyrZly/ZZ1d//NFCeAZf2omGfbv/E6C/B+10JrtswqkkTzmVN4peeWpNug5tySyCgSpQIg6yElChsNaFwqn5/suHy4o/y3y+5/KZ9z9E69u7bg9cXNkGu1zeVi7NdZLjWHuLM/i0OEtErEHEWwIB1FTF9ObsMl6dyegz6SSX6xbBu2QJEpGIax79eWfrO2/uPfrCdrQf+PJfgNpomKlIsvaEf3E25AYCODI9LSPliswBpAxDqj2GdBteOuyUZWK6QAcnp6V3dEzGJqdkZr7ERi63efi9d1e3H/jq67+WTn3zLfwtuZtAu9+nusau7bKVoqWRatnU1savLG2k9YGgxMDsQtNoJCbhUJjDPr80l+b5PIiOHT1CfZfy0vN7Lxe93sbc4MByGfpDlc71DUY7nqksfNGrO3aQYxj5XNuqpfWdnTrBYF88gWCiVvtCYZ6bGMf0jK1PToxxw+SI7jFM/n7eQbQ4i/uiMf2wZfGDLS1Q9Rl4GjJiWJY2Q8FD5ULhg9jWLccpaVmqtbX1+LMdz6/tfv01PA4Nj+FByOPTvnCEORhCdV1G+0NhlnJFT18dZLaLqMwWwaapTa+Pye+HN2lpb30de1MWPMkkbsyXZPeFC4+SUmq5Ump7Npt9Yri/3273GqEWk5dkmOMeVggpQyJeH3zBiPhrakiYhUwFHp9CNG7R+Mg1qdg2lGWRUV0lc0T4aXiYvhu4gk/WP3KeABARBZk5DaIVWqQJRMkoUaxV0eKcSQ0rWC0KKlZBwyMRNuli+QbeIoWXN27SL85VuDCUx+iGDZpjMe7+Yj/Gi0W9qbGRa4cnegg3BQAAKwXXcYJKqTSImkHUJCIpAwitULxopaKGNR7VMOggeP3pbejoeE5fP/Qjxw/swUhlXlfFEpxadT9m2h/DcPcxMU/+ttW4tdGIyAaQB5DXjtPFzGmXqLnXcZvOupzaX3b9NUTxtROT2+xCgQZCAYkKqC5SwzHTj6FfemZOdf96/oc5e99Apdy1kGQiwq0YM4OZASK4juNXSqWIqAlALh6PP7lu3bqH8v39V+xzfSc8TPZV1700q3UvieS1yIgQSnS7TmZmaK1vvQxKKRARXNf1K6XiIlINQBGhqLVMQWRGM1cgAohAtMZtgTs5//8++BPGRp2HJBCRwwAAAABJRU5ErkJggg==';
                                break;
                            case "Lorry":
                                icon = 'data:image/png;base64,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';
                                break;
                            case "Truck":
                                icon = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAALEgAACxIB0t1+/AAAAB90RVh0U29mdHdhcmUATWFjcm9tZWRpYSBGaXJld29ya3MgOLVo0ngAAAT5SURBVEiJ7ZZLbJRlFIbf8/1zYTq9QZleaUlpS4GUIrRgIaBBDGoDQkyQSESjwUtMjK6JrkxM3EEh0RiCCTEkIHgjhAVhIYJSFLDlIkNL6YXpdWintP1n/v/7vnNcQA0QXZK48CzP4n3Oe5KT85KI4HGWeqzq/wP+EwDqfOetimBezkvhglgZiMrEcimEC2HsTDHGtb5/Cb5/Xtic11Z+n9eyZ/JRkfgH76N2955/Blx98/WXCTgEIjihMIJZWQhm5yCUny/kOCTMALMABDEGwraLiFqFuVU8r3V8bPTigl0tGgAubd2CpYe+eQgQIEAgAptOQ6dSnM5kFPs+RGtxIlkUml2AcKxIwrNjKhSNCthWsTZVnJ56RbtTCGTSEt++7bLr+ZuWHj7SAwAff7oLn+z88B5ArAWMAbRmZDIkWrN4Hlhr0q7L7vAQhK8SM3MwEqFwOMR+Og03EMAEKXJzckhmxeoDDaveOL3v4LELv5y+eGD/F39fL13ZtnWLGHOYMxlwOs3seYp9H5aZjTGKRQBr2YqogOPwYO5MNfFsM8rrFiEYK2IPUFOuj6kpF66bEU8bXZCX05oVlMObN6zfGxCtIVrDeh6z75P4PltjYK0ltsJGDMBM1gorFjKxYv7hSjuyu3uxeMlSql9Uy8n+Poyn7lByaEiGBhLBvtu9a5pWrlrd1d2bCmhfI8AWMEZBazbWKrYCzZYtWyXWgg2zFVEKYEeRar/0GxbWLoSfns/X49dVX28POm/E+XZfj0qNjgIQrqysUouWrlwS+KytfXJxdhTr8nIRZgZEYMVCrIXVDGsZYg0sMxwCrDVYvXY9xu/cQVl5BfoTfRgYHoWnDSor56G4qQk1NTVoaGiUjq7uNlJEuaFI5MDC2tpNNe4Ur43MUIu0hjGGjbXKsIXRzMxGzQiFOFm/TMXXrEeYgN7uLn56zWo1p6QQZWVzmJRSAJDOZNhRitovX35XkVKcE41OtrS0oCsvn44WlfB7vuHzdY00NLOQ2fdZjEfWGLbWEkTYasO+5/GsWCHNCIc5Es3hSdela/FO/unMr3zj+p/UnxiANhxyiKhaROZba5ffiMf1Rzt3Bu+MpyiZXYDo5m2qvbiUJkJh0SMjqsBqMRWV6nZRKYnRZK2R0uIilZ+fRxdbz0jH9SsqOZigRKJPZhYUkmU+4RDRiNb6bFtb25G7d+92j42NNQwPD6fbLp7rOHX86EnOzs8JPtE4Sz/3Ar6+dRunervote2voryiHB1d3VJRWkzR7FwcP/a99PT00ODwCDo6bsryhiqS0ZETJCJQSkFEQEQhIiohohVEVA+gwFpLsdKy2ZV1jXWjA325qYGekpKSUnlm/QbkxUqobn6VzJ07B5/vbaGB/oS4nk8V86rRtGohUbDgK3rwZU6DABARFRJRveM4jSJSJsKOCFRzc/PbO3bswK5du7Fm4zZZnvqZvIancCGeknVNT1JxcbF3+uzZC509vccWL1v2bWBamJnBzLivLgCGAJw0xpwjogWO46xg5nnpdHoqkUhEXXcSB/ft+ePm8y/W5J/vSyZudQ4c3v9lb3dH500RuUZEp/Z99+PgQw6ICI+GAKXUdD9MROWRSKS5urp64+Dg4EgymRyAiC/ACBF1A7gpIv0AUsysRQT0b6li2tWjIKUUiUiuiMTuO51g5nEA3gMrhrX23tCPO7b8BZRlDRCNGPZnAAAAAElFTkSuQmCC';
                                break;
                            default:
                                icon = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAAVCAYAAACkCdXRAAAABGdBTUEAANjr9RwUqgAAASxpQ0NQUGhvdG9zaG9wIElDQyBwcm9maWxlAACtkb1Kw1AYhp/TioJDEAluwsFBXMSfrWPSliI41CiSZGuSQxVtcjg5/nTyJrwIBxdHQe+g4iA4eQlugjg4OAQJTiL4TM/3Di8vfNBY8Tp+tzEHo9yaoOfLMIrlzCPTNAFgkJba6/e3AfIiV/xEwPszAuBp1ev4Xf7GbKqNBT6BzUyVKYh1IDuz2oK4BNzkSFsQV4Br9oI2iDvAGVY+AZyk8hfAMWEUg3gF3GEYxdAAcJPKXcC16twCtAs9NofDAys3Wq2W9LIiUXJ3XFo1KuVWnhZGF2ZgVQZU+6rdntbHSgY9n/8ljGJZ2dsOAhALkzqrSU/M6fcPxMPvd90xvgcvgKnbOtv/gOs1WGzW2fISzF/Ajf4CLL1HpgAAACBjSFJNAAB6JQAAgIMAAPQlAACE0QAAbV8AAOhsAAA8iwAAG1iD5wd4AAAACXBIWXMAAAsTAAALEwEAmpwYAAADIUlEQVR4nJWTX0hTcRTHz93d7v6pu9PUKBR7aGVp4ks+FmLYQ4gJPSlI9GAhmlBWZhJlUpL/0NJsYqJh4YP5EmQUWg+JRhSWuqmp2Mp05r+5uV3dfp2fu4rXiG4HvjvjnN/53HN+fxhAC2cYU7JGk5ms1yfF63RxPW73p5cOx6tut7t5mpBhkGnMTobZl28wVGVFRBzXBgQAOBywhJpmWWiZm3vRsLiYh0CrLNgZjaasLCTkAp+YCJCRAdDeDiutreswm1YLNfPz5W1u90VZsMdqdf9JnS5Wl5ICUFAAYDaDs7ISvvl8YA8MhC5B6L/u8cTJgnVxnBBDiCpIrwdVZCSsTU3BT7sdpjC5wHEwCrCaLQicLFidUmk9plCYFIIALAbcqHnUEsqjVkOv12u5tbYWLQuWwbK1BaGh51ROJyzixq9gkIrDw1hBPbTbazq83lxZsDCG2XuW41rSo6ISgnAsJWrW5YJ53LO2iYl3rYJweoaQEbr2LwzfJoz+INB0RKHITovakxu4KpAZTs08Gx+r6PX56hE0RtdRQ69AEbGWUEPvFWNE8rWrOZfJKkNcjmWH7kFjHc1xyFChV6JnRRiIxV5kCag1/E/lk8DM1Y3EEBTgsY5Y1UUlRTRnRIiOAUaLM1AoKy6l3VCQE70DvYcCJbCK8noSbOR9Q0MDitK7N2lxFCoYgXr0GzCclxEIrIPsCJpF7/wDVlJ6D2FGGBz8DDVVd0IwdJDCUNptXdEb9Av1AzUD/gvglcAKiysIz/MwbBkA8/3ycAwdQu2geyfuiyB2sSjC7HRMMSc9gPxrpSTQEARjo1Zoqq/SYCgGFQH+U6cdLIiwZZRLhG+cpvTu5F0pIQH4HifHR6G5oZrm4lG7UDbUpAigxT4RQLbWS2A5l4qJDt8ohT1pqg3D0GHUNMoidiQp3m5bYbuzzhfaeGMwjFi+QPvTR6cwRt/7R7Gjf9pW2NETaeld0Qfi4H3PG+h+/TwbYy3g32BZtgnDuxRp2h/dFhuXkPCh723f+NjXTPCPJ9s2YSzLKlNTU28YeD7pu83W0dnZeft/QNR+A+EeQycf8kL4AAAAAElFTkSuQmCC';
                                break;
                        }
                        break;
                    case "ON":
                        if (sessSpeed > 3) {
                            tmpZIndex = 10;
                            switch (sessHeading) {
                                case "N":
                                    icon = 'data:image/gif;base64,R0lGODlhGAAYAOYAAACMOajItGO7iNbd2T2gZ4uolr/KwyKlW+rw7WGXeHTDlyWIT5jUsorCozyLXXyfi67Wv8/Z0xqCRUiRZ0uyeO/58+Hm46S3rBKOR5q6p0awcxmdUzOZZsfZz0uweLG+tpWyoMXl04LDntTt3onLpnytkm2hgle3gjycZAqaSdzz5g6NQrDdxJ7UtlKkdLXMwHezkf///zmtawiGPCupYu/x8L/YytDq3ESNYiyYWiqJUubo5pPRrdzl4Gu0iszMzLPZw6PXuLHCuIOulpS9pYbOpW7Bkd3g3nrGnECvcKa7rQGUPx6DSR6dVImulqy8s+X07c3p2VaVcPf7+A2XSQ6aSlyZdQSOP5PHqWm9jV66htHd1q3evU+1e5S2or3hzM3W0bXfxpTWrX+ylDOpZiKTU3ukjODx6Eq1cy+nYuft6gCMQjinaWKffDyNX5y9rbfBu4fGoo/PqtDt3UqMZqXXurPFu46vm63HuAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAEHABUALAAAAAAYABgAAAf/gBWCg4QDDwWEiYqKPTAlNRUxi5OSFWBxY2qRk4uVIC1YEZWciZJqRFxcF6ScYG8hYRkDrIo1F0AqIwEGtKUXUVAVNnBTvYM/L2cxMWd2xcYVTzZnU1NnNkfQFiAsc3NQURBK0GANDGFRUWF1Tpq0MUIiPEFfIVwMJTu9MSZZSDwtWDAo4gNErwgutGSJA5AEEgFtnnGq4QVFly4L5SjIQoEAHlY93DSRQeGEvywnZKAYwqrDiioHyHQ5YeSEBhobJiAgdWfGlSobyJRMkqYMBiZbSCmRYsYBlSZsPLDJYcZKAnecdjp5GfRAmVkSWeEAsARoCgw2oNWgI2HBAh06EBZ8mDIK5IseCLZEsAFpUSAAOw==';
                                    break;
                                case "NE":
                                    icon = 'data:image/gif;base64,R0lGODlhGAAYAOYAAACMOafAsmS9ikOsctTn3S6UWoqKiry9vGaAcovIpSCjWR+BSefv64eMirzdyWaYeZmZmazPu3y+mVmkd8zMzDFvS1RyYRCNRj2lafb29o2vnHi1kmWgfc/d1X+Ggp7Uttvj3rPVwRidUmzCkaS6rlK1fXGuiwqXSCGSUnCLfLzSxS6lYWG7h0iRZ0ezdo+5oun275jGrKXYvKjGtJKhl8Tczlq3gsfn1P///0CbZNfx43KTgObm5k6hcqusqwyNQxOUSnzGnSGcUnS2kMDm0a7Yv7XexsLDwtjY2O338qSlpc3r2mvAkIegjz+sbYzPqmp7cVewfllwYpSimYKLhpLQrMzj1iijXq+zsThxTfj9+tvw5XqEfo27oO/v76LWuOft6XLElL3dyrTXw1y6hQ+dTdXu373Fva3eve/394O1mAeaRjKrZkqtcxiLSweIOobAnjmraoHEn4aSi2uVflB6YXiLgb3WxUuyeKXMtgaYRCGlUnO9lJWunwAAAAAAACH5BAEHADgALAAAAAAYABgAAAf/gDiCg4SFhRlahoqKWkhnYIuRg0dzNDySixRKCHR9GZiEXjxKUDsSalagghk8WB47MR8JNao4SEoNdhpFRjIRiZJaWj5cUyRiSzo3eR3AjF5HBikvIWZJ12IzMJFIB1QWD08fRAyCMBHNil5YUnUtUWHiN2bCBCqfhl4QFW4oTmQjnsi4sWWLGSvlFIHhAOCEAhclmFT5QoSIESMqImkpksOhE3hVZMj48iVGB0k8mrgR4cRGmCpVnjyREwGfIiRQslwQMaBEmCAjgoQxAUJSgAVv9CgYQEYACwE2SqhJIqlGDgBreOJx4sJJHAx3QDmYcEEBmxUrrii4soEqKAI9JoCUEQFnwgkUtGrV6PFDAo4xBYY4UxWiixccWqzYrCVoMOPHhQIBADs=';
                                    break;
                                case "E":
                                    icon = 'data:image/gif;base64,R0lGODlhGAAYAOYAAACIN6jJtWe2iDStadjh3ImuliuKVLrEvlaYc+/w8Ge+jYi8n8HfzgqWRq7cwyilXlC0fHfHmpjSskaudOHn5IvOqPT795+4qBefUziFWYOvlk2SasXRyg+CPZvBqtHe1mKXeniji6XXvHHBlNDr2+D06QebR7G8tSyWWli3gbrKwJG0n0ixdYDIn2yhgzGkZMne0waMPz6ubZPRrsLNxrbIunm3kheTTuns6g+MRT+JXtzh3kqhcOL37M7p2V66hZS+pRWZTR6lWcjYzqS5raO7rjGtY////7XdxTOZZrvZyMPl0UuwePD08nvBmHOsjGq/jqzZvjGJVg+VSYuynGKZd4bOpRiBRIKcjJS3o63BtYvIpYW5mjakZZ7Vtdbe1hGbTQWUQ5TTr3rGm0u0eEKMYtrx5YzPqtPt3uP06r7Qxd7m4Uqtc8bo1AGRPI2wmymUUsHJxFCccCumYZy1rczMzJ/CrXPFlLXFtYPJobXMv+Pz6+b371+7h2KdfBmMQiH5BAEHAE0ALAAAAAAYABgAAAf/gE2Cg4SFhoeIiYqLjI2Oj4MqK0MfkINHbx0xBhpqR5Y1MQ1BSTwuWjtrhUesn00gOgVVUxgDZCl9TgtURHEWOK6EZQAxU1MPExB9EWczIkp6RBc0OIVlbiZgcywQAmNnEkg+ZmloUR5AKzQEHzgbDWAvTBAKLTNe4j4Wnz1tDiJbbITAsmGKkQkpoIyp4MUBAzR79h0xs8SBBC5PQlxAEEZIFxkJR+SRIAJJmxIlYASwc4EDgSasMuS4geIFSAURrIgREYWBmhN4aCSwQOhAERgX4DwYMOGHgCcaVnBIkGCVIFcr/sR7IUcDBwpEFbnScEWKnyxrqkKywKESTEtwB+PKnUsXbiAAOw==';
                                    break;
                                case "SE":
                                    icon = 'data:image/gif;base64,R0lGODlhGAAYAOYAAACUP6XFslydeEllVMzMzI3CpDCXWm3AkcXFxW5+dFO2fuDn4yGUUUatcpvWtDmsa3p6emZmZs7o2b7kzkegbIfMpef07XywkQmYR6/awtbW1rW1tV1uZHjDmKPXuhx7RCOiWlm4go/Pq9jl3p7FrzKcXkKucbvTxb3ezm2wivb7+HXElyijXRCaTIS5m1+6htbe3kKfaD2fZWWAcNTt31RoXd3w5aXevQyKQoSEhHJycpi3pMTl0qO1qb6+vpHQrCqeW8DVyiqkX83r2RGVSS6nYqzEt2ZmZjiKW+338V66hejt6qXWtbTfxtzj3qvbwM/q23fFmdPh2cjd0JTWrQeXRXJ7dXnGmt7e3qTKtFimeiGcVnmFfmt1bkekboKwlqrZvoS7m2G9iWmAcZXAp8bYzQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAEHACoALAAAAAAYABgAAAfjgCqCg4SFhocqIzsIiI2EMjg1WEkxYxqOhEklAAA4AwYAHzmYgzIYpxhEqkQDl45SJS0gLLRCtkVIo45lDBhCDybBwiY1BJhLJSAmIcwhSkpiAhCkTkAPIQcr2itXVxzGjUleIA8KBxUi6elfVo4UVeTmFQ5g9fVd4IdSWlvl5w5NeAjkYUQHphEp5AGEYqOhBCuMMEkJEwagBAsqktAIYJBUIhIZhmDU2CSBD4+CFgSh0ZDGBDIRUA5acGKCTQ9jNsgcNCKLBw8uYu4cNKVAhRk6hw4KcoELFqUzeySFSrUqqUAAOw==';
                                    break;
                                case "S":
                                    icon = 'data:image/gif;base64,R0lGODlhGAAYAOYAAACQPZfVslu4gszMzDSoZ4GlkKnavh6YU97w5zGta3+okIDEnqy9slOidRSKR+T17Ja2o8zp2G6ihL7izUewdCqaWa3NunvGm0qQaKi9sdLf1w6GQY/HqAyaSfH59CekXWy/kKK5rKLWuGi+jGSaemS9iufo57vLwcPm0j2LXQCUOkyzeI3NqSGeV9vh3rXex0WydLS+uIC1lxCfT+3v7tDs3ByGSUGgas/X0lqScXeqjTCmYn+2l6HNs42tmkqUaxGKRPb7+K7DtwWVQ9735pu2ppHHqAWMPbXEvJrUsyOhWabYuzmra0q1czOZZr7XyE+zepTRrrXezofJpMfn1XK/kxCbTM7q27rkzNni3ePg4XOkh2ybfwKWQqzdwiGcUqe7r8vTzjqmaBORSRWFRIrNpvn4+BicUdXZ11SWbwiIP7jNwQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAEHAEEALAAAAAAYABgAAAf/gEGCg4I0Qh4mNGYnJoSOjmZBRWQODmobRxhmkY+QQTRjMy1WXQBbQZydg5FaBx8Eo10Kqo4uOSRcFQQUsFYpJBgMtKg2HS07KwK8SlZDRxnDZhgtuwIjAitMSmdAGsNBQjsw1hcg2Ewfad9BWTcCJRcsC+ZQYhDrQRLwLElR8yUN8AXJUKVMAC8i/IGQQAMfDR4BDKB4ISLAAiGo1tHwsWTClSsvlnDAITAIEixXECCIgKVIySBoniAI4gEBlRip8MV4IAjBmkYlzTCg8uABlRANX7qwUKMGFowvP0HAgqJHmIwvQxjwci9qpDBGktzLWVLGFDRRBUXyoQNrWjAFA74FAgA7';
                                    break;
                                case "SW":
                                    icon = 'data:image/gif;base64,R0lGODlhGAAYAOYAAACNPKbBsW2tiTSoZ9XY1mR3bYqVjr3ZySaBTY7FplK1fPDw8LDfxSigWxeQSmaifniyj6ClotDd1ZzUtODp5DGta0Z6XIPJoXWCe5S9pVaDab3dy5HFp7LHu5mZmdXt4PH59XOVggKVQXLDlSGaVTOZZs3q2qbJtX++mVa4gDVxTmq+jqHWt0Cub97v5szMzHN6dgycS53Era2vroyckuj07v///wuLQmuthrXezlZuYCSgWtzc3ICFghqcUbO0s0KTZInNpsbh0cLQyJDMqsjj1IyhlVi3gW6cgYG5mDd5Umu9lO/48ubm5q3KuRKPRanSvOTv6LjQwgaVREOwcw6TR8PXyzmra7Xfxo/Pqr7kz97v3tDs3IbMpV67hWpwbBuXUIW/nnuWhmm0iiimXny5lSGhWZrNrzmmaNPg2C2HU3uAfaLXurXmzs3p2JKimAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAEHACAALAAAAAAYABgAAAf/gCCCg4SFhoeIIDaKijaLiYcLUY4LMguQh2U4k0k3CZiFB1VgGWFgMVUooIMCOzsNsGQ+AGeMkBtoaC27VFc+IgBjj4hMEAopyF4KAyTAahSQEmMrI9Qryz43KhhRkCcXQUFZXSspAz4Ib03DhTYSHCwT8eQKV2YOEC7shVJYWFpaJtCzN+VJhkQUhJhYuIHFhRVHrpB4ouQHogVDXDCpwQULiyDXgFjQMePSoTQHmNhg8qHNhCAPNKyZwePQogVO3NQAwaQjhxAwItgy5MgKFBMfuBTpQOOLB5OIHslgoAXLCRo9IjQZahOEECJszoiB4WHdKhAUknQJUcADBaigHKIEQNKWgKB9kAgYMfDiLldMTWZs9Xt2ENzCgQAAOw==';
                                    break;
                                case "W":
                                    icon = 'data:image/gif;base64,R0lGODlhGAAYAOYAAAGMO6u+s3akiszMzDSraI2umrHdxSmMU9ju41ujeoTLo2K9iSGHS5jTsZu1puj07UyQaLngyiikXU60e9Hn26zavxiZTnvFmvX39giYRiuVWIKnkW3BkcDIw9Xc11u4g7S+uMTdz0OvcpLPrafPuACMQrfOwZ+/rOTo5Xawju7w71Sccy+fXtbu4InIpCOkW7bGvTaFV0WmbhOJRROWSi2jYcTn0369mKPXuo6znhuMS////9fh3Oz28afDs2G6hzuQXgmTRI7Oqn+cis7Y05y1rcDgzheiVIytlLnZx2Sce8PKxd3n4SGLTnXFlzGEVRCaTEK1c93x5VOObF2XdWq/j1O1fIHIoJ3VtRmcUoSvlpfTsO/49KvEtcLm0bvDvo27oN7e3nurjzirai+nZNHs3LvPw7LdxACUOs7X0YvNqPb8+AuZSSmcWqXWtQaOPpvGrjOZZkKta6XevXmmjKG3qr3mziOPUjCJVhSdTwAAAAAAAAAAAAAAAAAAAAAAACH5BAEHAFwALAAAAAAYABgAAAf9gFyCg4SFhoeIiYqLjI2Oj5CCOx6RgyZ0eDFdXDuNKGEbK3EsUAebnZKFGBhEDlopCx8iBC9AHUxLA5ySk0sgJkkkWCNXHD8TYxIadzNTghhrMA4nJhQPLV5nw1dVC8hkR0FiIAVaOVgGNlJrOz0INhE4Wwoc3nISNEpDAjdCcxFlpHB6UMbGmS1CLvywIqJGniBKVHjgESAHHBJeEDx4VwGLGoUMJWTJAEBMIR4g6nQJYaRCAyFOFoS0kOENAAiHUKj4UgeMiwuxRJDJooOKgCcbBnVCNYgIjAIJZLDIwoYBJQyOVKgwo6TJGwabKqkI4QNJmkpK0apdyxZtIAA7';
                                    break;
                                case "NW":
                                    icon = 'data:image/gif;base64,R0lGODlhGAAYAPcAAAGNPK3AtGG9iUKuctXV1SGgWZKhmWx6crbWw1V0YYS3murv7CKHTZPJq3iwkGmWe8Hm0VuZdEyTap7VtoaKiJS1oeDo4yGZVAuWRrTgx47PqjOVXff392C7hzBxTH2DgKSkpNfk3KTLtFa1fsPEwwuHPjClYm/Dk6bYvG2bf2F6awiZR9/f37/gzCKlW9zx5njFmp24qer28K+vr8bm1JLQrUK1c3vFlLm8umiBciqUVRp+RHOUga3Nu8XRyhiPTKzavlm4gyelXv///1qwfjOZZs3l146Ykny7lkelb9bu4O/z8Dmran++mmi/jJ6ro+P06rrax8ng04XDoIGHhJmZmQ+WShGaTCilXyyWWnd9eqHWuA2PRWdxa6W6r0qxdm2Mekqtc3PDlZTWrVy7hOXl5e/v77rXx5nHrIzWrT2eZt3j3+/394W+nZW7pq3evQCUOqzGtlV8ZsTl0UmebIGUifj8+jV3UFezfzKrZsDAwHiUhLPQvsnZ0Mvr2Gu9lKWtpYbFooWKhxGeTwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAEHAGwALAAAAAAYABgAAAj/ANkIHEiwoMGDCBMW5GCGg8AhQxQatEPiSQALbCBKNMjiCA8FRjRuLEgiR5MpPULYGVkQEJgJKETECeGQJRsWXdBkgNCCj48FIhNqBLEHiBIoUEJI6WNhpdCICz5U8CNjCAclGdAgWOL0IEQzgOpEqcrhRQYUgRy4USKxjCAvR18ombNFwwkiSdpgTAjCwBkagGkA0QCDDBMTWdosQVhGixuYQFBsGXOiwwAXVjZE4ctDg2cNNWrA6PAFCx00i72aoZACxgkxsMU4GYAFiUQOOBLgERBkBJnfX0xcaGAnIkIzVCQMWM58QJ5BXBTURKhHjg4sWApo136liIiuBwmoN2BgxcoPLivSF2kBHuEMDwB0PLhTAoAaBMWNJzRTJQKaBQfsoEMU+Y20wBqLERADfjYZBFFQAQEAOw==';
                                    break;
                                default:
                                    icon = 'data:image/gif;base64,R0lGODlhGAAYAOYAAACMOajItGO7iNbd2T2gZ4uolr/KwyKlW+rw7WGXeHTDlyWIT5jUsorCozyLXXyfi67Wv8/Z0xqCRUiRZ0uyeO/58+Hm46S3rBKOR5q6p0awcxmdUzOZZsfZz0uweLG+tpWyoMXl04LDntTt3onLpnytkm2hgle3gjycZAqaSdzz5g6NQrDdxJ7UtlKkdLXMwHezkf///zmtawiGPCupYu/x8L/YytDq3ESNYiyYWiqJUubo5pPRrdzl4Gu0iszMzLPZw6PXuLHCuIOulpS9pYbOpW7Bkd3g3nrGnECvcKa7rQGUPx6DSR6dVImulqy8s+X07c3p2VaVcPf7+A2XSQ6aSlyZdQSOP5PHqWm9jV66htHd1q3evU+1e5S2or3hzM3W0bXfxpTWrX+ylDOpZiKTU3ukjODx6Eq1cy+nYuft6gCMQjinaWKffDyNX5y9rbfBu4fGoo/PqtDt3UqMZqXXurPFu46vm63HuAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAEHABUALAAAAAAYABgAAAf/gBWCg4QDDwWEiYqKPTAlNRUxi5OSFWBxY2qRk4uVIC1YEZWciZJqRFxcF6ScYG8hYRkDrIo1F0AqIwEGtKUXUVAVNnBTvYM/L2cxMWd2xcYVTzZnU1NnNkfQFiAsc3NQURBK0GANDGFRUWF1Tpq0MUIiPEFfIVwMJTu9MSZZSDwtWDAo4gNErwgutGSJA5AEEgFtnnGq4QVFly4L5SjIQoEAHlY93DSRQeGEvywnZKAYwqrDiioHyHQ5YeSEBhobJiAgdWfGlSobyJRMkqYMBiZbSCmRYsYBlSZsPLDJYcZKAnecdjp5GfRAmVkSWeEAsARoCgw2oNWgI2HBAh06EBZ8mDIK5IseCLZEsAFpUSAAOw==';
                                    break;
                            }
                            break;
                        } else {
                            tmpZIndex = 100;
                            switch (sessVehicleType) {
                                case "Bus":
                                    icon = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAbvAAAG7wBureguwAAAB90RVh0U29mdHdhcmUATWFjcm9tZWRpYSBGaXJld29ya3MgOLVo0ngAAATiSURBVEiJ7ZVJbJRlGMf/77dMv+nMN1un7XQ6S7eZLpQWKEVKCVQQSqK4oGGJXsSDJpqYePJgjAc18YoxGDUxHEyUC4YAASQeoGzd7NTS6TK005a2TKftzPTrzPRbXw8Isb3ghRv/5Eme9/D8f8+TvMmfUErxLMU8U/fngP8jdll2kKaGinZD1z9KpVc+XZGkbxeTS+/GJuIFg0P3FsbG42q4plLZOPjjmbNo2bLpqQAydn/a0d/ff0lR5LbaUA2KS0ogWq1gOU7VdT25KkkTeVkZURRllFKMZLLZ2ejwWPyDkydSG81CW3ZhfODWekA2u+Y+9d3p66PjsfpgRSV0TYVgMiEcDsHlcsHn90MQBACUymsyNF2XVFWbVFR12jCM4dVc/t7S0nLk2FvHhqma1jZCOZZnAYbVZFVFKi0hk15GPpdFfGoKkxMx+P1+1NU1oFC0kdraelitVpu50NzMm9hmRV47bLFYqcPpXOmL9KVv9UWjmkF7feVlXVVe558AVI5fWqI7t+4HI3ixujKD6fgoFhILcLuLEY9PQlUVzM/NYnCgH16fHy++1AmGE1BZHYLH44HAM0RVZLsi5+yp5eXg3OyDQ7lcljY3NX1+4vjRL1ll88XCSuvkyd1BX0lF+Q4UBZvg9ZaDJRT3hgYh57IgLIuJ2Bhy2VXwPIcrF8+h7+4tFJgLwRMNF879hp47t9HbfQf9vT3o7b5LbHZH+aHOg7+yN19dtJzPRN67nb7gFnAZtWIa9Z4wguU7sHn7fnir6wDomJ2OQVlbg88fxPzcA6zlVgEKuEvLcLPrBlazOTAsB4+nDCUeDziOc9jdpQmWe42x5Tj6yZRgWP6IpNH10wimxd8hit2oYfKoLqpEoLodNbsOozxYBY/bhmRiDoKpAIFgEN5gJRYWU1ANILOaRSKxAFmVURGsIF5vgCP8L7zASOQzWVA+3HS+0fF+5cc4tXgG9/d0oQnAds2MNi0EP7sbsrAbMcWOpfQysitzaKirRiqZQGRwCE6nCzbRhiJXCYqKShHwegwlJ93hjLd1gSGma3DD6Wi0v1NzvMxWc9WCGA9EGCBC8jjLDaJaG0Rn/mds4cMI2zowb6pHcl6BL9CAlk1tKHWJcIsEhYIEhk8iLl0iQxODAgHA8uA9KtRa0SIebmna9mZSWM5EW6MmhOE3RN0MlQLqv/+6G+jwAfvddlTlWxFqOIiCUgNJ5SFG8+MYyU8hIj2EzHuNOJu5TgCABw8VKgghAoBaCtpqQkEtE2YqjHZaZWzVQ5pPFW1nnXhhYBdioQTir/SiugDYZinBLGNCj/QAigJAA0CADvPLEEdt4+S/gUMIedQUgGNltooBtjGEbYSDDSjtqi8w42/54YvTtuuXb+BrxzegTRpabAfQKrTh+5Gv0OiqR0dxBzBGMNk1qboN1xXusTGlFI9hpJBoAMYAjOnU8JEUbaYXtZBkz7A3+rr2zCzMgCfMohrn3fN/RTHslHFcP4q9jXtBp4356Gh0wJaznm5uqbu2/gIzAc2vj1Ce8AAAgzHshmHUFxcXHxULraUz0mwaIl9eJQX8NeGKlM7Tv3uHu3tS6UzEYraMv9H5urJzRzOebL6xOHDr3gzzKDocDgchhARZMPt4MPuIiWxlGbboyIEjcDrsTxZ7PEeeh/7T9A8rcDMGPdQofwAAAABJRU5ErkJggg==';
                                    break;
                                case "Car":
                                    icon = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAALEgAACxIB0t1+/AAAAB90RVh0U29mdHdhcmUATWFjcm9tZWRpYSBGaXJld29ya3MgOLVo0ngAAAPgSURBVEiJ7ZVPbFRlFMXPvd97M30zr/PamYG2U1paSmtLidFWpGAT2CkJkChECQY1GoyGBRuNYUdcCFt0YRSMiTEGE8UENNTQxEJqtShiqlLMjLaBgBQ70zL/3nTee991QSSIuIOFCWd7k/PLvbknh0QEd1N8V93vAe4B7ogMAIgdCCO/e+Efg5q97e1mY37AcBZSFNZAiC6Txee8aT2Rf6F0Izzrjgzi5BOj/wmgm4MWOxDu9KZji72z9a+FGysbjQEXqn0BHA7AYQaFFbQhulqu/qBmjRMraOWh0S3fTO05tgf7Nu27PcD5wIKUZKt29ZvwuLHyiQ35LkfErAnMakAhVKe0+YjBusfT/bk1vDq8FqsSA4glYsGXM8d27d+0/50NB7fg+M5P/wVQxv3cpBrUODebNsGAniwKciy0wMSmErMuKlamjmKTDWLlask8E5drv7uSzeakUqrwA8n+jbtHXl01tPPIR3+bvnv+bfQnV13fILor4mDKmW/pasQfa9OaHGF4QPBLoGVcsZF1EL+U1FZdjIOWipZ6n+PTLcicn0Ai0aATdXFO9Fqy4pnOj1vjzUeUmMdf7H65eONEz770HJlipvt6+zpev7JXF1M55iiDHaUpCvazYVDe1Txps+u42ppqYnw9h0Ioh9DykLYfCvOSh5eg3V6Gzli3LI926EQoOeS67hvb79sxpmYuXlERK7K9r7+v+bPPT5LnhIGyhhQ8kbwm7SqoBi3U45PZBPEXV0gPeDBXmlAdSqTep3KphGq1qj2/ygW/wBXtduXz5ecPv3X4FCmlupRST7e1tT2avvhbiXvrbbTZy6jBSHKkBBWqinKKoBoRsolELCE7QGKmHl6kQnO5ohi+icZIipxIraBCyH07T1dHL2P1K/3nCAARUZSZUwRaAZFuImpAhOK6zW5Hi91KS0NLOFpRRm1RAJA3RXCOKqx/ar0+++Bp5oqJzdZmnaxJ8vtD76E4u6Db1rTyRZo6TbhOuP5SrOAHflQplSJQDwHdEDTBEBvNkWbdUruUOkKt+nLZ3rFoA57ctk1/8fNRPlj4EFKOasdwuL+1B48563AiPSJj2bHHjVsbjYhKANIA0r4Ohpk5RQH14EK5my+4TTIKS9Wq5Oxgdmu+cI1i844E1+JkWVH2dQ1GJn7Mf3Vp7BwmCoeCq9Vh4yZjiAhuBjJzGUAGQMYPgmGlVBMxdUsp6D1z5vtF0WhkMJPJTGH6z/GKkS2hWP2V3OAnTZIWyAxpcul2nczM0FrfuhmUUiAiBEFgKaWSIlIHQBFQFMiciORZs6chEAi0aNwWcCf1/++DvwD0MLFiLcHrLwAAAABJRU5ErkJggg==';
                                    break;
                                case "Lorry":
                                    icon = 'data:image/png;base64,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';
                                    break;
                                case "Truck":
                                    icon = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAALEgAACxIB0t1+/AAAAB90RVh0U29mdHdhcmUATWFjcm9tZWRpYSBGaXJld29ya3MgOLVo0ngAAATkSURBVEiJ7ZZNbJRVFIbfc+837Uyn7UxbKO0MLRloC5VagUIthIqESBSCEEklMbhrWBvZYdxo4hqBxGhQExYm1h9QFBIJhAiVFqTQaAy1P7Qdpp22Qzst7fx9957jAjBAdEniwru8i/e5z01Ozksigqd51FNN/x/wnwDQkvPBan9B4Wv+QGEYjDByNsRZlJMrJZKTlM7RDc2eq47xXHWM8+sve6/PPxmy+asNuNx27Z8BwTO+10H4kgDkKy98jg+FnkIU5RVKHjtErECkRENBGQ0lesgjnm4N3W2M7Z67O9tzcd8VFwBKTvkxs2fhcUDgrLcNgg7KEDjLLFlRnGHABfu0TwULSlAWWMTBQEB5vFpcuJSzOSxkFiTlppEyKbmnFn7Lupnds3vSIwDw7geH8f6htx4Y/OBrg0GHpIUlIyQZEckyOAeCVSJWQSwRmMTxKlLenGTTOVgLCEAlngCWFISxpXTne6tSa09f67rYc+LTj/+eXio+md9GLnVIRiBpYUmxkhyBjWJiR7HRIAazaEXacJC9al/lATRH1iBUWMXMWs2nMphPpTGfTkvWNW5ZoKi7IE869uzcfswhl8AuQzIPDHLEYh2Q1SSuZrEaEEViicXDFPJFeOzCKM4WJ9HQuIYaV6/kiXgUs8m7lJiYkInxmCd6Z7S1ZeOmzUO3R5OOZAQkBLhQyAnDKAWjIdZhMVqBNaxRDFYKZFlrUjdvdKO2fhVymVq+1XdLRUdHMPBnH9+Jjqjk9DQA4UhkhXpm7cbnnNQJd14vI6h6gERBRAOsIa6GGAUxDsQqgBVIOTDGoHXrdiRmEghXVWMsFsX45DSyrkEkshwVLS2ora1FU9N66R8a7nXsBenMy/N+V1dftzseHIddCcyWZSAMwGqI0RDWgNGAdmGtRXVdAyJa0NV1BVtaN6N1YzPC4aUgdX9u05kMtFKITUwVOIoU+4v880ePHMXBtw9S6VApD14cxNJXGqkfIzxmJwCjCMZheBwSEbbGwBqD0sXl5M3PZ5+/CPOpFEVjcZ6amkKw0EuBYCmyhvM0EdWISJ21dkNfX5976J1DntnpJAW4HHvX7FdLM5XkEa/cnZtVbl5GQkG/ipgGsjZH1hoJVSxRwWCAerovS/+t31UiHqNYLCrB0nJikbOaiKZc1+3s7e39em5ubnhmZqZpcnIy3dvT1f/TmW/OOb5A0erqtaXbal9E/NIwUt0Jat//Jqqqq9A/NCzVoQryFxbjx9OnZGRkhOKTU+jvH5TmphUkdyfPkohAKQURARHlEVElETUTUSOAMmstLQ6FF0Ua1jdMj0eLk2OjlaFwSLa+tBOBxZXUULdCli1bio+OHaHxsZiksjmqXl6Dlk31RJ6yz+nRlfkQBICIqJyIGrXW60UkLMJaBGrHjh0H2tvbcfjwh2jd9YZsSF6ibNMLuN6XlG0tz1NFRUX2587O6wMjo6efXbfuW+dhMDODmfEgXQBMADhnjOkiolVa62ZmXp5OpxdisZg/lZrHF8eP3hx8+dXa4NVoInZ7YLzjs09Gh/sHBkXkDyI6f/zk9/HHDIgIT5YApdTD+3wiqvL5fDtqamp2xePxqUQiMQ6RnABTRDQMYFBExgAkmdkVEdC/tYqHVk+ClFIkIsUisviB6T1mngWQfeSLYa29/+inXVv+AhyttIZvOC/HAAAAAElFTkSuQmCC';
                                    break;
                                default:
                                    icon = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAAVCAYAAACkCdXRAAABLGlDQ1BQaG90b3Nob3AgSUNDIHByb2ZpbGUAAK2RvUrDUBiGn9OKgkMQCW7CwUFcxJ+tY9KWIjjUKJJka5JDFW1yODn+dPImvAgHF0dB76DiIDh5CW6CODg4BAlOIvhMz/cOLy980FjxOn63MQej3Jqg58swiuXMI9M0AWCQltrr97cB8iJX/ETA+zMC4GnV6/hd/sZsqo0FPoHNTJUpiHUgO7PagrgE3ORIWxBXgGv2gjaIO8AZVj4BnKTyF8AxYRSDeAXcYRjF0ABwk8pdwLXq3AK0Cz02h8MDKzdarZb0siJRcndcWjUq5VaeFkYXZmBVBlT7qt2e1sdKBj2f/yWMYlnZ2w4CEAuTOqtJT8zp9w/Ew+933TG+By+Aqds62/+A6zVYbNbZ8hLMX8CN/gIsvUemAAAAIGNIUk0AAHolAACAgwAA9CUAAITRAABtXwAA6GwAADyLAAAbWIPnB3gAAAAJcEhZcwAACxMAAAsTAQCanBgAAAM3SURBVHiclZJZaBNRFIZPMskkzSTppHZRg0WwxlabBl8MPogiPhQRKaK++FB9EinWgAti9UFL0KqtxaVa3DcUQa2gaEGxigsoLtTWLqmNlNS0SVOaTJJmmxnPbcfSUcTxhz8nnHvPN+eee1WAovMoW95KU2XBCsuqGWVmR+g999nfNvo09Jq7lgryvaBQKjqfWlBUNbPRvqWonMliIC7GIBKJgBBUQf8d/5PvNwIuBPYoglk3scftBwt3OnOXQbl+PbQlH8HjsXvABwHUfho8F/z1Qw/CuxTBipvy2metZu3L2XLYzFRDy/gNuDlyDhKDGaBHDTD6KtbuPTbqUASz3y9I6ReotUazEWbqrDCSGobAUBDSAQHUEQ0kvHy6b2+IVgSbd9TSwy7X2zKqNAAFICRE4MNoTgBNigbuY7J74ES4RBEsfz3TtNBVuC1BxYGLcCCMixNARmcEXdIAvVd9p0KP49WKYNpcar610nzdvtHm1Jlo0GpoiIZikB7jobPF82boLrclPcJ7yN6/MIQpGPlBoC17qb6qdG1RNc9kRHVIo+p46GmIfEg2I6if7CPCqEaLUq1IhJGXcqLsa9v3uUR1RohHuajh4tlLZI1GhhajBiMlwUAq5pGVQmfwP7EggzVdOikajOakp8ejcx9wkzULQgwqUGXhGQiUkraSbggohpHDmCRAGayhvlnMsbBCV1enuu7YIVI8F52DQIZMQ4LheVUpESZAQQSNYIz9AXPXnUaYBb5+/QKnGo/MwNQiAkNn/dZVAh1C/0AH0OMkL4PV1DaILMtCb3cnnD9TX4CpMnQumZ00l5TURViCBckxpTX5BezeXyeass3Q39cDV5ob9ZgqRc+ByVsnHYxJsCg6LsF/3ab87bj2ukWjyQQD3j64duEkWVuMno32oQckACkWJIA4vV7+NPbUigaGmYDdutKUj6kl6GF0t9SRrPh3TYdZt+6o8bGWHPB0d8C925c3YM6P/iR19E9Nh61Ys27T85KFDnj/9gW0PXtUhbnrMDlgRZqC4VsqtBWX3LE7nM4P716+8/Z/q4TJ4ynWFIyiKE1FRcXBbJZdNejztbS2th7+HxDRT1/RVCfK/1OBAAAAAElFTkSuQmCC';
                                    break;
                            }
                            break;
                        }
                    default:
                        icon = 'http://ire.mstrackweb.com/Images/Vehicles/purple.png';
                        break;
                }
            }

            if (sessEvent == 'Position') {
                sessEvent = "";
            }
            var thisLabel = VehicleArray[i][0];
            if (isHistory === true || isHistory == 'True') {
                useLabels = false;
                thisLabel = '';
            }
            if (Display_Desc === true && isHistory === false) {
                thisLabel = VehicleArray[i][1];
            }
            if (Display_Both === true && isHistory === false) {
                thisLabel = VehicleArray[i][0] + ' (' + VehicleArray[i][1] + ')';
            }
            if ((isHistory === true || isHistory == 'True') && (Display_Date == 'True' || Display_Date === true)) {
                thisLabel = VehicleArray[i][4];
                useLabels = true;
            }
            var sessDetails = '<div><B>' + thisLabel + ' </B><br>Date: ' + VehicleArray[i][4] + '<br>Location: ' + VehicleArray[i][6] + '<br>Speed:   ' + VehicleArray[i][5] + '<br>Odometer:   ' + sessOdo + '<br><B>' + sessEvent + '</B></div>';
            if (useDrivers == "True") {
                sessDetails = '<div><B>' + thisLabel + ' </B><br>Date: ' + VehicleArray[i][4] + '<br>Location: ' + VehicleArray[i][6] + '<br>Speed:   ' + VehicleArray[i][5] + '<br>Odometer:   ' + sessOdo + '<br>Driver:   ' + VehicleArray[i][11] + '<br><B>' + sessEvent + '</B></div>';
            }
            Markers_Detail.push(sessDetails);
            var myIcon = new google.maps.MarkerImage(icon,
                null,
                null,
                new google.maps.Point(12, 12),
                new google.maps.Size(24, 24));

            var loc = new google.maps.LatLng(VehicleArray[i][2], VehicleArray[i][3]);
            if (useLabels !== true) {
                Markers.push(new google.maps.Marker({ position: loc, map: map, title: thisLabel, icon: myIcon, zIndex: tmpZIndex }));
            } else {
                Markers.push(new MarkerWithLabel({
                    position: loc,
                    map: map,
                    icon: myIcon,
                    zIndex: 2,
                    labelContent: thisLabel,
                    labelAnchor: new google.maps.Point(-5, 20),
                    labelClass: "labels", // the CSS class for the label	 
                    labelStyle: { opacity: 1.0 },
                    labelZIndex: 1
                }));
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
                            maxWidth: 300,
                            size: new google.maps.Size(50, 50),
                            position: this.position,
                            pixelOffset: new google.maps.Size(0, -15)
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
        }
        try {

            //var saveBounds = map.getBounds();
            //var centerLat = saveBounds.getCenter().lat();
            //var centerLng = saveBounds.getCenter().lng();
            // window.external.saveMapBounds(centerLat, centerLng, map.getZoom());
        }
        catch (e) {

        }
        if (joinPoints === true || joinPoints == 'True') {
            JoinHistoryPoints();
        }
        if (isHistory === false || isHistory == 'False') {
            try {
                if (markerCluster) {
                    markerCluster.clearMarkers();
                    markerCluster = null;
                }
            } catch (e) {
            }
            if ((VehicleArray.length > 2) && ((typeof markerCluster == "undefined") || (markerCluster === null))) {
                var mcOptions = { gridSize: 50, maxZoom: 15, zoomOnClick: true };
                markerCluster = new MarkerClusterer(map, Markers, mcOptions);
            }
        }
    }
}
function saveFleetMapBounds() {
    var saveBounds = map.getBounds();
    var centerLat = saveBounds.getCenter().lat();
    var centerLng = saveBounds.getCenter().lng();
    zoomLevel = map.getZoom();
    window.external.SaveMapBounds(centerLat, centerLng, zoomLevel);

}
function SetMapCenter(mapStartLat, mapstartlng, mapStartZoom) {
    SetupMap();
    var latLng = new google.maps.LatLng(mapStartLat, mapstartlng);
    map.setCenter(latLng);
    map.setZoom(mapStartZoom);
    LoadContextMenu(isMSTrack);
}

function SetMapOptions(strVehicleArray, buildFlag, mapBoundsFlag, labels, points, is_Sites, geofences, sites, bufferzone, dispRegNo, dispDesc, dispBoth, dispDate, is_History, polyGeofences, polyRoutes, iconsBase64, iconsArray, polyRoutesWayPoints, optimizedRoutes, isMSTrackFlag, startLat, startLng, startZoom) {
    ClearRoutes();
    isMSTrack = isMSTrackFlag;
    if (!map) {
        Load_Google_Map(strVehicleArray);
    }
    if (strVehicleArray.length < 1) {
        SetMapCenter(startLat, startLng, startZoom);
    }
    else {
        BuildArray(strVehicleArray, buildFlag);
    }
    str_GeofenceArray = geofences;
    str_PolygonGeo = polyGeofences;
    str_PolylineRoutes = polyRoutes;
    str_PolyRoutesWayPoints = polyRoutesWayPoints;
    str_SitesArray = sites;
    str_BufferZoneArray = sites;
    str_IconArray = iconsArray;
    str_IconBase64 = iconsBase64;
    isSites = is_Sites;
    isHistory = Boolean(is_History);
    useLabels = labels;
    joinPoints = points;

    Display_RegNo = dispRegNo;
    Display_Desc = dispDesc;
    Display_Both = dispBoth;
    Display_Date = dispDate;
    isOptimizedRoute = optimizedRoutes;
    AddGoogleMarkers();
    if (mapBoundsFlag == 1) {
        SetMapBounds();
    }

}
function Mid(str, start, len) {
    /***	
    IN: str - the string we are LEFTing	
    start - our string's starting position (0 based!!)	
    len - how many characters from start we want to get	
    
    RETVAL: The substring from start to start+len	
    ***/
    // Make sure start and len are within proper bounds	
    if (start < 0 || len < 0) {
        return "";
    }
    var iEnd, iLen = String(str).length;
    if (start + len > iLen) {
        iEnd = iLen;
    }
    else {
        iEnd = start + len;
    }
    return String(str).substring(start, iEnd);
}
function SetMapBounds() {
    if (VehicleArray.length < 1) {
        return;
    }
    var latlngbounds = new google.maps.LatLngBounds();
    for (var i = 0; i < VehicleArray.length; i++) {
        var loc = new google.maps.LatLng(VehicleArray[i][2], VehicleArray[i][3]);
        latlngbounds.extend(loc);

    }
    if (VehicleArray.length > 1) {
        if (fixedMapBounds != 0) {
            try {
                var tmpArray = fixedMapBounds.toString().split(')');
                var southWest = Mid(tmpArray[0], 2, tmpArray[0].length);
                var northEast = Mid(tmpArray[1], 3, tmpArray[1].length);
                var sWest = southWest.split(',');
                var nEast = northEast.split(',');
                sWest = new google.maps.LatLng(parseFloat(sWest[0]), parseFloat(sWest[1]));
                nEast = new google.maps.LatLng(parseFloat(nEast[0]), parseFloat(nEast[1]));
                latlngbounds = new google.maps.LatLngBounds(sWest, nEast);
                map.setCenter(latlngbounds.getCenter());            //, map.fitBounds(latlngbounds));	
                map.setZoom(parseInt(fixedMapZoom));
            } catch (e) {
                alert("Error 1269: " + e.Message);
            }
        }
        else {
            map.setCenter(latlngbounds.getCenter(), map.fitBounds(latlngbounds));
        }
    }
    else {
        map.setCenter(latlngbounds.getCenter(), zoomLevel);
    }
}
function addDistanceListeners() {
    try {
        endDistanceListeners();
        polyMarkers = [];
        DistLabels = [];
        distpolylineArray = [];
        distpolylineDistance = 0;
        map.setOptions({ draggableCursor: 'crosshair' });
        var polyLineOptions = {
            strokeColor: '#000000',
            strokeOpacity: 1.0,
            strokeWeight: 1
        }
        distpolyline = new google.maps.Polyline(polyLineOptions);
        distpolyline.setMap(map);
        distpolylineArray.push(distpolyline);
        // Add a listener for the click event
        if (!polylineListener) {
            polylineListener = google.maps.event.addListener(map, 'click', addLatLng);
        }
    } catch (e) {
        alert("Error 1319: " + e.Message);
    }
}
function addLatLng(event) {
    var path = distpolyline.getPath();
    // Because path is an MVCArray, we can simply append a new coordinate
    // and it will automatically appear
    path.push(event.latLng);
    // Add a new marker at the new plotted point on the polyline.
    if (path.length > 1) {
        distpolylineDistance = 0;
        for (var i = 1; i < path.length; i++) {
            var pt1 = new google.maps.LatLng(path.getAt(i - 1).lat(), path.getAt(i - 1).lng());
            var pt2 = new google.maps.LatLng(path.getAt(i).lat(), path.getAt(i).lng());
            distpolylineDistance += google.maps.geometry.spherical.computeDistanceBetween(pt1, pt2);
        }
    }
    var tmpDistance;
    if (distUnits == 'Miles') {
        tmpDistance = parseFloat(distpolylineDistance / 1000 * distUnitMultiplier).toFixed(2) + ' ' + 'mls';
    } else {
        tmpDistance = parseFloat(distpolylineDistance / 1000 * distUnitMultiplier).toFixed(3) + ' ' + 'kms';
    }
    var DistLabel = new Label({ map: map }, '#F2F2F2', 12, 12, '100');
    DistLabel.set('position', event.latLng);
    DistLabel.set('text', tmpDistance);
    DistLabels.push(DistLabel);
}
function endDistanceListeners() {
    map.setOptions({ draggableCursor: 'default' });
    if (polylineListener) { google.maps.event.removeListener(polylineListener); }
    polylineListener = null;
    distpolyline = null;
}
function clearDistanceListeners() {
    endDistanceListeners();
    if (DistLabels) {
        for (i in DistLabels) {
            DistLabels[i].setMap(null);
        }
        DistLabels = [];
    }
    if (polyMarkers) {
        for (i in polyMarkers) {
            polyMarkers[i].setMap(null);
        }
        polyMarkers = [];
    }
    if (distpolylineArray) {
        for (i in distpolylineArray) {
            distpolylineArray[i].setMap(null);
        }
        distpolylineArray = [];
        distpolylineDistance = 0;
    }
}
function toRad(deg) {
    return deg * Math.PI / 180;
}
function plotRoute() {
    if (routeOverlay) {
        routeOverlay.setMap(null);
    }
    var polyOptions = {
        strokeColor: '#0000FF',
        strokeOpacity: 1.0,
        strokeWeight: 3
    }
    routeOverlay = new google.maps.Polyline(polyOptions, routePoints);
}
function createMarker(point, title) {
    var marker = new google.maps.Marker(point, { title: title });
    return marker;
}


function showSelectedRoutes(selectedRoutes) {
    try {
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
        showRoutes = true;
        waypointMarkersArray = [];
        waypointMarkersDetails = [];
        var theseRoutes = selectedRoutes.split("|");
        if (theseRoutes.length > 0) {
            waypointMarkersArray = [];
            BuildRoutesArray(str_PolylineRoutes, str_PolyRoutesWayPoints);
            try {
                bounds = new google.maps.LatLngBounds();
                RouteMapArray = [];
                for (var r = 0; r < theseRoutes.length; r++) {
                    for (var i = 0; i < RoutesArray.length; i++) {
                        var thisRoute = theseRoutes[r].split('~');
                        if (thisRoute[1] == RoutesArray[i][3]) {
                            // Set up the Waypoint Markers
                            var displayMarkerArray = [];
                            for (var k = 0; k < WayPointsArray.length; k++) {
                                if (WayPointsArray[k][1] == RoutesArray[i][6]) {
                                    displayMarkerArray.push(WayPointsArray[k])
                                }
                            }
                            for (var k = 0; k < displayMarkerArray.length; k++) {
                                var icon;
                                if (k === 0) {
                                    icon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=S|ff1b1b|000000'
                                }
                                else if (k == displayMarkerArray.length - 1) {
                                    icon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=F|1bff1b|000000'
                                } else {
                                    icon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=WP|ffd151|000000'
                                }
                                //Put the marker in the route
                                var wayPointPos = new google.maps.LatLng(parseFloat(displayMarkerArray[k][3]), parseFloat(displayMarkerArray[k][4]));
                                var thisRoute = displayMarkerArray[k][0];
                                var thisLabel = displayMarkerArray[k][2];
                                var tmpDistance;
                                if (distUnits == 'Miles') {
                                    tmpDistance = displayMarkerArray[k][6] + ' ' + 'mls';
                                } else {
                                    tmpDistance = displayMarkerArray[k][6] + ' ' + 'kms';
                                }
                                var waypointDetails = '<div><B>Route Name: ' + thisRoute + '<br><br>Waypoint: ' + thisLabel + ' </B><br><br>Distance Travelled: ' + tmpDistance + '<br>Elapsed Time : ' + secondsToTimeString(displayMarkerArray[k][7]) + '</div>';
                                waypointMarkersDetails.push(waypointDetails)
                                waypointMarkersArray.push(new MarkerWithLabel({
                                    position: wayPointPos,
                                    map: map,
                                    icon: icon,
                                    zIndex: 2,
                                    labelContent: thisLabel,
                                    labelAnchor: new google.maps.Point(-5, 20),
                                    labelClass: "labels", // the CSS class for the label	 
                                    labelStyle: { opacity: 1.0 },
                                    labelZIndex: 1
                                }));
                                google.maps.event.addListener(waypointMarkersArray[waypointMarkersArray.length - 1], "mouseover", function () {
                                    for (i = 0; i < waypointMarkersArray.length; i++) {
                                        if (waypointMarkersArray[i].position == this.position) {
                                            try {
                                                infowindow.close();
                                            } catch (Error) {
                                            }
                                            infowindow = new google.maps.InfoWindow({
                                                content: waypointMarkersDetails[i],
                                                maxWidth: 300,
                                                size: new google.maps.Size(50, 50),
                                                position: this.position,
                                                pixelOffset: new google.maps.Size(0, -15)
                                            });
                                            infowindow.open(map);
                                        }
                                    }
                                });
                                google.maps.event.addListener(waypointMarkersArray[waypointMarkersArray.length - 1], "mouseout", function () {
                                    for (i = 0; i < waypointMarkersArray.length; i++) {
                                        if (waypointMarkersArray[i].position == this.position) {
                                            try {
                                                infowindow.close();
                                            } catch (Error) {
                                            }
                                        }
                                    }
                                });
                            }

                            // Define the LatLng coordinates for the polygon's path.
                            var polyCoords = [];
                            var polyCoords1 = [];
                            var polyCoords2 = [];
                            polyCoords = RoutesArray[i][7].split('#');
                            for (var j = 0; j < polyCoords.length - 1; j++) {
                                polyCoords1 = polyCoords[j].split(',');
                                polyCoords2[j] = new google.maps.LatLng(parseFloat(polyCoords1[0]), parseFloat(polyCoords1[1]))
                            }
                            // Set the polygon colours based upon the Company allocation
                            var thisPoly;
                            var polystrokeColor;
                            var isAllocated = false;

                            if (RoutesArray[i][1].length > 1) {
                                isAllocated = true;
                                polystrokeColor = '#8A0808';
                                // Construct the polyline.	
                                thisPoly = new google.maps.Polyline({
                                    path: polyCoords2,
                                    //geodesic: true,
                                    strokeColor: polystrokeColor,
                                    strokeOpacity: 0.8,
                                    strokeWeight: 5,
                                    indexID: i,
                                    ID: RoutesArray[i][6],
                                    PName: RoutesArray[i][3],
                                    PAllocated: isAllocated,
                                    PType: 4
                                });
                            } else {
                                isAllocated = false;
                                polystrokeColor = '#045FB4';

                                // Construct the polyline.	
                                var lineSymbol = {
                                    path: 'M 0,-1 0,1',
                                    strokeOpacity: 1,
                                    scale: 4
                                };
                                thisPoly = new google.maps.Polyline({
                                    path: polyCoords2,
                                    //geodesic: true,
                                    strokeColor: polystrokeColor,
                                    strokeOpacity: 0.1,
                                    icons: [{
                                        icon: lineSymbol,
                                        offset: '0',
                                        repeat: '20px'
                                    }],
                                    indexID: i,
                                    ID: RoutesArray[i][6],
                                    PName: RoutesArray[i][3],
                                    PAllocated: isAllocated,
                                    PType: 4
                                });
                            }
                            if (thisPoly) {
                                thisPoly.setMap(map);
                                RouteMapArray.push(thisPoly);
                            }
                            //Now put the marker in the ploygon	
                            // calculate the bounds of the polygon	
                            for (var j = 0; j < thisPoly.getPath().getLength(); j++) {
                                bounds.extend(thisPoly.getPath().getAt(j));
                            }
                        }
                    }
                }
                map.fitBounds(bounds);
            }
            catch (error) {
                window.alert("Error 1540: " + error.message);
            }
        }
        else {
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
            waypointMarkersDetails = [];
        }
    }
    catch (error) {
        window.alert(" Show Selected Routes Error 1288: " + error.message);
    }

}

function CreateRoutesLayer() {
    try {
        //Clear the routes
        ClearRoutes();

        if (showRoutes == "True") {
            waypointMarkersArray = [];
            waypointMarkersDetails = [];
            if (str_PolylineRoutes === "") {
                showRoutes = "False";
                window.external.NoRoutesCreated('NoRoutesCreated');
                return;
            }
            BuildRoutesArray(str_PolylineRoutes, str_PolyRoutesWayPoints);
            var hasAllocation = false;
            for (var i = 0; i < RoutesArray.length; i++) {
                if (RoutesArray[i][1].length > 1) {
                    hasAllocation = true;
                }
            }
            if (hasAllocation === false) {
                window.external.NoRoutesCreated('NoRoutesAllocated');
                showRoutes = "False";
                return;
            }
            BuildIconArray(str_IconArray);
            BuildIconBase64Array(str_IconBase64);
            try {
                bounds = new google.maps.LatLngBounds();
                RouteMapArray = [];
                for (var i = 0; i < RoutesArray.length; i++) {
                    // Set the polygon colours based upon the Company allocation
                    var polystrokeColor;
                    var isAllocated = false;
                    if (RoutesArray[i][1].length > 1) {
                        isAllocated = true;
                        polystrokeColor = '#8A0808';
                    } else {
                        isAllocated = false;
                        polystrokeColor = '#045FB4';
                    }
                    // if route is allocated then display the route
                    if (isAllocated === true) {
                        // Display the markers for the way points
                        var displayMarkerArray = [];
                        for (var k = 0; k < WayPointsArray.length; k++) {
                            if (WayPointsArray[k][1] == RoutesArray[i][6]) {
                                displayMarkerArray.push(WayPointsArray[k])
                            }
                        }
                        for (var k = 0; k < displayMarkerArray.length; k++) {
                            var icon;
                            if (k === 0) {
                                icon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=S|ff1b1b|000000'
                            }
                            else if (k == displayMarkerArray.length - 1) {
                                icon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=F|1bff1b|000000'
                            } else {
                                icon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=WP|ffd151|000000'
                            }
                            //Put the marker in the route
                            var wayPointPos = new google.maps.LatLng(parseFloat(displayMarkerArray[k][3]), parseFloat(displayMarkerArray[k][4]));
                            var thisRoute = displayMarkerArray[k][0];
                            var thisLabel = displayMarkerArray[k][2];

                            var tmpDistance;
                            if (distUnits == 'Miles') {
                                tmpDistance = displayMarkerArray[k][6] + ' ' + 'mls';
                            } else {
                                tmpDistance = displayMarkerArray[k][6] + ' ' + 'kms';
                            }
                            var waypointDetails = '<div><B>Route Name: ' + thisRoute + '<br><br>Waypoint: ' + thisLabel + ' </B><br><br>Distance Travelled: ' + tmpDistance + '<br>Elapsed Time : ' + secondsToTimeString(displayMarkerArray[k][7]) + '</div>';
                            waypointMarkersDetails.push(waypointDetails)
                            waypointMarkersArray.push(new MarkerWithLabel({
                                position: wayPointPos,
                                map: map,
                                icon: icon,
                                zIndex: 2,
                                labelContent: thisLabel,
                                labelAnchor: new google.maps.Point(-5, 20),
                                labelClass: "labels", // the CSS class for the label	 
                                labelStyle: { opacity: 1.0 },
                                labelZIndex: 1
                            }));

                            google.maps.event.addListener(waypointMarkersArray[waypointMarkersArray.length - 1], "mouseover", function () {
                                for (i = 0; i < waypointMarkersArray.length; i++) {
                                    if (waypointMarkersArray[i].position == this.position) {
                                        try {
                                            infowindow.close();
                                        }
                                        catch (Error) {
                                        }

                                        infowindow = new google.maps.InfoWindow({
                                            content: waypointMarkersDetails[i],
                                            maxWidth: 300,
                                            size: new google.maps.Size(50, 50),
                                            position: this.position,
                                            pixelOffset: new google.maps.Size(0, -15)
                                        });
                                        infowindow.open(map);
                                    }
                                }
                            });
                            google.maps.event.addListener(waypointMarkersArray[waypointMarkersArray.length - 1], "mouseout", function () {
                                for (i = 0; i < waypointMarkersArray.length; i++) {
                                    if (waypointMarkersArray[i].position == this.position) {
                                        try {
                                            infowindow.close();
                                        }
                                        catch (Error) {
                                        }
                                    }
                                }
                            });
                        }

                        // Define the LatLng coordinates for the polygon's path.
                        var polyCoords = [];
                        var polyCoords1 = [];
                        var polyCoords2 = [];
                        polyCoords = RoutesArray[i][7].split('#');
                        for (j = 0; j <= polyCoords.length - 1; j++) {
                            polyCoords1 = polyCoords[j].split(',');
                            polyCoords2[j] = new google.maps.LatLng(parseFloat(polyCoords1[0]), parseFloat(polyCoords1[1]));
                        }
                        var thisPoly;
                        // Construct the polyline.	
                        thisPoly = new google.maps.Polyline({
                            path: polyCoords2,
                            geodesic: false,
                            strokeColor: polystrokeColor,
                            strokeOpacity: 0.8,
                            strokeWeight: 5,
                            indexID: i,
                            ID: RoutesArray[i][6],
                            PName: RoutesArray[i][3],
                            PAllocated: isAllocated,
                            PType: 4
                        });
                        thisPoly.setMap(map);
                        RouteMapArray.push(thisPoly);
                        // calculate the bounds of the polygon	
                        for (var j = 0; j < thisPoly.getPath().getLength(); j++) {
                            bounds.extend(thisPoly.getPath().getAt(j));
                        }
                    }
                }
                map.fitBounds(bounds);
            }
            catch (Error) {
                window.alert("Error 1540: " + Error.message);
            }
        }
        else {
            //Clear the routes
            ClearRoutes();
            //if (RouteMapArray) {
            //    for (i in RouteMapArray) {
            //        RouteMapArray[i].setMap(null);
            //    }
            //}
            //RouteMapArray = [];
            //if (waypointMarkersArray) {
            //    for (i in waypointMarkersArray) {
            //        waypointMarkersArray[i].setMap(null);
            //    }
            //}
            //waypointMarkersArray = [];
            //waypointMarkersDetails = [];
        }
    }

    catch (Error) {
        window.alert("Error 1430: " + Error.message);
    }
}

function secondsToTimeString(totalSec) {
    var hours = parseInt(totalSec / 3600) % 24;
    var minutes = parseInt(totalSec / 60) % 60;
    var seconds = totalSec % 60;

    var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);
    return result
}
function CreateGeofenceLayer() {
    try {
        if (showGeofences == "True") {
            geofenceMarkersArray = [];
            geofenceTitleArray = [];
            if (str_GeofenceArray.length < 1) {

                return;
            }
            bounds = new google.maps.LatLngBounds();
            BuildGeoFenceArray(str_GeofenceArray);
            if (GeofenceArray.length < 1) {
                //document.getElementById("alertNoGeofences").style.display = "block";
                return;
            }
            for (var i = 0; i < GeofenceArray.length; i++) {
                if (GeofenceArray[i].GeoFenceSource === 1 || GeofenceArray[i].GeoFenceSource === 6) {
                    var geoID = GeofenceArray[i].GeofenceGeoCode;
                    var geoName = GeofenceArray[i].GeofenceGeoName;
                    var geoLat;
                    var geoLong;
                    var geoStrokeColor = '#FF0000';
                    var geoFillColor = '#FF0000';

                    if (GeofenceArray[i].GeoFenceSource === 1) {
                        geoLat = parseFloat(GeofenceArray[i].GeofenceLatitude);
                        geoLong = parseFloat(GeofenceArray[i].GeofenceLongitude);
                    }
                    if (GeofenceArray[i].GeoFenceSource === 6) {
                        var pointCoords = GeofenceArray[i].Polygon.split(',');
                        if (pointCoords.length == 2) {
                            geoLat = parseFloat(pointCoords[0]);
                            geoLong = parseFloat(pointCoords[1]);
                        }
                    }
                    var geoRadius = GeofenceArray[i].GeofenceRadius;
                    if (geoLat !== "0" && geoLong !== "0") {
                        var GeoCircle;
                        var GeofenceOptions = {
                            strokeColor: geoStrokeColor,
                            strokeOpacity: 0.8,
                            strokeWeight: 2,
                            fillColor: geoFillColor,
                            fillOpacity: 0.35,
                            map: map,
                            center: new google.maps.LatLng(geoLat, geoLong),
                            radius: parseInt(geoRadius)
                        };
                        GeoCircle = new google.maps.Circle(GeofenceOptions);
                        GeofenceCircle.push(GeoCircle);
                        var geoDetails = '<b>Geofence ID:</b> ' + geoID + '</br><b>Geofence:</b> ' + geoName;
                        var image = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAlCAYAAAAjt+tHAAAGRklEQVRYhbWXW08bVxeGH9vDeBhsfMDmaA6hiSBp0AcGpYlyUakkaW8q9apS+wdof1GT/IHc9Q+QXDRqSpp+fCVFgrpQYcAcHMA2Nh7MMHumF54ZbAIGqnxLWprtfVjvO+9ee3uWx7IsAB4+fCgBXkC2nxIf1gzABHTAnJ6eNgA8lmU54DKg2C7bBLwfCNy0CehAxXZ9enra8Dx48MABV4FW+6nw/1GgAmhA0X7qzlsqNni7aU7NfGDgOvN6n9yzmyZg1EqvmubUzOTkMPF4kEhE/aDA+bzGzk6JFy+mZrzeJyPY21CbeApwafAffvip7vf333/acP6pmE6eeZ1sd/xC8ONjQSq1/V7/mzcraJp+WRIuppNobrbbp7LOHj/+ia+/nuDFi0UUpQlZlujvb2N1dQ+A3t4IpdIRz58vomk64+P9PH++yHffNVTFWwfsmGladf74cVXqH3/8H7FYgI2NAisru/j9EpOTN3n06BaxWJCVlV02Ngq0tPiZnV11iZ+OdyaLWrOsE3/69KXbbxgmqVSWjo5WhoY68Xg85PMa/f0xJiYG+OqrMUZHe8lk8nXxnj59WRfzQgJCWK5/++1dYrEALS1+AD7//DahkIosS9y/f4ORkQRCWOztldnYKKBpx3zzzSfk8xqqKtPTE+bLL0frYl6CwIkvLm4RCqmUy0c8ejRCZ2cEy4Jk8hpCwC+//M3aWo5gUOXGjS7u3buBLMsMD3ehaTql0hFzc+t1MS+tQCaTY2trn/X1HB0dIdrbQwhhMTzc7c6pbdf60FA3k5MfUywe4vHA7Gz6KgpUJ66t5ZBlH7pu8Nlnt5mfX3fHdF0ghEUg0ExnZ4T19RxCWMzMLLGwsIGqKrS3h5EkL7ouyGRyVyPw7Nkrlpa2ODoykCSf/VY9LC5uEAg0UyweugFfv14im90nny8zPv4R1693IYTFr78uE4+3YlkWhUKZZ89eXY6AYZy0s9l9VNWPYVT7E4k4mUweVVXdvrGx6/T2xhGieiqc/tHRj+joiJLN7p8Zu6ECtXZ8LNjcrEo4P79KPB523/6PP9IUChqq2oyqKmxtFRDColDQEMLC4/E0jH0hgba2VgzDwDRheXmTkZFBhLBYXt60k7APVVVIpTIsLKxRKh3ax7KIEBb5/AGxWKghgff+8w3D4osvPmFtLUsuV8QwTKLRENFoCMOoBtjf1ygUNAKBZgAODircvj3ort/bO6CrK87mZg6/X6KnJ87IyKC7/kICVfNgmhaWZZFOb5NIdLCwsALArVvX3Lm7uwVMs9oulw9paWlmeHgAw7DQ9WPa2kKYpnUm+LlbIIRFW1uEd+/yNDVJ/PnnKrouSCQ6GBxMkEqtkUqtIYRFJBJiaGgAISxk2c/ycgYhLH7+eQ5J8rG1tUs8Hrn6PeDxeEkkqne+z+djZuYtiqLw7l2ea9cSKIqfw0MdISxKJc2+vLL09XWzuLhCpaITiYSQJIlwOPTvruL+/gQej4dYLIJhCF6+/C+qqiIExGJt+HxNCAF+fzNCwMHBIW/ezLO9vUtnZ5xSqczY2McNr+L3cqCW5evXswBks7tEoxGKxRJzcwv4fD6i0TDhcCsej5dyuczOzh66fgxAe3uM7e0dAH777S13746/j3yeAqZ54nfunCz0+/0oip+mpiaCwQBHR8csLa3w119/UyppBINBJEmitTVIuay56+7cGa+LeR4Bd+j0B8TERBKAnp5u+vr6aGlpIRwOUywW3SDFYhFZlpFlmXg8zs2bwwBMTCQbfZCYUN0Co8bRtAqKotSxTCaTmCYoSjODg9Xznk6v1s3p6uqmq6vbVdFZU2uVSsVpupgSJ+VSpUqgKt9pEqctmUw2HD9tlUrFjW1j6YAp1YBrXu+Te+n02YXJwMAA0Wj0UmC5XI50On3mmF2YaNTUBab9o2hPGKG+NPMCpNNTr4ALSTjgXu+T+3aXsxGnS7MKYF6mOHW/4U1zaraREjXg49Tn1vnF6QXlufOsLd9+P4tEDfhYrcSnCJxdnjcym5gCBGxXTXPqbS2JGvD/2OAHtlccoPPsQgLnkAg4SgC1b37AFcAvTeAMEq02iVnA2fMDqsl1afArEThFQrVdtod0qtJrVwG/MoEaEk5SOkfVOWL6VcAB/gHkx+PSeZthnwAAAABJRU5ErkJggg==';
                        var myLatLng = new google.maps.LatLng(geoLat, geoLong);
                        var geoMarker = new google.maps.Marker({
                            position: myLatLng,
                            map: map,
                            icon: image,
                            zIndex: 10
                        });
                        geofenceMarkersArray.push(geoMarker);
                        geofenceTitleArray.push(geoDetails);
                        google.maps.event.addListener(geofenceMarkersArray[geofenceMarkersArray.length - 1], "mouseover", function () {
                            for (i = 0; i < geofenceMarkersArray.length; i++) {
                                if (geofenceMarkersArray[i].position == this.position) {
                                    try {
                                        infowindow.close();
                                    }
                                    catch (Error) {
                                    }
                                    var contentString = '<div id="infoContent" class="map.info.window" ><p>' + geofenceTitleArray[i] + '</p></div>'

                                    infowindow = new google.maps.InfoWindow({
                                        content: contentString,
                                        position: this.position,
                                        pixelOffset: new google.maps.Size(0, -15)
                                    });
                                    infowindow.open(map);
                                }
                            }
                        });
                        google.maps.event.addListener(geofenceMarkersArray[geofenceMarkersArray.length - 1], "mouseout", function () {
                            for (i = 0; i < geofenceMarkersArray.length; i++) {
                                if (geofenceMarkersArray[i].position == this.position) {
                                    try {
                                        infowindow.close();
                                    }
                                    catch (Error) {
                                    }
                                }
                            }
                        });
                        bounds.extend(myLatLng);
                    }
                }
            }
            try {
                //BuildPolygonGeoArray(str_PolygonGeo);

                PolygonMapArray = [];
                var polyIndex = 0;
                for (var j = 0; j < GeofenceArray.length; j++) {
                    if (GeofenceArray[j].Polygon !== null) {

                        // Define the LatLng coordinates for the polygon's path.	
                        var polyCoords = [];
                        var polyCoords1 = [];
                        var polyCoords2 = [];
                        polyCoords = GeofenceArray[j].Polygon.split('#');
                        for (k in polyCoords) {
                            polyCoords1 = polyCoords[k].split(',');
                            polyCoords2[k] = new google.maps.LatLng(parseFloat(polyCoords1[0]), parseFloat(polyCoords1[1]));
                        }
                        // Set the polygon colours based upon the Company allocation
                        var polystrokeColor;
                        var polyfillColor;
                        var isAllocated = false;
                        if (GeofenceArray[j].GeofenceCompany.length > 1) {
                            isAllocated = true;
                            polystrokeColor = '#8A0808';
                            polyfillColor = '#FF0000';
                        }
                        else {
                            isAllocated = false;
                            polystrokeColor = '#045FB4';
                            polyfillColor = '#0080FF';
                        }
                        var thisPoly;
                        if (GeofenceArray[j].GeoFenceSource == 3) {
                            geoID = GeofenceArray[j].GeoId;
                            // Construct the polygon.	
                            thisPoly = new google.maps.Polygon({
                                paths: polyCoords2,
                                strokeColor: polystrokeColor,
                                strokeOpacity: 0.8,
                                strokeWeight: 2,
                                fillColor: polyfillColor,
                                fillOpacity: 0.35,
                                indexID: polyIndex,
                                ID: GeofenceArray[j].GeoId,
                                PName: GeofenceArray[j].GeofenceGeoName,
                                IsAllocated: isAllocated,
                                IsNew: false
                            });
                        }
                        if (GeofenceArray[j].GeoFenceSource == 6) {
                            geoID = GeofenceArray[j].GeoId;
                            var GeofenceOptions = {
                                strokeColor: polystrokeColor,
                                strokeOpacity: 0.8,
                                strokeWeight: 2,
                                fillColor: polyfillColor,
                                fillOpacity: 0.35,
                                map: map,
                                center: new google.maps.LatLng(geoLat, geoLong),
                                radius: 100
                            };
                            thisPoly = new google.maps.Circle(GeofenceOptions);
                        }

                        var polyGeoDetails = '<b>Geofence ID:</b> ' + GeofenceArray[j].GeoId + '</br><b>Geofence:</b> ' + GeofenceArray[j].GeofenceGeoName + ' (Unallocated)';
                        google.maps.event.addListener(thisPoly, 'click', function (eClick) {
                            SetupDrawingManager();
                            SetupPolygonContextMenu();
                            setSelection(PolygonMapArray[this.indexID]);
                            google.maps.event.addListener(PolygonMapArray[this.indexID], 'rightclick', function (e) {
                                if (selectedShape) {
                                    contextPolyMenu.show(e.latLng);
                                }
                            });
                        });


                        thisPoly.setMap(map);
                        PolygonMapArray.push(thisPoly);
                        polyIndex++;

                        //Now put the marker in the ploygon	
                        // calculate the bounds of the polygon	
                        var polyGeoMarker;
                        if (GeofenceArray[j].GeoFenceSource == 3) {
                            var polybounds = new google.maps.LatLngBounds();
                            for (var l = 0; l < thisPoly.getPath().getLength(); l++) {
                                bounds.extend(thisPoly.getPath().getAt(l));
                                polybounds.extend(thisPoly.getPath().getAt(l));
                            }

                            var point = polybounds.getCenter();
                            if (google.maps.geometry.poly.containsLocation(point, thisPoly)) {
                                image = 'http://maps.google.com/mapfiles/ms/icons/blue-dot.png';
                                if (isAllocated === true) {
                                    polyGeoDetails = '<b>Geofence ID:</b> ' + GeofenceArray[j].GeoId + '</br><b>Geofence:</b> ' + GeofenceArray[j].GeofenceGeoName;
                                    image = 'http://maps.google.com/mapfiles/ms/icons/red-dot.png';
                                }
                                polyGeoMarker = new google.maps.Marker({
                                    position: point,
                                    map: map,
                                    title: polyGeoDetails,
                                    icon: image,
                                    zIndex: 10
                                });
                            }
                        }
                        if (GeofenceArray[j].GeoFenceSource == 6) {
                            polyGeoDetails = '<b>Geofence ID:</b> ' + geoID + '</br><b>Geofence:</b> ' + geoName;
                            var image = 'data:image/png;base64,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';
                            var myLatLng = new google.maps.LatLng(geoLat, geoLong);
                            polyGeoMarker = new google.maps.Marker({
                                position: myLatLng,
                                map: map,
                                icon: image,
                                zIndex: 10
                            });

                        }
                        geofenceMarkersArray.push(polyGeoMarker);
                        geofenceTitleArray.push(polyGeoDetails);
                        google.maps.event.addListener(geofenceMarkersArray[geofenceMarkersArray.length - 1], "mouseover", function () {
                            for (i = 0; i < geofenceMarkersArray.length; i++) {
                                if (geofenceMarkersArray[i].position == this.position) {
                                    try {
                                        infowindow.close();
                                    }
                                    catch (Error) {
                                    }
                                    var contentString = '<div id="infoContent" class="map.info.window" ><p>' + geofenceTitleArray[i] + '</p></div>'

                                    infowindow = new google.maps.InfoWindow({
                                        content: contentString,
                                        position: this.position,
                                        pixelOffset: new google.maps.Size(0, -15)
                                    });
                                    infowindow.open(map);
                                }
                            }
                        });
                        google.maps.event.addListener(geofenceMarkersArray[geofenceMarkersArray.length - 1], "mouseout", function () {
                            for (i = 0; i < geofenceMarkersArray.length; i++) {
                                if (geofenceMarkersArray[i].position == this.position) {
                                    try {
                                        infowindow.close();
                                    }
                                    catch (Error) {
                                    }
                                }
                            }
                        });
                    }
                }
                map.fitBounds(bounds);
            }
            catch (Error) {
                window.alert("Error 1540: " + Error.message);
            }

        }
        else {
            clear_Geofences();
        }
    }
    catch (Error) {
        window.alert("Error 1569: " + Error.message);
    }
}
function BuildPolygonGeoArray(tmpArrayStr) {
    try {
        PolygonGeoArray = [];
        PolygonGeoArray = $.parseJSON(tmpArrayStr);
    }
    catch (Error) {
        window.alert("Build PolygonGeoArray Array Error: " + Error.message);
    }
}

function BuildRoutesArray(tmpArrayStr, tmpPolyRoutesWayPoints) {
    try {
        var tmpArray = [];
        var tmpWayPointsArray = [];
        tmpArray = tmpArrayStr.split('~');
        tmpWayPointsArray = tmpPolyRoutesWayPoints.split('~');
        RoutesArray = [];
        WayPointsArray = [];
        for (var i = 0; i < tmpArray.length - 1; i++) {
            RoutesArray[i] = tmpArray[i].split('|');
        }
        for (var i = 0; i < tmpWayPointsArray.length - 1; i++) {
            WayPointsArray[i] = tmpWayPointsArray[i].split('|');
        }
    }
    catch (Error) {
        window.alert('Build Routes Array Error: ' + Error.message);
    }
}
function BuildGeoFenceArray(tmpArrayStr) {
    try {
        GeofenceArray = [];
        GeofenceArray = $.parseJSON(tmpArrayStr);
    }
    catch (Error) {
        window.alert("Build GeoFence Array Error: " + Error.message);
    }
}
function BuildBufferZoneArray(tmpArrayStr) {
    try {
        var tmpArray = [];
        tmpArray = tmpArrayStr.split('~');
        BufferZoneArray = [];
        for (var i = 0; i < tmpArray.length - 1; i++) {
            var tmpArray_1 = tmpArray[i].split(',');
            BufferZoneArray[i] = new google.maps.LatLng(tmpArray_1[0], tmpArray_1[1]);
        }
        var latlngbounds = new google.maps.LatLngBounds();
        for (i = 0; i < BufferZoneArray.length; i++) {
            var loc = BufferZoneArray[i];
            latlngbounds.extend(loc);
        }
        if (BufferZoneArray.length > 1) {
            map.setCenter(latlngbounds.getCenter(), map.fitBounds(latlngbounds));
        }
        else {
            map.setCenter(latlngbounds.getCenter(), zoomLevel);
        }
        pBufferZone = new google.maps.Polyline({
            path: BufferZoneArray,
            editable: false,
            strokeColor: '#FF0000',
            strokeOpacity: 0.25,
            strokeWeight: 20
        });
        pBufferZone.setMap(map);
        google.maps.event.addListener(pBufferZone, "dblclick", function () {
            if (BufferZoneIsEditable == "True") {
                BufferZoneIsEditable = "False";
                pBufferZone.setOptions({
                    editable: false
                });
            } else {
                BufferZoneIsEditable = "True";
                pBufferZone.setOptions({
                    editable: true
                });
            }
        });
    }
    catch (Error) {
        window.alert("Build BufferZone Array Error: " + Error.message);
    }
}
function CreateSitesLayer() {
    try {
        if (showSites == "True") {
            BuildSiteArray(str_SitesArray);
            BuildIconArray(str_IconArray);
            BuildIconBase64Array(str_IconBase64);
            for (var i = 0; i < SiteArray.length; i++) {
                var thisSiteName = SiteArray[i].SiteName;
                var thisSiteCatDesc = SiteArray[i].SiteCatDesc;
                var thisSiteIconPath = SiteArray[i].SiteIconPath;
                var thisSiteComments = SiteArray[i].SiteComments;
                var thisSiteLat = SiteArray[i].SiteLatitude;
                var thisSiteLong = SiteArray[i].SiteLongitude;
                var thisSiteContact = SiteArray[i].SiteContact;
                var thisSiteContactNo = SiteArray[i].SiteContactNumber;
                var thisLabel;
                var loc;
                var sessDetails;
                var icon;
                try {
                    icon = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAGzElEQVR4nO2WW4hd1RnHf3utvffZZ5/7OXO/JJlMnKTaJGpGAnGkjXhJmwoxijZopaWlT0ob34r2odBK6Yu0lBZKKYi2YMGGxsSgxUurUSdXM9FkcpmZMzMnkzO3M3Pu+777IBQkSTtIYl/8P64P1v/H+rO+74Mv9aX+z1KuVfjjY2t/cvr4+PY7YvioYt/jw8EfbgSAeq3C/Xdvf26NGaf8ySTJ3s6bGR794gAOPTWYjKa76e13ac5Ukels5kaYA4irHUpFyWm6iioVhJBEjWjitR8OXjOu6w4QUdWsEBL4FEDTTRQhszcC4KoRqAo5pMAPAFXFCwRmWywztv+7S5oe2SSN5BahRtYJLTIjhMgLISeEpk9mNj1dvS4AmiqzUlMBn3pxhtVDXyUzeMtBaST7ZLRdE3oKReooCiiEgAehS+XiX8pCKHlFyAkpRV6oal6oah78Cac8lTfXPVVZEYBvW7nQtbAv56GxRDXVSTW1Y2CVlqdYtskZl4lHQlAEQShB0VBEBKEaKUWLbRaasVkREqSKHzg0Zz+iUTzO6Mt3/2LDo289+78BaqVs2FxGKCFhAIYakjRshB/imRup+rPM2UnqvkbKGWVGWU2/9R7FoJUWisSFhR6JYi2NsTjxLhecDTSNVSQbdbmyCLxGzrctnKYFIcxVfeatXvqDGRqNSySNZYzaRSKeSyIZww5rEIZcML/GUn0UR81h+WkGCk/yYfRJWoKP6Km8ga8lrvC6KkCgKFklsFF8D0VAm+GR5jhRzSEZSwEhSnaAqtpOo3SUce0rbAxGyFgTdIlJtOopPF9BCpvVk89zwbiXS/E9dCwdXBmAQZBzPYUQQeDDTFWlIO9moHmIRcdB12sojSJGo0o2oaMGx9DVkKbeyazdxE1vpR5dQ9/0m3zc+1Nalv5Jbu7PSE1fGYAvgpymQhgG6BFojzbJWAcxNZucKpBhFNfow8p2MlM6wWm5jdvLR1jQFkjXjtFiTZCx6mDNsnbql5w2dlJMPMKa6qGVAUSFkrMdQEgsG+brkkntW2ywDlCwQjqCGagfISwXiOsO3eER/OoJLkWH8GuCqvlNFtsH2VncwYm2vfSV38AsT6PJ4NoA1tiv77crl3f5nvWN+X37V2siIPB8NBWyhotx4Xl0o4AZrMMJKpT1m7iU3E6i8j7vRh7hQecEMT9Pn/8J6YXX8KdrIAIGZl/gA203vWo3fe7wtQHUWMdvRCQ34Ntl0ru+TXWhROyOdvoGBnHNCsfVPQTVV3i1Psg28wPM6mm6vPdpV6YxnWliosFs2MkZe4A54yGKqc18Z2Inx+KPc2tlH7Zi4n/atT6j/wwYu/Di6KHxm9Ybco41jdeZmVlAVQw6OjrJpUK85jxuc5mFUh0Xg+lwHXm7h7g7zYv+j3jG2cMB/1GG5Ou0uhfwXRffdRit9PC22MM98m+sMcb3Oc3aDx7+Vbl0xQuEgc89PWepNnxm5z2WFpsEVolUPEezdT1leStZM09XtoFvLZEu5dlinsJplNhUeRNVdzDrZabcDt4Md3Be3cre+m5Omg/zqPNbehMlFMIHf/e2/XdN8rLrYwOhBPjx07c+XKnOP6G5jt6WCpgceYuT7x1FNOcwkzla2mK4ajtOucB09ntIr0HFjTITDOAHCTQ9Rjqd4vboSfrFx2xRDjFkv4TjqdwmD5M1Gyw0TN5RHmP06PC58bmgxqeT2JEPbO76k51Yfm5UHddfOvIBp/afZ7WSYLEiKS7Z9Pcm6e5SMZpniAQLmHOvQu0iY80OxpT1xCJJ8pkdpL1Z8pldpFNtJLOriCVyGHpAcTlkstnDudRDbLNf5MRFb2J8zs0DFaAkb+lqPWhPegcaI3K0pxRrbunubrVdaebiKRQZ55CaZ3g0z9l8icq8hSkk6QQc+3icc9MVKnbIyeU0A4svcKC6lWDxLH91v8980Eu/8gmv1e8j4RTY6L2N7ShMnRc3z1TcfzS84B3AveqW8+yuwYHA9+48VSjcGfSEQ5Fedf1CqYGTAzWicdvZOFtXZXD8ZRpWjflKit07NKKai+85LNYk816W9iDPVCmJoYeMjXm88q/qqfHl5hNzljdyxS/4b9q+oSUTwpCUwVCiVb/r3lVrB2MRqemqQCiC+eUqU6uK9MYFbTWP3qRLIupSbgrOFZIc/rAenCu6P/dV7WfDU/Of6Uafa8975oGNmgzYKiNiSJORoeGxsW3izkgmkfaZ8pv4swpmDTY1I4yM+BdrXrjncKF07Gp3XbdF8+v9mQ1KwBBqMKR0Ru4KosFa54z7+yBk74eFsn29fFaseztT8S/c9PPo3w6rC+/8Ty7QAAAAAElFTkSuQmCC';
                    try {
                        if (isNaN(thisSiteIconPath) === false) {
                            icon = IconBase64Array[thisSiteIconPath];
                        }
                        else if (thisSiteIconPath.indexOf("Images/") >= 0) {
                            try {
                                var tmpArray = thisSiteIconPath.split('/');
                                if (tmpArray.length == 3) {
                                    var tmpArray1 = tmpArray[2].split('.');
                                    var iconName = tmpArray1[0];
                                    for (var j = 0; j < IconArray.length; j++) {
                                        if (iconName == IconArray[j]) {
                                            icon = IconBase64Array[j];
                                            break;
                                        }
                                    }
                                }
                            } catch (e) {

                            }
                        }
                        else {
                            try {
                                for (var k = 0; k < IconArray.length; k++) {
                                    if (thisSiteIconPath == IconArray[k]) {
                                        icon = IconBase64Array[k];
                                        break;
                                    }
                                }
                            } catch (e) {
                                icon = 'data:image/png;base64,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';
                            }
                        }
                    }
                    catch (e) {
                        alert("1886: " + e.message)
                        icon = 'data:image/png;base64,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';
                    }
                    thisLabel = thisSiteName;
                    sessDetails = '<div><B>Site:' + thisSiteName + '</B><br>Category:' + thisSiteCatDesc + '<br>Contact:' + thisSiteContact + '<br>ContactNumber:' + thisSiteContactNo + '<br>Comments:' + thisSiteComments + '<br>Latitude:' + thisSiteLat + '<br>Longitude:' + thisSiteLong + '</div>';
                    siteMarkers_Detail.push(sessDetails);
                    var myIcon = new google.maps.MarkerImage(icon,
                        null,
                        null,
                        null,
                        new google.maps.Size(35, 35));
                    loc = new google.maps.LatLng(thisSiteLat, thisSiteLong);
                    siteMarkers.push(new google.maps.Marker({ position: loc, map: map, title: thisLabel, icon: myIcon, zIndex: 100 }));
                    siteMarkers[siteMarkers.length - 1].setMap(map);
                    if (showSiteLabels == "True") {
                        var SiteLabel = new Label({ map: map }, '#99E6FF', 10, -10, 'auto');
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
                                    maxWidth: 300,
                                    size: new google.maps.Size(50, 25),
                                    position: this.position,
                                    pixelOffset: new google.maps.Size(0, -15)
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

                }
                catch (e) {
                    window.alert("Create Site Layer Error:" + e.message);
                }
            }
        } else {
            clear_Sites();
            clear_SitesLabels();
        }
    }
    catch (Error) {
        window.alert("Create Sites Error 957: " + Error.message);
    }
}
function BuildSiteArray(str_SitesArray) {
    try {
        SiteArray = [];
        SiteArray = $.parseJSON(str_SitesArray);
    }
    catch (Error) {
        window.alert("Build Sites Array Error: " + Error.message);
    }
}
function BuildIconArray(str_IconArray) {
    try {
        IconArray = [];
        IconArray = str_IconArray.split('~');
    }
    catch (Error) {
        window.alert("Build Icon Array Error: " + Error.message);
    }
}
function BuildIconBase64Array(str_IconBase64) {
    try {
        IconBase64Array = [];
        IconBase64Array = str_IconBase64.split('~');
    }
    catch (Error) {
        window.alert("Build Icon Array Error: " + Error.message);
    }
}
function CreateSite(clickedPoint) {
    var thisLoc = clickedPoint;
    var thisLatLong;
    if (CreatingGeofence === false) {
        CreatingGeofence = true;
        var thisZoomLevel = map.getZoom();
        if (thisZoomLevel > 13) {
            var createMsg = "Create site at this location";
            input_box = confirm(createMsg);
            if (input_box === true) {
                var thisLat = clickedPoint.lat();
                var thisLong = clickedPoint.lng();
                thisLatLong = thisLat + ',' + thisLong;
                window.external.CreateSite(thisLatLong);
            }
            else {
                CreatingGeofence = false;
            }
        }
        else {
            var cantMsg = "Cannot create site at this zoom level. Please zoom in and retry"
            window.alert(cantMsg);
        }
        CreatingGeofence = false;
    }
}
function loadKMLRoute(kmlUri) {
    try {
        var kmlOptions = {
            suppressInfoWindows: true,
            preserveViewport: false,
            map: map
        };
        var kmlLayer = new google.maps.KmlLayer(kmlUri, kmlOptions);
    } catch (e) {
        alert(e.message);
    }
}
