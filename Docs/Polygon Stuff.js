//This is the polygon stuff

function CreatePolygonGeoFence() {
    IsCreatingPolygon = true;
    SetupDrawingManager();
    SetupPolygonContextMenu();
    drawingManager.setDrawingMode(google.maps.drawing.OverlayType.POLYGON);
    drawingManager.setOptions({
        drawingControl: true
    });

}
function SetupDrawingManager() {
    try {
        var polyOptions = {
            strokeWeight: 0,
            fillOpacity: 0.45,
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
        google.maps.event.addListener(drawingManager, 'overlaycomplete', function (event) {
            if (event.type == google.maps.drawing.OverlayType.CIRCLE) {
                var radius = event.overlay.getRadius();
            }
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
                });

                google.maps.event.addListener(newShape, 'rightclick', function (mEvent) {
                    if (selectedShape) {
                        var point = convertPoint(mEvent.latLng);
                        PolygonContextMenu.showContextMenu(point.x + 400, point.y + 25);
                    }
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

function convertPoint(latLng) {
    var topRight = map.getProjection().fromLatLngToPoint(map.getBounds().getNorthEast());
    var bottomLeft = map.getProjection().fromLatLngToPoint(map.getBounds().getSouthWest());
    var scale = Math.pow(2, map.getZoom());
    var worldPoint = map.getProjection().fromLatLngToPoint(latLng);
    return new google.maps.Point((worldPoint.x - bottomLeft.x) * scale, (worldPoint.y - topRight.y) * scale);
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
    polygonOptions.fillColor = color;
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

function clearSelection() {
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

function selectColor(color) {
    try {
        selectedColor = color;
        for (var i = 0; i < colors.length; ++i) {
            var currColor = colors[i];
            try {
                colorButtons[currColor].style.border = currColor == color ? '2px solid #789' : '2px solid #fff';
            } catch (e) {

            }
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
        polygonOptions.fillColor = color;
        drawingManager.set('polygonOptions', polygonOptions);
    } catch (e) {

    }
}


function SetupPolygonContextMenu() {
    try {
        PolygonContextMenu = new dhtmlXMenuObject();
        PolygonContextMenu = new dhtmlXMenuObject(null, "dhx_blue");
        PolygonContextMenu.setImagePath("../../../dhtmlxMenu/codebase/imgs/");
        PolygonContextMenu.setIconsPath("../images/");
        PolygonContextMenu.renderAsContextMenu();
        PolygonContextMenu.setOpenMode("web");
        PolygonContextMenu.attachEvent("onClick", ShowPolygonContextMenu);
        clientID = document.getElementById("hiddenClientID").value;
        var xmlFile = "xmlFiles/" + clientID + "_polygonContextMenu.xml?etc=" + new Date().getTime();
        PolygonContextMenu.loadXML(xmlFile);

        // PolygonContextMenu.addContextZone("myMap");
    }
    catch (Error) {
        window.alert("SetMapContextMenu Error 834: " + Error.message);
    }
}

function ShowPolygonContextMenu(id) {
    if (selectedShape) {
        switch (id) {
            case 'save':
                if (IsCreatingPolygon == true) {
                    SaveCoords();
                    CreateNewGeofence(0, 0);
                } else {
                    SaveCoords();
                }
                break;
            case 'delete':
                if (selectedShape) {
                    selectedShape.setMap(null);
                    $("#hiddenPolyGeoID").val(selectedShape.ID);
                    $("#hiddenPolygonGeoName").val(selectedShape.PName);
                    $("#hiddenButtonDeletePolyGeofence").click();                 
                }
                break;
            case 'cancel':
                break;
        }
    }
}

function SaveCoords() {
    if (IsCreatingPolygon == true) {
        $("#hiddenIsNewPolygon").val(true);
        getPolyCoords();
    } else {
        $("#hiddenIsNewPolygon").val(false);
        $("#hiddenPolyGeoID").val(selectedShape.ID);
        getPolyCoords();
        $("#hiddenButtonSavePolyGeofence").click();
    }

}
function getPolyCoords() {

    if (selectedShape) {
        var polylineData = '';
        var i = 0;
        var firstLat;
        var firstLng;
        for (i = 0; i < selectedShape.getPath().length; ++i) {
            var thisLat = selectedShape.getPath().getAt(i).lat().toFixed(5);
            var thisLng = selectedShape.getPath().getAt(i).lng().toFixed(5);
            if (i == 0) {
                firstLat = thisLat;
                firstLng = thisLng;
            }
            polylineData += thisLng + " " + thisLat + ",";
        }
        polylineData += firstLng + " " + firstLat;
        $("#hiddenPolyGeoCoords").val(polylineData);
    }
}
function setKMLColor(selectColor) {
    var gp0 = "40";
    var gp1 = selectColor.substring(1, 3);
    var gp2 = selectColor.substring(3, 5);
    var gp3 = selectColor.substring(5, 7);
    return gp0 + gp3 + gp2 + gp1;
}

function OnSuccessShowPolygonGeofences(response) {                      //This is the result of GetPolygonGeofences
    BuildPolygonGeoArray(response);
    PolygonMapArray = [];

    for (i in PolygonGeoArray) {
        // Define the LatLng coordinates for the polygon's path.
        var polyCoords = [];
        var polyCoords1 = [];
        var polyCoords2 = [];
        polyCoords = PolygonGeoArray[i][2].split("#");
        for (j in polyCoords) {
            polyCoords1 = polyCoords[j].split(",");
            polyCoords2[j] = new google.maps.LatLng(parseFloat(polyCoords1[0]), parseFloat(polyCoords1[1]));
        }
        // Construct the polygon.
        var thisPoly = new google.maps.Polygon({
            paths: polyCoords2,
            strokeColor: '#FF0000',
            strokeOpacity: 0.25,
            strokeWeight: 1,
            fillColor: '#FFCCFF',
            fillOpacity: 0.5,
            indexID: i,
            ID: PolygonGeoArray[i][3],
            PName: PolygonGeoArray[i][1]

        });
        var geoDetails = 'Geofence: ' + PolygonGeoArray[i][1];
        // setup InfoWindow to use as 'tooltip'
        //var html = "<strong>Geofence: " + PolygonGeoArray[i][1] + "</strong>";
        //attachPolygonInfoWindow(thisPoly, html);
        google.maps.event.addListener(thisPoly, 'click', function (eClick) {
            SetupDrawingManager();
            SetupPolygonContextMenu();
            setSelection(PolygonMapArray[this.indexID]);
            google.maps.event.addListener(PolygonMapArray[this.indexID], 'rightclick', function (e) {
                if (selectedShape) {
                    var point = convertPoint(e.latLng);
                    PolygonContextMenu.showContextMenu(point.x + 400, point.y + 25);
                }
            });
        });


        thisPoly.setMap(map);
        PolygonMapArray.push(thisPoly);

        //Now put the marker in the ploygon
        // calculate the bounds of the polygon
        var bounds = new google.maps.LatLngBounds();

        for (var i = 0; i < thisPoly.getPath().getLength() ; i++) {
            bounds.extend(thisPoly.getPath().getAt(i));
        }

        var sw = bounds.getSouthWest();
        var ne = bounds.getNorthEast();
        // Guess 100 random points inside the bounds, 
        // put a marker at the first one contained by the polygon and break out of the loop
        for (var i = 0; i < 100; i++) {
            var ptLat = Math.random() * (ne.lat() - sw.lat()) + sw.lat();
            var ptLng = Math.random() * (ne.lng() - sw.lng()) + sw.lng();
            var point = new google.maps.LatLng(ptLat, ptLng);
            if (google.maps.geometry.poly.containsLocation(point, thisPoly)) {
                image = 'Images/geofence.png';
                geoMarker = new google.maps.Marker({
                    position: point,
                    map: map,
                    title: geoDetails,
                    icon: image,
                    zIndex: 10
                });
                break;
            }
        }
        geofenceMarkersArray.push(geoMarker);
    }
}

function attachPolygonInfoWindow(polygon, html) {                       // Display polygon info on mouse over
    polygon.infoWindow = new google.maps.InfoWindow({
        content: html,
    });
    google.maps.event.addListener(polygon, 'mouseover', function (e) {
        var latLng = e.latLng;
        this.setOptions({ fillOpacity: 0.1 });
        polygon.infoWindow.setPosition(latLng);
        polygon.infoWindow.open(map);
    });
    google.maps.event.addListener(polygon, 'mouseout', function () {
        this.setOptions({ fillOpacity: 0.35 });
        polygon.infoWindow.close();
    });
}
function BuildPolygonGeoArray(tmpArrayStr) {
    var tmpArray = tmpArrayStr.split('~');
    PolygonGeoArray = [];
    for (var i = 0; i < tmpArray.length - 1; i++) {
        PolygonGeoArray[i] = tmpArray[i].split('|');
    }
}
// End of Polygon stuff