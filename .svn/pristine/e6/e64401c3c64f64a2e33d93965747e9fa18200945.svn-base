//放大编辑
(function ($) {
    $.fn.extend({
        insertAtCaret: function (myValue) {
            var $t = $(this)[0];
            if (document.selection) {
                this.focus();
                sel = document.selection.createRange();
                sel.text = myValue;
                this.focus();
            }
            else
                if ($t.selectionStart || $t.selectionStart == '0') {
                    var startPos = $t.selectionStart;
                    var endPos = $t.selectionEnd;
                    var scrollTop = $t.scrollTop;
                    $t.value = $t.value.substring(0, startPos) + myValue + $t.value.substring(endPos, $t.value.length);
                    this.focus();
                    $t.selectionStart = startPos + myValue.length;
                    $t.selectionEnd = startPos + myValue.length;
                    $t.scrollTop = scrollTop;
                }
                else {
                    this.value += myValue;
                    this.focus();
                }
        }
        });


    // 初始化 放大编辑  修改为委托的方式加载编辑器放大效果 20140423 by wtang , YLuo
    jQuery(document).delegate(".b-edit", "dblclick", function () {
        var $t = jQuery(this), $box = jQuery(".texttcc"), top = jQuery(window).scrollTop(), h = $box.height(), w = $box.width(), sw = jQuery(window).width(), sh = jQuery(window).height();
        $box.data("textarea", $t);

        jQuery(".texttcc .Editboxdiv-title .Editboxdiv-title-btke strong").html(jQuery(this).attr("boxtitle") == undefined ? "编辑框放大" : jQuery(this).attr("boxtitle"));

        $box.css({ 'top': top + ((sh - h) / 2), 'left': (sw - w) / 2 });
        jQuery("body").append('<div class="m-media">');
        jQuery(".m-media").css("height", jQuery(document).height());
        $box.show().find(".text-edit").html('<textarea>' + $t.val() + '</textarea>');
        jQuery(".texttcc .text-edit textarea").focus();  // 获取焦点  
    })
    .delegate(".text-title a", "click", function () {
        var txt = jQuery(this).html();
        jQuery(".texttcc .text-edit textarea").insertAtCaret(txt);
        return false;
    })
    .delegate(".clexit", "click", function (event) { jQuery('.texttcc,.m-media').fadeOut('slow').data("target", "") })
    .delegate(".text-save", "click", function () {
        var $text = jQuery(".texttcc").data("textarea"),
			 val = jQuery(".texttcc textarea").val();
        $text.val(val);
        jQuery('.texttcc,.m-media').fadeOut('slow').data("target", "");
    })

//    .delegate(".j-autoHeight", "click", function (event) { 
//        // 判断表格是否存在滚动条,并设置相应的样式
//        JudgeTableIsExistScroll();
//    })
    ;


})(jQuery);
