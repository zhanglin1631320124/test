namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class ProductExport : AdminBasePage
    {

        private ArrayList ConvertToPaiPai(string common, string fast, List<ProductInfo> productList)
        {
            ArrayList list = new ArrayList();
            string[] strArray = new string[] { 
                "id", "商品名称", "出售方式", "商品类目", "店铺类目", "商品数量", "有效期", "定时上架", "新旧程度", "价格", "加价幅度", "省", "市", "运费承担", "平邮", "快递", 
                "购买限制", "付款方式", "有发票", "有保修", "支持财付通", "自动重发", "错误原因", "图片", "商品详情", "上架选项", "皮肤风格", "属性", "诚保", "橱窗"
             };
            list.Add(strArray);
            foreach (ProductInfo info in productList)
            {
                string[] strArray2 = new string[30];
                strArray2[0] = "-1";
                strArray2[1] = info.Name;
                strArray2[2] = "b";
                strArray2[3] = "0";
                strArray2[4] = "";
                if (ShopConfig.ReadConfigInfo().ProductStorageType == 1)
                    strArray2[5] = (info.TotalStorageCount - info.SendCount).ToString();
                else
                    strArray2[5] = info.ImportActualStorageCount.ToString();
                strArray2[6] = "0";
                strArray2[7] = "0";
                strArray2[8] = "1";
                strArray2[9] = info.MarketPrice.ToString();
                strArray2[10] = "0";
                strArray2[11] = "全国";
                strArray2[12] = "全国";
                strArray2[13] = "1";
                strArray2[14] = common.ToString();
                strArray2[15] = fast.ToString();
                strArray2[0x10] = "0";
                strArray2[0x11] = "0";
                strArray2[0x12] = "0";
                strArray2[0x13] = "0";
                strArray2[20] = "1";
                strArray2[0x15] = "1";
                strArray2[0x16] = "";
                strArray2[0x17] = @"images\" + info.Photo.Substring(info.Photo.LastIndexOf('/') + 1, info.Photo.Length - info.Photo.LastIndexOf('/') - 1);
                strArray2[0x18] = info.Summary;
                strArray2[0x19] = "0";
                strArray2[0x1a] = "0";
                strArray2[0x1b] = "";
                strArray2[0x1c] = "0";
                strArray2[0x1d] = "0";
                list.Add(strArray2);
            }
            return list;
        }

        private ArrayList ConvertToTaobao(string classID, string common, string fast, string ems, List<ProductInfo> productList)
        {
            ArrayList list = new ArrayList();
            string[] strArray = new string[] { 
                "宝贝名称", "宝贝类目", "店铺类目", "新旧程度", "省", "城市", "出售方式", "宝贝价格", "加价幅度", "宝贝数量", "有效期", "运费承担", "平邮", "EMS", "快递", "付款方式", 
                "支付宝", "发票", "保修", "自动重发", "放入仓库", "橱窗推荐", "发布时间", "心情故事", "宝贝描述", "宝贝图片", "宝贝属性", "团购价", "最小团购件数", "邮费模版ID", "会员打折", "修改时间", 
                "上传状态", "图片状态", "返点比例", "新图片", "销售属性组合", "用户输入ID串", "用户输入名-值对", "商家编码"
             };
            list.Add(strArray);
            foreach (ProductInfo info in productList)
            {
                string[] strArray2 = new string[40];
                strArray2[0] = info.Name;
                strArray2[1] = classID;
                strArray2[2] = "0";
                strArray2[3] = "5";
                strArray2[4] = "全国";
                strArray2[5] = "全国";
                strArray2[6] = "SaleWay";
                strArray2[7] = info.MarketPrice.ToString();
                strArray2[8] = "0";
                if (ShopConfig.ReadConfigInfo().ProductStorageType == 1)
                    strArray2[9] = (info.TotalStorageCount - info.SendCount).ToString();
                else
                    strArray2[9] = info.ImportActualStorageCount.ToString();
                strArray2[10] = "365天";
                strArray2[11] = "买家";
                strArray2[12] = common;
                strArray2[13] = ems;
                strArray2[14] = fast;
                strArray2[15] = "";
                strArray2[0x10] = "";
                strArray2[0x11] = "0";
                strArray2[0x12] = "0";
                strArray2[0x13] = "1";
                strArray2[20] = "-1";
                strArray2[0x15] = info.IsTop.ToString();
                strArray2[0x16] = info.AddDate.ToString();
                strArray2[0x17] = "";
                strArray2[0x18] = info.Summary;
                strArray2[0x19] = info.Photo;
                strArray2[0x1a] = "property";
                strArray2[0x1b] = "0";
                strArray2[0x1c] = "0";
                strArray2[0x1d] = "";
                strArray2[30] = "0";
                strArray2[0x1f] = info.AddDate.ToString();
                strArray2[0x20] = "";
                strArray2[0x21] = "";
                strArray2[0x22] = "0";
                strArray2[0x23] = info.Photo.Substring(info.Photo.LastIndexOf('/') + 1, info.Photo.Length - info.Photo.LastIndexOf('/') - Path.GetExtension(ServerHelper.MapPath(info.Photo)).Length - 1).ToString() + ":0:0:;";
                strArray2[0x24] = "";
                strArray2[0x25] = "";
                strArray2[0x26] = "";
                strArray2[0x27] = "";
                list.Add(strArray2);
            }
            return list;
        }

        private static void ExecBAT(string path, string fileName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.Arguments = "/k " + path + @"\" + fileName;
            startInfo.WorkingDirectory = path + @"\";
            startInfo.CreateNoWindow = true;
            Process process = Process.Start(startInfo);
        }

        private void ExportFile(string csvFileName, string strDataFormat, ArrayList ls, List<ProductInfo> productList)
        {
            if (ls.Count >= 1)
            {
                bool flag = false;
                if (strDataFormat == "1") flag = true;
                int length = csvFileName.LastIndexOf('\\');
                string path = csvFileName.Substring(0, length);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                StringBuilder builder = new StringBuilder();
                foreach (string[] strArray in ls)
                {
                    for (int i = 0; i <= strArray.Length - 1; i++)
                    {
                        builder.Append(strArray[i] + "\t");
                    }
                    builder.Append("\r\n");
                }
                Encoding unicode = Encoding.Unicode;
                StreamWriter writer = new StreamWriter(csvFileName, false, unicode);
                writer.Write(builder.ToString());
                writer.Flush();
                writer.Close();
                string str2 = "images";
                if (flag)
                {
                    str2 = csvFileName.Substring(length + 1);
                    str2 = str2.Substring(0, str2.Length - 4);
                }
                if (!Directory.Exists(path + @"\" + str2)) Directory.CreateDirectory(path + @"\" + str2);
                foreach (ProductInfo info in productList)
                {
                    try
                    {
                        string destFileName = path + @"\" + str2 + @"\" + info.Photo.Substring(info.Photo.LastIndexOf('/') + 1, info.Photo.Length - info.Photo.LastIndexOf('/') - 1);
                        if (flag) destFileName = destFileName.Substring(0, destFileName.LastIndexOf(".")) + ".tbi";
                        File.Copy(ServerHelper.MapPath(info.Photo), destFileName);
                    }
                    catch
                    {
                    }
                }
                if (flag)
                {
                    string str4 = ServerHelper.MapPath("totbi.bat");
                    if (!File.Exists(str4)) throw new Exception("批处理文件不存在，将图片扩展名改为.tbi失败，语句中止！");
                    File.Copy(str4, path + @"\" + str2 + @"\totai.bat", true);
                    ExecBAT(path + @"\" + str2, "totai.bat");
                }
                int num3 = path.LastIndexOf('\\');
                string str5 = path.Substring(num3 + 1, path.Length - num3 - 1);
                string strZip = Path.GetDirectoryName(path) + @"\" + str5 + ".zip";
                ZipHelper.ZipFile(path, strZip);
                HttpResponse response = HttpContext.Current.Response;
                FileInfo info2 = new FileInfo(strZip);
                response.Clear();
                response.Buffer = true;
                response.AddHeader("content-disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(info2.Name.ToString()));
                response.AddHeader("content-length", info2.Length.ToString());
                response.ContentType = "application/octet-stream";
                response.ContentEncoding = unicode;
                response.WriteFile(strZip);
                response.Flush();
                Directory.Delete(path, true);
                File.Delete(strZip);
                ResponseHelper.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ProductSearchInfo productSearch = new ProductSearchInfo();
            productSearch.Key = RequestHelper.GetQueryString<string>("Key");
            productSearch.ClassID = RequestHelper.GetQueryString<string>("ClassID");
            productSearch.BrandID = RequestHelper.GetQueryString<int>("BrandID");
            productSearch.IsSpecial = RequestHelper.GetQueryString<int>("IsSpecial");
            productSearch.IsNew = RequestHelper.GetQueryString<int>("IsNew");
            productSearch.IsHot = RequestHelper.GetQueryString<int>("IsHot");
            productSearch.IsSale = 1;
            productSearch.IsTop = RequestHelper.GetQueryString<int>("IsTop");
            productSearch.StartAddDate = RequestHelper.GetQueryString<DateTime>("StartAddDate");
            productSearch.EndAddDate = ShopCommon.SearchEndDate(RequestHelper.GetQueryString<DateTime>("EndAddDate"));
            List<ProductInfo> productList = ProductBLL.SearchProductList(productSearch);
            string filePath = string.Empty;
            ArrayList ls = new ArrayList();
            if (this.DataFormat.SelectedValue == "1")
            {
                filePath = @"Taobao\Taobao" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
                ls = this.ConvertToTaobao(this.ClassID.Text, this.Common.Text.Trim(), this.Fast.Text.Trim(), this.EMS.Text.Trim(), productList);
            }
            else
            {
                filePath = @"Paipai\Paipai" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
                ls = this.ConvertToPaiPai(this.Common.Text.Trim(), this.Fast.Text.Trim(), productList);
            }
            this.ExportFile(ServerHelper.MapPath(filePath), this.DataFormat.SelectedValue, ls, productList);
        }
    }
}

