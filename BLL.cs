using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Name: Queen Sarah Anumu Bih
//ID: 2311432
//Date: 30/11/2023


namespace Business
{
    class Programs
    {
        internal static int UpdatePrograms()
        {
            DataSet ds = Data.DataTables.getDataSet();

            DataTable dt = ds.Tables["Programs"]
                              .GetChanges(DataRowState.Added | DataRowState.Modified);
            if (dt != null)
            {
                if (dt.AsEnumerable().Any(r => !IsValidProgId(r.Field<string>("ProgId"))))
                {
                    Project.Form1.BLLMessage("Invalid Id for Programs");
                    ds.RejectChanges();
                    return -1;
                }
                else
                {
                    return Data.Programs.UpdatePrograms();
                }
            }
            else
            {
                return Data.Programs.UpdatePrograms();
            }
        }

        private static bool IsValidProgId(string progId)
        {
            bool r = true;
            if (progId.Length != 5) { r = false; }
            else if (progId[0] != 'P') { r = false; }
            else
            {
                for (int i = 1; i < progId.Length; i++)
                {
                    r = r && Char.IsDigit(progId[i]);
                }
            }
            return r;
        }
    }

    class Courses
    {
        internal static int UpdateCourses()
        {
            DataSet ds = Data.DataTables.getDataSet();

            DataTable dt = ds.Tables["Courses"]
                              .GetChanges(DataRowState.Added | DataRowState.Modified);
            if (dt != null)
            {
                if (dt.AsEnumerable().Any(r => !IsValidCId(r.Field<string>("CId"))))
                {
                    Project.Form1.BLLMessage("Invalid Id for Courses");
                    ds.RejectChanges();
                    return -1;
                }
                else
                {
                    return Data.Courses.UpdateCourses();
                }
            }
            else
            {
                return Data.Courses.UpdateCourses();
            }
        }

        private static bool IsValidCId(string CId)
        {
            bool r = true;
            if (CId.Length != 7) { r = false; }
            else if (CId[0] != 'C') { r = false; }
            else
            {
                for (int i = 1; i < CId.Length; i++)
                {
                    r = r && Char.IsDigit(CId[i]);
                }
            }
            return r;
        }
    }

    class Students
    {
        internal static int UpdateStudents()
        {
            DataSet ds = Data.DataTables.getDataSet();

            DataTable dt = ds.Tables["Students"]
                              .GetChanges(DataRowState.Added | DataRowState.Modified);
            // dt typically will be null or have one line
            if (dt != null)
            {
                if (dt.AsEnumerable().Any(r => !IsValidStId(r.Field<string>("StId"))))
                {
                    Project.Form1.BLLMessage("Invalid Id for Students");
                    ds.RejectChanges();
                    return -1;
                }
                else
                {
                    return Data.Students.UpdateStudents();
                }
            }
            else
            {
                return Data.Students.UpdateStudents();
            }
        }

        private static bool IsValidStId(string StId)
        {
            bool r = true;
            if (StId.Length != 10) { r = false; }
            else if (StId[0] != 'S') { r = false; }
            else
            {
                for (int i = 1; i < StId.Length; i++)
                {
                    r = r && Char.IsDigit(StId[i]);
                }
            }
            return r;
        }
    }

    class Enrollments
    {
        internal static int UpdateFinalGrade(string[] a, string fg)
        {
            Nullable<int> finalGrade;
            int temp;

            if (fg == "")
            {
                finalGrade = null;
            }
            else if (int.TryParse(fg, out temp) && (0 <= temp && temp <= 100))
            {
                finalGrade = temp;
            }
            else
            {
                Project.Form1.BLLMessage(
                          "Final Grade must be an integer between 0 and 100"
                          );
                return -1;
            }

            return Data.Enrollments.UpdateFinalGrade(a, finalGrade);

        }
    }
}
