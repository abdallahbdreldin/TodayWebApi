using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Identity;
using TodayWebAPi.DAL.Data.Models;

namespace TodayWebAPi.DAL.Data.Context
{
    public class StoreContext : IdentityDbContext<User>
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           

            // Seeding Product Brands
            modelBuilder.Entity<ProductBrand>().HasData(
                new ProductBrand { Id = 1, Name = "Apple" },
                new ProductBrand { Id = 2, Name = "Samsung" },
                new ProductBrand { Id = 3, Name = "Sony" }
            );

            // Seeding Product Types
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Name = "Smartphone" },
                new ProductType { Id = 2, Name = "Laptop" },
                new ProductType { Id = 3, Name = "Headphones" }
            );

            // Seeding Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15",
                    Description = "Latest Apple smartphone with A16 chip",
                    Price = 999.99m,
                    PictureUrl = "images/iphone15.jpg",
                    ProductTypeId = 1,
                    ProductBrandId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung Galaxy S24",
                    Description = "Flagship Samsung smartphone with high-end features",
                    Price = 899.99m,
                    PictureUrl = "images/galaxys24.jpg",
                    ProductTypeId = 1,
                    ProductBrandId = 2
                },
                new Product
                {
                    Id = 3,
                    Name = "Sony WH-1000XM5",
                    Description = "High-quality noise-canceling headphones from Sony",
                    Price = 349.99m,
                    PictureUrl = "images/sony-headphones.jpg",
                    ProductTypeId = 3,
                    ProductBrandId = 3
                }
            );
        }
    }
}
