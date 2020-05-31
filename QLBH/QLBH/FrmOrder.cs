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
    public partial class FrmOrder : Form
    {
        QLBHEntities2 db = new QLBHEntities2();
        public FrmOrder()
        {
            InitializeComponent();
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            var user = db.Users.Where(x => x.phone == txtPhone.Text ).Select(x => x).ToList();
            if ( user.Count() > 0)
            {
                txtName.Text = user[0].name;
                txtEmail.Text = user[0].email;
                txtSdt.Text = user[0].phone;
            }else
            {
                MessageBox.Show("So dien thoai chua dang ki");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                db.Categories.Add(new Category { Name = txtName.Text });
                db.SaveChanges();
                MessageBox.Show("Thêm thành công!", "Thêm mới");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Them moi khong thanh cong");
            }
        }
    }
}
