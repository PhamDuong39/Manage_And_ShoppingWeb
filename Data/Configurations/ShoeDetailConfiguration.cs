using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class ShoeDetailConfiguration : IEntityTypeConfiguration<ShoeDetails>
    {
        public void Configure(EntityTypeBuilder<ShoeDetails> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(p => p.Sales).WithMany(p => p.ShoeDetails).HasForeignKey(p => p.IdSale);
            builder.HasOne(p => p.Brands).WithMany(p => p.ShoeDetails).HasForeignKey(p => p.IdBrand);
            builder.HasOne(p => p.Supplier).WithMany(p => p.ShoeDetails).HasForeignKey(p => p.IdSupplier);
            builder.HasOne(p => p.Categories).WithMany(p => p.ShoeDetails).HasForeignKey(p => p.IdCategory);
        }
    }
}
