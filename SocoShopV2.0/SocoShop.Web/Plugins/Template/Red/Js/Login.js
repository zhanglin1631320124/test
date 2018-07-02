function checkLogin() {
    if(o("UserName").value==""){
        alertMessage("用户名不能为空");
        return false;
    }
    if(o("UserPassword").value==""){
        alertMessage("密码不能为空");
        return false;
    }
    if(o("SafeCode").value==""){
        alertMessage("验证码不能为空");
        return false;
    }
    return true
}