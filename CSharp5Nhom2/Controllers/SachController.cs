using CSharp5Nhom2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSharp5Nhom2.Controllers
{
    public class SachController : Controller
    {
        DBSach context;
        public SachController()
        {
            context = new DBSach();
        }
        public ActionResult Index()
        {
            var index = context.sachs.ToList();
            return View(index);
        }

        // GET: DongVatController/Details/5
        public ActionResult Details(string id)
        {
            var details = context.sachs.Find(id);
            return View(details);
        }

        // GET: DongVatController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DongVatController/Create
        [HttpPost]
        public ActionResult Create(Sach sach)
        {
            try
            {
                context.sachs.Add(sach);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: DongVatController/Edit/5
        public ActionResult Edit(string id)
        {
            var edit = context.sachs.Find(id);
            return View(edit);
        }

        // POST: DongVatController/Edit/5
        [HttpPost]
        public ActionResult Edit(Sach sach)
        {
            var item = context.sachs.Find(sach.IDSach);
            item.TenSach = sach.TenSach;
            item.IDTacGia = sach.IDTacGia;
            item.IDNXB = sach.IDNXB;
            item.IDTheLoai = sach.IDTheLoai;
            item.Gia = sach.Gia;
            item.SoLuong = sach.SoLuong;                     
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var delete = context.sachs.Find(id);
            context.Remove(delete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AddtoCart(Guid id, int soLuong)
        {
            // Kiểm tra dữ liệu đăng nhập
            var username = HttpContext.Session.GetString("login");
            if (String.IsNullOrEmpty(username)) // chưa đăng nhập => bắt đăng nhập
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                // Lấy thông tin người dùng từ cơ sở dữ liệu
                var user = context.users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    return BadRequest("User Không tồn tại");
                }

                // Lấy ID của giỏ hàng dựa trên ID của người dùng
                var gioHang = context.gioHangs.FirstOrDefault(gh => gh.IDUser == user.IDUser);
                if (gioHang == null)
                {
                    return BadRequest("Giỏ hàng không tồn tại");
                }

                // Lấy thông tin sản phẩm từ kho
                var sach = context.sachs.FirstOrDefault(s => s.IDSach == id);
                if (sach == null)
                {
                    return BadRequest("Sản phẩm không tồn tại");
                }

                // Kiểm tra số lượng sản phẩm có trong kho
                if (soLuong > sach.SoLuong)
                {
                    return BadRequest("Số lượng nhập lớn hơn số lượng trong kho");
                }

                // Kiểm tra xem trong giỏ hàng của user này đã có sản phẩm này hay chưa?
                var cartItem = context.gioHangChiTiets.FirstOrDefault(p => p.IDSach == id && p.IDGH == gioHang.IDGH);
                if (cartItem == null)
                {
                    // Nếu giỏ hàng của user này chưa có sản phẩm đó
                    GioHangChiTiet cartDetails = new GioHangChiTiet()
                    {
                        IDGHCT = Guid.NewGuid(),
                        IDGH = gioHang.IDGH,
                        IDSach = id,
                        IDUser = user.IDUser,
                        SoLuong = soLuong,
                    };
                    context.gioHangChiTiets.Add(cartDetails);
                    context.SaveChanges();
                }
                else
                {
                    // Nếu có rồi thì mình sẽ cộng dồn
                    if (cartItem.SoLuong + soLuong > sach.SoLuong)
                    {
                        return BadRequest("Số lượng nhập lớn hơn số lượng trong kho");
                    }
                    cartItem.SoLuong = cartItem.SoLuong + soLuong; // Cập nhật số lượng
                    context.gioHangChiTiets.Update(cartItem);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Cart");
            }
        }


    }

}

