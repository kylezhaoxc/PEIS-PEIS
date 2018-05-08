using System;

namespace PEIS.Model
{
	[Serializable]
	public class OnCustFee
	{
		private string _type;

		private string _ID_Section;

		private string _SectionName;

		private string _Apply = string.Empty;

		private int _id_custfee;

		private long? _id_customer;

		private int? _id_fee;

		private string _feeitemname;

		private int? _id_register;

		private string _registername;

		private DateTime? _registdate;

		private decimal _originalprice;

		private decimal _discount;

		private decimal _factprice;

		private int? _id_discounter;

		private string _discountername;

		private bool _is_feecharged;

		private int? _id_feecharger;

		private string _feecharger;

		private DateTime _feechargetime;

		private bool? _is_feerefund;

		private int? _id_feerefunder;

		private string _feerefunder;

		private bool? _is_examined;

		private int? _id_examdoctor;

		private string _examdoctorname;

		private DateTime? _examdate;

		private bool? _is_discard;

		private int? _id_feetype;

		private bool _Is_Printed;

		private string _feetypeName = string.Empty;

		public string ApplyID
		{
			get
			{
				return this._Apply;
			}
			set
			{
				this._Apply = value;
			}
		}

		public string SectionName
		{
			get
			{
				return this._SectionName;
			}
			set
			{
				this._SectionName = value;
			}
		}

		public string ID_Section
		{
			get
			{
				return this._ID_Section;
			}
			set
			{
				this._ID_Section = value;
			}
		}

		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		public int ID_CustFee
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

		public string FeeItemName
		{
			get
			{
				return this._feeitemname;
			}
			set
			{
				this._feeitemname = value;
			}
		}

		public int? ID_Register
		{
			get
			{
				return this._id_register;
			}
			set
			{
				this._id_register = value;
			}
		}

		public string RegisterName
		{
			get
			{
				return this._registername;
			}
			set
			{
				this._registername = value;
			}
		}

		public DateTime? RegistDate
		{
			get
			{
				return this._registdate;
			}
			set
			{
				this._registdate = value;
			}
		}

		public decimal OriginalPrice
		{
			get
			{
				return this._originalprice;
			}
			set
			{
				this._originalprice = value;
			}
		}

		public decimal Discount
		{
			get
			{
				return this._discount;
			}
			set
			{
				this._discount = value;
			}
		}

		public decimal FactPrice
		{
			get
			{
				return this._factprice;
			}
			set
			{
				this._factprice = value;
			}
		}

		public int? ID_Discounter
		{
			get
			{
				return this._id_discounter;
			}
			set
			{
				this._id_discounter = value;
			}
		}

		public string DiscounterName
		{
			get
			{
				return this._discountername;
			}
			set
			{
				this._discountername = value;
			}
		}

		public bool Is_FeeCharged
		{
			get
			{
				return this._is_feecharged;
			}
			set
			{
				this._is_feecharged = value;
			}
		}

		public bool Is_Printed
		{
			get
			{
				return this._Is_Printed;
			}
			set
			{
				this._Is_Printed = value;
			}
		}

		public int? ID_FeeCharger
		{
			get
			{
				return this._id_feecharger;
			}
			set
			{
				this._id_feecharger = value;
			}
		}

		public string FeeCharger
		{
			get
			{
				return this._feecharger;
			}
			set
			{
				this._feecharger = value;
			}
		}

		public DateTime FeeChargeTime
		{
			get
			{
				return this._feechargetime;
			}
			set
			{
				this._feechargetime = value;
			}
		}

		public bool? Is_FeeRefund
		{
			get
			{
				return this._is_feerefund;
			}
			set
			{
				this._is_feerefund = value;
			}
		}

		public int? ID_FeeRefunder
		{
			get
			{
				return this._id_feerefunder;
			}
			set
			{
				this._id_feerefunder = value;
			}
		}

		public string FeeRefunder
		{
			get
			{
				return this._feerefunder;
			}
			set
			{
				this._feerefunder = value;
			}
		}

		public bool? Is_Examined
		{
			get
			{
				return this._is_examined;
			}
			set
			{
				this._is_examined = value;
			}
		}

		public int? ID_ExamDoctor
		{
			get
			{
				return this._id_examdoctor;
			}
			set
			{
				this._id_examdoctor = value;
			}
		}

		public string ExamDoctorName
		{
			get
			{
				return this._examdoctorname;
			}
			set
			{
				this._examdoctorname = value;
			}
		}

		public DateTime? ExamDate
		{
			get
			{
				return this._examdate;
			}
			set
			{
				this._examdate = value;
			}
		}

		public bool? Is_Discard
		{
			get
			{
				return this._is_discard;
			}
			set
			{
				this._is_discard = value;
			}
		}

		public int? ID_FeeType
		{
			get
			{
				return this._id_feetype;
			}
			set
			{
				this._id_feetype = value;
			}
		}

		public string FeetypeName
		{
			get
			{
				return this._feetypeName;
			}
			set
			{
				this._feetypeName = value;
			}
		}
	}
}
