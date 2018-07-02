//读取购物车
function readCart(){
    loading("CartProductAjax","购物车");
    Ajax.requestURL("CartAjax.aspx?Action=Read",dealReadCart)
}
function dealReadCart(data){
    o("CartProductAjax").innerHTML=data;
}
//清空购物车
function clearCart(){
    Ajax.requestURL("CartAjax.aspx?Action=ClearCart",dealClearCart)
}
function dealClearCart(data){
    o("CartProductAjax").innerHTML="";
    o("ProductBuyCount").innerHTML=0;
    o("ProductTotalPrice").innerHTML=0;
}
//改变购买数量
function changeOrderProductBuyCount(strCartID,buyCountObj,price,productCount,leftStorageCount,productWeight){
    var buyCount=buyCountObj.value;
    var oldCount=o("BuyCount"+strCartID).value;
    if(buyCount!=oldCount){
        if(Validate.isInt(buyCount) &&  parseInt(buyCount)>0){
            if(parseInt(buyCount)<=leftStorageCount){   
                Ajax.requestURL("CartAjax.aspx?Action=ChangeBuyCount&StrCartID="+strCartID+"&BuyCount="+buyCount+"&OldCount="+oldCount+"&Price="+price+"&ProductCount="+productCount+"&ProductWeight="+productWeight,dealChangeCart);
            }
            else{
                buyCountObj.value=oldCount;
                alertMessage("当前库存不能满足您的购买数量");
            }
        }
        else{
            alertMessage("数量填写有错误");
        }
    }
}
function dealChangeCart(data){
    try{
        var dataArray=data.split("|");
        o("ProductBuyCount").innerHTML=dataArray[1];
        o("ProductTotalPrice").innerHTML=dataArray[2];
        o("CartProductTotalPrice").innerHTML="￥"+dataArray[2];
        o("CartProductPrice"+dataArray[0]).innerHTML="￥"+parseFloat(dataArray[3]).toFixed(2);
        o("BuyCount"+dataArray[0]).value=dataArray[4];
     }
    catch(e){alertMessage("修改失败");}
}
//删除购物车
function deleteOrderProduct(strCartID,price,productCount,productWeight){
    var oldCount=o("BuyCount"+strCartID).value;
    Ajax.requestURL("CartAjax.aspx?Action=Delete&StrCartID="+strCartID+"&OldCount="+oldCount+"&Price="+price+"&ProductCount="+productCount+"&ProductWeight="+productWeight,dealDeleteCart);    
}
function dealDeleteCart(data){
    try{
        var dataArray=data.split("|");
        o("ProductBuyCount").innerHTML=dataArray[1];
        o("ProductTotalPrice").innerHTML=dataArray[2];
        o("CartProductTotalPrice").innerHTML="￥"+dataArray[2];
        removeSelf(o("Cart"+dataArray[0]));
    }
    catch(e){alertMessage("删除失败");}
}
readCart();