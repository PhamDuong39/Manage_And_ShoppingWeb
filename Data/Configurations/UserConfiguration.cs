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
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(p => p.Roles).WithMany(p => p.Users).HasForeignKey(p => p.IdRole);
            builder.HasOne(p => p.Carts).WithOne(p => p.Users).HasForeignKey<Carts>(p => p.IdUser);
        }
    }
}
