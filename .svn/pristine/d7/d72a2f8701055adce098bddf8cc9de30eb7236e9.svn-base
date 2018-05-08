/************************该页面为个人登记工作量统计公用脚本******************************/
/// <summary>
///分页函数 XMHuang 2013-08-12 添加注释 
///pageIndex:当前页面ID
/// </summary>
jQuery(document).ready(function () {
    jQuery("#slRegister").select2();
    ResetSearchInfo("尚无任何数据...");
});
/// <summary>
/// 获取个人登记工作量信息
/// </summary>
function GetRegisteWorkLoad() {
    var ID_Register = document.getElementById("slRegister").options[document.getElementById("slRegister").selectedIndex].value; //登记人
    var BeginExamDate = jQuery('#BeginExamDate').val();
    BeginExamDate = encodeURIComponent(BeginExamDate); //编码开始日期
    var EndExamDate = jQuery('#EndExamDate').val();
    EndExamDate = encodeURIComponent(EndExamDate);    //结束日期
    //判断时间差 Begin xmhuang 2014-01-14
    if (!CheckTime(BeginExamDate, EndExamDate)) {
        return false;
    }    
    if (ID_Register == -1) {
        ShowSystemDialog("对不起，请您选择需要查看的登记人!");
        return false;
    }
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxStatistics.aspx",
        data: {
            ID_Register: ID_Register,
            BeginExamDate: BeginExamDate,
            EndExamDate: EndExamDate,
            action: 'GetRegisteWorkLoad'
        },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (msg != undefined) {
                var newcontent = '';
                var templateContent = jQuery("#TemplateRegister tbody").html();
                if (templateContent == undefined) { return false; }
                var RowNum = 1;
                jQuery(msg.dataList0).each(function (i, item) {
                    if (templateContent != null) {
                        newcontent += templateContent.replace(/@RowNum/gi, RowNum)
                            .replace(/@RegisterName/gi, item.RegisterName)
                            .replace(/@FeeItemName /gi, item.FeeItemName)
                            .replace(/@RegistCount/gi, item.RegistCount)
                            .replace(/@SumFactPrice/gi, item.SumFactPrice)
                            ;
                        RowNum++;
                    }
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent); //将值填充到Searchresult中显示
                } else {
                    ResetSearchInfo("");
                }
                jQuery(msg.dataList1).each(function (i, item) {
                    jQuery("#loadExcel").attr("href", item.FilePath);
                });
            }
            else {
                ResetSearchInfo("");
            }
        }
    });
}
/// <summary>
/// 重置检索无结果显示的信息
/// </summary>
function ResetSearchInfo(msgInfo) {
    if (msgInfo == "" || msgInfo == undefined) {
        msgInfo = "在您查询的时间段内，没有找到任何信息！";
    }
    var html = "<tr><td style='text-align: center; line-height: 100px;' colSpan='11'>" + msgInfo + "</td></tr>";
    jQuery('#Searchresult').html(html); //设置无数据检索时显示提示信息
}