using System;

namespace PEIS.Model
{
	[Serializable]
	public class SysMenu
    {
        private int _menuid;
        private string _menuname;
        private int? _parentid;
        private string _url;
        private int? _disporder;
        private int? _menulevel;
        private string _rightcode;
        private bool _is_combinewithsection;
        /// <summary>
        /// 
        /// </summary>
        public int MenuID
        {
            set { _menuid = value; }
            get { return _menuid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MenuName
        {
            set { _menuname = value; }
            get { return _menuname; }
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
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DispOrder
        {
            set { _disporder = value; }
            get { return _disporder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MenuLevel
        {
            set { _menulevel = value; }
            get { return _menulevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RightCode
        {
            set { _rightcode = value; }
            get { return _rightcode; }
        }

        public bool Is_CombineWithSection
        {
            set { _is_combinewithsection = value; }
            get { return _is_combinewithsection; }
        }
    }
}
