(function ($) {


    var RoleID = jQuery("#RoleID").val();

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { RoleID: RoleID,
            action: 'GetRightListByRoleID',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                init(jsonmsg.dataList0);
            }
        }
    });


    var init = function (rights) {
        var MT = window.MT = new moveTree({
            containers: {
                unBox: $(".j-un-tree"),
                ownBox: $(".j-own-tree")
            },
            core: {
                rights: rights
            }
        });
        var util = MT.util;
        var trees = util.trees;
        $(".purviewdiv-center-b-div1-k2-right").click(function () {
            util.move(trees.unTree, trees.ownTree);
            return false;
        });
        $(".purviewdiv-center-b-div1-k2-left").click(function () {
            util.move(trees.ownTree, trees.unTree);
            return false;
        });

        $(".buttom-qd").bind("click", function () {
            if ($(this).hasClass("disabled")) return false;
            var rights = [], allRights = [], ks = [];
            var ownTree = trees.ownTree;
            ownTree.select_all();
            allRights = ownTree.get_selected();
            ownTree.deselect_all();
            for (var i = 0; i < allRights.length; i++) {
                var node = ownTree.get_node(allRights[i]);
                //if (node.children.length == 0) {
                if (node.li_attr.ks) {
                    ks.push(node.id);
                } else {
                    rights.push(node.id);
                }
                //}
            }

            SaveRoleRightRel(rights);

            //            console.log(rights);
            //            console.log(ks);
        });
    }
})(jQuery);