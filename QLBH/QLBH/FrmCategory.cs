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
        QLBHEntities2 db = new QLBHEntities2();
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
            var r = db.getcat().ToList();
            listCate.DataSource = r;
            listCate.Columns[0].HeaderText = "Mã danh mục";
            listCate.Columns[1].HeaderText = "Tên danh mục";
            listCate.Columns[2].HeaderText = "Ngày tạo";
        }
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            autoLoad();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    var c = new Category { Name = txtName.Text, create_at = DateTime.Now };
                    db.Categories.Add(c);
                    db.SaveChanges();
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                    autoLoad();
                }else
                {
                    MessageBox.Show("Nhập tên lại sản phẩm ");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm mới thất bại!", "Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
                MessageBox.Show("Bạn phải chọn trước khi sửa!");
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
                    var p = db.Products.Where(x => x.cat_id == i).Select(x => x).ToList();
                    if( p.Count() <= 0)
                    {
                        db.Categories.Remove(attr);
                        db.SaveChanges();
                        autoLoad();
                    }else {
                        MessageBox.Show("Bạn không thể xóa loại sản phẩm này ");
                    }
                  
                }
            }
            else
            {
                MessageBox.Show("Bạn phảo chọn trước khi xóa");
            }
        }

        private void listCate_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                autoLoad();
            }
            else
            {
                var result = db.searchCateName(txtSearch.Text);
                listCate.DataSource = result;
            }
        }
    }
}
