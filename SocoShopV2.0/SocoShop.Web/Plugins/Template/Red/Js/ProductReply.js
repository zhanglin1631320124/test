var tempPage=1;
 //读取产品回复
function readProductReply(){
    loading("ProductReplyAjax","产品回复");
    var url="/ProductReplyAjax.aspx?CommentID="+commentID+"&Page="+tempPage;
    Ajax.requestURL(url,dealSearchProductReply);
}
function dealSearchProductReply(content){
    o("ProductReplyAjax").innerHTML=content
}
//分页
function goPage(page){
    tempPage=page;
    readProductReply();
}
//提交回复
function postReply(){
    var content=o("content").value;
    if(content==""){
        alertMessage("请输入内容");
        return false;
    }
    var url="/ProductReplyAjax.aspx?Action=Add&ProductID="+productID+"&CommentID="+commentID+"&Content="+content;
    Ajax.requestURL(url,dealPostReply);
}
function dealPostReply(content){
    if(content=="ok"){
        alertMessage("回复成功");
        tempPage=1;
        readProductReply();
    }
    else{
        alertMessage(content);
    }
}
