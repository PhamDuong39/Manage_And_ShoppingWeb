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
    public class AchivePointConfiguration : IEntityTypeConfiguration<AchivePoint>
    {
        public void Configure(EntityTypeBuilder<AchivePoint> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(p => p.Users).WithMany(p => p.AchivePoints).HasForeignKey(p => p.IdUser);
        }
    }
}
