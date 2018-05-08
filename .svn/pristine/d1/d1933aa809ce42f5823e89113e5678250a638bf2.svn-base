(function ($) {
    /**
    * 表格横向滚动插件
    * @param options
    * @returns {*}
    */
    $.fn.extend({
        TableScrollX: function () {
            var $scrollControl = $(".j-scroll-control");
            if ($scrollControl.length > 0) {
                var widthLeft = $scrollControl.width() - $scrollControl[0].clientWidth;
                if (widthLeft > 0) {
                    var $scrollTitle = $(".j-control-title");
                    $scrollTitle.css("width", $scrollTitle.width() + widthLeft);
                }
                $scrollControl.bind("scroll.j-control", function () {
                    var left = $(this).scrollLeft();
                    $(".j-control-title").css("margin-left", 0 - left);
                });
            }
        }
    });
})(jQuery);

