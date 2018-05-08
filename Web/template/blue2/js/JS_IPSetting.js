(function (S) {
    var getJson = function (data, success, error) {
        data = S.extend({}, data, { t: Math.random() });
        S.ajax({
            url: "/Ajax/AjaxClientList.aspx",
            type: "post",
            dataType: "json",
            data: data,
            success: function (json) {
                success && "function" === typeof success && success(json);
            },
            error: function (json) {
                error && "function" === typeof error && error(json);
            }
        });
    };
    var servers = [],
            tmp = '<div class="IP-Setup-right-nr-bl1">' +
                '<strong>{{name}}</strong>' +
                '<p><span>{{begin_ip}}</span> - <span>{{end_ip}}</span></p>' +
                '</div>',
            localTmp = '<span class="IP-Setup-left-bt-font1">{{name}}</span>' +
                '<p><span class="IP-Setup-left-bt-font2">{{ip}}</span></p>',
            codeTmp = '<div class="IP-Setup-right-nr-bh{{tag}}"><span>{{code}}</span></div>';
    String.prototype.format = function () {
        if (arguments.length <= 0) return this;
        var result = this,
                type = function (obj) {
                    return Object.prototype.toString.call(obj).slice(8, -1).toLowerCase();
                };
        var reg;
        if (1 === arguments.length && "object" === type(arguments[0])) {
            for (var key in arguments[0]) {
                reg = new RegExp("\\{\\{" + key + "\\}\\}", "gi");
                result = result.replace(reg, arguments[0][key]);
            }
        } else {
            for (var i = 0; i < arguments.length; i++) {
                reg = new RegExp("\\{" + i + "\\}", "gi");
                result = result.replace(reg, arguments[i]);
            }
        }
        //未绑定的默认以空字符填充
        reg = new RegExp("(\\{[0-9]+\\})|(\\{\\{[0-9a-z]+\\}\\})", "gi");
        result = result.replace(reg, "");
        return result;
    };
    var hForm = {
        form: S(".edit-form"),
        fill: function (data) {
            hForm.form.find("input[name]").each(function () {
                var st = S(this);
                var value = (data ? data[st.attr("name")] || "" : "");
                st.val(value);
            });
        },
        json: function () {
            var array = hForm.form.serializeArray(),
                    data = {};
            for (var i = 0; i < array.length; i++) {
                var item = array[i];
                if (!data.hasOwnProperty(item.name))
                    data[item.name] = S.trim(item.value);
                else
                    data[item.name] += "," + S.trim(item.value);
            }
            return data;
        },
        valid: function () {
            var data = hForm.json();
            for (var prop in data) {
                if (prop !== "guid" && data[prop] == "") {
                    ShowCallBackSystemDialog("信息不完整！", function () {
                        hForm.focusInput(prop);
                    });
                    return false;
                }
            }
            return true;
        },
        focusInput: function (name) {
            hForm.form.find('input[name="' + name + '"]').focus();
        },
        setBtn: function (btns, state) {
            btns = S(btns);
            if (state) {
                btns.removeClass("disabled").removeAttr("disabled");
            } else {
                btns.addClass("disabled").attr("disabled", "disabled");
            }
        }
    };
    var inputData;
    var loadData = function () {
        getJson({
            action: "List"
        }, function (json) {
            var sbox = S(".IP-Setup-right-nr-bl"),
                    scode = S(".IP-Setup-right-nr-bh"),
                    sul = S(".Setupdiv-center-b-left ul");
            servers = json.servers;
            sbox.html("");
            scode.html("");
            sul.html("");
            for (var i = 0; i < servers.length; i++) {
                sbox.append(tmp.format(servers[i]));
                scode.append(codeTmp.format({ tag: (i == 0 ? 1 : 2), code: servers[i].code }));
                var sli = S('<li><a href="#">·{0}</a></li>'.format(servers[i].name));
                sli.data("server", servers[i]);
                sul.append(sli);
            }
            sul.find("li a").bind("click", function () {
                var st = S(this).parent();
                if (st.hasClass("active")) return false;
                var formData = hForm.json();
                if (S.trim(formData.guid) == "")
                    inputData = formData;
                st.addClass("active").siblings("li").removeClass("active");
                var server = st.data("server");
                hForm.fill(server);
                hForm.setBtn(S(".buttom-tj-s,.buttom-sc-s"), true);
                return false;
            });
            if (servers.length == 0) {
                hForm.fill({});
            } else {
                sul.find("li>a:eq(0)").click();
            }
            inputData = {};
            S(".IP-Setup-right-fen").height((servers.length - 1) * 107 + 2);
            S(".IP-Setup-left").height(servers.length * 106);
            var top = (servers.length * 106 - 120) / 2;
            S(".IP-Setup-left-bt").css("top", top > 0 ? top : 0);
            S(".IP-Setup-left-bt").html(localTmp.format(json.local));
        });
    };
    loadData();
    var dialog = S(".Setupdiv");

    var setDialog = function (state) {
        if (state) {
            S(".dialog-mask").show();
            dialog.fadeIn();
        } else {
            dialog.fadeOut(200, function () {
                S(".dialog-mask").hide();
            });
        }
    };
    S(".buttom-pz a").bind("click", function () {
        setDialog(true);
        return false;
    });
    S(".Setupdiv-title-off").click(function () {
        setDialog(false);
        return false;
    });

    //新增按钮
    S(".buttom-tj-s a").bind("click", function () {
        var target = S(this).parent();
        if (target.hasClass("disabled")) return false;
        hForm.fill(inputData);
        hForm.focusInput("code");
        S(".Setupdiv-center-b-left li").removeClass("active");
        hForm.setBtn(target, false);
        hForm.setBtn(S(".buttom-sc-s"), false);
        return false;
    });
    //保存按钮
    S(".buttom-bc-s a").bind("click", function () {
        var target = S(this).parent();
        if (target.hasClass("disabled")) return false;
        S(this).focus();
        hForm.setBtn(target, false);
        var data = hForm.json();
        if (!hForm.valid()) {
            hForm.setBtn(target, true);
            return false;
        }
        if (/[<>\'\"]/.test(data.name)) {
            ShowCallBackSystemDialog("部门名称中不能包含(<>\"')等特殊字符！", function () {
                hForm.focusInput("name");
                hForm.setBtn(target, true);
            });
            return false;
        }

        if (!/^[2-9]$/.test(data.code)) {
            ShowCallBackSystemDialog("部门编号只能是2-9之间的数字！", function () {
                hForm.focusInput("code");
                hForm.setBtn(target, true);
            });
            return false;
        }
        var ipReg = /^(((25[0-5])|(2[0-4]\d)|(1\d{2})|([1-9]?\d))\.){3}((25[0-5])|(2[0-4]\d)|(1\d{2})|([1-9]?\d))$/;
        if (!ipReg.test(data.begin_ip)) {
            ShowCallBackSystemDialog("起始IP地址格式不正确！", function () {
                hForm.setBtn(target, true);
                hForm.focusInput("begin_ip");
            });
            return false;
        }
        if (!ipReg.test(data.end_ip)) {
            ShowCallBackSystemDialog("终止IP地址格式不正确！", function () {
                hForm.setBtn(target, true);
                hForm.focusInput("end_ip");
            });
            return false;
        }
        data.action = "Update";
        getJson(data, function (json) {
            if (json.state) {
                ShowCallBackSystemDialog("保存成功！", function () { });
                loadData();
                inputData = {};
                //setDialog(false);
            } else {
                ShowSystemWarningDialog(json.msg);
            }
            hForm.setBtn(target, true);
        });
        return false;
    });
    S(".buttom-sc-s a").bind("click", function () {
        var target = S(this).parent();
        if (target.hasClass("disabled")) return false;
        var msgContent = "确认删除该服务器配置？";
        var dialog = art.dialog({
            id: 'artDialogIDRegisterDate',
            lock: true,
            fixed: true,
            opacity: 0.3,
            title: '温馨提示',
            content: msgContent,
            button: [{
                name: '取消',
                callback: function () {
                    return true;
                }
            }, {
                name: '确定',
                callback: function () {
                    hForm.setBtn(target, false);
                    var guid = S(".edit-form").find('input[name="guid"]').val();
                    guid = S.trim(guid);
                    if (guid == "") {
                        ShowCallBackSystemDialog("请选择服务器节点！", function () { });
                        return false;
                    }
                    getJson({
                        action: "Delete",
                        guid: guid
                    }, function (json) {
                        if (json.state) {
                            ShowCallBackSystemDialog("删除成功！", function () { });
                            loadData();
                            //setDialog(false);
                        } else {
                            ShowSystemWarningDialog(json.msg);
                        }
                        hForm.setBtn(target, true);
                    });
                    return true;

                }, focus: true
            }]

        }).lock();
        return false;
    });
    hForm.form.find("input[name]").bind("keyup", function (e) {
        if (13 == e.keyCode) {
            S(".buttom-bc-s a").click();
        }
    });
    $("#center").autoHeight({ left: 188, min: 300 });
})(jQuery);
jQuery(document).ready(function () {
    /**
    * 设置dialog
    */
    var jQuerydialog = jQuery(".Setupdiv");
    var left = (jQuery(window).width() - jQuerydialog.width()) / 2;
    jQuerydialog.css("left", left);

    jQuerydialog.drag({ handler: jQuery(".Setupdiv-title-total") });
});