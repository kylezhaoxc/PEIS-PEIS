

//用户登陆
function UserLoginAjax() {
    // $("#btnLogin").attr("disabled", "disabled");
    var UserLoginName = $("#UserLoginName").val();
    var UserPassword = $("#UserPassword").val();
    if (jQuery.trim(UserLoginName) == "" ) {
        LoginTips("请输入您的用户名！", "UserLoginName");
        return;
    }
    if (jQuery.trim(UserPassword) == "") {
        LoginTips("请输入您的密码！", "UserPassword");
        return;
    }
    var VerifyCode = $("#VerifyCode").val();
    $.post("/ajax/AjaxUser.aspx?action=UserLogin", { UserLoginName: UserLoginName, UserPassword: UserPassword,
        VerifyCode: VerifyCode,
        param: Math.random()
    },
        function (data) {
            if (data != "" && data != null) {
                $("#UserLoginTips").html(data,"");
                if (data == "登录成功") {
                    location.replace('/System/Index.aspx?IsLogin=1');

                } else {
                    LoginTips(data);
                    if (data == "用户名不存在") {
                        jQuery("#UserLoginName").focus();
                        jQuery("#UserLoginName").select();
                        jQuery("#UserPassword").val("");
                    } else {
                        //  如果用户名输入框为空，则用户名输入框获取焦点，否则密码输入框获取焦点
                        if (jQuery("#UserLoginName").val() == "") {
                            jQuery("#UserLoginName").focus();
                        } else {
                            jQuery("#UserPassword").focus();
                        }
                    }
                }

                return true;
            }
        }
    )
    .complete(function () {
        $("#btnLogin").removeAttr("disabled");
    })
    .success(function () { })
    .error(function () {
        $("#btnLogin").removeAttr("disabled");
        $("#UserLoginTips").html("登录失败，请与系统管理人员联系！");
        LoginTips("登录失败，请与系统管理人员联系！","");
    });
}

function LoginTips(msg,idfoucs) {

    msg = "<span style='font-size:12px;'>" + msg + "</span>";
    art.dialog({
        id: 'artLoginDialogID',
        lock: true,
        fixed: true,
        opacity: 0.3,
        time: 2,
        content: msg,
        button: [{
            name: '确定',focus:true,
            callback: function () {
                if (idfoucs != "") {
                    jQuery("#" + idfoucs).focus();
                } else {
                    jQuery("#UserLoginName").focus();
                }
            }
        }],
        close: function () {
            if (idfoucs != "") {
                jQuery("#" + idfoucs).focus();
            } else {
                jQuery("#UserLoginName").focus();
            }
        }

    });
}

