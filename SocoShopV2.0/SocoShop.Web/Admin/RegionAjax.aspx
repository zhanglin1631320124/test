﻿<%@ Page Language="C#"  AutoEventWireup="True" CodeBehind="RegionAjax.aspx.cs" Inherits="SocoShop.Web.Admin.RegionAjax" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Business" %>
<div style="text-align:center; vertical-align:middle; line-height:24px"><%=name %>下级地区列表</div>
<%foreach(RegionInfo region in regionList)
{ %>
<div  style="width:25%; float:left; line-height:30px;">
	    <span onclick="edit(this,updateRegion,<%=region.ID %>)"><%=region.RegionName%></span>  | <a href='javascript:void(0)' onclick="readRegion(<%=region.ID %>)">管理</a> <a href='javascript:void(0)' onclick="deleteRegion(<%=region.ID %>)">删除</a>
</div>
<%} %>
