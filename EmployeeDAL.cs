using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using Employee_MVC.Models;
using System.Data;

namespace Employee_MVC.DataAccessLayer
{
    public class EmployeeDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        //Get Employees
        public List<EmployeeModel> GetEmployee()
        {
            cmd = new SqlCommand("Select *from EmployeeMaster", con);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
          

            List<EmployeeModel> EmpList = new List<EmployeeModel>();
            foreach (DataRow dr in dt.Rows)
            {

                EmpList.Add(new EmployeeModel
                    {
                        Emp_Id=Convert.ToInt32(dr["Emp_Id"]),
                        Emp_Firstname = Convert.ToString(dr["Emp_Firstname"]),
                        Emp_Lastname = Convert.ToString(dr["Emp_Lastname"]),
                        Emp_Department = Convert.ToString(dr["Emp_Department"]),
                        Emp_MobileNo = Convert.ToString(dr["Emp_MobileNo"]),
                        Emp_Email = Convert.ToString(dr["Emp_Email"]),
                        Emp_Profile = Convert.ToString(dr["Emp_Profile"])
                });
            }

            
            return EmpList;
        }

        //Insert Employee
        public bool InsertEmployee(EmployeeModel empModel)
        {
            string query = "select * from DepartmentMaster where department_Id = @dept_id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@dept_id", Convert.ToInt32(empModel.Emp_Department));

           
            SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Adpt.Fill(dt);
           
            foreach (DataRow dr in dt.Rows)
            {
                empModel.Dept_Id_Fk = Convert.ToInt32(dr["Department_Id"]);
                empModel.Emp_Department = Convert.ToString(dr["Department"]);
            }
            

            /*
            cmd = new SqlCommand("select *from DepartmentMaster where department_Id=@dept_id",con);
            cmd.Parameters.AddWithValue("@dept_id", Convert.ToInt32(empModel.Emp_Department));
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                //string id = Convert.ToString(dr["Department_Id"]);
                string dname = Convert.ToString(dr["Department"]);
            }
            */


            cmd = new SqlCommand("insert into EmployeeMaster values(@firstname,@lastname,@department,@mobileno,@email,@deptidfk,@profile)", con);
            cmd.Parameters.AddWithValue("@firstname", empModel.Emp_Firstname);  
            cmd.Parameters.AddWithValue("@lastname", empModel.Emp_Lastname);

            cmd.Parameters.AddWithValue("@department", empModel.Emp_Department);

            cmd.Parameters.AddWithValue("@mobileno", empModel.Emp_MobileNo);
            cmd.Parameters.AddWithValue("@email", empModel.Emp_Email);

            cmd.Parameters.AddWithValue("@deptidfk", empModel.Dept_Id_Fk);
            cmd.Parameters.AddWithValue("@profile",empModel.Emp_Profile);
            con.Open();
            int r = cmd.ExecuteNonQuery();
            if(r>0)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }
        //Update
        public bool UpdateEmployee(EmployeeModel empModel)
        {

            cmd = new SqlCommand("update EmployeeMaster set Emp_Firstname=@firstname, Emp_Lastname=@lastname, Emp_MobileNo=@mobileno, Emp_Email=@email, Emp_Profile=@profile where Emp_Id=@id", con);
            cmd.Parameters.AddWithValue("@id", empModel.Emp_Id);
            cmd.Parameters.AddWithValue("@firstname", empModel.Emp_Firstname);
            cmd.Parameters.AddWithValue("@lastname", empModel.Emp_Lastname);
            cmd.Parameters.AddWithValue("@mobileno", empModel.Emp_MobileNo);
            cmd.Parameters.AddWithValue("@email", empModel.Emp_Email);
            cmd.Parameters.AddWithValue("@profile", empModel.Emp_Profile);


            con.Open();
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Delete
        public int DeleteEmployee(int id)
        {
            //EmployeeModel empModel = new EmployeeModel();
            cmd = new SqlCommand("delete from EmployeeMaster where Emp_Id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
  
            con.Open();

            return cmd.ExecuteNonQuery();
        }

        //Get Department
        public List<DepartmentModel> GetDepartment()
        {
            con.Open();
            cmd = new SqlCommand("Select *from DepartmentMaster", con);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);


            List<DepartmentModel> DeptList = new List<DepartmentModel>();
            foreach (DataRow dr in dt.Rows)
            {

                DeptList.Add(new DepartmentModel
                {
                    Department_Id = Convert.ToInt32(dr["Department_Id"]),
                    Department = Convert.ToString(dr["Department"])
                });
            }
            return DeptList;
        }


    }
}