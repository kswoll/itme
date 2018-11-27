﻿// <auto-generated />
using System;
using ItMe.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ItMe.Migrations
{
    [DbContext(typeof(ItMeDb))]
    [Migration("20181127020817_AddFavIconToPerson")]
    partial class AddFavIconToPerson
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ItMe.Database.DbBlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Excerpt");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("PersonId");

                    b.Property<string>("Slug");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("PersonId", "Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("ItMe.Database.DbFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("ItMe.Database.DbPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FavIconS3Key");

                    b.Property<string>("Host")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Port");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Host")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("ItMe.Database.DbPersonLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PasswordHash");

                    b.Property<int>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonLogins");
                });

            modelBuilder.Entity("ItMe.Database.DbBlogPost", b =>
                {
                    b.HasOne("ItMe.Database.DbPerson", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItMe.Database.DbFeature", b =>
                {
                    b.HasOne("ItMe.Database.DbPerson", "Person")
                        .WithMany("Features")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItMe.Database.DbPersonLogin", b =>
                {
                    b.HasOne("ItMe.Database.DbPerson", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
