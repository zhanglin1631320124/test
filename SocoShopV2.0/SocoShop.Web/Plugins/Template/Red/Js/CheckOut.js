//读取用户地址
function readUserAddress(){
    loading("CheckOutAddressAjax","用户地址");
    var id=0;
    if(o("UserAddress")!=null){
        id=o("UserAddress").value;
    }
    var url="CheckOutAddressAjax.aspx?ID="+id;
    Ajax.requestURL(url,dealReadUserAddress);
}
function dealReadUserAddress(data){
    var reg = /<script(.|\n)*?>((.|\n|\r\n)*)?<\/script>/im;
    var match=data.match(reg);
    var myScript="";
    if(match!=null){
        myScript=match[2];
        var script=document.createElement("script");
        script.text=myScript;
        document.getElementsByTagName("head")[0].appendChild(script);
    }    
    var html=data.replace(reg,"");
    o("CheckOutAddressAjax").innerHTML=html;
    readShippingList();
}
//读取配送方式
function readShippingList(){
    loading("ShippingListAjax","配送方式");
    var count=parseInt(o("UnlimitClassGradeCount").value);
    var regionID=readSearchClassID("");
    var url="CheckOutShippingAjax.aspx?RegionID="+regionID;
    Ajax.requestURL(url,dealReadShippingList);    
}
function dealReadShippingList(data){
    o("ShippingListAjax").innerHTML=data;
    selectShipping();
}
//选择配送方式
function selectShipping(){
    var shippingID=getRadioValue(os("name","ShippingID"));
    var regionID=readSearchClassID("");
    var url="Ajax.aspx?Action=SelectShipping&RegionID="+regionID+"&ShippingID="+shippingID;
    Ajax.requestURL(url,dealSelectShipping);    
}
function dealSelectShipping(data){
    var dataArray=data.split("|");
    o("ShippingMoney").value=dataArray[0];
    o("FavorableMoney").value=dataArray[1];
    showMoneyDetail();
}
readUserAddress();
//金额检查
function checkMoney(moneyLeft,fillMoney,obj){
    if(!Validate.isNumber(fillMoney) || fillMoney<0 || fillMoney>moneyLeft){ 
        alertMessage("请填写正确金额",500);
        obj.value=0;
    }
    else{
        o("Balance").value=obj.value;
        showMoneyDetail();
    }
}
//索要发票
function needInvoice(checked){
    if(checked){
        o("InvoiceDiv").style.display="";
    }
    else{
        o("InvoiceDiv").style.display="none";
    }
}
//填写优惠券
function fillUserCoupon(checked){
    if(checked){
        o("UserCouponDiv").style.display="";
    }
    else{
        o("UserCouponDiv").style.display="none";
    }
}
//添加优惠券
function addUserCoupon(){
    var number=o("Number").value;
    var password=o("Password").value;
    if(number!="" && password!=""){
        var url="Ajax.aspx?Action=CheckUserCoupon&Number="+number+"&Password="+password;
        Ajax.requestURL(url,dealAddUserCoupon); 
    }
    else{
        alertMessage("编号和密码不能为空",500);
    }
}
function dealAddUserCoupon(data){
    if(data.indexOf("|")==-1){
       alertMessage(data,500);
    }
    else{
        var dataArray=data.split("|");
        var number=o("Number").value;
        o("UserCoupon").options.add(new Option("编号："+number+"（"+dataArray[1]+" 元）", dataArray[0]+"|"+dataArray[1]));
        o("UserCoupon").value=dataArray[0]+"|"+dataArray[1];
        selectUserCoupon();
    }
}
//选择优惠券
function selectUserCoupon(){
    var userCoupon=o("UserCoupon").value;
    o("CouponMoney").value=userCoupon.split("|")[1];
    showMoneyDetail();    
}
//显示金额
function showMoneyDetail(){
    var productMoney= parseFloat(o("ProductMoney").value);
    var favorableMoney= parseFloat(o("FavorableMoney").value);
    var shippingMoney= parseFloat(o("ShippingMoney").value);
    var balance= parseFloat(o("Balance").value);
    var couponMoney= parseFloat(o("CouponMoney").value);
    if(productMoney-favorableMoney+shippingMoney-balance-couponMoney>=0){
        var content="产品金额："+productMoney+" 元";
        content+="- 优惠金额："+favorableMoney+" 元";
        content+="+ 物流费用："+shippingMoney+" 元";
        content+="- 余额："+balance+" 元";
        content+="- 优惠券："+couponMoney+" 元";
        content+="= 应付金额：<span>"+ parseFloat(productMoney-favorableMoney+shippingMoney-balance-couponMoney).toFixed(2)+" 元</span>";
        o("MoneyDetail").innerHTML=content;
    }
    else{
        alertMessage("金额有错误",500);
    }
}
//提交检查
function checkSubmit(){
    var consignee=o("Consignee").value;
    if(consignee==""){
        alertMessage("收货人姓名不能为空",500);
        return false;
    }
    var address=o("Address").value;
    if(address==""){
        alertMessage("地址不能为空",500);
        return false;
    }
    var tel=o("Tel").value;
    var mobile=o("Mobile").value;
    if(tel=="" && mobile==""){
        alertMessage("固定电话，手机必须得填写一个",500);
        return false;
    }
    var shippingID=getRadioValue(os("name","ShippingID"));
    if(shippingID==""){
        alertMessage("请选择配送方式",500);
        return false;
    }
    var productMoney= parseFloat(o("ProductMoney").value);
    var favorableMoney= parseFloat(o("FavorableMoney").value);
    var shippingMoney= parseFloat(o("ShippingMoney").value);
    var balance= parseFloat(o("Balance").value);
    var couponMoney= parseFloat(o("CouponMoney").value);
    if(productMoney-favorableMoney+shippingMoney-balance-couponMoney<0){
        alertMessage("金额有错误",500);
        return false;
    }
    return true;
}