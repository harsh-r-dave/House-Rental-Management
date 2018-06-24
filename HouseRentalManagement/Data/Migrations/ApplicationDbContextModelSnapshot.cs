﻿// <auto-generated />
using HouseRentalManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HouseRentalManagement.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HouseRentalManagement.Models.AccessCode", b =>
                {
                    b.Property<Guid>("AccessCodeId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Hash");

                    b.Property<string>("PlainTextCode");

                    b.Property<string>("Salt");

                    b.HasKey("AccessCodeId");

                    b.ToTable("AccessCode");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.Amenity", b =>
                {
                    b.Property<int>("AmenityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("HouseAmenityId");

                    b.Property<string>("ImageFileName");

                    b.HasKey("AmenityId");

                    b.HasIndex("HouseAmenityId");

                    b.ToTable("Amenity");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.Facility", b =>
                {
                    b.Property<Guid>("FacilityId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateUtc");

                    b.Property<Guid?>("HouseFacilityId");

                    b.Property<string>("Name");

                    b.HasKey("FacilityId");

                    b.HasIndex("HouseFacilityId");

                    b.ToTable("Facility");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.FeaturedImage", b =>
                {
                    b.Property<int>("FeaturedImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("FileName");

                    b.Property<bool>("ToDisplay");

                    b.HasKey("FeaturedImageId");

                    b.ToTable("FeaturedImage");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.House", b =>
                {
                    b.Property<Guid>("HouseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<int?>("AmenityId");

                    b.Property<DateTime>("AuditUtc");

                    b.Property<DateTime>("AvailableFrom");

                    b.Property<string>("City");

                    b.Property<bool>("ContactForAvailableFrom");

                    b.Property<string>("Country");

                    b.Property<DateTime>("CreateUtc");

                    b.Property<string>("Description");

                    b.Property<int?>("HouseAmenityId");

                    b.Property<Guid?>("HouseFacilityId");

                    b.Property<int?>("HouseLeaseLengthId");

                    b.Property<bool>("IsDisplaying");

                    b.Property<int>("Occupancy");

                    b.Property<int>("ParakingSpace");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Province");

                    b.Property<decimal>("Rent");

                    b.Property<string>("UrlSlug");

                    b.Property<decimal>("Washrooms");

                    b.HasKey("HouseId");

                    b.HasIndex("AmenityId");

                    b.HasIndex("HouseAmenityId");

                    b.HasIndex("HouseFacilityId");

                    b.HasIndex("HouseLeaseLengthId");

                    b.ToTable("House");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseAmenity", b =>
                {
                    b.Property<int>("HouseAmenityId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmenityId");

                    b.Property<Guid>("HouseId");

                    b.Property<bool>("IncludedInUtility")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("HouseAmenityId");

                    b.HasIndex("AmenityId");

                    b.HasIndex("HouseId");

                    b.ToTable("HouseAmenity");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseFacility", b =>
                {
                    b.Property<Guid>("HouseFacilityId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateUtc");

                    b.Property<Guid>("FacilityId");

                    b.Property<Guid>("HouseId");

                    b.HasKey("HouseFacilityId");

                    b.HasIndex("FacilityId");

                    b.HasIndex("HouseId");

                    b.ToTable("HouseFacility");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseGettingAround", b =>
                {
                    b.Property<int>("HouseGettingAroundId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BikeTime");

                    b.Property<string>("CarTime");

                    b.Property<DateTime>("Create");

                    b.Property<decimal>("Distance");

                    b.Property<Guid>("HouseId");

                    b.Property<string>("LocationName");

                    b.Property<string>("WalkingTime");

                    b.HasKey("HouseGettingAroundId");

                    b.HasIndex("HouseId");

                    b.ToTable("HouseGettingAround");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseImage", b =>
                {
                    b.Property<Guid>("HouseImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateUtc");

                    b.Property<string>("FileName");

                    b.Property<Guid>("HouseId");

                    b.Property<bool?>("IsHomePageImage");

                    b.HasKey("HouseImageId");

                    b.HasIndex("HouseId");

                    b.ToTable("HouseImage");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseLeaseLength", b =>
                {
                    b.Property<int>("HouseLeaseLengthId");

                    b.Property<Guid>("HouseId");

                    b.Property<int>("LeaseLengthId");

                    b.HasKey("HouseLeaseLengthId");

                    b.ToTable("HouseLeaseLengths");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseRestriction", b =>
                {
                    b.Property<int>("HouseRestrictionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("HouseId");

                    b.Property<int>("RestrictionId");

                    b.HasKey("HouseRestrictionId");

                    b.HasIndex("HouseId");

                    b.HasIndex("RestrictionId");

                    b.ToTable("HouseRestriction");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.LeaseLength", b =>
                {
                    b.Property<int>("LeaseLengthId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("LeaseLengthId");

                    b.ToTable("LeaseLengths");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.Restriction", b =>
                {
                    b.Property<int>("RestrictionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Title")
                        .HasMaxLength(500);

                    b.HasKey("RestrictionId");

                    b.ToTable("Restriction");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.Tenant", b =>
                {
                    b.Property<Guid>("TenantId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<Guid?>("HouseId");

                    b.Property<bool>("IsOnWaitList");

                    b.Property<string>("LastName");

                    b.Property<string>("Occupation");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("ReferenceName");

                    b.Property<string>("ReferencePhone");

                    b.Property<string>("ReferencedEmail");

                    b.Property<DateTime>("StayStartDate");

                    b.HasKey("TenantId");

                    b.HasIndex("HouseId");

                    b.ToTable("Tenant");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.Amenity", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.HouseAmenity")
                        .WithMany("Amenities")
                        .HasForeignKey("HouseAmenityId");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.Facility", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.HouseFacility")
                        .WithMany()
                        .HasForeignKey("HouseFacilityId");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.House", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.Amenity")
                        .WithMany("Houses")
                        .HasForeignKey("AmenityId");

                    b.HasOne("HouseRentalManagement.Models.HouseAmenity")
                        .WithMany("Houses")
                        .HasForeignKey("HouseAmenityId");

                    b.HasOne("HouseRentalManagement.Models.HouseFacility")
                        .WithMany()
                        .HasForeignKey("HouseFacilityId");

                    b.HasOne("HouseRentalManagement.Models.HouseLeaseLength", "HouseLeaseLength")
                        .WithMany("Houses")
                        .HasForeignKey("HouseLeaseLengthId");
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseAmenity", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.Amenity")
                        .WithMany("HouseAmenities")
                        .HasForeignKey("AmenityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HouseRentalManagement.Models.House")
                        .WithMany("HouseAmenities")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseFacility", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.Facility", "Facility")
                        .WithMany()
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HouseRentalManagement.Models.House", "House")
                        .WithMany("Facilities")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseGettingAround", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.House", "House")
                        .WithMany("HouseGettingArounds")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseImage", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.House", "House")
                        .WithMany("HouseImages")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseLeaseLength", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.LeaseLength", "LeaseLength")
                        .WithMany("HouseLeaseLength")
                        .HasForeignKey("HouseLeaseLengthId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HouseRentalManagement.Models.HouseRestriction", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.House", "Houses")
                        .WithMany("HouseRestrictions")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HouseRentalManagement.Models.Restriction", "Restriction")
                        .WithMany()
                        .HasForeignKey("RestrictionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HouseRentalManagement.Models.Tenant", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.House", "House")
                        .WithMany("Tenants")
                        .HasForeignKey("HouseId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HouseRentalManagement.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HouseRentalManagement.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
