namespace SocoShop.Page
{
    using SocoShop.Common;
    using SocoShop.Page.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public abstract class PluginsBasePage : Page
    {
        protected SocoShop.Page.Controls.Foot Foot = null;
        protected SocoShop.Page.Controls.Head Head = null;
        protected SocoShop.Page.Controls.Top Top = null;

        protected PluginsBasePage()
        {
        }

        private void LoadUserControl(string name)
        {
            PlaceHolder holder = (PlaceHolder) this.Page.FindControl("P" + name);
            if (holder != null)
            {
                string str = name;
                if (str != null)
                {
                    if (!(str == "Head"))
                    {
                        if (str == "Top")
                        {
                            this.Top = (SocoShop.Page.Controls.Top) this.Page.LoadControl("/Plugins/Template/" + ShopConfig.ReadConfigInfo().TemplatePath + "/Controls/Top.ascx");
                            holder.Controls.Add(this.Top);
                        }
                        else if (str == "Foot")
                        {
                            this.Foot = (SocoShop.Page.Controls.Foot) this.Page.LoadControl("/Plugins/Template/" + ShopConfig.ReadConfigInfo().TemplatePath + "/Controls/Foot.ascx");
                            holder.Controls.Add(this.Foot);
                        }
                    }
                    else
                    {
                        this.Head = (SocoShop.Page.Controls.Head) this.Page.LoadControl("/Plugins/Template/" + ShopConfig.ReadConfigInfo().TemplatePath + "/Controls/Head.ascx");
                        holder.Controls.Add(this.Head);
                    }
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.LoadUserControl("Head");
            this.LoadUserControl("Top");
            this.LoadUserControl("Foot");
        }
    }
}

