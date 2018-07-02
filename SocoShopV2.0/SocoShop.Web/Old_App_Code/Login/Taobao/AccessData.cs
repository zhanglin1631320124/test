using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SocoShop.Login.Taobao
{
    /// <summary>
    /// 请求淘宝访问令牌返回的数据
    /// </summary>
    public partial class AccessData
    {
        private string _expires_in = string.Empty;
        private string _token_type = string.Empty;
        private string _access_token = string.Empty;

        public string expires_in
        {
            get { return this._expires_in; }
            set { this._expires_in = value; }
        }
        public string token_type
        {
            get { return this._token_type; }
            set { this._token_type = value; }
        }
        public string access_token
        {
            get { return this._access_token; }
            set { this._access_token = value; }
        }
    }
}
