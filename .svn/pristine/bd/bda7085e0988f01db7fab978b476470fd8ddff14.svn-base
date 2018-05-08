/*$(function(){
    $.ajax({
        //请求baseurl
        async: false,//必须先执行这个函数请求配置的baseurl
        url:"",//服务器ip地址
        type: "get",//get请求
        //data: {"handsetNo": handsetno,"password":password,
        timeout: 2000,
        success: function (data) {
            //$("#loginbtn").removeClass("disabled-mouse");
            baseurl = data.baseurl;
        },
        error: function () {
            alert("服务器无响应，请联系管理员查看，错误代码001");
        }
    })
})*/
var baseUrl = "/template/blue2";
/*var baseUrl = "../.."*/
/*设置html*/
//document.write('<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />');
document.write('<meta http-equiv="X-UA-Compatible" content="IE=edge">');
document.write('<meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1.0,user-scalable=no">');
//document.write('<title>xxxx健康管理中心</title>');
//document.write('<link href="$!{baseurl}/xyy.ico" rel="icon"> ');
/*库文件*/
document.write('<script src="'+baseUrl+'/js/skin05/lib/jquery-3.1.1.min.js"></script>');
/*工具文件*/
document.write('<script src="'+baseUrl+'/js/skin05/common/tools.js"></script>');
/*document.write('<script src="data.js"></script>');
/!*配置文件*!/
document.write('<script src="conf.js"></script>');*/
/*页面引用*/
var strUrl=window.location.href;
var arrUrl=strUrl.split("/");
var strPage=arrUrl[arrUrl.length-1];
//var strPage1 = strPage.toString();
/*var   gettype=Object.prototype.toString
console.log (gettype.call(strPage));
console.log (typeof(strPage));*/
if(strPage.indexOf("Login.aspx")>=0){
    document.write('<link rel="stylesheet" href="' + baseUrl + '/LoginSkin/Skin04/css/style.css">');
    document.write('<script src="'+baseUrl+'/js/skin05/pages/login.js"></script>');
} else if (strPage.indexOf("index.aspx") >= 0 || strPage.indexOf("pass.html") >= 0) {

    //document.write('<script src="' + baseUrl + '/js/skin05/lib/bootstrap.min.js"></script>');
   // document.write('<link rel="stylesheet" href="' + baseUrl + '/css/skin05/lib/bootstrap.min.css">');
    document.write('<link rel="stylesheet" href="' + baseUrl + '/css/skin05/pages/pintuer.css">');
    document.write('<link rel="stylesheet" href="' + baseUrl + '/css/skin05/pages/admin.css">');
    document.write('<link rel="stylesheet" href="' + baseUrl + '/css/skin05/common.css">');
    //document.write('<script src="../js/pintuer.js"></script>');
    document.write('<script src="' + baseUrl + '/js/skin05/pages/common.js"></script>');
    document.write('<script src="' + baseUrl + '/js/skin05/pages/ajaxdata.js"></script>');
    document.write('<script src="' + baseUrl + '/js/skin05/common/check.js"></script>');
    document.write('<script src="' + baseUrl + '/js/skin05/pages/indexListModule.js"></script>');
    //document.write('<script src="'+ baseUrl+ '/js/skin05/pages/index.js"></script>');
    //时间选择
    document.write('<link rel="stylesheet" href="' + baseUrl + '/css/skin05/lib/flatpickr.min.css">');
    document.write('<script src="' + baseUrl + '/js/skin05/lib/flatpickr.js"></script>');
} else {
    document.write('<script src="' + baseUrl + '/js/skin05/lib/bootstrap.min.js"></script>');
    document.write('<link rel="stylesheet" href="' + baseUrl + '/css/skin05/lib/bootstrap.min.css">');
    document.write('<link rel="stylesheet" href="' + baseUrl + '/css/skin05/pages/pintuer.css">');
    document.write('<link rel="stylesheet" href="' + baseUrl + '/css/skin05/pages/admin.css">');
    document.write('<link rel="stylesheet" href="' + baseUrl + '/css/skin05/common.css">');
    //document.write('<script src="../js/pintuer.js"></script>');
    document.write('<script src="' + baseUrl + '/js/skin05/pages/common.js"></script>');
    document.write('<script src="' + baseUrl + '/js/skin05/pages/ajaxdata.js"></script>');
    document.write('<script src="' + baseUrl + '/js/skin05/common/check.js"></script>');
    document.write('<script src="' + baseUrl + '/js/skin05/pages/indexListModule.js"></script>');
    //document.write('<script src="'+ baseUrl+ '/js/skin05/pages/index.js"></script>');
    //时间选择
    document.write('<link rel="stylesheet" href="' + baseUrl + '/css/skin05/lib/flatpickr.min.css">');
    document.write('<script src="' + baseUrl + '/js/skin05/lib/flatpickr.js"></script>');
    //子页面通用js
    document.write('<script src="' + baseUrl + '/js/skin05/pages/sonPageCommon.js"></script>');
    //原js
    //document.write(' <link rel="stylesheet" href="' + baseUrl + '/css/style.css" type="text/css" />');
   // document.write('<link rel="stylesheet" href="' + baseUrl + '/css/new.css" type="text/css" />');
    //document.write('<link rel="stylesheet" href="' + baseUrl + '/css/exam.css" />');
    //document.write('<link rel="stylesheet" href="' + baseUrl + '/css/uniform.css" />');
   // document.write('<script type="text/javascript" src="' + baseUrl + '/js/jquery.min.js"></script>');
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/jquery.md5.js"></script>');
    //document.write('<script type="text/javascript" src="' + baseUrl + '/js/bootstrap.min.js"></script>');
    //document.write('<script type="text/javascript" src="' + baseUrl + '/js/jquery.uniform.js"></script>');
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/select2.min.js"></script>');
    //document.write('<script type="text/javascript" src="' + baseUrl + '/js/My97DatePicker/WdatePicker.js"></script>');
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/Commom.js"></script>');
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/JS_FloatRight.js"></script>');
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/jquery.pagination.js"></script>');
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/artDialog4.1.7/artDialog.source.js?skin=opera"></script>');
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/artDialog4.1.7/plugins/iframeTools.source.js"></script>');
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/WebIm.js"></script>');

    document.write('<script type="text/javascript" src="' + baseUrl + '/js/Enlarge-Editor.js"></script>');
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/Jq-common.js"></script>');
    document.write(' <script type="text/javascript" src="' + baseUrl + '/js/Total-seizure.js"></script>');

    //<!--<script type="text/javascript" src="' + baseUrl + '/js/stripe.js"></script>-->
    document.write(' <script type="text/javascript" src="' + baseUrl + '/js/wp-auto-top.js"></script>');
    //<!--表头固定插件 xmhuang 2014-04-02-->
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/jquery.tablefix_1.0.1.js"></script>');
    //<!--表横向滚动插件 xmhuang 2014-04-04引入-->
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/table-scroll-x.js"></script>');
    //<!--表单拖动 xmhuang 2014-04-04引入-->
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/dragDrop.min.js"></script>');
    //document.write('<script type="text/javascript" src="' + baseUrl + '/js/JS_index.js"></script>');
    //<!-- jQuery poshytip -->
    document.write('<script type="text/javascript" src="' + baseUrl + '/js/jquery.poshytip.min.js"></script>');
}


