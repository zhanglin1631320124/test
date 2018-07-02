var objID = "Add";
var page = 1;
readUserRecharge(objID, page);
function readUserRecharge(id, page) {
    loading("UserRechargeAjax", "在线充值");
    o(objID).className = "";
    objID = id;
    var obj = o(objID);
    obj.className = "on";
    var url = "UserRechargeAjax.aspx?Action=" + id + "&Page=" + page;
    Ajax.requestURL(url, dealReadUserRecharge);
}
function dealReadUserRecharge(content) {
    o("UserRechargeAjax").innerHTML = content;
}
function goPage(page) {
    readUserRecharge(objID, page);
} 
//添加在线充值
function addUserRecharge() {
    var money = o("Money").value;
    var payKey = getRadioValue(os("name","PayKey"));
    if (money != "" && payKey!= "") {
        var url = "UserRechargeAjax.aspx?Action=AddUserRecharge&Money=" + money + "&PayKey=" + payKey;
        Ajax.requestURL(url, dealAddUserRecharge);
    }
    else {
        alertMessage("请填写金额和选择支付方式");
    }
}
function dealAddUserRecharge(content){
    if (!Validate.isInt(content)) {
        alertMessage(content);
    }
    else {
        var payKey = getRadioValue(os("name","PayKey"));    
        var url = "/Plugins/Pay/" + payKey + "/Pay.aspx?Action=Apply&ApplyID=" + content;
        window.open(url);
    }
}