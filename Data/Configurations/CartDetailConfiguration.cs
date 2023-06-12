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
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetails>
    {
        public void Configure(EntityTypeBuilder<CartDetails> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.ShoeDetails).WithMany(p => p.CartDetails).HasForeignKey(p => p.IdShoeDetail);
            builder.HasOne(p => p.Carts).WithMany(p => p.CartDetails).HasForeignKey(p => p.IdUser);
        }
    }
}
