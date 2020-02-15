using Andreys.Data;
using Andreys.Models;
using Andreys.Models.Enums;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Andreys.Services
{
    public class ProductService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void Add(AddProductsInputModel model)
        {
            var gender = Enum.Parse<Gender>(model.Gender);
            var category = Enum.Parse<Category>(model.Category);

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Category = category,
                Gender = gender,
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return this.db.Products.Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                ImageUrl = x.ImageUrl
            }).ToArray();
        }

        public Product GetProductById(int id)
        {
            var product = this.db.Products.FirstOrDefault(x=>x.Id == id);
            return product;
        }
    }
}
