using Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class CouponsConfiguration : IEntityTypeConfiguration<Coupons>
    {
        public void Configure(EntityTypeBuilder<Coupons> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.VoucherName).HasColumnType("nvarchar(30)");

        }
    }
}
