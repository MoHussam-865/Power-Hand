﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDatabase;


#nullable disable

namespace Power_Hand.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240623080036_InvoiceWithInvoiceItems")]
    partial class InvoiceWithInvoiceItems
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Power_Hand.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<double>("Discount")
                        .HasColumnType("REAL");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Power_Hand.Models.Emploee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Emploee");
                });

            modelBuilder.Entity("Power_Hand.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Date")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Discount")
                        .HasColumnType("REAL");

                    b.Property<long?>("DueDate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmploeeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InvoiceNote")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

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

            modelBuilder.Entity("Power_Hand.Models.InvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Discount")
                        .HasColumnType("REAL");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFolder")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<double>("Quantity")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceItem");
                });

            modelBuilder.Entity("Power_Hand.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<double>("Discount")
                        .HasColumnType("REAL");

                    b.Property<double?>("Expence")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFolder")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Power_Hand.Models.InvoiceItem", b =>
                {
                    b.HasOne("Power_Hand.Models.Invoice", "Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("Power_Hand.Models.Invoice", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
