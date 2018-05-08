using System;

namespace PEIS.Model
{
	[Serializable]
	public class SYSSection
	{
        private int _sectionid;
        private string _sectionname;
        private bool _functiontype;
        private string _displaymenu;
        private bool _isdel;
        private string _interfacetype;
        private bool _isnotsamepage;
        private bool _isnonprintsectsummary;
        private bool _isowninterface;
        private bool _isautoapprove;
        private string _imagetype;
        private string _picprintsetup;
        private string _pacsinterfaceflag;
        private string _inputcode;
        private int? _disporder;
        private string _summaryname;
        private string _defaultsummary;
        private string _sepbetweenexamitems;
        private string _sepbetweensymptoms;
        private string _terminalsymbol;
        private string _sepexamandvalue;
        private string _nobetweenexamitems;
        private string _nobetweensympotms;
        private string _note;
        private bool _isnoentryfinalsummary;
        private bool _isnonprintinreport;
        private int? _isprintbarcode = 0;
        private string _displaymenu2;
        /// <summary>
        /// 
        /// </summary>
        public int SectionID
        {
            set { _sectionid = value; }
            get { return _sectionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SectionName
        {
            set { _sectionname = value; }
            get { return _sectionname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool FunctionType
        {
            set { _functiontype = value; }
            get { return _functiontype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayMenu
        {
            set { _displaymenu = value; }
            get { return _displaymenu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InterfaceType
        {
            set { _interfacetype = value; }
            get { return _interfacetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsNotSamePage
        {
            set { _isnotsamepage = value; }
            get { return _isnotsamepage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsNonPrintSectSummary
        {
            set { _isnonprintsectsummary = value; }
            get { return _isnonprintsectsummary; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsOwnInterface
        {
            set { _isowninterface = value; }
            get { return _isowninterface; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsAutoApprove
        {
            set { _isautoapprove = value; }
            get { return _isautoapprove; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImageType
        {
            set { _imagetype = value; }
            get { return _imagetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PicPrintSetup
        {
            set { _picprintsetup = value; }
            get { return _picprintsetup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PacsInterfaceFlag
        {
            set { _pacsinterfaceflag = value; }
            get { return _pacsinterfaceflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InputCode
        {
            set { _inputcode = value; }
            get { return _inputcode; }
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
        public string SummaryName
        {
            set { _summaryname = value; }
            get { return _summaryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DefaultSummary
        {
            set { _defaultsummary = value; }
            get { return _defaultsummary; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SepBetweenExamItems
        {
            set { _sepbetweenexamitems = value; }
            get { return _sepbetweenexamitems; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SepBetweenSymptoms
        {
            set { _sepbetweensymptoms = value; }
            get { return _sepbetweensymptoms; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TerminalSymbol
        {
            set { _terminalsymbol = value; }
            get { return _terminalsymbol; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SepExamAndValue
        {
            set { _sepexamandvalue = value; }
            get { return _sepexamandvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NoBetweenExamItems
        {
            set { _nobetweenexamitems = value; }
            get { return _nobetweenexamitems; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NoBetweenSympotms
        {
            set { _nobetweensympotms = value; }
            get { return _nobetweensympotms; }
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
        public bool IsNoEntryFinalSummary
        {
            set { _isnoentryfinalsummary = value; }
            get { return _isnoentryfinalsummary; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsNonPrintInReport
        {
            set { _isnonprintinreport = value; }
            get { return _isnonprintinreport; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsPrintBarCode
        {
            set { _isprintbarcode = value; }
            get { return _isprintbarcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayMenu2
        {
            set { _displaymenu2 = value; }
            get { return _displaymenu2; }
        }
    }
}
