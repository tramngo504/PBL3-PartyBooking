using PBL3.BLL;
using PBL3.DAL;
using PBL3.DTO;
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
    public partial class ThucDon : Form
    {
        public ThucDon(ACCOUNT acc)
        {
            InitializeComponent();
            SetCBB();
            SetCBB_Category();
            checkAccount(acc);
            Show("", "");
        }
        public void checkAccount(ACCOUNT a)
        {
            if (a.TypeAcc.Equals("Staff"))
            {
                btnAdd.Visible = false;
                btnDel.Visible = false;
                btnEdit.Visible = false;
                btnReset.Visible = false;
                btn_LoadImage.Visible = false;
            }
        }
        public void SetCBB_Category()
        {
            CSDL db = new CSDL();
            cb_Category.Items.Add(new CBBItem { Text = "All", Value = 0 });
            foreach (FOODCATEGORY i in db.FOODCATEGORies)
            {
                cb_Category.Items.Add(new CBBItem
                {
                    Text = i.NameFCategory,
                    Value = i.IDFoodCategory
                });
            }
            cb_Category.SelectedIndex = 0;
        }
        public void SetCBB()
        {
            CSDL db = new CSDL();
            foreach (FOODCATEGORY i in db.FOODCATEGORies)
            {
                cbFoodCategory.Items.Add(new CBBItem
                {
                    Text = i.NameFCategory,
                    Value = i.IDFoodCategory
                });
            }
            cbFoodCategory.SelectedIndex = 0;
        }

        public void Show(string category, string search)
        {
            dtgvFood.DataSource = BLL_ThucDon.Instance.GetList(cb_Category.Text, txbSearch.Text);
            dtgvFood.Columns[0].HeaderText = "ID";
            dtgvFood.Columns[1].HeaderText = "Tên món ăn";
            dtgvFood.Columns[2].HeaderText = "Loại món ăn";
            dtgvFood.Columns[3].HeaderText = "Giá món";
            dtgvFood.Columns[4].HeaderText = "Nguyên liệu";
            dtgvFood.Columns[0].Visible = false;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                Show(cb_Category.Text, txbSearch.Text);
            }
            catch
            {
                MessageBox.Show("Lỗi!");
            }
        }

        private void dtgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CSDL db = new CSDL();
                int id = (int)dtgvFood.CurrentRow.Cells["IDFood"].Value;
                ShowData(id);
            }
            catch { }
        }
        public void ShowData(int ID)
        {
            try
            {
                CSDL db = new CSDL();
                FOOD f = db.FOODs.Find(ID);
                txbIDFood.Text = f.IDFood.ToString();
                txbNameFood.Text = f.NameFood;
                txbPrice.Text = f.PriceFood.ToString();
                txbMaterial.Text = f.Material;
                cbFoodCategory.Text = f.FOODCATEGORY.NameFCategory;
                if (f.PhotoFood != null)
                {
                    ptbFood.Image = BLL_ThucDon.Instance.ConvertBinaryToImage(f.PhotoFood);
                }
                else
                {
                    ptbFood.Image = null;
                }
            }
            catch { }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CSDL db = new CSDL();
            try
            {
                var n = (from p in db.FOODs where p.NameFood == txbNameFood.Text select p).Count();
                if (n == 0)
                {
                    int id = BLL_ThucDon.Instance.AddFood(txbNameFood.Text, Convert.ToDouble(txbPrice.Text), txbMaterial.Text,
                    cbFoodCategory.Text, ptbFood.Image);
                    Show(cb_Category.Text, txbSearch.Text);
                    MessageBox.Show("Thêm thành công!");
                    ShowData(id);
                }
                else MessageBox.Show("Món ăn đã tồn tại!");
            }
            catch
            {
                MessageBox.Show("Nhập chính xác thông tin!");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            CSDL db = new CSDL();
            if (txbIDFood.Text != "")
            {
                try
                {
                   BLL_ThucDon.Instance.EditFood(Convert.ToInt32(txbIDFood.Text), cbFoodCategory.Text, txbNameFood.Text,
                        Convert.ToDouble(txbPrice.Text), txbMaterial.Text, ptbFood.Image);
                    MessageBox.Show("Sửa thành công!");
                    Show(cb_Category.Text, txbSearch.Text);
                }
                catch
                {
                    MessageBox.Show("Nhập chính xác thông tin!");
                }
            }
            else
                MessageBox.Show("Chưa có thông tin!");
        }
        public void Reset()
        {
            txbIDFood.Text = "";
            txbNameFood.Text = "";
            txbPrice.Text = "";
            txbMaterial.Text = "";
            cbFoodCategory.Text = "";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txbIDFood.Text != "")
                {
                    int ID = (int)dtgvFood.CurrentRow.Cells["IDFood"].Value;
                    BLL_ThucDon.Instance.DeleteFood(txbIDFood.Text, ID);
                    MessageBox.Show("Xóa thành công!");
                    Show(cb_Category.Text, txbSearch.Text);
                    Reset();
                }
                else MessageBox.Show("Chưa có thông tin!");
            }
            catch
            {
                MessageBox.Show("Lỗi!");
            }
        }

        private void ptb_Sort_Click(object sender, EventArgs e)
        {
            try
            {
                List<FoodView> list = new List<FoodView>();
                list = BLL_ThucDon.Instance.GetList(cb_Category.Text, txbSearch.Text);
                FoodView[] arr = list.ToArray();
                BLL_ThucDon.Instance.Sort(arr);
                dtgvFood.DataSource = arr;
            }
            catch { }
        }

        private void btn_LoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ptbFood.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void txbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
