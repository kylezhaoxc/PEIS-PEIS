

/// <summary>
/// 查询
/// </summary>
function ButtonClickQuery() {

    jQuery("#QueryExamSectionListArea").hide(); // 分科检查列表
    jQuery("#DivGuideSheetReturn").hide();     // 指引单回收情况

    var templeteContent = jQuery("#GuideSheetReturnMessageTemplete").html();
    if (templeteContent == undefined) { return; }

    var TipsMessageTempleteContent = jQuery('#TipsMessageTemplete').html();     //读取提示信息模版
    if (TipsMessageTempleteContent == undefined) { return; }

    var customerid = jQuery.trim(jQuery('#txtCustomerID').val());    // 体检号
    if (customerid == "") {
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "请输入体检号！"));   //显示信息
        jQuery("#TipsArea").show();
        jQuery("#DivGuideSheetReturn").hide();
    } else {
        if (!isCustomerExamNo(customerid)) {

            jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "你输入的体检号不正确，请确认后再输入！"));   //显示信息
            jQuery("#TipsArea").show();
            jQuery("#DivGuideSheetReturn").hide();

            return;
        } else {
            DoLoad('/System/GuideSheet/GuideSheetReturnOper.aspx?txtCustomerID=' + customerid, '');
        }
    }
}

var isShowReturnedBtn = "0"; // 用于控制是否显示回收按钮
var isGuideSheetReturned = 0; // 标记指引单是否已经被回收
var oldQueryCustomerID = "";  // 记录上次查询的体检号
/// <summary>
/// 自动查询
/// </summary>
function AutoGuideSheetReturnQuery() {

    var curEvent = window.event || e;
    if (curEvent.keyCode == 13) {
        var customerid = jQuery.trim(jQuery('#txtCustomerID').val());    // 体检号
        if (oldQueryCustomerID == customerid) { return; } // 如果与上次一样，则退出
        if (customerid != "") {
            // 如果输入的值满足体检号的条件，则自动跳转到指引单回收页面
            if (isCustomerExamNo(customerid)) {
                oldQueryCustomerID = customerid;
                DoLoad('/System/GuideSheet/GuideSheetReturnOper.aspx?txtCustomerID=' + customerid, '');
            }
        }
    }
}

// 读取分科检查（自动调用）
jQuery(document).ready(function () {

    SwitchHeader(7); // 显示页面自定义头部
    jQuery(".j-autoHeight").autoHeight(); // 自适应高度

    //            // 查询客户的基本信息，并显示
    //            SearchCustomerBaseInfo();

    jQuery("#QueryExamSectionListArea").hide(); // 分科检查列表
    jQuery("#DivGuideSheetReturn").hide();     // 指引单回收情况
    //  ButtonClickQuery(); 
    // 获取指引单信息 及 分科检查列表
    GetGuideSheetReturnExamSectionItem();
});


/// <summary>
/// 获取指引单信息 及 分科检查列表
/// </summary>
function GetGuideSheetReturnExamSectionItem() {
    var CustomerID = jQuery.trim(jQuery('#txtCustomerID').val()); // 体检号
    if (CustomerID == "") {
        return;
    }
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxCustExam.aspx",
        data: { CustomerID: CustomerID,
            action: 'GetGuideSheetReturnExamSectionItem',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的信息（指引单信息 及 分科检查情况 ）
                ShowGuideSheetReturnExamSectionItem_Ajax(jsonmsg);
            }
        }
    });
}


/// <summary>
/// 显示查询到的信息（指引单信息 及 分科检查情况 ）
/// </summary>
function ShowGuideSheetReturnExamSectionItem_Ajax(queryItems) {

    var templeteContent = jQuery("#GuideSheetReturnMessageTemplete").html();
    if (templeteContent == undefined) { return; }

    // 判断是否没有这个体检号
    if (queryItems == null || queryItems == "" || parseInt(queryItems.totalCount) <= 0 || parseInt(queryItems.dataList0.length) <= 0) {

        var TipsMessageTempleteContent = jQuery('#TipsMessageTemplete').html();     //读取提示信息模版
        if (TipsMessageTempleteContent == undefined) { return; }

        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "您输入的体检号错误或不存在，请核对后再操作！"));   //显示信息
        jQuery("#TipsArea").show();
        jQuery("#DivGuideSheetReturn").hide();

        return;
    }

    ShowGuideSheetReturn(queryItems.dataList0); // 显示指引单回收情况
    ShowExamSectionList(queryItems.dataList1);  // 显示分科检查列表

}


/// <summary>
/// 显示指引单回收情况
/// </summary>
function ShowGuideSheetReturn(GSRList) {

    isGuideSheetReturned = 0;
    var templeteContent = jQuery("#GuideSheetReturnShowSuccessTemplete").html();
    if (templeteContent == undefined) { return; }

    jQuery("#TipsArea").hide();

    var newContent = ""; // 用来记录新的数据
    var isShowPrintReceliveCertificateBtn = "0"; // 用于控制是否显示补打按钮
    jQuery(GSRList).each(function (j, GSRitem) {

        newContent += templeteContent
                            .replace(/@ID_Customer/gi, GSRitem.ID_Customer)
                            .replace(/@CustomerName/gi, GSRitem.CustomerName)
                            .replace(/@Is_GuideSheetPrinted/gi, GSRitem.Is_GuideSheetPrinted == "True" ? '√' : '--')
                            .replace(/@Is_GuideSheetReturned/gi, GSRitem.Is_GuideSheetReturned == "True" ? '√' : '--')
                            .replace(/@GuideSheetReturnedby/gi, GSRitem.GuideSheetReturnedby)
                            .replace(/@GuideSheetReturnedDate/gi, GSRitem.GuideSheetReturnedDate)
                            ; 
        jQuery("#hideGenderName").val(GSRitem.GenderName);
        // 判断是否显示指引单回收按钮
        if (GSRitem.Is_GuideSheetPrinted == "True" && GSRitem.Is_GuideSheetReturned != "True") {
            isShowReturnedBtn = "1";
        } else {
            isShowReturnedBtn = "0";
        }
        // 判断是否显示补打按钮
        if (GSRitem.Is_GuideSheetPrinted == "True" && GSRitem.Is_GuideSheetReturned == "True" && GSRitem.Is_Team != "True") {
            isShowPrintReceliveCertificateBtn = 1;
        } else {
            isShowPrintReceliveCertificateBtn = "0";
        }
        jQuery("#Is_Team").val(GSRitem.Is_Team);                        // 是否是团队体检，用于判断回收指引单后是否打印 报告领取凭证。
        jQuery("#ID_CustomerCode128").val(GSRitem.ID_CustomerCode128);  // 128Code 的体检号
        jQuery("#CustomerName").val(GSRitem.CustomerName);              // 客户名称
        jQuery("#Is_GuideSheetReturned").val(GSRitem.Is_GuideSheetReturned);

    });

    jQuery("#GuideSheetReturnTipsInfo").html(newContent);
    jQuery("#DivGuideSheetReturn").show();

    // 控制是否显示指引单回收按钮
    if (isShowReturnedBtn == "1") {
        jQuery("#trGuideSheetReturned").show();
        isGuideSheetReturned = 0;
    } else {
        jQuery("#trGuideSheetReturned").hide();
        isGuideSheetReturned = 1;
    }

    // 控制是否显示补打按钮
    if (isShowPrintReceliveCertificateBtn == "1") {
        jQuery("#trPrintReceliveCertificate").show();
    } else {
        jQuery("#trPrintReceliveCertificate").hide();
    }

    // 如果回收指引单 和 补单按钮都没有显示，则显示回收指引的不可操作的按钮。
    if (jQuery("#trPrintReceliveCertificate").is(':hidden') && jQuery("#trGuideSheetReturned").is(':hidden')) {

        jQuery("#trGuideSheetReturnedDisable").show();

    } else {
        jQuery("#trGuideSheetReturnedDisable").hide();
    }

}

/// <summary>
/// 显示分科检查情况
/// </summary>
function ShowExamSectionList(ExamSectionList) {

    var templeteContent = jQuery("#QueryExamSectionListTemplete").html();
    if (templeteContent == undefined) { return; }
    var strGiveUp = ""; // 记录弃检的标志或按钮
    var newContent = ""; // 用来记录新的数据
    var IsShowBtnAllGiveUp = 0; // 是否显示弃检所有科室的按钮

    var RowNum = 1;

    jQuery(ExamSectionList).each(function (j, examsectionitem) {
        if (isShowReturnedBtn == 0 || examsectionitem.SummaryDoctorName != '') {
            strGiveUp = '--';
            if (examsectionitem.IS_giveup == 'True')                          // 表示已经进行了弃检
            {
                strGiveUp = '√';
            }
        }    // 表示已检
        else if (examsectionitem.IS_giveup == 'True')                          // 表示已经进行了弃检
        {
            strGiveUp = '√';
            if (jQuery("#Is_GuideSheetReturned").val() != "True") {
                // strGiveUp += '&nbsp;&nbsp;&nbsp;<input type="button" name="btnGiveUp_@ID_CustExamSection" id="btnGiveUp_@ID_CustExamSection" onclick="SetExamSectionGiveUp(@ID_CustExamSection,0,@ID_Section);" value=" 恢 复 " />';  // 弃检后，可以进行恢复操作，防止误操作

                strGiveUp = '<a href="javascript:void(0);" name="btnGiveUp_@ID_CustExamSection" id="btnGiveUp_@ID_CustExamSection" onclick="SetExamSectionGiveUp(@ID_CustExamSection,0,@ID_Section);" class="abandoned-seized-cancel">取消</a>';  // 弃检后，可以进行恢复操作，防止误操作

            }

        }
        else {
            if (examsectionitem.InterfaceType == "") {
                IsShowBtnAllGiveUp = 1;
            }

            if (isGuideSheetReturned == 1) { strGiveUp = '--'; }    // 表示已检
            else {
                // strGiveUp = '<input type="button" name="btnGiveUp_@ID_CustExamSection" id="btnGiveUp_@ID_CustExamSection" onclick="SetExamSectionGiveUp(@ID_CustExamSection,1,@ID_Section);" value=" 弃 检 " />';  // 表示可以进行弃检
                strGiveUp = '<a href="javascript:void(0);" class="abandoned-seized" name="btnGiveUp_@ID_CustExamSection" id="btnGiveUp_@ID_CustExamSection" onclick="SetExamSectionGiveUp(@ID_CustExamSection,1,@ID_Section);">弃检</a>';  // 表示可以进行弃检
            }
        }
        if (templeteContent == undefined) { return; }



        newContent += templeteContent.replace(/@RowNum/gi, RowNum)
                        .replace(/@SectionName/gi, examsectionitem.SectionName)
                        .replace(/@ID_Section/gi, examsectionitem.ID_Section)
                        .replace(/@ID_CustExamSection/gi, examsectionitem.ID_CustExamSection)
                        .replace(/@Examed/gi, examsectionitem.SummaryDoctorName == '' ? '--' : '√')
                        .replace(/@SummaryDoctorName/gi, examsectionitem.SummaryDoctorName)
                        .replace(/@SectionSummaryDate/gi, examsectionitem.SummaryDoctorName == '' ? '--' : examsectionitem.SectionSummaryDate)
                        .replace(/@GiveUp/gi, strGiveUp.replace(/@ID_CustExamSection/gi, examsectionitem.ID_CustExamSection).replace(/@ID_Section/gi, examsectionitem.ID_Section))
                        .replace(/@Checked/gi, examsectionitem.Is_Check == 'True' ? '√' : '--')
                        .replace(/@CheckerName/gi, examsectionitem.Is_Check == 'True' ? examsectionitem.CheckerName : '--')
                        .replace(/@CheckDate/gi, examsectionitem.Is_Check == 'True' ? examsectionitem.CheckDate : '--')
                        ;

        RowNum++;
    });

    jQuery("#QueryExamSectionList").html(newContent);
    jQuery("#QueryExamSectionListArea").show();
    if (IsShowBtnAllGiveUp == "1") {
        jQuery("#GiveUpAllSectionAreaDisable").hide(); // 
        jQuery("#GiveUpAllSectionArea").show(); // 不显示弃检所有科室对应的按钮
    } else {
        jQuery("#GiveUpAllSectionArea").hide(); // 显示弃检所有科室对应的按钮
        jQuery("#GiveUpAllSectionAreaDisable").show(); // 
    }

    SetTableRowStyleFocusBg(); // 奇偶行背景

    // 判断表格是否存在滚动条,并设置相应的样式
    JudgeTableIsExistScroll();
}


/// <summary>
/// 指引单回收
/// </summary>
function SetGuideSheetReturnState() {

    var CustomerID = jQuery.trim(jQuery('#txtCustomerID').val());
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxCustExam.aspx",
        data: { CustomerID: CustomerID,
            action: 'SetGuideSheetReturnState',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {

            // 检查Ajax返回数据的状态等 20140430 by wtang 
            jsonmsg = CheckAjaxReturnDataInfo(jsonmsg);
            if (jsonmsg == null || jsonmsg == "") {
                return;
            }


            if (jsonmsg != null && jsonmsg != "") {
                if (parseInt(jsonmsg) > 0) {

                    jQuery("#trGuideSheetReturned").hide();
                    if (jQuery("#Is_Team").val() == "True") {
                        ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#txtCustomerID').val()) + "】指引单回收成功!");
                    } else {
                        PrintReceliveCertificate(); // 打印领取凭证
                    }
                    ButtonClickQuery();
                }
                else if (parseInt(jsonmsg) == 0) { ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#txtCustomerID').val()) + "】指引单回收失败，请与技术人员联系!") }
                else {
                    if (jsonmsg == "-1") {
                        ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#txtCustomerID').val()) + "】不存在!");
                    } else if (jsonmsg == "-2") {
                        ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#txtCustomerID').val()) + "】指引单已经回收过了。");
                    } else if (jsonmsg == "-3") {
                        ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#txtCustomerID').val()) + "】指引单已经回收过了。"); // 表示该体检号已经被总审，说明指引单已经回收过了。
                    } else if (jsonmsg == "-4") {
                        ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#txtCustomerID').val()) + "】指引单已经回收过了。"); // 表示该体检号已经总检，说明指引单已经回收过了。
                    } else if (jsonmsg == "-5") {
                        ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#txtCustomerID').val()) + "】指引单还未打印，不能进行回收。");
                    } else if (jsonmsg == "-6") {
                        ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#txtCustomerID').val()) + "】分科检查还未完成（需要先检查完成，或弃检后才能回收指引单）。");
                    }
                }
            }
        }
    });

}

/// <summary>
/// 设置弃检标志
/// </summary>
function SetExamSectionGiveUp(ID_CustExamSection, State, ID_Section) {

    jQuery("#hiddenID_CustExamSection").val(ID_CustExamSection);
    jQuery("#hiddenIsGiveUp").val(State);
    jQuery("#hiddenID_Section").val(ID_Section);
    var CustomerID = jQuery.trim(jQuery('#txtCustomerID').val());

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxCustExam.aspx",
        data: {
            ID_CustExamSection: ID_CustExamSection,
            Is_GiveUp: State,
            ID_Section: ID_Section,
            CustomerID: CustomerID,
            action: 'SetExamSectionGiveUp',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {

            // 检查Ajax返回数据的状态等 20140430 by wtang 
            jsonmsg = CheckAjaxReturnDataInfo(jsonmsg);
            if (jsonmsg == null || jsonmsg == "") {
                return;
            }


            if (jsonmsg != null && jsonmsg != "") {
                if (parseInt(jsonmsg) > 0) {

                    if (jQuery("#hiddenIsGiveUp").val() == "0") {
                        ShowSystemDialog("恢复检查成功!");
                    } else {
                        ShowSystemDialog("设置弃检成功!");
                    }

                    var strGiveUp = "";
                    var hiddenID_CustExamSection = jQuery("#hiddenID_CustExamSection").val();
                    var hiddenID_Section = jQuery("#hiddenID_Section").val();
                    if (jQuery("#hiddenIsGiveUp").val() == "1")                         //如果当前操作是 弃检
                    {
                        strGiveUp = '√&nbsp;&nbsp;&nbsp;<input type="button" name="btnGiveUp_' + hiddenID_CustExamSection + '" id="btnGiveUp_' + hiddenID_CustExamSection + '" onclick="SetExamSectionGiveUp(' + hiddenID_CustExamSection + ',0,' + hiddenID_Section + ');" value=" 恢 复 " />';  // 弃检后，可以进行恢复操作，防止误操作

                        strGiveUp = '<a href="javascript:void(0);" name="btnGiveUp_' + hiddenID_CustExamSection + '" id="btnGiveUp_' + hiddenID_CustExamSection + '" onclick="SetExamSectionGiveUp(' + hiddenID_CustExamSection + ',0,' + hiddenID_Section + ');" class="abandoned-seized-cancel">取消</a>';  // 弃检后，可以进行恢复操作，防止误操作

                    }
                    else {
                        IsShowBtnAllGiveUp = 1;
                        //strGiveUp = '<input type="button" name="btnGiveUp_' + hiddenID_CustExamSection + '" id="btnGiveUp_' + hiddenID_CustExamSection + '" onclick="SetExamSectionGiveUp(' + hiddenID_CustExamSection + ',1,' + hiddenID_Section + ');" value=" 弃 检 " />';  // 表示可以进行弃检

                        strGiveUp = '<a href="javascript:void(0);" class="abandoned-seized" name="btnGiveUp_' + hiddenID_CustExamSection + '" id="btnGiveUp_' + hiddenID_CustExamSection + '" onclick="SetExamSectionGiveUp(' + hiddenID_CustExamSection + ',1,' + hiddenID_Section + ');">弃检</a>';  // 表示可以进行弃检
                    }

                    jQuery("#tdGiveUp_" + hiddenID_CustExamSection).html(strGiveUp);  // 修改弃检列显示的按钮。

                }
                else {
                    ShowSystemDialog("操作失败，请与技术人员联系!");

                }
            }
        }
    });

}

/// <summary>
/// 设置所有未检科室为弃检状态
/// </summary>
function SetAllNotExamedSectionGiveUp(State) {

    var CustomerID = jQuery.trim(jQuery('#txtCustomerID').val());
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxCustExam.aspx",
        data: { CustomerID: CustomerID,
            Is_GiveUp: State,
            action: 'SetAllNotExamedSectionGiveUp',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {

            // 检查Ajax返回数据的状态等 20140430 by wtang 
            jsonmsg = CheckAjaxReturnDataInfo(jsonmsg);
            if (jsonmsg == null || jsonmsg == "") {
                return;
            }


            if (jsonmsg != null && jsonmsg != "") {
                if (parseInt(jsonmsg) > 0) {

                    ShowSystemDialog("设置弃检成功!");

                    ButtonClickQuery();
                }
                else {
                    ShowSystemDialog("操作失败，请与技术人员联系!");

                }
            }
        }
    });

}


/// <summary>
/// 打印领取凭证
/// </summary>
function PrintReceliveCertificate() {
    var DefaultReceliveDate = jQuery('#DefaultReceliveDate').val();  // 报告领取时间（默认为体检后10天）
    var showReceliveDate = DefaultReceliveDate;
    var FYHSaveReceliveDate = GetCookie('FYHSaveReceliveDate');     //上次保存时间，用于判断是否是当天的时间
    var FYHReceliveDate = GetCookie('FYHReceliveDate');     // 获取上次选择的领取时间
    var exp = new Date(); // 设置当前时间，只有当天才有效 （在取出时，先判断时间是否一致。）
    var CurrentDate = exp.getFullYear() + "-" + exp.getMonth() + '-' + exp.getDay();
    if (CurrentDate == FYHSaveReceliveDate && FYHReceliveDate != "" && FYHReceliveDate != null) {
        showReceliveDate = FYHReceliveDate;  // 如果保存了cookie值，则使用cookie中的值替换默认值
    }

    var tipscontent = '<table class="ModifyPassword">' +
            '<tbody>' +
            '    <tr><td class="left">领取日期：</td><td><input maxlength="30" type="text" name="txtReceliveDate" id="txtReceliveDate" value="' + showReceliveDate + '" onfocus="WdatePicker({minDate:\'%y-%M-#{%d}\', maxDate:\'%y-%M-#{%d+30}\'})" class="Wdate span120" /> &nbsp;</td></tr>' +
            '</tbody>' +
            '</table>';

    art.dialog({
        id: 'OperWindowFrame',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        title: '打印体检报告领取凭证',
        button: [{
            name: '确定',
            callback: function () {

                var TemplateName = "ReportReceliveCertificate.frx";

                var ID_Customer = jQuery.trim(jQuery('#txtCustomerID').val());                // 体检号
                var ID_CustomerCode128 = jQuery('#ID_CustomerCode128').val();    // 128Code 的体检号
                var CustomerName = jQuery('#CustomerName').val();                // 客户名称
                var ReceliveDate = jQuery('#txtReceliveDate').val();             // 获取当前选择的领取日期
                var CurrentDate = exp.getFullYear() + "-" + exp.getMonth() + '-' + exp.getDay();
                if (isDate(ReceliveDate) == false) {
                    ShowSystemDialog("你输入的日期格式有错，请重新输入！");
                    // PrintReceliveCertificate();
                    return false;
                }
                SetCookie('FYHReceliveDate', ReceliveDate); // 将当前选择的领取日期设置到Cookie中
                SetCookie('FYHSaveReceliveDate', CurrentDate); //保存领取日期的时间

                //xmhuang20150508 新增扩展报告内容（体检地点、尊称）
                var curIP = convertIP(jQuery("#ClientIP").val());
                var ExamPlaceName = IPParameters.本部.ExamPlaceName;
                if (parseFloat(convertIP(IPParameters.二部.BeginIP)) <= parseFloat(curIP) && parseFloat(curIP) <= parseFloat(convertIP(IPParameters.二部.EndIP))) {
                    ExamPlaceName = IPParameters.二部.ExamPlaceName;
                }
                var sex = jQuery('#hideGenderName').val();
                var GenderName = "同志";
                if (sex == "男") {
                    GenderName = "先生";
                }
                else if (sex == "女") {
                    GenderName = "女士";
                }
                var detailXml = "<CustomerInfo>";
                detailXml += "<ID_Customer>" + ID_Customer + "</ID_Customer>";
                detailXml += "<ID_CustomerCode128>" + CheckXmlChar(ID_CustomerCode128) + "</ID_CustomerCode128>";
                detailXml += "<CustomerName>" + CheckXmlChar(CustomerName) + "</CustomerName>";
                detailXml += "<RecvDate>" + CheckXmlChar(ReceliveDate) + "</RecvDate>";
                detailXml += "<GenderName>" + CheckXmlChar(GenderName) + "</GenderName>";
                detailXml += "<ExamPlaceName>" + CheckXmlChar(ExamPlaceName) + "</ExamPlaceName>";
                detailXml += "</CustomerInfo>";
                //xmhuang20150508 新增扩展报告内容（体检地点、尊称）

                FastReport.GenerateCustomerRptRecvCertificate(ID_Customer, ID_CustomerCode128, TemplateName, CustomerName, ReceliveDate, detailXml);


            }, focus: true
        }, {
            name: '取消'
        }]
    });



}

