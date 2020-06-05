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
        int idC;
        int idS;
        String image;
        QLBHEntities2 db = new QLBHEntities2();
        public FrmProduct()
        {
            InitializeComponent();
            cboCate.DropDownHeight = 150;
            cboCate.Width = 407;
            cboCate.Height = 35;
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
            var product = db.selectProductcate2().ToList();
            listProduct.DataSource = product;
            listProduct.Columns[0].HeaderText = "Id sản phẩm";
            listProduct.Columns[1].HeaderText = "Mã sản phẩm";
            listProduct.Columns[2].HeaderText = "Tên sản phẩm";
            listProduct.Columns[3].HeaderText = "Giá sản phẩm";
            listProduct.Columns[4].HeaderText = "Loại sản phẩm";
            listProduct.Columns[5].HeaderText = "Số lượng sản phẩm";
            listProduct.Columns[6].HeaderText = "Ảnh sản phẩm";
           listProduct.Columns[7].HeaderText = "Màu sắc";
           listProduct.Columns[8].HeaderText = "Kích cỡ";
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
        private  Boolean validate()
        {
            if (txtName.Text == null | txtCode.Text == null | txtPrice.Text == null | txtQuantity.Text  == null | cboCate.Text == null | cboSize.Text == null | cboColor.Text == null)
            {

                return false;
            }else if(txtImage.Text == null)
            {
                return false;
            }
                return true;

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                Category cat = cboCate.SelectedItem as Category;
                int CatId = cat.id;
                Attribute a = cboColor.SelectedItem as Attribute;
                int attrColorId = a.id;
                Attribute b = cboSize.SelectedItem as Attribute;
                int attrSizeId = b.id;
                try
                {
                    var checkProd = db.Products.Where(x => x.code == txtCode.Text).ToList();
                    if(checkProd.Count() <= 0)
                    {
                        Product p = new Product { name = txtName.Text, cat_id = CatId, code = txtCode.Text, price = Convert.ToDouble(txtPrice.Text), image = txtImage.Text, quantity = Convert.ToInt32(txtQuantity.Text), create_at = DateTime.Now };
                        db.Products.Add(p);
                        db.productAttrs.Add(new productAttr { product_id = p.id, attr_id = attrSizeId });
                        db.productAttrs.Add(new productAttr { product_id = p.id, attr_id = attrColorId });
                        db.SaveChanges();
                        autoLoad();
                        File.Copy(txtImage.Text, Path.Combine(@"C:\Users\Chung Dep Trai\Desktop\QLBH\QLBH\QLBH\Images", Path.GetFileName(txtImage.Text)), true);
                        MessageBox.Show("thêm thành công");
                    }else
                    {
                        MessageBox.Show("Mã sản phẩm đã tồn tại, hãy cập nhập số lượng sản phẩm của mã này");
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dữ liệu chưa đúng định dạng!");
                }
            }else
            {
                MessageBox.Show("Dữ liệu chưa đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                txtName.Text = selectedRow.Cells[2].Value.ToString();
                image = selectedRow.Cells[6].Value.ToString();
                txtCode.Text = selectedRow.Cells[1].Value.ToString();
                txtPrice.Text = selectedRow.Cells[3].Value.ToString();
                txtImage.Text = selectedRow.Cells[6].Value.ToString();
                txtQuantity.Text = selectedRow.Cells[5].Value.ToString();
                int ipr = Convert.ToInt32(id);
                List<productAttr> attrIdColor = db.productAttrs.Where(x => x.product_id == ipr).Select(x => x).ToList();
                foreach ( productAttr a in attrIdColor )
                {
                    var attrType = db.Attributes.Where(x => x.id == a.attr_id).Select(x => x).First();
                    
                    if( attrType.type == 0)
                    {
                        cboColor.SelectedItem = attrType.value;
                        cboColor.SelectedValue = attrType.id;
                        idC = a.id;
                    }
                    else
                    {
                        cboSize.SelectedItem = attrType.value;
                        cboSize.SelectedValue = attrType.id;
                        idS = a.id;
                    }
                }
                String catName = selectedRow.Cells[4].Value.ToString();
                var catId = db.Categories.Where(x => x.Name == catName).Select(x => x).First();
                cboCate.SelectedItem = selectedRow.Cells[4].Value.ToString();
                cboCate.SelectedValue = catId.id;

                pictureBox1.ImageLocation = image;
            }
            else
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

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (listProduct.SelectedRows != null)
            {
                Byte i = Convert.ToByte(id);
                Product p = db.Products.FirstOrDefault(att => att.id.Equals(i));
                p.name = txtName.Text;
                p.price = Convert.ToDouble(txtPrice.Text);
                p.image = txtImage.Text;
                p.quantity =Convert.ToInt32(txtQuantity.Text);
                p.code = txtCode.Text;
                Category cat = cboCate.SelectedItem as Category;
                p.cat_id = cat.id;
                db.SaveChanges();
                Attribute a = cboColor.SelectedItem as Attribute;
                Attribute b = cboSize.SelectedItem as Attribute;
                if (a != null)
                {
                    productAttr pattr = db.productAttrs.FirstOrDefault(att => att.id.Equals(idC));
                    pattr.attr_id = a.id;
                }
               if(b != null)
                {
                    productAttr PaS = db.productAttrs.FirstOrDefault(att => att.id.Equals(idS));
                    PaS.attr_id = b.id;
                }
               
                db.SaveChanges();
                autoLoad();
            }
            else
            {
                MessageBox.Show("Chọn sản phẩm trước khi sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (listProduct.SelectedRows != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Byte i = Convert.ToByte(id);
                        Product p = db.Products.FirstOrDefault(a => a.id.Equals(i));

                        var prod = db.BillDetails.Where(x => x.product_id == i).Select(x => x).ToList();
                        if (prod.Count() <= 0)
                        {
                            var pa = (from c in db.productAttrs
                                      where c.product_id == i
                                      select c);
                            db.Products.Remove(p);
                            db.productAttrs.RemoveRange(pa);
                            var file = image;
                            pictureBox1.Image.Dispose();
                            pictureBox1.Image = null;
                            using (var s = new FileStream(file, System.IO.FileMode.Open))
                            {
                                pictureBox1.Image = Image.FromStream(s);
                            }
                            System.IO.File.Delete(file);
                            db.SaveChanges();
                            autoLoad();
                        }
                        else
                        {
                            MessageBox.Show("Bạn không thể xóa sản phẩm này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Mời bạn chọn sản phẩm trước khi xóa","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }catch( Exception ex)
            {
                MessageBox.Show("Bạn không thể xóa sản phẩm này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSearch.Text))
            {
                autoLoad();
            }
            else
            {
                var result = db.selectProductSearch(txtMaSearch.Text);
                listProduct.DataSource = result;
            }
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
