using System;

namespace PEIS.Model
{
	[Serializable]
	public class DictMarriage
	{
        private int _marriageid;
        private string _marriagename;
        private int _ismarried;
        private string _inputcode;
        /// <summary>
        /// 
        /// </summary>
        public int MarriageID
        {
            set { _marriageid = value; }
            get { return _marriageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MarriageName
        {
            set { _marriagename = value; }
            get { return _marriagename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsMarried
        {
            set { _ismarried = value; }
            get { return _ismarried; }
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
