using System;
using System.Collections.Generic;
using System.Management;

namespace PEIS.Common
{
	public class Computer
	{
		private string _CpuID = string.Empty;

		private string _MacAddress = string.Empty;

		private string _DiskID = string.Empty;

		private string _IpAddress = string.Empty;

		private string _LoginUserName = string.Empty;

		private string _ComputerName = string.Empty;

		private string _SystemType = string.Empty;

		private string _TotalPhysicalMemory = string.Empty;

		private static Computer _instance;

		public string CpuID
		{
			get
			{
				return this._CpuID;
			}
			set
			{
				this._CpuID = value;
			}
		}

		public string MacAddress
		{
			get
			{
				return this._MacAddress;
			}
			set
			{
				this._MacAddress = value;
			}
		}

		public string DiskID
		{
			get
			{
				return this._DiskID;
			}
			set
			{
				this._DiskID = value;
			}
		}

		public string IpAddress
		{
			get
			{
				return this._IpAddress;
			}
			set
			{
				this._IpAddress = value;
			}
		}

		public string LoginUserName
		{
			get
			{
				return this._LoginUserName;
			}
			set
			{
				this._LoginUserName = value;
			}
		}

		public string ComputerName
		{
			get
			{
				return this._ComputerName;
			}
			set
			{
				this._ComputerName = value;
			}
		}

		public string SystemType
		{
			get
			{
				return this._SystemType;
			}
			set
			{
				this._SystemType = value;
			}
		}

		public string TotalPhysicalMemory
		{
			get
			{
				return this._TotalPhysicalMemory;
			}
			set
			{
				this._TotalPhysicalMemory = value;
			}
		}

		public static Computer Instance()
		{
			if (Computer._instance == null)
			{
				Computer._instance = new Computer();
			}
			return Computer._instance;
		}

		public Computer()
		{
			this.CpuID = Computer.GetCpuID();
			this.MacAddress = Computer.GetMacAddress();
			this.DiskID = Computer.GetDiskID();
			this.IpAddress = Computer.GetIPAddress();
			this.LoginUserName = Computer.GetUserName();
			this.SystemType = Computer.GetSystemType();
			this.TotalPhysicalMemory = Computer.GetTotalPhysicalMemory();
			this.ComputerName = this.GetComputerName();
		}

		public static string GetCpuID()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_Processor");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject.Properties["ProcessorId"].Value.ToString();
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			finally
			{
			}
			return result;
		}

		public static string GetMacAddress()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						if ((bool)managementObject["IPEnabled"])
						{
							text = managementObject["MacAddress"].ToString();
							break;
						}
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			finally
			{
			}
			return result;
		}

		public static string GetIPAddress()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						if ((bool)managementObject["IPEnabled"])
						{
							System.Array array = (System.Array)managementObject.Properties["IpAddress"].Value;
							text = array.GetValue(0).ToString();
							break;
						}
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			finally
			{
			}
			return result;
		}

		public static string GetBaseBoard()
		{
			string result;
			try
			{
				List<string> list = new List<string>();
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						string text = managementObject["SerialNumber"].ToString().Trim();
						if (!string.IsNullOrEmpty(text))
						{
							list.Add(text);
						}
					}
				}
				if (list.Count > 0)
				{
					list.Sort();
					result = list[0];
					return result;
				}
			}
			catch
			{
			}
			result = string.Empty;
			return result;
		}

		public static string GetDiskID()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_DiskDrive");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = (string)managementObject.Properties["Model"].Value;
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			finally
			{
			}
			return result;
		}

		public static string GetUserName()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_ComputerSystem");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject["UserName"].ToString();
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			finally
			{
			}
			return result;
		}

		public static string GetSystemType()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_ComputerSystem");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject["SystemType"].ToString();
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			finally
			{
			}
			return result;
		}

		public static string GetTotalPhysicalMemory()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_ComputerSystem");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject["TotalPhysicalMemory"].ToString();
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			finally
			{
			}
			return result;
		}

		private string GetComputerName()
		{
			string result;
			try
			{
				result = System.Environment.GetEnvironmentVariable("ComputerName");
			}
			catch
			{
				result = "unknow";
			}
			finally
			{
			}
			return result;
		}
	}
}
