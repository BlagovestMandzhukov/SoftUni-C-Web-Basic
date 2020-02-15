using Andreys.Models;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IProductsService
    {
        IEnumerable<Product> GetAll();

        void Add(AddProductsInputModel model);

        Product GetProductById(int id);
    }

}
