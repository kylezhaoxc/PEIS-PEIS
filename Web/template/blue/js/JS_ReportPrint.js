var tempOperPageCount = 0;
var tempOldtotalCount = 0; //初始总页数，用于判断是否更新页码
var editTitle = "点击进行修改"; //编辑提示
function OnFormKeyUp(e) {
    var curEvent = window.event || e;
    var id = document.activeElement.id;
    if (curEvent.keyCode == 13)//回车事件
    {
        //如果是在搜索中
        if (id == "txtCustomerID") {
            AutoGetCustomerReport();
        }
    }
}
/// <summary>
/// 读取指定模版
///TemplateTeamTaskGroupID:模版ID
/// </summary>
function ReadTemplateTeamTaskGroup(TemplateTeamTaskGroupID) {
    //默认是读取tblTemplateTeamTaskList模版填充到tblTeamTask中显示
    if (TemplateTeamTaskGroupID == "" || TemplateTeamTaskGroupID == undefined) {
        TemplateTeamTaskGroupID = "TemplateReportView";
    }
    var teamTaskGroupListContent = ""; //团体任务分组table内容
    var teamTaskGroupListTheadTempleteContent = jQuery('#' + TemplateTeamTaskGroupID + ' thead').html(); //团体任务模版Thead部分
    var teamTaskListBodyTempleteContent = jQuery('#' + TemplateTeamTaskGroupID + ' tbody').html(); //团体任务模版body部分
    this.teamTaskGroupListTheadTempleteContent = teamTaskGroupListTheadTempleteContent;
    this.teamTaskListBodyTempleteContent = teamTaskListBodyTempleteContent;
    return this;
}


/****************************已审核列表分页绑定 Begin******************************/
function pageselectCallback(page_index, jq) {

    //    if (tempOperPageCount > 0) {
    //        QueryPagesData(page_index);
    //    }
    //    tempOperPageCount++;

    if (tempOperPageCount > 0) {
        tempOperPageCount++;
        QueryPagesData(page_index);
    }
    tempOperPageCount++;

    return false;
}

jQuery(document).ready(function () {

    tempOperPageCount = 0;
    QueryPagesData(0);

});
/// <summary>
/// 获取查询类型 全部：0 个人：1 团体：2
///TemplateTeamTaskGroupID:模版ID
/// </summary>
function GetSearchType() {
    this.SearchType = 0;
    this.ID_Team = -1;
    var SearchType = 0;
    if (document.getElementById("printReportAll").checked) {
        this.SearchType = 0;
    }
    else if (document.getElementById("printReportRadioSelf").checked) {
        this.SearchType = 1;
    }
    else if (document.getElementById("printReportRadioTeam").checked) {
        this.SearchType = 2;
        //获取团体ID
        this.ID_Team = document.getElementById("slTeam").options[document.getElementById("slTeam").selectedIndex].value; //婚姻状况
    }
}
var OldSearchType = 0;
function QueryPagesData(pageIndex) {
    //判断是全部、团体还是个人
    var Search = new GetSearchType();
    optInit = getOptionsFromForm05();
    var modelName = jQuery("#modelName").val();
    var totalCount = 0;
    var CustomerName = jQuery.trim(jQuery('#txtSFZ').val());
    var optInit;
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxReportPreview.aspx",
        data: { SearchType: Search.SearchType, ID_Team: Search.ID_Team, modelName: modelName, pageIndex: pageIndex, pageSize: pagePagination05.items_per_page, action: 'PrintReport' },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (parseInt(msg.totalCount) > 0) {
                //                jQuery("#Pagination").show();
                //                if (OldSearchType != Search.SearchType) {
                //                    tempOldtotalCount = msg.totalCount;
                //                    OldSearchType = Search.SearchType;
                //                    tempOperPageCount = 1;
                //                    jQuery("#Pagination").pagination(msg.totalCount, optInit);
                //                }
                //                else {
                //                    if (tempOperPageCount == 0) {
                //                        jQuery("#Pagination").pagination(msg.totalCount, optInit);
                //                        tempOldtotalCount = msg.totalCount;
                //                        tempOperPageCount = 1;
                //                    }
                //                    else if (tempOldtotalCount != msg.totalCount) {
                //                        jQuery("#Pagination").pagination(msg.totalCount, optInit);
                //                        tempOperPageCount = 1;
                //                    }
                //                }


                jQuery("#Pagination").show();
                if (tempOperPageCount == 0) {
                    jQuery("#Pagination").pagination(msg.totalCount, optInit);
                    tempOldtotalCount = msg.totalCount;
                }
                else if (tempOldtotalCount != msg.totalCount) {
                    jQuery("#Pagination").pagination(msg.totalCount, optInit);
                }


                var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup("TemplateReportView");
                //var teamTaskGroupFeeListTheadTempleteContent = TemplateTeamTaskGroupFeeContent.teamTaskGroupListTheadTempleteContent;
                var templateContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
                var index = 1;
                var newContent = '';
                jQuery(msg.dataList).each(function (i, item) {
                    if (templateContent != null) {
                        newContent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                                    .replace(/@RowNum/gi, index)
                                    .replace(/@CustomerName/gi, item.CustomerName)
                                    .replace(/@GenderName/gi, item.GenderName)
                                    .replace(/@date/gi, item.date)
                                    .replace(/@TeamName/gi, item.TeamName)
                                    .replace(/@Checker/gi, item.Checker)
                                    .replace(/@CheckedDate/gi, item.CheckedDate)
                                    .replace(/@ReportPrinter/gi, item.ReportPrinter)
                                    .replace(/@ReportPrintedDate/gi, item.ReportPrintedDate)
                                    .replace(/@Is_Checked/gi, item.Is_Checked)
                                    .replace(/@Is_ReportPrinted/gi, item.Is_ReportPrinted)
                                    .replace(/@Is_Informed/gi, item.Is_Informed)
                                    .replace(/@Is_ReportReceipted/gi, item.Is_ReportReceipted)
                                    .replace(/@FinalDoctor/gi, item.FinalDoctor)
                                    .replace(/@FinalDoctor/gi, item.FinalDoctor)
                                    .replace(/@FinalDate/gi, item.FinalDate)
                                    .replace(/@Department/gi, item.Department)
                                    .replace(/@Informer/gi, item.Informer)
                                    .replace(/@InformedDate/gi, item.InformedDate)
                                    .replace(/@ReportReceiptor/gi, item.ReportReceiptor)
                                    .replace(/@ReportReceiptedDate/gi, item.ReportReceiptedDate)
                                     .replace(/@RowNum/gi, index)
                                       .replace(/@checked/gi, true)
                                    ;
                        index++;
                    }

                });
                if (newContent != '') {
                    jQuery('#tblLeftReportPrint tbody').html(newContent);
                }
            } else {
                jQuery('#tblLeftReportPrint tbody').html("");
                jQuery("#Pagination").hide();
            }
        }
    });
}
/****************************已审核列表分页绑定 end******************************/

/****************************已审核列表功能操作 Begin******************************/
var allWidth = jQuery("#tdLeft").width() + jQuery("#tdRight").width();
jQuery("#hidRight").hide();
jQuery("#tdRight").hide();
jQuery("#tdLeft").css("width", allWidth);
function ToggerterCenter() {
    //
    var hidLeft = jQuery.trim(jQuery("#hidLeft").text());
    if (hidLeft == "→") {
        jQuery("#hidRight").text("←");
    }
    else if (hidLeft == "←") {
        jQuery("#hidRight").text("→");
    }
    jQuery("#hidRight").show();
    jQuery("#tdLeft").css("width", allWidth / 2);
    jQuery("#tdLeft").show("slow");
    jQuery("#tdRight").css("width", allWidth / 2);
    jQuery("#tdRight").show("slow");
}

function Toggerter(objID) {
    jQuery("#hidRight").hide();
    var value = jQuery.trim(jQuery(objID).text());
    //var allWidth = jQuery("#tdLeft").width() + jQuery("#tdRight").width();
    if (value == "→") {
        jQuery(objID).text("←");
        jQuery("#tdLeft").css("width", allWidth);
        jQuery("#tdRight").hide("slow");
        jQuery("#tdLeft").show("slow");
    }
    else {
        jQuery(objID).text("→");
        jQuery("#tdRight").css("width", allWidth);
        jQuery("#tdLeft").hide("slow");
        jQuery("#tdRight").show("slow");
    }
}
/// <summary>
///通过查找父元素下的所有复选框，并设置其选中状态
///obj:父元素ID，这里主要是Table元素的ID值
///ParentObj:父元素ID，这里主要是Table元素的ID值
/// </summary>
function checkAll(obj, ParentObj) {
    var ParentObj = jQuery(obj).parent().parent().parent().parent(); //由于Checkbox是td--tr--thread--table，需要找到table则需要parent.parent
    var checked = obj.checked;
    jQuery(ParentObj).find("[name='ItemCheckbox']").each(function () {
        //判断是否隐藏
        jQuery(this).attr('checked', checked);
    })
}
/// <summary>
///移除勾选的元素
///obj:元素ID，这里主要是Table元素的ID值
/// </summary>
function RemovePrintReport(obj) {
    jQuery(obj).parent().parent().remove();
    ResetRownum();
}
/// <summary>
///移除所有勾选的元素
/// </summary>
function RemoveAllPrintReport() {
    jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").each(function () {
        //判断当前元素是否是隐藏的
        if (jQuery(this).parent().parent().is(":visible") == true) {
            jQuery(this).parent().parent().remove();
        }
    });
    ResetRownum();
}
/// <summary>
///移除勾选的元素
///obj:元素ID，这里主要是Table元素的ID值
/// </summary>
function ResetRownum() {
    var index = 1;
    jQuery("#tblRightReportPrint tbody tr td label[name='lblRowNum']").each(function () {
        jQuery(this).text(index);
        index++;

    });
}
/// <summary>
///扫描体检号
///ID_Customer:ID_Customer
///</summary>
function AutoGetCustomerReport() {
    var ID_Customer = jQuery.trim(jQuery("#txtCustomerID").val());
    if (!isCustomerExamNo(ID_Customer)) {
        ShowSystemDialog("对不起，请您输入正确的体检号！");
        return false;
    }
    jQuery("#printReportAll").click();
    var newContent = "";
    //判断体检号是否相同
    jQuery("#tblRightReportPrint tbody tr td label[name='lblID_Customer']").each(function () {
        if (jQuery.trim(jQuery(this).text()) == ID_Customer) {
            newContent += jQuery(this).parent().parent()[0].outerHTML;
            jQuery(this).parent().parent().remove();
        }
        else {
            jQuery(this).parent().parent().removeClass("externSelect");
        }
    });
    if (newContent != "") {
        jQuery("#tblRightReportPrint tbody").append(newContent);
        jQuery("#tblRightReportPrint tbody tr").last().addClass("externSelect");
    }
    else {
        LoadCustomer(ID_Customer);
    }
    ResetRownum();
    jQuery("#txtCustomerID").val("");
    jQuery("#txtCustomerID").focus();
}
/// <summary>
///从服务器加载体检号对应信息
///ID_Customer:ID_Customer
///</summary>
function LoadCustomer(ID_Customer) {
    var modelName = jQuery("#modelName").val();
    var Search = new GetSearchType();
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxReportPreview.aspx",
        data: { SearchType: Search.SearchType, ID_Team: Search.ID_Team, ID_Customer: ID_Customer, modelName: modelName, pageIndex: 0, pageSize: pagePagination05.items_per_page, action: 'PrintReport' },
        cache: false,
        dataType: "json",
        success: function (msg) {

            if (parseInt(msg.totalCount) > 0) {
                var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup("TemplateReportPrintView");
                var templateContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
                var index = 1;
                var newContent = '';
                jQuery(msg.dataList).each(function (i, item) {
                    if (templateContent != null) {
                        newContent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                                    .replace(/@RowNum/gi, index)
                                    .replace(/@CustomerName/gi, item.CustomerName)
                                    .replace(/@GenderName/gi, item.GenderName)
                                    .replace(/@date/gi, item.date)
                                    .replace(/@TeamName/gi, item.TeamName)
                                    .replace(/@Checker/gi, item.Checker)
                                    .replace(/@CheckedDate/gi, item.CheckedDate)
                                    .replace(/@ReportPrinter/gi, item.ReportPrinter)
                                    .replace(/@ReportPrintedDate/gi, item.ReportPrintedDate)
                                    .replace(/@Is_Checked/gi, item.Is_Checked)
                                    .replace(/@Is_ReportPrinted/gi, item.Is_ReportPrinted)
                                    .replace(/@Is_Informed/gi, item.Is_Informed)
                                    .replace(/@Is_ReportReceipted/gi, item.Is_ReportReceipted)
                                    .replace(/@FinalDoctor/gi, item.FinalDoctor)
                                    .replace(/@FinalDoctor/gi, item.FinalDoctor)
                                    .replace(/@FinalDate/gi, item.FinalDate)
                                    .replace(/@Department/gi, item.Department)
                                    .replace(/@Informer/gi, item.Informer)
                                    .replace(/@InformedDate/gi, item.InformedDate)
                                    .replace(/@ReportReceiptor/gi, item.ReportReceiptor)
                                    .replace(/@ReportReceiptedDate/gi, item.ReportReceiptedDate)
                                    .replace(/@RowNum/gi, index)
                                    .replace(/@checked/gi, true)
                                    ;
                        index++;
                    }
                });
                if (newContent != '') {
                    jQuery('#tblRightReportPrint tbody').append(newContent);
                    jQuery("#tblRightReportPrint tbody tr").last().addClass("externSelect")
                    ResetRownum();
                }
            }
            else {
                ShowSystemDialog("对不起，您查询的体检号不存在或者您没有权限查看此体检号的相关信息！");
                return false;
            }
        }

    });
}
/// <summary>
///批量打印
/// </summary>
function PrintReport() {

    if (jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").length == 0) {
        jQuery("#lblErrorMessage").text("(您尚未勾选需要打印的报告或列表中不存在待打印报告！)");
        return false;
    }
    else {

        FastReport.SetUserInfo(ID_User, UserName);
        var ID_Customer = "", CustomerName = "";
        //var lblErrorMessage
        jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").each(function () {
            //判断当前元素是否是隐藏的
            if (jQuery(this).parent().parent().is(":visible") == true) {
                ID_Customer = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblID_Customer']").text());
                CustomerName = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblCustomerName']").text());
                jQuery("#lblErrorMessage").text("(正在打印[" + CustomerName + "]的体检报告...)");
                FastReport.GenerateCustomerExam_Merge(ID_Customer, "ExamReport_Common_Caption.frx", 1, 1);
                jQuery("#lblErrorMessage").text("(已完成[" + CustomerName + "]的体检报告打印。)");
                jQuery(this).parent().parent().remove();
            }
        });
        jQuery("#lblErrorMessage").text("(报告打印完毕。)");
        tempOperPageCount = 0;
        //重新绑定审核列表
        QueryPagesData(0);
    }
}
/// <summary>
///移除勾选的元素
///obj:元素ID，这里主要是Table元素的ID值
///key:关键字标识，这里主要是Table元素的ID值
/// </summary>
function SelectPrintReport(obj, key) {

    //点击全部
    if (key == "all") {
        //jQuery("#slTeam").hide();
        jQuery("#tblRightReportPrint tbody tr").show();
    }
    //点击个人
    else if (key == "self") {
        //jQuery("#slTeam").hide();
        jQuery("#tblRightReportPrint tbody tr[is_team='1']").hide();
        jQuery("#tblRightReportPrint tbody tr[is_team!='1']").show();

    }
    //点击团体
    else if (key == "team") {

        jQuery("#tblRightReportPrint tbody tr[is_team!='1']").hide();
        //jQuery("#slTeam").show(); //显示团体下拉框 Is_CanSelect
        jQuery("#tblRightReportPrint tbody tr[is_team='1']").show();

    }
    tempOperPageCount = 0;
    QueryPagesData(0);
    return false;
}

function SelectTeamPrintReport(obj) {
    var value = obj.value;
    jQuery("#tblRightReportPrint tbody tr td label[name='lblTeamName']").each(function () {
        if (jQuery.trim(jQuery(this).text()) == value) {
            jQuery(this).parent().parent().show();
        }
        else {
            jQuery(this).parent().parent().hide();
        }
    });
}
function AppendToRight() {
    //获取已勾选数据
    var rowCount = jQuery("#tblLeftReportPrint tbody tr td input[name='ItemCheckbox']:checked").length;
    if (rowCount == 0) {
        return false;
    }
    var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup("TemplateReportPrintView");
    var templateContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
    var index = jQuery("#tblRightReportPrint tbody tr").length + 1, newContent = "";
    var Is_Team = 0, ID_Customer = "", CustomerName = "", GenderName = "", date = "", TeamName = "", Checker = "",
    CheckedDate = "", ReportPrinter = "", ReportPrintedDate = "", Is_Checked = "", Is_ReportPrinted = "",
    Is_Informed = "", Is_ReportReceipted = "", FinalDoctor = "", FinalDate = "", Department = "",
    Informer = "", InformedDate = "", ReportReceiptor = "", ReportReceiptedDate = "";
    var AllID_Customer = "";
    jQuery("#tblRightReportPrint tbody tr td label[name='lblID_Customer']").each(function () {
        AllID_Customer += jQuery.trim(jQuery(this).text()) + ",";
    });
    document.getElementById("chbPrintReport").checked = false;
    //    jQuery("#slTeam").empty();
    //    jQuery("#slTeam").append('<option value="-1" selected="selected">----</option>');
    jQuery("#tblLeftReportPrint tbody tr td input[name='ItemCheckbox']:checked").each(function () {
        //判断是否在待打印列表中已经存在，不存在则添加，存在则不添加
        ID_Customer = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblID_Customer']").text());
        if (AllID_Customer.search(ID_Customer) == -1) {
            Is_Checked = jQuery(this).parent().parent().attr("is_checked");
            Is_ReportPrinted = jQuery(this).parent().parent().attr("is_reportprinted");
            Is_Informed = jQuery(this).parent().parent().attr("is_informed");
            Is_ReportReceipted = jQuery(this).parent().parent().attr("is_reportreceipted");
            CustomerName = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblCustomerName']").text());
            GenderName = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblGenderName']").text());
            date = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lbldate']").text());
            TeamName = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblTeamName']").text());
            Is_Team = TeamName == "" ? 0 : 1;
            Checker = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblChecker']").text());
            CheckedDate = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblCheckedDate']").text());
            ReportPrinter = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblReportPrinter']").text());
            ReportPrintedDate = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblReportPrintedDate']").text());
            Informer = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblInformer']").text());
            InformedDate = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblInformedDate']").text());
            ReportReceiptor = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblReportReceiptor']").text());
            ReportReceiptedDate = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblReportReceiptedDate']").text());
            if (templateContent != null) {
                newContent += templateContent.replace(/@ID_Customer/gi, ID_Customer)
                                    .replace(/@RowNum/gi, index)
                                    .replace(/@CustomerName/gi, CustomerName)
                                    .replace(/@GenderName/gi, GenderName)
                                    .replace(/@date/gi, date)
                                    .replace(/@TeamName/gi, TeamName)
                                    .replace(/@Checker/gi, Checker)
                                    .replace(/@CheckedDate/gi, CheckedDate)
                                    .replace(/@ReportPrinter/gi, ReportPrinter)
                                    .replace(/@ReportPrintedDate/gi, ReportPrintedDate)
                                    .replace(/@Is_Checked/gi, Is_Checked)
                                    .replace(/@Is_ReportPrinted/gi, Is_ReportPrinted)
                                    .replace(/@Is_Informed/gi, Is_Informed)
                                    .replace(/@Is_ReportReceipted/gi, Is_ReportReceipted)
                //.replace(/@FinalDoctor/gi, FinalDoctor)
                //.replace(/@FinalDate/gi, FinalDate)
                                    .replace(/@Department/gi, Department)
                                    .replace(/@Informer/gi, Informer)
                                    .replace(/@InformedDate/gi, InformedDate)
                                    .replace(/@ReportReceiptor/gi, ReportReceiptor)
                                    .replace(/@ReportReceiptedDate/gi, ReportReceiptedDate)
                                     .replace(/@RowNum/gi, index)
                                       .replace(/@Is_CanSelect/gi, 1)
                                        .replace(/@Is_Team/gi, Is_Team)
                                    .replace(/@checked/gi, true)
                                    ;
                index++;
            }
            //            if (Is_Team == 1) {
            //                //判断是否存在
            //                if (jQuery("#slTeam").find("option[value='" + TeamName + "'").length < 1) {
            //                    alert();
            //                    jQuery("#slTeam").append('<option value="' + TeamName + '">' + TeamName + '</option>');
            //                }
            //            }
        }
    });
    if (newContent != "") {
        jQuery('#tblRightReportPrint tbody').append(newContent);
        ToggerterCenter();
    }
}

/****************************已审核列表功能操作 End******************************/