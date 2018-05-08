using System;

namespace PEIS.Model
{
    [Serializable]
    public class SYSRight
    {
        private int _rightid;
        private string _rightname;
        private string _rightcode;
        private int _disporder;
        private int _is_locked;
        private int? _parentid;
        private string _remark;
        private int? _rightlevel;
        private int? _OperatorID;
        private DateTime _CreateDate;
        /// <summary>
        /// 
        /// </summary>
        public int RightID
        {
            set { _rightid = value; }
            get { return _rightid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RightName
        {
            set { _rightname = value; }
            get { return _rightname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RightCode
        {
            set { _rightcode = value; }
            get { return _rightcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DispOrder
        {
            set { _disporder = value; }
            get { return _disporder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Is_Locked
        {
            set { _is_locked = value; }
            get { return _is_locked; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RightLevel
        {
            set { _rightlevel = value; }
            get { return _rightlevel; }
        }

        public int? OperatorID
        {
            set { _OperatorID = value; }
            get { return _OperatorID; }
        }
        public DateTime CreateDate
        {
            set { _CreateDate = value; }
            get { return _CreateDate; }
        }
    }
}
