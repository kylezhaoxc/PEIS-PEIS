using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace PEIS.DBUtility
{
	public class CommandInfo
	{
		public object ShareObject = null;

		public object OriginalData = null;

		public string CommandText;

		public System.Data.Common.DbParameter[] Parameters;

		public EffentNextType EffentNextType = EffentNextType.None;

		private event EventHandler _solicitationEvent;

		public event EventHandler SolicitationEvent
		{
			add
			{
				this._solicitationEvent += value;
			}
			remove
			{
				this._solicitationEvent -= value;
			}
		}

		public void OnSolicitationEvent()
		{
			if (this._solicitationEvent != null)
			{
				this._solicitationEvent(this, new EventArgs());
			}
		}

		public CommandInfo()
		{
		}

		public CommandInfo(string sqlText, System.Data.SqlClient.SqlParameter[] para)
		{
			this.CommandText = sqlText;
			this.Parameters = para;
		}

		public CommandInfo(string sqlText, System.Data.SqlClient.SqlParameter[] para, EffentNextType type)
		{
			this.CommandText = sqlText;
			this.Parameters = para;
			this.EffentNextType = type;
		}
	}
}
