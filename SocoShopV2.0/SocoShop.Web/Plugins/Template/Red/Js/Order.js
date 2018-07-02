//订单操作
var tempOrderStatus;
var tempOrderID;
function orderOperate(orderID,orderStatus){
    if(window.confirm("确定操作？")){
        tempOrderID=orderID;
        tempOrderStatus=orderStatus;
        var url = "/User/OrderAjax.aspx?Action=OrderOperate&OrderID=" + orderID+"&OrderStatus=" + orderStatus;
        Ajax.requestURL(url, dealOrderOperate);
    }
}
function dealOrderOperate(content){
    if(content!=""){
        alertMessage(content);
    }
    else{
        alertMessage("操作成功");
        if(tempOrderStatus=="1" || tempOrderStatus=="2"){//未付款或者未审核
            o("OrderStatus"+tempOrderID).innerHTML="无效";
        }
        else if(tempOrderStatus=="5"){//已发货
            o("OrderStatus"+tempOrderID).innerHTML="已收货";
        }
        else{
        }
        o("OrderOperate"+tempOrderID).innerHTML="<a href=\"/User/OrderDetail.aspx?ID="+tempOrderID+"\">查看</a>";
    }
}