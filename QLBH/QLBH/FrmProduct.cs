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
            var product = db.selectProductcate().ToList();
            listProduct.DataSource = product;
  
            
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
                db.productAttrs.Add(new productAttr { product_id = p.id, attr_id = attrSizeId });
                db.productAttrs.Add(new productAttr { product_id = p.id, attr_id = attrColorId });
               db.SaveChanges();
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
                MessageBox.Show("Chon truoc khi sua");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listProduct.SelectedRows != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Byte i = Convert.ToByte(id);
                    Product p = db.Products.FirstOrDefault(a => a.id.Equals(i));
                    var pa = (from c in db.productAttrs
                                   where c.product_id  == i
                                   select c);
                    db.Products.Remove(p);
                    db.productAttrs.RemoveRange(pa);
                    var file = image;
                    using (var s = new FileStream(file, System.IO.FileMode.Open))
                    {
                        pictureBox1.Image = Image.FromStream(s);
                    }
                    System.IO.File.Delete(file);
                    db.SaveChanges();
                    autoLoad();
                }
            }
            else
            {
                MessageBox.Show("Chon truoc khi xoa");
            }
        }
    }
}
