﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Public Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Public ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("MSTGoogleMap.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Public Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to ///**
        '''//* Based on code provided by Mike Williams
        '''//* http://econym.org.uk/gmap/arrows.htm
        '''//* Improved and transformed to v3
        '''//*/
        '''
        '''var map, setArrows, arrowheads = [];
        '''
        '''
        '''function ArrowHandler() {
        '''    this.setMap(map);
        '''    // Markers with &apos;head&apos; arrows must be stored
        '''    this.arrowheads = [];
        '''}
        '''// Extends OverlayView from the Maps API
        '''ArrowHandler.prototype = new google.maps.OverlayView();
        '''
        '''// Draw is inter alia called on zoom change events.
        '''// So we can use the draw method as zoom change  [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property Arrowheads() As String
            Get
                Return ResourceManager.GetString("Arrowheads", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to ///*
        '''//	ContextMenu v1.0
        '''	
        '''//	A context menu for Google Maps API v3
        '''//	http://code.martinpearman.co.uk/googlemapsapi/contextmenu/
        '''	
        '''//	Copyright Martin Pearman
        '''//	Last updated 21st November 2011
        '''	
        '''//	developer@martinpearman.co.uk
        '''	
        '''//	This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
        '''
        '''//	This program is distributed in th [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property ContextMenu() As String
            Get
                Return ResourceManager.GetString("ContextMenu", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to // ELabel.js 
        '''//
        '''//   This Javascript is provided by Mike Williams
        '''//   Community Church Javascript Team
        '''//   http://www.bisphamchurch.org.uk/   
        '''//   http://econym.org.uk/gmap/
        '''//
        '''//   This work is licenced under a Creative Commons Licence
        '''//   http://creativecommons.org/licenses/by/2.0/uk/
        '''//
        '''// Version 0.2      the .copy() parameters were wrong
        '''// version 1.0      added .show() .hide() .setContents() .setPoint() .setOpacity() .overlap
        '''// version 1.1      Works with GMarkerManager in v2.67, v2.68, v2.69,  [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property elabel() As String
            Get
                Return ResourceManager.GetString("elabel", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to /*
        '''    geoXML3.js
        '''
        '''    Renders KML on the Google Maps JavaScript API Version 3 
        '''    http://code.google.com/p/geoxml3/
        '''
        '''   Copyright 2009 Sterling Udell
        '''
        '''   Licensed under the Apache License, Version 2.0 (the &quot;License&quot;);
        '''   you may not use this file except in compliance with the License.
        '''   You may obtain a copy of the License at
        '''
        '''       http://www.apache.org/licenses/LICENSE-2.0
        '''
        '''   Unless required by applicable law or agreed to in writing, software
        '''   distributed under the License is distributed on an &quot;AS  [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property geoxml3() As String
            Get
                Return ResourceManager.GetString("geoxml3", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        '''</summary>
        Public ReadOnly Property GLB1() As System.Drawing.Icon
            Get
                Dim obj As Object = ResourceManager.GetObject("GLB1", resourceCulture)
                Return CType(obj,System.Drawing.Icon)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to /*
        '''Polylines with arrows in Google Maps API v3
        '''by Pavel Zotov
        '''http://yab.hot-line.su/
        '''2011-02-19
        '''*/
        '''GPolylineWithArrows = function (options) {
        '''    this.options = options;
        '''    this.arrows = [];
        '''}
        '''GPolylineWithArrows.prototype = new google.maps.OverlayView();
        '''GPolylineWithArrows.prototype.onAdd = function () {
        '''    this.polyline = new google.maps.Polyline({
        '''        path: this.options.path,
        '''        strokeColor: this.options.strokeColor,
        '''        strokeWeight: this.options.strokeWeight,
        '''        st [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property GPolyLine() As String
            Get
                Return ResourceManager.GetString("GPolyLine", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to /*! jQuery v1.11.1 | (c) 2005, 2014 jQuery Foundation, Inc. | jquery.org/license */
        '''!function(a,b){&quot;object&quot;==typeof module&amp;&amp;&quot;object&quot;==typeof module.exports?module.exports=a.document?b(a,!0):function(a){if(!a.document)throw new Error(&quot;jQuery requires a window with a document&quot;);return b(a)}:b(a)}(&quot;undefined&quot;!=typeof window?window:this,function(a,b){var c=[],d=c.slice,e=c.concat,f=c.push,g=c.indexOf,h={},i=h.toString,j=h.hasOwnProperty,k={},l=&quot;1.11.1&quot;,m=function(a,b){return new m.fn.init(a,b)},n=/^[\s\uFEFF\xA [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property jquery_1_11_1_min() As String
            Get
                Return ResourceManager.GetString("jquery_1_11_1_min", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        '''</summary>
        Public ReadOnly Property Key() As System.Drawing.Icon
            Get
                Dim obj As Object = ResourceManager.GetObject("Key", resourceCulture)
                Return CType(obj,System.Drawing.Icon)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to // Define the overlay, derived from google.maps.OverlayView
        '''function Label(opt_options, colour, offset_x, offset_y, z_index) {
        '''    // Initialization 
        '''    this.setValues(opt_options);
        '''    // Label specific 
        '''    var span = this.span_ = document.createElement(&apos;span&apos;);
        '''    // Add code to allow use of offset for better label positioning
        '''    var pos_right = (-12 + offset_y) + &apos;px&apos;;
        '''    var pos_top = (-30 + offset_x) + &apos;px&apos;;
        '''    span.style.cssText = &apos;position: relative; right: &apos; + pos_right + &apos;; top: &apos; +  [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property label_v3() As String
            Get
                Return ResourceManager.GetString("label_v3", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to ///**
        '''// * @name MarkerClustererPlus for Google Maps V3
        '''// * @version 2.1.1 [November 4, 2013]
        '''// * @author Gary Little
        '''// * @fileoverview
        '''// * The library creates and manages per-zoom-level clusters for large amounts of markers.
        '''// * &lt;p&gt;
        '''// * This is an enhanced V3 implementation of the
        '''// * &lt;a href=&quot;http://gmaps-utility-library-dev.googlecode.com/svn/tags/markerclusterer/&quot;
        '''// * &gt;V2 MarkerClusterer&lt;/a&gt; by Xiaoxi Wu. It is based on the
        '''// * &lt;a href=&quot;http://google-maps-utility-library-v3.googlecode.com/svn/t [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property markerclusterer() As String
            Get
                Return ResourceManager.GetString("markerclusterer", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to /**
        ''' * @name MarkerWithLabel for V3
        ''' * @version 1.1.9 [June 30, 2013]
        ''' * @author Gary Little (inspired by code from Marc Ridey of Google).
        ''' * @copyright Copyright 2012 Gary Little [gary at luxcentral.com]
        ''' * @fileoverview MarkerWithLabel extends the Google Maps JavaScript API V3
        ''' *  &lt;code&gt;google.maps.Marker&lt;/code&gt; class.
        ''' *  &lt;p&gt;
        ''' *  MarkerWithLabel allows you to define markers with associated labels. As you would expect,
        ''' *  if the marker is draggable, so too will be the label. In addition, a marke [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property markerwithlabel() As String
            Get
                Return ResourceManager.GetString("markerwithlabel", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to .context_menu {
        '''    background-color: #ffffff;
        '''    border: 1px solid #212121;
        '''    border-top : 10px solid #212121;   
        '''    border-radius: 20px;
        '''}
        '''
        '''.context_menu_item {
        '''    color: #212121;
        '''    font-family: Tahoma;
        '''    font-size: 10pt;
        '''    padding: 3px 6px;
        '''}
        '''
        '''.context_menu_separator {
        '''    background-color: gray;
        '''    height: 1px;
        '''    margin: 0;
        '''    padding: 0;
        '''}
        '''
        '''.context_menu_item:hover {
        '''    background-color: #CCCCCC;
        '''        border-radius: 20px;
        '''}
        '''
        '''.gm-style-iw {
        '''    maxWidth: 300 [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property MST_GoogleMapCSS() As String
            Get
                Return ResourceManager.GetString("MST_GoogleMapCSS", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to //*****************************************************************************************
        '''// File Name:       MST_GoogleMapJS.js
        '''// Author:          Rory Gilleran 
        '''// Copyright:       Business Information System Ltd 2011
        '''// Last Updated:    16th December 2015
        '''//*****************************************************************************************
        '''
        '''
        '''var bounds;
        '''var BufferZoneArray = [];
        '''var BufferZoneIsEditable = &quot;False&quot;;
        '''var BufferZoneWidth = &quot;0&quot;;
        '''var centerMarker;
        '''var changedMapType;
        '''var [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property MST_GoogleMapJS() As String
            Get
                Return ResourceManager.GetString("MST_GoogleMapJS", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to //*****************************************************************************************
        '''// File Name:       MST_Routes.js
        '''// Author:          Rory Gilleran 
        '''// Copyright:       Business Information System Ltd 2011
        '''// Last Updated:    02nd January 2016
        '''// Notes:           Copied from MSTW_Routes.js in MS Track Web 8
        '''//*****************************************************************************************
        '''
        '''var map = null;
        '''var MapEng;
        '''var newRoutesArray = new Array();
        '''var tmpRoutesArray = new  [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property MST_RoutesJS() As String
            Get
                Return ResourceManager.GetString("MST_RoutesJS", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to // Create an overlay on the map from a projected image - Maps v3...
        '''// Author. John D. Coryat 05/2009
        '''// USNaviguide LLC - http://www.usnaviguide.com
        '''// Thanks go to Mile Williams EInsert: http://econym.googlepages.com/einsert.js, Google&apos;s GOverlay Example and Bratliff&apos;s suggestion...
        '''// Opacity code from TPhoto: http://gmaps.tommangan.us/addtphoto.html
        '''// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Softwa [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property ProjectedOverlay() As String
            Get
                Return ResourceManager.GetString("ProjectedOverlay", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Public ReadOnly Property skin_modern_header() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("skin_modern_header", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Public ReadOnly Property spinner() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("spinner", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
    End Module
End Namespace
