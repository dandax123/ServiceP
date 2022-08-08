﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceP.Data;

#nullable disable

namespace ServiceP.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220803171655_tryv1")]
    partial class tryv1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ServiceP.Models.Booking", b =>
                {
                    b.Property<int>("bookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("bookingId"), 1L, 1);

                    b.Property<int>("customeruserId")
                        .HasColumnType("int");

                    b.Property<int>("provideruserId")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("serviceId")
                        .HasColumnType("int");

                    b.HasKey("bookingId");

                    b.HasIndex("customeruserId");

                    b.HasIndex("provideruserId");

                    b.HasIndex("serviceId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("ServiceP.Models.Service", b =>
                {
                    b.Property<int>("serviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("serviceId"), 1L, 1);

                    b.Property<int>("creatoruserId")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("service_type")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("serviceId");

                    b.HasIndex("creatoruserId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ServiceP.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("ServiceP.Models.Customer", b =>
                {
                    b.HasBaseType("ServiceP.Models.User");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("ServiceP.Models.Provider", b =>
                {
                    b.HasBaseType("ServiceP.Models.User");

                    b.Property<string>("brand_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Provider");
                });

            modelBuilder.Entity("ServiceP.Models.Booking", b =>
                {
                    b.HasOne("ServiceP.Models.Customer", "customer")
                        .WithMany("bookings")
                        .HasForeignKey("customeruserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceP.Models.Provider", "provider")
                        .WithMany()
                        .HasForeignKey("provideruserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceP.Models.Service", "service")
                        .WithMany("bookings")
                        .HasForeignKey("serviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("provider");

                    b.Navigation("service");
                });

            modelBuilder.Entity("ServiceP.Models.Service", b =>
                {
                    b.HasOne("ServiceP.Models.Provider", "creator")
                        .WithMany("services")
                        .HasForeignKey("creatoruserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("creator");
                });

            modelBuilder.Entity("ServiceP.Models.Service", b =>
                {
                    b.Navigation("bookings");
                });

            modelBuilder.Entity("ServiceP.Models.Customer", b =>
                {
                    b.Navigation("bookings");
                });

            modelBuilder.Entity("ServiceP.Models.Provider", b =>
                {
                    b.Navigation("services");
                });
#pragma warning restore 612, 618
        }
    }
}