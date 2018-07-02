 var tempPage=1;
 //搜索产品
function searchProduct(){
    loading("ProductAjax","产品");
    var url="/ProductAjax.aspx?"+searchContion+"&SearchType="+searchType+"&Page="+tempPage;;
    Ajax.requestURL(url,dealSearchProduct);
}
function dealSearchProduct(content){
    o("ProductCount").innerHTML=content.substr(content.lastIndexOf("#")+1);            
    o("ProductAjax").innerHTML=content.substr(0,content.lastIndexOf("#"));
}
//改变排序方式
function changeProductOrderType(productOrderType){
    tempPage=1;
    addCookie("ProductOrderType",productOrderType,0);
    searchProduct();
}
//选择产品展现方式
function selectShowWay(way){
    tempPage=1;
    var oldWay=getCookie("ProductShowWay");
    if(oldWay!=way){
        addCookie("ProductShowWay",way,0);        
        searchProduct();
    }    
    changeShowDisplay();
}
//页面初始化
function pageInit(){
    var productOrderType=getCookie("ProductOrderType");
    if(productOrderType=="" || productOrderType=="undefined"){
        productOrderType="ID";
    }
    o("ProductOrderType").value=productOrderType
    changeShowDisplay()
    searchProduct();
}
//显示的方式的展现
function changeShowDisplay(){
    var productShowWay=getCookie("ProductShowWay");
    var content="";
    if(productShowWay=="2"){
        content="<img src=\""+templatePath+"Style/Images/pictureOff.png\" title=\"图片方式\" onclick=\"selectShowWay(1)\"  /> <img src=\""+templatePath+"Style/Images/listOn.png\" title=\"列表方式\" onclick=\"selectShowWay(2)\" />";
    }
    else{
        content="<img src=\""+templatePath+"Style/Images/pictureOn.png\" title=\"图片方式\" onclick=\"selectShowWay(1)\"  /> <img src=\""+templatePath+"Style/Images/listOff.png\" title=\"列表方式\" onclick=\"selectShowWay(2)\" />";
    }
    o("showWayDiv").innerHTML=content;
}
//分页
function goPage(page){
    tempPage=page;
    searchProduct();
}
pageInit();
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
