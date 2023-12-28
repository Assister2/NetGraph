﻿/**
 * Minified by jsDelivr using Terser v3.14.1.
 * Original file: /npm/cytoscape-euler@1.2.2/cytoscape-euler.js
 *
 * Do NOT use SRI with dynamically generated files! More information: https://www.jsdelivr.com/using-sri-with-dynamic-files
 */
!function (t, e) { "object" == typeof exports && "object" == typeof module ? module.exports = e() : "function" == typeof define && define.amd ? define([], e) : "object" == typeof exports ? exports.cytoscapeEuler = e() : t.cytoscapeEuler = e() }(this, function () { return function (t) { var e = {}; function n(o) { if (e[o]) return e[o].exports; var r = e[o] = { i: o, l: !1, exports: {} }; return t[o].call(r.exports, r, r.exports, n), r.l = !0, r.exports } return n.m = t, n.c = e, n.i = function (t) { return t }, n.d = function (t, e, o) { n.o(t, e) || Object.defineProperty(t, e, { configurable: !1, enumerable: !0, get: o }) }, n.n = function (t) { var e = t && t.__esModule ? function () { return t.default } : function () { return t }; return n.d(e, "a", e), e }, n.o = function (t, e) { return Object.prototype.hasOwnProperty.call(t, e) }, n.p = "", n(n.s = 11) }([function (t, e, n) { "use strict"; t.exports = null != Object.assign ? Object.assign.bind(Object) : function (t) { for (var e = arguments.length, n = Array(e > 1 ? e - 1 : 0), o = 1; o < e; o++)n[o - 1] = arguments[o]; return n.forEach(function (e) { Object.keys(e).forEach(function (n) { return t[n] = e[n] }) }), t } }, function (t, e, n) { "use strict"; var o = n(0), r = Object.freeze({ source: null, target: null, length: 80, coeff: 2e-4, weight: 1 }); t.exports = { makeSpring: function (t) { return o({}, r, t) }, applySpring: function (t) { var e = t.source, n = t.target, o = t.length < 0 ? r.length : t.length, i = n.pos.x - e.pos.x, a = n.pos.y - e.pos.y, s = Math.sqrt(i * i + a * a); 0 === s && (i = (Math.random() - .5) / 50, a = (Math.random() - .5) / 50, s = Math.sqrt(i * i + a * a)); var u = s - o, c = (!t.coeff || t.coeff < 0 ? r.springCoeff : t.coeff) * u / s * t.weight; e.force.x += c * i, e.force.y += c * a, n.force.x -= c * i, n.force.y -= c * a } } }, function (t, e, n) { "use strict"; var o = function () { function t(t, e) { for (var n = 0; n < e.length; n++) { var o = e[n]; o.enumerable = o.enumerable || !1, o.configurable = !0, "value" in o && (o.writable = !0), Object.defineProperty(t, o.key, o) } } return function (e, n, o) { return n && t(e.prototype, n), o && t(e, o), e } }(); var r = n(13), i = n(0), a = n(4), s = n(10).tick, u = n(7).makeQuadtree, c = n(3).makeBody, f = n(1).makeSpring, l = function (t) { return t.isParent() }, p = function (t) { return !l(t) }, d = function (t) { return l(t.source()) || l(t.target()) }, y = function (t) { return !d(t) }, h = function (t) { return t.scratch("euler").body }, v = function (t) { return l(t) ? t.descendants().filter(p) : t }, m = function (t) { var e = t.scratch("euler"); return e || (e = {}, t.scratch("euler", e)), e }, g = function (t, e) { return "function" == typeof t ? t(e) : t }, x = function (t) { function e(t) { return function (t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") }(this, e), function (t, e) { if (!t) throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); return !e || "object" != typeof e && "function" != typeof e ? t : e }(this, (e.__proto__ || Object.getPrototypeOf(e)).call(this, i({}, a, t))) } return function (t, e) { if ("function" != typeof e && null !== e) throw new TypeError("Super expression must either be null or a function, not " + typeof e); t.prototype = Object.create(e && e.prototype, { constructor: { value: t, enumerable: !1, writable: !0, configurable: !0 } }), e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e) }(e, r), o(e, [{ key: "prerun", value: function (t) { var e = t; e.quadtree = u(); var n = e.bodies = []; e.nodes.filter(function (t) { return p(t) }).forEach(function (t) { var o = m(t), r = c({ pos: { x: o.x, y: o.y }, mass: g(e.mass, t), locked: o.locked }); r._cyNode = t, o.body = r, r._scratch = o, n.push(r) }); var o = e.springs = []; e.edges.filter(y).forEach(function (t) { var n = f({ source: h(t.source()), target: h(t.target()), length: g(e.springLength, t), coeff: g(e.springCoeff, t) }); n._cyEdge = t; var r = m(t); n._scratch = r, r.spring = n, o.push(n) }), e.edges.filter(d).forEach(function (t) { var n = v(t.source()), r = v(t.target()); n = [n[0]], r = [r[0]], n.forEach(function (n) { r.forEach(function (r) { o.push(f({ source: h(n), target: h(r), length: g(e.springLength, t), coeff: g(e.springCoeff, t) })) }) }) }) } }, { key: "tick", value: function (t) { return s(t) <= t.movementThreshold } }]), e }(); t.exports = x }, function (t, e, n) { "use strict"; var o = Object.freeze({ pos: { x: 0, y: 0 }, prevPos: { x: 0, y: 0 }, force: { x: 0, y: 0 }, velocity: { x: 0, y: 0 }, mass: 1 }), r = function (t, e) { return { x: (n = function (t, e) { return null != t ? t : e }(t, e)).x, y: n.y }; var n }; t.exports = { makeBody: function (t) { var e = {}; return e.pos = r(t.pos, o.pos), e.prevPos = r(t.prevPos, e.pos), e.force = r(t.force, o.force), e.velocity = r(t.velocity, o.velocity), e.mass = null != t.mass ? t.mass : o.mass, e.locked = t.locked, e } } }, function (t, e, n) { "use strict"; var o = Object.freeze({ springLength: function (t) { return 80 }, springCoeff: function (t) { return 8e-4 }, mass: function (t) { return 4 }, gravity: -1.2, pull: .001, theta: .666, dragCoeff: .02, movementThreshold: 1, timeStep: 20 }); t.exports = o }, function (t, e, n) { "use strict"; var o = .02; t.exports = { applyDrag: function (t, e) { var n = void 0; n = null != e ? e : null != t.dragCoeff ? t.dragCoeff : o, t.force.x -= n * t.velocity.x, t.force.y -= n * t.velocity.y } } }, function (t, e, n) { "use strict"; t.exports = { integrate: function (t, e) { var n, o = 0, r = 0, i = 0, a = 0, s = t.length; if (0 === s) return 0; for (n = 0; n < s; ++n) { var u = t[n], c = e / u.mass; if (!u.grabbed) { u.locked ? (u.velocity.x = 0, u.velocity.y = 0) : (u.velocity.x += c * u.force.x, u.velocity.y += c * u.force.y); var f = u.velocity.x, l = u.velocity.y, p = Math.sqrt(f * f + l * l); p > 1 && (u.velocity.x = f / p, u.velocity.y = l / p), o = e * u.velocity.x, i = e * u.velocity.y, u.pos.x += o, u.pos.y += i, r += Math.abs(o), a += Math.abs(i) } } return (r * r + a * a) / s } } }, function (t, e, n) { "use strict"; var o = n(9), r = n(8), i = function (t) { t.x = 0, t.y = 0 }, a = function (t, e) { var n = Math.abs(t.x - e.x), o = Math.abs(t.y - e.y); return n < 1e-8 && o < 1e-8 }; function s(t, e) { return 0 === e ? t.quad0 : 1 === e ? t.quad1 : 2 === e ? t.quad2 : 3 === e ? t.quad3 : null } function u(t, e, n) { 0 === e ? t.quad0 = n : 1 === e ? t.quad1 = n : 2 === e ? t.quad2 = n : 3 === e && (t.quad3 = n) } t.exports = { makeQuadtree: function () { var t = [], e = new r, n = [], c = 0, f = l(); function l() { var t = n[c]; return t ? (t.quad0 = null, t.quad1 = null, t.quad2 = null, t.quad3 = null, t.body = null, t.mass = t.massX = t.massY = 0, t.left = t.right = t.top = t.bottom = 0) : (t = new o, n[c] = t), ++c, t } function p(t) { for (e.reset(), e.push(f, t); !e.isEmpty();) { var n = e.pop(), o = n.node, r = n.body; if (o.body) { var i = o.body; if (o.body = null, a(i.pos, r.pos)) { var c = 3; do { var p = Math.random(), d = (o.right - o.left) * p, y = (o.bottom - o.top) * p; i.pos.x = o.left + d, i.pos.y = o.top + y, c -= 1 } while (c > 0 && a(i.pos, r.pos)); if (0 === c && a(i.pos, r.pos)) return } e.push(o, i), e.push(o, r) } else { var h = r.pos.x, v = r.pos.y; o.mass = o.mass + r.mass, o.massX = o.massX + r.mass * h, o.massY = o.massY + r.mass * v; var m = 0, g = o.left, x = (o.right + g) / 2, b = o.top, k = (o.bottom + b) / 2; h > x && (m += 1, g = x, x = o.right), v > k && (m += 2, b = k, k = o.bottom); var q = s(o, m); q ? e.push(q, r) : ((q = l()).left = g, q.top = b, q.right = x, q.bottom = k, q.body = r, u(o, m, q)) } } } return { insertBodies: function (t) { if (0 !== t.length) { var e = Number.MAX_VALUE, n = Number.MAX_VALUE, o = Number.MIN_VALUE, r = Number.MIN_VALUE, i = void 0, a = t.length; for (i = a; i--;) { var s = t[i].pos.x, u = t[i].pos.y; s < e && (e = s), s > o && (o = s), u < n && (n = u), u > r && (r = u) } var d = o - e, y = r - n; for (d > y ? r = n + d : o = e + y, c = 0, (f = l()).left = e, f.right = o, f.top = n, f.bottom = r, (i = a - 1) >= 0 && (f.body = t[i]); i--;)p(t[i]) } }, updateBodyForce: function (e, n, o, r) { var a = t, s = void 0, u = void 0, c = void 0, l = void 0, p = 0, d = 0, y = 1, h = 0, v = 1; a[0] = f, i(e.force); var m = -e.pos.x, g = -e.pos.y, x = Math.sqrt(m * m + g * g), b = e.mass * r / x; for (p += b * m, d += b * g; y;) { var k = a[h], q = k.body; y -= 1, h += 1; var w = q !== e; q && w ? (u = q.pos.x - e.pos.x, c = q.pos.y - e.pos.y, 0 === (l = Math.sqrt(u * u + c * c)) && (u = (Math.random() - .5) / 50, c = (Math.random() - .5) / 50, l = Math.sqrt(u * u + c * c)), p += (s = n * q.mass * e.mass / (l * l * l)) * u, d += s * c) : w && (u = k.massX / k.mass - e.pos.x, c = k.massY / k.mass - e.pos.y, 0 === (l = Math.sqrt(u * u + c * c)) && (u = (Math.random() - .5) / 50, c = (Math.random() - .5) / 50, l = Math.sqrt(u * u + c * c)), (k.right - k.left) / l < o ? (p += (s = n * k.mass * e.mass / (l * l * l)) * u, d += s * c) : (k.quad0 && (a[v] = k.quad0, y += 1, v += 1), k.quad1 && (a[v] = k.quad1, y += 1, v += 1), k.quad2 && (a[v] = k.quad2, y += 1, v += 1), k.quad3 && (a[v] = k.quad3, y += 1, v += 1))) } e.force.x += p, e.force.y += d } } } } }, function (t, e, n) { "use strict"; function o() { this.stack = [], this.popIdx = 0 } function r(t, e) { this.node = t, this.body = e } t.exports = o, o.prototype = { isEmpty: function () { return 0 === this.popIdx }, push: function (t, e) { var n = this.stack[this.popIdx]; n ? (n.node = t, n.body = e) : this.stack[this.popIdx] = new r(t, e), ++this.popIdx }, pop: function () { if (this.popIdx > 0) return this.stack[--this.popIdx] }, reset: function () { this.popIdx = 0 } } }, function (t, e, n) { "use strict"; t.exports = function () { this.body = null, this.quad0 = null, this.quad1 = null, this.quad2 = null, this.quad3 = null, this.mass = 0, this.massX = 0, this.massY = 0, this.left = 0, this.top = 0, this.bottom = 0, this.right = 0 } }, function (t, e, n) { "use strict"; var o = n(6).integrate, r = n(5).applyDrag, i = n(1).applySpring; t.exports = { tick: function (t) { var e = t.bodies, n = t.springs, a = t.quadtree, s = t.timeStep, u = t.gravity, c = t.theta, f = t.dragCoeff, l = t.pull; e.forEach(function (t) { var e = t._scratch; e && (t.locked = e.locked, t.grabbed = e.grabbed, t.pos.x = e.x, t.pos.y = e.y) }), a.insertBodies(e); for (var p = 0; p < e.length; p++) { var d = e[p]; a.updateBodyForce(d, u, c, l), r(d, f) } for (var y = 0; y < n.length; y++) { var h = n[y]; i(h) } var v = o(e, s); return e.forEach(function (t) { var e = t._scratch; e && (e.x = t.pos.x, e.y = t.pos.y) }), v } } }, function (t, e, n) { "use strict"; var o = n(2), r = function (t) { t && t("layout", "euler", o) }; "undefined" != typeof cytoscape && r(cytoscape), t.exports = r }, function (t, e, n) { "use strict"; t.exports = Object.freeze({ animate: !0, refresh: 10, maxIterations: 1e3, maxSimulationTime: 4e3, ungrabifyWhileSimulating: !1, fit: !0, padding: 30, boundingBox: void 0, ready: function () { }, stop: function () { }, randomize: !1, infinite: !1 }) }, function (t, e, n) { "use strict"; var o = function () { function t(t, e) { for (var n = 0; n < e.length; n++) { var o = e[n]; o.enumerable = o.enumerable || !1, o.configurable = !0, "value" in o && (o.writable = !0), Object.defineProperty(t, o.key, o) } } return function (e, n, o) { return n && t(e.prototype, n), o && t(e, o), e } }(); var r = n(0), i = n(12), a = n(14), s = n(15), u = s.setInitialPositionState, c = s.refreshPositions, f = s.getNodePositionData, l = n(16).multitick, p = function () { function t(e) { !function (t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") }(this, t); var n = this.options = r({}, i, e), o = this.state = r({}, n, { layout: this, nodes: n.eles.nodes(), edges: n.eles.edges(), tickIndex: 0, firstUpdate: !0 }); o.animateEnd = n.animate && "end" === n.animate, o.animateContinuously = n.animate && !o.animateEnd } return o(t, [{ key: "run", value: function () { var t = this, e = this.state; if (e.tickIndex = 0, e.firstUpdate = !0, e.startTime = Date.now(), e.running = !0, e.currentBoundingBox = a(e.boundingBox, e.cy), e.ready && t.one("ready", e.ready), e.stop && t.one("stop", e.stop), e.nodes.forEach(function (t) { return u(t, e) }), t.prerun(e), e.animateContinuously) { var n = function (t) { var n, o = t.target; f(n = o, e).grabbed = n.grabbed() }, o = n, r = function (t) { var n = t.target, o = f(n, e), r = n.position(); o.x = r.x, o.y = r.y }, i = function () { e.fit && e.animateContinuously && e.cy.fit(e.padding) }, s = function () { c(e.nodes, e), i(), requestAnimationFrame(p) }, p = function () { l(e, s, d) }, d = function () { c(e.nodes, e), i(), e.nodes.forEach(function (t) { var i; i = t, e.ungrabifyWhileSimulating && f(i, e).grabbable && i.grabify(), function (t) { t.removeListener("grab", n), t.removeListener("free", o), t.removeListener("drag", r) }(t) }), e.running = !1, t.emit("layoutstop") }; t.emit("layoutstart"), e.nodes.forEach(function (t) { var i; i = t, e.ungrabifyWhileSimulating && (f(i, e).grabbable = i.grabbable()) && i.ungrabify(), function (t) { t.on("grab", n), t.on("free", o), t.on("drag", r) }(t) }), p() } else { for (var y = !1, h = function () { }, v = function () { return y = !0 }; !y;)l(e, h, v); e.eles.layoutPositions(this, e, function (t) { var n = f(t, e); return { x: n.x, y: n.y } }) } return t.postrun(e), this } }, { key: "prerun", value: function () { } }, { key: "postrun", value: function () { } }, { key: "tick", value: function () { } }, { key: "stop", value: function () { return this.state.running = !1, this } }, { key: "destroy", value: function () { return this } }]), t }(); t.exports = p }, function (t, e, n) { "use strict"; t.exports = function (t, e) { return null == (t = null == t ? { x1: 0, y1: 0, w: e.width(), h: e.height() } : { x1: t.x1, x2: t.x2, y1: t.y1, y2: t.y2, w: t.w, h: t.h }).x2 && (t.x2 = t.x1 + t.w), null == t.w && (t.w = t.x2 - t.x1), null == t.y2 && (t.y2 = t.y1 + t.h), null == t.h && (t.h = t.y2 - t.y1), t } }, function (t, e, n) { "use strict"; var o = n(0); t.exports = { setInitialPositionState: function (t, e) { var n = t.position(), r = e.currentBoundingBox, i = t.scratch(e.name); null == i && (i = {}, t.scratch(e.name, i)), o(i, e.randomize ? { x: r.x1 + Math.random() * r.w, y: r.y1 + Math.random() * r.h } : { x: n.x, y: n.y }), i.locked = t.locked() }, getNodePositionData: function (t, e) { return t.scratch(e.name) }, refreshPositions: function (t, e) { t.positions(function (t) { var n = t.scratch(e.name); return { x: n.x, y: n.y } }) } } }, function (t, e, n) { "use strict"; var o = function () { }, r = function (t) { var e = t, n = t.layout.tick(e); e.firstUpdate && (e.animateContinuously && e.layout.emit("layoutready"), e.firstUpdate = !1), e.tickIndex++; var o = Date.now() - e.startTime; return !e.infinite && (n || e.tickIndex >= e.maxIterations || o >= e.maxSimulationTime) }; t.exports = { tick: r, multitick: function (t) { for (var e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : o, n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : o, i = !1, a = t, s = 0; s < a.refresh && !(i = !a.running || r(a)); s++); i ? n() : e() } } }]) });
//# sourceMappingURL=/sm/c1e271809562639f413ee743a27ac7004023bd16917706ef6d5c83ef40069c61.map