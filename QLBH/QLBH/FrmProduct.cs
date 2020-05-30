using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH
{
    public partial class FrmProduct : Form
    {
        String id;
        QLBHModel db = new QLBHModel();
        public FrmProduct()
        {
            InitializeComponent();
            cboCate.DropDownHeight = 30;
            cboCate.Width = 407;
        }

        private void comboBoxCate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void autoLoad()
        {
            var r = db.Products.Select(x => x).ToList();
            listProduct.DataSource = r;
  
            
        }
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            autoLoad();
            var result = db.Categories.Select(x=>x).ToList();
            cboCate.DisplayMember = "Name";
            cboCate.ValueMember = "id";
            cboCate.DataSource = result;
            var size = db.Attributes.Where(x=>x.type == 1).Select(x => x).ToList();
            cboSize.DisplayMember = "value";
            cboSize.ValueMember = "id";
            cboSize.DataSource = size;
            var color = db.Attributes.Where(x => x.type == 0).Select(x => x).ToList();
            cboColor.DisplayMember = "value";
            cboColor.ValueMember = "id";
            cboColor.DataSource = color;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Category cat = cboCate.SelectedItem as Category;
               int CatId = cat.id;
                Attribute a = cboColor.SelectedItem as Attribute;
               int attrColorId  =  a.id;
                Attribute b = cboSize.SelectedItem as Attribute;
               int attrSizeId = b.id;
            try
            { Product p = new Product { name = txtName.Text, cat_id = CatId, code = txtCode.Text, price = Convert.ToDouble(txtPrice.Text), image = txtImage.Text, quantity = Convert.ToInt32(txtQuantity.Text) };
                db.Products.Add(p);
                db.SaveChanges();
                autoLoad();
                db.productAttr.Add(new productAttr { });
                File.Copy(txtImage.Text, Path.Combine(@"C:\Users\Chung Dep Trai\Desktop\QLBH\QLBH\QLBH\Images", Path.GetFileName(txtImage.Text)), true);
                MessageBox.Show("thêm thành công");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listProduct_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void listProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (listProduct.SelectedRows != null)
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = listProduct.Rows[index];
                id = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();
                cboCate.SelectedItem = selectedRow.Cells[1].Value.ToString();
                txtCode.Text = selectedRow.Cells[3].Value.ToString();
                txtPrice.Text = selectedRow.Cells[4].Value.ToString();
                txtImage.Text = selectedRow.Cells[5].Value.ToString();
                txtQuantity.Text = selectedRow.Cells[6].Value.ToString();
            }else
            {
                MessageBox.Show("Chọn hàng");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUpload_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png| All Files(*.*)|*.*";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                txtImage.Text = opnfd.FileName;
                pictureBox1.Image = new Bitmap(opnfd.FileName);
            }
        }


    }
}
