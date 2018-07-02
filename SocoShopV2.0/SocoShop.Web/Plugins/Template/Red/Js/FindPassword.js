function checkUserName() {
    var userNameObj = o("UserName");
    var messageObj = o("UserNameWarningMessage");
    var checkUserNameObj = o("CheckUserName");
    if (userNameObj.value != "") {
        messageObj.innerHTML = "";
        var url = "/Ajax.aspx?UserName=" + encodeURIComponent(userNameObj.value) + "&Action=CheckUserName";
        Ajax.requestURL(url, dealCheckUserName);
    }
    else {
        messageObj.innerHTML = " * 用户名不能为空！";
        checkUserNameObj.value="0";
    } 
}
function dealCheckUserName(data) {
    //1-不存在该用户名，2-用户名填写正确,3-有非法字符
    var messageObj = o("UserNameWarningMessage");
    var checkUserNameObj = o("CheckUserName");
    if(data=="1"){
        messageObj.innerHTML = " * 不存在该用户名！";
        checkUserNameObj.value="0";
    }
    else{
        checkUserNameObj.value="1";
    }
}
function checkEmail() {
    var emailObj = o("Email");
    var warningObj = o("EmailWarningMessage");
    var checkEmailObj = o("CheckEmail");
    if (emailObj.value != "") {
        if (!Validate.isEmail(emailObj.value)) {
            warningObj.innerHTML = " * 请输写正确的Email地址！";
            checkEmailObj.value="0";
            return;
        }
        warningObj.innerHTML = "";
        var url = "/Ajax.aspx?Email=" + emailObj.value + "&Action=CheckEmail";
        Ajax.requestURL(url, dealCheckEmail);
    }
    else {
        warningObj.innerHTML = " * Email不能为空！";
        checkEmailObj.value="0";
    }
}
function dealCheckEmail(data) {
    var warningObj = o("EmailWarningMessage");
    var checkEmailObj = o("CheckEmail");
    if(data=="1"){
        warningObj.innerHTML = " * 不存在该Email！";
        checkEmailObj.value="0";
    }
    else{
        checkEmailObj.value="1";
    }  
}
function checkFindPassword() {
    if(o("CheckUserName").value=="0"){
        alertMessage("用户名有错误");
        return false;
    }
    if(o("CheckEmail").value=="0"){
        alertMessage("Email有错误");
        return false;
    }
    if(o("SafeCode").value==""){
        alertMessage("验证码不能为空");
        return false;
    }
    return true
}