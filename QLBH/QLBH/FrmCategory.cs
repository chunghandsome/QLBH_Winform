using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH
{
    public partial class FrmCategory : Form
    {
        String id;
        QLBHModel db = new QLBHModel();
        public FrmCategory()
        {
            InitializeComponent();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void autoLoad()
        {
            var r = db.Categories.Select(x => x).ToList();
            listCate.DataSource = r;
        }
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            autoLoad();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                db.Categories.Add(new Category { Name = txtName.Text });
                db.SaveChanges();
                MessageBox.Show("Thêm thành công!", "Thêm mới");
                autoLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Them moi khong thanh cong");
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listCate.SelectedRows != null)
            {
                Byte i = Convert.ToByte(id);
                Category attr = db.Categories.FirstOrDefault(att => att.id.Equals(i));
                attr.Name = txtName.Text;
                db.SaveChanges();
                autoLoad();
            }
            else
            {
                MessageBox.Show("Chon truoc khi sua");
            }

        }

        private void listCate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = listCate.Rows[index];
            id = selectedRow.Cells[0].Value.ToString();
            txtName.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listCate.SelectedRows != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Byte i = Convert.ToByte(id);
                    Category attr = db.Categories.FirstOrDefault(a => a.id.Equals(i));
                    db.Categories.Remove(attr);
                    db.SaveChanges();
                    autoLoad();
                }
            }
            else
            {
                MessageBox.Show("Chon truoc khi xoa");
            }
        }

        private void listCate_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
