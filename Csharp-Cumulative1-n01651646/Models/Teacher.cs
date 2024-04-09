using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Csharp_Cumulative1_n01651646.Models
{
    public class Teacher
    {
        //The following fields are the various details for the Teachers
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string EmployeeNumber;
        public DateTime HireDate;
        public string Salary;

        public bool IsValid()
        {
            bool valid = true;

            if (TeacherFname == null || TeacherLname == null)
            {
 
                valid = false;
            }
            else
            {
 
                if (TeacherFname.Length < 2 || TeacherFname.Length > 255) valid = false;
                if (TeacherLname.Length < 2 || TeacherFname.Length > 255) valid = false;

            }
            Debug.WriteLine("The model validity is : " + valid);

            return valid;
        }

        public Teacher() { }
    }
}
