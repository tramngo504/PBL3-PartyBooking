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
using PBL3.DAL;
//using Excel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;
namespace PBL3
{
    public partial class BillDattiec : Form
    {
        private static BILL bill;
        private static int IDBILL;
        public BillDattiec(int IDbill)
        {
            InitializeComponent();
            bill = BLL_Bill.INSTANCE.getTTBill(IDbill);
            IDBILL = IDbill;
            setTTKH();
            SetTTTiec();
            SetDGVTD();
        }
        private void setTTKH()
        {
            lbSo.Text = lbSo.Text + IDBILL.ToString();
            lbNTN.Text = "Hôm nay ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            lbHTen.Text = bill.CUSTOMER.NameKH;
            lbCMND.Text = bill.CUSTOMER.CMND;
            lbDC.Text = bill.CUSTOMER.DIACHI;
            lbSDT.Text = bill.CUSTOMER.SDT;
        }
        private void SetTTTiec()
        {
            lbLoaiHinhTiec.Text = bill.PARTY.NamePT;
            lbGiaT.Text = bill.PARTY.PricePT.ToString();
            lbSLban.Text = bill.Quantity.ToString();
            lbTenSanh.Text = bill.SANH.NameSanh;
            lbGiaSanh.Text = bill.SANH.PriceSanh.ToString();
            lbNgayDat.Text = bill.CreateDate.ToShortDateString();
            if (BLL_Bill.INSTANCE.getTTBill(IDBILL).Time == 1)
            {
                lbgio.Text = "9h00 - 13h00";
            }
            else lbgio.Text = "16h00 - 20h00";
            lbGTD.Text = BLL_MENUDETAIL.Instance.getTongTienMenuBan(IDBILL).ToString();
        }
        private void SetDGVTD()
        {
            dgvDSTD.DataSource = BLL_MENUDETAIL.Instance.getListMenuF(IDBILL);
            dgvDSTD.Columns["IdBill"].Visible = false;
            dgvDSTD.Columns["IDFood"].Visible = false;
            dgvDSTD.Columns["NameFood"].HeaderText = "Tên món";
            dgvDSTD.Columns["PriceFood"].HeaderText = "Giá món";
            dgvDSTD.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvDSTD.Columns["ThanhTien"].HeaderText = "Thành tiền";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            string time = "10h - 14h";
            if (bill.Time == 2)
            {
                time = "16h - 21h";
            }
            try
            {
                string saveExcelFile = @"C:\Users\TechCare\Documents\Test\HD.xlsx";
                //Excel.Application xlApp = new Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("Lỗi không thể sử dụng được thư viện EXCEL");
                    return;
                }
                xlApp.Visible = false;

                object misValue = System.Reflection.Missing.Value;

                Workbook wb = xlApp.Workbooks.Add(misValue);

                Worksheet ws = (Worksheet)wb.Worksheets[1];
                if (ws == null)
                {
                    MessageBox.Show("Không thể tạo được WorkSheet");
                    return;
                }
                int row = 1;
                string fontName = "Times New Roman";
                int fontSizeTieuDe = 14;
                int fontSizeTenTruong = 13;
                int fontSizeNoiDung = 11;
                Range row1_TieuDe = ws.get_Range("D1", "H1");
                row1_TieuDe.Merge();
                row1_TieuDe.Font.Size = fontSizeTieuDe;
                row1_TieuDe.Font.Name = fontName;
                row1_TieuDe.Value2 = "Cộng hòa xã hội chủ nghĩa Việt Nam";
                Range row2_TieuDe = ws.get_Range("D2", "H2");
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Size = fontSizeTieuDe;
                row2_TieuDe.Font.Name = fontName;
                row2_TieuDe.Value2 = "   Độc lập - tự do - hạnh phúc";
                Range row3_TieuDe = ws.get_Range("D4", "H4");
                row3_TieuDe.Merge();
                row3_TieuDe.Font.Size = fontSizeTieuDe;
                row3_TieuDe.Font.Name = fontName;
                row3_TieuDe.Font.Bold = true;
                row3_TieuDe.Value2 = "HỢP ĐỒNG KINH TẾ - ĐẶT TIỆC";
                Range row4_TieuDe = ws.get_Range("A5", "E5");
                row4_TieuDe.Merge();
                row4_TieuDe.Font.Size = fontSizeNoiDung;
                row4_TieuDe.Font.Italic = true;
                row4_TieuDe.Font.Name = fontName;
                row4_TieuDe.Value2 = "Hôm nay, ngày " + DateTime.Now.Day + ", tháng " + DateTime.Now.Month + ", năm " + DateTime.Now.Year;
                Range row5_TieuDe = ws.get_Range("A7", "D7");
                row5_TieuDe.Merge();
                row5_TieuDe.Font.Size = fontSizeTenTruong;
                row5_TieuDe.Font.Name = fontName;
                row5_TieuDe.Font.Bold = true;
                row5_TieuDe.Value2 = "Bên A : Nhà hàng tiệc cưới ForU";
                Range row6_TieuDe = ws.get_Range("A8", "D8");
                row6_TieuDe.Merge();
                row6_TieuDe.Font.Size = fontSizeNoiDung;
                row6_TieuDe.Font.Name = fontName;
                row6_TieuDe.Value2 = "Đại diện: Trần Thị Ngọc Như";
                Range row7_TieuDe = ws.get_Range("A9", "I9");
                row7_TieuDe.Merge();
                row7_TieuDe.Font.Size = fontSizeNoiDung;
                row7_TieuDe.Font.Name = fontName;
                row7_TieuDe.Value2 = "Địa chỉ: 29 Đ.Nguyễn Tất Thành phường Hòa Khánh quận Liên Chiểu TP Đà Nẵng";
                Range row8_TieuDe = ws.get_Range("A10", "B10");
                row8_TieuDe.Merge();
                row8_TieuDe.Font.Size = fontSizeNoiDung;
                row8_TieuDe.Font.Name = fontName;
                row8_TieuDe.Value2 = "MST:11203920100";
                Range row9_TieuDe = ws.get_Range("A11", "B11");
                row9_TieuDe.Merge();
                row9_TieuDe.Font.Size = fontSizeNoiDung;
                row9_TieuDe.Font.Name = fontName;
                row9_TieuDe.Value2 = "SĐT: 0919399421";
                Range row10_TieuDe = ws.get_Range("A12", "B12");
                row10_TieuDe.Merge();
                row10_TieuDe.Font.Size = fontSizeTenTruong;
                row10_TieuDe.Font.Name = fontName;
                row10_TieuDe.Font.Bold = true;
                row10_TieuDe.Value2 = "Bên B:";
                Range row11_TieuDe = ws.get_Range("A13", "D13");
                row11_TieuDe.Merge();
                row11_TieuDe.Font.Size = fontSizeNoiDung;
                row11_TieuDe.Font.Name = fontName;
                row11_TieuDe.Value2 = "Ông bà: " + bill.CUSTOMER.NameKH;
                Range row12_TieuDe = ws.get_Range("A14", "H14");
                row12_TieuDe.Merge();
                row12_TieuDe.Font.Size = fontSizeNoiDung;
                row12_TieuDe.Font.Name = fontName;
                row12_TieuDe.Value2 = "Địa chỉ: " + bill.CUSTOMER.DIACHI;
                Range row13_TieuDe = ws.get_Range("A15", "B15");
                row13_TieuDe.Merge();
                row13_TieuDe.Font.Size = fontSizeNoiDung;
                row13_TieuDe.Font.Name = fontName;
                row13_TieuDe.Value2 = "CMND: " + bill.CUSTOMER.CMND;
                Range row14_TieuDe = ws.get_Range("A16", "B16");
                row14_TieuDe.Merge();
                row14_TieuDe.Font.Size = fontSizeNoiDung;
                row14_TieuDe.Font.Name = fontName;
                row14_TieuDe.Value2 = "SĐT: " + bill.CUSTOMER.SDT;
                Range row15_TieuDe = ws.get_Range("A18", "H18");
                row15_TieuDe.Merge();
                row15_TieuDe.Font.Size = fontSizeNoiDung;
                row15_TieuDe.Font.Italic = true;
                row15_TieuDe.Font.Name = fontName;
                row15_TieuDe.Value2 = "Hai bên chúng tôi thỏa thuận với nhau những điều kiện sau: ";
                Range row25_TieuDe = ws.get_Range("A20", "C20");
                row25_TieuDe.Font.Bold = true;
                row25_TieuDe.Merge();
                row25_TieuDe.Font.Size = fontSizeTenTruong;
                row25_TieuDe.Font.Name = fontName;
                row25_TieuDe.Value2 = "I. Thông tin tiệc";
                Range row16_TieuDe = ws.get_Range("E20", "H20");
                row16_TieuDe.Merge();
                row16_TieuDe.Font.Size = fontSizeTenTruong;
                row16_TieuDe.Font.Bold = true;
                row16_TieuDe.Font.Name = fontName;
                row16_TieuDe.Value2 = "II.Danh sách thực đơn ";
                Range row28_TieuDe = ws.get_Range("A22", "D22");
                row28_TieuDe.Merge();
                row28_TieuDe.Font.Size = fontSizeNoiDung;
                row28_TieuDe.Font.Name = fontName;
                row28_TieuDe.Value2 = "Tên tiệc: " + bill.PARTY.NamePT;
                Range row17_TieuDe = ws.get_Range("A23", "D23");
                row17_TieuDe.Merge();
                row17_TieuDe.Font.Size = fontSizeNoiDung;
                row17_TieuDe.Font.Name = fontName;
                row17_TieuDe.Value2 = "Giá tiệc: " + bill.PARTY.PricePT;
                Range row18_TieuDe = ws.get_Range("A24", "D24");
                row18_TieuDe.Merge();
                row18_TieuDe.Font.Size = fontSizeNoiDung;
                row18_TieuDe.Font.Name = fontName;
                row18_TieuDe.Value2 = "Số lượng bàn: " + bill.Quantity;
                Range row19_TieuDe = ws.get_Range("A25", "D25");
                row19_TieuDe.Merge();
                row19_TieuDe.Font.Size = fontSizeNoiDung;
                row19_TieuDe.Font.Name = fontName;
                row19_TieuDe.Value2 = "Sảnh: " + bill.SANH.NameSanh;
                Range row22_TieuDe = ws.get_Range("A26", "D26");
                row22_TieuDe.Merge();
                row22_TieuDe.Font.Size = fontSizeNoiDung;
                row22_TieuDe.Font.Name = fontName;
                row22_TieuDe.Value2 = "Giá sảnh: " + bill.SANH.PriceSanh;
                Range row20_TieuDe = ws.get_Range("A27", "D27");
                row20_TieuDe.Merge();
                row20_TieuDe.Font.Size = fontSizeNoiDung;
                row20_TieuDe.Font.Name = fontName;
                row20_TieuDe.Value2 = "Thời gian: " + time + " ngày " + bill.CreateDate.ToShortDateString();
                Range row21_TieuDe = ws.get_Range("A28", "D28");
                row21_TieuDe.Merge();
                row21_TieuDe.Font.Size = fontSizeNoiDung;
                row21_TieuDe.Font.Name = fontName;
                row21_TieuDe.Value2 = "Giá thực đơn(1 bàn): " + BLL_MENUDETAIL.Instance.getTongTienMenuBan(bill.IDBILL);
                Range row23_TieuDe = ws.get_Range("A29", "D29");
                row23_TieuDe.Merge();
                row23_TieuDe.Font.Size = fontSizeNoiDung;
                row23_TieuDe.Font.Name = fontName;
                row23_TieuDe.Value2 = "Đặt cọc: " + bill.DATCOC;
                Range row24_TieuDe = ws.get_Range("A30", "D30");
                row24_TieuDe.Merge();
                row24_TieuDe.Font.Size = fontSizeNoiDung;
                row24_TieuDe.Font.Name = fontName;
                row24_TieuDe.Value2 = "Tổng tiền: " + bill.TOTAL;

                //Tạo Ô Tên món:
                Range row23_MaSP = ws.get_Range("E22", "G22");//Cột B dòng 2 và dòng 3
                row23_MaSP.Merge();
                row23_MaSP.Font.Size = fontSizeNoiDung;
                row23_MaSP.Font.Name = fontName;
                row23_MaSP.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row23_MaSP.Value2 = "Tên món";
                //Tạo Ô giá món :
                Range row23_TenSP = ws.get_Range("H22");//Cột C dòng 2 và dòng 3
                row23_TenSP.Merge();
                row23_TenSP.Font.Size = fontSizeNoiDung;
                row23_TenSP.Font.Name = fontName;
                row23_TenSP.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row23_TenSP.Value2 = "Giá món";

                //Tạo Ô số lượng :
                Range row2_GiaSP = ws.get_Range("I22");//Cột D->E của dòng 2
                row2_GiaSP.Merge();
                row2_GiaSP.Font.Size = fontSizeNoiDung;
                row2_GiaSP.Font.Name = fontName;
                row2_GiaSP.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row2_GiaSP.Value2 = "Số lượng";

                //Tạo Ô Giá Nhập:
                Range row3_GiaNhap = ws.get_Range("J22");//Ô D3
                row3_GiaNhap.Font.Size = fontSizeNoiDung;
                row3_GiaNhap.Font.Name = fontName;
                row3_GiaNhap.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row3_GiaNhap.Value2 = "Thành tiền";
                row = 22;//dữ liệu xuất bắt đầu từ dòng số 4 trong file Excel (khai báo 3 để vào vòng lặp nó ++ thành 4)

                //Viền header
                Range borderTemp = ws.get_Range("E22", "J22");
                borderTemp.Borders.Color = Color.Black.ToArgb();

                foreach (MenuFoodView fOOD in BLL_MENUDETAIL.Instance.getListMenuF(IDBILL))
                {
                    row++;
                    //
                    Range rowData = ws.get_Range("E" + row, "G" + row);
                    rowData.Merge();
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    rowData.Value2 = fOOD.NameFood;
                    //rowData.Borders.Color = Color.Black.ToArgb();
                    //
                    Range rowData1 = ws.get_Range("H" + row);
                    rowData1.Font.Size = fontSizeNoiDung;
                    rowData1.Font.Name = fontName;
                    rowData1.Value2 = fOOD.PriceFood;
                    Range rowData2 = ws.get_Range("I" + row);
                    rowData2.Font.Size = fontSizeNoiDung;
                    rowData2.Font.Name = fontName;
                    rowData2.Value2 = fOOD.SoLuong;
                    Range rowData3 = ws.get_Range("J" + row);
                    rowData3.Font.Size = fontSizeNoiDung;
                    rowData3.Font.Name = fontName;
                    rowData3.Value2 = fOOD.ThanhTien;
                    Range borderTemp2 = ws.get_Range("E" + row, "J" + row);
                    borderTemp2.Borders.Color = Color.Black.ToArgb();
                }

                //Lưu file excel xuống Ổ cứng
                wb.SaveAs(saveExcelFile);

                //đóng file để hoàn tất quá trình lưu trữ
                wb.Close(true, misValue, misValue);
                //thoát và thu hồi bộ nhớ cho COM
                xlApp.Quit();
                releaseObject(ws);
                releaseObject(wb);
                releaseObject(xlApp);

                //Mở File excel sau khi Xuất thành công
                System.Diagnostics.Process.Start(saveExcelFile);
            }
            catch
            {
                MessageBox.Show("Đã có lỗi");
            }*/
        }
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                obj = null;
            }
            finally
            { GC.Collect(); }
        }
    }
}
