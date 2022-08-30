﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeApp.Infrastructure.Context;

namespace RecipeApp.Infrastructure.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20220630130137_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("RecipeApp.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<bool>("NotApproved")
                        .HasColumnType("bit");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.FavoriteRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemberId", "RecipeId")
                        .IsUnique();

                    b.ToTable("FavoriteRecipes");
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 30, DateTimeKind.Local).AddTicks(8520),
                            IsDeleted = false,
                            Name = "Egg yolk"
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(7540),
                            IsDeleted = false,
                            Name = "Sugar"
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8150),
                            IsDeleted = false,
                            Name = "Milk (Cow)"
                        },
                        new
                        {
                            Id = 4,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8160),
                            IsDeleted = false,
                            Name = "Cream"
                        },
                        new
                        {
                            Id = 5,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8170),
                            IsDeleted = false,
                            Name = "Vanilla"
                        },
                        new
                        {
                            Id = 6,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8180),
                            IsDeleted = false,
                            Name = "Cloves"
                        },
                        new
                        {
                            Id = 7,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8190),
                            IsDeleted = false,
                            Name = "Veal"
                        },
                        new
                        {
                            Id = 8,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8190),
                            IsDeleted = false,
                            Name = "White onion"
                        },
                        new
                        {
                            Id = 9,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8200),
                            IsDeleted = false,
                            Name = "Carrots"
                        },
                        new
                        {
                            Id = 10,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8210),
                            IsDeleted = false,
                            Name = "Celery"
                        },
                        new
                        {
                            Id = 11,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8220),
                            IsDeleted = false,
                            Name = "Mushrooms"
                        },
                        new
                        {
                            Id = 12,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8230),
                            IsDeleted = false,
                            Name = "Butter"
                        },
                        new
                        {
                            Id = 13,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8230),
                            IsDeleted = false,
                            Name = "Lemon"
                        },
                        new
                        {
                            Id = 14,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8240),
                            IsDeleted = false,
                            Name = "Sour cream"
                        });
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.IngredientComposition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("IngredientCompositions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(3930),
                            IngredientId = 1,
                            IsDeleted = false,
                            Name = "Egg yolk",
                            Quantity = 4.0,
                            RecipeId = 1,
                            Unit = 0
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(7960),
                            IngredientId = 2,
                            IsDeleted = false,
                            Name = "Sugar",
                            Quantity = 130.0,
                            RecipeId = 1,
                            Unit = 2
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(8930),
                            IngredientId = 3,
                            IsDeleted = false,
                            Name = "Milk (Cow)",
                            Quantity = 12.0,
                            RecipeId = 1,
                            Unit = 5
                        },
                        new
                        {
                            Id = 4,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(8950),
                            IngredientId = 4,
                            IsDeleted = false,
                            Name = "Cream",
                            Quantity = 35.0,
                            RecipeId = 1,
                            Unit = 5
                        },
                        new
                        {
                            Id = 5,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(8960),
                            IngredientId = 5,
                            IsDeleted = false,
                            Name = "Vanilla",
                            Quantity = 2.0,
                            RecipeId = 1,
                            Unit = 0
                        },
                        new
                        {
                            Id = 6,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9020),
                            IngredientId = 6,
                            IsDeleted = false,
                            Name = "Cloves",
                            Quantity = 3.0,
                            RecipeId = 2,
                            Unit = 0
                        },
                        new
                        {
                            Id = 7,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9040),
                            IngredientId = 7,
                            IsDeleted = false,
                            Name = "Veal",
                            Quantity = 1.0,
                            RecipeId = 2,
                            Unit = 3
                        },
                        new
                        {
                            Id = 8,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9050),
                            IngredientId = 8,
                            IsDeleted = false,
                            Name = "White onion",
                            Quantity = 2.0,
                            RecipeId = 2,
                            Unit = 0
                        },
                        new
                        {
                            Id = 9,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9060),
                            IngredientId = 9,
                            IsDeleted = false,
                            Name = "Carrots",
                            Quantity = 2.0,
                            RecipeId = 2,
                            Unit = 0
                        },
                        new
                        {
                            Id = 10,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9070),
                            IngredientId = 10,
                            IsDeleted = false,
                            Name = "Celery",
                            Quantity = 1.0,
                            RecipeId = 2,
                            Unit = 0
                        },
                        new
                        {
                            Id = 11,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9130),
                            IngredientId = 11,
                            IsDeleted = false,
                            Name = "Mushrooms",
                            Quantity = 250.0,
                            RecipeId = 2,
                            Unit = 2
                        },
                        new
                        {
                            Id = 12,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9140),
                            IngredientId = 12,
                            IsDeleted = false,
                            Name = "Butter",
                            Quantity = 85.0,
                            RecipeId = 2,
                            Unit = 2
                        },
                        new
                        {
                            Id = 13,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9150),
                            IngredientId = 13,
                            IsDeleted = false,
                            Name = "Lemon",
                            Quantity = 0.5,
                            RecipeId = 2,
                            Unit = 0
                        },
                        new
                        {
                            Id = 14,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9160),
                            IngredientId = 14,
                            IsDeleted = false,
                            Name = "Sour cream",
                            Quantity = 2.0,
                            RecipeId = 2,
                            Unit = 6
                        });
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.MemberUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(25)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Profile")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserPhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NickName")
                        .IsUnique();

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(1956, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 55, DateTimeKind.Local).AddTicks(4070),
                            Email = "leiaorgana@gmail.com",
                            FirstName = "Leia",
                            Gender = 1,
                            IsBanned = false,
                            IsDeleted = false,
                            LastName = "Organa",
                            NickName = "Leia56",
                            Password = "S1ACOMQSLTfJsGpwYjnNhM4GH2HYRra6A94DwVUEZJg=",
                            Profile = 1,
                            Salt = "wMyGt8HoMVdxffgKwil9hg=="
                        },
                        new
                        {
                            Id = 3,
                            BirthDate = new DateTime(1990, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 209, DateTimeKind.Local).AddTicks(7080),
                            Email = "hp@gmail.com",
                            FirstName = "Harry",
                            Gender = 0,
                            IsBanned = false,
                            IsDeleted = false,
                            LastName = "Potter",
                            NickName = "HarryP",
                            Password = "N14bWq7nmFIqzHDrX6SWDYEgjF38VW6/BzY6KzUlYdQ=",
                            Profile = 1,
                            Salt = "0GuIFO5154j74BsW/wiBLQ=="
                        },
                        new
                        {
                            Id = 4,
                            BirthDate = new DateTime(1951, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 366, DateTimeKind.Local).AddTicks(2660),
                            Email = "lsky@gmail.com",
                            FirstName = "Luke",
                            Gender = 0,
                            IsBanned = false,
                            IsDeleted = false,
                            LastName = "Skywalker",
                            NickName = "LukeSky",
                            Password = "t0PEV7M5bsAQN6S/xSrkiWuluGxFXT+59oSpvHWinjc=",
                            Profile = 1,
                            Salt = "N4iywHZduExi/ySloRWfDw=="
                        },
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(1992, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 518, DateTimeKind.Local).AddTicks(6700),
                            Email = "c@email.com",
                            FirstName = "Cintia",
                            Gender = 1,
                            IsBanned = false,
                            IsDeleted = false,
                            LastName = "Mendes",
                            NickName = "cintiaM",
                            Password = "tu5mm7icrjKPqEiAjW4oor55/73/+flEyzXPSUqPMfs=",
                            Profile = 0,
                            Salt = "P6LqA5jSxqz3g1dEe5p4rg=="
                        });
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorMemberId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<string>("Duration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("NumberRatings")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Rating")
                        .HasColumnType("DECIMAL(3,2)");

                    b.Property<string>("RecipePhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Servings")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<int>("TotalRating")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isPublished")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CreatorMemberId");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 6,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 656, DateTimeKind.Local).AddTicks(1550),
                            CreatorMemberId = 2,
                            Description = "...",
                            Difficulty = 0,
                            Duration = "2:15",
                            IsDeleted = false,
                            NumberRatings = 0,
                            PublishDate = new DateTime(2022, 6, 30, 14, 1, 36, 656, DateTimeKind.Local).AddTicks(8290),
                            Rating = 0m,
                            Servings = 4,
                            Title = "Crème brûlée",
                            TotalRating = 0,
                            isPublished = true
                        },
                        new
                        {
                            Id = 2,
                            Category = 2,
                            CreationDate = new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(630),
                            CreatorMemberId = 4,
                            Description = "...",
                            Difficulty = 0,
                            Duration = "0:45",
                            IsDeleted = false,
                            NumberRatings = 0,
                            Rating = 0m,
                            Servings = 6,
                            Title = "Blanquette de veau",
                            TotalRating = 0,
                            isPublished = false
                        });
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.Comment", b =>
                {
                    b.HasOne("RecipeApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("CommentsList")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.FavoriteRecipe", b =>
                {
                    b.HasOne("RecipeApp.Domain.Entities.MemberUser", "MemberUser")
                        .WithMany("FavoriteRecipes")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MemberUser");
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.IngredientComposition", b =>
                {
                    b.HasOne("RecipeApp.Domain.Entities.Ingredient", "Ingredient")
                        .WithMany("IngredientCompositions")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("IngredientCompoList")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.Recipe", b =>
                {
                    b.HasOne("RecipeApp.Domain.Entities.MemberUser", "MemberUser")
                        .WithMany("CreatedRecipes")
                        .HasForeignKey("CreatorMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MemberUser");
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.Ingredient", b =>
                {
                    b.Navigation("IngredientCompositions");
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.MemberUser", b =>
                {
                    b.Navigation("CreatedRecipes");

                    b.Navigation("FavoriteRecipes");
                });

            modelBuilder.Entity("RecipeApp.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("CommentsList");

                    b.Navigation("IngredientCompoList");
                });
#pragma warning restore 612, 618
        }
    }
}
