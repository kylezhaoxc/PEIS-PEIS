(function ($) {

    //表格内超出文字隐藏，点击显示
    String.prototype.sub = function (c) {
        var len = 0;
        var str = '';
        for (var i = 0; i < this.length; i++) {
            len++;
            var code = this.charCodeAt(i);
            if (code > 255 || code < 0)
                len++;
            str += this.substr(i, 1);
            if (len >= c)
                return str;
        }
    };
    String.prototype.len = function () {
        var len = 0;
        for (var i = 0; i < this.length; i++) {
            len++;
            var code = this.charCodeAt(i);
            if (code > 255 || code < 0)
                len++;
        }
        return len;
    };


    (function ($) {
        $.fn.subLength = function (options) {
            var opts = $.extend({
                len: 44,
                left: 8,
                sprit: ['<big>...[详细]</big>', '<big>收起</big>'],
                control: 'big'
            }, options || {}),
			$contents = $(this);
            return $.each($contents, function () {
                var $t = $(this), con = $t.html(), len,
					text = $t.text().replace(/(^\s+)|(\s+$)|(<[a-z]+[a-z0-9]*[^>]*>)/gi, '').replace(/\s{2,}/gi, '\s'),
					ps = $.extend({}, opts);
                len = text.len();
                var dataOptions = $t.data("sub") || $t.attr("data-sub");
                if (dataOptions && "string" === typeof dataOptions) {
                    dataOptions = eval('(' + dataOptions + ')');
                }
                if ("object" === typeof dataOptions)
                    ps = $.extend(dataOptions, ps);
                $t.data("con", con);
                if (con.length > ps.len + ps.left || len > ps.len + ps.left ) {
                    var c = text.sub(ps.len);
                    $t.html('<span>' + c + '</span>' + ps.sprit[0]);
                    $t.data('state', 0);
                    $t.delegate(ps.control, "click", function () {
                        var $item = $(this).parents("td"),
							state = $item.data("state"),
							con = $item.data("con");
                        if (state) {
                            var c = text.sub(ps.len);
                            $item.html('<span>' + c + '</span>' + ps.sprit[0]);
                            $item.data('state', 0);
                        } else {
                            $item.html('<span>' + con + '</span>' + ps.sprit[1]);
                            $item.data('state', 1);
                        }
                    });
                }
            });
        };
    })(jQuery);

    //分科信息查看明细弹出层
    $.fn.extend({
        ShowSectionDetail: function (options) {

            $('.btnShow').click(function (event) {
                var ShowSectionID = $(this).attr("itemid");
                var SectionInterfaceType = $(this).attr("interfacetype"); // 科室接口类型
                jQuery(".SectionDetailTitle").text($(this).attr("itemname") + " 明细"); // 

                //取消事件冒泡
                event.stopPropagation();
                var $t = $(this),
			        w = $t.width(),
			        h = $t.height(),
			        offset = $t.offset(),
			        scrollTop = $(window).scrollTop(),
			        divHeight = 600,
			        $top = $('.SectionDetailInfo');

                if ($top.data("target") == this) {
                    $top.fadeOut("slow");
                    $top.data("target", "");
                    return false;
                }

                //设置弹出层的位置
                $top.css({ top: "8%", left: "10px" });
                //按钮的toggle,如果div是可见的,点击按钮切换为隐藏的;如果是隐藏的,切换为可见的。
                //:Todo 绑定分科明细
                $top.fadeIn('slow');
                $top.data("target", this);
                InitSectionDetailInfo(ShowSectionID, SectionInterfaceType);
                return true;
            });
            //点击关闭隐藏弹出层，下面分别为滑动和淡出效果。  
            $(".close").click(function (event) { $('.SectionDetailInfo').fadeOut('slow').data("target", "") });
            $(".SectionDetailInfo").css("display", "none");

            //            $(".SectionDetailInfo").drag({ handler: '.Detailsdiv-title' });
        }
    });

    //分科对比明细弹出层 
    $.fn.extend({
        ShowSectionCompareDetail: function (options) {

            $('.btnCompareShow').click(function (event) {
                var ShowSectionID = $(this).attr("itemid");
                var SectionInterfaceType = $(this).attr("interfacetype"); // 科室接口类型
                jQuery(".SectionDetailTitle").text($(this).attr("itemname") + " 对比"); // 


                var custid01 = $("#project-center-lb1-list-lh-s-id01").attr("custid"); // 对比的第一个体检号
                var custid02 = $("#project-center-lb1-list-lh-s-id02").attr("custid"); // 对比的第二个体检号

                var custid01state = $("#project-center-lb1-list-lh-s-id01").attr("state"); // 对比的第一个体检号的状态
                var custid02state = $("#project-center-lb1-list-lh-s-id02").attr("state"); // 对比的第二个体检号的状态

                jQuery(".SectionCompareDetailInfo .project-center-lb1-list-cust01").html(custid01 + " (" + custid01.substring(6, 10) + ")" ); //  
                jQuery(".SectionCompareDetailInfo .project-center-lb1-list-cust02").html(custid02 + " (" + custid02.substring(6, 10) + ")" ); //  

                //取消事件冒泡
                event.stopPropagation();
                var $t = $(this),
			        w = $t.width(),
			        h = $t.height(),
			        offset = $t.offset(),
			        scrollTop = $(window).scrollTop(),
			        divHeight = 600,
			        $top = $('.SectionCompareDetailInfo');

                if ($top.data("target") == this) {
                    $top.fadeOut("slow");
                    $top.data("target", "");
                    return false;
                }

                //设置弹出层的位置
                $top.css({ top: "10%", left: "5%" });
                //按钮的toggle,如果div是可见的,点击按钮切换为隐藏的;如果是隐藏的,切换为可见的。
                //:Todo 绑定分科明细
                $top.fadeIn('slow');
                $top.data("target", this);

                // 科室历次对比详细信息 在总检页面重载 
                InitSectionCompareDetailInfo(ShowSectionID, SectionInterfaceType, custid01, custid01state, custid02, custid02state );
                return true;
            });
            //点击关闭隐藏弹出层，下面分别为滑动和淡出效果。  
            $(".close").click(function (event) { $('.SectionCompareDetailInfo').fadeOut('slow').data("target", "") });
            $(".SectionCompareDetailInfo").css("display", "none");

            //            $(".SectionCompareDetailInfo").drag({ handler: '.Detailsdiv-title' });
        }
    });

})(jQuery);


/// <summary>
// 科室检查详细信息 在总检页面重载 
/// </summary>
/// <param name="SectionID">科室ID</param>
/// <param name="InterfaceType">接口类型</param>
function InitSectionDetailInfo(SectionID, InterfaceType) {

}

/// <summary>
/// 科室历次对比详细信息 在总检页面重载
/// </summary>
/// <param name="SectionID">科室ID</param>
/// <param name="InterfaceType">接口类型</param>
/// <param name="CustomerID01">体检号一</param>
/// <param name="CustomerID01State">体检号一的状态</param>
/// <param name="CustomerID02">体检号二</param>
/// <param name="CustomerID01State">体检号二的状态</param>
function InitSectionCompareDetailInfo(SectionID, InterfaceType, CustomerID01, CustomerID01State, CustomerID02, CustomerID01State ) {
    
}