$(function () { 

    //修改密码
    $("#newpass").attr("disabled", true);
    $("#renewpass").attr("disabled", true);
    adddomClass("#changepass", "disabled-mouse");
    $("#mpass").blur(function () {
        var mpass = $("#mpass").val();
        var tips = IsPasswordLevel(mpass);
        $("#mpass").after("&nbsp;&nbsp; <span class='tipscolor tipsfont' id='tip' >" + tips + "</span>");
        if (tips === "空密码") {
            $("#mpass").focus();
        } else {
            $("#newpass").attr("disabled", false);
        }
        setTimeout("removedomid(tip)", 2000);
    });
    $("#newpass").blur(function () {
        var mpass = $("#newpass").val();
        var tips = IsPasswordLevel(mpass);
        $("#newpass").after(" &nbsp;&nbsp;<span class='tipscolor tipsfont' id='tip'>" + tips + "</span>");
        if (tips === "空密码") {
            $("#newpass").focus();
        } else {
            $("#renewpass").attr("disabled", false);
        }
        setTimeout("removedomid(tip)", 3000);
    });
    $("#renewpass").blur(function () {
        var newpass = $("#newpass").val();
        var renewpass = $("#renewpass").val();
        if (newpass != renewpass) {
            $("#renewpass").after(" &nbsp;&nbsp;<span class='tipscolor tipsfont' id='tip'>密码输入不一致，请重新输入</span>");
            $("#renewpass").val("");
            $("#newpass").val("").focus();
            setTimeout("removedomid(tip)", 3000);
        } else {
            removedomclass("#changepass", "disabled-mouse");
        }
    });
    $("#changepass").bind("click", changepass);
    $("#userLoginName").html(getCookies("UserLoginName"));

})