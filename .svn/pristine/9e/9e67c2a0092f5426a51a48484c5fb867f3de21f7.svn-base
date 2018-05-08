using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Exam
{
	public class ExamOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("ExamItemID", 0);
			vltContext.Put("pageTitle", "新增检查项目");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改检查项目");
				this.GetEditExamItemInfo(@int, ref vltContext);
			}
		}

		protected void GetEditExamItemInfo(int ID_ExamItem, ref VelocityContext vltContext)
		{
			PEIS.Model.BusExamItem model = PEIS.BLL.BusExamItem.Instance.GetModel(ID_ExamItem);
			vltContext.Put("ID_ExamItem", model.ID_ExamItem);
			vltContext.Put("ExamItemName", model.ExamItemName);
			vltContext.Put("ID_Section", model.ID_Section);
			vltContext.Put("GetResultWay", model.GetResultWay);
			vltContext.Put("ExamItemCode", model.ExamItemCode);
			vltContext.Put("Is_LisValueNull", model.Is_LisValueNull);
			vltContext.Put("Is_EntrySectSum", model.Is_EntrySectSum);
			vltContext.Put("EntrySectSumLevel", model.EntrySectSumLevel);
			vltContext.Put("Is_AutoCalc", model.Is_AutoCalc);
			vltContext.Put("CalcExpression", model.CalcExpression);
			vltContext.Put("SymCols", model.SymCols);
			vltContext.Put("TextboxRows", model.TextboxRows);
			vltContext.Put("Is_SameRow", model.Is_SameRow);
			vltContext.Put("ExamItemUnit", model.ExamItemUnit);
			vltContext.Put("MaleLoLimit", model.MaleLoLimit);
			vltContext.Put("MaleHiLimit", model.MaleHiLimit);
			vltContext.Put("FemaleLoLimit", model.FemaleLoLimit);
			vltContext.Put("FemaleHiLimit", model.FemaleHiLimit);
			vltContext.Put("Is_SymMultiValue", model.Is_SymMultiValue);
			vltContext.Put("Forsex", model.Forsex);
			vltContext.Put("Note", model.Note);
			vltContext.Put("DispOrder", model.DispOrder);
			vltContext.Put("AbbrExamName", model.AbbrExamName);
			vltContext.Put("Is_ExamItemNonPrintInReport", model.Is_ExamItemNonPrintInReport);
			string sectionName = CommonSystemInfo.Instance.GetSectionName(int.Parse(model.ID_Section.ToString()));
			vltContext.Put("SectionName", sectionName);
		}
	}
}
