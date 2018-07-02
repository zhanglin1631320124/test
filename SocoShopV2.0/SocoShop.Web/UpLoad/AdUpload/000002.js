var myDate=new Date();
var nowDate=myDate.getFullYear()+"-"+(myDate.getMonth()+1)+"-"+(myDate.getDate()+1);
if(compareDate(nowDate,"2011-02-01") && compareDate("2013-02-23",nowDate))
{
document.write('<a href="/Ad.aspx?AdID=2&URL=http%3a%2f%2fwww.skyces.com%2fShop%2fDown.aspx" target="_blank" title=史上最强.net商城系统震撼发布><img src="/Upload/AdUpload/201110/fb34b41f-4c69-4d11-90c2-95d7990f172c.jpg"  border="0" width="200" height="200"></a>');
}
else
{
document.write('广告过期');
}
function compareDate(dateOne,dateTwo)
{ 
var monthOne = dateOne.substring(5,dateOne.lastIndexOf ("-"))
var dayOne = dateOne.substring(dateOne.length,dateOne.lastIndexOf ("-")+1)
var yearOne = dateOne.substring(0,dateOne.indexOf ("-"))
var monthTwo = dateTwo.substring(5,dateTwo.lastIndexOf ("-"))
var dayTwo = dateTwo.substring(dateTwo.length,dateTwo.lastIndexOf ("-")+1)
var yearTwo = dateTwo.substring(0,dateTwo.indexOf ("-"))
if (Date.parse(monthOne+" / "+dayOne+" / "+yearOne) >Date.parse(monthTwo+"/"+dayTwo+"/"+yearTwo))
{
return true;
}
else
{
return false;
}
}
