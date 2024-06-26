﻿// <auto-generated />
using System;
using BookShop.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookShop.SqlServer.Migrations
{
    [DbContext(typeof(BookShopContext))]
    partial class BookShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<DateTime>("RemovedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("RemovedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ward")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.HasKey("Id");

                    b.ToTable("Addresses", null, t =>
                        {
                            t.HasComment("Contain address records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RemovedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("RemovedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Carts", null, t =>
                        {
                            t.HasComment("Contain cart records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("RemovedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("RemovedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("MONEY");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems", null, t =>
                        {
                            t.HasComment("Contain CartItem records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.HasKey("Id");

                    b.ToTable("Categorys", null, t =>
                        {
                            t.HasComment("Contain Category records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpectedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrderStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RemovedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("RemovedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("MONEY");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders", null, t =>
                        {
                            t.HasComment("Contain Order records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cost")
                        .HasColumnType("MONEY");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("RemovedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("RemovedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails", null, t =>
                        {
                            t.HasComment("Contain OrderDetail records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.OrderStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses", null, t =>
                        {
                            t.HasComment("Contain OrderStatus records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<int>("QuantityCurrent")
                        .HasColumnType("int");

                    b.Property<int>("QuantitySold")
                        .HasColumnType("int");

                    b.Property<DateTime>("RemovedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("RemovedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products", null, t =>
                        {
                            t.HasComment("Contain Product records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("AccessTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("RefreshTokenValue")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccessTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens", null, t =>
                        {
                            t.HasComment("Contain refresh token records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(200)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RemovedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("RemovedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews", null, t =>
                        {
                            t.HasComment("Contain Review records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", null, t =>
                        {
                            t.HasComment("Contain role records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.RoleDetail", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RemovedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("RemovedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId");

                    b.ToTable("RoleDetails", null, t =>
                        {
                            t.HasComment("Contain role detail records.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", null, t =>
                        {
                            t.HasComment("Contain user record.");
                        });
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.UserDetail", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<DateTime>("RemovedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("RemovedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.HasIndex("AddressId");

                    b.ToTable("UserDetails", null, t =>
                        {
                            t.HasComment("Contain user record.");
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("nvarchar(34)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<Guid>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("nvarchar(34)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserToken<Guid>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.UserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasDiscriminator().HasValue("UserRole");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.UserToken", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("DATETIME2");

                    b.ToTable("UserTokens", null, t =>
                        {
                            t.HasComment("Contain user record.");
                        });

                    b.HasDiscriminator().HasValue("UserToken");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Cart", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.UserDetail", "UserDetail")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("UserDetail");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.CartItem", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Shared.Entities.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Order", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.Address", "Address")
                        .WithMany("Orders")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Shared.Entities.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Shared.Entities.UserDetail", "UserDetail")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("OrderStatus");

                    b.Navigation("UserDetail");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.OrderDetail", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Shared.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Product", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.RefreshToken", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.UserDetail", "UserDetail")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("UserDetail");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Review", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Shared.Entities.UserDetail", "UserDetail")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("UserDetail");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.RoleDetail", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.Role", "Role")
                        .WithOne("RoleDetail")
                        .HasForeignKey("BookShop.Data.Shared.Entities.RoleDetail", "RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.UserDetail", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.Address", "Address")
                        .WithMany("UserDetails")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Shared.Entities.User", "User")
                        .WithOne("UserDetail")
                        .HasForeignKey("BookShop.Data.Shared.Entities.UserDetail", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.UserRole", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Shared.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.UserToken", b =>
                {
                    b.HasOne("BookShop.Data.Shared.Entities.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Address", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("OrderDetails");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.Role", b =>
                {
                    b.Navigation("RoleDetail");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.User", b =>
                {
                    b.Navigation("UserDetail");

                    b.Navigation("UserRoles");

                    b.Navigation("UserTokens");
                });

            modelBuilder.Entity("BookShop.Data.Shared.Entities.UserDetail", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
