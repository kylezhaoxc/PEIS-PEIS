using System;

namespace PEIS.Model
{
	[Serializable]
	public class DictCultrul
    {
        private int _cultrulid;
        private string _cultrulname;
        private string _inputcode;
        /// <summary>
        /// 
        /// </summary>
        public int CultrulID
        {
            set { _cultrulid = value; }
            get { return _cultrulid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CultrulName
        {
            set { _cultrulname = value; }
            get { return _cultrulname; }
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
