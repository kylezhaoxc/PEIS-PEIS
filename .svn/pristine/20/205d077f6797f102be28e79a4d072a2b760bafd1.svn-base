using PEIS.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.Common
{
	public class ClientListManagement
	{
		private static readonly ClientListManagement _Instance = new ClientListManagement();

		private System.Collections.Generic.Dictionary<string, ClientInfo> _ClientList = new System.Collections.Generic.Dictionary<string, ClientInfo>();

		private static string[] Columns = new string[]
		{
			"Interval",
			"LoginDate",
			"UserName",
			"RowNum",
			"GUID",
			"LoginState",
			"Message",
			"CpuID",
			"MacAddress",
			"DiskID",
			"IpAddress",
			"LoginUserName",
			"UserID",
			"ComputerName",
			"SystemType",
			"TotalPhysicalMemory"
		};

		private DataTable dt = new DataTable();

		public System.Collections.Generic.Dictionary<string, ClientInfo> ClientList
		{
			get
			{
				if (this._ClientList == null)
				{
					this._ClientList = new System.Collections.Generic.Dictionary<string, ClientInfo>();
				}
				return this._ClientList;
			}
		}

		private ClientListManagement()
		{
		}

		public static ClientListManagement Instance()
		{
			return ClientListManagement._Instance;
		}

		public string LogOutCustomer(string GUID)
		{
			string result = "{\"success\":\"0\",\"Message\":\"未获取到客户端信息\"}";
			string[] array = GUID.Split(new char[]
			{
				','
			});
			int num = 0;
			if (array.Length > 0)
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					if (text != string.Empty)
					{
						num += this.LogOutClient(text);
					}
				}
				result = "{\"success\":\"1\",\"Message\":\"共成功设置下线客户端" + num + "个！\"}";
			}
			return result;
		}

		private int LogOutClient(string GUID)
		{
			int result;
			if (this._ClientList.ContainsKey(GUID))
			{
				ClientInfo clientInfo = this._ClientList[GUID];
				clientInfo.LoginState = LoginState.已被迫下线;
				clientInfo.Message = "管理员剔除下线";
				this._ClientList[GUID] = clientInfo;
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public string SendMessageToCustomer(string GUID, string Msg)
		{
			string result = "{\"success\":\"0\",\"Message\":\"未获取到客户端信息\"}";
			string[] array = GUID.Split(new char[]
			{
				','
			});
			int num = 0;
			if (array.Length > 0)
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					if (text != string.Empty)
					{
						num += this.SendMessage(text, Msg);
					}
				}
				result = "{\"success\":\"1\",\"Message\":\"共成功发送消息到" + num + "个客户端！\"}";
			}
			return result;
		}

		private int SendMessage(string GUID, string Msg)
		{
			int result;
			if (this._ClientList.ContainsKey(GUID))
			{
				ClientInfo clientInfo = this._ClientList[GUID];
				clientInfo.Message = Msg;
				this._ClientList[GUID] = clientInfo;
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public void ClearMe(string Key)
		{
			if (this._ClientList.ContainsKey(Key))
			{
				this._ClientList.Remove(Key);
			}
		}

		public void ClearAll()
		{
			this._ClientList.Clear();
		}

		public void AddClientInfo(ClientInfo CI)
		{
			try
			{
				if (CI != null)
				{
					if (string.IsNullOrEmpty(CI.GUID))
					{
						Log4J.Instance.Info("新增客户信息(AddClientInfo)失败！原因：CI.GUID 为NULL ");
					}
					else if (!this._ClientList.ContainsKey(CI.GUID))
					{
						this._ClientList.Add(CI.GUID, CI);
					}
					else
					{
						this._ClientList[CI.GUID] = CI;
					}
				}
			}
			catch (System.Exception ex)
			{
				Log4J.Instance.Info("新增客户信息(AddClientInfo)失败！失败原因是：" + ex.Message);
			}
		}

		public void DeleteClientInfo(ClientInfo CI)
		{
			foreach (System.Collections.Generic.KeyValuePair<string, ClientInfo> current in this._ClientList)
			{
				if (CI.GUID.Trim() == current.Key.Trim())
				{
					this._ClientList.Remove(current.Key);
				}
			}
		}

		public void DeleteClientInfo(string GUID)
		{
			foreach (System.Collections.Generic.KeyValuePair<string, ClientInfo> current in this._ClientList)
			{
				if (GUID.Trim() == current.Key.Trim())
				{
					this._ClientList.Remove(current.Key);
				}
			}
		}

		public string GetClientList()
		{
			string result;
			lock (this.dt)
			{
				int num = 1;
				if (this.dt.Columns.Count == 0)
				{
					string[] columns = ClientListManagement.Columns;
					for (int i = 0; i < columns.Length; i++)
					{
						string columnName = columns[i];
						this.dt.Columns.Add(columnName);
					}
				}
				this.dt.Clear();
				try
				{
					if (this._ClientList == null)
					{
						Log4J.Instance.Info("获取客户端(GetClientList)失败！失败原因是：客户端常量（_ClientList）为Null，无法获取数据。line:209");
					}
					else
					{
						Log4J.Instance.Info("获取客户端(GetClientList)成功！当前共:" + this._ClientList.Count + "个客户端在线（含相同登录名）。line:213");
					}
					foreach (System.Collections.Generic.KeyValuePair<string, ClientInfo> current in this._ClientList)
					{
						DataRow dataRow = this.dt.NewRow();
						dataRow["RowNum"] = num;
						dataRow["ComputerName"] = current.Value.ComputerName;
						dataRow["GUID"] = current.Value.GUID;
						dataRow["LoginState"] = current.Value.LoginState;
						dataRow["Message"] = current.Value.Message;
						dataRow["CpuID"] = current.Value.CpuID;
						dataRow["MacAddress"] = current.Value.MacAddress;
						dataRow["DiskID"] = current.Value.DiskID;
						dataRow["IpAddress"] = current.Value.IpAddress;
						dataRow["LoginUserName"] = current.Value.LoginUserName;
						dataRow["UserID"] = current.Value.UserID;
						dataRow["ComputerName"] = current.Value.ComputerName;
						dataRow["SystemType"] = current.Value.SystemType;
						dataRow["TotalPhysicalMemory"] = current.Value.TotalPhysicalMemory;
						dataRow["UserName"] = current.Value.UserName;
						dataRow["LoginDate"] = current.Value.LoginDate.ToString("yyyy-MM-dd HH:mm:ss");
						dataRow["Interval"] = current.Value.Interval;
						this.dt.Rows.Add(dataRow);
						num++;
					}
				}
				catch (System.Exception ex)
				{
					Log4J.Instance.Info("获取客户端(GetClientList)失败！失败原因是：" + ex.Message + "。line:245");
				}
				result = JsonHelperFont.Instance.DataTableToJSON(this.dt, "dataList");
			}
			return result;
		}

		public bool IsOnline(string UserID)
		{
			bool result;
			foreach (System.Collections.Generic.KeyValuePair<string, ClientInfo> current in this._ClientList)
			{
				if (current.Value.UserID == UserID || current.Value.LoginUserName == UserID)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public string GetClientInfo(string GUID)
		{
			int num = 1;
			if (this.dt.Columns.Count == 0)
			{
				string[] columns = ClientListManagement.Columns;
				for (int i = 0; i < columns.Length; i++)
				{
					string columnName = columns[i];
					this.dt.Columns.Add(columnName);
				}
			}
			this.dt.Clear();
			if (this._ClientList.ContainsKey(GUID))
			{
				ClientInfo clientInfo = this._ClientList[GUID];
				DataRow dataRow = this.dt.NewRow();
				dataRow["RowNum"] = num;
				dataRow["ComputerName"] = clientInfo.ComputerName;
				dataRow["GUID"] = clientInfo.GUID;
				dataRow["LoginState"] = clientInfo.LoginState;
				dataRow["Message"] = clientInfo.Message;
				dataRow["CpuID"] = clientInfo.CpuID;
				dataRow["MacAddress"] = clientInfo.MacAddress;
				dataRow["DiskID"] = clientInfo.DiskID;
				dataRow["IpAddress"] = clientInfo.IpAddress;
				dataRow["LoginUserName"] = clientInfo.LoginUserName;
				dataRow["UserID"] = clientInfo.UserID;
				dataRow["ComputerName"] = clientInfo.ComputerName;
				dataRow["SystemType"] = clientInfo.SystemType;
				dataRow["TotalPhysicalMemory"] = clientInfo.TotalPhysicalMemory;
				dataRow["UserName"] = clientInfo.UserName;
				dataRow["LoginDate"] = clientInfo.LoginDate.ToString("yyyy-MM-dd HH:mm:ss");
				this.dt.Rows.Add(dataRow);
				dataRow["Interval"] = clientInfo.Interval;
				num++;
			}
			return JsonHelperFont.Instance.DataTableToJSON(this.dt, "dataList");
		}
	}
}
