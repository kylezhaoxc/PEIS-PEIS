using System;

namespace PEIS.Model
{
	[Serializable]
	public class OnCustPhysicalExamInfo
	{
		private long _id_customer;

		private string _customername;

		private bool? _is_team;

		private int? _id_team;

		private int? _is_paused;

		private bool? _is_sectionlock;

		private int? _id_examtype;

		private string _examtypename;

		private int? _id_set;

		private string _setname;

		private int? _id_examplace;

		private string _examplacename;

		private int? _id_guidenurse;

		private string _guidenurse;

		private int? _id_reportway;

		private string _reportwayname;

		private int? _id_feeway;

		private string _feewayname;

		private int? _securitylevel;

		private int? _diseaselevel;

		private bool? _is_feesettled;

		private bool? _is_guidesheetprinted;

		private bool? _is_guidesheetreturned;

		private DateTime? _guidesheetreturneddate;

		private string _guidesheetreturnedby;

		private bool? _is_usehidecode;

		private bool? _is_finalfinished;

		private int? _id_finaldoctor;

		private string _finaldoctor;

		private DateTime? _finaldate;

		private string _finaloverview;

		private string _finalconclusion;

		private string _resultcompare;

		private string _maindiagnose;

		private string _finaldietguide;

		private string _finalsportguide;

		private string _finalhealthknowlage;

		private bool? _is_checked;

		private int? _id_checker;

		private string _checker;

		private DateTime? _checkeddate;

		private bool? _is_reportreceipted;

		private DateTime? _subscribdate;

		private DateTime? _operatedate;

		private int? _id_operator;

		private string _operator;

		private string _note;

		private int? _is_subscribed;

		private string _invoice;

		private int? _id_userguidesheetreturnedby;

		private int? _id_subscriber;

		private string _subscriber;

		private DateTime? _subscriberoperdate;

		private bool? _is_examstarted = new bool?(false);

		private string _teamname;

		private int? _id_gender;

		private string _gendername;

		private int? _id_marriage;

		private string _marriagename;

		private int? _id_nation;

		private string _nationname;

		private int? _id_cultrul;

		private string _cultrulname;

		private int? _id_vocation;

		private string _vocationname;

		private string _idcard;

		private string _examcard;

		private byte[] _photo;

		private DateTime? _birthday;

		private string _address;

		private string _mobileno;

		private string _email;

		private string _department;

		private string _departsuba;

		private string _departsubb;

		private string _departsubc;

		private string _indicatordiagnose;

		private string _otherdiagnose;

		private string _secondarydiagnose;

		private string _normaldiagnose;

		private bool? _is_diseaselevelinformed;

		private int? _id_diseaselevelinformer;

		private string _diseaselevelinformer;

		private DateTime? _diseaselevelinformeddate;

		private string _diseaselevelinformnote;

		public long ID_Customer
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

		public bool? Is_Team
		{
			get
			{
				return this._is_team;
			}
			set
			{
				this._is_team = value;
			}
		}

		public int? ID_Team
		{
			get
			{
				return this._id_team;
			}
			set
			{
				this._id_team = value;
			}
		}

		public int? Is_Paused
		{
			get
			{
				return this._is_paused;
			}
			set
			{
				this._is_paused = value;
			}
		}

		public bool? Is_SectionLock
		{
			get
			{
				return this._is_sectionlock;
			}
			set
			{
				this._is_sectionlock = value;
			}
		}

		public int? ID_ExamType
		{
			get
			{
				return this._id_examtype;
			}
			set
			{
				this._id_examtype = value;
			}
		}

		public string ExamTypeName
		{
			get
			{
				return this._examtypename;
			}
			set
			{
				this._examtypename = value;
			}
		}

		public int? PEPackageID
		{
			get
			{
				return this._id_set;
			}
			set
			{
				this._id_set = value;
			}
		}

		public string PEPackageName
		{
			get
			{
				return this._setname;
			}
			set
			{
				this._setname = value;
			}
		}

		public int? ExamPlaceID
		{
			get
			{
				return this._id_examplace;
			}
			set
			{
				this._id_examplace = value;
			}
		}

		public string ExamPlaceName
		{
			get
			{
				return this._examplacename;
			}
			set
			{
				this._examplacename = value;
			}
		}

		public int? ID_GuideNurse
		{
			get
			{
				return this._id_guidenurse;
			}
			set
			{
				this._id_guidenurse = value;
			}
		}

		public string GuideNurse
		{
			get
			{
				return this._guidenurse;
			}
			set
			{
				this._guidenurse = value;
			}
		}

		public int? ID_ReportWay
		{
			get
			{
				return this._id_reportway;
			}
			set
			{
				this._id_reportway = value;
			}
		}

		public string ReportWayName
		{
			get
			{
				return this._reportwayname;
			}
			set
			{
				this._reportwayname = value;
			}
		}

		public int? ID_FeeWay
		{
			get
			{
				return this._id_feeway;
			}
			set
			{
				this._id_feeway = value;
			}
		}

		public string FeeWayName
		{
			get
			{
				return this._feewayname;
			}
			set
			{
				this._feewayname = value;
			}
		}

		public int? SecurityLevel
		{
			get
			{
				return this._securitylevel;
			}
			set
			{
				this._securitylevel = value;
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

		public bool? Is_FeeSettled
		{
			get
			{
				return this._is_feesettled;
			}
			set
			{
				this._is_feesettled = value;
			}
		}

		public bool? Is_GuideSheetPrinted
		{
			get
			{
				return this._is_guidesheetprinted;
			}
			set
			{
				this._is_guidesheetprinted = value;
			}
		}

		public bool? Is_GuideSheetReturned
		{
			get
			{
				return this._is_guidesheetreturned;
			}
			set
			{
				this._is_guidesheetreturned = value;
			}
		}

		public DateTime? GuideSheetReturnedDate
		{
			get
			{
				return this._guidesheetreturneddate;
			}
			set
			{
				this._guidesheetreturneddate = value;
			}
		}

		public string GuideSheetReturnedby
		{
			get
			{
				return this._guidesheetreturnedby;
			}
			set
			{
				this._guidesheetreturnedby = value;
			}
		}

		public bool? Is_UseHideCode
		{
			get
			{
				return this._is_usehidecode;
			}
			set
			{
				this._is_usehidecode = value;
			}
		}

		public bool? Is_FinalFinished
		{
			get
			{
				return this._is_finalfinished;
			}
			set
			{
				this._is_finalfinished = value;
			}
		}

		public int? ID_FinalDoctor
		{
			get
			{
				return this._id_finaldoctor;
			}
			set
			{
				this._id_finaldoctor = value;
			}
		}

		public string FinalDoctor
		{
			get
			{
				return this._finaldoctor;
			}
			set
			{
				this._finaldoctor = value;
			}
		}

		public DateTime? FinalDate
		{
			get
			{
				return this._finaldate;
			}
			set
			{
				this._finaldate = value;
			}
		}

		public string FinalOverView
		{
			get
			{
				return this._finaloverview;
			}
			set
			{
				this._finaloverview = value;
			}
		}

		public string FinalConclusion
		{
			get
			{
				return this._finalconclusion;
			}
			set
			{
				this._finalconclusion = value;
			}
		}

		public string ResultCompare
		{
			get
			{
				return this._resultcompare;
			}
			set
			{
				this._resultcompare = value;
			}
		}

		public string MainDiagnose
		{
			get
			{
				return this._maindiagnose;
			}
			set
			{
				this._maindiagnose = value;
			}
		}

		public string FinalDietGuide
		{
			get
			{
				return this._finaldietguide;
			}
			set
			{
				this._finaldietguide = value;
			}
		}

		public string FinalSportGuide
		{
			get
			{
				return this._finalsportguide;
			}
			set
			{
				this._finalsportguide = value;
			}
		}

		public string FinalHealthKnowlage
		{
			get
			{
				return this._finalhealthknowlage;
			}
			set
			{
				this._finalhealthknowlage = value;
			}
		}

		public bool? Is_Checked
		{
			get
			{
				return this._is_checked;
			}
			set
			{
				this._is_checked = value;
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

		public string Checker
		{
			get
			{
				return this._checker;
			}
			set
			{
				this._checker = value;
			}
		}

		public DateTime? CheckedDate
		{
			get
			{
				return this._checkeddate;
			}
			set
			{
				this._checkeddate = value;
			}
		}

		public bool? Is_ReportReceipted
		{
			get
			{
				return this._is_reportreceipted;
			}
			set
			{
				this._is_reportreceipted = value;
			}
		}

		public DateTime? SubScribDate
		{
			get
			{
				return this._subscribdate;
			}
			set
			{
				this._subscribdate = value;
			}
		}

		public DateTime? OperateDate
		{
			get
			{
				return this._operatedate;
			}
			set
			{
				this._operatedate = value;
			}
		}

		public int? ID_Operator
		{
			get
			{
				return this._id_operator;
			}
			set
			{
				this._id_operator = value;
			}
		}

		public string Operator
		{
			get
			{
				return this._operator;
			}
			set
			{
				this._operator = value;
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

		public int? Is_Subscribed
		{
			get
			{
				return this._is_subscribed;
			}
			set
			{
				this._is_subscribed = value;
			}
		}

		public string Invoice
		{
			get
			{
				return this._invoice;
			}
			set
			{
				this._invoice = value;
			}
		}

		public int? ID_UserGuideSheetReturnedBy
		{
			get
			{
				return this._id_userguidesheetreturnedby;
			}
			set
			{
				this._id_userguidesheetreturnedby = value;
			}
		}

		public int? ID_SubScriber
		{
			get
			{
				return this._id_subscriber;
			}
			set
			{
				this._id_subscriber = value;
			}
		}

		public string SubScriber
		{
			get
			{
				return this._subscriber;
			}
			set
			{
				this._subscriber = value;
			}
		}

		public DateTime? SubScriberOperDate
		{
			get
			{
				return this._subscriberoperdate;
			}
			set
			{
				this._subscriberoperdate = value;
			}
		}

		public bool? Is_ExamStarted
		{
			get
			{
				return this._is_examstarted;
			}
			set
			{
				this._is_examstarted = value;
			}
		}

		public string TeamName
		{
			get
			{
				return this._teamname;
			}
			set
			{
				this._teamname = value;
			}
		}

		public int? ID_Gender
		{
			get
			{
				return this._id_gender;
			}
			set
			{
				this._id_gender = value;
			}
		}

		public string GenderName
		{
			get
			{
				return this._gendername;
			}
			set
			{
				this._gendername = value;
			}
		}

		public int? ID_Marriage
		{
			get
			{
				return this._id_marriage;
			}
			set
			{
				this._id_marriage = value;
			}
		}

		public string MarriageName
		{
			get
			{
				return this._marriagename;
			}
			set
			{
				this._marriagename = value;
			}
		}

		public int? NationID
		{
			get
			{
				return this._id_nation;
			}
			set
			{
				this._id_nation = value;
			}
		}

		public string NationName
		{
			get
			{
				return this._nationname;
			}
			set
			{
				this._nationname = value;
			}
		}

		public int? CultrulID
		{
			get
			{
				return this._id_cultrul;
			}
			set
			{
				this._id_cultrul = value;
			}
		}

		public string CultrulName
		{
			get
			{
				return this._cultrulname;
			}
			set
			{
				this._cultrulname = value;
			}
		}

		public int? VocationID
		{
			get
			{
				return this._id_vocation;
			}
			set
			{
				this._id_vocation = value;
			}
		}

		public string VocationName
		{
			get
			{
				return this._vocationname;
			}
			set
			{
				this._vocationname = value;
			}
		}

		public string IDCard
		{
			get
			{
				return this._idcard;
			}
			set
			{
				this._idcard = value;
			}
		}

		public string ExamCard
		{
			get
			{
				return this._examcard;
			}
			set
			{
				this._examcard = value;
			}
		}

		public byte[] Photo
		{
			get
			{
				return this._photo;
			}
			set
			{
				this._photo = value;
			}
		}

		public DateTime? BirthDay
		{
			get
			{
				return this._birthday;
			}
			set
			{
				this._birthday = value;
			}
		}

		public string Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}

		public string MobileNo
		{
			get
			{
				return this._mobileno;
			}
			set
			{
				this._mobileno = value;
			}
		}

		public string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				this._email = value;
			}
		}

		public string Department
		{
			get
			{
				return this._department;
			}
			set
			{
				this._department = value;
			}
		}

		public string DepartSubA
		{
			get
			{
				return this._departsuba;
			}
			set
			{
				this._departsuba = value;
			}
		}

		public string DepartSubB
		{
			get
			{
				return this._departsubb;
			}
			set
			{
				this._departsubb = value;
			}
		}

		public string DepartSubC
		{
			get
			{
				return this._departsubc;
			}
			set
			{
				this._departsubc = value;
			}
		}

		public string IndicatorDiagnose
		{
			get
			{
				return this._indicatordiagnose;
			}
			set
			{
				this._indicatordiagnose = value;
			}
		}

		public string OtherDiagnose
		{
			get
			{
				return this._otherdiagnose;
			}
			set
			{
				this._otherdiagnose = value;
			}
		}

		public string SecondaryDiagnose
		{
			get
			{
				return this._secondarydiagnose;
			}
			set
			{
				this._secondarydiagnose = value;
			}
		}

		public string NormalDiagnose
		{
			get
			{
				return this._normaldiagnose;
			}
			set
			{
				this._normaldiagnose = value;
			}
		}

		public bool? Is_DiseaseLevelInformed
		{
			get
			{
				return this._is_diseaselevelinformed;
			}
			set
			{
				this._is_diseaselevelinformed = value;
			}
		}

		public int? ID_DiseaseLevelInformer
		{
			get
			{
				return this._id_diseaselevelinformer;
			}
			set
			{
				this._id_diseaselevelinformer = value;
			}
		}

		public string DiseaseLevelInformer
		{
			get
			{
				return this._diseaselevelinformer;
			}
			set
			{
				this._diseaselevelinformer = value;
			}
		}

		public DateTime? DiseaseLevelInformedDate
		{
			get
			{
				return this._diseaselevelinformeddate;
			}
			set
			{
				this._diseaselevelinformeddate = value;
			}
		}

		public string DiseaseLevelInformNote
		{
			get
			{
				return this._diseaselevelinformnote;
			}
			set
			{
				this._diseaselevelinformnote = value;
			}
		}
	}
}
