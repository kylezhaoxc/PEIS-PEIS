
var defalutImagSrc = "/template/blue/images/icons/nohead.gif"; //默认头像
/// <summary>
///页面初始化
/// </summary>
jQuery(document).ready(function () {
    if (selectedNation != "") {
        ShowQuickSelectNation(selectedNation, selectedNationName);          // 设置民族的已选项
    }
    else {
        ShowQuickSelectNation(1, "汉族");          // 设置民族的已选项
    }
    jQuery("[name='txtIDCard']").focus();
});

/// <summary>
///验证身份证是否存在
/// </summary>
function CheckCustomerIDCard() {
    var IDCard = jQuery.trim(jQuery("[name='txtIDCard']").val()); //证件号
    if (IDCard.length == 18 || IDCard.length == 15) {
        if (parent.parent.GetCustomerIDCard(IDCard)) {
            ShowCallBackSystemDialog("对不起，证件号[" + IDCard + "]已在任务中存在，请重新填写!", jQuery("[name='txtIDCard']"));
            return false;
        }
    }
}

/// <summary>
///保存客户基本信息
/// </summary>
function SaveTeamCustomerManage(IsClose) {
    var IDCard = jQuery.trim(jQuery("[name='txtIDCard']").val()); //证件号
    if (IDCard.length == 18 || IDCard.length == 15) {
        if (parent.parent.GetCustomerIDCard(IDCard)) {
            ShowCallBackSystemDialog("对不起，证件号[" + IDCard + "]已在任务中存在，请重新填写!", jQuery("[name='txtIDCard']"));
            return false;
        }
    }
    var CustomerName = jQuery.trim(jQuery("[name='txtCustomerName']").val()); //客户名称
    var Gender = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].value; //性别
    var GenderName = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].text; //性别
    var ID_Marriage = document.getElementById("slMarriage").options[document.getElementById("slMarriage").selectedIndex].value; //性别
    var MarriageName = document.getElementById("slMarriage").options[document.getElementById("slMarriage").selectedIndex].text; //性别
    var BirthDay = jQuery.trim(jQuery("[name='txtBirthDay']").val()); //客户出生日期
    var MobileNo = jQuery.trim(jQuery("[name='txtMobileNo']").val()); //客户联系方式
    var NationID = jQuery("#idSelectNation").val();               //名族ID
    var NationName = jQuery("#nameSelectNation").val();         //名族
    var Base64Photo = jQuery("[name='HeadImg']").attr("base64photo");
    var Role = jQuery.trim(jQuery("[name='txtRole']").val()); //角色
    var Department = jQuery.trim(jQuery("[name='txtDepartment']").val()); //部门
    var DepartSubA = jQuery.trim(jQuery("[name='txtDepartSubA']").val()); //部门1
    var DepartSubB = jQuery.trim(jQuery("[name='txtDepartSubB']").val()); //部门2
    var DepartSubC = jQuery.trim(jQuery("[name='txtDepartSubC']").val()); //部门3
    var Note = jQuery.trim(jQuery("[name='txtNote']").val()); //部门3
    if (IDCard == "") {
        ShowCallBackSystemDialog("对不起，客户证件号不允许为空，请您填写!", jQuery("[name='txtIDCard']"));
        return false;
    }
    else if (CustomerName == "") {
        ShowCallBackSystemDialog("对不起，客户姓名不允许为空，请您填写!", jQuery("[name='txtCustomerName']"));
        return false;
    }
    else if (Gender == "" || Gender == "-1") {
        ShowCallBackSystemDialog("对不起，客户性别不允许为空，请您填写!");
        return false;
    }
    else if (BirthDay == "") {
        ShowCallBackSystemDialog("对不起，客户出生日期不允许为空，请您填写!", jQuery("[name='txtBirthDay']"));
        return false;
    }

    else if (MobileNo == "") {
        ShowCallBackSystemDialog("对不起，客户联系电话不允许为空，请您填写!", jQuery("[name='txtMobileNo']"));
        return false;
    }

    //验证证件号是否满足要求
    if (IDCard.length != 18) {
        ShowCallBackSystemDialog("对不起，客户证件号格式不正确，请输入正确的二代身份证号码!", jQuery("[name='txtIDCard']"));
        return false;
    }
    //判断性别、出生日期是否相符
    var ErrorMsg = "";
    var CustomerInfo = new GetUserInfoByIDCard(IDCard);
    if (CustomerInfo.birthday != BirthDay) {
        ErrorMsg += "客户出生日期和身份证不符合";
        //ShowSystemDialog("对不起，客户出生日期和身份证不符合，请输入正确的二代身份证号码!");
        //return false;
    }
    if ((CustomerInfo.sex % 2) != (Gender % 2)) {
        if (ErrorMsg != "") {
            ErrorMsg += ",且客户性别和身份证性别不符合";
        }
        else {
            ErrorMsg += "客户性别和身份证性别不符合";
        }
        //ShowSystemDialog("对不起，客户性别和身份证不符合，请输入正确的二代身份证号码!");
        //return false;
    }
    //    var qustData = {
    //        IDCard: IDCard,
    //        CustomerName: CustomerName,
    //        Gender: Gender,
    //        GenderName: GenderName,
    //        BirthDay: BirthDay,
    //        IDCard: IDCard,
    //        MobileNo: MobileNo,
    //        NationID: NationID,
    //        NationName: NationName,
    //        ID_Marriage: ID_Marriage,
    //        MarriageName: MarriageName,
    //        Base64Photo: Base64Photo,
    //        Department: Department,
    //        DepartSubA: DepartSubA,
    //        DepartSubB: DepartSubB,
    //        DepartSubC: DepartSubC
    //    };
    var CNData = {
        "身份证": IDCard,
        "姓名": CustomerName,
        "性别": GenderName,
        "出生日期": BirthDay,
        "身份证": IDCard,
        "联系电话": MobileNo,
        "民族": NationName,
        "婚姻": MarriageName,
        "头像": Base64Photo,
        "部门": Department,
        "二级部门": DepartSubA,
        "三级部门": DepartSubB,
        "四级部门": DepartSubC,
        "角色": Role,
        "备注": Note,
        "主动关闭": IsClose,
        "取消": 0
    };
    if (ErrorMsg != "") {
        art.dialog({
            lock: true, fixed: true, opacity: 0.3,
            content: ErrorMsg + ",您确定要保存吗？",
            icon: 'info',
            cancelVal: '关闭',
            cancel: true, //为true等价于function(){}
            okVal: "保存", //为true等价于function(){}
            ok: function () {
                parent.parent.art.dialog.data("TeamCustomerInfo", CNData);
                if (IsClose == 1) {
                    parent.parent.art.dialog.get('OperWindowFrame').close();
                } else {
                    parent.parent.BindCustomerInfo(CNData);
                    ResetCustomerInfo();
                }
            }
        });
    }
    else {
        parent.parent.art.dialog.data("TeamCustomerInfo", CNData);
        if (IsClose == 1) {
            parent.parent.art.dialog.get('OperWindowFrame').close();
        }
        else {
            parent.parent.BindCustomerInfo(CNData);
            ResetCustomerInfo();
        }
    }
}
function ResetCustomerInfo() {
    jQuery("[name='txtIDCard']").val("");
    jQuery("[name='txtCustomerName']").val("");
    jQuery("[name='txtBirthDay']").val("");
    jQuery("[name='txtMobileNo']").val("");
    jQuery("#idSelectNation").val("");               //名族ID
    jQuery("#nameSelectNation").val("");         //名族
    jQuery("[name='HeadImg']").attr("base64photo", "");
    jQuery("[name='HeadImg']").attr("src", defalutImagSrc);
    jQuery("[name='txtRole']").val("");
    jQuery("[name='txtDepartment']").val("");
    jQuery("[name='txtDepartSubA']").val("");
    jQuery("[name='txtDepartSubB']").val("");
    jQuery("[name='txtDepartSubC']").val("");
    jQuery("[name='txtNote']").val("");
    jQuery("[name='txtIDCard']").focus();
}
function SetTeamCustomerInfo(dataList, selectedID_ArcCustomer, IsReadCard) {

    if (dataList == null || dataList == undefined)
        return false;
    var item;
    if (dataList.length > 0) {

        for (var c = 0; c < dataList.length; c++) {
            item = dataList[c];
            if (selectedID_ArcCustomer != "" && selectedID_ArcCustomer != item.ID_ArcCustomer) {
                continue;
            }
            if (IsReadCard != 1)//如果不是读卡
            {
                jQuery("[name='HeadImg']").attr("src", defalutImagSrc);
                jQuery("[name='HeadImg']").attr("base64photo", "");
                //设置头像
                if (item.Base64Photo == "") {
                    jQuery("[name='HeadImg']").attr("src", defalutImagSrc);
                }
                else {
                    if (selectedID_ArcCustomer == item.ID_ArcCustomer) {
                        jQuery("[name='HeadImg']").attr("src", "data:image/gif;base64," + item.Base64Photo + "");
                        jQuery("[name='HeadImg']").attr("Base64Photo", item.Base64Photo);
                    }
                }
                if (dataList.length == 1) {
                    if (item.Base64Photo == "") {
                        jQuery("[name='HeadImg']").attr("src", defalutImagSrc);
                    }
                    else {
                        jQuery("[name='HeadImg']").attr("src", "data:image/gif;base64," + item.Base64Photo + "");
                        jQuery("[name='HeadImg']").attr("Base64Photo", item.Base64Photo);
                    }
                }
            }
            jQuery("[name='txtIDCard']").val(item.IDCard);
            jQuery("[name='txtCustomerName']").val(item.CustomerName);
            jQuery("[name='txtBirthDay']").val(item.date);
            jQuery("[name='txtMobileNo']").val(item.MobileNo);
            jQuery("#slSex [value='" + parseInt(item.ID_Gender) + "']").attr("selected", true);
            jQuery("#slMarriage [value='" + item.ID_Marriage + "']").attr("selected", true);
            if (item.NationID != "") {
                ShowQuickSelectNation(parseInt(item.NationID), NationArray[parseInt(item.NationID) - 1]);          // 设置民族的已选项 xmhuang 2013-10-18 获取客户基本信息，需要使用此方法设置民族
            }

        }
    }


    jQuery("#lblErrorMessage").text("");
}

//选择用户基本信息,调用此方法时，用户身份证信息是>1条的
function ChooseTeamCustomerInfo(dataList) {
    var content = '<table id="tbChoose" class="tblList tblExamItemTable">';
    var imgSrc = "";
    jQuery(dataList).each(function (i, item) {
        if (item.Base64Photo == "") {
            imgSrc = defalutImagSrc;
        }
        else {
            imgSrc = "data:image/gif;base64," + item.Base64Photo;
        }
        content += ' <tr ID_ArcCustomer="' + item.ID_ArcCustomer + '" IDCard="' + item.IDCard + '" CustomerName="' + item.CustomerName + '" onclick="SureChoose(this)"><td rowspan="4"><img id="HeadImg1" name="HeadImg1" class="headPic" src="' + imgSrc + '" /></td><td>' + item.IDCard + '</td></tr>';
        content += '<tr><td>' + item.CustomerName + '</td></tr>';
        content += '<tr><td>' + item.date + '</td></tr>';
        content += '<tr><td>' + item.GenderName + '</td></tr>';
        //content += '<tr id="trChoose_"' + item.ID_ArcCustomer + '" IDCard="trChoose_"' + item.IDCard + '" CustomerName="trChoose_"' + item.CustomerName + '"><td>' + item.IDCard + '</td><td>' + item.CustomerName + '</td><td>' + item.date + '</td><td>' + item.ID_Gender + '</td></tr>';
    });
    content += '</table>';

    var dialog = art.dialog({
        lock: true, fixed: true, opacity: 0.3, width: 320,
        height: 240,
        id: "dialog1",
        title: '请选择客户信息(点击图片即可选择)',
        content: content,
        follow: document.getElementById('txtSFZ')
    }).lock();
    jQuery(window).data("dataList", dataList);
}
function SureChoose(obj, dataList) {
    var ID_ArcCustomer = jQuery(obj).attr("ID_ArcCustomer");
    var CurdataList = jQuery(window).data("dataList");
    SetTeamCustomerInfo(CurdataList, ID_ArcCustomer);
    CloseDialog("dialog1");
}
function CloseDialog(dialogID) {
    var list = art.dialog.list;
    for (var i in list) {
        if (i == dialogID) {
            list[i].close();
        }
    };
}
function RequestCustomerInfo(IDCard, CustomerName, IsReadCard) {
    //IsGenerateCustomerCard为1表示自定义身份证,为0则表示只通过证件号进行筛选，这里通过证件号和姓名作为过滤条件进行筛选
    var IsGenerateCustomerCard = 1;
    var data = { action: "GetCustomerByIDCardX", IsGenerateCustomerCard: IsGenerateCustomerCard, Key: "IDCard", KeyValue: IDCard, CustomerName: encodeURIComponent(CustomerName) };
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxRegiste.aspx",
        data: data,
        cache: false,
        dataType: "json",
        success: function (msg) {
            //这里绑定客户基本信息
            var msgLength = msg.dataList.length;
            //如果客户信息大于两条则提示操作者选择客户信息后绑定其基本信息
            if (msgLength > 0) {
                //只存在一个客户信息
                if (msgLength == 1) {
                    SetTeamCustomerInfo(msg.dataList, '', IsReadCard);
                }
                //存在相同身份证多个客户的情况
                else {
                    ChooseTeamCustomerInfo(msg.dataList);
                }
            }
            else {
                //ShowSystemDialog("对不起,未检索到客户信息");
                ShowCallBackSystemDialog("对不起，未检索到此证件号的信息!", jQuery("[name='txtIDCard']"));
                jQuery("[name='txtIDCard']").val(IDCard);
                jQuery("[name='txtCustomerName']").val(CustomerName);
                return false;
            }
        }
    });
}
/// <summary>
/// 从银安读卡器中读取身份证信息 xmhuang 2013-10-21
/// </summary>
function SearchIDCard() {
    
    //判断身份证号码是否存在，存在则通过ajax请求获取该身份证的信息
    var CurIDCard = jQuery.trim(jQuery("[name='txtIDCard']").val());
    var CustomerName = jQuery.trim(jQuery("[name='txtCustomerName']").val()); //客户名称
    if (CurIDCard != "") {
        if (CurIDCard.length == 15 || CurIDCard.length == 18) {
            //            if (CustomerName == "") {
            //                ShowCallBackSystemDialog("对不起，请输入客户名称进行检索!", jQuery("[name='txtCustomerName']"));
            //                return false;
            //            }
            //            else {
            //这里从后台查询指定证件号的客户基本信息
            ResetCustomerInfo(); //重置信息
            RequestCustomerInfo(CurIDCard, "");
            //}
        }
        else {
            ShowCallBackSystemDialog("对不起，请输入正确的证件号!", jQuery("[name='txtIDCard']"));
            return false;
        }
    }
    else {
        ResetCustomerInfo(); //重置信息
        this.errorMsg = "";
        var IsUpdateCustomerPhoto = 1;
        var SynCardOcxOne = document.getElementById("CVR_IDCard"); //获取身份证插件
        var ret = SynCardOcxOne.ReadCard();
        if (ret == 0) {
            //判断卡片的身份证号码和当前的身份证号码是否匹配
            var IDCard = jQuery.trim(jQuery("[name='txtIDCard']").val()); //客户证件号
            var CardNo = SynCardOcxOne.CardNo;
            jQuery("[name='txtCustomerName']").val(jQuery.trim(SynCardOcxOne.Name));
            jQuery("[name='txtIDCard']").val(jQuery.trim(SynCardOcxOne.CardNo));
            var Born = jQuery.trim(SynCardOcxOne.Born);
            jQuery("[name='txtBirthDay']").val(Born.substr(0, 4) + "-" + Born.substr(4, 2) + "-" + Born.substr(6, 2));
            if (SynCardOcxOne.Sex == "男") {
                jQuery("[name='slSex'] [value='1']").attr("selected", true);
            }
            else {
                jQuery("[name='slSex'] [value='2']").attr("selected", true);
            }
            //jQuery("[name='slSex'] [value='" + SynCardOcxOne.Sex + "']").attr("selected", true);
            jQuery("[name='HeadImg']").attr("src", "data:image/gif;base64," + SynCardOcxOne.Picture + "");
            jQuery("[name='HeadImg']").attr("base64photo", SynCardOcxOne.Picture);
            ShowQuickSelectNation(parseInt(SynCardOcxOne.NationCode), NationArray[SynCardOcxOne.NationCode - 1]); // 设置民族的已选项 xmhuang 2013-10-11 19:28
            // }
            //设置用户信息
            this.errorMsg += "成功读取身份信息";
            this.flag = true;
            jQuery("#lblErrorMessage").text(this.errorMsg);
            jQuery("#lblErrorMessage").text("");
            RequestCustomerInfo(SynCardOcxOne.CardNo, SynCardOcxOne.Name, 1); //检索证件号和姓名的客户是否存在，存在则重新绑定基本信息 xmhuang 2014-02-24
            CheckCustomerIDCard();
        }
        else {
            this.flag = false;
            this.errorMsg += "读取身份信息失败,请您确认身份证已正确安放！";
            jQuery("#lblErrorMessage").text(this.errorMsg);
        }
        return this.flag;
    }
}
function HideNationQuickTable() {
    ShowHideQuickQueryNationTable(false, "");   // 民族
}
function Cancel() {
    var CNData = {
        "取消": 1
    };
    parent.parent.art.dialog.data("TeamCustomerInfo", CNData);
    parent.parent.art.dialog.get('OperWindowFrame').close();
}