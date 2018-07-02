var tempVoteID=0;
function vote(voteID,voteType){
    tempVoteID=voteID;
    var voteObjs=os("name","vote");
    var itemID = "";
    if(voteType==1){
        itemID=getRadioValue(voteObjs);
    }
    else{
        itemID=getCheckboxValue(voteObjs).join(",");
    }
    if(itemID!=""){
        var url="/VoteResult.aspx?Action=Vote&ItemID="+itemID;
        Ajax.requestURL(url,dealVote);
    }
    else{
        alert("请选择投票项");
    }
}
function dealVote(content){
    if(content=="ok"){
        alertMessage("投票成功");
        viewResult();
    }
    else{
        alertMessage(content);
    }
}
function viewResult(){
    var url="/VoteResult.aspx?Action=View";
    Ajax.requestURL(url,dealViewResult);
}
function dealViewResult(content){
    o("VoteAjax").innerHTML=content;
}
function prePareVote(){
    var url="/VoteResult.aspx?Action=Prepare";
    Ajax.requestURL(url,dealViewResult);
}
prePareVote();
