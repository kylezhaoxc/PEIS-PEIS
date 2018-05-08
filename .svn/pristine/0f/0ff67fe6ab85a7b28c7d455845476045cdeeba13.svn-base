using PEIS.Base;
using PEIS.Common;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace PEIS.Web.Ajax
{
	public class AjaxBase : BasePage
	{
		public string ErrorMessage = string.Empty;

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public void TestOutMessage()
		{
			this.OutPutMessage("This is the Test Info ... ");
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ErrorMessage = "error";
			string @string = base.GetString("action");
			MethodInfo method = base.GetType().GetMethod(@string);
			try
			{
				method.Invoke(this, null);
			}
			catch
			{
				this.OutPutMessage(this.ErrorMessage);
			}
		}

		public void UserLogOut()
		{
			try
			{
				this.Session["UserID"] = "0";
				BasePage.ClearCookie("");
				this.OutPutMessage("1");
			}
			catch (Exception ex)
			{
				this.OutPutMessage("0");
			}
		}

		public void GetUploadImageServerSystemTime()
		{
			string text = base.GetString("WebServiceUrl");
			text = Input.URLDecode(text);
			string str = "GetSystemTime";
			try
			{
				string text2 = this.AspNetPost(text, "action=" + str);
				Log4J.Instance.Debug("测试图片服务器函数： GetUploadImageServerSystemTime ,Web URL：" + text + "，结果：" + text2);
				this.OutPutMessage(text2);
			}
			catch (Exception ex)
			{
				Log4J.Instance.Debug("测试图片服务器函数： GetUploadImageServerSystemTime ,Web URL：" + text + "，结果：" + ex.Message);
			}
		}

		public void ClearSectionImageDir()
		{
			string text = base.GetString("WebServiceUrl");
			text = Input.URLDecode(text);
			string text2 = "ClearSectionImgageDir";
			int @int = base.GetInt("ID_Section", 0);
			long int2 = base.GetInt64("ID_Customer", 0L);
			string text3 = @int.ToString() + int2.ToString();
			text3 = Secret.DES.Encrypt(text3);
			text3 = Secret.DES.Encrypt(text3);
			try
			{
				string param = string.Concat(new string[]
				{
					"action=",
					text2,
					"&ID_Section=",
					@int.ToString(),
					"&ID_Customer=",
					int2.ToString(),
					"&AuthorizationCode=",
					Input.URLEncode(text3.ToString())
				});
				string text4 = this.AspNetPost(text, param);
				Log4J.Instance.Debug(string.Concat(new object[]
				{
					Public.GetClientIP(),
					"清除科室图片目录下面的图片【成功】,科室：",
					@int,
					",体检号：",
					int2,
					",",
					this.LoginUserModel.UserName,
					",Web URL：",
					text,
					"，结果：",
					text4
				}));
				this.OutPutMessage(text4);
			}
			catch (Exception ex)
			{
				Log4J.Instance.Debug(string.Concat(new object[]
				{
					Public.GetClientIP(),
					"清除科室图片目录下面的图片【失败】,科室：",
					@int,
					",体检号：",
					int2,
					",",
					this.LoginUserModel.UserName,
					",Web URL：",
					text,
					"，结果：",
					ex.Message
				}));
			}
		}

		public void WebServiceUploadImage()
		{
			string text = base.GetString("WebServiceUrl");
			string @string = base.GetString("OrgImageFileName");
			text = Input.URLDecode(text);
			string text2 = base.GetString("ImageBase64String");
			text2 = base.Server.UrlEncode(text2);
			int @int = base.GetInt("ID_Section", 0);
			long int2 = base.GetInt64("ID_Customer", 0L);
			string text3 = "UploadImage";
			try
			{
				string param = string.Concat(new string[]
				{
					"action=",
					text3,
					"&ID_Section=",
					@int.ToString(),
					"&ID_Customer=",
					int2.ToString(),
					"&OrgImageFileName=",
					@string.ToString(),
					"&ImageBase64String=",
					text2
				});
				string text4 = this.AspNetPost(text, param);
				Log4J.Instance.Debug(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",科室：",
					@int,
					",体检号：",
					int2,
					",",
					this.LoginUserModel.UserName,
					",Web URL：",
					text,
					"，结果：",
					text4
				}));
				this.OutPutMessage(text4);
			}
			catch (Exception ex)
			{
				Log4J.Instance.Debug(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",科室：",
					@int,
					",体检号：",
					int2,
					",",
					this.LoginUserModel.UserName,
					",Web URL：",
					text,
					" 错误信息：",
					ex.Message
				}));
				this.OutPutMessage("");
			}
		}

		protected string AspNetPost(string url, string param)
		{
			Encoding encoding = Encoding.GetEncoding("utf-8");
			byte[] bytes = Encoding.ASCII.GetBytes(param);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
			httpWebRequest.ContentLength = (long)bytes.Length;
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
				requestStream.Close();
			}
			string text = string.Empty;
			using (WebResponse response = httpWebRequest.GetResponse())
			{
				Stream responseStream = response.GetResponseStream();
				Encoding encoding2 = Encoding.GetEncoding("utf-8");
				StreamReader streamReader = new StreamReader(responseStream, encoding2);
				char[] array = new char[256];
				int i = streamReader.Read(array, 0, 256);
				string str = string.Empty;
				while (i > 0)
				{
					str = new string(array, 0, i);
					text += str;
					i = streamReader.Read(array, 0, 256);
				}
			}
			return text;
		}
	}
}
