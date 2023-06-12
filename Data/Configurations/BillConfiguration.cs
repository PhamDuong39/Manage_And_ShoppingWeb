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
    public class BillConfiguration : IEntityTypeConfiguration<Bills>
    {
        public void Configure(EntityTypeBuilder<Bills> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Users).WithMany(p => p.Bills).HasForeignKey(p => p.IdUser);
            builder.HasOne(p => p.Location).WithMany(p => p.Bills).HasForeignKey(p => p.IdLocation);
            builder.HasOne(p => p.Coupons).WithMany(p => p.Bills).HasForeignKey(p => p.IdCoupon);
            builder.HasOne(p => p.ShipAdressMethod).WithMany(p => p.Bills).HasForeignKey(p => p.IdShipAdressMethod);
            builder.HasOne(p => p.PaymentMethods).WithMany(p => p.Bills).HasForeignKey(p => p.IdPaymentMethod);
        }
    }
}
