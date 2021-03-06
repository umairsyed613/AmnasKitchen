﻿using System;
using Microsoft.EntityFrameworkCore;
using ApnaBawarchiKhana.Shared;
using EFDbFactory.Sql;

namespace ApnaBawarchiKhana.Server.Database
{
    public partial class ApnaBawarchiKhanaDbContext : CommonDbContext
    {
        public ApnaBawarchiKhanaDbContext(DbContextOptions<ApnaBawarchiKhanaDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<RecipeCategory> RecipeCategories { get; set; }
        public virtual DbSet<RecipeImage> RecipeImages { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeRating> RecipeRatings { get; set; }
        public virtual DbSet<UploadedImage> UploadedImages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "sa_amna");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.HasOne(d => d.UploadedImage)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK__Categories__ImageId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.ToTable("Direction", "sa_amna");

                entity.Property(e => e.Step).IsRequired();

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Directions)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK_Directions_RecipeId");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient", "sa_amna");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Quantity)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK_Ingredients_RecipeId");
            });

            modelBuilder.Entity<RecipeCategory>(entity =>
            {
                entity.ToTable("RecipeCategory", "sa_amna");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.RecipeCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RecipeCat__CatId").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeCategories)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RecipeCat__RecipeId").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RecipeImage>(entity =>
            {
                entity.ToTable("RecipeImage", "sa_amna");

                entity.HasOne(d => d.UploadedImage)
                    .WithMany(p => p.RecipeImages)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RecipeImages__ImageId").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeImages)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RecipeImages__RecipeId").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe", "sa_amna");

                entity.Property(e => e.TimeUnit)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<RecipeRating>(entity =>
                {
                    entity.ToTable("RecipeRating", "sa_amna");

                    entity.HasOne(d => d.Recipe)
                          .WithMany(p => p.RecipeRatings)
                          .HasForeignKey(d => d.RecipeId)
                          .OnDelete(DeleteBehavior.ClientSetNull)
                          .HasConstraintName("FK__RecipeRating__RecipeId").OnDelete(DeleteBehavior.Cascade);

                    entity.Property(e => e.Rating)
                          .IsRequired();
                });

            modelBuilder.Entity<UploadedImage>(entity =>
            {
                entity.ToTable("UploadedImage", "sa_amna");

                entity.Property(e => e.ImageData).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "sa_amna");

                entity.Property(e => e.Email).HasMaxLength(512);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
