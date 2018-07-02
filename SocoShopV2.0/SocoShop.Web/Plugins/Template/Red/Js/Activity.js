var tempThemeActivityPage=1;
 //读取商品专题
function readThemeActivity(){
    loading("ThemeActivityAjax","商品专题");
    var url="/ThemeActivityAjax.aspx?Page="+tempThemeActivityPage;
    Ajax.requestURL(url,dealReadThemeActivity);
}
function dealReadThemeActivity(content){
    o("ThemeActivityAjax").innerHTML=content
}
//分页
function goPage(page){
    tempThemeActivityPage=page;
    readThemeActivity();
}
readThemeActivity();



var tempGiftPackPage=1;
 //读取特价大礼包
function readGiftPack(){
    loading("GiftPackAjax","特价大礼包");
    var url="/GiftPackAjax.aspx?Page="+tempGiftPackPage;
    Ajax.requestURL(url,dealReadGiftPack);
}
function dealReadGiftPack(content){
    o("GiftPackAjax").innerHTML=content
}
//分页
function goPage(page){
    tempGiftPackPage=page;
    readGiftPack();
}
readGiftPack();

