using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Exam
{
	public class SymptomOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("SymptomID", 0);
			int int2 = base.GetInt("ExamItemID", 0);
			vltContext.Put("ID_ExamItem", int2);
			vltContext.Put("pageTitle", "新增体征词");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改体征词");
				this.GetEditSymptomItemInfo(@int, ref vltContext);
			}
		}

		protected void GetEditSymptomItemInfo(int ID_Symptom, ref VelocityContext vltContext)
		{
			PEIS.Model.BusSymptom model = PEIS.BLL.BusSymptom.Instance.GetModel(ID_Symptom);
			if (model != null)
			{
				vltContext.Put("ID_Symptom", model.ID_Symptom);
				vltContext.Put("ID_ExamItem", model.ID_ExamItem);
				vltContext.Put("SymptomName", model.SymptomName);
				vltContext.Put("ID_Conclusion", model.ID_Conclusion);
				vltContext.Put("DiseaseLevel", model.DiseaseLevel);
				vltContext.Put("NumOperSign", model.NumOperSign);
				vltContext.Put("NumMale", model.NumMale);
				vltContext.Put("NumFemale", model.NumFemale);
				vltContext.Put("Is_Default", model.Is_Default);
				vltContext.Put("SymptomDescribe", model.SymptomDescribe);
				vltContext.Put("DispOrder", model.DispOrder);
				vltContext.Put("Is_Banned", model.Is_Banned);
				if (model.ID_Conclusion.HasValue)
				{
					string conclusionName = CommonConclusion.Instance.GetConclusionName(int.Parse(model.ID_Conclusion.ToString()));
					vltContext.Put("ConclusionName", conclusionName);
				}
			}
		}
	}
}
