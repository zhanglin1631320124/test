<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ProductAjax.aspx.cs" Inherits="SocoShop.Web.Admin.ProductAjax" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
 <select name='<%=NamePrefix %><%=controlName %>' id='<%=IDPrefix%><%=controlName %>' <%=cssContent %> <%=dobuleClickContent %>>
 <%if ("SearchProductAccessory,SearchRelationProduct,SearchProductByName".IndexOf(action) > -1)
   {
        foreach (ProductInfo product in productList){ %>
        <option value="<%=product.ID %>"><%=product.Name %></option>
        <%}
    }
   else if (action == "SearchRelationArticle")
   {
       foreach (ArticleInfo article in articleList){ %>
    <option value="<%=article.ID %>"><%=article.Title%></option>
      <%}
   } 
    else if (action == "SearchUser")
   {
       foreach (UserInfo user in userList){ %>
    <option value="<%=user.ID %>|<%=user.UserName%>"><%=user.UserName%></option>
      <%}
   }
   %>
</select>