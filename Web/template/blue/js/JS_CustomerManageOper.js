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
});

/// <summary>
///保存客户基本信息
/// </summary>
function SaveCustomerManage() {
    var ID_ArcCustomer = jQuery.trim(jQuery("[name='lblID_ArcCustomer']").text()); //客户存档ID
    var CustomerName = jQuery.trim(jQuery("[name='txtCustomerName']").val()); //客户名称
    var Gender = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].value; //性别
    var GenderName = document.getElementById("slSex").options[document.getElementById("slSex").selectedIndex].text; //性别
    var ID_Marriage = document.getElementById("slMarriage").options[document.getElementById("slMarriage").selectedIndex].value; //性别
    var MarriageName = document.getElementById("slMarriage").options[document.getElementById("slMarriage").selectedIndex].text; //性别
    var BirthDay = jQuery.trim(jQuery("[name='txtBirthDay']").val()); //客户出生日期
    var IDCard = jQuery.trim(jQuery("[name='txtIDCard']").val()); //客户证件号
    var MobileNo = jQuery.trim(jQuery("[name='txtMobileNo']").val()); //客户联系方式
    var NationID = jQuery("#idSelectNation").val();               //名族ID
    var NationName = jQuery("#nameSelectNation").val();         //名族
    var Base64Photo = jQuery("[name='HeadImg']").attr("base64photo");
    if (ID_ArcCustomer == "") {
        ShowSystemDialog("获取客户存档ID失败，请关闭本页面后重试!");
        return false;
    }
    else if (CustomerName == "") {
        ShowSystemDialog("对不起，客户姓名不允许为空，请您填写!");
        jQuery("[name='txtCustomerName']").focus();
        return false;
    }

    else if (Gender == "" || Gender == "-1") {
        ShowSystemDialog("对不起，客户性别不允许为空，请您填写!");

        return false;
    }
    else if (BirthDay == "") {
        ShowSystemDialog("对不起，客户出生日期不允许为空，请您填写!");
        jQuery("[name='txtBirthDay']").focus();
        return false;
    }
    else if (IDCard == "") {
        ShowSystemDialog("对不起，客户证件号不允许为空，请您填写!");
        jQuery("[name='txtIDCard']").focus();
        return false;
    }
    else if (MobileNo == "") {
        ShowSystemDialog("对不起，客户联系电话不允许为空，请您填写!");
        jQuery("[name='txtMobileNo']").focus();
        return false;
    }

    //验证证件号是否满足要求
    if (IDCard.length != 18) {
        ShowSystemDialog("对不起，客户证件号格式不正确，请输入正确的二代身份证号码!");
        jQuery("[name='txtIDCard']").focus();
        jQuery("[name='txtIDCard']").select();
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
    var qustData = {
        ID_ArcCustomer: ID_ArcCustomer,
        CustomerName: CustomerName,
        Gender: Gender,
        GenderName: GenderName,
        BirthDay: BirthDay,
        IDCard: IDCard,
        MobileNo: MobileNo,
        NationID: NationID,
        NationName: NationName,
        ID_Marriage: ID_Marriage,
        MarriageName: MarriageName,
        Base64Photo: Base64Photo,
        action: "SaveCustomerManage"
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

                //这里执行Ajax请求保存客户基本信息
                //存储大数据请设置Content-length值
                jQuery.ajax({
                    type: "POST",
                    url: "/Ajax/AjaxCustomerManage.aspx",
                    data: qustData,
                    cache: false,
                    contentType: "application/x-www-form-urlencoded;Content-length=1024000",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.success == 0) {
                            ShowSystemDialog(msg.Message);
                            return false;
                        }
                        else if (msg.success == 1) {
                            CloseDialogWindow();
                        }
                    },
                    complete: function () {
                    }
                });
            }
        });
    }
    else {
        //存储大数据请设置Content-length值
        jQuery.ajax({
            type: "POST",
            url: "/Ajax/AjaxCustomerManage.aspx",
            data: qustData,
            cache: false,
            contentType: "application/x-www-form-urlencoded;Content-length=1024000",
            dataType: "json",
            success: function (msg) {
                if (msg.success == 0) {
                    ShowSystemDialog(msg.Message);
                    return false;
                }
                else if (msg.success == 1) {
                    CloseDialogWindow();
                }
            },
            complete: function () {

            }
        });
    }
}
/// <summary>
/// 从银安读卡器中读取身份证信息 xmhuang 2013-10-21
/// </summary>
function SearchIDCard() {
    var IsUpdateCustomerPhoto = 1;
    var SynCardOcxOne = document.getElementById("CVR_IDCard"); //获取身份证插件
    var ret = SynCardOcxOne.ReadCard();
    if (ret == 0) {
        //判断卡片的身份证号码和当前的身份证号码是否匹配
        var IDCard = jQuery.trim(jQuery("[name='txtIDCard']").val()); //客户证件号
        var CardNo = SynCardOcxOne.CardNo;
        if (IDCard != CardNo) {
            art.dialog({
                lock: true, fixed: true, opacity: 0.3,
                content: "客户证件号[" + IDCard + "]和读取的证件号[" + CardNo + "]不匹配,您确定要继续吗？",
                icon: 'info',
                cancelVal: '取消',
                cancel: true, //为true等价于function(){}
                okVal: "继续", //为true等价于function(){}
                ok: function () {
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

                }
            });
        }
        else {
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
        }
        //设置用户信息
        this.errorMsg += "成功读取身份信息";
        this.flag = true;
        jQuery("#lblErrorMessage").text(this.errorMsg);
        jQuery("#lblErrorMessage").text("");
    }
    else {
        this.flag = false;
        this.errorMsg += "读取身份信息失败,请您确认身份证已正确安放！";
        jQuery("#lblErrorMessage").text(this.errorMsg);
    }

    return this.flag;
}
var str = "";
/// <summary>
/// 从新中兴读卡器中读取身份证信息
/// </summary>
function SearchIDCard_XZX() {
    var IsUpdateCustomerPhoto = 1;
    var SynCardOcxOne = document.getElementById("SynCardOcxOne"); //获取身份证插件
    //设置身份证图片保存格式为jepg
    try {
        SynCardOcxOne.SetPhotoType(1);
    } catch (e) { }

    this.errorMsg = "";
    this.flag = false;
    if (str == "") {
        str = SynCardOcxOne.FindReader();
    }
    if (str > 0) {
        if (str > 1000) {
            this.errorMsg = "读卡器连接在USB " + str;
            this.flag = true;
            jQuery("#lblErrorMessage").text(this.errorMsg);
        }
        else {
            this.errorMsg = "读卡器连接在COM " + str;
            this.flag = true;
            jQuery("#lblErrorMessage").text(this.errorMsg);
        }
        var nRet = SynCardOcxOne.ReadCardMsg();
        if (nRet == 0) {
            //判断卡片的身份证号码和当前的身份证号码是否匹配
            var IDCard = jQuery.trim(jQuery("[name='txtIDCard']").val()); //客户证件号
            var CardNo = SynCardOcxOne.CardNo;

            if (IDCard != CardNo) {
                art.dialog({
                    lock: true, fixed: true, opacity: 0.3,
                    content: "客户证件号[" + IDCard + "]和读取的证件号[" + CardNo + "]不匹配,您确定要继续吗？",
                    icon: 'info',
                    cancelVal: '取消',
                    cancel: true, //为true等价于function(){}
                    okVal: "继续", //为true等价于function(){}
                    ok: function () {
                        jQuery("[name='txtCustomerName']").val(jQuery.trim(SynCardOcxOne.NameA));
                        jQuery("[name='txtIDCard']").val(jQuery.trim(SynCardOcxOne.CardNo));
                        var Born = jQuery.trim(SynCardOcxOne.Born);
                        jQuery("[name='txtBirthDay']").val(Born.substr(0, 4) + "-" + Born.substr(4, 2) + "-" + Born.substr(6, 2));
                        jQuery("[name='slSex'] [value='" + SynCardOcxOne.Sex + "']").attr("selected", true);
                        jQuery("[name='HeadImg']").attr("src", "data:image/gif;base64," + SynCardOcxOne.Base64Photo + "");
                        jQuery("[name='HeadImg']").attr("base64photo", SynCardOcxOne.Base64Photo);
                        ShowQuickSelectNation(parseInt(SynCardOcxOne.Nation), NationArray[SynCardOcxOne.Nation - 1]); // 设置民族的已选项 xmhuang 2013-10-11 19:28

                    }
                });
            }
            else {
                jQuery("[name='txtCustomerName']").val(jQuery.trim(SynCardOcxOne.NameA));
                jQuery("[name='txtIDCard']").val(jQuery.trim(SynCardOcxOne.CardNo));
                var Born = jQuery.trim(SynCardOcxOne.Born);
                jQuery("[name='txtBirthDay']").val(Born.substr(0, 4) + "-" + Born.substr(4, 2) + "-" + Born.substr(6, 2));
                jQuery("[name='slSex'] [value='" + SynCardOcxOne.Sex + "']").attr("selected", true);
                jQuery("[name='HeadImg']").attr("src", "data:image/gif;base64," + SynCardOcxOne.Base64Photo + "");
                jQuery("[name='HeadImg']").attr("base64photo", SynCardOcxOne.Base64Photo);
                ShowQuickSelectNation(parseInt(SynCardOcxOne.Nation), NationArray[SynCardOcxOne.Nation - 1]); // 设置民族的已选项 xmhuang 2013-10-11 19:28
            }
            //设置用户信息
            this.errorMsg += ",成功读取身份信息";
            this.flag = true;
            jQuery("#lblErrorMessage").text(this.errorMsg);
            jQuery("#lblErrorMessage").text("");
        }
        else {
            this.flag = false;
            this.errorMsg += ",读取身份信息失败,请您确认身份证已正确安放！";
            jQuery("#lblErrorMessage").text(this.errorMsg);
        }
    }
    else {
        this.errorMsg = "没有找到读卡器";
        this.flag = false;
        jQuery("#lblErrorMessage").text(this.errorMsg);
    }
    return this.flag;
}


function HideNationQuickTable() {
    ShowHideQuickQueryNationTable(false, "");   // 民族
}
