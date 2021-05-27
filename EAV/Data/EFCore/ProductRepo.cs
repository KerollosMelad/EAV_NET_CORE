using EAV.Models;
using EAV.Models.EFCore;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAV.Data.EFCore
{
    public class ProductRepo : EfCoreRepository<Product, MyDBContext>
    {
        private readonly MyDBContext _context;

        public ProductRepo(MyDBContext context) : base(context)
               => (_context) = (context);

        public async Task AddMockProduct()
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
                Product = prod,
                Property = prop,
                Value = "XL"
            });

            _context.Products.Add(prod);
            await _context.SaveChangesAsync();
        }

        public async Task AddMockProducts()
        {
            List<Product> products = new List<Product>();
            List<Property> Properties = new List<Property>();
            List<ProductProperty<int>> ProductProperties_int = new List<ProductProperty<int>>();
            List<ProductProperty<decimal>> ProductProperties_decimal = new List<ProductProperty<decimal>>();
            List<ProductProperty<string>> ProductProperties_string = new List<ProductProperty<string>>();
            List<ProductProperty<DateTime>> ProductProperties_DateTime = new List<ProductProperty<DateTime>>();
            List<string> props = new List<string>() { "int", "decimal", "string", "dateTime" };
            for (int i = 0; i < 5000000; i++)
            {
                Product prod = new Product()
                {
                    Id = i,
                    Name = $"prod {i}",
                };

                Property prop = new Property()
                {
                    Id = i,
                    Name = $"Prop {i % 8}",
                    DataType = props[i % 4]
                };
                Properties.Add(prop);

                var noOfProp = new Random().Next(1, 9);
                for (int j = 0; j < noOfProp % 5; j++)
                {
                    IProperity productProp;
                    if ((i + j) % 4 == 0)
                    {
                        productProp = new ProductProperty<int>
                        {
                            ProductId = prod.Id,
                            PropertyId = prop.Id,
                            Value = new Random().Next(30)
                        };
                        ProductProperties_int.Add(productProp as ProductProperty<int>);
                    }

                    else if ((i + j) % 4 == 1)
                    {
                        productProp = new ProductProperty<decimal>
                        {
                            ProductId = prod.Id,
                            PropertyId = prop.Id,
                            Value = (decimal)new Random().NextDouble() + new Random().Next(30)
                        };
                        ProductProperties_decimal.Add(productProp as ProductProperty<decimal>);
                    }

                    else if ((i + j) % 4 == 2)
                    {
                        productProp = new ProductProperty<string>
                        {
                            ProductId = prod.Id,
                            PropertyId = prop.Id,
                            Value = $"property value = {i % 10}"
                        };
                        ProductProperties_string.Add(productProp as ProductProperty<string>);
                    }

                    else if ((i + j) % 4 == 3)
                    {
                        productProp = new ProductProperty<DateTime>
                        {
                            ProductId = prod.Id,
                            PropertyId = prop.Id,
                            Value = DateTime.Today.AddDays(i % 10)
                        };
                        ProductProperties_DateTime.Add(productProp as ProductProperty<DateTime>);
                    }
                }
                products.Add(prod);
            }

            _context.BulkInsert(products);
            _context.BulkInsert(Properties);
            _context.BulkInsert(ProductProperties_int);
            _context.BulkInsert(ProductProperties_decimal);
            _context.BulkInsert(ProductProperties_string);
            _context.BulkInsert(ProductProperties_DateTime);
            await _context.SaveChangesAsync();
        }

        public Task<Product> GetProduct(int id)
        {
            return _context.Products
                 .Include(p => p.PropertiesDetails_int)
                 .Include(p => p.PropertiesDetails_decimal)
                 .Include(p => p.PropertiesDetails_string)
                 .Include(p => p.PropertiesDetails_dateTime)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
