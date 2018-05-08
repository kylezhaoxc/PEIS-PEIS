using System;

namespace PEIS.Model
{
	[Serializable]
	public class OnCustApply
	{
		private string _id_apply;

		private int? _id_section;

		private long? _id_customer;

		private string _applytitle;

		private string _specimenname;

		private string _batchnumber;

		private string _sectionname;

		private string _deptname;

		private string _examnumber;

		private DateTime? _acquisitiontime;

		private DateTime? _recvtime;

		private DateTime? _reporttime;

		private string _applydoctorname;

		private string _detectiondoctorname;

		private string _checkdoctorname;

		private DateTime? _createtime;

		private string _examitems;

		private string _sendprojectids;

		private string _sendgroupparams;

		private string _specimenno;

		private bool? _is_typistfinish;

		private int? _id_typist;

		private string _typistname;

		private DateTime? _typistdate;

		private int? _id_detectiondoctor;

		public string ID_Apply
		{
			get
			{
				return this._id_apply;
			}
			set
			{
				this._id_apply = value;
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

		public string ApplyTitle
		{
			get
			{
				return this._applytitle;
			}
			set
			{
				this._applytitle = value;
			}
		}

		public string SpecimenName
		{
			get
			{
				return this._specimenname;
			}
			set
			{
				this._specimenname = value;
			}
		}

		public string BatchNumber
		{
			get
			{
				return this._batchnumber;
			}
			set
			{
				this._batchnumber = value;
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

		public string DeptName
		{
			get
			{
				return this._deptname;
			}
			set
			{
				this._deptname = value;
			}
		}

		public string ExamNumber
		{
			get
			{
				return this._examnumber;
			}
			set
			{
				this._examnumber = value;
			}
		}

		public DateTime? AcquisitionTime
		{
			get
			{
				return this._acquisitiontime;
			}
			set
			{
				this._acquisitiontime = value;
			}
		}

		public DateTime? RecvTime
		{
			get
			{
				return this._recvtime;
			}
			set
			{
				this._recvtime = value;
			}
		}

		public DateTime? ReportTime
		{
			get
			{
				return this._reporttime;
			}
			set
			{
				this._reporttime = value;
			}
		}

		public string ApplyDoctorName
		{
			get
			{
				return this._applydoctorname;
			}
			set
			{
				this._applydoctorname = value;
			}
		}

		public string DetectionDoctorName
		{
			get
			{
				return this._detectiondoctorname;
			}
			set
			{
				this._detectiondoctorname = value;
			}
		}

		public string CheckDoctorName
		{
			get
			{
				return this._checkdoctorname;
			}
			set
			{
				this._checkdoctorname = value;
			}
		}

		public DateTime? CreateTime
		{
			get
			{
				return this._createtime;
			}
			set
			{
				this._createtime = value;
			}
		}

		public string ExamItems
		{
			get
			{
				return this._examitems;
			}
			set
			{
				this._examitems = value;
			}
		}

		public string SendProjectIDs
		{
			get
			{
				return this._sendprojectids;
			}
			set
			{
				this._sendprojectids = value;
			}
		}

		public string SendGroupParams
		{
			get
			{
				return this._sendgroupparams;
			}
			set
			{
				this._sendgroupparams = value;
			}
		}

		public string SpecimenNo
		{
			get
			{
				return this._specimenno;
			}
			set
			{
				this._specimenno = value;
			}
		}

		public bool? Is_TypistFinish
		{
			get
			{
				return this._is_typistfinish;
			}
			set
			{
				this._is_typistfinish = value;
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

		public int? ID_DetectionDoctor
		{
			get
			{
				return this._id_detectiondoctor;
			}
			set
			{
				this._id_detectiondoctor = value;
			}
		}
	}
}
