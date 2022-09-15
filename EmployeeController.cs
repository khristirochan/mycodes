using Employee_MVC.DataAccessLayer;
using Employee_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        EmployeeDAL empDAL = new EmployeeDAL();
        public ActionResult List()
        {
            var empData = empDAL.GetEmployee();
            return View(empData);
        }
        public ActionResult Create()
        {
            ViewBag.Departments = empDAL.GetDepartment();

            return View();
       
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel empModel,FormCollection formcollection, HttpPostedFileBase Emp_Profile)
        {
             
            if (Emp_Profile.ContentLength > 0)
            {
                Emp_Profile.SaveAs(Server.MapPath("../UploadedFiles/" + Emp_Profile.FileName));
                string profile = "../UploadedFiles/" + Emp_Profile.FileName;
                empModel.Emp_Profile = profile;
            }
            else
            {
                Response.Write("<script>alert('Image not selected')</script>");
            }


            empModel.Dept_Id_Fk = empModel.dprts.Department_Id;
            empModel.Emp_Department = empModel.dprts.Department;
  
            if (empDAL.InsertEmployee(empModel))
            {
                TempData["insertmsg"] = "<script>alert('Data Inserted.')</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["inserErrtmsg"] = "<script>alert('Data Not Inserted.')</script>";
            }
            return View();
          
        }

        public ActionResult Edit(int id)
        {
            var empData = empDAL.GetEmployee().Find(a => a.Emp_Id == id);
            return View(empData);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel empModel, HttpPostedFileBase Emp_Profile)
        {
            if (Emp_Profile.FileName != null)
            {
                Emp_Profile.SaveAs(Server.MapPath("~/UploadedFiles/" + Emp_Profile.FileName));
                string profile = "../UploadedFiles/" + Emp_Profile.FileName;
                empModel.Emp_Profile = profile;
            }

            if (empDAL.UpdateEmployee(empModel))
            {
                TempData["editmsg"] = "<script>alert('Data Updated.')</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["editErrmsg"] = "<script>alert('Data Not Updated.')</script>";
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            int r = empDAL.DeleteEmployee(id);
            if (r > 0)
            {
                TempData["deletemsg"] = "<script>alert('Data Deleted.')</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["deleteErrmsg"] = "<script>alert('Data Not Deleted.')</script>";
            }
            return View();
        }

    }
}