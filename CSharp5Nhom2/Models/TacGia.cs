﻿namespace CSharp5Nhom2.Models
{
    public class TacGia
    {
        public string IDTacGia { get; set; }
        public string TenTacGia { get; set; }
        public virtual List<Sach> Sachs { get; set; }
    }
}
