using EAV.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EAV.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public virtual IEnumerable<ProductProperty<object>> PropertiesDetails
        {
            get
            {
                return (PropertiesDetails_int as IEnumerable<ProductProperty<object>>)
                .Concat(PropertiesDetails_decimal as IEnumerable<ProductProperty<object>>)
                .Concat(PropertiesDetails_string as IEnumerable<ProductProperty<object>>)
                .Concat(PropertiesDetails_dateTime as IEnumerable<ProductProperty<object>>);
            }
        }

        public void AddProperty<T>(ProductProperty<T> productProperty)
        {
            productProperty.Product = this;
            switch (productProperty.Value)
            {
                case int:
                    if (PropertiesDetails_int == null) PropertiesDetails_int = new List<ProductProperty<int>>();
                    PropertiesDetails_int.Add(productProperty as ProductProperty<int>);
                    break;
                case decimal:
                    if (PropertiesDetails_decimal == null) PropertiesDetails_decimal = new List<ProductProperty<decimal>>();
                    PropertiesDetails_decimal.Add(productProperty as ProductProperty<decimal>);
                    break;
                case string:
                    if (PropertiesDetails_string == null) PropertiesDetails_string = new List<ProductProperty<string>>();
                    PropertiesDetails_string.Add(productProperty as ProductProperty<string>);
                    break;
                case DateTime:
                    if (PropertiesDetails_dateTime == null) PropertiesDetails_dateTime = new List<ProductProperty<DateTime>>();
                    PropertiesDetails_dateTime.Add(productProperty as ProductProperty<DateTime>);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public virtual List<ProductProperty<int>> PropertiesDetails_int { get; set; } 
        public virtual List<ProductProperty<decimal>> PropertiesDetails_decimal { get; set; } 
        public virtual List<ProductProperty<string>> PropertiesDetails_string { get; set; }
        public virtual List<ProductProperty<DateTime>> PropertiesDetails_dateTime { get; set; } 
    }

    public class Property : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
    }


    public class ProductProperty<T> 
    {
        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public T Value { get; set; }
    }
}
