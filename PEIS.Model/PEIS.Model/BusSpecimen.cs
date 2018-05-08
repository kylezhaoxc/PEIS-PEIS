using System;

namespace PEIS.Model
{
	[Serializable]
	public class BusSpecimen
	{
		private int _id_specimen;

		private string _specimenname;

		private string _inputcode;

		private int _disporder;

		private string _lisspecimenname;

		public int ID_Specimen
		{
			get
			{
				return this._id_specimen;
			}
			set
			{
				this._id_specimen = value;
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

		public string LisSpecimenName
		{
			get
			{
				return this._lisspecimenname;
			}
			set
			{
				this._lisspecimenname = value;
			}
		}
	}
}
