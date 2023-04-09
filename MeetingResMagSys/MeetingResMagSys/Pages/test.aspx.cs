using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MeetingResMagSys.Pages
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            
            if (FileUpload1.HasFile)
            {
                //FileUpload1.SaveAs(Server.MapPath("~/UploadFile/") + FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/image/") + FileUpload1.FileName);
                Label1.Text = "上传成功！";
            }
            
        }
    }
}