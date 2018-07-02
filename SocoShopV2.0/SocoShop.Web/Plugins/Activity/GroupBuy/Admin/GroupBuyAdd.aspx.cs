using System;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SocoShop.Common;
using SocoShop.Business;
using SocoShop.Entity;
using SkyCES.EntLib;

namespace SocoShop.Web
{
    public partial class GroupBuyAdd : SocoShop.Page.AdminBasePage
	{
		/// <summary>
		/// 页面加载方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				int groupBuyID = RequestHelper.GetQueryString<int>("ID");
				if (groupBuyID != 0)
				{
					CheckAdminPower("ReadGroupBuy", PowerCheckType.Single);
					GroupBuyInfo groupBuy = GroupBuyBLL.ReadGroupBuy(groupBuyID);
					Name.Text = groupBuy.Name;
                    Photo.Text = groupBuy.Photo;
					Description.Value = groupBuy.Description;
                    PrdouctID.Items.Add(new ListItem(ProductBLL.ReadProduct(groupBuy.ProductID).Name, groupBuy.ProductID.ToString()));
					StartDate.Text = groupBuy.StartDate.ToString("yyyy-MM-dd");
					EndDate.Text = groupBuy.EndDate.ToString("yyyy-MM-dd");
                    Price.Text = groupBuy.Price.ToString();
                    MinCount.Text = groupBuy.MinCount.ToString();
                    MaxCount.Text = groupBuy.MaxCount.ToString();
					EachNumber.Text = groupBuy.EachNumber.ToString();
				}
			}
		}
		/// <summary>
		/// 提交按钮点击方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void SubmitButton_Click(object sender, EventArgs e)
		{
			GroupBuyInfo groupBuy =new GroupBuyInfo();
			groupBuy.ID = RequestHelper.GetQueryString<int>("ID");
			groupBuy.Name =Name.Text;
            groupBuy.Photo = Photo.Text;
            groupBuy.Description = Description.Value;
            groupBuy.ProductID = RequestHelper.GetForm<int>("ctl00$ContentPlaceHolder$PrdouctID");
			groupBuy.StartDate = Convert.ToDateTime(StartDate.Text);
			groupBuy.EndDate = Convert.ToDateTime(EndDate.Text).AddDays(1).AddSeconds(-1);
            groupBuy.Price = Convert.ToDecimal(Price.Text);
            groupBuy.MinCount = Convert.ToInt32(MinCount.Text);
            groupBuy.MaxCount = Convert.ToInt32(MaxCount.Text);
			groupBuy.EachNumber = Convert.ToInt32(EachNumber.Text);
			string alertMessage = ShopLanguage.ReadLanguage("AddOK");
			if (groupBuy.ID == int.MinValue)
			{
				CheckAdminPower("AddGroupBuy", PowerCheckType.Single);
				int id =GroupBuyBLL.AddGroupBuy(groupBuy);
				AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("GroupBuy"), id);
			}
			else
			{
				CheckAdminPower("UpdateGroupBuy", PowerCheckType.Single);
				GroupBuyBLL.UpdateGroupBuy(groupBuy);
				AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("GroupBuy"), groupBuy.ID);
				alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
			}
            Alert(alertMessage, RequestHelper.RawUrl);
		}     
	}
}