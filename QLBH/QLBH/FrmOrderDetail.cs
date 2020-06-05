﻿using System;
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
        String id;
        QLBHEntities2 db = new QLBHEntities2();
        String email = "";
        public List(String e)
        {
            email = e;
            InitializeComponent();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            var result = db.getProductByCodec(txtSearch.Text).ToList();
            if (result.Count > 0)
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
        private void autoLoad(int BillId)
        {
            var order = db.getBillDetail6(BillId).ToList();
            listOrder.DataSource = order;
            listOrder.Columns[0].HeaderText = "Id sản phẩm";
            listOrder.Columns[1].HeaderText = "Mã sản phẩm";
            listOrder.Columns[2].HeaderText = "Tên sản phẩm";
            listOrder.Columns[3].HeaderText = "Mau sac";
            listOrder.Columns[4].HeaderText = "Kich co";
            listOrder.Columns[5].HeaderText = "Loai sản phẩm";
            listOrder.Columns[6].HeaderText = "Giá sản phẩm";
            listOrder.Columns[7].HeaderText = "So luong sản phẩm";
            listOrder.Columns[8].HeaderText = "Ma hoa don";
            listOrder.Columns[9].HeaderText = "Tong tien";

        }
        private double total(int ids)
        {
            var getPrice = db.getPrice1(ids).ToList();
            Double t = 0;
            foreach (getPrice1_Result p in getPrice)
            {
                t += Convert.ToDouble(p.price * p.quantity);
            }

            return t;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var result = db.getProductByCodec(txtSearch.Text).ToList();
                var quantity = db.Products.Where(x => x.code == txtSearch.Text).Select(x => x.quantity).First();
                if (quantity > 0 && quantity >= Convert.ToInt32(txtQuantity.Text))
                {
                    if (result.Count > 0)
                    {
                        int ids = 0;
                        var maHD = db.Bills.Where(x => x.maHoaDon == txtMaHoaDon.Text).Select(x => x).ToList();
                        if (maHD.Count() == 0)
                        {
                            var bill = new Bill { userPhone = txtSdtKhach.Text, adminPhone = txtSdtNv.Text, maHoaDon = txtMaHoaDon.Text, create_at = DateTime.Now };
                            db.Bills.Add(bill);
                            db.SaveChanges();
                            ids = bill.id;
                        }
                        else
                        {
                            ids = maHD[0].id;
                        }
                        int q = Convert.ToInt32(txtQuantity.Text);
                        Double tt = Convert.ToDouble(result[0].Giá_sản_phẩm * q);
                        db.BillDetails.Add(new BillDetail { product_id = result[0].Id_sản_phẩm, bill_id = ids, quantity = q, total = tt, userPhone = txtSdtKhach.Text, adminPhone = txtSdtNv.Text, create_at = DateTime.Now });
                        db.SaveChanges();
                        autoLoad(ids);
                        txtTotal.Text = total(ids).ToString();
                        MessageBox.Show("Thêm thành công!", "Thêm mới");
                    }
                }
                else
                {
                    MessageBox.Show("Sản phẩm đã hết hàng hoặc số lượng không đủ");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtMaNv_Load(object sender, EventArgs e)
        {
            String em = email.ToString();

            var admin = db.Users.Where(x => x.email == em).Select(x => x).ToList();
            txtSdtNv.Text = em;
            txtTenNv.Text = admin[0].name;
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
            }
            else
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
            if (user.Count() > 0)
            {
                txtTenNv.Text = user[0];
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listOrder.SelectedCells.Count > 0)
            {
                Byte i = Convert.ToByte(id);
                BillDetail biD = db.BillDetails.FirstOrDefault(att => att.id.Equals(i));
                var qttProd = db.getProductQtt(i).First();
                int quan = Convert.ToInt32(txtQuantity.Text);
                if (qttProd >= quan)
                {
                    biD.quantity = quan;
                    biD.userPhone = txtSdtKhach.Text;
                    biD.quantity = Convert.ToInt32(txtQuantity.Text);
                    Double to = Convert.ToInt32(txtPrice.Text) * Convert.ToInt32(txtQuantity.Text);
                    biD.total = to;
                    db.SaveChanges();
                    var bill = db.BillDetails.Where(x => x.id == i).Select(x => x.bill_id).First();
                    int bi = (int)bill;
                    autoLoad(bi);
                    txtTotal.Text = total(bi).ToString();

                }
                else
                {
                    MessageBox.Show("Số lượng sản phẩm không đủ!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Bạn chọn sản phẩm trước khi sửa!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void listOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = listOrder.Rows[index];
            id = selectedRow.Cells[0].Value.ToString();
            int i = Convert.ToInt32(id);
            var BillId = db.BillDetails.Where(x => x.id == i).Select(x => x.bill_id).First();
            var info = db.getBillDetailtoList(BillId).First();
            txtName.Text = selectedRow.Cells[2].Value.ToString();
            txtPrice.Text = selectedRow.Cells[6].Value.ToString();
            txtCate.Text = selectedRow.Cells[5].Value.ToString();
            txtQuantity.Text = selectedRow.Cells[7].Value.ToString();
            txtSdtKhach.Text = info.userPhone;
            txtColor.Text = selectedRow.Cells[3].Value.ToString();
            txtSize.Text = selectedRow.Cells[4].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listOrder.SelectedRows != null)
            {
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Byte i = Convert.ToByte(id);
                    var bills = db.BillDetails.Where(x => x.id == i).Select(x => x).First();
                    int BillId = Convert.ToInt32(bills.bill_id);
                    var billTotal = db.BillDetails.Where(x => x.bill_id == BillId).ToList();
                    BillDetail b = db.BillDetails.FirstOrDefault(a => a.id.Equals(i));
                    db.BillDetails.Remove(b);

                    if (billTotal.Count == 0)
                    {
                        Bill bi = db.Bills.FirstOrDefault(a => a.id.Equals(BillId));
                        db.Bills.Remove(bi);
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                    //  listOrder.DataSource = db.getBillDetail(BillId);

                    autoLoad(BillId);
                }
            }
            else
            {
                MessageBox.Show("Chon truoc khi xoa");
            }
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                Bill ps = db.Bills.FirstOrDefault(at => at.maHoaDon == txtMaHoaDon.Text);
                Double t = Convert.ToDouble(txtTotal.Text);
                ps.total = t;

                var ProductIdQunatity = db.getProductIdQuantity1(txtMaHoaDon.Text).ToList();
                foreach (getProductIdQuantity1_Result p in ProductIdQunatity)
                {
                    var id = p.product_id;
                    Product prr = db.Products.FirstOrDefault(at => at.id == id);
                    var quant = p.ProductQuantity - p.quantity;
                    prr.quantity = quant;
                    db.SaveChanges();
                }
                MessageBox.Show("Thanh toán thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.None);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn không thể thanh toán","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
    }
}
