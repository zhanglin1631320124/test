var ExchangeAwardAdd={
    //搜索商品
    searchProduct:function(){
        var classID=o(globalIDPrefix +"ClassID").value;
        var productName=o("ProductName").value;
        var url = "ExchangeAwardAdd.aspx?Action=SearchProduct&ProductName=" + encodeURI(productName)+"&ClassID="+classID;
        Ajax.requestURL(url, this.dealSearchProduct);
        alertMessage("正在搜索...", 1); 
    },
    dealSearchProduct:function(data){
        closeAlertDiv();
        if(data!=""){
            var result="";
            var productArray=data.split("#");
            for(var i=0;i<productArray.length;i++){
                var tempArray=productArray[i].split("|");
                result+="<span><input type=\"checkbox\" value=\""+productArray[i]+"\" onclick=\"ExchangeAwardAdd.selectProduct(this)\"> "+substring(tempArray[1],10)+" </span>";
            }
            o("SearchProduct").innerHTML=result;
        }
    },
    selectProduct:function(obj){
        var productArray=obj.value.split("|");
        if(obj.checked){
            if(o("Product"+productArray[0])==null){
                var content="<div class=\"exchangeAwardPhoto\" id=\"Product"+productArray[0]+"\"><img  src=\""+productArray[2]+"\" alt=\"\" width=\"100\" height=\"100\" title=\""+productArray[1]+"\" /><br />"+substring(productArray[1],8)+"<br /><span onclick=\"ExchangeAwardAdd.deleteProduct("+productArray[0]+")\" style=\"cursor:pointer\"><img src=\"/Admin/style/images/delete.gif\" /></span><br/>积分数：<input class=\"input\" name=\"PointList\" style=\"width:50px;\" type=\"text\" value=\"0\"/><input class=\"input\" name=\"ProductList\" type=\"hidden\" value=\""+productArray[0]+"\"/></div>";
                o("AwardProduct").innerHTML=o("AwardProduct").innerHTML+content;
            }
           else{
                alertMessage("已存在该商品"); 
           }
       }
       else{
            try{
                this.deleteProduct(giftArray[0]);
            }catch(e){}
       }
    },
    deleteProduct:function(id){
        var obj = o("Product" + id);
        obj.parentNode.removeChild(obj); 
    }
}