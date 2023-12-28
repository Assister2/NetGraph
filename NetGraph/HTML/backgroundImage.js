
var test_img_data =
	"data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBwgHBgkIBwgKCgkLDRYPDQwMDRsUFRAWIB0iIiAdHx8kKDQsJCYxJx8fLT0tMTU3Ojo6Iys/RD84QzQ5OjcBCgoKDQwNGg8PGjclHyU3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3N//AABEIAHoAowMBIgACEQEDEQH/xAAcAAABBQEBAQAAAAAAAAAAAAADAQIEBQYABwj/xABDEAABAwIEAgYFCQYFBQAAAAABAAIDBBEFEiExBkETIlFhcYFCkZKx0QcUFSMyUlRywRYzQ2Kh4SSDovDxJjRTVZP/xAAaAQADAQEBAQAAAAAAAAAAAAAAAQIDBAUG/8QAIhEAAgIBBAMBAQEAAAAAAAAAAAECEQMSITFBBBNRFCMi/9oADAMBAAIRAxEAPwCzjmN91JY+6qWvtzUiOVfTWfPEnFKltBhtRWmPP0LC8tBtmtyT2MbLAyQC2ZodbsuqXiqf/pytF92AetwVtSPLaWFpN7RtF7dym/8AVGtLTZ3QeSaYCFJEg5rukHYqsSIhZZDcxTHZChujB2KAI1kiK6MhDcCEgEukKa6Rjd3NHiUw1EN9JWHuDrpWgCFNshOqIh6T/YPwSfOR6Mch8gPeUrQ6YfKmujUKnxI1ElQ2OAjoZMhzOGpsjGqlaB9S3Ugav/sjUmDTQ5zEFwKc6SVw3jb5E/qEB0kmexmjAt9z+6ltDSY9K0i6iSSW+1VNHm0KM6riDnB1cLWHptHuSckPQ2W+cdq5Uvzym/GD/wCpXI1h62X7ASjNaVVS4zBFYtdFYnm+5HkFEm4kiZm+uIHLIwD3pPLBdiWKb6JvFd24DUXGhLB/qCuGTMijaHyNbZo3csFi+PxYjSmlje5zrhxJkvt3bKLJxcBcxMANgPq4t/WVk/JgnZuvHk4pHopr4NbPLrfdaShuxEBt2xPINtyAvMpeKap1wOkseWYN9yiS49WPFurb+Zxcs35sS14j7PUZcULHAEwtv2vvb3KLJjbGuINXGBb0G3+K8vdilU7+Kxvg1DdXVT96qXy09yzfm/EaLxF9PTDjsRJJqJnA7WBH6BR341T3OaOR4P33/ErzYzyP+1NI7xeUy8Zvm18SofmTfBX5YnoT8ep43Oc2OFultXhRzxUxpuX07Taw6xKwocwHqhp7githmf8AYp5XflYSpfk5CvRjRrn8W3dczR2B0yxFAk4tcSf8RJ5R2WdFBXkXbQ1Nu3onW9ybSUclQ698g79VPuyjWLGi6+npIMz2yTfXu6QlpsSe/VCfxHIdP8S780mnvUSXDMQqGxiippZ2xjK50bL6pn0FjX/rqn2EOWXqxpY+yQ7HXn+HIfGRBdi7z/AHtLm8O42dsOn9Q+KceGsdG+HzDzb8VP8AX4x/y+oE7FZP/C31phxSblGwIx4dxr8BL7Tfik/Z3GfwMntt+KVZfjHeP6gH0nUfdj9k/Fci/s7jH4J/tt+K5LTl+MerH9RD+c1Mr8vSSuc70W6E+QUqmwLGasg0+EV0t/SFO8j12VxwTxDDg2LxVVTE6WMMLOqAXNvzF/cvZsI4mw7FYOkpappt9qORha5viFcMXsV2TKeno8TZwzjWGNFTiFC6nicQxpe5t8x7gb8lJreBMWwmjfW4m6n6COwLGSEuJJt2fqvTflCqYpcNoGNyHNXR3IPLVSflJkp38KVXRts4yMtY/wAy1eGKXAlO+GeV8I8HM4jknzVb6ZsQBGVgde5I5lauL5K8OabyV1XJ4FrR7lYfI5RfOW19iRlYzl2ucvQ5sHmAuzUDusnD0LaXI5RyveJ5vH8m2CM+1BPJ+adw91lJj4KwGDT6LjJ7ZHOd7yte6KZjnNsbtNjomWHptJ7V0xWLpI5pLJ22ZuPh3CIv3eGUg/ygjDDKSP8Ad0dO3wib8FeOZTkXDXetBfCPRJWq0/DJxl9Kk0+T7MTR4NTHMfzafUk4mxMYFhb6x0fSG4axpNrk3+CjcPv4k4gweHFKGkwwQyuc1rZKl7XjKSDswjkUnlhF0L1yqyPjjXfRVWCDrEd15+2Po27L0+uwjiiopZKabDaAtkGUvjrTcetgXm+JTNw/EJqCbpaeogfkkflbK1p07CO0Lk8iabTR0ePHlGj4OcGUU4OpMtxfwCv+lH9l59MzFafNKXvDI9Tbs1N9zp1T6lMoMfrKaq6CvjcC37TJNCnDydCSkh5PF1NuDs2mZpSEtsmUlRBWQCWAhw5i+oXPcBysuxSTVo4nFp0xjy3VRpCBsiPeFGkKLEkNL9d1yEVyVlUeXgEEEbhWmG4jNTSNdmdG8HSRhVdoluBa1+9eLGTi9j1mk0a+fH34hHTw1LmEwzNkMjDuB2jkVc8QY/SYjhz6anqY5XEh3Vv+qwNG9zhLc+jp/VNw+UiQ35tXSs8qr6YPCr26PReA8Tjw0T5qkQF4aNX5c1ifit/TcQ1Dh9VW5x+cFeC1b80cPnuhwslc76lhv2tSeZL/AC42PTK7To+hqXiKoiM3SshlBlt128soU48RXjOelpcttgF4RhhxQ6Nrp2x88shIP91bvraqNscTq2eNpFmvJvb1jmrUMclemgeTIuz1Ctr4ZaOciAMs12rbX2KaHQ21zg/mC8krocaNyMQlnYdx0hBPkqieqxCJxEs9SD3vcjXoVJA25u2bf5WJGjB6QNcSDUa+yVC+T/iuelpIsMlZWfNWSExupCxlg5xLsxcNdzqCLLFyzyTtyzPdI3seSU2OZ8QtFJJGP5Hke5ZPKnK2DhcaPXeJMYwt+QRQY3UZZAXuFWS1wB1bbPueWlu9eZyz4bNjOIl1Ba9S/L0zszmi5FtztYKHS1MzqgN+czjNuRK7ltzUeupgZHyBk7nuJc55N7ntSnK1sXigos0EbaWTqsklY8tIAZKQ25a4Dy6x/r2qvrZZz0cGIBz5Y22ilLtbcgHcx3HbuVXSzvhk1de3ar6nr4Z4uhqGNlYfRdyKjU2qN6SdhMCqZ6ch7HPFtPFayPFqSSIOlmhjfs5rnAEFUUYiawOiHVA9SqcXxisw+djKcQ9G9t+vHc35/ot8WT1rc5s+P2GtkxPD7/8AdQX/ADhR5MToPxcHthYr9pa4uzFlOSRb91y/2Up4lqjvBTH/AC1r+qJh+Zmv+kaH8VF7QXLFvx2Z7i4xRAnsauT/AExH+cq0tkrQXHqtJUqCifKddAvOUW+DrG0e0luY+KbTU0zn3jGUje/JWAhp6cWuS7sBRIRNUktYMjOa2UOERYPoGXa2d3SSNGkce3mVY09IHW6YhrRtE3Qefan09PHCOqOtzKktAB03K3jBckthxewsbDYJKoCW7HElrm23XNFy0crpslufitCSNS1L4ZPm00hAbox3d3qbIwuFpLOCh10XTND2AFw/qkw+sHVhnOmzXHl3FRdbMGuxJ8Nhk1b1T3KtqMLmYLsNwtI5lv8AhMt5qXCLBSMpTwyRVTS9pyi+u/JWBcMhcXaWVvJCyQddgJ7diok+GxyCw2+674qVDTwVdmfnaX1TLAlp30R42CJ1v1U6ow8hhyCRneCCFFdC5rLPILhzCynGnZrCV7FpS1BaNToofETGy07JW/w3a+BUGOpfG7LfyU+xq4HMB3Fkm9qGZwtPIGy4tHJ106Rro3ljj1hoQU0A9nqUAJlXJ1u/+i5IC2Agg0cMx7BskkqJJerGAG7dyDDCXWFy4q0pqcMGZ1r9y6Um+DK0CpqK9ny3PirBgDW5WbJmbkNk8aDRaKKXBNj2abgojO8FBudLFFBNt1aAI03JsLJJPPZNY82J5prnE80AK22W1tVArI7SdK0AA6Hsv2qXcg3BSzAPaQR1X6FTLdDQ3D63LaKc6ei7s7lZkCyzL+pI5h1sdDZWWHVwaBBUEhvou7O4qYy6JlHtE9xQ7lFkj05kHYoJaNiqBDrX8UCppWzscLWcRo62oRcrf9lDlfHGPrXtaDsS6yBq7M7URGKRzJABI3fv70+lqzTm5IAG6XG6mAVDWstoy5cOd+SpZpnSG1rBcktnsbrgLiFQyesfMwWzWv4pWTXGoaBzFrKGiwSGJ4cACNiCNwoT3BkixOxAHZdIp7YYntD2wNIdqDdctNLI1E+CPINBZGLygNOqK3VdJkEan3QglBTAM0p90IGyXNrunYwyaXJt9Ey6GwHOsdCuH2cvJMJStdqkAypg6WO7P3jdvgoDTpz81aB2V1+SiYhCGkSt+yT1tFEkNMk4diBYBBOers13YrF7NAQbrN7qdQV5jtDMepyceXj3JqQnHssTcLKcSNe3ECXklrmAtvyWtcAq3F8OFdCACGysvld+hU5I6olRdMx6RPmifDK6KRuV7TYhMXIbHJUi5ADw9wFgSuTVyLYGmabogKCzZEau45QoOic0oXYiN2QMcSkzJvNKgB+bfuXEpnM+ISoGOLk2+qQpOaVgFvcJC7MwMc3MDomNXem3xCGBXvhfBIWOIt6JvySHsU6v2i8SoHMqCibh9b0ZEMzjkOjXHkrUg23CznarrDiTSR3N9CnF9CaKjiSl+zVNHY1/6FUC2GKgGgqL6/VlY9YZVUjSDtHLly5ZlnLkq5AH/9k=";


var TOP_LEFT = 0;
var TOP_MIDDLE = 1;
var TOP_RIGHT = 2;
var MIDDLE_LEFT = 3;
var MIDDLE_RIGHT = 4;
var BOTTOM_LEFT = 5;
var BOTTOM_MIDDLE = 6;
var BOTTOM_RIGHT = 7;1
var SHAPE_OFFSET = 4;

var SEL_NONE = -1;
var SEL_IMAGE = 1;
var SEL_SHAPE = 2;

var KEY_LEFT = 37;
var KEY_UP = 38;
var KEY_RIGHT = 39;
var KEY_BOTTOM = 40;
var KEY_DEL = 46;

var img_data = [
	/*{ id: uuidv4(), img_data: test_img_data, img_obj: null, left: 0, top: 20, width: 120, height: 150, rotate: 0 },
	{ id: uuidv4(), img_data: test_img_data, img_obj: null, left: 210, top: 20, width: 50, height: 150, rotate: 0 },
	{ id: uuidv4(), img_data: test_img_data, img_obj: null, left: 10, top: 50, width: 120, height: 30, rotate: 0 }*/
];

var sel_shapes = [
	{ type: "top_left", obj: null, left: 0, top: 0, visible: false, full_screen: "false" },
	{ type: "top_middle", obj: null, left: 0, top: 0, visible: false, full_screen: "false" },
	{ type: "top_right", obj: null, left: 0, top: 0, visible: false, full_screen: "false" },
	{ type: "middle_left", obj: null, left: 0, top: 0, visible: false, full_screen: "false" },
	{ type: "middle_right", obj: null, left: 0, top: 0, visible: false, full_screen: "false" },
	{ type: "bottom_left", obj: null, left: 0, top: 0, visible: false, full_screen: "false" },
	{ type: "bottom_middle", obj: null, left: 0, top: 0, visible: false, full_screen: "false" },
	{ type: "bottom_right", obj: null, left: 0, top: 0, visible: false, full_screen: "false" },
]

var sel_background_color = "rgb(255,255,255)";
var selected_img_data = null;
var selected_shape_index = null;

var down_posx = 0;
var down_posy = 0;
var down_flag = -1;

var img_back_flag = true;

$(document).ready(function () {
	$("#img_wrap").on("mousedown", handle_mousedown);
	$("#img_wrap").on("mouseup", handle_mouseup);
	$("#img_wrap").on("mousemove", handle_mousemove);

	$(window).on("keyup", img_keyup);
	initBackgroundCanvas();
});

function enalbeImageBackground() {
	img_back_flag = true;
	$("#img_wrap").css("z-index", "9999");
}

function disableImageBackground() {
	img_back_flag = false;
	$("#img_wrap").css("z-index", "-2");
	hide_selection_shapes();
}

function clearImageBackground() {
	img_data = [];
	hide_selection_shapes();
	$(".img_back_item").remove();
	$(".svg_back_item").remove();
	setBackgroundColor("rgba(255,255,255,0)");
}

function initBackgroundCanvas() {
	init_sel_shapes();
}

function setBackgroundImage(data, flag, width = 0, height = 0) {
	flag = flag.toLowerCase();
	var obj = { id: uuidv4(), img_data: data, img_obj: null, left: 0, top: 0, width: width, height: height, rotate: 0, full_screen: flag, type: "image" };
	if (flag == "false") {
		obj.width = $("#img_wrap").width();
		obj.height = $("#img_wrap").height();
	}
	img_data.push(obj);
	draw_background_image(obj);
}

function setBackgroundSVG(data, flag, width = 0, height = 0) {
	flag = flag.toLowerCase();
	var obj = { id: uuidv4(), img_data: atob(data), img_obj: null, left: 0, top: 0, width: width, height: height, rotate: 0, full_screen: flag, type: "svg" };
	img_data.push(obj);
	draw_background_svg(obj);
}

function init_sel_shapes() {
	for (var i = 0; i < sel_shapes.length; i++) {
		sel_shapes[i].obj = document.createElement("div");
		sel_shapes[i].obj.setAttribute("class", "sel_item sel_item_" + i);
		sel_shapes[i].obj.style.display = "none";
		document.getElementById("img_wrap").appendChild(sel_shapes[i].obj);
	}
}

function init_backimg() {
	draw_background();
}

function setBackgroundColor(rgb) {
	sel_background_color = rgb;
	$("#img_wrap").css("background", sel_background_color);
}

function resizeMainBrowser(width, height) {
	draw_background(width, height);
}

function draw_background(width = 0, height = 0) {
	for (var i = 0; i < img_data.length; i++) {
		var item = img_data[i];
		if (item.full_screen == "true") {
			item.width = width;
			item.height = height;
		}
		if (item.type == "image") {
			draw_background_image(item);
		} else {
			draw_background_svg(item);
		}
	}
}

function addBackgrounds(tmp_img_data) {
	img_data = tmp_img_data;
	//img_data = JSON.parse(img_data_str);
	draw_background();
}

function set_fullscreen_size() {
	selected_img_data.left = 0;
	selected_img_data.top = 0;
	selected_img_data.width = $("#img_wrap").width();
	selected_img_data.height = $("#img_wrap").height();

	draw_background_image(selected_img_data);
}

function draw_background_image(item) {
	if (item.img_obj == null || item.img_obj == "" || item.img_obj == {} || item.img_obj == "{}") {
		item.img_obj = document.createElement("img");
		item.img_obj.setAttribute("class", "img_back_item");
		document.getElementById("img_wrap").appendChild(item.img_obj);
		item.img_obj.src = item.img_data;
		item.img_obj.onload = function () {
			if (item.full_screen == "false") {
				item.width = item.img_obj.width;
				item.height = item.img_obj.height;
			} else {
				item.img_obj.width = item.width;
				item.img_obj.height = item.height;
			}
		};
		item.img_obj.addEventListener("keyup", img_keyup);
	} else {
		item.img_obj.style.width = item.width + "px";
		item.img_obj.style.height = item.height + "px";
	}
	item.img_obj.style.left = item.left + "px";
	item.img_obj.style.top = item.top + "px";
}

function draw_background_svg(item) {
	if (item.img_obj == null || item.img_obj == "" || item.img_obj == {} || item.img_obj == "{}") {
		item.img_obj = $(item.img_data);
		$(item.img_obj).addClass("svg_back_item");
		$("#img_wrap").append($(item.img_obj));
		$(item.img_obj).on("keyup", img_keyup);
		$(item.img_obj).attr("width", item.width);
		$(item.img_obj).attr("height", item.height);
	} else {
		$(item.img_obj).attr("width", item.width);
		$(item.img_obj).attr("height", item.height);
	}
	$(item.img_obj).css("transform", "translate(" + item.left + "px, " + item.top + "px)");
}

function img_keyup(e) {
	if (selected_img_data == null) return;
	if (e.keyCode == KEY_DEL) {
		var uid = selected_img_data.id;
		var tmp_arr = [];
		for (var i = 0; i < img_data.length; i++) {
			if (img_data[i].id == uid) {
				selected_img_data.img_obj.remove();
				hide_selection_shapes();
			} else {
				tmp_arr.push(img_data[i]);
			}
		}
		img_data = [...tmp_arr];
	}
}

function handle_mousedown(e) {
	e.preventDefault();

	down_posx = e.clientX;
	down_posy = e.clientY;
	selected_shape_index = get_selected_shape(down_posx, down_posy);
	selected_img_data = get_selected_img(down_posx, down_posy);

	if (selected_shape_index != null) {
		down_flag = SEL_SHAPE;
	} else if (selected_img_data != null) {
		down_flag = SEL_IMAGE;
	}

	if (down_flag == SEL_NONE) {
		hide_selection_shapes();
		return;
	}
	draw_selection_shapes(selected_img_data, selected_img_data != null);
}

function handle_mouseup(e) { 
	down_flag = SEL_NONE;
}

function handle_mousemove(e) {
	e.preventDefault();
	if (down_flag == SEL_NONE) return;

	var posx = e.clientX;
	var posy = e.clientY;

	var offsetx = posx - down_posx;
	var offsety = posy - down_posy;
	down_posx = posx;
	down_posy = posy;

	if (down_flag == SEL_IMAGE) {
		selected_img_data.left = parseInt(selected_img_data.left) + parseInt(offsetx);
		selected_img_data.top = parseInt(selected_img_data.top) + parseInt(offsety);
	} else if (down_flag == SEL_SHAPE) {
		switch (selected_shape_index) {
			case TOP_LEFT:
				selected_img_data.left = parseInt(selected_img_data.left) + parseInt(offsetx);
				selected_img_data.top = parseInt(selected_img_data.top) + parseInt(offsety);
				selected_img_data.width = parseInt(selected_img_data.width) - parseInt(offsetx);
				selected_img_data.height = parseInt(selected_img_data.height) - parseInt(offsety);
				break;
			case TOP_MIDDLE:
				selected_img_data.top = parseInt(selected_img_data.top) + parseInt(offsety);
				selected_img_data.height = parseInt(selected_img_data.height) - parseInt(offsety);
				break;
			case TOP_RIGHT:
				selected_img_data.top = parseInt(selected_img_data.top) + parseInt(offsety);
				selected_img_data.width = parseInt(selected_img_data.width) + parseInt(offsetx);
				selected_img_data.height = parseInt(selected_img_data.height) - parseInt(offsety);
				break;
			case MIDDLE_LEFT:
				selected_img_data.left = parseInt(selected_img_data.left) + parseInt(offsetx);
				selected_img_data.width = parseInt(selected_img_data.width) - parseInt(offsetx);
				break;
			case MIDDLE_RIGHT:
				selected_img_data.width = parseInt(selected_img_data.width) + parseInt(offsetx);
				break;
			case BOTTOM_LEFT:
				selected_img_data.left = parseInt(selected_img_data.left) + parseInt(offsetx);
				selected_img_data.width = parseInt(selected_img_data.width) - parseInt(offsetx);
				selected_img_data.height = parseInt(selected_img_data.height) + parseInt(offsety);
				break;
			case BOTTOM_MIDDLE:
				selected_img_data.height = parseInt(selected_img_data.height) + parseInt(offsety);
				break;
			case BOTTOM_RIGHT:
				selected_img_data.width = parseInt(selected_img_data.width) + parseInt(offsetx);
				selected_img_data.height = parseInt(selected_img_data.height) + parseInt(offsety);
				break;
		}
	}
	if (selected_img_data.type == "image") {
		draw_background_image(selected_img_data);
	} else {
		draw_background_svg(selected_img_data);
	}
	draw_selection_shapes(selected_img_data);
}

function get_selected_img(posx, posy) {
	for (var i = 0; i < img_data.length; i++) {
		var item = img_data[img_data.length - i - 1];
		if (posx > (item.left - SHAPE_OFFSET) && posx < (item.left + item.width + SHAPE_OFFSET) && posy > (item.top - SHAPE_OFFSET) && posy < (item.top + item.height + SHAPE_OFFSET)) {
			return item;
		}
	}
	return null;
}

function get_selected_shape(posx, posy) {
	for (var i = 0; i < sel_shapes.length; i++) {
		var left = sel_shapes[i].left;
		var top = sel_shapes[i].top;
		if (posx > (left - SHAPE_OFFSET) && posx < (left + SHAPE_OFFSET) && posy > (top - SHAPE_OFFSET) && posy < (top + SHAPE_OFFSET)) {
			return i;
		}
	}
	return null;
}

function draw_selection_shapes(img_data, flag = true) {
	if (img_data == null) return;
	var left = img_data.left;
	var top = img_data.top;
	var width = img_data.width;
	var height = img_data.height;
	var right = left + width;
	var bottom = top + height;
	var middle_x = left + parseInt(width / 2);
	var middle_y = top + parseInt(height / 2);

	sel_shapes[TOP_LEFT].left = left;
	sel_shapes[TOP_LEFT].top = top;
	sel_shapes[TOP_MIDDLE].left = middle_x;
	sel_shapes[TOP_MIDDLE].top = top;
	sel_shapes[TOP_RIGHT].left = right;
	sel_shapes[TOP_RIGHT].top = top;
	sel_shapes[MIDDLE_LEFT].left = left;
	sel_shapes[MIDDLE_LEFT].top = middle_y;
	sel_shapes[MIDDLE_RIGHT].left = right;
	sel_shapes[MIDDLE_RIGHT].top = middle_y;
	sel_shapes[BOTTOM_LEFT].left = left;
	sel_shapes[BOTTOM_LEFT].top = bottom;
	sel_shapes[BOTTOM_MIDDLE].left = middle_x;
	sel_shapes[BOTTOM_MIDDLE].top = bottom;
	sel_shapes[BOTTOM_RIGHT].left = right;
	sel_shapes[BOTTOM_RIGHT].top = bottom;

	for (var i = 0; i < sel_shapes.length; i++) {
		sel_shapes[i].obj.style.left = (sel_shapes[i].left - SHAPE_OFFSET) + "px";
		sel_shapes[i].obj.style.top = (sel_shapes[i].top - SHAPE_OFFSET) + "px";
		sel_shapes[i].obj.style.display = flag ? "block" : "none";
	}
}

function hide_selection_shapes() {
	for (var i = 0; i < sel_shapes.length; i++) {
		sel_shapes[i].obj.style.display = "none";
	}
}

function uuidv4() {
	return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
		(c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
	);
}