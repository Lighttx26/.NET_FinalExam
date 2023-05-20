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
    public partial class MainForm : Form
    {
        private List<ItemDgv> datasource;
        public MainForm()
        {
            InitializeComponent();

            dgv.Columns.Add("#", "STT");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ReloadDgv();
            ReloadSortCb();
            ReloadPositionCb();
        }

        private void ReloadPositionCb()
        {
            cbbRole.Items.Clear();
            cbbRole.Items.Add(new ItemCbb { Value = 0, Text = "Tat ca" });
            cbbRole.Items.AddRange(BLL.BLL.Instance.GetAllPositions().ToArray());
            cbbRole.SelectedIndex = 0;
        }

        private void ReloadSortCb()
        {
            cbbSort.Items.Clear();
            cbbSort.Items.Add("NONE");
            cbbSort.Items.AddRange(new string[]
            {
                "Id",
                "Name",
                "Birthday",
                "Gender",
                "PositionName",
                "Salary",
            });

            cbbSort.SelectedIndex = 0;
        }

        private void ReloadDgv()
        {
            datasource = BLL.BLL.Instance.GetAllEmployees();
            dgv.DataSource = datasource;
            RenameColumn();
        }

        public void RenameColumn()
        {
            // Cac cot khong can hien thi nhung van can de lay thong tin
            dgv.Columns["PositionId"].Visible = false;

            // Doi ten hien thi cac cot
            dgv.Columns["Id"].HeaderText = "Mã nhân viên";
            dgv.Columns["Name"].HeaderText = "Tên nhân viên";
            dgv.Columns["Birthday"].HeaderText = "Ngày sinh";
            dgv.Columns["Gender"].HeaderText = "Giới tính";
            dgv.Columns["PositionName"].HeaderText = "Chức vụ";
            dgv.Columns["Salary"].HeaderText = "Thu nhập";
        }

        private void cbbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSearch.Clear();
            cbbSort.SelectedIndex = 0;

            int positionid = ((ItemCbb)cbbRole.SelectedItem).Value;
            dgv.DataSource = BLL.BLL.Instance.GetEmployeesByFilter(datasource, positionid);
            RenameColumn();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            cbbSort.SelectedIndex = 0;

            int positionid = ((ItemCbb)cbbRole.SelectedItem).Value;
            string searchtext = tbSearch.Text;
            dgv.DataSource = BLL.BLL.Instance.GetEmployeesByFilter(datasource, positionid, searchtext);
            RenameColumn();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            DetailsForm df = new DetailsForm();
            df.ReloadMainform = new DetailsForm.MyDelegate(this.ReloadDgv);
            df.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count != 1)
            {
                MessageBox.Show("Chon 1 hang de thuc hien");
            }

            else
            {
                string eid = dgv.SelectedRows[0].Cells["Id"].Value.ToString();

                DetailsForm dform = new DetailsForm(eid);
                dform.ReloadMainform = new DetailsForm.MyDelegate(this.ReloadDgv); // Ham this.Reload() se duoc thuc thi khi MyDelegate DetailsForm duoc goi
                dform.Show();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chon 1 hang de thuc hien");
            }

            else
            {
                if (MessageBox.Show("Bạn muốn xóa những dòng này?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        string eid = row.Cells["Id"].Value.ToString();
                        BLL.BLL.Instance.DeleteEmployee(eid);
                    }
                }
            }

            ReloadDgv();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            int positionid = ((ItemCbb)cbbRole.SelectedItem).Value;
            string searchtext = tbSearch.Text;
            string sortprop = cbbSort.Text;

            dgv.DataSource = BLL.BLL.Instance.GetEmployeesByFilter(datasource, positionid, searchtext, sortprop);
            RenameColumn();
        }

        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgv.Rows[e.RowIndex].Cells["#"].Value = (e.RowIndex + 1).ToString();
        }
    }
}
