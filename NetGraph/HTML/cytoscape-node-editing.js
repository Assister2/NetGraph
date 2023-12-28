var sel_node_shapes = [
	{ type: "top_left", obj: null, left: 0, top: 0, visible: false },
	{ type: "top_middle", obj: null, left: 0, top: 0, visible: false },
	{ type: "top_right", obj: null, left: 0, top: 0, visible: false },
	{ type: "middle_left", obj: null, left: 0, top: 0, visible: false },
	{ type: "middle_right", obj: null, left: 0, top: 0, visible: false },
	{ type: "bottom_left", obj: null, left: 0, top: 0, visible: false },
	{ type: "bottom_middle", obj: null, left: 0, top: 0, visible: false },
	{ type: "bottom_right", obj: null, left: 0, top: 0, visible: false },
];

var sel_node_x, sel_node_y;
var _down_x, shape_down_y;
var selected_node_id = null;
var selected_shape_editable_index = null;
var resize_offset = 10;

$(document).ready(function () {
	init_sel_node_shapes();

	$("#sel_node_wrap").on("mousedown", handle_nodeitem_mousedown);
	$("#sel_node_wrap").on("mouseup", handle_nodeitem_mouseup);
	$("#sel_node_wrap").on("mousemove", handle_nodeitem_mousemove);
});

function select_node_event(node) {
	var node_data = node.data();
	var selected_node = cy.$(':selected');
	if (selected_node == null || selected_node == undefined || selected_node.length == 0) return;

	var zoom = cy.zoom();
	var node_width = parseFloat(node_data.width);
	var node_height = parseFloat(node_data.height);
	var node_zoom_width = node_width * zoom;
	var node_zoom_height = node_height * zoom;

	var node_x = parseFloat(node.renderedPosition().x);
	var node_y = parseFloat(node.renderedPosition().y);

	sel_node_x = node_x - node_zoom_width / 2 ;
	sel_node_y = node_y - node_zoom_height / 2;

	draw_selection_shapes_for_node(sel_node_x, sel_node_y, node_zoom_width, node_zoom_height);
}

function update_shape_position_for_node() { 
	var selected_node = cy.$(':selected');
	if (selected_node == null || selected_node == undefined || selected_node.length == 0 || selected_node.data() == null ||
		selected_node.data() == undefined || selected_node.data().nodeType == null || selected_node.data().nodeType == undefined) return;
	//if (selected_node.data().nodeType.indexOf("group") >= 1) return;
	if (selected_node.data().shape == undefined) return;
	var node_data = selected_node.data();
	
	var zoom = cy.zoom();
	var node_width = selected_node.data().nodeType.indexOf("group") >= 1 ? parseFloat(selected_node[0].width()) + parseFloat(2 *15 -7) : parseFloat(node_data.width);
	var node_height = selected_node.data().nodeType.indexOf("group") >= 1 ? parseFloat(selected_node[0].height()) + parseFloat(2*15-7): parseFloat(node_data.height);
	var node_x = parseFloat(selected_node.renderedPosition().x);
	var node_y = parseFloat(selected_node.renderedPosition().y);
	var node_zoom_width = node_width * zoom;
	var node_zoom_height = node_height * zoom;

	sel_node_x = node_x - node_zoom_width / 2;
	sel_node_y = node_y - node_zoom_height / 2;

	draw_selection_shapes_for_node(sel_node_x, sel_node_y, node_zoom_width, node_zoom_height);
}

function init_sel_node_shapes() {
	for (var i = 0; i < sel_node_shapes.length; i++) {
		sel_node_shapes[i].obj = document.createElement("div");
		sel_node_shapes[i].obj.setAttribute("class", "sel_node_item sel_item_" + i);
		sel_node_shapes[i].obj.style.display = "none";
		
		document.getElementById("sel_node_wrap").appendChild(sel_node_shapes[i].obj);
	}
}

function draw_selection_shapes_for_node(left, top, width, height, flag = true) {
	if (flag == false) {
		for (var i = 0; i < sel_node_shapes.length; i++) {
			sel_node_shapes[i].obj.style.left = (sel_node_shapes[i].left - SHAPE_OFFSET) + "px";
			sel_node_shapes[i].obj.style.top = (sel_node_shapes[i].top - SHAPE_OFFSET) + "px";
			sel_node_shapes[i].obj.style.display = flag ? "block" : "none";
		}
		return;
	}
	if (width < resize_offset || height < resize_offset) return;
	var right = left + width;
	var bottom = top + height;
	var middle_x = left + parseFloat(width / 2);
	var middle_y = top + parseFloat(height / 2);
	sel_node_shapes[TOP_LEFT].left = left;
	sel_node_shapes[TOP_LEFT].top = top;
	sel_node_shapes[TOP_MIDDLE].left = middle_x;
	sel_node_shapes[TOP_MIDDLE].top = top;
	sel_node_shapes[TOP_RIGHT].left = right;
	sel_node_shapes[TOP_RIGHT].top = top;
	sel_node_shapes[MIDDLE_LEFT].left = left;
	sel_node_shapes[MIDDLE_LEFT].top = middle_y;
	sel_node_shapes[MIDDLE_RIGHT].left = right;
	sel_node_shapes[MIDDLE_RIGHT].top = middle_y;
	sel_node_shapes[BOTTOM_LEFT].left = left;
	sel_node_shapes[BOTTOM_LEFT].top = bottom;
	sel_node_shapes[BOTTOM_MIDDLE].left = middle_x;
	sel_node_shapes[BOTTOM_MIDDLE].top = bottom;
	sel_node_shapes[BOTTOM_RIGHT].left = right;
	sel_node_shapes[BOTTOM_RIGHT].top = bottom;

	for (var i = 0; i < sel_node_shapes.length; i++) {
		sel_node_shapes[i].obj.style.left = (sel_node_shapes[i].left - SHAPE_OFFSET) + "px";
		sel_node_shapes[i].obj.style.top = (sel_node_shapes[i].top - SHAPE_OFFSET) + "px";
		sel_node_shapes[i].obj.style.display = flag ? "block" : "none";
		sel_node_shapes[i].obj.style["background-color"] = "red";
	}
}

function handle_nodeitem_mousedown(e) {
	e.preventDefault();
	var selected_node = cy.$(':selected');
	if (selected_node == null || selected_node == undefined || selected_node.length == 0) return;
	if (selected_node.data().nodeType.indexOf("group") >= 1) return;
	shape_down_posx = e.clientX;
	shape_down_posy = e.clientY;
	selected_shape_editable_index = get_selected_shape_editable(shape_down_posx, shape_down_posy);

	if (selected_shape_editable_index == null) {
		return;
	}
	document.getElementById("sel_node_wrap").style.zIndex = 100; 
}

function handle_nodeitem_mouseup(e) {
	selected_shape_editable_index = null;
	document.getElementById("sel_node_wrap").style.zIndex = null;
}

function handle_nodeitem_mousemove(e) {
	e.preventDefault();

	var selected_node = cy.$(':selected');
	if (selected_node == null || selected_node == undefined || selected_node.length == 0) return;

	//
	if (selected_shape_editable_index == null) return;

	var posx = e.clientX;
	var posy = e.clientY;

	var node_left = sel_node_shapes[TOP_LEFT].left;
	var node_top = sel_node_shapes[TOP_LEFT].top;
	var node_width = sel_node_shapes[BOTTOM_RIGHT].left - sel_node_shapes[TOP_LEFT].left;
	var node_height = sel_node_shapes[BOTTOM_RIGHT].top - sel_node_shapes[TOP_LEFT].top;

	if (node_width < resize_offset || node_height < resize_offset) return;

	switch (selected_shape_editable_index) {
		case TOP_LEFT:
			node_left = posx;
			node_top = posy;
			node_width = node_width - (posx - sel_node_shapes[TOP_LEFT].left);
			node_height = node_height - (posy - sel_node_shapes[TOP_LEFT].top);
			break;
		case TOP_MIDDLE:
			node_top = posy;
			node_height = node_height - (posy - sel_node_shapes[TOP_LEFT].top);
			break;
		case TOP_RIGHT:
			node_top = posy;
			node_width = (posx - sel_node_shapes[TOP_LEFT].left);
			node_height = node_height - (posy - sel_node_shapes[TOP_LEFT].top);
			break;
		case MIDDLE_LEFT:
			node_left = posx;
			node_width = node_width - (posx - sel_node_shapes[TOP_LEFT].left);
			break;
		case MIDDLE_RIGHT:
			node_width = (posx - sel_node_shapes[TOP_LEFT].left);
			break;
		case BOTTOM_LEFT:
			node_left = posx;
			node_width = node_width - (posx - sel_node_shapes[TOP_LEFT].left);
			node_height = (posy - sel_node_shapes[TOP_LEFT].top);
			break;
		case BOTTOM_MIDDLE:
			node_height = (posy - sel_node_shapes[TOP_LEFT].top);
			break;
		case BOTTOM_RIGHT:
			node_width = (posx - sel_node_shapes[TOP_LEFT].left);
			node_height = (posy - sel_node_shapes[TOP_LEFT].top);
			break;
	}
	draw_selection_shapes_for_node(node_left, node_top, node_width, node_height);
	update_selected_node_data(node_left, node_top, node_width, node_height);
	//if (selected_node.data().nodeType.indexOf("group") >= 1) selected_shape_editable_index = null;
}

function update_selected_node_data(node_left, node_top, node_width, node_height) {
	var selected_node = cy.$(':selected');
	if (selected_node == null || selected_node == undefined || selected_node.length == 0) return;

	var cy_pan = cy.pan();
	var offset_x = cy_pan.x;
	var offset_y = cy_pan.y;
	var zoom = cy.zoom();
	var node_zoom_width = node_width / zoom;
	var node_zoom_height = node_height / zoom;

	if (node_zoom_width < 15 || node_zoom_height < 15) return;

	var updated_x = parseFloat(node_left / zoom) + parseFloat(node_zoom_width / 2) - parseFloat(offset_x / zoom);
	var updated_y = parseFloat(node_top / zoom) + parseFloat(node_zoom_height / 2) - parseFloat(offset_y / zoom);

	selected_node.position("x", updated_x);
	selected_node.position("y", updated_y);
	selected_node.data("width", node_zoom_width);
	selected_node.data("height", node_zoom_height);
}

function get_selected_shape_editable(posx, posy) {
	for (var i = 0; i < sel_node_shapes.length; i++) {
		var left = sel_node_shapes[i].left;
		var top = sel_node_shapes[i].top;
		if (posx > (left - SHAPE_OFFSET) && posx < (left + SHAPE_OFFSET) && posy > (top - SHAPE_OFFSET) && posy < (top + SHAPE_OFFSET)) {
			return i;
		}
	}
	return null;
}