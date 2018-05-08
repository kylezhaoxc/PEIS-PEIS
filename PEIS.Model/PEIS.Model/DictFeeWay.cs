using System;

namespace PEIS.Model
{
	[Serializable]
	public class DictFeeWay
	{
        private int _feewayid;
        private string _feewayname;
        private bool _default;
        private string _inputcode;
        /// <summary>
        /// 
        /// </summary>
        public int FeeWayID
        {
            set { _feewayid = value; }
            get { return _feewayid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FeeWayName
        {
            set { _feewayname = value; }
            get { return _feewayname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Default
        {
            set { _default = value; }
            get { return _default; }
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
