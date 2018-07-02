//添加产品到礼品包
function addGiftPack(giftPackID,groupID,count,productID,productPhoto,productName){
    productName=productName.replace(",","").replace("|","");
    var url="Ajax.aspx?Action=AddGiftPack&GiftPackID="+giftPackID+"&GroupID="+groupID+"&Count="+count+"&ProductID="+productID+"&ProductPhoto="+productPhoto+"&ProductName="+productName;
    Ajax.requestURL(url,dealAddGiftPack);
}
function dealAddGiftPack(data){
    if(data!=""){
        alertMessage(data);
    }
    else{
        alertMessage("添加成功");
        showGiftPack();
    }
}
//显示已经选择的产品
function showGiftPack(){
    Ajax.requestURL("GiftPackDetailAjax.aspx?GiftPackID="+giftPackID,dealShowGiftPack)
}
function dealShowGiftPack(data){
    o("GiftPackDetailAjax").innerHTML=data;
}
//删除礼品包商品
function deleteGiftPack(groupID,productID){
    var url="Ajax.aspx?Action=DeleteGiftPack&GiftPackID="+giftPackID+"&GroupID="+groupID+"&ProductID="+productID;
    Ajax.requestURL(url,dealDeleteGiftPack);
}
function dealDeleteGiftPack(data){
    if(data!=""){
        alertMessage(data);
    }
    else{
        alertMessage("删除成功");
        showGiftPack();
    }
}
//添加礼品包到购物车
function addToCart(giftPackID){
    var url="Ajax.aspx?Action=AddGiftPackToCart&GiftPackID="+giftPackID;
    Ajax.requestURL(url,dealAddToCart); 
}
function dealAddToCart(data){    
    if(data.indexOf("|")>0){
        var dataArray=data.split("|");
        alertMessage("添加成功");
        o("GiftPackDetailAjax").innerHTML="";
        o("ProductBuyCount").innerHTML=parseInt(o("ProductBuyCount").innerHTML)+parseInt(dataArray[1]);
        o("ProductTotalPrice").innerHTML=parseFloat(o("ProductTotalPrice").innerHTML)+parseFloat(dataArray[2]);
    }
    else{
        alertMessage(data);
    }
}