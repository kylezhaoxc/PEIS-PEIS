(function ($) {

    var UserID = jQuery("#RightUserID").val();

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { UserID: UserID,
            action: 'GetRoleRightListByUserID',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                InitUserRoleRight(jsonmsg.dataList0, jsonmsg.dataList1, jsonmsg.dataList2);
            }
        }
    });


    var InitUserRoleRight = function (roles, rightList, roleRight) {
        var MT = new moveTree({
            containers: {
                roleBox: $(".j-roles"),
                unBox: $(".purviewdiv-center-b-div1-k1:eq(0)"),
                ownBox: $(".purviewdiv-center-b-div1-k1:eq(1)")
            },
            core: {
                rights: rightList,
                roles: roles,
                relations: roleRight
            }
        });
        window.MT = MT;
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

        $(".j-roles input[name='chkRole']").bind("change", function () {
            var $t = $(this), checked = this.checked, val = $t.val();
            var relations = util.relations.find({ role: val });
            if (relations.length == 0 || relations[0].rights.length == 0) return;
            var rights = relations[0].rights;
            if (checked) {
                trees.unTree.select_node(rights);
                trees.ownTree.deselect_node(rights);
            } else {
                trees.ownTree.select_node(rights);
                trees.unTree.deselect_node(rights);
            }
        });

        $(".buttom-qd").bind("click", function () {
            if ($(this).hasClass("disabled")) return false;
            var rights = [], allRights = [], ks = [], roles = [];
            //获取角色
            var $roles = $('input[name="chkRole"]:checked');
            $roles.each(function () {
                roles.push(this.value);
            });
            var ownTree = trees.ownTree;
            ownTree.select_all();
            allRights = ownTree.get_selected();
            ownTree.deselect_all();
            for (var i in allRights) {
                if (!allRights.hasOwnProperty(i)) continue;
                var node = ownTree.get_node(allRights[i]);

                //if (node.children.length == 0) {
                if (node.li_attr.ks == 1) {
                    ks.push(node.id);
                } else {
                    rights.push(node.id);
                }
                //}
            }

            SaveUserRoleRightSectionItemNew(roles, rights, ks);

            //            alert(rights);
            //            alert(ks);
            //            alert(roles);


        });
    };
})(jQuery);