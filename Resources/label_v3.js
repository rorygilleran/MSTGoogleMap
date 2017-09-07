// Define the overlay, derived from google.maps.OverlayView
function Label(opt_options, colour, offset_x, offset_y, z_index) {
    // Initialization 
    this.setValues(opt_options);
    // Label specific 
    var span = this.span_ = document.createElement('span');
    // Add code to allow use of offset for better label positioning
    var pos_right = (-12 + offset_y) + 'px';
    var pos_top = (-30 + offset_x) + 'px';
    span.style.cssText = 'position: relative; right: ' + pos_right + '; top: ' + pos_top + '; ' +
                      'white-space: nowrap; border: 1px solid black; ' +
                      'text-decoration:underline;font-family:Tahoma;font-style:italic;font-size:8.25pt;font-weight: bold; color:black;' +
					  'padding: 2px; background-color: ' + colour + '; z-index:' + z_index +';';
    var div = this.div_ = document.createElement('div');
    div.appendChild(span);
    div.style.cssText = 'position: absolute; display: none';
};

Label.prototype = new google.maps.OverlayView;
// Implement onAdd
Label.prototype.onAdd = function () {
    var pane = this.getPanes().overlayLayer;
    pane.appendChild(this.div_);
    // Ensures the label is redrawn if the text or position is changed. 
    var me = this;
    this.listeners_ = [
         google.maps.event.addListener(this, 'position_changed', function () { me.draw(); }),
         google.maps.event.addListener(this, 'text_changed', function () { me.draw(); })
 ];
};
// Implement onRemove
Label.prototype.onRemove = function () {
    this.div_.parentNode.removeChild(this.div_);
    // Label is removed from the map, stop updating its position/text.
    for (var i = 0, I = this.listeners_.length; i < I; ++i) {
        google.maps.event.removeListener(this.listeners_[i]);
    }
};
// Implement draw
Label.prototype.draw = function () {
    var projection = this.getProjection();
    var position = projection.fromLatLngToDivPixel(this.get('position'));
    var div = this.div_;
    div.style.left = (position.x) + 'px';
    div.style.top = (position.y) + 'px';
    div.style.display = 'block';
    this.span_.innerHTML = this.get('text').toString();
};