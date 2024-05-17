using Microsoft.AspNetCore.Mvc;

namespace CSharp5Nhom2.Models
{
    public class Sach
    {
        public Guid IDSach { get; set; }
        public string TenSach { get; set; } 
        public string IDTacGia { get; set; }
        public string IDNXB { get; set; }
        public string IDTheLoai { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public virtual TacGia TacGia { get; set; }
        public virtual NhaXuatBan NhaXuatBan { get; set; }
        public virtual TheLoai TheLoai { get; set; }
        public virtual HoaDonChiTiet HoaDonChiTiet { get; set; }
    }
}
