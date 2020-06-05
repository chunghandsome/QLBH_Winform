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
    public partial class frmAttrbute : Form
    {
        String id;
        QLBHEntities2 db = new QLBHEntities2();
        public frmAttrbute()
        {
            InitializeComponent();
        }

        private void autoLoad()
        {
            var r = db.getAttr().ToList();
            dataAttr.DataSource = r;
            dataAttr.Columns[0].HeaderText = "Mã thuộc tính";
            dataAttr.Columns[1].HeaderText = "Giá trị thuộc tính";
            dataAttr.Columns[2].HeaderText = "Kiểu thuộc tính";
        }
        private void frmAttrbute_Load(object sender, EventArgs e)
        {

            autoLoad();
           List<String> ListItem = new List<String>() { "Color", "Size" };
            selectType.DataSource = ListItem;
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
               byte t = 1;
                if (selectType.SelectedItem.ToString() == "Color")
                {
                    t = 0;
                }
                db.Attributes.Add(new Attribute { type = t, value = txtValue.Text });
                db.SaveChanges();
                MessageBox.Show("Thêm thành công!", "Thêm mới");
                autoLoad();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Them moi khong thanh cong");
            }
          

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataAttr.SelectedRows != null)
            {
                Byte i = Convert.ToByte(id);
                Attribute attr = db.Attributes.FirstOrDefault(att => att.id.Equals(i));
                if(selectType.SelectedItem.ToString() == "Color")
                {
                    attr.type = 0;
                }else
                {
                    attr.type = 1;
                }
                attr.value = txtValue.Text;
                db.SaveChanges();
                autoLoad();

            }
            else
            {
                MessageBox.Show("Chon truoc khi sua");
            }

        }

        private void dataAttr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataAttr.Rows[index];
            id = selectedRow.Cells[0].Value.ToString();
            txtValue.Text = selectedRow.Cells[1].Value.ToString();
            if(selectedRow.Cells[2].Value.ToString() == "0")
            {
                selectType.SelectedItem = "Color";
            }else
            {
                selectType.SelectedItem = "Size";
            }
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataAttr.SelectedRows != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Byte i = Convert.ToByte(id);
                    Attribute attr = db.Attributes.FirstOrDefault(a => a.id.Equals(i));
                    var p = db.productAttrs.Where(x => x.attr_id == i).Select(x => x).ToList();
                    if( p.Count() <= 0)
                    {
                        db.Attributes.Remove(attr);
                        db.SaveChanges();
                        autoLoad();
                    }else
                    {
                        MessageBox.Show("Bạn không thể xóa Thuộc tính này ");
                    }
                   
                }
            }else
            {
                MessageBox.Show("Chon truoc khi xoa");
            }
        }
    }
}
