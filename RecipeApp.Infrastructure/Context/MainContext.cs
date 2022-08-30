using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Core;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.HelperClasses;

namespace RecipeApp.Infrastructure.Context
{
    public class MainContext : DbContext
    {
        //in MIGRATIONS: RenameColumn() SUBSTITUING ADDCOLUMN/DROPCOLUMN for changing names of the properties without deleting the data that is already in the DB
        #region Properties
        public DbSet<MemberUser> MemberUsers { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientComposition> IngredientCompositions { get; set; }
        #endregion

        #region Constructors
        public MainContext() : base()
        {
        }

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().Navigation(d => d.IngredientCompoList).AutoInclude();

            modelBuilder.Entity<MemberUser>().HasIndex(m => m.Email).IsUnique();
            modelBuilder.Entity<MemberUser>().HasIndex(m => m.NickName).IsUnique();

            modelBuilder.Entity<Ingredient>().HasIndex(i => i.Name).IsUnique();

            modelBuilder.Entity<FavoriteRecipe>().HasIndex(fr => new { fr.MemberId, fr.RecipeId}).IsUnique();

            modelBuilder.Entity<Ingredient>()
                .HasData
                (
                new Ingredient
                {
                    Name = "Egg yolk",
                    Id = 1
                },
                new Ingredient
                {
                    Name = "Sugar",
                    Id = 2
                },
                new Ingredient
                {
                    Name = "Milk (Cow)",
                    Id = 3
                },
                new Ingredient
                {
                    Name = "Cream",
                    Id = 4
                },
                new Ingredient
                {
                    Name = "Vanilla",
                    Id = 5
                },
                new Ingredient
                {
                    Name = "Cloves",
                    Id = 6
                },
                new Ingredient
                {
                    Name = "Veal",
                    Id = 7
                },
                new Ingredient
                {
                    Name = "White onion",
                    Id = 8
                },
                new Ingredient
                {
                    Name = "Carrots",
                    Id = 9
                },
                new Ingredient
                {
                    Name = "Celery",
                    Id = 10
                },
                new Ingredient
                {
                    Name = "Mushrooms",
                    Id = 11
                },
                new Ingredient
                {
                    Name = "Butter",
                    Id = 12
                },
                new Ingredient
                {
                    Name = "Lemon",
                    Id = 13
                },
                new Ingredient
                {
                    Name = "Sour cream",
                    Id = 14
                }
                );

            modelBuilder.Entity<MemberUser>()
                .HasData
                (
                new MemberUser
                {
                    NickName = "Leia56",
                    FirstName = "Leia",
                    LastName = "Organa",
                    Email = "leiaorgana@gmail.com",
                    Password = "organaL-56",
                    BirthDate = new DateTime(1956, 10, 21),
                    Gender = User.Genders.feminine,
                    Id = 2,
                    Profile = User.Profiles.Member
                },
                new MemberUser
                {
                    NickName = "HarryP",
                    FirstName = "Harry",
                    LastName = "Potter",
                    Email = "hp@gmail.com",
                    Password = "HP_owl1990",
                    BirthDate = new DateTime(1990, 07, 31),
                    Gender = User.Genders.masculine,
                    Id = 3,
                    Profile = User.Profiles.Member
                },
                new MemberUser
                {
                    NickName = "LukeSky",
                    FirstName = "Luke",
                    LastName = "Skywalker",
                    Email = "lsky@gmail.com",
                    Password = "luke!56S",
                    BirthDate = new DateTime(1951, 09, 25),
                    Gender = User.Genders.masculine,
                    Id = 4,
                    Profile = User.Profiles.Member
                },
                new MemberUser //ADMIN
                {
                    NickName = "cintiaM",
                    FirstName = "Cintia",
                    LastName = "Mendes",
                    Email = "c@email.com",
                    Password = "adminCM-2022",
                    BirthDate = new DateTime(1992, 06, 21),
                    Gender = User.Genders.feminine,
                    Id = 1,
                    Profile = User.Profiles.Administrator
                }
                );

            modelBuilder.Entity<Recipe>()
                .HasData
                (
                new Recipe //publicada 
                {
                    Title = "Crème brûlée",
                    Category = Categories.Dessert,
                    Difficulty = 0,
                    Duration = "2:15",
                    Servings = 4,
                    Description = "...",
                    CreatorMemberId = 2,
                    Id = 1,
                    PublishDate = DateTime.Now,
                    isPublished = true
                },
                new Recipe
                {
                    Title = "Blanquette de veau",
                    Category = Categories.Meat,
                    Difficulty = 0,
                    Duration = "0:45",
                    Servings = 6,
                    Description = "...",
                    CreatorMemberId = 4,
                    Id = 2
                }
                );

            modelBuilder.Entity<IngredientComposition>()
                .HasData
                                (
                new IngredientComposition
                {
                    Name = "Egg yolk",
                    Quantity = 4,
                    Unit = MesurementUnits.unit,
                    IngredientId = 1,
                    RecipeId = 1,
                    Id = 1
                },
                new IngredientComposition
                {
                    Name = "Sugar",
                    Quantity = 130,
                    Unit = MesurementUnits.g,
                    IngredientId = 2,
                    RecipeId = 1,
                    Id = 2
                },
                new IngredientComposition
                {
                    Name = "Milk (Cow)",
                    Quantity = 12,
                    Unit = MesurementUnits.cL,
                    IngredientId = 3,
                    RecipeId = 1,
                    Id = 3
                },
                new IngredientComposition
                {
                    Name = "Cream",
                    Quantity = 35,
                    Unit = MesurementUnits.cL,
                    IngredientId = 4,
                    RecipeId = 1,
                    Id = 4
                },
                new IngredientComposition
                {
                    Name = "Vanilla",
                    Quantity = 2,
                    Unit = MesurementUnits.unit,
                    IngredientId = 5,
                    RecipeId = 1,
                    Id = 5
                },
                new IngredientComposition
                {
                    Name = "Cloves",
                    Quantity = 3,
                    Unit = MesurementUnits.unit,
                    IngredientId = 6,
                    RecipeId = 2,
                    Id = 6
                },
                new IngredientComposition
                {
                    Name = "Veal",
                    Quantity = 1,
                    Unit = MesurementUnits.kg,
                    IngredientId = 7,
                    RecipeId = 2,
                    Id = 7
                },
                new IngredientComposition
                {
                    Name = "White onion",
                    Quantity = 2,
                    Unit = MesurementUnits.unit,
                    IngredientId = 8,
                    RecipeId = 2,
                    Id = 8
                },
                new IngredientComposition
                {
                    Name = "Carrots",
                    Quantity = 2,
                    Unit = MesurementUnits.unit,
                    IngredientId = 9,
                    RecipeId = 2,
                    Id = 9
                },
                new IngredientComposition
                {
                    Name = "Celery",
                    Quantity = 1,
                    Unit = MesurementUnits.unit,
                    IngredientId = 10,
                    RecipeId = 2,
                    Id = 10
                },
                new IngredientComposition
                {
                    Name = "Mushrooms",
                    Quantity = 250,
                    Unit = MesurementUnits.g,
                    IngredientId = 11,
                    RecipeId = 2,
                    Id = 11
                },
                new IngredientComposition
                {
                    Name = "Butter",
                    Quantity = 85,
                    Unit = MesurementUnits.g,
                    IngredientId = 12,
                    RecipeId = 2,
                    Id = 12
                },
                new IngredientComposition
                {
                    Name = "Lemon",
                    Quantity = 0.5,
                    Unit = MesurementUnits.unit,
                    IngredientId = 13,
                    RecipeId = 2,
                    Id = 13
                },
                new IngredientComposition
                {
                    Name = "Sour cream",
                    Quantity = 2,
                    Unit = MesurementUnits.dL,
                    IngredientId = 14,
                    RecipeId = 2,
                    Id = 14
                }
                );
        }
        
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is CoreEntity && e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ((CoreEntity)entry.Entity).CreationDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        ((CoreEntity)entry.Entity).UpdateDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChanges();
        }
        #endregion
    }
}
