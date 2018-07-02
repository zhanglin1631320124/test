namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;

    public class ActiveUser : CommonBasePage
    {
        protected string result = string.Empty;

        protected override void PageLoad()
        {
            base.PageLoad();
            string queryString = RequestHelper.GetQueryString<string>("CheckCode");
            if (queryString != string.Empty)
            {
                string str2 = StringHelper.Decode(queryString, ShopConfig.ReadConfigInfo().SecureKey);
                if (str2.IndexOf('|') > 0)
                {
                    int id = Convert.ToInt32(str2.Split(new char[] { '|' })[0]);
                    string str3 = str2.Split(new char[] { '|' })[1];
                    string str4 = str2.Split(new char[] { '|' })[2];
                    UserInfo info = UserBLL.ReadUser(id);
                    if (info.ID > 0 && info.UserName == str4 && info.Email == str3)
                    {
                        if (info.Status == 1)
                        {
                            UserBLL.ChangeUserStatus(info.ID.ToString(), 2);
                            this.result = "恭喜您，成功激活用户";
                        }
                        else
                            this.result = "该用户已经激活了";
                    }
                    else
                        this.result = "错误的激活信息";
                }
                else
                    this.result = "错误的激活信息";
            }
            else
                this.result = "激活信息格式错误";
        }
    }
}

