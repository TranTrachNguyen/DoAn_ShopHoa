using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHoa.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        dbShopHoaDataContext data = new dbShopHoaDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QL_DonDatHang()
        {
            return View(data.DonDatHangs.ToList());
        }
        
        public ActionResult Edit_DH(int id)
        {
            DonDatHang donDatHang = data.DonDatHangs.First(d => d.MaDonHang == id);

            if (donDatHang == null)
            {
                return HttpNotFound();
            }

            return View(donDatHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaDDHThanhCong(DonDatHang donDatHang)
        {

            if (ModelState.IsValid)
            {
                data.DonDatHangs.Attach(donDatHang);
                data.Refresh(RefreshMode.KeepCurrentValues, donDatHang);
                data.SubmitChanges();

                return RedirectToAction("QL_DonDatHang");
            }
            return View(donDatHang);
        }

        public ActionResult Details_DH(int id)
        {
            DonDatHang donDatHang = data.DonDatHangs.First(d => d.MaDonHang == id);
            ViewBag.MaDonHang = donDatHang.MaDonHang;
            if (donDatHang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(donDatHang);
        }
        
        public ActionResult Delete_DH(int id)
        {
            DonDatHang donDatHang = data.DonDatHangs.First(d => d.MaDonHang == id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            DonDatHang donDatHang = data.DonDatHangs.First(d => d.MaDonHang == id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            data.DonDatHangs.DeleteOnSubmit(donDatHang);
            data.SubmitChanges();

            return View();
        }
        public ActionResult QL_KhachHang()
        {
            return View(data.KhachHangs.ToList());
        }
        public ActionResult Details_KH(int id)
        {
            KhachHang khachhang = data.KhachHangs.First(d => d.MaKhachHang == id);
            ViewBag.MaKhachHang = khachhang.MaKhachHang;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        public ActionResult Delete_KH(int id)
        {
            KhachHang khachhang = data.KhachHangs.First(d => d.MaKhachHang == id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed_KH(int id)
        {
            KhachHang khachhang = data.KhachHangs.First(d => d.MaKhachHang == id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            data.KhachHangs.DeleteOnSubmit(khachhang);
            data.SubmitChanges();
            return View();
            
        }
        public ActionResult Edit_KH(int id)
        {
            KhachHang khachhang = data.KhachHangs.First(d => d.MaKhachHang == id);

            if (khachhang == null)
            {
                return HttpNotFound();
            }

            return View(khachhang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_KH(KhachHang khachhang)
        {
            if (ModelState.IsValid)
            {
                data.KhachHangs.Attach(khachhang);
                data.Refresh(RefreshMode.KeepCurrentValues, khachhang);
                data.SubmitChanges();

                return View(khachhang);
            }

            return View(khachhang);
        }
        public ActionResult QL_SanPham()
        {
            return View(data.Hoas.ToList());
        }
        public ActionResult Edit_SP(int id)
        {
            Hoa sp = data.Hoas.First(d => d.MaHoa == id);

            if (sp == null)
            {
                return HttpNotFound();
            }
            var loaiHoaList = data.LoaiHoas.ToList();
            ViewBag.MaLoaiHoaList = new SelectList(loaiHoaList, "MaLoaiHoa", "TenLoaiHoa");
            var mauList = data.MauSacs.ToList();
            ViewBag.MaMauList = new SelectList(mauList, "MaMau", "TenMau");

            return View(sp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSPThanhCong(Hoa sp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        file.SaveAs(path);
                        sp.AnhHoa = fileName;
                    }

                    data.Hoas.Attach(sp);
                    data.Refresh(RefreshMode.KeepCurrentValues, sp);
                    data.SubmitChanges();
                    return View(sp);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật sản phẩm. Vui lòng thử lại.");
                    Console.WriteLine(ex.Message);
                }
            }
            return View();
        }
        public ActionResult Details_SP(int id)
        {
            Hoa hoa = data.Hoas.First(d => d.MaHoa == id);
            ViewBag.MaHoa = hoa.MaHoa;
            if (hoa == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hoa);
        }
        public ActionResult Delete_SP(int id)
        {
            Hoa hoa = data.Hoas.First(d => d.MaHoa == id);
            if (hoa == null)
            {
                return HttpNotFound();
            }
            return View(hoa);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed_SP(int id)
        {
            Hoa hoa = data.Hoas.First(d => d.MaHoa == id);
            if (hoa == null)
            {
                return HttpNotFound();
            }
            data.Hoas.DeleteOnSubmit(hoa);
            data.SubmitChanges();
            return View();
        }
        public ActionResult Create_SP()
        {
            Hoa newHoa = new Hoa();
            var loaiHoaList = data.LoaiHoas.ToList();
            ViewBag.LoaiHoaList = new SelectList(loaiHoaList, "MaLoaiHoa", "TenLoaiHoa");

            var mauList = data.MauSacs.ToList();
            ViewBag.MauList = new SelectList(mauList, "MaMau", "TenMau");
            return View(newHoa);
        }

        public ActionResult ThemSPThanhCong(Hoa newHoa, HttpPostedFileBase file)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        file.SaveAs(path);
                        newHoa.AnhHoa = fileName;
                    }
                    data.Hoas.InsertOnSubmit(newHoa);
                    data.SubmitChanges();
                    return RedirectToAction("ThemSPThanhCong");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm sản phẩm. Vui lòng thử lại.");
                    Console.WriteLine(ex.Message);
                }
            }
            return View(newHoa);
        }
        public ActionResult QL_TaiKhoan()
        {
            return View(data.DangKies.ToList());
        }

        public ActionResult XoaTaiKhoan(int id)
        {
            DangKy a = data.DangKies.SingleOrDefault(m => m.MaDangKy == id);
            data.DangKies.DeleteOnSubmit(a);
            data.SubmitChanges();
            return RedirectToAction("QL_TaiKhoan", "Admin");
        }        

        public ActionResult Sua_TK(int id)
        {
            var tk = data.DangKies.SingleOrDefault(m => m.MaDangKy == id);

            if (tk == null)
            {
                return HttpNotFound();
            }
            return View(tk);
        }

        [HttpPost]
        public ActionResult Sua_TK(int ma, string tendn, string mk, string vaitro)
        {
            var tk = data.DangKies.SingleOrDefault(m => m.MaDangKy == ma);

            if (tk == null)
            {

                return HttpNotFound();
            }
            
            tk.TenDangNhap = tendn;
            tk.MatKhau = mk;
            tk.VaiTroNguoiDung = vaitro;
            data.SubmitChanges();

            return RedirectToAction("QL_TaiKhoan", "Admin");
        }
        
    }
}