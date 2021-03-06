﻿var url = $('#iframe', parent.document).prop("src");
var quickSearchBoxData = "";
var modelName = getUrlParam(url, "modelName");
var ISTEAM = getUrlParam(url, "IsTeam");
var type =  getUrlParam(url, "type");
if (modelName === "Sign" && ISTEAM === "1" ) {
    quickSearchBoxData = [{ type: 0, inputid: "txtSearchX", inputtype: "number", placeholder: "证件号/体检号", name: "btnSearchX", btnid: "btnSearch", btnbgcolor: "btn-blue", function: "vailData(5)", tip: "搜索" },
    { type: 2, name: "", function: "vailData(1)", tip: "保存", btnbgcolor: "bg-green", icon: "icon-floppy-o", btnid: "btnSaveData", btnname: "btnComplete" },
    { type: 2, name: "", function: "vailData(4)", tip: "补打", btnbgcolor: "bg-blue", icon: "icon-print", btnid: "btnPrintCust", btnname: "btnPrintCust" },
    { type: 2, name: "", function: "vailData(0)", tip: "完成", btnbgcolor: "bg-red", icon: "icon-check", btnid: "btnRegiste", btnname: "btnComplete" },
    { type: 2, name: "", function: "vailData(6)", tip: "列表", btnbgcolor: "bg-blue", icon: "icon-bars", btnid: "", btnname: "" }
    ];
} else if (modelName === "SignAndRegiste" && ISTEAM === "0" ) {
    quickSearchBoxData = [{ type: 0, inputid: "txtSearchX", inputtype: "number", placeholder: "体检号", name: "btnSearchX", btnid: "btnSearch", btnbgcolor: "btn-blue", function: "vailData(5)", tip: "搜索" },
   { type: 2, name: "", function: "vailData(1)", tip: "保存", btnbgcolor: "bg-green", icon: "icon-floppy-o", btnid: "btnSaveData", btnname: "btnComplete" },
   { type: 2, name: "", function: "vailData(4)", tip: "补打", btnbgcolor: "bg-blue", icon: "icon-print", btnid: "btnPrintCust", btnname: "btnPrintCust" },
   { type: 2, name: "", function: "vailData(0)", tip: "完成", btnbgcolor: "bg-red", icon: "icon-check", btnid: "btnRegiste", btnname: "btnComplete" },
   { type: 2, name: "", function: "vailData(2)", tip: "读证件", btnbgcolor: "bg-blue", icon: "icon-id-card", btnid: "btnReadFromMachine", btnname: "btnReadFromMachine" },
   { type: 2, name: "", function: "vailData(3)", tip: "无证件", btnbgcolor: "bg-red", icon: "icon-user-times", btnid: "", btnname: "" },
   { type: 2, name: "", function: "vailData(6)", tip: "列表", btnbgcolor: "bg-blue", icon: "icon-bars", btnid: "", btnname: "" }
    ];
}

QuickSearchBox(quickSearchBoxData);

function completePhoto() {
   
    if ($("#HeadImg") == undefined) {
        return false;
    }
    if (base64img == "") {
        ShowSystemDialog("获取图像失败,请确保您已安装视频驱动并且视频已正常连接到电脑!");
        return true;
    }
    $("#headimg").css({ "margin-left": "-50px" });
    $("#paizhao").css({ "margin-left": "70px" });
    $("#HeadImg").attr("src", base64img);
   // jQuery("#divBox").attr("src", "data:image/gif;base64," + base64img + "");
    var base64data = base64img.substr(22);
    $("#HeadImg").attr("Base64Photo", base64data);
    $("#HeadImg").attr("IsExist", 1); //标记该头像已经存在，不用再重新设置    
    //$("#myModal").hide();
    $(".modal-body").html("");
    $(this).removeData("bs.modal");
    return true;
   
}
function takePictureButton() {
    var picture = '<div style="width:100px;overflow-x:hidden"><video id="video" width="160" height="120" autoplay></video></div><button id="snap">拍照</button><div style="margin-left:-60px"><canvas id="canvas" width="160" height="120"></canvas></div>';
    $(".modal-body").html(picture);
    takePicture();
   // $("#takePicture").hide();
}
$(function () {
    
    $("#txtBirthDay").flatpickr({ onChange: function () { CaltAgeByBirthDay() } })
})