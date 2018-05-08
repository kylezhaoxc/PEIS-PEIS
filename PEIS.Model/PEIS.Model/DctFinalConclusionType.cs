using System;

namespace PEIS.Model
{
	[Serializable]
	public class DctFinalConclusionType
	{
		private int _id_finalconclusiontype;

		private string _finalconclusiontypename;

		private string _inputcode;

		private string _note;

		private int? _id_creator;

		private DateTime? _createdate;

		private bool? _is_banned;

		private int? _id_banopr;

		private string _bandescribe;

		private int _disporder;

		private DateTime? _bandate;

		private string _banoperator;

		private string _finalconclusionsigncode;

		public int ID_FinalConclusionType
		{
			get
			{
				return this._id_finalconclusiontype;
			}
			set
			{
				this._id_finalconclusiontype = value;
			}
		}

		public string FinalConclusionTypeName
		{
			get
			{
				return this._finalconclusiontypename;
			}
			set
			{
				this._finalconclusiontypename = value;
			}
		}

		public string InputCode
		{
			get
			{
				return this._inputcode;
			}
			set
			{
				this._inputcode = value;
			}
		}

		public string Note
		{
			get
			{
				return this._note;
			}
			set
			{
				this._note = value;
			}
		}

		public int? ID_Creator
		{
			get
			{
				return this._id_creator;
			}
			set
			{
				this._id_creator = value;
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

		public bool? Is_Banned
		{
			get
			{
				return this._is_banned;
			}
			set
			{
				this._is_banned = value;
			}
		}

		public int? ID_BanOpr
		{
			get
			{
				return this._id_banopr;
			}
			set
			{
				this._id_banopr = value;
			}
		}

		public string BanDescribe
		{
			get
			{
				return this._bandescribe;
			}
			set
			{
				this._bandescribe = value;
			}
		}

		public int DispOrder
		{
			get
			{
				return this._disporder;
			}
			set
			{
				this._disporder = value;
			}
		}

		public DateTime? BanDate
		{
			get
			{
				return this._bandate;
			}
			set
			{
				this._bandate = value;
			}
		}

		public string BanOperator
		{
			get
			{
				return this._banoperator;
			}
			set
			{
				this._banoperator = value;
			}
		}

		public string FinalConclusionSignCode
		{
			get
			{
				return this._finalconclusionsigncode;
			}
			set
			{
				this._finalconclusionsigncode = value;
			}
		}
	}
}
