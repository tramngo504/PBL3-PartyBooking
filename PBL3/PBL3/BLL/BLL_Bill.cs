using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DAL;
using PBL3.DTO;

namespace PBL3.BLL
{
    class BLL_Bill
    {
        private static BLL_Bill _instance;
        public static BLL_Bill INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BLL_Bill();
                }
                return _instance;
            }
            private set { }
        }
        public void AddBILL(BILL bill)
        {
            CSDL da = new CSDL();
            da.BILLs.Add(bill);
            da.SaveChanges();
        }
        public void UpdateBill(BILL bill)
        {
            CSDL da = new CSDL();
            BILL bll = da.BILLs.Find(bill.IDBILL);
            bll.IDPARTY = bill.IDPARTY;
            bll.IDSanh = bill.IDSanh;
            bll.BookingDate = bill.BookingDate;
            bll.CreateDate = bill.CreateDate;
            bll.Quantity = bill.Quantity;
            bll.DATCOC = bill.DATCOC;
            bll.TOTAL = bill.TOTAL;
            bll.Time = bill.Time;
            da.SaveChanges();
        }
        public void DeleteBill(int idB)
        {
            CSDL da = new CSDL();
            BILL bill = da.BILLs.Find(idB);
            da.BILLs.Remove(bill);
            da.SaveChanges();
        }
        public BILL getTTBill(int idB)
        {
            CSDL da = new CSDL();
            return da.BILLs.Find(idB);
        }
        public List<TKHDView> ThongkeHD(DateTime DateIn, DateTime DateOut)
        {
            CSDL db = new CSDL();
            var l = from c in db.BILLs
                    where (DateTime.Compare((DateTime)c.BookingDate, DateIn) >= 0 && DateTime.Compare((DateTime)c.BookingDate, DateOut) <= 0)
                    select new TKHDView
                    {
                        IDBILL = c.IDBILL,
                        NAMEKH = c.CUSTOMER.NameKH,
                        CMND = c.CUSTOMER.CMND,
                        SDT = c.CUSTOMER.SDT,
                        TIEC = c.PARTY.NamePT,
                        SANH = c.SANH.NameSanh,
                        SOLUONG = (int)c.Quantity,
                        STATUS = c.STATUS,
                        BookingDate = (DateTime)c.BookingDate,
                        TOTAL =(double)c.TOTAL
                 };
            return l.ToList();
        }
        public List<TKHDView> Search(DateTime DateIn, DateTime DateOut, string Status)
        {
            CSDL db = new CSDL();
            var l = from p in ThongkeHD(DateIn, DateOut)
                    where p.STATUS == Status
                    select p;
            return l.ToList();
        }
        public double GetRevenue(int nam, int thang)
        {
            CSDL da = new CSDL();
            double dt = 0;
            try
            {
                double datcoc = da.BILLs.Where(p => p.PayDay.Value.Year == nam && p.PayDay.Value.Month == thang && p.STATUS.Equals("Đã hoàn thành")).Select(p => p.DATCOC).Sum();
                double tt = (double)da.BILLs.Where(p => p.PayDay.Value.Year == nam && p.PayDay.Value.Month == thang && p.STATUS.Equals("Đã hoàn thành")).Select(p => p.TOTAL).Sum();
                dt = tt + datcoc;
            }
            catch { }

            return dt;
        }
        public int GetYear()
        {
            CSDL db = new CSDL();
            int year = 0;
            BILL[] b = (from p in db.BILLs where p.STATUS.Equals("Đã hoàn thành") select p).ToArray();
            for(int i = 0 ; i < b.Length - 1; ++i)
                for(int j = i + 1; j <b.Length; ++j)
                {
                    if (b[i].PayDay.Value.Year >= b[j].PayDay.Value.Year) year = b[j].PayDay.Value.Year;
                }
            return year;
        }
    }
}
