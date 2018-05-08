using System;

namespace PEIS.Model
{
	[Serializable]
	public class DictReceiveReportWay
	{
        private int _reportwayid;
        private string _reportwayname;
        private bool _default;
        private string _inputcode;
        /// <summary>
        /// 
        /// </summary>
        public int ReportWayID
        {
            set { _reportwayid = value; }
            get { return _reportwayid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReportWayName
        {
            set { _reportwayname = value; }
            get { return _reportwayname; }
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
