/**逐个编辑**/
function saveSingleEdit(productID) {
    var productNumber = o("ProductNumber" + productID);
    var weight = o("Weight" + productID);
    var marketPrice = o("MarketPrice" + productID);
    var memberPrice="";
    var sendPoint = o("SendPoint" + productID);
    var totalStorageCount = o("TotalStorageCount" + productID);
    var lowerCount = o("LowerCount" + productID);
    var upperCount = o("UpperCount" + productID);
    if (!Validate.isInt(weight.value)) {
        alertMessage("重量必须是数字类型");
        return;
    }
    if (!Validate.isNumber(marketPrice.value)) {
        alertMessage("市场价必须是货币类型");
        return;
    }
    if(userGradeIDList!=""){
        var userGradeIDArray=userGradeIDList.split(",");
        var userGradeNameArray=userGradeNameList.split(",");
        for(var i=0;i<userGradeIDArray.length;i++){           
            if (!Validate.isNumber(o("MemberPrice" +userGradeIDArray[i]+"_"+ productID).value)) {
                alertMessage(userGradeNameArray[i]+"价必须是货币类型");
                return;
            }
            if(memberPrice==""){
                memberPrice=o("MemberPrice" +userGradeIDArray[i]+"_"+ productID).value;
            }
            else{
                memberPrice+=","+o("MemberPrice" +userGradeIDArray[i]+"_"+ productID).value;
            }
        }
    }
    if (!Validate.isInt(sendPoint.value)) {
        alertMessage("赠送积分必须是数字类型");
        return;
    }
    if (!Validate.isInt(totalStorageCount.value)) {
        alertMessage("库存数量必须是数字类型");
        return;
    }
    if (!Validate.isInt(lowerCount.value)) {
        alertMessage("库存下限必须是数字类型");
        return;
    }
    if (!Validate.isInt(upperCount.value)) {
        alertMessage("库存上限必须是数字类型");
        return;
    }   
    var url = "ProductSingleEdit.aspx?Action=SingleEdit&ProductID=" + productID;
    url += "&ProductNumber=" + productNumber.value;
    url += "&Weight=" + weight.value;
    url += "&MarketPrice=" + marketPrice.value;
    url += "&MemberPrice=" + memberPrice;
    url += "&SendPoint=" + sendPoint.value;
    url += "&TotalStorageCount=" + totalStorageCount.value;
    url += "&LowerCount=" + lowerCount.value;
    url += "&UpperCount=" + upperCount.value;
    Ajax.requestURL(url, dealSaveEdit);
}
function dealSaveEdit(data) {
    alertMessage(data);
}

/**统一编辑**/
function deleteProduct(productID){
    removeSelf(o("Product"+productID));
}
function saveUnionEdit(){
    var productIDList=getCheckboxValue(os("name","ProductID"));
    if(productIDList!=""){
        var isEdit=false;
        var weight = o("Weight");
        var marketPrice = o("MarketPrice");
        var memberPrice="";
        var sendPoint = o("SendPoint");
        var totalStorageCount = o("TotalStorageCount");
        var lowerCount = o("LowerCount" );
        var upperCount = o("UpperCount");
        if(weight.value!=""){
            if (!Validate.isInt(weight.value)) {
                alertMessage("重量必须是数字类型");
                return;
            }
            isEdit=true;
        }
        if(marketPrice.value!=""){
            if (!Validate.isNumber(marketPrice.value)) {
                alertMessage("市场价必须是货币类型");
                return;
            }
            isEdit=true;
        }
        if(userGradeIDList!=""){
            var userGradeIDArray=userGradeIDList.split(",");
            var userGradeNameArray=userGradeNameList.split(",");
            for(var i=0;i<userGradeIDArray.length;i++){     
                if(o("MemberPrice" +userGradeIDArray[i]).value!=""){      
                    if (!Validate.isNumber(o("MemberPrice" +userGradeIDArray[i]).value)) {
                        alertMessage(userGradeNameArray[i]+"价必须是货币类型");
                        return;
                    }
                    isEdit=true;
                }
                if(memberPrice==""){
                    memberPrice=o("MemberPrice" +userGradeIDArray[i]).value;
                }
                else{
                    memberPrice+=","+o("MemberPrice" +userGradeIDArray[i]).value;
                }
            }
        }
        if(sendPoint.value!=""){
            if (!Validate.isInt(sendPoint.value)) {
                alertMessage("赠送积分必须是数字类型");
                return;
            }
            isEdit=true;
        }
        if(totalStorageCount.value!=""){
            if (!Validate.isInt(totalStorageCount.value)) {
                alertMessage("库存数量必须是数字类型");
                return;
            }
            isEdit=true;
        }
        if(lowerCount.value!=""){
            if (!Validate.isInt(lowerCount.value)) {
                alertMessage("库存下限必须是数字类型");
                return;
            }
            isEdit=true;
        }
        if(upperCount.value!=""){
            if (!Validate.isInt(upperCount.value)) {
                alertMessage("库存上限必须是数字类型");
                return;
            } 
            isEdit=true;  
        }   
        if(isEdit){
            var url = "ProductUnionEdit.aspx?Action=UnionEdit&ProductIDList=" + productIDList;
            url += "&Weight=" + weight.value;
            url += "&MarketPrice=" + marketPrice.value;
            url += "&MemberPrice=" + memberPrice;
            url += "&SendPoint=" + sendPoint.value;
            url += "&TotalStorageCount=" + totalStorageCount.value;
            url += "&LowerCount=" + lowerCount.value;
            url += "&UpperCount=" + upperCount.value;
            Ajax.requestURL(url, dealSaveUnionEdit);
        }
        else{
            alertMessage("请先选择要编辑的项");
        }
    }
    else{
        alertMessage("请先选择商品");
    }
}
function dealSaveUnionEdit(data) {
    alertMessage(data);
}