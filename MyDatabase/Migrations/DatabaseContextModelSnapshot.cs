﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDatabase;

#nullable disable

namespace Power_Hand.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("MyDatabase.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "address");

                    b.Property<double>("Discount")
                        .HasColumnType("REAL");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "email");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "location");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "phone");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("MyDatabase.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rule")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("Password");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("MyDatabase.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Date")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "date");

                    b.Property<double?>("Discount")
                        .HasColumnType("REAL");

                    b.Property<long?>("DueDate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InvoiceNote")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "note");

                    b.Property<double>("Payed")
                        .HasColumnType("REAL");

                    b.Property<double>("Remaining")
                        .HasColumnType("REAL");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("VAT")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("MyDatabase.Models.InvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Discount")
                        .HasColumnType("REAL");

                    b.Property<string>("ImagePath")
                        .HasColumnType("TEXT");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFolder")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "itemId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL")
                        .HasAnnotation("Relational:JsonPropertyName", "price");

                    b.Property<double>("Quantity")
                        .HasColumnType("REAL")
                        .HasAnnotation("Relational:JsonPropertyName", "qty");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceItem");

                    b.HasAnnotation("Relational:JsonPropertyName", "items");
                });

            modelBuilder.Entity("MyDatabase.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<double>("Discount")
                        .HasColumnType("REAL")
                        .HasAnnotation("Relational:JsonPropertyName", "discount");

                    b.Property<double?>("Expence")
                        .HasColumnType("REAL");

                    b.Property<string>("ImageAbsolutePath")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "imageAbsolutePath");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "imageUrl");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFolder")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "isFolder");

                    b.Property<int>("LastUpdate")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "lastUpdate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "notes");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "parentId");

                    b.Property<double>("Price")
                        .HasColumnType("REAL")
                        .HasAnnotation("Relational:JsonPropertyName", "price");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("MyDatabase.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "content");

                    b.Property<string>("ImagePath")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "imagePath");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "title");

                    b.HasKey("Id");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("MyDatabase.Models.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Name")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("MyDatabase.Models.InvoiceItem", b =>
                {
                    b.HasOne("MyDatabase.Models.Invoice", "Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("MyDatabase.Models.Settings", b =>
                {
                    b.HasOne("MyDatabase.Models.Employee", "Employee")
                        .WithMany("Settings")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("MyDatabase.Models.Employee", b =>
                {
                    b.Navigation("Settings");
                });

            modelBuilder.Entity("MyDatabase.Models.Invoice", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
