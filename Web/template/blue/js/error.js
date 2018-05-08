/****************************************

1.	文件名称(File Name)： 	错误信息公用脚本文件
2.	功能描述(Description):  对错误数据处理
3.	作者(Author)：			WTang
4.	日期(Create Date)：		2013.8.6

****************************************/



// ================================ 错误编码的解析及错误信息的显示 ==== start ==================================================

/// <summary>
/// 根据错误编码显示不同的错误信息
/// </summary>
/// <return>
///  1.【已成功登陆】，登录成功。 
/// -1:【许可证错误】，没有许可文件 或 许可文件错误，请核对后重新启动服务...
/// -2:【服务未开启】，请稍后再连接服务器...
/// -3:【IP地址受限】，不允许登录，请联系管理人员...
/// -4:【超出连接数】，客户端连接数量超出最大允许数量，请联系管理人员...
/// -5:【已被迫下线】，你已经被管理员踢下线，请与管理人员联系...
/// </return>

function ShowErrorMessage() {

    var errorType = "";
    var html = "";
    var SystemErrorCode = GetQueryString("c"); // 错误编码，通过不同的编码显示不同的错误信息
    switch (SystemErrorCode) {

        case '-1':
            errorType = "许可证错误";
            html = "没有许可文件 或 许可已过期 或 许可文件被篡改，请核对后重新启动服务...";
            break;
        case '-2':
            errorType = "服务未开启";
            html = "请稍后再连接服务器...";
            break;
        case '-3':
            errorType = "IP地址受限";
            html = "不允许登录，请联系管理人员...";
            break;
        case '-4':
            errorType = "超出连接数";
            html = "客户端连接数量超出最大允许数量，请联系管理人员...";
            break;
        case '-5':
            errorType = "已下线";
            html = "你已下线，您可以尝试重新登录...";
            break;
        case '-6':
            errorType = "许可证错误";
            html = "服务尚未开启,请核对后重新启动服务...";
            break;
        case '-7':
            errorType = "登录超时";
            html = "登录超时，请重新登录...";
            break;
        default:
            errorType = "许可证错误";
            html = "没有许可文件 或 许可文件错误，请核对后重新启动服务...";
            break;
    }
    if (html != "") {
        jQuery("#ShowErrorType").html("抱歉，" + errorType);
        jQuery("#ShowErrorMessage").html(html);
    }
}


/// <summary>
/// 获取页面地址参数
/// </summary>
function GetQueryString(sProp) {
    var re = new RegExp(sProp + "=([^\\&;]*)", "i");
    var a = re.exec(document.location.search);
    if (a == null)
        return "";
    return a[1];
}

// ================================ 错误编码的解析及错误信息的显示 ==== end ==================================================
