!function (a) {
    "function" == typeof define && define.amd ? define(["jquery"], a) : "object" == typeof exports ? module.exports = a : a(jQuery)
} (function (a) {
    function b(b) {
        var g = b || window.event, h = i.call(arguments, 1), j = 0, l = 0, m = 0, n = 0, o = 0, p = 0;
        if (b = a.event.fix(g), b.type = "mousewheel", "detail" in g && (m = -1 * g.detail), "wheelDelta" in g && (m = g.wheelDelta), "wheelDeltaY" in g && (m = g.wheelDeltaY), "wheelDeltaX" in g && (l = -1 * g.wheelDeltaX), "axis" in g && g.axis === g.HORIZONTAL_AXIS && (l = -1 * m, m = 0), j = 0 === m ? l : m, "deltaY" in g && (m = -1 * g.deltaY, j = m), "deltaX" in g && (l = g.deltaX, 0 === m && (j = -1 * l)), 0 !== m || 0 !== l) {
            if (1 === g.deltaMode) {
                var q = a.data(this, "mousewheel-line-height");
                j *= q, m *= q, l *= q
            } else if (2 === g.deltaMode) {
                var r = a.data(this, "mousewheel-page-height");
                j *= r, m *= r, l *= r
            }
            if (n = Math.max(Math.abs(m), Math.abs(l)), (!f || f > n) && (f = n, d(g, n) && (f /= 40)), d(g, n) && (j /= 40, l /= 40, m /= 40), j = Math[j >= 1 ? "floor" : "ceil"](j / f), l = Math[l >= 1 ? "floor" : "ceil"](l / f), m = Math[m >= 1 ? "floor" : "ceil"](m / f), k.settings.normalizeOffset && this.getBoundingClientRect) {
                var s = this.getBoundingClientRect();
                o = b.clientX - s.left, p = b.clientY - s.top
            }
            return b.deltaX = l, b.deltaY = m, b.deltaFactor = f, b.offsetX = o, b.offsetY = p, b.deltaMode = 0, h.unshift(b, j, l, m), e && clearTimeout(e), e = setTimeout(c, 200), (a.event.dispatch || a.event.handle).apply(this, h)
        }
    }

    function c() {
        f = null
    }

    function d(a, b) {
        return k.settings.adjustOldDeltas && "mousewheel" === a.type && b % 120 === 0
    }

    var e, f, g = ["wheel", "mousewheel", "DOMMouseScroll", "MozMousePixelScroll"], h = "onwheel" in document || document.documentMode >= 9 ? ["wheel"] : ["mousewheel", "DomMouseScroll", "MozMousePixelScroll"], i = Array.prototype.slice;
    if (a.event.fixHooks) for (var j = g.length; j; ) a.event.fixHooks[g[--j]] = a.event.mouseHooks;
    var k = a.event.special.mousewheel = { version: "3.1.11", setup: function () {
        if (this.addEventListener) for (var c = h.length; c; ) this.addEventListener(h[--c], b, !1); else this.onmousewheel = b;
        a.data(this, "mousewheel-line-height", k.getLineHeight(this)), a.data(this, "mousewheel-page-height", k.getPageHeight(this))
    }, teardown: function () {
        if (this.removeEventListener) for (var c = h.length; c; ) this.removeEventListener(h[--c], b, !1); else this.onmousewheel = null;
        a.removeData(this, "mousewheel-line-height"), a.removeData(this, "mousewheel-page-height")
    }, getLineHeight: function (b) {
        var c = a(b)["offsetParent" in a.fn ? "offsetParent" : "parent"]();
        return c.length || (c = a("body")), parseInt(c.css("fontSize"), 10)
    }, getPageHeight: function (b) {
        return a(b).height()
    }, settings: { adjustOldDeltas: !0, normalizeOffset: !0}
    };
    a.fn.extend({ mousewheel: function (a) {
        return a ? this.bind("mousewheel", a) : this.trigger("mousewheel")
    }, unmousewheel: function (a) {
        return this.unbind("mousewheel", a)
    } 
    })
});
(function ($) {
    var autoBoxs = [],
        setBoxHeight = function (h) {
            for (var i = 0; i < autoBoxs.length; i++) {
                var $a = $(autoBoxs[i]);
                $a.data("left", ~ ~$a.data("left") - h);
                $a.css({ height: $a.height() + h }); 
                if(i == (autoBoxs.length - 1) )
                {
                    setTimeout(function () {
                     // 判断表格是否存在滚动条,并设置相应的样式 
                        JudgeTableIsExistScroll();
                    },100);
                }
            }
           
        };

    $.fn.extend({
        /**
        * 收起/隐藏插件
        * @param options
        * @returns {*}
        */
        hiddenAway: function (options) {
            var opts = $.extend({
                toggleClass: 'ha-show',     //切换的class
                container: 'ha-content',    //切换的容器
                state: 0,                   //初始状态
                toggleHtml: null,          //切换的html
                joint: null,               //协同控制
                time: 300,                 //切换时间(秒)
                resize: true                //是否重置autoHeight
            }, options || {}),
                $t = $(this);
            return $.each($t, function (i) {
                var $item = $t.eq(i),
                    data = $item.data("hiddenaway") || $item.attr("data-away"),
                    ps = $.extend({}, opts);
                if (data && "string" === typeof data) {
                    try {
                        data = eval('(' + data + ')');
                    } catch (e) {
                    }
                }
                if ("object" === typeof data)
                    ps = $.extend(ps, data);
                $(ps.container).data("state", ps.state);
                $item.data("ps", ps);
                var toggleFn = function (obj, toggle) {
                    var $h = $(obj),
                        ops = $h.data("ps"),
                        $c = $(ops.container),
                        cls = ops.toggleClass,
                        time = ops.time,
                        htm, $joint;
                    cls && $h.toggleClass(cls);
                    if (toggle) {
                        var state = $c.data("state");
                        var h = $c.outerHeight();
                        if (state) {
                            ops.resize && setTimeout(function () {
                                setBoxHeight(h);
                            }, time + 200);
                            $c.fadeOut(time).data("state", 0);
                        } else {
                            h = 0 - h;
                            ops.resize && setBoxHeight(h);
                            $c.fadeIn(time).data("state", 1);
                        }//alert(h);
                    }
                    htm = ops.toggleHtml;
                    if (htm != null) {
                        ops.toggleHtml = $h.html();
                        $h.html(htm);
                    }
                    $joint = $(ops.joint);
                    if (toggle && $joint) {
                        toggleFn($joint, false);
                    }
                    $h.data("ps", ops)
                };
                $item.bind("click", function () {
                    toggleFn(this, true);
                    return false;
                });
            });
        },
        /**
        * 自适应高度
        * @param opt
        */
        autoHeight: function (options) {
            autoBoxs = [];
            var opts = $.extend({
                leftHeight: 314,
                minHeight: 350
            }, options || {}),
                $t = $(this);
            $t.each(function () {
                autoBoxs.push(this)
            });
            var setHeight = function () {
                var sh = $(window).height();
                $t.each(function (i) {
                    var $item = $t.eq(i);
                    var menuheght = jQuery("#ShowUserMenuDiv").height();
                    if(menuheght > 800){ menuheght = 0;} 
                    var $tmp = $item.data("left");
                    var $extra = $item.data("extra");
                    var $dynamic = $item.data("dynamic");
                    var $sendarea = $item.data("sendarea");
                    if($extra == undefined) $extra = 0;
                    var siblingsheight=jQuery(this).prev().height();
                    if(siblingsheight == null){ siblingsheight = 0;}
                    if(jQuery("#LoginUserMenuClassDivFrame").is(":visible") == true && siblingsheight!=null)
                    {
                        $tmp = 202 + menuheght;
                        //$tmp = 202;
                        if(siblingsheight>0)
                        {
                            $tmp=$tmp+siblingsheight;
                        }
                        if(jQuery(".tablesubtitle").height() > 0){
                            $tmp=$tmp+jQuery(".tablesubtitle").height();
                        }
                    }else if($sendarea == true){
                        $tmp = 203;
                    }
                    if($extra > 0 || $extra < 0){
                            $tmp=$tmp-$extra;
                    }
                    if(jQuery("#AutoHeightOtherArea02").length > 0 )
                    {
                        $tmp = $tmp + jQuery("#AutoHeightOtherArea02").height() - 13;
                    }
                    
                    if( ($sendarea == true || $dynamic == true) && jQuery(".other_dynamic_height").is(":visible") == true  && jQuery(".other_dynamic_height").length > 0){
                        $tmp=$tmp+jQuery(".other_dynamic_height").height() + 10 ;
                    }

                    // left = $item.data("left") || opts.leftHeight,
                    var left = $tmp || opts.leftHeight,
                        min = $item.data("min") || opts.minHeight,
                        h = sh - left;
                    if (h <= min) h = min;
                        //alert(h);

                    h = h + 30;
                    $item.css({ "height": h });
                    
                });
//                setTimeout(function(){
//                    resizeTag=false;                
//                },1200);
            };
            setHeight();
//            $(window).bind("resize.autoHeight", function (e) {
////                e.stopPropagation();
////                if(!resizeTag){
////                    resizeTag = true;
//                   // jQuery("#QueryExamListData").attr("data-left", (269 + jQuery("#ShowUserMenuDiv").height()));
//                    
//                    setHeight();
//                //}
//            });
        },
        wheelMove: function (options) {
            var options = $.extend({}, {
                items: '>li',
                prev: '.w-prev',
                next: '.w-next',
                direction: 'top',
                step: 1,            //每次移动几个元素
                min: 2,             //最少元素，低于将无效果
                start: 0,           //从第几个开始
                show: 3,            //每页显示几个
                speed: 500,         //速度
                loop: true,         //是否可循环滚动
                blank: true          //是否运行留白
            }, options),
                $t = $(this),
                perPixel,
                moving = false,
                target = {};
            target.items = $t.find(options.items);
            target.len = target.items.length;

            if (target.len < options.min) return false;
            var $v = target.items.eq(0);
            if ("top" === options.direction) {
                perPixel = $v.outerHeight();
            } else {
                perPixel = $v.outerWidth();
            }
            $t.data("index", 0);
            var moveStep = function (step) {
                if (moving) return false;
                moving = true;
                var index = $t.data("index");
                index += step;
                //判断循环设置
                if (step > 0) {
                    //向下
                    var left = target.len - index;
                    if (!options.blank && left < options.step && left > 0) {
                        index -= options.step - left;
                    }
                } else {
                    if (index < 0 && index > 0 - options.step) index = 0;
                }
                if (!options.loop && (index < 0 || index >= target.len)) {
                    moving = false;
                    return false;
                }
                var last = target.len % options.show;
                if (last == 0) last = options.show;
                if (index < 0) index = (target.len - last);
                if (index >= target.len) index = 0;
                if ("top" === options.direction) {
                    $t.animate({
                        "margin-top": (0 - index * perPixel)
                    }, options.speed);
                } else {
                    $t.animate({
                        "margin-left": (0 - index * perPixel)
                    }, options.speed);
                }
                setTimeout(function () {
                    moving = false;
                }, options.speed);
                $t.data("index", index);
            };
            options.prev && $(options.prev).bind("click", function () {
                moveStep(0 - options.step);
            });
            options.next && $(options.next).bind("click", function () {
                moveStep(options.step);
            });
            $t.bind("mousewheel.wheelmove", function (e, delta) {
                moveStep(delta < 0 ? options.step : 0 - options.step);
                return false;
            });
            target.reset = function () {
                target.items = $t.find(options.items);
                target.len = target.items.length;
            };
            return target;
        }
    });
    $(".j-hiddenAway").hiddenAway();
    $(".j-autoHeight").autoHeight();
})(jQuery);
