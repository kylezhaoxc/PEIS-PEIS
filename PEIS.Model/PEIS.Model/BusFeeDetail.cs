using System;

namespace PEIS.Model
{
	[Serializable]
	public class BusFeeDetail
	{
		private int _id_dtlfee;

		private int? _id_fee;

		private int? _id_examitem;

		public int ID_DtlFee
		{
			get
			{
				return this._id_dtlfee;
			}
			set
			{
				this._id_dtlfee = value;
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

		public int? ID_ExamItem
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
	}
}
