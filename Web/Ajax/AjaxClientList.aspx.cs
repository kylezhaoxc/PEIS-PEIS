using PEIS.Base;
using PEIS.Common;
using PEIS.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace PEIS.Web.Ajax
{
	public class AjaxClientList : BasePage
	{
		private string FilePath = HttpContext.Current.Request.PhysicalApplicationPath;

		public string ErrorMessage = string.Empty;

		public void List()
		{
			ServerUtils serverUtils = ServerUtils.Instance();
			List<ServerInfo> servers = serverUtils.GetServers();
			this.ResponseJson(new
			{
				servers = servers,
				local = serverUtils.GetLocation()
			});
		}

		public void Update()
		{
			string guid = base.Request.Form["guid"];
			string text = base.Request.Form["name"];
			string text2 = base.Request.Form["begin_ip"];
			string text3 = base.Request.Form["end_ip"];
			string s = base.Request.Form["code"];
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text3))
			{
				this.ResponseJson(new
				{
					state = 0,
					msg = "Ip服务器信息不完整！"
				});
			}
			else
			{
				text = new Regex("[<>'\"]").Replace(text, "");
				int code;
				if (!int.TryParse(s, out code))
				{
					code = 0;
				}
				ServerUtils serverUtils = ServerUtils.Instance();
				string msg;
				bool state = serverUtils.Update(new ServerInfo
				{
					guid = guid,
					code = code,
					name = text,
					begin_ip = text2,
					end_ip = text3
				}, out msg);
				this.ResponseJson(new
				{
					state,
					msg
				});
			}
		}

		public void Delete()
		{
			string guid = base.Request.Form["guid"];
			ServerUtils serverUtils = ServerUtils.Instance();
			string msg;
			bool state = serverUtils.Delete(guid, out msg);
			this.ResponseJson(new
			{
				state,
				msg
			});
		}

		public void ResponseJson(object obj)
		{
			base.Response.Clear();
			base.Response.Expires = -1;
			base.Response.ContentType = "application/json";
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			string s = javaScriptSerializer.Serialize(obj);
			base.Response.Write(s);
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

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public void TestOutMessage()
		{
			this.OutPutMessage("This is the Test Info ... ");
		}

		public void GetClientList()
		{
			string clientList = ClientListManagement.Instance().GetClientList();
			this.OutPutMessage(clientList);
		}

		public void LogOutCustomer()
		{
			string gUID = base.GetString("GUID").Trim().TrimEnd(new char[]
			{
				','
			});
			string msg = ClientListManagement.Instance().LogOutCustomer(gUID);
			this.OutPutMessage(msg);
		}

		public void GetClientInfo()
		{
			string clientInfo = ClientListManagement.Instance().GetClientInfo(this.Session.SessionID);
			this.OutPutMessage(clientInfo);
		}

		public void SendMessageToCustomer()
		{
			string gUID = base.GetString("GUID").Trim().TrimEnd(new char[]
			{
				','
			});
			string msg = base.GetString("Msg").Trim();
			string msg2 = ClientListManagement.Instance().SendMessageToCustomer(gUID, msg);
			this.OutPutMessage(msg2);
		}
	}
}
