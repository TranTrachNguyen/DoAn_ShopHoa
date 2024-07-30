using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHoa.Controllers
{
    public class GioMuaHangController : Controller
    {
        dbShopHoaDataContext data=new dbShopHoaDataContext();
        public List<GioMuaHang> laygiohang()
        {
            List<GioMuaHang> lstGioHang = Session["GioMuaHang"] as List<GioMuaHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioMuaHang>();
                Session["GioMuaHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioMuaHang(int maHoa, string strURL)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                // Chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("DangNhap", "NguoiDung");
            }

            // Đã đăng nhập
            DangKy dk = (DangKy)Session["TenDangNhap"];
            KhachHang kh = data.KhachHangs.FirstOrDefault(k => k.MaDangKy == dk.MaDangKy);

            // Nếu khách hàng tồn tại, thêm sản phẩm vào giỏ hàng và liên kết với tài khoản của họ
            if (kh != null)
            {
                List<GioMuaHang> lstGioHang = laygiohang();
                GioMuaHang sanpham = lstGioHang.Find(n => n.MaHoa == maHoa);

                if (sanpham == null)
                {
                    sanpham = new GioMuaHang(maHoa);
                    lstGioHang.Add(sanpham);

                    GioHang gioHangDB = new GioHang
                    {
                        MaKhachHang = kh.MaKhachHang,
                        MaHoa = sanpham.MaHoa,
                        SoLuong = sanpham.SoLuong
                    };
                    // Thêm vào bảng GioHangs
                    data.GioHangs.InsertOnSubmit(gioHangDB);
                }
                else
                {
                    sanpham.SoLuong++;
                    GioHang gioHangDB = data.GioHangs.FirstOrDefault(g => g.MaHoa == sanpham.MaHoa);
                    if (gioHangDB != null)
                    {
                        gioHangDB.SoLuong = sanpham.SoLuong;
                    }
                }
                data.SubmitChanges();
            }

            return Redirect(strURL);
        }
        private int TongSoLuong()
        {
            int tongSL = 0;
            List<GioMuaHang> lstGioHang = Session["GioMuaHang"] as List<GioMuaHang>;
            if (lstGioHang != null)
                tongSL = lstGioHang.Sum(n => n.SoLuong);
            return tongSL;
        }
        private double TongTien()
        {
            double tongTien = 0;
            List<GioMuaHang> lstGioHang = Session["GioMuaHang"] as List<GioMuaHang>;
            if (lstGioHang != null)
                tongTien = lstGioHang.Sum(n => n.ThanhTien);
            return tongTien;
        }
        public ActionResult GioMuaHang()
        {            
            List<GioMuaHang> lstGioHang = laygiohang();
            if (lstGioHang.Count == 0)
                return RedirectToAction("GioHangTrong");
            ViewBag.tongsoluong = TongSoLuong();
            ViewBag.tongtien = TongTien();
            
            return View(lstGioHang);
        }
        public ActionResult GioHangTrong()
        {
            return View();
        }
        public ActionResult GioMuaHangPartial()
        {
            ViewBag.tongsl = TongSoLuong();
            ViewBag.tt = TongTien();
            return PartialView();
        }
        public ActionResult XoaGioMuaHang(int maSP)
        {
            List<GioMuaHang> lstGiohang = laygiohang();
            GioMuaHang sp = lstGiohang.SingleOrDefault(n => n.MaHoa == maSP);

            if (sp != null)
            {
                lstGiohang.RemoveAll(n => n.MaHoa == maSP);

                // Xóa bản ghi trong cơ sở dữ liệu
                GioHang gioHangDB = data.GioHangs.SingleOrDefault(g => g.MaHoa == maSP);

                if (gioHangDB != null)
                {
                    data.GioHangs.DeleteOnSubmit(gioHangDB);
                    data.SubmitChanges();
                }
            }

            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("GioHangTrong");
            }

            return RedirectToAction("GioMuaHang");
        }
        public ActionResult CapNhatGioMuaHang(int maSP, FormCollection f)
        {
            List<GioMuaHang> lstGiohang = laygiohang();
            GioMuaHang sp = lstGiohang.SingleOrDefault(n => n.MaHoa == maSP);
            if (sp != null)
            {
                sp.SoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            Session["GioMuaHang"] = lstGiohang;

            GioHang gioHangDB = data.GioHangs.First(g => g.MaHoa == maSP);
            if (gioHangDB != null)
            {
                gioHangDB.SoLuong = sp.SoLuong;
                data.SubmitChanges();
            }

            return RedirectToAction("GioMuaHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioMuaHang> lstGiohang = laygiohang();

            // Lấy thông tin khách hàng từ session
            DangKy dk = (DangKy)Session["TenDangNhap"];
            KhachHang kh = data.KhachHangs.FirstOrDefault(k => k.MaDangKy == dk.MaDangKy);

            if (kh != null)
            {
                // Xóa toàn bộ giỏ hàng trong session
                lstGiohang.Clear();

                // Xóa toàn bộ giỏ hàng trong database
                var gioHangs = data.GioHangs.Where(g => g.MaKhachHang == kh.MaKhachHang);
                data.GioHangs.DeleteAllOnSubmit(gioHangs);
                data.SubmitChanges();
            }
            return RedirectToAction("XoaTatCaGH");
        }

        public ActionResult XoaTatCaGH()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
                return RedirectToAction("DangNhap", "NguoiDung");
            if (Session["GioMuaHang"] == null)
                return RedirectToAction("Index", "FlowerStore");
            List<GioMuaHang> lstGioHang = laygiohang();
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            DangKy dk = (DangKy)Session["TenDangNhap"];
            KhachHang kh = data.KhachHangs.FirstOrDefault(k => k.MaDangKy == dk.MaDangKy);
            kh.MaDangKy = dk.MaDangKy;

            DonDatHang ddh = data.DonDatHangs.FirstOrDefault(a => a.MaKhachHang == kh.MaKhachHang);
            ddh.MaKhachHang = kh.MaKhachHang;

            List<GioMuaHang> gh = laygiohang();
            
            ddh.MaKhachHang = kh.MaKhachHang;

            ddh.NgayDatHang = DateTime.Now;
            ddh.SoLuong = 0;
            ddh.TrangThaiDonHang = null;
            ddh.TongGiaTriDonHang = null;

            data.DonDatHangs.InsertOnSubmit(ddh);
            data.DangKies.InsertOnSubmit(dk);
            data.SubmitChanges();

            Session["GioMuaHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioMuaHang");
        }
        public ActionResult XacNhanDonHang()
        {
            DonDatHang ddh = new DonDatHang();

            ddh.TenKhachHang = Request.Form["TenKhachHang"];
            int maKhachHang;

            if (int.TryParse(Request.Form["MaKhachHang"], out maKhachHang))
            {
                ddh.MaKhachHang = maKhachHang;
            }
            ddh.DiaChiGiaoHang = Request.Form["DiaChiGiaoHang"];
            ddh.SoDienThoai = Request.Form["SoDienThoai"];

            string ngayDatHangValue = Request.Form["NgayDatHang"];
            DateTime ngayDatHang;

            if (DateTime.TryParse(ngayDatHangValue, out ngayDatHang))
            {
                ddh.NgayDatHang = ngayDatHang;
            }

            string tongSoLuongValue = Request.Form["SoLuong"];
            int tongSLDonHang;

            if (int.TryParse(tongSoLuongValue, out tongSLDonHang))
            {
                ddh.SoLuong = tongSLDonHang;
            }
            ddh.TrangThaiDonHang = Request.Form["TrangThaiDonHang"];

            string tongGiaTriDonHangValue = Request.Form["TongGiaTriDonHang"];
            decimal tongGiaTriDonHang;

            if (decimal.TryParse(tongGiaTriDonHangValue, out tongGiaTriDonHang))
            {
                ddh.TongGiaTriDonHang = tongGiaTriDonHang;
            }
            data.DonDatHangs.InsertOnSubmit(ddh);
            data.SubmitChanges();
            XoaTatCaGioHang();

            return RedirectToAction("DatHangThanhCong");
        }
        public ActionResult DatHangThanhCong()
        {
            return View();
        }
    }
}