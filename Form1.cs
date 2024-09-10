using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Name: Queen Sarah Anumu Bih
//ID: 2311432
//Date: 30/11/2023

namespace Project
{
    public partial class Form1 : Form
    {
        internal enum Grids
        {
            Prog,
            Course,
            Stud,
            Enroll
        }
        internal static Form1 current;
        private Grids grid;
        private bool OKToChange = true;
        public Form1()
        {
            current = this;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new Form2();
            Form2.current.Visible = false;

            Text = "Students, Courses & Programs";
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void programsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                grid = Grids.Prog;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource1.DataSource = Data.Programs.GetPrograms();
                bindingSource1.Sort = "ProgId";
                dataGridView1.DataSource = bindingSource1;

                dataGridView1.Columns["ProgId"].HeaderText = "Program ID";
                dataGridView1.Columns["ProgName"].HeaderText = "Program Name";
                dataGridView1.Columns["ProgId"].DisplayIndex = 0;
                dataGridView1.Columns["ProgName"].DisplayIndex = 1;
            }
            else
            {
                OKToChange = true;
            }
        }
        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                grid = Grids.Course;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource2.DataSource = Data.Courses.GetCourses();
                bindingSource2.Sort = "CId";
                dataGridView1.DataSource = bindingSource2;

                dataGridView1.Columns["CId"].HeaderText = "Course ID";   
                dataGridView1.Columns["ProgId"].HeaderText = "Program ID";
                dataGridView1.Columns["CName"].HeaderText = "Course Name";
                dataGridView1.Columns["CId"].DisplayIndex = 0;
                dataGridView1.Columns["CName"].DisplayIndex = 1;
                dataGridView1.Columns["ProgId"].DisplayIndex = 2;
            }
            else
            {
                OKToChange = true;
            }
        }
        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                grid = Grids.Stud;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource3.DataSource = Data.Students.GetStudents();
                bindingSource3.Sort = "StId";
                dataGridView1.DataSource = bindingSource3;

                dataGridView1.Columns["StId"].HeaderText = "Student ID";      ////////Could be Name as the header
                dataGridView1.Columns["StId"].DisplayIndex = 0;
                dataGridView1.Columns["StName"].DisplayIndex = 1;
                dataGridView1.Columns["ProgId"].DisplayIndex = 2;
            }
            else
            {
                OKToChange = true;
            }
        }
        //Just 3 binding sources needed. Enrollment table doesn't need a binding source!
        private void enrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange && (grid != Grids.Enroll))
            {
                grid = Grids.Enroll;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource4.DataSource = Data.Enrollments.GetDisplayEnrollments();
                bindingSource4.Sort = "StId, CId";  // using bindingSource to sort by two columns
                //bindingSource4.Sort = "CId";
                dataGridView1.DataSource = bindingSource4;

                dataGridView1.Columns["StId"].HeaderText = "Student ID";
                dataGridView1.Columns["StName"].HeaderText = "Student Name";
                dataGridView1.Columns["CId"].HeaderText = "Course ID";
                dataGridView1.Columns["CName"].HeaderText = "Course Name";
                dataGridView1.Columns["FinalGrade"].HeaderText = "Final Grade";
                dataGridView1.Columns["ProgId"].HeaderText = "Program ID";
                dataGridView1.Columns["ProgName"].HeaderText = "Program Name";

                dataGridView1.Columns["StId"].DisplayIndex = 0;
                dataGridView1.Columns["StName"].DisplayIndex = 1;
                dataGridView1.Columns["CId"].DisplayIndex = 2;
                dataGridView1.Columns["CName"].DisplayIndex = 3;
                dataGridView1.Columns["FinalGrade"].DisplayIndex = 4;
                dataGridView1.Columns["ProgId"].DisplayIndex = 5;
                dataGridView1.Columns["ProgName"].DisplayIndex = 6;


            }
            else
            {
                OKToChange = true;
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            //BusinessLayer.Departments.UpdateDepartments();
            if (Business.Programs.UpdatePrograms() == -1)
            {
                bindingSource1.ResetBindings(false);
            }
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            if (Business.Courses.UpdateCourses() == -1)
            {
                bindingSource2.ResetBindings(false);
            }
        }

        private void bindingSource3_CurrentChanged(object sender, EventArgs e)
        {
            if (Business.Students.UpdateStudents() == -1)
            {
                bindingSource3.ResetBindings(false);
            }
        }

        private void menuStrip1_Click(object sender, EventArgs e)
        {
            OKToChange = true;
            Validate();
            if (grid == Grids.Prog)
            {
                if (Business.Programs.UpdatePrograms() == -1)
                {
                    OKToChange = false;
                }
            }
            else if (grid == Grids.Course)
            {
                if (Business.Courses.UpdateCourses() == -1)
                {
                    OKToChange = false;
                }
            }
            else if (grid == Grids.Stud)
            {
                if (Business.Students.UpdateStudents() == -1)
                {
                    OKToChange = false;
                }
            }
        }

        internal static void BLLMessage(string s)
        {
            MessageBox.Show("Business Layer: " + s);
        }

        internal static void DALMessage(string s)
        {
            MessageBox.Show("Data Layer: " + s);
        }


        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2.current.Start(Form2.Modes.ADD, null);
        }
        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("A line must be selected for update");
            }
            else if (c.Count > 1)
            {
                MessageBox.Show("Only one line must be selected for update");
            }
            else
            {
                if ("" + c[0].Cells["FinalGrade"].Value == "")
                {
                    Form2.current.Start(Form2.Modes.MODIFY, c);
                }
                else
                {
                    MessageBox.Show("To update this line, Final Grade value must be removed first.");
                }
            }
        }
       
        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("At least one line must be selected for deletion");
            }
            else
            {
                List<string[]> lId = new List<string[]>();
                for (int i = 0; i < c.Count; i++)
                {
                    if (("" + c[i].Cells["FinalGrade"].Value) != "")
                    {

                        MessageBox.Show("Please remove the final grade before you can delete");
                        MessageBox.Show("" + c[i].Cells["FinalGrade"].Value);
                        break;
                    }
                    else
                    {


                        MessageBox.Show("" + c[i].Cells["FinalGrade"].Value);


                        lId.Add(new string[] { "" + c[i].Cells["StId"].Value,
                                           "" + c[i].Cells["CId"].Value });

                        Data.Enrollments.DeleteData(lId);

                    }
                }
            }
        }
        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("A line must be selected for final grade update");
            }
            else if (c.Count > 1)
            {
                MessageBox.Show("Only one line must be selected for update");
            }
            else
            {
                Form2.current.Start(Form2.Modes.FINALGRADE, c);
            }
        }


      

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Impossible to insert / update");
            e.Cancel = false;  // Ensure automatic undoing of the error
            OKToChange = false;
        }

    }
}
