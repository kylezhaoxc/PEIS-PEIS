using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PEIS.Web.System.Customer
{
	public class OCXTestForm : Page
	{
		protected HtmlForm form1;

		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected Button Button1;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Code128c("11250620130047", 100);
		}

        public Bitmap Code128c(string barString, int height)
        {
            Bitmap bitmap = new Bitmap(barString.Length, height, PixelFormat.Format24bppRgb);
            try
            {
                char[] chArray = barString.ToCharArray();
                for (int i = 0; i < chArray.Length; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (chArray[i] == 'b')
                        {
                            bitmap.SetPixel(i, j, Color.Black);
                        }
                        else
                        {
                            bitmap.SetPixel(i, j, Color.White);
                        }
                    }
                }
                bitmap.Save(@"c:\aa.jpg");
                return bitmap;
            }
            catch
            {
                return null;
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
		{
			base.Response.Clear();
		}
	}
}
