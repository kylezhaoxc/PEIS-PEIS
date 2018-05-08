/************************该文件为总检公用脚本******************************/
/****************************************

1.	文件名称(File Name)： 	总检公用脚本文件
2.	功能描述(Description):  页面数据处理
3.	作者(Author)：			WTang
4.	日期(Create Date)：		2013.6.7

****************************************/

var gCustExamSectionItems = ""; // 用于保存客户分检的信息。
/// <summary>
/// 读取客户分科检查信息
/// </summary>
function GetCustExamSectionItem() {

    HideAllConclusionInfo();    // 隐藏总检的内容部分

    //    jQuery("#FinalConclusionDataHeadInfo").hide();    // 隐藏表头信息
    //    jQuery("#FinalConclusionDataBottomInfo").hide();  // 隐藏下面输入及操作信息

    //显示等待信息
    var ExamItemWaitingTempleteContent = jQuery('#ExamItemWaitingTemplete').html(); //读取数据等待信息模版
    if (ExamItemWaitingTempleteContent == undefined) { return; }
    jQuery('#TipsMessage').html(ExamItemWaitingTempleteContent);                   //显示等待信息

    var TipsMessageTempleteContent = jQuery('#TipsMessageTemplete').html();     //读取提示信息模版
    if (TipsMessageTempleteContent == undefined) { return; }
    var CustomerNameText = jQuery("#CustomerNameText").html();

    var Is_FinalCheck = jQuery("#Is_FinalCheck").val();         // 是否是总审
    var Is_FinalFinished = jQuery("#Is_FinalFinished").val();   // 完成总检

    var ExamState = jQuery('#ExamState').val();                         //体检状态,当次体检信息的状态：0表示在线，1表示归档，2表示在一号分库，3表示在二号分库…
    //体检状态,当次体检信息的状态：0表示在线，1表示归档，2表示在一号分库，3表示在二号分库…
    if (ExamState == "") {
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "请输入客户的体检号！"));   //显示空信息
        jQuery('#TipsArea').show();
        return;
    }

    if (ExamState != "0") {
        var showmsg = "该客户已归档，所以不能进行总检！";
        if (Is_FinalCheck == "True") {
            showmsg = "该客户已归档，所以不能进行审核！";
        }
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
    }


    if (Is_FinalCheck == "True" && Is_FinalFinished != "True") {
        var ID_FinalDoctor = jQuery("#ID_FinalDoctor").val();  // 总检医生ID 根据总检医生ID是否为空，判断是否是未通过的总检
        var showmsg = "该客户体检信息还未进行总检，所以不能进行审核！";
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
    }

    var Is_GuideSheetPrinted = jQuery("#Is_GuideSheetPrinted").val();         // 指引单是否打印
    var Is_Paused = jQuery("#Is_Paused").val();                               // 是否禁检
    var Is_FeeSettled = jQuery("#Is_FeeSettled").val();                       // 是否完成缴费

    if (Is_FeeSettled != "True") {
        var showmsg = "该客户还未完成缴费，不能进行总检！";
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
    }

    if (Is_Paused == "True" || Is_Paused == "1") {
        var showmsg = "该客户已经禁检，不能进行总检！";
        if (Is_FinalCheck == "True") {
            showmsg = "该客户已经禁检，不能进行总审！";
        }
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
    }

    if (Is_GuideSheetPrinted != "True") {
        var showmsg = "该客户指引单还未打印，所以不能进行总检！";

        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
    }

    var Is_GuideSheetReturned = jQuery("#Is_GuideSheetReturned").val();         // 指引单是否回收
    if (Is_GuideSheetReturned != "True") {
        var showmsg = "该客户指引单还未回收，所以不能进行总检！";
        if (Is_FinalFinished == "True") {
            showmsg = "该客户指引单还未回收，所以不能进行总审！";
        }
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, showmsg));   //显示空信息
        return;
    }

    var CustomerSecurityLevel = jQuery("#CustomerSecurityLevel").val(); // 客户  操作密级
    var OperateLevel = jQuery("#OperateLevel").val();                   // 操作员操作密级
    var Is_ReportReceipted = jQuery('#Is_ReportReceipted').val();               //报告已领取

    // 检查操作密级
    if (Is_ReportReceipted == "True" && parseInt(CustomerSecurityLevel) > parseInt(OperateLevel)) {
        jQuery("#divCustomerInfoArea").hide();  // 如果没有权限，则客户基本信息页不允许查看
        jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "对不起，你没有权限对该客户进行体检！"));   //显示提示信息
        return;
    }

    var Is_FinalConclusion = jQuery("#Is_FinalConclusion").val();           // 是否是总检
    if (Is_FinalConclusion == "True" && Is_FinalFinished == "True") {       // 如果已经总检，则不能再次进行修改，只有等审核不通过的情况下，才能再次进行总检的修改

        CtrlFinalFinishedButtonDisabled(0, false); // 禁用按钮

        jQuery("#txtConclusionInputCode").hide();
        jQuery("#trConclusionQueryArea1").hide();  // 隐藏结论及结论词区域
        jQuery("#trConclusionQueryArea2").hide();  // 隐藏结论及结论词区域


        art.dialog({
            id: 'artDialogID',
            lock: true,
            fixed: true,
            zIndex: 555,
            opacity: 0.3,
            content: '【提示】该客户已总检！<br/>【备注】1、当前只能进行浏览！<br/>&nbsp;&nbsp;&nbsp;&nbsp;2、解除总检/审核不通过时，才能再次进行修改！',
            button: [{
                name: '确定',
                callback: function() {
                    jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                }
}],
                close: function() {
                    jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                }
            });
        }

        if (CustomerNameText == null || CustomerNameText == "") {

            jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "您输入的体检号错误或不存在，请核对后再操作！"));   //显示空信息
            return;

        } else {

            var CustomerID = jQuery.trim(jQuery('#HiddenCustomerID').val());
            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxConclusion.aspx",
                data: { CustomerID: CustomerID,
                    action: 'GetCustExamSectionItem',
                    currenttime: encodeURIComponent(new Date())
                },
                cache: false,
                dataType: "json",
                success: function(jsonmsg) {
                    if (jsonmsg != null && jsonmsg != "") {
                        // 显示查询到的分科检查信息
                        gCustExamSectionItems = jsonmsg;
                        ShowCustExamSectionItem_Ajax(jsonmsg);
                    }
                }
            });
        }
    }

    /// <summary>
    /// 隐藏总检内容区的内容
    /// </summary>
    function HideAllConclusionInfo() {

        jQuery("#btnCustomerSimpleInfo").hide();  // 客户基本信息
        jQuery("#divCustomerBaseInfo").hide();    // 客户信息
        jQuery("#fkls").hide();    // Tab 切换
        jQuery("#fklscen").hide();    // 中间数据部分
        //jQuery("#ConclusionResultTitle").hide();    // 总检标题
        //jQuery("#ConclusionResultBox").hide();    // 总检信息

        jQuery('#TipsArea').show(); // 显示页面内部的提示信息 20140408 by wtang 

    }

    /// <summary>
    /// 显示总检内容区的内容
    /// </summary>
    function ShowAllConclusionInfo() {

        jQuery('#TipsArea').hide(); // 隐藏页面内部的提示信息 20140408 by wtang 

        jQuery("#btnCustomerSimpleInfo").show();  // 客户基本信息
        jQuery("#divCustomerBaseInfo").hide();    // 客户信息(刚加载时，内容部分默认为隐藏)
        jQuery("#fkls").show();    // Tab 切换
        jQuery("#fklscen").show();    // 中间数据部分
        //jQuery("#ConclusionResultTitle").show();    // 总检标题
        //jQuery("#ConclusionResultBox").hide();    // 总检信息(刚加载时，内容部分默认为隐藏)


    }


    /// <summary>
    /// 显示客户分科检查信息
    /// </summary>
    function ShowCustExamSectionItem_Ajax(examSectionItems) {

        //            var Is_GuideSheetPrinted = jQuery('#Is_GuideSheetPrinted').val();  //指引单是否打印
        //            var TipsMessageTempleteContent = jQuery('#TipsMessageTemplete').html();     //读取提示信息模版
        //            if (Is_GuideSheetPrinted == "False") {
        //                jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "该客户还未打印指引单，请先打印指引单后，再进行分科检查！"));   //显示空信息
        //                return;
        //            }
        //            

        var TipsMessageTempleteContent = jQuery('#TipsMessageTemplete').html();     //读取提示信息模版
        if (TipsMessageTempleteContent == undefined) { return; }
        if (examSectionItems == null || examSectionItems == "" || parseInt(examSectionItems.totalCount) <= 0) {

            jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "您输入的体检号错误或不存在，请核对后再操作！"));   //显示空信息
            return;

        }


        //    jQuery("#FinalConclusionDataHeadInfo").show();    // 隐藏表头信息
        //    jQuery("#FinalConclusionDataBottomInfo").show();  // 隐藏下面输入及操作信息


        var CurrExamSeciontCount = 0; // 当前分科检查的编号
        var examSectionContent = "";  // 分科检查的内容
        // 分科检查列表模版
        var CustExamSectionListTempleteContent = jQuery('#CustExamSectionListTemplete').html();
        if (CustExamSectionListTempleteContent == undefined) { return; }
        var isCanFinalConclusion = true; // 是否能进行总检
        // dataList0 分科检查小结信息
        var Is_EvenLine = true;

        var RowNum = 1;

        jQuery(examSectionItems.dataList0).each(function(j, examsectionitem) {

            CurrExamSeciontCount++;

            examSectionContent += CustExamSectionListTempleteContent
                            .replace(/@RowNum/gi, RowNum)
                            .replace(/@ID_Section/gi, examsectionitem.ID_Section)
                            .replace(/@InterfaceType/gi, examsectionitem.InterfaceType)
                            .replace(/@SectionName/gi, examsectionitem.SectionName)
                            .replace(/@SectionSummaryText/gi, htmlEncode(examsectionitem.SectionSummary.replace(/-{15}/gi, '')))
                            .replace(/@SummaryDoctorName/gi, examsectionitem.SummaryDoctorName)
                            .replace(/@SectionSummaryDate/gi, examsectionitem.SummaryDoctorName != '' ? examsectionitem.SectionSummaryDate : "")
                            .replace(/@Examed/gi, examsectionitem.IS_giveup == 'True' ? "<span title='已弃检'>×</span>" : examsectionitem.SummaryDoctorName == '' ? '--' : '√')
                            .replace(/@Checked/gi, examsectionitem.Is_Check == 'True' ? '√' : '--')
                            .replace(/@GiveUp/gi, examsectionitem.IS_giveup == 'True' ? '√' : '--')
                            .replace(/@tr_class/gi, Is_EvenLine == true ? "tr_even" : "tr_odd")
                            ;
            RowNum++;
            if (Is_EvenLine == true) {
                Is_EvenLine = false;
            }
            else {
                Is_EvenLine = true;
            }
            if (isCanFinalConclusion == true) {
                // 如果存在一个科室有 未弃检 且 未审核的情况， 则不能进行总检。
                if (examsectionitem.Is_Check != 'True' && examsectionitem.IS_giveup != 'True') {
                    isCanFinalConclusion = false;
                }
            }
        });
        jQuery("#CustExamSectionList").html(examSectionContent);
        ShowAllConclusionInfo();  // 显示页面内容

        // 当体检次数大于1次时，才能进行总检对比   临时改成显示总检对比 20140507 by wtang 
        if (parseInt(jQuery("#totalExamNumber").val()) >= 0) {

            jQuery("#ExamInfoTabLi2").show();      // 显示历史对比 20140408 by wtang 

        }

        // 数据加载完成 20140408 by wtang
        $(".d-content").subLength();  // 隐藏分科多余的内容，只显示40个字
        jQuery(".SectionDetailInfo").ShowSectionDetail(); // 分科明细弹出框

        if (jQuery("#Is_FinalConclusion").val() != "True") {

            // 如果是总审页面，则先设置背景， 设置Table的基数偶数项背景
            SetTableEvenOddRowStyle();
        }

        jQuery("#ButReprotPreview").show();         // 报告预览
        jQuery("#ButUnLockFinalCheck1").show();     // 解除总检

        // 显示结论词列表
        var conclusionContent = ""; //结论词table内容
        var conclusionEditContent = ""; //结论词文本编辑区域
        var ConclusionSelectListTempleteContent = jQuery('#ConclusionSelectListTemplete').html();             //选择后的结论词显示模版
        if (ConclusionSelectListTempleteContent == undefined) { return; }
        // 文本编辑区域模版

        var ConclusionSelectedDataEditTempleteContent = jQuery('#ConclusionSelectedDataEditTemplete').html(); //选择后的结论词显示模版
        if (ConclusionSelectedDataEditTempleteContent == undefined) { return; }

        var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
        var currCodeIndex = 0;

        // 控制只有总检页面才加载下面的信息 20130623 by WTang s
        if (jQuery("#Is_FinalConclusion").val() == "True") {
            // dataList1 加载已保存的结论词
            jQuery(examSectionItems.dataList1).each(function(j, lastconclusionitems) {

                CurrQueryCount++;
                // 列表区域
                conclusionContent += ConclusionSelectListTempleteContent
                    .replace(/@ID_Conclusion/gi, lastconclusionitems.ID_Conclusion)
                    .replace(/@ID_CustConclusion/gi, lastconclusionitems.ID_CustConclusion)
                    .replace(/@ConclusionName/gi, lastconclusionitems.ConclusionName)
                    .replace(/@FinalConclusionTypeName/gi, lastconclusionitems.FinalConclusionTypeName)
                    .replace(/@FinalConclusionSignCode/gi, lastconclusionitems.DiagnoseType)                                // 由原来的 FinalConclusionSignCode 改为 读取诊断类型 DiagnoseType by wtang 20140421 
                    .replace(/@FinalConclusionTypeClass/gi, GetFinalConclusionTypeClass(lastconclusionitems.DiagnoseType))  // 由原来的 FinalConclusionSignCode 改为 读取诊断类型 DiagnoseType by wtang 20140421 
                    .replace(/@DispOrder/gi, lastconclusionitems.DispOrder == "" ? CurrQueryCount : lastconclusionitems.DispOrder)
                    .replace(/@Is_Checked/gi, lastconclusionitems.Is_NoEntrySuggestion == "True" ? " checked = \"checked\" " : "")
                    .replace(/@ConclusionTypeName/gi, lastconclusionitems.TeamConclusionName)
                    .replace(/@chkSelectedConclusionListName/gi, "chkSelectedConclusionList")
                    ;

                // 文本编辑区域
                conclusionEditContent += ConclusionSelectedDataEditTempleteContent
                    .replace(/@ID_Conclusion/gi, lastconclusionitems.ID_Conclusion)
                    .replace(/@ConclusionName/gi, lastconclusionitems.ConclusionShowName)
                    .replace(/@FinalConclusionSignCode/gi, lastconclusionitems.DiagnoseType == "" ? "-" : lastconclusionitems.DiagnoseType) // 由原来的 FinalConclusionSignCode 改为 读取诊断类型 DiagnoseType by wtang 20140421 
                    .replace(/@Explanation/gi, lastconclusionitems.Explanation.replace(/<br\/>/gi, "\n"))
                    .replace(/@Suggestion/gi, lastconclusionitems.Suggestion.replace(/<br\/>/gi, "\n"))
                    .replace(/@DietGuide/gi, lastconclusionitems.DietGuide.replace(/<br\/>/gi, "\n"))
                    .replace(/@SportsGuide/gi, lastconclusionitems.SportGuide.replace(/<br\/>/gi, "\n"))
                    .replace(/@HealthKnowledge/gi, lastconclusionitems.HealthKnowledge.replace(/<br\/>/gi, "\n"))
                    ;
            });
        }

        QueryConclusionList(false);
        jQuery('#txtConclusionInputCode').val(""); //清空文本框中的数据

        if (conclusionEditContent != "") {
            jQuery("#ConclusionSelectedDataEdit").html(conclusionEditContent);
            // 设置诊断类型的选中状态
            jQuery(examSectionItems.dataList1).each(function(j, lastconclusionitems) {
                jQuery("input[name='rdiDiagnoseType_" + lastconclusionitems.ID_Conclusion + "']").each(function() {
                    if (lastconclusionitems.DiagnoseType == jQuery(this).val()) {
                        jQuery(this).attr("checked", true);
                    }
                });
            });

            jQuery("#ConclusionSelectedDataEdit").show();
        }
        if (conclusionContent != "") {
            jQuery("#ConclusionDataListParent").show();
            jQuery("#ConclusionSelectedList").html(conclusionContent);
            ScrollToTop('ConclusionSelectedListFrame'); // 滚动条滚动到底部
            SortByOrderNumber(); // 重新排序
        }


        var Is_SectionLock = jQuery('#Is_SectionLock').val();               //分科锁定
        if (Is_SectionLock == "True") {
            jQuery("#ButUnLock").removeAttr("disabled");
            jQuery("#ButLock").attr("disabled", "disabled");
        } else {
            jQuery("#ButUnLock").attr("disabled", "disabled");
            jQuery("#ButLock").removeAttr("disabled");
        }

        // 显示总检相关的提示信息 (总检提示)
        ShowFinalConclusionMsg(isCanFinalConclusion);
        // 显示总审相关的提示信息 (总审提示)
        ShowFinalCheckMsg(isCanFinalConclusion);

        // 显示已检查科室链接 20130829 by WTang 
        jQuery("#divSectionExamedCount").show();

        SetSideFloatXY();

        // 控制只有总检页面才自动关联结论词 20131114 by WTang
        // 如果还没有添加过结论词，则自动进行关联
        if (jQuery("#Is_FinalConclusion").val() == "True" && CurrQueryCount == 0) {

            if (jQuery("#IsLoadConnectConclusion").val() == "1") {
                //  根据体征词自动关联结论词
                GetDefaultSymptomConnectConclusion();
            }
        }
        // 设置Table的基数偶数项背景
        SetTableEvenOddRowStyle();

        // 初始化 放大编辑
        InitBigEditer();

    }

    /// <summary>
    /// 显示总检相关的提示信息(总检提示)
    /// </summary>
    function ShowFinalConclusionMsg(isCanFinalConclusion) {

        // 检测体检状态，主要检查客户资料是否已经归档
        CheckExamState();
        if (jQuery('#ExamState').val() != "0") { return; }

        // 是否是总检
        var Is_FinalConclusion = jQuery("#Is_FinalConclusion").val();
        if (Is_FinalConclusion != "True") {
            return;
        }
        // 完成总检
        var Is_FinalFinished = jQuery("#Is_FinalFinished").val();

        if (isCanFinalConclusion == true && Is_FinalFinished != "True") {
            UpdateFinalConclusionSectionLock(1); // 进入总检时，即刻对分科检查进行锁定。
        }

        // 判断是否能进行总检（即判断分检是否完成/分检是否审核）
        if (Is_FinalConclusion == "True" && isCanFinalConclusion == false && Is_FinalFinished != "True") {

            CtrlFinalFinishedButtonDisabled(2, false); // 禁用按钮

            art.dialog({
                id: 'artDialogID',
                lock: true,
                fixed: true,
                zIndex: 555,
                opacity: 0.3,
                content: '【提示】该客户还不能进行总检！<br/>【原因】分检未完成/分检未审核！',
                button: [{
                    name: '确定',
                    callback: function() {
                        jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                        return true;
                    }
}],
                    close: function() {
                        jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                        return true;
                    }
                });
            }
            else {
                jQuery("#IsLoadConnectConclusion").val("1");  // 开启 根据体征词自动关联结论词 
            }
        }

        // 设置选中的结论词获取焦点
        function SetSelectConclusionItemFocus() {
            jQuery("#txtConclusionOrder_" + OldshowConclusionID).focus();  // 获取焦点
            jQuery("#txtConclusionOrder_" + OldshowConclusionID).select(); // 选中文字

        }

        /// <summary>
        /// 显示总审相关的提示信息 (总审提示)
        /// </summary>
        function ShowFinalCheckMsg(isCanFinalConclusion) {

            // 检测体检状态，主要检查客户资料是否已经归档
            CheckExamState();
            if (jQuery('#ExamState').val() != "0") { return; }

            // 是否是总审
            var Is_FinalCheck = jQuery("#Is_FinalCheck").val();
            if (Is_FinalCheck != "True") {
                return;
            }
            // 总审通过
            var Is_Checked = jQuery("#Is_Checked").val();

            // 完成总检 （审核不通过时，将完成总检标记置为False）
            var Is_FinalFinished = jQuery("#Is_FinalFinished").val();

            // 判断是否总审通过
            if (Is_Checked == "True") {

                CtrlFinalCheckButtonDisabled(0, false);

            } else {
                if (Is_FinalFinished != "True") {
                    CtrlFinalCheckButtonDisabled(0, false);
                    art.dialog({
                        id: 'artDialogID',
                        lock: true,
                        fixed: true,
                        zIndex: 555,
                        opacity: 0.3,
                        content: '【提示】该客户还未完成总检，不能进行总审！<br/>【备注】请先总检后，再进行总审！',
                        button: [{
                            name: '确定',
                            callback: function() {
                                jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                                return true;
                            }

}],
                            close: function() {
                                jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                                return true;
                            }
                        });

                    }
                }
            }



            /// <summary>
            /// 检测体检状态，主要检查客户资料是否已经归档
            /// </summary>
            function CheckExamState() {

                var ExamState = jQuery('#ExamState').val();                         //体检状态,当次体检信息的状态：0表示在线，1表示归档，2表示在一号分库，3表示在二号分库…
                //体检状态,当次体检信息的状态：0表示在线，1表示归档，2表示在一号分库，3表示在二号分库…
                if (ExamState != "0") {
                    jQuery('#FinalExamDatailInfo').hide();   //隐藏检查详细内容

                    art.dialog({
                        id: 'artDialogID',
                        lock: true,
                        fixed: true,
                        zIndex: 555,
                        opacity: 0.3,
                        content: '【提示】该客户已归档，不能进行总检操作！',
                        button: [{
                            name: '确定',
                            callback: function() {
                                jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                                return true;
                            }
}],
                            close: function() {
                                jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                                return true;
                            }
                        });
                        return;
                    }

                }


                /// <summary>
                /// 控制总检页面按钮是否能操作
                /// </summary>
                /// <param name="type">0：提交 1：分科解锁 </param>
                /// <param name="flag"> fasle：表示有部分按钮不能使用  true：表示所有按钮都能使用 </param>
                function CtrlFinalFinishedButtonDisabled(type, flag) {

                    if (flag == false) {
                        switch (type) {
                            case 0:
                            case 1:
                                jQuery("#ButSave").attr("disabled", "disabled");     // 提交
                                jQuery("#ButCollect").attr("disabled", "disabled");  // 汇总
                                jQuery("#ButClear").attr("disabled", "disabled");    // 清除
                                jQuery("#ButUnLock01").attr("disabled", "disabled"); // 分科解锁1
                                jQuery("#ButUnLock02").attr("disabled", "disabled"); // 分科解锁2
                                jQuery("#ButSaveFinalCompare").attr("disabled", "disabled"); // 总检对比
                                jQuery("#ButSaveFinalCompare").addClass("ImageGray");   // 总检对比 背景图片灰色
                                jQuery("#txtWriteResultCompare").attr("disabled", "disabled"); // 总检对比文本输入框


                                break;
                            case 2:
                                jQuery("#ButSave").attr("disabled", "disabled");     // 提交
                                jQuery("#ButCollect").attr("disabled", "disabled");  // 汇总
                                jQuery("#ButClear").attr("disabled", "disabled");    // 清除
                                jQuery("#ButUnLock01").removeAttr("disabled");  // 分科解锁1
                                jQuery("#ButUnLock02").removeAttr("disabled");  // 分科解锁2
                                jQuery("#ButSaveFinalCompare").attr("disabled", "disabled"); // 总检对比
                                jQuery("#txtWriteResultCompare").attr("disabled", "disabled"); // 总检对比文本输入框
                                jQuery("#ButSaveFinalCompare").addClass("ImageGray");   // 总检对比 背景图片灰色

                                break;
                        }

                    } else {
                        jQuery("#ButSave").removeAttr("disabled");      // 提交
                        jQuery("#ButCollect").removeAttr("disabled");   // 汇总
                        jQuery("#ButClear").removeAttr("disabled");     // 清除
                        jQuery("#ButUnLock01").removeAttr("disabled");  // 分科解锁1
                        jQuery("#ButUnLock02").removeAttr("disabled");  // 分科解锁2
                        jQuery("#ButSaveFinalCompare").removeAttr("disabled");      // 总检对比
                        jQuery("#txtWriteResultCompare").removeAttr("disabled");    // 总检对比
                        jQuery("#ButSaveFinalCompare").removeClass("ImageGray");    // 总检对比 背景图片灰色
                    }
                }



                /// <summary>
                /// 控制总审页面按钮是否能操作
                /// </summary>
                /// <param name="type">0：审核通过 1：审核不通过 2：解除总检 </param>
                /// <param name="flag"> fasle：表示有部分按钮不能使用  true：表示所有按钮都能使用 </param>
                function CtrlFinalCheckButtonDisabled(type, flag) {

                    if (flag == false) {
                        switch (type) {
                            case 0:
                            case 1:
                            case 2:
                                jQuery("#ButChecked").attr("disabled", "disabled"); // 总审通过
                                jQuery("#ButUnCheck").attr("disabled", "disabled"); // 总审不通过
                                jQuery("#ButUnLockFinalCheck1").attr("disabled", "disabled"); // 解除总检1
                                jQuery("#ButUnLockFinalCheck2").attr("disabled", "disabled"); // 解除总检2
                                jQuery("#ButSaveCustomerFinalCheck").attr("disabled", "disabled");
                                jQuery("#CustomerFinalCheckState01").attr("disabled", "disabled");
                                jQuery("#CustomerFinalCheckState02").attr("disabled", "disabled");
                                jQuery("#ButSaveCustomerFinalCheck").addClass("ImageGray");   // 背景图片灰色
                                break;
                        }

                    } else {
                        jQuery("#ButChecked").removeAttr("disabled"); // 总审通过
                        jQuery("#ButUnCheck").removeAttr("disabled"); // 总审不通过
                        jQuery("#ButUnLockFinalCheck1").removeAttr("disabled"); // 解除总检1
                        jQuery("#ButUnLockFinalCheck2").removeAttr("disabled"); // 解除总检2
                        jQuery("#ButSaveCustomerFinalCheck").removeAttr("disabled");
                        jQuery("#CustomerFinalCheckState01").removeAttr("disabled");
                        jQuery("#CustomerFinalCheckState02").removeAttr("disabled");
                        jQuery("#ButSaveCustomerFinalCheck").removeClass("ImageGray");    // 背景图片灰色
                    }
                }



                var gAllConclusionDataList = "";    // 保存全部的结论词列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
                var OldInputCode = "";              // 记录上次输入的输入码
                var gAllConclusionListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
                /// <summary>
                /// 根据输入码查询结论词（通过Ajax后台过滤）
                /// </summary>
                function GetConclusionByKeyWords_Ajax() {

                    var curEvent = window.event || e;
                    if (curEvent.keyCode == 13 || curEvent.keyCode == 37 || curEvent.keyCode == 38 || curEvent.keyCode == 39 || curEvent.keyCode == 40) {

                        // jQuery("#chkConclusion_" + jQuery("#tempSelectedConclusionID").val() + "").attr('checked', true); 默认不选中第一条结论词 20131113 by wtang
                        jQuery("#chkConclusion_" + jQuery("#tempSelectedConclusionID").val() + "").focus();
                    }

                    var InputCode = jQuery('#txtConclusionInputCode').val();
                    if (OldInputCode != InputCode) {
                        OldInputCode = InputCode;
                    } else {
                        QueryConclusionList(true);
                        return;
                    }
                    if (InputCode != "") {
                        jQuery.ajax({
                            type: "POST",
                            url: "/Ajax/AjaxConclusion.aspx",
                            data: { InputCode: encodeURIComponent(InputCode),
                                action: 'GetConclusionByKeyWords',
                                currenttime: encodeURIComponent(new Date())
                            },
                            cache: false,
                            dataType: "json",
                            success: function(jsonmsg) {

                                // 检查Ajax返回数据的状态等 
                                jsonmsg = CheckAjaxReturnDataInfo(jsonmsg);
                                if (jsonmsg == null || jsonmsg == "") {
                                    QueryConclusionList(false);
                                    return;
                                }


                                // 显示查询到的结论词
                                ShowConclusionDataList_Ajax(jsonmsg, InputCode);
                            }
                        });
                    } else {
                        ShowConclusionDataList_Ajax("", InputCode);
                    }
                }

                /// <summary>
                /// 根据查询结果数据，显示结论词（通过Ajax过滤）
                /// </summary>
                function ShowConclusionDataList_Ajax(conclusionlist, InputCode) {

                    var conclusionContent = ""; //结论词table内容

                    var ConclusionQueryListTempleteContent = jQuery('#ConclusionQueryListTemplete').html();             //结论词模版
                    if (ConclusionQueryListTempleteContent == undefined) { return; }
                    var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
                    var currCodeIndex = 0;

                    if (conclusionlist != null && conclusionlist != "") {

                        jQuery("#tempSelectedConclusionID").val("");
                        jQuery(conclusionlist.dataList).each(function(j, conclusionitem) {
                            // 如果字符串不包含这个输入码，则继续下一条数据的判断

                            CurrQueryCount++;
                            conclusionContent += ConclusionQueryListTempleteContent
                            .replace(/@ID_Conclusion/gi, conclusionitem.ID_Conclusion)
                            .replace(/@ConclusionName/gi, conclusionitem.ConclusionName)
                            .replace(/@ConclusionTypeName/gi, conclusionitem.TeamConclusionName)
                            .replace(/@FinalConclusionTypeName/gi, conclusionitem.FinalConclusionTypeName)
                            .replace(/@FinalConclusionSignCode/gi, conclusionitem.FinalConclusionSignCode)
                            .replace(/@FinalConclusionTypeClass/gi, GetFinalConclusionTypeClass(conclusionitem.FinalConclusionSignCode))
                            ;

                            if (CurrQueryCount == 1) {
                                jQuery("#tempSelectedConclusionID").val(conclusionitem.ID_Conclusion);
                            }
                        });
                    }
                    if (conclusionContent != "") {
                        QueryConclusionList(true);
                        jQuery("#QueryConclusionDataTableList").html(conclusionContent);

                    } else {
                        QueryConclusionList(false);
                        jQuery("#QueryConclusionDataTableList").html("");
                    }

                }

                // ===============  选项结论词后，点击确定按钮的相关操作 Start ===========================



                /// <summary>
                /// 点击确定按钮（确定选择结论词）
                /// </summary>
                function SelectConclusionDataList() {
                    var selectedValueList = "";
                    jQuery("input[name='chkConclusionQueryList']:checkbox:checked").each(function() {
                        selectedValueList += jQuery(this).val() + ",";
                    });
                    if (selectedValueList != "") {
                        AddToSelectedConclusionDataList(selectedValueList);
                    }
                    jQuery("#txtConclusionInputCode").val(""); // 情况输入框
                    jQuery("#txtConclusionInputCode").focus(); // 输入框获取焦点
                }

                /// <summary>
                /// 将选中的结论词添加到下面显示区域
                /// </summary>
                function AddToSelectedConclusionDataList(IDList) {
                    var ExistIDList = "";
                    jQuery("input[name='chkSelectedConclusionList']").each(function() {
                        ExistIDList += jQuery(this).val() + ",";
                    });
                    jQuery.ajax({
                        type: "POST",
                        url: "/Ajax/AjaxConclusion.aspx",
                        data: { IDList: IDList,
                            ExistIDList: ExistIDList,
                            action: 'GetConclusionAllContentByIDs',
                            currenttime: encodeURIComponent(new Date())
                        },
                        cache: false,
                        dataType: "json",
                        success: function(jsonmsg) {
                            // 显示查询到的结论词
                            ShowAddToSelectedConclusionDataList(jsonmsg);
                            AddEditInfoConclusionDataEditArea(jsonmsg);

                        }
                    });
                }
                /// <summary>
                /// 根据体征词自动关联结论词
                /// </summary>
                function GetDefaultSymptomConnectConclusion() {

                    var CustomerID = jQuery.trim(jQuery('#HiddenCustomerID').val());
                    if (!isCustomerExamNo(CustomerID)) {
                        return;
                    }
                    jQuery.ajax({
                        type: "POST",
                        url: "/Ajax/AjaxConclusion.aspx",
                        data: { CustomerID: CustomerID,
                            action: 'GetSymptomConnectConclusion',
                            currenttime: encodeURIComponent(new Date())
                        },
                        cache: false,
                        dataType: "json",
                        success: function(jsonmsg) {
                            // 显示查询到的结论词
                            ShowAddToSelectedConclusionDataList(jsonmsg);
                            AddEditInfoConclusionDataEditArea(jsonmsg);
                        }
                    });
                }
                /// <summary>
                /// 显示选择的数据到操作区域
                /// </summary>
                function ShowAddToSelectedConclusionDataList(conclusionlist) {

                    var conclusionContent = ""; //结论词table内容
                    var showID_Conclusion = "0"; //列表加载完成后，自动显示的结论词详细信息 
                    var ConclusionSelectListTempleteContent = jQuery('#ConclusionSelectListTemplete').html();             //选择后的结论词显示模版
                    if (ConclusionSelectListTempleteContent == undefined) { return; }
                    var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
                    var currCodeIndex = 0;

                    if (conclusionlist != null && conclusionlist != "") {
                        jQuery(conclusionlist.dataList).each(function(j, conclusionitem) {
                            // 如果字符串不包含这个输入码，则继续下一条数据的判断

                            CurrQueryCount++;
                            conclusionContent += ConclusionSelectListTempleteContent
                            .replace(/@ID_Conclusion/gi, conclusionitem.ID_Conclusion)
                            .replace(/@ID_CustConclusion/gi, "0")
                            .replace(/@DispOrder/gi, "0")
                            .replace(/@Is_Checked/gi, "")
                            .replace(/@FinalConclusionTypeName/gi, conclusionitem.FinalConclusionTypeName)
                            .replace(/@FinalConclusionSignCode/gi, conclusionitem.FinalConclusionSignCode)
                            .replace(/@FinalConclusionTypeClass/gi, GetFinalConclusionTypeClass(conclusionitem.FinalConclusionSignCode))
                            .replace(/@ConclusionName/gi, conclusionitem.ConclusionName)
                            .replace(/@ConclusionTypeName/gi, conclusionitem.TeamConclusionName)
                            .replace(/@chkSelectedConclusionListName/gi, "chkSelectedConclusionList")
                            ;
                            showID_Conclusion = conclusionitem.ID_Conclusion; // 记录最后一次的结论词ID
                            jQuery("#tempShowConclusionID").val(conclusionitem.ID_Conclusion);
                        });
                    }
                    QueryConclusionList(false);
                    jQuery('#txtConclusionInputCode').val(""); //清空文本框中的数据
                    if (conclusionContent != "") {
                        jQuery("#ConclusionDataListParent").show();
                        jQuery("#ConclusionSelectedList").append(conclusionContent);
                        ScrollToTop('ConclusionSelectedListFrame'); // 滚动条滚动到底部
                    }

                    // 清空默认的显示顺序
                    jQuery(conclusionlist.dataList).each(function(j, conclusionitem) {
                        jQuery("#txtConclusionOrder_" + conclusionitem.ID_Conclusion).val("");
                    });

                    jQuery("#txtConclusionInputCode").val(""); // 清空输入框

                }


                /// <summary>
                /// 添加对应的编辑框到编辑区域。
                /// </summary>
                function AddEditInfoConclusionDataEditArea(conclusionlist) {
                    var conclusionContent = "";
                    var ConclusionSelectedDataEditTempleteContent = jQuery('#ConclusionSelectedDataEditTemplete').html(); //选择后的结论词显示模版
                    if (ConclusionSelectedDataEditTempleteContent == undefined) { return; }
                    if (conclusionlist != null && conclusionlist != "") {
                        jQuery(conclusionlist.dataList).each(function(j, conclusionitem) {

                            // 如果字符串不包含这个输入码，则继续下一条数据的判断

                            conclusionContent += ConclusionSelectedDataEditTempleteContent
                            .replace(/@ID_Conclusion/gi, conclusionitem.ID_Conclusion)
                            .replace(/@ConclusionName/gi, conclusionitem.ConclusionName)
                            .replace(/@FinalConclusionSignCode/gi, conclusionitem.FinalConclusionSignCode == "" ? "-" : conclusionitem.FinalConclusionSignCode)
                            .replace(/@Explanation/gi, conclusionitem.Explanation)
                            .replace(/@Suggestion/gi, conclusionitem.Suggestion)
                            .replace(/@DietGuide/gi, conclusionitem.DietGuide)
                            .replace(/@SportsGuide/gi, conclusionitem.SportsGuide)
                            .replace(/@HealthKnowledge/gi, conclusionitem.HealthKnowledge)
                            ;
                        });
                    }
                    if (conclusionContent != "") {
                        jQuery("#ConclusionSelectedDataEdit").append(conclusionContent);
                        jQuery("#ConclusionSelectedDataEdit").show();

                        if (jQuery("#tempShowConclusionID").val() != "") {
                            ShowSelectedEditArea(jQuery("#tempShowConclusionID").val());
                        }

                        // 初始化 放大编辑
                        InitBigEditer();
                    }

                }

                // ===============  选项结论词后，点击确定按钮的相关操作 End ===========================

                /// <summary>
                /// 删除选择的结论词
                /// </summary>
                function delSelectedConclusion(DelID) {


                    // 完成总检 
                    var Is_FinalFinished = jQuery("#Is_FinalFinished").val();
                    if (Is_FinalFinished == "True") {

                        ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#HiddenCustomerID').val()) + "】已经完成总检，不能再删除结论词!");
                        return;
                    }

                    var tipscontent = "您正在执行删除“" + jQuery("#chkSelectedConclusionList_" + DelID).attr("ConclusionName") + "”的操作，请确认是否继续！";
                    art.dialog({
                        id: 'artDialogID',
                        content: tipscontent,
                        lock: true,
                        zIndex: 555,
                        fixed: true,
                        opacity: 0.3,
                        button: [{
                            name: '确定删除',
                            title: '提示',
                            callback: function() {
                                jQuery("#trSel_" + DelID).remove();
                                jQuery("#ConclusionSelectedDataEdit_" + DelID).remove();
                                ScrollToTop('ConclusionSelectedListFrame'); // 滚动条滚动到底部
                                return true;
                            }, focus: true
                        }, {
                            name: '取消',
                            callback: function() {
                                return true;
                            }
}],
                            close: function() {
                                return true;
                            }
                        });


                    }
                    var OldshowConclusionID = "";
                    // 编辑框的选中与隐藏
                    function ShowSelectedEditArea(ConclusionID) {
                        // 1. 如果之前有显示的，先隐藏以前的编辑框
                        if (OldshowConclusionID != "") {

                            jQuery("#ConclusionSelectedDataEdit_" + OldshowConclusionID).hide();
                            jQuery("#trSel_" + OldshowConclusionID).attr("class", "");
                        }
                        // 2. 显示当前选择的编辑框
                        jQuery("#ConclusionSelectedDataEdit_" + ConclusionID).show();
                        jQuery("#trSel_" + ConclusionID).attr("class", "selected");
                        OldshowConclusionID = ConclusionID;

                    }


                    // ===============  通过js的方式在前台进行数据的过滤 Start ===========================

                    // ===============  按钮函数区域 Start ========================================================



                    /// <summary>
                    /// 清空客户信息（清空客户的所有信息，便于下一个客户数据的读取）
                    /// </summary>
                    function ClearCustomerInfo() {

                        // 查询数据前，先隐藏客户基本信息区域
                        jQuery("#divCustomerInfoArea").hide();

                        var ExamItemEmptyTempleteContent = jQuery('#ExamItemEmptyTemplete').html();     //读取空数据信息模版
                        if (ExamItemEmptyTempleteContent == undefined) { return; }
                        jQuery('#TipsMessage').html(ExamItemEmptyTempleteContent);                     //显示空信息
                        jQuery('#TipsArea').show(); // 显示页面内部的提示信息 20140408 by wtang 

                        jQuery('#IDCardNoText').html("&nbsp;");
                        jQuery('#CustomerNameText').html("&nbsp;");
                        jQuery('#GenderNameText').html("&nbsp;");
                        jQuery('#MarriageNameText').html("&nbsp;");
                        jQuery('#MobileNoText').html("&nbsp;");

                        jQuery('#txtCustomerID').val("");  //清空体检号


                        jQuery("#FinalConclusionDataHeadInfo").hide();    // 隐藏表头信息
                        jQuery("#FinalConclusionDataBottomInfo").hide();  // 隐藏下面输入及操作信息

                    }

                    var WrapCharacter = "\n";        //显示的换行符号
                    var WrapCharacter2 = "\r\n";     //显示的换行符号
                    var htmlWrapCharacter = "<br/>"; //html 换行符号

                    /// <summary>
                    /// 清除(这里是指，清除文本框的数据)
                    /// </summary>
                    function ClearFinalConclusionText() {

                        jQuery("#txtConclusionFinalConclusion").val("");       // 结论汇总
                        jQuery("#txtConclusionFinalSportsGuide").val("");      // 运动指导
                        jQuery("#txtConclusionFinalDietGuide").val("");        // 饮食指导
                        jQuery("#txtConclusionFinalHealthKnowlage").val("");   // 健康建议

                        jQuery("#txtConclusionMainDiagnose").val("");          // 主要诊断
                        jQuery("#txtConclusionSecondaryDiagnose").val("");     // 次要诊断
                        jQuery("#txtConclusionIndicatorDiagnose").val("");     // 指标异常
                        jQuery("#txtConclusionNormalDiagnose").val("");        // 正常诊断
                        jQuery("#txtConclusionOtherDiagnose").val("");         // 其它诊断
                        jQuery("#txtConclusionResultCompare").val("");         // 结果对比
                        jQuery("#txtConclusionFinalOverView").val("");         // 综述


                        jQuery("#htmlConclusionFinalConclusion").html("");      // 结论汇总
                        jQuery("#htmlConclusionFinalSportsGuide").html("");     // 运动指导
                        jQuery("#htmlConclusionFinalDietGuide").html("");       // 饮食指导
                        jQuery("#htmlConclusionFinalHealthKnowlage").html("");  // 健康建议

                        jQuery("#htmlConclusionMainDiagnose").html("");      // 主要诊断
                        jQuery("#htmlConclusionSecondaryDiagnose").val("");  // 次要诊断
                        jQuery("#htmlConclusionIndicatorDiagnose").val("");  // 指标异常
                        jQuery("#htmlConclusionNormalDiagnose").val("");     // 正常诊断
                        jQuery("#htmlConclusionOtherDiagnose").val("");      // 其它诊断
                        jQuery("#htmlConclusionFinalOverView").html("");     // 综述

                    }

                    /// <summary>
                    /// 锁定 / 解除锁定 （总检）
                    /// -1:体检号不存在。
                    /// -2:还未进行分科锁定，不需要进行分科解锁。
                    /// -3:已经被总审，不能进行分科解锁，需要先解除总审,总检后才能解除总检。
                    /// -4:已经总检，不能进行分科解锁，需要先解除总检后才能进行分科解锁。
                    /// </summary>
                    function UpdateFinalConclusionSectionLock(IsSectionLock) {

                        if (IsSectionLock == 0)
                            jQuery('#Is_SectionLock').val("False");                 //分科解锁
                        if (IsSectionLock == 1)
                            jQuery('#Is_SectionLock').val("True");                  //分科锁定

                        var CustomerID = jQuery.trim(jQuery('#HiddenCustomerID').val());
                        jQuery.ajax({
                            type: "POST",
                            url: "/Ajax/AjaxConclusion.aspx",
                            data: { CustomerID: CustomerID,
                                IsSectionLock: IsSectionLock,
                                action: 'UpdateFinalConclusionSectionLock',
                                currenttime: encodeURIComponent(new Date())
                            },
                            cache: false,
                            dataType: "json",
                            success: function(jsonmsg) {
                                var tmpstr = "";
                                if (jQuery('#Is_SectionLock').val() == "False") { temstr = "解除"; }

                                if (jsonmsg != null && jsonmsg != "") {
                                    if (parseInt(jsonmsg) > 0) {

                                        if (jQuery('#Is_SectionLock').val() == "False") {

                                            CtrlFinalFinishedButtonDisabled(1, false);
                                            ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#HiddenCustomerID').val()) + "】解除分科锁定成功!");

                                        } else {

                                        }
                                    }
                                    else if (parseInt(jsonmsg) == 0) { ShowSystemDialog(temstr + "分科锁定失败，请与技术人员联系!") }
                                    else {
                                        if (jsonmsg == "-101") {
                                            ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#HiddenCustomerID').val()) + "】已经归档,不能进行分科解锁!");
                                        } else if (jsonmsg == "-1") {
                                            ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#HiddenCustomerID').val()) + "】不存在!");
                                        } else if (jsonmsg == "-2") {
                                            ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#HiddenCustomerID').val()) + "】还未进行分科锁定，不需要进行分科解锁。");
                                        } else if (jsonmsg == "-3") {
                                            ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#HiddenCustomerID').val()) + "】已经被总审，不能进行分科解锁，需要先解除总审,总检后才能解除总检。");
                                        } else if (jsonmsg == "-4") {
                                            ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#HiddenCustomerID').val()) + "】已经总检，不能进行分科解锁，需要先解除总检后才能进行分科解锁。");
                                        } else if (jsonmsg == "-5") {
                                            ShowSystemDialog("体检号【" + jQuery.trim(jQuery('#HiddenCustomerID').val()) + "】体检报告已经领取，不能进行解除总审操作 。");
                                        }
                                    }
                                }
                            }
                        });

                    }


                    /// <summary>
                    /// 提交确认
                    /// </summary>
                    function SaveCustomerFinalConclusionConfirm(Is_FinalFinished) {
                        var tipscontent = "您正在执行总检【提交审核】操作，提交后将不能再进行修改，请确认是否继续！";
                        art.dialog({
                            id: 'artDialogID',
                            content: tipscontent,
                            lock: true,
                            fixed: true,
                            zIndex: 555,
                            opacity: 0.3,
                            button: [{
                                name: '确定提交',
                                title: '提示',
                                callback: function() {
                                    SaveCustomerFinalConclusion(Is_FinalFinished);
                                    return true;
                                }, focus: true
                            }, {
                                name: '取消',
                                callback: function() {
                                    return true;
                                }
}],
                                close: function() {
                                    return true;
                                }
                            });


                        }

                        /// <summary>        
                        /// 提交总检对比 1、提交时，先将文本框中的数据同步到总检结论显示的文本框中，和显示区域中。
                        ///             2、然后再调用汇总函数。
                        /// </summary>
                        function btnSaveFinalCompare() {

                            var tmpResultCompare = jQuery("#txtWriteResultCompare").val(); // 对比结果中输入的值
                            jQuery("#txtConclusionResultCompare").val(tmpResultCompare);       // 结论汇总（隐藏文本框内）
                            jQuery("#htmlConclusionResultCompare").html(tmpResultCompare.replace(/\n/gi, "<br\/>"));       // 结论汇总（html）
                            SaveCustomerFinalConclusion(0);
                        }

                        /// <summary>
                        /// 完成
                        /// </summary>
                        function SaveCustomerFinalConclusion(Is_FinalFinished) {

                            jQuery("#Oper_IsFinalFinished").val(Is_FinalFinished); // 操作类型  0：汇总 1：提交

                            GetFinalConclusionText(); // 先进行汇总

                            var txtConclusionFinalConclusion = jQuery("#txtConclusionFinalConclusion").val();        // 结论汇总
                            var txtConclusionFinalSportsGuide = jQuery("#txtConclusionFinalSportsGuide").val();      // 运动指导
                            var txtConclusionFinalDietGuide = jQuery("#txtConclusionFinalDietGuide").val();          // 饮食指导
                            var txtConclusionFinalHealthKnowlage = jQuery("#txtConclusionFinalHealthKnowlage").val(); // 健康建议

                            var txtConclusionMainDiagnose = jQuery("#txtConclusionMainDiagnose").val();      // 主要诊断
                            if (txtConclusionMainDiagnose == undefined) { txtConclusionMainDiagnose = ""; }

                            var txtConclusionSecondaryDiagnose = jQuery("#txtConclusionSecondaryDiagnose").val();      // 次要诊断
                            if (txtConclusionSecondaryDiagnose == undefined) { txtConclusionSecondaryDiagnose = ""; }

                            var txtConclusionIndicatorDiagnose = jQuery("#txtConclusionIndicatorDiagnose").val();      // 指标异常
                            if (txtConclusionIndicatorDiagnose == undefined) { txtConclusionIndicatorDiagnose = ""; }

                            var txtConclusionNormalDiagnose = jQuery("#txtConclusionNormalDiagnose").val();    // 正常诊断
                            if (txtConclusionNormalDiagnose == undefined) { txtConclusionNormalDiagnose = ""; }

                            var txtConclusionOtherDiagnose = jQuery("#txtConclusionOtherDiagnose").val();      // 其它诊断
                            if (txtConclusionOtherDiagnose == undefined) { txtConclusionOtherDiagnose = ""; }

                            var txtConclusionResultCompare = jQuery("#txtConclusionResultCompare").val(); //jQuery("#txtConclusionResultCompare").html();           // 结果对比
                            var txtConclusionFinalOverView = jQuery("#txtConclusionFinalOverView").val(); //jQuery("#txtConclusionFinalOverView").html();         // 综述

                            var CustomerID = jQuery.trim(jQuery('#HiddenCustomerID').val());

                            // 总检结论词相关参数
                            var FinalSelectedConclusionDataParams = GetSaveCustomerFinalConclusionParams();
                            if (FinalSelectedConclusionDataParams == "-1") {

                                CtrlFinalFinishedButtonDisabled(0, true);
                                return;
                            }

                            jQuery.ajax({
                                type: "POST",
                                url: "/Ajax/AjaxConclusion.aspx",
                                data: { CustomerID: CustomerID,
                                    Is_FinalFinished: Is_FinalFinished, // 0：汇总 1：提交
                                    txtConclusionFinalConclusion: encodeURIComponent(txtConclusionFinalConclusion),
                                    txtConclusionFinalSportsGuide: encodeURIComponent(txtConclusionFinalSportsGuide),
                                    txtConclusionFinalDietGuide: encodeURIComponent(txtConclusionFinalDietGuide),
                                    txtConclusionFinalHealthKnowlage: encodeURIComponent(txtConclusionFinalHealthKnowlage),
                                    txtConclusionMainDiagnose: encodeURIComponent(txtConclusionMainDiagnose),
                                    txtConclusionSecondaryDiagnose: encodeURIComponent(txtConclusionSecondaryDiagnose),
                                    txtConclusionIndicatorDiagnose: encodeURIComponent(txtConclusionIndicatorDiagnose),
                                    txtConclusionNormalDiagnose: encodeURIComponent(txtConclusionNormalDiagnose),
                                    txtConclusionOtherDiagnose: encodeURIComponent(txtConclusionOtherDiagnose),
                                    txtConclusionResultCompare: encodeURIComponent(txtConclusionResultCompare),
                                    txtConclusionFinalOverView: encodeURIComponent(txtConclusionFinalOverView),
                                    FinalSelectedConclusionDataParams: FinalSelectedConclusionDataParams,
                                    action: 'SaveCustomerFinalConclusion',
                                    currenttime: encodeURIComponent(new Date())
                                },
                                cache: false,
                                dataType: "json",
                                success: function(jsonmsg) {

                                    // 检查Ajax返回数据的状态等 20140430 by wtang 
                                    jsonmsg = CheckAjaxReturnDataInfo(jsonmsg);
                                    if (jsonmsg == null || jsonmsg == "") {
                                        return;
                                    }

                                    if (jsonmsg != null && jsonmsg != "") {

                                        if (jQuery("#Oper_IsFinalFinished").val() == "1") {
                                            if (parseInt(jsonmsg) > 0) {

                                                jQuery("#txtConclusionInputCode").hide();
                                                jQuery("#trConclusionQueryArea1").hide();  // 隐藏结论及结论词区域
                                                jQuery("#trConclusionQueryArea2").hide();  // 隐藏结论及结论词区域
                                                CtrlFinalFinishedButtonDisabled(0, false);

                                                art.dialog({
                                                    id: 'artDialogID',
                                                    lock: true,
                                                    fixed: true,
                                                    opacity: 0.3,
                                                    content: "提交总检信息成功",
                                                    button: [{
                                                        name: '确定',
                                                        callback: function() {
                                                            jQuery("#txtCustomerID").focus();  // 设置体检号文本框中获取焦点
                                                            return true;
                                                        }, focus: true
}]
                                                    });

                                                }
                                                else {
                                                    ShowSystemDialog("提交总检信息失败，请与技术人员联系!")
                                                }
                                            } else {
                                                if (parseInt(jsonmsg) > 0) {
                                                    ShowSystemDialog("汇总总检信息成功!");
                                                } else {
                                                    ShowSystemDialog("汇总总检信息失败，请与技术人员联系!");
                                                }
                                            }
                                        }
                                    }
                                });

                            }


                            /// <summary>
                            /// 结论及结论词的参数拼接
                            /// </summary>
                            function GetSaveCustomerFinalConclusionParams() {

                                var tmpConclusionName = "";     // 临时保存 当前结论词名称
                                var tmpConclusionTypeName = ""; // 临时保存 结论词分类名称
                                var tmpHealthKnowledge = "";    // 临时保存 健康知识
                                var tmpSportsGuide = "";        // 临时保存 运动指导
                                var tmpDietGuide = "";          // 临时保存 饮食指导
                                var tmpSuggestion = "";         // 临时保存 结论解释
                                var tmpExplanation = "";        // 临时保存 结论解释
                                var tmpDispOrder = "";          // 临时保存 显示排序
                                var tmpDiagnoseType = "";       // 临时保存 诊断类型

                                //                                    结论汇总编号       、结论词ID、     不汇总到建议          、页面排序   、结论词显示名称、    、结论词分类名称      、结论解释     、主要诊断   、饮食指导   、运动指导     、健康知识         、诊断类型
                                var SelectedConclusionDataTemplete = "@ID_CustConclusion、@ID_Conclusion、@Is_NoEntrySuggestion、@DispOrder、@ConclusionShowName、@ConclusionTypeName、@Explanation、@Suggestion、@DietGuide、@SportsGuide、@HealthKnowledge、@DiagnoseType";
                                var SelectedConclusionDataParams = "";    // 保存拼接后的字符串

                                var tempConclusionID = "0";

                                var Is_NoEntrySuggestion = "0"; // 不汇总到建议
                                jQuery("input[name='chkSelectedConclusionList']").each(function() {
                                    Is_NoEntrySuggestion = "0"
                                    if (jQuery(this).attr("checked") == "checked") {
                                        Is_NoEntrySuggestion = 1; // 不汇总到建议
                                    }

                                    tempConclusionID = jQuery(this).val(); // 当前选择的结论词ID
                                    if (jQuery("input[name='rdiDiagnoseType_" + tempConclusionID + "']:checked").val() == undefined) {
                                        ShowSystemDialog("请选择结论词【" + jQuery("#txtConclusionName_" + tempConclusionID).attr("ConclusionName") + "】的诊断结论类型！");
                                        SelectedConclusionDataParams = "-1";
                                        return false;
                                    }

                                    // 显示排序
                                    tmpDispOrder = jQuery("#txtConclusionOrder_" + tempConclusionID).val();
                                    // 结论词分类名称
                                    tmpConclusionTypeName = jQuery(this).attr("ConclusionTypeName");
                                    // 结论词
                                    tmpConclusionName = jQuery("#txtConclusionName_" + tempConclusionID).val();
                                    // 结论解释
                                    tmpExplanation = jQuery("#txtExplanation_" + tempConclusionID).val();
                                    // 运动指导
                                    tmpSportsGuide = jQuery("#txtSportsGuide_" + tempConclusionID).val();
                                    // 饮食指导
                                    tmpDietGuide = jQuery("#txtDietGuide_" + tempConclusionID).val();
                                    // 结论解释 
                                    tmpSuggestion = jQuery("#txtSuggestion_" + tempConclusionID).val();
                                    // 健康建议
                                    tmpHealthKnowledge = jQuery("#txtHealthKnowledge_" + tempConclusionID).val();
                                    // 诊断类型
                                    tmpDiagnoseType = jQuery("input[name='rdiDiagnoseType_" + tempConclusionID + "']:checked").val();

                                    SelectedConclusionDataParams +=
                SelectedConclusionDataTemplete.replace(/@ID_Conclusion/gi, encodeURIComponent(tempConclusionID))
                .replace(/@ID_CustConclusion/gi, encodeURIComponent(jQuery(this).attr("ID_CustConclusion")))
                .replace(/@Is_NoEntrySuggestion/gi, encodeURIComponent(Is_NoEntrySuggestion))
                .replace(/@DispOrder/gi, encodeURIComponent(tmpDispOrder))
                .replace(/@ConclusionShowName/gi, encodeURIComponent(tmpConclusionName))
                .replace(/@ConclusionTypeName/gi, encodeURIComponent(tmpConclusionTypeName))
                .replace(/@Explanation/gi, encodeURIComponent(tmpExplanation))
                .replace(/@SportsGuide/gi, encodeURIComponent(tmpSportsGuide))
                .replace(/@DietGuide/gi, encodeURIComponent(tmpDietGuide))
                .replace(/@Suggestion/gi, encodeURIComponent(tmpSuggestion))
                .replace(/@HealthKnowledge/gi, encodeURIComponent(tmpHealthKnowledge))
                .replace(/@DiagnoseType/gi, encodeURIComponent(tmpDiagnoseType))
                + "|";
                                });

                                // 返回拼接好的字符串
                                return SelectedConclusionDataParams;
                            }

                            /// <summary>
                            /// 排序
                            /// </summary>
                            function SortByOrderNumber() {
                                var OrderNumberStr = "";        // 记录需要排序的值
                                var OrderConclusionIDStr = "";  // 记录需要排序的值对应的结论词的ID

                                var tempOrderValue = "0";       // 临时记录需要排序的值

                                jQuery("input[name='chkSelectedConclusionList']").each(function() {
                                    //                if (jQuery(this).attr("checked") == "checked") {
                                    //                    return true; // continue; 如果选择了不出现在结论汇总中，则跳过这行
                                    //                }

                                    tempOrderValue = jQuery("#txtConclusionOrder_" + jQuery(this).val()).val()
                                    if (tempOrderValue == "") {
                                        tempOrderValue = "0"; //  如果没有输入，则按照默认值排序，排在最后
                                    }

                                    if (OrderNumberStr == "") {
                                        OrderNumberStr = tempOrderValue;
                                        OrderConclusionIDStr = jQuery(this).val();
                                    } else {
                                        OrderNumberStr = OrderNumberStr + "," + tempOrderValue;
                                        OrderConclusionIDStr = OrderConclusionIDStr + "," + jQuery(this).val();
                                    }
                                });

                                // 如果没有选项结论词，则直接返回 
                                // 20130711 by WTang
                                if (OrderConclusionIDStr == "") { return; }

                                // 处理只选择了一个结论词的情况，所以在末尾添加一个分割符，否则分割的时候会报错，导致数据不正确。
                                // 20130711 by WTang
                                OrderNumberStr = OrderNumberStr + ",";

                                var OrderNumberArray = OrderNumberStr.split(",");               // 排序的值 数组
                                var OrderConclusionIDArray = OrderConclusionIDStr.split(",");   // 排序的值对应的结论词的ID 数组
                                var i, j, stop, len = OrderNumberArray.length - 1;
                                for (i = 0; i < len; i = i + 1) {
                                    for (j = 0, stop = len - i; j < stop; j = j + 1) {
                                        // 20130801 by WTang 修改成按钮数字多位进行排序。而不是按照数字大小进行排序。
                                        if (OrderNumberArray[j + 1] != "" && OrderNumberArray[j] > OrderNumberArray[j + 1]) {  // 按照字符排序的方式
                                            //if (parseInt(OrderNumberArray[j]) > parseInt(OrderNumberArray[j + 1])) {               // 按照数字大小排序方式
                                            // 交换排序值
                                            var temp = OrderNumberArray[j];
                                            OrderNumberArray[j] = OrderNumberArray[j + 1];
                                            OrderNumberArray[j + 1] = temp;
                                            // 交换ID
                                            var tempID = OrderConclusionIDArray[j];
                                            OrderConclusionIDArray[j] = OrderConclusionIDArray[j + 1];
                                            OrderConclusionIDArray[j + 1] = tempID;
                                        }
                                    }
                                }


                                var showID_Conclusion = "0"; //列表加载完成后，自动显示的结论词详细信息 
                                var ConclusionSelectListTempleteContent = jQuery('#ConclusionSelectListTemplete').html();             //选择后的结论词显示模版
                                if (ConclusionSelectListTempleteContent == undefined) { return; }
                                var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
                                var currCodeIndex = 0;
                                var conclusionContent = ""; // 临时保存拼接的字符串
                                for (i = 0; i < len; i = i + 1) {

                                    CurrQueryCount++;
                                    conclusionContent += ConclusionSelectListTempleteContent
                            .replace(/@ID_Conclusion/gi, OrderConclusionIDArray[i])
                            .replace(/@ID_CustConclusion/gi, jQuery("#chkSelectedConclusionList_" + OrderConclusionIDArray[i]).attr("ID_CustConclusion"))
                            .replace(/@DispOrder/gi, jQuery("#txtConclusionOrder_" + OrderConclusionIDArray[i]).val() == "" ? "100000" : jQuery("#txtConclusionOrder_" + OrderConclusionIDArray[i]).val())
                            .replace(/@Is_Checked/gi, jQuery("#chkSelectedConclusionList_" + OrderConclusionIDArray[i]).attr("checked") == "checked" ? " checked = \"checked\" " : "")
                            .replace(/@ConclusionName/gi, jQuery("#chkSelectedConclusionList_" + OrderConclusionIDArray[i]).attr("ConclusionName"))
                            .replace(/@ConclusionTypeName/gi, jQuery("#chkSelectedConclusionList_" + OrderConclusionIDArray[i]).attr("ConclusionTypeName"))
                            .replace(/@FinalConclusionTypeName/gi, jQuery("#chkSelectedConclusionList_" + OrderConclusionIDArray[i]).attr("FinalConclusionTypeName"))
                            .replace(/@FinalConclusionSignCode/gi, jQuery("#chkSelectedConclusionList_" + OrderConclusionIDArray[i]).attr("FinalConclusionSignCode"))
                            .replace(/@FinalConclusionTypeClass/gi, GetFinalConclusionTypeClass(jQuery("#chkSelectedConclusionList_" + OrderConclusionIDArray[i]).attr("FinalConclusionSignCode")))
                            .replace(/@chkSelectedConclusionListName/gi, "chkSelectedConclusionList")
                            ;

                                    showID_Conclusion = OrderConclusionIDArray[i]; // 记录最后一次的结论词ID
                                }

                                QueryConclusionList(false);
                                if (conclusionContent != "") {
                                    jQuery("#ConclusionDataListParent").show();
                                    jQuery("#ConclusionSelectedList").html(conclusionContent);
                                    ScrollToTop('ConclusionSelectedListFrame'); // 滚动条滚动到底部
                                }

                                jQuery("#txtConclusionInputCode").val(""); // 清空输入框
                                jQuery("#txtConclusionInputCode").focus(); // 输入框获取焦点


                                jQuery("input[name='chkSelectedConclusionList']").each(function() {

                                    tempOrderValue = jQuery("#txtConclusionOrder_" + jQuery(this).val()).val()
                                    if (tempOrderValue == "100000") {
                                        jQuery("#txtConclusionOrder_" + jQuery(this).val()).val(""); //   排序还原为空值
                                    }
                                });

                                // 显示最后一个结论词的详细信息
                                if (showID_Conclusion != "0") {
                                    ShowSelectedEditArea(showID_Conclusion);
                                    ScrollToTop('ConclusionSelectedListFrame'); // 滚动条滚动到底部
                                }
                            }

                            /// <summary>
                            /// 汇总
                            /// </summary>
                            function GetFinalConclusionText() {

                                SortByOrderNumber(); // 排序

                                var tmpConclusionName = "";   // 临时保存 当前结论词名称
                                var totalConclusionName = ""; // 汇总 结论汇总 包括结论名 + 结论描述
                                var countConclusionName = 0;  // 结论汇总当前计数

                                var tmpHealthKnowledge = "";  // 临时保存 健康知识
                                var totalHealthKnowledge = ""; // 汇总 健康知识
                                var countHealthKnowledge = 0; // 健康知识当前计数


                                var tmpSportsGuide = "";    // 临时保存 运动指导
                                var totalSportsGuide = "";  // 汇总 运动指导
                                var countSportsGuide = 0;   // 运动指导当前计数


                                var tmpDietGuide = "";    // 临时保存 饮食指导
                                var totalDietGuide = "";  // 汇总     饮食指导
                                var countDietGuide = 0;   // 饮食指导当前计数

                                var tmpFinalConclusionSignCode = ""; // 总检汇总标记代码

                                var tmpSuggestion = "";    // 临时保存 主要诊断
                                var totalSuggestion = "";  // 汇总    主要诊断
                                var countSuggestion = 0;   // 主要诊断当前计数

                                var tmpSecondaryDiagnose = "";    // 临时保存 次要诊断
                                var totalSecondaryDiagnose = "";  // 汇总     次要诊断
                                var countSecondaryDiagnose = 0;   // 次要诊断当前计数

                                var tmpIndicatorDiagnose = "";    // 临时保存 指标异常
                                var totalIndicatorDiagnose = "";  // 汇总     指标异常
                                var countIndicatorDiagnose = 0;   // 指标异常当前计数

                                var tmpNormalDiagnose = "";    // 临时保存 正常诊断
                                var totalNormalDiagnose = "";  // 汇总     正常诊断
                                var countNormalDiagnose = 0;   // 正常诊断当前计数

                                var tmpOtherDiagnose = "";    // 临时保存 其它诊断
                                var totalOtherDiagnose = "";  // 汇总    其它诊断
                                var countOtherDiagnose = 0;   // 其它诊断当前计数

                                var tmpExplanation = "";   // 临时保存 结论解释

                                var preSpace = "　　";     // 前置空格(全角)

                                var ExistIDList = "";
                                jQuery("input[name='chkSelectedConclusionList']").each(function() {
                                    if (jQuery(this).attr("checked") == "checked") {
                                        return true; // continue; 如果选择了不出现在结论汇总中，则跳过这行
                                    }
                                    // 结论词
                                    tmpConclusionName = jQuery("#txtConclusionName_" + jQuery(this).val()).val();
                                    if (tmpConclusionName != "") {
                                        countConclusionName++; // 结论汇总当前计数
                                        totalConclusionName += countConclusionName + "、" + tmpConclusionName + WrapCharacter;
                                    }

                                    // 运动指导
                                    tmpSportsGuide = jQuery.trim(jQuery("#txtSportsGuide_" + jQuery(this).val()).val()).replace(/^　+|　$/gi, "");
                                    if (tmpSportsGuide != "") {
                                        tmpSportsGuide = preSpace + tmpSportsGuide; // 添加前置空格(全角)
                                        jQuery("#txtSportsGuide_" + jQuery(this).val()).val(tmpSportsGuide);
                                        countSportsGuide++;
                                        totalSportsGuide += countSportsGuide + "、" + tmpConclusionName + WrapCharacter;
                                        totalSportsGuide += tmpSportsGuide + WrapCharacter;
                                    }

                                    // 饮食指导
                                    tmpDietGuide = jQuery.trim(jQuery("#txtDietGuide_" + jQuery(this).val()).val()).replace(/^　+|　$/gi, "");
                                    if (tmpDietGuide != "") {
                                        tmpDietGuide = preSpace + tmpDietGuide; // 添加前置空格(全角)
                                        jQuery("#txtDietGuide_" + jQuery(this).val()).val(tmpDietGuide);
                                        countDietGuide++;
                                        totalDietGuide += countDietGuide + "、" + tmpConclusionName + WrapCharacter;
                                        totalDietGuide += tmpDietGuide + WrapCharacter;
                                    }

                                    // 主要诊断  
                                    tmpExplanation = jQuery.trim(jQuery("#txtExplanation_" + jQuery(this).val()).val()).replace(/^　+|　$/gi, "");
                                    // 结论解释
                                    tmpSuggestion = jQuery.trim(jQuery("#txtSuggestion_" + jQuery(this).val()).val()).replace(/^　+|　$/gi, "");

                                    //判断是否采用原来的方式进行汇总 20140123 

                                    // 总检汇总标记代码
                                    tmpFinalConclusionSignCode = jQuery("#txtFinalConclusionSignCode_" + jQuery(this).val()).val();
                                    if (tmpFinalConclusionSignCode != undefined) {
                                        if (tmpSuggestion != "" || tmpExplanation != "") {

                                            switch (tmpFinalConclusionSignCode) {

                                                case "10":    // 主要诊断
                                                    countSuggestion++;
                                                    totalSuggestion += countSuggestion + "、" + tmpConclusionName + WrapCharacter;
                                                    // 结论解释
                                                    if (tmpExplanation != "") {
                                                        tmpExplanation = preSpace + tmpExplanation; // 添加前置空格(全角)
                                                        jQuery("#txtExplanation_" + jQuery(this).val()).val(tmpExplanation);
                                                        totalSuggestion += tmpExplanation + WrapCharacter;
                                                    }
                                                    // 结论建议
                                                    if (tmpSuggestion != "") {
                                                        tmpSuggestion = preSpace + tmpSuggestion; // 添加前置空格(全角)
                                                        jQuery("#txtSuggestion_" + jQuery(this).val()).val(tmpSuggestion);
                                                        totalSuggestion += tmpSuggestion + WrapCharacter;
                                                    }
                                                    break;

                                                case "20":         // 次要诊断 
                                                    countSecondaryDiagnose++;
                                                    totalSecondaryDiagnose += countSecondaryDiagnose + "、" + tmpConclusionName + WrapCharacter;
                                                    // 结论解释
                                                    if (tmpExplanation != "") {
                                                        tmpExplanation = preSpace + tmpExplanation; // 添加前置空格(全角)
                                                        jQuery("#txtExplanation_" + jQuery(this).val()).val(tmpExplanation);
                                                        totalSecondaryDiagnose += tmpExplanation + WrapCharacter;
                                                    }
                                                    // 结论建议
                                                    if (tmpSuggestion != "") {
                                                        tmpSuggestion = preSpace + tmpSuggestion; // 添加前置空格(全角)
                                                        jQuery("#txtSuggestion_" + jQuery(this).val()).val(tmpSuggestion);
                                                        totalSecondaryDiagnose += tmpSuggestion + WrapCharacter;
                                                    }
                                                    break;

                                                case "30":         // 指标异常 
                                                    countIndicatorDiagnose++;
                                                    totalIndicatorDiagnose += countIndicatorDiagnose + "、" + tmpConclusionName + WrapCharacter;
                                                    // 结论解释
                                                    if (tmpExplanation != "") {
                                                        tmpExplanation = preSpace + tmpExplanation; // 添加前置空格(全角)
                                                        jQuery("#txtExplanation_" + jQuery(this).val()).val(tmpExplanation);
                                                        totalIndicatorDiagnose += tmpExplanation + WrapCharacter;
                                                    }
                                                    // 结论建议
                                                    if (tmpSuggestion != "") {
                                                        tmpSuggestion = preSpace + tmpSuggestion; // 添加前置空格(全角)
                                                        jQuery("#txtSuggestion_" + jQuery(this).val()).val(tmpSuggestion);
                                                        totalIndicatorDiagnose += tmpSuggestion + WrapCharacter;
                                                    }
                                                    break;

                                                case "40":         // 正常诊断 
                                                    countNormalDiagnose++;
                                                    totalNormalDiagnose += countNormalDiagnose + "、" + tmpConclusionName + WrapCharacter;
                                                    // 结论解释
                                                    if (tmpExplanation != "") {
                                                        tmpExplanation = preSpace + tmpExplanation; // 添加前置空格(全角)
                                                        jQuery("#txtExplanation_" + jQuery(this).val()).val(tmpExplanation);
                                                        totalNormalDiagnose += tmpExplanation + WrapCharacter;
                                                    }
                                                    // 结论建议
                                                    if (tmpSuggestion != "") {
                                                        tmpSuggestion = preSpace + tmpSuggestion; // 添加前置空格(全角)
                                                        jQuery("#txtSuggestion_" + jQuery(this).val()).val(tmpSuggestion);
                                                        totalNormalDiagnose += tmpSuggestion + WrapCharacter;
                                                    }
                                                    break;

                                                case "50":     // 其它诊断
                                                    countOtherDiagnose++;
                                                    totalOtherDiagnose += countOtherDiagnose + "、" + tmpConclusionName + WrapCharacter;
                                                    // 结论解释
                                                    if (tmpExplanation != "") {
                                                        tmpExplanation = preSpace + tmpExplanation; // 添加前置空格(全角)
                                                        jQuery("#txtExplanation_" + jQuery(this).val()).val(tmpExplanation);
                                                        totalOtherDiagnose += tmpExplanation + WrapCharacter;
                                                    }
                                                    // 结论建议
                                                    if (tmpSuggestion != "") {
                                                        tmpSuggestion = preSpace + tmpSuggestion; // 添加前置空格(全角)
                                                        jQuery("#txtSuggestion_" + jQuery(this).val()).val(tmpSuggestion);
                                                        totalOtherDiagnose += tmpSuggestion + WrapCharacter;
                                                    }
                                                    break;

                                            }
                                        }
                                    } else {
                                        if (tmpSuggestion != "" || tmpExplanation != "") {
                                            countSuggestion++;
                                            totalSuggestion += countSuggestion + "、" + tmpConclusionName + WrapCharacter;
                                            // 疾病解释
                                            if (tmpExplanation != "") {
                                                tmpExplanation = preSpace + tmpExplanation; // 添加前置空格(全角)
                                                jQuery("#txtExplanation_" + jQuery(this).val()).val(tmpExplanation);
                                                totalSuggestion += tmpExplanation + WrapCharacter;
                                            }
                                            // 总检建议
                                            if (tmpSuggestion != "") {
                                                tmpSuggestion = preSpace + tmpSuggestion; // 添加前置空格(全角)
                                                jQuery("#txtSuggestion_" + jQuery(this).val()).val(tmpSuggestion);
                                                totalSuggestion += tmpSuggestion + WrapCharacter;
                                            }
                                        }
                                    }

                                    // 健康建议
                                    tmpHealthKnowledge = jQuery.trim(jQuery("#txtHealthKnowledge_" + jQuery(this).val()).val()).replace(/^　+|　$/gi, "");
                                    if (tmpHealthKnowledge != "") {
                                        tmpHealthKnowledge = preSpace + tmpHealthKnowledge; // 添加前置空格(全角)
                                        jQuery("#txtHealthKnowledge_" + jQuery(this).val()).val(tmpHealthKnowledge);
                                        countHealthKnowledge++;
                                        totalHealthKnowledge += countHealthKnowledge + "、" + tmpConclusionName + WrapCharacter;
                                        totalHealthKnowledge += tmpHealthKnowledge + WrapCharacter;
                                    }

                                });
                                jQuery("#txtConclusionFinalConclusion").val(totalConclusionName);       // 结论汇总
                                jQuery("#txtConclusionMainDiagnose").val(totalSuggestion);              // 主要诊断
                                jQuery("#txtConclusionSecondaryDiagnose").val(totalSecondaryDiagnose);  // 次要诊断
                                jQuery("#txtConclusionIndicatorDiagnose").val(totalIndicatorDiagnose);  // 指标异常
                                jQuery("#txtConclusionNormalDiagnose").val(totalNormalDiagnose);        // 正常诊断
                                jQuery("#txtConclusionOtherDiagnose").val(totalOtherDiagnose);          // 其它诊断
                                jQuery("#txtConclusionFinalSportsGuide").val(totalSportsGuide);         // 运动指导
                                jQuery("#txtConclusionFinalDietGuide").val(totalDietGuide);             // 饮食指导
                                jQuery("#txtConclusionFinalHealthKnowlage").val(totalHealthKnowledge);  // 健康建议

                                var ConclusionFinalOverViewContent = ""; // 综述
                                var CountFinalOverView = 0; // 综述计数编号
                                jQuery(gCustExamSectionItems.dataList0).each(function(j, examsectionitem) {
                                    if (examsectionitem.SectionSummary == "") { return true; }  // 如果为空，则继续下一条
                                    CountFinalOverView++;
                                    ConclusionFinalOverViewContent += CountFinalOverView + "、" + examsectionitem.SectionName + htmlWrapCharacter + examsectionitem.SectionSummary + htmlWrapCharacter;
                                });

                                // textarea 区域中，显示换行
                                jQuery("#txtConclusionFinalOverView").val(ConclusionFinalOverViewContent.replace(/<br\/>/gi, "\n")); // 综述


                                jQuery("#htmlConclusionFinalOverView").html(htmlEncode(ConclusionFinalOverViewContent)); // 综述（html）

                                jQuery("#htmlConclusionFinalConclusion").html(htmlEncode(totalConclusionName.replace(/\n/gi, "<br\/>")));           // 结论汇总（html）
                                jQuery("#htmlConclusionMainDiagnose").html(htmlEncode(totalSuggestion.replace(/\n/gi, "<br\/>")));                  // 主要诊断（html）
                                jQuery("#htmlConclusionSecondaryDiagnose").html(htmlEncode(totalSecondaryDiagnose.replace(/\n/gi, "<br\/>")));      // 次要诊断（html）
                                jQuery("#htmlConclusionIndicatorDiagnose").html(htmlEncode(totalIndicatorDiagnose.replace(/\n/gi, "<br\/>")));      // 指标异常（html）
                                jQuery("#htmlConclusionNormalDiagnose").html(htmlEncode(totalNormalDiagnose.replace(/\n/gi, "<br\/>")));            // 正常诊断（html）
                                jQuery("#htmlConclusionOtherDiagnose").html(htmlEncode(totalOtherDiagnose.replace(/\n/gi, "<br\/>")));              // 其它诊断（html）
                                jQuery("#htmlConclusionFinalSportsGuide").html(htmlEncode(totalSportsGuide.replace(/\n/gi, "<br\/>")));         // 运动指导（html）
                                jQuery("#htmlConclusionFinalDietGuide").html(htmlEncode(totalDietGuide.replace(/\n/gi, "<br\/>")));             // 饮食指导（html）
                                jQuery("#htmlConclusionFinalHealthKnowlage").html(htmlEncode(totalHealthKnowledge.replace(/\n/gi, "<br\/>")));  // 健康建议（html）

                                //    jQuery("#htmlConclusionFinalOverView").append('<br/> <div style="width:780px; height:1px; border:0px solid red; "></div>');
                                //    jQuery("#htmlConclusionFinalConclusion").append('<br/> <div style="width:780px; height:1px; border:0px solid red; "></div>');
                                //    jQuery("#htmlConclusionMainDiagnose").append('<br/> <div style="width:780px; height:1px; border:0px solid red; "></div>');
                                //    jQuery("#htmlConclusionIndicatorDiagnose").append('<br/> <div style="width:780px; height:1px; border:0px solid red; "></div>');
                                //    jQuery("#htmlConclusionOtherDiagnose").append('<br/> <div style="width:780px; height:1px; border:0px solid red; "></div>');
                                //    jQuery("#htmlConclusionFinalSportsGuide").append('<br/> <div style="width:780px; height:1px; border:0px solid red; "></div>');
                                //    jQuery("#htmlConclusionFinalDietGuide").append('<br/> <div style="width:780px; height:1px; border:0px solid red; "></div>');
                                //    jQuery("#htmlConclusionFinalHealthKnowlage").append('<br/> <div style="width:780px; height:1px; border:0px solid red; "></div>');

                            }

                            // ===============  按钮函数区域 End   ========================================================


                            // ===============  文本域切换区域 Start   ========================================================

                            /// <summary>
                            /// 显示隐藏结果文本域
                            /// </summary>
                            function ShowHideFinalTextArea(index) {

                                jQuery("#trConclusionFinalOverView").hide();        // 综述
                                jQuery("#trConclusionFinalConclusion").hide();      // 结论汇总
                                jQuery("#trConclusionResultCompare").hide();        // 结果对比
                                jQuery("#trConclusionMainDiagnose").hide();         // 主要诊断
                                jQuery("#trConclusionSecondaryDiagnose").hide();    // 次要诊断
                                jQuery("#trConclusionNormalDiagnose").hide();       // 正常诊断
                                jQuery("#trConclusionFinalDietGuide").hide();       // 饮食建议
                                jQuery("#trConclusionFinalSportGuide").hide();      // 运动建议
                                jQuery("#trConclusionFinalHealthKnowlage").hide();  // 健康建议


                                jQuery("#liConclusionFinalOverView").attr("Class", "");        // 综述
                                jQuery("#liConclusionFinalConclusion").attr("Class", "");      // 结论汇总
                                jQuery("#liConclusionResultCompare").attr("Class", "");        // 结果对比
                                jQuery("#liConclusionMainDiagnose").attr("Class", "");         // 主要诊断
                                jQuery("#liConclusionSecondaryDiagnose").attr("Class", "");    // 次要诊断
                                jQuery("#liConclusionNormalDiagnose").attr("Class", "");       // 正常诊断
                                jQuery("#liConclusionFinalDietGuide").attr("Class", "");       // 饮食建议
                                jQuery("#liConclusionFinalSportGuide").attr("Class", "");      // 运动建议
                                jQuery("#liConclusionFinalHealthKnowlage").attr("Class", "");  // 健康建议

                                if (jQuery("#liConclusionIndicatorDiagnose") != undefined) {
                                    jQuery("#trConclusionIndicatorDiagnose").hide();             // 指标异常
                                    jQuery("#liConclusionIndicatorDiagnose").attr("Class", "");  // 指标异常
                                }

                                if (jQuery("#liConclusionOtherDiagnose") != undefined) {
                                    jQuery("#trConclusionOtherDiagnose").hide();             // 其它诊断
                                    jQuery("#liConclusionOtherDiagnose").attr("Class", "");  // 其它诊断
                                }

                                switch (index) {
                                    case 1:
                                        jQuery("#trConclusionFinalOverView").show();                            // 综述
                                        jQuery("#liConclusionFinalOverView").attr("Class", "yl-zjxx-left-a");         // 综述
                                        break;
                                    case 2:
                                        jQuery("#trConclusionFinalConclusion").show();                          // 结论汇总
                                        jQuery("#liConclusionFinalConclusion").attr("Class", "yl-zjxx-left-a");       // 结论汇总
                                        break;
                                    case 3:
                                        jQuery("#trConclusionResultCompare").show();                            // 结果对比
                                        jQuery("#liConclusionResultCompare").attr("Class", "yl-zjxx-left-a");         // 结果对比
                                        break;
                                    case 4:
                                        jQuery("#trConclusionMainDiagnose").show();                         // 主要诊断
                                        jQuery("#liConclusionMainDiagnose").attr("Class", "yl-zjxx-left-a");      // 主要诊断
                                        break;
                                    case 5:
                                        jQuery("#trConclusionFinalDietGuide").show();                           // 饮食建议
                                        jQuery("#liConclusionFinalDietGuide").attr("Class", "yl-zjxx-left-a");  // 饮食建议
                                        break;
                                    case 6:
                                        jQuery("#trConclusionFinalSportGuide").show();                          // 运动建议
                                        jQuery("#liConclusionFinalSportGuide").attr("Class", "yl-zjxx-left-a"); // 运动建议
                                        break;
                                    case 7:
                                        jQuery("#trConclusionFinalHealthKnowlage").show();                      // 指标异常
                                        jQuery("#liConclusionFinalHealthKnowlage").attr("Class", "yl-zjxx-left-a");   // 指标异常
                                        break;
                                    case 8:
                                        jQuery("#trConclusionIndicatorDiagnose").show();                      // 其它诊断
                                        jQuery("#liConclusionIndicatorDiagnose").attr("Class", "yl-zjxx-left-a");   // 其它诊断
                                        break;
                                    case 9:
                                        jQuery("#trConclusionOtherDiagnose").show();                      // 其它诊断
                                        jQuery("#liConclusionOtherDiagnose").attr("Class", "yl-zjxx-left-a");   // 其它诊断
                                        break;
                                    case 10:
                                        jQuery("#trConclusionSecondaryDiagnose").show();                         // 次要诊断
                                        jQuery("#liConclusionSecondaryDiagnose").attr("Class", "yl-zjxx-left-a");      // 次要诊断
                                        break;
                                    case 11:
                                        jQuery("#trConclusionNormalDiagnose").show();                         // 正常诊断
                                        jQuery("#liConclusionNormalDiagnose").attr("Class", "yl-zjxx-left-a");      // 正常诊断
                                        break;
                                }

                            }

                            // ===============  文本域切换区域 End   ========================================================

                            /// <summary>
                            /// 根据输入码查询结论词（通过js过滤）
                            /// </summary>
                            function GetConclusionByKeyWords() {

                                var InputCode = jQuery('#txtConclusionInputCode').val();
                                if (OldInputCode != InputCode) {
                                    OldInputCode = InputCode;
                                } else {
                                    return;
                                }
                                if (gAllConclusionDataList == "") {
                                    jQuery.ajax({
                                        type: "POST",
                                        url: "/Ajax/AjaxConclusion.aspx",
                                        data: { InputCode: "", // 首先传入空输入码，先取得全部的结论词列表
                                            action: 'GetConclusionByKeyWords',
                                            currenttime: encodeURIComponent(new Date())
                                        },
                                        cache: false,
                                        dataType: "json",
                                        success: function(jsonmsg) {
                                            gAllConclusionDataList = jsonmsg;
                                            // 显示查询到的结论词
                                            ShowConclusionDataList(jsonmsg, InputCode);
                                        }
                                    });
                                } else {

                                    // 显示查询到的结论词
                                    ShowConclusionDataList(gAllConclusionDataList, InputCode);
                                }
                            }


                            /// <summary>
                            /// 根据查询结果数据，显示结论词（通过js过滤）
                            /// </summary>
                            function ShowConclusionDataList(conclusionlist, InputCode) {

                                var conclusionContent = ""; //结论词table内容

                                var ConclusionQueryListTempleteContent = jQuery('#ConclusionQueryListTemplete').html();             //结论词模版
                                if (ConclusionQueryListTempleteContent == undefined) { return; }
                                var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
                                var judgeflag = 0;
                                if (InputCode != "") {
                                    InputCode = InputCode.toUpperCase();
                                    judgeflag = 1;
                                }

                                if (judgeflag == 0 && gAllConclusionListContent != "") {
                                    jQuery("#QueryConclusionDataTableList").html(gAllConclusionListContent);
                                }
                                else {
                                    var currCodeIndex = 0;
                                    jQuery(conclusionlist.dataList).each(function(j, conclusionitem) {
                                        // 如果字符串不包含这个输入码，则继续下一条数据的判断
                                        currCodeIndex = conclusionitem.InputCode.search(InputCode);
                                        if (judgeflag == 1 && (currCodeIndex < 0 || currCodeIndex > 0)) {
                                            return true;
                                        }
                                        CurrQueryCount++;
                                        conclusionContent += ConclusionQueryListTempleteContent
                            .replace(/@ID_Conclusion/gi, conclusionitem.ID_Conclusion)
                            .replace(/@ConclusionName/gi, conclusionitem.ConclusionName)
                            .replace(/@ConclusionTypeName/gi, conclusionitem.TeamConclusionName)
                            .replace(/@FinalConclusionTypeName/gi, conclusionitem.FinalConclusionTypeName)
                            .replace(/@FinalConclusionSignCode/gi, conclusionitem.FinalConclusionSignCode)
                            .replace(/@FinalConclusionTypeClass/gi, GetFinalConclusionTypeClass(conclusionitem.FinalConclusionSignCode))
                            ;
                                    });

                                    if (judgeflag == 0 && gAllConclusionListContent == "") {
                                        gAllConclusionListContent = conclusionContent;
                                    }
                                    if (parseInt(CurrQueryCount) > 0) {
                                        jQuery("#QueryConclusionDataTableList").html(conclusionContent);
                                    } else {
                                        jQuery("#QueryConclusionDataTableList").html("");
                                    }
                                }
                            }

                            /// <summary>
                            /// 根据指标分类，获取对应的样式(以前的方法，暂时不再使用，20140420 by wtang )
                            /// </summary>
                            function GetFinalConclusionTypeClass_Old(FCSCode) {
                                var tempClassName = "";
                                switch (FCSCode) {
                                    case "MainDiagnose":    // 主要诊断
                                        tempClassName = "zjxz-jlc-nr-left-table-nr-lx1";
                                        break;
                                    case "FinalUnsual":         // 指标异常 
                                        tempClassName = "zjxz-jlc-nr-left-table-nr-lx2";
                                        break;
                                    case "OtherUnsual":         // 其它诊断
                                        tempClassName = "zjxz-jlc-nr-left-table-nr-lx3";
                                        break;
                                    default:    // 未分类
                                        tempClassName = "zjxz-jlc-nr-left-table-nr-lx";
                                        break;

                                }
                                return tempClassName;
                            }
                            /// <summary>
                            /// 根据指标分类，获取对应的样式
                            /// </summary>
                            function GetFinalConclusionTypeClass(FCSCode) {
                                var tempClassName = "";
                                switch (FCSCode) {
                                    case "10":         // 主要诊断
                                        tempClassName = "zjxz-jlc-nr-left-table-nr-lx1";
                                        break;
                                    case "20":         // 次要诊断 
                                        tempClassName = "zjxz-jlc-nr-left-table-nr-lx2";
                                        break;
                                    case "30":         // 指标异常
                                        tempClassName = "zjxz-jlc-nr-left-table-nr-lx3";
                                        break;
                                    case "40":         // 正常
                                        tempClassName = "zjxz-jlc-nr-left-table-nr-lx4";
                                        break;
                                    case "50":         // 其它
                                        tempClassName = "zjxz-jlc-nr-left-table-nr-lx5";
                                        break;
                                    default:           // 未分类
                                        tempClassName = "zjxz-jlc-nr-left-table-nr-lx";
                                        break;

                                }
                                return tempClassName;
                            }

                            // ===============  通过js的方式在前台进行数据的过滤 End ===========================


                            /// <summary>
                            /// 根据查询结果数据，显示结论词
                            /// </summary>
                            function QueryConclusionList(flag) {

                                var InputCode = jQuery('#txtConclusionInputCode').val();

                                if (InputCode != "" && flag == true) {
                                    jQuery("#ConclusionQueryArea").attr("Class", "showQueryConclusionClass");
                                } else {
                                    jQuery("#ConclusionQueryArea").attr("Class", "hideQueryConclusionClass");
                                }
                            }


                            // -------------------- 查询按钮 及自动查询函数 (总检) Start ----------------------

                            /// <summary>
                            /// 根据输入情况，自动获取客户的个人信息及体检项目 （当输入数据达到体检号的数据长度时，自动读取）
                            /// </summary>
                            function AutoGetCustomerSectionExamInfo(flag) {

                                var curEvent = window.event || e;
                                if (curEvent.keyCode == 13) {
                                    var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
                                    if (isCustomerExamNo(customerid)) {
                                        var ExamUrl = "/System/Conclusion/ConclusionOper.aspx?txtCustomerID=" + customerid;
                                        if (flag == "NewPage") {
                                            ExamUrl = "/System/ConclusionEx/ConclusionOper.aspx?txtCustomerID=" + customerid;
                                        }
                                        DoLoad(ExamUrl, '');
                                    }
                                }
                            }

                            /// <summary>
                            /// 根据体检号，查询客户的个人信息及体检项目
                            /// </summary>
                            function GetCustomerSectionExamInfo(flag) {

                                jQuery('#IDCardNoText').html("&nbsp;");
                                jQuery('#CustomerNameText').html("&nbsp;");
                                jQuery('#GenderNameText').html("&nbsp;");
                                jQuery('#MarriageNameText').html("&nbsp;");
                                jQuery('#MobileNoText').html("&nbsp;");

                                jQuery("#FinalConclusionDataHeadInfo").hide();    // 隐藏表头信息
                                jQuery("#FinalConclusionDataBottomInfo").hide();  // 隐藏下面输入及操作信息

                                //显示等待信息
                                var ExamItemWaitingTempleteContent = jQuery('#ExamItemWaitingTemplete').html(); //读取数据等待信息模版
                                if (ExamItemWaitingTempleteContent == undefined) { return; }
                                jQuery('#TipsMessage').html(ExamItemWaitingTempleteContent);                   //显示等待信息

                                var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
                                var TipsMessageTempleteContent = jQuery('#TipsMessageTemplete').html();     //读取提示信息模版
                                if (TipsMessageTempleteContent == undefined) { return; }
                                if (customerid == "") {
                                    jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "请输入客户的体检号！"));   //显示空信息
                                    return;
                                }
                                if (!isCustomerExamNo(customerid)) {

                                    jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "体检号格式错误，请检核对后重新输入！"));   //显示空信息
                                    return;
                                }
                                var ExamUrl = "/System/Conclusion/ConclusionOper.aspx?txtCustomerID=" + customerid;
                                if (flag == "NewPage") {
                                    ExamUrl = "/System/ConclusionEx/ConclusionOper.aspx?txtCustomerID=" + customerid;
                                }
                                DoLoad(ExamUrl, '');
                            }

                            // -------------------- 查询按钮 及自动查询函数 (总检) End ----------------------



                            // -------------------- 查询按钮 及自动查询函数 (总审) Start ----------------------



                            /// <summary>
                            /// 隐藏总审内容区的内容
                            /// </summary>
                            function HideAllConclusionCheckInfo() {

                                jQuery("#btnCustomerSimpleInfo").hide();  // 客户基本信息
                                jQuery("#divCustomerBaseInfo").hide();    // 客户信息
                                jQuery("#fkls").hide();    // Tab 切换
                                jQuery("#fklscen").hide();    // 中间数据部分
                                jQuery("#ButReprotPreview").hide();         // 报告预览
                                jQuery("#ButUnLockFinalCheck1").hide();     // 解除总检

                                jQuery('#TipsArea').show(); // 显示页面内部的提示信息 20140410 by wtang 

                            }

                            /// <summary>
                            /// 显示总审内容区的内容
                            /// </summary>
                            function ShowAllConclusionCheckInfo() {

                                jQuery('#TipsArea').hide(); // 隐藏页面内部的提示信息 20140410 by wtang 

                                jQuery("#btnCustomerSimpleInfo").show();  // 客户基本信息
                                jQuery("#divCustomerBaseInfo").hide();    // 客户信息(刚加载时，内容部分默认为隐藏)
                                jQuery("#fkls").show();    // Tab 切换
                                jQuery("#fklscen").show();    // 中间数据部分
                                jQuery("#ButReprotPreview").show();         // 报告预览
                                jQuery("#ButUnLockFinalCheck1").show();     // 解除总检

                            }


                            /// <summary>
                            /// 根据输入情况，自动获取客户的个人信息及体检项目 （当输入数据达到体检号的数据长度时，自动读取） （总审）
                            /// </summary>
                            function AutoGetCustomerConclusionCheckInfo(flag) {

                                var curEvent = window.event || e;
                                if (curEvent.keyCode == 13) {
                                    var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
                                    if (isCustomerExamNo(customerid)) {
                                        var ExamUrl = "/System/Conclusion/ConclusionCheck.aspx?txtCustomerID=" + customerid;
                                        if (flag == "NewPage") {
                                            ExamUrl = "/System/ConclusionEx/ConclusionCheck.aspx?txtCustomerID=" + customerid;
                                        }
                                        DoLoad(ExamUrl, '');
                                    }
                                }
                            }

                            /// <summary>
                            /// 根据体检号，查询客户的个人信息及体检项目（总审）
                            /// </summary>
                            function GetCustomerConclusionCheckInfo(flag) {
                                jQuery('#IDCardNoText').html("&nbsp;");
                                jQuery('#CustomerNameText').html("&nbsp;");
                                jQuery('#GenderNameText').html("&nbsp;");
                                jQuery('#MarriageNameText').html("&nbsp;");
                                jQuery('#MobileNoText').html("&nbsp;");

                                HideAllConclusionCheckInfo(); // 隐藏总审其它信息

                                jQuery("#FinalConclusionDataHeadInfo").hide();    // 隐藏表头信息
                                jQuery("#FinalConclusionDataBottomInfo").hide();  // 隐藏下面输入及操作信息

                                //显示等待信息
                                var ExamItemWaitingTempleteContent = jQuery('#ExamItemWaitingTemplete').html(); //读取数据等待信息模版
                                if (ExamItemWaitingTempleteContent == undefined) { return; }
                                jQuery('#TipsMessage').html(ExamItemWaitingTempleteContent);                   //显示等待信息

                                var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
                                var TipsMessageTempleteContent = jQuery('#TipsMessageTemplete').html();     //读取提示信息模版
                                if (TipsMessageTempleteContent == undefined) { return; }
                                if (customerid == "") {
                                    jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "请输入客户的体检号！"));   //显示空信息
                                    jQuery('#TipsArea').show(); // 显示页面内部的提示信息 20140410 by wtang 
                                    return;
                                }
                                if (!isCustomerExamNo(customerid)) {

                                    jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "体检号格式错误，请检核对后重新输入！"));   //显示空信息
                                    jQuery('#TipsArea').show(); // 显示页面内部的提示信息 20140410 by wtang 
                                    return;
                                }

                                var ExamUrl = "/System/Conclusion/ConclusionCheck.aspx?txtCustomerID=" + customerid;
                                if (flag == "NewPage") {
                                    ExamUrl = "/System/ConclusionEx/ConclusionCheck.aspx?txtCustomerID=" + customerid;
                                }
                                DoLoad(ExamUrl, '');
                            }


                            /// <summary>
                            /// 保存总审结果（总审）
                            /// </summary>
                            function SaveCustomerFinalCheck(IsChecked) {

                                if (IsChecked == 0) {
                                    jQuery('#Is_Checked').val("False");               //审核不通过
                                }
                                if (IsChecked == 1) {
                                    jQuery('#Is_Checked').val("True");                //审核通过
                                }
                                var CustomerID = jQuery.trim(jQuery('#HiddenCustomerID').val());            //客户体检号
                                var CustomerNameText = jQuery('#CustomerNameText').val();   //客户姓名
                                var RefuseReason = "";        //拒绝理由

                                var ID_FinalDoctor = jQuery('#ID_FinalDoctor').val();       //总检医生ID
                                var FinalDoctor = jQuery('#FinalDoctor').val();             //总检医生
                                var FinalDateDetail = jQuery('#FinalDateDetail').val();     //总检提交时间

                                if (IsChecked == 0) {
                                    RefuseReason = jQuery('#txtRefuseReason').val();        //拒绝理由
                                    if (RefuseReason == "") {
                                        ShowSystemDialog("请填写拒绝理由！");
                                        return false;
                                    }
                                }

                                jQuery.ajax({
                                    type: "POST",
                                    url: "/Ajax/AjaxConclusion.aspx",
                                    data: { CustomerID: CustomerID,
                                        CustomerName: encodeURIComponent(CustomerNameText),
                                        IsChecked: IsChecked,
                                        ID_FinalDoctor: ID_FinalDoctor,
                                        FinalDoctor: encodeURIComponent(FinalDoctor),
                                        FinalDateDetail: encodeURIComponent(FinalDateDetail),
                                        RefuseReason: encodeURIComponent(RefuseReason),
                                        action: 'SaveCustomerFinalCheck',
                                        currenttime: encodeURIComponent(new Date())
                                    },
                                    cache: false,
                                    dataType: "json",
                                    success: function(jsonmsg) {
                                        if (jsonmsg != null && jsonmsg != "") {

                                            if (parseInt(jsonmsg) > 0) {

                                                CtrlFinalCheckButtonDisabled(0, false); // 屏蔽总审页面上的按钮
                                                ShowSystemDialog("保存审核信息成功!");
                                            }
                                            else { ShowSystemDialog("保存审核信息失败，请与技术人员联系!") }
                                        }
                                    }
                                });

                            }


                            ///// <summary>
                            ///// 锁定 / 解除锁定 （总审）
                            ///// </summary>
                            //function LockCustomerFinalCheck(IsConclusionLock) {

                            //    if (IsConclusionLock == 0)
                            //        jQuery('#Is_ConclusionLock').val("False");                 //总检锁定
                            //    
                            //    var CustomerID = jQuery.trim(jQuery('#HiddenCustomerID').val());
                            //    jQuery.ajax({
                            //        type: "POST",
                            //        url: "/Ajax/AjaxConclusion.aspx",
                            //        data: { CustomerID: CustomerID,
                            //            IsConclusionLock: IsConclusionLock,
                            //            action: 'LockCustomerFinalCheck',
                            //            currenttime: encodeURIComponent(new Date())
                            //        },
                            //        cache: false,
                            //        dataType: "json",
                            //        success: function (jsonmsg) {
                            //            var tmpstr = "";

                            //            if (jQuery('#Is_ConclusionLock').val() == "False") { temstr = "解除"; }

                            //            if (jsonmsg != null && jsonmsg != "") {
                            //                if (parseInt(jsonmsg) > 0) {
                            //                    ShowSystemDialog(temstr + "总检锁定成功!");
                            //                    if (jQuery('#Is_ConclusionLock').val() == "False") {
                            //                        jQuery("#ButUnLock").attr("disabled", "disabled");
                            //                        jQuery("#ButChecked").attr("disabled", "disabled");
                            //                        jQuery("#ButUnCheck").attr("disabled", "disabled");
                            //                    } else {
                            //                        jQuery("#ButUnLock").removeAttr("disabled");
                            //                    }
                            //                }
                            //                else { ShowSystemDialog(temstr + "总检锁定失败，请与技术人员联系!") }
                            //            }
                            //        }
                            //    });

                            //}

                            // -------------------- 查询按钮 及自动查询函数 (总审) End ----------------------



                            // ================================ 结论词分类快速选择函数区域 ==== start ==================================================

                            /// <summary>
                            /// 隐藏，显示快速查询结论词分类列表
                            /// </summary>
                            function ShowHideQuickQueryConclusionTypeTable(flag, InputCode) {
                                if (flag == true) {
                                    jQuery("#QuickQueryConclusionTypeTable").show();
                                    ShowHideQuickQuerySectionTable(false);
                                } else {
                                    jQuery("#QuickQueryConclusionTypeTable").hide();
                                }
                            }

                            var gAllConclusionTypeDataList = "";    // 保存全部的结论词分类列表，前台输入输入码后，在这个列表中进行过滤，然后显示即可。
                            var OldConclusionTypeInputCode = "****";              // 记录上次输入的输入码
                            var gAllConclusionTypeListContent = ""; // 保存查询条件为空时，显示的信息，避免每次去执行替换。
                            /// <summary>
                            /// 根据输入码查询结论词分类（通过Ajax后台过滤）
                            /// </summary>
                            function QuickQueryConclusionTypeTableData_Ajax() {

                                var InputCode = jQuery('#txtConclusionTypeInputCode').val();
                                if (OldConclusionTypeInputCode != InputCode) {
                                    OldConclusionTypeInputCode = InputCode;
                                } else {
                                    ShowHideQuickQueryConclusionTypeTable(true, InputCode);
                                    return;
                                }

                                jQuery.ajax({
                                    type: "POST",
                                    url: "/Ajax/AjaxConfig.aspx",
                                    data: { InputCode: InputCode,
                                        action: 'GetQuickConclusionTypeList',
                                        currenttime: encodeURIComponent(new Date())
                                    },
                                    cache: false,
                                    dataType: "json",
                                    success: function(jsonmsg) {
                                        // 显示查询到的结论词分类
                                        ShowQuickQueryConclusionTypeTableData_Ajax(jsonmsg, InputCode);
                                    }
                                });

                            }


                            /// <summary>
                            /// 根据查询结果数据，显示结论词分类（通过Ajax过滤）
                            /// </summary>
                            function ShowQuickQueryConclusionTypeTableData_Ajax(ConclusionTypelist) {
                                if (ConclusionTypelist == "" || ConclusionTypelist.totalCount == 0) {

                                    ShowHideQuickQueryConclusionTypeTable(true, jQuery('#txtConclusionTypeInputCode').val());
                                    // 显示没有找到结论词分类信息
                                    jQuery("#QuickQueryConclusionTypeTableData").html(jQuery('#EmptyConclusionTypeQuickQueryDataTemplete').html());
                                }
                                else {

                                    var conclusionContent = ""; //结论词分类table内容

                                    var ConclusionTypeQuickQueryTableTempleteContent = jQuery('#ConclusionTypeQuickQueryTableTemplete').html();             //快速查询结论词分类列表模版
                                    if (ConclusionTypeQuickQueryTableTempleteContent == undefined) { return; }
                                    var CurrQueryCount = 0; // 满足当前查询条件的数据条数。
                                    var currCodeIndex = 0;

                                    if (ConclusionTypelist != "") {
                                        jQuery(ConclusionTypelist.dataList).each(function(j, ConclusionTypeitem) {
                                            // 如果字符串不包含这个输入码，则继续下一条数据的判断

                                            CurrQueryCount++;
                                            conclusionContent += ConclusionTypeQuickQueryTableTempleteContent
                            .replace(/@ID_ConclusionType/gi, ConclusionTypeitem.ID_ConclusionType)
                            .replace(/@ConclusionTypeName/gi, ConclusionTypeitem.ConclusionTypeName)
                            .replace(/@InputCode/gi, ConclusionTypeitem.InputCode)
                            .replace(/@chkConclusionTypeQueryList/gi, "chkConclusionTypeQueryList")
                            ;
                                        });
                                    }
                                    if (conclusionContent != "") {
                                        ShowHideQuickQueryConclusionTypeTable(true, jQuery('#txtConclusionTypeInputCode').val());
                                        jQuery("#QuickQueryConclusionTypeTableData").html(conclusionContent);
                                    } else {
                                        ShowHideQuickQueryConclusionTypeTable(false);
                                        jQuery("#QuickQueryConclusionTypeTableData").html("");
                                    }
                                }

                            }


                            /// <summary>
                            /// 点击确定按钮（确定选择结论词分类）
                            /// </summary>
                            function SelectConclusionTypeDataList() {
                                var selectedItemContent = "";
                                var UserSelectedConclusionTypeItemDataTempleteContent = jQuery('#UserSelectedConclusionTypeItemDataTemplete').html();             //用户已经选择的结论词分类列表模版
                                if (UserSelectedConclusionTypeItemDataTempleteContent == undefined) { return; }

                                jQuery("input[name='chkConclusionTypeQueryList']:radio:checked").each(function() {
                                    if (jQuery("#chkUserConclusionType_" + jQuery(this).val()).val() == undefined) {
                                        selectedItemContent = UserSelectedConclusionTypeItemDataTempleteContent
                            .replace(/@ID_ConclusionType/gi, jQuery(this).val())
                            .replace(/@ConclusionTypeName/gi, jQuery(this).attr("ConclusionTypename"))
                            ;
                                        jQuery("#spanSelectConclusionType").html(selectedItemContent);
                                        jQuery("#spanSelectConclusionType").show();
                                        jQuery("#idSelectConclusionType").val(jQuery(this).val());
                                        jQuery("#nameSelectConclusionType").val(jQuery(this).attr("ConclusionTypename"));
                                        jQuery("#spanConclusionType").hide();
                                    }
                                });

                                ShowHideQuickQueryConclusionTypeTable(false);
                            }

                            /// <summary>
                            /// 删除选择的结论词分类
                            /// </summary>
                            function RemoveSelectedConclusionType() {
                                jQuery('#spanSelectConclusionType').hide();
                                jQuery('#spanConclusionType').show();
                                jQuery('#spanSelectConclusionType').html('');
                                jQuery('#idSelectConclusionType').val('');
                                jQuery('#nameSelectConclusionType').val('');
                            }


                            /// <summary> 
                            /// 点击选中对应结论词的单选按钮
                            /// </summary>
                            function SetConclusionChecked(ID_Conclusion) {
                                jQuery("#rdiConclusion_" + ID_Conclusion).attr("checked", true);
                                ShowHideQuickQueryConclusionTypeTable(false, '');
                            }


                            // ================================ 结论词分类快速选择函数区域 ==== end ==================================================




                            // ================================ 结论词管理 ==== start ==================================================

                            /// <summary>
                            /// 弹出结论词设置页面
                            /// </summary>
                            function OpenEditConclusionWindow() {
                                var ConclusionID = jQuery("input[name='ConclusionRadio']:checked").val();
                                if (ConclusionID == undefined) {
                                    ShowSystemDialog("请选择你要修改的结论词！");
                                    return;
                                }
                                OpenConclusionOperWindowParams(ConclusionID);
                            }

                            /// <summary>
                            /// 弹出新增结论词页面
                            /// </summary>
                            function OpenConclusionOperWindow() {
                                OpenConclusionOperWindowParams("");
                            }

                            /// <summary>
                            /// 弹出结论词设置页面(结论词ID编号)
                            /// </summary>
                            function OpenConclusionOperWindowParams(ConclusionID) {

                                var url = '/System/Config/Conclusion/ConclusionOper.aspx?num=' + Math.random();
                                if (ConclusionID != "") {
                                    url = url + "&ConclusionID=" + ConclusionID;
                                }
                                art.dialog.open(url,
    {
        width: 450,
        height: 600,
        drag: false,
        lock: true,
        id: 'OperWindowFrame',
        title: "结论词维护"
    });
                            }


                            /// <summary>
                            /// 保存结论词参数。 (保存)
                            /// </summary>
                            function SaveConclusionInfo() {

                                // 获取保存时提交的参数。 (参数提取)
                                var ConclusionParams = GetSaveConclusionParams();

                                var isCanSaveInfo = jQuery("#IsCanSaveInfo").val();    // 数据验证是否通过

                                if (isCanSaveInfo == "False") { return; } // 如果验证没有通过，则不能进行下面的操作

                                jQuery.ajax({
                                    type: "POST",
                                    url: "/Ajax/AjaxConclusion.aspx",
                                    data: ConclusionParams,         // 结论词参数
                                    cache: false,
                                    dataType: "json",
                                    success: function(jsonmsg) {
                                        if (parseInt(jsonmsg) > 0) {

                                            if (jQuery("#ID_Conclusion").val() != "") {
                                                parent.GetSingleConclusionInfo(jQuery("#ID_Conclusion").val(), "edit");
                                            }
                                            else {
                                                parent.GetSingleConclusionInfo(jsonmsg, "add");
                                            }

                                            parent.ShowSystemDialogCloseDialog("保存结论词成功!", jQuery("#IS_AutoCloseDialog").val());

                                            // 如果是自动关闭弹出窗口
                                            if (jQuery("#IS_AutoCloseDialog").val() != "True") {
                                                btnResetClick(); // 否则清空数据

                                                RemoveSelectedConclusionType();
                                            }

                                        }
                                        else if (parseInt(jsonmsg) == -1) {
                                            parent.ShowSystemDialogCloseDialog("结论词名称：【" + jQuery("#txtConclusionName").val() + "】已经存在，请核对后再操作！");
                                        }
                                        else { parent.ShowSystemDialogCloseDialog("保存结论词失败，请与技术人员联系!") }
                                    }
                                });

                            }

                            /// <summary>
                            /// 获取保存时提交的参数。 (参数提取)
                            /// </summary>
                            function GetSaveConclusionParams() {

                                var ID_Conclusion = jQuery("#ID_Conclusion").val();                     // 结论词ID
                                var ConclusionName = jQuery("#txtConclusionName").val();                // 结论词名称
                                ConclusionName = encodeURIComponent(ConclusionName);                    // 结论词名称 编码处理
                                // 暂时不显示结论词分类 20131011 by WTang 
                                // 恢复结论词分类的管理 20131212 by WTang
                                var ID_ConclusionType = jQuery("#idSelectConclusionType").val();        // 分类ID
                                var ConclusionTypeName = jQuery("#nameSelectConclusionType").val();     // 分类名称

                                var ID_ICD = jQuery("#idSelectICD10").val();        // ICD10

                                var TeamConclusionName = jQuery("#txtTeamConclusionName").val();                // 团检结论词名称
                                TeamConclusionName = encodeURIComponent(TeamConclusionName);                    // 团检结论词名称 编码处理

                                var DispOrder = jQuery("#txtDispOrder").val();                          // 排序编号
                                var Forsex = jQuery("input[name='radioForsex']:checked").val();   // 适用性别 0:女士 1：男士，2:男女均适用

                                var Explanation = jQuery("#txtExplanation").val();           // 结论解释
                                var Suggestion = jQuery("#txtSuggestion").val();             // 主要诊断
                                var DietGuide = jQuery("#txtDietGuide").val();               // 饮食指导
                                var SportsGuide = jQuery("#txtSportsGuide").val();           // 运动指导
                                var HealthKnowledge = jQuery("#txtHealthKnowledge").val();   // 健康知识

                                Explanation = encodeURIComponent(Explanation);              // 结论解释 编码处理
                                Suggestion = encodeURIComponent(Suggestion);                // 主要诊断 编码处理
                                DietGuide = encodeURIComponent(DietGuide);                  // 饮食指导 编码处理 
                                SportsGuide = encodeURIComponent(SportsGuide);              // 运动指导 编码处理 
                                HealthKnowledge = encodeURIComponent(HealthKnowledge);      // 健康知识 编码处理 

                                var Is_Banned = jQuery("input[name='radioIs_Banned']:checked").val();   // 是否被禁用
                                var BanDescribe = jQuery("#txtBanDescribe").val();              // 禁用说明
                                BanDescribe = encodeURIComponent(BanDescribe);                  // 禁用说明 编码处理

                                // 添加结论类型管理 20140121 by WTang
                                var ID_FinalConclusionType = jQuery("#idSelectFinalConclusionType").val();        // 结论类型 ID
                                var FinalConclusionTypeName = jQuery("#nameSelectFinalConclusionType").val();     // 结论类型 名称

                                jQuery("#IsCanSaveInfo").val("False");    // 数据验证开始，先置为不能保持的状态

                                if (ConclusionName == "") {
                                    jQuery('#txtConclusionName').focus();
                                    parent.ShowSystemDialogCloseDialog("请输入结论词名称!");
                                    return;
                                }
                                // 暂时不显示结论词分类 20131011 by WTang 
                                //    if (ID_ConclusionType == "") {
                                //        parent.ShowSystemDialogCloseDialog("请选择结论词分类!");
                                //        return;
                                //    }

                                jQuery("#IsCanSaveInfo").val("True");    // 数据验证成功，标记可以进行保存

                                var ConclusionSaveParams = {
                                    ConclusionName: ConclusionName,
                                    ID_Conclusion: ID_Conclusion,
                                    ID_ConclusionType: ID_ConclusionType,
                                    ConclusionTypeName: ConclusionTypeName,
                                    ID_FinalConclusionType: ID_FinalConclusionType,
                                    TeamConclusionName: TeamConclusionName,
                                    DispOrder: DispOrder,
                                    Forsex: Forsex,
                                    Explanation: Explanation,
                                    Suggestion: Suggestion,
                                    DietGuide: DietGuide,
                                    SportsGuide: SportsGuide,
                                    HealthKnowledge: HealthKnowledge,
                                    Is_Banned: Is_Banned,
                                    BanDescribe: BanDescribe,
                                    ID_ICD: ID_ICD,
                                    action: 'SaveConclusionInfo',
                                    currenttime: encodeURIComponent(new Date())
                                };

                                return ConclusionSaveParams; // 返回拼接后的参数

                            }


                            /// <summary>
                            /// 初始化结论词设置页面
                            /// </summary>
                            function InitConclusionEditData() {
                                if (jQuery("#ID_Conclusion").val() == "") {
                                    return;
                                }

                                jQuery("#btnSave").hide(); // 如果是编辑操作，则不显示"保存&继续"按钮
                                jQuery("#btnSaveclose").val("保存"); // 修改 "保存&关闭" 按钮 名称为 "保存"

                                var selectedItemContent = "";
                                if (jQuery("#idSelectConclusionType").val() != "") {

                                    var UserSelectedConclusionTypeItemDataTempleteContent = jQuery('#UserSelectedConclusionTypeItemDataTemplete').html();             //用户已经选择的列表模版
                                    if (UserSelectedConclusionTypeItemDataTempleteContent == undefined) { return; }

                                    selectedItemContent = UserSelectedConclusionTypeItemDataTempleteContent
                            .replace(/@ID_ConclusionType/gi, jQuery("#idSelectConclusionType").val())
                            .replace(/@ConclusionTypeName/gi, jQuery("#nameSelectConclusionType").val())
                            ;
                                    jQuery("#spanSelectConclusionType").html(selectedItemContent);
                                    jQuery("#spanSelectConclusionType").show();
                                    jQuery("#spanConclusionType").hide();
                                }

                                selectedItemContent = "";
                                if (jQuery("#idSelectICD10").val() != "") {

                                    var UserSelectedICD10ItemDataTempleteContent = jQuery('#UserSelectedICD10ItemDataTemplete').html();             //用户已经选择的列表模版
                                    if (UserSelectedICD10ItemDataTempleteContent == undefined) { return; }

                                    selectedItemContent = UserSelectedICD10ItemDataTempleteContent
                            .replace(/@ID_ICD/gi, jQuery("#idSelectICD10").val())
                            .replace(/@ICDCNName/gi, jQuery("#nameSelectICD10").val())
                            ;
                                    jQuery("#spanSelectICD10").html(selectedItemContent);
                                    jQuery("#spanSelectICD10").show();
                                    jQuery("#spanICD10").hide();
                                }

                                selectedItemContent = "";
                                if (jQuery("#idSelectFinalConclusionType").val() != "") {

                                    var UserSelectedFinalConclusionTypeItemDataTempleteContent = jQuery('#UserSelectedFinalConclusionTypeItemDataTemplete').html();             //用户已经选择的列表模版
                                    if (UserSelectedFinalConclusionTypeItemDataTempleteContent == undefined) { return; }

                                    selectedItemContent = UserSelectedFinalConclusionTypeItemDataTempleteContent
                            .replace(/@ID_FinalConclusionType/gi, jQuery("#idSelectFinalConclusionType").val())
                            .replace(/@FinalConclusionTypeName/gi, jQuery("#nameSelectFinalConclusionType").val())
                            ;
                                    jQuery("#spanSelectFinalConclusionType").html(selectedItemContent);
                                    jQuery("#spanSelectFinalConclusionType").show();
                                    jQuery("#spanFinalConclusionType").hide();
                                }

                                // 性别
                                jQuery("input[name='radioForsex']").each(function() {
                                    if (jQuery('#txtForsex').val() == jQuery(this).val()) {
                                        jQuery(this).attr("checked", true);
                                    }
                                });

                                jQuery("input[name='radioIs_Banned']").each(function() {

                                    var tmpIsBanned = jQuery('#txtIs_Banned').val() == "True" ? 1 : 0;
                                    if (tmpIsBanned == jQuery(this).val()) {
                                        jQuery(this).attr("checked", true);
                                        jQuery(this).change();
                                    }
                                });

                            }



                            // ================================ 结论词管理 ==== end ==================================================




                            // ================================ 解除总审 ==== start ==================================================
                            /// <summary>
                            /// 根据输入情况，自动获取客户的个人信息及同时进行总审的解除 （当输入数据达到体检号的数据长度时，自动读取）
                            /// </summary>
                            function AutoUnLockFinalCheck() {

                                var curEvent = window.event || e;
                                if (curEvent.keyCode == 13) {
                                    // 查询客户的基本信息，并显示
                                    UnLockFinalCheck();
                                }
                            }

                            /// <summary>
                            /// 获取个人信息及同时进行总审的解除 
                            ///  1：解除总审成功 
                            ///  0:表示解除失败， 
                            /// -1:表示体检号不存在，
                            /// -2:表示还未完成总审，不需要进行解除，
                            /// -3:体检报告已经领取，不能进行解除总审操作 
                            /// </summary>
                            function UnLockFinalCheck() {

                                jQuery("#divCustomerInfoArea").hide();  // 隐藏客户基本资料区域
                                jQuery("#DivUnCheckedTipsInfo").hide(); // 隐藏解除审核结果信息区域

                                jQuery("#Is_UncheckORUnFinish").val("UnCheck"); // 标记当前操作的是解除总检

                                var CustomerID = jQuery.trim(jQuery('#HiddenCustomerID').val());
                                if (isCustomerExamNo(CustomerID)) {

                                    jQuery.ajax({
                                        type: "POST",
                                        url: "/Ajax/AjaxConclusion.aspx",
                                        data: { CustomerID: CustomerID,
                                            action: 'UpdateOnCustFinalUnCheck',
                                            currenttime: encodeURIComponent(new Date())
                                        },
                                        cache: false,
                                        dataType: "json",
                                        success: function(jsonmsg) {
                                            if (parseInt(jsonmsg) > 0) {

                                                // 状态更新成功后，获取该体检号的总审相关信息
                                                GetConclusionInfoByCustomerID(CustomerID);

                                                // 查询客户的基本信息，并显示
                                                SearchCustomerBaseInfo(0, 0); //原型：SearchCustomerBaseInfo(IsShowMsg, IsLoadCustomerInfo)

                                            }
                                            else {
                                                //                    var templeteContent = jQuery("#UnCheckedOtherMessageTemplete").html();
                                                //                    if (templeteContent == undefined) { return; }

                                                // 1：解除总审成功 0:表示解除失败， -1:表示体检号不存在，-2:表示还未完成总审，不需要进行解除 -101：表示已经归档，不能进行解除
                                                if (jsonmsg == "-101") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>体检号【" + CustomerID + "】已经归档,不能进行解除！</div>");
                                                } else if (jsonmsg == "-1") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>体检号【" + CustomerID + "】不存在,请核对后再进行解除！</div>");
                                                } else if (jsonmsg == "-2") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>体检号【" + CustomerID + "】还未完成总审，请核对后再进行解除！</div>");
                                                } else if (jsonmsg == "-3") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>体检号【" + CustomerID + "】体检报告已经领取，不能进行解除总审操作！</div>");
                                                } else if (jsonmsg == "0") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>解除失败，请联系系统维护人员！</div>");
                                                }
                                                jQuery("#DivUnCheckedTipsInfo").show();

                                            }
                                        }
                                    });
                                } else {

                                    //        var templeteContent = jQuery("#UnCheckedOtherMessageTemplete").html();
                                    //        if (templeteContent == undefined) { return; }

                                    jQuery("#UnCheckedTipsInfo").html("<div>你输入的体检号不正确请确认后再输入！</div>");
                                    jQuery("#DivUnCheckedTipsInfo").show();

                                }
                            }

                            /// <summary>
                            /// 获取个人信息及同时进行总检的解除 
                            /// </summary>
                            function AutoUnLockFinalFinished() {

                                var curEvent = window.event || e;
                                if (curEvent.keyCode == 13) {
                                    UnLockFinalFinished()
                                }
                            }

                            /// <summary>
                            /// 获取个人信息及同时进行总检的解除 
                            ///  1:解除总检成功，  
                            ///  0:表示解除失败， 
                            /// -1:表示体检号不存在，
                            /// -2:表示还未完成总检，不需要进行解除，
                            /// -3:表示该体检号已经被总审，不能解除总检，需要先解除总审后才能解除总检
                            /// -4:体检报告已经领取，不能进行解除总检操作 
                            /// </summary>
                            function UnLockFinalFinished() {

                                jQuery("#divCustomerInfoArea").hide();  // 隐藏客户基本资料区域
                                jQuery("#DivUnCheckedTipsInfo").hide(); // 隐藏解除总检结果信息区域

                                jQuery("#Is_UncheckORUnFinish").val("UnFinish"); // 标记当前操作的是解除总检

                                var CustomerID = jQuery.trim(jQuery('#HiddenCustomerID').val());
                                if (isCustomerExamNo(CustomerID)) {

                                    jQuery.ajax({
                                        type: "POST",
                                        url: "/Ajax/AjaxConclusion.aspx",
                                        data: { CustomerID: CustomerID,
                                            action: 'UpdateCustomerNotFinalFinished',
                                            currenttime: encodeURIComponent(new Date())
                                        },
                                        cache: false,
                                        dataType: "json",
                                        success: function(jsonmsg) {
                                            if (parseInt(jsonmsg) > 0) {

                                                // 状态更新成功后，获取该体检号的总检相关信息
                                                GetConclusionInfoByCustomerID(CustomerID);

                                                // 查询客户的基本信息，并显示
                                                SearchCustomerBaseInfo(0, 0); //原型：SearchCustomerBaseInfo(IsShowMsg, IsLoadCustomerInfo)

                                            }
                                            else {

                                                // 1：解除总审成功 0:表示解除失败， -1:表示体检号不存在，-2:表示还未完成总审，不需要进行解除 -101：表示已经归档，不能进行解除
                                                if (jsonmsg == "-101") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>体检号【" + CustomerID + "】已经归档,不能进行解除！</div>");
                                                } else if (jsonmsg == "-1") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>体检号【" + CustomerID + "】不存在,请核对后再进行解除！</div>");
                                                } else if (jsonmsg == "-2") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>体检号【" + CustomerID + "】还未完成总检，不需要进行解除！</div>");
                                                } else if (jsonmsg == "-3") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>体检号【" + CustomerID + "】已经被总审，不能解除总检，需要先解除总审后才能解除总检！</div>");
                                                } else if (jsonmsg == "-4") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>体检号【" + CustomerID + "】体检报告已经领取，不能进行解除总检操作！</div>");
                                                } else if (jsonmsg == "0") {
                                                    jQuery("#UnCheckedTipsInfo").html("<div>解除失败，请联系系统维护人员！</div>");
                                                }
                                                jQuery("#DivUnCheckedTipsInfo").show();
                                            }
                                        }
                                    });
                                } else {

                                    //        var templeteContent = jQuery("#UnCheckedOtherMessageTemplete").html();
                                    //        if (templeteContent == undefined) { return; }

                                    jQuery("#UnCheckedTipsInfo").html("<div>你输入的体检号不正确请确认后再输入！</div>");
                                    jQuery("#DivUnCheckedTipsInfo").show();

                                }
                            }

                            /// <summary>
                            /// 根据体检号获取客户的检查信息（主要获取客户的总检，总审的信息）
                            /// </summary>
                            function GetConclusionInfoByCustomerID(CustomerID) {

                                if (isCustomerExamNo(CustomerID)) {

                                    jQuery.ajax({
                                        type: "POST",
                                        url: "/Ajax/AjaxConclusion.aspx",
                                        data: { CustomerID: CustomerID,
                                            action: 'GetConclusionInfoByCustomerID',
                                            currenttime: encodeURIComponent(new Date())
                                        },
                                        cache: false,
                                        dataType: "json",
                                        success: function(jsonmsg) {
                                            if (jsonmsg != null && jsonmsg != "") {

                                                var templeteContent = "";
                                                if (jQuery("#Is_UncheckORUnFinish").val() == "UnCheck") {
                                                    templeteContent = jQuery("#UnCheckedSuccessTemplete").html();
                                                } else {
                                                    templeteContent = jQuery("#UnFinishedSuccessTemplete").html();
                                                }
                                                if (templeteContent == undefined) { return; }
                                                var newContent = ""; // 用来记录新的数据

                                                jQuery(jsonmsg.dataList0).each(function(j, conclusionitem) {

                                                    newContent += templeteContent
                            .replace(/@ID_Customer/gi, conclusionitem.ID_Customer)
                            .replace(/@CustomerName/gi, conclusionitem.CustomerName)
                            .replace(/@Checker/gi, conclusionitem.Checker)
                            .replace(/@CheckedDate/gi, conclusionitem.CheckedDate)
                            .replace(/@FinalDoctor/gi, conclusionitem.FinalDoctor)
                            .replace(/@FinalDate/gi, conclusionitem.FinalDate)
                            ;
                                                });

                                                jQuery("#UnCheckedTipsInfo").html(newContent);
                                                jQuery("#DivUnCheckedTipsInfo").show();
                                            }
                                        }
                                    });

                                }
                            }

                            // ================================ 解除总审 ==== end ==================================================



                            // ================================ 解除总检 ==== start ==================================================

                            /// <summary>
                            /// 解除总检(Ajax方式)
                            /// </summary>
                            function UnLockFinalFinished_Ajax() {

                                var CustomerID = jQuery.trim(jQuery('#HiddenCustomerID').val());
                                if (isCustomerExamNo(CustomerID)) {

                                    jQuery.ajax({
                                        type: "POST",
                                        url: "/Ajax/AjaxConclusion.aspx",
                                        data: { CustomerID: CustomerID,
                                            action: 'UpdateCustomerNotFinalFinished',
                                            currenttime: encodeURIComponent(new Date())
                                        },
                                        cache: false,
                                        dataType: "json",
                                        success: function(jsonmsg) {
                                            if (parseInt(jsonmsg) > 0) {
                                                CtrlFinalCheckButtonDisabled(2, false); // 屏蔽总审页面上的按钮
                                                ShowSystemDialog("体检号【" + CustomerID + "】解除总检成功！");
                                            }
                                            else {
                                                // 1：解除总审成功 0:表示解除失败， -1:表示体检号不存在，-2:表示还未完成总审，不需要进行解除 -101：表示已经归档，不能进行解除
                                                if (jsonmsg == "-101") {
                                                    ShowSystemDialog("体检号【" + CustomerID + "】已经归档,不能进行解除！");
                                                } else if (jsonmsg == "-1") {
                                                    ShowSystemDialog("体检号【" + CustomerID + "】不存在,请核对后再进行解除！");
                                                } else if (jsonmsg == "-2") {
                                                    ShowSystemDialog("体检号【" + CustomerID + "】还未完成总检,请核对后再进行解除！");
                                                } else if (jsonmsg == "-3") {
                                                    ShowSystemDialog("体检号【" + CustomerID + "】已通过总审，不能解除总检，需要先解除总审后才能解除总检！");
                                                } else if (jsonmsg == "0") {
                                                    ShowSystemDialog("解除失败，请联系系统维护人员！");
                                                }
                                            }
                                        }
                                    });
                                }
                            }


                            // ================================ 解除总检 ==== end ==================================================


                            /*************键盘操作事件：主要为上下左右键，适用于table******************/
                            function ConclusionKeyMove(item, event) {

                                var elementID = "QueryConclusionDataTableList";
                                var maxCellsIndex = 0; //  document.getElementById(elementID).rows[0].cells.length -1 ;   //计算表格有列数下标最大值
                                var maxRowsIndex = document.getElementById(elementID).rows.length - 1;            // 计算表格行数下标最大值
                                var objTable = document.getElementById(elementID); 				      // 获取table
                                var currrow = item.parentNode; 								      // 获取当前行
                                while (currrow.tagName != "TR") currrow = currrow.parentNode;
                                var CurrRowIndex = currrow.rowIndex;    // 获取当前行的下标
                                var CurrCellIndex = 0;                  // 获取当前列的下标 （固定为第一列）
                                var nextCellIndex = CurrCellIndex;
                                var nextRowIndex = CurrRowIndex;

                                if (event.keyCode == 40 || event.keyCode == 39) {
                                    if (nextRowIndex < maxRowsIndex) {
                                        nextRowIndex += 1;
                                    }
                                }
                                else if (event.keyCode == 38 || event.keyCode == 37) {
                                    if (nextRowIndex >= 1) {
                                        nextRowIndex -= 1;
                                    }
                                } else if (event.keyCode == 13) {
                                    // 否则，不动
                                } else { return; }

                                if (objTable.rows[nextRowIndex].style.display == "none")
                                    return;
                                if (objTable.rows[nextRowIndex].cells[nextCellIndex] != undefined) {
                                    if (objTable.rows[nextRowIndex].cells[nextCellIndex].children[0] != undefined) {

                                        //回车默认选中当前行
                                        if (event.keyCode == 13) {
                                            objTable.rows[nextRowIndex].cells[0].children[0].checked = true;
                                        }
                                        if (objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].type != "button") {
                                            objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].blur();
                                            objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].focus();
                                        }
                                        else {

                                        }
                                    }
                                }
                                return;
                            }


                            /*************键盘操作事件：主要为上下左右键，适用于table******************/
                            function SelectConclusionKeyMove(item, event) {

                                var elementID = "ConclusionSelectedList";
                                var maxCellsIndex = 0; //  document.getElementById(elementID).rows[0].cells.length -1 ;   //计算表格有列数下标最大值
                                var maxRowsIndex = document.getElementById(elementID).rows.length - 1;            // 计算表格行数下标最大值
                                var objTable = document.getElementById(elementID); 				      // 获取table
                                var currrow = item.parentNode; 								      // 获取当前行
                                while (currrow.tagName != "TR") currrow = currrow.parentNode;
                                var CurrRowIndex = currrow.rowIndex;    // 获取当前行的下标
                                var CurrCellIndex = 1;                  // 获取当前列的下标 （固定为第二列）
                                var nextCellIndex = CurrCellIndex;
                                var nextRowIndex = CurrRowIndex;
                                if (event.keyCode == 40 || event.keyCode == 39) {
                                    if (nextRowIndex < maxRowsIndex) {
                                        nextRowIndex += 1;
                                    }
                                }
                                else if (event.keyCode == 38 || event.keyCode == 37) {
                                    if (nextRowIndex >= 1) {
                                        nextRowIndex -= 1;
                                    }
                                } else if (event.keyCode == 13) {
                                    // 否则，不动
                                } else if (event.keyCode == 120) {

                                } else {

                                    return;
                                }

                                if (objTable.rows[nextRowIndex].style.display == "none")
                                    return;
                                if (objTable.rows[nextRowIndex].cells[nextCellIndex] != undefined) {
                                    if (objTable.rows[nextRowIndex].cells[nextCellIndex].children[0] != undefined) {

                                        if (objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].type == "text") {
                                            //objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].blur();
                                            objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].focus();
                                            objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].select();
                                        }
                                        else {

                                        }
                                    }
                                }
                                return;
                            }



                            // 报告预览
                            function ReprotPreview() {
                                var customerid = jQuery.trim(jQuery('#HiddenCustomerID').val());
                                if (isCustomerExamNo(customerid)) {
                                    var ExamPreviewReport = jQuery("#ExamPreviewReport").val();
                                    if (ExamPreviewReport == "" || ExamPreviewReport == undefined) {
                                        ShowSystemDialog("报告预览模板未配置，请配置后再预览！");
                                        return;
                                    }
                                    jQuery(".hiddleScroll").hide();
                                    // var strContent = '<div style="width: 836px; height: 100%;vertical-align:top; min-height:700px; border:1px solid #dcdcdc;"><object id="FastReportView" classid="clsid:11DB7EF1-5149-4893-8C8D-CBFB5CF0C9C2"  style="top:0px;width: 100%; height: 100%; min-height:700px;"> </object></div>';
                                    var strContent = '<object id="FastReportView" classid="clsid:11DB7EF1-5149-4893-8C8D-CBFB5CF0C9C2"  style=" display:none;width: 836px; min-height:480px;"> </object>';

                                    // jQuery("#reportView").html(strContent); 
                                    //        var FastReportView = document.getElementById("FastReportView");
                                    //        if (FastReportView != null) {
                                    //            FastReportView.ClearChirldControl();    //清除所有子控件
                                    //            //判断当前客户的体检类型是否包含为彩色打印 Begin
                                    //            var ExamType = jQuery.trim(jQuery("#lblExamType").text());       //客户体检类型
                                    //            var UseColorPrintPaperExamType = jQuery.trim(jQuery("#UseColorPrintPaperExamType").val());
                                    //            if (UseColorPrintPaperExamType.indexOf(ExamType) > -1) {
                                    //                //获取客户性别
                                    //                var Sex = jQuery("#GenderNameText").text();
                                    //                if (Sex == "男") {
                                    //                    var MaleColorPreviewReport = jQuery("#MaleColorPreviewReport").val();
                                    //                    FastReportView.ReportPreview(customerid, MaleColorPreviewReport, "1", 0);
                                    //                }
                                    //                else if (Sex == "女") {
                                    //                    var FeMaleColorPreviewReport = jQuery("#FeMaleColorPreviewReport").val();
                                    //                    FastReportView.ReportPreview(customerid, FeMaleColorPreviewReport, "1", 0);
                                    //                }
                                    //                else {
                                    //                    //性别不详则默认报告打印
                                    //                    FastReportView.ReportPreview(customerid, ExamPreviewReport, "1", 0);
                                    //                }
                                    //            }
                                    //            else {
                                    //                FastReportView.ReportPreview(customerid, ExamPreviewReport, "1", 0);
                                    //            }
                                    //            //判断当前客户的体检类型是否包含为彩色打印，如果区分则分别获取对应模板 End

                                    //            jQuery("#FastReportView").css("height", jQuery("#FastReportView").parent().parent().height() + "px");
                                    //            jQuery("#FastReportView").show();
                                    //            //dxqh(4); jQuery("#ExamInfoTabLi4").show();
                                    //        }

                                    art.dialog({
                                        id: 'artDlgReportView',
                                        title: '报告预览',
                                        lock: true,
                                        fixed: true,
                                        padding: 0,
                                        width: 836,
                                        height: '100%',
                                        opacity: 0.3,
                                        content: strContent,
                                        init: function() {
                                            var FastReportView = document.getElementById("FastReportView");
                                            if (FastReportView != null) {
                                                FastReportView.ClearChirldControl();    //清除所有子控件
                                                //判断当前客户的体检类型是否包含为彩色打印 Begin
                                                var ExamType = jQuery.trim(jQuery("#lblExamType").text());       //客户体检类型
                                                var UseColorPrintPaperExamType = jQuery.trim(jQuery("#UseColorPrintPaperExamType").val());
                                                var IsUseCustReport = jQuery.trim(jQuery("#IsUseCustReport").val());
                                                if (IsUseCustReport == 1 || IsUseCustReport == "True") {

                                                    if (UseColorPrintPaperExamType.indexOf(ExamType) > -1) {
                                                        //获取客户性别
                                                        var Sex = jQuery("#GenderNameText").text();
                                                        if (Sex == "男") {
                                                            var MaleColorPreviewReport = jQuery("#CustMaleColorPreviewReport").val();
                                                            FastReportView.CustReportPreview(customerid, MaleColorPreviewReport, "1", 0);
                                                        }
                                                        else if (Sex == "女") {
                                                        var FeMaleColorPreviewReport = jQuery("#CustFeMaleColorPreviewReport").val();
                                                        FastReportView.CustReportPreview(customerid, FeMaleColorPreviewReport, "1", 0);
                                                        }
                                                        else {
                                                            //性别不详则默认报告打印
                                                            ExamPreviewReport = jQuery("#CustExamPreviewReport").val();
                                                            FastReportView.CustReportPreview(customerid, ExamPreviewReport, "1", 0);
                                                        }
                                                    }
                                                    else {
                                                        ExamPreviewReport = jQuery("#CustExamPreviewReport").val();
                                                        FastReportView.CustReportPreview(customerid, ExamPreviewReport, "1", 0);
                                                    }
                                                    //判断当前客户的体检类型是否包含为彩色打印，如果区分则分别获取对应模板 End

                                                    jQuery("#FastReportView").css("height", jQuery("#FastReportView").parent().parent().height() + "px");
                                                    jQuery("#FastReportView").show();
                                                }
                                                else {

                                                    if (UseColorPrintPaperExamType.indexOf(ExamType) > -1) {
                                                        //获取客户性别
                                                        var Sex = jQuery("#GenderNameText").text();
                                                        if (Sex == "男") {
                                                            var MaleColorPreviewReport = jQuery("#MaleColorPreviewReport").val();
                                                            FastReportView.ReportPreview(customerid, MaleColorPreviewReport, "1", 0);
                                                        }
                                                        else if (Sex == "女") {
                                                            var FeMaleColorPreviewReport = jQuery("#FeMaleColorPreviewReport").val();
                                                            FastReportView.ReportPreview(customerid, FeMaleColorPreviewReport, "1", 0);
                                                        }
                                                        else {
                                                            //性别不详则默认报告打印
                                                            FastReportView.ReportPreview(customerid, ExamPreviewReport, "1", 0);
                                                        }
                                                    }
                                                    else {
                                                        FastReportView.ReportPreview(customerid, ExamPreviewReport, "1", 0);
                                                    }
                                                    //判断当前客户的体检类型是否包含为彩色打印，如果区分则分别获取对应模板 End

                                                    jQuery("#FastReportView").css("height", jQuery("#FastReportView").parent().parent().height() + "px");
                                                    jQuery("#FastReportView").show();
                                                }
                                            }
                                        },
                                        close: function() {
                                            jQuery("#FastReportView").hide();
                                        }
                                    });


                                } else {

                                    ShowSystemDialog("体检号错误，不能预览报告！");
                                }




                            }


                            /// <summary>
                            // 科室检查详细信息 在总检页面重载  20140408 by wtang 
                            /// </summary>
                            /// <param name="SectionID">科室ID</param>
                            /// <param name="InterfaceType">接口类型</param>
                            function InitSectionDetailInfo(SectionID, InterfaceType) {

                                // 检验科的模板
                                if (InterfaceType == "LAB") {
                                    jQuery(".DetailsDivTypeLab").show();     // 检验科模板
                                    jQuery(".DetailsDivTypeGeneral").hide(); // 一般科室模板

                                    var CustExamDetailListNoDataTempleteContent = jQuery('#CustExamDetailListNoDataLabTemplete').html();
                                    if (CustExamDetailListNoDataTempleteContent == undefined) { jQuery("#ShowSectionDetailDataLabList").html(""); return; }

                                    jQuery("#ShowSectionDetailDataList").html(CustExamDetailListNoDataTempleteContent);
                                    jQuery("#ShowSectionDetailDataList td").html("数据加载中，请稍后...");

                                    var examSectionItems = gCustExamSectionItems;
                                    var examItemContent = "";
                                    var CustExamDetailListTempleteContent = jQuery('#CustExamDetailListLabTemplete').html();
                                    if (CustExamDetailListTempleteContent == undefined) { return; }

                                    jQuery(examSectionItems.dataList2).each(function(j, examitem) {
                                        if (examitem.ID_Section == SectionID) {
                                            examItemContent += CustExamDetailListTempleteContent
                        .replace(/@ExamItemName/gi, examitem.ExamItemName)
                        .replace(/@ResultLabValues/gi, examitem.ResultLabValues != "" ? examitem.ResultLabValues : "")
                        .replace(/@ResultLabUnit/gi, examitem.ResultLabUnit)
                        .replace(/@ResultLabMark/gi, examitem.ResultLabMark)
                        .replace(/@ResultLabRange/gi, examitem.ResultLabRange)
                        ;
                                        }
                                    });

                                    if (examItemContent != "") {
                                        jQuery("#ShowSectionDetailDataListLab").html(examItemContent);

                                        // 设置Table的基数偶数项背景
                                        SetTableEvenOddRowStyle();

                                    } else {
                                        jQuery("#ShowSectionDetailDataListLab").html(CustExamDetailListNoDataTempleteContent);
                                    }

                                } else {
                                    jQuery(".DetailsDivTypeLab").hide();     // 检验科模板
                                    jQuery(".DetailsDivTypeGeneral").show(); // 一般科室模板

                                    var CustExamDetailListNoDataTempleteContent = jQuery('#CustExamDetailListNoDataTemplete').html();
                                    if (CustExamDetailListNoDataTempleteContent == undefined) { jQuery("#ShowSectionDetailDataList").html(""); return; }

                                    jQuery("#ShowSectionDetailDataList").html(CustExamDetailListNoDataTempleteContent);
                                    jQuery("#ShowSectionDetailDataList td").html("数据加载中，请稍后...");

                                    var examSectionItems = gCustExamSectionItems;
                                    var examItemContent = "";
                                    var CustExamDetailListTempleteContent = jQuery('#CustExamDetailListTemplete').html();
                                    if (CustExamDetailListTempleteContent == undefined) { return; }

                                    jQuery(examSectionItems.dataList2).each(function(j, examitem) {

                                        if (examitem.ID_Section == SectionID) {
                                            examItemContent += CustExamDetailListTempleteContent
                        .replace(/@ExamItemName/gi, examitem.ExamItemName)
                        .replace(/@ExamResultSummary/gi, examitem.ResultSummary)
                        ;
                                        }
                                    });

                                    if (examItemContent != "") {
                                        jQuery("#ShowSectionDetailDataList").html(examItemContent);

                                        // 设置Table的基数偶数项背景
                                        SetTableEvenOddRowStyle();
                                    } else {
                                        jQuery("#ShowSectionDetailDataList").html(CustExamDetailListNoDataTempleteContent);
                                    }
                                }

                            }



                            /// <summary>
                            /// 科室历次对比详细信息 在总检页面重载 
                            /// </summary>
                            /// <param name="ID_Section">科室ID</param>
                            /// <param name="InterfaceType">接口类型</param>
                            /// <param name="ID_Customer01">体检号一</param>
                            /// <param name="CustomerExamState01">体检号一的状态</param>
                            /// <param name="ID_Customer02">体检号二</param>
                            /// <param name="CustomerExamState02">体检号二的状态</param>
                            /// 返回值：
                            /// json:表示返回的数据对比后的结果数据。
                            /// 1：和上次数据一样。
                            /// 2：和上次的数据位置互换 （1,2号体检号位置互换）
                            ///-1：数据查询失败 
                            function InitSectionCompareDetailInfo(ID_Section, InterfaceType, ID_Customer01, CustomerExamState01, ID_Customer02, CustomerExamState02) {


                                var CustSectionCompareDetailListNoDataTempleteContent = jQuery('#CustSectionExamCompareDetailListNoDataTemplete').html();
                                if (CustSectionCompareDetailListNoDataTempleteContent == undefined) { jQuery("#ShowSectionDetailDataLabList").html(""); return; }

                                jQuery("#ShowSectionCompareDetailDataList").html(CustSectionCompareDetailListNoDataTempleteContent);
                                jQuery("#ShowSectionCompareDetailDataList td").html("数据加载中，请稍后...");

                                if (InterfaceType == "LAB") {
                                    jQuery("#ShowSectionCompareDetailDataList td").html("实验室类数据请在【实验对比】中查看！");
                                    return;
                                } // 备注，检验科的不在这里弹出对比 20140411 by wtang 
                                jQuery.ajax({
                                    type: "POST",
                                    url: "/Ajax/AjaxCustExam.aspx",
                                    data: { ID_Customer01: ID_Customer01,
                                        CustomerExamState01: CustomerExamState01,
                                        ID_Customer02: ID_Customer02,
                                        CustomerExamState02: CustomerExamState02,
                                        ID_Section: ID_Section,
                                        action: 'GetCustomerCompareExamItem',
                                        currenttime: encodeURIComponent(new Date())
                                    },
                                    cache: false,
                                    dataType: "json",
                                    success: function(msg) {

                                        var FeeItemCount = 0;  // 记录收费项目的个数
                                        var newcontent = "";
                                        var TempFeeListIndex = "";
                                        var CurrFeeItemID = 0;
                                        var CurrExamItemCount = 0;
                                        if (msg == null || msg == "" || parseInt(msg.totalCount) <= 0) {

                                            jQuery("#ShowSectionCompareDetailDataList").html(CustSectionCompareDetailListNoDataTempleteContent);
                                            return;
                                        }
                                        else if (msg != null && msg != "" && parseInt(msg.totalCount) > 0) {

                                            var tempFeeItemAllCountent = ""; // 收费项目所有信息
                                            var ExamItemFirstRowTempleteContent = jQuery('#SectionCompare_ExamItemFirstRowTemplete').html();             //收费项目-检查项目第一行 模版
                                            var ExamItemOtherRowTempleteContent = jQuery('#SectionCompare_ExamItemOtherRowTemplete').html();             //检查项目其它行 模版

                                            // 加载收费项目
                                            jQuery(msg.dataList0).each(function(i, feeitem) {

                                                FeeItemCount++;  // 记录收费项目的个数

                                                tempFeeItemAllCountent = ""; // 一个收费项目开始
                                                // 先拼接第一个检查项目的模版
                                                tempFeeItemAllCountent +=
                             ExamItemFirstRowTempleteContent.replace(/@FeeName/gi, feeitem.FeeName)
                            .replace(/@ID_Fee/gi, feeitem.ID_Fee)
                            .replace(/@ID_CustFee/gi, feeitem.ID_CustFee)
                            ;
                                                CurrFeeItemID = 0; //在加载检查项目使用
                                                CurrExamItemCount = 0; // 从零开始计数

                                                // 加载检查项目列表
                                                jQuery(msg.dataList1).each(function(j, examitem) {

                                                    if (feeitem.ID_Fee == examitem.ID_Fee) {

                                                        CurrExamItemCount++; // 当前检查项目的编号 在循环加载检查项目时，用于计数，计数需要合并的行数

                                                        if (CurrExamItemCount == 1) {
                                                            tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@FeeExamItemID/gi, examitem.ID_Fee + "_" + examitem.ID_ExamItem)
                                    .replace(/@ExamItemName/gi, examitem.ExamItemName)
                                    .replace(/@ResultSummary01/gi, examitem.ResultSummary01)
                                    .replace(/@ResultSummary02/gi, examitem.ResultSummary02)
                                    .replace(/@CompareResultClass/gi, examitem.IsSameExamSummary == "1" ? "" : "CompareResultClass")
                                    ;

                                                        } else {
                                                            tempFeeItemAllCountent += ExamItemOtherRowTempleteContent.replace(/@FeeExamItemID/gi, examitem.ID_Fee + "_" + examitem.ID_ExamItem)
                                     .replace(/@ExamItemName/gi, examitem.ExamItemName)
                                    .replace(/@ResultSummary01/gi, examitem.ResultSummary01)
                                    .replace(/@ResultSummary02/gi, examitem.ResultSummary02)
                                    .replace(/@CompareResultClass/gi, examitem.IsSameExamSummary == "1" ? "" : "CompareResultClass")
                                    ;
                                                        }
                                                    }

                                                }); // 结束检查项目循环

                                                // 合并收费项目的行数，并进行拼接
                                                if (CurrExamItemCount >= 1) {
                                                    // tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@FeeItemRowSpan/gi, CurrExamItemCount);
                                                    newcontent += tempFeeItemAllCountent;
                                                }

                                            }); // 结束收费项目循环


                                            if (FeeItemCount > 0 && newcontent != '') {
                                                jQuery('#ShowSectionCompareDetailDataList').html(newcontent);
                                                SetTableEvenOddRowStyle(); // 奇偶行背景
                                            } else {
                                                jQuery("#ShowSectionCompareDetailDataList").html(CustSectionCompareDetailListNoDataTempleteContent);
                                            }
                                        }

                                    }

                                });

                            }
