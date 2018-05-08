

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

        jQuery(document).ready(function () {

            jQuery("#QueryExamListData").attr("data-left", (232 + jQuery("#ShowUserMenuDiv").height()));
            jQuery(".j-autoHeight").autoHeight(); // 自适应高度

            jQuery("#txtCustomerID").focus(); // 体检号文本框获取焦点

            GetGuideSheetReturnListParams();  // 获取Cookie中存放的指引单查询列表页参数

            // QueryPagesData(0);  // 改成页面加载时，不进行自动读取数据 20140422 by wtang
        });

        /// <summary>
        /// 查询
        /// </summary>
        function ButtonClickQuery() {

            var customerid = jQuery.trim(jQuery('#txtCustomerID').val()); ;    // 体检号

            if (customerid == "") {
                tempOperPageCount = 0;
                QueryPagesData(0);
                SaveGuideSheetReturnListParams(); // 保存指引单查询列表页参数

            } else {
                if (!isCustomerExamNo(customerid)) {
                    ShowSystemDialog("体检号格式错误，请检核对后重新输入！");
                    return;
                } else {
                    DoLoad('/System/GuideSheet/GuideSheetReturnOper.aspx?txtCustomerID=' + customerid, '');
                }
            }
        }

        var oldQueryCustomerID = ""; // 记录上次查询的体检号
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
        /// <summary>
        /// 按条件进行分页查询
        /// </summary>
        function QueryPagesData(pageIndex) {
            var optInit;
            optInit = getOptionsFromForm();
            var totalCount = 0;


            var TipsInfoTempletecontent = jQuery('#TipsInfoTemplete').html();
            if (TipsInfoTempletecontent == undefined) { return; }
            jQuery('#Searchresult').html(TipsInfoTempletecontent.replace(/@TipsInfo/gi, "正在查询，请稍等..."));


            var BeginExamDate = jQuery('#BeginExamDate').val(); // 开始日期
            BeginExamDate = encodeURIComponent(BeginExamDate);

            var EndExamDate = jQuery('#EndExamDate').val();     // 结束日期
            EndExamDate = encodeURIComponent(EndExamDate);

            var selIsTeam = jQuery('#selIsTeam').val();         // 体检类型

            var OnlyMySelf = jQuery("input[name='chkOnlyMySelf']:checked").val(); // 仅显示我操作的
            var IsNotExam = jQuery("input[name='chkNotExam']:checked").val();       // 未回收

            jQuery.ajax({
                type: "GET",
                url: "/Ajax/AjaxCustExam.aspx",
                data: { pageIndex: pageIndex,
                    BeginExamDate: BeginExamDate,
                    EndExamDate: EndExamDate,
                    selIsTeam: selIsTeam,
                    pageSize: pagePagination.items_per_page,
                    OnlyMySelf: OnlyMySelf,
                    IsNotExam: IsNotExam,
                    action: 'GetGuideSheetReturnedList'
                },
                cache: false,
                dataType: "json",
                success: function (msg) {

                    // 检查Ajax返回数据的状态等 20140430 by wtang 
                    msg = CheckAjaxReturnDataInfo(msg);
                    if (msg == null || msg == "") {
                        return;
                    }

                    var tmpCustomerIDsStr = ""; // 临时记录体检号（逗号分隔的字符串）
                    if (parseInt(msg.totalCount) > 0) {

                        jQuery("#Pagination").show();
                        if (tempOperPageCount == 0) {
                            jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
                            tempOldtotalCount = msg.totalCount;
                        }
                        else if (tempOldtotalCount != msg.totalCount) {
                            jQuery("#Pagination ul").pagination(msg.totalCount, optInit);
                        }


                        var newcontent = "";
                        // 从模版中读取数据加载列表
                        var templateContent = jQuery('#QueryDataListTemplete').html();
                        var RowNum = 1;
                        if (pageIndex > 0) {
                            RowNum = optInit.items_per_page * (pageIndex) + 1;
                        }
                        jQuery(msg.dataList).each(function (i, item) {

                            if (tmpCustomerIDsStr == "") {
                                tmpCustomerIDsStr = item.ID_Customer;
                            } else {
                                tmpCustomerIDsStr = tmpCustomerIDsStr + "," + item.ID_Customer;
                            }

                            newcontent +=
                             templateContent.replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@CustomerID/gi, item.ID_Customer)
                            .replace(/@GuideSheetReturnedby/gi, item.GuideSheetReturnedby)
                            .replace(/@GuideSheetReturnedDate/gi, item.GuideSheetReturnedDate == '' ? '--' : item.GuideSheetReturnedDate)
                            .replace(/@Is_GuideSheetReturned/gi, item.Is_GuideSheetReturned == 'True' ? '√' : '--')
                            .replace(/@Is_GuideSheetPrinted/gi, item.Is_GuideSheetPrinted == 'True' ? '√' : '--')
                            .replace(/@bgclass/gi, item.Is_GuideSheetPrinted == 'True' ? '' : 'noprint_bgclass')
                            .replace(/@trTitle/gi, item.Is_GuideSheetPrinted == 'True' ? '' : "【" + item.CustomerName + '】还未打印指引单！')
                            .replace(/@RowNum/gi, RowNum);
                            RowNum++;

                        });
                        if (newcontent != '') {
                            jQuery('#Searchresult').html(newcontent);

                            // 查询指定体检号的存档信息及体检基本信息 (查询分页列表的补充信息)
                            GetCustomerExamListArcPhysicalInfo(tmpCustomerIDsStr);

                            SetTableEvenOddRowStyle(); // 奇偶行背景
                        }
                    } else {
                        jQuery('#Searchresult').html(TipsInfoTempletecontent.replace(/@TipsInfo/gi, "在您查询的时间段内，没有找到指引单信息！"));
                        jQuery("#Pagination").hide(); // 没有数据的时候，隐藏分页信息
                    }
                    // 判断表格是否存在滚动条,并设置相应的样式
                    JudgeTableIsExistScroll();
                }
            });
        }


        function Search() {

            tempOperPageCount = 0;
            QueryPagesData(0); //重新按照新输入的条件进行查询

        }



        /// <summary>
        /// 查询指定体检号的存档信息及体检基本信息 (查询分页列表的补充信息)
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
                        jQuery("#ExamCard_" + onarccustitem.ID_Customer).html(onarccustitem.ExamCard);
                        jQuery("#MobileNo_" + onarccustitem.ID_Customer + " div").html(onarccustitem.MobileNo);
                        jQuery("#MobileNo_" + onarccustitem.ID_Customer).attr("title", onarccustitem.MobileNo);
                    });
                }
            });

        }


        function ChangeNotExamState() {

            var OnlyMySelf = jQuery("input[name='chkOnlyMySelf']:checked").val();   // 仅显示我操作的
            var IsNotExam = jQuery("input[name='chkNotExam']:checked").val();       // 未检
            if (IsNotExam == "0") {
                jQuery("#chkOnlyMySelf").attr("checked", false);
            } else {
                jQuery("#chkOnlyMySelf").attr("checked", true);
            }
        }

        function ChangeOnlyMySelf() {

            var OnlyMySelf = jQuery("input[name='chkOnlyMySelf']:checked").val();   // 仅显示我操作的
            var IsNotExam = jQuery("input[name='chkNotExam']:checked").val();       // 未检
            if (OnlyMySelf == "0") {
                jQuery("#chkNotExam").attr("checked", false);
            }
        }
        /// <summary>
        /// 获取Cookie中存放的指引单查询列表页参数
        /// </summary>
        function GetGuideSheetReturnListParams() {

            var ParamsArgArray = GetUserCurrentQueryParams("QParam_GuideSheetReturnList");
            if (ParamsArgArray == null) { return; }
            if (ParamsArgArray.length <= 4) { return; }
            // 注意放入数组的顺序
            var BeginExamDate = ParamsArgArray[0];  // 开始日期
            var EndExamDate = ParamsArgArray[1];    // 结束日期
            var OnlyMySelf = ParamsArgArray[2];     // 仅显示我操作的
            var IsNotExam = ParamsArgArray[3];      // 未检
            var selIsTeam = ParamsArgArray[4];      // 体检类型（团体，个人）

            jQuery('#BeginExamDate').val(BeginExamDate); // 开始日期
            jQuery('#EndExamDate').val(EndExamDate);     // 结束日期
            // 仅显示我操作的
            if (OnlyMySelf == "0") {
                jQuery("#chkOnlyMySelf").attr("checked", true);
            } else {
                jQuery("#chkOnlyMySelf").attr("checked", false);
            }
            // 未检
            if (IsNotExam == "0") {
                jQuery("#chkNotExam").attr("checked", true);
            } else {
                jQuery("#chkNotExam").attr("checked", false);
            }
            jQuery('#selIsTeam').val(selIsTeam); // 体检类型（团体，个人）


        }
        /// <summary>
        /// 保存指引单查询列表页参数
        /// </summary>
        function SaveGuideSheetReturnListParams() {

            var ParamsArgArray = new Array();

            var BeginExamDate = jQuery('#BeginExamDate').val(); // 开始日期
            BeginExamDate = encodeURIComponent(BeginExamDate);
            var EndExamDate = jQuery('#EndExamDate').val();     // 结束日期
            EndExamDate = encodeURIComponent(EndExamDate);
            var OnlyMySelf = jQuery("input[name='chkOnlyMySelf']:checked").val();   // 仅显示我操作的
            var IsNotExam = jQuery("input[name='chkNotExam']:checked").val();       // 未检
            var selIsTeam = jQuery('#selIsTeam').val();         // 体检类型（团体，个人）

            // 注意放入数组的顺序
            ParamsArgArray.push(BeginExamDate); // 开始日期
            ParamsArgArray.push(EndExamDate);   // 结束日期
            ParamsArgArray.push(OnlyMySelf);    // 仅显示我操作的
            ParamsArgArray.push(IsNotExam);     // 未检
            ParamsArgArray.push(selIsTeam);     // 体检类型（团体，个人）

            // 保存科室分检查询列表的参数
            SetUserCurrentQueryParams("QParam_GuideSheetReturnList", ParamsArgArray);

        }

