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
    public partial class FrmDetails : Form
    {
        QLBHEntities2 db = new QLBHEntities2();
        int idD;
        public FrmDetails(int id)
        {
            InitializeComponent();
             idD = id;
        }
        private void autoLoad()
        {
            var result = db.BillDetails.Where(x => x.bill_id == idD).Select(x => x).ToList();
            ListDetail.DataSource = result;
        }
        private void FrmDetails_Load(object sender, EventArgs e)
        {

            autoLoad();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(txtMa.Text != null)
            {
                String ma = txtMa.Text;
                ListDetail.DataSource = db.getBillDetailByMaProduct(ma, idD);
            }
            else
            {
                autoLoad();
            }
        }
    }
}
