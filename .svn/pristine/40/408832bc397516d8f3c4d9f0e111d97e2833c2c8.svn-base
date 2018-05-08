/*
获取客户历年体检信息共用脚本CustomerPhysicalExamInfo
*/
var defalutImagSrc = "/template/blue/images/icons/nohead.gif"; //默认头像
function GetCustomerPhysicalExamInfo() {
    var IDCard = jQuery.trim(jQuery('#txtSFZ').val()); // 证件号
    if (IDCard == "") {
        //从卡片读取
        var SynCardOcxOne = document.getElementById("CVR_IDCard"); //获取身份证插件
        var ret = SynCardOcxOne.ReadCard();
        if (ret == 0) {
            jQuery.trim(jQuery('#txtSFZ').val(SynCardOcxOne.CardNo));
            QueryPagesData(0); //重新按照新输入的条件进行查询
        }
        else {
            ShowSystemDialog("对不起，读取身份信息失败,请您确认身份证已正确安放！");
        }
        return false;
    }
    else {
        QueryPagesData(0); //重新按照新输入的条件进行查询
    }
}
/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的条件内，没有找到任何客户信息！";
    }
    var html = "<tr><td style='text-align: center; line-height: 100px;' colSpan='24'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
    jQuery("#Pagination").hide(); //隐藏分页控件
}
/// <summary>
///查询数据
/// </summary>
function QueryPagesData(pageIndex) {
    ResetSearchInfo("");
    var IDCard = jQuery.trim(jQuery('#txtSFZ').val()); // 证件号
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            IDCard: IDCard,
            action: 'GetCustomerPhysicalExamInfo'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            var PhysicalExamInfoTemplate = jQuery("#PhysicalExamInfoTemplate tbody").html(); //获取体检明细模板
            var Content = "", PhysicalExamInfoContent = "";
            var RowNum = 1, Department = "";
            if (msg == null) {
                return false;
            }
            jQuery(msg.dataList).each(function (i, item) {
                if (item.Base64Photo == "") {
                    item.Base64Photo = defalutImagSrc;
                }
                else {
                    item.Base64Photo = "data:image/gif;base64," + item.Base64Photo;
                }
                Department = item.Department;
                if (Department != "") {
                    if (item.DepartSubA != "") {
                        Department += "--" + item.DepartSubA;
                    }
                    else if (item.DepartSubB != "") {
                        Department += "--" + item.DepartSubB;
                    }
                    else if (item.DepartSubC != "") {
                        Department += "--" + item.DepartSubC;
                    }
                }
                Content += PhysicalExamInfoTemplate.replace(/@RowNum/gi, RowNum)
                            .replace(/@ID_ArcCustomer/gi, item.ID_ArcCustomer)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@BirthDay/gi, item.BirthDay)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@Base64Photo/gi, item.Base64Photo)
                            .replace(/@IDCard/gi, item.IDCarD)
                            .replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@ExamTypeName/gi, item.ExamTypeName)
                            .replace(/@Operator/gi, item.Operator)
                            .replace(/@OperateDate/gi, item.OperateDate)
                            .replace(/@GuideSheetReturnedby/gi, item.GuideSheetReturnedby)
                            .replace(/@GuideSheetReturnedDate/gi, item.GuideSheetReturnedDate)
                            .replace(/@FinalDoctor/gi, item.FinalDoctor)
                            .replace(/@FinalDate/gi, item.FinalDate)
                            .replace(/@Checker/gi, item.Checker)
                            .replace(/@CheckedDate/gi, item.CheckedDate)
                            .replace(/@ReportPrinter/gi, item.ReportPrinter)
                            .replace(/@ReportPrintedDate/gi, item.ReportPrintedDate)
                            .replace(/@ReportReceiptor/gi, item.ReportReceiptor)
                            .replace(/@ReportReceiptedDate/gi, item.ReportReceiptedDate)
                            .replace(/@TeamName/gi, item.TeamName)
                            .replace(/@Department/gi, Department)
                RowNum++;
            });
            if (Content != "") {
                jQuery("#Pagination").show();
                jQuery("#Searchresult").html(Content);
            }
        }
    });
}
/// <summary>
///报告预览
/// </summary>
function PreviewReport(ID_Customer) {
    FastReport.ReportPreview(ID_Customer, "ExamReport_Common_Caption.frx", "1", 0);
}
jQuery(document).ready(function () {
    jQuery("#txtSFZ").focus();
});