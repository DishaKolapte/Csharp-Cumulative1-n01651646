using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Csharp_Cumulative1_n01651646.Models
{
    public class Student
    {
        //The following fields are the various details for the Students
        public int StudentId;
        public string StudentFname;
        public string StudentLname;
        public string StudentNumber;
        public DateTime EnrolDate;

        public bool IsValid()
        {
            bool valid = true;

            if (StudentFname == null || StudentLname == null)
            {

                valid = false;
            }
            else
            {

                if (StudentFname.Length < 2 || StudentFname.Length > 255) valid = false;
                if (StudentLname.Length < 2 || StudentFname.Length > 255) valid = false;

            }
            Debug.WriteLine("The model validity is : " + valid);

            return valid;
        }

    }
}