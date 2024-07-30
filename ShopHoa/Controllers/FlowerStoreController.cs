using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHoa.Controllers
{
    public class FlowerStoreController : Controller
    {
        dbShopHoaDataContext data = new dbShopHoaDataContext();

        public List<Hoa> LayHoa(int count)
        {
            return data.Hoas.OrderByDescending(a => a.TenHoa).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var hoas = data.Hoas.ToList();
            return View(hoas);
        }
        public ActionResult LoaiHoa()
        {
            var loaihoa = from lh in data.LoaiHoas select lh;
            return PartialView(loaihoa);
        }
        public ActionResult MauSac()
        {
            var mausac = from ms in data.MauSacs select ms;
            return PartialView(mausac);
        }
        public ActionResult SPTheoLoaiHoa(int id)
        {
            var hoa = from h in data.Hoas where h.MaLoaiHoa == id select h;
            return View(hoa.ToList());
        }
        public ActionResult SPTheoMauSac(int id)
        {
            var hoa = from h in data.Hoas where h.MaMau == id select h;
            return View(hoa.ToList());
        }
        public ActionResult Details(int id)
        {
            var hoa = from h in data.Hoas
                       where h.MaHoa == id
                       select h;
            return View(hoa.Single());
        }
        
    }
}