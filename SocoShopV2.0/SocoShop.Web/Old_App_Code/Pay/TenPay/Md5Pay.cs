using System;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using  System.Collections.Specialized;
using System.Configuration;

namespace SocoShop.Pay.TenPay
{
	/// <summary>
	/// ��ɹ�������
	/// 1:֧������
	/// 2:֧���������
	/// 3:��ѯ��������.
	/// 4:��ѯ�����������.
	/// </summary>
	public partial class Md5Pay
	{
		/// <summary>
		/// �̻��ţ��滻Ϊ���ѵ��̻��ţ�
		/// </summary>
		private string bargainor_id = "";
		public string Bargainor_id
		{
			get{return bargainor_id;}
			set{bargainor_id = value;}
		}

		/// <summary>
		/// �̻�KEY���滻Ϊ���ѵ�KEY��
		/// </summary>
		private string key = "";
		public string Key
		{
			get{return key;}
			set{key = value;}
		}

		/// <summary>
		/// �Ƹ�֧ͨ������URL
		/// </summary>
		private string paygateurl ="https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi";
		public string Paygateurl
		{
			get{return paygateurl;}
			set{paygateurl = value;}
		}
	
		/// <summary>
		/// �Ƹ�ͨ��ѯ����URL
		/// </summary>
		private string querygateurl = "http://portal.tenpay.com/cfbiportal/cgi-bin/cfbiqueryorder.cgi";
		public string Querygateurl
		{
			get{return querygateurl;}
			set{querygateurl = value;}
		}

		/// <summary>
		/// ֧���������ҳ��
		/// �Ƽ�ʹ��ip��ַ�ķ�ʽ(�255���ַ�)
		/// ����ʹ����Ե�ַ������,��ʹ��ǰƴװȫ��ַ����.�������㲿��.
		/// </summary>
        private string return_url = "http://" + HttpContext.Current.Request.ServerVariables["Http_Host"] + "/Plugins/Pay/TenPay/Return.aspx";
		public string Return_url
		{
			get{return return_url;}
			set{return_url = value;}
		}

		/// <summary>
		/// ��ѯ�������ҳ��
		/// �Ƽ�ʹ��ip��ַ�ķ�ʽ(�255���ַ�)
		/// ����ʹ����Ե�ַ������,��ʹ��ǰƴװȫ��ַ����.�������㲿��.
		/// </summary>
        private string queryreturn_url = "";
		public string Queryreturn_url
		{
			get{return queryreturn_url;}
			set{queryreturn_url = value;}
		}

		/// <summary>
		/// ֧������.1
		/// </summary>
		private const int cmdno = 1;
		
		/// <summary>
		/// ��ѯ����.2
		/// </summary>
		private const int querycmdno = 2;

		/// <summary>
		/// ��������,������ֻ֧�� 1:�����
		/// </summary>
		private int fee_type = 1;

		private string date;

		#region �����ֶ�����,��ʽΪyyyyMMdd		

		/// <summary>
		/// ֧������,yyyyMMdd
		/// </summary>
		public string Date
		{
			get
			{
				if(date == null)
				{
					date = DateTime.Now.ToString("yyyyMMdd");					
				}

				return date;
			}
			set
			{
				if(value == null || value.Trim().Length != 8)
				{
					date = DateTime.Now.ToString("yyyyMMdd");
				}
				else
				{
					try
					{
						string strTmp = value.Trim();
						date = DateTime.Parse(strTmp.Substring(0,4) + "-" + strTmp.Substring(4,2) + "-" 
							+ strTmp.Substring(6,2)).ToString("yyyyMMdd");
					}
					catch
					{
						date = DateTime.Now.ToString("yyyyMMdd");
					}

				}
			}
		}

		#endregion

		private string sp_billno = "";
		/// <summary>
		/// �̻�������,10λ������
		/// </summary>
		public string Sp_billno
		{
			get{return sp_billno;}
			set{sp_billno = value;}
		}

		private long total_fee = 0;

		/// <summary>
		/// �������,�Է�Ϊ��λ
		/// </summary>
		public long Total_fee
		{
			get{return total_fee;}
			set{total_fee = value;}
		}


		/// <summary>
		/// ȡʱ��������漴��,�滻���׵����еĺ�10λ��ˮ��
		/// �Ƹ�ͨ�Ľ��׵����в�������ַ����ֵ��ַ�
		/// </summary>
		/// <returns></returns>
		public UInt32 UnixStamp()
		{
			TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}

		private string transaction_id ="";
		/// <summary>
		/// ���׵���,�̻���(10)+֧������(8)+�̻�������(10,����Ļ���0)=28λ.
		/// </summary>
		public string Transaction_id
		{
			get
			{
				return transaction_id;
			}
			set
			{
				transaction_id = value;
			}
		}

		private string desc = "";
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public string Desc
		{
			get{return UrlDecode(desc);}
			set{desc = UrlEncode(value);}
		}

		private string attach = "";
		/// <summary>
		/// ָ���ʶ,ÿ��ָ���������ֶ�,�Ƹ�ͨ�ڴ�����ɺ��ԭ������.
		/// </summary>
		public string Attach
		{
			get{return UrlDecode(attach);}
			set{attach = UrlEncode(value);}
		}
		
		private string purchaser_id = "";
		/// <summary>
		/// ��ҲƸ�ͨ�ʺ�
		/// </summary>
		public string Purchaser_id
		{
			get{return purchaser_id;}
			set{purchaser_id=value;}
		}

		private string spbill_create_ip= "";
		/// <summary>
		/// �û�ip
		/// </summary>
		public string Spbill_create_ip
		{
			get{return spbill_create_ip;}
			set{spbill_create_ip = value;}
		}

		private int pay_result;
		/// <summary>
		///  ֧���Y��
		/// </summary>
		public const int PAYOK = 0;
		public const int PAYSPERROR = 1;
		public const int PAYMD5ERROR = 2;
		public const int PAYERROR = 3;
		/// <summary>
		/// ֧����� 
		/// 0:֧���ɹ�.
		/// 1:�̻��Ŵ�.
		/// 2:ǩ������.
		/// 3:֧��ʧ��.
		/// </summary>
		public int Pay_Result
		{
			get{return pay_result;}
		}


		/// <summary>
		/// ֧�����˵���ֶ�
		/// </summary>
		public string PayResultStr
		{
			get
			{
				switch(pay_result)
				{
					case PAYOK :
						return "֧���ɹ�";
					case PAYSPERROR :
						return "�̻��Ŵ�";
					case PAYMD5ERROR :
						return "ǩ������";
					case PAYERROR :
						return "֧��ʧ��";
					default :
                        return "δ֪����(" + pay_result +")";
				}
			}
		}

		private string payerrmsg = "";

		/// <summary>
		/// ���Ϊ֧��ʧ��ʱ,�Ƹ�ͨ���صĴ�����Ϣ
		/// </summary>
		public string PayErrMsg
		{
			get{return payerrmsg;}
		}

		/// <summary>
		/// ���ַ�������URL����
		/// </summary>
		/// <param name="instr">��������ַ���</param>
		/// <returns>������</returns>
		private static string UrlEncode(string instr)
		{
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				return instr.Replace("%","%25").Replace("=","%3d").Replace("&","%26").
					Replace("\"","%22").Replace("?","%3f").Replace("'","%27").Replace(" ","%20");
			}
		}

		/// <summary>
		/// ���ַ�������URL����
		/// </summary>
		/// <param name="instr">��������ַ���</param>
		/// <returns>������</returns>
		private static string UrlDecode(string instr)
		{
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				return instr.Replace("%3d","=").Replace("%26","&").Replace("%22","\"").Replace("%3f","?")
					.Replace("%27","'").Replace("%20"," ").Replace("%25","%");
			}
		}

		/// <summary>
		/// ��ȡ��д��MD5ǩ�����
		/// </summary>
		/// <param name="encypStr"></param>
		/// <returns></returns>
		private static string GetMD5(string encypStr)
		{
			string retStr;
			MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

			//����md5����
			byte[] inputBye;
			byte[] outputBye;

			//ʹ��GB2312���뷽ʽ���ַ���ת��Ϊ�ֽ����飮
			inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
			
			outputBye = m5.ComputeHash(inputBye);

			retStr = System.BitConverter.ToString(outputBye);
			retStr = retStr.Replace("-", "").ToUpper();
			return retStr;
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		public Md5Pay()
		{

		}
 


		/// <summary>
		/// ��ȡ֧��ǩ��
		/// </summary>
		/// <returns>���ݲ����õ�ǩ��</returns>
		private string GetPaySign()
		{
			string sign_text = "cmdno=" + cmdno + "&date=" + Date + "&bargainor_id=" + bargainor_id 
				+ "&transaction_id=" + Transaction_id + "&sp_billno=" + sp_billno +  "&total_fee=" 
				+ total_fee + "&fee_type=" + fee_type + "&return_url=" + return_url +  "&attach=" + Attach;
			if(spbill_create_ip!="")
			{
				sign_text += "&spbill_create_ip=" + spbill_create_ip;
			}
			sign_text += "&key=" + key;

			return GetMD5(sign_text);
		}

		/// <summary>
		/// ��ȡ֧�����ǩ��
		/// </summary>
		/// <returns>���ݲ����õ�ǩ��</returns>
		private string GetPayResultSign()
		{
			string sign_text = "cmdno=" + cmdno + "&pay_result=" + pay_result + "&date=" + date + "&transaction_id=" + transaction_id 
				+ "&sp_billno=" + sp_billno + "&total_fee=" + total_fee + "&fee_type=" + fee_type + "&attach=" + attach + "&key=" + key;

			return GetMD5(sign_text);
		}


		/// <summary>
		/// ��ȡ֧��ҳ��URL
		/// </summary>
		/// <param name="url">�������������,��֧��URL,����������ؼ�,�Ǵ�����Ϣ</param>
		/// <returns>����ִ���Ƿ�ɹ�</returns>
		public bool GetPayUrl(out string url)
		{
			try
			{				
				string sign = GetPaySign();
				
				url = paygateurl +"?cmdno=" + cmdno + "&date=" + date + "&bank_type=0&desc="+ desc +"&purchaser_id=&bargainor_id=" 
					+ bargainor_id + "&transaction_id=" + transaction_id + "&sp_billno=" + sp_billno + "&total_fee=" + total_fee
					+ "&fee_type=" + fee_type + "&return_url=" + return_url + "&attach=" + attach;
				if(spbill_create_ip!="")
				{
					url += "&spbill_create_ip=" + spbill_create_ip;
				}
				url += "&sign=" + sign;

				return true;
			}
			catch(Exception err)
			{
				url = "����URLʱ����,������Ϣ:" + err.Message;
				return false;
			}
		}

		/// <summary>
		/// ��֧�����ҳ���URL��������л�ȡ�����Ϣ
		/// </summary>
		/// <param name="querystring">֧�����ҳ���URL�������</param>
		/// <param name="errmsg">����ִ�в��ɹ��Ļ�,���ش�����Ϣ</param>
		/// <returns>����ִ���Ƿ�ɹ�</returns>
		public bool GetPayValueFromUrl(NameValueCollection querystring, out string errmsg)
		{
			//���URL������������
/*
?cmdno=1&pay_result=0&pay_info=OK&date=20070423&bargainor_id=1201143001&transaction_id=1201143001200704230000000013
&sp_billno=13&total_fee=1&fee_type=1&attach=%D5%E2%CA%C7%D2%BB%B8%F6%B2%E2%CA%D4%BD%BB%D2%D7%B5%A5				
&sign=ADD7475F2CAFA793A3FB35051869E301
*/

			#region ���в���У��

			if(querystring == null || querystring.Count == 0)
			{
				errmsg = "����Ϊ��";
				return false;
			}

			if(querystring["cmdno"] == null || querystring["cmdno"].ToString().Trim() != cmdno.ToString())
			{
				errmsg = "û��cmdno������cmdno��������ȷ";
				return false;
			}

			if(querystring["pay_result"] == null)
			{
				errmsg = "û��pay_result����";
				return false;
			}

			if(querystring["date"] == null)
			{
				errmsg = "û��date����";
				return false;
			}

			if(querystring["pay_info"] == null)
			{
				errmsg = "û��pay_info����";
				return false;
			}

			if(querystring["bargainor_id"] == null)
			{
				errmsg = "û��bargainor_id����";
				return false;
			}

			if(querystring["transaction_id"] == null)
			{
				errmsg = "û��transaction_id����";
				return false;
			}

			if(querystring["sp_billno"] == null)
			{
				errmsg = "û��sp_billno����";
				return false;
			}

			if(querystring["total_fee"] == null)
			{
				errmsg = "û��total_fee����";
				return false;
			}

			if(querystring["fee_type"] == null)
			{
				errmsg = "û��fee_type����";
				return false;
			}

			if(querystring["attach"] == null)
			{
				errmsg = "û��attach����";
				return false;
			}

			if(querystring["sign"] == null)
			{
				errmsg = "û��sign����";
				return false;
			}

			#endregion

			errmsg = "";

			try
			{
				pay_result = Int32.Parse(querystring["pay_result"].Trim());
                
				payerrmsg = UrlDecode(querystring["pay_info"].Trim());
				Date = querystring["date"];
				transaction_id = querystring["transaction_id"];
				sp_billno = querystring["sp_billno"];
				total_fee = long.Parse(querystring["total_fee"]);
				fee_type = Int32.Parse(querystring["fee_type"]);
				attach = querystring["attach"];

				if(querystring["bargainor_id"] != bargainor_id)
				{
					pay_result = PAYSPERROR;
					return true;
				}

				string strsign = querystring["sign"];
				string sign = GetPayResultSign();

				if(sign != strsign)
				{
					pay_result = PAYMD5ERROR;
				}
				
				return true;
			}
			catch(Exception err)
			{
				errmsg = "������������:" + err.Message;
				return false;
			}
		}


		/// <summary>
		/// ��ѯ�������ò�������
		/// </summary>
		/// <param name="adate">֧������</param>
		/// <param name="atransaction_id">���׵���</param>
		/// <param name="asp_billno">�̻�������</param>
		/// <param name="aattach">ָ���ʶ����</param>
		public void InitQueryParam(string adate, string atransaction_id, string asp_billno,string aattach)
		{
			Date = adate;
			Sp_billno = asp_billno;
			Transaction_id = atransaction_id;
			Attach = aattach;
		}

		/// <summary>
		/// ��ȡ��ѯǩ��
		/// </summary>
		/// <returns>���ݲ����õ�ǩ��</returns>
		private string GetQuerySign()
		{
			string sign_text = "cmdno=" + querycmdno + "&date=" + date + "&bargainor_id=" + bargainor_id + "&transaction_id=" 
				+ transaction_id + "&sp_billno=" + sp_billno + "&return_url=" + queryreturn_url + "&attach=" + Attach + "&key=" + key;

			return GetMD5(sign_text);
		}

		/// <summary>
		/// ��ȡ��ѯ���ǩ��
		/// </summary>
		/// <returns>���ݲ����õ�ǩ��</returns>
		private string GetQueryResultSign()
		{
			string sign_text = "cmdno=" + querycmdno + "&pay_result=" + pay_result + "&date=" + date + "&transaction_id=" + transaction_id 
				+ "&sp_billno=" + sp_billno + "&total_fee=" + total_fee + "&fee_type=" + fee_type + "&attach=" + attach + "&key=" + key;

			return GetMD5(sign_text);
		}

		/// <summary>
		/// ��ȡ��ѯҳ��URL
		/// </summary>
		/// <param name="url">�������������,�ǲ�ѯURL,����������ؼ�,�Ǵ�����Ϣ</param>
		/// <returns>����ִ���Ƿ�ɹ�</returns>
		public bool GetQueryUrl(out string url)
		{

			try
			{				
				string sign = GetQuerySign();
				
				url = querygateurl +"?cmdno=" + querycmdno + "&date=" + date + "&bargainor_id=" + bargainor_id + "&transaction_id=" 
					+ transaction_id + "&sp_billno=" + sp_billno + "&return_url=" + queryreturn_url + "&attach=" + attach + "&sign=" + sign;

				return true;
			}
			catch(Exception err)
			{
				url = "����URLʱ����,������Ϣ:" + err.Message;
				return false;
			}
		}

		/// <summary>
		/// �Ӳ�ѯ���ҳ���URL��������л�ȡ�����Ϣ
		/// </summary>
		/// <param name="querystring">��ѯ���ҳ���URL�������</param>
		/// <param name="errmsg">����ִ�в��ɹ��Ļ�,���ش�����Ϣ</param>
		/// <returns>����ִ���Ƿ�ɹ�</returns>
		public bool GetQueryValueFromUrl(NameValueCollection querystring, out string errmsg)
		{
			//���URL������������
			/*
			?cmdno=2&pay_result=0&pay_info=OK&date=20070423&bargainor_id=1201143001&transaction_id=1201143001200704230000000001
			&sp_billno=1&total_fee=1&fee_type=1&attach=test11&sign=E80632F587263EF0AFA4A8EEC84A467C&PcacheTime=353851
			*/

			#region ���в���У��

			if(querystring == null || querystring.Count == 0)
			{
				errmsg = "����Ϊ��";
				return false;
			}

			if(querystring["cmdno"] == null || querystring["cmdno"].ToString().Trim() != querycmdno.ToString())
			{
				errmsg = "û��cmdno������cmdno��������ȷ";
				return false;
			}

			if(querystring["pay_result"] == null)
			{
				errmsg = "û��pay_result����";
				return false;
			}

			if(querystring["date"] == null)
			{
				errmsg = "û��date����";
				return false;
			}

			if(querystring["pay_info"] == null)
			{
				errmsg = "û��pay_info����";
				return false;
			}

			if(querystring["bargainor_id"] == null)
			{
				errmsg = "û��bargainor_id����";
				return false;
			}

			if(querystring["transaction_id"] == null)
			{
				errmsg = "û��transaction_id����";
				return false;
			}

			if(querystring["sp_billno"] == null)
			{
				errmsg = "û��sp_billno����";
				return false;
			}

			if(querystring["total_fee"] == null)
			{
				errmsg = "û��total_fee����";
				return false;
			}

			if(querystring["fee_type"] == null)
			{
				errmsg = "û��fee_type����";
				return false;
			}

			if(querystring["attach"] == null)
			{
				errmsg = "û��attach����";
				return false;
			}

			if(querystring["sign"] == null)
			{
				errmsg = "û��sign����";
				return false;
			}

			#endregion

			errmsg = "";

			try
			{
				pay_result = Int32.Parse(querystring["pay_result"].Trim());
                
				payerrmsg = UrlDecode(querystring["pay_info"].Trim());
				Date = querystring["date"];
				transaction_id = querystring["transaction_id"];
				sp_billno = querystring["sp_billno"];
				total_fee = long.Parse(querystring["total_fee"]);
				fee_type = Int32.Parse(querystring["fee_type"]);
				attach = querystring["attach"];

				if(querystring["bargainor_id"] != bargainor_id)
				{
					pay_result = PAYSPERROR;
					return true;
				}

				string strsign = querystring["sign"];
				string sign = GetQueryResultSign();

				if(sign != strsign)
				{
					pay_result = PAYMD5ERROR;
				}
				
				return true;
			}
			catch(Exception err)
			{
				errmsg = "������������:" + err.Message;
				return false;
			}
		}
	}
}
