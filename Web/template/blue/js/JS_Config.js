/****************************************

1.	文件名称(File Name)： 	配置公用脚本文件
2.	功能描述(Description):  配置系统参数相关页面数据处理（如，科室的设置，收费项目，检查项目，体征词，结论词等）
3.	作者(Author)：			WTang
4.	日期(Create Date)：		2013.7.2

****************************************/


// ================================ 公用函数区域 ==== start ==================================================


/// <summary>
/// 关闭弹出窗口
/// </summary>
function CloseDialogWindow() {
    parent.art.dialog.get('OperWindowFrame').close();
}

/// <summary>
/// 设置是否自动关闭窗口
/// </summary>
function AutoCloseDialogWindow(flag) {
    jQuery("#IS_AutoCloseDialog").val(flag);
}

// ================================ 公用函数区域 ==== end ==================================================


// ================================ 标本快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询标本列表
/// </summary>
function ShowHideQuickQuerySpecimenTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQuerySpecimenTable").show();
        ShowHideQuickQuerySectionTable(false);
    } else {
        jQuery("#QuickQuerySpecimenTable").hide();
    }
}

var gAllSpecimenDataList = "";    // 保存全部的标本列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldSpecimenInputCode = "****";              // 记录上次输入的输入码
var gAllSpecimenListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询标本（通过Ajax后台过滤）
/// </summary>
function QuickQuerySpecimenTableData_Ajax() {

    var InputCode = jQuery('#txtSpecimenInputCode').val();
    if (OldSpecimenInputCode != InputCode) {
        OldSpecimenInputCode = InputCode;
    } else {
        ShowHideQuickQuerySpecimenTable(true, InputCode);
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { InputCode: InputCode,
            action: 'GetQuickSpecimenList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的标本
            ShowQuickQuerySpecimenTableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示标本（通过Ajax过滤）
/// </summary>
function ShowQuickQuerySpecimenTableData_Ajax(Specimenlist) {
    if (Specimenlist == "" || Specimenlist.totalCount == 0) {

        ShowHideQuickQuerySpecimenTable(true, jQuery('#txtSpecimenInputCode').val());
        // 显示没有找到标本信息
        jQuery("#QuickQuerySpecimenTableData").html(jQuery('#EmptySpecimenQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //标本table内容

        var SpecimenQuickQueryTableTempleteContent = jQuery('#SpecimenQuickQueryTableTemplete').html();             //快速查询标本列表模版
        if (SpecimenQuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (Specimenlist != "") {
            jQuery(Specimenlist.dataList).each(function (j, Specimenitem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += SpecimenQuickQueryTableTempleteContent
                            .replace(/@ID_Specimen/gi, Specimenitem.ID_Specimen)
                            .replace(/@SpecimenName/gi, Specimenitem.SpecimenName)
                            .replace(/@InputCode/gi, Specimenitem.InputCode)
                            .replace(/@chkSpecimenQueryList/gi, "chkSpecimenQueryList")
                            ;
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQuerySpecimenTable(true, jQuery('#txtSpecimenInputCode').val());
            jQuery("#QuickQuerySpecimenTableData").html(conclusionContent);
        } else {
            ShowHideQuickQuerySpecimenTable(false);
            jQuery("#QuickQuerySpecimenTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择标本）
/// </summary>
function SelectSpecimenDataList() {
    var selectedItemContent = "";
    var UserSelectedSpecimenItemDataTempleteContent = jQuery('#UserSelectedSpecimenItemDataTemplete').html();             //用户已经选择的标本列表模版
    if (UserSelectedSpecimenItemDataTempleteContent == undefined) { return; }
    
    jQuery("input[name='chkSpecimenQueryList']:radio:checked").each(function () {
        if (jQuery("#chkUserSpecimen_" + jQuery(this).val()).val() == undefined) {
            selectedItemContent = UserSelectedSpecimenItemDataTempleteContent
                            .replace(/@ID_Specimen/gi, jQuery(this).val())
                            .replace(/@SpecimenName/gi, jQuery(this).attr("Specimenname"))
                            ;
            jQuery("#spanSelectSpecimen").html(selectedItemContent);
            jQuery("#spanSelectSpecimen").show();
            jQuery("#idSelectSpecimen").val(jQuery(this).val());
            jQuery("#nameSelectSpecimen").val(jQuery(this).attr("Specimenname"));
            jQuery("#spanSpecimen").hide();
        }
    });

    ShowHideQuickQuerySpecimenTable(false);
}

/// <summary>
/// 删除选择的标本
/// </summary>
function RemoveSelectedSpecimen() {
    jQuery('#spanSelectSpecimen').hide();
    jQuery('#spanSpecimen').show();
    jQuery('#spanSelectSpecimen').html('');
    jQuery('#idSelectSpecimen').val('');
    jQuery('#nameSelectSpecimen').val('');
}



// ================================ 标本快速选择函数区域 ==== end ==================================================


// ================================ 报告合并收费项目快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询报告合并收费项目列表
/// </summary>
function ShowHideQuickQueryFeeReportMergerTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQueryFeeReportMergerTable").show();
        ShowHideQuickQuerySectionTable(false);
    } else {
        jQuery("#QuickQueryFeeReportMergerTable").hide();
    }
}

var gAllFeeReportMergerDataList = "";    // 保存全部的报告合并收费项目列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldFeeReportMergerInputCode = "****";              // 记录上次输入的输入码
var gAllFeeReportMergerListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询报告合并收费项目（通过Ajax后台过滤）
/// </summary>
function QuickQueryFeeReportMergerTableData_Ajax() {

    var InputCode = jQuery('#txtFeeReportMergerInputCode').val();
    if (OldFeeReportMergerInputCode != InputCode) {
        OldFeeReportMergerInputCode = InputCode;
    } else {
        ShowHideQuickQueryFeeReportMergerTable(true, InputCode);
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { InputCode: InputCode,
            action: 'GetQuickFeeReportMergerList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的报告合并收费项目
            ShowQuickQueryFeeReportMergerTableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示报告合并收费项目（通过Ajax过滤）
/// </summary>
function ShowQuickQueryFeeReportMergerTableData_Ajax(FeeReportMergerlist) {
    if (FeeReportMergerlist == "" || FeeReportMergerlist.totalCount == 0) {

        ShowHideQuickQueryFeeReportMergerTable(true, jQuery('#txtFeeReportMergerInputCode').val());
        // 显示没有找到报告合并收费项目信息
        jQuery("#QuickQueryFeeReportMergerTableData").html(jQuery('#EmptyFeeReportMergerQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //报告合并收费项目table内容

        var FeeReportMergerQuickQueryTableTempleteContent = jQuery('#FeeReportMergerQuickQueryTableTemplete').html();             //快速查询报告合并收费项目列表模版
        if (FeeReportMergerQuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (FeeReportMergerlist != "") {
            jQuery(FeeReportMergerlist.dataList).each(function (j, FeeReportMergeritem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += FeeReportMergerQuickQueryTableTempleteContent
                            .replace(/@ID_FeeReportMerger/gi, FeeReportMergeritem.ID_FeeReportMerger)
                            .replace(/@FeeReportMergerName/gi, FeeReportMergeritem.FeeReportMergerName)
                            .replace(/@InputCode/gi, FeeReportMergeritem.InputCode)
                            .replace(/@chkFeeReportMergerQueryList/gi, "chkFeeReportMergerQueryList")
                            ;
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQueryFeeReportMergerTable(true, jQuery('#txtFeeReportMergerInputCode').val());
            jQuery("#QuickQueryFeeReportMergerTableData").html(conclusionContent);
        } else {
            ShowHideQuickQueryFeeReportMergerTable(false);
            jQuery("#QuickQueryFeeReportMergerTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择报告合并收费项目）
/// </summary>
function SelectFeeReportMergerDataList() {
    var selectedItemContent = "";
    var UserSelectedFeeReportMergerItemDataTempleteContent = jQuery('#UserSelectedFeeReportMergerItemDataTemplete').html();             //用户已经选择的报告合并收费项目列表模版
    if (UserSelectedFeeReportMergerItemDataTempleteContent == undefined) { return; }
    
    jQuery("input[name='chkFeeReportMergerQueryList']:radio:checked").each(function () {
        //if (jQuery("#chkFeeReportMerger_" + jQuery(this).val()).val() == undefined) {
        selectedItemContent = UserSelectedFeeReportMergerItemDataTempleteContent
                            .replace(/@ID_FeeReportMerger/gi, jQuery(this).val())
                            .replace(/@FeeReportMergerName/gi, jQuery(this).attr("FeeReportMergername"))
                            ;
        jQuery("#spanSelectFeeReportMerger").html(selectedItemContent);
        jQuery("#spanSelectFeeReportMerger").show(); 
        jQuery("#idSelectFeeReportMerger").val(jQuery(this).attr("FeeReportMergerValue"));
        jQuery("#nameSelectFeeReportMerger").val(jQuery(this).attr("FeeReportMergername"));
        jQuery("#spanFeeReportMerger").hide();
        //}
    });

    ShowHideQuickQueryFeeReportMergerTable(false);
}

/// <summary>
/// 删除选择的报告合并收费项目
/// </summary>
function RemoveSelectedFeeReportMerger() {
    jQuery('#spanSelectFeeReportMerger').hide();
    jQuery('#spanFeeReportMerger').show();
    jQuery('#spanSelectFeeReportMerger').html('');
    jQuery('#idSelectFeeReportMerger').val('');
    jQuery('#nameSelectFeeReportMerger').val('');
}



// ================================ 报告合并收费项目快速选择函数区域 ==== end ==================================================


// ================================ 科室快速选择函数区域 ==== start ==================================================



/// <summary>
/// 隐藏，显示快速查询科室列表
/// </summary>
function ShowHideQuickQuerySectionTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQuerySectionTable").show();
        ShowHideQuickQuerySpecimenTable(false);
    } else {
        jQuery("#QuickQuerySectionTable").hide();
    }
}



var gAllSectionDataList = "";    // 保存全部的科室列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldSectionInputCode = "****";              // 记录上次输入的输入码
var gAllSectionListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询科室（通过Ajax后台过滤）
/// </summary>
function QuickQueryTableData_Ajax() {

    var InputCode = jQuery('#txtSectionInputCode').val();
    if (OldSectionInputCode != InputCode) {
        OldSectionInputCode = InputCode;
    } else {
        ShowHideQuickQuerySectionTable(true, InputCode);
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

    if (sectionlist == "" || sectionlist.totalCount == 0) {

        ShowHideQuickQuerySectionTable(true, jQuery('#txtSectionInputCode').val());
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
            ShowHideQuickQuerySectionTable(true, jQuery('#txtSectionInputCode').val());
            jQuery("#QuickQuerySectionTableData").html(conclusionContent);
        } else {
            ShowHideQuickQuerySectionTable(false);
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

    jQuery("input[name='chkSectionQueryList']:radio:checked").each(function () {
        if (jQuery("#chkUserSection_" + jQuery(this).val()).val() == undefined) {
            selectedItemContent = UserSelectedSectionItemDataTempleteContent
                            .replace(/@ID_Section/gi, jQuery(this).val())
                            .replace(/@SectionName/gi, jQuery(this).attr("sectionname"))
                            ;
            jQuery("#spanSelectSection").html(selectedItemContent);
            jQuery("#spanSelectSection").show();
            jQuery("#idSelectSection").val(jQuery(this).val());
            jQuery("#nameSelectSection").val(jQuery(this).attr("sectionname"));
            jQuery("#spanSection").hide();
        }
    });

    ShowHideQuickQuerySectionTable(false);
}

/// <summary>
/// 删除选择的科室
/// </summary>
function RemoveSelectedSection() {
    jQuery('#spanSelectSection').hide(); 
    jQuery('#spanSection').show();
    jQuery('#spanSelectSection').html('');
    jQuery('#idSelectSection').val('');
    jQuery('#nameSelectSection').val('');
}

// ================================ 科室快速选择函数区域 ==== start ==================================================


// ================================ 检查项目快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询检查项目列表
/// </summary>
function ShowHideQuickQueryExamItemTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQueryExamItemTable").show();
        ShowHideQuickQuerySpecimenTable(false);
    } else {
        jQuery("#QuickQueryExamItemTable").hide();
    }
}



var gAllExamItemDataList = "";    // 保存全部的检查项目列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldExamItemInputCode = "****";              // 记录上次输入的输入码
var gAllExamItemListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询检查项目（通过Ajax后台过滤）
/// </summary>
function QuickQueryExamItemTableData_Ajax() {

    var InputCode = jQuery('#txtExamItemInputCode').val();
    if (OldExamItemInputCode != InputCode) {
        OldExamItemInputCode = InputCode;
    } else {
        ShowHideQuickQueryExamItemTable(true, InputCode);
        return;
    }
    
    var ID_Section = jQuery('#ID_Section').val();
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { InputCode: InputCode, 
            ID_Section: ID_Section,
            action: 'GetQuickExamItemList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的检查项目
            ShowQuickQueryExamItemTableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示检查项目（通过Ajax过滤）
/// </summary>
function ShowQuickQueryExamItemTableData_Ajax(ExamItemlist) {
    if (ExamItemlist == "" || ExamItemlist.totalCount == 0) {

        ShowHideQuickQueryExamItemTable(true, jQuery('#txtExamItemInputCode').val());
        // 显示没有找到检查项目信息
        jQuery("#QuickQueryExamItemTableData").html(jQuery('#EmptyExamItemQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //检查项目table内容

        var ExamItemQuickQueryTableTempleteContent = jQuery('#ExamItemQuickQueryTableTemplete').html();             //快速查询检查项目列表模版
        if (ExamItemQuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (ExamItemlist != "") {
            jQuery(ExamItemlist.dataList).each(function (j, ExamItemitem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += ExamItemQuickQueryTableTempleteContent
                            .replace(/@ID_ExamItem/gi, ExamItemitem.ID_ExamItem)
                            .replace(/@ExamItemName/gi, ExamItemitem.ExamItemName)
                            .replace(/@SectionName/gi, ExamItemitem.SectionName)
                            .replace(/@ExamItemCode/gi, ExamItemitem.ExamItemCode)
                            .replace(/@InputCode/gi, ExamItemitem.InputCode)
                            .replace(/@chkExamItemQueryList/gi, "chkExamItemQueryList")
                            .replace(/@Note/gi, ExamItemitem.Note)
                            ;
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQueryExamItemTable(true, jQuery('#txtExamItemInputCode').val());
            jQuery("#QuickQueryExamItemTableData").html(conclusionContent);
        } else {
            ShowHideQuickQueryExamItemTable(false);
            jQuery("#QuickQueryExamItemTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择检查项目）
/// </summary>
function SelectExamItemDataList() {
    var selectedItemContent = "";
    var UserSelectedExamItemItemDataTempleteContent = jQuery('#UserSelectedExamItemItemDataTemplete').html();             //用户已经选择的检查项目列表模版
    if (UserSelectedExamItemItemDataTempleteContent == undefined) { return; }

    jQuery("input[name='chkExamItemQueryList']:checkbox:checked").each(function () {
        if (jQuery("#chkSelectedExamItem_" + jQuery(this).val()).val() == undefined) {
            selectedItemContent = UserSelectedExamItemItemDataTempleteContent
                            .replace(/@ID_ExamItem/gi, jQuery(this).val())
                            .replace(/@ExamItemName/gi, jQuery(this).attr("ExamItemname"))
                            .replace(/@chkSelectedExamItemList/gi, "chkSelectedExamItemList")
                            ;
            jQuery("#tmpSelectedExamItemList").append(selectedItemContent);
        }
    });

    ShowHideQuickQueryExamItemTable(false);
}

/// <summary>
/// 删除选择的检查项目
/// </summary>
function RemoveSelectedExamItem() {
    jQuery('#spanSelectExamItem').hide();
    jQuery('#spanExamItem').show();
    jQuery('#spanSelectExamItem').html('');
    jQuery('#idSelectExamItem').val('');
    jQuery('#nameSelectExamItem').val('');
}


// ================================ 检查项目快速选择函数区域 ==== start ==================================================



// ================================ 收费项目快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询收费项目列表
/// </summary>
function ShowHideQuickQueryFeeTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQueryFeeTable").show();
        ShowHideQuickQuerySpecimenTable(false);
    } else {
        jQuery("#QuickQueryFeeTable").hide();
    }
}



var gAllFeeDataList = "";    // 保存全部的收费项目列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldFeeInputCode = "****";              // 记录上次输入的输入码
var gAllFeeListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询收费项目（通过Ajax后台过滤）
/// </summary>
function QuickQueryFeeTableData_Ajax() {

    var InputCode = jQuery('#txtFeeInputCode').val();
    if (OldFeeInputCode != InputCode) {
        OldFeeInputCode = InputCode;
    } else {
        ShowHideQuickQueryFeeTable(true, InputCode);
        return;
    }

    var ID_Section = jQuery('#ID_Section').val();
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { InputCode: InputCode,
            ID_Section: ID_Section,
            action: 'GetQuickFeeList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的收费项目
            ShowQuickQueryFeeTableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示收费项目（通过Ajax过滤）
/// </summary>
function ShowQuickQueryFeeTableData_Ajax(Feelist) {
    if (Feelist == "" || Feelist.totalCount == 0) {

        ShowHideQuickQueryFeeTable(true, jQuery('#txtFeeInputCode').val());
        // 显示没有找到收费项目信息
        jQuery("#QuickQueryFeeTableData").html(jQuery('#EmptyFeeQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //收费项目table内容

        var FeeQuickQueryTableTempleteContent = jQuery('#FeeQuickQueryTableTemplete').html();             //快速查询收费项目列表模版
        if (FeeQuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (Feelist != "") {
            jQuery(Feelist.dataList).each(function (j, Feeitem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += FeeQuickQueryTableTempleteContent
                            .replace(/@ID_Fee/gi, Feeitem.ID_Fee)
                            .replace(/@FeeName/gi, Feeitem.FeeName)
                            .replace(/@SectionName/gi, Feeitem.SectionName)
                            .replace(/@FeeCode/gi, Feeitem.FeeCode)
                            .replace(/@InputCode/gi, Feeitem.InputCode)
                            .replace(/@chkFeeQueryList/gi, "chkFeeQueryList")
                            ;
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQueryFeeTable(true, jQuery('#txtFeeInputCode').val());
            jQuery("#QuickQueryFeeTableData").html(conclusionContent);
        } else {
            ShowHideQuickQueryFeeTable(false);
            jQuery("#QuickQueryFeeTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择收费项目）
/// </summary>
function SelectFeeDataList() {
    var selectedItemContent = "";
    var UserSelectedFeeItemDataTempleteContent = jQuery('#UserSelectedFeeItemDataTemplete').html();             //用户已经选择的收费项目列表模版
    if (UserSelectedFeeItemDataTempleteContent == undefined) { return; }

    jQuery("input[name='chkFeeQueryList']:checkbox:checked").each(function () {
        if (jQuery("#chkSelectedFee_" + jQuery(this).val()).val() == undefined) {
            selectedItemContent = UserSelectedFeeItemDataTempleteContent
                            .replace(/@ID_Fee/gi, jQuery(this).val())
                            .replace(/@FeeName/gi, jQuery(this).attr("FeeName"))
                            .replace(/@chkSelectedFeeList/gi, "chkSelectedFeeList")
                            ;
            jQuery("#tmpSelectedFeeList").append(selectedItemContent);
        }
    });

    ShowHideQuickQueryFeeTable(false);
}

/// <summary>
/// 删除选择的收费项目
/// </summary>
function RemoveSelectedFee() {
    jQuery('#spanSelectFee').hide();
    jQuery('#spanFee').show();
    jQuery('#spanSelectFee').html('');
    jQuery('#idSelectFee').val('');
    jQuery('#nameSelectFee').val('');
}


// ================================ 收费项目快速选择函数区域 ==== start ==================================================




// ================================ 结论词快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询结论词列表
/// </summary>
function ShowHideQuickQueryConclusionTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQueryConclusionTable").show();
        ShowHideQuickQuerySectionTable(false);
    } else {
        jQuery("#QuickQueryConclusionTable").hide();
    }
}

var gAllConclusionDataList = "";    // 保存全部的结论词列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldConclusionInputCode = "****";              // 记录上次输入的输入码
var gAllConclusionListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询结论词（通过Ajax后台过滤）
/// </summary>
function QuickQueryConclusionTableData_Ajax() {
    var InputCode = jQuery('#txtConclusionInputCode').val();
    if (OldConclusionInputCode != InputCode) {
        OldConclusionInputCode = InputCode;
    } else {
        ShowHideQuickQueryConclusionTable(true, InputCode);
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConclusion.aspx",
        data: { InputCode: InputCode,
            action: 'GetConclusionByKeyWords',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的结论词
            ShowQuickQueryConclusionTableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示结论词（通过Ajax过滤）
/// </summary>
function ShowQuickQueryConclusionTableData_Ajax(Conclusionlist) {
    if (Conclusionlist == "" || Conclusionlist.totalCount == 0) {

        ShowHideQuickQueryConclusionTable(true, jQuery('#txtConclusionInputCode').val());
        // 显示没有找到结论词信息
        jQuery("#QuickQueryConclusionTableData").html(jQuery('#EmptyConclusionQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //结论词table内容

        var ConclusionQuickQueryTableTempleteContent = jQuery('#ConclusionQuickQueryTableTemplete').html();             //快速查询结论词列表模版
        if (ConclusionQuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (Conclusionlist != "") {
            jQuery(Conclusionlist.dataList).each(function (j, Conclusionitem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += ConclusionQuickQueryTableTempleteContent
                            .replace(/@ID_Conclusion/gi, Conclusionitem.ID_Conclusion)
                            .replace(/@ConclusionName/gi, Conclusionitem.ConclusionName)
                            .replace(/@InputCode/gi, Conclusionitem.InputCode)
                            .replace(/@ConclusionTypeName/gi, Conclusionitem.ConclusionTypeName)
                            .replace(/@chkConclusionQueryList/gi, "chkConclusionQueryList")
                            ;
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQueryConclusionTable(true, jQuery('#txtConclusionInputCode').val());
            jQuery("#QuickQueryConclusionTableData").html(conclusionContent);
        } else {
            ShowHideQuickQueryConclusionTable(false);
            jQuery("#QuickQueryConclusionTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择结论词）
/// </summary>
function SelectConclusionDataList() {
    var selectedItemContent = "";
    var UserSelectedConclusionItemDataTempleteContent = jQuery('#UserSelectedConclusionItemDataTemplete').html();             //用户已经选择的结论词列表模版
    if (UserSelectedConclusionItemDataTempleteContent == undefined) { return; }

    jQuery("input[name='chkConclusionQueryList']:radio:checked").each(function () {
        if (jQuery("#chkUserConclusion_" + jQuery(this).val()).val() == undefined) {
            selectedItemContent = UserSelectedConclusionItemDataTempleteContent
                            .replace(/@ID_Conclusion/gi, jQuery(this).val())
                            .replace(/@ConclusionName/gi, jQuery(this).attr("Conclusionname"))
                            ;
            jQuery("#spanSelectConclusion").html(selectedItemContent);
            jQuery("#spanSelectConclusion").show();
            jQuery("#idSelectConclusion").val(jQuery(this).val());
            jQuery("#nameSelectConclusion").val(jQuery(this).attr("Conclusionname"));
            jQuery("#spanConclusion").hide();
        }
    });

    ShowHideQuickQueryConclusionTable(false);
}

/// <summary>
/// 删除选择的结论词
/// </summary>
function RemoveSelectedConclusion() {
    jQuery('#spanSelectConclusion').hide();
    jQuery('#spanConclusion').show();
    jQuery('#spanSelectConclusion').html('');
    jQuery('#idSelectConclusion').val('');
    jQuery('#nameSelectConclusion').val('');
}



// ================================ 结论词快速选择函数区域 ==== end ==================================================




// ================================ 收费项目管理 ==== start ==================================================


/// <summary>
/// 获取收费项目单条数据的模版内容
/// </summary>
function GetFeePagesListItemTemplete() {

    // 从模版中读取数据加载列表
    var templateContent = jQuery('#FeePagesListItemTemplete').html();
    if (templateContent == undefined) { return ""; }
    else { return templateContent; }
}

/// <summary>
/// 保存收费项目参数。 (保存)
/// </summary>
function SaveFeeItemInfo() {


    // 获取保存时提交的参数。 (参数提取)
    var FeeItemParams = GetSaveFeeItemParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过
    
    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: FeeItemParams,         // 收费项目参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                if (jQuery("#ID_Fee").val() != "") {
                    parent.GetSingleFeeItem(jQuery("#ID_Fee").val(), "edit");
                }
                else {
                    parent.GetSingleFeeItem(jsonmsg, "add");
                }
                parent.ShowSystemDialogCloseDialog("保存收费项目成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据

                    RemoveSelectedSection(); 
                    RemoveSelectedSpecimen();
                }
            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialog("收费项目名称：【" + jQuery("#txtFeeName").val() + "】已经存在，请先禁用同名的收费项目，然后再进行添加！");
            }
            else { parent.ShowSystemDialog("保存收费项目失败，请与技术人员联系!") }
        }
    });

}

/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveFeeItemParams() {

    var ID_Fee = jQuery("#ID_Fee").val();       // 收费项目ID
    var FeeName = jQuery("#txtFeeName").val();              // 收费项目名称
    FeeName = encodeURIComponent(FeeName);                  // 编码处理
    var ReportFeeName = jQuery("#txtReportFeeName").val();  // 收费项目报告名称
    ReportFeeName = encodeURIComponent(ReportFeeName);      // 编码处理
    var InterfaceName = jQuery("#txtInterfaceName").val();  // 收费项目接口名称
    InterfaceName = encodeURIComponent(InterfaceName);      // 编码处理
    var FeeCode = jQuery("#txtFeeCode").val();              // 收费项目代码
    var Price = jQuery("#txtPrice").val();                  // 价格
    var ID_Section = jQuery("#idSelectSection").val();      // 科室ID
    var ID_Specimen = jQuery("#idSelectSpecimen").val();    // 标本ID
    var SectionName = jQuery("#nameSelectSection").val();      // 科室名称
    var SpecimenName = jQuery("#nameSelectSpecimen").val();    // 标本名称
    var DispOrder = jQuery("#txtDispOrder").val();          // 排序编号
    var BreakfastOrder = jQuery("input[name='radioBreakfastOrder']:checked").val() ;    // 早餐顺序
    var Forsex = jQuery("input[name='radioForsex']:checked").val();               // 适用于
    var WorkGroupCode = jQuery("#txtWorkGroupCode").val();          // 工作组代码
    var WorkStationCode = jQuery("#txtWorkStationCode").val();      // 工作站代码
    var WorkBenchCode = jQuery("#txtWorkBenchCode").val();          // 工作台代码
    WorkGroupCode = encodeURIComponent(WorkGroupCode);              // 工作组代码 编码处理
    WorkStationCode = encodeURIComponent(WorkStationCode);          // 工作站代码 编码处理
    WorkBenchCode = encodeURIComponent(WorkBenchCode);              // 工作台代码 编码处理 
    var Is_Banned = jQuery("input[name='radioIs_Banned']:checked").val();   // 是否被禁用
    var BanDescribe = jQuery("#txtBanDescribe").val();              // 禁用说明
    BanDescribe = encodeURIComponent(BanDescribe);                  // 禁用说明 编码处理
    var Note = jQuery("#txtNote").val();                            // 收费项目说明
    Note = encodeURIComponent(Note);                                // 收费项目说明 编码处理
    var Is_FeeNonPrintInReport = jQuery("input[name='radioIs_FeeNonPrintInReport']:checked").val();       // 不打报告

    var IS_FeeReportMerger = jQuery("input[name='radioIS_FeeReportMerger']:checked").val();   // 是否在报告上合并显示 0：不合并 1：合并 2：以自己作为合并父级进行合并。
    var ID_FeeReportMerger = 0;
    var FeeReportMergerName = "";
    // 如果选择的是合并，则取选择的值
    if (IS_FeeReportMerger == 1) {
        ID_FeeReportMerger = jQuery("#idSelectFeeReportMerger").val();     // 合并显示的ID
        FeeReportMergerName = jQuery("#nameSelectFeeReportMerger").val();  // 合并显示的名称
    }

    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (FeeName == "") {
        jQuery('#txtFeeName').focus();
        parent.ShowSystemDialog("请输入收费项目名称!");
        return;
    }

    if (ReportFeeName == "") {
        jQuery('#txtReportFeeName').focus();
        parent.ShowSystemDialog("请输入收费项目报告名称!");
        return;
    }

    if (Price == "") {
        jQuery('#txtPrice').focus();
        parent.ShowSystemDialog("请输入收费项目价格!");
        return;
    }

    if (IS_FeeReportMerger == 1 && ID_FeeReportMerger == 0) {
        jQuery('#txtFeeReportMergerInputCode').focus();
        parent.ShowSystemDialog("请选择隶属收费项目!");
        return;
    }


    var strOperationalDate = "";
    jQuery("input[name='chkOperationalDate']:checkbox").each(function () {

        if (jQuery(this).attr("checked") == 'checked') {
            if (strOperationalDate == "") { strOperationalDate = jQuery(this).val(); }
            else { strOperationalDate = strOperationalDate + ',' + jQuery(this).val(); }
        }
    });
    if (strOperationalDate != "") {
        // 目前固定加上按星期的规则进行解析
        strOperationalDate = 'week:' + strOperationalDate;
    }

    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var SectionSummaryParams = { 
        FeeName: FeeName,
        ID_Fee: ID_Fee,
        ReportFeeName: ReportFeeName,
        InterfaceName: InterfaceName,
        FeeCode: FeeCode,
        Price: Price,
        ID_Specimen: ID_Specimen,
        IS_FeeReportMerger: IS_FeeReportMerger,
        ID_FeeReportMerger: ID_FeeReportMerger,
        FeeReportMergerName: FeeReportMergerName,
        ID_Section: ID_Section,
        SectionName: SectionName,
        SpecimenName: SpecimenName,
        DispOrder: DispOrder,
        BreakfastOrder: BreakfastOrder,
        Forsex: Forsex,
        WorkGroupCode: WorkGroupCode,
        WorkStationCode: WorkStationCode,
        WorkBenchCode: WorkBenchCode,
        Is_Banned: Is_Banned,
        Is_FeeNonPrintInReport: Is_FeeNonPrintInReport,
        BanDescribe: BanDescribe,
        Note: Note,
        OperationalDate: encodeURIComponent(strOperationalDate),
        action: 'SaveFeeItemInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return SectionSummaryParams; // 返回拼接后的参数

}

/// <summary>
/// 弹出收费项目设置页面
/// </summary>
function OpenFeeOperWindow() {
    OpenFeeOperWindowParams("");
}

/// <summary>
/// 弹出收费项目设置页面(收费项目ID编号)
/// </summary>
function OpenEditFeeItemWindow() {
    var FeeID = jQuery("input[name='FeeItemRadio']:checked").val();
    if (FeeID == undefined) {
        ShowSystemDialog("请选择你要修改的收费项目！");
        return;
    }
    OpenFeeOperWindowParams(FeeID);
}

/// <summary>
/// 弹出收费项目设置页面(收费项目ID编号)
/// </summary>
function OpenFeeOperWindowParams(FeeID) {
    
    var url = '/System/Config/Fee/FeeOper.aspx?num=' + Math.random();
    if (FeeID != "") {
        url = url + "&FeeID=" + FeeID;
    }
    art.dialog.open(url,
    { 
        width: 500,
        height: 600, 
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "收费项目设置" 
    });
}

/// <summary>
/// 弹出收费项目明细设置页面
/// </summary>
function OpenFeeExamRelOperWindow() {

    var FeeID = jQuery("input[name='FeeItemRadio']:checked").val();
    if (FeeID == undefined) {
        ShowSystemDialog("请选择需要添加明细的检查项目！");
        return;
    }
    var feename = jQuery("#rdiFeeItem_" + FeeID).attr("feename");
    var sectionid = jQuery("#rdiFeeItem_" + FeeID).attr("sectionid");
    feename = encodeURIComponent(feename);
    
    var tmpUrl = "/System/Config/Fee/FeeExamRelOper.aspx?FeeID="+FeeID+"&sectionid="+sectionid+"&feename="+feename+"&num=" + Math.random();
    art.dialog.open(tmpUrl,
    {
        width: 600,
        height: 390,
        lock: true,
        drag: false,
        id: 'OperWindowFrame',
        title: "收费项目明细设置" 
    });
}

function BtnSaveResizeToSmall() {

    jQuery("#btnSave").hide(); // 如果是编辑操作，则不显示"保存&继续"按钮
    jQuery("#btnSaveclose").val("保存"); // 修改 "保存&关闭" 按钮 名称为 "保存"
    jQuery("#btnSaveclose").removeClass("btn-bcbgb");
    jQuery("#btnSaveclose").removeClass("button105");
    jQuery("#btnSaveclose").addClass("btn-bc-s");
    jQuery("#btnSaveclose").addClass("button60");
}

/// <summary>
/// 初始化收费项目设置页面
/// </summary>
function InitFeeEditData() {
    if (jQuery("#ID_Fee").val() == "") {
        return;
    }
    BtnSaveResizeToSmall();

    var selectedItemContent = "";
    if (jQuery("#idSelectSection").val() != "") {

        var UserSelectedSectionItemDataTempleteContent = jQuery('#UserSelectedSectionItemDataTemplete').html();             //用户已经选择的科室列表模版
        if (UserSelectedSectionItemDataTempleteContent == undefined) { return; }

        selectedItemContent = UserSelectedSectionItemDataTempleteContent
                            .replace(/@ID_Section/gi, jQuery("#idSelectSection").val())
                            .replace(/@SectionName/gi, jQuery("#nameSelectSection").val())
                            ;
        jQuery("#spanSelectSection").html(selectedItemContent);
        jQuery("#spanSelectSection").show();
        jQuery("#spanSection").hide();
    }

    if (jQuery("#idSelectSpecimen").val() != "") {

        selectedItemContent = "";
        var UserSelectedSpecimenItemDataTempleteContent = jQuery('#UserSelectedSpecimenItemDataTemplete').html();             //用户已经选择的标本列表模版
        if (UserSelectedSpecimenItemDataTempleteContent == undefined) { return; }

        selectedItemContent = UserSelectedSpecimenItemDataTempleteContent
                    .replace(/@ID_Specimen/gi, jQuery("#idSelectSpecimen").val())
                    .replace(/@SpecimenName/gi, jQuery("#nameSelectSpecimen").val())
                    ;
        jQuery("#spanSelectSpecimen").html(selectedItemContent);
        jQuery("#spanSelectSpecimen").show();
        jQuery("#spanSpecimen").hide();

    }


    jQuery("input[name='radioForsex']").each(function () {
        if (jQuery('#txtForsex').val() == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });

    jQuery("input[name='radioBreakfastOrder']").each(function () {
        if (jQuery('#txtBreakfastOrder').val() == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });

    jQuery("input[name='radioIs_Banned']").each(function () {

        var tmpIsBanned = jQuery('#txtIs_Banned').val()=="True"?1:0;
        if (tmpIsBanned == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });


    // 不打报告
    jQuery("input[name='radioIs_FeeNonPrintInReport']").each(function () {

        var tmpvalue = jQuery('#txtIs_FeeNonPrintInReport').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });


    if (jQuery("#idSelectFeeReportMerger").val() != "") {
        selectedItemContent = "";
        var UserSelectedFeeReportMergerItemDataTempleteContent = jQuery('#UserSelectedFeeReportMergerItemDataTemplete').html();             //用户已经选择的标本列表模版
        if (UserSelectedFeeReportMergerItemDataTempleteContent == undefined) { return; }

        selectedItemContent = UserSelectedFeeReportMergerItemDataTempleteContent
                    .replace(/@ID_FeeReportMerger/gi, jQuery("#idSelectFeeReportMerger").val())
                    .replace(/@FeeReportMergerName/gi, jQuery("#nameSelectFeeReportMerger").val())
                    ;
        jQuery("#spanSelectFeeReportMerger").html(selectedItemContent);
        jQuery("#spanSelectFeeReportMerger").show();
        jQuery("#spanFeeReportMerger").hide();

    }

    jQuery("input[name='radioIS_FeeReportMerger']").each(function () {

        var tmpIsBanned = jQuery('#txtIS_FeeReportMerger').val() == "True" ? 1 : 0;
        if (jQuery('#idSelectFeeReportMerger').val() == jQuery('#ID_Fee').val()) {
            tmpIsBanned = 2;
        }
        if (tmpIsBanned == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

    if (jQuery("#txtOperationalDate").val() != "") {
        SetOperationalDate(); // 设置开设日期  20140916 by wtang 
    }
}



/*********设置开设日期 Begin**** 20140916 by wtang ***************/
function SetOperationalDate() {
    var curWeekIndex = "";
    var allWeekDayStr = "";
    var OperationalDate = jQuery("#txtOperationalDate").val();
    //按星期开设
    if (OperationalDate.indexOf("week") > -1) {
        var weekArray = OperationalDate.split(':');
        if (weekArray.length > 1) {
            var weekDays = weekArray[1];
            if (weekDays != "") {
                var allWeekDayArray = weekDays.split(',');

                for (var i = 0; i < allWeekDayArray.length; i++) {
                    if (allWeekDayArray[i] != "") {
                        curWeekIndex = GetWeekIndex(allWeekDayArray[i]);
                        jQuery("#chkOperationalDate0" + curWeekIndex).attr("checked", true);
                    }
                }
            }
        }
    }
    //按天开设
    else if (OperationalDate.indexOf("day") > -1) {

    }
}

//中文星期数组 
var CHWeekArray = ["星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
//英文日期数组
var ENWeekArray = ["SunDay", "Monday", "TuesDay", "WednesDay", "Thursday", "Friday", "Saturday"];
function GetWeekIndex(WeekName) {
    for (var i = 0; i < CHWeekArray.length; i++) {
        if (CHWeekArray[i].toLowerCase() == WeekName.toLowerCase() || ENWeekArray[i].toLowerCase() == WeekName.toLowerCase()) {
            return i;
        }
    }
}
// ================================ 收费项目管理 ==== end ==================================================


// ================================ 收费项目明细管理 ==== start ==================================================


/// <summary>
/// 保存收费项目明细信息（确认）
/// </summary>
function SaveFeeExamRelConfirm() {
    var tipscontent = "您正在保存收费项目明细，提交后将不能再进行删除，请确认是否继续！";
    parent.art.dialog({
        id: 'testID',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        button: [{
            name: '确定保存',
            title: '保存收费项目明细',
            callback: function () {
                SaveFeeExamRel(); // 保存收费项目明细信息
                return true;
            }, focus: true
        }, {
            name: '取消'
        }]
    });


}


/// <summary>
/// 保存收费项目明细信息
/// </summary>
function SaveFeeExamRel() {

    var ID_Fee = jQuery("#ID_Fee").val();                        // 收费项目ID
    var OrgExamItemIDStrs = jQuery("#OrgExamItemIDStrs").val();  // 原收费项目ID字符串

    var newExamItemIDStrs = ""; // 当前选择的检查项目ID字符串
    var nCount = 0;
    jQuery("input[name='chkSelectedExamItemList']:checkbox").each(function () {
        if(nCount == 0){ newExamItemIDStrs = jQuery(this).val(); }
        else{
            newExamItemIDStrs = newExamItemIDStrs +","+jQuery(this).val(); 
        }
        nCount ++;

    });


    if ((OrgExamItemIDStrs == "" && newExamItemIDStrs == "") || OrgExamItemIDStrs == newExamItemIDStrs) {

        parent.ShowSystemDialogCloseDialog("保存收费项目明细成功!", jQuery("#IS_AutoCloseDialog").val());

    } else {

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { action: 'SaveFeeExamRel',
            newExamItemIDStrs: newExamItemIDStrs,
            ID_Fee: ID_Fee,
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 判断操作结果
                if (parseInt(jsonmsg) > 0) {

                    parent.GetExamItemListByFee(jQuery("#ID_Fee").val()); // 刷新父页面的该收费项目明细

                    parent.ShowSystemDialogCloseDialog("保存收费项目明细成功!", jQuery("#IS_AutoCloseDialog").val());

                }
                else { parent.ShowSystemDialog("保存收费项目明细失败，请与技术人员联系!") }
            }
        }
    });

    }

}

// ================================ 收费项目明细管理 ==== end ==================================================



// ================================ 检查项目管理 ==== start ==================================================


/// <summary>
/// 保存检查项目参数。 (保存)
/// </summary>
function SaveExamItemInfo() {

    // 获取保存时提交的参数。 (参数提取)
    var ExamItemParams = GetSaveExamItemParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作


    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: ExamItemParams,         // 检查项目参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                if (jQuery("#ID_ExamItem").val() != "") {
                    parent.GetSingleExamItem(jQuery("#ID_ExamItem").val(), "edit");
                }
                else {
                    parent.GetSingleExamItem(jsonmsg, "add");
                }

                parent.ShowSystemDialogCloseDialog("保存检查项目成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据
                    RemoveSelectedSection();
                }
            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialog("检查项目名称：【" + jQuery("#txtExamItemName").val() + "】,项目编码：【" + jQuery("#txtExamItemCode").val() + "】,备注：【" + jQuery("#txtNote").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialog("保存检查项目失败，请与技术人员联系!") }
        }
    });

}

/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveExamItemParams() {

    var ID_ExamItem = jQuery("#ID_ExamItem").val();         // 检查项目ID
    var ExamItemName = jQuery("#txtExamItemName").val();    // 检查项目名称
    ExamItemName = encodeURIComponent(ExamItemName);        // 检查项目名称 编码处理
    var ID_Section = jQuery("#idSelectSection").val();      // 科室ID
    var SectionName = jQuery("#nameSelectSection").val();   // 科室名称
    SectionName = encodeURIComponent(SectionName);          // 科室名称 编码处理
    var GetResultWay = jQuery("input[name='radioGetResultWay']:checked").val();             // 结果获取途径
    var AbbrExamName = jQuery("#txtAbbrExamName").val();    // 缩写
    AbbrExamName = encodeURIComponent(AbbrExamName);        // 缩写 编码处理

    var ExamItemCode = jQuery("#txtExamItemCode").val();                                    // 检查项目编码
    var Is_LisValueNull = jQuery("input[name='radioIs_LisValueNull']:checked").val();       // 检验值允许为空
    var Is_EntrySectSum = jQuery("input[name='radioIs_EntrySectSum']:checked").val();       // 是否进入科室小结
    var EntrySectSumLevel = jQuery("#txtEntrySectSumLevel").val();                          // 进入科室小结级别
    var Is_AutoCalc = jQuery("input[name='radioIs_AutoCalc']:checked").val();               // 自动计算
    var CalcExpression = jQuery("#txtCalcExpression").val();            // 计算公式
    CalcExpression = encodeURIComponent(CalcExpression);                // 计算公式 编码处理

    var SymCols = jQuery("#txtSymCols").val();                          // 体征词列数
    var TextboxRows = jQuery("#txtTextboxRows").val();                  // 文本框的高度
    var Is_SameRow = jQuery("input[name='radioIs_SameRow']:checked").val(); // 同行

    var ExamItemUnit = jQuery("#txtExamItemUnit").val();                // 检查项目单位
    ExamItemUnit = encodeURIComponent(ExamItemUnit);                    // 检查项目单位 编码处理
    var MaleLoLimit = jQuery("#txtMaleLoLimit").val();                  // 男低值
    var MaleHiLimit = jQuery("#txtMaleHiLimit").val();                  // 男高值
    var FemaleLoLimit = jQuery("#txtFemaleLoLimit").val();              // 女低值
    var FemaleHiLimit = jQuery("#txtFemaleHiLimit").val();              // 女高值
    var Is_SymMultiValue = jQuery("input[name='radioIs_SymMultiValue']:checked").val();     // 是否多值
    var Forsex = jQuery("input[name='radioForsex']:checked").val();                   // 适用于 0:女士 1：男士，2:男女均适用
    var Note = jQuery("#txtNote").val();                                // 检查项目说明
    Note = encodeURIComponent(Note);                                    // 检查项目说明 编码处理
    var DispOrder = jQuery("#txtDispOrder").val();                      // 排序编号

    var Is_ExamItemNonPrintInReport = jQuery("input[name='radioIs_ExamItemNonPrintInReport']:checked").val();       // 不打报告
    
    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (ExamItemName == "") {
        jQuery('#txtExamItemName').focus();
        parent.ShowSystemDialog("请输入检查项目名称!");
        return;
    }

    if (ID_Section == "") {
        parent.ShowSystemDialog("请选择科室!");
        jQuery('#txtSectionInputCode').focus();
        return;
    }

    if (GetResultWay == "" || GetResultWay == undefined) {
        parent.ShowSystemDialog("请选择结果类型!");
        return;
    }

    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var ExamItemInfoParams = {
        ExamItemName: ExamItemName,
        ID_ExamItem: ID_ExamItem,
        ID_Section: ID_Section,
        SectionName: SectionName,
        GetResultWay: GetResultWay,
        AbbrExamName: AbbrExamName,
        ExamItemCode: ExamItemCode,
        Is_LisValueNull: Is_LisValueNull,
        Is_EntrySectSum: Is_EntrySectSum,
        EntrySectSumLevel: EntrySectSumLevel,
        Is_AutoCalc: Is_AutoCalc,
        Is_SameRow: Is_SameRow,
        CalcExpression: CalcExpression,
        SymCols: SymCols,
        TextboxRows: TextboxRows,
        ExamItemUnit: ExamItemUnit,
        MaleLoLimit: MaleLoLimit,
        MaleHiLimit: MaleHiLimit,
        FemaleLoLimit: FemaleLoLimit,
        FemaleHiLimit: FemaleHiLimit,
        Is_SymMultiValue: Is_SymMultiValue,
        Forsex: Forsex,
        Note: Note,
        DispOrder: DispOrder,
        Is_ExamItemNonPrintInReport: Is_ExamItemNonPrintInReport,
        action: 'SaveExamItemInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return ExamItemInfoParams; // 返回拼接后的参数

}



/// <summary>
/// 初始化检查项目设置页面
/// </summary>
function InitExamItemEditData() {
    if (jQuery("#ID_ExamItem").val() == "") {
        return;
    }

    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

    var selectedItemContent = "";
    if (jQuery("#idSelectSection").val() != "") {

        var UserSelectedSectionItemDataTempleteContent = jQuery('#UserSelectedSectionItemDataTemplete').html();             //用户已经选择的科室列表模版
        if (UserSelectedSectionItemDataTempleteContent == undefined) { return; }

        selectedItemContent = UserSelectedSectionItemDataTempleteContent
                            .replace(/@ID_Section/gi, jQuery("#idSelectSection").val())
                            .replace(/@SectionName/gi, jQuery("#nameSelectSection").val())
                            ;
        jQuery("#spanSelectSection").html(selectedItemContent);
        jQuery("#spanSelectSection").show();
        jQuery("#spanSection").hide();
    }

    // 适用于 0：女性 1：男性 2：男女均可
    jQuery("input[name='radioForsex']").each(function () {
        if (jQuery('#txtForsex').val() == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });
    // 
    jQuery("input[name='radioGetResultWay']").each(function () {
        if (jQuery('#txtGetResultWay').val() == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });
    // 允许为空
    jQuery("input[name='radioIs_LisValueNull']").each(function () {

        var tmpvalue = jQuery('#txtIs_LisValueNull').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

    // 是否进入科室小结
    jQuery("input[name='radioIs_EntrySectSum']").each(function () {

        var tmpvalue = jQuery('#txtIs_EntrySectSum').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

    // 允许自动计算
    jQuery("input[name='radioIs_AutoCalc']").each(function () {

        var tmpvalue = jQuery('#txtIs_AutoCalc').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

    // 是否多值 
    jQuery("input[name='radioIs_SymMultiValue']").each(function () {

        var tmpvalue = jQuery('#txtIs_SymMultiValue').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

    // 同行 
    jQuery("input[name='radioIs_SameRow']").each(function () {

        var tmpvalue = jQuery('#txtIs_SameRow').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });


    // 不打报告
    jQuery("input[name='radioIs_ExamItemNonPrintInReport']").each(function () {

        var tmpvalue = jQuery('#txtIs_ExamItemNonPrintInReport').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });

}



/// <summary>
/// 弹出检查项目设置页面
/// </summary>
function OpenEditExamItemWindow() {
    var ExamItemID = jQuery("input[name='ExamItemRadio']:checked").val();
    if (ExamItemID == undefined) {
        ShowSystemDialog("请选择你要修改的检查项目！");
        return;
    }
    OpenExamItemOperWindowParams(ExamItemID);
}

/// <summary>
/// 弹出新增检查项目页面
/// </summary>
function OpenExamItemOperWindow() {
    OpenExamItemOperWindowParams("");
}

/// <summary>
/// 弹出检查项目设置页面(检查项目ID编号)
/// </summary>
function OpenExamItemOperWindowParams(ExamItemID) {

    var url = '/System/Config/Exam/ExamOper.aspx?num=' + Math.random();
    if (ExamItemID != "") {
        url = url + "&ExamItemID=" + ExamItemID;
    }
    art.dialog.open(url,
    {
        width: 450,
        height: 515,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "检查项目设置",
        init: function () {
            jQuery(".aui_close").hide();
        },
        close: function () {

        }
    });
}


/// <summary>
/// 根据收费项目ID，查找收费项目明细 (获取)
/// </summary>
function GetExamItemListByFee(FeeID) {

    jQuery("#rdiFeeItem_" + FeeID).attr("checked", true);

    jQuery("#FeeItemName").html(jQuery("#rdiFeeItem_" + FeeID).attr("feename"));
    
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { action: 'GetExamDetailListByFee',
                FeeID: FeeID,
                currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的权限信息
                ShowExamItemListByFee(jsonmsg);
            }
        }
    });
}

/// <summary>
/// 根据收费项目ID，查找收费项目明细 (显示)
/// </summary>
function ShowExamItemListByFee(examlist) {

    var tempListContent = "";  // 临时内容

    // 检查项目显示模版
    var SymptomListItemTempleteContent = jQuery('#ExamListItemTemplete').html();
    if (SymptomListItemTempleteContent == undefined) { return; }

    jQuery("#FeeExamList").html(""); //   先清空列表
    jQuery("#SymptomListItem").html(""); //   先清空列表（体征词）

    var Forsex01 = ""; // 男性是否选中
    var Forsex00 = ""; // 女性是否选中
    var ExamItemCount = 0;
    
    var tempFirstItemID = 0;  // 记录第一个数据的ID
    // dataList0 显示所有的角色信息
    jQuery(examlist.dataList0).each(function (j, examitem) {
        ExamItemCount ++;
        if (examitem.Forsex == '2') {
            Forsex01 = "√";
            Forsex00 = "√";
        } else if (examitem.Forsex == '1') {
            Forsex01 = "√";
            Forsex00 = "-";
        } else {
            Forsex01 = "-";
            Forsex00 = "√";
        }
        tempListContent += SymptomListItemTempleteContent
                        .replace(/@ID_ExamItem/gi, examitem.ID_ExamItem)
                        .replace(/@ExamName/gi, examitem.ExamItemName)
                        .replace(/@InputCode/gi, examitem.InputCode)
                        .replace(/@Is_SymMultiValue/gi, examitem.Is_SymMultiValue == 'True' ? '√' : ' ')
                        .replace(/@TextboxRows/gi, examitem.TextboxRows)
                        .replace(/@Is_SameRow/gi, examitem.Is_SameRow == 'True' ? '√' : ' ')
                        .replace(/@Forsex00/gi, Forsex00)
                        .replace(/@Forsex01/gi, Forsex01)
                        .replace(/@SymCols/gi, examitem.SymCols)

                        .replace(/@Is_LisValueNull/gi, examitem.Is_LisValueNull == 'True' ? '√' : ' ')
                        .replace(/@ExamItemCode/gi, examitem.ExamItemCode)
                        .replace(/@EntrySectSumLevel/gi, examitem.EntrySectSumLevel)
                        .replace(/@ExamItemUnit/gi, examitem.ExamItemUnit)
                        .replace(/@MaleLoLimit/gi, examitem.MaleLoLimit)
                        .replace(/@MaleHiLimit/gi, examitem.MaleHiLimit)
                        .replace(/@FemaleLoLimit/gi, examitem.FemaleLoLimit)
                        .replace(/@FemaleHiLimit/gi, examitem.FemaleHiLimit)
                        .replace(/@Note/gi, examitem.Note);


        // 记录第一个检查项目的ID
        if (tempFirstItemID == 0) {
            tempFirstItemID = examitem.ID_ExamItem;
        }
                        
    });
    jQuery("#ExamItemCount").html(" 【共有 " + ExamItemCount + " 个检查项目】");
    jQuery("#ExamItemName").html("体征词列表（预览）");
    jQuery("#SymptomItemCount").html("");

    jQuery("#FeeExamList").html(tempListContent);  // 显示每列的数据

    // 自动读取第一行对应的体征词
    if (tempFirstItemID != 0) { GetSymptomDetailListByExamID(tempFirstItemID); }
}


/// <summary>
/// 根据收费项目ID，查找收费项目明细 (在明细设置页面中使用)
/// </summary>
function GetExamItemListByFee_Ex01(FeeID) {

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { action: 'GetExamDetailListByFee',
            FeeID: FeeID,
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的权限信息
                ShowExamItemListByFee_Ex01(jsonmsg);
            }
        }
    });
}

/// <summary>
/// 根据收费项目ID，查找收费项目明细 (在明细设置页面中使用，显示)
/// </summary>
function ShowExamItemListByFee_Ex01(examlist) {

    var tempListContent = "";    // 临时内容
    var OrgExamItemIDStrs = "";  // 已经选择的检查项目ID
    // 检查项目显示模版
    var UserSelectedExamItemItemDataTempleteContent = jQuery('#UserSelectedExamItemItemDataTemplete_CantDel').html();
    if (UserSelectedExamItemItemDataTempleteContent == undefined) { return; }

    jQuery("#tmpSelectedExamItemList").html(""); //   先清空列表
    var nCount = 0;
    // dataList0 显示所有的已选择的检查项目信息
    jQuery(examlist.dataList0).each(function (j, examitem) {
        // 将已经选择的检查项目ID拼接后，保存下来。
        if (nCount == 0) {
            OrgExamItemIDStrs = examitem.ID_ExamItem;
        } else {
            OrgExamItemIDStrs = OrgExamItemIDStrs + "," + examitem.ID_ExamItem;
        }

        nCount++;

        tempListContent += UserSelectedExamItemItemDataTempleteContent
                            .replace(/@ID_ExamItem/gi, examitem.ID_ExamItem)
                            .replace(/@ExamItemName/gi, examitem.ExamItemName)
                            .replace(/@chkSelectedExamItemList/gi, "chkSelectedExamItemList")
                            ;
    });
    jQuery("#tmpSelectedExamItemList").append(tempListContent);
    jQuery("#OrgExamItemIDStrs").val(OrgExamItemIDStrs);
}


// ================================ 检查项目管理 ==== end ==================================================




// ================================ 体征词管理 ==== start ==================================================



/// <summary>
/// 根据检查项目ID，查找体征词明细 (获取)
/// </summary>
function GetSymptomDetailListByExamID(ID_ExamItem) {

    jQuery("#rdi_ExamItem_" + ID_ExamItem).attr("checked", true);

    jQuery("#ExamItemName").html(jQuery("#rdi_ExamItem_" + ID_ExamItem).attr("examname"));

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { action: 'GetSymptomDetailListByExamID',
                ID_ExamItem: ID_ExamItem,
                currenttime: encodeURIComponent(new Date())
        },
        cache: false, 
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的信息
                ShowSymptomDetailListByExamID(jsonmsg);
            }
        }
    });
}

/// <summary>
/// 根据检查项目ID，查找体征词明细 (显示)
/// </summary>
function ShowSymptomDetailListByExamID(symptomlist) {

    var tempListContent = "";  // 临时内容

    // 检查项目显示模版
    var SymptomListItemTempleteContent = jQuery('#SymptomListItemTemplete').html();
//    var Is_ShowSymptomEditRadio = jQuery("#Is_ShowSymptomEditRadio").val();
//    if (Is_ShowSymptomEditRadio == "True") {
//        SymptomListItemTempleteContent = jQuery('#SymptomListItemTemplete').html();
//    }
    if (SymptomListItemTempleteContent == undefined) { return; }

    jQuery("#SymptomListItem").html(""); //   先清空列表

    var SymptomItemCount = 0;
    // dataList0 显示所有的角色信息
    jQuery(symptomlist.dataList0).each(function (j, symptomitem) {
        SymptomItemCount++;
        tempListContent += SymptomListItemTempleteContent
                        .replace(/@ID_Symptom/gi, symptomitem.ID_Symptom)
                        .replace(/@SymptomName/gi, symptomitem.SymptomName)
                        .replace(/@ConclusionName/gi, symptomitem.ConclusionName)
                        .replace(/@InputCode/gi, symptomitem.InputCode)
                        .replace(/@Is_Default/gi, symptomitem.Is_Default == 'True' ? '√' : ' ')
                        .replace(/@Is_Banned/gi, symptomitem.Is_Banned == 'True' ? '<a title="' + symptomitem.BanOperator + ':' + symptomitem.BanDate + ' ">√</a>' : ' ')
                        .replace(/@DiseaseLevel/gi, symptomitem.DiseaseLevel)
                        .replace(/@NumOperSign/gi, symptomitem.NumOperSign)
                        .replace(/@NumMale/gi, symptomitem.NumMale)
                        .replace(/@NumFemale/gi, symptomitem.NumFemale)
                        .replace(/@DispOrder/gi, symptomitem.DispOrder);
    });

    jQuery("#SymptomItemCount").html(" 【共有 " + SymptomItemCount + " 个体征词】");
    jQuery("#SymptomListItem").html(tempListContent);  // 显示每列的数据

}

/// <summary>
/// 弹出体征词设置页面
/// </summary>
function OpenSymptomOperWindow() {

    var ExamItemID = jQuery("input[name='ExamItemRadio']:checked").val();
    if (ExamItemID == undefined) {
        ShowSystemDialog("请选择新增体征词隶属检查项目！");
        return;
    }
    OpenSymptomOperWindowParams("");
}

/// <summary>
/// 弹出体征词设置页面(体征词ID编号)
/// </summary>
function OpenEditSymptomItemWindow() {
    var SymptomID = jQuery("input[name='SymptomItemRadio']:checked").val();
    if (SymptomID == undefined) {
        ShowSystemDialog("请选择你要修改的检查项目！");
        return;
    }
    OpenSymptomOperWindowParams(SymptomID);
}

/// <summary>
/// 弹出体征词设置页面(体征词ID编号)
/// </summary>
function OpenSymptomOperWindowParams(SymptomID) {

    var ExamItemID = jQuery("input[name='ExamItemRadio']:checked").val();
    var url = '/System/Config/Exam/SymptomOper.aspx?num=' + Math.random();
    if (SymptomID != "") {
        url = url + "&SymptomID=" + SymptomID;
    }
    if (ExamItemID != "") {
        url = url + "&ExamItemID=" + ExamItemID;
    }
    art.dialog.open(url,
    {
        width: 450,
        height: 370,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "体征词设置"
    });
}


/// <summary>
/// 保存体征词参数。 (保存)
/// </summary>
function SaveSymptomInfo() {

    // 获取保存时提交的参数。 (参数提取)
    var SymptomParams = GetSaveSymptomParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作


    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: SymptomParams,         // 体征词参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                parent.GetSymptomDetailListByExamID(jQuery("#ID_ExamItem").val());

                parent.ShowSystemDialogCloseDialog("保存体征词成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据
                }
            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialog("体征词名称【" + jQuery("#txtSymptomName").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialog("保存体征词失败，请与技术人员联系!") }
        }
    });

}



/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveSymptomParams() {

    var ID_ExamItem = jQuery("#ID_ExamItem").val();                 // 检查项目ID

    var ID_Symptom = jQuery("#ID_Symptom").val();                   // 体征词ID
    var SymptomName = jQuery("#txtSymptomName").val();              // 体征词名称
    SymptomName = encodeURIComponent(SymptomName);                  // 体征词名称 编码处理
    var ID_Conclusion = jQuery("#idSelectConclusion").val();        // 结论词ID
    var ConclusionName = jQuery("#nameSelectConclusion").val();     // 结论词
    ConclusionName = encodeURIComponent(ConclusionName);            // 结论词 编码处理


    var DiseaseLevel = jQuery("#txtDiseaseLevel").val();                    // 病症级别
    var NumOperSign = jQuery("#txtNumOperSign").val();                      // 运算操作符号
    NumOperSign = encodeURIComponent(NumOperSign);                          // 运算操作符号 编码处理
    var NumMale = jQuery("#txtNumMale").val();                              // 男操作数
    var NumFemale = jQuery("#txtNumFemale").val();                          // 女操作数
    var Is_Default = jQuery("input[name='radioIs_Default']:checked").val(); // 是否默认
    
    var SymptomDescribe = jQuery("#txtSymptomDescribe").val();              // 体征词备注
    SymptomDescribe = encodeURIComponent(SymptomDescribe);                  // 体征词说明 编码处理
    var DispOrder = jQuery("#txtDispOrder").val();                          // 排序编号

    var Is_Banned = jQuery("input[name='radioIs_Banned']:checked").val();   // 是否被禁用

    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (SymptomName == "") {
        jQuery('#txtSymptomName').focus();
        parent.ShowSystemDialog("请输入体征词名称!");
        return;
    }
    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var SymptomInfoParams = {
        SymptomName: SymptomName,
        ID_ExamItem: ID_ExamItem,
        ID_Symptom: ID_Symptom,
        ID_Conclusion: ID_Conclusion,
        ConclusionName: ConclusionName,
        DiseaseLevel: DiseaseLevel,
        NumOperSign: NumOperSign,
        NumMale: NumMale,
        NumFemale: NumFemale,
        Is_Default: Is_Default,
        SymptomDescribe: SymptomDescribe,
        DispOrder: DispOrder,
        Is_Banned: Is_Banned,
        action: 'SaveSymptomInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return SymptomInfoParams; // 返回拼接后的参数

}


/// <summary>
/// 初始化体征词设置页面
/// </summary>
function InitSymptomEditData() {

    if (jQuery("#ID_Symptom").val() == "") {
        return;
    }
    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

    var selectedItemContent = "";
    if (jQuery("#idSelectConclusion").val() != "") {

        var UserSelectedConclusionItemDataTempleteContent = jQuery('#UserSelectedConclusionItemDataTemplete').html();             //用户已经选择的结论词列表模版
        if (UserSelectedConclusionItemDataTempleteContent == undefined) { return; }

        selectedItemContent = UserSelectedConclusionItemDataTempleteContent
                            .replace(/@ID_Conclusion/gi, jQuery("#idSelectConclusion").val())
                            .replace(/@ConclusionName/gi, jQuery("#nameSelectConclusion").val())
                            ;
        jQuery("#spanSelectConclusion").html(selectedItemContent);
        jQuery("#spanSelectConclusion").show();
        jQuery("#spanConclusion").hide();
    }

    // 默认值
    jQuery("input[name='radioIs_Default']").each(function () {

        var tmpvalue = jQuery('#txtIs_Default').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

    jQuery("input[name='radioIs_Banned']").each(function () {

        var tmpIsBanned = jQuery('#txtIs_Banned').val() == "True" ? 1 : 0;
        if (tmpIsBanned == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

}



// ================================ 体征词管理 ==== end ==================================================

// ================================ 科室管理 ==== start ==================================================



/// <summary>
/// 弹出科室设置页面
/// </summary>
function OpenEditSectionWindow() {
    var SectionID = jQuery("input[name='SectionRadio']:checked").val();
    if (SectionID == undefined) {
        parent.ShowSystemDialog("请选择你要修改的科室！");
        return;
    }
    OpenSectionOperWindowParams(SectionID);
}

/// <summary>
/// 弹出新增科室页面
/// </summary>
function OpenSectionOperWindow() {
    OpenSectionOperWindowParams("");
}

/// <summary>
/// 弹出科室设置页面(科室ID编号)
/// </summary>
function OpenSectionOperWindowParams(SectionID) {
    
    var url = '/System/Config/Section/SectionOper.aspx?num=' + Math.random();
    if (SectionID != "") {
        url = url + "&SectionID=" + SectionID;
    }
    art.dialog.open(url,
    {
        width: 500,
        height: 530,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "科室设置"
    });
}

/// <summary>
/// 保存科室参数。 (保存)
/// </summary>
function SaveSectionInfo() {

    // 获取保存时提交的参数。 (参数提取)
    var SectionParams = GetSaveSectionParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作


    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: SectionParams,         // 科室参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                if (jQuery("#ID_Section").val() != "") {
                    parent.GetSingleSectionItem(jQuery("#ID_Section").val(), "edit");
                }
                else {
                    parent.GetSingleSectionItem(jsonmsg, "add");
                }

                parent.ShowSystemDialogCloseDialog("保存科室成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据
                }
            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialog("科室名称【" + jQuery("#txtSectionName").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialog("保存科室失败，请与技术人员联系!") }
        }
    });

}



/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveSectionParams() {

    var ID_Section = jQuery("#ID_Section").val();                           // 科室ID
    var SectionName = jQuery("#txtSectionName").val();                      // 科室名称
    SectionName = encodeURIComponent(SectionName);                          // 科室名称 编码处理
    var Is_NonFunction = jQuery("input[name='radioIs_NonFunction']:checked").val();   // 非功能科室
    var Is_AutoApprove = jQuery("input[name='radioIs_AutoApprove']:checked").val();   // 自动审核

    var Is_OwnInterface = jQuery("input[name='radioIs_OwnInterface']:checked").val(); // 有接口
    var InterfaceType = jQuery("#selInterfaceType").val();                  // 接口类型
    var PacsInterfaceFlag = jQuery("#txtPacsInterfaceFlag").val();          // 接口标志
    PacsInterfaceFlag = encodeURIComponent(PacsInterfaceFlag);              // 接口标志 编码处理
    var ImageType = jQuery("#selImageType").val();                          // 影像类型

    var Is_NonPrintSectSummary = jQuery("input[name='radioIs_NonPrintSectSummary']:checked").val();   // 小结是否打印
    var Is_NotSamePage = jQuery("input[name='radioIs_NotSamePage']:checked").val(); // 报告打印方式
    var PicPrintSetup = jQuery("input[name='radioPicPrintSetup']:checked").val();   // 图片打印设置
    var SummaryName = jQuery("#txtSummaryName").val();                              // 小结命名
    SummaryName = encodeURIComponent(SummaryName);                                  // 小结命名 编码处理

    var DispOrder = jQuery("#txtDispOrder").val();                                  // 排列顺序
    var DefaultSummary = jQuery("#txtDefaultSummary").val();                        // 缺省小结
    DefaultSummary = encodeURIComponent(DefaultSummary);                            // 缺省小结 编码处理
    var SepBetweenExamItems = jQuery("#txtSepBetweenExamItems").val();              // 项目分隔符
    SepBetweenExamItems = encodeURIComponent(SepBetweenExamItems);                  // 项目分隔符 编码处理

    var SepBetweenSymptoms = jQuery("#txtSepBetweenSymptoms").val();                // 体征词分隔符
    SepBetweenSymptoms = encodeURIComponent(SepBetweenSymptoms);                    // 体征词分隔符 编码处理
    var TerminalSymbol = jQuery("#txtTerminalSymbol").val();                        // 项目终结符
    TerminalSymbol = encodeURIComponent(TerminalSymbol);                            // 项目终结符 编码处理

    var SepExamAndValue = jQuery("#txtSepExamAndValue").val();                      // 小结分隔符
    SepExamAndValue = encodeURIComponent(SepExamAndValue);                          // 小结分隔符 编码处理
    var NoBetweenExamItems = jQuery("#txtNoBetweenExamItems").val();                // 项目序号
    NoBetweenExamItems = encodeURIComponent(NoBetweenExamItems);                    // 项目序号 编码处理

    var NoBetweenSympotms = jQuery("#txtNoBetweenSympotms").val();                  // 体征词序号
    NoBetweenSympotms = encodeURIComponent(NoBetweenSympotms);                      // 体征词序号 编码处理

    var Is_NoEntryFinalSummary = jQuery("input[name='radioIs_NoEntryFinalSummary']:checked").val(); // 不进入总检综述
    var Is_NonPrintInReport = jQuery("input[name='radioIs_NonPrintInReport']:checked").val();       // 不打报告

    var Note = jQuery("#txtNote").val();                                            // 备注
    Note = encodeURIComponent(Note);                                                // 备注 编码处理

    var Is_Banned = jQuery("input[name='radioIs_Banned']:checked").val();   // 是否被禁用
    //  var BanDescribe = jQuery("#txtBanDescribe").val();              // 禁用说明
    //  BanDescribe = encodeURIComponent(BanDescribe);                  // 禁用说明 编码处理
    
    var DisplayMenu = jQuery("#txtDisplayMenu").val();              // 显示所属菜单
    DisplayMenu = encodeURIComponent(DisplayMenu);                  // 显示所属菜单 编码处理

    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (SectionName == "") {
        jQuery('#txtSectionName').focus();
        parent.ShowSystemDialog("请输入科室名称!");
        return;
    }
    if (SummaryName == "") {
        jQuery('#txtSummaryName').focus();
        parent.ShowSystemDialog("请输入小结命名!");
        return;
    }
    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var SectionInfoParams = {
        ID_Section: ID_Section,
        SectionName: SectionName,
        Is_NonFunction: Is_NonFunction,
        Is_AutoApprove: Is_AutoApprove,
        Is_OwnInterface: Is_OwnInterface,
        InterfaceType: InterfaceType,
        PacsInterfaceFlag: PacsInterfaceFlag,
        ImageType: ImageType,
        Is_NonPrintSectSummary: Is_NonPrintSectSummary,
        Is_NotSamePage: Is_NotSamePage,
        PicPrintSetup: PicPrintSetup,
        SummaryName: SummaryName,
        DispOrder: DispOrder,
        DefaultSummary: DefaultSummary,
        SepBetweenExamItems: SepBetweenExamItems,
        SepBetweenSymptoms: SepBetweenSymptoms,
        TerminalSymbol: TerminalSymbol,
        SepExamAndValue: SepExamAndValue,
        NoBetweenExamItems: NoBetweenExamItems,
        NoBetweenSympotms: NoBetweenSympotms,
        Is_NoEntryFinalSummary: Is_NoEntryFinalSummary,
        Is_NonPrintInReport: Is_NonPrintInReport,
        Note: Note,
        DisplayMenu: DisplayMenu,
        Is_Banned: Is_Banned,
        action: 'SaveSectionInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return SectionInfoParams; // 返回拼接后的参数

}

/// <summary> 
/// 点击选中对应科室的单选按钮
/// </summary>
function SetSectionChecked(ID_Section) {
    jQuery("#rdiSection_" + ID_Section).attr("checked", true);
}


/// <summary>
/// 初始化科室设置页面
/// </summary>
function InitSectionEditData() {

    if (jQuery("#ID_Section").val() == "") {
        return;
    }
    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

    // 非功能科室
    jQuery("input[name='radioIs_NonFunction']").each(function () {
        var tmpvalue = jQuery('#txtIs_NonFunction').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });
    // 自动审核
    jQuery("input[name='radioIs_AutoApprove']").each(function () {
        var tmpvalue = jQuery('#txtIs_AutoApprove').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });
    // 有接口
    jQuery("input[name='radioIs_OwnInterface']").each(function () {

        var tmpvalue = jQuery('#txtIs_OwnInterface').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });
    // 小结是否打印
    jQuery("input[name='radioIs_NonPrintSectSummary']").each(function () {

        var tmpvalue = jQuery('#txtIs_NonPrintSectSummary').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });
    // 报告打印方式
    jQuery("input[name='radioIs_NotSamePage']").each(function () {

        var tmpvalue = jQuery('#txtIs_NotSamePage').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });
    // 不进入总检综述
    jQuery("input[name='radioIs_NoEntryFinalSummary']").each(function () {

        var tmpvalue = jQuery('#txtIs_NoEntryFinalSummary').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });
    // 不打报告
    jQuery("input[name='radioIs_NonPrintInReport']").each(function () {

        var tmpvalue = jQuery('#txtIs_NonPrintInReport').val() == "True" ? 1 : 0;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });
    // 图片打印设置 
    jQuery("input[name='radioPicPrintSetup']").each(function () {

        var tmpvalue = jQuery('#txtPicPrintSetup').val();
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

    // 是否禁用
    jQuery("input[name='radioIs_Banned']").each(function () {

        var tmpIsBanned = jQuery('#txtIs_Banned').val() == "True" ? 1 : 0;
        if (tmpIsBanned == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });


    // 接口类型
    jQuery("#selInterfaceType").val(jQuery('#txtInterfaceType').val());
    // 影像类型
    jQuery("#selImageType").val(jQuery('#txtImageType').val());

}


// ================================ 科室管理 ==== end ==================================================



// ================================ 用户 ==== start ==================================================



/// <summary>
/// 弹出用户设置页面
/// </summary>
function OpenEditUserWindow() {
    var UserID = jQuery("input[name='UserRadio']:checked").val();
    if (UserID == undefined) {
        ShowSystemDialog("请选择你要修改的用户！");
        return;
    }
    OpenUserOperWindowParams(UserID);
}

/// <summary>
/// 弹出新增用户页面
/// </summary>
function OpenUserOperWindow() {
    OpenUserOperWindowParams("");
}

/// <summary>
/// 弹出用户设置页面(用户ID编号)
/// </summary>
function OpenUserOperWindowParams(UserID) {

    var url = '/System/Config/User/UserOper.aspx?num=' + Math.random();
    if (UserID != "") {
        url = url + "&UserID=" + UserID;
    }
    art.dialog.open(url,
    {
        width: 420,
        height: 350,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "用户设置"
    });
}


/// <summary>
/// 弹出用户密码重置页面
/// </summary>
function OpenUserPasswordResetWindow() {

    var UserID = jQuery("input[name='UserRadio']:checked").val();
    if (UserID == undefined) {
        ShowSystemDialog("请选择你要修改口令的用户！");
        return;
    }
    var UserName = jQuery("#rdiUser_" + UserID).attr("username");
    var DefaultPassword = "888888"; // 默认密码
    var tipscontent = ' 是否确定将用户【' + UserName + '】的密码重置为：【' + DefaultPassword + '】？ ';

    art.dialog({
        id: 'OperWindowFrame',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        title: '用户口令重置',
        button: [{
            name: '取消'
        }, {
            name: '确定',
            callback: function () {
                ResetUserPasswordInfo(UserID, DefaultPassword); // 重置用户的密码。 (重置密码) 
                return true;
            }, focus: true
        }]
    });

}


/// <summary>
/// 重置用户的密码。 (重置密码)
/// </summary>
function ResetUserPasswordInfo(ID_User, DefaultPassword) {

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { ID_User: ID_User, 
                DefaultPassword: DefaultPassword,
                action: 'ResetUserPasswordInfo',
                currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                var UserID = jQuery("input[name='UserRadio']:checked").val();
                var UserName = jQuery("#rdiUser_" + UserID).attr("username");

                ShowSystemDialog("重置用户【" + UserName + "】的密码成功!");

            }
            else { ShowSystemDialog("重置用户密码失败，请与技术人员联系!") }
        }
    });


}


/// <summary>
/// 弹出用户登录错误次数清除窗口
/// </summary>
function OpenUserLoginCountClearWindow() {

    var UserID = jQuery("input[name='UserRadio']:checked").val();
    if (UserID == undefined) {
        parent.ShowSystemDialog("请选择你要清除登录错误次数的用户！");
        return;
    }
    var UserName = jQuery("#rdiUser_" + UserID).attr("username");
    var tipscontent = ' 是否确定清除用户【' + UserName + '】登录错误次数？ ';

    art.dialog({
        id: 'OperWindowFrame',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        title: '清除登录错误次数',
        button: [{
            name: '取消'
        }, {
            name: '确定',
            callback: function () {
                ClearUserLoginCountInfo(UserID); // 清除登录错误次数。  
                return true;
            }, focus: true
        }]
    });

}


/// <summary>
/// 清除登录错误次数
/// </summary>
function ClearUserLoginCountInfo(ID_User) {

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { ID_User: ID_User, 
                action: 'ClearUserLoginCountInfo',
                currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                var UserID = jQuery("input[name='UserRadio']:checked").val();
                var UserName = jQuery("#rdiUser_" + UserID).attr("username");

                ShowSystemDialog("清除用户【" + UserName + "】登录错误次数成功!");

            }
            else { ShowSystemDialog("清除登录错误次数失败，请与技术人员联系!") }
        }
    });


}


/// <summary> 
/// 点击选中对应用户的单选按钮
/// </summary>
function SetQueryListUserChecked(ID_User) {
    jQuery("#rdiUser_" + ID_User).attr("checked", true);
}



/// <summary>
/// 保存用户参数。 (保存)
/// </summary>
function SaveUserInfo() {

    // 获取保存时提交的参数。 (参数提取)
    var UserParams = GetSaveUserParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作


    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: UserParams,         // 用户参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {

            if (parseInt(jsonmsg) > 0) {
                if (jQuery("#ID_User").val() != "") {
                    parent.GetSingleSectionUserInfo(jQuery("#ID_User").val(), "edit");
                }
                else {
                    parent.GetSingleSectionUserInfo(jsonmsg, "add");
                }

                parent.ShowSystemDialogCloseDialog("保存用户成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据
                    RemoveSelectedSection();
                }

            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialog("用户名称【" + jQuery("#txtUserName").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialog("保存用户失败，请与技术人员联系!"); }
        }
    });

}



/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveUserParams() {

    var ID_User = jQuery("#ID_User").val();                             // 用户ID
    var UserName = jQuery("#txtUserName").val();                        // 用户名称
    UserName = encodeURIComponent(UserName);                            // 用户名称 编码处理
    var ID_Section = jQuery("#idSelectSection").val();                  // 科室ID
    var SectionName = jQuery("#nameSelectSection").val();               // 科室名称
    SectionName = encodeURIComponent(SectionName);                      // 科室名称 编码处理

    var OperateLevel = jQuery("#selOperateLevel").val();                // 操作密级
    var DisCountRate = jQuery("#selDisCountRate").val();                // 折扣率
    var VocationType = jQuery("#selVocationType").val();        // 分类
    var GenderName = jQuery("input[name='radioGenderName']:checked").val();   // 性别
    var Is_Del = jQuery("input[name='radioIs_Del']:checked").val();     // 状态

    var Note = jQuery("#txtNote").val();                                // 备注
    Note = encodeURIComponent(Note);                                    // 备注 编码处理

    var Signature = jQuery("#showSignatureImage").data("imagedata");       // 用户签名图片64位数据
    
    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (UserName == "") {
        jQuery('#txtUserName').focus();
        parent.ShowSystemDialog("请输入姓名!");
        return;
    }

    if (GenderName == undefined ) {
        parent.ShowSystemDialog("请选择性别!");
        return;
    }

    if (ID_Section == "") {
        parent.ShowSystemDialog("请选择科室!");
        jQuery('#txtSectionInputCode').focus();
        return;
    }

    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var UserInfoParams = {
        UserID: UserID,
        UserName: UserName,
        SectionID: SectionID,
        SectionName: SectionName,
        OperateLevel: OperateLevel,
        DisCountRate: DisCountRate,
        VocationType: VocationType,
        GenderName: GenderName,
        Signature: Signature,
        IsDel: IsDel,
        Note: Note,
        action: 'SaveUserInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return UserInfoParams; // 返回拼接后的参数

}


/// <summary>
/// 初始化收费项目设置页面
/// </summary>
function InitUserEditData() {
    if (jQuery("#ID_User").val() == "") {
        return;
    }
    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

    var selectedItemContent = "";
    if (jQuery("#idSelectSection").val() != "") {

        var UserSelectedSectionItemDataTempleteContent = jQuery('#UserSelectedSectionItemDataTemplete').html();             //用户已经选择的科室列表模版
        if (UserSelectedSectionItemDataTempleteContent == undefined) { return; }

        selectedItemContent = UserSelectedSectionItemDataTempleteContent
                            .replace(/@ID_Section/gi, jQuery("#idSelectSection").val())
                            .replace(/@SectionName/gi, jQuery("#nameSelectSection").val())
                            ;
        jQuery("#spanSelectSection").html(selectedItemContent);
        jQuery("#spanSelectSection").show();
        jQuery("#spanSection").hide();
    }


    // 性别
    jQuery("input[name='radioGenderName']").each(function () {
        var tmpvalue = jQuery('#txtGenderName').val() ;
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });

    // 状态
    jQuery("input[name='radioIs_Del']").each(function () {
        var tmpvalue = jQuery('#txtIs_Del').val();
        if (tmpvalue == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });

    // 操作密级
    jQuery("#selOperateLevel").val(jQuery('#txtOperateLevel').val());
    // 折扣率
    jQuery("#selDisCountRate").val(jQuery('#txtDisCountRate').val());
    // 分类
    jQuery("#selVocationType").val(jQuery('#txtVocationType').val());

}


// ================================ 用户 ==== end ==================================================


// ================================ 套餐 ==== start ================================================

/// <summary>
/// 弹出检查项目设置页面
/// </summary>
function OpenEditSetItemWindow() {
    var SetID = jQuery("input[name='SetRadio']:checked").val();
    if (SetID == undefined) {
        ShowSystemDialog("请选择你要修改的套餐！");
        return;
    }
    OpenSetItemOperWindowParams(SetID);
}

/// <summary>
/// 弹出新增检查项目页面
/// </summary>
function OpenSetItemOperWindow() {
    OpenSetItemOperWindowParams("");
}

/// <summary>
/// 弹出检查项目设置页面(检查项目ID编号)
/// </summary>
function OpenSetItemOperWindowParams(SetID) {

    var url = '/System/Config/Set/SetOper.aspx?num=' + Math.random();
    if (SetID != "") {
        url = url + "&SetID=" + SetID;
    }
    art.dialog.open(url,
    {
        width: 450,
        height: 300,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "套餐设置",
        init: function () {
            jQuery(".aui_close").hide();
        },
        close: function () {

        }
    });
}

/// <summary>
/// 根据套餐ID，查找收费项目明细 (获取)
/// </summary>
function GetFeeListBySet(SetID) {

    jQuery("#rdi_Set_" + SetID).attr("checked", true);

    jQuery("#SetItemName").html(jQuery("#rdi_Set_" + SetID).attr("setname"));

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { action: 'GetFeeDetailListBySet',
            SetID: SetID,
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的收费项目信息
                ShowFeeListBySet(jsonmsg);
            }
        }
    });
}

/// <summary>
/// 根据收费项目ID，查找收费项目明细 (显示)
/// </summary>
function ShowFeeListBySet(feelist) {

    var tempListContent = "";  // 临时内容

    // 检查项目显示模版
    var FeeListItemTempleteContent = jQuery('#FeeListItemTemplete').html();
    if (FeeListItemTempleteContent == undefined) { return; }

    jQuery("#FeeExamList").html(""); //   先清空列表

    var txtBreakfastOrder = ""; // 早餐顺序
    var Forsex01 = ""; // 男性是否选中
    var Forsex00 = ""; // 女性是否选中
    var FeeItemCount = 0;

    var tempFirstItemID = 0;  // 记录第一个数据的ID
    // dataList0 显示所有的角色信息
    jQuery(feelist.dataList0).each(function (j, feeitem) {
        FeeItemCount++;
        if (feeitem.Forsex == '2') {
            Forsex01 = "√";
            Forsex00 = "√";
        } else if (feeitem.Forsex == '1') {
            Forsex01 = "√";
            Forsex00 = "-";
        } else {
            Forsex01 = "-";
            Forsex00 = "√";
        }

        switch (feeitem.BreakfastOrder) {
            case "1": txtBreakfastOrder = "<span class='red'>A</span>"; break; // 餐前
            case "2": txtBreakfastOrder = "<span class='blue'>B</span>"; break; // 早餐
            case "3": txtBreakfastOrder = "C"; break; // 餐前后均可
            case "4": txtBreakfastOrder = "D"; break; // 餐后
        }


        tempListContent += FeeListItemTempleteContent
                .replace(/@FeeName/gi, feeitem.FeeName)
                .replace(/@SectionName/gi, feeitem.SectionName)
                .replace(/@ID_Section/gi, feeitem.ID_Section)
                .replace(/@ID_Fee/gi, feeitem.ID_Fee)
                .replace(/@Price/gi, feeitem.Price)
                .replace(/@Forsex00/gi, Forsex00)
                .replace(/@Forsex01/gi, Forsex01)
                .replace(/@SpecimenName/gi, feeitem.SpecimenName)
                .replace(/@InputCode/gi, feeitem.InputCode)
                .replace(/@BreakfastOrder/gi, txtBreakfastOrder)
                .replace(/@Is_Banned/gi, feeitem.Is_Banned == 'True' ? '√' : '')
                ;


        // 记录第一个检查项目的ID
        if (tempFirstItemID == 0) {
            tempFirstItemID = feeitem.ID_Fee;
        }

    });
    jQuery("#FeeItemCount").html(" 【共有 " + FeeItemCount + " 个收费项目】");

    jQuery("#FeeExamList").html(tempListContent);  // 显示每列的数据

}


/// <summary>
/// 根据设置收费项目列选中状态 (获取)
/// </summary>
function SetFeeRadioSelect(ID_Fee) {

    jQuery("#rdi_Fee_" + ID_Fee).attr("checked", true);

}


/// <summary>
/// 弹出套餐收费项目设置页面
/// </summary>
function OpenSetFeeRelOperWindow() {

    var SetID = jQuery("input[name='SetRadio']:checked").val();
    if (SetID == undefined) {
        ShowSystemDialog("请选择需要设置收费项目的套餐！");
        return;
    }
    var setname = jQuery("#rdi_Set_" + SetID).attr("setname");
    setname = encodeURIComponent(setname);

    var tmpUrl = "/System/Config/Set/SetDetail.aspx?SetID=" + SetID + "&setname=" + setname + "&num=" + Math.random();
    art.dialog.open(tmpUrl,
    {
        width: 600,
        height: 390,
        lock: true,
        drag: false,
        id: 'OperWindowFrame',
        title: "套餐明细设置"
    });
}


/// <summary>
/// 初始化套餐设置页面
/// </summary>
function InitSetEditData() {
    if (jQuery("#PEPackageID").val() == "") {
        return;
    }
    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

    jQuery("input[name='radioForsex']").each(function () {
        if (jQuery('#txtForsex').val() == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });

    jQuery("input[name='radioIs_Banned']").each(function () {

        var tmpIsBanned = jQuery('#txtIs_Banned').val() == "True" ? 1 : 0;
        if (tmpIsBanned == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });


}


/// <summary>
/// 保存套餐参数。 (保存)
/// </summary>
function SaveBusSetInfo() {

    // 获取保存时提交的参数。 (参数提取)
    var BusSetParams = GetSaveBusSetParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: BusSetParams,         // 收费项目参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {
                
                if (jQuery("#PEPackageID").val() != "") {
                     parent.GetSingleBusSet(jQuery("#PEPackageID").val(), "edit");
                }
                else {
                     parent.GetSingleBusSet(jsonmsg, "add");
                }
                parent.ShowSystemDialogCloseDialog("保存套餐成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据
                }
            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialog("套餐名称：【" + jQuery("#txtPEPackageName").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialog("保存套餐失败，请与技术人员联系!") }
        }
    });

}

/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveBusSetParams() {

    var PEPackageID = jQuery("#PEPackageID").val();       // 收费项目ID
    var PEPackageName = jQuery("#txtPEPackageName").val();              // 套餐名称
    PEPackageName = encodeURIComponent(PEPackageName);                  // 套餐名称 编码处理
    var DispOrder = jQuery("#txtDispOrder").val();          // 排序编号
    DispOrder = encodeURIComponent(DispOrder);              // 排序编号 编码处理

    var Forsex = jQuery("input[name='radioForsex']:checked").val();               // 适用于
    var Is_Banned = jQuery("input[name='radioIs_Banned']:checked").val();   // 是否被禁用
    var BanDescribe = jQuery("#txtBanDescribe").val();              // 禁用说明
    BanDescribe = encodeURIComponent(BanDescribe);                  // 禁用说明 编码处理
    var Note = jQuery("#txtNote").val();                            // 套餐说明
    Note = encodeURIComponent(Note);                                // 套餐说明 编码处理


    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (PEPackageName == "") {
        jQuery('#txtPEPackageName').focus();
        parent.ShowSystemDialog("请输入套餐名称!");
        return;
    }


    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var BusSetParams = {
        PEPackageName: PEPackageName,
        PEPackageID: PEPackageID,
        Forsex: Forsex,
        DispOrder: DispOrder,
        Is_Banned: Is_Banned,
        BanDescribe: BanDescribe,
        Note: Note,
        action: 'SaveBusSetInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return BusSetParams; // 返回拼接后的参数

}


/// <summary>
/// 根据套餐ID，查找收费项目明细 (在明细设置页面中使用)
/// </summary>
function GetFeeListBySet_Ex01(SetID) {

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { action: 'GetFeeDetailListBySet',
            SetID: SetID,
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的权限信息
                ShowFeeListBySet_Ex01(jsonmsg);
            }
        }
    });
}

/// <summary>
/// 根据套餐ID，查找收费项目明细 (在明细设置页面中使用，显示)
/// </summary>
function ShowFeeListBySet_Ex01(feelist) {

    var tempListContent = "";    // 临时内容
    var OrgFeeIDStrs = "";  // 已经选择的收费项目ID
    // 收费项目显示模版
    var UserSelectedFeeItemDataTempleteContent = jQuery('#UserSelectedFeeItemDataTemplete').html();
    if (UserSelectedFeeItemDataTempleteContent == undefined) { return; }

    jQuery("#tmpSelectedFeeList").html(""); //   先清空列表
    var nCount = 0;
    // dataList0 显示所有的已选择的收费项目信息
    jQuery(feelist.dataList0).each(function (j, Fee) {
        // 将已经选择的收费项目ID拼接后，保存下来。
        if (nCount == 0) {
            OrgFeeIDStrs = Fee.ID_Fee;
        } else {
            OrgFeeIDStrs = OrgFeeIDStrs + "," + Fee.ID_Fee;
        }

        nCount++;

        tempListContent += UserSelectedFeeItemDataTempleteContent
                            .replace(/@ID_Fee/gi, Fee.ID_Fee)
                            .replace(/@FeeName/gi, Fee.FeeName)
                            .replace(/@chkSelectedFeeList/gi, "chkSelectedFeeList")
                            ;
    });
    jQuery("#tmpSelectedFeeList").append(tempListContent);
    jQuery("#OrgFeeIDStrs").val(OrgFeeIDStrs);
}


// ================================ 套餐 ==== end ================================================


// ================================ 套餐明细管理 ==== start ==================================================
/// <summary>
/// 保存套餐明细信息
/// </summary>
function SaveSetFeeRel() {
    
    var PEPackageID = jQuery("#PEPackageID").val();                        // 套餐ID
    var OrgFeeIDStrs = jQuery("#OrgFeeIDStrs").val();            // 原收费项目ID字符串

    var newFeeIDStrs = ""; // 当前选择的收费项目ID字符串
    var nCount = 0;
    jQuery("input[name='chkSelectedFeeList']:checkbox").each(function () {
        if (nCount == 0) { newFeeIDStrs = jQuery(this).val(); }
        else {
            newFeeIDStrs = newFeeIDStrs + "," + jQuery(this).val();
        }
        nCount++;

    });


    if ((OrgFeeIDStrs == "" && newFeeIDStrs == "") || OrgFeeIDStrs == newFeeIDStrs) {

        parent.ShowSystemDialogCloseDialog("保存套餐明细成功!", jQuery("#IS_AutoCloseDialog").val());

    } else {

        jQuery.ajax({
            type: "POST",
            url: "/Ajax/AjaxConfig.aspx",
            data: { action: 'SaveSetFeeRel',
                newFeeIDStrs: newFeeIDStrs,
                PEPackageID: PEPackageID,
                currenttime: encodeURIComponent(new Date())
            },
            cache: false,
            dataType: "json",
            success: function (jsonmsg) {
                if (jsonmsg != null && jsonmsg != "") {
                    // 判断操作结果
                    if (parseInt(jsonmsg) > 0) {

                        parent.GetFeeListBySet(jQuery("#PEPackageID").val()); // 刷新父页面的该套餐明细

                        parent.ShowSystemDialogCloseDialog("保存套餐明细成功!", jQuery("#IS_AutoCloseDialog").val());

                    }
                    else { parent.ShowSystemDialog("保存套餐明细失败，请与技术人员联系!") }
                }
            }
        });

    }

}

// ================================ 套餐明细管理 ==== end ==================================================



// ================================ 样本管理 ==== start ==================================================

/// <summary> 
/// 点击选中对应样本的单选按钮
/// </summary>
function SetSpecimenChecked(ID_Specimen) {
    jQuery("#rdiSpecimen_" + ID_Specimen).attr("checked", true);
}

/// <summary>
/// 弹出样本设置页面
/// </summary>
function OpenEditSpecimenWindow() {
    
    var SpecimenID = jQuery("input[name='SpecimenRadio']:checked").val();
    if (SpecimenID == undefined) {
        ShowSystemDialog("请选择你要修改的样本！");
        return;
    }
    OpenSpecimenOperWindowParams(SpecimenID);
}

/// <summary>
/// 弹出新增样本页面
/// </summary>
function OpenSpecimenOperWindow() {
    OpenSpecimenOperWindowParams("");
}

/// <summary>
/// 弹出样本设置页面(样本ID编号)
/// </summary>
function OpenSpecimenOperWindowParams(SpecimenID) {
    
    var url = '/System/Config/Specimen/SpecimenOper.aspx?num=' + Math.random();
    if (SpecimenID != "") {
        url = url + "&SpecimenID=" + SpecimenID;
    }
    art.dialog.open(url,
    {
        width: 450,
        height: 200,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "样本维护"
    });
}


/// <summary>
/// 保存样本参数。 (保存)
/// </summary>
function SaveSpecimenInfo() {

    // 获取保存时提交的参数。 (参数提取)
    var SpecimenParams = GetSaveSpecimenParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: SpecimenParams,         // 样本参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                if (jQuery("#ID_Specimen").val() != "") {
                    parent.GetSingleSpecimenInfo(jQuery("#ID_Specimen").val(), "edit");
                }
                else {
                    parent.GetSingleSpecimenInfo(jsonmsg, "add");
                }


                parent.ShowSystemDialogCloseDialog("保存样本成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据

                }

            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialogCloseDialog("样本名称：【" + jQuery("#txtSpecimenName").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialogCloseDialog("保存样本失败，请与技术人员联系!") }
        }
    });

}

/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveSpecimenParams() {

    var ID_Specimen = jQuery("#ID_Specimen").val();                     // 样本ID
    var SpecimenName = jQuery("#txtSpecimenName").val();                // 样本名称
    SpecimenName = encodeURIComponent(SpecimenName);                    // 样本名称 编码处理

    var DispOrder = jQuery("#txtDispOrder").val();                      // 排序编号

    var LisSpecimenName = jQuery("#txtLisSpecimenName").val();          // Lis样本名称
    LisSpecimenName = encodeURIComponent(LisSpecimenName);              // Lis样本名称 编码处理

    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (SpecimenName == "") {
        jQuery('#txtSpecimenName').focus();
        parent.ShowSystemDialogCloseDialog("请输入样本名称!");
        return;
    }


    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var SpecimenSaveParams = {
        SpecimenName: SpecimenName,
        ID_Specimen: ID_Specimen,
        LisSpecimenName: LisSpecimenName,
        DispOrder: DispOrder,
        action: 'SaveSpecimenInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return SpecimenSaveParams; // 返回拼接后的参数

}


/// <summary>
/// 初始化样本设置页面
/// </summary>
function InitSpecimenEditData() {
    if (jQuery("#ID_Specimen").val() == "") {
        return;
    }

    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

}



// ================================ 样本管理 ==== end ==================================================



// ================================ 体检类型管理 ==== start ==================================================

/// <summary> 
/// 点击选中对应体检类型的单选按钮
/// </summary>
function SetExamTypeChecked(ID_ExamType) {
    jQuery("#rdiExamType_" + ID_ExamType).attr("checked", true);
}

/// <summary>
/// 弹出体检类型设置页面
/// </summary>
function OpenEditExamTypeWindow() {

    var ExamTypeID = jQuery("input[name='ExamTypeRadio']:checked").val();
    if (ExamTypeID == undefined) {
        ShowSystemDialog("请选择你要修改的体检类型！");
        return;
    }
    OpenExamTypeOperWindowParams(ExamTypeID);
}

/// <summary>
/// 弹出新增体检类型页面
/// </summary>
function OpenExamTypeOperWindow() {
    OpenExamTypeOperWindowParams("");
}

/// <summary>
/// 弹出体检类型设置页面(体检类型ID编号)
/// </summary>
function OpenExamTypeOperWindowParams(ExamTypeID) {

    var url = '/System/Config/ExamType/ExamTypeOper.aspx?num=' + Math.random();
    if (ExamTypeID != "") {
        url = url + "&ExamTypeID=" + ExamTypeID;
    }
    art.dialog.open(url,
    {
        width: 450,
        height: 200,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "体检类型维护"
    });
}


/// <summary>
/// 保存体检类型参数。 (保存)
/// </summary>
function SaveExamTypeInfo() {

    // 获取保存时提交的参数。 (参数提取)
    var ExamTypeParams = GetSaveExamTypeParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: ExamTypeParams,         // 体检类型参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                if (jQuery("#ID_ExamType").val() != "") {
                    parent.GetSingleExamTypeInfo(jQuery("#ID_ExamType").val(), "edit");
                }
                else {
                    parent.GetSingleExamTypeInfo(jsonmsg, "add");
                }


                parent.ShowSystemDialogCloseDialog("保存体检类型成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据

                }

            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialogCloseDialog("体检类型名称：【" + jQuery("#txtExamTypeName").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialogCloseDialog("保存体检类型失败，请与技术人员联系!") }
        }
    });

}

/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveExamTypeParams() {

    var ID_ExamType = jQuery("#ID_ExamType").val();                     // 体检类型ID
    var ExamTypeName = jQuery("#txtExamTypeName").val();                // 体检类型名称
    ExamTypeName = encodeURIComponent(ExamTypeName);                    // 体检类型名称 编码处理

    var Is_Document = jQuery("input[name='radioIs_Document']:checked").val();                  // 排序编号

    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (ExamTypeName == "") {
        jQuery('#txtExamTypeName').focus();
        parent.ShowSystemDialogCloseDialog("请输入体检类型名称!");
        return;
    }


    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var ExamTypeSaveParams = {
        ExamTypeName: ExamTypeName,
        ID_ExamType: ID_ExamType,
        Is_Document: Is_Document,
        action: 'SaveExamTypeInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return ExamTypeSaveParams; // 返回拼接后的参数

}


/// <summary>
/// 初始化体检类型设置页面
/// </summary>
function InitExamTypeEditData() {
    if (jQuery("#ID_ExamType").val() == "") {
        return;
    }

    jQuery("input[name='radioIs_Document']").each(function () {
        var tmpIsDocument = jQuery('#txtIs_Document').val() == "1" ? 1 : 0;
        if (tmpIsDocument == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
        }
    });


    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

}



// ================================ 体检类型管理 ==== end ==================================================




// ================================ 结论词分类管理 ==== start ==================================================

/// <summary> 
/// 点击选中对应结论词分类的单选按钮
/// </summary>
function SetConclusionTypeChecked(ID_ConclusionType) {
    jQuery("#rdiConclusionType_" + ID_ConclusionType).attr("checked", true);
}

/// <summary>
/// 弹出结论词分类设置页面
/// </summary>
function OpenEditConclusionTypeWindow() {

    var ConclusionTypeID = jQuery("input[name='ConclusionTypeRadio']:checked").val();
    if (ConclusionTypeID == undefined) {
        ShowSystemDialog("请选择你要修改的结论词分类！");
        return;
    }
    OpenConclusionTypeOperWindowParams(ConclusionTypeID);
}

/// <summary>
/// 弹出新增结论词分类页面
/// </summary>
function OpenConclusionTypeOperWindow() {
    OpenConclusionTypeOperWindowParams("");
}

/// <summary>
/// 弹出结论词分类设置页面(结论词分类ID编号)
/// </summary>
function OpenConclusionTypeOperWindowParams(ConclusionTypeID) {

    var url = '/System/Config/Conclusion/ConclusionTypeOper.aspx?num=' + Math.random();
    if (ConclusionTypeID != "") {
        url = url + "&ConclusionTypeID=" + ConclusionTypeID;
    }
    art.dialog.open(url,
    {
        width: 450,
        height: 200,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "结论词分类维护"
    });
}


/// <summary>
/// 保存结论词分类参数。 (保存)
/// </summary>
function SaveConclusionTypeInfo() {

    // 获取保存时提交的参数。 (参数提取)
    var ConclusionTypeParams = GetSaveConclusionTypeParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: ConclusionTypeParams,         // 结论词分类参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                if (jQuery("#ID_ConclusionType").val() != "") {
                    parent.GetSingleConclusionTypeInfo(jQuery("#ID_ConclusionType").val(), "edit");
                }
                else {
                    parent.GetSingleConclusionTypeInfo(jsonmsg, "add");
                }


                parent.ShowSystemDialogCloseDialog("保存结论词分类成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据

                }

            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialogCloseDialog("结论词分类名称：【" + jQuery("#txtConclusionTypeName").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialogCloseDialog("保存结论词分类失败，请与技术人员联系!") }
        }
    });

}

/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveConclusionTypeParams() {
    
    var ID_ConclusionType = jQuery("#ID_ConclusionType").val();                     // 结论词分类ID
    var ConclusionTypeName = jQuery("#txtConclusionTypeName").val();                // 结论词分类名称
    ConclusionTypeName = encodeURIComponent(ConclusionTypeName);                    // 结论词分类名称 编码处理

    var Is_Banned = jQuery("input[name='radioIs_Banned']:checked").val();   // 是否被禁用
    var BanDescribe = jQuery("#txtBanDescribe").val();              // 禁用说明
    BanDescribe = encodeURIComponent(BanDescribe);                  // 禁用说明 编码处理

    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (ConclusionTypeName == "") {
        jQuery('#txtConclusionTypeName').focus();
        parent.ShowSystemDialogCloseDialog("请输入结论词分类名称!");
        return;
    }

    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var ConclusionTypeSaveParams = {
        ConclusionTypeName: ConclusionTypeName,
        ID_ConclusionType: ID_ConclusionType,
        Is_Banned: Is_Banned,
        BanDescribe: BanDescribe,
        action: 'SaveConclusionTypeInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return ConclusionTypeSaveParams; // 返回拼接后的参数

}


/// <summary>
/// 初始化结论词分类设置页面
/// </summary>
function InitConclusionTypeEditData() {
    if (jQuery("#ID_ConclusionType").val() == "") {
        return;
    }

    jQuery("input[name='radioIs_Banned']").each(function () {

        var tmpIsBanned = jQuery('#txtIs_Banned').val() == "True" ? 1 : 0;
        if (tmpIsBanned == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

}



// ================================ 结论词分类管理 ==== end ==================================================



// ================================ 结论类型管理 ==== start ==================================================

/// <summary> 
/// 点击选中对应结论类型的单选按钮
/// </summary>
function SetFinalConclusionTypeChecked(ID_FinalConclusionType) {
    jQuery("#rdiFinalConclusionType_" + ID_FinalConclusionType).attr("checked", true);
}

/// <summary>
/// 弹出结论类型设置页面
/// </summary>
function OpenEditFinalConclusionTypeWindow() {

    var FinalConclusionTypeID = jQuery("input[name='FinalConclusionTypeRadio']:checked").val();
    if (FinalConclusionTypeID == undefined) {
        ShowSystemDialog("请选择你要修改的结论类型！");
        return;
    }
    OpenFinalConclusionTypeOperWindowParams(FinalConclusionTypeID);
}

/// <summary>
/// 弹出新增结论类型页面
/// </summary>
function OpenFinalConclusionTypeOperWindow() {
    OpenFinalConclusionTypeOperWindowParams("");
}

/// <summary>
/// 弹出结论类型设置页面(结论类型ID编号)
/// </summary>
function OpenFinalConclusionTypeOperWindowParams(FinalConclusionTypeID) {

    var url = '/System/Config/Conclusion/FinalConclusionTypeOper.aspx?num=' + Math.random();
    if (FinalConclusionTypeID != "") {
        url = url + "&FinalConclusionTypeID=" + FinalConclusionTypeID;
    }
    art.dialog.open(url,
    {
        width: 450,
        height: 280,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "结论类型维护"
    });
}


/// <summary>
/// 保存结论类型参数。 (保存)
/// </summary>
function SaveFinalConclusionTypeInfo() {

    // 获取保存时提交的参数。 (参数提取)
    var FinalConclusionTypeParams = GetSaveFinalConclusionTypeParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: FinalConclusionTypeParams,         // 结论类型参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                if (jQuery("#ID_FinalConclusionType").val() != "") {
                    parent.GetSingleFinalConclusionTypeInfo(jQuery("#ID_FinalConclusionType").val(), "edit");
                }
                else {
                    parent.GetSingleFinalConclusionTypeInfo(jsonmsg, "add");
                }


                parent.ShowSystemDialogCloseDialog("保存结论类型成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据

                }

            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialogCloseDialog("结论类型名称：【" + jQuery("#txtFinalConclusionTypeName").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialogCloseDialog("保存结论类型失败，请与技术人员联系!") }
        }
    });

}

/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveFinalConclusionTypeParams() {
    
    var ID_FinalConclusionType = jQuery("#ID_FinalConclusionType").val();                     // 结论类型ID
    var FinalConclusionTypeName = jQuery("#txtFinalConclusionTypeName").val();                // 结论类型名称
    FinalConclusionTypeName = encodeURIComponent(FinalConclusionTypeName);                    // 结论类型名称 编码处理

    var FinalConclusionSignCode = jQuery("#txtFinalConclusionSignCode").val();                // 汇总标志代码
    FinalConclusionSignCode = encodeURIComponent(FinalConclusionSignCode);                    // 汇总标志代码 编码处理
    var DispOrder = jQuery("#txtDispOrder").val();                  // 排序编号

    var Is_Banned = jQuery("input[name='radioIs_Banned']:checked").val();   // 是否被禁用
    var BanDescribe = jQuery("#txtBanDescribe").val();              // 禁用说明
    BanDescribe = encodeURIComponent(BanDescribe);                  // 禁用说明 编码处理
    var Note = jQuery("#txtNote").val();              // 说明
    Note = encodeURIComponent(Note);                  // 说明 编码处理

    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (FinalConclusionTypeName == "") {
        jQuery('#txtFinalConclusionTypeName').focus();
        parent.ShowSystemDialogCloseDialog("请输入结论类型名称!");
        return;
    }

    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var FinalConclusionTypeSaveParams = {
        FinalConclusionTypeName: FinalConclusionTypeName,
        ID_FinalConclusionType: ID_FinalConclusionType,
        FinalConclusionSignCode: FinalConclusionSignCode,
        Is_Banned: Is_Banned,
        BanDescribe: BanDescribe,
        DispOrder: DispOrder,
        Note: Note,
        action: 'SaveFinalConclusionTypeInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return FinalConclusionTypeSaveParams; // 返回拼接后的参数

}


/// <summary>
/// 初始化结论类型设置页面
/// </summary>
function InitFinalConclusionTypeEditData() {
    if (jQuery("#ID_FinalConclusionType").val() == "") {
        return;
    }

    jQuery("input[name='radioIs_Banned']").each(function () {

        var tmpIsBanned = jQuery('#txtIs_Banned').val() == "True" ? 1 : 0;
        if (tmpIsBanned == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

}



// ================================ 结论类型管理 ==== end ==================================================



// ================================ ICD管理 ==== start ==================================================

/// <summary> 
/// 点击选中对应ICD的单选按钮
/// </summary>
function SetICDChecked(ID_ICD) {
    jQuery("#rdiICD_" + ID_ICD).attr("checked", true);
}

/// <summary>
/// 弹出ICD设置页面
/// </summary>
function OpenEditICDWindow() {

    var ICDID = jQuery("input[name='ICDRadio']:checked").val();
    if (ICDID == undefined) {
        ShowSystemDialog("请选择你要修改的ICD！");
        return;
    }
    OpenICDOperWindowParams(ICDID);
}

/// <summary>
/// 弹出新增ICD页面
/// </summary>
function OpenICDOperWindow() {
    OpenICDOperWindowParams("");
}

/// <summary>
/// 弹出ICD设置页面(ICDID编号)
/// </summary>
function OpenICDOperWindowParams(ICDID) {

    var url = '/System/Config/ICDTen/ICDTenOper.aspx?num=' + Math.random();
    if (ICDID != "") {
        url = url + "&ICDID=" + ICDID;
    }
    art.dialog.open(url,
    {
        width: 450,
        height: 500,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "ICD维护"
    });
}


/// <summary>
/// 保存ICD参数。 (保存)
/// </summary>
function SaveICDInfo() {

    // 获取保存时提交的参数。 (参数提取)
    var ICDParams = GetSaveICDParams();

    var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

    if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: ICDParams,         // ICD参数
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (parseInt(jsonmsg) > 0) {

                if (jQuery("#ID_ICD").val() != "") {
                    parent.GetSingleICDInfo(jQuery("#ID_ICD").val(), "edit");
                }
                else {
                    parent.GetSingleICDInfo(jsonmsg, "add");
                }


                parent.ShowSystemDialogCloseDialog("保存ICD成功!", jQuery("#IS_AutoCloseDialog").val());

                // 如果是自动关闭弹出窗口
                if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                    btnResetClick(); // 否则清空数据

                }

            }
            else if (parseInt(jsonmsg) == -1) {
                parent.ShowSystemDialogCloseDialog("ICD名称：【" + jQuery("#txtICDName").val() + "】已经存在，请核对后再操作！");
            }
            else { parent.ShowSystemDialogCloseDialog("保存ICD失败，请与技术人员联系!") }
        }
    });

}

/// <summary>
/// 获取保存时提交的参数。 (参数提取)
/// </summary>
function GetSaveICDParams() {

    var ID_ICD = jQuery("#ID_ICD").val();                     // ICDID
    var ICDCNName = jQuery("#txtICDCNName").val();                // ICD中文名称
    ICDCNName = encodeURIComponent(ICDCNName);                    // ICD中文名称 编码处理
    var ICDENName = jQuery("#txtICDENName").val();                // ICD英文名称
    ICDENName = encodeURIComponent(ICDENName);                    // ICD英文名称 编码处理

    var Code = jQuery("#txtCode").val();                // ICD Code
    Code = encodeURIComponent(Code);                    // ICD Code 编码处理

    var Codea = jQuery("#txtCodea").val();                // ICD Code a
    Codea = encodeURIComponent(Codea);                    // ICD Code a 编码处理
    var ICDtoSection = jQuery("#txtICDtoSection").val();                // ICD 隶属科室 
    ICDtoSection = encodeURIComponent(ICDtoSection);                    // ICD 隶属科室 编码处理

    var LevelA = jQuery("#txtLevelA").val();                // LevelA
    var LevelB = jQuery("#txtLevelB").val();                // LevelB
    var LevelC = jQuery("#txtLevelC").val();                // LevelC
    var LevelD = jQuery("#txtLevelD").val();                // LevelD
    var LevelE = jQuery("#txtLevelE").val();                // LevelE
    var LevelTree = jQuery("#txtLevelTree").val();          // LevelTree

    var Class = jQuery("#txtClass").val();                // ICD 类型
    Class = encodeURIComponent(Class);                    // ICD 类型 编码处理

    var Tag = jQuery("#txtTag").val();                // ICD 标签
    Tag = encodeURIComponent(Tag);                    // ICD 标签 编码处理

    var Note = jQuery("#txtNote").val();                // ICD 说明
    Note = encodeURIComponent(Note);                    // ICD 说明 编码处理

    var Is_Banned = jQuery("input[name='radioIs_Banned']:checked").val();   // 是否被禁用
    var BanDescribe = jQuery("#txtBanDescribe").val();              // 禁用说明
    BanDescribe = encodeURIComponent(BanDescribe);                  // 禁用说明 编码处理

    jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

    if (ICDCNName == "") {
        jQuery('#txtICDCNName').focus();
        parent.ShowSystemDialogCloseDialog("请输入ICD中文名称!");
        return;
    }

    if (ICDENName == "") {
        jQuery('#txtICDENName').focus();
        parent.ShowSystemDialogCloseDialog("请输入ICD英文名称!");
        return;
    }

    jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

    var ICDSaveParams = {
        ID_ICD: ID_ICD,
        ICDCNName: ICDCNName,
        ICDENName: ICDENName,
        Code: Code,
        Codea: Codea,
        LevelA: LevelA,
        LevelB: LevelB,
        LevelC: LevelC,
        LevelD: LevelD,
        LevelE: LevelE,
        LevelTree: LevelTree,
        Class: Class,
        Tag: Tag,
        Note: Note,
        ICDtoSection: ICDtoSection,
        Is_Banned: Is_Banned,
        BanDescribe: BanDescribe,
        action: 'SaveICDInfo',
        currenttime: encodeURIComponent(new Date())
    };

    return ICDSaveParams; // 返回拼接后的参数

}


/// <summary>
/// 初始化ICD设置页面
/// </summary>
function InitICDEditData() {
    if (jQuery("#ID_ICD").val() == "") {
        return;
    }

    jQuery("input[name='radioIs_Banned']").each(function () {

        var tmpIsBanned = jQuery('#txtIs_Banned').val() == "True" ? 1 : 0;
        if (tmpIsBanned == jQuery(this).val()) {
            jQuery(this).attr("checked", true);
            jQuery(this).change();
        }
    });

    // 重置保存按钮的大小
    BtnSaveResizeToSmall(); 

}



// ================================ ICD管理 ==== end ==================================================




// ================================ ICD 10快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询ICD 10列表
/// </summary>
function ShowHideQuickQueryICD10Table(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQueryICD10Table").show();
        ShowHideQuickQuerySectionTable(false);
    } else {
        jQuery("#QuickQueryICD10Table").hide();
    }
}

var gAllICD10DataList = "";    // 保存全部的ICD 10列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldICD10InputCode = "****";              // 记录上次输入的输入码
var gAllICD10ListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询ICD 10（通过Ajax后台过滤）
/// </summary>
function QuickQueryICD10TableData_Ajax() {

    var InputCode = jQuery('#txtICD10InputCode').val();
    if (OldICD10InputCode != InputCode) {
        OldICD10InputCode = InputCode;
    } else {
        ShowHideQuickQueryICD10Table(true, InputCode);
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { InputCode: InputCode,
            action: 'GetQuickICD10List',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的ICD 10
            ShowQuickQueryICD10TableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示ICD 10（通过Ajax过滤）
/// </summary>
function ShowQuickQueryICD10TableData_Ajax(ICD10list) {
    if (ICD10list == "" || ICD10list.totalCount == 0) {

        ShowHideQuickQueryICD10Table(true, jQuery('#txtICD10InputCode').val());
        // 显示没有找到ICD 10信息
        jQuery("#QuickQueryICD10TableData").html(jQuery('#EmptyICD10QuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //ICD 10table内容

        var ICD10QuickQueryTableTempleteContent = jQuery('#ICD10QuickQueryTableTemplete').html();             //快速查询ICD 10列表模版
        if (ICD10QuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (ICD10list != "") {
            jQuery(ICD10list.dataList).each(function (j, ICD10item) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += ICD10QuickQueryTableTempleteContent
                            .replace(/@ID_ICD/gi, ICD10item.ID_ICD)
                            .replace(/@ICDCNName/gi, ICD10item.ICDCNName)
                            .replace(/@Code/gi, ICD10item.Code)
                            .replace(/@InputCode/gi, ICD10item.InputCode)
                            .replace(/@chkICD10QueryList/gi, "chkICD10QueryList")
                            ;
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQueryICD10Table(true, jQuery('#txtICD10InputCode').val());
            jQuery("#QuickQueryICD10TableData").html(conclusionContent);
        } else {
            ShowHideQuickQueryICD10Table(false);
            jQuery("#QuickQueryICD10TableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择ICD 10）
/// </summary>
function SelectICD10DataList() {
    var selectedItemContent = "";
    var UserSelectedICD10ItemDataTempleteContent = jQuery('#UserSelectedICD10ItemDataTemplete').html();             //用户已经选择的ICD 10列表模版
    if (UserSelectedICD10ItemDataTempleteContent == undefined) { return; }

    jQuery("input[name='chkICD10QueryList']:radio:checked").each(function () {
        if (jQuery("#chkUserICD10_" + jQuery(this).val()).val() == undefined) {
            selectedItemContent = UserSelectedICD10ItemDataTempleteContent
                            .replace(/@ID_ICD/gi, jQuery(this).val())
                            .replace(/@ICDCNName/gi, jQuery(this).attr("ICDCNName"))
                            ;
            jQuery("#spanSelectICD10").html(selectedItemContent);
            jQuery("#spanSelectICD10").show();
            jQuery("#idSelectICD10").val(jQuery(this).val());
            jQuery("#nameSelectICD10").val(jQuery(this).attr("ICDCNName"));
            jQuery("#spanICD10").hide();
        }
    });

    ShowHideQuickQueryICD10Table(false);
}

/// <summary>
/// 删除选择的ICD 10
/// </summary>
function RemoveSelectedICD10() {
    jQuery('#spanSelectICD10').hide();
    jQuery('#spanICD10').show();
    jQuery('#spanSelectICD10').html('');
    jQuery('#idSelectICD10').val('');
    jQuery('#nameSelectICD10').val('');
}


/// <summary> 
/// 点击选中对应结论词的单选按钮
/// </summary>
function SetICD10Checked(ID_ICD) {
    jQuery("#rdiICD10_" + ID_ICD).attr("checked", true);
}


// ================================ ICD 10快速选择函数区域 ==== end ==================================================



// ================================ 结论类型快速选择函数区域 ==== start ==================================================

/// <summary>
/// 隐藏，显示快速查询结论类型列表
/// </summary>
function ShowHideQuickQueryFinalConclusionTypeTable(flag, InputCode) {
    if (flag == true) {
        jQuery("#QuickQueryFinalConclusionTypeTable").show();
        ShowHideQuickQuerySectionTable(false);
    } else {
        jQuery("#QuickQueryFinalConclusionTypeTable").hide();
    }
}

var gAllFinalConclusionTypeDataList = "";    // 保存全部的结论类型列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
var OldFinalConclusionTypeInputCode = "****";              // 记录上次输入的输入码
var gAllFinalConclusionTypeListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
/// <summary>
/// 根据输入码查询结论类型（通过Ajax后台过滤）
/// </summary>
function QuickQueryFinalConclusionTypeTableData_Ajax() {

    var InputCode = jQuery('#txtFinalConclusionTypeInputCode').val();
    if (OldFinalConclusionTypeInputCode != InputCode) {
        OldFinalConclusionTypeInputCode = InputCode;
    } else {
        ShowHideQuickQueryFinalConclusionTypeTable(true, InputCode);
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { InputCode: InputCode,
            action: 'GetQuickFinalConclusionTypeList',
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            // 显示查询到的结论类型
            ShowQuickQueryFinalConclusionTypeTableData_Ajax(jsonmsg, InputCode);
        }
    });

}


/// <summary>
/// 根据查询结果数据，显示结论类型（通过Ajax过滤）
/// </summary>
function ShowQuickQueryFinalConclusionTypeTableData_Ajax(FinalConclusionTypelist) {
    if (FinalConclusionTypelist == "" || FinalConclusionTypelist.totalCount == 0) {

        ShowHideQuickQueryFinalConclusionTypeTable(true, jQuery('#txtFinalConclusionTypeInputCode').val());
        // 显示没有找到结论类型信息
        jQuery("#QuickQueryFinalConclusionTypeTableData").html(jQuery('#EmptyFinalConclusionTypeQuickQueryDataTemplete').html());
    }
    else {

        var conclusionContent = ""; //结论类型table内容

        var FinalConclusionTypeQuickQueryTableTempleteContent = jQuery('#FinalConclusionTypeQuickQueryTableTemplete').html();             //快速查询结论类型列表模版
        if (FinalConclusionTypeQuickQueryTableTempleteContent == undefined) { return; }
        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        if (FinalConclusionTypelist != "") {
            jQuery(FinalConclusionTypelist.dataList).each(function (j, FinalConclusionTypeitem) {
                // 如果字符串不包含这个输入码，则继续下一条数据的判断

                CurrQueryCount++;
                conclusionContent += FinalConclusionTypeQuickQueryTableTempleteContent
                            .replace(/@ID_FinalConclusionType/gi, FinalConclusionTypeitem.ID_FinalConclusionType)
                            .replace(/@FinalConclusionTypeName/gi, FinalConclusionTypeitem.FinalConclusionTypeName)
                            .replace(/@InputCode/gi, FinalConclusionTypeitem.InputCode)
                            .replace(/@chkFinalConclusionTypeQueryList/gi, "chkFinalConclusionTypeQueryList")
                            ;
            });
        }
        if (conclusionContent != "") {
            ShowHideQuickQueryFinalConclusionTypeTable(true, jQuery('#txtFinalConclusionTypeInputCode').val());
            jQuery("#QuickQueryFinalConclusionTypeTableData").html(conclusionContent);
        } else {
            ShowHideQuickQueryFinalConclusionTypeTable(false);
            jQuery("#QuickQueryFinalConclusionTypeTableData").html("");
        }
    }

}


/// <summary>
/// 点击确定按钮（确定选择结论类型）
/// </summary>
function SelectFinalConclusionTypeDataList() {
    var selectedItemContent = "";
    var UserSelectedFinalConclusionTypeItemDataTempleteContent = jQuery('#UserSelectedFinalConclusionTypeItemDataTemplete').html();             //用户已经选择的结论类型列表模版
    if (UserSelectedFinalConclusionTypeItemDataTempleteContent == undefined) { return; }

    jQuery("input[name='chkFinalConclusionTypeQueryList']:radio:checked").each(function () {
        if (jQuery("#chkUserFinalConclusionType_" + jQuery(this).val()).val() == undefined) {
            selectedItemContent = UserSelectedFinalConclusionTypeItemDataTempleteContent
                            .replace(/@ID_FinalConclusionType/gi, jQuery(this).val())
                            .replace(/@FinalConclusionTypeName/gi, jQuery(this).attr("FinalConclusionTypename"))
                            ;
            jQuery("#spanSelectFinalConclusionType").html(selectedItemContent);
            jQuery("#spanSelectFinalConclusionType").show();
            jQuery("#idSelectFinalConclusionType").val(jQuery(this).val());
            jQuery("#nameSelectFinalConclusionType").val(jQuery(this).attr("FinalConclusionTypename"));
            jQuery("#spanFinalConclusionType").hide();
        }
    });

    ShowHideQuickQueryFinalConclusionTypeTable(false);
}

/// <summary>
/// 删除选择的结论类型
/// </summary>
function RemoveSelectedFinalConclusionType() {
    jQuery('#spanSelectFinalConclusionType').hide();
    jQuery('#spanFinalConclusionType').show();
    jQuery('#spanSelectFinalConclusionType').html('');
    jQuery('#idSelectFinalConclusionType').val('');
    jQuery('#nameSelectFinalConclusionType').val('');
}


/// <summary> 
/// 点击选中对应结论词的单选按钮
/// </summary>
function SetConclusionChecked(ID_Conclusion) {
    jQuery("#rdiConclusion_" + ID_Conclusion).attr("checked", true);
    ShowHideQuickQueryFinalConclusionTypeTable(false, '');
}


// ================================ 结论类型快速选择函数区域 ==== end ==================================================



// ================================ 检查项目分组管理 ==== start ==================================================

/// <summary>
/// 根据检查项目分组ID，查找检查项目分组明细 (在明细设置页面中使用)
/// </summary>
function GetExamItemListByExamItemGroup_Ex01(ExamItemGroupID) {
   
    jQuery.ajax({
        type: "POST",
        url: "/Ajax/AjaxConfig.aspx",
        data: { action: 'GetExamDetailListByExamItemGroup',
            ExamItemGroupID: ExamItemGroupID,
            currenttime: encodeURIComponent(new Date())
        },
        cache: false,
        dataType: "json",
        success: function (jsonmsg) {
            if (jsonmsg != null && jsonmsg != "") {
                // 显示查询到的权限信息
                ShowExamItemListByExamItemGroup_Ex01(jsonmsg);
            }
        }
    });
}

/// <summary>
/// 根据检查项目分组ID，查找检查项目分组明细 (在明细设置页面中使用，显示)
/// </summary>
function ShowExamItemListByExamItemGroup_Ex01(examlist) {

    var tempListContent = "";    // 临时内容
    var OrgExamItemIDStrs = "";  // 已经选择的检查项目ID
    // 检查项目显示模版
    var UserSelectedExamItemItemDataTempleteContent = jQuery('#UserSelectedExamItemItemDataTemplete_CantDel').html();
    if (UserSelectedExamItemItemDataTempleteContent == undefined) { return; }

    jQuery("#tmpSelectedExamItemList").html(""); //   先清空列表
    var nCount = 0;
    // dataList0 显示所有的已选择的检查项目信息
    jQuery(examlist.dataList0).each(function (j, examitem) {
        // 将已经选择的检查项目ID拼接后，保存下来。
        if (nCount == 0) {
            OrgExamItemIDStrs = examitem.ID_ExamItem;
        } else {
            OrgExamItemIDStrs = OrgExamItemIDStrs + "," + examitem.ID_ExamItem;
        }

        nCount++;

        tempListContent += UserSelectedExamItemItemDataTempleteContent
                            .replace(/@ID_ExamItem/gi, examitem.ID_ExamItem)
                            .replace(/@ExamItemName/gi, examitem.ExamItemName)
                            .replace(/@chkSelectedExamItemList/gi, "chkSelectedExamItemList")
                            ;
    });
    jQuery("#tmpSelectedExamItemList").append(tempListContent);
    jQuery("#OrgExamItemIDStrs").val(OrgExamItemIDStrs);
}


/// <summary>
/// 保存检查项目分组明细信息（确认）
/// </summary>
function SaveExamItemGroupExamRelConfirm() {
    var tipscontent = "您正在保存检查项目分组明细，请确认是否继续！";
    parent.art.dialog({
        id: 'testID',
        content: tipscontent,
        lock: true,
        fixed: true,
        opacity: 0.3,
        button: [{
            name: '确定保存',
            title: '保存检查项目分组明细',
            callback: function () {
                SaveExamItemGroupExamRel(); // 保存检查项目分组明细信息
                return true;
            }, focus: true
        }, {
            name: '取消'
        }]
    });


}


/// <summary>
/// 保存检查项目分组明细信息
/// </summary>
function SaveExamItemGroupExamRel() {
    
    var ID_ExamItemGroup = jQuery("#ID_ExamItemGroup").val();                        // 检查项目分组ID
    var OrgExamItemIDStrs = jQuery("#OrgExamItemIDStrs").val();  // 原检查项目分组ID字符串

    var newExamItemIDStrs = ""; // 当前选择的检查项目ID字符串
    var nCount = 0;
    jQuery("input[name='chkSelectedExamItemList']:checkbox").each(function () {
        if (nCount == 0) { newExamItemIDStrs = jQuery(this).val(); }
        else {
            newExamItemIDStrs = newExamItemIDStrs + "," + jQuery(this).val();
        }
        nCount++;

    });


    if ((OrgExamItemIDStrs == "" && newExamItemIDStrs == "") || OrgExamItemIDStrs == newExamItemIDStrs) {

        parent.ShowSystemDialogCloseDialog("保存检查项目分组明细成功!", jQuery("#IS_AutoCloseDialog").val());

    } else {

        jQuery.ajax({
            type: "POST",
            url: "/Ajax/AjaxConfig.aspx",
            data: { action: 'SaveExamItemGroupExamRel',
                newExamItemIDStrs: newExamItemIDStrs,
                ID_ExamItemGroup: ID_ExamItemGroup,
                currenttime: encodeURIComponent(new Date())
            },
            cache: false,
            dataType: "json",
            success: function (jsonmsg) {
                if (jsonmsg != null && jsonmsg != "") {
                    // 判断操作结果
                    if (parseInt(jsonmsg) > 0) {

                        // parent.GetExamItemListByExamItemGroup(jQuery("#ID_ExamItemGroup").val()); // 刷新父页面的该检查项目分组明细

                        parent.ShowSystemDialogCloseDialog("保存检查项目分组明细成功!", jQuery("#IS_AutoCloseDialog").val());

                    }
                    else { parent.ShowSystemDialog("保存检查项目分组明细失败，请与技术人员联系!") }
                }
            }
        });

    }

}

// ================================ 检查项目分组管理 ==== end ==================================================


