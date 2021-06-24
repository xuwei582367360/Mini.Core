; window.pack = {};
(function ($, pack) {
    "use strict";
    $.extend(pack, {
        openDialog: function (option) {
            if (pack.isMobile()) {
                option.width = 'auto';
                option.height = 'auto';
            }
            else {
                if (!option.height) {
                    option.height = ($(window).height() - 50) + 'px';
                }
            }
            var _option = $.extend({
                type: 2,
                title: '',
                width: '768px',
                content: '',
                maxmin: true,
                shade: 0.4,
                btn: ['确认', '关闭'],
                callback: null,
                shadeClose: false,
                fix: false,
                closeBtn: 1
            }, option);
            layer.open({
                type: _option.type, // 2表示content的值为url，1表示content的值为html
                area: [_option.width, _option.height],
                maxmin: _option.maxmin,
                shade: _option.shade,
                title: _option.title,
                content: _option.content,
                btn: _option.btn,
                shadeClose: _option.shadeClose, // 弹层外区域关闭     
                fix: _option.fix,
                closeBtn: _option.closeBtn,  // 1表示带关闭，0表示不带
                yes: _option.callback,
                cancel: function (index) {
                    return true;
                }
            });
        },
        openDialogContent: function (option) {
            if (pack.isMobile()) {
                option.width = 'auto';
                option.height = 'auto';
            }
            else {
                if (!option.height) {
                    option.height = ($(window).height() - 50) + 'px';
                }
            }
            var _option = $.extend({
                type: 1,
                title: false,
                width: '768px',
                content: '',
                maxmin: false,
                shade: 0.4,
                btn: null,
                callback: null,
                shadeClose: true,
                fix: true,
                closeBtn: 0
            }, option);
            layer.open({
                type: _option.type, // 2表示content的值为url，1表示content的值为html
                area: [_option.width, _option.height],
                maxmin: _option.maxmin,
                shade: _option.shade,
                title: _option.title,
                content: _option.content,
                btn: _option.btn,
                shadeClose: _option.shadeClose, // 弹层外区域关闭
                fix: _option.fix,
                closeBtn: _option.closeBtn,  // 1表示带关闭，0表示不带
                yes: _option.callback,
                cancel: function (index) {
                    return true;
                }
            });
        },
        closeDialog: function () {
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
        },

        msgWarning: function (content) {
            layer.msg(content, { icon: 0, time: 1000, shift: 5 });
        },
        msgSuccess: function (content) {
            if (pack.isNullOrEmpty(content)) {
                content = "操作成功";
            }
            top.layer.msg(content, { icon: 1, time: 1000, shift: 5 });
        },
        msgError: function (content) {
            if (pack.isNullOrEmpty(content)) {
                content = "操作失败";
            }
            layer.msg(content, { icon: 2, time: 3000, shift: 5 });
        },

        alertWarning: function (content) {
            layer.alert(content, {
                icon: 0,
                title: "系统提示",
                btn: ['确认'],
                btnclass: ['btn btn-primary'],
            });
        },
        alertSuccess: function (content) {
            layer.alert(content, {
                icon: 1,
                title: "系统提示",
                btn: ['确认'],
                btnclass: ['btn btn-primary'],
            });
        },
        alertError: function (content) {
            layer.alert(content, {
                icon: 2,
                title: "系统提示",
                btn: ['确认'],
                btnclass: ['btn btn-primary'],
            });
        },
        confirm: function (content, callback) {
            layer.confirm(content, {
                icon: 3,
                title: "系统提示",
                btn: ['确认', '取消'],
                btnclass: ['btn btn-primary', 'btn btn-danger'],
            }, function (index) {
                layer.close(index);
                callback(true);
            });
        },

        showLoading: function (message) {
            $.blockUI({ message: '<div class="loaderbox"><div class="loading-activity"></div> ' + message + '</div>', css: { border: "none", backgroundColor: 'transparent' } });
        },
        closeLoading: function () {
            setTimeout(function () { $.unblockUI(); }, 50);
        },

        getIds: function (row) {
            var ids = '';
            $.each(row, function (i, obj) {
                if (i == 0) {
                    ids = obj.Id;
                }
                else {
                    ids += "," + obj.Id;
                }
            });
            return ids;
        },
        checkRowEdit: function (row) {
            if (row.length == 0) {
                pack.msgError("您没有选择任何行！");
            } else if (row.length > 1) {
                pack.msgError("您的选择大于1行！");
            } else if (row.length == 1) {
                return true;
            }
            return false;
        },
        checkRowDelete: function (row) {
            if (row.length == 0) {
                pack.msgError("您没有选择任何行！");
            } else if (row.length > 0) {
                return true;
            }
            return false;
        },

        ajax: function (option) {
            var opt = $.extend({
                url: option.url,
                async: true,
                type: "get",
                data: option.data || {},
                dataType: option.dataType || "json",
                beforeSend: function (xhr) {
                    debugger;
                    var token = pack.isNullOrEmpty(localStorage.getItem("UserInfo")) ? " " : JSON.parse(localStorage.getItem("UserInfo")).access;
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token);
                    pack.showLoading("正在处理中...");
                },
                complete: function (xhr, status, XMLHttpRequest) {
                    pack.statusCode(xhr.status, XMLHttpRequest);
                    pack.closeLoading();
                },
                success: function (rdata) { pack.msgSuccess(); },
                //error: function (xhr, status, obj) { toastr.error("系统出错了"); },
            }, option);
            if (pack.isNullOrEmpty(opt.url)) {
                pack.alertError("url 参数不能为空");
                return;
            }
            $.ajax({
                url: opt.url,
                async: opt.async,
                type: opt.type,
                data: opt.data,
                dataType: opt.dataType,
                //error: opt.error,
                success: opt.success,
                beforeSend: opt.beforeSend,
                complete: opt.complete,
            });
        },
        statusCode: function (status, XMLHttpRequest) {
            switch (status) {
                case 400:
                    var str = '';
                    var errorInfo = JSON.parse(XMLHttpRequest.responseText);
                    if (errorInfo.ModelState != undefined && errorInfo.ModelState.length != 0) {
                        $.each(errorInfo.ModelState, function (n, val) { str += val[0] + ';<br>'; });
                        toastr.error(str);
                    }
                    break;
                case 401:
                    toastr.error('登录超时，请重新登录！');
                    break;
                case 403:
                    toastr.error('没有访问权限！');
                    break;
                case 404:
                    var str = ''; var errorInfo = JSON.parse(XMLHttpRequest.responseText);
                    str += 'code:' + errorInfo.code + ';<br>' + 'message:' + errorInfo.message;
                    toastr.error(str);
                    break;
                case 500:
                    var str = '';
                    var errorInfo = JSON.parse(XMLHttpRequest.responseText);
                    str += 'code:' + errorInfo.code + ';<br>' + 'message:' + errorInfo.message;
                    toastr.error(str);
                    break;
            }
        },
        ajaxUploadFile: function (option) {
            var opt = $.extend({
                url: option.url,
                data: option.data || {},
                error: function (xhr, status, obj) { pack.alertError("系统出错了"); },
                success: function (rdata) {
                    pack.msgSuccess();
                },
                beforeSend: function (xhr) {
                    pack.showLoading("正在处理中...");
                },
                complete: function (xhr, status) {
                    pack.closeLoading();
                }
            }, option);

            if (pack.isNullOrEmpty(opt.url)) {
                pack.alertError("url 参数不能为空");
                return;
            }
            if (pack.isNullOrEmpty(opt.data)) {
                pack.alertError("data 参数不能为空");
                return;
            }
            $.ajax({
                url: opt.url,
                data: opt.data,
                type: "post",
                processData: false,
                contentType: false,
                error: opt.error,
                success: opt.success,
                beforeSend: opt.beforeSend,
                complete: opt.complete
            })
        },
        exportExcel: function (url, postData) {
            pack.ajax({
                url: url,
                type: "post",
                data: postData,
                success: function (obj) {
                    if (obj.Tag == 1) {
                        window.location.href = ctx + "File/DownloadFile?filePath=" + obj.Data + "&delete=1";
                    }
                    else {
                        pack.msgError(obj.Message);
                    }
                },
                beforeSend: function (xhr) {
                    pack.showLoading("正在导出数据，请稍后...");
                }
            });
        },
        request: function (name) {
            var params = decodeURI(window.location.search);
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = params.substr(1).match(reg);
            if (r != null) {
                return unescape(r[2]);
            }
            return null;
        },
        getHttpFileName: function (url) {
            if (url == null || url == '') {
                return url;
            }
            var i = url.lastIndexOf('/');
            if (i > 0) {
                return url.substring(i + 1);
            }
            return url;
        },
        getFileNameWithoutExtension: function (fileName) {
            if (fileName == null || fileName == '') {
                return fileName;
            }
            var i = fileName.indexOf('.');
            if (i > 0) {
                return fileName.substring(0, i);
            }
            return fileName;
        },
        changeURLParam: function (url, arg, arg_val) {
            var pattern = arg + '=([^&]*)';
            var replaceText = arg + '=' + arg_val;
            if (url.match(pattern)) {
                var tmp = '/(' + arg + '=)([^&]*)/gi';
                tmp = url.replace(eval(tmp), replaceText);
                return tmp;
            } else {
                if (url.match('[\?]')) {
                    var arr = url.split('#');
                    if (arr.length > 1) {
                        return arr[0] + '&' + replaceText + '#' + arr[1];
                    }
                    else {
                        return url + '&' + replaceText;
                    }
                } else {
                    return url + '?' + replaceText;
                }
            }
        },

        isNullOrEmpty: function (obj) {
            if ((typeof (obj) == "string" && obj == "") || obj == null || obj == undefined) {
                return true;
            }
            else {
                return false;
            }
        },
        // Html.Raw()方法会提示语法错误，所以用这个函数包装一下
        getJson: function (value) {
            return value;
        },
        getGuid: function () {
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20)) guid += "-";
            }
            return guid;
        },
        getValueByKey: function (json, key) {
            var value = "";
            $.each(json, function (i, obj) {
                if (obj.Key == key) {
                    value = obj.Value;
                }
            });
            return value;
        },
        getLastValue: function (str) {
            if (!pack.isNullOrEmpty(str)) {
                var arr = str.toString().split(',');
                return arr[arr.length - 1];
            }
            return '';
        },
        // 格式为 yyyy-MM-dd HH:mm:ss
        formatDate: function (v, format) {
            if (!v) return "";
            var d = v;
            if (typeof v === 'string') {
                if (v.indexOf("/Date(") > -1)
                    d = new Date(parseInt(v.replace("/Date(", "").replace(")/", ""), 10));
                else
                    d = new Date(Date.parse(v.replace(/-/g, "/").replace("T", " ").split(".")[0]));
            }
            var o = {
                "M+": d.getMonth() + 1,  //month
                "d+": d.getDate(),       //day
                "H+": d.getHours(),      //hour
                "m+": d.getMinutes(),    //minute
                "s+": d.getSeconds(),    //second
                "q+": Math.floor((d.getMonth() + 3) / 3),  //quarter
                "S": d.getMilliseconds() //millisecondjsonca4
            };
            if (/(y+)/.test(format)) {
                format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
            }
            for (var k in o) {
                if (new RegExp("(" + k + ")").test(format)) {
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
                }
            }
            return format;
        },
        trimStart: function (rawStr, c) {
            if (c == null || c == '') {
                var str = rawStr.replace(/^s*/, '');
                return str;
            }
            else {
                var rg = new RegExp('^' + c + '*');
                var str = rawStr.replace(rg, '');
                return str;
            }
        },
        trimEnd: function (rawStr, c) {
            if (c == null || c == "") {
                var rg = /s/;
                var i = rawStr.length;
                while (rg.test(rawStr.charAt(--i)));
                return rawStr.slice(0, i + 1);
            }
            else {
                var rg = new RegExp(c);
                var i = rawStr.length;
                while (rg.test(rawStr.charAt(--i)));
                return rawStr.slice(0, i + 1);
            }
        },
        toString: function (value) {
            if (value == null) {
                return '';
            }
            return value.toString();
        },
        openLink: function (href, target) {
            var a = document.createElement('a')
            if (target) {
                a.target = target;
            }
            else {
                a.target = '_blank';
            }
            a.href = href;
            a.click();
        },
        recursion: function (obj, id, destArr, key, parentKey) {
            if (!key) {
                key = "id";
            }
            if (!parentKey) {
                parentKey = "parentId";
            }
            for (var item in obj) {
                if (obj[item][key] == id) {
                    destArr.push(obj[item]);
                    return pack.recursion(obj, obj[item][parentKey], destArr, key, parentKey);
                }
            }
        },
        isMobile: function () {
            return navigator.userAgent.match(/(Android|iPhone|SymbianOS|Windows Phone|iPad|iPod)/i);
        }
    });
})(window.jQuery, window.pack);