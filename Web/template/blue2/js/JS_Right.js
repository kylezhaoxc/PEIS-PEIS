/****************************************

1.	文件名称(File Name)： 	权限公用脚本文件
2.	功能描述(Description):  权限，角色相关页面数据处理
****************************************/



// ================================ 权限管理 ==== start ==================================================

/// <summary>
/// 读取所有的权限信息
/// </summary>
function GetAllRightItemList() {

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
                // 显示查询到的权限信息
                ShowAllRightItemList(jsonmsg);
            }
        }
    });
}
/// <summary>
/// 显示所有的权限信息(List显示方式)
/// </summary>
function ShowAllRightItemList(rightlist) {

    var tempRightListContent = "";  // 一个权限的临时内容

    // 权限显示模版
    var SingleRightItemTempleteContent = jQuery('#SingleRightItemTemplete').html();
    if (SingleRightItemTempleteContent == undefined) {return;}
    // dataList0 显示所有的权限信息
    jQuery(rightlist.dataList0).each(function (j, rightitem) {
        
        tempRightListContent = SingleRightItemTempleteContent
                            .replace(/@ID_ParentRight/gi, rightitem.ID_ParentRight)
                            .replace(/@ID_Right/gi, rightitem.ID_Right)
                            .replace(/@RightCode/gi, rightitem.RightCode)
                            .replace(/@CurrentLevel/gi, rightitem.CurrentLevel)
                            .replace(/@Is_Locked/gi, rightitem.Is_Locked)
                            .replace(/@DispOrder/gi, rightitem.DispOrder)
                            .replace(/@RightName/gi, rightitem.RightName)
                            .replace(/@ShowRightName/gi, rightitem.Is_Locked == "1" ? rightitem.RightName + "<font color=\"red\">(已锁定)</font>" : rightitem.RightName);

        if (rightitem.ID_ParentRight == "0") {
            jQuery("#RightTree").append(tempRightListContent);
        } else {
            jQuery("#SubRightFrame_" + rightitem.ID_ParentRight).append(tempRightListContent);
            jQuery("#SubRightFrame_" + rightitem.ID_ParentRight).hide();
            jQuery("#openflag_" + rightitem.ID_ParentRight).html("+");
        }
    });
}

/// <summary>
/// 显示或隐藏下级权限项
/// </summary>
function ShowHideSubRightItem(rightid) {
    if (jQuery("#SubRightFrame_" + rightid) != null && jQuery("#openflag_" + rightid).html() == "+") {
        jQuery("#SubRightFrame_" + rightid).show();
        jQuery("#openflag_" + rightid).html("-");
    } else if (jQuery("#SubRightFrame_" + rightid) != null && jQuery("#openflag_" + rightid).html() == "-") {
        jQuery("#SubRightFrame_" + rightid).hide();
        jQuery("#openflag_" + rightid).html("+");
    }
}

/// <summary>
/// 选择权限选项，并显示到右边编辑区域
/// </summary>
function SelectRightItem(rightid) {
    jQuery("#txtRightID").val(jQuery("#chkRight_" + rightid).val());
    jQuery("#txtRightName").val(jQuery("#chkRight_" + rightid).attr("rightname"));
    jQuery("#txtCurrentLevel").val(jQuery("#chkRight_" + rightid).attr("currentlevel"));
    jQuery("#txtRightCode").val(jQuery("#chkRight_" + rightid).attr("rightcode"));
    jQuery("#txtParentRightID").val(jQuery("#chkRight_" + rightid).attr("parentid"));
    jQuery("#txtDispOrder").val(jQuery("#chkRight_" + rightid).attr("disporder"));
    // 是否锁定的标记，置为选中状态
    var locked = jQuery("#chkRight_" + rightid).attr("isLocked");
    jQuery(".RightItemContentArea input[name='IsLockedRight']:radio").each(function () {
        if (jQuery(this).val() == locked) {
            jQuery(this).attr("checked", true);
        }
    });

}

/// <summary>
/// 点击删除权限节点信息
/// </summary>
function DeleteRightNodeClick() {

    var txtRightID = jQuery('#txtRightID').val();           // 当前选中的节点ID
    if (txtRightID == "") {
        ShowSystemDialog("请在左边选择需要删除的节点！");
        return;
    }

    var RightName = jQuery("#chkRight_" + txtRightID).attr("rightname");

    var tipscontent = "您正在执行删除“" + RightName + "”的操作，请确认是否继续！";
    art.dialog({
        id: 'artDialogID',
        content: tipscontent,
        button: [{
            name: '确定删除',
            title: '提示',
            lock: true,
            fixed: true,
            opacity: 0.3,
            callback: function () {
                DeleteRightNodeItem();
                return true;
            }, focus: true
        }, {
            name: '取消'
        }]
    });
}


/// <summary>
/// 删除权限节点信息
/// </summary>
function DeleteRightNodeItem() {

    var txtRightID = jQuery('#txtRightID').val();           // 当前选中的节点ID

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { txtRightID: txtRightID,
            action: 'DeleteRightNodeItem',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            var tmpstr = "";
            if (jsonmsg == "0") {
                ShowSystemDialog("操作失败，请联系技术人员！");
            } else if (jsonmsg == "1") {

                jQuery("#RightTree").html("");
                GetAllRightItemList();

                jQuery('#txtRightID').val("");          // 当前选中的节点ID（只对添加下级节点 和 修改当前阶段有效）
                jQuery('#txtCurrentLevel').val("");     // 当前节点所在层级编号
                jQuery('#txtRightName').val("");        // 权限名称 
                jQuery('#txtRightCode').val("");        // 权限代码
                jQuery('#txtDispOrder').val("");        // 分组排序
                jQuery('#txtParentRightID').val("");    // 上级权限ID
                jQuery("[name='IsLockedRight'][checked]").each(function () {
                    jQuery(this).attr("checked", false);
                });     // 取消锁定选中的值

                ShowSystemDialog("删除成功！");
            } else if (jsonmsg == "-1") {
                ShowSystemDialog("该权限正在使用，不能删除！");
            } else if (jsonmsg == "-2") {
                ShowSystemDialog("该权限还存在子权限，请先删除对应的子权限！");
            }
            else  {
                ShowSystemDialog("操作失败，请联系技术人员！");
            }
        }
    });


}


/// <summary>
/// 保存权限节点信息
/// </summary>
/// <param name="type">1：新增第一级节点 2：添加为选中项的下一个节点 3：修改当前节点值 </param>
function SaveRightNodeItem(type) {
    
    var txtRightID = jQuery('#txtRightID').val();           // 当前选中的节点ID（只对添加下级节点 和 修改当前阶段有效）
    var txtCurrentLevel = jQuery('#txtCurrentLevel').val(); // 当前节点所在层级编号
    var txtRightName = jQuery('#txtRightName').val();       // 权限名称 
    var txtRightCode = jQuery('#txtRightCode').val();       // 权限代码
    var txtDispOrder = jQuery('#txtDispOrder').val();       // 分组排序
    var txtParentRightID = jQuery('#txtParentRightID').val();       // 上级权限ID

    var IsLockedRight = jQuery('#RightItemContentArea input[name=IsLockedRight]:checked').val();     // 锁定

    if (type == 2 && txtRightID == "") {

        ShowSystemDialog("请在左边选择所属上级节点");
        return;
    }

    if (type == 3 && txtRightID == "") {
        ShowSystemDialog("请选择你要修改的节点!");
        return;
    }

    if(txtRightName == "") {
        jQuery('#txtRightName').focus();
        ShowSystemDialog("请输入权限名称!");
        return;
    }
    if(txtRightCode == "") {
        jQuery('#txtRightCode').focus();
        ShowSystemDialog("请输入权限代码!");
        return;
    }
    if(txtDispOrder == "") 
    {
        txtDispOrder = "500";
    }
    if (IsLockedRight == undefined ) {
        IsLockedRight = "0";
    }
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { type: type,
            txtRightID: txtRightID,
            txtCurrentLevel: txtCurrentLevel,
            txtParentRightID: txtParentRightID,
            txtRightName: encodeURIComponent(txtRightName),
            txtRightCode: encodeURIComponent(txtRightCode),
            txtDispOrder: encodeURIComponent(txtDispOrder),
            IsLockedRight: IsLockedRight,
            action: 'SaveRightNodeItem',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            var tmpstr = "";
            if (jsonmsg == "0") {
                ShowSystemDialog("操作失败，请联系技术人员！");
            }
            else if (jsonmsg == "-1") {
                ShowSystemDialog("该权限代码已经存在，请核对后重新输入！");
            }
            else if (jsonmsg == "-2") {
                ShowSystemDialog("传入的参数错误，请核对后在操作！");
            } else {

                jQuery("#RightTree").html("");
                GetAllRightItemList();


                jQuery('#txtRightID').val("");           // 当前选中的节点ID（只对添加下级节点 和 修改当前阶段有效）
                jQuery('#txtCurrentLevel').val(""); // 当前节点所在层级编号
                jQuery('#txtRightName').val("");       // 权限名称 
                jQuery('#txtRightCode').val("");       // 权限代码
                jQuery('#txtDispOrder').val("");       // 分组排序
                jQuery('#txtParentRightID').val("");       // 上级权限ID
                jQuery("[name='IsLockedRight'][checked]").each(function () {
                    jQuery(this).attr("checked", false);
                });     // 取消锁定选中的值

                ShowSystemDialog("操作成功！");
            }
        }
    });

}


// ================================ 权限管理 ==== end ==================================================


// ================================ 角色管理 ==== start ==================================================


/// <summary>
/// 读取所有的角色信息
/// </summary>
function GetAllRoleItemList() {
    
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { action: 'GetAllRoleItemList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的权限信息
                ShowAllRoleItemList(jsonmsg);
            }
        }
    });
}
/// <summary>
/// 显示所有的权限信息
/// </summary>
function ShowAllRoleItemList(rolelist) {

    var tempRoleListContent = "";  // 一个权限的临时内容
    
    // 角色列表头部
    var SystemRoleHeaderItemTempleteContent = jQuery('#SystemRoleHeaderItemTemplete').html();
    if (SystemRoleHeaderItemTempleteContent == undefined) { return; }
    // 角色显示模版
    var SystemRoleListTempleteContent = jQuery('#SystemRoleListItemTemplete').html();
    if (SystemRoleListTempleteContent == undefined) { return; }

    jQuery("#SystemRoleList").html(""); //   先清空列表
    jQuery("#SystemRoleList").append(SystemRoleHeaderItemTempleteContent); //显示头部

    // dataList0 显示所有的角色信息
    jQuery(rolelist.dataList0).each(function (j, roleitem) {

        tempRoleListContent = SystemRoleListTempleteContent
                        .replace(/@ID_Role/gi, roleitem.ID_Role)
                        .replace(/@Is_Locked/gi, roleitem.Is_Locked)
                        .replace(/@DispOrder/gi, roleitem.DispOrder)
                        .replace(/@Is_DefaultRole/gi, roleitem.Is_DefaultRole)
                        .replace(/@ShowLockedState/gi, roleitem.Is_Locked == "1" ? "已锁定" : "--")
                        .replace(/@ShowRoleName/gi, roleitem.Is_DefaultRole == "1" ? "<span title='系统默认角色'>" + roleitem.RoleName + "</span>" : roleitem.RoleName)
                        .replace(/@RoleName/gi, roleitem.RoleName);

        jQuery("#SystemRoleList").append(tempRoleListContent);  // 显示每列的数据


    });
}

/// <summary>
/// 角色对应的行，添加点击事件
/// </summary>
function RoleLiClick(RoleID) {
    jQuery("#radioRole_" + RoleID).attr("checked", true);
    RoleValueOnchange(RoleID);
}

/// <summary>
/// Radio值发生变化的时候
/// </summary>
function RoleValueOnchange(RoleID) {
    jQuery("#txtRoleID").val(jQuery("#radioRole_" + RoleID).val());
    jQuery("#txtRoleName").val(jQuery("#radioRole_" + RoleID).attr("rolename"));
    jQuery("#txtDispOrder").val(jQuery("#radioRole_" + RoleID).attr("disporder"));

    // 是否锁定的标记，置为选中状态
    var locked = jQuery("#radioRole_" + RoleID).attr("isLocked");

    jQuery(".RightItemContentArea input[name='IsLockedRole']:radio").each(function () {
        if (jQuery(this).val() == locked) {
            jQuery(this).attr("checked", true);
        }
    });

    GetRoleAssignedRightItemList(RoleID); // 读取该角色的初始权限值 

    try{
        // 判断是否是系统默认角色，如果是，则不允许进行修改，同时，下面操作按钮不可见。
        var isdefaultrole = jQuery("#radioRole_" + RoleID).attr("isdefaultrole");
        if (isdefaultrole == "1") {
            jQuery("#RoleOperButtomArea").hide();
        } else {
            jQuery("#RoleOperButtomArea").show();
        }
    }catch(e){
        
    }

}



/// <summary>
/// 保存权限节点信息
/// </summary>
/// <param name="type">1：添加角色 2：修改当前角色 </param>
function SaveRoleNodeItem(type) {

    var txtRoleID = jQuery("#txtRoleID").val();             // 当前角色ID
    var txtRoleName = jQuery('#txtRoleName').val();         // 角色名称 
    var txtDispOrder = jQuery('#txtDispOrder').val();       // 分组排序

    var IsLockedRole = jQuery('#RightItemContentArea input[name=IsLockedRole]:checked').val();     // 锁定

    if (type == 2 && txtRoleID == "") {

        ShowSystemDialog("请在左边选择你要修改的角色！");
        return;
    }

    if (txtRoleName == "") {
        jQuery('#txtRoleName').focus();
        ShowSystemDialog("请输入角色名称!");
        return;
    }
    if (txtDispOrder == "") {
        txtDispOrder = "500";
    }
    if (IsLockedRole == undefined ) {
        IsLockedRole = "0";
    }


    // ----------------------获取 已经选中的权限个数---------------------------------
    var currRightIDsStr = ""; //记录当前选中的权限ID字符串
    jQuery("input[name='chkRight']:checkbox").each(function () {
        // 如果是选中的话，则拼接到 currRightIDsStr 字符串中
        if (jQuery(this).attr("checked") == "checked") {
            if (currRightIDsStr == "") {
                currRightIDsStr = jQuery(this).val();
            } else {
                currRightIDsStr = currRightIDsStr + "," + jQuery(this).val();
            }
        }
    });

    if (currRightIDsStr == "") {
        ShowSystemDialog("请选择【" + txtRoleName + "】对应的权限!");
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { type: type,
            txtRoleID: txtRoleID,
            txtRoleName: encodeURIComponent(txtRoleName),
            txtDispOrder: encodeURIComponent(txtDispOrder),
            IsLockedRole: IsLockedRole,
            currRightIDsStr: currRightIDsStr,
            action: 'SaveRoleNodeItem',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            var tmpstr = "";
            if (jsonmsg == "0") {
                ShowSystemDialog("操作失败，请联系技术人员！");
            }
            else if (jsonmsg == "-1") {
                ShowSystemDialog("该角色名称已经存在，请核对后重新输入！");
            }
            else if (jsonmsg == "-2") {
                ShowSystemDialog("传入的参数错误，请核对后在操作！");
            } else {

                GetAllRoleItemList();
                ClearAllRightChecked();

                jQuery('#txtRoleID').val("");          // 当前选中的角色ID
                jQuery('#txtRoleName').val("");        // 角色名称 
                jQuery('#txtDispOrder').val("");       // 分组排序

                jQuery("[name='IsLockedRole'][checked]").each(function () {
                    jQuery(this).attr("checked", false);
                });     // 取消锁定选中的值

                ShowSystemDialog("操作成功！");
            }
        }
    });

}


/// <summary>
/// 点击删除角色信息
/// </summary>
function DeleteRoleNodeClick() {

    var txtRoleID = jQuery("#txtRoleID").val(); // 当前角色ID
    if (txtRoleID == "") {
        ShowSystemDialog("请在左边选择需要删除的角色！");
        return;
    }
    var RoleName = jQuery("#radioRole_" + txtRoleID).attr("rolename");

    var tipscontent = "您正在执行删除“" + RoleName + "”的操作，请确认是否继续！";
    art.dialog({
        id: 'artDialogID',
        content: tipscontent,
        button: [{
            name: '确定删除',
            title: '提示',
            lock: true,
            fixed: true,
            opacity: 0.3,
            callback: function () {
                DeleteRoleNodeItem();
                return true;
            }, focus: true
        }, {
            name: '取消'
        }]
    });
}

/// <summary>
/// 删除角色信息
/// </summary>
function DeleteRoleNodeItem() {

    var txtRoleID = jQuery("#txtRoleID").val(); // 当前角色ID

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { txtRoleID: txtRoleID,
            action: 'DeleteRoleNodeItem',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            var tmpstr = "";
            if (jsonmsg == "0") {
                ShowSystemDialog("操作失败，请联系技术人员！");
            }  else if (jsonmsg == "-1") {
                ShowSystemDialog("该角色正在使用，不能删除！");
            }
            else if (jsonmsg == "-2") {
                ShowSystemDialog("该角色是系统默认角色，不能删除！");
            }
            else {

                GetAllRoleItemList();

                jQuery('#txtRoleID').val("");          // 当前选中的角色ID
                jQuery('#txtRoleName').val("");        // 角色名称 
                jQuery('#txtDispOrder').val("");       // 分组排序

                jQuery("[name='IsLockedRole'][checked]").each(function () {
                    jQuery(this).attr("checked", false);
                });     // 取消锁定选中的值

                ShowSystemDialog("删除成功！");
            }
        }
    });


}


/// <summary>
/// 读取角色已经分配的权限 (读取初始值)
/// </summary>
function GetRoleAssignedRightItemList(RoleID) {

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { RoleID: RoleID,
            action: 'GetRoleAssignedRightItemList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的权限信息
                ShowRoleAssignedRightItemList(jsonmsg);
            }
        }
    });
}


/// <summary>
/// 在页面上显示角色已经分配的权限 (显示初始值)
/// </summary>
function ShowRoleAssignedRightItemList(rolerightlist) {

    ClearAllRightChecked(); // 清空所有权限的选中状态

    var LastTimeSelectedRightCount = 0; // 记录上次已经选中的权限个数

    var tmpParentID = 0;
    // dataList1 显示已经选择的权限信息
    jQuery(rolerightlist.dataList0).each(function (k, rightitem) {
        jQuery("#chkRight_" + rightitem.ID_Right).attr("checked", "checked");
        LastTimeSelectedRightCount++;
        // 展开上级元素
        tmpParentID = jQuery("#chkRight_" + rightitem.ID_Right).attr("parentid");
        while (parseInt(tmpParentID) > 0) {
            jQuery("#SubRightFrame_" + tmpParentID).show();
            jQuery("#openflag_" + tmpParentID).html("-");
            tmpParentID = jQuery("#chkRight_" + tmpParentID).attr("parentid");
        }
    });

}


// ================================ 角色管理 ==== end ==================================================




// ================================ 用户权限分配管理 ==== start ==================================================


/// <summary>
/// 角色对应的行，添加点击事件
/// </summary>
function UserRightLiClick(UserID) {
    
    jQuery("#radioUser_" + UserID).attr("checked", true);
    UserIDOnchange(UserID);
}

/// <summary>
/// Radio值发生变化的时候
/// </summary>
function UserIDOnchange(UserID) {

    jQuery("#SelectedUserName").html(jQuery("#radioUser_" + UserID).attr("username"));  //  用户名称
    jQuery("#RightUserID").val(UserID);

    // 读取用户已经分配的权限及角色
    GetUserAssignedRoleRightItemList(UserID);

}


/// <summary>
/// 读取所有的角色信息 （用户权限分配页面中）
/// </summary>
function GetUserAssignAllRoleItemList() {

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { action: 'GetAllRoleItemList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的权限信息
                ShowUserAssignAllRoleItemList(jsonmsg);
            }
        }
    });
}
/// <summary>
/// 显示所有的权限信息（用户权限分配页面中）
/// </summary>
function ShowUserAssignAllRoleItemList(rolelist) {

    var tempRoleListContent = "";  // 一个权限的临时内容

    // 角色显示模版
    var SelectRoleListItemTempleteContent = jQuery('#SelectRoleListItemTemplete').html();
    if (SelectRoleListItemTempleteContent == undefined) { return; }

    jQuery("#SelectRoleList").html(""); //   先清空列表

    // dataList0 显示所有的角色信息
    jQuery(rolelist.dataList0).each(function (j, roleitem) {

        tempRoleListContent = SelectRoleListItemTempleteContent
                        .replace(/@ID_Role/gi, roleitem.ID_Role)
                        .replace(/@ShowLockedState/gi, roleitem.Is_Locked == "1" ? "(锁)" : "")
                        .replace(/@RoleName/gi, roleitem.RoleName);

        jQuery("#SelectRoleList").append(tempRoleListContent);  // 显示每列的数据


    });
}

/// <summary>
/// 读取所有的权限信息（用户权限分配页面中）
/// </summary>
function GetUserAssignAllRightItemList() {

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
                // 显示查询到的权限信息
                ShowUserAssignAllRightItemList(jsonmsg);
            }
        }
    });
}
/// <summary>
/// 显示所有的权限信息(List显示方式)（用户权限分配页面中）
/// </summary>
function ShowUserAssignAllRightItemList(rightlist) {

    var tempRightListContent = "";  // 一个权限的临时内容

    // 权限显示模版
    var SingleRightItemTempleteContent = jQuery('#SelectSingleRightItemTemplete').html();
    if (SingleRightItemTempleteContent == undefined) { return; }

    jQuery("#RightTree").html(""); //先清空权限树
    // dataList0 显示所有的权限信息
    jQuery(rightlist.dataList0).each(function (j, rightitem) {

        tempRightListContent = SingleRightItemTempleteContent
                            .replace(/@ID_ParentRight/gi, rightitem.ID_ParentRight)
                            .replace(/@ID_Right/gi, rightitem.ID_Right)
                            .replace(/@RightCode/gi, rightitem.RightCode)
                            .replace(/@CurrentLevel/gi, rightitem.CurrentLevel)
                            .replace(/@Is_Locked/gi, rightitem.Is_Locked)
                            .replace(/@DispOrder/gi, rightitem.DispOrder)
                            .replace(/@RightName/gi, rightitem.RightName)
                            .replace(/@ShowRightName/gi, rightitem.Is_Locked == "1" ? rightitem.RightName + "<font color=\"red\">(已锁定)</font>" : rightitem.RightName);

        if (rightitem.ID_ParentRight == "0") {
            jQuery("#RightTree").append(tempRightListContent);
        } else {
            jQuery("#SubRightFrame_" + rightitem.ID_ParentRight).append(tempRightListContent);
            jQuery("#SubRightFrame_" + rightitem.ID_ParentRight).hide();
            jQuery("#openflag_" + rightitem.ID_ParentRight).html("+");
        }
    });
}


/// <summary>
/// 读取用户已经分配的权限及角色 (读取初始值)
/// </summary>
function GetUserAssignedRoleRightItemList(UserID) {
    
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { UserID: UserID,
            action: 'GetUserAssignedRoleRightItemList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的权限信息
                ShowUserAssignedRoleRightItemList(jsonmsg);
            }
        }
    });
}


/// <summary>
/// 在页面上显示用户已经分配的权限及角色 (显示初始值)
/// </summary>
function ShowUserAssignedRoleRightItemList(rolerightlist) {

    ClearAllRoleChecked();  // 清空所有角色的选中状态
    ClearAllRightChecked(); // 清空所有权限的选中状态
    ClearCheckedSectionItemList(); //清空所选科室数据(清空科室)

    ShowHideQuickQueryTable(false); // 隐藏科室搜索结果
    // 取消科室搜索结果的选中效果
    jQuery("input[name='chkSectionQueryList']:checkbox").each(function () {
        jQuery(this).removeAttr("checked");
    });
    jQuery("#chkSelectAllUserSection").removeAttr("checked");


    var LastTimeSelectedRoleCount = 0;  // 记录上次已经选中的角色个数
    var LastTimeSelectedRightCount = 0; // 记录上次已经选中的权限个数

    // dataList0 显示已经选择的角色信息
    jQuery(rolerightlist.dataList0).each(function (j, roleitem) {
        jQuery("#selRole_" + roleitem.ID_Role).attr("checked", "checked");
        LastTimeSelectedRoleCount ++;
    });

    var tmpParentID = 0;
    // dataList1 显示已经选择的权限信息
    jQuery(rolerightlist.dataList1).each(function (k, rightitem) {
        jQuery("#chkRight_" + rightitem.ID_Right).attr("checked", "checked");
        LastTimeSelectedRightCount ++;
        // 展开上级元素
        tmpParentID = jQuery("#chkRight_" + rightitem.ID_Right).attr("parentid");
        while (parseInt(tmpParentID) > 0) {
            jQuery("#SubRightFrame_" + tmpParentID).show();
            jQuery("#openflag_" + tmpParentID).html("-");
            tmpParentID = jQuery("#chkRight_" + tmpParentID).attr("parentid");
        }
    });

    var selectedItemContent = "";
    var UserSelectedSectionItemDataTempleteContent = jQuery('#UserSelectedSectionItemDataTemplete').html(); //用户已经选择的科室列表模版
    if (UserSelectedSectionItemDataTempleteContent == undefined) { return; }             

    // dataList1 显示已经选择的科室信息
    jQuery(rolerightlist.dataList2).each(function (m, sectionitem) {
        selectedItemContent = UserSelectedSectionItemDataTempleteContent
                            .replace(/@chkUserSection/gi, "chkUserSection")
                            .replace(/@ID_Section/gi, sectionitem.ID_Section )
                            .replace(/@SectionName/gi, sectionitem.SectionName )
                            ;
        jQuery("#SectionItemList").append(selectedItemContent);
    });


    jQuery("#LastTimeSelectedRoleCount").val(LastTimeSelectedRoleCount);   // 记录上次已经选中的角色个数
    jQuery("#LastTimeSelectedRightCount").val(LastTimeSelectedRightCount); // 记录上次已经选中的权限个数

}

/// <summary>
/// 清空所有角色的选中状态 (清空角色)
/// </summary>
function ClearAllRoleChecked() {
    jQuery("input[name='SelectUserRole']:checkbox").each(function () {
            jQuery(this).attr("checked", false);
    });
}

/// <summary>
/// 清空所有权限的选中状态 (清空权限)
/// </summary>
function ClearAllRightChecked() {
    var tmpRightID = 0;
    var tmpOpenFlag = "";
    jQuery("input[name='chkRight']:checkbox").each(function () {
        jQuery(this).attr("checked", false);
        // 如果子项目是展开的，则将其折叠起来。
        tmpRightID = jQuery(this).val();
        tmpOpenFlag = jQuery("#openflag_" + tmpRightID ).html();
        if (tmpOpenFlag == "-") {
            jQuery("#SubRightFrame_" + tmpRightID).hide();
            jQuery("#openflag_" + tmpRightID).html("+");
        }

    });
}


/// <summary>
/// 清空所选科室数据 (清空科室)
/// </summary>
function ClearCheckedSectionItemList() {

    jQuery("#SectionItemList").html(jQuery("#UserSelectedSectionItemHeaderTemplete").html()); 

}


/// <summary>
/// 保存用户与角色，权限，科室的关系( 点击保存按钮)
/// </summary>
function SaveUserRoleRightSectionClick() {
    
    var currUserID = jQuery("#RightUserID").val();                  // 当前选中的用户ID
    var currSelectedUserName = jQuery("#SelectedUserName").html();  // 当前选中的用户名

    var LastTimeSelectedRoleCount = jQuery("#LastTimeSelectedRoleCount").val();   // 记录上次已经选中的角色个数
    var LastTimeSelectedRightCount = jQuery("#LastTimeSelectedRightCount").val(); // 记录上次已经选中的权限个数

    if (currUserID == "") {
        // 判断是否选择了用户
        ShowSystemDialog("您还未选择用户，请在左边选择需要设置权限的用户！");
        return;
    }

    // ----------------------获取 上次已经选中的角色个数---------------------------------
    var currRoleIDsStr = ""; //记录当前选中的角色ID字符串
    jQuery("input[name='SelectUserRole']:checkbox").each(function () {
        // 如果是选中的话，则拼接到 currRoleIDsStr 字符串中
        if (jQuery(this).attr("checked") == "checked") {
            if (currRoleIDsStr == "") {
                currRoleIDsStr = jQuery(this).val();
            } else {
                currRoleIDsStr = currRoleIDsStr +","+ jQuery(this).val();
            }
        }
    });

    // ----------------------获取 上次已经选中的权限个数---------------------------------
    var currRightIDsStr = ""; //记录当前选中的权限ID字符串
    jQuery("input[name='chkRight']:checkbox").each(function () {
        // 如果是选中的话，则拼接到 currRightIDsStr 字符串中
        if (jQuery(this).attr("checked") == "checked") {
            if (currRightIDsStr == "") {
                currRightIDsStr = jQuery(this).val();
            } else {
                currRightIDsStr = currRightIDsStr + "," + jQuery(this).val();
            }
        }
    });

    // ----------------------获取 当前选择的科室ID---------------------------------
    var currSectionIDsStr = ""; //记录当前选中的科室ID字符串
    jQuery("input[name='chkUserSection']:checkbox").each(function () {
        // 如果是选中的话，则拼接到 currSectionIDsStr 字符串中
        //if (jQuery(this).attr("checked") == "checked") {
            if (currSectionIDsStr == "") {
                currSectionIDsStr = jQuery(this).val();
            } else {
                currSectionIDsStr = currSectionIDsStr + "," + jQuery(this).val();
            }
        //}
    });

    if (LastTimeSelectedRoleCount == 0 && currRoleIDsStr == "") {
        if (LastTimeSelectedRightCount == 0 && currRightIDsStr == "") {
            // 判断是否进行了修改 (这里只判断保存前后是否都没有选择的情况)
            ShowSystemDialog("您还未选择该用户对应的角色或权限，请选择后再操作！");
            return;
        }
    }


    // 系统确认提示
    var tipscontent = "您正在修改“" + currSelectedUserName + "”的权限，请确认是否继续！";
    art.dialog({
        id: 'testID',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        button: [{
            name: '确定保存',
            title: '提示',
            callback: function () {
                SaveUserRoleRightSectionItem(currUserID,
                                LastTimeSelectedRoleCount,
                                LastTimeSelectedRightCount,
                                currRoleIDsStr,
                                currRightIDsStr,
                                currSectionIDsStr);
                return true;
            }, focus: true
        }, {
            name: '取消'
        }]
    });

}    

/// <summary>
/// 保存用户与角色，权限，科室的关系( 点击保存按钮)
/// </summary>
function SaveUserRoleRightSectionItemNew(currRoleIDsStr, currRightIDsStr, currSectionIDsStr) {
    
    var currUserID = jQuery("#RightUserID").val();            // 当前选中的用户ID
    var currSelectedUserName = jQuery("#RightUserName").val();
    // 系统确认提示
    var tipscontent = "您正在修改【" + currSelectedUserName + "】的权限，请确认是否继续！";
    parent.art.dialog({
        id: 'testID',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        button: [{
            name: '确定保存',
            title: '提示',
            callback: function () {
                
                SaveUserRoleRightSectionItem(currUserID, 0, 0,
                                currRoleIDsStr.join(","),
                                currRightIDsStr.join(","),
                                currSectionIDsStr.join(","));
                return true;
            }, focus: true
        }, {
            name: '取消'
        }]
    });

}


/// <summary>
/// 保存用户与角色，权限，科室的关系( 保存数据 )
/// </summary>
function SaveUserRoleRightSectionItem( currUserID,
                                LastTimeSelectedRoleCount,
                                LastTimeSelectedRightCount,
                                currRoleIDsStr,
                                currRightIDsStr,
                                currSectionIDsStr) {
    
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { UserID: currUserID,
            LastTimeSelectedRoleCount: LastTimeSelectedRoleCount,
            LastTimeSelectedRightCount: LastTimeSelectedRightCount,
            currRoleIDsStr: currRoleIDsStr,
            currRightIDsStr: currRightIDsStr,
            currSectionIDsStr: currSectionIDsStr,
            action: 'SaveUserRoleRightSectionItem',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                try {
                    if (parseInt(jsonmsg) > 0) {
                        parent.ShowSystemDialog("保存权限信息成功!");
                    }
                    else {
                        parent.ShowSystemDialog("保存权限信息失败，请与技术人员联系!");
                    }
                }
                catch (e) {
                    parent.ShowSystemDialog("保存权限信息失败，请与技术人员联系.");
                }
            }
        }
    });

}


/// <summary>
/// 隐藏，显示快速查询科室列表
/// </summary>
function ShowHideQuickQueryTable(flag, InputCode) {
//InputCode != "" && 
    if (flag == true) {
        jQuery("#QuickQueryTable").show();
    } else {
        jQuery("#QuickQueryTable").hide();
    }
}


var gAllSectionDataList = "";    // 保存全部的科室列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldSectionInputCode = "###￥￥￥";              // 记录上次输入的输入码
var gAllSectionListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询科室（通过Ajax后台过滤）
/// </summary>
function QuickQueryTableData_Ajax() {

    var InputCode = jQuery('#txtSectionInputCode').val();
    if (OldSectionInputCode != InputCode) {
        OldSectionInputCode = InputCode;
    } else {
        ShowHideQuickQueryTable(true, InputCode);
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxSection.aspx",
        data: { InputCode: InputCode,
            action: 'GetQuickSectionList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的科室
            ShowQuickQueryTableData_Ajax(jsonmsg, InputCode);
        }
    });
    
}


/// <summary>
/// 根据查询结果数据，显示科室（通过Ajax过滤）
/// </summary>
function ShowQuickQueryTableData_Ajax(sectionlist) {

    if ( sectionlist == "" || sectionlist.totalCount == 0) {

        ShowHideQuickQueryTable(true, jQuery('#txtSectionInputCode').val());
        // 显示没有找到科室信息
        jQuery("#QuickQuerySectionTableData").html(jQuery('#EmptySectionQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //科室table内容

        var SectionQuickQueryTableTempleteContent = jQuery('#SectionQuickQueryTableTemplete').html();             //快速查询科室列表模版
        if (SectionQuickQueryTableTempleteContent == undefined) { return; }             
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (sectionlist != "") {
            jQuery(sectionlist.dataList).each(function (j, sectionitem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += SectionQuickQueryTableTempleteContent
                            .replace(/@ID_Section/gi, sectionitem.ID_Section)
                            .replace(/@SectionName/gi, sectionitem.SectionName)
                            .replace(/@InputCode/gi, sectionitem.InputCode)
                            .replace(/@chkSectionQueryList/gi, "chkSectionQueryList")
                            ;
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQueryTable(true, jQuery('#txtSectionInputCode').val());
            jQuery("#QuickQuerySectionTableData").html(conclusionContent);
        } else {
            ShowHideQuickQueryTable(false);
            jQuery("#QuickQuerySectionTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择科室）
/// </summary>
function SelectSectionDataList() {
    var selectedItemContent = "";
    var UserSelectedSectionItemDataTempleteContent = jQuery('#UserSelectedSectionItemDataTemplete').html();             //用户已经选择的科室列表模版
    if (UserSelectedSectionItemDataTempleteContent == undefined) { return; } 

    jQuery("input[name='chkSectionQueryList']:checkbox:checked").each(function () {
        if (jQuery("#chkUserSection_" + jQuery(this).val()).val() == undefined) {
            selectedItemContent = UserSelectedSectionItemDataTempleteContent
                            .replace(/@chkUserSection/gi, "chkUserSection")
                            .replace(/@ID_Section/gi, jQuery(this).val())
                            .replace(/@SectionName/gi, jQuery(this).attr("sectionname"))
                            ;
            jQuery("#SectionItemList").append(selectedItemContent);
        }
    });

    ShowHideQuickQueryTable(false);
}


/// <summary>
/// 删除选择的科室
/// </summary>
function delSelectedSection(SectionID) {
    
    var currUserID = jQuery("#RightUserID").val();                  // 当前选中的用户ID
    if (currUserID != "0") {
        var tipscontent = "您正在执行删除“" + jQuery("#SelectedUserName").html() + "”与“" + jQuery("#chkUserSection_" + SectionID).attr("sectionname") + "”的关联，请确认是否继续！";
        art.dialog({
            id: 'testID',
            content: tipscontent,
            lock: true,
            fixed: true,
            opacity: 0.3,
            button: [{
                name: '确定删除',
                title: '提示',
                callback: function () {
                    jQuery("#liSectionItem_" + SectionID).remove();
                    return true;
                }, focus: true
            }, {
                name: '取消'
            }]
        });
    } else {
        jQuery("#liSectionItem_" + SectionID).remove();
    }

}

/// <summary>
/// 删除选择的科室
/// </summary>
function AllUserSectionSelect(obj) {

    if (jQuery(obj).attr("checked") == "checked") {

        jQuery("input[name='chkSectionQueryList']:checkbox").each(function () {
            jQuery(this).attr("checked", "checked");
        });
    }
    else { 
        
        jQuery("input[name='chkSectionQueryList']:checkbox").each(function () {
            jQuery(this).removeAttr("checked");
        });
    }
}


// ================================ 用户权限分配管理 ==== end ==================================================




// ================================ 角色管理 ==== start ==================================================

/// <summary> 
/// 点击选中对应角色的单选按钮
/// </summary>
function SetRoleChecked(ID_Role) {
    jQuery("#rdiRole_" + ID_Role).attr("checked", true);
}

/// <summary>
/// 弹出角色设置页面
/// </summary>
function OpenEditRoleWindow() {

    var RoleID = jQuery("input[name='RoleRadio']:checked").val();
    if (RoleID == undefined) {
        ShowSystemDialog("请选择你要修改的角色！");
        return;
    }
    OpenRoleOperWindowParams(RoleID);
}

/// <summary>
/// 弹出新增角色页面
/// </summary>
function OpenRoleOperWindow() {
    OpenRoleOperWindowParams("");
}

/// <summary>
/// 弹出角色设置页面(角色ID编号)
/// </summary>
function OpenRoleOperWindowParams(RoleID) {

    var url = '/System/right/RoleOper.aspx?num=' + Math.random();
    if (RoleID != "") {
        url = url + "&RoleID=" + RoleID;
    }
    art.dialog.open(url,
    {
        width: 320,
        height: 223,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "角色维护"

    });
}


/// <summary>
/// 保存角色参数。 (保存)
/// </summary>
function SaveRoleInfo() {
    // 获取保存时提交的参数。 (参数提取)
    var RoleParams = GetSaveRoleParams();
    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: RoleParams,         // 角色参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {
                
                if (jQuery("#ID_Role").val() != "" && jQuery("#ID_Role").val() != "0") {
                    parent.GetSingleRoleInfo(jQuery("#ID_Role").val(), "edit");
                }
                else 
                {
                    parent.GetSingleRoleInfo(jsonmsg, "add");
                }
                parent.ShowSystemDialogCloseDialog("保存角色成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据
                }
            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialogCloseDialog("角色名称：【" + jQuery("#txtRoleName").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialogCloseDialog("保存角色失败，请与技术人员联系!") }
        }
    });

}

/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveRoleParams() {

    var ID_Role = jQuery("#ID_Role").val();                     // 角色ID
    var RoleName = jQuery("#txtRoleName").val();                // 角色名称
    RoleName = encodeURIComponent(RoleName);                    // 角色名称 编码处理

    var DispOrder = jQuery("#txtDispOrder").val();                        // 排序编号
    var Is_Default = jQuery("input[name='radioIs_Default']:checked").val();   // 是否默认

    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (RoleName == "") {
        jQuery('#txtRoleName').focus();
        parent.ShowSystemDialogCloseDialog("请输入角色名称!");
        return;
    }

    var Remark = jQuery("#txtNote").val();                              // 角色说明
    Remark = encodeURIComponent(Remark);                                // 角色说明 编码处理

    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var RoleSaveParams = {
        RoleName: RoleName,
        ID_Role: ID_Role,
        Is_Default: Is_Default,
        Remark: Remark,
        DispOrder: DispOrder,
        action: 'SaveRoleInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return RoleSaveParams; // 返回拼接后的参数

}


/// <summary>
/// 初始化角色设置页面
/// </summary>
function InitRoleEditData() {
    if (jQuery("#ID_Role").val() == "") {
        return;
    }
    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

    // 默认值
    jQuery("input[name='radioIs_Default']").each(function () {

        var tmpvalue = jQuery('#txtIs_Default').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

}


/// <summary>
/// 弹出角色对应权限分配页面(角色ID编号)
/// </summary>
function OpenRoleRightRelWindowParams() {


    var RoleID = jQuery("input[name='RoleRadio']:checked").val();
    if (RoleID == undefined) {
        parent.ShowSystemDialog("请选择需要配置的角色！");
        return;
    }
    
    var RoleName = jQuery("#rdiRole_" + RoleID).attr("Rolename");
    var url = '/System/right/RoleRightRel.aspx?num=' + Math.random();
    if (RoleID != "") {
        url = url + "&RoleID=" + RoleID + "&RoleName=" + encodeURIComponent(RoleName);
    }
    art.dialog.open(url,
    {
        width: 652,
        height: 450,
        lock: true,
        id: 'OperWindowFrame',
        title: "【" + RoleName + "】角色配置",
        init: function () {

        },
        close: function () {

        }
    });
}


// ================================ 角色管理 ==== end ==================================================


/// <summary>
/// 保存角色与权限的对应关系
/// </summary>
function SaveRoleRightRel(RightArray) {

    var txtRoleID = jQuery("#RoleID").val();            // 当前选中的角色ID
    var currRightIDsStr = RightArray.join(",");

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { txtRoleID: txtRoleID,
            currRightIDsStr: currRightIDsStr,
            action: 'SaveRoleRightRel', // SaveRoleNodeItem 
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            var tmpstr = "";
            if (jsonmsg == "0") {
                parent.ShowSystemDialog("操作失败，请联系技术人员！");
            }
            else if (jsonmsg == "-1") {
                parent.ShowSystemDialog("该角色名称已经存在，请核对后重新输入！");
            }
            else if (jsonmsg == "-2") {
                parent.ShowSystemDialog("传入的参数错误，请核对后在操作！");
            } else {

                parent.ShowSystemDialog("操作成功！");
            }
        }
    });

}

