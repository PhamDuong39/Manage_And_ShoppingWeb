using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class FavouriteShoesConfiguration : IEntityTypeConfiguration<FavouriteShoes>
    {
        public void Configure(EntityTypeBuilder<FavouriteShoes> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(p => p.Users).WithMany(p => p.FavoriteShoes).HasForeignKey(p => p.IdUser);
            builder.HasOne(p => p.ShoeDetails).WithMany(p => p.FavoriteShoes).HasForeignKey(p => p.IdShoeDetail);
        }
    }
}
