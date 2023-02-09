using PBL3.DAL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    class BLL_ThucDon
    {
        private static BLL_ThucDon _Instance;
        public static BLL_ThucDon Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_ThucDon();
                }
                return _Instance;
            }
            private set { }
        }
        public List<FoodView> GetList(string category, string search)
        {
            CSDL db = new CSDL();
            if ((category == "All") && search == "")
            {
                var food = from p in db.FOODs
                           select new FoodView
                           {
                               IDFood = p.IDFood,
                               NameFood = p.NameFood,
                               NameFCategory = p.FOODCATEGORY.NameFCategory,
                               Price = p.PriceFood,
                               Material = p.Material
                           };
                return food.ToList();
            }
            else if (category == "All" && search != "")
            {
                var food = from p in db.FOODs
                           where p.NameFood.Contains(search)
                           select new FoodView
                           {
                               IDFood = p.IDFood,
                               NameFood = p.NameFood,
                               NameFCategory = p.FOODCATEGORY.NameFCategory,
                               Price = p.PriceFood,
                               Material = p.Material
                           };
                return food.ToList();
            }
            else
            {
                var food = from p in db.FOODs
                           where p.FOODCATEGORY.NameFCategory == category
                            && p.NameFood.Contains(search)
                           select new FoodView
                           {
                               IDFood = p.IDFood,
                               NameFood = p.NameFood,
                               NameFCategory = p.FOODCATEGORY.NameFCategory,
                               Price = p.PriceFood,
                               Material = p.Material
                           };
                return food.ToList();
            }
        }
        public int AddFood(string name, double price, string material, string category, Image image)
        {
            CSDL db = new CSDL();
            int id = (int)(from p in db.FOODCATEGORies where p.NameFCategory == category select p.IDFoodCategory).FirstOrDefault();
            byte[] i = null;
            if (image != null)
            {
                i = BLL_ThucDon.Instance.ConvertImageToBinary(image);
            }
            FOOD f = new FOOD
            {
                NameFood = name,
                PriceFood = price,
                Material = material,
                IDFoodCategory = id,
                PhotoFood = i
            };
            db.FOODs.Add(f);
            db.SaveChanges();
            return f.IDFood;
        }
        public void DeleteFood(string txt, int id)
        {
            CSDL db = new CSDL();
            if (txt != "")
            {
                FOOD f = db.FOODs.Find(id);
                db.FOODs.Remove(f);
                db.SaveChanges();
            }
        }
        public void EditFood(int ID, string NameCategory, string Name, double Price, string Material, Image image)
        {
            CSDL db = new CSDL();
            FOOD f = db.FOODs.Find(ID);
            int id = (from p in db.FOODCATEGORies where p.NameFCategory == NameCategory
                      select p.IDFoodCategory).FirstOrDefault();
            byte[] i = null;
            if (image != null)
            {
                i = BLL_ThucDon.Instance.ConvertImageToBinary(image);
            }             
            f.IDFoodCategory = id;
            f.NameFood = Name;
            f.PriceFood = Price;
            f.Material = Material;
            f.PhotoFood = i;
            db.SaveChanges();
        }
        public Image ConvertBinaryToImage(byte[] data)
        {
            Image img = null;
            if (data != null)
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    img = Image.FromStream(ms);
                }
            }
            return img;
            
        }
        public byte[] ConvertImageToBinary(Image img)
        {
            using (var ms = new MemoryStream())
            {
                ImageFormat format;
                Bitmap bmp = new Bitmap(img);
                switch (img.ToString())
                {
                    case "image/png":
                        format = ImageFormat.Png;
                        break;
                    case "image/gif":
                        format = ImageFormat.Gif;
                        break;
                    default:
                        format = ImageFormat.Jpeg;
                        break;
                }
                bmp.Save(ms, format);
                return ms.ToArray();
            }
        }
        public void Sort(FoodView[] arr)
        {
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                for (int j = i + 1; j < arr.Length; ++j)
                {
                    if (arr[i].Price > arr[j].Price)
                    {
                        FoodView temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }

    }
}
