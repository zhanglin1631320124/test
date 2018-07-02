var myDate=new Date();
var nowDate=myDate.getFullYear()+"-"+(myDate.getMonth()+1)+"-"+(myDate.getDate()+1);
if(compareDate(nowDate,"2011-04-26") && compareDate("2015-04-28",nowDate))
{
document.write('<a href="/Ad.aspx?AdID=4&URL=http%3a%2f%2fwww.skyces.com%2fShop%2fExperience.aspx" target="_blank" title=首页横幅广告><img src="/Upload/AdUpload/20110427125601602.jpg"  border="0" width="960" height="60"></a>');
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
