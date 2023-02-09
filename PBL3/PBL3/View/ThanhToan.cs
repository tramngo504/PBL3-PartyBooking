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
using PBL3.BLL;
namespace PBL3
{
    public partial class ThanhToan : Form
    {
        public delegate void MyDel(int ID);
        public MyDel d;
        int ID = 0;
        ACCOUNT account;
        private void GetData(int _ID)
        {
            ID = _ID;
        }
        public ThanhToan(ACCOUNT acc)
        {
            account = acc;
            d = new MyDel(GetData);
            InitializeComponent();
        }

        private void bt_Pay_Click(object sender, EventArgs e)
        {
            try
            {
                CSDL db = new CSDL();
                BILL b = db.BILLs.Find(ID);
                b.STATUS = "Đã hoàn thành";
                b.PayDay = DateTime.Now;
                db.SaveChanges();
                bt_Print.Visible = true;
                bt_Pay.Visible = false;
            }
            catch { }
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            try
            {
                CSDL db = new CSDL();
                BILL b = db.BILLs.Find(ID);
                MENUDETAIL menu = (from p in db.MENUDETAILs where p.IDBILL == ID select p).FirstOrDefault();
                lbNameKH.Text = b.CUSTOMER.NameKH;
                lbCMND.Text = b.CUSTOMER.CMND;
                lbSDT.Text = b.CUSTOMER.SDT;
                lbAddress.Text = b.CUSTOMER.DIACHI;
                lbDateHold.Text = b.BookingDate.ToString();
                int time = (int)b.Time;
                if (time == 1)
                {
                    lbTime.Text = "9h00 - 13h00";
                }
                else { lbTime.Text = "16h00 - 20h00"; }
                lbNumber.Text = b.Quantity.ToString();
                lbDeposit.Text = b.DATCOC.ToString();
                lbCreateDate.Text = b.CreateDate.ToString();
                lbParty.Text = b.PARTY.NamePT;
                lbPriceParty.Text = b.PARTY.PricePT.ToString();
                lbHall.Text = b.SANH.NameSanh;
                lbPriceHall.Text = b.SANH.PriceSanh.ToString();
                lbPhatsinh.Text = b.INCUR;
                lbChiPhi.Text = b.INCURCOST.ToString();
                lbTongMenu.Text = BLL_MENUDETAIL.Instance.getTongTienMenuBan(menu.IDBILL).ToString();
                lbDiscount.Text = b.DISCOUNT.ToString();
                lbTotal.Text = b.TOTAL.ToString();
                lbDay.Text = DateTime.Now.Day.ToString();
                lbMonth.Text = Convert.ToString(DateTime.Now.Month);
                lbYear.Text = Convert.ToString(DateTime.Now.Year);
                var menufood = from p in db.MENUDETAILs
                               where p.IDBILL == b.IDBILL
                               select new
                               {
                                   p.FOOD.NameFood,
                                   p.FOOD.FOODCATEGORY.NameFCategory,
                                   p.FOOD.PriceFood,
                                   p.SLFood,
                                   p.ThanhTien
                               };
                dtgvMenu.DataSource = menufood.ToList();
                dtgvMenu.Columns[0].HeaderText = "Tên Món";
                dtgvMenu.Columns[1].HeaderText = "Loại Món";
                dtgvMenu.Columns[2].HeaderText = "Giá Món";
                dtgvMenu.Columns[3].HeaderText = "Số Lượng";
                dtgvMenu.Columns[4].HeaderText = "Thành Tiền";
                dtgvMenu.DefaultCellStyle.Font = new Font("Times New Roman", 8);
                dtgvMenu.Columns[0].Width = 100;
                dtgvMenu.Columns[1].Width = 45;
                dtgvMenu.Columns[2].Width = 45;
                dtgvMenu.Columns[3].Width = 20;
                dtgvMenu.Columns[4].Width = 75;
                bt_Print.Visible = false;
            }
            catch { }
        }

        private void ThanhToan_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CSDL db = new CSDL();
                BILL b = db.BILLs.Find(ID);
                b.STATUS = "Đã hoàn thành";
                b.PayDay = DateTime.Now;
                db.SaveChanges();
                TrangChuNV f = new TrangChuNV(account);
                this.Hide();
                f.ShowDialog();
            }
            catch { }
        }

        
        private void bt_Print_Click(object sender, EventArgs e)
        {
            //Add a panel control
            Panel panel = new Panel();
            this.Controls.Add(panel);
            // Create a Bitmap the same size of Form
            Graphics graphics = panel.CreateGraphics();
            Size size = this.ClientSize;
            bitmap = new Bitmap(size.Width, size.Height, graphics);
            graphics = Graphics.FromImage(bitmap);
            // Copy screen area that panel covers
            Point point = PointToScreen(panel.Location);
            graphics.CopyFromScreen(point.X, point.Y, 0, 0, size);
            // Show print preview
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
        Bitmap bitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Print the contents
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
