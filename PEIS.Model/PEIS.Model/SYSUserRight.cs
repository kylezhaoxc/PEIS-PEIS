using System;

namespace PEIS.Model
{
	[Serializable]
	public class SYSUserRight
    {
		private int _userrightID;

		private int? _RightID;

		private DateTime? _createdate;

		private int? _createuser;

		private int? _userID;

		public int UserRightID
        {
			get
			{
				return this._userrightID;
			}
			set
			{
				this._userrightID = value;
			}
		}

		public int? RightID
        {
			get
			{
				return this._RightID;
			}
			set
			{
				this._RightID = value;
			}
		}

		public DateTime? CreateDate
        {
			get
			{
				return this._createdate;
			}
			set
			{
				this._createdate = value;
			}
		}

		public int? OperatorID
        {
			get
			{
				return this._createuser;
			}
			set
			{
				this._createuser = value;
			}
		}

		public int? UserID
        {
			get
			{
				return this._userID;
			}
			set
			{
				this._userID = value;
			}
		}
	}
}
