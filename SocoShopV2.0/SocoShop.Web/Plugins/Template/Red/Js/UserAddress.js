//提交检查
function checkSubmit() {
    var consignee = o("Consignee").value;
    if(consignee==""){
        alertMessage("收货人不能为空",500);
        return false;
    }
    var zipCode = o("ZipCode").value;
    if(zipCode==""){
        alertMessage("邮编不能为空",500);
        return false;            
    }
    var tel = o("Tel").value;
    var mobile = o("Mobile").value;
    if(tel=="" &&　mobile==""){
        alertMessage("固定电话，移动电话必须填写一个",500);
        return false;
    }
    var address = o("Address").value;
    if(address==""){
        alertMessage("详细地址不能为空",500);
        return false;
    }
    return true;
}