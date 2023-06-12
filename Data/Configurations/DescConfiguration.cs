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
    public class DescConfiguration : IEntityTypeConfiguration<Descriptions>
    {
        public void Configure(EntityTypeBuilder<Descriptions> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.ShoeDetails).WithMany(p => p.Descriptions).HasForeignKey(p => p.IdShoeDetail);
        }
    }
}
