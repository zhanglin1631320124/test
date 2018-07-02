function checkUserPass(minLength, maxLength) {
    var userPassObj = o("UserPassword1");
    var warningObj = o("PasswordWarningMessage1");
    var checkUserPasswordObj1 = o("CheckUserPassword1");
    if (userPassObj.value != "") {
        var length = getStringLength(userPassObj.value);
        if (length < minLength || length > maxLength) {
            warningObj.innerHTML = " * 密码长度为" + minLength + "- " + maxLength + "个字符！";
            checkUserPasswordObj1.value="0";
        }
        else {
            warningObj.innerHTML = "";
            checkUserPasswordObj1.value="1";
        }
    }
    else {
        warningObj.innerHTML = " * 密码不能为空！";
        checkUserPasswordObj1.value="0";
    }
}
function checkUserPass2(minLength, maxLength) {
    var userPassObj = o("UserPassword2");
    var warningObj = o("PasswordWarningMessage2");
    var checkUserPasswordObj2 = o("CheckUserPassword2");
    if (userPassObj.value != "") {
        var length = getStringLength(userPassObj.value);
        if (length < minLength || length > maxLength) {
            warningObj.innerHTML = " * 密码长度为" + minLength + "- " + maxLength + "个字符！";
            checkUserPasswordObj2.value="0";
        }
        else {
            if(userPassObj.value!=o("UserPassword1").value){
                 warningObj.innerHTML = " * 两次密码不一样，请重新输入！";
                 checkUserPasswordObj2.value="0";
            }
            else{
                warningObj.innerHTML = "";
                checkUserPasswordObj2.value="1";
            }
        }
    }
    else {
        warningObj.innerHTML = " * 密码不能为空！";
        checkUserPasswordObj2.value="0";
    }
}
function checkResetPassword() {
    if(o("CheckUserPassword1").value=="0"){
        alertMessage("密码有错误");
        return false;
    }
    if(o("CheckUserPassword2").value=="0"){
        alertMessage("确认密码有错误");
        return false;
    }
    return true
}