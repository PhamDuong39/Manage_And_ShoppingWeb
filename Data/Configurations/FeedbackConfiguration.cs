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
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedbacks>
    {
        public void Configure(EntityTypeBuilder<Feedbacks> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Users).WithMany(p => p.Feedbacks).HasForeignKey(p => p.IdUser);
            builder.HasOne(p => p.ShoeDetails).WithMany(p => p.Feedbacks).HasForeignKey(p => p.IdShoeDetail);
        }
    }
}
