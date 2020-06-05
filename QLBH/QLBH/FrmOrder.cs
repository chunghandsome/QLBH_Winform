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
        String id;
        QLBHEntities2 db = new QLBHEntities2();
        public FrmOrder()
        {
            InitializeComponent();
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                listOrders.DataSource = db.Bills.ToList();
            }
            else
            {
                var bill = db.Bills.Where(x => x.maHoaDon == txtMa.Text).Select(x => x).ToList();
                if (bill.Count() > 0)
                {
                    listOrders.DataSource = bill;
                }
                else
                { 
                   MessageBox.Show("Mã hóa đơn không tông tại");
                }
               
            }

        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            listOrders.DataSource = db.getAllBill().ToList();
        }

        private void listOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = listOrders.Rows[index];
             id = selectedRow.Cells[0].Value.ToString();
            

        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(id);
            new FrmDetails(i).Show();
        }
    }
}
