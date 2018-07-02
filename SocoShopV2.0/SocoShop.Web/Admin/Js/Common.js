//取得对象
function o(id){
    return document.getElementById(id);
}
//取得对象组
function os(type,name){      
    var objs=null;   
    switch(type){
        case "name":
            objs=document.getElementsByName(name);
            break;
        case "tagName":
            objs=document.getElementsByTagName(tagName);
            break;
        default:
            break;
    }
    return objs;
}
//取得普通文本框值
function getTextValue(objs){
    var result = new Array();
    if (objs != null && objs.length > 0) {
        for (var i = 0; i < objs.length; i++) {
            result[i]=objs[i].value;
        }
    }
    return result;
}
//取得单选值
function getRadioValue(objs){
    var result = "";
    if (objs != null && objs.length > 0) {
        for (var i = 0; i < objs.length; i++) {
            if (objs[i].checked == true) {
                result=objs[i].value;
                break;
            }
        }
    }
    return result;
}
//设置单选值
function setRadioValue(objs,value){
  if (objs != null && objs.length > 0) {
        for (var i = 0; i < objs.length; i++) {
            if (value==objs[i].value) {
                objs[i].checked = true;
            }
        }
    }
}
//取得复选值
function getCheckboxValue(objs){
    var result = new Array();
    if (objs != null && objs.length > 0) {
        var j=0;
        for (var i = 0; i < objs.length; i++) {
            if (objs[i].checked == true) {
                result[j]=objs[i].value;
                j++;
            }
        }
    }
    return result;
}
//设置复选值
function setCheckboxValue(objs,value){
  if (objs != null && objs.length > 0) {
        for (var i = 0; i < objs.length; i++) {
            if (value.indexOf("|" + objs[i].value + "|") > -1) {
                objs[i].checked = true;
            }
        }
    }
}

function bindEvent(event,fn){
    if (typeof window.addEventListener != 'undefined'){
        window.addEventListener(event, fn, false);
    }
    else if (typeof document.addEventListener != 'undefined'){
        document.addEventListener(event, fn, false);
    }
    else{
        window.attachEvent('on'+event, fn);
    }
}

//图片缩放
function photoLoad(obj, wid, heig) {
    var virtualImageObj = document.createElement("img");
    virtualImageObj.src = obj.src;
    if (wid < virtualImageObj.width || heig < virtualImageObj.height) {
        if (virtualImageObj.width / virtualImageObj.height > wid / heig) {
            w = wid;
            h = (virtualImageObj.height / virtualImageObj.width) * wid;
            obj.style.marginTop = (heig - h) / 2 + "px";
            obj.style.marginLeft = "0px";
        }
        else {
            h = heig;
            w = (virtualImageObj.width / virtualImageObj.height) * heig;
            obj.style.marginLeft = (wid - w) / 2 + "px";
            obj.style.marginTop = "0px";
        }
        obj.height = h;
        obj.width = w;
    }
    else {
        obj.style.marginTop = (heig - virtualImageObj.height) / 2 + "px";
        obj.style.marginLeft = (wid - virtualImageObj.width) / 2 + "px";
        obj.height = virtualImageObj.height;
        obj.width = virtualImageObj.width;
    }
}
//取得url参数值
function getQueryString(parmName) {
    var result = '';
    var url = document.location.search;
    if (url != "undefined") {
        if (url.substr(0, 1) == "?") {
            url = url.substr(1);
        }
        var arrParam = url.split("&");
        for (var i = 0; i < arrParam.length; i++) {
            if (arrParam[i].split("=")[0].toLowerCase() == parmName.toLowerCase()) {
                result = arrParam[i].replace(parmName + "=", "");
                break;
            }
        }
    }
    return result;
}
//取得字符长度
function getStringLength(data) {
    var i, sum;
    sum = 0;
    for (i = 0; i < data.length; i++) {
        if ((data.charCodeAt(i) >= 0) && (data.charCodeAt(i) <= 255))
            sum = sum + 1;
        else
            sum = sum + 2;
    }
    return sum;
}
//截取字符串
function substring(data,length) {
   if(getStringLength(data)>length)
   {
        data=data.substr(0,length)+"..";
   }
    return data;
}
//删除自身节点
function removeSelf(obj) {
    obj.parentNode.removeChild(obj);
}
//设为首页
function home(name,url){
    name.style.behavior='url(#default#homepage)';
    name.setHomePage(url);
}
//加载数据
function loading(id, title) {
    o(id).innerHTML = "<div style='margin:10px auto;text-align:center;'><div style='border:#d78e42 1px solid; padding:8px; width:260px;display:inline-block; background:#ffffcc;color:#FF0000;font-weight:bold'><img src='/admin/style/images/loading.gif' align='absmiddle'/> 正在加载" + title + ",请稍候．．．</div></div>";
}
///插入flash
function displayFlash(url, width, height, parameter) {
    document.write('<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0" width="' + width + '" height="' + height + '"> ');
    document.write('<param name="movie" value="' + url + '">');
    document.write('<param name="quality" value="high"> ');
    document.write('<param name="wmode" value="transparent"> ');
    document.write('<param name="FlashVars" value="' + parameter + '">');
    document.write('<param name="menu" value="false"> ');
    document.write('<embed src="' + url + '" quality="high" FlashVars="' + parameter + '" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="' + width + '" height="' + height + '" wmode="transparent"></embed> ');
    document.write('</object> ');
}
//时间比较：DateOne>DateTwo返回True 否则返回False
function compareDate(DateOne, DateTwo) {
    var OneMonth = DateOne.substring(5, DateOne.lastIndexOf("-"))
    var OneDay = DateOne.substring(DateOne.length, DateOne.lastIndexOf("-") + 1)
    var OneYear = DateOne.substring(0, DateOne.indexOf("-"))
    var TwoMonth = DateTwo.substring(5, DateTwo.lastIndexOf("-"))
    var TwoDay = DateTwo.substring(DateTwo.length, DateTwo.lastIndexOf("-") + 1)
    var TwoYear = DateTwo.substring(0, DateTwo.indexOf("-"))
    if (Date.parse(OneMonth + " / " + OneDay + " / " + OneYear) > Date.parse(TwoMonth + "/" + TwoDay + "/" + TwoYear)) {
        return true;
    }
    else {
        return false;
    }
}
 //复制内容到减切板
 function copyText(data,message) {
     window.clipboardData.setData("Text", data);
     alertMessage(message);
 }
//提示信息
function alertMessage(message, checked) {
    var alertDiv = document.createElement("div");
    var divWidth = 200;
    var divHeight = 50;
    var time = 2000;
    var screenWidth = (window.innerWidth || document.documentElement && document.documentElement.clientWidth || document.body.clientWidth);
    var screenHeight = (window.innerHeight || document.documentElement && document.documentElement.clientHeight || document.body.clientHeight); 
    var scrollLeft = (document.documentElement.scrollLeft || document.body.scrollLeft);
    var scrollTop = (document.documentElement.scrollTop || document.body.scrollTop);
    var top = (screenHeight - divHeight) / 2 + scrollTop;
    var left = (screenWidth - divWidth) / 2 + scrollLeft;
    with (alertDiv) {
        style.border = "#d78e42 1px solid";
        style.background = "#ffffcc";
        style.position = "absolute";
        style.padding = "12px";
        style.left = left + "px";
        style.top = top + "px";
        style.width = divWidth+"px";
        style.textAlign = "center";
        style.color="#FF0000";
        style.fontWeight="bold";
        id = "AlertMessage";
        innerHTML = message;
    }
    document.body.appendChild(alertDiv);
    if (checked != "1") {    
        setTimeout("closeAlertDiv()", time);
    }
}
function closeAlertDiv() {
    var alerObj = o("AlertMessage");
    if (alerObj != null) {
        document.body.removeChild(alerObj);
    }
 }
 //添加cookie
function addCookie(objName, objValue, objMinutes) {
    var str = objName + "=" + escape(objValue);
    if (objMinutes > 0) {   //为0时不设定过期时间，浏览器关闭时cookie自动消失
        var date = new Date();
        var ms = objMinutes * 60 * 1000;
        date.setTime(date.getTime() + ms);
        str += "; expires=" + date.toGMTString();
    }
    str +=";path=/";
    document.cookie = str;
}
//获取指定名称的cookie的值 
function getCookie(objName) {
    var arrStr = document.cookie.split("; ");
    for (var i = 0; i < arrStr.length; i++) {
        var temp = arrStr[i].split("=");
        if (temp[0] == objName) return unescape(temp[1]);
    }
    return "";
}
//删除指定名称的cookie
function delCookie(name) {
    var date = new Date();
    date.setTime(date.getTime() - 10000);
    document.cookie = name + "=a; expires=" + date.toGMTString();
}
//浏览器类
var Browser= {
    isMozilla:  (typeof document.implementation != 'undefined') && (typeof document.implementation.createDocument != 'undefined') && (typeof HTMLDocument != 'undefined'),
    isIE:  window.ActiveXObject ? true : false,
    isFirefox:  (navigator.userAgent.toLowerCase().indexOf("firefox") != -1),
    isSafari:  (navigator.userAgent.toLowerCase().indexOf("safari") != -1),
    isOpera:  (navigator.userAgent.toLowerCase().indexOf("opera") != -1)
}
//删除记录
function deleteRecord(obj){
    if(confirm("确定要删除")){
        Ajax.requestURL(obj.href,function(data){if(data=="ok"){reloadPage();}else{alertMessage("删除失败");}});
    }
    return false;
}
//刷新页面
function reloadPage(){
    //window.location.href=document.location;   
    window.location.reload(); 
}
//添加事件
function addEvent(obj, evt, fn ) {
    if(typeof obj.attachEvent != 'undefined' ){
        obj.attachEvent( "on" + evt, fn);
    } 
    else if(typeof obj.addEventListener != 'undefined' ){
        obj.addEventListener( evt, fn, false );
    } 
    else {
    }
}
//验证类
var Validate = { 
    isEmpty: function(val) {
        switch (typeof (val)) {
            case 'string':
                return Utils.trim(val).length == 0 ? true : false;
                break;
            case 'number':
                return val == 0;
                break;
            case 'object':
                return val == null;
                break;
            case 'array':
                return val.length == 0;
                break;
            default:
                return true;
        }
    },
    isNumber: function(val) {
        var reg = /^[|+\-]*[\d|\.|,]+$/;
        return reg.test(val);
    },

    isInt: function(val) {
        if (val == "") {
            return false;
        }
        var reg = /\D+/;
        return !reg.test(val);
    },

    isEmail: function(email) {
        var reg1 = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
        return reg1.test(email);
    },

    isTel: function(tel) {
        var reg = /^[\d|\-|\s|\_]+$/; //只允许使用数字-空格等
        return reg.test(tel);
    },
    isTime: function(val) {
        var reg = /^\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}$/;
        return reg.test(val);
    },
    isDate: function(val) {
        var reg = /^\d{4}-\d{1,2}-\d{1,2}$/;
        return reg.test(val);
    }, 
    reg: function(v, r) {
        var re = r;
        if (!re.test(v)) {
            return false;
        }
        return true;
    },
    fixEvent: function(e) {
        var evt = (typeof e == "undefined") ? window.event : e;
        return evt;
    },
    srcElement: function(e) {
        if (typeof e == "undefined") e = window.event;
        var src = document.all ? e.srcElement : e.target;

        return src;
    }
}
//Ajax类
var Ajax={
    //取得组件
    getHttpRequest: function()
    {
        if (window.XMLHttpRequest)
        {
            return new XMLHttpRequest() ;
        }
        else if (window.ActiveXObject)
        {
            return new ActiveXObject("Microsoft.XMLHTTP") ;
        }
    },  
    //提交数据
    postURL : function(URL, PostData , FunctionName)
    {
        var TempAjax = this;
        var IsFunction = (typeof(FunctionName) == 'function');
        var TempXmlHttp = this.getHttpRequest();
        TempXmlHttp.open("POST", URL, IsFunction) ;
        if (IsFunction)
        {
            TempXmlHttp.onreadystatechange = function() 
            {
                if ( TempXmlHttp.readyState == 4)
                {
                    var Result = TempXmlHttp.responseText;
                    TempAjax.DOMDocument = TempXmlHttp.responseXML ;
                    FunctionName(Result) ;
                }
            }
        }
        if (this.debug == 1)
        {
            alert("Error");
        }
        TempXmlHttp.setRequestHeader("CONTENT-TYPE","application/x-www-form-urlencoded");
        TempXmlHttp.send(PostData) ;
        if (!IsFunction)
        {
            this.DOMDocument = TempXmlHttp.responseXML ;
        }
    },
    //请求数据
    requestURL : function(URL,FunctionName)
    {
        var TempAjax = this;
        var IsFunction = (typeof(FunctionName) == 'function');
        var TempXmlHttp = this.getHttpRequest();
        TempXmlHttp.open("GET",URL,IsFunction);
        if (IsFunction)
        {
            TempXmlHttp.onreadystatechange = function() 
            {
                if ( TempXmlHttp.readyState == 4 )
                {
                    var Result = TempXmlHttp.responseText;
                    TempAjax.DOMDocument = TempXmlHttp.responseXML;
                    FunctionName(Result);
                }
            }
        }
        TempXmlHttp.send(null);
        if (!IsFunction)
        {
            this.DOMDocument = TempXmlHttp.responseXML;
        }
    },
    //读取XML节
    selectNodes: function(Xpath)
    {
        if (document.all)
        {
            return this.DOMDocument.selectNodes(Xpath) ;
        }
        else
        {
            var NodeArray = new Array();
            var PathResult = this.DOMDocument.evaluate(Xpath,this.DOMDocument,this.DOMDocument.createNSResolver(this.DOMDocument.documentElement),XPathResult.ORDERED_NODE_ITERATOR_TYPE, null);
            if (PathResult) 
            {
                var TempNode = PathResult.iterateNext() ;
                while(TempNode)
                {
                    NodeArray[NodeArray.length] = TempNode ;
                    TempNode = PathResult.iterateNext();
                }
            } 
            return NodeArray;
        }
    },
    //读取单个XML字内容
    selectSingleNode: function(Xpath) 
    {
        if (document.all)
        {
            return this.DOMDocument.selectSingleNode(Xpath);
        }
        else
        {
            var PathResult = this.DOMDocument.evaluate(Xpath,this.DOMDocument,this.DOMDocument.createNSResolver(this.DOMDocument.documentElement),9,null);
            if (PathResult && PathResult.singleNodeValue )
            {
                return PathResult.singleNodeValue;
            }
            else
            {
                return null ;
            }
        }
    }
}