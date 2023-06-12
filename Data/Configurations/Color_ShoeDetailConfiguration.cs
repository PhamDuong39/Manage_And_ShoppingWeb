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
    public class Color_ShoeDetailConfiguration : IEntityTypeConfiguration<Color_ShoeDetails>
    {
        public void Configure(EntityTypeBuilder<Color_ShoeDetails> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(p => p.Colors).WithMany(p => p.Color_ShoeDetails).HasForeignKey(p => p.IdColor);
            builder.HasOne(p => p.ShoeDetails).WithMany(p => p.Color_ShoeDetails).HasForeignKey(p => p.IdShoeDetail);
        }
    }
}
