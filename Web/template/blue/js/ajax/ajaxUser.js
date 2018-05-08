

//用户登陆
function UserLoginAjax() {
    // $("#btnLogin").attr("disabled", "disabled");
    var UserLoginName = $("#UserLoginName").val();
    var UserPassword = $("#UserPassword").val();
    var VerifyCode = $("#VerifyCode").val();
    $.post("/ajax/AjaxUser.aspx?action=UserLogin", { UserLoginName: UserLoginName, UserPassword: UserPassword,
        VerifyCode: VerifyCode,
        param: Math.random()
    },
        function (data) {
            if (data != "" && data != null) {
                $("#UserLoginTips").html(data);
                if (data == "登录成功") {
                    location.replace('/System/Index.aspx?IsLogin=1');

                } else {
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
    });
}

