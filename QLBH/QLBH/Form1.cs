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
    public partial class Form1 : Form
    {
        QLBHModel db = new QLBHModel();
        public Form1()
        {
            InitializeComponent();
        }
        void Login(string email, string password)
        {
            var result = db.Users.Where(x => x.email == email && x.password == password).SingleOrDefault();
            if (result != null)
            {
                MessageBox.Show("Đã vào");
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var result = db.Products.Select(x=>x.name).ToList();
            listAccount.DataSource = result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAttrbute f = new frmAttrbute();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}
