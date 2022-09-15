using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Employee_MVC.Models
{
    public class EmployeeModel
    {
        public int Emp_Id { get; set; }

        [Required(ErrorMessage ="Required")]
        public string Emp_Firstname { get; set; }

        [Required(ErrorMessage = "Required")]
        public  string Emp_Lastname { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Emp_Department { get; set; }

        [Required(ErrorMessage = "Required")]

        //[RegularExpression(@"^{10}$", ErrorMessage = "Invalid Mobile Number.")]
        public string Emp_MobileNo{ get; set; }

        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage ="Please enter correct email address")]
        public string Emp_Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Emp_Profile { get; set; }

        public int Dept_Id_Fk { get; set; }

        DepartmentModel dprt = new DepartmentModel();

        public DepartmentModel dprts
        {
            get { return this.dprt; }
            set { dprt = value; }
        }

    }

}