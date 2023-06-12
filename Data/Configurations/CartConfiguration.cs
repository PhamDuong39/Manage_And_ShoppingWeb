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
    public class CartConfiguration : IEntityTypeConfiguration<Carts>
    {
        public void Configure(EntityTypeBuilder<Carts> builder)
        {
            builder.HasKey(p => p.IdUser);
            builder.HasOne(p => p.Users).WithOne(p => p.Carts).HasForeignKey<Carts>(p => p.IdUser);
        }
    }
}
