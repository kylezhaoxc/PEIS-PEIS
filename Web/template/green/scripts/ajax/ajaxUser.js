

//用户登陆
function UserLoginAjax() {

    var UserLoginName = $("#UserLoginName").val();
    var UserPassword = $("#UserPassword").val();
    var VerifyCode = $("#VerifyCode").val();

    $.post("/ajax/AjaxUser.aspx?action=UserLogin", { UserLoginName: UserLoginName, UserPassword: UserPassword, VerifyCode: VerifyCode, param: Math.random() },
        function (data) {
            if (data != "" && data != null) {
                $("#UserLoginTips").html(data);
                if (data == "登录成功") 
                {
                    location.href = "/System/Index.aspx";
                }
                return true;
            }
        }
    );
}

