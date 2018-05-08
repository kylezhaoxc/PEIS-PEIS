(function ($) {
    var rightDataList;

    var permissions = $(".Permissions-preview-center-k");
    var fillList = function (pid, depth) {
        if (permissions.length <= depth) return false;

        if (rightDataList != "" && rightDataList != null && rightDataList != undefined) {
            showlist(pid, depth, rightDataList);
            return;
        }

        // 第一次从后台获取数据，然后保存到公用变量中。
        jQuery.ajax({
            type: "POST",
            url: "/Ajax/AjaxRight.aspx",
            data: { action: 'GetAllRightItemList',
                currenttime: encodeURIComponent(new Date())
            },
            cache: false,
            dataType: "json",
            success: function (jsonmsg) {
                if (jsonmsg != null && jsonmsg != "") {
                    rightDataList = jsonmsg.dataList0;
                    showlist(pid, depth, rightDataList);
                }
            }
        });
    };

    fillList(0, 0);

    var showlist = function (pid, depth, json) {

        var $ul = $("<ul>");
        for (var i = 0; i < rightDataList.length; i++) {
            var item = rightDataList[i];
            if (item.ID_ParentRight == pid) {
                var $item = $('<li>' + item.RightName + '</li>');
                $item.data("tree", { id: item.ID_Right, depth: depth + 1 });
                $ul.append($item);
            }
        }
        for (var i = 0; i < permissions.length; i++) {
            if (i > depth)
                permissions.eq(i).html("");
        }
        permissions.eq(depth).html($ul);
        $ul.find("li").bind("click", function () {
            var $t = $(this), tree = $t.data("tree");
            $t.addClass("active").siblings("li").removeClass("active");
            fillList(tree.id, tree.depth);
        });
    };

})(jQuery);