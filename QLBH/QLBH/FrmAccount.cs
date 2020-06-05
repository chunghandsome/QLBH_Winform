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
    public partial class FrmAccount : Form
    {
        String id;
        QLBHEntities2 db = new QLBHEntities2();
        public FrmAccount()
        {
            InitializeComponent();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void autoLoad()
        {
            var r = db.Users.Select(x => x).ToList();
            dataUser.DataSource = r;
        }
        private void FrmAccount_Load(object sender, EventArgs e)
        {
            autoLoad();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPhone.Text) | !string.IsNullOrEmpty(txtName.Text) | !string.IsNullOrEmpty(txtPassword.Text))
                {
                    var u = db.Users.Where(x => x.phone == txtPhone.Text).ToList();
                   
                    if (u.Count() <= 0)
                    {
                        db.Users.Add(new User { name = txtName.Text, email = txtEmail.Text, phone = txtPhone.Text, password = txtPassword.Text, role = Convert.ToByte(checkAdmin.Checked) });
                        db.SaveChanges();
                        MessageBox.Show("Thêm thành công!", "Thêm mới");
                        autoLoad();
                    }else
                    {
                        MessageBox.Show("Ma khach hang da ton tai");
                    }
                }else
                {
                    MessageBox.Show("Nhập trước khi thêm");
                }
                                              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Them moi khong thanh cong");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataUser.SelectedRows != null)
            {
                Byte i = Convert.ToByte(id);
                User attr = db.Users.FirstOrDefault(att => att.id.Equals(i));
                attr.name = txtName.Text;
                attr.phone = txtPhone.Text;
                attr.email = txtEmail.Text;
                attr.password = txtPassword.Text;
              
                if (checkAdmin.Checked == true)
                {
                    attr.role = 1;
                }else
                {
                    attr.role = 0;
                }
               
                db.SaveChanges();
                autoLoad();
            }
            else
            {
                MessageBox.Show("Chon truoc khi sua");
            }
        }

        private void dataUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataUser.Rows[index];
            id = selectedRow.Cells[0].Value.ToString();
            txtName.Text = selectedRow.Cells[1].Value.ToString();
            txtPhone.Text  = selectedRow.Cells[2].Value.ToString();
            txtEmail.Text = selectedRow.Cells[3].Value.ToString();
            txtPassword.Text = selectedRow.Cells[4].Value.ToString();
            if(selectedRow.Cells[5].Value.ToString() == "1")
            {
                checkAdmin.Checked = true ;
            }else
            {
                checkAdmin.Checked = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dataUser.SelectedRows != null)
            {
                
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Byte i = Convert.ToByte(id);
                    User attr = db.Users.FirstOrDefault(a => a.id.Equals(i));
                   if( attr.role == 0)
                    {
                        db.Users.Remove(attr);
                        db.SaveChanges();
                        autoLoad();
                    }else
                    {
                        MessageBox.Show("Ban khong duoc phep xoa ");
                    }
                   
                }
            }
            else
            {
                MessageBox.Show("Chon truoc khi xoa");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhoneSearch.Text))
            {
                autoLoad();
            }
            else
            {
                var result = db.Users.Where(x => x.phone == txtPhoneSearch.Text).Select(x => x).ToList();
                dataUser.DataSource = result;
            }
        }

        private void dataUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
