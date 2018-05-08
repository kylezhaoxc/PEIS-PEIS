using System;

namespace PEIS.Model
{
	[Serializable]
	public class SYSUserRole
	{
        private int _userroleid;
        private int? _roleid;
        private int? _userid;
        private DateTime? _createdate;
        private int? _operatorid;
        /// <summary>
        /// 
        /// </summary>
        public int UserRoleID
        {
            set { _userroleid = value; }
            get { return _userroleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OperatorID
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
    }
}
