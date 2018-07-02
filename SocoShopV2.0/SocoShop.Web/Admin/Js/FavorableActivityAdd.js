function changeShippingWay(){
    var objs=os("name","ShippingWay");
    var regionObj=o("ShippingRegionDiv");
    var value=getRadioValue(objs);
    if(value=="0"){
        regionObj.style.display="none";
    }
    else{
        regionObj.style.display="";
    }
}
function changeReduceWay(){
    var objs=os("name","ReduceWay");
    var reduceMoneyObj=o("ReduceMoneyDiv");
    var reduceDiscountObj=o("ReduceDiscountDiv");
    var value=getRadioValue(objs);
    if(value=="0"){
        reduceMoneyObj.style.display="none";
        reduceDiscountObj.style.display="none";
    }
    else if(value=="1"){
        reduceMoneyObj.style.display="";
        reduceDiscountObj.style.display="none";
    }
    else{
        reduceMoneyObj.style.display="none";
        reduceDiscountObj.style.display="";
    }
}
function changeGiftWay(){
    var objs=os("name","GiftWay");
    var giftObj=o("GiftDiv");
    var value=getRadioValue(objs);
    if(value=="0"){
        giftObj.style.display="none";
    }
    else{
        giftObj.style.display="";
    }
}

function  searchGift(){
    var name=o("GiftName").value;
    var url = "Ajax.aspx?Action=SearchGift&Name=" + name;
    Ajax.requestURL(url, dealSearchGift);
    alertMessage("正在搜索...", 1); 
}
function dealSearchGift(data){
    closeAlertDiv();
    if(data!=""){
        var result="";
        var giftArray=data.split("#");
        for(var i=0;i<giftArray.length;i++){
            var tempArray=giftArray[i].split("|");
            result+="<span><input type=\"checkbox\" value=\""+giftArray[i]+"\" onclick=\"selectGift(this)\"> "+substring(tempArray[1],10)+" </span>";
        }
        o("SearchGiftList").innerHTML="<br />"+result;
    }
}
function selectGift(obj){
    var giftArray=obj.value.split("|");
    if(obj.checked){
        if(o("Gift"+giftArray[0])==null){
            var content="<span id=\"Gift"+giftArray[0]+"\">"+substring(giftArray[1],8)+"<span onclick=\"deleteGift("+giftArray[0]+")\" style=\"cursor:pointer\"><img src=\"style/images/delete.gif\" /></span><input class=\"input\" name=\"GiftList\" type=\"hidden\" value=\""+giftArray[0]+"\"/></span>";
            o("SelectGiftList").innerHTML=o("SelectGiftList").innerHTML+content;
        }
       else{
            alertMessage("已存在该礼品"); 
       }
   }
   else{
        try{
           deleteGift(giftArray[0]);
       }catch(e){}
   }
}
function deleteGift(id){
    var obj = o("Gift" + id);
    obj.parentNode.removeChild(obj); 
}