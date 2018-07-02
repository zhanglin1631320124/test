<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="AttributeRecordAjax.aspx.cs"  Inherits="SocoShop.Web.Admin.AttributeRecordAjax" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<% foreach (AttributeInfo attribute in attributeList)
   {%>
<ul>
    <li class="left"><%=attribute.Name%>：</li>
    <li class="right">
        <%
            switch (attribute.InputType)
            {
                case (int)InputType.Text:
        %>
        <input name="<%=attribute.ID %>Value" class="input" type="text" value="<%=attribute.AttributeRecord.Value %>"  style="width:200px"/>
        <%
            break;
            case (int)InputType.KeyWord:%>
        <input name="<%=attribute.ID %>Value" class="input" type="text" value="<%=attribute.AttributeRecord.Value %>" style="width:200px" id="<%=attribute.ID %>Value" />
        <%foreach (string inputValue in attribute.InputValue.Split(',')){%><span onclick="selectKeyword(<%=attribute.ID %>,'<%=inputValue%>')" style="cursor:pointer"><%=inputValue%></span> <%} %>
        <%
            break;
                    case (int)InputType.Select:
        %>
        <select name="<%=attribute.ID %>Value" style="width: 200px;">
            <%foreach (string inputValue in attribute.InputValue.Split(','))
              {%>
            <option value="<%=inputValue%>" <%if(attribute.AttributeRecord.Value==inputValue){%>selected="selected"<%} %>>
                <%=inputValue%></option>
            <%} %>
        </select>
        <%    
            break;
            case (int)InputType.Textarea:%>
        <textarea name="<%=attribute.ID %>Value" class="input" style="width:400px; height:50px"><%=attribute.AttributeRecord.Value%></textarea>
        <%
            break;
            case (int)InputType.Radio:%>
             <%foreach (string inputValue in attribute.InputValue.Split(','))
               {%>
        <input name="<%=attribute.ID %>Value" type="radio" <%if(attribute.AttributeRecord.Value==inputValue){%>checked="checked"<%} %> value="<%=inputValue %>" /> <%=inputValue %> 
        <%} %>
        <%
            break;
            case (int)InputType.CheckBox:%>
             <%foreach (string inputValue in attribute.InputValue.Split(','))
               {%>
        <input name="<%=attribute.ID %>Value" type="checkbox" <%if((","+attribute.AttributeRecord.Value+",").IndexOf(","+inputValue+",")>-1){%> checked="checked"<%} %>  value="<%=inputValue %>" /> <%=inputValue %> 
        <%} %>
        <%
            break;
            default:
            break;
        }%>
    </li>
</ul>
<%    }    %>