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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ProductBrand>().HasData(
                new ProductBrand { Id = 1, Name = "Apple" },
                new ProductBrand { Id = 2, Name = "Samsung" },
                new ProductBrand { Id = 3, Name = "Sony" },
                new ProductBrand { Id = 4, Name = "Dell" },
                new ProductBrand { Id = 5, Name = "HP" },
                new ProductBrand { Id = 6, Name = "Bose" }
            );
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Name = "Smartphone" },
                new ProductType { Id = 2, Name = "Laptop" },
                new ProductType { Id = 3, Name = "Headphones" },
                new ProductType { Id = 4, Name = "Tablet" },
                new ProductType { Id = 5, Name = "Smartwatch" }
            );          
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15",
                    Description = "Latest Apple smartphone with A16 chip",
                    Price = 999.99m,
                    PictureUrl = "images/iphone15.jpg",
                    ProductTypeId = 1,
                    ProductBrandId = 1,
                    InStock = 50
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung Galaxy S24",
                    Description = "Flagship Samsung smartphone with high-end features",
                    Price = 899.99m,
                    PictureUrl = "images/galaxys24.jpg",
                    ProductTypeId = 1,
                    ProductBrandId = 2,
                    InStock = 30
                },
                new Product
                {
                    Id = 3,
                    Name = "Sony WH-1000XM5",
                    Description = "High-quality noise-canceling headphones from Sony",
                    Price = 349.99m,
                    PictureUrl = "images/sony-headphones.jpg",
                    ProductTypeId = 3,
                    ProductBrandId = 3,
                    InStock = 20
                },
                new Product
                {
                    Id = 4,
                    Name = "Dell XPS 15",
                    Description = "Powerful laptop with stunning display",
                    Price = 1599.99m,
                    PictureUrl = "images/dell-xps15.jpg",
                    ProductTypeId = 2,
                    ProductBrandId = 4,
                    InStock = 15
                },
                new Product
                {
                    Id = 5,
                    Name = "HP Spectre x360",
                    Description = "Convertible laptop with sleek design",
                    Price = 1399.99m,
                    PictureUrl = "images/hp-spectre.jpg",
                    ProductTypeId = 2,
                    ProductBrandId = 5,
                    InStock = 25
                },
                new Product
                {
                    Id = 6,
                    Name = "Bose QuietComfort 45",
                    Description = "Premium noise-canceling headphones with immersive sound",
                    Price = 329.99m,
                    PictureUrl = "images/bose-qc45.jpg",
                    ProductTypeId = 3,
                    ProductBrandId = 6,
                    InStock = 10
                },
                new Product
                {
                    Id = 7,
                    Name = "iPad Pro 12.9",
                    Description = "Apple's most powerful tablet with M2 chip",
                    Price = 1099.99m,
                    PictureUrl = "images/ipad-pro.jpg",
                    ProductTypeId = 4,
                    ProductBrandId = 1,
                    InStock = 12
                },
                new Product
                {
                    Id = 8,
                    Name = "Samsung Galaxy Tab S9",
                    Description = "Premium Android tablet with AMOLED display",
                    Price = 799.99m,
                    PictureUrl = "images/galaxy-tab-s9.jpg",
                    ProductTypeId = 4,
                    ProductBrandId = 2,
                    InStock = 18
                },
                new Product
                {
                    Id = 9,
                    Name = "Apple Watch Series 9",
                    Description = "Latest Apple smartwatch with fitness tracking",
                    Price = 499.99m,
                    PictureUrl = "images/apple-watch9.jpg",
                    ProductTypeId = 5,
                    ProductBrandId = 1,
                    InStock = 35
                },
                new Product
                {
                    Id = 10,
                    Name = "Samsung Galaxy Watch 6",
                    Description = "Advanced smartwatch with health monitoring features",
                    Price = 449.99m,
                    PictureUrl = "images/galaxy-watch6.jpg",
                    ProductTypeId = 5,
                    ProductBrandId = 2,
                    InStock = 40
                }
            );

            modelBuilder.Entity<DeliveryMethod>().HasData(
                 new DeliveryMethod { Id = 1, ShortName = "Standard", DeliveryTime = "5-7 Days", Description = "Standard shipping method", Price = 20m },
                 new DeliveryMethod { Id = 2, ShortName = "Express", DeliveryTime = "2-3 Days", Description = "Faster shipping", Price = 50m },
                 new DeliveryMethod { Id = 3, ShortName = "Overnight", DeliveryTime = "1 Day", Description = "Next-day delivery", Price = 100m }
            );
        }
    }
}
