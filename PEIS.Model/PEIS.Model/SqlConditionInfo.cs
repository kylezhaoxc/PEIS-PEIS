using System;

namespace PEIS.Model
{
	public class SqlConditionInfo
	{
		private string _pname;

		private object _pvalue;

		private TypeCode _ptype;

		private int _blur = 0;

		private string _poper;

		private int _place = 3;

		private bool _IsSelf = false;

		public string ParamName
		{
			get
			{
				return this._pname;
			}
		}

		public object ParamValue
		{
			get
			{
				return this._pvalue;
			}
		}

		public TypeCode ParamType
		{
			get
			{
				return this._ptype;
			}
		}

		public int Blur
		{
			get
			{
				return this._blur;
			}
			set
			{
				this._blur = value;
			}
		}

		public string ParamOper
		{
			get
			{
				return this._poper;
			}
			set
			{
				this._poper = value;
			}
		}

		public int Place
		{
			get
			{
				return this._place;
			}
			set
			{
				this._place = value;
			}
		}

		public bool IsSelf
		{
			get
			{
				return this._IsSelf;
			}
			set
			{
				this._IsSelf = value;
			}
		}

		public SqlConditionInfo(string paramname, object paramvalue, TypeCode paramtype)
		{
			this._pname = paramname;
			this._pvalue = paramvalue;
			this._ptype = paramtype;
		}
	}
}
