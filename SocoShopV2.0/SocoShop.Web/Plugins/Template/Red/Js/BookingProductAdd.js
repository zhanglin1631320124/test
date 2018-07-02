//提交检查
function checkSubmit(){
    var relationUser=o("RelationUser").value;
    if(relationUser==""){
        alertMessage("联系人不能为空",500);
        return false;
    }
    var email=o("Email").value;
    if(email==""){
        alertMessage("Email不能为空",500);
        return false;
    }
    var tel=o("Tel").value;
    if(tel==""){
        alertMessage("联系电话不能为空",500);
        return false;
    }
    return true;
}