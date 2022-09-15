using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Employee_System
{
    public partial class EmployeeInfo : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grid();
                DptList();
            }
        }
        private void grid()
        {
            string query = "select *from EmployeeTable";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            empGrid.DataSource = ds;
            empGrid.DataBind();
           
        }

        public void DptList()
        {
            string query = "select *from DepartmentMaster";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DataRow[] rows = new DataRow[dt.Rows.Count];
            dt.Rows.CopyTo(rows, 0);
            var res = from row in rows
                      select new
                      {
                          id = row["Department_Id"].ToString(),
                          dept = row["Department"].ToString()
                      };

            DepartmentListDDL.DataSource = res;

            DepartmentListDDL.DataTextField = "dept";
            DepartmentListDDL.DataValueField = "id";
            DepartmentListDDL.DataBind();
            DepartmentListDDL.Items.Insert(0, new ListItem("---Select Department---", "0"));


        }

        protected void btn_sub_Click(object sender, EventArgs e)
        {

            string ins = "insert into EmployeeTable values(@fname,@lname,@email,@mobileno,@gender,@profile,@Dept_Id_Fk,@department)";
            SqlCommand cmd = new SqlCommand(ins, con);

            cmd.Parameters.AddWithValue("@fname", SqlDbType.VarChar);
            cmd.Parameters["@fname"].Value = EmpFnameTxt.Text;

            cmd.Parameters.AddWithValue("@lname", SqlDbType.VarChar);
            cmd.Parameters["@lname"].Value = EmpLnameTxt.Text;

            cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar);
            cmd.Parameters["@email"].Value = EmpEmailTxt.Text;

            cmd.Parameters.AddWithValue("@mobileno", SqlDbType.Int);
            cmd.Parameters["@mobileno"].Value = EmpMobilenoTxt.Text;


            if (rbtn_male.Checked)
            {
                cmd.Parameters.AddWithValue("@gender", SqlDbType.VarChar);
                cmd.Parameters["@gender"].Value = rbtn_male.Text;
            }
            else
            {
                cmd.Parameters.AddWithValue("@gender", SqlDbType.VarChar);
                cmd.Parameters["@gender"].Value = rbtn_female.Text;
            }

            if (EmpProfile.HasFile)
            {
                EmpProfile.SaveAs(Server.MapPath("~/EmpProfiles/" + EmpProfile.FileName));

                cmd.Parameters.AddWithValue("@profile", SqlDbType.VarChar);
                cmd.Parameters["@profile"].Value = "~/EmpProfiles/" + EmpProfile.FileName;

            }
            else
            {
                Response.Write("<script>alert('Image not selected')</script>");
            }

            cmd.Parameters.AddWithValue("@Dept_Id_Fk", SqlDbType.Int);
            cmd.Parameters["@Dept_Id_Fk"].Value = DepartmentListDDL.SelectedValue;

            cmd.Parameters.AddWithValue("@department", SqlDbType.VarChar);
            cmd.Parameters["@department"].Value = DepartmentListDDL.SelectedItem.Text;

            try
            {
                con.Close();
                con.Open();
                cmd.ExecuteNonQuery();
                /*
                lblMessage.Visible = true;
                lblMessage.Text = "Data Insert Successfully";
                lblMessage.CssClass = "text-success h4";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                */
                Response.Write("<script>alert('Data Inserted')</script>");
                grid();
            }
            catch (SqlException ex)
            {
                Response.Write(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
                con = null;

            }

        }

        protected void empGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "dlt")
            {
                string del = "delete from EmployeeTable where Emp_Id=@id";
                SqlCommand cmd = new SqlCommand(del, con);

                cmd.Parameters.AddWithValue("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = e.CommandArgument;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Data Deleted')</script>");
                    /*
                    lblMessage.Visible = true;
                    lblMessage.Text = "Data Deleted Successfully";
                    lblMessage.CssClass = "text-danger h4";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    */
                    grid();
                }
                catch (SqlException ex)
                {
                    Response.Write(ex);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                    cmd.Dispose();

                }


            }
        }

        
        protected void empGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = empGrid.Rows[e.RowIndex];
            Label id = (Label)row.FindControl("Label1");
            TextBox fname = (TextBox)row.FindControl("txtFName");
            TextBox lname = (TextBox)row.FindControl("txtLName");
            TextBox mobile = (TextBox)row.FindControl("txtMob");
            TextBox email = (TextBox)row.FindControl("txtEmail");
            DropDownList gen = (DropDownList)row.FindControl("ddl_gender");
            FileUpload profile = (FileUpload)row.FindControl("fuProfile");
            Label lblEditProfile = (Label)row.FindControl("lblEditProfile");
            DropDownList dept = (DropDownList)row.FindControl("ddl_department");

            string ud = "update EmployeeTable set Emp_Firstname=@f1,Emp_Lastname=@f2,Emp_Email=@f3,Emp_MobileNo=@f4,Emp_Gender=@f5,Emp_Profile=@f6,Dept_Id_Fk=@f7,Department=@f8 where Emp_Id=@id";
            SqlCommand cmd = new SqlCommand(ud, con);

            
            cmd.Parameters.AddWithValue("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = Convert.ToInt32(id.Text);

            cmd.Parameters.AddWithValue("@f1", SqlDbType.VarChar);
            cmd.Parameters["@f1"].Value = fname.Text;

            cmd.Parameters.AddWithValue("@f2", SqlDbType.VarChar);
            cmd.Parameters["@f2"].Value = lname.Text;

            cmd.Parameters.AddWithValue("@f3", SqlDbType.VarChar);
            cmd.Parameters["@f3"].Value = email.Text;

            cmd.Parameters.AddWithValue("@f4", SqlDbType.Int);
            cmd.Parameters["@f4"].Value = Convert.ToInt64(mobile.Text);


            cmd.Parameters.AddWithValue("@f5", SqlDbType.VarChar);
            cmd.Parameters["@f5"].Value = gen.SelectedItem.Text;

            if (profile.HasFile)
            {
                profile.SaveAs(Server.MapPath("~/EmpProfiles/" + profile.FileName));

                cmd.Parameters.AddWithValue("@f6", SqlDbType.VarChar);
                cmd.Parameters["@f6"].Value = "~/EmpProfiles/" + profile.FileName;
            }
            else
            {
                cmd.Parameters.AddWithValue("@f6", SqlDbType.VarChar);
                cmd.Parameters["@f6"].Value = lblEditProfile.Text;
            }

            cmd.Parameters.AddWithValue("@f7", SqlDbType.Int);
            cmd.Parameters["@f7"].Value = dept.SelectedItem.Value;

            cmd.Parameters.AddWithValue("@f8", SqlDbType.VarChar);
            cmd.Parameters["@f8"].Value = dept.SelectedItem.Text;


            try
            {
                con.Close();
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Data Updated')</script>");
                empGrid.EditIndex = -1;
                grid();
            }
            catch (SqlException ex)
            {
                Response.Write(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
                con = null;
                cmd = null;
            }
        }
        protected void empGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            empGrid.EditIndex = e.NewEditIndex;
            grid();

            DropDownList dept_ddl = empGrid.Rows[e.NewEditIndex].FindControl("ddl_department") as DropDownList;
            Label dept_lbl = empGrid.Rows[e.NewEditIndex].FindControl("lblDepartment") as Label;

            //bind DDL in gridview
            string query = "select *from DepartmentMaster";
            SqlCommand cmd2 = new SqlCommand(query, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DataRow[] rows = new DataRow[dt.Rows.Count];
            dt.Rows.CopyTo(rows, 0);
            var res = from row2 in rows
                      select new
                      {
                          id = row2["Department_Id"].ToString(),
                          dept = row2["Department"].ToString()
                      };

            dept_ddl.DataSource = res;
            dept_ddl.DataTextField = "dept";
            dept_ddl.DataValueField = "id";
            dept_ddl.DataBind();
            dept_ddl.Items.Insert(0, new ListItem("---- Select Department ----", "0"));
       
            if (dept_ddl.Items.FindByText(dept_lbl.Text)!=null)
            {
                dept_ddl.SelectedItem.Text = dept_lbl.Text;
            }
         
        }

        protected void empGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            empGrid.EditIndex = -1;
            grid();
        }


        protected void srchBtn_Click(object sender, EventArgs e)
        {
            string query = "select *from EmployeeTable where Emp_Firstname LIKE '%" + searchTxt.Text + "%'";
          
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Adpt.Fill(dt);
            empGrid.DataSource = dt;
            empGrid.DataBind();
        }

        protected void searchTxt_TextChanged(object sender, EventArgs e)
        {
            string query = "select *from EmployeeTable where Emp_Firstname LIKE '%" + searchTxt.Text + "%'";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Adpt.Fill(dt);
            empGrid.DataSource = dt;
            empGrid.DataBind();
        }
    }
}