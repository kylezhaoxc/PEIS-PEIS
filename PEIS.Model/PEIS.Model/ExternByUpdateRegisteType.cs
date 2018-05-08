using System;
using System.Collections;

namespace PEIS.Model
{
	public class ExternByUpdateRegisteType
	{
		private bool? _is_updateRegisteDate = new bool?(false);

		private DateTime _RegisteDate;

		private ArrayList _CustBackLogList = null;

		public bool? Is_updateRegisteDate
		{
			get
			{
				return this._is_updateRegisteDate;
			}
			set
			{
				this._is_updateRegisteDate = value;
			}
		}

		public DateTime RegisteDate
		{
			get
			{
				return this._RegisteDate;
			}
			set
			{
				this._RegisteDate = value;
			}
		}

		public ArrayList CustBackLogList
		{
			get
			{
				return this._CustBackLogList;
			}
			set
			{
				this._CustBackLogList = value;
			}
		}
	}
}
