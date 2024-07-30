using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHoa.Controllers
{
    public class NguoiDungController : Controller
    {
        dbShopHoaDataContext data = new dbShopHoaDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh,DangKy dk)
        {
            var hoten = collection["HoTenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var nhaplaimatkhau = collection["MatKhauNhapLai"];
            var diachi = collection["DiaChi"];
            var email = collection["Email"];
            var dienthoai = collection["DienThoai"];
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Ho ten khong duoc de trong";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phai nhap ten dang nhap";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phai nhap mat khau";
            }
            else if (String.IsNullOrEmpty(nhaplaimatkhau))
            {
                ViewData["Loi4"] = "Phai nhap lai mat khau";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email khong duoc bo trong";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phai nhap so dien thoai";
            }
            else
            {
                kh.TenKhachHang = hoten;
                dk.TenDangNhap = tendn;
                dk.MatKhau = matkhau;
                dk.VaiTroNguoiDung = "User";
                kh.Email = email;
                kh.DiaChi = diachi;
                kh.SoDienThoai = dienthoai;
                data.DangKies.InsertOnSubmit(dk);
                data.SubmitChanges();

                int maDangKyMoi = dk.MaDangKy;

                kh.MaDangKy = maDangKyMoi;

                data.KhachHangs.InsertOnSubmit(kh);
                data.SubmitChanges();

                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
                ViewData["Loi1"] = "Thiếu Thông Tin Đăng Nhập";
            else if (String.IsNullOrEmpty(matkhau))
                ViewData["Loi2"] = "Thiếu Mật Khẩu";
            
            DangKy dk = data.DangKies.SingleOrDefault(n => n.TenDangNhap == tendn && n.MatKhau == matkhau);
            if (dk != null)
            {
                Session["TenDangNhap"] = dk;

                if (dk.VaiTroNguoiDung == "Admin")
                {
                    Session["Quyen"] = "Admin";
                }
                else if (dk.VaiTroNguoiDung == "User")
                {
                    Session["Quyen"] = "User";
                }
                else
                {
                    Session["Quyen"] = "User";
                }

                return RedirectToAction("Index", "FlowerStore");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }            
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Clear(); 
            return RedirectToAction("Index", "FlowerStore"); 
        }
    }
}