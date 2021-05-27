using EAV.Models;
using EAV.Models.EFCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EAV.Data.EFCore
{
    public class ProductRepo : EfCoreRepository<Product, MyDBContext>
    {
        private readonly MyDBContext _context;

        public ProductRepo(MyDBContext context) : base(context)
            => (_context) = (context);

        public async Task AddMockProducts()
        {
            Product prod = new Product()
            {
                Name = "prod1",
            };

            Property prop = new Property()
            {
                Name = "Size",
                DataType = "string"
            };

            prod.AddProperty(new ProductProperty<string>
            {
                Property = prop,
                Value = "XL"
            });

            _context.Products.Add(prod);
            await _context.SaveChangesAsync();
        }
    }
}
