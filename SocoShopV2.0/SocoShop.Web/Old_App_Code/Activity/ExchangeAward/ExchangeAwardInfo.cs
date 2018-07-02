using System;
using System.Collections.Generic;
using System.Text;

namespace SocoShop.Web
{
    public sealed class ExchangeAwardInfo
    {
        private string name = string.Empty;
        private string content = string.Empty;
        private string porudctIDList;
        private string pointList;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        public string PorudctIDList
        {
            get { return this.porudctIDList; }
            set { this.porudctIDList = value; }
        }
        public string PointList
        {
            get { return this.pointList; }
            set { this.pointList = value; }
        }
    }
}