using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Exam
{
	public class ExamItemGroupRelOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "检查项目分组明细设置");
			int @int = base.GetInt("ExamItemGroupID", 0);
			if (@int > 0)
			{
				this.GetEditExamItemGroupInfo(@int, ref vltContext);
			}
		}

		protected void GetEditExamItemGroupInfo(int ID_ExamItemGroup, ref VelocityContext vltContext)
		{
			if (ID_ExamItemGroup > 0)
			{
				PEIS.Model.BusExamItemGroup model = PEIS.BLL.BusExamItemGroup.Instance.GetModel(ID_ExamItemGroup);
				if (null != model)
				{
					vltContext.Put("ID_ExamItemGroup", model.ID_ExamItemGroup);
					vltContext.Put("ExamItemGroupName", model.ExamItemGroupName);
					vltContext.Put("Is_Banned", model.Is_Banned);
					vltContext.Put("Operator", model.Operator);
					vltContext.Put("DispOrder", model.DispOrder);
					vltContext.Put("InputCode", model.InputCode);
					vltContext.Put("ID_BanOpr", model.ID_Operator);
					vltContext.Put("BanDescribe", model.BanDescribe);
					vltContext.Put("OperateDate", model.OperateDate);
					vltContext.Put("Note", model.Note);
				}
			}
		}
	}
}
