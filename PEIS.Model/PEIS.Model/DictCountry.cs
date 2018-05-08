using System;

namespace PEIS.Model
{
	[Serializable]
	public class DictCountry
	{
        private int _countryid;
        private string _countryname;
        private string _inputcode;
        /// <summary>
        /// 
        /// </summary>
        public int CountryID
        {
            set { _countryid = value; }
            get { return _countryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CountryName
        {
            set { _countryname = value; }
            get { return _countryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InputCode
        {
            set { _inputcode = value; }
            get { return _inputcode; }
        }
    }
}
