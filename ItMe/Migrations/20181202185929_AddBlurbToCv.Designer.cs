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
    [Migration("20181202185929_AddBlurbToCv")]
    partial class AddBlurbToCv
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

            modelBuilder.Entity("ItMe.Database.DbCv", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Blurb");

                    b.Property<int>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Cvs");
                });

            modelBuilder.Entity("ItMe.Database.DbExternalProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CvId");

                    b.Property<string>("Name");

                    b.Property<string>("Uri");

                    b.HasKey("Id");

                    b.HasIndex("CvId");

                    b.ToTable("ExternalProfiles");
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

            modelBuilder.Entity("ItMe.Database.DbJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company");

                    b.Property<int>("CvId");

                    b.HasKey("Id");

                    b.HasIndex("CvId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("ItMe.Database.DbJobRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("End")
                        .IsRequired();

                    b.Property<int>("JobId");

                    b.Property<string>("Start")
                        .IsRequired();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("JobRoles");
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

            modelBuilder.Entity("ItMe.Database.DbCv", b =>
                {
                    b.HasOne("ItMe.Database.DbPerson", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItMe.Database.DbExternalProfile", b =>
                {
                    b.HasOne("ItMe.Database.DbCv", "Cv")
                        .WithMany("Profiles")
                        .HasForeignKey("CvId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItMe.Database.DbFeature", b =>
                {
                    b.HasOne("ItMe.Database.DbPerson", "Person")
                        .WithMany("Features")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItMe.Database.DbJob", b =>
                {
                    b.HasOne("ItMe.Database.DbCv", "Cv")
                        .WithMany("Jobs")
                        .HasForeignKey("CvId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItMe.Database.DbJobRole", b =>
                {
                    b.HasOne("ItMe.Database.DbJob", "Job")
                        .WithMany("Roles")
                        .HasForeignKey("JobId")
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
