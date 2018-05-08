using System;

namespace PEIS.Common
{
	public class ExcelModel
	{
		private string _key = string.Empty;

		private string _value = string.Empty;

		private int is_visible = 1;

		public string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		public int Is_visible
		{
			get
			{
				return this.is_visible;
			}
			set
			{
				this.is_visible = value;
			}
		}
	}
}
