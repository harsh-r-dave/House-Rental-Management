﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HouseRentalManagement.Models;

namespace HouseRentalManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<House> House { get; set; }
        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<HouseImage> HouseImage { get; set; }
        public DbSet<HouseFacility> HouseFacility { get; set; }
        public DbSet<Facility> Facility { get; set; }
        public DbSet<HouseAmenity> houseAmenity { get; set; }
        public DbSet<Amenity> Amenity { get; set; }
        public DbSet<LeaseLength> LeaseLengths { get; set; }
        public DbSet<HouseLeaseLength> HouseLeaseLengths { get; set; }
        public DbSet<HouseAmenity> HouseAmenity { get; set; }
        public DbSet<HouseGettingAround> HouseGettingAround { get; set; }
        public DbSet<FeaturedImage> FeaturedImage { get; set; }
        public DbSet<AccessCode> AccessCode { get; set; }
        public DbSet<HouseRestriction> HouseRestriction { get; set; }
        public DbSet<Restriction> Restriction { get; set; }
        public DbSet<Inquiry> Inquiry { get; set; }
        public DbSet<HouseMapImage> HouseMapImage { get; set; }
        public DbSet<SiteConfig> SiteConfig { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<House>().HasKey(e => e.HouseId);
            builder.Entity<House>().HasMany<Tenant>(e => e.Tenants);
            builder.Entity<House>().HasMany<HouseImage>(e => e.HouseImages);
            builder.Entity<House>().HasMany<HouseAmenity>(e => e.HouseAmenities);
            builder.Entity<House>().HasOne<HouseLeaseLength>(e => e.HouseLeaseLength);

            builder.Entity<Tenant>().HasKey(e => e.TenantId);
            builder.Entity<Tenant>().
                HasOne<House>(e => e.House)
                .WithMany(h => h.Tenants)
                .HasForeignKey(e => e.HouseId);

            builder.Entity<HouseImage>().HasKey(e => e.HouseImageId);
            builder.Entity<HouseImage>().HasOne<House>(e => e.House);

            builder.Entity<HouseFacility>().HasKey(e => e.HouseFacilityId);
            builder.Entity<HouseFacility>().HasMany<Facility>();
            builder.Entity<HouseFacility>().HasMany<House>();

            builder.Entity<Facility>().HasKey(e => e.FacilityId);

            builder.Entity<Amenity>(entity =>
            {
                entity.HasKey(a => a.AmenityId);

                entity.HasMany(a => a.HouseAmenities);
            });

            builder.Entity<HouseAmenity>(entity =>
            {
                entity.HasKey(a => a.HouseAmenityId);

                entity.HasMany(a => a.Houses);

                entity.HasOne(a => a.Amenity)
                .WithMany(b => b.HouseAmenities)
                .HasForeignKey(a => a.AmenityId);

                entity.Property(a => a.IncludedInUtility).HasDefaultValue(false);
            });

            builder.Entity<LeaseLength>(entity =>
            {
                entity.HasKey(a => a.LeaseLengthId);
                entity.HasMany(a => a.HouseLeaseLength)
                .WithOne(hl => hl.LeaseLength);
            });

            builder.Entity<HouseLeaseLength>(entity =>
            {
                entity.HasKey(a => a.HouseLeaseLengthId);

                //entity.HasMany(a => a.Houses)
                //.WithOne(h => h.HouseLeaseLength)
                //.HasForeignKey(a => a.HouseId);

                entity.HasOne(a => a.LeaseLength)
                .WithMany(ll => ll.HouseLeaseLength)
                .HasForeignKey(a => a.HouseLeaseLengthId);
            });

            builder.Entity<HouseGettingAround>(entity =>
            {
                entity.HasKey(a => a.HouseGettingAroundId);

                entity.HasOne(a => a.House)
                .WithMany(b => b.HouseGettingArounds)
                .HasForeignKey(a => a.HouseId);
            });

            builder.Entity<FeaturedImage>(entity =>
            {
                entity.HasKey(a => a.FeaturedImageId);
            });

            builder.Entity<AccessCode>(entity =>
            {
                entity.HasKey(a => a.AccessCodeId);
            });

            builder.Entity<Restriction>(entity =>
            {
                entity.HasKey(a => a.RestrictionId);
                entity.Property(a => a.Title).HasMaxLength(500);
            });

            builder.Entity<HouseRestriction>(entity =>
            {
                entity.HasKey(a => a.HouseRestrictionId);

                entity.HasOne(a => a.Houses)
                .WithMany(b => b.HouseRestrictions)
                .HasForeignKey(a => a.HouseId);

                entity.HasOne(a => a.Restriction)
                .WithMany()
                .HasForeignKey(a => a.RestrictionId);
            });

            builder.Entity<Inquiry>(entity =>
            {
                entity.HasKey(a => a.InquiryId);

                entity.Property(a => a.Name).HasMaxLength(100);
                entity.Property(a => a.EmailAddress).HasMaxLength(100);
                entity.Property(a => a.Message).HasMaxLength(500);
                entity.Property(a => a.Read).HasDefaultValue(false);
            });

            builder.Entity<HouseMapImage>(entity =>
            {
                entity.HasKey(a => a.HouseMapImageId);
                entity.HasOne(a => a.House)
                .WithMany(h => h.MapImages)
                .HasForeignKey(a => a.HouseId);
            });

            builder.Entity<SiteConfig>(entity =>
            {
                entity.HasKey(a => a.SiteConfigId);
                entity.Property(a => a.PhoneNumber).HasMaxLength(15);
                entity.Property(a => a.WhatsappNumber).HasMaxLength(15);
                entity.Property(a => a.PrimaryEmail).HasMaxLength(255);
            });
        }
    }
}
