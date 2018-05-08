/************************该页面为预约登记列表页公用脚本******************************/
// 记录读取分页数据操作的次数，用于判断是否进行回调
// 1、只有第1次才调用 jQuery("#Pagination").pagination
// 2、只有第2次及以后的操作才调用回调函数 pageselectCallback 中的 QueryPagesData(page_index );
var IsCommon = ""; //标记个人预约、个人登记、团体登记是否共用此功能界面 xmhuang 2014-03-21
var SynCardOcx1 = "";
var IsTeam = ""                                      //是否团体的标记，该标记从参数中输出到页面隐藏域
var tempOperPageCount = 0;                           //总页数
var tempOldtotalCount = 0;                           //初始总页数，用于判断是否更新页码
var editTitle = "点击进行修改";                      //编辑提示

var defalutImagSrc = "/template/blue2/images/avatar.jpg"; //默认头像

// 记录读取分页数据操作的次数，用于判断是否进行回调
// 1、只有第1次才调用 jQuery("#Pagination").pagination
// 2、只有第2次及以后的操作才调用回调函数 pageselectCallback 中的 QueryPagesData(page_index );
var tempOperPageCount = 0;
var tempOldtotalCount = 0; //初始总页数，用于判断是否更新页码
function pageselectCallback(page_index, jq) {

    if (tempOperPageCount > 0) {
        tempOperPageCount++;
        QueryPagesData(page_index);
    }
    tempOperPageCount++;

    return false;
}

/// <summary>
///分页函数 XMHuang 2013-08-12 添加注释 
///pageIndex:当前页面ID
/// </summary>
jQuery(document).ready(function () {
    //SwitchHeader(1); // 显示通用头部 xmhuang 2014-04-14
    jQuery("#QueryExamListData").attr("data-left", (232 + jQuery("#ShowUserMenuDiv").height()));
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度

    IsCommon = document.getElementById("IsCommon") == null ? 0 : document.getElementById("IsCommon").value;     //获取是否通用参数 xmhuang 2014-03-18
    SynCardOcx1 = document.getElementById("SynCardOcx1");       //获取身份证插件
    IsTeam = document.getElementById("IsTeam") == null ? 0 : document.getElementById("IsTeam").value;           //从隐藏域中获取是否团体参数值 XMHuang 2013-08-12 添加注释

    //如果当前查询的是个人登记、预约则显示团体信息，否则不显示团体信息
    if (IsTeam == 1) {
        jQuery("[name='tdTeam']").show();
        jQuery("#btnAdd").val(" 登 记(F7) ");
    }
    else {
        jQuery("[name='tdTeam']").hide();
        jQuery("#btnAdd").val(" 申 请(F7) ");
    }
    jQuery("#txtSFZ").focus();                                  //设置默认光标 XMHuang 2013-08-12 添加注释

    //GetRegistListParams();                                      // 获取Cookie中存放的登记查询列表页参数

    //QueryPagesData(0);                                          //分页检索预约、登记数据       XMHuang 2013-08-12 添加注释
    ResetSearchInfo("");

});

/// <summary>
///分页函数
///pageIndex:当前页面ID
/// </summary>
function QueryPagesData(pageIndex) {
    optInit = getOptionsFromForm();                             //获取分页配置参数
    var modelName = jQuery("#modelName").val();                 //获取模块名称，该参数在配置连接时设定
    var Subscribed = -1;
    if (modelName.toLowerCase() == "regist")
    { Subscribed = 1; }
    if (modelName.toLowerCase() == "sign")
    { Subscribed = 0; }
    //如果是共用界面,则支持身份证和体检号共用检索功能 xmhuang 2014-03-21
    var curIsTeam = IsTeam;
    if (IsCommon == 1) {
        curIsTeam = -1;
    }
    else {

    }
    var totalCount = 0;                                         //总条数
    var IDCard = jQuery.trim(jQuery('#txtSFZ').val());          //客户名称，这里获取的是客户体检号信息
    var BeginExamDate = jQuery('#BeginExamDate').val();         // 开始日期
    BeginExamDate = encodeURIComponent(BeginExamDate);          //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();             //结束日期
    EndExamDate = encodeURIComponent(EndExamDate);

    var dateTypeName = document.getElementById("slDateType").options[document.getElementById("slDateType").selectedIndex].value; //获取筛选时间类型 xmhuang 2014-03-25
    var OnlyMySelf = jQuery("input[name='chkOnlyMySelf']:checked").val(); // 仅显示我操作的
    var CustomerName = jQuery.trim(jQuery('#txtCustomerName').val());    //姓名
    CustomerName = encodeURIComponent(CustomerName);
    var Is_FeeSettled = document.getElementById("slIs_FeeSettled").options[document.getElementById("slIs_FeeSettled").selectedIndex].value; // xmhuang 2013-11-11 是否收费
    var optInit;
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxRegiste.aspx",
        data: {
            dateTypeName: dateTypeName,
            IsTeam: curIsTeam,
            modelName: modelName,
            pageIndex: pageIndex,
            IDCard: IDCard,
            pageSize: pagePagination.items_per_page,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            OnlyMySelf: OnlyMySelf,
            CustomerName: CustomerName,
            Is_FeeSettled: Is_FeeSettled,
            action: 'GetRegisteCustomerList'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (parseInt(msg.totalCount) > 0) {
                jQuery("#Pagination").show();
                if (tempOperPageCount == 0) {
                    jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
                }
                else if (tempOldtotalCount != msg.totalCount) {
                    jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
                }
                tempOldtotalCount = msg.totalCount;

                var tmpCustomerIDsStr = ""; // 临时记录体检号（逗号分隔的字符串）

                var newcontent = '';
                var templateContent = jQuery("#RegistListTemplate").html();
                if (templateContent == undefined) { return; }
                var rowNum = 1;
                if (pageIndex > 0) {
                    rowNum = optInit.items_per_page * (pageIndex) + 1;
                }
                jQuery(msg.dataList).each(function (i, item) {

                    if (tmpCustomerIDsStr == "") {
                        tmpCustomerIDsStr = item.ID_Customer;
                    } else {
                        tmpCustomerIDsStr = tmpCustomerIDsStr + "," + item.ID_Customer;
                    }

                    //√×
                    if (item.Is_Team == "True") {
                        item.Is_Team = "√";
                    }
                    else {
                        item.Is_Team = "×";
                    }
                    if (item.Is_Subscribed == "1") {
                        item.Is_Subscribed = "√"
                    }
                    else {
                        item.Is_Subscribed = "×"
                    }
                    if (item.Is_FeeSettled == "True") {
                        item.Is_FeeSettled = "√"
                    }
                    else {
                        item.Is_FeeSettled = "×"
                    }
                    //xmhuang 2013-09-27 如果是预约,则操作人显示预约人
                    //                    if (Subscribed == 1) {
                    //                        item.Operator = item.SubScriber;
                    //                    }
                    if (item.Is_GuideSheetPrinted == "False") {
                        item.OperateDate = "";
                    }
                    if (templateContent != null) {
                        if (item.Base64Photo != "") {
                            item.Base64Photo = "data:image/gif;base64," + item.Base64Photo;
                        } else {
                            item.Base64Photo = defalutImagSrc;
                        }

                        newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@ID_ArcCustomer/gi, item.ID_ArcCustomer)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@IDCard/gi, item.IDCard)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@Is_Team/gi, item.Is_Team)
                            .replace(/@Is_Team/gi, item.TeamName)
                            .replace(/@Is_FeeSettled/gi, item.Is_FeeSettled)
                            .replace(/@Is_Subscribed/gi, item.Is_Subscribed)
                            .replace(/@modelName/gi, modelName)
                            .replace(/@editTitle/gi, "点击进入【" + item.CustomerName + "】的登记界面")
                            .replace(/@IsTeam/gi, IsTeam)
                            .replace(/@Operator/gi, item.Operator)
                            .replace(/@SubScribDate/gi, item.SubScribDate)//体检预约日期
                            .replace(/@IsCommon/gi, IsCommon)//xmhuang 2014-03-18新增是否通用模块参数
                            .replace(/@Creator/gi, item.SubScriber + (item.Operator == "" ? "" : " | " + item.Operator))
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@MobileNo/gi, item.MobileNo)

                            .replace(/@Age/gi, item.Age)
                            .replace(/@Base64Photo/gi, item.Base64Photo)
                            .replace(/@RowNum/gi, rowNum);
                        rowNum++;
                    }
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                    SetTableRowStyle();
                    ShowCustomerBigPic();
                    //设置固定表头 xmhuang 2014-04-01
                    //$('#tbCustomerList').tablefix({ width: 920, fixRows: 1, fixCols: 2 });
                }
            } else {
                ResetSearchInfo("");
            }

            // 判断表格是否存在滚动条,并设置相应的样式
            JudgeTableIsExistScroll();

        }
    });
}

function DoLoadX(obj) {
    if (jQuery(obj).attr("targeturl") != "") {
        DoLoad(jQuery(obj).attr("targeturl"), '');
    }
}

/*删除 Begin*/
function DoDel() {

    if (confirm("您确认要删除吗？")) {
        //判断是否有选中项目
        var ItemExamCard = '';
        var ItemArcCustomer = '';
        jQuery("[name='ItemCheckbox']").each(function () {
            if (jQuery(this).attr('checked')) {
                jQuery(this).parent().parent().remove();
                ItemExamCard += "'" + jQuery(this).attr("id") + "',";
                ItemArcCustomer += "'" + jQuery(this).attr("ArcCustomer") + "',";
            }
        });
        var qustData = { action: 'DelData',
            type: "Registe",
            ItemExamCard: ItemExamCard,
            ItemArcCustomer: ItemArcCustomer
        };
        if (ItemExamCard != '') {
            //存储大数据请设置Content-length值
            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxRegiste.aspx",
                data: qustData,
                cache: false,
                contentType: "application/x-www-form-urlencoded;Content-length=1024000",
                dataType: "json",
                success: function (msg) {
                    ShowSystemDialog(msg.Message);
                    if (msg.success == "1") {
                        RegistListSearch();
                    }
                    else {

                    }

                }
            });
        }
    }
    else {
        return false;
    }
}
/*删除 End*/

function RegistListSearch() {
    ResetSearchInfo("正在查询，请稍候...");
    tempOperPageCount = 0;
    QueryPagesData(0); //重新按照新输入的条件进行查询

    SaveRegistListParams(); // 保存登记查询列表页参数

}
var LastLoadID_Customer = "";
function DirectQueryCustomerID() {
    var ErrorMsg = "";
    var ID_Customer = jQuery.trim(jQuery("#txtSFZ").val());
    if (isCustomerExamNo(ID_Customer)) {
        var card = ID_Customer;
        var TypeCode = card.substring(1, 2); //TypeCode:3 个人预约 ,6个人登记 ,9团体登记
        if (IsCommon == 1) {
            var allISDeleteDT = GetCustomerInfo(ID_Customer);
            //判断是否是对应模块的客户(是个人还是团体)
            if (allISDeleteDT == "" || allISDeleteDT.length == 0) {
                ShowSystemDialog("对不起,系统未找到体检号[" + ID_Customer + "]对应的信息!");
                return false;
            }
            else {
                jQuery.ajax({
                    type: "GET",
                    url: "/Ajax/AjaxRegiste.aspx",
                    data: { action: "GetCustomerNumber", ID_Customer: ID_Customer, currenttime: encodeURIComponent(new Date()) },
                    cache: false,
                    dataType: "json",
                    success: function (msg) {
                        if (msg != undefined) {
                            if (msg.nret != undefined) {
                                if (msg.nret > 0) {
                                    if (msg.SecurityLevel >= 100) //客户已加密
                                    {
                                        ShowSystemDialog("对不起，该客户已加密，不允许查看！");
                                        ResetSearchInfo("对不起，该客户已加密，不允许查看！");
                                        return false;
                                    }
                                    else {
                                        var modelName = jQuery("#modelName").val();
                                        var nret = msg.nret;
                                        var Is_Subscribed = msg.Is_Subscribed;
                                        if (Is_Subscribed == 1 || Is_Subscribed == "True") {
                                            modelName = "Regist";
                                        }
                                        else {
                                            modelName = "Sign";
                                        }
                                        var IsTeam = jQuery("#IsTeam").val();
                                        if (IsTeam == null || IsTeam == undefined || IsTeam == "") {
                                            IsTeam = 0;
                                        }
                                        if (LastLoadID_Customer != ID_Customer) {
                                            DoLoad('/System/Customer/RegistOper.aspx?type=Edit&ID_Customer=' + ID_Customer + '&modelName=' + modelName + '&IsTeam=' + IsTeam + '&IsCommon=' + IsCommon, '');
                                            LastLoadID_Customer = ID_Customer;
                                        }
                                        //return false;
                                    }
                                }
                            }
                        }
                        ResetSearchInfo("");
                    }
                });
            }
        }
        else {
            if (TypeCode == 3)//个人预约
            {
                if (IsTeam == 1)//如果当前模块是团体登记模块
                {
                    ErrorMsg = "对不起，体检号[" + card + "]为个人预约客户，请到个人登记处";
                }
                else {

                }
            }
            else if (TypeCode == 6)//个人登记
            {
                if (IsTeam == 1)//如果当前模块是团体登记模块
                {
                    ErrorMsg = "对不起，体检号[" + card + "]为个人登记客户，请到个人登记处";
                }
                else {
                    if (jQuery('#modelName').val().toLowerCase() == "regist") {
                        ErrorMsg = "对不起，体检号[" + card + "]为个人登记客户，请到个人登记处";
                    }
                    else if (jQuery('#modelName').val().toLowerCase() == "sign") {
                    }
                }
            }
            else if (TypeCode == 9)//团体登记
            {
                if (IsTeam == 1)//如果当前模块是团体登记模块
                { }
                else {
                    ErrorMsg = "对不起，体检号[" + card + "]为团体客户，请到团体登记处";
                }
            }
            else {

            }
        }

        if (ErrorMsg != "") {
            ShowSystemDialog(ErrorMsg);
            ErrorMsg = "";
            jQuery("#txtSFZ").val("");
            jQuery("#txtSFZ").focus();
            return false;
        }
        else {
            jQuery.ajax({
                type: "GET",
                url: "/Ajax/AjaxRegiste.aspx",
                data: { action: "GetCustomerNumber", ID_Customer: ID_Customer, currenttime: encodeURIComponent(new Date()) },
                cache: false,
                dataType: "json",
                success: function (msg) {
                    if (msg != undefined) {
                        if (msg.nret != undefined) {
                            if (msg.nret > 0) {
                                if (msg.SecurityLevel >= 100) //客户已加密
                                {
                                    ShowSystemDialog("对不起，该客户已加密，不允许查看！");
                                    ResetSearchInfo("对不起，该客户已加密，不允许查看！");
                                    return false;
                                }
                                else {
                                    var modelName = jQuery("#modelName").val();
                                    var nret = msg.nret;
                                    var Is_Subscribed = msg.Is_Subscribed;
                                    if (Is_Subscribed == 1 || Is_Subscribed == "True") {
                                        modelName = "Regist";
                                    }
                                    else {
                                        modelName = "Sign";
                                    }
                                    var IsTeam = jQuery("#IsTeam").val();
                                    if (IsTeam == null || IsTeam == undefined || IsTeam == "") {
                                        IsTeam = 0;
                                    }
                                    if (LastLoadID_Customer != ID_Customer) {
                                        DoLoad('/System/Customer/RegistOper.aspx?type=Edit&ID_Customer=' + ID_Customer + '&modelName=' + modelName + '&IsTeam=' + IsTeam + '&IsCommon=' + IsCommon, '');
                                        LastLoadID_Customer = ID_Customer;
                                    }
                                    //return false;
                                }
                            }
                        }
                    }
                    ResetSearchInfo("");
                }
            });
        }
    }
    else {
        // ResetSearchInfo("");
    }
}


/// <summary>
/// 查询指定体检号的存档信息及体检基本信息 (查询分页列表的补充信息) 20130721 by WTang 
/// </summary>
function GetCustomerExamListArcPhysicalInfo(CustomerIDsStr) {

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxCustExam.aspx",
        data: { CustomerIDsStr: CustomerIDsStr,
            action: 'GetCustomerExamListArcPhysicalInfo',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {

            if (jsonmsg == null || jsonmsg == "" || parseInt(jsonmsg.totalCount) <= 0)
            { return false; }

            jQuery(jsonmsg.dataList0).each(function (i, onarccustitem) {
                jQuery("#GenderName_" + onarccustitem.ID_Customer).html(onarccustitem.GenderName);
                jQuery("#Age_" + onarccustitem.ID_Customer).html(onarccustitem.Age);
                //jQuery("#IDCard_" + onarccustitem.ID_Customer).html(onarccustitem.IDCard);
                jQuery("#MarriageName_" + onarccustitem.ID_Customer).html(onarccustitem.MarriageName);
                jQuery("#MobileNo_" + onarccustitem.ID_Customer).html(onarccustitem.MobileNo);
            });
        }
    });

}
/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的条件内，没有找到任何客户信息！";
    }
    var html = "<tr><td class='msg' colSpan='160'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
    SetTableRowStyle();
    jQuery("#Pagination").hide(); //隐藏分页控件
}



var str = "";
function FindReaderOfList(objID, Key, IsUpdateCustomerPhoto) {
    this.errorMsg = "";
    var ret = CVR_IDCard.ReadCard();
    if (ret == 0) {
        this.errorMsg = "";
        jQuery("#" + objID).val(CVR_IDCard.CardNo);
    }
    else {
        this.errorMsg += "读取身份信息失败,请您确认身份证已正确安放！";
    }
    if (this.errorMsg != "") {
        ShowSystemDialog(this.errorMsg);
        this.errorMsg = "";
    }
}

function FindReaderOfList_XZX(objID, Key, IsUpdateCustomerPhoto) {
    this.errorMsg = "";
    if (str == '') {
        str = SynCardOcx1.FindReader();
    }
    if (str > 0) {
        if (str > 1000) {
            this.errorMsg = "读卡器连接在USB " + str;
        }
        else {
            this.errorMsg = "读卡器连接在COM " + str;
        }
        var nRet = SynCardOcx1.ReadCardMsg();
        if (nRet == 0) {
            this.errorMsg = "";
            jQuery("#" + objID).val(SynCardOcx1.CardNo);
        }
        else {
            this.errorMsg += ",读取身份信息失败,请您确认身份证已正确安放！";
        }
    }
    else {
        this.errorMsg = "没有找到读卡器";

    }
    if (this.errorMsg != "") {
        ShowSystemDialog(this.errorMsg);
        this.errorMsg = "";
    }
}

function GetCustomerInfo(ID_Customer) {
    var allISDeleteDT = "";
    var flag = false;
    jQuery.ajax({
        type: "GET",
        async: false,
        url: "/Ajax/AjaxRegiste.aspx",
        data: { action: "GetCustomerInfo", ID_Customer: ID_Customer },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg.dataList.length > 0) {
                allISDeleteDT = msg.dataList[0];
            }
        }
    });
    return allISDeleteDT;
}

/// <summary>
/// 获取Cookie中存放的登记查询列表页参数
/// </summary>
function GetRegistListParams() {

    var ParamsArgArray = GetUserCurrentQueryParams("QParam_RegistList");
    if (ParamsArgArray == null) { return; }
    if (ParamsArgArray.length <= 3) { return; }
    // 注意放入数组的顺序

    var BeginExamDate = ParamsArgArray[0];    // 开始日期
    var EndExamDate = ParamsArgArray[1];      // 结束日期
    var OnlyMySelf = ParamsArgArray[2];       // 仅显示我操作的

    jQuery('#BeginExamDate').val(BeginExamDate); // 开始日期
    jQuery('#EndExamDate').val(EndExamDate);     // 结束日期

    // 仅显示我操作的
    if (OnlyMySelf == "0") {
        jQuery("#chkOnlyMySelf").attr("checked", true);
    } else {
        jQuery("#chkOnlyMySelf").attr("checked", false);
    }
}
/// <summary>
/// 保存登记查询列表页参数
/// </summary>
function SaveRegistListParams() {

    var ParamsArgArray = new Array();

    var BeginExamDate = jQuery('#BeginExamDate').val(); // 开始日期
    BeginExamDate = encodeURIComponent(BeginExamDate);
    var EndExamDate = jQuery('#EndExamDate').val();     // 结束日期
    EndExamDate = encodeURIComponent(EndExamDate);
    var OnlyMySelf = jQuery("input[name='chkOnlyMySelf']:checked").val(); // 仅显示我操作的

    // 注意放入数组的顺序
    ParamsArgArray.push(BeginExamDate); // 开始日期
    ParamsArgArray.push(EndExamDate);   // 结束日期
    ParamsArgArray.push(OnlyMySelf);      // 仅显示我操作的

    // 保存科室分检查询列表的参数
    SetUserCurrentQueryParams("QParam_RegistList", ParamsArgArray);

}
 

