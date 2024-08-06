﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBlog.API.Data;

#nullable disable

namespace MyBlog.API.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240208173331_Add Relationships")]
    partial class AddRelationships
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlogPostCategory", b =>
                {
                    b.Property<Guid>("blog_postsid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("categoriesid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("blog_postsid", "categoriesid");

                    b.HasIndex("categoriesid");

                    b.ToTable("BlogPostCategory");
                });

            modelBuilder.Entity("MyBlog.API.Models.Domain.BlogPost", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("featured_image_url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_visible")
                        .HasColumnType("bit");

                    b.Property<DateTime>("published_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("short_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("url_handle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("blogPosts");
                });

            modelBuilder.Entity("MyBlog.API.Models.Domain.Category", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("url_handle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("BlogPostCategory", b =>
                {
                    b.HasOne("MyBlog.API.Models.Domain.BlogPost", null)
                        .WithMany()
                        .HasForeignKey("blog_postsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyBlog.API.Models.Domain.Category", null)
                        .WithMany()
                        .HasForeignKey("categoriesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}