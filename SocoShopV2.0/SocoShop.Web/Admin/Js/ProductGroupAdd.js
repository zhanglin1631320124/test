//增加一条商品组
function addProductGroup(photo,link,idList,nameList,photoList,themeActivityID){
    var countObj=o("ProductGroupCount");
    var parentObj=o("ProductGroup");
    var span = document.createElement("span");
    with (span) {
        id = "ProductGroup" + countObj.value;
        className="themeActivityBlock";
    }
    parentObj.appendChild(span);   
    span.innerHTML =readContent(photo,link,idList,nameList,photoList,countObj.value,themeActivityID) ;	
   o("ProductGroupCount").value=1+parseInt(countObj.value);
}
//修改一条商品组
function updateProductGroup(photo,link,idList,nameList,photoList,id,themeActivityID){
    var span = o("ProductGroup"+id);
    span.className="themeActivityBlock";
    span.innerHTML =readContent(photo,link,idList,nameList,photoList,id,themeActivityID);
}
//删除一条商品组
function deleteProductGroup(id){
    var obj = o("ProductGroup" + id);
    obj.parentNode.removeChild(obj); 
}
//提交检查
function checkSubmit() {
    checkProductHandler(globalIDPrefix+"Product", "RelationProductID");
    Page_ClientValidate();
}
//读取内容
function readContent(photo,link,idList,nameList,photoList,id,themeActivityID){
    var result="<input type=\"button\" class=\"button\" onclick=\"childPop('ProductGroupAdd.aspx?Action=Update&ID="+id+"&ThemeActivityID="+themeActivityID+"',800,600,'修改产品组','ProductGroupAdd')\" value=\"修改产品组\"  style=\"width:80px\" /> ";
    result+="<input type=\"button\" class=\"button\" onclick=\"deleteProductGroup("+id+")\" value=\"删除产品组\"  style=\"width:80px\" />";
    result+="<input name=\"ProductGroupValue"+id+"\" id=\"ProductGroupValue"+id+"\"  type=\"hidden\"  value=\""+photo+"|"+link+"|"+idList +"\"/>";
	result+="<ul>";
	result+="<li class=\"left\">图片：</li>";
	result+="<li class=\"right\">";
	if(photo!=""){
	    result+="<img src=\""+photo+"\"  height=\"60\"/>";
	}
    result+="</li>";
	result+="</ul>";	
	result+="<ul>";
	result+="<li class=\"left\">更多地址：</li>";
	result+="<li class=\"right\">"+link +"</li>";
	result+="</ul>";	
	result+="<ul>";
	result+="<li class=\"left\">商品：</li>";
	result+="<li class=\"right\">";
	if(idList!=""){
	    for(var i=0;i<idList.split(",").length;i++)	{	   
	        result+="<div class=\"themeActivityPhoto\"><img  src=\""+photoList.split(",")[i]+"\" alt=\"\" onload=\"photoLoad(this,60,60)\" title=\""+nameList.split(",")[i]+"\" /><br />"+nameList.split(",")[i]+"</div>";
	    }
	}
    result+="</li>";
	result+="</ul>";
	result+="<input name=\"ProductGroupNameValue"+id+"\" id=\"ProductGroupNameValue"+id+"\"  type=\"hidden\"  value=\""+nameList +"\"/>";
	return result;
}
//读取一条产品组数据
function readProductGroup(id,themeActivityID){
    var DG = frameElement.lhgDG;
    var productGroupValue=DG.iDoc("ThemeActivityAdd"+themeActivityID).getElementById("ProductGroupValue"+id).value;  
    var productGroupNameValue=DG.iDoc("ThemeActivityAdd"+themeActivityID).getElementById("ProductGroupNameValue"+id).value;   
    var valueArray=productGroupValue.split("|");
    var nameArray=productGroupNameValue.split(",");
    o(globalIDPrefix+"Photo").value=valueArray[0];
    o(globalIDPrefix+"Link").value=valueArray[1]; 
    var productObj=o(globalIDPrefix+"Product");
    for (var i = 0; i < valueArray[2].split(",").length; i++) {
        productObj.options.add(new Option(nameArray[i], valueArray[2].split(",")[i]));
    }
}