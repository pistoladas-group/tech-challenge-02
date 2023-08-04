﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechNews.Core.Api.Data;

#nullable disable

namespace TechNews.Core.Api.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TechNews.Core.Api.Data.Models.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("ImageSource")
                        .HasColumnType("VARCHAR(500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("BIT")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id")
                        .HasName("PK_Authors");

                    b.ToTable("Authors", (string)null);
                });

            modelBuilder.Entity("TechNews.Core.Api.Data.Models.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("Id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("AuthorId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(5000)");

                    b.Property<string>("ImageSource")
                        .HasColumnType("VARCHAR(500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("BIT")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("PublishDate")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id")
                        .HasName("PK_News");

                    b.HasIndex("AuthorId");

                    b.ToTable("News", (string)null);
                });

            modelBuilder.Entity("TechNews.Core.Api.Data.Models.News", b =>
                {
                    b.HasOne("TechNews.Core.Api.Data.Models.Author", "Author")
                        .WithMany("News")
                        .HasForeignKey("AuthorId")
                        .IsRequired()
                        .HasConstraintName("FK_News_Authors");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("TechNews.Core.Api.Data.Models.Author", b =>
                {
                    b.Navigation("News");
                });
#pragma warning restore 612, 618
        }
    }
}
