

        // 保存--读取到是体检数据，以重置页面时使用
        var gPageCustomExamDataMsg = "";
        // 保存--读取到上次的数据
        var gPageCustomExamLastSaveDataMsg = "";
        jQuery(document).ready(function () {
            SwitchHeader(2); // 显示分科的头部
            jQuery("#TipsArea").autoHeight(); // 自适应高度(提示信息区域)

            // 显示科室名称
            jQuery("#HeaderSectionTitle").html(jQuery("#SectionName").val());

            // 查询客户的基本信息，并显示
            SearchCustomerBaseInfo(0,0); //原型：SearchCustomerBaseInfo(IsShowMsg, IsLoadCustomerInfo)

            // 查询客户的体检项目等详细信息
            QueryExamDetailData();

            SepBetweenExamItems = jQuery("#SepBetweenExamItems").val();   //项目分隔符        如：、
            SepBetweenSymptoms = jQuery("#SepBetweenSymptoms").val();     //体征词分隔符      如：、
            TerminalSymbol = jQuery("#TerminalSymbol").val();             //项目终结符        如：。
            SepExamAndValue = jQuery("#SepExamAndValue").val();           //项目小结分隔符    如：：
            NoBetweenExamItems = jQuery("#NoBetweenExamItems").val();     //项目序号          如：(1)
            NoBetweenSympotms = jQuery("#NoBetweenSympotms").val();       //体征词序号        如： ①

        });


        /// <summary>
        /// 显示分单详情
        /// </summary>
        /// <param name="ID_Fee">收费项目ID</param>
        function GetCustomerSectionExamSingleInfo() {

            var tmpVerifyValueIsChange = VerifyValueIsChange();
            // 如果数据进行了变动，则提示进行保存。
            if (tmpVerifyValueIsChange == 1) {
                var tipscontent = "当前分单数据进行了改动，请确认是否进行保留！<br/>【保留】点击后任然停留在当前页面！<br/>【放弃】点击后放弃当前修改的值！";

                art.dialog({
                    id: 'artDialogID',
                    content: tipscontent,
                    lock: true,
                    fixed: true,
                    zIndex: 555,
                    opacity: 0.3,
                    button: [{
                        name: '保留',
                        title: '提示',
                        callback: function () {
                            jQuery("#txtCustomerID").val(jQuery("#txtHiddenCustomerID").val());
                            return true;
                        }, focus: true
                    }, {
                        name: '放弃',
                        callback: function () {

                            GetCustomerSectionExamSingleInfo_NextStep();
                            return true;
                        }
                    }],
                    close: function () {
                        return true;
                    }
                });

            } else {
                GetCustomerSectionExamSingleInfo_NextStep();
            }

        }

        /// <summary>
        /// 根据体检号，查询客户的个人信息及体检项目
        /// </summary>
        function GetCustomerSectionExamSingleInfo_NextStep() {
            jQuery('#IDCardNoText').html("&nbsp;");
            jQuery('#CustomerNameText').html("&nbsp;");
            jQuery('#GenderNameText').html("&nbsp;");
            jQuery('#MarriageNameText').html("&nbsp;");
            jQuery('#MobileNoText').html("&nbsp;");

            jQuery('#fkls').hide();
            jQuery('#fklscen').hide();
            jQuery('#TipsArea').hide();
            jQuery('#ExamInfoTabLi1').hide();
            jQuery('#ExamInfoTabLi2').hide();
            jQuery('#ExamInfoTabLi3').hide();
            jQuery('#btnCustomerSimpleInfo').hide();
            jQuery('#CustomerSectionQuickSwitch').hide();
            //jQuery('#CustExamResult').hide();

            //显示等待信息
            var ExamItemWaitingTempleteContent = jQuery('#ExamItemWaitingTemplete').html(); //读取数据等待信息模版
            jQuery('#TipsMessage').html(ExamItemWaitingTempleteContent);                    //显示等待信息
            jQuery('#TipsArea').show();

            var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
            var TipsMessageTempleteContent = jQuery('#TipsMessageTemplete').html();     //读取提示信息模版
            if (customerid == "") {
                jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "请输入客户的体检号！"));   //显示空信息
                jQuery('#TipsArea').show();
                return;
            }
            if (!isCustomerExamNo(customerid)) {

                jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "体检号格式错误，请检核对后重新输入！"));   //显示空信息
                jQuery('#TipsArea').show();
                return;
            }

            var ExamUrl = "/System/Exam/LabExamOperSingle.aspx?txtSectionID=" + jQuery('#txtSectionID').val() + "&txtCustomerID=" + customerid;
            DoLoad(ExamUrl, '');
        }


        /// <summary>
        /// 显示分单详情
        /// </summary>
        /// <param name="ID_Fee">收费项目ID</param>
        function AutoGetCustomerSectionExamSingleInfo() {

            var curEvent = window.event || e;
            if (curEvent.keyCode == 13) {
                var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
                if (isCustomerExamNo(customerid)) {
                    var tmpVerifyValueIsChange = VerifyValueIsChange();
                    // 如果数据进行了变动，则自动进行保存。
                    if (tmpVerifyValueIsChange == 1) {
                        var tipscontent = "当前分单数据进行了改动，请确认是否进行保留！<br/>【保留】点击后任然停留在当前页面！<br/>【放弃】点击后放弃当前修改的值！";

                        art.dialog({
                            id: 'artDialogID',
                            content: tipscontent,
                            lock: true,
                            fixed: true,
                            zIndex: 555,
                            opacity: 0.3,
                            button: [{
                                name: '保留',
                                title: '提示',
                                callback: function () {
                                    jQuery("#txtCustomerID").val(jQuery("#txtHiddenCustomerID").val());
                                    return true;
                                }, focus: true
                            }, {
                                name: '放弃',
                                callback: function () {

                                    AutoGetCustomerSectionExamSingleInfo_NextStep();
                                    return true;
                                }
                            }],
                            close: function () {
                                return true;
                            }
                        });

                    } else {
                        AutoGetCustomerSectionExamSingleInfo_NextStep();
                    }
                } 
            }
        }

        /// <summary>
        /// 根据输入情况，自动获取客户的个人信息及体检项目 （当输入数据达到体检号的数据长度时，自动读取）
        /// </summary>
        function AutoGetCustomerSectionExamSingleInfo_NextStep() {

            var customerid = jQuery.trim(jQuery('#txtCustomerID').val());
            var ExamUrl = "/System/Exam/LabExamOperSingle.aspx?txtSectionID=" + jQuery('#txtSectionID').val() + "&txtCustomerID=" + customerid;
            DoLoad(ExamUrl, '');

        }

        /// <summary>
        /// 清除各个体检项目的默认值，及操作者输入的或选择的值
        /// </summary>
        function ClearCustomerDefaultValue() {

        }
        /// <summary>
        /// 清空客户信息（清空客户的所有信息，便于下一个客户数据的读取）
        /// </summary>
        function ClearCustomerInfo() {

            var ExamItemEmptyTempleteContent = jQuery('#ExamItemEmptyTemplete').html();     //读取空数据信息模版
            jQuery('#TipsMessage').html(ExamItemEmptyTempleteContent);                     //显示空信息
            jQuery('#TipsArea').show();

            jQuery('#IDCardNoText').html("&nbsp;");
            jQuery('#CustomerNameText').html("&nbsp;");
            jQuery('#GenderNameText').html("&nbsp;");
            jQuery('#MarriageNameText').html("&nbsp;");
            jQuery('#MobileNoText').html("&nbsp;");

            jQuery('#txtCustomerID').val("");  //清空体检号

        }

        /// <summary>
        /// 查询客户的体检项目等详细信息
        /// </summary>
        function QueryExamDetailData() {

            jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标

            jQuery('#fkls').hide();
            jQuery('#fklscen').hide();
            jQuery('#fklscen').hide();
            jQuery('#TipsArea').hide();
            jQuery('#ExamInfoTabLi1').hide();
            jQuery('#ExamInfoTabLi2').hide();
            jQuery('#ExamInfoTabLi3').hide();
            jQuery('#btnCustomerSimpleInfo').hide();
            jQuery('#CustomerSectionQuickSwitch').hide();
            //jQuery('#CustExamResult').hide();

            // 查询数据前，先隐藏客户基本信息区域
            jQuery("#divCustomerInfoArea").hide();

            var ExamState = jQuery('#ExamState').val();                         //体检状态,当次体检信息的状态：0表示在线，1表示归档，2表示在一号分库，3表示在二号分库…
            var Is_FeeSettled = jQuery('#Is_FeeSettled').val();                 //是否完成缴费
            var Is_GuideSheetPrinted = jQuery('#Is_GuideSheetPrinted').val();   //指引单是否打印
            var Is_SectionLock = jQuery('#Is_SectionLock').val();               //分科锁定
            var TipsMessageTempleteContent = jQuery('#TipsMessageTemplete').html();     //读取提示信息模版
            var Is_ReportReceipted = jQuery('#Is_ReportReceipted').val();               //报告已领取

            var CustomerSecurityLevel = jQuery("#CustomerSecurityLevel").val(); // 客户  操作密级
            var OperateLevel = jQuery("#OperateLevel").val();                   // 操作员操作密级
            var Is_Paused = jQuery('#Is_Paused').val();                         // 0表示处于正常体检状态；1表示处于禁检状态（若客户处于禁检状态时，只有解除禁检后方能进行体检）

            if (ExamState == "") {
                jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "请输入你要操作的体检号！"));   //显示提示信息
                jQuery('#TipsArea').show();
                return;
            }

            //体检状态,当次体检信息的状态：0表示在线，1表示归档，2表示在一号分库，3表示在二号分库…
            if (ExamState != "0") {
                jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "该客户已归档，不能进行分科检查！"));   //显示提示信息
                jQuery('#TipsArea').show();
                return;
            }
            if (Is_Paused == "True" || Is_Paused == "1") {
                jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "该客户已被禁检，不能进行分科检查！"));   //显示提示信息
                jQuery('#TipsArea').show();
                return;
            }

            // 检查操作密级
            if (Is_ReportReceipted == "True" && parseInt(CustomerSecurityLevel) > parseInt(OperateLevel)) {
                jQuery("#divCustomerInfoArea").hide();  // 如果没有权限，则客户基本信息页不允许查看
                jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "对不起，你没有权限对该客户进行体检！"));   //显示提示信息
                jQuery('#TipsArea').show();
                return;
            }
            if (Is_FeeSettled != "True") {
                jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "该客户还未完成缴费，请先缴费后，再进行分科检查！"));   //显示提示信息
                jQuery('#TipsArea').show();
                return;
            }

            if (Is_GuideSheetPrinted != "True") {
                jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "该客户还未打印指引单，请先打印指引单后，再进行分科检查！"));   //显示提示信息
                jQuery('#TipsArea').show();
                return;
            }

            // 分科锁定后，修改为任然可以正常查看数据，但是不能进行修改等操作。
            //            if (Is_SectionLock == "True") {
            //                jQuery('#TipsMessage').html(TipsMessageTempleteContent.replace(/@TipsMessage/gi, "该客户分科检查已被锁定，请解除分科锁定后，再进行分科检查！"));   //显示提示信息
            //                return;
            //            }

            var NoFeeItemTempleteContent = jQuery('#NoFeeItemTemplete').html(); //暂无交费项目或体检项目模版
            //显示等待信息
            var ExamItemWaitingTempleteContent = jQuery('#ExamItemWaitingTemplete').html(); //读取数据等待信息模版
            jQuery('#TipsMessage').html(ExamItemWaitingTempleteContent);                   //显示等待信息
            jQuery('#TipsArea').show();

            var CustomerID = jQuery.trim(jQuery('#txtCustomerID').val());
            var SectionID = jQuery('#txtSectionID').val();
            var ID_Gender = jQuery('#txtGenderID').val();
            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxCustExam.aspx",
                data: { CustomerID: CustomerID,
                    SectionID: SectionID,
                    InterfaceType: "lab",
                    ID_Gender: ID_Gender,
                    action: 'GetCustomerExamDetailDataList',
                    currenttime: encodeURIComponent(new Date())
                },
                cache: false,
                dataType: "json",
                success: function (jsonmsg) {
                    gPageCustomExamDataMsg = jsonmsg;

                    InitCustomerExamListSinglePage(jsonmsg);

                    if (jsonmsg != null && jsonmsg != "" && parseInt(jsonmsg.totalCount) > 0) {
                        // 当体检次数大于1次时，才能进行分科对比
                        if (parseInt(jQuery("#totalExamNumber").val()) > 1) {
                            jQuery("#ExamInfoTabLi2").show();           // 分科对比 显示
                        } else {
                            jQuery("#ExamInfoTabLi2").hide();           // 分科对比 隐藏
                        }
                    }
                }
            });

        }


        var idFirstFee = 0; // 默认情况加载第一个分单详细内容
        /// <summary>
        /// 根据获得的数据，初始化页面分单列表
        /// </summary>
        /// <param name="msg">该客户的收费项目，体检项目，检查结果，分单列表，分单保存的数据，以及发送到接口的分单申请数据</param>
        function InitCustomerExamListSinglePage(msg) {
            var strIDFeeReportMerger = "Merger;"; // 记录已经加载的分单 ，组合格式如："Merger;12;21;78;90;967;567;"
            var NoFeeItemTempleteContent = jQuery('#NoFeeItemTemplete').html(); //暂无交费项目或体检项目模版

            var strFirstApplyID = "";   // 第一个申请号ID

            var FeeItemCount = 0;  // 记录收费项目的个数
            if (msg == null || msg == "" || parseInt(msg.totalCount) <= 0) {

                jQuery('#TipsMessage').html(NoFeeItemTempleteContent);             //显示没有收费项目的提示信息
                jQuery('#TipsArea').show();
                return;
            }
            else if (msg != null && msg != "" && parseInt(msg.totalCount) > 0) {

                // 从模版中读取数据加载列表
                var SectionExamedListSingleContent = jQuery('#SectionExamedListSingleContentTemplete').html();   //收费项目模版

                var tmpCustApplyID = "";    // 临时客户申请号存放
                var tmpSendProjectIDs = ""; // 临时客户申请号号对应的收费编码
                var tmpCustApplySGMark = "【手工单】"; // 临时客户申请单手工单的标记
                var newcontent = ""; //  ExamItemHeaderTempleteContent; 表头已经在模板外定义 20140404 by wtang
                var RowNumber = 1;

                
                // 加载收费项目 分单列表
                jQuery(msg.dataList4).each(function (i, applyitem) {
                    if (applyitem.ApplyID == "") { return true; } // 如果为手工单，则通过下面的方式加载。 20150604 by wtang 
                    if (idFirstFee == 0) {
                        strFirstApplyID = applyitem.ApplyID;
                    }
                    // 根据收费项目编码来获取申请号，如果收费编码为空，则采用ID_CustFee替换 20150603 by wtang 
                    tmpCustApplyID = applyitem.ApplyID;

                    strIDFeeReportMerger = strIDFeeReportMerger + tmpCustApplyID + ";";

                    newcontent +=
                             GetCustSectionExamedListSingleContentByFeeCode(applyitem.SendProjectIDs, msg, tmpCustApplyID, RowNumber, SectionExamedListSingleContent);
                    RowNumber++;
                });
            

                // 加载收费项目 分单列表
                jQuery(msg.dataList3).each(function (i, feeitem) {
                    // 根据收费项目编码来获取申请号，如果收费编码为空，则采用ID_CustFee替换 20150603 by wtang 
                    tmpCustApplyID = GetCustApllyIDByFeeCode(feeitem.FeeCode, msg, feeitem.ID_Customer, feeitem.ID_FeeReportMerger);

                    if (RowNumber == 1) {
                        idFirstFee = feeitem.ID_FeeReportMerger;
                        strFirstApplyID = tmpCustApplyID;
                    }
                    // 按照收费项目编码的方式获取申请号，去掉重复项目，20150603 by wtang 
                    if (strIDFeeReportMerger.indexOf(";" + tmpCustApplyID + ";", 0) > 0) {
                        return true; // continue;
                    }
                    strIDFeeReportMerger = strIDFeeReportMerger + tmpCustApplyID + ";";

                    if (tmpCustApplyID.startWith("F") == true) {
                        // tmpCustApplySGMark = "【手工单】"; // 临时客户申请单手工单的标记
                        tmpCustApplySGMark = ""; // 暂时不显示区别（因为外部版本都没有使用LIS接口，区分无意义） 20150519 by wtang
                    } else {
                        tmpCustApplySGMark = ""; // 非手工单
                    }
                    newcontent +=
                             SectionExamedListSingleContent.replace(/@RowNumber/gi, RowNumber)
                            .replace(/@SectionExamSingleName/gi, feeitem.ReportFeeName)
                            .replace(/@SectionExamSingleTitle/gi, feeitem.ReportFeeName + tmpCustApplySGMark)
                            .replace(/@ID_Fee/gi, feeitem.ID_FeeReportMerger)
                            .replace(/@ExamDoctorName/gi, feeitem.ExamDoctorName)
                            .replace(/@TypistName/gi, "&nbsp;")
                            .replace(/@ReportTime/gi, "&nbsp;")
                            .replace(/@FeeCode/gi, feeitem.FeeCode)
                            .replace(/@ApplyID/gi, tmpCustApplyID) // 根据收费编码获取到的 申请号
                            .replace(/@ReportStateBg/gi, "Single_list_table_Unfinished") // 标记未完成状态
                            ;
                    RowNumber++;
                });
            }
            
            if (newcontent != '') {
                jQuery('#TipsArea').hide();
                jQuery('#SectionExamedListSingleContent').html(newcontent);
                //jQuery('#CustExamResult').show()
                ShowExamSingleDetail(idFirstFee, strFirstApplyID, 1); // 默认加载第一个分单详细内容 20150424 by wtang

                jQuery(msg.dataList4).each(function (i, feeitem) {
                    jQuery(".SubReportList").each(function (m, applyitem) {
                        if (feeitem.Is_TypistFinish == "True" && jQuery(this).attr("applyid") == feeitem.ID_Apply) {
                            jQuery(this).removeClass("Single_list_table_Unfinished");
                        }
                    });
                });

                jQuery(".j-autoHeight").autoHeight(); // 自适应高度

                // 获取客户的病症级别，并判断是否进行相应的提示 20140326 by wtang 
                QueryCustomerExamItemDiseaseLevelTips();

                // 初始化按钮是否可以使用 , （在页面上次保存的数据加载完成后，调用一次按钮控制函数）
                // InitButtomDisabled(); 20150427 by wtang

                jQuery('#fkls').show();
                jQuery('#fklscen').show();
                // 显示分单列表 和 科室小结 Tab 选项
                jQuery('#ExamInfoTabLi1').show();
                jQuery('#ExamInfoTabLi3').show();
                jQuery('#btnCustomerSimpleInfo').show();
                jQuery('#CustomerSectionQuickSwitch').show();

                // 判断表格是否存在滚动条,并设置相应的样式
                JudgeTableIsExistScroll();
            } else {

                jQuery('#TipsMessage').html(NoFeeItemTempleteContent);             //显示没有收费项目的提示信息
                jQuery('#TipsArea').show();
                return;
            }

        }

        /// <summary>
        /// 通过收费项目编码 获取 申请单号
        /// </summary>
        /// <param name="_FeeCode">收费项目编码</param>
        function GetCustApllyIDByFeeCode(_FeeCode, msg, CustomerID, CustFeeID) {
            var ApllyID = "";
            jQuery(msg.dataList4).each(function (i, feeitem) {

                    var IDArray = feeitem.SendProjectIDs.split(";");
                    for (var i = 0; i < IDArray.length; i++) {
                        if (IDArray[i] == _FeeCode) {
                            ApllyID = feeitem.ApplyID;
                            return true;
                        }
                    }
            });

            if (ApllyID == "") {
                ApllyID = "F" + jQuery.md5(CustFeeID + "-" + CustomerID).toUpperCase().substring(8, 20);
            }

            return ApllyID;
        }
        
        /// <summary>
        /// 通过收费项目编码合并的字符串，获取分单列表
        /// </summary>
        /// <param name="_FeeCodeStr">收费项目编码合并的字符串</param>
        function GetCustSectionExamedListSingleContentByFeeCode(_FeeCodeStr, msg, ApplyID, RowNumber, SectionExamedListSingleContent) {
            
            var tempContent = "";
            jQuery(msg.dataList3).each(function (i, feeitem) {

                if (tempContent != "") { return false; }

                var IDArray = _FeeCodeStr.split(";");
                for (var i = 0; i < IDArray.length; i++) {
                    if ( IDArray[i] != "" && IDArray[i] == feeitem.FeeCode) {
                        if (idFirstFee == 0) {
                            idFirstFee = feeitem.ID_FeeReportMerger;
                        }
                        tempContent =
                             SectionExamedListSingleContent.replace(/@RowNumber/gi, RowNumber)
                            .replace(/@SectionExamSingleName/gi, feeitem.ReportFeeName)
                            .replace(/@SectionExamSingleTitle/gi, feeitem.ReportFeeName)
                            .replace(/@ID_Fee/gi, feeitem.ID_FeeReportMerger)
                            .replace(/@ExamDoctorName/gi, feeitem.ExamDoctorName)
                            .replace(/@TypistName/gi, "&nbsp;")
                            .replace(/@ReportTime/gi, "&nbsp;")
                            .replace(/@FeeCode/gi, feeitem.FeeCode)
                            .replace(/@ApplyID/gi, ApplyID) // 申请号
                            .replace(/@ReportStateBg/gi, "Single_list_table_Unfinished") // 标记未完成状态
                            ;
                        return false;
                    }
                }
                if (tempContent != "") { return false; }
            });

            return tempContent;
        }
        /// <summary>
        /// 显示分单详情
        /// </summary>
        /// <param name="ID_Fee">收费项目ID</param>
        function ShowExamSingleDetail(ID_ShowMergerFee, ApplyID, isFirst) {
            
            if (jQuery("#ID_CurrShowMergerFee").val() == ID_ShowMergerFee && jQuery("#ID_CurrShowApply").val() == ApplyID) { return; }
            if (isFirst == 1) {
                SwitchExamSingleDetail(ID_ShowMergerFee, ApplyID);
                return;
            }
            var tmpVerifyValueIsChange = VerifyValueIsChange();
            // 如果数据进行了变动，则自动进行保存。
            if (tmpVerifyValueIsChange == 1) {
                var tipscontent = "当前分单数据进行了改动，请确认是否进行保留！<br/>【保留】点击后任然停留在当前页面！<br/>【放弃】点击后放弃当前修改的值！";

                art.dialog({
                    id: 'artDialogID',
                    content: tipscontent,
                    lock: true,
                    fixed: true,
                    zIndex: 555,
                    opacity: 0.3,
                    button: [{
                        name: '保留',
                        title: '提示',
                        callback: function () {
                            return true;
                        }, focus: true
                    }, {
                        name: '放弃',
                        callback: function () {

                            SwitchExamSingleDetail(ID_ShowMergerFee, ApplyID);
                            return true;
                        }
                    }],
                    close: function () {
                        return true;
                    }
                });

            } else {
                SwitchExamSingleDetail(ID_ShowMergerFee, ApplyID);
            }

        }

        /// <summary>
        /// 显示分单详情
        /// </summary>
        /// <param name="ID_Fee">收费项目ID</param>
        function SwitchExamSingleDetail(ID_ShowMergerFee, ApplyID) {
            // 设置当前要显示的合并后的收费项目ID
            jQuery("#ID_CurrShowMergerFee").val(ID_ShowMergerFee);
            jQuery("#ID_CurrShowApply").val(ApplyID);
            jQuery(".SubReportList").removeClass("Single_list_table_click");
            jQuery("#SectionExamSingle_" + ID_ShowMergerFee + "_" + ApplyID).addClass("Single_list_table_click");
            jQuery(".Single_Content_title").html(jQuery("#SectionExamSingle_" + ID_ShowMergerFee + "_" + ApplyID ).attr("title"));

            // 显示正在加载的提示信息
            var SectionFeeListLoadingTempleteContent = jQuery('#SectionFeeListLoadingTemplete').html();             //检查项目其它行 模版
            if (SectionFeeListLoadingTempleteContent != undefined) {
                jQuery('#CustExamList').html(SectionFeeListLoadingTempleteContent);
            }
            ShowQuickSelectUser("", ""); // 清空检查医生

            setTimeout(function () {
                InitCustomExamItemPage(gPageCustomExamDataMsg);
            }, 10);

            if (jQuery("#SectionExamSingle_" + ID_ShowMergerFee + "_" + ApplyID).attr("applyid") == "") {
                ShowSystemDialog("该项目还未分单，请配置分单参数并核对后再录入!");
            }
        }


        /// <summary>
        /// 保存科室小结（审核） 
        /// </summary>
        /// <param name="state">1：审核 0：解除审核</param>
        function UpdateSectionSummaryCheckState(state) {
            // 将按钮设置为不可操作
            CtrlButtonDisabled(state, false);
            var ID_CustExamSection = jQuery("#ID_CustExamSection").val();   // 科室小结编号
            var statemsg = "提交";
            if (state == 0) { statemsg = "取回"; }

            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxCustExam.aspx",
                data: { ID_CustExamSection: ID_CustExamSection,
                    SummaryCheckState: state,
                    action: 'UpdateSectionSummaryCheckState',
                    currenttime: encodeURIComponent(new Date())
                },
                cache: false,
                dataType: "text",
                success: function (jsonmsg) {

                    // 检查Ajax返回数据的状态等 20140430 by wtang 
                    jsonmsg = CheckAjaxReturnDataInfo(jsonmsg);
                    if (jsonmsg == null || jsonmsg == "") {
                        return;
                    }

                    if (jsonmsg == "0") {
                        ShowSystemDialog(statemsg + "小结信息失败，请与技术人员联系!");

                    }
                    else {

                        ShowSystemDialogAutoClose(statemsg + "小结信息成功!", jQuery("#txtCustomerID"));

                        // 从新查询刚刚保存的数据 20150519 by wtang
                        ReLoadCustomerExamLastSaveData();
                    }
                }
            });
        }




        /// <summary>
        /// 初始化按钮是否可以使用
        /// </summary>
        function InitButtomDisabled() {

            if (jQuery("#IS_InitButtom").val() == "True") { return; }

            var isInitButton = 0; // 是否已经初始化按钮


            // 如果分科检查已经提交，则禁用其他按钮
            if (jQuery("#Is_SectionCheck").val() == "True") {
                CtrlButtonDisabled(1, false);
                isInitButton = 1; // 是否已经初始化按钮
            }

            // 分科锁定
            if (jQuery('#Is_SectionLock').val() == "True") {

                CtrlButtonDisabled(3, false); //禁用所有操作按钮
                isInitButton = 1; // 是否已经初始化按钮
                art.dialog({
                    lock: true,
                    id: 'artDialogID',
                    zIndex: 500,
                    fixed: true,
                    opacity: 0.3,
                    content: '【提示】该客户已被分科锁定，如果需修改，请先解除分科锁定！',
                    button: [{
                        name: '确定',
                        callback: function () {
                            jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                        }
                    }],
                    close: function () {
                        jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                    }

                });
            }
            // 分科弃检
            if (jQuery('#Is_GiveUp').val() == "True") {

                CtrlButtonDisabled(3, false); //禁用所有操作按钮
                isInitButton = 1; // 是否已经初始化按钮
                art.dialog({
                    lock: true,
                    id: 'artDialogID',
                    zIndex: 500,
                    fixed: true,
                    opacity: 0.3,
                    content: '【提示】该客户已弃检，请恢复该科室的检查后再做分科检查！',
                    button: [{
                        name: '确定',
                        callback: function () {
                            jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                        }
                    }],
                    close: function () {
                        jQuery("#txtCustomerID").focus();  // 设置选中文本框中的文字，并获取光标
                    }
                });
            }
            if (isInitButton == 1) {
                // 设置为已经初始化按钮
                jQuery("#IS_InitButtom").val("True");
            }
            
            if (isInitButton == 0) {
                CtrlButtonDisabled(0, true); //全部按钮可以使用  20150427 by wtang 
            }
        }



        /// <summary>
        /// 控制按钮是否能操作
        /// </summary>
        /// <param name="type">0：解除审核 1：审核 2：保存 3:全部禁用 </param>
        /// <param name="flag">0：解除审核 1：审核 </param>
        function CtrlButtonDisabled(type, flag) {

            if (flag == false) {
                switch (type) {
                    case 0:
                        jQuery("#ButDefault").removeAttr("disabled");
                        jQuery("#ButClear").removeAttr("disabled");
                        jQuery("#ButCollect").removeAttr("disabled");
                        jQuery("#ButSave").removeAttr("disabled");
                        jQuery("#ButCheck").removeAttr("disabled");
                        jQuery("#ButUnCheck").removeAttr("disabled");

                        jQuery("#ButCheck_Ex").show();           // 【提交】按钮 可用
                        jQuery("#ButCheck_Disable_Ex").hide();
                        jQuery("#ButUnCheck_Ex").show();         // 【取回】按钮 可用
                        jQuery("#ButUnCheck_Disable_Ex").hide();

                        break;
                    case 1:
                        jQuery("#ButDefault").attr("disabled", "disabled");
                        jQuery("#ButClear").attr("disabled", "disabled");
                        jQuery("#ButCollect").attr("disabled", "disabled");
                        jQuery("#ButSave").attr("disabled", "disabled");
                        jQuery("#ButCheck").attr("disabled", "disabled");
                        jQuery("#ButUnCheck").removeAttr("disabled");


                        jQuery("#ButCheck_Ex").hide();           // 【提交】按钮 不可用
                        jQuery("#ButCheck_Disable_Ex").show();
                        jQuery("#ButUnCheck_Ex").show();         // 【取回】按钮 可用
                        jQuery("#ButUnCheck_Disable_Ex").hide();
                        
                        break;
                    case 2:
                        jQuery("#ButDefault").attr("disabled", "disabled");
                        jQuery("#ButClear").attr("disabled", "disabled");
                        jQuery("#ButCollect").attr("disabled", "disabled");
                        jQuery("#ButSave").attr("disabled", "disabled");
                        jQuery("#ButCheck").removeAttr("disabled");
                        jQuery("#ButUnCheck").removeAttr("disabled");

                        jQuery("#ButCheck_Ex").show();           // 【提交】按钮 可用
                        jQuery("#ButCheck_Disable_Ex").hide();
                        jQuery("#ButUnCheck_Ex").show();         // 【取回】按钮 可用
                        jQuery("#ButUnCheck_Disable_Ex").hide();
                        break;
                    case 3:
                        jQuery("#ButDefault").attr("disabled", "disabled");
                        jQuery("#ButClear").attr("disabled", "disabled");
                        jQuery("#ButCollect").attr("disabled", "disabled");
                        jQuery("#ButSave").attr("disabled", "disabled");
                        jQuery("#ButUnCheck").attr("disabled", "disabled");
                        jQuery("#ButCheck").attr("disabled", "disabled");


                        jQuery("#ButCheck_Ex").hide();           // 【提交】按钮 不可用
                        jQuery("#ButCheck_Disable_Ex").show();
                        jQuery("#ButUnCheck_Ex").hide();         // 【取回】按钮 不可用
                        jQuery("#ButUnCheck_Disable_Ex").show();

                        break;
                }

            } else {
                jQuery("#ButDefault").removeAttr("disabled");
                jQuery("#ButClear").removeAttr("disabled");
                jQuery("#ButCollect").removeAttr("disabled");
                jQuery("#ButSave").removeAttr("disabled");
                jQuery("#ButCheck").removeAttr("disabled");
                jQuery("#ButUnCheck").removeAttr("disabled");

                jQuery("#ButCheck_Ex").show();           // 【提交】按钮 可用
                jQuery("#ButCheck_Disable_Ex").hide();
                jQuery("#ButUnCheck_Ex").show();         // 【取回】按钮 可用
                jQuery("#ButUnCheck_Disable_Ex").hide();
            }
        }



        /// <summary>
        /// 清除科室小结 及 删除检查明细值 20131112 by wtang
        /// </summary>
        function DeleteCustomerExamItemConfirm() {


            var ID_MergerFee = jQuery("#ID_CurrShowMergerFee").val();
            var ID_ShowApply = jQuery("#ID_CurrShowApply").val();
            var ApplyID = jQuery("#SectionExamSingle_" + ID_MergerFee + "_" + ID_ShowApply).attr("applyid");
//            if (ApplyID == "") {
//                ShowSystemDialog("该项目还未分单，请配置分单参数并核对后再录入!");
//                return;
//            }


            var tipscontent = "您确定要【清除】当前的检查信息吗？<br/>清除后，检查明细数据将无法恢复！！";
            art.dialog({
                id: 'artDialogIDcfm',
                content: tipscontent,
                lock: true,
                zIndex: 500,
                fixed: true,
                title: '清除检查信息提示',
                opacity: 0.3,
                button: [{
                    name: '确定清除',
                    callback: function () {
                        DeleteCustomerExamItem();
                        return true;
                    }, focus: true
                }, {
                    name: '取消'
                }]
            });
        }

        /// <summary>
        /// 清除科室小结 及 删除检查明细值 20150427 by wtang
        /// </summary>
        function DeleteCustomerExamItem() {

            var ID_MergerFee = jQuery("#ID_CurrShowMergerFee").val();
            var ID_ShowApply = jQuery("#ID_CurrShowApply").val();
            var ApplyID = jQuery("#SectionExamSingle_" + ID_MergerFee + "_" + ID_ShowApply).attr("applyid");
//            if ( ApplyID  == "") {
//                ShowSystemDialog("该项目还未分单，请配置分单参数并核对后再录入!");
//                return;
//            }

            var CustomerID = jQuery.trim(jQuery('#txtHiddenCustomerID').val());
            var SectionID = jQuery('#txtSectionID').val();

            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxCustExam.aspx",
                data: { CustomerID: CustomerID,
                    SectionID: SectionID,
                    ID_MergerFee: ID_MergerFee,
                    ApplyID: ApplyID,
                    InterfaceType: "Lab",
                    action: 'DeleteCustomerExamItem_LAB',
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
                    if (jsonmsg != "" && parseInt(jsonmsg) >= 0) {
                        jQuery("#SectionExamSingle_" + jQuery("#ID_CurrShowMergerFee").val()+"_" + jQuery("#ID_CurrShowApply").val()).addClass("Single_list_table_Unfinished"); // 设置为已经完成的状态
                        RemoveSelectedUser();
                        InitCustomExamItemPage_IsSetDefaultValue(gPageCustomExamDataMsg, 0);
                        ShowSystemDialogAutoClose("清除成功！", jQuery("#txtCustomerID"));
                        ReLoadCustomerExamLastSaveData(); // 从新查询上次保存的数据  20150519 by wtang 
                    } else if (jsonmsg == "-2") {

                        ShowSystemDialog("数据清除成功，但接口中的状态重置失败，请联系开发人员！");
                    } else {
                        ShowSystemDialog("清除失败，请联系开发人员！");
                    }
                }
            });
        }

        /*************键盘操作事件：主要为上下左右键，适用于table******************/
        function LabExamTableKeyMove_Mult(item, event, CurrCellIndex) {

            var elementID = "CustExamList";
            var maxCellsIndex = 8; //  document.getElementById(elementID).rows[0].cells.length -1 ;   //计算表格有列数下标最大值
            var maxRowsIndex = document.getElementById(elementID).rows.length - 1;            // 计算表格行数下标最大值

            var objTable = document.getElementById(elementID); 				      // 获取table
            var currrow = item.parentNode; 								      // 获取当前行
            while (currrow.tagName != "TR") currrow = currrow.parentNode;
            var CurrRowIndex = currrow.rowIndex;    // 获取当前行的下标
            
            //var CurrCellIndex = 2;                  // 获取当前列的下标 （固定为第一列）
            var nextCellIndex = CurrCellIndex;
            var nextRowIndex = CurrRowIndex;
            // 下
            if (event.keyCode == 40) {
                if (objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].tagName == "INPUT") {
                    if (nextRowIndex < maxRowsIndex) {
                        nextRowIndex += 1;
                    }
                }
            }
            // 上
            else if (event.keyCode == 38) {
                if (objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].tagName == "INPUT") {
                    if (nextRowIndex >= 1) {
                        nextRowIndex -= 1;
                    }
                }
            }
            // 右
            else if (event.keyCode == 39) {
                if (nextCellIndex >= maxCellsIndex) {
                    nextCellIndex = maxCellsIndex;
                } else {
                    nextCellIndex += 1;
                }
            }
            // 左
            else if (event.keyCode == 37) {
                if (nextCellIndex <= 2) {
                    nextCellIndex = 2;
                } else {
                    nextCellIndex -= 1;
                }
            }
            else if (event.keyCode == 13) {
                // 否则，回车 下移一行
                if (nextRowIndex < maxRowsIndex) {
                    nextRowIndex += 1;
                }
            } else { return; }
            if (objTable.rows[nextRowIndex].style.display == "none")
                return;
            if (objTable.rows[nextRowIndex].cells[nextCellIndex] != undefined) {
                var tempcellindex = nextCellIndex;
                if (objTable.rows[nextRowIndex].cells[nextCellIndex].children[0] != undefined) {

                    if (objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].tagName == "INPUT" ||
                        objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].tagName == "SELECT") {
                        tempcellindex = nextCellIndex;
                    } else {
                        tempcellindex = nextCellIndex - 1;
                    }

                    objTable.rows[nextRowIndex].cells[tempcellindex].children[0].blur();
                    objTable.rows[nextRowIndex].cells[tempcellindex].children[0].focus();

                }
            }
            return;
        }

        /*************键盘操作事件：主要为上下左右键，适用于table******************/
        function LabExamTableKeyMove2(item, event) {

            var elementID = "CustExamList";
            var maxCellsIndex = 5; //  document.getElementById(elementID).rows[0].cells.length -1 ;   //计算表格有列数下标最大值
            var maxRowsIndex = document.getElementById(elementID).rows.length - 1;            // 计算表格行数下标最大值
            var objTable = document.getElementById(elementID); 				      // 获取table
            var currrow = item.parentNode; 								      // 获取当前行
            while (currrow.tagName != "TR") currrow = currrow.parentNode;
            var CurrRowIndex = currrow.rowIndex;    // 获取当前行的下标
            var CurrCellIndex = 5;                  // 获取当前列的下标 
            var nextCellIndex = CurrCellIndex;
            var nextRowIndex = CurrRowIndex;
            if (event.keyCode == 39) {
                if (nextCellIndex == 2) {
                    nextCellIndex = 5;
                } else {
                    return;
                }
            }
            else if (event.keyCode == 37) {
                if (nextCellIndex == 5) {
                    nextCellIndex = 2;
                } else {
                    return;
                }
            }
            else if (event.keyCode == 13) {
                // 否则，回车 下移一行
                if (nextRowIndex < maxRowsIndex) {
                    nextRowIndex += 1;
                }
            } else { return; }

            if (objTable.rows[nextRowIndex].style.display == "none")
                return;
            if (objTable.rows[nextRowIndex].cells[nextCellIndex] != undefined) {
                var tempcellindex = nextCellIndex;
                if (objTable.rows[nextRowIndex].cells[nextCellIndex].children[0] != undefined) {

                    if (objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].tagName == "INPUT" ||
                        objTable.rows[nextRowIndex].cells[nextCellIndex].children[0].tagName == "SELECT") {
                        tempcellindex = nextCellIndex;
                    } else {
                        tempcellindex = nextCellIndex - 1;
                    }

                    objTable.rows[nextRowIndex].cells[tempcellindex].children[0].blur();
                    objTable.rows[nextRowIndex].cells[tempcellindex].children[0].focus();

                }
            }
            return;
        }



























































































        /// <summary>
        /// 查询上次保存的数据
        /// </summary>
        function GetCustomerExamLastSaveData() {
            if (gPageCustomExamLastSaveDataMsg == "") {
                var CustomerID = jQuery.trim(jQuery('#txtHiddenCustomerID').val());
                var SectionID = jQuery('#txtSectionID').val();

                jQuery.ajax({
                    type: "POST",
                    url: "/Ajax/AjaxCustExam.aspx",
                    data: { CustomerID: CustomerID,
                        SectionID: SectionID,
                        InterfaceType: "Lab",
                        action: 'GetCustomerExamLastSaveDataList',
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


                        gPageCustomExamLastSaveDataMsg = jsonmsg;
                        // 加载小结的内容,及各个检查项目的值
                        InitCustomExamLastSaveData();
                    }
                });
            } else {
                // 加载小结的内容,及各个检查项目的值
                InitCustomExamLastSaveData();
            }
        }


        /// <summary>
        /// 从新查询上次保存的数据
        /// </summary>
        function ReLoadCustomerExamLastSaveData() {

            // 重新获取检查数据
            var CustomerID = jQuery.trim(jQuery('#txtHiddenCustomerID').val());
            var SectionID = jQuery('#txtSectionID').val();
            var ID_Gender = jQuery('#txtGenderID').val();
            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxCustExam.aspx",
                data: { CustomerID: CustomerID,
                    SectionID: SectionID,
                    InterfaceType: "lab",
                    ID_Gender: ID_Gender,
                    action: 'GetCustomerExamDetailDataList',
                    currenttime: encodeURIComponent(new Date())
                },
                cache: false,
                dataType: "json",
                success: function (jsonmsg) {
                    gPageCustomExamDataMsg = jsonmsg;
                }
            });

            // 从新获取保存的值
            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxCustExam.aspx",
                data: { CustomerID: CustomerID,
                    SectionID: SectionID,
                    InterfaceType: "Lab",
                    action: 'GetCustomerExamLastSaveDataList',
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
                    gPageCustomExamLastSaveDataMsg = jsonmsg;
                }
            });

        }

        /// <summary>
        /// 根据获得的数据，初始化页面显示的内容
        /// </summary>
        /// <param name="msg">该客户的收费项目，体检项目，及对应的特征词数据</param>
        function InitCustomExamItemPage(msg) {

            // 默认需要初始化默认值
            InitCustomExamItemPage_IsSetDefaultValue(msg, 1);
        }


        /// <summary>
        /// 根据获得的数据，初始化页面显示的内容
        /// </summary>
        /// <param name="msg">该客户的收费项目，体检项目，及对应的特征词数据</param>
        /// <param name="_IsSetDefaultValue">用于标记是否加载默认选项的信息ID 1:加载默认值 0：不加载默认选项 </param>
        function InitCustomExamItemPage_IsSetDefaultValue(msg, _IsSetDefaultValue) {

            // 合并后的收费项目ID 20150417 by wtang 
            var ID_ShowMergerFee = jQuery("#ID_CurrShowMergerFee").val();

            jQuery("#SectionSummary").val("");           // 科室小结 
            jQuery("#Last_SectionSummary").val("");      // 科室小结 同时保存到备用文本中，用于对比是否进行了改动

            jQuery("#PositiveSummary").val("");         // 阳性结果
            jQuery("#Last_PositiveSummary").val("");    // 阳性结果 同时保存到备用文本中，用于对比是否进行了改动

            var totalInputCountNum = 0; // 总的输入框数量，输入框编号
            var NoFeeItemTempleteContent = jQuery('#NoFeeItemTemplete').html(); //暂无交费项目或体检项目模版

            // 将是否加载默认选项的值设置到隐藏变量中
            jQuery("#IsSetDefaultValue").val(_IsSetDefaultValue);
            var FeeItemCount = 0;  // 记录收费项目的个数
            if (msg == null || msg == "" || parseInt(msg.totalCount) <= 0) {

                jQuery('#TipsMessage').html(NoFeeItemTempleteContent);             //显示没有收费项目的提示信息
                jQuery('#TipsArea').show();
                return;
            }
            else if (msg != null && msg != "" && parseInt(msg.totalCount) > 0) {

                var newcontent = "";

                // 从模版中读取数据加载列表
                var feeIndexTempleteContent = jQuery('#FeeIndexTemplete').html();   //收费项目模版
                var feeTemplateContent = jQuery('#FeeDataListTemplete').html();             //收费项目模版
                var examTemplateContent = jQuery('#ExamDataListTemplete').html();           //体检项目模版

                var tempFeeItemAllCountent = ""; // 收费项目所有信息
                var SectionFeeListTempleteContent = jQuery('#SectionFeeListTemplete').html();                 //科室收费列表 模版
                var ExamItemHeaderTempleteContent = jQuery('#ExamItemHeaderTemplete').html();                 //检查项目表头
                var ExamItemFirstRowTempleteContent = jQuery('#ExamItemFirstRowTemplete').html();             //收费项目-检查项目第一行 模版
                var ExamItemOtherRowTempleteContent = jQuery('#ExamItemOtherRowTemplete').html();             //检查项目其它行 模版

                var SectionSummaryTextTempleteContent = jQuery('#SectionSummaryTextTemplete').html();               //科室小结模版
                var ExamDoctorSelectTempleteContent = jQuery('#ExamDoctorSelectTemplete').html();                   //检查医生模版
                var ExamOperatorButtonTempleteContent = jQuery('#ExamOperatorButtonTemplete').html();               //操作按钮模版

                var CurrFeeItemID = 0;      // 在加载检查项目使用
                var CurrExamItemID = 0;     // 在加载体征词时使用
                var CurrExamItemCount = 0;  // 当前检查项目的编号 在循环加载检查项目时，用于计数，计数需要合并的行数

                var TempExamItemContent = ""; // 临时体检项目内容

                var TempFeeListIndex = "";
                var tempFeeListCountent = ""; // 收费项目列表内容


                newcontent = ""; //  ExamItemHeaderTempleteContent; 表头已经在模板外定义 20140404 by wtang

//                var isLoadDoctorName = 0;
//                // 加载检验科的分单 检查医生，录入医生
//                jQuery(msg.dataList4).each(function (i, applyitem) {
//                    var applyid = jQuery("#SectionExamSingle_" + ID_ShowMergerFee).attr("applyid");
//                    if (applyid == applyitem.ID_Apply && applyitem.DetectionDoctorName != "") {
//                        isLoadDoctorName = 1;
//                        ShowQuickSelectUser(applyitem.ID_DetectionDoctor, applyitem.DetectionDoctorName); // 如果已经保存过，则加载保存后的检查医生
//                    }
//                });
//                if (isLoadDoctorName == 0) {
//                    ShowQuickSelectUser("", ""); // 如果已经保存过，则加载保存后的检查医生
//                }
                // 加载收费项目
                jQuery(msg.dataList0).each(function (i, feeitem) {

                    if (ID_ShowMergerFee != feeitem.ID_FeeReportMerger) {
                        return true; // continue;
                    }

                    TempFeeListIndex +=
                             feeIndexTempleteContent.replace(/@FeeName/gi, feeitem.FeeName)
                            .replace(/@ID_Fee/gi, feeitem.ID_Fee)
                            .replace(/@ID_CustFee/gi, feeitem.ID_CustFee)
                            ;

                    FeeItemCount++;  // 记录收费项目的个数

                    // 收费项目列表
                    tempFeeListCountent +=
                             SectionFeeListTempleteContent.replace(/@FeeName/gi, feeitem.FeeName)
                            .replace(/@ID_Fee/gi, feeitem.ID_Fee)
                            .replace(/@ID_CustFee/gi, feeitem.ID_CustFee)
                            ;

                    tempFeeItemAllCountent = ""; // 一个收费项目开始
                    // 先拼接第一个检查项目的模版
                    tempFeeItemAllCountent +=
                             ExamItemOtherRowTempleteContent.replace(/@FeeName/gi, feeitem.FeeName)
                            .replace(/@ID_Fee/gi, feeitem.ID_Fee)
                            .replace(/@ID_CustFee/gi, feeitem.ID_CustFee)
                            ;
                    CurrFeeItemID = 0; //在加载检查项目使用
                    CurrExamItemCount = 0; // 从零开始计数

                    // 加载检查项目列表
                    jQuery(msg.dataList1).each(function (j, examitem) {

                        // 这里可以判断出是否已经加载完成，如果已经加载完，则跳出循环
                        if (CurrFeeItemID > 0 && CurrFeeItemID != examitem.ID_Fee) {
                            return false;
                        }

                        if (feeitem.ID_Fee == examitem.ID_Fee) {

                            CurrFeeItemID = examitem.ID_Fee; //如果进入了这个循环

                            var ExamItemSymptom = "";
                            //if (examitem.GetResultWay != "N") { // GetResultWay 对检验科不起作用 20130717 by WTang

                            CurrExamItemCount++; // 当前检查项目的编号 在循环加载检查项目时，用于计数，计数需要合并的行数

                            totalInputCountNum = totalInputCountNum + 1; // 输入框编号+1
                            if (CurrExamItemCount == 1) {
                                tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@FeeExamItemID/gi, examitem.ID_Fee + "_" + examitem.ID_ExamItem)
                                    .replace(/@ExamItemID/gi, examitem.ID_ExamItem)
                                    .replace(/@ExamItemName/gi, examitem.ExamItemName)
                                    .replace(/@AbbrExamName/gi, examitem.AbbrExamName == '' ? '&nbsp;' : examitem.AbbrExamName)
                                    .replace(/@DetectionMethod/gi, '')
                                    .replace(/@SCO/gi, '')
                                    .replace(/@FeeID/gi, examitem.ID_Fee)
                                    .replace(/@ID_ExamItem/gi, examitem.ID_ExamItem)
                                    .replace(/@Is_EntrySectSum/gi, examitem.Is_EntrySectSum)
                                    .replace(/@Is_LisValueNull/gi, examitem.Is_LisValueNull)
                                    .replace(/@ExamItemBg/gi, examitem.Is_LisValueNull == 'False' ? '' : 'notmustinputbg')
                                    .replace(/@InputTextExamItemUnit/gi, examitem.ExamItemUnit == 'NULL' ? '' : examitem.ExamItemUnit)
                                    .replace(/@tabindex/gi, totalInputCountNum)
                                    .replace(/@LoLimit/gi, '')
                                    .replace(/@HiLimit/gi, '')

                                // 20130929 by WTang 屏蔽高值低值
                                //.replace(/@LoLimit/gi, jQuery('#GenderNameText').html() == "男" ? examitem.MaleLoLimit : examitem.FemaleLoLimit)
                                //.replace(/@HiLimit/gi, jQuery('#GenderNameText').html() == "男" ? examitem.MaleHiLimit : examitem.FemaleHiLimit)
                                    ;

                            } else {
                                tempFeeItemAllCountent += ExamItemOtherRowTempleteContent.replace(/@FeeExamItemID/gi, examitem.ID_Fee + "_" + examitem.ID_ExamItem)
                                    .replace(/@ExamItemID/gi, examitem.ID_ExamItem)
                                    .replace(/@ExamItemName/gi, examitem.ExamItemName)
                                    .replace(/@AbbrExamName/gi, examitem.AbbrExamName == '' ? '&nbsp;' : examitem.AbbrExamName)
                                    .replace(/@DetectionMethod/gi, '')
                                    .replace(/@SCO/gi, '')
                                    .replace(/@FeeID/gi, examitem.ID_Fee)
                                    .replace(/@ID_ExamItem/gi, examitem.ID_ExamItem)
                                    .replace(/@Is_EntrySectSum/gi, examitem.Is_EntrySectSum)
                                    .replace(/@Is_LisValueNull/gi, examitem.Is_LisValueNull)
                                    .replace(/@ExamItemBg/gi, examitem.Is_LisValueNull == 'False' ? '' : 'notmustinputbg')
                                    .replace(/@InputTextExamItemUnit/gi, examitem.ExamItemUnit == 'NULL' ? '' : examitem.ExamItemUnit)
                                    .replace(/@tabindex/gi, totalInputCountNum)
                                    .replace(/@LoLimit/gi, '')
                                    .replace(/@HiLimit/gi, '')

                                // 20130929 by WTang 屏蔽高值低值
                                //.replace(/@LoLimit/gi, jQuery('#GenderNameText').html() == "男" ? examitem.MaleLoLimit : examitem.FemaleLoLimit)
                                //.replace(/@HiLimit/gi, jQuery('#GenderNameText').html() == "男" ? examitem.MaleHiLimit : examitem.FemaleHiLimit)
                                    ;
                            }

                            if (feeitem.ID_ExamDoctor != "") {
                                // 读取检查医生模版（每个体检项目对应一个体检医生）
                                tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@FeeItemID/gi, feeitem.ID_Fee)
                                        .replace(/@ExamDoctorName/gi, feeitem.ExamDoctorName)
                                        .replace(/@ExamDoctorID/gi, feeitem.ID_ExamDoctor);
                            } else {
                                // 读取检查医生模版（每个体检项目对应一个体检医生）
                                tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@FeeItemID/gi, feeitem.ID_Fee)
                                        .replace(/@ExamDoctorName/gi, "--")
                                        .replace(/@ExamDoctorID/gi, "0");
                            }

                            // 读取检查医生模版（每个检查项目对应一个体检医生）
                            tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@ExamItemDoctorName/gi, "--")
                                    .replace(/@ExamItemDoctorID/gi, "0").replace(/@ExamItemDate/gi, "--");

                            // 20130929 by WTang 屏蔽高值低值
                            tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@LimitText/gi, "");
                            //                            if (jQuery('#GenderNameText').html() == "男") {
                            //                                if (examitem.MaleLoLimit != "" && examitem.MaleHiLimit != "") {
                            //                                    tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@LimitText/gi, (examitem.MaleLoLimit + " -- " + examitem.MaleHiLimit));
                            //                                } else {
                            //                                    tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@LimitText/gi, "");
                            //                                }
                            //                            } else {
                            //                                if (examitem.FemaleLoLimit != "" && examitem.FemaleHiLimit != "") {
                            //                                    tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@LimitText/gi, (examitem.FemaleLoLimit + " -- " + examitem.FemaleHiLimit));
                            //                                } else {
                            //                                    tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@LimitText/gi, "");
                            //                                }
                            //                            }
                            // }  // GetResultWay 对检验科不起作用 20130717 by WTang
                        }
                    }); // 加载检查项目 end
                    // 合并收费项目的行数，并进行拼接
                    if (CurrExamItemCount >= 1) {
                        tempFeeItemAllCountent = tempFeeItemAllCountent.replace(/@FeeItemRowSpan/gi, CurrExamItemCount);
                        newcontent += tempFeeItemAllCountent;
                    }

                });                        // 加载收费项目 end

                if (FeeItemCount > 0) {
//                    // 读取科室小结模版
//                    newcontent += SectionSummaryTextTempleteContent.replace(/@SectionSummaryArea/gi, "SectionSummary").replace(/@PositiveSummaryArea/gi, "PositiveSummary");

//                    // 读取操作按钮模版
//                    newcontent += ExamOperatorButtonTempleteContent.replace(/@ButDefault/gi, "ButDefault")
//                            .replace(/@ButCollect/gi, "ButCollect")
//                            .replace(/@ButClear/gi, "ButClear")
//                            .replace(/@ButSave/gi, "ButSave")
//                            .replace(/@ButCheck/gi, "ButCheck")
//                            .replace(/@ButUnCheck/gi, "ButUnCheck");

                } else {
                    jQuery('#TipsMessage').html(NoFeeItemTempleteContent);             //显示没有收费项目的提示信息
                    jQuery('#TipsArea').show();
                }
                
                // 收费项目列表
                if(tempFeeListCountent != "")
                {
                    jQuery('#FeeListCountent').html(tempFeeListCountent); 
                }

                if (FeeItemCount > 0 && newcontent != '') {

                    // 暂时先不显示导航的收费项目
                    //                    TempFeeListIndex = "<ul>" + TempFeeListIndex + "</ul>";
                    //                    newcontent = newcontent + TempFeeListIndex;

                    jQuery('#TipsArea').hide();
                    jQuery('#CustExamList').html(newcontent);
                    //jQuery('#CustExamResult').show()


                    jQuery(".ClassLabSectionTextInput").attr("maxlength","60");

                    jQuery(".j-autoHeight").autoHeight(); // 自适应高度


                    // 读取科室小结的信息（小结的时间，和小结的编号）
                    jQuery(msg.dataList2).each(function (m, sectionsummaryitem) {
                        jQuery("#ID_CustExamSection").val(sectionsummaryitem.ID_CustExamSection);


                        jQuery("#SectionSummaryDate").val(sectionsummaryitem.SectionSummaryDate);   //    小结时间
                        jQuery("#ID_SummaryDoctor").val(sectionsummaryitem.ID_SummaryDoctor);       //    医生编号
                        jQuery("#SummaryDoctorName").val(sectionsummaryitem.SummaryDoctorName);     //    医生姓名
                        jQuery("#ID_Typist").val(sectionsummaryitem.ID_Typist);     //    录入人编号
                        jQuery("#TypistName").val(sectionsummaryitem.TypistName);   //    录入人姓名
                        jQuery("#TypistDate").val(sectionsummaryitem.TypistDate);   //    录入时间

                        // 在首次进入分科检查的情况下，进行绑定
                        if (sectionsummaryitem.SummaryDoctorName == "" && sectionsummaryitem.TypistName == "") {

                            // 绑定用户，绑定后只能由该医生或护士进行体检，如果需要其他人员操作，需要进行解除绑定操作 (绑定)
                            // BandingCustomerSectionExamInfo();
                        }

                        // 对于检验科数据，不是由一个人进行数据的全部录入，所以这里不能显示由某人绑定。（20130717 BY WTang ）

                        //                        if (sectionsummaryitem.SummaryDoctorName == "" && sectionsummaryitem.TypistName != "") {
                        //                            if (sectionsummaryitem.ID_Typist != jQuery("#HiddenUserID").val()) {
                        //                                
                        //                                art.dialog({
                        //                                    content: '【提示】该客户已被(' + sectionsummaryitem.TypistName + ')绑定，只能由(' + sectionsummaryitem.TypistName + ')进行操作！<br/>【备注】如果，需要由其他人员操作，请先解除绑定！',
                        //                                    button: [{
                        //                                        name: '确定'
                        //                                    }]
                        //                                });
                        //                            }
                        //                        }

                    });

                    // 根据获得的数据，初始化页面隐藏域中的默认值 , 用于对比是否全部是默认小结
                    // SetCustomExamHiddenDefaultText(msg);

                    // 初始化 体征词相关事件 文本变化事件（change,keyup） 多选框点击事件 (click) 单选框点击事件(click)
                    InitSymptomEvent(msg);

                    // 初始化下拉列表框
                    // jQuery('.content select').select2();


                    //                    // 显示医生下拉框中的默认值  这里需要 初始化下拉列表框 后在赋值
                    //                    jQuery(msg.dataList3).each(function (m, item03) {
                    //                        jQuery("#ResultSummaryDoctor").find("option").removeAttr("selected");
                    //                        jQuery("#ResultSummaryDoctor [value='" + item03.ID_SummaryDoctor + "'").attr("selected", true); // 医生编号
                    //                        jQuery("#s2id_ResultSummaryDoctor .select2-choice span").text(item03.SummaryDoctorName); //显示选中项的文本
                    //                    });

                    // 初始化按钮是否可以使用 , （在页面元素加载完成后，调用一次按钮控制函数）
                    // InitButtomDisabled(); 20150427 by wtang
                }
            } else {
                jQuery('#TipsMessage').html(NoFeeItemTempleteContent);             //显示没有收费项目的提示信息
                jQuery('#TipsArea').show();
            }

            // 判断表格是否存在滚动条,并设置相应的样式
            //JudgeTableIsExistScrollByID("autoHeight_001");
        }

        function inputkeyup(e) {
            //jQuery('.ClassLabSectionTextInput').keyup(function (e) {

            // 兼容其他浏览器
            //var CurrEvent = (e) ? e : ((window.event) ? window.event : "");
            var CurrEvent = (window.event) ? window.event : "";
            //event = window.event || e;
            if (CurrEvent.keyCode == 13) {
                CurrEvent.keyCode = 9;
                if (window.event) { window.event.keyCode = 9; }
                //if (e) { window.event.keyCode = 9; }
            }

            SyncShowSelectLoHiLimitResult(e);

            //});
        }

        /// <summary>
        /// 初始化体征词事件
        /// </summary>
        function InitSymptomEvent(msg) {
            // 将体征所在输入框中的值，显示到检查项的结果输入文本框中。

            //            jQuery('.ClassLabSectionTextInput').change(function () {
            //                SyncShowSelectLoHiLimitResult(this);
            //            });
            //            jQuery('.ClassLabSectionTextInput').keyup(function (e) {
            //                
            //                // 兼容其他浏览器
            //                var CurrEvent = (e) ? e : ((window.event) ? window.event : "");
            //                //event = window.event || e;
            //                if (CurrEvent.keyCode == 13) {
            //                    CurrEvent.keyCode = 9;
            //                    if (window.event) { window.event.keyCode = 9; }
            //                    if (e) { window.event.keyCode = 9; }
            //                }

            //                SyncShowSelectLoHiLimitResult(this);

            //            });

            // 0：不绑定默认值 1：绑定默认值（已经小结的情况下，加载小结后的信息） 2：仅绑定默认值
            if (jQuery("#IsSetDefaultValue").val() == 1) {

                // 读取并加载小结的内容,及各个检查项目的值
                GetCustomerExamLastSaveData();
            }

        }


        /// <summary>
        /// 根据获得的数据，初始化页面隐藏域中的默认值
        /// </summary>
        /// <param name="msg">该客户的收费项目，体检项目，及对应的特征词数据</param>
        function SetCustomExamHiddenDefaultText(msg) {

            // 遍历体征词列表
            jQuery(msg.dataList2).each(function (k, symptomitem) {
                if (symptomitem.Is_Default == "True") {
                    jQuery("#txtDefaultValueResult_" + symptomitem.ID_Fee + "_" + symptomitem.ID_ExamItem).val(symptomitem.SymptomName);
                    // ShowSystemDialog(jQuery("#txtDefaultValueResult_" + symptomitem.ID_Fee + "_" + symptomitem.ID_ExamItem).val());
                }
            });
        }


        /// <summary>
        /// 将所在输入框中的值，进过计数后，显示时偏高，偏低还是正常。
        /// </summary>
        /// <param name="obg">操作的文本框对象</param>
        function SyncShowSelectLoHiLimitResult(obg) {

            var tempCurrObj = jQuery("#" + obg.name);
            var resultvalue = "";
            var tempResult = ""; // 结果值 正常 ↑ ↓
            var tempInputValue = tempCurrObj.val();
            var tempParentID = tempCurrObj.attr("parentname");
            var tempexamitemid = tempCurrObj.attr("examitemid");

//            jQuery("#ExamDoctor_" + tempParentID).val(jQuery("#LoginUserID").val());
//            jQuery("#ExamDoctor_" + tempParentID).attr("DoctorName", jQuery("#LoginUserName").val());
//            jQuery("#Lab_ExamDoctor_" + tempParentID).html(jQuery("#LoginUserName").val());

//            jQuery("#ExamItemDoctor_" + tempParentID + "_" + tempexamitemid).val(jQuery("#LoginUserID").val());
//            jQuery("#ExamItemDoctor_" + tempParentID + "_" + tempexamitemid).attr("DoctorName", jQuery("#LoginUserName").val());
//            jQuery("#Lab_ExamItemDoctor_" + tempParentID + "_" + tempexamitemid).html(jQuery("#LoginUserName").val());
//            jQuery("#Lab_ExamItemDate_" + tempParentID + "_" + tempexamitemid).html(jQuery("#CurrentDateToday").val());
            //            
            //                    <input type="hidden" id="ExamDoctor_@FeeItemID" name="ExamDoctor_@FeeItemID" value="@ExamDoctorID" DoctorName="@ExamDoctorName" />
            //                    <span id="Lab_ExamDoctor_@FeeItemID">@ExamDoctorName</span>

            //            // 20130929 by WTang 修改为不进行自动计算偏高，偏低的
            //            return;

            //            if (tempInputValue != "") {
            //                var tempSpanLimitObj = jQuery("#" + obg.name.replace(/txtSym/gi, "spanLimit"));
            //                var tempHiLimit = tempSpanLimitObj.attr("HiLimit");
            //                var tempLoLimit = tempSpanLimitObj.attr("LoLimit");
            //                if (tempHiLimit != "" && tempLoLimit != "") {
            //                    // 非负浮点数（正浮点数 + 0）
            //                    var patrn = regexEnum.decmal4;
            //                    // 判断最小值是否为数字
            //                    if (!tempLoLimit.match(patrn)) {
            //                        return false;
            //                    }
            //                    // 判断最大值是否为数字
            //                    if (!tempHiLimit.match(patrn)) {
            //                        return false;
            //                    }
            //                    // 如果输入值 大于 最大值
            //                    if (parseFloat(tempInputValue) > parseFloat(tempHiLimit)) {
            //                        tempResult = "↑"; // 偏高
            //                    }
            //                    // 如果输入值 小于 最小值
            //                    if (parseFloat(tempInputValue) < parseFloat(tempLoLimit)) {
            //                        tempResult = "↓"; // 偏低
            //                    }
            //                }
            //            }

            //            // 设置下拉的选择情况
            //            var tempCurrSelLoHiLimitResult = jQuery("#" + obg.name.replace(/txtSym/gi, "selLoHiLimitResult"));
            //            tempCurrSelLoHiLimitResult.find("option").removeAttr("selected");
            //            tempCurrSelLoHiLimitResult.attr("value", tempResult);

            tempResult = jQuery("#selLoHiLimitResult_" + tempParentID + "_" + tempexamitemid).val();   // 每个检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”


            var tdRowsName = "";
            if (tempResult != "") {

                tdRowsName = obg.name.replace(/selLoHiLimitResult/gi, "td");

               // jQuery("#" + tdRowsName + "_4").html("1"); // 病症级别

                jQuery("#" + tdRowsName + "_1").attr("Class", "lohilimitbg");
                jQuery("#" + tdRowsName + "_2").attr("Class", "lohilimitbg");
                jQuery("#" + tdRowsName + "_3").attr("Class", "lohilimitbg");
                jQuery("#" + tdRowsName + "_4").attr("Class", "lohilimitbg textcenter");
                jQuery("#" + tdRowsName + "_5").attr("Class", "lohilimitbg");
                jQuery("#" + tdRowsName + "_6").attr("Class", "lohilimitbg");
                jQuery("#" + tdRowsName + "_7").attr("Class", "lohilimitbg");
                jQuery("#" + tdRowsName + "_8").attr("Class", "lohilimitbg");
            } else {
                tdRowsName = obg.name.replace(/selLoHiLimitResult/gi, "td");

                //jQuery("#" + tdRowsName + "_4").html("&nbsp;"); // 病症级别

                jQuery("#" + tdRowsName + "_1").attr("Class", "");
                jQuery("#" + tdRowsName + "_2").attr("Class", "");
                jQuery("#" + tdRowsName + "_3").attr("Class", "");
                jQuery("#" + tdRowsName + "_4").attr("Class", "textcenter");
                jQuery("#" + tdRowsName + "_5").attr("Class", "");
                jQuery("#" + tdRowsName + "_6").attr("Class", "");
                jQuery("#" + tdRowsName + "_7").attr("Class", "");
                jQuery("#" + tdRowsName + "_8").attr("Class", "");
            }

        }

        var SepBetweenExamItems = "，";   //项目分隔符        如：、
        var SepBetweenSymptoms = "、";    //体征词分隔符      如：、
        var TerminalSymbol = "。";        //项目终结符        如：。
        var SepExamAndValue = ":";       //项目小结分隔符     如：：
        var NoBetweenExamItems = "(1)";   //项目序号         如：(1)
        var NoBetweenSympotms = "①";     //体征词序号        如： ①


        /// <summary>
        /// 将体征单选框中的值，显示到检查项的结果输入文本框中。
        /// </summary>
        /// <param name="obj">操作的单选框对象</param>
        function SyncShowSymptomRadioData(obj) {

            var symRadioHidValue = "";
            var symRadioShowText = "";

            jQuery("input[name='" + obj.name + "']:radio:checked").each(function () {
                symRadioHidValue = jQuery(this).val(); //获取ID值
                symRadioShowText = jQuery("#" + this.name.replace(/radioSym/gi, "lab") + "_" + jQuery(this).val()).attr("SymptomName"); // +SepBetweenExamItems; //获取显示的文本
            });

            jQuery("#" + obj.name.replace(/radioSym/gi, "txtHidValueResult")).val(symRadioHidValue);
            jQuery("#" + obj.name.replace(/radioSym/gi, "txtResult")).val(symRadioShowText);

        }

        /// <summary>
        /// 将体征复选框中的值，显示到检查项的结果输入文本框中。
        /// </summary>
        /// <param name="obj">操作的复选框对象</param>
        function SyncShowSymptomCheckboxData(obj) {

            var symchkHidValueList = "";
            var symchkShowTextList = "";
            var symchkPositiveTextList = ""; // 该检查项目的阳性结果

            symchkHidValueList = jQuery("#" + obj.name.replace(/ckbSym/gi, "txtHidValueResult")).val();
            if (symchkHidValueList == "") symchkHidValueList += SepBetweenSymptoms;
            if (jQuery(obj).attr("checked")) {
                symchkHidValueList = symchkHidValueList + jQuery(obj).val() + SepBetweenSymptoms;   // 获取ID值
            }
            else {
                var replaceReg01 = new RegExp(SepBetweenSymptoms + jQuery(obj).val() + SepBetweenSymptoms, "gi"); // 移除value中对应的选项
                symchkHidValueList = symchkHidValueList.replace(replaceReg01, SepBetweenSymptoms);
            }

            // 根据体征词ID，读取体征词对应的文本，并用指定的分隔符进行分割。(小结内容)
            symchkShowTextList = GetSymptomCheckboxTextByIDList(obj, symchkHidValueList, 1);
            // (阳性结果)
            symchkPositiveTextList = GetSymptomCheckboxTextByIDList(obj, symchkHidValueList, 2);
            jQuery("#" + obj.name.replace(/ckbSym/gi, "txtHidValueResult")).val(symchkHidValueList);
            jQuery("#" + obj.name.replace(/ckbSym/gi, "txtPositiveResult")).val(symchkPositiveTextList);
            jQuery("#" + obj.name.replace(/ckbSym/gi, "txtResult")).val(symchkShowTextList);
        }

        /// <summary>
        /// 根据体征词ID，读取体征词对应的文本，并用指定的分隔符进行分割。
        /// </summary>
        /// <param name="obj">操作的复选框对象</param>
        /// <param name="symchkHidValueList">现在的复选框对应的ID字符串</param>
        /// <param name="type">1：小结内容 2：阳性结果</param>
        function GetSymptomCheckboxTextByIDList(obj, symchkHidValueList, type) {
            var SymptomChkText = "";
            var SymptomChkArray = new Array();
            SymptomChkArray = symchkHidValueList.split(SepBetweenSymptoms);
            var arraylength = 0;
            if (SymptomChkArray.length > 0) {
                arraylength = SymptomChkArray.length - 2;
            }
            var chklableid = "";
            var currNumber = 0; // 记录当前的编号
            for (var i = 0; i <= arraylength; i++) {

                if (SymptomChkArray[i] != "" && SymptomChkArray[i] != null) {

                    chklableid = obj.name.replace(/ckbSym/gi, "lab") + "_" + SymptomChkArray[i];

                    // 获取 阳性结果 时，根据 病症级别 DiseaseLevel 进行判断的。
                    if (type == 2) {
                        if (jQuery("#" + chklableid).attr("DiseaseLevel") == "0") {
                            continue;
                        }
                        if (jQuery("#" + chklableid).attr("SymptomName") == "" || jQuery("#" + chklableid).attr("SymptomName") == "NULL") {
                            continue;
                        }
                    }

                    currNumber++; // 当前编号 ( +1 )
                    SymptomChkText += GetCountNumberBetweenItem(NoBetweenSympotms, currNumber, arraylength) + jQuery("#" + chklableid).attr("SymptomName"); //获取显示的文本

                    // 拼接分割符号
                    if (i < arraylength) {
                        SymptomChkText += SepBetweenSymptoms;
                    }
                    else {
                        // SymptomChkText += SepBetweenExamItems;
                    }
                }
            }
            return SymptomChkText;
        }


        /// <summary>
        /// 根据初始编号，获取指定数字的变化 
        /// <summary>
        function GetCountNumberBetweenItem(NoBetweenItems, currcount, totalcount) {

            // 如果编号为空，则直接返回空字符
            if (NoBetweenItems == "") {
                return "";
            }

            // 如果只有一个，则不需要编号
            if (totalcount <= 1) {
                return "";
            }
            // 定义的第一中编号
            var SpecialNumber01 = "①";
            var SpecialNumber01Array = ["", "①", "②", "③", "④", "⑤", "⑥", "⑦", "⑧", "⑨", "⑩", "⑪", "⑫", "⑬", "⑭", "⑮", "⑯", "⑰", "⑱", "⑲", "⑳"];

            var tempNumberBetweenItem = "";

            if (NoBetweenItems == SpecialNumber01) {
                if (currcount <= 20) {
                    return SpecialNumber01Array[currcount];
                } else {
                    return "" + currcount + ")"; // 大于20，用 21) 这种方式代替
                }
            }
            else {
                return NoBetweenItems.replace("1", currcount); // 直接替换掉编号中的数字
            }
        }


        /// <summary>
        /// 读取并加载小结的内容,及各个检查项目的值
        /// <summary>
        function InitCustomExamLastSaveData() {
            // 合并后的收费项目ID 20150417 by wtang 
            var ID_ShowMergerFee = jQuery("#ID_CurrShowMergerFee").val();
            // 当前加载的申请号ID 20150603 by wtang 
            var ID_CurrShowApply = jQuery("#ID_CurrShowApply").val();
            var CurrCustExamItemID = 0;     //在遍历体征词时使用

            // 初始化 放大编辑
            InitBigEditer();

            SetTableEvenOddRowStyle(); // 奇偶行背景

            // 读取科室小结的信息
            jQuery(gPageCustomExamLastSaveDataMsg.dataList2).each(function (m, lastSectionSummary) {
                jQuery("#SectionSummary").val(lastSectionSummary.SectionSummary);           // 科室小结
                jQuery("#Last_SectionSummary").val(lastSectionSummary.SectionSummary);      // 科室小结 同时保存到备用文本中，用于对比是否进行了改动

                jQuery("#PositiveSummary").val(lastSectionSummary.PositiveSummary);         // 阳性结果
                jQuery("#Last_PositiveSummary").val(lastSectionSummary.PositiveSummary);    // 阳性结果 同时保存到备用文本中，用于对比是否进行了改动


                jQuery("#Is_SectionCheck").val(lastSectionSummary.Is_Check);

                //                // 如果已经审核，则禁用其他按钮
                //                if (lastSectionSummary.Is_Check == "True") {
                //                    CtrlButtonDisabled(1, false);
                //                }

            }); // end dataList2

            // 循环加载检查项目列表
            jQuery(gPageCustomExamLastSaveDataMsg.dataList0).each(function (j, lastExamItem) {

                // 如果申请号不为空，并且不等于当前选中的分单对应分单号，则跳转到下一条数据
                if (lastExamItem.ID_CustApply != "" && ID_CurrShowApply != lastExamItem.ID_CustApply) 
                {
                    return true; //continue;
                }

                // 用于检查项目的小结 用于对比是否改动
                jQuery("#txtLastLabMarkResult_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ResultLabMark);        // 记录检查项目的符号 内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                jQuery("#txtCustExamItemID_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ID_CustExamItem);         // 记录检查项目的小结的ID，便于进行修改操作


                // 先判断是否是文本
                //                if (lastExamItem.ResultLabValues != "") {
                // 不需要再进行判断是否是文本型，还是数值型，所有结果都直接写入到 ResultLabValues。 20130826 by WTang
                // 用于检查项目的小结 用于对比是否改动
                jQuery("#txtLastLabValuesResult_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ResultLabValues); // 记录检查项目检验结果值 
                jQuery("#txtSym_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ResultLabValues);                // 检验结果值（文本型）
                //                }
                //                else {
                //                    // 用于检查项目的小结 用于对比是否改动
                //                    jQuery("#txtLastLabValuesResult_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ResultNumber);   // 记录检查项目检验结果值 
                //                    jQuery("#txtSym_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ResultNumber);                   // 检查项目数值（数值型）
                //                }

                // 如果偏高，或偏低则调用不同的背景颜色
                if (lastExamItem.ResultLabMark != "") {
                    jQuery("#td_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem + "_1").attr("Class", "lohilimitbg");
                    jQuery("#td_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem + "_2").attr("Class", "lohilimitbg");
                    jQuery("#td_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem + "_3").attr("Class", "lohilimitbg");
                    jQuery("#td_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem + "_4").attr("Class", "lohilimitbg textcenter");
                    jQuery("#td_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem + "_5").attr("Class", "lohilimitbg");
                    jQuery("#td_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem + "_6").attr("Class", "lohilimitbg");
                    jQuery("#td_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem + "_7").attr("Class", "lohilimitbg");
                    jQuery("#td_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem + "_8").attr("Class", "lohilimitbg");
                }
                // 采用事件的方式判断是否偏高，偏低
                // jQuery("#txtSym_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).change();

                // 设置下拉的选择情况
                jQuery("#selLoHiLimitResult_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).find("option").removeAttr("selected");
                jQuery("#selLoHiLimitResult_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).attr("value", lastExamItem.ResultLabMark);

                // 判断是否有参考值，如果有，则覆盖默认的值
                if (lastExamItem.ResultLabRange != "") {
                    jQuery("#txtLimit_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ResultLabRange);
                }
                // 缩写
                if (lastExamItem.AbbrExamName != "") {
                    jQuery("#td_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem + "_4").html(lastExamItem.AbbrExamName);
                }

                // 单位
                if (lastExamItem.ResultLabUnit != "") {
                    jQuery("#txtSymUnit_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ResultLabUnit);
                    jQuery("#txtSymUnit_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).attr("DefaultUnit", lastExamItem.ResultLabUnit);
                }
                // 滴度
                if (lastExamItem.SCO != "") {
                    jQuery("#txtSCO_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.SCO);
                }
                // 检测方法
                if (lastExamItem.DetectionMethod != "") {
                    jQuery("#txtDetectionMethod_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.DetectionMethod);
                }

                // 单位
                jQuery("#txtLastSymUnit_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ResultLabUnit);
                // 判断是否有参考值，如果有，则覆盖默认的值
                jQuery("#txtLastLimit_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ResultLabRange);
                // 滴度
                jQuery("#txtLastSCO_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.SCO);
                // 检测方法
                jQuery("#txtLastDetectionMethod_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.DetectionMethod);

                //                屏蔽病症级别列
                //                if (lastExamItem.ResultLabMark != "") {
                //                    jQuery("#td_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem + "_4").html("1");
                //                }


                //                jQuery("#ExamItemDoctor_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).val(lastExamItem.ID_SummaryDoctor);                 // 检查医生ID
                //                jQuery("#ExamItemDoctor_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).attr("DoctorName", lastExamItem.SummaryDoctorName); // 检查医生
                //                jQuery("#Lab_ExamItemDoctor_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).html(lastExamItem.SummaryDoctorName);           // 检查医生

                //                jQuery("#Lab_ExamItemDate_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).attr("ExamDate", lastExamItem.ExamItemSummaryDate);       // 检查时间/录入时间 (用于存入数据库)
                //                jQuery("#Lab_ExamItemDate_" + lastExamItem.ID_Fee + "_" + lastExamItem.ID_ExamItem).html(lastExamItem.FormatExamItemSummaryDate);             // 检查时间/录入时间 (显示用)

                // end if (lastFeeItem.GetResultWay == "N")

            });           // end dataList0

//            // 循环加载收费项目列表
//            jQuery(gPageCustomExamLastSaveDataMsg.dataList1).each(function (m, lastFeeItem) {

//                jQuery("#ExamDoctor_" + lastFeeItem.ID_Fee).val(lastFeeItem.ID_ExamDoctor);                 // 检查医生ID
//                jQuery("#ExamDoctor_" + lastFeeItem.ID_Fee).attr("DoctorName", lastFeeItem.ExamDoctorName); // 检查医生
//                jQuery("#Lab_ExamDoctor_" + lastFeeItem.ID_Fee).html(lastFeeItem.ExamDoctorName);           // 检查医生

//                jQuery("#Lab_ExamDate_" + lastFeeItem.ID_Fee).attr("ExamDate", lastFeeItem.ExamDate);       // 检查时间/录入时间 (用于存入数据库)
//                jQuery("#Lab_ExamDate_" + lastFeeItem.ID_Fee).html(lastFeeItem.FormatExamDate);             // 检查时间/录入时间 (显示用)

            //            });
            var isLoadDoctorName = 0;
            // 加载检验科的分单 检查医生，录入医生
            jQuery(gPageCustomExamLastSaveDataMsg.dataList3).each(function (i, applyitem) {
                var applyid = jQuery("#ID_CurrShowApply").val();
                if (applyid == applyitem.ID_Apply && applyitem.DetectionDoctorName != "") {
                    isLoadDoctorName = 1;
                    ShowQuickSelectUser(applyitem.ID_DetectionDoctor, applyitem.DetectionDoctorName); // 如果已经保存过，则加载保存后的检查医生
                }
            });
            if (isLoadDoctorName == 0) {
                ShowQuickSelectUser("", ""); // 如果已经保存过，则加载保存后的检查医生
            }

            // 获取客户的病症级别，并判断是否进行相应的提示 20140326 by wtang 
            QueryCustomerExamItemDiseaseLevelTips();

            // 初始化按钮是否可以使用 , （在页面上次保存的数据加载完成后，调用一次按钮控制函数）

            jQuery("#IS_InitButtom").val("False");
            InitButtomDisabled(); // 初始化按钮是否可以使用

            jQuery('#fkls').show();
            jQuery('#fklscen').show();
            // 显示分科信息 和 科室小结 Tab 选项
            jQuery('#ExamInfoTabLi1').show();
            jQuery('#ExamInfoTabLi3').show();
            jQuery('#btnCustomerSimpleInfo').show();
            jQuery('#CustomerSectionQuickSwitch').show();

        }

        /// <summary>
        /// 获取科室小结 (汇总)
        /// </summary>
        function GetCustomerExamSectionSummaryValue() {

            var WrapCharacter = "\n";        //显示的换行符号
            var Is_AutoAddWrapCharacter = 1; //是否自动添加换行符号
            var tempExamItemFirstChar = "  ";  // 检查项目的首字符

            var CustomerExamSectionSummaryText = "";    // 小结文本信息
            var CustomerExamSectionSummaryValue = "";   // 小结Value
            var CustomerExamPositiveSummaryText = "";   // 阳性结果
            var tempFeeItemNameText = "";               // 临时记录收费项目的名称
            var tempFeeItemSummaryText = "";            // 临时记录收费项目的小结文本信息
            var tempFeeItemSummaryValue = "";           // 临时记录收费项目的小结Value

            var tempExamItemLabValue = "";                  // 临时记录检查项目的输入值
            var tempExamItemLabMark = "";                   // 临时记录检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
            var tempExamItemLabValuesLastResult = "";       // 临时记录检查项目的输入值（上一次保存的）
            var tempExamItemLabMarkLastResult = "";         // 临时记录检查项目对应的选择值（上一次保存的）
            var tempResultLabUnit = "";                     // 检验值单位

            var tempExamItemSummaryText = "";           // 临时记录检查项目的小结文本信息
            var tempExamItemPositiveSummaryText = "";   // 临时记录检查项目的阳性结果文本信息
            var tempExamItemDefaultSummaryText = "";    // 临时记录检查项目的默认小结文本信息


            var CurrFeeItemID = 0;      //在遍历检查项目使用
            var PreFeeItemID = 0;       //记录已经拼接到项目小结中的收费项目ID
            var CurrExamItemID = 0;     //在遍历体征词时使用
            var FeeItemCount = 0;       //记录收费项目的个数
            var CurrExamItemCount = 0;  //当前检查项目编号
            var ExamItemIsEntrySectSum = ""; //是否允许进入科室小结
            var Is_AllValueNotChange = 1;    //标记是否是所有检查项目的值和结果都没有进行修改

            // 遍历检查项目列表
            jQuery(gPageCustomExamDataMsg.dataList1).each(function (j, item01) {

                // 本次输入的值
                tempExamItemLabValue = jQuery("#txtSym_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();                 // 每个检查项目的输入值
                // 本次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                tempExamItemLabMark = jQuery("#selLoHiLimitResult_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();      // 每个检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”

                // 上次输入的值
                tempExamItemLabValuesLastResult = jQuery("#txtLastLabValuesResult_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();   // 上次保存的结果值
                // 上次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                tempExamItemLabMarkLastResult = jQuery("#txtLastLabMarkResult_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();      // 上次保存的取值内容 “↑”或者“↓”或者“＋”或者“空

                if ((tempExamItemLabValue != tempExamItemLabValuesLastResult) || (tempExamItemLabMark != tempExamItemLabMarkLastResult)) {
                    Is_AllValueNotChange = 0; //检查到有修改的值
                    return false; // 跳出循环（ break ）
                }
            });

            // 1、小结
            // 遍历收费项目 汇总小结
            jQuery(gPageCustomExamDataMsg.dataList0).each(function (i, item00) {

                tempFeeItemNameText = jQuery("#lab_" + item00.ID_Fee).attr("FeeName"); // 临时记录收费项目的名称

                CurrExamItemCount = 0; //当前检查项目编号 从0开始计数
                CurrFeeItemID = 0; //在遍历检查项目使用
                // 遍历检查项目列表
                jQuery(gPageCustomExamDataMsg.dataList1).each(function (j, item01) {

                    // 这里可以判断出是否已经遍历完成，如果已经遍历完，则跳出循环 （最后一个项目不会）
                    if (CurrFeeItemID > 0 && CurrFeeItemID != item01.ID_Fee) {

                        // 如果收费项目中只有一个检查项目，则去掉项目编号
                        tempFeeItemSummaryText = ReplaceFistCountNumber(tempFeeItemSummaryText, CurrExamItemCount);
                        if (tempFeeItemSummaryText != "") {
                            CustomerExamSectionSummaryText += tempFeeItemSummaryText + TerminalSymbol;  //项目分隔符
                        }
                        tempFeeItemSummaryText = "";   // 清空临时变量数据
                        PreFeeItemID = CurrFeeItemID;  // 记录已经拼接到项目小结中的收费项目ID
                        return false;
                    }
                    if (item00.ID_Fee == item01.ID_Fee) {
                        CurrFeeItemID = item01.ID_Fee; //如果进入了这个循环

                        // 本次输入的值
                        tempExamItemLabValue = jQuery("#txtSym_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();              // 每个检查项目的输入值
                        // 本次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                        tempExamItemLabMark = jQuery("#selLoHiLimitResult_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();   // 每个检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                        tempResultLabUnit = jQuery("#txtSymUnit_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();            // 检验值单位

                        // 上次输入的值
                        tempExamItemLabValuesLastResult = jQuery("#txtLastLabValuesResult_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();   // 上次保存的结果值
                        // 上次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                        tempExamItemLabMarkLastResult = jQuery("#txtLastLabMarkResult_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();      // 上次保存的取值内容 “↑”或者“↓”或者“＋”或者“空


                        ExamItemIsEntrySectSum = jQuery("#lab_" + item01.ID_Fee + "_" + item01.ID_ExamItem).attr("IsEntrySectSum"); //是否允许进入科室小结
                        // 如果允许进入小结
                        if (ExamItemIsEntrySectSum == "True") {

                            if (tempExamItemLabMark != "") {

                                if (CurrExamItemCount > 0 && Is_AutoAddWrapCharacter == 1) {
                                    //tempFeeItemSummaryText += WrapCharacter;  // 检查项目之间，不进行换行 （20130530 改 by WTang）
                                }
                                else {
                                    if (FeeItemCount > 0 && Is_AutoAddWrapCharacter == 1) {
                                        tempFeeItemSummaryText = tempFeeItemSummaryText + WrapCharacter; // 如果不是第一个收费项目，则添加换行符号
                                    }
                                    tempFeeItemSummaryText += tempFeeItemNameText;
                                    if (Is_AutoAddWrapCharacter == 1) {
                                        tempFeeItemSummaryText = tempFeeItemSummaryText + WrapCharacter + tempExamItemFirstChar; // 如果自动换行，则收费项目名称单独作为一行显示（同时换行后，添加一个行的首字符）
                                    }
                                    FeeItemCount++;  // 记录有进入小结信息的收费项目的个数
                                }

                                CurrExamItemCount++;  //当前检查项目编号 ( +1 )

                                // 如果是“＋”标记，则不拼接到小结中 20131108 by wtang
                                if (tempExamItemLabMark == "＋") {
                                    tempExamItemLabMark = "";
                                }
                                if (tempExamItemLabMark == "±") {
                                    tempExamItemLabMark = "";
                                }
                                if (tempExamItemLabMark == "＋＋") {
                                    tempExamItemLabMark = "";
                                }
                                if (tempExamItemLabMark == "＋－") {
                                    tempExamItemLabMark = "";
                                }
                                tempFeeItemSummaryText += GetCountNumberBetweenItem(NoBetweenExamItems, CurrExamItemCount, 10) + jQuery("#lab_" + item01.ID_Fee + "_" + item01.ID_ExamItem).attr("ExamItemName") + SepExamAndValue + tempExamItemLabValue + " " + tempResultLabUnit + tempExamItemLabMark + SepBetweenExamItems; //添加项目终结符
                                tempFeeItemSummaryValue += jQuery("#txtHidValueResult_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();
                            }
                        }
                        // End 如果允许进入小结
                    }
                }); // End 遍历检查项目列表
                // 如果最后一个收费项目还未拼接，则在这里进行拼接。
                if (PreFeeItemID != CurrFeeItemID) {
                    // 如果收费项目中只有一个检查项目，则去掉项目编号
                    tempFeeItemSummaryText = ReplaceFistCountNumber(tempFeeItemSummaryText, CurrExamItemCount);
                    //如果该收费项目小结有内容
                    if (tempFeeItemSummaryText != "") {
                        // 加上最后一个收费项目的小结。
                        CustomerExamSectionSummaryText += tempFeeItemSummaryText + TerminalSymbol;          //项目分隔符
                    }
                    tempFeeItemSummaryText = "";   // 清空临时变量数据
                }
            });

            // 替换科室终结中的重复标点符号
            CustomerExamSectionSummaryText = ReplaceFlagInSectionSummaryText(CustomerExamSectionSummaryText);
            // 小结 end


            // 2、阳性结果 
            // 遍历收费项目 汇总 阳性结果
            CustomerExamPositiveSummaryText = "";
            FeeItemCount = 0;       //记录收费项目的个数
            jQuery(gPageCustomExamDataMsg.dataList0).each(function (i, item00) {

                tempFeeItemNameText = jQuery("#lab_" + item00.ID_Fee).attr("FeeName"); // 临时记录收费项目的名称

                CurrExamItemCount = 0;  //当前检查项目编号 从0开始计数
                CurrFeeItemID = 0;      //在遍历检查项目使用
                PreFeeItemID = 0;       //记录已经拼接到项目小结中的收费项目ID
                tempFeeItemSummaryText = "";
                // 遍历检查项目列表
                jQuery(gPageCustomExamDataMsg.dataList1).each(function (j, item01) {

                    // 这里可以判断出是否已经遍历完成，如果已经遍历完，则跳出循环 （最后一个项目不会）
                    if (CurrFeeItemID > 0 && CurrFeeItemID != item01.ID_Fee) {

                        // 如果收费项目中只有一个检查项目，则去掉项目编号
                        tempFeeItemSummaryText = ReplaceFistCountNumber(tempFeeItemSummaryText, CurrExamItemCount);

                        if (tempFeeItemSummaryText != "") {
                            CustomerExamPositiveSummaryText += tempFeeItemSummaryText + TerminalSymbol;  //项目分隔符
                        }
                        tempFeeItemSummaryText = "";   // 清空临时变量数据
                        PreFeeItemID = CurrFeeItemID;  // 记录已经拼接到项目小结中的收费项目ID
                        return false;
                    }

                    if (item00.ID_Fee == item01.ID_Fee) {
                        CurrFeeItemID = item01.ID_Fee; //如果进入了这个循环


                        // 本次输入的值
                        tempExamItemLabValue = jQuery("#txtSym_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();              // 每个检查项目的输入值
                        // 本次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                        tempExamItemLabMark = jQuery("#selLoHiLimitResult_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();   // 每个检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                        tempResultLabUnit = jQuery("#txtSymUnit_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();            // 检验值单位

                        // 上次输入的值
                        tempExamItemLabValuesLastResult = jQuery("#txtLastLabValuesResult_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();   // 上次保存的结果值
                        // 上次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                        tempExamItemLabMarkLastResult = jQuery("#txtLastLabMarkResult_" + item01.ID_Fee + "_" + item01.ID_ExamItem).val();      // 上次保存的取值内容 “↑”或者“↓”或者“＋”或者“空



                        if (tempExamItemLabMark != "") {

                            if (CurrExamItemCount > 0 && Is_AutoAddWrapCharacter == 1) {
                                tempFeeItemSummaryText += WrapCharacter;
                            }
                            else {
                                if (FeeItemCount > 0 && Is_AutoAddWrapCharacter == 1) {
                                    tempFeeItemSummaryText += WrapCharacter; // 如果不是第一个收费项目，则添加换行符号
                                }
                                tempFeeItemSummaryText += tempFeeItemNameText;
                                if (Is_AutoAddWrapCharacter == 1) {
                                    tempFeeItemSummaryText += WrapCharacter + tempExamItemFirstChar; // 如果自动换行，则收费项目名称单独作为一行显示（同时换行后，添加一个行的首字符）
                                }
                                FeeItemCount++;  // 记录有进入小结信息的收费项目的个数
                            }
                            CurrExamItemCount++;  //当前检查项目编号 ( +1 )

                            // 如果是“＋”标记，则不拼接到小结中 20131108 by wtang
                            if (tempExamItemLabMark == "＋") {
                                tempExamItemLabMark = "";
                            }
                            if (tempExamItemLabMark == "±") {
                                tempExamItemLabMark = "";
                            }
                            tempFeeItemSummaryText += GetCountNumberBetweenItem(NoBetweenExamItems, CurrExamItemCount, 10) + jQuery("#lab_" + item01.ID_Fee + "_" + item01.ID_ExamItem).attr("ExamItemName") + SepExamAndValue + tempExamItemLabMark + " " + tempResultLabUnit + tempExamItemLabMark + SepBetweenExamItems; //添加项目终结符

                        }
                    }
                });

                // 如果最后一个收费项目还未拼接，则在这里进行拼接。
                if (PreFeeItemID != CurrFeeItemID) {
                    // 如果收费项目中只有一个检查项目，则去掉项目编号
                    tempFeeItemSummaryText = ReplaceFistCountNumber(tempFeeItemSummaryText, CurrExamItemCount);
                    //如果该收费项目 阳性结果 有内容
                    if (tempFeeItemSummaryText != "") {
                        // 加上最后一个收费项目的 阳性结果。
                        CustomerExamPositiveSummaryText += tempFeeItemSummaryText + TerminalSymbol;          //项目分隔符
                    }
                    tempFeeItemSummaryText = "";   // 清空临时变量数据
                }
            });

            // 替换科室 阳性结果 中的重复标点符号
            CustomerExamPositiveSummaryText = ReplaceFlagInSectionSummaryText(CustomerExamPositiveSummaryText);
            // 阳性结果 end

            if (CustomerExamSectionSummaryText == "") {
                CustomerExamSectionSummaryText = jQuery("#SectionDefaultSummary").val();
                CustomerExamPositiveSummaryText = ""; // 阳性结果为空
            }

            //            //检查到有修改的值
            //            if (Is_AllValueNotChange == 0) {

            //            }
            //            else {
            //            }
            // 输出科室小结
            jQuery("#SectionSummary").val(CustomerExamSectionSummaryText);
            // 输出科室阳性结果
            jQuery("#PositiveSummary").val(CustomerExamPositiveSummaryText);



            // 等于2表示提交时，监测到有汇总的项目，先进行了汇总，再保存，然后再进行数据的提交
            if (jQuery("#IsAutoFinishSaveAndSubmit").val() == "2") {
                SaveCustomerSectionSummary();
            }
        }

        /// <summary>
        /// 替换科室小结中的重复标点符号（小结）
        /// </summary>
        function ReplaceFlagInSectionSummaryText(SummaryMsg) {
            var ReturnSummaryMsg = SummaryMsg; // 保存返回的数据
            var replaceReg01 = new RegExp(SepBetweenSymptoms + SepBetweenExamItems, "gi"); //  替换掉多余的 SepBetweenSymptoms
            var replaceReg02 = new RegExp(SepBetweenExamItems + TerminalSymbol, "gi"); //  替换掉多余的 SepBetweenSymptoms

            if (SepBetweenSymptoms != "" && SepBetweenExamItems != "")
                ReturnSummaryMsg = ReturnSummaryMsg.replace(replaceReg01, SepBetweenExamItems);
            if (SepBetweenExamItems != "" && TerminalSymbol != "")
                ReturnSummaryMsg = ReturnSummaryMsg.replace(replaceReg02, TerminalSymbol);

            return ReturnSummaryMsg;

            //            var SepBetweenExamItems = "，";   //项目分隔符        如：、
            //            var SepBetweenSymptoms = "、";    //体征词分隔符      如：、
            //            var TerminalSymbol = "。";        //项目终结符        如：。
            //            var SepExamAndValue = ":";       //项目小结分隔符     如：：
            //            var NoBetweenExamItems = "(1)";    //项目序号         如：(1)
            //            var NoBetweenSympotms = "①";     //体征词序号        如： ①
        }
        /// <summary>
        /// 如果收费项目中只有一个检查项目，则去掉项目编号
        /// </summary>
        function ReplaceFistCountNumber(TheOneFeeItemSummaryText, totalcount) {

            if (totalcount <= 1) {
                return TheOneFeeItemSummaryText.replace(NoBetweenExamItems, "");
            } else {
                return TheOneFeeItemSummaryText;
            }
        }


        /// <summary>
        /// 保存科室小结（审核） 
        /// </summary>
        /// <param name="state">1：审核 0：解除审核</param>
        function UpdateSectionSummaryCheckState_CompareLastSummary() {
            var state = 1;
            var tmpLast_SectionSummary = jQuery("#Last_SectionSummary").val();  // 上次结果
            var tmpSectionSummary = jQuery("#SectionSummary").val();            // 本次结果

            if (tmpSectionSummary == "" || jQuery.trim(tmpSectionSummary) == "") {
                ShowSystemDialog("科室小结不能为空，请填写小结后再进行保存！");
                return;
            }

            // 如果小结信息没有修改，则继续判断检查项目值及标记有无改动
            if (tmpLast_SectionSummary == tmpSectionSummary) {

                var tempExamItemLabValue = "";                  // 临时记录检查项目的输入值
                var tempExamItemLabMark = "";                   // 临时记录检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                var tempExamItemLabValuesLastResult = "";       // 临时记录检查项目的输入值（上一次保存的）
                var tempExamItemLabMarkLastResult = "";         // 临时记录检查项目对应的选择值（上一次保存的）

                var Is_FeeItemValueNotChange = 1;    // 收费项目下的检查项目是否存在改动
                var isFeeItemFinishedExam = 1;       // 记录收费项目是否完成了体检

                isFeeItemFinishedExam = 1;    // 默认为完成了体检
                Is_FeeItemValueNotChange = 1; // 默认为没有进行改动

                // 遍历收费项目 获取收费项目ID及检查医生
                jQuery(gPageCustomExamDataMsg.dataList0).each(function (i, feeitem) {

                    // 遍历检查项目列表  
                    jQuery(gPageCustomExamDataMsg.dataList1).each(function (j, examitem) {
                        if (feeitem.ID_Fee != examitem.ID_Fee) { return true; }

                        // 本次输入的值
                        tempExamItemLabValue = jQuery("#txtSym_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();                // 每个检查项目的输入值
                        // 本次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                        tempExamItemLabMark = jQuery("#selLoHiLimitResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();   // 每个检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”

                        // 上次输入的值
                        tempExamItemLabValuesLastResult = jQuery("#txtLastLabValuesResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();   // 上次保存的结果值
                        // 上次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                        tempExamItemLabMarkLastResult = jQuery("#txtLastLabMarkResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();      // 上次保存的取值内容 “↑”或者“↓”或者“＋”或者“空

                        if ((tempExamItemLabValue != tempExamItemLabValuesLastResult) || (tempExamItemLabMark != tempExamItemLabMarkLastResult)) {
                            Is_FeeItemValueNotChange = 0; //检查到有修改的值
                            return false; // 跳出循环（ break ）
                        }
                    });


                    // 如果检查项目值进行了修改。
                    if (Is_FeeItemValueNotChange == 0) {
                        return false; // 跳出循环（ break ）
                    }

                });     // End 遍历收费项目


                if (Is_FeeItemValueNotChange == 0) {
                    // 如果检查项目值或标记进行了修改。但是汇总信息没有变化，则首先进行汇总，再保存，最后再进行提交
                    jQuery("#IsAutoFinishSaveAndSubmit").val("2");
                    GetCustomerExamSectionSummaryValue(); // 汇总

                } else {
                    UpdateSectionSummaryCheckState(state);
                }
            }
            else {
                // 如果两次的值不一致，及做了修改。则首先进行保存，然后再进行提交
                jQuery("#IsAutoFinishSaveAndSubmit").val("2");
                SaveCustomerSectionSummary(); // 
            }
        }


        /// <summary>
        /// 保存科室小结（保存） 
        /// </summary>
        function SaveCustomerSectionSummary() {

//            if (jQuery("#SectionExamSingle_" + jQuery("#ID_CurrShowMergerFee").val()).attr("applyid") == "") {
//                ShowSystemDialog("该项目还未分单，请配置分单参数并核对后再录入!");
//                return;
//            }

            var SelectValue = jQuery("#idSelectUser").val();
            var SelectName = jQuery("#nameSelectUser").val();
            if (SelectName == "" || jQuery.trim(SelectName) == "") {
                ShowSystemDialog("请选择检查医生！");
                return;
            }


//            var SectionSummary = jQuery("#SectionSummary").val();           // 科室小结
//            if (SectionSummary == "" || jQuery.trim(SectionSummary) == "") {
//                ShowSystemDialog("科室小结不能为空，请填写小结后再进行保存！");
//                return;
//            }

            // 20130819 by WTang （这里改成，保存前禁用所有按钮，保存完成后，不进行提示，然后恢复按钮的使用）
            // 将按钮设置为不可操作（在数据提及的过程中）
            CtrlButtonDisabled(3, false);

            // 在保存前，自动进行一次汇总数据，防止忘记点击汇总造成的数据不统一的情况。
            // 暂时不进行自动汇总，考虑到有可能会在小结中进行直接编辑。20130724 by WTang
            // GetCustomerExamSectionSummaryValue();

            // 获取保存时提交的参数。 (参数提取)
            var SectionSummaryParams = GetSaveCustomerSectionSummaryParams();

            LastTimeSectionExamedCount = null; // 开始为空(比对客户已检科室数据是否需要重新读取)
            jQuery("#iptCurrShowSectionCustomerID").val("");     // 将上次查询的 科室+身份证 清空 (侧边栏，分科对比)

            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxCustExam.aspx",
                data: SectionSummaryParams,         // 科室小结参数
                cache: false,
                dataType: "text",
                success: function (jsonmsg) {

                    // 检查Ajax返回数据的状态等 20140430 by wtang 
                    jsonmsg = CheckAjaxReturnDataInfo(jsonmsg);
                    if (jsonmsg == null || jsonmsg == "") {
                        CtrlButtonDisabled(0, true); //全部按钮可以使用  20140430 by wtang 
                        return;
                    }

                    if (parseInt(jsonmsg) > 0) {

                        // 由于保存后，没有禁用按钮，所以需要在保存成功后，重新加载数据 20130826 by WTang
                        // 清空保存的小结数据信息
                        gPageCustomExamLastSaveDataMsg = "";
                        // 重新获取新的数据，并绑定到页面上
                        GetCustomerExamLastSaveData();


                        ShowSystemDialogAutoClose("【" + jQuery(".Single_Content_title").html() + "】保存成功！", jQuery("#txtCustomerID"));

                        // ShowSystemDialog("保存信息成功!");
                        if (gIsCurrentSaveReportFinishedExam == 1) {
                            jQuery("#SectionExamSingle_" + jQuery("#ID_CurrShowMergerFee").val() + "_" + jQuery("#ID_CurrShowApply").val() ).removeClass("Single_list_table_Unfinished"); // 设置为已经完成的状态
                        }

                        /*** 
                        
                        备注：保存完成后，将按钮置为不可用状态，如果后期需要开放为可用状态，则启用该段代码

                        // 重新初始化页面(清空页面上的值，然后重新获取)
                        InitCustomExamItemPage_IsSetDefaultValue(gPageCustomExamDataMsg, 0);

                        ***/

                        // CtrlButtonDisabled(0, true); // 将按钮置为可以使用 20130819 by WTang （这里改成，保存前禁用所有按钮，保存完成后，不进行提示，然后恢复按钮的使用）

                        // 将按钮设置为不可操作 保存后不进行提示，但是保存按钮不能再次点击，需要刷新页面后才能重新点击保存 20130904 by WTang
                        // 不隐藏保存按钮 20150427 by wtang 

                        // 如果分科检查已经提交，则禁用其他按钮
                        if (jQuery("#Is_SectionCheck").val() == "True") {
                            CtrlButtonDisabled(1, false);
                            isInitButton = 1; // 是否已经初始化按钮
                        } else {
                            CtrlButtonDisabled(0, true); //全部按钮可以使用  20150427 by WTang
                        }
                        // 等于2表示提交时，监测到有未保存的项目，先进行了数据的保持，然后再进行数据的提交
                        if (jQuery("#IsAutoFinishSaveAndSubmit").val() == "2") {
                            jQuery("#IsAutoFinishSaveAndSubmit").val("0");
                            UpdateSectionSummaryCheckState(1);
                        } else {

                            // 从新查询刚刚保存的数据 20150427 by wtang
                            ReLoadCustomerExamLastSaveData();
                        }

                    }
                    else if (jsonmsg == "-1") {
                        // ShowSystemDialog("保存信息成功!"); // 实际上是没有数据改动，没有入库操作 20130724 by WTang
                        CtrlButtonDisabled(0, true); //全部按钮可以使用  20130729 by WTang
                    }
                    else if (jsonmsg == "0") {
                        CtrlButtonDisabled(0, true); //全部按钮可以使用  20131204 by WTang
                        ShowSystemDialog("保存小结信息失败，请与技术人员联系!")
                    }
                    else {
                        ShowSystemDialog(jsonmsg); // 提示两人同时操作时，提示先操作的人的姓名，及操作时间 20150420 by WTang
                        CtrlButtonDisabled(3, false); //全部按钮不可以使用
                    }
                }
            });
        }

        var gIsCurrentSaveReportFinishedExam = 0; // 记录当前选择的分单是否已经完成（必填项目是否完全填写）

        /// <summary>
        /// 获取保存时提交的参数。 (参数提取)
        /// </summary>
        function GetSaveCustomerSectionSummaryParams() {
            
            // 获取当前要显示的合并后的收费项目ID
            var ID_CurrShowMergerFee = jQuery("#ID_CurrShowMergerFee").val();

            var ID_Customer = jQuery("#txtHiddenCustomerID").val();         // 体检号，该值从隐藏域中获取，防止页面上进行了修改。
            var ID_CustExamSection = jQuery("#ID_CustExamSection").val();   // 科室小结编号
            var SectionSummary = jQuery("#SectionSummary").val();           // 科室小结
            SectionSummary = encodeURIComponent(SectionSummary);            // 编码处理
            var PositiveSummary = jQuery("#PositiveSummary").val();         // 阳性结果
            PositiveSummary = encodeURIComponent(PositiveSummary);          // 编码处理
            var SummaryDoctorName = jQuery("#SummaryDoctorName").val();     // 小结医生姓名

            var CustExamDataInfoListStr = "";       // 记录所有检查项目拼接成为指定的特殊字符串后的信息（后台接收后，按照特殊的分割符进行分割并保存）
            var tempExamItemSummaryText = "";       // 临时记录检查项目的小结文本信息
            var tempExamItemSummaryValue = "";      // 临时记录检查项目的小结Value
            var SymptomStrList = "";
            var CustReportDataInfo = "";            // 记录当前报告的信息（当前申请单的信息）
            var CustFeeDataInfoListStr = "";        // 记录所有收费项目拼接成为指定的特殊字符串后的信息
            var tempLastExamItemSummaryText = "";   // 临时记录检查项目的小结文本信息（上次保存的数据）
            var tempCustExamItemID = "0";           // 临时记录项目结论编号，只有修改的时候才会有该值
            var tempExamDoctorID = "0";             // 临时记录项各个收费项目的体检医生ID
            var tempExamDoctorName = "";            // 临时记录项各个收费项目的体检医生姓名

            var tempExamDoctorID = jQuery("#idSelectUser").val();
            var tempExamDoctorName = jQuery("#nameSelectUser").val();


            var tempExamItemExamDate = "";   // 检查时间/录入时间
            var tempResultLabLowLimit = "";  // 检验值下限
            var tempResultLabHighLimit = ""; // 检验值上限

            var tempExamItemLabValue = "";                  // 临时记录检查项目的输入值
            var tempExamItemLabMark = "";                   // 临时记录检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
            var tempResultLabRange = "";                    // 检验值范围
            var tempResultLabUnit = "";                     // 检验值单位 
            var tempResultSCO = "";                         // 滴度
            var tempResultDetectionMethod = "";             // 检测方法
            var tempAbbrExamName = "";                      // 缩写

            var tempExamItemLabValuesLastResult = "";       // 临时记录检查项目的输入值（上一次保存的）
            var tempExamItemLabMarkLastResult = "";         // 临时记录检查项目对应的选择值（上一次保存的）
            var tempResultLabRangeLastResult = "";          // 检验值范围（上一次保存的）
            var tempResultLabUnitLastResult = "";           // 检验值单位（上一次保存的）
            var tempResultSCOLastResult = "";               // 滴度    （上一次保存的）
            var tempResultDetectionMethodLastResult = "";   // 检测方法 （上一次保存的）


            var tempExamItemIsLisValueNull = ""; // 检查项目值是否允许为空
            var IsReportFinishedExam = "1";      // 该申请单（报告）是否已经完成检查


            var Is_FeeItemValueNotChange = 1;    // 收费项目下的检查项目是否存在改动
            var isFeeItemFinishedExam = 1;       // 记录收费项目是否完成了体检

            // 每个检查项目内部使用 、进行分割   检查项目与检查项目之间使用 | 进行分割。 
            // 所有数据在拼接之前必须采用url编码后，再进行拼接。
            //         LAB 接口方式    收费项目ID、客户收费ID、  检查项目ID、  检查项目名称、  检查结果“↑↓＋±空”     、检查值（即输入值）     、客户检查项目ID   、医生ID       、医生姓名、检验值下限、检验值上限、检验值范围、检验值单位、  滴度、        检测方法 、                缩写

            var CustExamDataTemplete = "@ID_Fee、@ID_CustFee、@ID_ExamItem、@ExamItemName、@ExamItemResultSummary、@ExamItemSymptomValues、@ID_CustExamItem、@ID_SummaryDoctor、@SummaryDoctorName、0、0、@ExamItemRange、@ExamItemUnit、@ExamItemSCO、@ExamItemDetectionMethod、@AbbrExamName";
           
            // url编码后，再进行拼接。
            //                        收费项目ID、客户收费ID、医生ID            、医生姓名          、检查时间          、是否完成检查         
            var CustFeeDataTemplete = "@ID_Fee、@ID_CustFee、@ID_SummaryDoctor、@SummaryDoctorName、@ExamItemExamDate、@IsFeeItemFinishedExam";
            //                        申请单ID 、    是否完成检查、          合并后的收费项目ID、医生ID        、医生姓名            、收费编码      、
            var CustReportDataTemplete = "@ID_Apply、@IsReportFinishedExam、@ID_MergerFee、@ID_SummaryDoctor、@SummaryDoctorName、@SendProjectIDs、@SendGroupParams";
            
            // 这里不再通过SectionSummaryDate判断是否是第一次进行小结,因为这里需要从webservice接口获取相关的参数

            // 遍历收费项目
            jQuery(gPageCustomExamDataMsg.dataList0).each(function (i, feeitem) {

                if (feeitem.ID_FeeReportMerger != ID_CurrShowMergerFee) { return true; } // 如果不是当前显示的 合并收费项目 20150422 by wtang

                isFeeItemFinishedExam = 1; // 默认该收费项目完成了体检

                //                // 获取收费项目对应的医生ID，和 姓名 (直接从选择的检查医生中读取)
                //                tempExamDoctorID = jQuery("#ExamDoctor_" + feeitem.ID_Fee).val();
                //                tempExamDoctorName = jQuery("#ExamDoctor_" + feeitem.ID_Fee).attr("DoctorName");

                // 遍历检查项目列表  
                jQuery(gPageCustomExamDataMsg.dataList1).each(function (j, examitem) {

                    if (feeitem.ID_Fee != examitem.ID_Fee) { return true; }

                    // 本次输入的值
                    tempExamItemLabValue = jQuery("#txtSym_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();                // 每个检查项目的输入值
                    // 本次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                    tempExamItemLabMark = jQuery("#selLoHiLimitResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();     // 每个检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”

                    // 本次输入的 检验值单位
                    tempResultLabUnit = jQuery("#txtSymUnit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次输入的 结果范围
                    tempResultLabRange = jQuery("#txtLimit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次输入的 滴度
                    tempResultSCO = jQuery("#txtSCO_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次输入的值
                    tempResultDetectionMethod = jQuery("#txtDetectionMethod_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 检查项目对应的缩写
                    tempAbbrExamName = jQuery("#td_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem + "_4").html().replace("&nbsp;", " ");



                    // 上次输入的值
                    tempExamItemLabValuesLastResult = jQuery("#txtLastLabValuesResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();   // 上次保存的结果值
                    // 检验值单位
                    tempResultLabUnitLastResult = jQuery("#txtLastSymUnit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 上次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                    tempExamItemLabMarkLastResult = jQuery("#txtLastLabMarkResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();      // 上次保存的取值内容 “↑”或者“↓”或者“＋”或者“空
                    // 上次保存的结果范围
                    tempResultLabRangeLastResult = jQuery("#txtLastLimit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 滴度
                    tempResultSCOLastResult = jQuery("#txtLastSCO_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 检测方法
                    tempResultDetectionMethodLastResult = jQuery("#txtLastDetectionMethod_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();


                    // 检查项目值是否允许为空
                    tempExamItemIsLisValueNull = jQuery("#lab_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).attr("IsLisValueNull"); //检验值是否允许为空

                    // 如果其中有一项不允许为空，同时该输入框中的值为空，则修改该收费项目的结果状态为“未检”
                    if (tempExamItemIsLisValueNull == "False" && tempExamItemLabValue == "") {
                        isFeeItemFinishedExam = 0;
                        IsReportFinishedExam = 0; 
                        // 未完成体检
                        jQuery("#lab_" + examitem.ID_Fee).attr("isFeeItemFinishedExam", isFeeItemFinishedExam);
                    }


                    // 检查项目的小结 用于对比是否改动
                    tempExamItemSummaryText = jQuery("#txtResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();           // 临时记录检查项目的小结文本信息
                    tempExamItemSummaryValue = jQuery("#txtHidValueResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();  // 临时记录检查项目的小结Value
                    tempCustExamItemID = jQuery("#txtCustExamItemID_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();        // 临时记录检查项目结论编号，只有修改的时候才会有该值

                    tempexamitemSummaryText = jQuery("#txtLastLabValuesResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();   // 临时记录检查项目的小结文本信息（上次保存的数据）


                    if ((tempExamItemLabValue != tempExamItemLabValuesLastResult) ||
                        (tempExamItemLabMark != tempExamItemLabMarkLastResult) ||
                        (tempResultLabRange != tempResultLabRangeLastResult) ||
                        (tempResultLabUnit != tempResultLabUnitLastResult) ||
                        (tempResultSCO != tempResultSCOLastResult) ||
                        (tempResultDetectionMethod != tempResultDetectionMethodLastResult)
                        ) {


                        // 注意，在修改的时候，如果体检项目的小结为空的话，任然需要传到后台，以便进行上次数据的清空。
                        CustExamDataInfoListStr +=
                        CustExamDataTemplete.replace(/@ID_Fee/gi, encodeURIComponent(examitem.ID_Fee))
                        .replace(/@ID_CustFee/gi, encodeURIComponent(examitem.ID_CustFee))
                        .replace(/@ID_ExamItem/gi, encodeURIComponent(examitem.ID_ExamItem))
                        .replace(/@ExamItemName/gi, encodeURIComponent(examitem.ExamItemName))
                        .replace(/@ExamItemResultSummary/gi, encodeURIComponent(tempExamItemLabMark))   // 检查结果“↑↓＋±空”
                        .replace(/@ExamItemSymptomValues/gi, encodeURIComponent(tempExamItemLabValue))  // 检查值（即输入值）
                        .replace(/@ExamItemRange/gi, encodeURIComponent(tempResultLabRange))    // 检验值范围
                        .replace(/@ExamItemUnit/gi, encodeURIComponent(tempResultLabUnit))     // 检验值单位
                        .replace(/@ExamItemSCO/gi, encodeURIComponent(tempResultSCO))         // 滴度
                        .replace(/@ExamItemDetectionMethod/gi, encodeURIComponent(tempResultDetectionMethod)) // 检测方法
                        .replace(/@ID_CustExamItem/gi, encodeURIComponent(tempCustExamItemID))
                        .replace(/@ID_SummaryDoctor/gi, encodeURIComponent(tempExamDoctorID))
                        .replace(/@SummaryDoctorName/gi, encodeURIComponent(tempExamDoctorName))

                        .replace(/@ResultLabLowLimit/gi, encodeURIComponent(tempResultLabLowLimit))
                        .replace(/@ResultLabHighLimit/gi, encodeURIComponent(tempResultLabHighLimit))
                        .replace(/@AbbrExamName/gi, encodeURIComponent(tempAbbrExamName))
                        + "|";
                    }
                });
            });

            // 遍历收费项目 获取收费项目ID及检查医生
            jQuery(gPageCustomExamDataMsg.dataList0).each(function (i, feeitem) {
                isFeeItemFinishedExam = 1;    // 默认为完成了体检
                Is_FeeItemValueNotChange = 1; // 默认为没有进行改动

                if (feeitem.ID_FeeReportMerger != ID_CurrShowMergerFee) { return true; } // 如果不是当前显示的 合并收费项目 20150422 by wtang

                // 遍历检查项目列表  
                jQuery(gPageCustomExamDataMsg.dataList1).each(function (j, examitem) {
                    if (feeitem.ID_Fee != examitem.ID_Fee) { return true; }

                    // 本次输入的值
                    tempExamItemLabValue = jQuery("#txtSym_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();                // 每个检查项目的输入值
                    // 本次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                    tempExamItemLabMark = jQuery("#selLoHiLimitResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();     // 每个检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”

                    // 本次输入的 检验值单位
                    tempResultLabUnit = jQuery("#txtSymUnit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次输入的 结果范围
                    tempResultLabRange = jQuery("#txtLimit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次输入的 滴度
                    tempResultSCO = jQuery("#txtSCO_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次输入的值
                    tempResultDetectionMethod = jQuery("#txtDetectionMethod_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();




                    // 上次输入的值
                    tempExamItemLabValuesLastResult = jQuery("#txtLastLabValuesResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();   // 上次保存的结果值
                    // 检验值单位
                    tempResultLabUnitLastResult = jQuery("#txtLastSymUnit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 上次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                    tempExamItemLabMarkLastResult = jQuery("#txtLastLabMarkResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();      // 上次保存的取值内容 “↑”或者“↓”或者“＋”或者“空
                    // 上次保存的结果范围
                    tempResultLabRangeLastResult = jQuery("#txtLastLimit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 滴度
                    tempResultSCOLastResult = jQuery("#txtLastSCO_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 检测方法
                    tempResultDetectionMethodLastResult = jQuery("#txtLastDetectionMethod_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();

                    if ((tempExamItemLabValue != tempExamItemLabValuesLastResult) ||
                        (tempExamItemLabMark != tempExamItemLabMarkLastResult) ||
                        (tempResultLabRange != tempResultLabRangeLastResult) ||
                        (tempResultLabUnit != tempResultLabUnitLastResult) ||
                        (tempResultSCO != tempResultSCOLastResult) ||
                        (tempResultDetectionMethod != tempResultDetectionMethodLastResult)
                        ) {
                        Is_FeeItemValueNotChange = 0; //检查到有修改的值
                        return false; // 跳出循环（ break ）
                    }
                });


                // 如果检查项目值进行了修改。
                if (Is_FeeItemValueNotChange == 0) {
                    // 获取收费项目对应的医生ID，和 姓名
                    //                    tempExamDoctorID = jQuery("#ExamDoctor_" + feeitem.ID_Fee).val();
                    //                    tempExamDoctorName = jQuery("#ExamDoctor_" + feeitem.ID_Fee).attr("DoctorName");
                    //                    tempExamItemExamDate = jQuery("#Lab_ExamDate_" + feeitem.ID_Fee).attr("ExamDate");       // 检查时间/录入时间

                    isFeeItemFinishedExam = jQuery("#lab_" + feeitem.ID_Fee).attr("isFeeItemFinishedExam");  // 是否完成了体检

                    CustFeeDataInfoListStr +=
                        CustFeeDataTemplete.replace(/@ID_Fee/gi, encodeURIComponent(feeitem.ID_Fee))
                        .replace(/@ID_CustFee/gi, encodeURIComponent(feeitem.ID_CustFee))
                        .replace(/@ID_SummaryDoctor/gi, encodeURIComponent(tempExamDoctorID))
                        .replace(/@SummaryDoctorName/gi, encodeURIComponent(tempExamDoctorName))
                        .replace(/@ExamItemExamDate/gi, encodeURIComponent(tempExamItemExamDate))
                        .replace(/@IsFeeItemFinishedExam/gi, encodeURIComponent(isFeeItemFinishedExam))
                         + "|";
                }

            });      // End 遍历收费项目
            CustReportDataInfo +=
                        CustReportDataTemplete.replace(/@ID_MergerFee/gi, jQuery("#ID_CurrShowMergerFee").val())
                        .replace(/@ID_Apply/gi, jQuery("#ID_CurrShowApply").val())
                        .replace(/@ID_SummaryDoctor/gi, encodeURIComponent(tempExamDoctorID))
                        .replace(/@IsReportFinishedExam/gi, encodeURIComponent(IsReportFinishedExam))
            //                        .replace(/@SendProjectIDs/gi, encodeURIComponent(jQuery("#SectionExamSingle_" + jQuery("#ID_CurrShowMergerFee").val()).attr("SendProjectIDs")))
            //                        .replace(/@SendGroupParams/gi, encodeURIComponent(jQuery("#SectionExamSingle_" + jQuery("#ID_CurrShowMergerFee").val()).attr("SendGroupParams")))
                        .replace(/@SendProjectIDs/gi, "")
                        .replace(/@SendGroupParams/gi, "")
                        .replace(/@SummaryDoctorName/gi, encodeURIComponent(tempExamDoctorName));

            var SectionID = jQuery('#txtSectionID').val();
            var ID_Gender = jQuery('#txtGenderID').val();
            var SectionSummaryParams = { ID_CustExamSection: ID_CustExamSection,
                SummaryDoctorName: SummaryDoctorName,
                ID_Customer: ID_Customer,
                SectionID: SectionID,
                ID_Gender: ID_Gender,
                TypistDate: jQuery("#TypistDate").val(),
                ID_CurrShowMergerFee: ID_CurrShowMergerFee, // 当前显示的 收费项目ID(合并后的)
                SectionSummary: SectionSummary,
                PositiveSummary: PositiveSummary,
                CustExamDataInfoListStr: CustExamDataInfoListStr,
                CustReportDataInfo: CustReportDataInfo,
                InterfaceType: "LAB", // Lab 接口方式的数据
                CustFeeDataInfoListStr: CustFeeDataInfoListStr,
                action: 'SaveCustomerSectionSummaryInfo',
                currenttime: encodeURIComponent(new Date())
            };
            
            gIsCurrentSaveReportFinishedExam = IsReportFinishedExam;  // 当前分单是否完成了录入

            return SectionSummaryParams; // 返回拼接后的参数
        }


        /// <summary>
        /// 检测数据是否进行了修改 20150519 by wtang 
        /// </summary>
        function VerifyValueIsChange() {
            // 获取当前要显示的合并后的收费项目ID
            var ID_CurrShowMergerFee = jQuery("#ID_CurrShowMergerFee").val();

            var ID_Customer = jQuery("#txtHiddenCustomerID").val();         // 体检号，该值从隐藏域中获取，防止页面上进行了修改。
            var ID_CustExamSection = jQuery("#ID_CustExamSection").val();   // 科室小结编号
            var SectionSummary = jQuery("#SectionSummary").val();           // 科室小结
            SectionSummary = encodeURIComponent(SectionSummary);            // 编码处理
            var PositiveSummary = jQuery("#PositiveSummary").val();         // 阳性结果
            PositiveSummary = encodeURIComponent(PositiveSummary);          // 编码处理
            var SummaryDoctorName = jQuery("#SummaryDoctorName").val();     // 小结医生姓名

            var CustExamDataInfoListStr = "";       // 记录所有检查项目拼接成为指定的特殊字符串后的信息（后台接收后，按照特殊的分割符进行分割并保存）
            var tempExamItemSummaryText = "";       // 临时记录检查项目的小结文本信息
            var tempExamItemSummaryValue = "";      // 临时记录检查项目的小结Value
            var SymptomStrList = "";
            var CustReportDataInfo = "";            // 记录当前报告的信息（当前申请单的信息）
            var CustFeeDataInfoListStr = "";        // 记录所有收费项目拼接成为指定的特殊字符串后的信息
            var tempLastExamItemSummaryText = "";   // 临时记录检查项目的小结文本信息（上次保存的数据）
            var tempCustExamItemID = "0";           // 临时记录项目结论编号，只有修改的时候才会有该值
            var tempExamDoctorID = "0";             // 临时记录项各个收费项目的体检医生ID
            var tempExamDoctorName = "";            // 临时记录项各个收费项目的体检医生姓名

            var tempExamDoctorID = jQuery("#idSelectUser").val();
            var tempExamDoctorName = jQuery("#nameSelectUser").val();


            var tempExamItemExamDate = "";   // 检查时间/录入时间
            var tempResultLabLowLimit = "";  // 检验值下限
            var tempResultLabHighLimit = ""; // 检验值上限

            var tempExamItemLabValue = "";                  // 临时记录检查项目的输入值
            var tempExamItemLabMark = "";                   // 临时记录检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
            var tempResultLabRange = "";                    // 检验值范围
            var tempResultLabUnit = "";                     // 检验值单位 
            var tempResultDefaultUnit = "";                 // 默认 检验值单位 
            var tempResultSCO = "";                         // 滴度
            var tempResultDetectionMethod = "";             // 检测方法
            var tempAbbrExamName = "";                      // 缩写

            var tempExamItemLabValuesLastResult = "";       // 临时记录检查项目的输入值（上一次保存的）
            var tempExamItemLabMarkLastResult = "";         // 临时记录检查项目对应的选择值（上一次保存的）
            var tempResultLabRangeLastResult = "";          // 检验值范围（上一次保存的）
            var tempResultLabUnitLastResult = "";           // 检验值单位（上一次保存的）
            var tempResultSCOLastResult = "";               // 滴度    （上一次保存的）
            var tempResultDetectionMethodLastResult = "";   // 检测方法 （上一次保存的）


            var tempExamItemIsLisValueNull = ""; // 检查项目值是否允许为空
            var IsReportFinishedExam = "1";      // 该申请单（报告）是否已经完成检查
            var Is_ExamItemValueNotChange = 1;    // 检查项目下的检查项目是否存在改动

            var Is_FeeItemValueNotChange = 1;    // 收费项目下的检查项目是否存在改动
            var isFeeItemFinishedExam = 1;       // 记录收费项目是否完成了体检

            // 每个检查项目内部使用 、进行分割   检查项目与检查项目之间使用 | 进行分割。 
            // 所有数据在拼接之前必须采用url编码后，再进行拼接。
            //         LAB 接口方式    收费项目ID、客户收费ID、  检查项目ID、  检查项目名称、  检查结果“↑↓＋±空”     、检查值（即输入值）     、客户检查项目ID   、医生ID       、医生姓名、检验值下限、检验值上限、检验值范围、检验值单位、  滴度、        检测方法 、                缩写

            var CustExamDataTemplete = "@ID_Fee、@ID_CustFee、@ID_ExamItem、@ExamItemName、@ExamItemResultSummary、@ExamItemSymptomValues、@ID_CustExamItem、@ID_SummaryDoctor、@SummaryDoctorName、0、0、@ExamItemRange、@ExamItemUnit、@ExamItemSCO、@ExamItemDetectionMethod、@AbbrExamName";
           
            // url编码后，再进行拼接。
            //                        收费项目ID、客户收费ID、医生ID            、医生姓名          、检查时间          、是否完成检查         
            var CustFeeDataTemplete = "@ID_Fee、@ID_CustFee、@ID_SummaryDoctor、@SummaryDoctorName、@ExamItemExamDate、@IsFeeItemFinishedExam";
            //                        申请单ID 、    是否完成检查、          合并后的收费项目ID、医生ID        、医生姓名          
            var CustReportDataTemplete = "@ID_Apply、@IsReportFinishedExam、@ID_MergerFee、@ID_SummaryDoctor、@SummaryDoctorName";

            // 这里不再通过SectionSummaryDate判断是否是第一次进行小结,因为这里需要从webservice接口获取相关的参数

            // 遍历收费项目
            jQuery(gPageCustomExamDataMsg.dataList0).each(function (i, feeitem) {

                if (feeitem.ID_FeeReportMerger != ID_CurrShowMergerFee) { return true; } // 如果不是当前显示的 合并收费项目 20150422 by wtang

                isFeeItemFinishedExam = 1; // 默认该收费项目完成了体检

                //                // 获取收费项目对应的医生ID，和 姓名 (直接从选择的检查医生中读取)
                //                tempExamDoctorID = jQuery("#ExamDoctor_" + feeitem.ID_Fee).val();
                //                tempExamDoctorName = jQuery("#ExamDoctor_" + feeitem.ID_Fee).attr("DoctorName");

                // 遍历检查项目列表  
                jQuery(gPageCustomExamDataMsg.dataList1).each(function (j, examitem) {

                    if (feeitem.ID_Fee != examitem.ID_Fee) { return true; }

                    // 本次输入的值
                    tempExamItemLabValue = jQuery("#txtSym_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();                // 每个检查项目的输入值
                    // 本次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                    tempExamItemLabMark = jQuery("#selLoHiLimitResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();     // 每个检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”

                    // 本次输入的 检验值单位
                    tempResultLabUnit = jQuery("#txtSymUnit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次默认的 检验值单位
                    tempResultDefaultUnit = jQuery("#txtSymUnit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).attr("DefaultUnit");
                    // 本次输入的 结果范围
                    tempResultLabRange = jQuery("#txtLimit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次输入的 滴度
                    tempResultSCO = jQuery("#txtSCO_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次输入的值
                    tempResultDetectionMethod = jQuery("#txtDetectionMethod_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 检查项目对应的缩写
                    tempAbbrExamName = jQuery("#td_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem + "_4").html();



                    // 上次输入的值
                    tempExamItemLabValuesLastResult = jQuery("#txtLastLabValuesResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();   // 上次保存的结果值
                    // 检验值单位
                    tempResultLabUnitLastResult = jQuery("#txtLastSymUnit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 上次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                    tempExamItemLabMarkLastResult = jQuery("#txtLastLabMarkResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();      // 上次保存的取值内容 “↑”或者“↓”或者“＋”或者“空
                    // 上次保存的结果范围
                    tempResultLabRangeLastResult = jQuery("#txtLastLimit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 滴度
                    tempResultSCOLastResult = jQuery("#txtLastSCO_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 检测方法
                    tempResultDetectionMethodLastResult = jQuery("#txtLastDetectionMethod_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();


                    // 检查项目值是否允许为空
                    tempExamItemIsLisValueNull = jQuery("#lab_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).attr("IsLisValueNull"); //检验值是否允许为空

                    // 如果其中有一项不允许为空，同时该输入框中的值为空，则修改该收费项目的结果状态为“未检”
                    if (tempExamItemIsLisValueNull == "False" && tempExamItemLabValue == "") {
                        isFeeItemFinishedExam = 0;
                        IsReportFinishedExam = 0;
                        // 未完成体检
                        jQuery("#lab_" + examitem.ID_Fee).attr("isFeeItemFinishedExam", isFeeItemFinishedExam);
                    }


                    // 检查项目的小结 用于对比是否改动
                    tempExamItemSummaryText = jQuery("#txtResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();           // 临时记录检查项目的小结文本信息
                    tempExamItemSummaryValue = jQuery("#txtHidValueResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();  // 临时记录检查项目的小结Value
                    tempCustExamItemID = jQuery("#txtCustExamItemID_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();        // 临时记录检查项目结论编号，只有修改的时候才会有该值

                    tempexamitemSummaryText = jQuery("#txtLastLabValuesResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();   // 临时记录检查项目的小结文本信息（上次保存的数据）
                    

                    if ((tempExamItemLabValue != tempExamItemLabValuesLastResult) ||
                        (tempExamItemLabMark != tempExamItemLabMarkLastResult) ||
                        (tempResultLabRange != tempResultLabRangeLastResult) ||
                        (tempResultLabUnit != tempResultLabUnitLastResult && tempResultDefaultUnit != tempResultLabUnit) ||
                        (tempResultSCO != tempResultSCOLastResult) ||
                        (tempResultDetectionMethod != tempResultDetectionMethodLastResult)
                        ) {
                        Is_ExamItemValueNotChange = 0;
                        return false; // 跳出循环（ break ）
                    }
                });
            });

            // 遍历收费项目 获取收费项目ID及检查医生
            jQuery(gPageCustomExamDataMsg.dataList0).each(function (i, feeitem) {
                isFeeItemFinishedExam = 1;    // 默认为完成了体检
                Is_FeeItemValueNotChange = 1; // 默认为没有进行改动

                if (feeitem.ID_FeeReportMerger != ID_CurrShowMergerFee) { return true; } // 如果不是当前显示的 合并收费项目 20150422 by wtang

                // 遍历检查项目列表  
                jQuery(gPageCustomExamDataMsg.dataList1).each(function (j, examitem) {
                    if (feeitem.ID_Fee != examitem.ID_Fee) { return true; }

                    // 本次输入的值
                    tempExamItemLabValue = jQuery("#txtSym_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();                // 每个检查项目的输入值
                    // 本次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                    tempExamItemLabMark = jQuery("#selLoHiLimitResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();     // 每个检查项目对应的选择值 取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”

                    // 本次输入的 检验值单位
                    tempResultLabUnit = jQuery("#txtSymUnit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次默认的 检验值单位
                    tempResultDefaultUnit = jQuery("#txtSymUnit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).attr("DefaultUnit");
                    // 本次输入的 结果范围
                    tempResultLabRange = jQuery("#txtLimit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次输入的 滴度
                    tempResultSCO = jQuery("#txtSCO_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 本次输入的值
                    tempResultDetectionMethod = jQuery("#txtDetectionMethod_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();




                    // 上次输入的值
                    tempExamItemLabValuesLastResult = jQuery("#txtLastLabValuesResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();   // 上次保存的结果值
                    // 检验值单位
                    tempResultLabUnitLastResult = jQuery("#txtLastSymUnit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 上次取值内容为“↑”或者“↓”或者“＋”或者“±”或者“空”
                    tempExamItemLabMarkLastResult = jQuery("#txtLastLabMarkResult_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();      // 上次保存的取值内容 “↑”或者“↓”或者“＋”或者“空
                    // 上次保存的结果范围
                    tempResultLabRangeLastResult = jQuery("#txtLastLimit_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 滴度
                    tempResultSCOLastResult = jQuery("#txtLastSCO_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();
                    // 检测方法
                    tempResultDetectionMethodLastResult = jQuery("#txtLastDetectionMethod_" + examitem.ID_Fee + "_" + examitem.ID_ExamItem).val();

                    if ((tempExamItemLabValue != tempExamItemLabValuesLastResult) ||
                        (tempExamItemLabMark != tempExamItemLabMarkLastResult) ||
                        (tempResultLabRange != tempResultLabRangeLastResult) ||
                        (tempResultLabUnit != tempResultLabUnitLastResult && tempResultDefaultUnit != tempResultLabUnit) ||
                        (tempResultSCO != tempResultSCOLastResult) ||
                        (tempResultDetectionMethod != tempResultDetectionMethodLastResult)
                        ) {
                        Is_FeeItemValueNotChange = 0; //检查到有修改的值
                        return false; // 跳出循环（ break ）
                    }
                });



            });       // End 遍历收费项目

            // 没有修改  20150519 by wtang 
            if (Is_ExamItemValueNotChange == 1 && Is_FeeItemValueNotChange == 1) {
                return 0;
            } else {
                return 1;
            }

        }


        /// <summary>
        /// 自动计算体重指数函数
        /// </summary>
        /// <param name="obj">体重指数框对象（this）</param>
        /// <param name="param1">第一个框的数据库编号（用数据库编号，转换成为文本框的ID）</param>
        /// <param name="param2">第二个框的数据库编号（用数据库编号，转换成为文本框的ID）</param>
        function AutoCalculWeightPoint(obj, param1, param2) {

            var CurrTextID = jQuery(obj).attr("id");

            var replaceReg01 = new RegExp(jQuery(obj).attr("ExamItemID"), "gi"); // 需要替换的检查项目ID
            var ID_Param1 = CurrTextID.replace(replaceReg01, param1);
            var ID_Param2 = CurrTextID.replace(replaceReg01, param2);
            var value_param1 = jQuery("#" + ID_Param1).val();
            var value_param2 = jQuery("#" + ID_Param2).val();
            if (value_param1 == "") return false;
            if (value_param2 == "") return false;
            if (parseFloat(value_param1) > 0) {
                var ret = parseFloat(value_param2) * 10000 / parseFloat(value_param1) / parseFloat(value_param1);
                ret = Math.round(ret * 1000) / 1000;
                jQuery("#" + CurrTextID).val(ret);
            }
        }


        /// <summary>
        /// 绑定用户，绑定后只能由该医生或护士进行体检，如果需要其他人员操作，需要进行解除绑定操作 (绑定)
        /// </summary>
        function BandingCustomerSectionExamInfo() {

            var ID_Customer = jQuery.trim(jQuery("#txtHiddenCustomerID").val());         // 体检号，该值从隐藏域中获取，防止页面上进行了修改。
            var ID_CustExamSection = jQuery("#ID_CustExamSection").val();   // 科室小结编号
            var SectionID = jQuery('#txtSectionID').val();
            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxCustExam.aspx",
                data: { ID_CustExamSection: ID_CustExamSection,
                    ID_Customer: ID_Customer,
                    SectionID: SectionID,
                    action: 'BandingCustomerSectionExamInfo',
                    currenttime: encodeURIComponent(new Date())
                },
                cache: false,
                dataType: "json",
                success: function (jsonmsg) {
                    // 更新后，不用做任何的页面提示
                }
            });
        }




        // 选择用户后的回调函数
        function SetSelectedUserCallBack() {

            var SelectValue = jQuery("#idSelectUser").val();
            var SelectName = jQuery("#nameSelectUser").val();

//            // 遍历收费项目
//            jQuery(gPageCustomExamDataMsg.dataList0).each(function (i, feeitem) {

//                // 设置收费项目对应的医生ID，和 姓名
//                jQuery("#ExamDoctor_" + feeitem.ID_Fee).val(SelectValue);
//                jQuery("#ExamDoctor_" + feeitem.ID_Fee).attr("DoctorName", SelectName);
//                jQuery("#Lab_ExamDoctor_" + feeitem.ID_Fee).html(SelectName);
//            });

        }


        // 移除选择的医生
        function RemoveSelectedExamDoctor() {

            var SelectValue = "0";
            var SelectName = "--";

            jQuery("#idSelectUser").val("");
            jQuery("#nameSelectUser").val("");

            //            // 遍历收费项目
            //            jQuery(gPageCustomExamDataMsg.dataList0).each(function (i, feeitem) {

            //                // 设置收费项目对应的医生ID，和 姓名
            //                jQuery("#ExamDoctor_" + feeitem.ID_Fee).val(SelectValue);
            //                jQuery("#ExamDoctor_" + feeitem.ID_Fee).attr("DoctorName", SelectName);
            //                jQuery("#Lab_ExamDoctor_" + feeitem.ID_Fee).html(SelectName);

            //            });
            jQuery('#txtUserInputCode').focus();
        }



        /// <summary>
        /// 显示已经上传到服务器的图片(显示设备上传上来的图像列表 20140617 )
        /// </summary>
        function ShowDeviceResultImagesList(ImageUrl) {
            var Is_ShowDeviceResultImagesList = jQuery("#Is_ShowDeviceResultImagesList").val();           // 标记是否已经读取并显示过文件了
            if (Is_ShowDeviceResultImagesList != "1") { return; }

            var ImageFileDir = GetUserImageFilesDir();

            if (ImageUrl != "") {
                var SingleFileStr = ImageUrl.split("|");
                if (SingleFileStr.length > 1) {
                    for (var i = 0; i < SingleFileStr.length; i++) {
                        if (SingleFileStr[i] == "") { return true; } // continue ;

                        var sFileName = SingleFileStr[i].split(",");
                        if (sFileName.length > 0) {

                            if (sFileName[0] == "") { return true; } // continue ;
                            var ImageItem = '<p><img src="' + ImageFileDir + sFileName[0] + '" alt="" width="800px" /></p>';

                            // 显示设备上的图片 或 报告图片
                            jQuery("#DeviceResultImagesList").append(ImageItem);
                            jQuery("#ExamInfoTabLi5").show();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取客户图像的存放目录
        /// </summary>
        function GetUserImageFilesDir() {

            var ID_Customer = jQuery.trim(jQuery("#txtHiddenCustomerID").val());    // 体检号，该值从隐藏域中获取，防止页面上进行了修改。
            var ID_CustExamSection = jQuery("#ID_CustExamSection").val();           // 科室小结编号
            var ID_Section = jQuery('#txtSectionID').val();

            if (ID_Customer == "" || ID_Section == "") {
                return;
            }

            return jQuery("#CurrImageFileUploadUrl").val() + "/Images/" + ID_Customer.substr(6, 4) + ID_Customer.substr(2, 2) + "/" + ID_Customer.substr(4, 2) + "/" + ID_Customer + "/" + ID_Section + "/";
        }
        