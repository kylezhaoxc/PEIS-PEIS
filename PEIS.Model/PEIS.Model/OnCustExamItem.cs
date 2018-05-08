using System;
using System.Collections.Generic;

namespace PEIS.Model
{
	[Serializable]
	public class OnCustExamItem
	{
		private int _id_custexamitem;

		private int? _id_custfee;

		private int? _id_fee;

		private int _id_examitem;

		private string _examitemname;

		private int? _diseaselevel;

		private string _resultlabvalues;

		private decimal? _resultnumber;

		private decimal? _resultlablowlimit;

		private decimal? _resultlabhighlimit;

		private string _resultlabunit;

		private string _resultlabmark;

		private string _resultsummary;

		private int? _id_summarydoctor;

		private string _summarydoctorname;

		private string _typistname;

		private DateTime _examitemsummarydate;

		private int? _id_typist;

		private string _resultlabrange;

		private long? _id_customer;

		private string _abbrexamname;

		private string _detectionmethod;

		private string _sco;

		private string _id_custapply;

		private List<OnCustExamItemResult> _examItemResultList;

		public int ID_CustExamItem
		{
			get
			{
				return this._id_custexamitem;
			}
			set
			{
				this._id_custexamitem = value;
			}
		}

		public int? ID_CustFee
		{
			get
			{
				return this._id_custfee;
			}
			set
			{
				this._id_custfee = value;
			}
		}

		public int? ID_Fee
		{
			get
			{
				return this._id_fee;
			}
			set
			{
				this._id_fee = value;
			}
		}

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

		public int? DiseaseLevel
		{
			get
			{
				return this._diseaselevel;
			}
			set
			{
				this._diseaselevel = value;
			}
		}

		public string ResultLabValues
		{
			get
			{
				return this._resultlabvalues;
			}
			set
			{
				this._resultlabvalues = value;
			}
		}

		public decimal? ResultNumber
		{
			get
			{
				return this._resultnumber;
			}
			set
			{
				this._resultnumber = value;
			}
		}

		public decimal? ResultLabLowLimit
		{
			get
			{
				return this._resultlablowlimit;
			}
			set
			{
				this._resultlablowlimit = value;
			}
		}

		public decimal? ResultLabHighLimit
		{
			get
			{
				return this._resultlabhighlimit;
			}
			set
			{
				this._resultlabhighlimit = value;
			}
		}

		public string ResultLabUnit
		{
			get
			{
				return this._resultlabunit;
			}
			set
			{
				this._resultlabunit = value;
			}
		}

		public string ResultLabMark
		{
			get
			{
				return this._resultlabmark;
			}
			set
			{
				this._resultlabmark = value;
			}
		}

		public string ResultSummary
		{
			get
			{
				return this._resultsummary;
			}
			set
			{
				this._resultsummary = value;
			}
		}

		public int? ID_SummaryDoctor
		{
			get
			{
				return this._id_summarydoctor;
			}
			set
			{
				this._id_summarydoctor = value;
			}
		}

		public string SummaryDoctorName
		{
			get
			{
				return this._summarydoctorname;
			}
			set
			{
				this._summarydoctorname = value;
			}
		}

		public string TypistName
		{
			get
			{
				return this._typistname;
			}
			set
			{
				this._typistname = value;
			}
		}

		public DateTime ExamItemSummaryDate
		{
			get
			{
				return this._examitemsummarydate;
			}
			set
			{
				this._examitemsummarydate = value;
			}
		}

		public int? ID_Typist
		{
			get
			{
				return this._id_typist;
			}
			set
			{
				this._id_typist = value;
			}
		}

		public string ResultLabRange
		{
			get
			{
				return this._resultlabrange;
			}
			set
			{
				this._resultlabrange = value;
			}
		}

		public long? ID_Customer
		{
			get
			{
				return this._id_customer;
			}
			set
			{
				this._id_customer = value;
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

		public string DetectionMethod
		{
			get
			{
				return this._detectionmethod;
			}
			set
			{
				this._detectionmethod = value;
			}
		}

		public string SCO
		{
			get
			{
				return this._sco;
			}
			set
			{
				this._sco = value;
			}
		}

		public string ID_CustApply
		{
			get
			{
				return this._id_custapply;
			}
			set
			{
				this._id_custapply = value;
			}
		}

		public List<OnCustExamItemResult> ExamItemResultList
		{
			get
			{
				return this._examItemResultList;
			}
			set
			{
				this._examItemResultList = value;
			}
		}
	}
}
