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
    public class Size_ShoeDetailsConfiguration : IEntityTypeConfiguration<Sizes_ShoeDetails>
    {
        public void Configure(EntityTypeBuilder<Sizes_ShoeDetails> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Sizes).WithMany(p => p.Sizes_ShoeDetails).HasForeignKey(p => p.IdSize);
            builder.HasOne(p => p.ShoeDetails).WithMany(p => p.Sizes_ShoeDetails).HasForeignKey(p => p.IdShoeDetails);
        }
    }
}
