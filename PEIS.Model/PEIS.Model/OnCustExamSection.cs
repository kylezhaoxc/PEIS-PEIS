using System;

namespace PEIS.Model
{
	[Serializable]
	public class OnCustExamSection
	{
		private int _id_custexamsection;

		private long? _id_customer;

		private int? _id_section;

		private string _customername;

		private string _sectionname;

		private int? _diseaselevel;

		private DateTime? _sectionsummarydate;

		private string _sectionsummary;

		private string _positivesummary;

		private int? _id_summarydoctor;

		private string _summarydoctorname;

		private int? _id_typist;

		private string _typistname;

		private DateTime? _typistdate;

		private bool? _is_check;

		private string _checkername;

		private DateTime? _checkdate;

		private int? _id_checker;

		private bool? _is_giveup;

		private bool? _is_refund;

		private string _imageurl;

		public int ID_CustExamSection
		{
			get
			{
				return this._id_custexamsection;
			}
			set
			{
				this._id_custexamsection = value;
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

		public string CustomerName
		{
			get
			{
				return this._customername;
			}
			set
			{
				this._customername = value;
			}
		}

		public string SectionName
		{
			get
			{
				return this._sectionname;
			}
			set
			{
				this._sectionname = value;
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

		public DateTime? SectionSummaryDate
		{
			get
			{
				return this._sectionsummarydate;
			}
			set
			{
				this._sectionsummarydate = value;
			}
		}

		public string SectionSummary
		{
			get
			{
				return this._sectionsummary;
			}
			set
			{
				this._sectionsummary = value;
			}
		}

		public string PositiveSummary
		{
			get
			{
				return this._positivesummary;
			}
			set
			{
				this._positivesummary = value;
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

		public DateTime? TypistDate
		{
			get
			{
				return this._typistdate;
			}
			set
			{
				this._typistdate = value;
			}
		}

		public bool? Is_Check
		{
			get
			{
				return this._is_check;
			}
			set
			{
				this._is_check = value;
			}
		}

		public string CheckerName
		{
			get
			{
				return this._checkername;
			}
			set
			{
				this._checkername = value;
			}
		}

		public DateTime? CheckDate
		{
			get
			{
				return this._checkdate;
			}
			set
			{
				this._checkdate = value;
			}
		}

		public int? ID_Checker
		{
			get
			{
				return this._id_checker;
			}
			set
			{
				this._id_checker = value;
			}
		}

		public bool? IS_giveup
		{
			get
			{
				return this._is_giveup;
			}
			set
			{
				this._is_giveup = value;
			}
		}

		public bool? IS_Refund
		{
			get
			{
				return this._is_refund;
			}
			set
			{
				this._is_refund = value;
			}
		}

		public string ImageUrl
		{
			get
			{
				return this._imageurl;
			}
			set
			{
				this._imageurl = value;
			}
		}
	}
}
