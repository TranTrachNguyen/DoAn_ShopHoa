using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHoa.Models
{
    public class GioMuaHang
    {
        dbShopHoaDataContext data = new dbShopHoaDataContext();
        public int MaHoa { get; set; }
        public string TenHoa { get; set; }
        public string AnhHoa { get; set; }
        public Double GiaBan { get; set; }
        public int SoLuong { get; set; }
        public Double ThanhTien
        {
            get { return GiaBan * SoLuong; }
        }
        public GioMuaHang(int maHoa)
        {
            MaHoa = maHoa;
            Hoa hoa = data.Hoas.Single(n => n.MaHoa == maHoa);
            TenHoa = hoa.TenHoa;
            AnhHoa = hoa.AnhHoa;
            GiaBan = double.Parse(hoa.GiaBan.ToString());
            SoLuong = 1;
        }
    }
}