using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.DictExamType
{
	public class DictExamTypeOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("ExamTypeID", 0);
			vltContext.Put("pageTitle", "新增体检类型");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改体检类型");
				this.GetEditExamTypeInfo(@int, ref vltContext);
			}
		}

		protected void GetEditExamTypeInfo(int ExamTypeID, ref VelocityContext vltContext)
		{
			if (ExamTypeID > 0)
			{
				PEIS.Model.DictExamType model = PEIS.BLL.DictExamType.Instance.GetModel(ExamTypeID);
				if (null != model)
				{
					vltContext.Put("ExamTypeID", model.ExamTypeID);
					vltContext.Put("ExamTypeName", model.ExamTypeName);
					vltContext.Put("DocumentID", model.DocumentID);
					vltContext.Put("InputCode", model.InputCode);
				}
			}
		}
	}
}
