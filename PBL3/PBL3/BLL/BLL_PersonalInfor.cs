using PBL3.DAL;
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
    class BLL_PersonalInfor
    {
        private static BLL_PersonalInfor _Instance;
        public static BLL_PersonalInfor Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLL_PersonalInfor();
                return _Instance;
            }
            private set { }
        }
        public ACCOUNT Update(string ID, string Email, string SDT, Image image)
        {
            CSDL db = new CSDL();
            ACCOUNT a = db.ACCOUNTs.Find(Convert.ToInt32(ID));
            byte[] i = null;
            if (image != null)
            {
                i = BLL_PersonalInfor.Instance.ConvertImageToBinary(image);
            }
            a.EMAIL = Email;
            a.SDT = SDT;
            a.PhotoAC = i;
            db.SaveChanges();
            return a;
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
    }
}
