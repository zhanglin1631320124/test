namespace SocoShop.Web.Admin
{
    using System;

    public class AccessData
    {
        private string _access_token = string.Empty;
        private string _expires_in = string.Empty;
        private string _token_type = string.Empty;

        public string access_token
        {
            get
            {
                return this._access_token;
            }
            set
            {
                this._access_token = value;
            }
        }

        public string expires_in
        {
            get
            {
                return this._expires_in;
            }
            set
            {
                this._expires_in = value;
            }
        }

        public string token_type
        {
            get
            {
                return this._token_type;
            }
            set
            {
                this._token_type = value;
            }
        }
    }
}

