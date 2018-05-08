using System;

namespace PEIS.Model
{
	[Serializable]
	public class DictExamPlace
	{
        private int _examplaceid;
        private string _examplacename;
        private bool _default;
        private string _inputcode;
        /// <summary>
        /// 
        /// </summary>
        public int ExamPlaceID
        {
            set { _examplaceid = value; }
            get { return _examplaceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExamPlaceName
        {
            set { _examplacename = value; }
            get { return _examplacename; }
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
