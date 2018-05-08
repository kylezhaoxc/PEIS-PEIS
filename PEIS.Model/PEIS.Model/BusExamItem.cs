using System;

namespace PEIS.Model
{
	[Serializable]
	public class BusExamItem
	{
		private int _id_examitem;

		private string _examitemname;

		private string _getresultway;

		private string _examitemcode;

		private int? _id_section;

		private bool? _is_lisvaluenull;

		private bool? _is_entrysectsum;

		private int? _entrysectsumlevel;

		private bool? _is_autocalc;

		private string _calcexpression;

		private int? _symcols;

		private int? _textboxrows;

		private bool? _is_samerow;

		private string _examitemunit;

		private decimal? _malehilimit;

		private decimal? _malelolimit;

		private decimal? _femalehilimit;

		private decimal? _femalelolimit;

		private bool? _is_symmultivalue;

		private string _inputcode;

		private int? _disporder;

		private string _note;

		private int? _forgender;

		private bool? _is_examitemnonprintinreport;

		private int? _id_examitemgroup;

		private string _abbrexamname;

		public int ID_ExamItem
		{
			get
			{
				return this._id_examitem;
			}
			set
			{
				this._id_examitem = value;
			}
		}

		public string ExamItemName
		{
			get
			{
				return this._examitemname;
			}
			set
			{
				this._examitemname = value;
			}
		}

		public string GetResultWay
		{
			get
			{
				return this._getresultway;
			}
			set
			{
				this._getresultway = value;
			}
		}

		public string ExamItemCode
		{
			get
			{
				return this._examitemcode;
			}
			set
			{
				this._examitemcode = value;
			}
		}

		public int? ID_Section
		{
			get
			{
				return this._id_section;
			}
			set
			{
				this._id_section = value;
			}
		}

		public bool? Is_LisValueNull
		{
			get
			{
				return this._is_lisvaluenull;
			}
			set
			{
				this._is_lisvaluenull = value;
			}
		}

		public bool? Is_EntrySectSum
		{
			get
			{
				return this._is_entrysectsum;
			}
			set
			{
				this._is_entrysectsum = value;
			}
		}

		public int? EntrySectSumLevel
		{
			get
			{
				return this._entrysectsumlevel;
			}
			set
			{
				this._entrysectsumlevel = value;
			}
		}

		public bool? Is_AutoCalc
		{
			get
			{
				return this._is_autocalc;
			}
			set
			{
				this._is_autocalc = value;
			}
		}

		public string CalcExpression
		{
			get
			{
				return this._calcexpression;
			}
			set
			{
				this._calcexpression = value;
			}
		}

		public int? SymCols
		{
			get
			{
				return this._symcols;
			}
			set
			{
				this._symcols = value;
			}
		}

		public int? TextboxRows
		{
			get
			{
				return this._textboxrows;
			}
			set
			{
				this._textboxrows = value;
			}
		}

		public bool? Is_SameRow
		{
			get
			{
				return this._is_samerow;
			}
			set
			{
				this._is_samerow = value;
			}
		}

		public string ExamItemUnit
		{
			get
			{
				return this._examitemunit;
			}
			set
			{
				this._examitemunit = value;
			}
		}

		public decimal? MaleHiLimit
		{
			get
			{
				return this._malehilimit;
			}
			set
			{
				this._malehilimit = value;
			}
		}

		public decimal? MaleLoLimit
		{
			get
			{
				return this._malelolimit;
			}
			set
			{
				this._malelolimit = value;
			}
		}

		public decimal? FemaleHiLimit
		{
			get
			{
				return this._femalehilimit;
			}
			set
			{
				this._femalehilimit = value;
			}
		}

		public decimal? FemaleLoLimit
		{
			get
			{
				return this._femalelolimit;
			}
			set
			{
				this._femalelolimit = value;
			}
		}

		public bool? Is_SymMultiValue
		{
			get
			{
				return this._is_symmultivalue;
			}
			set
			{
				this._is_symmultivalue = value;
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

		public int? DispOrder
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

		public int? Forsex
		{
			get
			{
				return this._forgender;
			}
			set
			{
				this._forgender = value;
			}
		}

		public bool? Is_ExamItemNonPrintInReport
		{
			get
			{
				return this._is_examitemnonprintinreport;
			}
			set
			{
				this._is_examitemnonprintinreport = value;
			}
		}

		public int? ID_ExamItemGroup
		{
			get
			{
				return this._id_examitemgroup;
			}
			set
			{
				this._id_examitemgroup = value;
			}
		}

		public string AbbrExamName
		{
			get
			{
				return this._abbrexamname;
			}
			set
			{
				this._abbrexamname = value;
			}
		}
	}
}
