var IsShowProcessBar = true;
//分页配置参数
var pagePagination = {
    prev_text: '上一页',
    next_text: '下一页',
    totalnumber_prev_text: "共 ",
    totalnumber_next_text: " 条记录",
    items_per_page: 20,
    num_display_entries: 10,
    num_edge_entries: 2
}

function getOptionsFromForm() {
    var opt = { callback: pageselectCallback };
    opt.prev_text = pagePagination.prev_text;
    opt.next_text = pagePagination.next_text;
    opt.totalnumber_prev_text = pagePagination.totalnumber_prev_text;
    opt.totalnumber_next_text = pagePagination.totalnumber_next_text;
    opt.items_per_page = pagePagination.items_per_page;
    opt.num_display_entries = pagePagination.num_display_entries;
    opt.num_edge_entries = pagePagination.num_edge_entries;
    return opt;
}
//分页配置参数
var pagePagination04 = {
    prev_text: '上一页',
    next_text: '下一页',
    totalnumber_prev_text: "共 ",
    totalnumber_next_text: " 条记录",
    items_per_page: 30,
    num_display_entries: 4,
    num_edge_entries: 1
}
//报告打印分页配置参数
var pagePagination05 = {
    prev_text: '上一页',
    next_text: '下一页',
    totalnumber_prev_text: "共 ",
    totalnumber_next_text: " 条记录",
    items_per_page: 25,
    num_display_entries: 4,
    num_edge_entries: 1
}
function getOptionsFromForm05() {
    var opt = { callback: pageselectCallback };
    opt.prev_text = pagePagination05.prev_text;
    opt.next_text = pagePagination05.next_text;
    opt.totalnumber_prev_text = pagePagination05.totalnumber_prev_text;
    opt.totalnumber_next_text = pagePagination05.totalnumber_next_text;
    opt.items_per_page = pagePagination05.items_per_page;
    opt.num_display_entries = pagePagination05.num_display_entries;
    opt.num_edge_entries = pagePagination05.num_edge_entries;
    return opt;
}

function getOptionsFromForm04() {
    var opt = { callback: pageselectCallback };
    opt.prev_text = pagePagination04.prev_text;
    opt.next_text = pagePagination04.next_text;
    opt.totalnumber_prev_text = pagePagination04.totalnumber_prev_text;
    opt.totalnumber_next_text = pagePagination04.totalnumber_next_text;
    opt.items_per_page = pagePagination04.items_per_page;
    opt.num_display_entries = pagePagination04.num_display_entries;
    opt.num_edge_entries = pagePagination04.num_edge_entries;
    return opt;
}
var regexEnum =
{
    intege: "^-?[1-9]\\d*$", 				//整数
    intege1: "^[1-9]\\d*$", 				//正整数
    intege2: "^-[1-9]\\d*$", 				//负整数
    num: "^([+-]?)\\d*\\.?\\d+$", 		//数字
    num1: "^[1-9]\\d*|0$", 				//正数（正整数 + 0）
    num2: "^-[1-9]\\d*|0$", 				//负数（负整数 + 0）
    decmal: "^([+-]?)\\d*\\.\\d+$", 		//浮点数
    decmal1: "^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*$", //正浮点数
    decmal2: "^-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*)$", //负浮点数
    decmal3: "^-?([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0)$", //浮点数
    decmal4: "^[1-9]\\d*|^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0$", //非负浮点数（正浮点数 + 0）
    decmal5: "^(-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*))|0?.0+|0$", //非正浮点数（负浮点数 + 0）
    email: "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$", //邮件
    color: "^[a-fA-F0-9]{6}$", 			//颜色
    url: "^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&=]*)?$", //url
    chinese: "^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$", 				//仅中文
    ascii: "^[\\x00-\\xFF]+$", 			//仅ACSII字符
    zipcode: "^\\d{6}$", 					//邮编
    mobile: "^13[0-9]{9}|15[012356789][0-9]{8}|18[0256789][0-9]{8}|147[0-9]{8}$", 			//手机
    ip4: "^(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)$", //ip地址
    notempty: "^\\S+$", 					//非空
    picture: "(.*)\\.(jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$", //图片
    rar: "(.*)\\.(rar|zip|7zip|tgz)$", 							//压缩文件
    date: "^\\d{4}(\\-|\\/|\.)\\d{1,2}\\1\\d{1,2}$", 				//日期
    qq: "^[1-9]*[1-9][0-9]*$", 			//QQ号码
    tel: "^(([0\\+]\\d{2,3}-)?(0\\d{2,3})-)?(\\d{7,8})(-(\\d{3,}))?$", //电话号码的函数(包括验证国内区号,国际区号,分机号)
    phone: "/^0?1(3|5|8|)\d{9}$/",
    username: "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+|13[0-9]{9}|15[012356789][0-9]{8}|18[0256789][0-9]{8}|147[0-9]{8}|^[a-zA-Z][a-zA-Z0-9_]*$", //邮件 手机号码 字母开始的字母+数字的组合
    email: "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$", //邮件 手机号码 
    letter: "^[A-Za-z]+$", 				//字母
    letter_u: "^[A-Z]+$", 				//大写字母
    letter_l: "^[a-z]+$", 				//小写字母
    idcard: "^[1-9]([0-9]{14}|[0-9]{17})$"	//身份证
}
function checkAll(obj) {
    jQuery("[name='ItemCheckbox']").each(function () {
        jQuery(this).attr('checked', obj.checked);
    })
}

//验证是否为Email
function IsEmail(email) {


}
//判断是否为电话号码：包含手机和座机号码
function IsPhone(phoneNum) {

}
//判断是否是正整数 xmhuang 2013-10-23
function IsNum(s) {
    var patrn = regexEnum.intege1;
    if (!s.match(patrn)) {
        return false;
    }
    return true;
}

function isMobil(s) {
    var patrn = /^0?1(3|5|8|)\d{9}$/;
    if (!patrn.test(s)) {
        return false;
    }
    return true;
}
function isEmail(str) {
    //   var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
    //   var r = str.match(reg);
    var patrn = regexEnum.email;
    if (!str.match(patrn)) {
        return false;
    }
    return true;

}
/// <summary>
/// 判断是否是体检号 （全数字 + 14位）
/// </summary>
function isCustomerExamNo(s) {
    var patrn = regexEnum.intege1;
    if (!s.match(patrn)) {
        return false;
    }
    if (s.length != 14) {
        return false;
    }
    return true;
}

/// <summary>
/// 判断是否是日期格式
/// 修改人：xmhuang 2013-12-12 
/// 描述:此方法验证正确日期包含格式和实际日期
/// </summary>
function isDate(str) {
    var date = str;
    var result = date.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
    if (result == null) {
        return false;
    }
    var d = new Date(result[1], result[3] - 1, result[4]);
    return (d.getFullYear() == result[1] && (d.getMonth() + 1) == result[3] && d.getDate() == result[4]);

}
/// <summary>
/// 判断是否是身份证号码
/// </summary>
function isIDCardNo(s) {
    if (s.length == 15 || s.length == 18) {
        return true;
    }
    return false;
}
function IsIP(str) {
    var patrn = regexEnum.ip4;
    if (!str.match(patrn)) {
        return false;
    }
    return true;
}
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    else {
        return "";
    }
}
//打印预览
function DoPrintView(containerId) {
    var html = document.getElementById(containerId).innerHTML;
    window.document.body.innerHTML = html;
    window.print();
}
//function DoPrint(containerId) {
//    var html = document.getElementById(containerId).innerHTML;
//    window.document.body.innerHTML = html;
//    window.print();
//}
/*************通用弹出窗**********************/
/// <summary>
/// url:页面地址
///whichText：弹出窗口的显示标题
///whichForm
///
///
/// </summary>
function ShowDialogX(url, whichText, width, height, optValidata) {
    var modalForm = showModalDialog(url, window, "dialogTitle:" + whichText + ";dialogWidth:" + width + "px;dialogHeight:" + height + "px;help:no;scroll:on;status:off;resizable:on;");
    return modalForm;
}

/**************大小写转换通用 Begin*********************/

function regInput(reg) {
    var srcElem = event.srcElement
    var oSel = document.selection.createRange()
    oSel = oSel.duplicate()

    oSel.text = ""
    var srcRange = srcElem.createTextRange()
    oSel.setEndPoint("StartToStart", srcRange)
    var num = oSel.text + String.fromCharCode(event.keyCode) + srcRange.text.substr(oSel.text.length)
    event.returnvalue = reg.test(num)
}
function chineseNumber(num) {
    if (isNaN(num) || num > Math.pow(10, 12)) return ""
    var cn = "零壹贰叁肆伍陆柒捌玖"
    var unit = new Array("拾百千", "分角")
    var unit1 = new Array("万亿", "")
    var numArray = num.toString().split(".")
    var start = new Array(numArray[0].length - 1, 2)
    function toChinese(num, index) {
        var num = num.replace(/\d/g, function ($1) {
            return cn.charAt($1) + unit[index].charAt(start-- % 4 ? start % 4 : -1)
        })
        return num
    }
    for (var i = 0; i < numArray.length; i++) {
        var tmp = ""
        for (var j = 0; j * 4 < numArray[i].length; j++) {
            var strIndex = numArray[i].length - (j + 1) * 4
            var str = numArray[i].substring(strIndex, strIndex + 4)
            var start = i ? 2 : str.length - 1
            var tmp1 = toChinese(str, i)
            tmp1 = tmp1.replace(/(零.)+/g, "零").replace(/零+$/, "")
            tmp1 = tmp1.replace(/^壹拾/, "拾")
            tmp = (tmp1 + unit1[i].charAt(j - 1)) + tmp
        }
        numArray[i] = tmp
    }
    numArray[1] = numArray[1] ? numArray[1] : ""
    numArray[0] = numArray[0] ? numArray[0] + "元" : numArray[0], numArray[1] = numArray[1].replace(/^零+/, "")
    numArray[1] = numArray[1].match(/分/) ? numArray[1] : numArray[1]; // + "整"
    return numArray[0] + numArray[1] == "" ? "零元" : numArray[0] + numArray[1];
}
function aNumber(num) {
    var numArray = new Array()
    var unit = "亿万元$"
    for (var i = 0; i < unit.length; i++) {
        var re = eval("/" + (numArray[i - 1] ? unit.charAt(i - 1) : "") + "(.*)" + unit.charAt(i) + "/")
        if (num.match(re)) {
            numArray[i] = num.match(re)[1].replace(/^拾/, "壹拾")
            numArray[i] = numArray[i].replace(/[零壹贰叁肆伍陆柒捌玖]/g, function ($1) {
                return "零壹贰叁肆伍陆柒捌玖".indexOf($1)
            })
            numArray[i] = numArray[i].replace(/[分角拾百千]/g, function ($1) {
                return "*" + Math.pow(10, "分角 拾百千 ".indexOf($1) - 2) + "+"
            }).replace(/^\*|\+$/g, "").replace(/整/, "0")
            numArray[i] = "(" + numArray[i] + ")*" + Math.ceil(Math.pow(10, (2 - i) * 4))
        }
        else numArray[i] = 0
    }
    return eval(numArray.join("+"))
}

/**************大小写转换通用 End*********************/

function ShowSystemDialog(msg) {
    if (msg != "") {
        art.dialog({
            id: 'artDialogID',
            zIndex: 9999,
            lock: true,
            fixed: true,
            opacity: 0.3,
            content: msg,
            button: [{
                name: '确定'
            }]
        });
    }
}

function ShowSystemDialogCloseDialog(msg, isClose) {

    if (msg != "") {
        art.dialog({
            id: 'artDialogID',
            lock: true,
            fixed: true,
            opacity: 0.3,
            content: msg,
            button: [{
                name: '确定',
                callback: function () {
                    // 如果是自动关闭弹出窗口
                    if (isClose == "True") {
                        CloseDialogWindow();
                    } else {
                        btnResetClick(); // 重置页面数据
                    }
                    return true;
                }
            }]
        });
    }
}
/// <summary>
/// 打开系统提示窗口 并设置指定对象焦点，并选中
/// </summary>
function ShowCallBackSystemDialog(msg, canFocusObj) {
    if (msg != "") {
        art.dialog({
            id: 'artDialogID',
            zIndex: 9999,
            lock: true,
            fixed: true,
            opacity: 0.3,
            content: msg,
            button: [{
                name: '确定'
            }],
            close: function () {
                if (jQuery(canFocusObj).length > 0) {
                    jQuery(canFocusObj).focus();
                    jQuery(canFocusObj).select();
                }
                return true;
            }
        });
    }
}

function btnResetClick() {

    jQuery("#btnReset").click();
}


// ================================ 登录用户的菜单列表,权限列表，分检科室列表 ==== start ==================================================

/// <summary>
/// 获取用户的菜单列表,权限列表，分检科室列表
/// </summary>
function GetUserMenuRightSectionList() {
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxRight.aspx",
        data: { action: 'GetUserMenuRightSectionList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示菜单项，判断用户权限
                ShowUserMenuRightSectionList(jsonmsg);
            }
        }
    });
}

var gLoginRightJsonData = null; // 登录用户的权限变量
var gLoginUserMenuClass = null; // 用户菜单分组
var gLoginUserSection = null;   // 用户具有的科室

/// <summary>
///  判断登录用户是否具有某科室的操作权限
/// </summary>
function Is_LoginUserSection(currSectionID) {
    var tempCurrentResult = false;
    jQuery(gLoginUserSection).each(function (k, tempsectionitem) {
        if (tempsectionitem.ID_Section == currSectionID) {
            tempCurrentResult = true;
            return false; // brerak
        }
    });
    return tempCurrentResult;
}
/// <summary>
///  判断登录用户是否具有当前权限
/// </summary>
function Is_LoginUserRight(currRightCode) {
    var tempCurrentResult = false;
    jQuery(gLoginRightJsonData).each(function (i, rightitem) {
        if (rightitem.RightCode == currRightCode) {
            tempCurrentResult = true;
            return false; // brerak
        }
    });
    return tempCurrentResult;
}

/// <summary>
///  用于显示菜单项，判断用户权限
/// </summary>
function ShowUserMenuRightSectionList(UserMRSList) {

    gLoginRightJsonData = UserMRSList.dataList1; // 登录用户的权限变量
    gLoginUserSection = UserMRSList.dataList2;   // 用户具有的科室
    gLoginUserMenuClass = UserMRSList.dataList3; // 用户菜单分组

    // 菜单显示模版
    var LoginUserMenuTempleteContent = jQuery("#LoginUserMenuTemplete").html();
    if (LoginUserMenuTempleteContent == undefined) { return }

    // 菜单分组显示模版（分组的标题栏）
    var LoginUserMenuClassTempleteContent = jQuery("#LoginUserMenuClassTemplete").html();
    if (LoginUserMenuClassTempleteContent == undefined) { return }

    // 菜单分组显示模版（分组的详细信息）
    var LoginUserMenuClassSubItemTempleteContent = jQuery("#LoginUserMenuClassSubItemTemplete").html();
    if (LoginUserMenuClassSubItemTempleteContent == undefined) { return }


    var LoginUserMenuStrs = "";             // 用户菜单项内容字符串
    var LoginUserMenuClassStrs = "";        // 用户菜单分组内容字符串（分组的标题栏）
    var LoginUserMenuClassSubItemStrs = ""; // 用户菜单分组内容字符串（分组的详细信息）
    var tmpMenuCount = 0;                   // 记录菜单项的个数
    var tmpMenuClassCount = 0;              // 记录分组的个数
    var tmpFirstMeunClassID = 0;            // 记录第一个菜单分组的ID

    var strShowMoreBtnClassID = "";         // 记录需要显示“显示全部”的分组ID

    // 循环菜单分类 (dataList3)
    jQuery(UserMRSList.dataList3).each(function (k, menuclassitem) {

        LoginUserMenuStrs = ""; // 开始新的分类时，先清空先前的菜单数据
        tmpMenuCount = 0;       // 当前分类中菜单项的数量
        // dataList0 菜单项目
        jQuery(UserMRSList.dataList0).each(function (i, menuitem) {

            // 如果不是本类中的子菜单项，则跳过该条数据，继续判断下一条数据
            if (menuclassitem.ID_Menu != menuitem.ID_ParentMenu) { return true; } // continue;

            if (menuitem.Is_CombineWithSection == "True") {

                // dataList2 用户科室
                jQuery(UserMRSList.dataList2).each(function (j, sectionitem) {
                    tmpMenuCount++;
                    LoginUserMenuStrs += LoginUserMenuTempleteContent
                                    .replace(/@ID_Menu/gi, i)
                                    .replace(/@MenuName/gi, sectionitem.SectionName)
                                    .replace(/@ID_Section/gi, sectionitem.ID_Section)
                                    .replace(/@MenuUrl/gi, menuitem.MenuUrl + "?txtSectionID=" + sectionitem.ID_Section);
                });

            } else {
                tmpMenuCount++;
                LoginUserMenuStrs += LoginUserMenuTempleteContent
                                .replace(/@ID_Menu/gi, i)
                                .replace(/@ID_Section/gi, "0")
                                .replace(/@MenuName/gi, menuitem.MenuName)
                                .replace(/@MenuUrl/gi, menuitem.MenuUrl);
            }
        });

        // 如果当前菜单数量大于18，则显示“显示全部”这个按钮。
        if (tmpMenuCount > 18) {
            if (strShowMoreBtnClassID == "") {
                strShowMoreBtnClassID = menuclassitem.ID_Menu + ",";
            } else {
                strShowMoreBtnClassID = strShowMoreBtnClassID + menuclassitem.ID_Menu + ",";
            }
        }


        tmpMenuClassCount++;
        LoginUserMenuClassSubItemStrs += LoginUserMenuClassSubItemTempleteContent
                                    .replace(/@ID_Menu/gi, menuclassitem.ID_Menu)
                                    .replace(/@LoginUserMenuClassSubItem/gi, LoginUserMenuStrs);
        if (tmpMenuClassCount == 1) { tmpFirstMeunClassID = menuclassitem.ID_Menu; }


    });
    // 显示菜单项（显示所有分组后的详细信息）
    jQuery("#ShowUserMenuDiv").html(LoginUserMenuClassSubItemStrs);

    // 显示第一个分组
    jQuery("#LoginUserMenuClassSubItem_" + tmpFirstMeunClassID).show();

    // dataList3 菜单分类项目
    var IsSelected = " class='selected' ";
    tmpMenuClassCount = 0;
    jQuery(UserMRSList.dataList3).each(function (k, menuclassitem) {
        tmpMenuClassCount++;
        if (tmpMenuClassCount > 1) { IsSelected = ""; }
        LoginUserMenuClassStrs += LoginUserMenuClassTempleteContent
                            .replace(/@MenuClassName/gi, menuclassitem.MenuName)
                            .replace(/@ID_Menu/gi, menuclassitem.ID_Menu)
                            .replace(/@IsSelected/gi, IsSelected);
    });

    // 显示菜单分组
    jQuery("#LoginUserMenuClass").html(LoginUserMenuClassStrs);
    if (tmpMenuClassCount > 1) {
        jQuery("#LoginUserMenuClassDiv").show();
        jQuery("#LoginUserMenuClass").show();
    }

    // 判断是否有需要显示“显示全部”按钮的分组
    if (strShowMoreBtnClassID != "") {
        var showMoreBtnArray = strShowMoreBtnClassID.split(",");
        for (var i = 0; i < showMoreBtnArray.length; i++) {
            if (showMoreBtnArray[i] != "") {
                jQuery("#ShowAllLoginUserMenu_" + showMoreBtnArray[i]).show();
            }
        }
    }

    jQuery(".quick-actions li a").click(function () {
        //remove掉同级元素样式
        jQuery(".quick-actions li a").removeClass("selected");
        jQuery(this).addClass("selected");

        SetCookie('FYHSubMenuID', jQuery(this).attr("menuid"));       // 点击菜单项，保存对应的menuid
        SetCookie('FYHSubSectionID', jQuery(this).attr("sectionid")); // 点击菜单项，保存对应的sectionid （如果不是分检，则该值为空）

        DoLoad(jQuery(this).attr("targeturl"), '');
    });


    // 获取用户相应的权限
    GetIndexUserRight();


    // 获取当前跳转地址 20130803 by WTang
    GetUserRedirectUrl();

}

/// <summary>
/// 切换分组信息
/// </summary>
function ShowUserMenuClass(CurrentMenuID) {

    SetCookie('FYHSaveMenuID', CurrentMenuID); // 将当前的MenuID保存到Cookie中。

    // 循环菜单分类
    jQuery(gLoginUserMenuClass).each(function (k, menuclassitem) {
        jQuery("#LoginUserMenuClassTitle_" + menuclassitem.ID_Menu).removeClass("selected");    // 标题的选中效果取消
        jQuery("#LoginUserMenuClassSubItem_" + menuclassitem.ID_Menu).hide();                   // 隐藏对应的菜单子项
    });
    jQuery("#LoginUserMenuClassTitle_" + CurrentMenuID).addClass("selected");   // 选中当前点击的标题
    jQuery("#LoginUserMenuClassSubItem_" + CurrentMenuID).show();               // 显示当前点击菜单子项

    jQuery("#loadForm").html("<div style='height:500px;'>&nbsp;</div>");        // 当切换分组的时候，清空页面内容区域加载的数据

    // 隐藏侧边栏的链接 20130822 by WTang 
    jQuery("#listSectionExamedCount").hide();   // 隐藏已检查科室列表
    jQuery("#listSectionExamFloat").hide();     // 隐藏客户科室列表
    jQuery("#listHistoryExamCount").hide();     // 隐藏历史体检号对比列表


    jQuery("#divGotoTop").hide();             // 隐藏 Top
    jQuery("#divSectionExamedCount").hide();  // 隐藏 结果
    jQuery("#divHistoryExamCount").hide();    // 隐藏 对比
    jQuery("#divSectionExamFloat").hide();    // 隐藏 换科室
}


// ================================ 登录用户的菜单列表,权限列表，分检科室列表 ==== end ==================================================



// ================================ 年龄计算 ==== start ==================================================

function calculateYear(dateText) {
    var aDate = dateText.split("-");
    var birthdayYear = parseInt(aDate[0]);
    var curDate = new Date();
    var curYear = parseInt(curDate.getFullYear());
    return curYear - birthdayYear;
}
function calculateMonth(dateText) {
    var month = 1;
    var aDate = dateText.split("-");
    if (aDate[1].substr(0, 1) == "0") {
        aDate[1] = aDate[1].substring(1);
    }
    var birthdayMonth = parseInt(aDate[1]);
    var curDate = new Date();
    var curMonth = parseInt(curDate.getMonth() + 1);
    month = curMonth - birthdayMonth;
    return month;
}
function catcAge(dateText) {
    var birthDay = new Date(dateText.replace(/-/g, "\/"));
    var d = new Date();
    var age = d.getFullYear() - birthDay.getFullYear() - ((d.getMonth() < birthDay.getMonth() ||
    d.getMonth() == birthDay.getMonth() && d.getDate() < birthDay.getDate()) ? 1 : 0);
    var month = calculateMonth(dateText);
    var year = calculateYear(dateText);
    if (year >= 0) {
        if (month <= 0 && year == 1) {
            return 0;
        }
        else {
            return year;
        }
    }
    else {
        return -1;
    }
}
// ================================ 年龄计算 ==== end ==================================================




// ================================ 用户头像获取 ==== start ==================================================

/// <summary>
///  通用方法，获取图片的64位编码
/// </summary>
function GetImageCodeBase64(path) {

    var FastReport = document.getElementById("FastReport");
    var SignatureCodeBase64 = FastReport.GetImageCodeBase64(path);
    return SignatureCodeBase64;
}

// ================================ 用户头像获取 ==== start ==================================================




// ================================ 用户快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询用户列表
/// </summary>
function ShowHideQuickQueryUserTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQueryUserTable").show();
    } else {
        jQuery("#QuickQueryUserTable").hide();
    }
}

var gAllUserDataList = "";    // 保存全部的用户列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldUserInputCode = "****";              // 记录上次输入的输入码
var gAllUserListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询用户（通过Ajax后台过滤）
/// VocationType 1:医生 2:护士 3:其他工作人员
/// </summary>
function QuickQueryUserTableData_Ajax(VocationType) {

    var curEvent = window.event || e;
    if (curEvent.keyCode == 13 || curEvent.keyCode == 37 || curEvent.keyCode == 38 || curEvent.keyCode == 39 || curEvent.keyCode == 40) {

        jQuery("#chkUser_" + jQuery("#tempSelectedUserID").val() + "").attr('checked', true);
        jQuery("#chkUser_" + jQuery("#tempSelectedUserID").val() + "").focus();
    }

    var InputCode = jQuery('#txtUserInputCode').val();
    if (OldUserInputCode != InputCode) {
        OldUserInputCode = InputCode;
    } else {
        ShowHideQuickQueryUserTable(true, InputCode);
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxUser.aspx",
        data: { InputCode: InputCode,
            VocationType: VocationType,        // 1:医生 2:护士 3:其他工作人员
            action: 'GetQuickUserList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的用户
            ShowQuickQueryUserTableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示用户（通过Ajax过滤）
/// </summary>
function ShowQuickQueryUserTableData_Ajax(Userlist) {
    if (Userlist == "" || Userlist.totalCount == 0) {

        ShowHideQuickQueryUserTable(true, jQuery('#txtUserInputCode').val());
        // 显示没有找到用户信息
        jQuery("#QuickQueryUserTableData").html(jQuery('#EmptyUserQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //用户table内容

        var UserQuickQueryTableTempleteContent = jQuery('#UserQuickQueryTableTemplete').html();             //快速查询用户列表模版
        if (UserQuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (Userlist != "") {
            jQuery("#tempSelectedUserID").val("");
            jQuery(Userlist.dataList).each(function (j, Useritem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += UserQuickQueryTableTempleteContent
                            .replace(/@ID_User/gi, Useritem.ID_User)
                            .replace(/@UserName/gi, Useritem.UserName)
                            .replace(/@InputCode/gi, Useritem.LoginName)
                            .replace(/@chkUserQueryList/gi, "chkUserQueryList")
                            ;
                if (CurrQueryCount == 1) {
                    jQuery("#tempSelectedUserID").val(Useritem.ID_User);
                }
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQueryUserTable(true, jQuery('#txtUserInputCode').val());
            jQuery("#QuickQueryUserTableData").html(conclusionContent);
        } else {
            ShowHideQuickQueryUserTable(false);
            jQuery("#QuickQueryUserTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择用户）
/// </summary>
function SelectUserDataList() {

    jQuery("input[name='chkUserQueryList']:radio:checked").each(function () {
        ShowQuickSelectUser(jQuery(this).val(), jQuery(this).attr("Username"));
    });

    ShowHideQuickQueryUserTable(false);
}

/// <summary>
/// 删除选择的用户
/// </summary>
function RemoveSelectedUser() {
    jQuery('#spanSelectUser').hide();
    jQuery('#spanUser').show();
    jQuery('#spanSelectUser').html('');
    jQuery('#idSelectUser').val('');
    jQuery('#nameSelectUser').val('');

}


/// <summary> 
/// 点击选中对应用户的单选按钮（快速选择列表）
/// </summary>
function SetUserChecked(UserID) {
    jQuery("#chkUser_" + UserID).attr("checked", true);
    SelectQueryUser(UserID);

    ShowHideQuickQueryUserTable(false, '');
}


/// <summary> 
/// 点击用户列（快速选择列表）
/// </summary>
function UserClick(UserID) {
    jQuery("#chkUser_" + UserID).attr("checked", true);
    jQuery("#chkUser_" + UserID).focus();
    UserClickBackground(UserID);
}

/// <summary> 
/// 点击用户列（快速选择列表）
/// </summary>
function UserClickBackground(UserID) {
    jQuery("#QuickQueryUserTableData tr").removeClass();

    jQuery("#trUser_" + UserID).addClass("SelectedItem");
}


/// <summary> 
/// 选择用户（快速选择）
/// </summary>
function SelectQueryUser(UserID) {

    // 从模版中读取数据加载列表
    var templateContent = jQuery('#SecectedUserLableTemplete').html();
    if (templateContent == undefined) { return; }
    var tempUserName = jQuery("#chkUser_" + UserID).attr("UserName");

    var newcontent = templateContent.replace(/@UserName/gi, tempUserName); // 替换模版中的关键字

    jQuery('#spanSelectUser').html(newcontent);   // 显示到页面
    jQuery('#spanUser').hide();   // 显示到页面
    jQuery('#spanSelectUser').show();   // 显示到页面
    jQuery('#idSelectUser').val(UserID);         // 选择的用户ID
    jQuery('#nameSelectUser').val(tempUserName);  // 选择的用户名称

}


/// <summary> 
/// 显示快速选择的用户名
/// </summary>
function ShowQuickSelectUser(UserID, UserName) {
    if (UserName == "") { return; }
    // 从模版中读取数据加载列表
    var templateContent = jQuery('#SecectedUserLableTemplete').html();
    if (templateContent == undefined) { return; }

    var newcontent = templateContent.replace(/@UserName/gi, UserName); // 替换模版中的关键字

    jQuery('#spanSelectUser').html(newcontent);     // 显示到页面
    jQuery('#spanUser').hide();                     // 隐藏
    jQuery('#spanSelectUser').show();               // 显示到页面
    jQuery('#idSelectUser').val(UserID);           // 选择的用户ID  （隐藏值）
    jQuery('#nameSelectUser').val(UserName);        // 选择的用户名称（隐藏值）

}


// ================================ 用户快速选择函数区域 ==== end ==================================================



// ================================ 民族快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询民族列表
/// </summary>
function ShowHideQuickQueryNationTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQueryNationTable").show();
    } else {
        jQuery("#QuickQueryNationTable").hide();
    }
}

var gAllNationDataList = "";    // 保存全部的民族列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldNationInputCode = "****";              // 记录上次输入的输入码
var gAllNationListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询民族（通过Ajax后台过滤）
/// NationVocationType 1:医生 2:护士 3:其他工作人员
/// </summary>
function QuickQueryNationTableData_Ajax() {

    var curEvent = window.event || e;
    if (curEvent.keyCode == 13 || curEvent.keyCode == 37 || curEvent.keyCode == 38 || curEvent.keyCode == 39 || curEvent.keyCode == 40) {

        jQuery("#chkNation_" + jQuery("#tempSelectedNationID").val() + "").attr('checked', true);
        jQuery("#chkNation_" + jQuery("#tempSelectedNationID").val() + "").focus();
    }

    var InputCode = jQuery('#txtNationInputCode').val();
    if (OldNationInputCode != InputCode) {
        OldNationInputCode = InputCode;
    } else {
        ShowHideQuickQueryNationTable(true, InputCode);
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { InputCode: InputCode,
            action: 'GetQuickNationList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的民族
            ShowQuickQueryNationTableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示民族（通过Ajax过滤）
/// </summary>
function ShowQuickQueryNationTableData_Ajax(Nationlist) {
    if (Nationlist == "" || Nationlist.totalCount == 0) {

        ShowHideQuickQueryNationTable(true, jQuery('#txtNationInputCode').val());
        // 显示没有找到民族信息
        jQuery("#QuickQueryNationTableData").html(jQuery('#EmptyNationQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //民族table内容

        var NationQuickQueryTableTempleteContent = jQuery('#NationQuickQueryTableTemplete').html();             //快速查询民族列表模版
        if (NationQuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (Nationlist != "") {
            jQuery("#tempSelectedNationID").val("");
            jQuery(Nationlist.dataList).each(function (j, Nationitem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += NationQuickQueryTableTempleteContent
                            .replace(/@NationID/gi, Nationitem.NationID)
                            .replace(/@NationName/gi, Nationitem.NationName)
                            .replace(/@InputCode/gi, Nationitem.InputCode)
                            .replace(/@chkNationQueryList/gi, "chkNationQueryList")
                            ;
                if (CurrQueryCount == 1) {
                    jQuery("#tempSelectedNationID").val(Nationitem.NationID);
                }
            });


        }
        if (conclusionContent != "") {
            ShowHideQuickQueryNationTable(true, jQuery('#txtNationInputCode').val());
            jQuery("#QuickQueryNationTableData").html(conclusionContent);
        } else {
            ShowHideQuickQueryNationTable(false);
            jQuery("#QuickQueryNationTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择民族）
/// </summary>
function SelectNationDataList() {

    jQuery("input[name='chkNationQueryList']:radio:checked").each(function () {
        ShowQuickSelectNation(jQuery(this).val(), jQuery(this).attr("Nationname"));
    });

    ShowHideQuickQueryNationTable(false);
}

/// <summary>
/// 删除选择的民族
/// </summary>
function RemoveSelectedNation() {
    jQuery('#spanSelectNation').hide();
    jQuery('#spanNation').show();
    jQuery('#spanSelectNation').html('');
    jQuery('#idSelectNation').val('');
    jQuery('#nameSelectNation').val('');

}


/// <summary> 
/// 点击选中对应民族的单选按钮（快速选择列表）
/// </summary>
function SetNationChecked(NationID) {
    jQuery("#chkNation_" + NationID).attr("checked", true);
    SelectQueryNation(NationID);

    ShowHideQuickQueryNationTable(false, '');
}


/// <summary> 
/// 点击民族列（快速选择列表）
/// </summary>
function NationClick(NationID) {
    jQuery("#chkNation_" + NationID).attr("checked", true);
    jQuery("#chkNation_" + NationID).focus();
    NationClickBackground(NationID);
}

/// <summary> 
/// 点击民族列（快速选择列表）
/// </summary>
function NationClickBackground(NationID) {
    jQuery("#QuickQueryNationTableData tr").removeClass();

    jQuery("#trNation_" + NationID).addClass("SelectedItem");
}

/// <summary> 
/// 选择民族（快速选择）
/// </summary>
function SelectQueryNation(NationID) {

    // 从模版中读取数据加载列表
    var templateContent = jQuery('#SecectedNationLableTemplete').html();
    if (templateContent == undefined) { return; }
    var tempNationName = jQuery("#chkNation_" + NationID).attr("NationName");

    var newcontent = templateContent.replace(/@NationName/gi, tempNationName); // 替换模版中的关键字

    jQuery('#spanSelectNation').html(newcontent);   // 显示到页面
    jQuery('#spanNation').hide();   // 显示到页面
    jQuery('#spanSelectNation').show();   // 显示到页面
    jQuery('#idSelectNation').val(NationID);         // 选择的民族ID
    jQuery('#nameSelectNation').val(tempNationName);  // 选择的民族名称

}


/// <summary> 
/// 显示快速选择的民族名
/// </summary>
function ShowQuickSelectNation(NationID, NationName) {
    if (NationName == "") { return; }
    // 从模版中读取数据加载列表
    var templateContent = jQuery('#SecectedNationLableTemplete').html();
    if (templateContent == undefined) { return; }

    var newcontent = templateContent.replace(/@NationName/gi, NationName); // 替换模版中的关键字

    jQuery('#spanSelectNation').html(newcontent);     // 显示到页面
    jQuery('#spanNation').hide();                     // 隐藏
    jQuery('#spanSelectNation').show();               // 显示到页面
    jQuery('#idSelectNation').val(NationID);           // 选择的民族ID  （隐藏值）
    jQuery('#nameSelectNation').val(NationName);        // 选择的民族名称（隐藏值）

}


// ================================ 民族快速选择函数区域 ==== end ==================================================


// ================================ 体检类型快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询体检类型列表
/// </summary>
function ShowHideQuickQueryExamTypeTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQueryExamTypeTable").show();
    } else {
        jQuery("#QuickQueryExamTypeTable").hide();
    }
}

var gAllExamTypeDataList = "";    // 保存全部的体检类型列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldExamTypeInputCode = "****";              // 记录上次输入的输入码
var gAllExamTypeListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询体检类型（通过Ajax后台过滤）
/// ExamTypeVocationType 1:医生 2:护士 3:其他工作人员
/// </summary>
function QuickQueryExamTypeTableData_Ajax() {

    var curEvent = window.event || e;
    if (curEvent.keyCode == 13 || curEvent.keyCode == 37 || curEvent.keyCode == 38 || curEvent.keyCode == 39 || curEvent.keyCode == 40) {

        jQuery("#chkExamType_" + jQuery("#tempSelectedExamTypeID").val() + "").attr('checked', true);
        jQuery("#chkExamType_" + jQuery("#tempSelectedExamTypeID").val() + "").focus();
    }
    var InputCode = jQuery('#txtExamTypeInputCode').val();
    if (OldExamTypeInputCode != InputCode) {
        OldExamTypeInputCode = InputCode;
    } else {
        ShowHideQuickQueryExamTypeTable(true, InputCode);
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { InputCode: InputCode,
            action: 'GetQuickExamTypeList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的体检类型
            ShowQuickQueryExamTypeTableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示体检类型（通过Ajax过滤）
/// </summary>
function ShowQuickQueryExamTypeTableData_Ajax(ExamTypelist) {
    if (ExamTypelist == "" || ExamTypelist.totalCount == 0) {

        ShowHideQuickQueryExamTypeTable(true, jQuery('#txtExamTypeInputCode').val());
        // 显示没有找到体检类型信息
        jQuery("#QuickQueryExamTypeTableData").html(jQuery('#EmptyExamTypeQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //体检类型table内容

        var ExamTypeQuickQueryTableTempleteContent = jQuery('#ExamTypeQuickQueryTableTemplete').html();             //快速查询体检类型列表模版
        if (ExamTypeQuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (ExamTypelist != "") {

            jQuery("#tempSelectedExamTypeID").val("");
            jQuery(ExamTypelist.dataList).each(function (j, ExamTypeitem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += ExamTypeQuickQueryTableTempleteContent
                            .replace(/@ID_ExamType/gi, ExamTypeitem.ID_ExamType)
                            .replace(/@ExamTypeName/gi, ExamTypeitem.ExamTypeName)
                            .replace(/@InputCode/gi, ExamTypeitem.InputCode)
                            .replace(/@chkExamTypeQueryList/gi, "chkExamTypeQueryList")
                            ;
                if (CurrQueryCount == 1) {
                    jQuery("#tempSelectedExamTypeID").val(ExamTypeitem.ID_ExamType);
                }
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQueryExamTypeTable(true, jQuery('#txtExamTypeInputCode').val());
            jQuery("#QuickQueryExamTypeTableData").html(conclusionContent);
        } else {
            ShowHideQuickQueryExamTypeTable(false);
            jQuery("#QuickQueryExamTypeTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择体检类型）
/// </summary>
function SelectExamTypeDataList() {
    jQuery("input[name='chkExamTypeQueryList']:radio:checked").each(function () {
        ShowQuickSelectExamType(jQuery(this).val(), jQuery(this).attr("ExamTypename"));
    });

    ShowHideQuickQueryExamTypeTable(false);
}

/// <summary>
/// 删除选择的体检类型
/// </summary>
function RemoveSelectedExamType() {
    jQuery('#spanSelectExamType').hide();
    jQuery('#spanExamType').show();
    jQuery('#spanSelectExamType').html('');
    jQuery('#idSelectExamType').val('');
    jQuery('#nameSelectExamType').val('');

}


/// <summary> 
/// 点击选中对应体检类型的单选按钮（快速选择列表）
/// </summary>
function SetExamTypeChecked(ID_ExamType) {
    jQuery("#chkExamType_" + ID_ExamType).attr("checked", true);
    SelectQueryExamType(ID_ExamType);

    ShowHideQuickQueryExamTypeTable(false, '');
}


/// <summary> 
/// 选择体检类型（快速选择）
/// </summary>
function SelectQueryExamType(ID_ExamType) {

    // 从模版中读取数据加载列表
    var templateContent = jQuery('#SecectedExamTypeLableTemplete').html();
    if (templateContent == undefined) { return; }
    var tempExamTypeName = jQuery("#chkExamType_" + ID_ExamType).attr("ExamTypeName");

    var newcontent = templateContent.replace(/@ExamTypeName/gi, tempExamTypeName); // 替换模版中的关键字

    jQuery('#spanSelectExamType').html(newcontent);   // 显示到页面
    jQuery('#spanExamType').hide();   // 显示到页面
    jQuery('#spanSelectExamType').show();   // 显示到页面
    jQuery('#idSelectExamType').val(ID_ExamType);         // 选择的体检类型ID
    jQuery('#nameSelectExamType').val(tempExamTypeName);  // 选择的体检类型名称

}

/// <summary> 
/// 点击体检类型列（快速选择列表）
/// </summary>
function ExamTypeClick(ID_ExamType) {
    jQuery("#chkExamType_" + ID_ExamType).attr("checked", true);
    jQuery("#chkExamType_" + ID_ExamType).focus();
    SetClickBackground(ID_ExamType);
}

/// <summary> 
/// 点击体检类型列（快速选择列表）
/// </summary>
function ExamTypeClickBackground(ID_ExamType) {
    jQuery("#QuickQueryExamTypeTableData tr").removeClass();

    jQuery("#trExamType_" + ID_ExamType).addClass("SelectedItem");
}


/// <summary> 
/// 显示快速选择的体检类型名
/// </summary>
function ShowQuickSelectExamType(ID_ExamType, ExamTypeName) {
    if (ExamTypeName == "") { return; }
    // 从模版中读取数据加载列表
    var templateContent = jQuery('#SecectedExamTypeLableTemplete').html();
    if (templateContent == undefined) { return; }

    var newcontent = templateContent.replace(/@ExamTypeName/gi, ExamTypeName); // 替换模版中的关键字

    jQuery('#spanSelectExamType').html(newcontent);     // 显示到页面
    jQuery('#spanExamType').hide();                     // 隐藏
    jQuery('#spanSelectExamType').show();               // 显示到页面
    jQuery('#idSelectExamType').val(ID_ExamType);           // 选择的体检类型ID  （隐藏值）
    jQuery('#nameSelectExamType').val(ExamTypeName);        // 选择的体检类型名称（隐藏值）

}


// ================================ 体检类型快速选择函数区域 ==== end ==================================================


// ================================ 套餐类型快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询套餐类型列表
/// </summary>
function ShowHideQuickQuerySetTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQuerySetTable").show();
    } else {
        jQuery("#QuickQuerySetTable").hide();
    }
}

var gAllSetDataList = "";    // 保存全部的套餐类型列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldSetInputCode = "****";              // 记录上次输入的输入码
var gAllSetListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
var gSex = ""; // 保存上次查询的性别
/// <summary>
/// 根据输入码查询套餐类型（通过Ajax后台过滤）
/// SetVocationType 1:医生 2:护士 3:其他工作人员
/// </summary>
function QuickQuerySetTableData_Ajax() {

    var curEvent = window.event || e;
    if (curEvent.keyCode == 13 || curEvent.keyCode == 37 || curEvent.keyCode == 38 || curEvent.keyCode == 39 || curEvent.keyCode == 40) {

        jQuery("#chkSet_" + jQuery("#tempSelectedSetID").val() + "").attr('checked', true);
        jQuery("#chkSet_" + jQuery("#tempSelectedSetID").val() + "").focus();
    }
    var InputCode = jQuery('#txtSetInputCode').val();
    var Sex = jQuery('#slSex').val();
    Sex = (Sex == 1 ? Sex : 0); //0：女性，1男性，2：共用

    if (gSex != Sex || OldSetInputCode != InputCode) {
        OldSetInputCode = InputCode;
        gSex = Sex;
    } else {
        ShowHideQuickQuerySetTable(true, InputCode);
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { InputCode: InputCode,
            Sex: Sex,
            action: 'GetQuickSetList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的套餐类型
            ShowQuickQuerySetTableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示套餐类型（通过Ajax过滤）
/// </summary>
function ShowQuickQuerySetTableData_Ajax(Setlist) {
    if (Setlist == "" || Setlist.totalCount == 0) {

        ShowHideQuickQuerySetTable(true, jQuery('#txtSetInputCode').val());
        // 显示没有找到套餐类型信息
        jQuery("#QuickQuerySetTableData").html(jQuery('#EmptySetQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //套餐类型table内容

        var SetQuickQueryTableTempleteContent = jQuery('#SetQuickQueryTableTemplete').html();             //快速查询套餐类型列表模版
        if (SetQuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (Setlist != "") {
            jQuery("#tempSelectedSetID").val("");
            jQuery(Setlist.dataList).each(function (j, Setitem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += SetQuickQueryTableTempleteContent
                            .replace(/@PEPackageID/gi, Setitem.PEPackageID)
                            .replace(/@PEPackageName/gi, Setitem.PEPackageName)
                            .replace(/@InputCode/gi, Setitem.InputCode)
                            .replace(/@chkSetQueryList/gi, "chkSetQueryList")
                            ;
                if (CurrQueryCount == 1) {
                    jQuery("#tempSelectedSetID").val(Setitem.PEPackageID);
                }
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQuerySetTable(true, jQuery('#txtSetInputCode').val());
            jQuery("#QuickQuerySetTableData").html(conclusionContent);
        } else {
            ShowHideQuickQuerySetTable(false);
            jQuery("#QuickQuerySetTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择套餐类型）
/// </summary>
function SelectSetDataList() {
    jQuery("input[name='chkSetQueryList']:radio:checked").each(function () {
        ShowQuickSelectSet(jQuery(this).val(), jQuery(this).attr("PEPackageName"), true); // 套餐变动，重新读取数据
    });

    ShowHideQuickQuerySetTable(false);
}

/// <summary>
/// 删除选择的套餐类型
/// </summary>
function RemoveSelectedSet() {
    jQuery('#spanSelectSet').hide();
    jQuery('#spanSet').show();
    jQuery('#spanSelectSet').html('');
    jQuery('#idSelectSet').val('');
    jQuery('#nameSelectSet').val('');

}


/// <summary> 
/// 点击选中对应套餐类型的单选按钮（快速选择列表）
/// </summary>
function SetSetChecked(PEPackageID) {
    jQuery("#chkSet_" + PEPackageID).attr("checked", true);
    SelectQuerySet(PEPackageID);

    ShowHideQuickQuerySetTable(false, '');
}

/// <summary> 
/// 点击套餐类型列（快速选择列表）
/// </summary>
function SetClick(PEPackageID) {
    jQuery("#chkSet_" + PEPackageID).attr("checked", true);
    jQuery("#chkSet_" + PEPackageID).focus();
    SetClickBackground(PEPackageID);
}

/// <summary> 
/// 点击套餐类型列（快速选择列表）
/// </summary>
function SetClickBackground(PEPackageID) {
    jQuery("#QuickQuerySetTableData tr").removeClass();

    jQuery("#trSet_" + PEPackageID).addClass("SelectedItem");
}

/// <summary> 
/// 选择套餐类型（快速选择）
/// </summary>
function SelectQuerySet(PEPackageID) {

    // 从模版中读取数据加载列表
    var templateContent = jQuery('#SecectedSetLableTemplete').html();
    if (templateContent == undefined) { return; }
    var tempPEPackageName = jQuery("#chkSet_" + PEPackageID).attr("PEPackageName");

    ShowQuickSelectSet(PEPackageID, tempPEPackageName, true); // 套餐变动，重新读取数据

}


/// <summary> 
/// 显示快速选择的套餐类型名
/// 修改人：黄兴茂
///修改时间:2013-07-25
///修改内容：新增IsTeam参数以便特殊处理团体登记中套餐问题（由于团体备单时可用没有套餐，即使有套餐也有可用移除套餐中某些项目，这里不能直接单纯的触发套餐变动事件，需要特殊处理）
/// </summary>
function ShowQuickSelectSet(PEPackageID, PEPackageName, IsChange) {
    if (PEPackageName != "") {
        // 从模版中读取数据加载列表
        var templateContent = jQuery('#SecectedSetLableTemplete').html();
        if (templateContent == undefined) { return; }

        var newcontent = templateContent.replace(/@PEPackageName/gi, PEPackageName); // 替换模版中的关键字

        jQuery('#spanSelectSet').html(newcontent);     // 显示到页面
        jQuery('#spanSet').hide();                     // 隐藏
        jQuery('#spanSelectSet').show();               // 显示到页面
        jQuery('#idSelectSet').val(PEPackageID);           // 选择的套餐类型ID  （隐藏值）
        jQuery('#nameSelectSet').val(PEPackageName);        // 选择的套餐类型名称（隐藏值）
    }
    if (IsChange == true) {
        jQuery("#idSelectSet").change(); // 套餐变动，调用数据读取函数
    }
}


// ================================ 套餐类型快速选择函数区域 ==== end ==================================================


// ========================== 查询客户基本信息 == start ====================================


var defalutImagSrc = "/template/blue/images/icons/nohead.gif";
/// <summary>
///通过客户编号[体检号]检索客户基本信息和客户收费项目信息
/// </summary>
function SearchCustomerBaseInfo() {

    // 查询数据前，先隐藏客户基本信息区域
    jQuery("#divCustomerInfoArea").hide();

    var ID_Customer = jQuery('#txtCustomerID').val();

    if (ID_Customer == "") {
        return false;
    }

    //组建请求参数
    var Is_Org = 0;
    var data = { action: "GetCustomerByIDCustomer", ID_Customer: ID_Customer, currenttime: encodeURIComponent(new Date()) };
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxRegiste.aspx",
        data: data,
        cache: false,
        dataType: "json",
        success: function (msg) {
            ShowCustomerBaseInfo(msg);
            //这里绑定客户基本信息
        }
    });
    jQuery("#txtID_Customer").focus();
    jQuery("#txtID_Customer").select();
}
//设置用户基本信息
function ShowCustomerBaseInfo(msg) {
    if (msg == null || msg == undefined)
        return false;


    var item;
    var dataList0 = msg.dataList0; //存放客户基本信息
    var dataList1 = msg.dataList1; //存放客户体检信息
    var dataList2 = msg.dataList2; //存放客户报告信息 修改人：黄兴茂 修改日期：2013-07-26 修改内容：后台新增客户报告查询sql并客户端接受处理
    for (var c = 0; c < dataList0.length; c++) {
        item = dataList0[c];
        jQuery("#lblCustomerName").text(item.CustomerName);
        jQuery("#lblSex").text(item.GenderName);
        jQuery("#lblIDCard").text(item.IDCard);
        jQuery("#lblTel").text(item.MobileNo);
        jQuery("#lblMarriedName").text(item.MarriageName);
        //设置头像
        if (item.Base64Photo == "") {
            jQuery("#HeadImg").attr("src", defalutImagSrc);
        }
        else {
            jQuery("#HeadImg").attr("src", "data:image/gif;base64," + item.Base64Photo + "");
        }
        //绑定用户基本信息
        jQuery("#lblAge").text(item.Age);
        jQuery("#tblSearch tbody tr[id='loading']").hide();
        jQuery("#tblSearch tbody tr[id!='loading']").show();
    }
    for (var c = 0; c < dataList1.length; c++) {
        item = dataList1[c];
        //绑定用户基本信息
        jQuery("#lblID_Customer").text(item.ID_Customer);
        jQuery("#lblRegisterDate").text(item.SubScribDate);
        jQuery("#lblTeamName").text(item.TeamName);
        jQuery("#lblExamType").text(item.ExamTypeName);

        var data = { Is_GuideSheetPrinted: item.Is_GuideSheetPrinted, Is_Subscribed: item.Is_Subscribed };
        jQuery("#txtCustomerID").data("data", data);
        //这里判断总审是否存在,存在则赋值
        if (jQuery("#lblIs_Checked") != undefined) {

            if (item.Is_Checked == "True") {
                item.Is_Checked = "√";
                //设置报告预览按钮可见 xmhuang 2013-10-15 注释掉，移除预览限制
                //jQuery("#btnReportPreview").show();
            }
            else {
                item.Is_Checked = "×";
                //xmhuang 2013-10-15 注释掉，移除预览限制
                //jQuery("#btnReportPreview").hide();
            }
            jQuery("#lblIs_Checked").text(item.Is_Checked);
            jQuery("#lblChecker").text(item.Checker);
            jQuery("#lblCheckedDate").text(item.CheckedDate);
            //判断分检状态
            if (item.Is_ExamStarted == "True") {
                item.Is_ExamStarted = "√";
            }
            else {
                item.Is_ExamStarted = "×";
            }
            //判断总检状态
            if (item.Is_FinalFinished == "True") {
                item.Is_FinalFinished = "√";
            }
            else {
                item.Is_FinalFinished = "×";
            }
            //jQuery("#lblIs_ExamStarted").text(item.Is_ExamStarted); //是否总检
            jQuery("#lblIs_FinalFinished").text(item.Is_FinalFinished); //是否总检
            jQuery("#lblFinalDoctor").text(item.FinalDoctor); //总检医生
            jQuery("#lblFinalDate").text(item.FinalDate); //总检日期
        }
    }
    for (var c = 0; c < dataList2.length; c++) {
        item = dataList2[c];
        //判断报告打印状态
        if (item.Is_ReportPrinted == "True") {
            item.Is_ReportPrinted = "√";
        }
        else {
            item.Is_ReportPrinted = "×";
        }
        //判断报告通知状态
        if (item.Is_Informed == "True") {
            item.Is_Informed = "√";
        }
        else {
            item.Is_Informed = "×";
        }
        //判断报告领取状态
        if (item.Is_ReportReceipted == "True") {
            item.Is_ReportReceipted = "√";
        }
        else {
            item.Is_ReportReceipted = "×";
        }
        jQuery("#lblIs_ReportPrinted").text(item.Is_ReportPrinted); //是否打印
        jQuery("#lblReportPrinter").text(item.ReportPrinter); //打印人
        jQuery("#lblReportPrintedDate").text(item.ReportPrintedDate); //打印时间

        jQuery("#lblIs_Informed").text(item.Is_Informed); //是否通知
        jQuery("#lblInformer").text(item.Informer); //通知人
        jQuery("#lblInformedDate").text(item.InformedDate); //通知日期

        jQuery("#lblIs_ReportReceipted").text(item.Is_ReportReceipted); //是否领取
        jQuery("#lblReportReceiptor").text(item.ReportReceiptor); //领取人
        jQuery("#lblReportReceiptedDate").text(item.ReportReceiptedDate); //领取日期
    }

    if (dataList0.length == 0) {
        jQuery("#tblSearch tbody tr[id!='loading']").hide();
        jQuery("#tblSearch tbody tr[id='loading']").show();

        // 如果是分科检查，隐藏分科内容区域的信息 by WTang 20130819
        try {
            if (jQuery("#ContentArea") != null) { jQuery("#ContentArea").hide(); }
        } catch (e) {

        }
    }

    var CustomerSecurityLevel = jQuery("#CustomerSecurityLevel").val(); // 客户  操作密级
    var OperateLevel = jQuery("#OperateLevel").val();                   // 操作员操作密级

    // 检查操作密级
    if (CustomerSecurityLevel != undefined && parseInt(CustomerSecurityLevel) > parseInt(OperateLevel)) {
        jQuery("#divCustomerInfoArea").hide();  // 如果没有权限，则客户基本信息页不允许查看
    }
    else {
        // 如果查询到客户信息，则将客户信息进行显示
        jQuery("#divCustomerInfoArea").show();
    }

    jQuery("#txtCustomerID").focus();
    jQuery("#txtCustomerID").select();

}

// ========================== 查询客户基本信息 == end ====================================

/*******************************热键   Begin**********************************************/
//一个快捷键对象 
function KeyOne(id, keys, dom, isfun, fun, iskeydown) {
    this.id = id;
    this.keys = keys;
    this.dom = dom;
    this.isfun = isfun;
    this.fun = fun;
    this.isKeydown = iskeydown;
}

//快捷键管理类 
var KeyConlor = {};
KeyConlor.list = new Array();
//添加一个快捷键绑定焦点（当快捷键被激发时让焦点落在指定id对象上） 
//使用说明key的值如果是“c,50”则表示“ctrl”和键码为50的组合键 
// "a,50" 则表示“alt”和键码为50的组合键 
// "s,50" 则表示“shift”和键码为50的组合键 
// "50" 则表示键码为50的单键（建议使用组合键alt） 
//id指的是快捷键对应的焦点对象。 
//dom指的是id对象所在的document对象 
KeyConlor.addkeyfouse = function (id, key, dom, iskyedown) {
    var keyone = new KeyOne(id, key, dom, false, null, iskyedown);
    if (KeyConlor.KeyIsOK(keyone)) {
        KeyConlor.list.push(keyone);
    } else {
        ShowSystemDialog("快捷键" + keyone.keys + "已经被注册 不能重复注册了");
        return false;
    }
};

//快捷键绑定方法（当快捷键激发时触发方法） 
KeyConlor.addkeyfun = function (key, fun, iskeydown) {
    var keyone = new KeyOne("", key, "", true, fun, iskeydown);
    if (KeyConlor.KeyIsOK(keyone)) {
        KeyConlor.list.push(keyone)
    } else {
        ShowSystemDialog("快捷键:" + keyone.keys + ";已经被注册 .重复注册无效");
        return false;
    }
};

//--删除一个快捷键 
KeyConlor.removeFouseKey = function (id) {
    var keyone = new KeyOne(id, "");
    for (var i = 0; i < KeyConlor.list.length; i++) {
        if (keyone.id == KeyConlor.list[i].id) {
            KeyConlor.list[i] = null;
        }
    }
};

//--判断快捷键是不是重复注册 
KeyConlor.KeyIsOK = function (keyone) {
    for (var i = 0; i < KeyConlor.list.length; i++) {
        if (KeyConlor.list[i].keys == keyone.keys) {
            return false;
        }
    }
    return true;
};


document.onkeydown = function () {

    // 暂时不屏蔽用户手动输入 20130802 by WTang
    //    // 屏蔽掉鼠标输入体检号的功能 
    //    var ID = "txtCustomerID";
    //    if (jQuery('#' + ID) != undefined) {
    //        JudgeIsKeyBoardInputKeyDown();
    //    }

    for (var i = 0; i < KeyConlor.list.length; i++) {
        var keyone = KeyConlor.list[i];
        if (!keyone.isKeydown) continue;
        var control = keyone.keys.split(",")[0];
        switch (control) {
            case 's':
                var code = keyone.keys.split(",").length > 1 ? keyone.keys.split(",")[1] : keyone.keys.split(",")[0];
                if (event.shiftKey == true && event.keyCode == code) {
                    //获得焦点 
                    if (!keyone.isfun) {
                        keyone.dom.getElementById(keyone.id).focus();
                    } else {
                        keyone.fun();
                    }
                    event.keyCode = 0;
                    return false;
                }
                break;
            case 'c':
                var code = keyone.keys.split(",").length > 1 ? keyone.keys.split(",")[1] : keyone.keys.split(",")[0];
                if (event.ctrlKey == true && event.keyCode == code) {
                    //获得焦点 
                    if (!keyone.isfun) {
                        keyone.dom.getElementById(keyone.id).focus();
                    } else {
                        keyone.fun();
                    }
                    event.keyCode = 0;
                    return false;
                }
                break;
            case 'a':
                var code = keyone.keys.split(",").length > 1 ? keyone.keys.split(",")[1] : keyone.keys.split(",")[0];
                if (event.altKey == true && event.keyCode == code) {
                    //获得焦点 
                    if (!keyone.isfun) {
                        keyone.dom.getElementById(keyone.id).focus();
                    } else {
                        keyone.fun();
                    }
                    event.keyCode = 0;
                    return false;
                }
                event.keyCode = 0;
                break;

            default:
                //获得焦点 
                var code = keyone.keys.split(",").length > 1 ? keyone.keys.split(",")[1] : keyone.keys.split(",")[0];
                if (event.keyCode == code && event.altKey == false && event.ctrlKey == false && event.shiftKey == false) {
                    if (!keyone.isfun) {
                        keyone.dom.getElementById(keyone.id).focus();
                    } else {
                        keyone.fun();
                    }
                    event.keyCode = 0;
                    return false;
                }
                break;
        }
    }
};
document.onkeyup = function () {

    // 暂时不屏蔽用户手动输入 20130802 by WTang
    //    // 屏蔽掉鼠标输入体检号的功能
    //    var ID = "txtCustomerID";
    //    if (jQuery('#' + ID) != undefined) {
    //        JudgeIsKeyBoardInputKeyUp();
    //    }

    for (var i = 0; i < KeyConlor.list.length; i++) {
        var keyone = KeyConlor.list[i];
        if (keyone.isKeydown) continue;
        var control = keyone.keys.split(",")[0];
        switch (control) {
            case 's':
                var code = keyone.keys.split(",").length > 1 ? keyone.keys.split(",")[1] : keyone.keys.split(",")[0];
                if (event.shiftKey == true && event.keyCode == code) {
                    //获得焦点 
                    if (!keyone.isfun) {
                        keyone.dom.getElementById(keyone.id).focus();
                    } else {
                        keyone.fun();
                    }
                    event.keyCode = 0;
                    return false;
                }
                break;
            case 'c':
                var code = keyone.keys.split(",").length > 1 ? keyone.keys.split(",")[1] : keyone.keys.split(",")[0];
                if (event.ctrlKey == true && event.keyCode == code) {
                    //获得焦点 
                    if (!keyone.isfun) {
                        keyone.dom.getElementById(keyone.id).focus();
                    } else {
                        keyone.fun();
                    }
                    event.keyCode = 0;
                    return false;
                }
                break;
            case 'a':
                var code = keyone.keys.split(",").length > 1 ? keyone.keys.split(",")[1] : keyone.keys.split(",")[0];
                if (event.altKey == true && event.keyCode == code) {
                    //获得焦点 
                    if (!keyone.isfun) {
                        keyone.dom.getElementById(keyone.id).focus();
                    } else {
                        keyone.fun();
                    }
                    event.keyCode = 0;
                    return false;
                }
                break;

            default:
                //获得焦点 
                var code = keyone.keys.split(",").length > 1 ? keyone.keys.split(",")[1] : keyone.keys.split(",")[0];
                if (event.keyCode == code && event.altKey == false && event.ctrlKey == false && event.shiftKey == false) {
                    if (!keyone.isfun) {
                        keyone.dom.getElementById(keyone.id).focus();
                    } else {
                        keyone.fun();
                    }
                    event.keyCode = 0;
                    return false;
                }
                break;
        }
    }
};

//常用键盘码 
var keyCodeStr = {
    Alt: "a",
    Shift: "s",
    Ctrl: "c",
    Up: "38",
    Down: "40",
    Left: "37",
    Right: "39",
    Esc: "27",
    Enter: "13",
    Backspace: "8",
    Delete: "46",
    Tab: "9",
    CapsLK: "20",
    Space: "32",
    F1: "112",
    F2: "113",
    F3: "114",
    F4: "115",
    F5: "116",
    F6: "117",
    F7: "118",
    F8: "119",
    F9: "120",
    F10: "121",
    F11: "122",
    F12: "123",
    Save: "83",
    Add: "65",
    B: "66",
    Copy: "67",
    Delete: "68",
    CloseWin: "87",
    v: "86",
    Windowx: "91",
    Q: "81"
};
//为套餐搜索注册热键Ctrl+Enter,则用户按下Alt+s键时触发套餐选择的确定事件
KeyConlor.addkeyfun(
keyCodeStr.Ctrl + "," + keyCodeStr.Save,
function () {
    //判断是否显示状态
    if (document.getElementById("showBusFee") != undefined) {
        if (document.getElementById("showBusFee").style.display != "none") {
            if (document.getElementById("btnSure") != undefined) {
                document.getElementById("btnSure").click();
            }
        }
    }
}
, true
);
//为套餐搜索注册热键Ctrl+w,则用户按下Alt+w键关闭搜索框
KeyConlor.addkeyfun(
keyCodeStr.Ctrl + "," + keyCodeStr.CloseWin,
function () {
    if (document.getElementById("showBusFee") != undefined) {
        document.getElementById("showBusFee").style.display = "none";
    }
    //    if (document.getElementById("externTBlList") != undefined) {
    //        document.getElementById("showBusFee").style.display = "none";
    //    }
}
, true
);
//为批量操作注册热键Ctrl+d,则用户按下Alt+d键激发批量删除
KeyConlor.addkeyfun(
keyCodeStr.Ctrl + "," + keyCodeStr.Delete,
function () {
    if (document.getElementById("btnBatchDelete") != undefined) {
        document.getElementById("btnBatchDelete").click();
    }
}
, true
);
//为批量操作注册热键Ctrl+a,则用户按下Alt+a键激发批量新增
KeyConlor.addkeyfun(
keyCodeStr.Ctrl + "," + keyCodeStr.Q,
function () {
    if (document.getElementById("btnBatchAdd") != undefined) {
        document.getElementById("btnBatchAdd").click();
    }
}
, true
);

//为批量操作注册热键Esc,则用户按下Esc键激发批量新增
KeyConlor.addkeyfun(
keyCodeStr.Esc,
function () {
    //经讨论，系统将屏蔽原有的Esc系统功能，在使用Esc键时，只默认关闭收费项目列表
    if (jQuery("#showBusFeeItem").length > 0) {
        try {
            jQuery("#showBusFeeItem").empty();
            jQuery("#showBusFee").hide();
        }
        catch (e)
        { }
    }
}
, true
);

//注册到底部的快捷方式
KeyConlor.addkeyfun(
keyCodeStr.Ctrl + "," + keyCodeStr.Down,
function () {
    GotoBottom();
}
, true
);

//注册到顶部的快捷方式
KeyConlor.addkeyfun(
keyCodeStr.Ctrl + "," + keyCodeStr.Up,
function () {
    GotoTop();
}
, true
);

//热键 体检号/身份证 输入框获取焦点 F2
KeyConlor.addkeyfun(
keyCodeStr.F2,
function () {
    //体检号输入框获取焦点
    if (document.getElementById("txtCustomerID") != null) {
        if (jQuery("#txtCustomerID").attr("disabled") != "disabled") {
            jQuery("#txtCustomerID").focus();
            jQuery("#txtCustomerID").select();
        }
    }
    //体检号、身份证号获取焦点(个人登记列表页面的体检号文本框)
    if (document.getElementById("txtSFZ") != null) {
        if (jQuery("#txtSFZ").attr("disabled") != "disabled") {
            jQuery("#txtSFZ").focus();
            jQuery("#txtSFZ").select();
        }
    }
    //证件号(登记预约页面的证件号文本框)
    if (document.getElementById("txtSearchX") != null) {
        if (jQuery("#txtSearchX").attr("disabled") != "disabled") {
            jQuery("#txtSearchX").focus();
            jQuery("#txtSearchX").select();
        }
    }

    //体检号(收费页面体检号文本框)
    if (document.getElementById("txtID_Customer") != null) {
        if (jQuery("#txtID_Customer").attr("disabled") != "disabled") {
            jQuery("#txtID_Customer").focus();
            jQuery("#txtID_Customer").select();
        }
    }
}
, true
);

//热键 结论词输入框获取焦点 F4
//热键 查询按钮 F4
KeyConlor.addkeyfun(
keyCodeStr.F4,
function () {
    //查询按钮
    if (document.getElementById("btnSearch") != null) {
        if (jQuery("#btnSearch").is(":visible") && jQuery("#btnSearch").attr("disabled") != "disabled") {
            document.getElementById("btnSearch").click();
        }
    }
    //    //查询按钮（登记页面的检索按钮）
    //    if (document.getElementById("btnSearchX") != null) {
    //        if (jQuery("#btnSearchX").is(":visible") && jQuery("#btnSearchX").attr("disabled") != "disabled") {
    //            document.getElementById("btnSearchX").click();
    //        }
    //    }
    //查询按钮（收费退费页面的检索按钮）
    if (document.getElementById("btnSearchCustomer") != null) {
        if (jQuery("#btnSearchCustomer").is(":visible") && jQuery("#btnSearchCustomer").attr("disabled") != "disabled") {
            document.getElementById("btnSearchCustomer").click();
        }
    }
}
, true
);

//热键 刷新
KeyConlor.addkeyfun(
keyCodeStr.F5,
function () {
    DisposeVideoCapture(); //在加载页面之前判断是否开启采集卡资源，有则关闭

    location.replace(location.href);
}
, true
);

//热键 总检 汇总 
//热键 分检 汇总
KeyConlor.addkeyfun(
keyCodeStr.F6,
function () {
    //总检热键注册 (汇总)
    if (document.getElementById("ButCollect") != null) {
        if (jQuery("#ButCollect").attr("disabled") != "disabled") {
            document.getElementById("ButCollect").click();
        }
    }
    //    //分检热键注册 (汇总)
    //    if (document.getElementById("ButCollect") != null) {
    //        if (jQuery("#ButCollect").attr("disabled") != "disabled") {
    //            document.getElementById("ButCollect").click();
    //        }
    //    }
}
, true
);

//热键 分检 保存
//热键 登记列表 申请 
KeyConlor.addkeyfun(
keyCodeStr.F7,
function () {
    //    //分检热键注册 (保存)
    //    if (document.getElementById("ButSave") != null) {
    //        if (jQuery("#ButSave").attr("disabled") != "disabled") {
    //            document.getElementById("ButSave").click();
    //        }
    //    }
    //登记列表热键注册 (申请)
    if (document.getElementById("btnAdd") != null) {
        if (jQuery("#btnAdd").attr("disabled") != "disabled") {
            document.getElementById("btnAdd").click();
        }
    }

    //结论词输入框获取焦点
    if (document.getElementById("txtConclusionInputCode") != null) {
        if (jQuery("#txtConclusionInputCode").is(":visible") && jQuery("#txtConclusionInputCode").attr("disabled") != "disabled") {
            jQuery("#txtConclusionInputCode").focus();
            jQuery("#txtConclusionInputCode").select();
        }
    }

    //分检 采集图像 btnUploadImg
    if (jQuery("#btnSaveImg").is(":visible") && document.getElementById("btnSaveImg") != null) {
        if (jQuery("#btnSaveImg").attr("disabled") != "disabled") {
            document.getElementById("btnSaveImg").click();
        }
    }
}
, true
);

//热键 总检提交 
//热键 总审 审核通过
//热键 分检提交 
//热键 登记预约 读证件
KeyConlor.addkeyfun(
keyCodeStr.F8,
function () {
    //总检 热键注册 (提交)
    if (document.getElementById("ButSave") != null) {
        if (jQuery("#ButSave").is(":visible") && jQuery("#ButSave").attr("disabled") != "disabled") {
            // document.getElementById("ButSave").click();
            // 通过“分科解锁”按钮判断是否是总检页面
            if (document.getElementById("ButUnLock01") != null) {
                SaveCustomerFinalConclusionConfirm(1); // 总检提交确认操作
            }
        }
    }

    // 通过“解除总检”按钮判断是否是总审页面
    if (document.getElementById("ButUnLockFinalCheck1") != null) {
        //总审 热键注册 (审核通过)
        if (document.getElementById("ButChecked") != null) {
            if (jQuery("#ButChecked").is(":visible") && jQuery("#ButChecked").attr("disabled") != "disabled") {
                document.getElementById("ButChecked").click();
            }
        }
    }

    //分检 热键注册 (提交)
    if (document.getElementById("ButCheck") != null) {
        if (jQuery("#ButCheck").is(":visible") && jQuery("#ButCheck").attr("disabled") != "disabled") {
            document.getElementById("ButCheck").click();
        }
    }
    //登记预约 热键注册 (读证件)
    if (document.getElementById("btnReadFromMachine") != null) {
        if (jQuery("#btnReadFromMachine").is(":visible") && jQuery("#btnReadFromMachine").attr("disabled") != "disabled") {
            document.getElementById("btnReadFromMachine").click();
        }
    }
    //收费 热键注册 (收费)
    if (document.getElementById("btnCharge") != null) {
        if (jQuery("#btnCharge").is(":visible") && jQuery("#btnCharge").attr("disabled") != "disabled") {
            document.getElementById("btnCharge").click();
        }
    }
    //退费 热键注册 (退费)
    if (document.getElementById("btnUnCharge") != null) {
        if (jQuery("#btnUnCharge").is(":visible") && jQuery("#btnUnCharge").attr("disabled") != "disabled") {
            document.getElementById("btnUnCharge").click();
        }
    }
}
, true
);

//热键 总审 审核不通过
//热键 预约登记 完成
KeyConlor.addkeyfun(
keyCodeStr.F9,
function () {

    // 通过“解除总检”按钮判断是否是总审页面
    if (document.getElementById("ButUnLockFinalCheck1") != null) {
        //总审热键注册 (审核不通过)
        if (document.getElementById("ButUnCheck") != null) {
            if (jQuery("#ButUnCheck").attr("disabled") != "disabled") {
                document.getElementById("ButUnCheck").click();
            }
        }
    }
    //预约登记 (完成)
    if (document.getElementById("btnRegiste") != null) {
        if (jQuery("#btnRegiste").attr("disabled") != "disabled") {
            document.getElementById("btnRegiste").click();
        }
    }
    //分检 上传图像 
    if (jQuery("#btnUploadImg").is(":visible") && document.getElementById("btnUploadImg") != null) {
        if (jQuery("#btnUploadImg").attr("disabled") != "disabled") {
            document.getElementById("btnUploadImg").click();
        }
    }
}
, true
);


//热键 总检 预览
KeyConlor.addkeyfun(
keyCodeStr.F10,
function () {
    //总检热键注册 (预览)
    if (document.getElementById("ButReprotPreview") != null) {
        if (jQuery("#ButReprotPreview").attr("disabled") != "disabled") {
            document.getElementById("ButReprotPreview").click();
        }
    }
    //报告预览页面 (预览)
    if (jQuery("#btnReportPreview").is(":visible") && document.getElementById("btnReportPreview") != null) {
        if (jQuery("#btnReportPreview").attr("disabled") != "disabled") {
            document.getElementById("btnReportPreview").click();
        }
    }
    //分检 缺省
    if (jQuery("#ButDefault").is(":visible") && document.getElementById("ButDefault") != null) {
        if (jQuery("#ButDefault").attr("disabled") != "disabled") {
            document.getElementById("ButDefault").click();
        }
    }
}
, true
);

/*******************************热键   End**********************************************/

function ReplaceSelectText() {
    if (window.getSelection) {
        window.getSelection().deleteFromDocument();
    }
    else if (document.selection && document.selection.createRange) {
        document.selection.createRange().text = "";
    }
}

/// <summary>
/// 获取当前选中值
/// </summary>
function GetSelectionText() {
    if (window.getSelection) {
        return window.getSelection().toString();
    }
    else if (document.selection && document.selection.createRange) {
        return document.selection.createRange().text;
    }
}
/// <summary>
/// 设置当前选中值为指定值
/// </summary>
function SetSelectText(text) {
    if (window.getSelection) {
        window.getSelection().deleteFromDocument();
    }
    else if (document.selection && document.selection.createRange) {
        document.selection.createRange().text = text;
    }
}


/*******************************屏蔽掉鼠标输入体检号的功能 start **********************************************/
var lastInputTime = new Date();
var currInputTime = new Date();
// 判断是否是键盘输入的字符
function JudgeIsKeyBoardInputKeyDown() {
    lastInputTime = new Date();
}

function JudgeIsKeyBoardInputKeyUp() {

    var curEvent = window.event || e;
    try {
        if (curEvent.srcElement.id != "txtCustomerID") {
            return;
        }
    } catch (e) {
        return;
    }

    currInputTime = new Date();
    var time01 = parseInt(lastInputTime.getTime());
    var time02 = parseInt(currInputTime.getTime());
    var times = time02 - time01;

    if ((curEvent.ctrlKey) && (curEvent.keyCode == 86)) { // 如果是粘贴，则直接返回（不允许粘贴）

        jQuery("#" + curEvent.srcElement.id).val("");

        return false;
    }
    // 如果按键响应时间大于25毫秒，则认为是手动输入的，直接进行过滤掉。
    if (times > 25) {

        jQuery("#" + curEvent.srcElement.id).val("");

        return false;
    }

}


/*******************************屏蔽掉鼠标输入体检号的功能 end **********************************************/

/// <summary>
/// 关闭弹出窗口
/// </summary>
function CloseDialogWindow() {
    parent.art.dialog.get('OperWindowFrame').close();
}

/// <summary>
/// 获取页面地址参数
/// </summary>
function GetQueryString(sProp) {
    var re = new RegExp(sProp + "=([^\\&;]*)", "i");
    var a = re.exec(document.location.search);
    if (a == null)
        return "";
    return a[1];
}

/// <summary>
/// 保存当前访问的页面的查询参数（主要针对查询列表页面）
/// </summary>
function SetUserCurrentQueryParams(PageTag, ParamsArgArray) {

    var exp = new Date(); // 设置当前时间，只有当天才有效 （在取出时，先判断时间是否一致。）
    var CurrentDate = exp.getFullYear() + "-" + exp.getMonth() + '-' + exp.getDay();
    // 拼接的字符串，最后一个为当前日期，在取回的时候，需要做验证，如果是今天设置的，则在页面加载时进行匹配，否则为系统默认配置
    var ParamsArgStrs = "";
    var argCount = 0;
    var ParamsArgArrayLength = 0;

    for (var i = 0; i < ParamsArgArray.length; i++) {
        ParamsArgStrs = ParamsArgStrs + ParamsArgArray[i] + "；";
    }
    ParamsArgStrs = ParamsArgStrs + CurrentDate;
    ParamsArgStrs = encodeURIComponent(ParamsArgStrs);
    SetCookie(PageTag, ParamsArgStrs);

}


/// <summary>
/// 保存当前访问的页面的查询参数（主要针对查询列表页面）
/// </summary>
function GetUserCurrentQueryParams(PageTag) {

    // 拼接的字符串，第一个字符为当前日期，在取回的时候，需要做验证，如果是今天设置的，则在页面加载时进行匹配，否则为系统默认配置
    var ParamsArgStrs = GetCookie(PageTag);
    ParamsArgStrs = decodeURIComponent(ParamsArgStrs);

    var ParamsArgArray = ParamsArgStrs.split("；");


    var exp = new Date(); // 设置当前时间，只有当天才有效 （在取出时，先判断时间是否一致。）
    var CurrentDate = exp.getFullYear() + "-" + exp.getMonth() + '-' + exp.getDay();
    var SaveDate = "";
    if (ParamsArgArray != null && ParamsArgArray != undefined) {
        if (ParamsArgArray.length > 0) {
            SaveDate = ParamsArgArray[ParamsArgArray.length - 1];
        }
    }
    if (SaveDate == CurrentDate) {
        return ParamsArgArray;
    } else {
        return null;
    }
}


/// <summary>
/// 保存当前访问的子页面地址
/// </summary>
function SetUserRedirectUrl(url) {

    SetCookie('FYHCurrentUrl', url);
    var exp = new Date(); // 设置当前时间，只有当天才有效 （在取出时，先判断时间是否一致。）
    var CurrentDate = exp.getFullYear() + "-" + exp.getMonth() + '-' + exp.getDay();
    SetCookie('FYHSaveDate', CurrentDate);
    SetCookie('FYHUserID', jQuery("#CookieUserID").val()); // 写入当前用户ID
}

/// <summary>
/// 获取当前页面地址，并进行加载
/// </summary>
function GetUserRedirectUrl() {

    var FYHCurrentUrl = GetCookie('FYHCurrentUrl'); // 获取Url
    var FYHSaveDate = GetCookie('FYHSaveDate');     // 获取当前日期
    var FYHSaveMenuID = GetCookie('FYHSaveMenuID'); // 获取当前选择的菜单分类ID

    var FYHSubMenuID = GetCookie('FYHSubMenuID');       // 获取当前的子菜单ID
    var FYHSubSectionID = GetCookie('FYHSubSectionID'); // 获取当前的子菜单科室ID（如果不是分检，则该值为空）


    var FYHUserID = GetCookie('FYHUserID');       // 获取上次保存的UserID
    // 如果上次保存的CookieUserID不为空，且两个值不一致的情况，则不用加载相应的连接。 20130905 by WTang
    if (FYHUserID != null && FYHUserID != undefined && FYHUserID != "" && jQuery.trim(jQuery("#CookieUserID").val()) != jQuery.trim(FYHUserID)) {
        return;
    }

    var exp = new Date(); // 设置当前时间，只有当天才有效 （在取出时，先判断时间是否一致。）
    var CurrentDate = exp.getFullYear() + "-" + exp.getMonth() + '-' + exp.getDay();

    // 1、如果cookie 设置的时间，与当前时间是同一天
    // 2、如果同时当前地址不为空，则加载当前地址。

    if (FYHCurrentUrl != null && FYHCurrentUrl != undefined && FYHSaveDate != undefined && FYHSaveDate != null) {
        if (FYHCurrentUrl != "" && CurrentDate == FYHSaveDate) {
            if (FYHSaveMenuID != "" && FYHSaveMenuID != null) {

                // 循环菜单分类
                jQuery(gLoginUserMenuClass).each(function (k, menuclassitem) {
                    jQuery("#LoginUserMenuClassTitle_" + menuclassitem.ID_Menu).removeClass("selected");    // 标题的选中效果取消
                    jQuery("#LoginUserMenuClassSubItem_" + menuclassitem.ID_Menu).hide();                   // 隐藏对应的菜单子项
                });
                jQuery("#LoginUserMenuClassTitle_" + FYHSaveMenuID).addClass("selected");   // 选中当前点击的标题
                jQuery("#LoginUserMenuClassSubItem_" + FYHSaveMenuID).show();               // 显示当前点击菜单子项
                jQuery("#loadForm").html("<div style='height:500px;'>&nbsp;</div>");        // 当切换分组的时候，清空页面内容区域加载的数据

                jQuery("#menu_" + FYHSubMenuID + "_" + FYHSubSectionID).attr("class", "selected");   // 设置当前点击的菜单为选中状态
            }
            DoLoad(FYHCurrentUrl);
        }
    }

}

/// <summary>
/// 设置cookie
/// </summary>
function SetCookie(name, value) {
    var exp, y, m, d;
    exp = new Date();
    exp = new Date(exp.getFullYear() + 1, exp.getMonth(), exp.getDay());
    document.cookie = name + "=" + escape(value) + "; expires=" + exp.toGMTString() + "; path=/";
}

/// <summary>
/// 获取cookie值
/// </summary>
function GetCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]);
    return null;
}

/// <summary>
/// 显示或者隐藏列表区域
/// 该功能为指定name=ToggerMe的元素绑定点击事件，并显示和隐藏其下一个同级元素div
/// </summary>
function ShowMe() {
    jQuery("div [name='ToggerMe']").bind("click", function () {
        if ($(this).parent().next().is(":visible")) {
            $(this).parent().next().hide();
            $(this).attr("title", "点击显示列表");
        }
        else {
            $(this).parent().next().show();
            $(this).attr("title", "点击隐藏列表");
        }
    });

    jQuery("div [name='ToggerMe']").next().bind("click", function () {
        jQuery(this).prev().parent().next().show();
        jQuery(this).prev().parent().prev().find("[name='ToggerMe']").attr("title", "点击隐藏列表");
    });

    jQuery("div [name='ToggerMe']").css({ cursor: "pointer" }); //设置手型选择分组标题     
    jQuery("div [name='ToggerMe']").attr("title", "点击隐藏列表");
    jQuery("div [name='ToggerMe'][allowtogger='1']").attr("title", "点击显示列表");
    jQuery("div [name='ToggerMe'][allowtogger='1']").parent().next().hide(); //默认不显示列表
}

/// <summary>
/// 显示正在加载的进度条(默认文字)
/// </summary>
function ShowLoadingProcessBarDefault(flag) {
    ShowLoadingProcessBar(flag, "", "");
}
/// <summary>
/// 显示正在加载的进度条
/// </summary>
function ShowLoadingProcessBar(flag, title, info) {
    if (title == "") {
        title = "提示信息";
    }
    if (info == "") {
        info = "正在加载数据，请稍后...";
    }
    if (flag == 1) {
        if (IsShowProcessBar) {
            var top = (document.documentElement.scrollTop + window.screen.availHeight / 2 - 150) + "px";
            var left = (document.documentElement.scrollLeft + window.screen.availWidth / 2 - 100) + "px";
            document.getElementById("ProcessLoading").style.top = top;
            document.getElementById("ProcessLoading").style.left = left;
            jQuery("#ProcessLoading").show();
            //ShowCover(); // 显示遮罩层
        }
        else {
            jQuery("#ProcessLoading").hide();
        }
    } else {
        jQuery("#ProcessLoading").hide();
        //jQuery("#cover").hide(); // 隐藏遮罩层
    }
    //IsShowProcessBar = true;
}

/// <summary>
/// 显示遮罩层
/// </summary>
function ShowCover() {
    var cover = document.getElementById("cover");
    cover.style.width = document.documentElement.scrollWidth + "px";
    cover.style.heigh = document.documentElement.scrollHeigh + "px";
    cover.style.display = "block";
}
/// <summary>
/// 发送退出重新登录消息 xmhuang 2013-12-30
/// </summary>
function SendLogOutMessage() {
    var LoginName = jQuery("#CookieLoginName").val();
    var UserName = jQuery("#CookieUserName").val();
    return Wait(LoginName, UserName, "LOGOUT", "INFO");
}
var ShowTime = 1; //消息显示时常 默认1分钟
var LogoutTime = 3; //退出系统消息显示时常，默认3分钟
/// <summary>
/// 发送退出消息 xmhuang 2013-12-31
/// </summary>
function SendExitMessage() {
    var LoginName = jQuery("#CookieLoginName").val();
    var UserName = jQuery("#CookieUserName").val();
    //return Wait(LoginName, UserName, "EXIT", "EXIT");
    var SessionID = jQuery("#CookieSessionID").val();
    var LoginName = jQuery("#CookieLoginName").val();
    var UserName = jQuery("#CookieUserName").val();
    Wait(SessionID, LoginName, "EXIT", "EXIT");
    //SendMessage("EXIT", ShowTime, LogoutTime, SessionID, UserName, "SERVER", "EXIT");
}
/// <summary>
/// 关闭窗口（安全退出）
///IsFromLogin:是否从登录调用
/// </summary>
function CloseWindow(IsFromLogin) {
    var msg = "你确定要退出系统吗？";

    if (msg != "") {
        art.dialog({
            id: 'artDialogID',
            lock: true,
            fixed: true,
            title: '系统消息',
            opacity: 0.3,
            content: msg,
            button: [{
                name: '取消'
            }, {
                name: '确定',
                callback: function () {

                    DisposeVideoCapture(); //在加载页面之前判断是否开启采集卡资源，有则关闭

                    if (IsFromLogin == 1) {
                        window.opener = null;
                        window.open(" ", "_self"); //IE7必须的
                        window.close();
                        return true;
                    }
                    else {
                        return SendExitMessage();
                    }
                }, focus: true
            }]
        });
    }

}


/// <summary>
/// 关闭窗口（安全退出）
/// </summary>
function LogoutSystem() {

    var msg = "你确定要注销登录吗？";

    if (msg != "") {
        art.dialog({
            id: 'artDialogID',
            lock: true,
            fixed: true,
            title: '系统消息',
            opacity: 0.3,
            content: msg,
            button: [{
                name: '取消'
            }, {
                name: '确定',
                callback: function () {
                    SendLogOutMessage();
                    DisposeVideoCapture(); //在加载页面之前判断是否开启采集卡资源，有则关闭
                    location.replace("/Logout.aspx");
                    return true;
                }, focus: true
            }]
        });
    }

}

/// <summary>
/// 从身份证件中读取用户信息  黄兴茂 2013-08-19
/// </summary>
function GetUserInfoByIDCard(IDCard) {
    this.year = '';
    this.month = "";
    this.day = "";
    this.birthday = '';
    this.age = 0;
    this.sex = -1;
    this.sexName = "";
    if (IDCard == undefined || IDCard == "")
        return false;
    var IDCardLegth = IDCard.length;
    var xb = 0;
    var d = new Date();
    if (IDCardLegth == 15) {
        this.year = "19" + IDCard.substr(6, 2);
        this.month = IDCard.substr(8, 2);
        this.day = IDCard.substr(10, 2);
        xb = IDCard.substr(14, 1);
        this.birthday = this.year + '-' + this.month + '-' + this.day;
        if (xb % 2 == 0) {
            this.sex = 2;
            this.sexName = "女";
        }
        else {
            this.sex = 1;
            this.sexName = "男";
        }
        this.age = d.getYear() - 1900 - this.year;
    }
    else if (IDCardLegth == 18) {
        this.year = IDCard.substr(6, 4);
        this.month = IDCard.substr(10, 2);
        this.day = IDCard.substr(12, 2);
        xb = IDCard.substr(16, 1);
        this.birthday = this.year + '-' + this.month + '-' + this.day;
        if (xb % 2 == 0) {
            this.sex = 0;
            this.sexName = "女";
        }
        else {
            this.sex = 1;
            this.sexName = "男";
        }
        this.age = d.getYear() - this.year;
    }
}

/// <summary>
/// 导出Excel通用方法，该方法主要是从新连接中打开需要导出的Excel文件
///XMHuang 2013-09-12 新增
/// </summary>
function OutToExcel() {
    var href = jQuery.trim(jQuery("#loadExcel").attr("href"));
    if (href != "") {
        window.open(href, null, "dialogWidth:420px; dialogHeight:320px; scroll:no; status:no; resizable:no");
    }
    return false;
}

/***************************字符串操作通用脚本 Begin xmhuang 2013-09-24************************************/
var TT = {

    /*
    * 获取光标位置
    * @Method getCursorPosition
    * @param t element
    * @return number
    */
    getCursorPosition: function (t) {
        if (document.selection) {
            t.focus();
            var ds = document.selection;
            var range = ds.createRange();
            var stored_range = range.duplicate();
            stored_range.moveToElementText(t);
            stored_range.setEndPoint("EndToEnd", range);
            t.selectionStart = stored_range.text.length - range.text.length;
            t.selectionEnd = t.selectionStart + range.text.length;
            return t.selectionStart;
        } else return t.selectionStart
    },


    /*
    * 设置光标位置
    * @Method setCursorPosition
    * @param t element
    * @param p number
    * @return
    */
    setCursorPosition: function (t, p) {
        this.sel(t, p, p);
    },

    /*
    * 插入到光标后面
    * @Method add
    * @param t element
    * @param txt String
    * @return
    */
    add: function (t, txt) {
        var val = t.value;
        if (document.selection) {
            t.focus()
            document.selection.createRange().text = txt;
        } else {
            var cp = t.selectionStart;
            var ubbLength = t.value.length;
            var s = t.scrollTop;
            // document.getElementById('aaa').innerHTML += s + '<br />';
            t.value = t.value.slice(0, t.selectionStart) + txt + t.value.slice(t.selectionStart, ubbLength);
            this.setCursorPosition(t, cp + txt.length);
            // document.getElementById('aaa').innerHTML += t.scrollTop + '<br />';
            firefox = navigator.userAgent.toLowerCase().match(/firefox\/([\d\.]+)/) && setTimeout(function () {
                if (t.scrollTop != s) t.scrollTop = s;
            }, 0)
        };
    },


    /*
    * 删除光标 前面或者后面的 n 个字符
    * @Method del
    * @param t element
    * @param n number  n>0 后面 n<0 前面
    * @return
    * 重新设置 value 的时候 scrollTop 的值会被清0
    */
    del: function (t, n) {
        var p = this.getCursorPosition(t);
        var s = t.scrollTop;
        var val = t.value;
        t.value = n > 0 ? val.slice(0, p - n) + val.slice(p) :
      val.slice(0, p) + val.slice(p - n);
        this.setCursorPosition(t, p - (n < 0 ? 0 : n));
        firefox = navigator.userAgent.toLowerCase().match(/firefox\/([\d\.]+)/) && setTimeout(function () {
            if (t.scrollTop != s) t.scrollTop = s;
        }, 10)
    },

    /*
    * 选中 s 到 z 位置的文字
    * @Method sel
    * @param t element
    * @param s number
    * @param z number
    * @return
    */
    sel: function (t, s, z) {
        if (document.selection) {
            var range = t.createTextRange();
            range.moveEnd('character', -t.value.length);
            range.moveEnd('character', z);
            range.moveStart('character', s);
            range.select();
        } else {
            t.setSelectionRange(s, z);
            t.focus();
        }
    },


    /*
    * 选中一个字符串
    * @Method sel
    * @param t element
    * @param s String
    * @return
    */
    selString: function (t, s) {
        var index = t.value.indexOf(s);
        index != -1 ? this.sel(t, index, index + s.length) : false;
    }
}
/***************************字符串操作通用脚本 End xmhuang 2013-09-24************************************/

/// 判断是否按下回车按键
function IsEnterKeyDown() {
    var curEvent = window.event || e;
    var id = document.activeElement.id;

    if (curEvent.keyCode == 13) {
        return true;
    }
    return false;
}

/// 判断是否按下ESC键
function IsEscKeyDown() {
    var curEvent = window.event || e;
    var id = document.activeElement.id;

    if (curEvent.keyCode == 27) {
        return true;
    }
    return false;
}


/***************************接口信息发送 Begin xmhuang 2013-10-12************************************/
/// <summary>
/// 发送体检号信息到LES，PACS接口 xmhuang 2013-10-12
/// </summary>
function SendWaitToInterfaceByClient_Ajax(Forsex, ID_Customer) {
    if (Forsex == 1 || Forsex == "男") {
        Forsex = 1;
    }
    else if (Forsex == 2 || Forsex == "女") {
        Forsex = 0;
    }

    //ajax请求：由于字符在get请求中超长，这里必须使用post方式提交请求
    jQuery.ajax({
        type: "POST",
        cache: false,
        url: "/Ajax/AjaxRegiste.aspx",
        data: { Forsex: Forsex, ID_Customer: ID_Customer, action: 'SendWaitToInterfaceByClient' },
        dataType: "json",
        success: function (msg) {

        },
        complete: function () {

        }
    });
}
/***************************接口信息发送 End xmhuang 2013-10-12************************************/

/***************************模拟队列类 Begin xmhuang 2013-10-20************************************/

var Qu = {}; //定义一个对象，用于
//队列构造函数 先进先出
Qu.Queue = function (len) {
    this.capacity = len; //设置队列最大容量
    this.list = new Array(); //队列数据
};
//添加入队方法
Qu.Queue.prototype.enqueue = function (data) {
    if (data == null) return;
    if (data == undefined) return;
    if (this.list.length >= this.capacity) {
        this.list.remove(0);
    }
    this.list.push(data);
};

//添加出队方法
Qu.Queue.prototype.dequeue = function (data) {
    if (this.list == null) return;
    if (this.list.length == 0) return;
    this.list.remove(0);
};

//添加获取队列长度方法
Qu.Queue.prototype.size = function () {
    if (this == null) return;
    return this.list.length;
};

//添加获取队列是否为空方法
Qu.Queue.prototype.isEmpty = function () {
    if (this == null || this.list == null) return false;
    return this.list.length > 0;
};

//对象数组扩展属性
Array.prototype.remove = function (dx) {
    if (isNaN(dx) || dx > this.length) {
        return false;
    }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[i] != this[dx]) {
            this[n + 1] = this[i];
        }
    }
    this.length -= 1;
};


/// <summary>
/// 获取元素自身的html代码 20140103 by wtang
/// </summary>
jQuery.fn.outerHTML = function (s) {
    return (s) ? this.before(s).remove() : jQuery("<p>").append(this.eq(0).clone()).html();
}


/***************************模拟队列类 End xmhuang 2013-10-20************************************/


/// <summary>
/// 刷新当前页面
/// </summary>
function RefreshCurrentPage() {
    DisposeVideoCapture();
    location.replace('/System/Index.aspx');
}

/// <summary>
/// 释放掉视频资源 xmhuang 2013-10-29
/// </summary>
function DisposeCamera() {
    if (document.getElementById("TakePhoto") != undefined || document.getElementById("TakePhoto") != null) {
        try {
            document.getElementById("TakePhoto").CloseCam();
            document.getElementById("TakePhoto").Exit();
        }
        catch (e)
       { }
    }
}



/// <summary>
/// 释放掉采集卡资源 Wtang 2013-12-24
/// </summary>
function DisposeVideoCapture() {
    ComClose();
    var VideoCapture = null;

    if (VideoCapture == null || VideoCapture == undefined) {

        if (window.top.document.getElementById("FrameVideoCapture") == null) { return; }

        var IFrameVideoCapture = window.top.document.getElementById("FrameVideoCapture").contentWindow;
        VideoCapture = IFrameVideoCapture.document.getElementById("VideoCapture");                     //获取采集卡插件
    }

    if (VideoCapture != null && VideoCapture != undefined) {
        try {
            VideoCapture.StopVideoDisplay();
            VideoCapture.CloseVideoDisplay();
        } catch (e) { }
    }
}


/// <summary>
/// 获取服务器信息，目前只返回了服务器当前时间 xmhuang 2013-12-25
/// </summary>
function GetServerInfo() {
    var allServerInfo = "";
    jQuery.ajax({
        type: "POST",
        async: false,
        url: "/Ajax/AjaxRegiste.aspx",
        data: { action: "GetServerInfo" },
        contentType: "application/x-www-form-urlencoded;Content-length=1024000",
        cache: false,
        dataType: "json",
        success: function (msg) {
            allServerInfo = msg;
        }
    });
    return allServerInfo;
}



function InitComRead() {
//    var divComReadContent01 = "<object id='ComReadObject' classid='clsid:88D76990-883F-4678-9B08-CDF13021373B'></object>";
//    var divComReadContent02 = "<script type='text/javascript' language='javascript'>function ComReadObject::SendComRecvDataEvent(x,y,z){ ShowComReadData(x,y,z); }<//script>";
//    jQuery("#divComRead").html(divComReadContent01);
//    jQuery("#divComReadScript").html(divComReadContent02);
    OpenRecvComData();
}

function ShowComReadData(type, data) {
    //alert(type + "_" + data);
    if (type == 1) {
        var dataArray = data.split("|");
        if (dataArray.length > 2 && dataArray[0] != "0" && dataArray[0] != "") {
            if (jQuery("#txtSym_2295_103") != undefined) {
                if (jQuery("#txtSym_2295_103").val() == "") {
                    jQuery("#txtSym_2295_103").val(dataArray[0]);
                    jQuery("#txtSym_2295_103").focus();
                }
                if (jQuery("#txtSym_2295_104").val() == "") {
                    jQuery("#txtSym_2295_104").val(dataArray[1]);
                    jQuery("#txtSym_2295_104").focus();
                }
            }
        }
    }
    else if (type == 2) {
        var dataArray = data.split("|");
        if (dataArray.length > 2 && dataArray[0] != "0" && dataArray[0] != "") {
            if (jQuery("#txtSym_2295_103") != undefined) {
                if (jQuery("#txtSym_2295_6057").val() == "") {
                    jQuery("#txtSym_2295_6057").val(dataArray[0]);
                    jQuery("#txtSym_2295_6057").focus();
                }
                if (jQuery("#txtSym_2295_101").val() == "") {
                    jQuery("#txtSym_2295_101").val(dataArray[1]);
                    jQuery("#txtSym_2295_101").focus();
                }
                if (jQuery("#txtSym_2295_102").val() == "") {
                    jQuery("#txtSym_2295_102").val(dataArray[2]);
                    jQuery("#txtSym_2295_102").focus();
                }
            }
        }
    }
}

function OpenRecvComData() {

    var PortNumber01 = GetCookie('PortNumber01');
    if (PortNumber01 == undefined || PortNumber01 == null || PortNumber01 == "") {
        ShowSystemDialog("未设置串口参数或参数丢失，设置参数才能自动采集【血压计】和【身高体重】仪器的数据！");
        return;
    }

    var PortNumber01 = GetCookie('PortNumber01');         // 血压 端口号
    var Baudrate01 = GetCookie('Baudrate01');           // 血压 波特率
    var DataBit01 = GetCookie('DataBit01');             // 血压 数据位
    var CheckBit01 = GetCookie('CheckBit01');           // 血压 校验位
    var StopBit01 = GetCookie('StopBit01');             // 血压 停止位

    ComReadObject.SetComParam(PortNumber01, Baudrate01, DataBit01, CheckBit01, StopBit01, 1);
    ComReadObject.GetComRecvData();
    jQuery("#txtSym_2295_103").focus();
}
// 
function ChangeRecvComData(type) {

    var PortNumber01 = GetCookie('PortNumber01');
    if (PortNumber01 == undefined || PortNumber01 == null || PortNumber01 == "") {
        ShowSystemDialog("未设置串口参数或参数丢失，设置参数才能自动采集【血压计】和【身高体重】仪器的数据！");
        return;
    }
    var PortNumber01 = GetCookie('PortNumber01');         // 血压 端口号
    var Baudrate01 = GetCookie('Baudrate01');           // 血压 波特率
    var DataBit01 = GetCookie('DataBit01');             // 血压 数据位
    var CheckBit01 = GetCookie('CheckBit01');           // 血压 校验位
    var StopBit01 = GetCookie('StopBit01');             // 血压 停止位

    var PortNumber02 = GetCookie('PortNumber02');       // 身高体重 端口号
    var Baudrate02 = GetCookie('Baudrate02');           // 身高体重 波特率
    var DataBit02 = GetCookie('DataBit02');             // 身高体重 数据位
    var CheckBit02 = GetCookie('CheckBit02');           // 身高体重 校验位
    var StopBit02 = GetCookie('StopBit02');             // 身高体重 停止位

    if (type == "1") {          // 血压
        ComReadObject.SetComParam(PortNumber01, Baudrate01, DataBit01, CheckBit01, StopBit01, 1);
        ComReadObject.GetComRecvData();
        jQuery("#txtSym_2295_103").focus();
    } else if (type == "2") {   // 身高体重
        ComReadObject.SetComParam(PortNumber02, Baudrate02, DataBit02, CheckBit02, StopBit02, 2);
        ComReadObject.GetComRecvData();
        jQuery("#txtSym_2295_101").focus();
    }
}

/// <summary>
/// 关闭Com端口
/// </summary>
function ComClose() {
    try {
        var ComReadInterface = document.getElementById("ComReadObject");
        if (ComReadInterface != undefined && ComReadInterface != null) {
            ComReadInterface.ComClose();
        }
    } catch (e) { }
}



/// <summary>
/// 获取两个时间相差天数 xmhuang 2014-01-14 -1:开始时间小于结束时间 -2:结束日期格式不正确 -3:开始日期格式不正确
/// </summary>
function GetTimeSpanOfDay(BeginTime, EndTime) {
    //判断日期格式是否正确
    var newBeginTime = new Date(BeginTime.replace("-", "/")); // new Date(bYear, bMonth, bDay);
    if (isNaN(newBeginTime)) {
        return -3;
    }
    var newEndTime = new Date(EndTime.replace("-", "/")); //new Date(eYear, eMonth, eDay);
    if (isNaN(newEndTime)) {
        return -2;
    }
    var spanDays = newEndTime.getTime() - newBeginTime.getTime(); //Date.parse(newEndTime) - Date.parse(newBeginTime);
    if (spanDays < 0) {
        return -1;
    }
    return spanDays / (1000 * 60 * 60 * 24);
}
/// <summary>
/// 通用日期间隔判断调用接口 xmhuang 2014-01-14
/// </summary>
function CheckTime(BeginTime, EndTime) {
    var spanDays = GetTimeSpanOfDay(BeginTime, EndTime);
    if (spanDays == -1) {
        alert("开始时间不得小于结束时间！");
        return false;
    }
    else if (spanDays == -2) {
        alert("结束日期格式不正确！");
        return false;
    }
    else if (spanDays == -3) {
        alert("开始日期格式不正确！");
        return false;
    }
    else if (spanDays > 35) {
        alert("时间跨度不得大于35天！");
        return false;
    }
    return true;
}