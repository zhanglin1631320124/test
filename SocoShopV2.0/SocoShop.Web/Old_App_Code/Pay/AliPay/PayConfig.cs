using System;
using System.Collections.Generic;
using System.Web;
using SocoShop.Common;
using SkyCES.EntLib;

namespace SocoShop.Pay.AliPay
{
    public partial class PayConfig
    {
        private string partner = string.Empty;
        private string sellerEmail = string.Empty;
        private string securityKey = string.Empty;
        private string service = string.Empty;
        /// <summary>
        /// 合作伙伴ID
        /// </summary>
        public string Partner
        {
            get{return this.partner;}
        }
        /// <summary>
        /// 卖家帐号
        /// </summary>
        public string SellerEmail
        {
            get{return this.sellerEmail;}
        }
        /// <summary>
        /// partner账户的支付宝安全校验码
        /// </summary>
        public string SecurityKey
        {
            get{return this.securityKey;}
        }
        /// <summary>
        /// 服务参数
        /// </summary>
        public string Service
        {
            get { return this.service; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public PayConfig()
        {
            using (XmlHelper xh = new XmlHelper(ServerHelper.MapPath("/Plugins/Pay/AliPay/AliPay.Config")))
            {
                this.partner = xh.ReadAttribute("Pay/Partner", "Value");
                this.sellerEmail = xh.ReadAttribute("Pay/SellerEmail", "Value");
                this.securityKey = xh.ReadAttribute("Pay/SecurityKey", "Value");
                this.service = xh.ReadAttribute("Pay/Service", "Value");              
            }
        }
    }
}
