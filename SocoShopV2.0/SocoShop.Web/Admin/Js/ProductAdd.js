///属性操作
if(o(globalIDPrefix+"AttributeClassID")!=null){
    readAttribute(o(globalIDPrefix+"AttributeClassID").value);	  
}
function readAttribute(value) {  
    if(value=="0"){  
        o("Attribute-Ajax").innerHTML = "";
    }
    else{        
        loading("Attribute-Ajax","商品属性");
        var url = "AttributeRecordAjax.aspx?AttributeClassID=" + value + "&ProductID="+productID;
        Ajax.requestURL(url, dealReadAttribute);        
    }
}
function dealReadAttribute(content) {
    o("Attribute-Ajax").innerHTML = content;
}
function selectKeyword(attributeID,keyword){
    var attributeIDObj=o(attributeID+"Value");
    if((","+attributeIDObj.value+",").indexOf(","+keyword+",")>-1){
        alertMessage("已存在该关键字");
    }
    else{
        if(attributeIDObj.value==""){
           attributeIDObj.value= keyword;
        }
        else{
           attributeIDObj.value+=","+ keyword;
        }            
    }
}

//读取规格
function readStandard(standardType){
    if(standardType=="0"){
         o("Standard-Ajax").innerHTML = "";
    }
    else{
        loading("Standard-Ajax","商品规格");
        var url = "StandardRecordAjax.aspx?StandardType=" + standardType + "&ProductID="+productID;
        Ajax.requestURL(url, dealReadStandard);    
    }
}
function dealReadStandard(content) {
    o("Standard-Ajax").innerHTML = content;
}
if(o(globalIDPrefix+"StandardType")!=null){
    readStandard(o(globalIDPrefix+"StandardType").value);	  
}
 //添加规格
function selectStandardList(){
    var selectStandard=getCheckboxValue(os("name","StandardControl"));
    var newStandard="";
    if(selectStandard.length>0){
        var rows=o("standardRecordTable").rows;   
        for(var j=0;j<selectStandard.length;j++){      
            if((","+o("StandardIDList").value+",").indexOf(","+selectStandard[j]+",")==-1){
                newStandard=selectStandard[j]+","+newStandard;               
                var standardInfo=o("StandardInfo"+selectStandard[j]).value;
                var currentRow=0;
                for(var i=0;i< rows.length;i++){         
                    var cell = rows[i].insertCell(0);   
                    if(i==0){
                        cell.id="Standard_"+selectStandard[j];
                        cell.innerHTML=standardInfo.split("|")[0]+"<img src=\"Style/Images/delete.gif\" alt=\"删除\" onclick=\"deleteStandard("+selectStandard[j]+")\" style=\"vertical-align:bottom;cursor:pointer\" />"; 
                    }
                    else{
                         for(var k=currentRow;k<parseInt(o("recordCount").value);k++){
                            if(o("standardRecord"+k)!=null){
                                currentRow=k+1;
                                break;                            
                            }
                         }
                         var standardHTML="<select name=\"Standard"+k+"\" style=\"width:80px\">";              
                         var standardValueArray=standardInfo.split("|")[1].split(",");
                         for(var m=0;m<standardValueArray.length;m++){
                             standardHTML+="<option value=\""+standardValueArray[m]+"\">"+standardValueArray[m]+"</option>";
                         }
                         standardHTML+="</select>";
                         cell.innerHTML =standardHTML;  
                    }
                }
            }
        }
    }
    if(newStandard.length>1){
        newStandard=newStandard.substr(0,newStandard.length-1);   
        var oldStandard=o("StandardIDList").value;
        if(oldStandard==""){
            o("StandardIDList").value=newStandard;
         }
        else{
            o("StandardIDList").value=newStandard+","+oldStandard;
        }
    }
}
//删除规格
function deleteStandard(standardID){
    var target=o("Standard_"+standardID).cellIndex;
    var table = o("standardRecordTable");
    var rows=o("standardRecordTable").rows;   
    for(var i = 0;i < rows.length;i++){
        table.rows[i].deleteCell(target);
    }
    var oldStandard=o("StandardIDList").value;
    if(oldStandard==standardID){
        o("StandardIDList").value="";
     }
    else{
        oldStandard=oldStandard.replace(standardID,"").replace(",,",",");
        if(oldStandard.substr(0,1)==","){
            oldStandard=oldStandard.substr(1,oldStandard.length-1);
        }
        if(oldStandard.substr(oldStandard.length-1,1)==","){
            oldStandard=oldStandard.substr(0,oldStandard.length-1);
        }
        o("StandardIDList").value=oldStandard;
    }
}
//添加规格记录
function addStandardRecord(){
     var recordCount=parseInt(o("recordCount").value);
     o("recordCount").value=recordCount+1;
     var objRow =document.getElementById("standardRecordTable").insertRow(document.getElementById("standardRecordTable").rows.length);
     objRow.className = "tableMain";
     objRow.id="standardRecord"+recordCount;
     
     var standardIDList=o("StandardIDList").value;
     var i=0;
     if(standardIDList!=""){
         var standardIDArray=standardIDList.split(",");
         for(i=0;i<standardIDArray.length;i++){
             var objCel = objRow.insertCell(i);
             var standardHTML="<select name=\"Standard"+recordCount+"\" style=\"width:80px\">";        
             var standardInfo=o("StandardInfo"+standardIDArray[i]).value;        
             var standardValueArray=standardInfo.split("|")[1].split(",");
             for(var j=0;j<standardValueArray.length;j++){
                 standardHTML+="<option value=\""+standardValueArray[j]+"\">"+standardValueArray[j]+"</option>";
             }
             standardHTML+="</select>";
             objCel.innerHTML =standardHTML;  
         }  
     } 
 
     var standardType=o(globalIDPrefix+"StandardType").value;
     if(standardType=="2"){
         var objCel = objRow.insertCell(i);
         objCel.innerHTML = "<span id=\"ProductName"+recordCount+"\">当前产品</span><input type=\"hidden\" id=\"Product"+recordCount+"\" name=\"Product\" value=\"0\" /> <img src=\"Style/Images/edit.gif\" title=\"编辑\" onclick=\"selectProduct("+recordCount+")\" style=\"vertical-align:bottom;cursor:pointer\" />"; 
         objCel = objRow.insertCell(i+1);
         objCel.innerHTML = "<img src=\"Style/Images/delete.gif\" alt=\"删除\" onclick=\"deleteStandardRecord("+recordCount+")\" style=\"cursor:pointer\" />";  
     }
     else{
        var objCel = objRow.insertCell(i);
        objCel.innerHTML = "<img src=\"Style/Images/delete.gif\" alt=\"删除\" onclick=\"deleteStandardRecord("+recordCount+")\" style=\"cursor:pointer\" />";  
     }
}
//删除规格记录
function deleteStandardRecord(i){
    removeSelf(o("standardRecord"+i));
}
//选择商品
function selectProduct(i){
    var url="SelectProduct.aspx?Tag="+i;
    var width="800";
    var height="600";
    var title="选择商品";
    var id="SelectProduct";
    try{
        childPop(url,width,height,title,id);    
    }
    catch(e){
        pop(url,width,height,title,id);   
    }
}
function dealSelectProduct(productID,productName,tag){
    o("ProductName"+tag).innerHTML=productName;
    o("Product"+tag).value=productID;
}
//检查商品规格
function checkStandardRecord(){
    var standardType=o(globalIDPrefix+"StandardType").value;
    if(o("recordCount")!=null){
        var recordCount=o("recordCount").value;
        var result=true;
        if(standardType=="1" ||standardType=="2"){
            for(var i=0;i<recordCount;i++){
                if(os("name","Standard"+i)!=null){
                    var tempValue=getSelectValue("Standard"+i);
                    for(var k=i+1;k<recordCount;k++){
                        if(os("name","Standard"+k)!=null){
                            var tempValue2=getSelectValue("Standard"+k);
                            if(tempValue==tempValue2 && tempValue!=""){
                                return false;
                            }
                        }
                    }
                }
            }
            if(standardType=="2"){
                for(var i=0;i<recordCount;i++){
                    if(o("Product"+i)!=null){
                        var tempValue=o("Product"+i).value;
                        for(var k=i+1;k<recordCount;k++){
                            if(o("Product"+k)!=null){
                                var tempValue2=o("Product"+k).value;
                                if(tempValue==tempValue2 && tempValue!=""){
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }   
    return true;
}
function getSelectValue(name){
    var objs=os("name",name);
    var result="";
    if (objs != null && objs.length > 0) {
        for (var i = 0; i < objs.length; i++) {
            if(result==""){
                result=objs[i].value;   
            }
            else{
                result+=","+objs[i].value;   
            }               
        }
    }
    return result;
}
//搜索关联商品
function searchRelationProduct() {
    var productName = o(globalIDPrefix+"ProductName").value;
    var classID = o(globalIDPrefix+"RelationClassID").value;
    var brandID = o(globalIDPrefix+"RelationBrandID").value;
    var id= getQueryString("ID");   
    var url = "ProductAjax.aspx?ControlName=CandidateProduct&Action=SearchRelationProduct&ProductName=" + encodeURI(productName) + "&ClassID=" + classID + "&BrandID=" + brandID + "&ID=" + id;
    Ajax.requestURL(url, dealSearchRelationProduct);
    alertMessage("正在搜索...", 1); 
}
function dealSearchRelationProduct(data) {
    closeAlertDiv();
    var obj = o("CandidateProductBox");
    obj.removeChild(o(globalIDPrefix+"CandidateProduct"));
    obj.innerHTML = data;
}
//搜索关联配件
function searchProductAccessory(action) {
    var productAccessoryName = o(globalIDPrefix+"AccessoryProductName").value;
    var classID = o(globalIDPrefix+"AccessoryClassID").value;
    var brandID = o(globalIDPrefix+"AccessoryBrandID").value;
    var id= getQueryString("ID");    
    var url = "ProductAjax.aspx?ControlName=CandidateAccessory&Action=SearchProductAccessory&ProductName=" + encodeURI(productAccessoryName) + "&ClassID=" + classID + "&BrandID=" + brandID + "&ID=" + id;
    Ajax.requestURL(url, dealSearchProductAccessory);
    alertMessage("正在搜索...", 1); 
}
function dealSearchProductAccessory(data) {
    closeAlertDiv();
    var obj = o("CandidateAccessoryBox");
    obj.removeChild(o(globalIDPrefix+"CandidateAccessory"));
    obj.innerHTML = data;
}
//搜索关联文章
function searchRelationArticle() {
    var title = o(globalIDPrefix+"ArticleName").value;
    var classID = o(globalIDPrefix+"ArticleClassID").value;    
    var url = "ProductAjax.aspx?ControlName=CandidateArticle&Action=SearchRelationArticle&ArticleTitle=" + encodeURI(title)+"&ClassID="+classID;
    Ajax.requestURL(url, dealSearchArticle);
    alertMessage("正在搜索...", 1); 
}
function dealSearchArticle(data) {
    closeAlertDiv();
    var obj = o("CandidateArticleBox");
    obj.removeChild(o(globalIDPrefix+"CandidateArticle"));
    obj.innerHTML = data;
}


function addAll(candidateObjName, selectedObjName) {
    var strID = getSelectedID(selectedObjName);
    var candidateObj = o(candidateObjName);
    var selectedObj = o(selectedObjName);   
    for (var i = 0; i < candidateObj.length; i++) {
        if (strID.indexOf("|" + candidateObj.options[i].value + "|") == -1) { 
            selectedObj.options[selectedObj.length] = new Option(candidateObj.options[i].text, candidateObj.options[i].value);            
        }
    }
}

function addSingle(candidateObjName, selectedObjName) {
    var strID = getSelectedID(selectedObjName);
    var candidateObj = o(candidateObjName);
    var selectedObj = o(selectedObjName);  
    for (var i = 0; i < candidateObj.length; i++) {
        if (candidateObj.options[i].selected && strID.indexOf("|" + candidateObj.options[i].value + "|") == -1) {  
            selectedObj.options[selectedObj.length] = new Option(candidateObj.options[i].text, candidateObj.options[i].value);            
        }
    }
}

function dropSingle(objName) {
    var obj = o(objName);
    if (obj.length < 1) {
        return;
    }
    for (var i = obj.length - 1; i >= 0; i--) {
        if (obj.options[i].selected) {
            obj.remove(i);
        }
    }
}

function dropAll(objName) {
    var obj = o(objName);
    if (obj.length < 1) {
        return;
    }
    for (var i = obj.length - 1; i >= 0; i--) {
        obj.remove(i);
    }
}


function addProductAccessoryAll(candidateObjName, selectedObjName) {
    var strID = getSelectedID(selectedObjName);
    var candidateObj = o(candidateObjName);
    var selectedObj = o(selectedObjName);
    for (var i = 0; i < candidateObj.length; i++) {
        if (strID.indexOf("|" + candidateObj.options[i].value + "|") == -1) {
            selectedObj.options[selectedObj.length] = new Option(candidateObj.options[i].text , candidateObj.options[i].value);
         }
    }
}
function addProductAccessorySingle(candidateObjName, selectedObjName) {
    var strID = getSelectedID(selectedObjName);
    var candidateObj = o(candidateObjName);
    var selectedObj = o(selectedObjName);
    for (var i = 0; i < candidateObj.length; i++) {
        if (candidateObj.options[i].selected && strID.indexOf("|" + candidateObj.options[i].value + "|") == -1) {
            selectedObj.options[selectedObj.length] = new Option(candidateObj.options[i].text, candidateObj.options[i].value);
        }
    }
}
function getSelectedID(objName) {
    var obj = o(objName);
    var result = "";
    for (var i = 0; i < obj.length; i++) {
        result = result + "|" + obj.options[i].value;
    }
    result = result + "|";
    return result;
}


function checkProduct() {
    checkProductHandler(globalIDPrefix+"Product", "RelationProductID");
    checkProductHandler(globalIDPrefix+"Article", "RelationArticleID");
    checkProductHandler(globalIDPrefix+"Accessory", "RelationAccessoryID");    
    if(checkStandardRecord()){
        return Page_ClientValidate();  
    } 
    else{
         var standardType=o(globalIDPrefix+"StandardType").value;
         if(standardType=="1"){
            alertMessage("同一个产品不能有多个相同的规格");
         }
         else{
            alertMessage("每一个产品只能对应一个不同的规格");
         }
        return false;
    }      
}

function checkArticle() {
    checkProductHandler(globalIDPrefix+"Product", "RelationProductID");
}

function checkProductHandler(selectedObjName, operateObjName) {
    try {
        var obj = o(selectedObjName);
        var strID = '';
        for (var i = 0; i < obj.length; i++) {
            if (strID != '') {
                strID += ',' + obj.options[i].value;
            }
            else {
                strID = obj.options[i].value;
            }
        }
        o(operateObjName).value = strID;
    } catch(e){}
}
//添加商品图片
function addProductPhoto(photo,name){
    var productID=getQueryString("ID");	    
    if(productID==""){
        var content="<div class=\"productPhoto\" id=\"ProductPhoto"+photo+"\">";
        content+="<div><img src=\""+photo+"\" alt=\"\"  title=\""+photo+"\" onload=\"photoLoad(this,90,90)\"/></div>";
        content+=substring(name,6)+"<br/>";
        content+="<span onclick=\"deleteProductPhoto('"+photo+"')\" style=\"cursor:pointer\"><img src=\"style/images/delete.gif\" /></span>";	        
        content+="<input name=\"ProductPhoto\"  type=\"hidden\" value=\""+name+"|"+photo+"\"/>";
        content+="</div>";
        o("ProductPhotoList").innerHTML=o("ProductPhotoList").innerHTML+content;
    }
    else{
        Ajax.requestURL("Ajax.aspx?Action=AddProductPhoto&ProductID="+productID+"&Name="+name+"&Photo="+photo,dealAddProductPhoto);
    }
}
function dealAddProductPhoto(data){
    if(data!=""){
        var productPhotoArray=data.split("|");
        id=productPhotoArray[0];
        name=productPhotoArray[1];
        photo=productPhotoArray[2];
        var content="<div class=\"productPhoto\" id=\"ProductPhoto"+id+"\">";
        content+="<div><img src=\""+photo+"\" alt=\"\"  title=\""+photo+"\" onload=\"photoLoad(this,90,90)\"/></div>";
        content+=substring(name,6)+"<br/>";
        content+="<span onclick=\"deleteProductPhoto('"+id+"')\" style=\"cursor:pointer\"><img src=\"style/images/delete.gif\" /></span>";	 
        content+="</div>";
        o("ProductPhotoList").innerHTML=o("ProductPhotoList").innerHTML+content;
    }
}
//删除产品图片
function deleteProductPhoto(id){
    var productID=getQueryString("ID");
    removeSelf(o("ProductPhoto"+id));
    if(productID!=""){
        Ajax.requestURL("Ajax.aspx?Action=DeleteProductPhoto&ProductPhotoID="+id,function(){});
    }
}
//删除产品
function deleteProduct(productID){    if(window.confirm("确定要删除该产品？")){        var url="Ajax.aspx?Action=DeleteProduct&ProductID="+productID;        Ajax.requestURL(url,dealDeleteProduct);    }}function dealDeleteProduct(data){    if(data=="ok"){        reloadPage();    }    else{        alertMessage("该产品存在相关订单，不能删除。");    }}