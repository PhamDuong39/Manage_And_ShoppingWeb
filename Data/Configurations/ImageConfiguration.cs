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
    public class ImageConfiguration : IEntityTypeConfiguration<Images>
    {
        public void Configure(EntityTypeBuilder<Images> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.ShoeDetails).WithMany(p => p.Images).HasForeignKey(p => p.IdShoeDetail);
        }
    }
}
