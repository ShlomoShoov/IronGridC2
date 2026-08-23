using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Consumer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Consumer.DAL
{
    public class IronGridDbContext : DbContext
    {
        public IronGridDbContext(DbContextOptions<IronGridDbContext> options):base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AssetLiveStatus>(e =>
            {
                e.HasKey(al=> al.Id);
                e.HasOne(al=> al.Asset)
                    .WithOne(a=> a.AssetLiveStatus)
                    .HasForeignKey<AssetLiveStatus>(al=> al.AssetId);

                e.Property(al=> al.AssetType)
                    .HasConversion<string>();
                    
                e.Property(al=> al.ProcessedStatus)
                    .HasConversion<string>();
            });


            modelBuilder.Entity<Asset>(e =>
            {
                e.HasKey(a => a.Id);
                e.HasOne(a => a.Unit)
                    .WithMany(u=> u.Assets)
                    .HasForeignKey(a => a.UnitId);

                e.Property(a => a.AssetType)
                    .HasConversion<string>();
            });


            modelBuilder.Entity<Unit>(e =>
            {
                e.HasKey(u=> u.Id);
            });
        }

        public DbSet<Unit> Units {get; set;}
        public DbSet<Asset> Assets {get ; set;}
        public DbSet<AssetLiveStatus> AssetLiveStatuses  { get; set;}
    }
}