﻿// <auto-generated />
using System;
using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCore.CodeFirst.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220612204439_EditEntities")]
    partial class EditEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EFCore.CodeFirst.DAL.BasePerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons", (string)null);
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Barcode")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<int>("Kdv")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastAccessDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComputedColumnSql("[UnitPrice]*[Kdv]");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.ProductFeature", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProductFeatures");
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("StudentTeacher", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<int>("TeachersId")
                        .HasColumnType("int");

                    b.HasKey("StudentsId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("StudentTeacher");
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Employee", b =>
                {
                    b.HasBaseType("EFCore.CodeFirst.DAL.BasePerson");

                    b.Property<decimal>("Salary")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Manager", b =>
                {
                    b.HasBaseType("EFCore.CodeFirst.DAL.BasePerson");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.ToTable("Managers", (string)null);
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Product", b =>
                {
                    b.HasOne("EFCore.CodeFirst.DAL.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.ProductFeature", b =>
                {
                    b.HasOne("EFCore.CodeFirst.DAL.Product", "Product")
                        .WithOne("ProductFeature")
                        .HasForeignKey("EFCore.CodeFirst.DAL.ProductFeature", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("StudentTeacher", b =>
                {
                    b.HasOne("EFCore.CodeFirst.DAL.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCore.CodeFirst.DAL.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Employee", b =>
                {
                    b.HasOne("EFCore.CodeFirst.DAL.BasePerson", null)
                        .WithOne()
                        .HasForeignKey("EFCore.CodeFirst.DAL.Employee", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.OwnsOne("EFCore.CodeFirst.DAL.OwnedType", "OwnedType", b1 =>
                        {
                            b1.Property<int>("EmployeeId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CreatedTime")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime?>("ModifiedTime")
                                .HasColumnType("datetime2");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.Navigation("OwnedType")
                        .IsRequired();
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Manager", b =>
                {
                    b.HasOne("EFCore.CodeFirst.DAL.BasePerson", null)
                        .WithOne()
                        .HasForeignKey("EFCore.CodeFirst.DAL.Manager", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.OwnsOne("EFCore.CodeFirst.DAL.OwnedType", "OwnedType", b1 =>
                        {
                            b1.Property<int>("ManagerId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CreatedTime")
                                .HasColumnType("datetime2")
                                .HasColumnName("CreatedTime");

                            b1.Property<DateTime?>("ModifiedTime")
                                .HasColumnType("datetime2")
                                .HasColumnName("ModifiedTime");

                            b1.HasKey("ManagerId");

                            b1.ToTable("Managers");

                            b1.WithOwner()
                                .HasForeignKey("ManagerId");
                        });

                    b.Navigation("OwnedType")
                        .IsRequired();
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("EFCore.CodeFirst.DAL.Product", b =>
                {
                    b.Navigation("ProductFeature")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}