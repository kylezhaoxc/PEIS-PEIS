var ShowTime = 1; //消息默认显示时间,单位分钟
var LogoutTime = 3; //下线小时默认响应时间,单位分钟
var CurShowTime = ShowTime;
var CurLogoutTime = LogoutTime;
function Send() {
    //向comet_broadcast.asyn发送请求，消息体为文本框content中的内容，请求接收类为AsnyHandler
    $.post("comet_broadcast.asyn", { content: $("#content").val() });
    //清空内容
    $("#content").val("");
}

function SendMessage(Command, ShowTime, LogoutTime, Sender, UserName, Reciver, content) {
    if (Command == undefined) {
        Command = "INFO";
    }
    //向comet_broadcast.asyn发送请求，消息体为文本框content中的内容，请求接收类为AsnyHandler
    $.post("comet_broadcast.asyn", { Command: Command, ShowTime: ShowTime, LogoutTime: LogoutTime, Sender: Sender, UserName: UserName, Reciver: Reciver, content: content });
}

//var UserID = jQuery("#CookieUserID").val();
//var UserName = jQuery("#CookieUserName").val();
//SessionID不为空时候表示，清除此Session值所对应的用户
function Wait(Sender, UserName, Content, Command) { 
    if (Command == undefined) {
        Command = "INFO";
    }
    $.post("comet_broadcast.asyn", { Command: Command, Sender: Sender, Reciver: "SERVER", UserName: UserName, content: Content },
         function (data, status) {
             //xmhuang 20140610 通过序列华后获取数据，解决回车无法显示数据的问题
             if (data.dataList != undefined) {
                 data = data.dataList[0];
             }
             //获取当前光标
             //var activeElementID = document.activeElement.id;
             if (data != null) {
                 if (data.IsSendTome != 0) {
                     var Message = data.Content;
                     var title = "系统消息";
                     if (data.ShowTime != undefined) {
                         CurShowTime = data.ShowTime;
                     }
                     if (data.UserName != undefined) {
                         title = "来自[" + data.UserName + "]的消息";
                     }
                     if (data.Command == "LOGOUT") //如果接收的是下线命令
                     {
                         if (data.LogoutTime != undefined) {
                             CurLogoutTime = data.LogoutTime;
                         }
                         Message = "系统将在" + CurLogoutTime + "分钟后暂停为您服务，请尽快保存当前操作！"
                         // StartTimer();
                         //alert(CurLogoutTime * 60 * 1000);
                         setInterval("CloseMe()", CurLogoutTime * 60 * 1000);
                     }
                     else if (data.Command == "EXIT") //如果是退出命令
                     {
                         window.opener = null;
                         window.open(" ", "_self"); //IE7必须的
                         window.close();
                     }
                     if (data.Command == "REFRESHCLIENT")//刷新消息，即广播消息
                     {
                         Message = "";
                     }
                     if (Message != "" && Message.indexOf("欢迎回来")<0) {
                         var tipscontent = '<div style="word-break:break;width:100%;height:100px;overflow:hidden;" title="' + Message + '">' + Message + '</div>';
                         if (art.dialog.get('msg') != null) {
                             art.dialog.get('msg').content(tipscontent).title(title).time(CurShowTime * 60);
                         }
                         else {
                             art.dialog({
                                 id: 'msg',
                                 title: title,
                                 content: tipscontent,
                                 width: 200,
                                 height: 100,
                                 left: '100%',
                                 top: '100%',
                                 fixed: true,
                                 drag: false,
                                 resize: false,
                                 time: CurShowTime * 60,
                                 close: function () {

                                 }
                                 , focus: false
                             })
                         }
                     }
                 }
                 if (data.Command != "CLOSEME") //如果接收的是下线命令
                 {
                     Wait(Sender, UserName, "-1", "INFO"); //服务器返回消息,再次立连接
                 }
                 if (data.Command == "REFRESHCLIENT") //如果刷新客户端列表
                 {
                     if (jQuery("#btnRefreshClient").length > 0)//判断刷新按钮是否存在
                     {
                         jQuery("#btnRefreshClient").click();
                     }
                 }
             }
             else {
                 //Wait(Sender, UserName, "-1", "INFO"); //服务器返回消息,再次立连接
             }
             //jQuery("#" + activeElementID).focus();
         }, "json"
         );
}
function StartTimer() {
    //setInterval("CloseMe()", 1000);
}
function CloseMe() {
    var SessionID = jQuery("#CookieSessionID").val();
    var LoginName = jQuery("#CookieLoginName").val();
    var UserName = jQuery("#CookieUserName").val();
    SendMessage("CLOSEME", ShowTime, LogoutTime, SessionID, UserName, "SERVER", UserName + "已下线");
    if (jQuery("#btnRefreshClient").length > 0)//判断刷新按钮是否存在
    {
        jQuery("#btnRefreshClient").click();
    }
    window.location.href = "/error.htm?c=-5";
}