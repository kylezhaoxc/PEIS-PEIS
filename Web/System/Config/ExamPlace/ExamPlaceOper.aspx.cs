using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.ExamPlace
{
	public class ExamPlaceOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("ExamPlaceID", 0);
			vltContext.Put("pageTitle", "新增体检地址");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改体检地址");
				this.GetEditExamPlaceInfo(@int, ref vltContext);
			}
		}

		protected void GetEditExamPlaceInfo(int ExamPlaceID, ref VelocityContext vltContext)
		{
			if (ExamPlaceID > 0)
			{
				PEIS.Model.DictExamPlace model = PEIS.BLL.DictExamPlace.Instance.GetModel(ExamPlaceID);
				if (null != model)
				{
					vltContext.Put("ExamPlaceID", model.ExamPlaceID);
					vltContext.Put("ExamPlaceName", model.ExamPlaceName);
					vltContext.Put("InputCode", model.InputCode);
					vltContext.Put("Default", model.Default);
				}
			}
		}
	}
}
