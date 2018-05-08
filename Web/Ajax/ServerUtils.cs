using PEIS.Model;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;

namespace PEIS.Web.Ajax
{
	public class ServerUtils
	{
		private readonly string _configPath;

		private List<ServerInfo> _serversCache;

		private static ServerInfo _defaultServer;

		private static ServerUtils _instance;

		private static readonly object LockObject = new object();

		private ServerUtils()
		{
			this._configPath = HttpContext.Current.Server.MapPath("~/config/base/base.config");
			this.InitServers();
		}

		public static ServerUtils Instance()
		{
			if (ServerUtils._instance == null)
			{
				lock (ServerUtils.LockObject)
				{
					ServerUtils._instance = new ServerUtils();
				}
			}
			return ServerUtils._instance;
		}

		private void InitServers()
		{
			XDocument xDocument = XDocument.Load(this._configPath);
			XElement xElement = xDocument.Descendants("configuration").FirstOrDefault<XElement>();
			if (xElement != null)
			{
               IEnumerable<XElement> source = xElement.Elements("CustomerNum");
				this._serversCache = (from t in source.Select(new Func<XElement, ServerInfo>(ServerUtils.ParseElement))
				where t != null
				select t).ToList<ServerInfo>();
				XElement xElement2 = xElement.Element("LostCustomerNum");
				ServerUtils._defaultServer = ServerUtils.ParseElement(xElement2);
			}
		}

		public List<ServerInfo> GetServers()
		{
			this.InitServers();
			return this._serversCache;
		}

		public ServerInfo GetLocation()
		{
			return ServerUtils._defaultServer;
		}

		public bool CheckCodeNum(string guid, int code, out string msg)
		{
			msg = "";
			bool result;
			if (code < 2 || code > 9)
			{
				msg = "部门编码只能是2-9之间的数字！";
				result = false;
			}
			else
			{
				int num = this._serversCache.Count((ServerInfo t) => t.guid != guid && t.code == code);
				if (num > 0)
				{
					msg = "部门编号重复！";
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		public bool CheckName(string guid, string name, out string msg)
		{
			msg = "";
			bool result;
			if (string.IsNullOrEmpty(name))
			{
				msg = "部门名称不能为空！";
				result = false;
			}
			else
			{
				int num = this._serversCache.Count((ServerInfo t) => t.guid != guid && t.name == name);
				if (num > 0)
				{
					msg = "部门名称重复！";
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		public bool CheckIpArea(string guid, string beginIp, string endIp, out string msg)
		{
			msg = "";
			Regex regex = new Regex("^(((25[0-5])|(2[0-4]\\d)|(1\\d{2})|([1-9]?\\d))\\.){3}((25[0-5])|(2[0-4]\\d)|(1\\d{2})|([1-9]?\\d))$");
			bool result;
			if (!regex.IsMatch(beginIp) || !regex.IsMatch(endIp))
			{
				msg = "IP地址无效！";
				result = false;
			}
			else
			{
				long num = ServerUtils.ParseIpToLong(beginIp);
				long num2 = ServerUtils.ParseIpToLong(endIp);
				bool flag = true;
				if (num > num2)
				{
					msg = "IP区间不正确！";
					result = false;
				}
				else
				{
					List<ServerInfo> list = (from t in this._serversCache
					where t.guid != guid
					select t).ToList<ServerInfo>();
					foreach (ServerInfo current in list)
					{
						if (ServerUtils.ParseIpToLong(current.begin_ip) <= num2 && ServerUtils.ParseIpToLong(current.end_ip) >= num)
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						msg = "ip地址区间重复！";
					}
					result = flag;
				}
			}
			return result;
		}

		private static long ParseIpToLong(string ip)
		{
			string[] array = ip.Split(new char[]
			{
				'.'
			});
			long num = 0L;
			for (int i = 0; i < 4; i++)
			{
				long num2 = 1L;
				long num3;
				if (!long.TryParse(array[3 - i], out num3))
				{
					num3 = 0L;
				}
				for (int j = 0; j < i; j++)
				{
					num2 *= 256L;
				}
				num += num3 * num2;
			}
			return num;
		}

		public bool Update(ServerInfo info, out string msg)
		{
			msg = "";
			XDocument xDocument = XDocument.Load(this._configPath);
			XElement xElement = xDocument.Descendants("configuration").FirstOrDefault<XElement>();
			bool result;
			if (xElement != null)
			{
				if (!this.CheckCodeNum(info.guid, info.code, out msg))
				{
					result = false;
					return result;
				}
				if (!this.CheckName(info.guid, info.name, out msg))
				{
					result = false;
					return result;
				}
				if (!this.CheckIpArea(info.guid, info.begin_ip, info.end_ip, out msg))
				{
					result = false;
					return result;
				}
				XElement xElement2 = xElement.Elements("CustomerNum").FirstOrDefault((XElement t) => t.Attribute("GUID").Value == info.guid);
				bool flag = false;
				if (xElement2 == null)
				{
					flag = true;
					xElement2 = new XElement("CustomerNum", new XAttribute("GUID", this.CreateGuid()));
				}
				xElement2.SetAttributeValue("DefaultHeaderCode", info.code);
				xElement2.SetAttributeValue("DefaultName", info.name);
				xElement2.SetAttributeValue("BeginIP", info.begin_ip);
				xElement2.SetAttributeValue("EndIP", info.end_ip);
				if (flag)
				{
					xElement.Add(xElement2);
				}
				xDocument.Save(this._configPath);
				this.InitServers();
			}
			result = true;
			return result;
		}

		public bool Delete(string guid, out string msg)
		{
			msg = string.Empty;
			bool result;
			if (!string.IsNullOrEmpty(guid))
			{
				XDocument xDocument = XDocument.Load(this._configPath);
				XElement xElement = xDocument.Descendants("configuration").FirstOrDefault<XElement>();
				if (xElement != null)
				{
					XElement xElement2 = xElement.Elements("CustomerNum").FirstOrDefault((XElement t) => t.Attribute("GUID").Value == guid.Trim());
					if (xElement2 != null)
					{
						xElement2.Remove();
						xDocument.Save(this._configPath);
						this.InitServers();
						result = true;
						return result;
					}
				}
			}
			msg = "服务器节点未找到或已被删除！";
			result = false;
			return result;
		}

		public string CreateGuid()
		{
			return Guid.NewGuid().ToString().Replace("-", "").ToLower();
		}

		private static ServerInfo ParseElement(XElement xElement)
		{
			ServerInfo result;
			if (xElement == null)
			{
				result = null;
			}
			else
			{
				ServerInfo serverInfo = new ServerInfo
				{
					guid = ServerUtils.GetValue(xElement, "GUID"),
					begin_ip = ServerUtils.GetValue(xElement, "BeginIP"),
					end_ip = ServerUtils.GetValue(xElement, "EndIP"),
					name = ServerUtils.GetValue(xElement, "DefaultName"),
					ip = ServerUtils.GetValue(xElement, "DefaultIp")
				};
				int code;
				if (!int.TryParse(xElement.Attribute("DefaultHeaderCode").Value, out code))
				{
					code = 0;
				}
				serverInfo.code = code;
				result = serverInfo;
			}
			return result;
		}

		private static string GetValue(XElement ele, string name)
		{
			XAttribute xAttribute = ele.Attribute(name);
			return (xAttribute == null) ? "" : xAttribute.Value.Trim();
		}
	}
}
