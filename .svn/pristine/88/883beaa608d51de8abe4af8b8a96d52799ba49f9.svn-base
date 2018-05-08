using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.ICDTen
{
	public class ICDTenOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("ICDID", 0);
			vltContext.Put("pageTitle", "新增ICD");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改ICD");
				this.GetEditICDInfo(@int, ref vltContext);
			}
		}

		protected void GetEditICDInfo(int ID_ICD, ref VelocityContext vltContext)
		{
			if (ID_ICD > 0)
			{
				PEIS.Model.DctICDTen model = PEIS.BLL.DctICDTen.Instance.GetModel(ID_ICD);
				if (null != model)
				{
					vltContext.Put("ID_ICD", model.ID_ICD);
					vltContext.Put("ICDCNName", model.ICDCNName);
					vltContext.Put("ICDENName", model.ICDENName);
					vltContext.Put("Code", model.Code);
					vltContext.Put("Codea", model.Codea);
					vltContext.Put("LevelA", model.LevelA);
					vltContext.Put("LevelB", model.LevelB);
					vltContext.Put("LevelC", model.LevelC);
					vltContext.Put("LevelD", model.LevelD);
					vltContext.Put("LevelE", model.LevelE);
					vltContext.Put("LevelTree", model.LevelTree);
					vltContext.Put("Class", model.Class);
					vltContext.Put("Tag", model.Tag);
					vltContext.Put("Note", model.Note);
					vltContext.Put("ICDtoSection", model.ICDtoSection);
					vltContext.Put("Is_Banned", model.Is_Banned);
					vltContext.Put("BanDate", model.BanDate);
					vltContext.Put("BanOperator", model.BanOperator);
					vltContext.Put("BanDescribe", model.BanDescribe);
				}
			}
		}
	}
}
