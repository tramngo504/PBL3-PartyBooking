using PBL3.BLL;
using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
            SetCa();
            SetPT();
            SetSearch();
        }
        public int idBill;
        public float tong { get; set; }
        bool check;
        int c = 0;
        public void ShowTT(float total)
        {
            tong = total;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtIDBill.Text != "")
            {
                int idbill = Convert.ToInt32(txtIDBill.Text);
                Menu mn = new Menu(idbill, c);
                mn.tt += new Menu.PriceMenu(ShowTT);
                mn.ShowDialog();
                this.Show();
            }
        }
        public void Enable()
        {
            txtIDBill.Enabled = false;
            txtName.Enabled = false;
            txtCMND.Enabled = false;
            txtSDT.Enabled = false;
            cbbPT.Enabled = false;
            cbbCa.Enabled = false;
            cbbSanh.Enabled = false;
            txtBan.Enabled = false;
            btnEdit.Enabled = false;
            btnDel.Enabled = false;
        }
        public void Unenable()
        {
            cbbCa.Enabled = true;
            cbbSanh.Enabled = true;
            txtBan.Enabled = true;
            btnEdit.Enabled = true;
            btnDel.Enabled = true;
        }
        public void Reset()
        {
            txtIDBill.Text = "";
            txtName.Text = "";
            txtCMND.Text = "";
            txtSDT.Text = "";
            cbbCa.Text = null;
            cbbSanh.Text = null;
            cbbPT.Text = null;
            txtBan.Text = "";
        }
        public void ShowDG(DateTime NgayTC)
        {
            dataGridView1.DataSource = BLL_TrangChu.Instance.ShowTC(NgayTC);
        }
        public void ShowTC(DateTime NgayTC)
        {
            dataGridView1.DataSource = BLL_TrangChu.Instance.Show(NgayTC);
        }
        public void SetCa()
        {
            cbbCa.Items.Add(new CBBItem { Text = "9h - 14h", Value = 1 });
            cbbCa.Items.Add(new CBBItem { Text = "16h - 19h", Value = 2 });
        }
        public void SetPT()
        {
            using (CSDL db = new CSDL())
            {
                foreach (PARTY i in BLL_Party.Instance.GetListParty(0))
                {
                    cbbPT.Items.Add(new CBBItem
                    {
                        Text = i.NamePT,
                        Value = i.IDPARTY
                    });
                }
            }
        }
        public void SetSearch()
        {
            int count = 0;
            cbbSearch.Items.Add(new CBBItem { Text = "CMND", Value = count++ });
            cbbSearch.Items.Add(new CBBItem { Text = "SDT", Value = count++ });
            cbbSearch.SelectedIndex = 0;
        }

        public void SetSanh()
        {
            DateTime NgayTC = dateTimePicker1.Value;
            if (cbbCa.SelectedItem != null)
            {
                int SLBan = 0;
                if (txtBan.Text != "") SLBan = Convert.ToInt32(txtBan.Text);
                int Ca = ((CBBItem)cbbCa.SelectedItem).Value - 1;
                cbbSanh.Items.AddRange(BLL_Sanh.Instance.AddCBBsanh(NgayTC, Ca, SLBan, idBill).ToArray());
                cbbSanh.SelectedIndex = 0;
            }
        }


    private void button3_Click(object sender, EventArgs e)
        {
            DateTime NgayTC = dateTimePicker1.Value;
            ShowTC(NgayTC);
            check = false;
            c = 0;
            Enable();
            Reset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime NgayTC = dateTimePicker1.Value;
            string s = ((CBBItem)cbbSearch.SelectedItem).Text;
            switch (s)
            {
                case "CMND": dataGridView1.DataSource = BLL_TrangChu.Instance.SearchByKH(NgayTC, txtSearch.Text, null); break;
                case "SDT": dataGridView1.DataSource = BLL_TrangChu.Instance.SearchByKH(NgayTC, null, txtSearch.Text); break;
            }
            check = true;
            c = 1;
            Unenable();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Today;
            DateTime NgayTC = dateTimePicker1.Value;
            ShowTC(NgayTC);
            dataGridView1.Columns["IDBILL"].HeaderText = "Mã Bill";
            dataGridView1.Columns["NameKH"].HeaderText = "Tên khách hàng";
            dataGridView1.Columns["NameSanh"].HeaderText = "Tên Sảnh";
            dataGridView1.Columns["NamePT"].HeaderText = "Tên Tiệc";
            dataGridView1.Columns["Time"].HeaderText = "Ca tổ chức";
            dataGridView1.Columns["Quantity"].HeaderText = "Số lượng bàn";
            check = false;
            Enable();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
                if (dataGridView1.CurrentCell != null)
                {
                    idBill = Convert.ToInt32(dataGridView1.CurrentRow.Cells["IDBILL"].Value.ToString());
                    BILL bill = BLL_TrangChu.Instance.GetBill(idBill);
                     int idsanh =(int)bill.IDSanh;
                    cbbSanh.Text = BLL_Sanh.Instance.GetSanh(idsanh).NameSanh;
                    cbbPT.SelectedIndex = (int)bill.IDPARTY - 1;
                    cbbCa.SelectedIndex = (int)bill.Time - 1;
                    txtBan.Text = bill.Quantity.ToString();
                    txtIDBill.Text = bill.IDBILL.ToString();
                    if (check == true)
                    {
                        CUSTOMER cus = BLL_TrangChu.Instance.GetCustomer(idBill);
                        txtName.Text = cus.NameKH;
                        txtSDT.Text = cus.SDT;
                        txtCMND.Text = cus.CMND;
                    }
                }
            
        }
        public float Tongtien(int idSanh, int idParty, float GiaTD, int slban)
        {
            SANH s = BLL_Sanh.Instance.GetSanh(idSanh);
            PARTY p = BLL_Party.Instance.GetParty(idParty);
            float a = 0, b = 0, c = 0, total;
            try
            {
                a = (float) s.PriceSanh;
                b = (float) p.PricePT;
                c =  GiaTD;
            }
            catch { }
            total = a + b + c * slban;
            return total;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int idbill = Convert.ToInt32(txtIDBill.Text);
                int idparty = ((CBBItem)cbbPT.SelectedItem).Value;
                int idca = ((CBBItem)cbbCa.SelectedItem).Value;
                int slban = Convert.ToInt32(txtBan.Text);
                int idsanh = ((CBBItem)cbbSanh.SelectedItem).Value;
                float GiaTD = tong;
                float total = Tongtien(idsanh, idparty, GiaTD, slban);
                BLL_TrangChu.Instance.UpdateBill(idbill, idsanh, idca, slban, total);
                MessageBox.Show("Sửa thành công");
                ShowDG(dateTimePicker1.Value);
            }
            catch
            {
                MessageBox.Show("Sửa không thành công");
            }
            cbbSanh.SelectedIndex = 0;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int idbill = Convert.ToInt32(dataGridView1.CurrentRow.Cells["IDBILL"].Value.ToString());
            DateTime NgayTC = dateTimePicker1.Value;
            try
            {
                int idKH = BLL_TrangChu.Instance.GetCustomer(idbill).IDKH;
                BLL_TrangChu.Instance.DelBill(idbill);
                MessageBox.Show("Xoa thanh cong");
                ShowDG(NgayTC);
                BLL_KhachHang.INSTANCE.DeleteKH(idKH);
            }
            catch
            {

            }
        }

        
        private void txtBan_TextChanged(object sender, EventArgs e)
        {
            cbbSanh.Items.Clear();
            SetSanh();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbbCa_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbSanh.Items.Clear();
            SetSanh();
        }
    }
}
