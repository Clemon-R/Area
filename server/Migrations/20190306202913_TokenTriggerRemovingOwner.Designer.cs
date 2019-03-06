﻿// <auto-generated />
using System;
using Area;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Area.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190306202913_TokenTriggerRemovingOwner")]
    partial class TokenTriggerRemovingOwner
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Area.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("LastVerificationDate");

                    b.Property<string>("Password");

                    b.Property<string>("Token");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Area.Models.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccessToken");

                    b.Property<int?>("AccountId");

                    b.Property<int>("ExpireIn");

                    b.Property<string>("RefreshToken");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Area.Models.Trigger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId");

                    b.Property<int>("ActionType");

                    b.Property<int>("ReactionType");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Triggers");
                });

            modelBuilder.Entity("Area.Models.Token", b =>
                {
                    b.HasOne("Area.Models.Account")
                        .WithMany("Tokens")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("Area.Models.Trigger", b =>
                {
                    b.HasOne("Area.Models.Account")
                        .WithMany("Triggers")
                        .HasForeignKey("AccountId");
                });
#pragma warning restore 612, 618
        }
    }
}
