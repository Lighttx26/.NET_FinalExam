using NET_Final.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NET_Final
{
    public partial class DetailsForm : Form
    {
        public delegate void MyDelegate();
        public MyDelegate ReloadMainform { get; set; }

        private readonly string _eid;
        public DetailsForm(string eid = "")
        {
            InitializeComponent();

            this._eid = eid;
        }

        private void DetailsForm_Load(object sender, EventArgs e)
        {
            cbbRole.Items.Clear();
            cbbRole.Items.AddRange(BLL.BLL.Instance.GetAllPositions().ToArray());
            // Edit
            if (_eid != "")
            {
                var emp = BLL.BLL.Instance.GetEmployee(this._eid);

                tbId.ReadOnly = true;
                tbId.Text = _eid;

                tbName.Text = emp.Name;
                dtp.Text = emp.Birthday.ToString();

                if (emp.Gender == true) rdMale.Checked = true;
                else rdFemale.Checked = true;  

                tbRate.Text = emp.Rate.ToString();
                cbbRole.Text = BLL.BLL.Instance.GetPosition(emp.PositionId).PositionName;
            }

            else
            {

            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInput();

                if (_eid != "") EditHandler();
                else AddHandler();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AddHandler()
        {
            try
            {
                //// Neu mssv chua co thi them sinh vien moi vao DB
                //if (!BLL.BLL.Instance.IsExistStudent(studentidTb.Text))
                //{
                //    BLL.BLL.Instance.AddStudent(new Student
                //    {
                //        StudentID = studentidTb.Text,
                //        StudentName = nameTb.Text,
                //        ClassName = classCbb.Text,
                //        Gender = maleRadio.Checked,
                //    });
                //}

                // Kiem tra nhan vien co chua
                if (BLL.BLL.Instance.IsExistEmployee(tbId.Text))
                {
                    throw new Exception("Nhan vien da ton tai");
                }

                BLL.BLL.Instance.AddEmployee(new Employee
                {
                    Id = tbId.Text,
                    Name = tbName.Text,
                    Birthday = Convert.ToDateTime(dtp.Text),
                    Gender = rdMale.Checked,
                    Rate = Convert.ToDouble(tbRate.Text),
                    PositionId = ((ItemCbb)cbbRole.SelectedItem).Value,
                });

                ReloadMainform();
                this.Dispose();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditHandler()
        {
            try
            {
                Employee e = new Employee
                {
                    Id = tbId.Text,
                    Name = tbName.Text,
                    Birthday = Convert.ToDateTime(dtp.Text),
                    Gender = rdMale.Checked,
                    Rate = Convert.ToDouble(tbRate.Text),
                    PositionId = ((ItemCbb)cbbRole.SelectedItem).Value,
                };
                // Cap nhat thong tin nhan vien
                BLL.BLL.Instance.UpdateEmployee(e);

                ReloadMainform();
                this.Dispose();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ValidateInput()
        {
            if (tbId.Text == "") throw new Exception("Khong duoc de trong ma nhan vien");
            if (tbName.Text == "") throw new Exception("Khong duoc de trong ten nhan vien");
            if (dtp.Text == "") throw new Exception("Khong duoc de trong ngay sinh");
            if (!(rdMale.Checked || rdFemale.Checked)) throw new Exception("Khong duoc de trong gioi tinh");
            if (tbRate.Text == "") throw new Exception("Khong duoc de trong he so luong");
            if (cbbRole.Text == "") throw new Exception("Khong duoc de trong chuc vu");
        }
    }
}
