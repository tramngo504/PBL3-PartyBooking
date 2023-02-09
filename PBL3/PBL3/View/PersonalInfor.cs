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
    public partial class PersionalInfor : Form
    {
        ACCOUNT account;
        public PersionalInfor(ACCOUNT acc)
        {
            InitializeComponent();
            account = acc;
            ShowUser(acc);
            CheckAcc();
        }
        public void CheckAcc()
        {
            if (account.TypeAcc.Equals("Staff"))
            {
                txbCMND.Enabled = false;
                btn_LoadImage.Enabled = false;
            }
        }
        public void ShowUser(ACCOUNT acc)
        {
            txbSDT.Text = acc.SDT;
            txbUsername.Text = acc.USERNAME;
            txbPassword.Text = acc.PASS;
            txbHoTen.Text = acc.NAME;
            txbChucVu.Text = acc.CHUCVU;
            txbCMND.Text = acc.CMND;
            txbEmail.Text = acc.EMAIL;
            txbID.Text = acc.IDTK.ToString();
            if(acc.PhotoAC != null)
            {
                ptbAvatar.Image = BLL_PersonalInfor.Instance.ConvertBinaryToImage(acc.PhotoAC);
            }
            else
            {
                ptbAvatar.Image = null;
            }    
            txbChucVu.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(txbSDT.Text.Length == 10)
                {
                    ACCOUNT b = BLL_PersonalInfor.Instance.Update(txbID.Text, txbEmail.Text,
                   txbSDT.Text, ptbAvatar.Image);
                    ShowUser(b);
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("SDT không phù hợp");
                }
            }
            catch
            {
                MessageBox.Show("Nhập chính xác thông tin!");
            }
        }

        private void btn_LoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ptbAvatar.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void txbHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txbCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
