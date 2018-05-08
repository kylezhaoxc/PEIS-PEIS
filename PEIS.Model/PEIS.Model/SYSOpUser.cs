using System;

namespace PEIS.Model
{
    /// <summary>
    /// SYSOpUser:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SYSOpUser
    {
      
        #region Model
        private int _userid;
        private int? _sectionid;
        private string _username;
        private string _loginname;
        private string _password;
        private DateTime? _lastlogintime;
        private string _note;
        private DateTime? _disdate;
        private byte[] _signature;
        private decimal? _discountrate;
        private int? _sex;
        private int? _is_del;
        private int? _vocationtype;
        private int? _operatelevel;
        private int? _totalnum;
        private int? _failcount;
        private DateTime _createtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
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
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoginName
        {
            set { _loginname = value; }
            get { return _loginname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PassWord
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DisDate
        {
            set { _disdate = value; }
            get { return _disdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Signature
        {
            set { _signature = value; }
            get { return _signature; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DisCountRate
        {
            set { _discountrate = value; }
            get { return _discountrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Is_Del
        {
            set { _is_del = value; }
            get { return _is_del; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? VocationType
        {
            set { _vocationtype = value; }
            get { return _vocationtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OperateLevel
        {
            set { _operatelevel = value; }
            get { return _operatelevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TotalNum
        {
            set { _totalnum = value; }
            get { return _totalnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FailCount
        {
            set { _failcount = value; }
            get { return _failcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }
}

