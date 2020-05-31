using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH
{
    public partial class List : Form
    {
        QLBHEntities2 db = new QLBHEntities2();
        public List()
        {
            InitializeComponent();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
          
            int s = Convert.ToInt32(txtSearch.Text);
            var result = db.getProductByCode(s).ToList();
            if(result.Count > 0)
            {
                var attr = db.getAttrByProductId(result[0].Id_sản_phẩm).ToList();
                if (result != null)
                {
                    txtName.Text = result[0].Tên_sản_phẩm;
                    txtPrice.Text = result[0].Giá_sản_phẩm.ToString();
                    txtCate.Text = result[0].Tên_danh_mục;

                    foreach (getAttrByProductId_Result at in attr)
                    {
                        if (at.type == 1)
                        {
                            txtSize.Text = at.value;
                        }
                        else
                        {
                            txtColor.Text = at.value;
                        }

                    }

                }
            }
            
            else
            {
                MessageBox.Show("Mã sản phẩm không tồn tai");
            }
            
        }
        private void autoLoad()
        {
           
        }
        private double total(int ids)
        {
            var getPrice = db.getPrice1(ids).ToList();
            Double t = 0;
            foreach ( getPrice1_Result p in getPrice)
            {
                t += Convert.ToDouble(p.price * p.quantity);
            }

            return t;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
           try
            {
                int s = Convert.ToInt32(txtSearch.Text);
                var result = db.getProductByCode(s).ToList();
                if (result.Count > 0)
                {
                    int ids = 0;
                    var maHD = db.Bills.Where(x => x.maHoaDon == txtMaHoaDon.Text).Select(x => x).ToList();
                    if( maHD.Count() == 0) {
                        var bill = new Bill { userPhone = txtSdtKhach.Text, adminPhone = comMaNv.SelectedItem.ToString(), maHoaDon = txtMaHoaDon.Text };
                        db.Bills.Add(bill);
                        db.SaveChanges();
                        ids = bill.id;
                    }
                    else
                    {
                        ids = maHD[0].id;
                    }
                    int q = Convert.ToInt32(txtQuantity.Text);
                    db.BillDetails.Add(new BillDetail { product_id = result[0].Id_sản_phẩm ,bill_id =ids , quantity = q});
                    db.SaveChanges();
                    var a = db.getBillDetailbyBill1(ids);
                    listOrder.DataSource = a ;
                    txtTotal.Text = total(ids).ToString();
                    MessageBox.Show("Thêm thành công!", "Thêm mới");
                }
            }catch(Exception ex)
           {
                MessageBox.Show(ex.Message);
            }



        }
       
        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtMaNv_Load(object sender, EventArgs e)
        {
            var admin = db.Users.Where(x => x.role == 0).Select(x => x.name).ToList();
            comMaNv.DataSource = admin;
            txtMaHoaDon.Text = CreateKey("HDB");
           
        }
        public static string CreateKey(string tiento)
        {
            string key = tiento;
            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            //Ví dụ 07/08/2009
            string d = String.Format("{0}{1}{2}", partsDay[0], partsDay[1], partsDay[2]);
            key = key + d;
            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            //Ví dụ 7:08:03 PM hoặc 7:08:03 AM
            if (partsTime[2].Substring(3, 2) == "PM")
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            if (partsTime[2].Substring(3, 2) == "AM")
                if (partsTime[0].Length == 1)
                    partsTime[0] = "0" + partsTime[0];
            //Xóa ký tự trắng và PM hoặc AM
            partsTime[2] = partsTime[2].Remove(2, 3);
            string t;
            t = String.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }
        public static string ConvertTimeTo24(string hour)
        {
            string h = "";
            switch (hour)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }

        private void btnSeachMaKh_Click(object sender, EventArgs e)
        {
            var user = db.Users.Where(x => x.phone == txtSdtKhach.Text).Select(x => x.name).ToList();
            if (user.Count() > 0)
            {
                txtTenKhach.Text = user[0];
            }else
            {
                MessageBox.Show("Khach hang chua dang ki the thanh vien");
            }
        }

        private void comMaNv_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void comMaNv_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            String phone = cmb.SelectedValue.ToString();
            var user = db.Users.Where(x => x.phone == phone).Select(x => x.name).ToList();
            if(user.Count() > 0)
            {
                txtTenNv.Text = user[0];
            }
        }
    }
}
