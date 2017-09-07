/*
Polylines with arrows in Google Maps API v3
by Pavel Zotov
http://yab.hot-line.su/
2011-02-19
*/
GPolylineWithArrows = function (options) {
    this.options = options;
    this.arrows = [];
}
GPolylineWithArrows.prototype = new google.maps.OverlayView();
GPolylineWithArrows.prototype.onAdd = function () {
    this.polyline = new google.maps.Polyline({
        path: this.options.path,
        strokeColor: this.options.strokeColor,
        strokeWeight: this.options.strokeWeight,
        strokeOpacity: this.options.strokeOpacity
    });
    this.polyline.setMap(this.getMap());
}
GPolylineWithArrows.prototype.onRemove = function () {
    for (var i = this.arrows.length; i > 0; i--) {
        try {
            this.arrows[i - 1].setMap(null);
            delete this.arrows[i - 1];
        }
        catch (Error) {
        }
        this.arrows.length--;
    }
    this.polyline.setMap(null);
    delete this.polyline;
}
GPolylineWithArrows.prototype.draw = function () {
    for (var i = this.arrows.length; i > 0; i--) {
        try {
            this.arrows[i - 1].setMap(null);
            delete this.arrows[i - 1];
        }
        catch (Error) {
        }
        this.arrows.length--;
    }

    var prj = this.getProjection(), middle;
    for (var i = 1; i < this.options.path.length; i++) {
        var p1 = prj.fromLatLngToContainerPixel(this.options.path[i]),
			p0 = prj.fromLatLngToContainerPixel(this.options.path[i - 1]),
			vector = new google.maps.Point(p1.x - p0.x, p1.y - p0.y),
			length = Math.sqrt(vector.x * vector.x + vector.y * vector.y),
			normal = new google.maps.Point(vector.x / length, vector.y / length);

        if (length > this.options.arrowSize) {
            if (this.options.middleArrow) middle = new google.maps.Point((p1.x + p0.x) / 2, (p1.y + p0.y) / 2);
            else middle = p1;
            var offsetMiddle = new google.maps.Point(normal.x * this.options.arrowSize, normal.y * this.options.arrowSize),
				arrowPart1 = new google.maps.Point(-offsetMiddle.y * 0.4, offsetMiddle.x * 0.4),
				arrowPart2 = new google.maps.Point(offsetMiddle.y * 0.4, -offsetMiddle.x * 0.4),
				arrowPoint1 = new google.maps.Point(middle.x - offsetMiddle.x + arrowPart1.x, middle.y - offsetMiddle.y + arrowPart1.y),
				arrowPoint2 = new google.maps.Point(middle.x - offsetMiddle.x + arrowPart2.x, middle.y - offsetMiddle.y + arrowPart2.y);

            this.arrows[i - 1] = new google.maps.Polygon({
                map: this.getMap(),
                path: [
					prj.fromContainerPixelToLatLng(middle),
					prj.fromContainerPixelToLatLng(arrowPoint1),
					prj.fromContainerPixelToLatLng(arrowPoint2)
				],
                fillColor: this.options.fillColor ? this.options.fillColor : this.options.strokeColor,
                fillOpacity: this.options.fillOpacity ? this.options.fillOpacity : this.options.strokeOpacity,
                strokeColor: this.options.arrowStrokeColor ? this.options.arrowStrokeColor : this.options.strokeColor,
                strokeOpacity: this.options.arrowStrokeOpacity ? this.options.arrowStrokeOpacity : this.options.strokeOpacity,
                strokeWeight: this.options.arrowStrokeWeight ? this.options.arrowStrokeWeight : this.options.strokeWeight
            });
        }
    }
}
