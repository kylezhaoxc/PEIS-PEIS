using System;

namespace PEIS.Model
{
	[Serializable]
	public class OnCustExamItemResult
	{
		private int _id_examitemresult;

		private int? _id_custexamitem;

		private int? _id_symptom;

		public int ID_ExamItemResult
		{
			get
			{
				return this._id_examitemresult;
			}
			set
			{
				this._id_examitemresult = value;
			}
		}

		public int? ID_CustExamItem
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

		public int? ID_Symptom
		{
			get
			{
				return this._id_symptom;
			}
			set
			{
				this._id_symptom = value;
			}
		}
	}
}
