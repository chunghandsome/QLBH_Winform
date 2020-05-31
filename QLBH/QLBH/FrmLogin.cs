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
    public partial class FrmLogin : Form
    {
        QLBHEntities2 db = new QLBHEntities2();
        public FrmLogin()
        {
            InitializeComponent();
        }
        void Login(string email, string password)
        {
          
            var result = db.Users.Where(x => x.email == email && x.password == password).ToList();
            if (result.Count() > 0)
            {
                FrmMain m = new FrmMain();
                this.Hide();
                m.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var result = db.Products.Select(x=>x.name).ToList();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login(tk.Text, pass.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
