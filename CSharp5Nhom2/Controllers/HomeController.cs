using CSharp5Nhom2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace CSharp5Nhom2.Controllers
{
    public class HomeController : Controller
    {
        DBSach sach;

        public HomeController()
        {
            sach = new DBSach();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login(string username, string matkhau)
        {
            if (username == null && matkhau == null)
            {
                return View();
            }
            else
            {
                var data = sach.users.FirstOrDefault(p => p.Username == username && p.MatKhau == matkhau);
                if (data == null)
                {
                    TempData["none"] = "Tài khoản hoặc mật khẩu không hợp lệ";
                    return View();
                }
                else
                {
                    // Lưu username vào session
                    HttpContext.Session.SetString("login", username);

                    // Lưu IDUser vào session
                    HttpContext.Session.SetString("IDUser", data.IDUser.ToString());

                    return RedirectToAction("Index", "Home");
                }
            }

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
