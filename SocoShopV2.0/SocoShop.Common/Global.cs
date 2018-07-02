namespace SocoShop.Common
{
    using System;
    using System.Configuration;

    public sealed class Global
    {
        public static string CopyRight = "Copyright \x00a9 天易CES 2009";
        private static string dataProvider = string.Empty;
        public static string Description = "<span>SocoShop是天易CES开发组利用将近两年的时间，研究了各种商城开发出来的商城系统，开发的语言是net(C#)。无论在功能、操作人性化、运行效率、安全等级和扩展性等方面都居国内外同类产品领先地位。</span><br/><span>1、功能强大：SocoShop囊括了当今商城系统的大部分的功能，主要分基础设置、商品管理、用户中心、市场营销、订单与统计五大版块，每个版块又做了很细致的深化，满足不同顾客，不同行业的各种不同的需求。</span><br/><span>2、人性化：SocoShop的界面设计和使用习惯都是基于为用户着想这一理念，这使我们的产品达到了极高的易用性。只需轻点鼠标+简单录入即可完成商城管理。 </span><br/><span> 3、高效性：系统采用三层结构，充分利用了缓存技术；对sql语句和相关逻辑的优化；经过多次的反复测试；大大提高了系统的反应速度。<span><br/><span>4、安全：严格的权限控制机制，让您可以精确控制到每一步的操作；操作日志的记录，可以随时查询系统的变化情况；强有力的漏洞检测（Sql注入，地址欺骗等），让系统可以免除安全隐患。</span><br/><span>5、扩展方便：SocoShop系统设计初期就考虑到未来市场和企业潜在的需求，采用了灵活的插件机制，为各种企业应用提供很好的接口，可以无缝整合我们公司即将开发的分销系统，库存系统等各项产品。让您的企业“选择一次，轻松实现企业电子商务化”。</span>";
        public static string ProductName = "Soco商城系统(SocoShop)";
        public static string Version = "V2.0";

        public static string DataProvider
        {
            get
            {
                if (dataProvider == string.Empty) dataProvider = ConfigurationManager.AppSettings["DataProvider"];
                return dataProvider;
            }
        }
    }
}

