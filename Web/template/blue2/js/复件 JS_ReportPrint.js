var tempOperPageCount = 0;
var tempOldtotalCount = 0; //初始总页数，用于判断是否更新页码
var editTitle = "点击进行修改"; //编辑提示
var IsPrintCover = 0;          //是否打印封面 默认为打印整个报告，为1时打印封面，则不显示带打印按钮、打印按钮
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
/// <summary>
/// 设置表横向滚动 
/// </summary>
function TableScrollByID(titleID, scrollID) {
    var $scrollControl = $("#" + scrollID);
    if ($scrollControl.length > 0) {
        var widthLeft = $scrollControl.width() - $scrollControl[0].clientWidth;
        if (widthLeft > 0) {
            var $scrollTitle = $("#" + titleID);
            $scrollTitle.css("width", $scrollTitle.width() + widthLeft);
        }
        $scrollControl.bind("scroll.j-control", function () {
            var left = $(this).scrollLeft();
            $("#" + titleID).css("margin-left", 0 - left);
        });
    }
}
jQuery(document).ready(function () {
    //SwitchHeader(99); // 隐藏头部
    //jQuery("#QueryExamListData").attr("data-left", (168 + jQuery("#ShowUserMenuDiv").height()));
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度
    //TableScrollByID("customerScrollTitle", "customerScrollControl");
    jQuery(".reportdiv").drag({ handler: jQuery(".reportdiv-title-total") });
    tempOperPageCount = 0;
    QueryPagesData(0);

    jQuery("#txtCustomerID").focus();
    /*******获取打印封面参数 是否打印封面 默认为打印整个报告，为1时打印封面，则不显示带打印按钮、打印按钮 Begin  xmhuang 20141020 *******/
    IsPrintCover = jQuery("#IsPrintCover").val(); //获取打印封面参数 xmhuang 20141020
    if (IsPrintCover == 1) {
        jQuery("#readyPrintCover").show();
        jQuery("#readyPrint").hide();
        jQuery("#waitPrint").hide();
    }
    else {
        jQuery("#readyPrintCover").hide();
        jQuery("#readyPrint").show();
        jQuery("#waitPrint").show();
    }
    /*******获取打印封面参数 是否打印封面 默认为打印整个报告，为1时打印封面，则不显示带打印按钮、打印按钮 End    xmhuang 20141020 *******/

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
        this.ID_Team = jQuery('#idSelectTeam').val();
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
                jQuery("#Pagination").show();
                if (tempOperPageCount == 0) {
                    jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
                    tempOldtotalCount = msg.totalCount;
                }
                else if (tempOldtotalCount != msg.totalCount) {
                    jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
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
                                    .replace(/@ID_ExamType/gi, item.ID_ExamType)
                                    .replace(/@ExamTypeName/gi, item.ExamTypeName)
                                    ;
                        index++;
                    }

                });
                if (newContent != '') {
                    jQuery('#tblLeftReportPrint tbody').html(newContent);
                    SetTableRowStyle();
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
function checkAll(obj, ParentObjID) {
    //    var ParentObj = jQuery(obj).parent().parent().parent().parent(); //由于Checkbox是td--tr--thread--table，需要找到table则需要parent.parent
    var checked = obj.checked;
    //    jQuery(ParentObj).find("[name='ItemCheckbox']").each(function () {
    //        //判断是否隐藏
    //        jQuery(this).attr('checked', checked);
    //    })
    jQuery("#" + ParentObjID + " tbody tr td input[name='ItemCheckbox']").each(function () {
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
    //判断是否为空
    if (jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").length == 0) {
        return false;
    }
    var msgContent = "您确认要移除吗？";
    var dialog = art.dialog({
        id: 'artDialogIDRegisterDate',
        lock: true,
        fixed: true,
        opacity: 0.3,
        title: '温馨提示',
        content: msgContent,
        button: [{
            name: '取消',
            callback: function () {
                return true;
            }
        }, {
            name: '确定',
            callback: function () {
                jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").each(function () {
                    //判断当前元素是否是隐藏的
                    if (jQuery(this).parent().parent().is(":visible") == true) {
                        jQuery(this).parent().parent().remove();
                    }
                });
                ResetRownum();
                return true;

            }, focus: true
        }]

    }).lock();

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
    //jQuery("#printReportAll").click(); //xmhuang 移除此项设置以保证不重复查询
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
        LoadCustomer(ID_Customer, 1);
    }
    ResetRownum();
    jQuery("#txtCustomerID").val("");
    jQuery("#txtCustomerID").focus();
}
/// <summary>
///从服务器加载体检号对应信息
///ID_Customer:ID_Customer
///</summary>
function LoadCustomer(ID_Customer, IsContainCheck) {
    var containCheck = IsContainCheck;                       //包含查询check状态
    var action = "PrintReport";
    if (IsPrintCover == 1)//如果是打印封面则调用PrintCoverReport
    {
        action = "PrintCoverReport";
    }
    var modelName = jQuery("#modelName").val();
    var Search = new GetSearchType();
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxReportPreview.aspx",
        data: { containCheck: containCheck, SearchType: Search.SearchType, ID_Team: Search.ID_Team, ID_Customer: ID_Customer, modelName: modelName, pageIndex: 0, pageSize: pagePagination05.items_per_page, action: action },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (parseInt(msg.totalCount) > 0) {
                var TemplateTeamTaskGroupFeeContent = ReadTemplateTeamTaskGroup("TemplateReportPrintView");
                var templateContent = TemplateTeamTaskGroupFeeContent.teamTaskListBodyTempleteContent;
                var index = 1;
                var newContent = '';
                jQuery(msg.dataList).each(function (i, item) {
                    if (item.Is_Checked == "False") {
                        ShowSystemDialog("对不起，您查询的体检号尚未通过总审不能进行报告打印！");
                        return false;
                    }
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
                                    .replace(/@ID_ExamType/gi, item.ID_ExamType)
                                    .replace(/@ExamTypeName/gi, item.ExamTypeName);
                        index++;
                    }
                });
                if (newContent != '') {
                    jQuery('#tblRightReportPrint tbody').append(newContent);
                    jQuery("#tblRightReportPrint tbody tr").last().addClass("externSelect")
                    ResetRownum();
                    SetTableRowStyle();
                }
                // 判断表格是否存在滚动条,并设置相应的样式
                JudgeTableIsExistScroll();
            }
            else {
                ShowSystemDialog("对不起，您查询的体检号不存在或者您没有权限查看此体检号的相关信息！");
                return false;
            }
        }

    });
}

function WritePrintReportLog(allPrintID_Customer) {
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxReportPreview.aspx",
        data: { allPrintID_Customer: allPrintID_Customer, action: 'WritePrintReportLog' },
        cache: false,
        dataType: "json",
        success: function (msg) {
            //alert(msg.Message);
        }
    });
}
/// <summary>
///批量打印 IsPrintCover:是否打印封面 如果是单独打印封面则单独使用封面模板
/// </summary>
function PrintReport(IsPrintCover) {
    if (jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").length == 0) {
        ShowSystemDialog("您尚未勾选需要打印的报告或列表中不存在待打印报告！");
        return false;
    }
    else {
        var printTitle = "批量打印报告";
        if (IsPrintCover == 1) {
            printTitle = "批量打印报告封面";
        }
        /******************记录打印列表顺序 xmhuang 2014-10-11 Begin***********************/
        var allPrintID_Customer = ""; //记录所有当次打印全部的体检号信息
        jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").each(function () {
            //判断当前元素是否是隐藏的
            if (jQuery(this).parent().parent().is(":visible") == true) {
                allPrintID_Customer += jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblID_Customer']").text()) + ",";
            }
        });
        if (allPrintID_Customer.length > 0) {
            //WritePrintReportLog(allPrintID_Customer);
            FastReport.WriteLogFile(printTitle + "【" + allPrintID_Customer.substring(0, allPrintID_Customer.length - 1) + "】");
        }
        //上传日志
        /******************记录打印列表顺序 xmhuang 2014-10-11 End***********************/

        FastReport.SetUserInfo(ID_User, UserName);
        var ID_Customer = "", CustomerName = "";
        var ExamReport = jQuery("#ExamReport").val();
        var DefaultCoverReport = jQuery("#DefaultCoverReport").val(); //默认报告封面
        var MaleCoverReport = jQuery("#MaleCoverReport").val();      //男性彩打封面
        var FeMaleCoverReport = jQuery("#FeMaleCoverReport").val();  //女性彩打封面
        //var lblErrorMessage
        jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").each(function () {

            //判断当前元素是否是隐藏的
            if (jQuery(this).parent().parent().is(":visible") == true) {
                ID_Customer = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblID_Customer']").text());
                CustomerName = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblCustomerName']").text());
                jQuery("#lblErrorMessage").text("(正在打印[" + CustomerName + "]的体检报告...)");
                //FastReport.GenerateCustomerExam_Merge(ID_Customer, "ExamReport_Common_Caption.frx", 1, 1);
                //从配置节点中读取当前报告打印调用的模板 xmhuang 20140424
                //判断当前客户的体检类型是否包含为彩色打印 Begin
                var ExamType = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblExamTypeName']").text()); //客户体检类型
                var UseColorPrintPaperExamType = jQuery.trim(jQuery("#UseColorPrintPaperExamType").val());
                var IsUseCustReport = jQuery.trim(jQuery("#IsUseCustReport").val());
                if (IsUseCustReport == 1 || IsUseCustReport == "True") {
                    if (UseColorPrintPaperExamType.indexOf(ExamType) > -1) {
                        var Sex = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblGenderName']").text());    //获取客户性别
                        if (Sex == "男") {
                            var MaleColorReport = jQuery("#MaleColorReport").val();
                            if (IsPrintCover == 1)//打印男性彩打封面
                            {
                                FastReport.CustGenerateCustomerExam_Merge(ID_Customer, MaleCoverReport, 1, 1);
                            }
                            else {
                                FastReport.CustGenerateCustomerExam_Merge(ID_Customer, MaleColorReport, 1, 1);
                            }
                        }
                        else if (Sex == "女") {
                            if (IsPrintCover == 1)//打印女性彩打封面
                            {
                                FastReport.CustGenerateCustomerExam_Merge(ID_Customer, FeMaleCoverReport, 1, 1);
                            }
                            else {
                                var FeMaleColorReport = jQuery("#FeMaleColorReport").val();
                                FastReport.CustGenerateCustomerExam_Merge(ID_Customer, FeMaleColorReport, 1, 1);
                            }
                        }
                        else {
                            if (IsPrintCover == 1)//打印默认封面
                            {
                                FastReport.CustGenerateCustomerExam_Merge(ID_Customer, DefaultCoverReport, 1, 1);
                            }
                            else {
                                //性别不详则默认报告打印
                                FastReport.CustGenerateCustomerExam_Merge(ID_Customer, ExamReport, 1, 1);
                            }
                        }
                    }
                    else {
                        if (IsPrintCover == 1)//打印默认封面
                        {
                            FastReport.CustGenerateCustomerExam_Merge(ID_Customer, DefaultCoverReport, 1, 1);
                        }
                        else {
                            FastReport.CustGenerateCustomerExam_Merge(ID_Customer, jQuery("#ExamReport").val(), 1, 1);
                        }
                    }
                }
                else {
                    if (UseColorPrintPaperExamType.indexOf(ExamType) > -1) {
                        var Sex = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblGenderName']").text());    //获取客户性别
                        if (Sex == "男") {
                            var MaleColorReport = jQuery("#MaleColorReport").val();
                            if (IsPrintCover == 1)//打印男性彩打封面
                            {
                                FastReport.GenerateCustomerExam_Merge(ID_Customer, MaleCoverReport, 1, 1);
                            }
                            else {
                                FastReport.GenerateCustomerExam_Merge(ID_Customer, MaleColorReport, 1, 1);
                            }
                        }
                        else if (Sex == "女") {
                            if (IsPrintCover == 1)//打印女性彩打封面
                            {
                                FastReport.GenerateCustomerExam_Merge(ID_Customer, FeMaleCoverReport, 1, 1);
                            }
                            else {
                                var FeMaleColorReport = jQuery("#FeMaleColorReport").val();
                                FastReport.GenerateCustomerExam_Merge(ID_Customer, FeMaleColorReport, 1, 1);
                            }
                        }
                        else {
                            if (IsPrintCover == 1)//打印默认封面
                            {
                                FastReport.GenerateCustomerExam_Merge(ID_Customer, DefaultCoverReport, 1, 1);
                            }
                            else {
                                //性别不详则默认报告打印
                                FastReport.GenerateCustomerExam_Merge(ID_Customer, ExamReport, 1, 1);
                            }
                        }
                    }
                    else {
                        if (IsPrintCover == 1)//打印默认封面
                        {
                            FastReport.GenerateCustomerExam_Merge(ID_Customer, DefaultCoverReport, 1, 1);
                        }
                        else {
                            FastReport.GenerateCustomerExam_Merge(ID_Customer, jQuery("#ExamReport").val(), 1, 1);
                        }
                    }
                }
                //判断当前客户的体检类型是否包含为彩色打印，如果区分则分别获取对应模板 End 
                //FastReport.GenerateCustomerExam_Merge(ID_Customer, jQuery("#ExamReport").val(), 1, 1);
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
///批量打印 IsPrintCover:是否打印封面 如果是单独打印封面则单独使用封面模板
/// </summary>
function PrintReport_Waste(IsPrintCover) {
    if (jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").length == 0) {
        ShowSystemDialog("您尚未勾选需要打印的报告或列表中不存在待打印报告！");
        return false;
    }
    else {
        var printTitle = "批量打印报告";
        if (IsPrintCover == 1) {
            printTitle = "批量打印报告封面";
        }
        /******************记录打印列表顺序 xmhuang 2014-10-11 Begin***********************/
        var allPrintID_Customer = ""; //记录所有当次打印全部的体检号信息
        jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").each(function () {
            //判断当前元素是否是隐藏的
            if (jQuery(this).parent().parent().is(":visible") == true) {
                allPrintID_Customer += jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblID_Customer']").text()) + ",";
            }
        });
        if (allPrintID_Customer.length > 0) {
            //WritePrintReportLog(allPrintID_Customer);
            FastReport.WriteLogFile(printTitle + "【" + allPrintID_Customer.substring(0, allPrintID_Customer.length - 1) + "】");
        }
        //上传日志
        /******************记录打印列表顺序 xmhuang 2014-10-11 End***********************/

        FastReport.SetUserInfo(ID_User, UserName);
        var ID_Customer = "", CustomerName = "";
        var ExamReport = jQuery("#ExamReport").val();
        var DefaultCoverReport = jQuery("#DefaultCoverReport").val(); //默认报告封面
        var MaleCoverReport = jQuery("#MaleCoverReport").val();      //男性彩打封面
        var FeMaleCoverReport = jQuery("#FeMaleCoverReport").val();  //女性彩打封面
        //var lblErrorMessage
        jQuery("#tblRightReportPrint tbody tr td input[name='ItemCheckbox']:checked").each(function () {

            //判断当前元素是否是隐藏的
            if (jQuery(this).parent().parent().is(":visible") == true) {
                ID_Customer = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblID_Customer']").text());
                CustomerName = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblCustomerName']").text());
                jQuery("#lblErrorMessage").text("(正在打印[" + CustomerName + "]的体检报告...)");
                //FastReport.GenerateCustomerExam_Merge(ID_Customer, "ExamReport_Common_Caption.frx", 1, 1);
                //从配置节点中读取当前报告打印调用的模板 xmhuang 20140424
                //判断当前客户的体检类型是否包含为彩色打印 Begin
                var ExamType = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblExamTypeName']").text()); //客户体检类型
                var UseColorPrintPaperExamType = jQuery.trim(jQuery("#UseColorPrintPaperExamType").val());
                if (UseColorPrintPaperExamType.indexOf(ExamType) > -1) {
                    var Sex = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblGenderName']").text());    //获取客户性别
                    if (Sex == "男") {
                        var MaleColorReport = jQuery("#MaleColorReport").val();
                        if (IsPrintCover == 1)//打印男性彩打封面
                        {
                            FastReport.GenerateCustomerExam_Merge(ID_Customer, MaleCoverReport, 1, 1);
                        }
                        else {
                            FastReport.GenerateCustomerExam_Merge(ID_Customer, MaleColorReport, 1, 1);
                        }
                    }
                    else if (Sex == "女") {
                        if (IsPrintCover == 1)//打印女性彩打封面
                        {
                            FastReport.GenerateCustomerExam_Merge(ID_Customer, FeMaleCoverReport, 1, 1);
                        }
                        else {
                            var FeMaleColorReport = jQuery("#FeMaleColorReport").val();
                            FastReport.GenerateCustomerExam_Merge(ID_Customer, FeMaleColorReport, 1, 1);
                        }
                    }
                    else {
                        if (IsPrintCover == 1)//打印默认封面
                        {
                            FastReport.GenerateCustomerExam_Merge(ID_Customer, DefaultCoverReport, 1, 1);
                        }
                        else {
                            //性别不详则默认报告打印
                            FastReport.GenerateCustomerExam_Merge(ID_Customer, ExamReport, 1, 1);
                        }
                    }
                }
                else {
                    if (IsPrintCover == 1)//打印默认封面
                    {
                        FastReport.GenerateCustomerExam_Merge(ID_Customer, DefaultCoverReport, 1, 1);
                    }
                    else {
                        FastReport.GenerateCustomerExam_Merge(ID_Customer, jQuery("#ExamReport").val(), 1, 1);
                    }
                }
                //判断当前客户的体检类型是否包含为彩色打印，如果区分则分别获取对应模板 End

                //FastReport.GenerateCustomerExam_Merge(ID_Customer, jQuery("#ExamReport").val(), 1, 1);
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
        jQuery("#slTeam").hide();
        jQuery("#tblRightReportPrint tbody tr").show();
    }
    //点击个人
    else if (key == "self") {
        jQuery("#slTeam").hide();
        //        jQuery("#tblRightReportPrint tbody tr[is_team='1']").hide();
        //        jQuery("#tblRightReportPrint tbody tr[is_team!='1']").show();

    }
    //点击团体
    else if (key == "team") {
        jQuery("#slTeam").show();
        //        jQuery("#tblRightReportPrint tbody tr[is_team!='1']").hide();
        //        jQuery("#tblRightReportPrint tbody tr[is_team='1']").show();

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
    var ID_ExamType = "";
    var ExamTypeName = "";
    jQuery("#tblRightReportPrint tbody tr td label[name='lblID_Customer']").each(function () {
        AllID_Customer += jQuery.trim(jQuery(this).text()) + ",";
    });
    document.getElementById("chbPrintReport").checked = false;
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
            ID_ExamType = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblID_ExamType']").text());
            ExamTypeName = jQuery.trim(jQuery(this).parent().parent().find("td label[name='lblExamTypeName']").text());
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
                                    .replace(/@Department/gi, Department)
                                    .replace(/@Informer/gi, Informer)
                                    .replace(/@InformedDate/gi, InformedDate)
                                    .replace(/@ReportReceiptor/gi, ReportReceiptor)
                                    .replace(/@ReportReceiptedDate/gi, ReportReceiptedDate)
                                    .replace(/@RowNum/gi, index)
                                    .replace(/@Is_CanSelect/gi, 1)
                                    .replace(/@Is_Team/gi, Is_Team)
                                    .replace(/@checked/gi, true)
                                    .replace(/@ID_ExamType/gi, ID_ExamType)
                                    .replace(/@ExamTypeName/gi, ExamTypeName)
                                    ;
                index++;
            }
        }
    });
    if (newContent != "") {
        jQuery('#tblRightReportPrint tbody').append(newContent);
        ToggerterCenter();
        SetTableRowStyle();
    }
    // 判断表格是否存在滚动条,并设置相应的样式
    JudgeTableIsExistScroll();
}