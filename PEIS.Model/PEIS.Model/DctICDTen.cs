using System;

namespace PEIS.Model
{
	[Serializable]
	public class DctICDTen
	{
		private int _id_icd;

		private string _icdcnname;

		private string _icdenname;

		private string _code;

		private string _codea;

		private int? _id_creator;

		private string _creator;

		private DateTime? _createdate;

		private bool? _is_banned;

		private int? _id_banopr;

		private string _banoperator;

		private DateTime? _bandate;

		private string _bandescribe;

		private int? _levela;

		private int? _levelb;

		private int? _levelc;

		private int? _leveld;

		private int? _levele;

		private int? _leveltree;

		private string _class;

		private string _tag;

		private string _icdtosection;

		private string _note;

		private string _inputcode;

		public int ID_ICD
		{
			get
			{
				return this._id_icd;
			}
			set
			{
				this._id_icd = value;
			}
		}

		public string ICDCNName
		{
			get
			{
				return this._icdcnname;
			}
			set
			{
				this._icdcnname = value;
			}
		}

		public string ICDENName
		{
			get
			{
				return this._icdenname;
			}
			set
			{
				this._icdenname = value;
			}
		}

		public string Code
		{
			get
			{
				return this._code;
			}
			set
			{
				this._code = value;
			}
		}

		public string Codea
		{
			get
			{
				return this._codea;
			}
			set
			{
				this._codea = value;
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

		public string Creator
		{
			get
			{
				return this._creator;
			}
			set
			{
				this._creator = value;
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

		public int? LevelA
		{
			get
			{
				return this._levela;
			}
			set
			{
				this._levela = value;
			}
		}

		public int? LevelB
		{
			get
			{
				return this._levelb;
			}
			set
			{
				this._levelb = value;
			}
		}

		public int? LevelC
		{
			get
			{
				return this._levelc;
			}
			set
			{
				this._levelc = value;
			}
		}

		public int? LevelD
		{
			get
			{
				return this._leveld;
			}
			set
			{
				this._leveld = value;
			}
		}

		public int? LevelE
		{
			get
			{
				return this._levele;
			}
			set
			{
				this._levele = value;
			}
		}

		public int? LevelTree
		{
			get
			{
				return this._leveltree;
			}
			set
			{
				this._leveltree = value;
			}
		}

		public string Class
		{
			get
			{
				return this._class;
			}
			set
			{
				this._class = value;
			}
		}

		public string Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		public string ICDtoSection
		{
			get
			{
				return this._icdtosection;
			}
			set
			{
				this._icdtosection = value;
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
	}
}
