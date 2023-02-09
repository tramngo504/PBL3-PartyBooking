using PBL3.DAL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    class BLL_HoaDon
    {
        
        private static BLL_HoaDon _Instance;
        static double T = 0, Total = 0;
        public static BLL_HoaDon Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_HoaDon();
                }
                return _Instance;
            }
            private set { }
        }
        public BILL ShowInfor(string cmnd, DateTime date, int time, string sdt)
        {
            try
            {
                CSDL db = new CSDL();
                BILL b = (from p in db.BILLs
                          where (p.CUSTOMER.CMND == cmnd || p.CUSTOMER.SDT == sdt) 
                          && DateTime.Compare((DateTime)p.BookingDate, date) == 0 
                          && p.Time == time && p.STATUS == "Chưa hoàn thành"
                          select p).FirstOrDefault();
                return b;
            }
            catch
            {
                return null;
            }
        }
        public List<MenuView> ShowMenu(string cmnd, DateTime date, int time, string sdt)
        {
            CSDL db = new CSDL();
            BILL b = ShowInfor(cmnd, date, time, sdt);
            MENUDETAIL menu = (from p in db.MENUDETAILs where p.IDBILL == b.IDBILL select p).FirstOrDefault();
            var menufood = from p in db.MENUDETAILs
                           where p.IDBILL == b.IDBILL
                           select new MenuView
                           {
                               NameFood =  p.FOOD.NameFood,
                               NameCategory = p.FOOD.FOODCATEGORY.NameFCategory,
                               PriceFood = p.FOOD.PriceFood,
                               SL = p.SLFood,
                               ThanhTien = (double)p.ThanhTien
                           };
            return menufood.ToList();
        }
        public BILL Confirm(BILL b, double tong, double chiphi, string incur, int discount, int id)
        {
            CSDL db = new CSDL();
            BILL bill = db.BILLs.Find(b.IDBILL);
            bill.TOTAL = tong;
            bill.INCURCOST = chiphi;
            bill.INCUR = incur;
            bill.DISCOUNT = discount;
            bill.IDTK = id;
            db.SaveChanges();
            return bill;
        }
        public double Cal(int Discount, double Chiphi, double Temp)
        {
            T = Temp + Chiphi;
            Total = T - (T * Discount / 100);
            return Total;
        }
    }
}
