using CSharp5Nhom2.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CSharp5Nhom2.Controllers
{
    public class CardController : Controller
    {
        DBSach context;
        public CardController()
        {
            context = new DBSach();
        }
        public IActionResult Index() // Hiển thị tất cả danh sách các sản phẩm có trong giỏ hàng của 1 user
        {
            // Check xem đã đăng nhập chưa
            var username = HttpContext.Session.GetString("login");
            if (String.IsNullOrEmpty(username)) // chưa đăng nhập => bắt đăng nhập
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                // Lấy IDUser từ session và chuyển đổi thành Guid
                var userIdString = HttpContext.Session.GetString("IDUser");

                if (String.IsNullOrEmpty(userIdString))
                {
                    return RedirectToAction("Login", "Home");
                }

                Guid userId;
                try
                {
                    userId = Guid.Parse(userIdString);
                }
                catch (FormatException)
                {
                    // Xử lý trường hợp chuỗi không đúng định dạng Guid
                    return RedirectToAction("Login", "Home");
                }

                var cartItems = context.gioHangChiTiets.Where(p => p.IDUser == userId).ToList();
                return View(cartItems); // truyền dữ liệu lấy được sang bên view
            }
        }


    }
}
