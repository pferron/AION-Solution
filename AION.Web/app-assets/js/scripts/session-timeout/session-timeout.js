! function (e, t) {
    "object" == typeof exports && "object" == typeof module ? module.exports = t() : "function" == typeof define && define.amd ? define([], t) : "object" == typeof exports ? exports.sessionTimeout = t() : e.sessionTimeout = t()
}(window, (function () {
    return function (e) {
        var t = {};

        function n(o) {
            if (t[o]) return t[o].exports;
            var r = t[o] = {
                i: o,
                l: !1,
                exports: {}
            };
            return e[o].call(r.exports, r, r.exports, n), r.l = !0, r.exports
        }
        return n.m = e, n.c = t, n.d = function (e, t, o) {
            n.o(e, t) || Object.defineProperty(e, t, {
                enumerable: !0,
                get: o
            })
        }, n.r = function (e) {
            "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, {
                value: "Module"
            }), Object.defineProperty(e, "__esModule", {
                value: !0
            })
        }, n.t = function (e, t) {
            if (1 & t && (e = n(e)), 8 & t) return e;
            if (4 & t && "object" == typeof e && e && e.__esModule) return e;
            var o = Object.create(null);
            if (n.r(o), Object.defineProperty(o, "default", {
                enumerable: !0,
                value: e
            }), 2 & t && "string" != typeof e)
                for (var r in e) n.d(o, r, function (t) {
                    return e[t]
                }.bind(null, r));
            return o
        }, n.n = function (e) {
            var t = e && e.__esModule ? function () {
                return e.default
            } : function () {
                return e
            };
            return n.d(t, "a", t), t
        }, n.o = function (e, t) {
            return Object.prototype.hasOwnProperty.call(e, t)
        }, n.p = "", n(n.s = 0)
    }([function (e, t, n) {
        "use strict";
        n.r(t);
        n(1);
        t.default = function (e) {
            var t, n, o = Object.assign({
                appendTimestamp: !1,
                keepAliveMethod: "POST",
                keepAliveUrl: "/base/keep-alive",
                logOutBtnText: "Log out now",
                logOutUrl: "/account/signout",
                message: "Your session is about to expire.",
                stayConnectedBtnText: "Stay connected",
                timeOutAfter: 12e5,
                timeOutUrl: "/account/signout",
                titleText: "Session Timeout",
                warnAfter: 9e5
            }, e),
                r = document.createElement("div"),
                i = document.createElement("div"),
                s = document.createElement("div"),
                a = document.createElement("div"),
                u = document.createElement("div"),
                y = document.createElement("div"),
                c = document.createElement("button"),
                l = document.createElement("button"),
                modalheader = document.createElement("h4"),
                modalbody = document.createElement("h3"),
                d = function () {
                    $("#SessionModal").modal("show"),clearTimeout(t)
                },
                f = function () {
                    window.location = o.timeOutUrl
                };
            c.addEventListener("click", (function () {
                window.location = o.logOutUrl
            })), l.addEventListener("click", (function () {
                $("#SessionModal").modal("hide");
                var e = o.appendTimestamp ? "".concat(o.keepAliveUrl, "?time=").concat(Date.now()) : o.keepAliveUrl,
                    i = new XMLHttpRequest;
                i.open(o.keepAliveMethod, e), i.send(), t = setTimeout(d, o.warnAfter), clearTimeout(n), n = setTimeout(f, o.timeOutAfter)
            })),
                $("#SessionModal").modal("hide"),
                r.classList.add("modal", "fade", "text-left"),
                r['id'] = "SessionModal",
                r['tabIndex'] = -1,
                r['role'] = "dialog",
                i.classList.add("modal-dialog"),
                a.classList.add("modal-header", "bg-cyan", "white"),
                s.classList.add("modal-content"),
                u.classList.add("modal-footer"),
                c.classList.add("btn", "btn-default"),
                l.classList.add("btn", "btn-poppy"),
                y.classList.add("modal-body"),
                modalheader.classList.add("white"),
                modalheader.innerText = o.titleText,
                modalbody.innerText = o.message,
                c.innerText = o.logOutBtnText,
                l.innerText = o.stayConnectedBtnText,
                a.appendChild(modalheader);
                y.appendChild(modalbody);
                u.appendChild(c),
                u.appendChild(l),
                s.appendChild(a),
                s.appendChild(y),
                s.appendChild(u),
                i.appendChild(s),
                r.appendChild(i),
                document.body.appendChild(r), t = setTimeout(d, o.warnAfter), n = setTimeout(f, o.timeOutAfter)
        }
    }, function (e, t, n) {
        var o = n(2);
        "string" == typeof o && (o = [
            [e.i, o, ""]
        ]);
        var r = {
            hmr: !0,
            transform: void 0,
            insertInto: void 0
        };
        n(4)(o, r);
        o.locals && (e.exports = o.locals)
    }, function (e, t, n) {
        (e.exports = n(3)(!1)).push([e.i, ".sessionTimeout {\n  position: fixed;\n  z-index: 1;\n  left: 0;\n  top: 0;\n  width: 100%;\n  height: 100%;\n  overflow: auto;\n  background-color: #D8D8D8;\n  background-color: rgba(0, 0, 0, 0.5);\n}\n\n.sessionTimeout-modal {\n  background-color: #FFFFFF;\n  margin: 10% auto;\n  padding: 0.2rem;\n  width: 30%;\n}\n\n.sessionTimeout-title {\n  font-family: Helvetica, Arial, sans-serif;\n  background-color: #DEDEDE;\n  font-weight: bold;\n  padding: .4em 1em;\n  white-space: nowrap;\n  overflow: hidden;\n  text-overflow: ellipsis;\n}\n\n.sessionTimeout-content {\n  font-size: 22px;\n  font-family: Helvetica, Arial, sans-serif;\n  text-align: center;\n  margin: 1em 0 2em 0;\n}\n\n.sessionTimeout-buttons {\n  text-align: right;\n}\n\n.sessionTimeout-btn {\n  font-size: 16px;\n  border: none;\n  padding: 0.5em 0.75em;\n  margin: 0 0.25em;\n  background-color: #6C757D;\n  color: #FFFFFF;\n  cursor: pointer;\n}\n\n.sessionTimeout-btn:hover {\n  background-color: #5A6268;\n}\n\n.sessionTimeout-btn--primary {\n  background-color: #007BFF;\n}\n\n.sessionTimeout-btn--primary:hover {\n  background-color: #0069D9;\n}\n\n.sessionTimeout--hidden {\n  display: none;\n}\n", ""])
    }, function (e, t, n) {
        "use strict";
        e.exports = function (e) {
            var t = [];
            return t.toString = function () {
                return this.map((function (t) {
                    var n = function (e, t) {
                        var n = e[1] || "",
                            o = e[3];
                        if (!o) return n;
                        if (t && "function" == typeof btoa) {
                            var r = (s = o, "/*# sourceMappingURL=data:application/json;charset=utf-8;base64," + btoa(unescape(encodeURIComponent(JSON.stringify(s)))) + " */"),
                                i = o.sources.map((function (e) {
                                    return "/*# sourceURL=" + o.sourceRoot + e + " */"
                                }));
                            return [n].concat(i).concat([r]).join("\n")
                        }
                        var s;
                        return [n].join("\n")
                    }(t, e);
                    return t[2] ? "@media " + t[2] + "{" + n + "}" : n
                })).join("")
            }, t.i = function (e, n) {
                "string" == typeof e && (e = [
                    [null, e, ""]
                ]);
                for (var o = {}, r = 0; r < this.length; r++) {
                    var i = this[r][0];
                    null != i && (o[i] = !0)
                }
                for (r = 0; r < e.length; r++) {
                    var s = e[r];
                    null != s[0] && o[s[0]] || (n && !s[2] ? s[2] = n : n && (s[2] = "(" + s[2] + ") and (" + n + ")"), t.push(s))
                }
            }, t
        }
    }, function (e, t, n) {
        var o, r, i = {},
            s = (o = function () {
                return window && document && document.all && !window.atob
            }, function () {
                return void 0 === r && (r = o.apply(this, arguments)), r
            }),
            a = function (e, t) {
                return t ? t.querySelector(e) : document.querySelector(e)
            },
            u = function (e) {
                var t = {};
                return function (e, n) {
                    if ("function" == typeof e) return e();
                    if (void 0 === t[e]) {
                        var o = a.call(this, e, n);
                        if (window.HTMLIFrameElement && o instanceof window.HTMLIFrameElement) try {
                            o = o.contentDocument.head
                        } catch (e) {
                            o = null
                        }
                        t[e] = o
                    }
                    return t[e]
                }
            }(),
            c = null,
            l = 0,
            d = [],
            f = n(5);

        function p(e, t) {
            for (var n = 0; n < e.length; n++) {
                var o = e[n],
                    r = i[o.id];
                if (r) {
                    r.refs++;
                    for (var s = 0; s < r.parts.length; s++) r.parts[s](o.parts[s]);
                    for (; s < o.parts.length; s++) r.parts.push(g(o.parts[s], t))
                } else {
                    var a = [];
                    for (s = 0; s < o.parts.length; s++) a.push(g(o.parts[s], t));
                    i[o.id] = {
                        id: o.id,
                        refs: 1,
                        parts: a
                    }
                }
            }
        }

        function m(e, t) {
            for (var n = [], o = {}, r = 0; r < e.length; r++) {
                var i = e[r],
                    s = t.base ? i[0] + t.base : i[0],
                    a = {
                        css: i[1],
                        media: i[2],
                        sourceMap: i[3]
                    };
                o[s] ? o[s].parts.push(a) : n.push(o[s] = {
                    id: s,
                    parts: [a]
                })
            }
            return n
        }

        function v(e, t) {
            var n = u(e.insertInto);
            if (!n) throw new Error("Couldn't find a style target. This probably means that the value for the 'insertInto' parameter is invalid.");
            var o = d[d.length - 1];
            if ("top" === e.insertAt) o ? o.nextSibling ? n.insertBefore(t, o.nextSibling) : n.appendChild(t) : n.insertBefore(t, n.firstChild), d.push(t);
            else if ("bottom" === e.insertAt) n.appendChild(t);
            else {
                if ("object" != typeof e.insertAt || !e.insertAt.before) throw new Error("[Style Loader]\n\n Invalid value for parameter 'insertAt' ('options.insertAt') found.\n Must be 'top', 'bottom', or Object.\n (https://github.com/webpack-contrib/style-loader#insertat)\n");
                var r = u(e.insertAt.before, n);
                n.insertBefore(t, r)
            }
        }

        function h(e) {
            if (null === e.parentNode) return !1;
            e.parentNode.removeChild(e);
            var t = d.indexOf(e);
            t >= 0 && d.splice(t, 1)
        }

        function b(e) {
            var t = document.createElement("style");
            if (void 0 === e.attrs.type && (e.attrs.type = "text/css"), void 0 === e.attrs.nonce) {
                var o = function () {
                    0;
                    return n.nc
                }();
                o && (e.attrs.nonce = o)
            }
            return y(t, e.attrs), v(e, t), t
        }

        function y(e, t) {
            Object.keys(t).forEach((function (n) {
                e.setAttribute(n, t[n])
            }))
        }

        function g(e, t) {
            var n, o, r, i;
            if (t.transform && e.css) {
                if (!(i = "function" == typeof t.transform ? t.transform(e.css) : t.transform.default(e.css))) return function () { };
                e.css = i
            }
            if (t.singleton) {
                var s = l++;
                n = c || (c = b(t)), o = x.bind(null, n, s, !1), r = x.bind(null, n, s, !0)
            } else e.sourceMap && "function" == typeof URL && "function" == typeof URL.createObjectURL && "function" == typeof URL.revokeObjectURL && "function" == typeof Blob && "function" == typeof btoa ? (n = function (e) {
                var t = document.createElement("link");
                return void 0 === e.attrs.type && (e.attrs.type = "text/css"), e.attrs.rel = "stylesheet", y(t, e.attrs), v(e, t), t
            }(t), o = L.bind(null, n, t), r = function () {
                h(n), n.href && URL.revokeObjectURL(n.href)
            }) : (n = b(t), o = O.bind(null, n), r = function () {
                h(n)
            });
            return o(e),
                function (t) {
                    if (t) {
                        if (t.css === e.css && t.media === e.media && t.sourceMap === e.sourceMap) return;
                        o(e = t)
                    } else r()
                }
        }
        e.exports = function (e, t) {
            if ("undefined" != typeof DEBUG && DEBUG && "object" != typeof document) throw new Error("The style-loader cannot be used in a non-browser environment");
            (t = t || {}).attrs = "object" == typeof t.attrs ? t.attrs : {}, t.singleton || "boolean" == typeof t.singleton || (t.singleton = s()), t.insertInto || (t.insertInto = "head"), t.insertAt || (t.insertAt = "bottom");
            var n = m(e, t);
            return p(n, t),
                function (e) {
                    for (var o = [], r = 0; r < n.length; r++) {
                        var s = n[r];
                        (a = i[s.id]).refs--, o.push(a)
                    }
                    e && p(m(e, t), t);
                    for (r = 0; r < o.length; r++) {
                        var a;
                        if (0 === (a = o[r]).refs) {
                            for (var u = 0; u < a.parts.length; u++) a.parts[u]();
                            delete i[a.id]
                        }
                    }
                }
        };
        var T, w = (T = [], function (e, t) {
            return T[e] = t, T.filter(Boolean).join("\n")
        });

        function x(e, t, n, o) {
            var r = n ? "" : o.css;
            if (e.styleSheet) e.styleSheet.cssText = w(t, r);
            else {
                var i = document.createTextNode(r),
                    s = e.childNodes;
                s[t] && e.removeChild(s[t]), s.length ? e.insertBefore(i, s[t]) : e.appendChild(i)
            }
        }

        function O(e, t) {
            var n = t.css,
                o = t.media;
            if (o && e.setAttribute("media", o), e.styleSheet) e.styleSheet.cssText = n;
            else {
                for (; e.firstChild;) e.removeChild(e.firstChild);
                e.appendChild(document.createTextNode(n))
            }
        }

        function L(e, t, n) {
            var o = n.css,
                r = n.sourceMap,
                i = void 0 === t.convertToAbsoluteUrls && r;
            (t.convertToAbsoluteUrls || i) && (o = f(o)), r && (o += "\n/*# sourceMappingURL=data:application/json;base64," + btoa(unescape(encodeURIComponent(JSON.stringify(r)))) + " */");
            var s = new Blob([o], {
                type: "text/css"
            }),
                a = e.href;
            e.href = URL.createObjectURL(s), a && URL.revokeObjectURL(a)
        }
    }, function (e, t) {
        e.exports = function (e) {
            var t = "undefined" != typeof window && window.location;
            if (!t) throw new Error("fixUrls requires window.location");
            if (!e || "string" != typeof e) return e;
            var n = t.protocol + "//" + t.host,
                o = n + t.pathname.replace(/\/[^\/]*$/, "/");
            return e.replace(/url\s*\(((?:[^)(]|\((?:[^)(]+|\([^)(]*\))*\))*)\)/gi, (function (e, t) {
                var r, i = t.trim().replace(/^"(.*)"$/, (function (e, t) {
                    return t
                })).replace(/^'(.*)'$/, (function (e, t) {
                    return t
                }));
                return /^(#|data:|http:\/\/|https:\/\/|file:\/\/\/|\s*$)/i.test(i) ? e : (r = 0 === i.indexOf("//") ? i : 0 === i.indexOf("/") ? n + i : o + i.replace(/^\.\//, ""), "url(" + JSON.stringify(r) + ")")
            }))
        }
    }]).default
}));