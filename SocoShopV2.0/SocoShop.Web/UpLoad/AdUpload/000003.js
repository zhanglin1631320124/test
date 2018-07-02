var myDate=new Date();
var nowDate=myDate.getFullYear()+"-"+(myDate.getMonth()+1)+"-"+(myDate.getDate()+1);
if(compareDate(nowDate,"2011-02-15") && compareDate("2015-02-24",nowDate))
{
document.write('<a href="/Ad.aspx?AdID=3&URL=%2fProduct.aspx" target="_blank" title=简约生活系列休闲鞋><img src="/Upload/AdUpload/20110222114426781.gif"  border="0" width="750" height="80"></a>');
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
