using CSharp5Nhom2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSharp5Nhom2.Configuration
{
    public class HoaDonConfig : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.HasKey(x => x.IDHoaDon);
            builder.HasOne(p => p.User).WithMany(p => p.HoaDons).HasForeignKey(p => p.IDUser);
        }
    }
}
