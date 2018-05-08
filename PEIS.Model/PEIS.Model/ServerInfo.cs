using System;

namespace PEIS.Model
{
	[Serializable]
	public class ServerInfo
	{
		public string guid
		{
			get;
			set;
		}

		public int code
		{
			get;
			set;
		}

		public string name
		{
			get;
			set;
		}

		public string begin_ip
		{
			get;
			set;
		}

		public string end_ip
		{
			get;
			set;
		}

		public string ip
		{
			get;
			set;
		}
	}
}
