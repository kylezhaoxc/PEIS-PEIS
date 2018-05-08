using System;

namespace PEIS.Model
{
	[Serializable]
	public class DictNation
	{
        private int _nationid;
        private string _nationname;
        private string _inputcode;
        /// <summary>
        /// 
        /// </summary>
        public int NationID
        {
            set { _nationid = value; }
            get { return _nationid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NationName
        {
            set { _nationname = value; }
            get { return _nationname; }
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
