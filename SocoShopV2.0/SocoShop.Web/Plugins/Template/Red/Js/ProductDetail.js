var preID="Introduce";
//切换页签
function show(id){
    if(id!=preID){        
        o("title"+preID).className="productOff";
        o("product"+preID).style.display="none";
        o("title"+id).className="productOn";
        o("product"+id).style.display="";
        preID=id;
    }
}
//记录浏览的产品
function recordProduct(){  
    var historyProduct=getCookie("HistoryProduct");
    if((","+historyProduct+",").indexOf(","+productID+",")==-1){
        if(historyProduct==""){
            historyProduct=productID;
        }
        else{
            historyProduct=productID+","+historyProduct;
        }
        if(historyProduct.toString().indexOf(",")>-1){
            if(historyProduct.split(",").length>8){
                historyProduct=historyProduct.substring(0,historyProduct.lastIndexOf(","));          
            }   
        }   
        addCookie("HistoryProduct",historyProduct,0);    
    }  
}
var tempPage=1;
 //读取产品评论
function readProductComment(){
    loading("ProductCommentAjax","产品评论");
    var url="/ProductCommentAjax.aspx?ProductID="+productID+"&Page="+tempPage;
    Ajax.requestURL(url,dealSearchProductComment);
}
function dealSearchProductComment(content){
    o("ProductCommentAjax").innerHTML=content
}
//分页
function goPage(page){
    tempPage=page;
    readProductComment();
}
//页面初始化
function pageInit(allowComment){
    recordProduct();
    if(allowComment==1){
        readProductComment();
    }
    productPhotoScroll();
}
//提交评论
function postComment(){
    var rank=getRadioValue(os("name","rank"));
    if(rank==""){
        alertMessage("请选择分数");
        return false;
    }
    var title=o("title").value;
    if(title==""){
        alertMessage("请输入标题");
        return false;
    }
    var content=o("content").value;
    if(content==""){
        alertMessage("请输入内容");
        return false;
    }
    var url="/ProductCommentAjax.aspx?Action=Add&ProductID="+productID+"&Rank="+rank+"&Title="+title+"&Content="+content;
    Ajax.requestURL(url,dealPostComment);
}
function dealPostComment(content){
    if(content=="ok"){
        alertMessage("评论成功");
        tempPage=1;
        readProductComment();
    }
    else{
        alertMessage(content);
    }
}
var tempCommentID=0;
//反对评论
function against(commentID){
    tempCommentID=commentID;
    var url="/ProductCommentAjax.aspx?Action=Against&CommentID="+commentID;    
    Ajax.requestURL(url,dealAgainst);
}
function dealAgainst(content){
    if(content=="ok"){
        alertMessage("反对成功");
        o("Against"+tempCommentID).innerHTML= parseFloat(o("Against"+tempCommentID).innerHTML)+1;
    }
    else{
        alertMessage(content);
    }
}
//支持评论
function support(commentID){
    tempCommentID=commentID;
    var url="/ProductCommentAjax.aspx?Action=Support&CommentID="+commentID;
    Ajax.requestURL(url,dealSupport);
}
function dealSupport(content){
    if(content=="ok"){
        alertMessage("支持成功");
        o("Support"+tempCommentID).innerHTML= parseFloat(o("Support"+tempCommentID).innerHTML)+1;
    }
    else{
        alertMessage(content);
    }
}
//收藏产品
function collectProduct(productID){
    var url="/Ajax.aspx?Action=Collect&ProductID="+productID;;
    Ajax.requestURL(url,dealCollectProduct);
}
function dealCollectProduct(content){
    if(content!=""){
        alertMessage(content);
    }
}
//添加好友
function addFriend(userID){
    var url="/Ajax.aspx?Action=AddFriend&UserID="+userID;;
    Ajax.requestURL(url,dealAddFriend);
}
function dealAddFriend(content){
    if(content!=""){
        alertMessage(content);
    }
}
//选择单一规格
function selectSingleStandard(value,id){
    o("Standard_"+id).innerHTML=value;
    o("StandardValue_"+id).value=value;
    var standardValue=getTextValue(os("name","StandardValue"));
    var check=true;
    for(var i=0;i<standardValue.length;i++){
        if(standardValue[i]==""){
            check=false;
            break;
        }
    }
    if(check){
        var standardRecordValueList=o("StandardRecordValueList").value;
        if(standardRecordValueList.indexOf("|"+productID+","+standardValue+"|")==-1){
            o("Standard_"+id).innerHTML="请选择";
            o("StandardValue_"+id).value="";
            alertMessage("没有该规格的产品");
        }
    }
}
//选择产品组规格
function selectMultiStandard(productID){
    window.location.href="/ProductDetail-I"+productID+".aspx";  
}
//提交标签
function addTags(){
    var content=o("Word").value;
    if(content==""){
        alertMessage("请输入内容");
        return false;
    }
    var url="/ProductCommentAjax.aspx?Action=AddTags&ProductID="+productID+"&Word="+content;
    Ajax.requestURL(url,dealAddTags);
}
function dealAddTags(content){
    if(content=="ok"){
        alertMessage("提交成功");
        var content=o("Word").value;
        content="<a href=\"/Product/Tags/"+encodeURIComponent(content)+".aspx\" >"+content+"</a>";
        o("ProductTagAjax").innerHTML= o("ProductTagAjax").innerHTML+content;
    }
    else{
        alertMessage(content);
    }
}
//产品图片
function productPhotoScroll(){
    var photoScroll = new Scroll();
    photoScroll.num = 5;
    photoScroll.speed = 10; //速度(毫秒)
    photoScroll.space = 2; //每次移动(px)
    photoScroll.pageWidth = 61; //翻页宽度
    photoScroll.scrollList1 = "PhotoList1";
    photoScroll.scrollList2 = "PhotoList2";
    photoScroll.leftButton = "PhotoLeft";
    photoScroll.rightButton = "PhotoRight";
    photoScroll.scrollAll = "PhotoCount";
    photoScroll.Init();
}
//显示大图
function showPhoto(bigPhoto){
    o("BigPhoto").src=bigPhoto;
}
//计算价格
function countPrice(count,leftStorageCount){
    if(Validate.isInt(count) &&  parseInt(count)>0){    
        if(count<=leftStorageCount){
            var currentMemberPrice=o("CurrentMemberPrice").value;
            o("currentTotalMemberPrice").innerHTML=(parseInt(count)* parseFloat(currentMemberPrice)).toFixed(2);
        }
        else{
            alertMessage("当前库存不能满足您的购买数量");
            o("BuyCount").value="";
            o("BuyCount").focus();
        }   
    }
    else{
        alertMessage("数量填写有错误");
    }    
}
//添加到购物车
function addToCart(productID,productName,productStandardType){
    var check=true;
    var attributeName="";
    if(productStandardType=="1"){
        var standardValue=getTextValue(os("name","StandardValue"));       
        for(var i=0;i<standardValue.length;i++){
            attributeName+=standardValue[i]+",";
            if(standardValue[i]==""){
                check=false;
                break;
            }
        }
    }
    if(check){
        var buyCount=o("BuyCount").value;
        if(Validate.isInt(buyCount) &&  parseInt(buyCount)>0){
            if(attributeName!=""){
                productName=productName+"("+attributeName.substr(0,attributeName.length-1)+")";
            }
            var currentMemberPrice=o("CurrentMemberPrice").value; 
            var url="/Ajax.aspx?Action=AddToCart&ProductID="+productID+"&ProductName="+ encodeURIComponent(productName)+"&BuyCount="+buyCount+"&CurrentMemberPrice="+currentMemberPrice;
            Ajax.requestURL(url,dealAddToCart);
        }
        else{
            alertMessage("数量填写有错误");
        }
    }
    else{
        alertMessage("请选择规格");
    }
}
//立即购买
var redirect=false;
function buyNow(productID,productName,productStandardType){
    addToCart(productID,productName,productStandardType);
    redirect=true;
}
function dealAddToCart(content){     
    if(content=="ok"){
        if(redirect){//立即购买 
            redirect=false; 
            window.location.href="/Cart.aspx";
        }
        else{//添加到购物车
            alertMessage("添加成功");
            var buyCount=o("BuyCount").value;
            var currentMemberPrice=o("CurrentMemberPrice").value; 
            o("ProductBuyCount").innerHTML=parseInt(o("ProductBuyCount").innerHTML)+parseInt(buyCount);
            o("ProductTotalPrice").innerHTML=parseFloat(o("ProductTotalPrice").innerHTML)+parseInt(buyCount)*parseFloat(currentMemberPrice);
        }
    }
    else{
        redirect=false; 
        alertMessage(content);
    }
}

//缺货登记
function bookingProduct(productID,productName,productStandardType){
    var check=true;
    var attributeName="";
    if(productStandardType=="1"){
        var standardValue=getTextValue(os("name","StandardValue"));       
        for(var i=0;i<standardValue.length;i++){
            attributeName+=standardValue[i]+",";
            if(standardValue[i]==""){
                check=false;
                break;
            }
        }
    }
    if(check){
        if(attributeName!=""){
            productName=productName+"("+attributeName.substr(0,attributeName.length-1)+")";
        }
        var url="/BookingProductAdd.aspx?ProductID="+productID+"&ProductName="+ encodeURIComponent(productName);
        window.location.href=url;
    }
    else{
        alertMessage("请选择规格");
    }
}

