using System;

namespace PEIS.Model
{
	[Serializable]
	public class SYSUserSection
	{
        private int _id_usersection;
        private int? _sectionid;
        private int? _userid;
        private int? _operatorid;
        private DateTime? _createdate;
        /// <summary>
        /// 
        /// </summary>
        public int UserSectionID
        {
            set { _id_usersection = value; }
            get { return _id_usersection; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SectionID
        {
            set { _sectionid = value; }
            get { return _sectionid; }
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
        public int? OperatorID
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
    }
}
