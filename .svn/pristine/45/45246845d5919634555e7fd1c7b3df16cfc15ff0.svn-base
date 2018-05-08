/**
 * Created by gjq on 2017/2/23.
 */
$(function () {
    $(".signin").bind("click", login);
    $("#username").focus();
    $("#username").val("");
    $("#password").val("");
});
function login() {
    $("#loginbtn").addClass("disabled-mouse");
    var handsetno = $("#username").val();
    var password = $("#password").val();
    var VerifyCode = $("#VerifyCode").val();

    //var name = handsetno.replace(/[^a-zA-Z]/g,'');
    if (handsetno != "" && password != "") {
        $("#loginbtn").val("登陆中。。。");
        $.ajax({
            async: true,
            url: "/ajax/AjaxUser.aspx?action=UserLogin",
            type: "post",
            data: { "UserLoginName": handsetno, "UserPassword": password, "VerifyCode": VerifyCode, "param": Math.random() },
            timeout: 2000,
            success: function (data) {
                if (data == "登录成功") {
                    //location.replace('/System/Index.aspx?IsLogin=1');
                    window.open("/System/index.aspx?IsLogin=1","_self");
                } else {
                    //如果登陆不成功则直接打印错误信息给用户看到
                    $("#tips").text(data);
                    $("#username").val("").focus();
                    $("#password").val("");
                    setTimeout("cleanUp(tips)", 5000);
                }
                // window.location.href = "index.html"//登陆成功
            },
            error: function () {
                $("#tips").text("登陆失败请联系管理员");
                $("#password").val("");
                $("#username").val("").focus();

                setTimeout("cleanUp(tips)", 10000);
            },
            complete: function () {
                $("#loginbtn").removeClass("disabled-mouse");
                $("#loginbtn").val("登陆");
            }
        })
    } else {
        $("#tips").text("请将项目输入完整");
        if (handsetno != "") {
            $("#password").focus();
        } else {
            $("#username").focus();
        }
        setTimeout("cleanUp(tips)", 5000);
    }
}

function onPressEnterKey() {
    var curEvent = window.event || e;
    var id = document.activeElement.id;
    if (curEvent.keyCode == 13) {
        if (id == "password" || id == "loginbtn") { // 如果是 密码输入框或是登录按钮，则直接提交
            login();
        } else if (id == "username") {             // 如果是用户名输入框，则焦点移动到密码框中
            // 当密码不为空时，直接触发登录事件
            if ($("#password").val() != "") {
                login();
            } else {
                $("#password").focus();
            }
        }
    }
}
