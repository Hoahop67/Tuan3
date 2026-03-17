using Tuan3.Models;// Tuan 5
namespace Tuan3.Data

{
    public class SeedData
    {
        // Gia lap co so du lieu cho Category
        public static List<Category> Categories = new List<Category>


            {
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                new Category { CategoryId = 2, CategoryName = "Books" },
                new Category { CategoryId = 3, CategoryName = "Clothing" }
            };

        public static List<Product> Products = new List<Product>


            {
                new Product { ProductId = 1, ProductName = "Laptop", Price = 999.99m, CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Smartphone", Price = 499.99m, CategoryId = 1 },
                new Product { ProductId = 3, ProductName = "Novel", Price = 19.99m, CategoryId = 2 },
                new Product { ProductId = 4, ProductName = "T-Shirt", Price = 14.99m, CategoryId = 3 }
            };
        
    }
}
