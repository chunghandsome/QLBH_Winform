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
    public partial class FrmMain : Form
    {
        String em = "";
        public FrmMain( String email)
        {

            em = email;
            InitializeComponent();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            FrmProduct p = new FrmProduct();
            p.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
             if(em =="admin@gmail.com")
            {
                FrmAccount ac = new FrmAccount();
                ac.Show();
            }else
            {
                MessageBox.Show("Bạn không có quyền vào quản lý người dùng");
            }
          
           
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAttr_Click(object sender, EventArgs e)
        {
            frmAttrbute attr = new frmAttrbute();
            attr.Show();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            FrmCategory cate = new FrmCategory();
            cate.Show();
        }

        private void btnUser_Click_1(object sender, EventArgs e)
        {
            if (em == "admin@gmail.com")
            {
                FrmAccount ac = new FrmAccount();
                ac.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền vào quản lý người dùng");
            }

            
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            List od = new List(em);
            od.Show();
        }

        private void btnProduct_Click_1(object sender, EventArgs e)
        {
            FrmProduct p = new FrmProduct();
            p.Show();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            new FrmOrder().Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
