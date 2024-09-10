using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Name: Queen Sarah Anumu Bih
//ID: 2311432
//Date: 30/11/2023

namespace Data
{
    internal class Connect
    {
        private static string cliComConnectionString = GetConnectString();
        internal static String ConnectionString { get => cliComConnectionString; }
        private static String GetConnectString()
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "College1en";
            cs.UserID = "sa";
            cs.Password = "sysadm";
            return cs.ConnectionString;
        }
    }

    internal class DataTables
    {
        private static SqlDataAdapter adapterProg = InitAdapterProg();
        private static SqlDataAdapter adapterCourse = InitAdapterCourse();
        private static SqlDataAdapter adapterStud = InitAdapterStud();
        private static SqlDataAdapter adapterEnroll = InitAdapterEnroll();



        private static DataSet ds = InitDataSet();

        private static SqlDataAdapter InitAdapterProg()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Programs ORDER BY ProgId ",
                Connect.ConnectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();
            return r;
        }

        private static SqlDataAdapter InitAdapterCourse()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Courses ORDER BY CId ",
                Connect.ConnectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();
            return r;
        }

        private static SqlDataAdapter InitAdapterStud()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Students ORDER BY StId ",
                Connect.ConnectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();
            return r;
        }

        private static SqlDataAdapter InitAdapterEnroll()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Enrollments ORDER BY StId, CId ",
                Connect.ConnectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();
            return r;
        }

        private static DataSet InitDataSet()
        {
            DataSet ds = new DataSet();
            loadProg(ds);
            loadCourse(ds);
            loadStud(ds);
            loadEnroll(ds);
            createFkCourse(ds);
            createFkStud(ds);
            return ds;
        }

        private static void loadProg(DataSet ds)
        {
            adapterProg.Fill(ds, "Programs");

            ds.Tables["Programs"].Columns["ProgId"].AllowDBNull = false;
            ds.Tables["Programs"].Columns["ProgName"].AllowDBNull = false;

            ds.Tables["Programs"].PrimaryKey = new DataColumn[1]
                    { ds.Tables["Programs"].Columns["ProgId"]};
        }

        private static void loadCourse(DataSet ds)
        {
            adapterCourse.Fill(ds, "Courses");

            ds.Tables["Courses"].Columns["CId"].AllowDBNull = false;
            ds.Tables["courses"].Columns["CName"].AllowDBNull = false;

            ds.Tables["Courses"].PrimaryKey = new DataColumn[1]
                    { ds.Tables["Courses"].Columns["CId"]};
        }

        private static void loadStud(DataSet ds)
        {
            adapterStud.Fill(ds, "Students");

            ds.Tables["Students"].Columns["StId"].AllowDBNull = false;
            ds.Tables["Students"].Columns["StName"].AllowDBNull = false;

            ds.Tables["Students"].PrimaryKey = new DataColumn[1]
                    { ds.Tables["Students"].Columns["StId"]};
        }

        private static void loadEnroll(DataSet ds)
        {
            adapterEnroll.Fill(ds, "Enrollments");
            ds.Tables["Enrollments"].Columns["StId"].AllowDBNull = false;
            ds.Tables["Enrollments"].Columns["CId"].AllowDBNull = false;

            ds.Tables["Enrollments"].PrimaryKey = new DataColumn[2]
                    { ds.Tables["Enrollments"].Columns["StId"], ds.Tables["Enrollments"].Columns["CId"] };
            ForeignKeyConstraint myFK01 = new ForeignKeyConstraint("MyFK01",
                new DataColumn[]{
                    ds.Tables["Students"].Columns["StId"]
                },
                new DataColumn[] {
                    ds.Tables["Enrollments"].Columns["StId"]
                }
            );
            myFK01.DeleteRule = Rule.Cascade;
            myFK01.UpdateRule = Rule.Cascade;
            ds.Tables["Enrollments"].Constraints.Add(myFK01);

            ForeignKeyConstraint myFK02 = new ForeignKeyConstraint("MyFK02",
              new DataColumn[]{
                    ds.Tables["Courses"].Columns["CId"]
              },
              new DataColumn[] {
                    ds.Tables["Enrollments"].Columns["CId"]
              }
          );
            myFK02.DeleteRule = Rule.Cascade; //None
            myFK02.UpdateRule = Rule.Cascade;    //None
            ds.Tables["Enrollments"].Constraints.Add(myFK02);
        }
        private static void createFkCourse(DataSet ds)
        {
            ForeignKeyConstraint My_fk_course = new ForeignKeyConstraint("My_fk_course",
                new DataColumn[]
                {
                    ds.Tables["Programs"].Columns["ProgId"],
                },
                new DataColumn[]
                {
                    ds.Tables["Courses"].Columns["ProgId"],
                });
            My_fk_course.DeleteRule = Rule.None;
            My_fk_course.UpdateRule = Rule.Cascade; // None;
            ds.Tables["Courses"].Constraints.Add(My_fk_course);
        }

        private static void createFkStud(DataSet ds)
        {
            ForeignKeyConstraint My_fk_stud = new ForeignKeyConstraint("My_fk_stud",
                new DataColumn[]
                {
                    ds.Tables["Programs"].Columns["ProgId"],
                },
                new DataColumn[]
                {
                    ds.Tables["Students"].Columns["ProgId"],
                });
            My_fk_stud.DeleteRule = Rule.None;
            My_fk_stud.UpdateRule = Rule.Cascade; // None;
            ds.Tables["Students"].Constraints.Add(My_fk_stud);
        }

        internal static SqlDataAdapter getAdapterProg()
        {
            return adapterProg;
        }
        internal static SqlDataAdapter getAdapterCourse()
        {
            return adapterCourse;
        }
        internal static SqlDataAdapter getAdapterStud()
        {
            return adapterStud;
        }
        internal static SqlDataAdapter getAdapterEnroll()
        {
            return adapterEnroll;
        }
        internal static DataSet getDataSet()
        {
            return ds;
        }
    }

    internal class Programs
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterProg();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetPrograms()
        {
            return ds.Tables["Programs"];
        }

        internal static int UpdatePrograms()
        {
            if (!ds.Tables["Programs"].HasErrors)
            {
                return adapter.Update(ds.Tables["Programs"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Courses
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterCourse();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetCourses()
        {
            return ds.Tables["Courses"];
        }

        internal static int UpdateCourses()
        {
            if (!ds.Tables["Courses"].HasErrors)
            {
                return adapter.Update(ds.Tables["Courses"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Students
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterStud();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetStudents()
        {
            return ds.Tables["Students"];
        }

        internal static int UpdateStudents()
        {
            if (!ds.Tables["Students"].HasErrors)
            {
                return adapter.Update(ds.Tables["Students"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Enrollments
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterEnroll();
        private static DataSet ds = DataTables.getDataSet();

        private static DataTable displayEnroll = null;

        internal static DataTable GetDisplayEnrollments()
        {
            /* 
             * next line is needed to ensure "delete row"
             * due to the cascade are actually removed.
             */
            ds.Tables["Enrollments"].AcceptChanges();

            var query = (
                   from enroll in ds.Tables["Enrollments"].AsEnumerable()
                   from stud in ds.Tables["Students"].AsEnumerable()
                   from course in ds.Tables["Courses"].AsEnumerable()
                   from prog in ds.Tables["Programs"].AsEnumerable()
                   where enroll.Field<string>("StId") == stud.Field<string>("StId")
                   where enroll.Field<string>("CId") == course.Field<string>("CId")
                   where prog.Field<string>("ProgId") == stud.Field<string>("ProgId")
                   select new
                   {
                       StId = stud.Field<string>("StId"),
                       StName = stud.Field<string>("StName"),
                       CId = course.Field<string>("CId"),
                       CName = course.Field<string>("CName"),              
                       FinalGrade = enroll.Field<Nullable<int>>("FinalGrade"),
                       ProgId = prog.Field<string>("ProgId"),
                       ProgName = prog.Field<string>("ProgName")
                   });
            DataTable result = new DataTable();
            result.Columns.Add("StId");
            result.Columns.Add("StName");
            result.Columns.Add("CId");
            result.Columns.Add("CName");
            result.Columns.Add("FinalGrade");
            result.Columns.Add("ProgId");
            result.Columns.Add("ProgName");
            foreach (var x in query)
            {
                object[] allFields = { x.StId, x.StName, x.CId, x.CName, x.FinalGrade, x.ProgId, x.ProgName };
                result.Rows.Add(allFields);
            }
            displayEnroll = result;
            return displayEnroll;
        }

        internal static int InsertData(string[] a)
        {
            var test = (
                   from enroll in ds.Tables["Enrollments"].AsEnumerable()
                   where enroll.Field<string>("StId") == a[0]
                   where enroll.Field<string>("CId") == a[1]
                   select enroll);
            if (test.Count() > 0)
            {
                Project.Form1.DALMessage("This enrollment already exists");
                return -1;
            }

            var studProg = (
                   from stud in ds.Tables["Students"].AsEnumerable()
                   where stud.Field<string>("StId") == a[0]
                   select new
                   {
                       ProgId = stud.Field<string>("ProgId")
                   });

            var cProg = (
                   from course in ds.Tables["Courses"].AsEnumerable()
                   where course.Field<string>("CId") == a[1]
                   select new
                   {
                       ProgId = course.Field<string>("ProgId")
                   });

            foreach (var x in studProg)
            {
                foreach (var y in cProg)
                {
                    if (x.ProgId != y.ProgId)
                    {
                        Project.Form1.DALMessage("The student can not be inserted in a course that is not in his program");
                        return -1;
                    }
                }
            }

            try
            {
                DataRow line = ds.Tables["Enrollments"].NewRow();
                line.SetField("StId", a[0]);
                line.SetField("CId", a[1]);
                ds.Tables["Enrollments"].Rows.Add(line);

                adapter.Update(ds.Tables["Enrollments"]);

                if (displayEnroll != null)
                {
                    var query = (
                           from stud in ds.Tables["Students"].AsEnumerable()
                           from course in ds.Tables["Courses"].AsEnumerable()
                           from prog in ds.Tables["Programs"].AsEnumerable()
                           where stud.Field<string>("StId") == a[0]
                           where course.Field<string>("CId") == a[1]
                           where prog.Field<string>("ProgId") == a[2]
                           select new
                           {
                               StId = stud.Field<string>("StId"),
                               StName = stud.Field<string>("StName"),
                               CId = course.Field<string>("CId"),
                               CName = course.Field<string>("CName"),
                               FinalGrade = line.Field<Nullable<int>>("FinalGrade"),
                               ProgId = prog.Field<string>("ProgId"),
                               ProgName = prog.Field<string>("ProgName")
                           });
                    var r = query.Single();
                    displayEnroll.Rows.Add(new object[] { r.StId, r.StName, r.CId, r.CName, r.FinalGrade, r.ProgId, r.ProgName });
                }
                return 0;
            }
            catch (Exception)
            {
                Project.Form1.DALMessage("Insertion / Update rejected");
                return -1;
            }
        }

        internal static int UpdateData(string[] a)
        {
            return 0;  
        }

        internal static int DeleteData(List<string[]> lId)
        {
            try
            {
                var lines = ds.Tables["Enrollments"].AsEnumerable()
                                .Where(s =>
                                   lId.Any(x => (x[0] == s.Field<string>("StId") && x[1] == s.Field<string>("CId"))));

                foreach (var line in lines)
                {
                    line.Delete();
                }

                adapter.Update(ds.Tables["Enrollments"]);

                if (displayEnroll != null)
                {
                    foreach (var p in lId)
                    {
                        var r = displayEnroll.AsEnumerable()
                                .Where(s => (s.Field<string>("StId") == p[0] && s.Field<string>("CId") == p[1]))
                                .Single();
                        displayEnroll.Rows.Remove(r);
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                Project.Form1.DALMessage("Update / Deletion rejected");
                return -1;
            }
        }

        internal static int UpdateFinalGrade(string[] a, Nullable<int> finalGrade)
        {
            try
            {
                var line = ds.Tables["Enrollments"].AsEnumerable()
                                    .Where(s =>
                                      (s.Field<string>("StId") == a[0] && s.Field<string>("CId") == a[1]))
                                    .Single();

                line.SetField("FinalGrade", finalGrade);

                adapter.Update(ds.Tables["Enrollments"]);

                if (displayEnroll != null)
                {
                    var r = displayEnroll.AsEnumerable()
                                    .Where(s =>
                                      (s.Field<string>("StId") == a[0] && s.Field<string>("CId") == a[1]))
                                    .Single();
                    r.SetField("FinalGrade", finalGrade);
                }
                return 0;
            }
            catch (Exception)
            {
                Project.Form1.DALMessage("Update / Deletion rejected");
                return -1;
            }
        }
    }
}
