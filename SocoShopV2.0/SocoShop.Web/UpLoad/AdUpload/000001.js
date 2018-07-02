var myDate=new Date();
var nowDate=myDate.getFullYear()+"-"+(myDate.getMonth()+1)+"-"+(myDate.getDate()+1);
if(compareDate(nowDate,"2009-10-01") && compareDate("2012-11-01",nowDate))
{
document.write('<a href="/Ad.aspx?AdID=1&URL=%2fProduct-C7.aspx" target="_blank" title=日用货品大促销><img src="/Upload/AdUpload/20110222112738901.jpg"  border="0" width="750" height="120"></a>');
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
