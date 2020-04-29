using System;
using System.Collections.Generic;

namespace ProductService.Lib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService
    {
        List<Product> IProductService.GetProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name="Coke 440 ML",
                    Category="Soft Drinks",
                    DateCreated=DateTime.Now.AddDays(-3),
                    Price= 4
                },
                new Product()
                {
                    Name="Sprite 440 ML",
                    Category="Soft Drinks",
                    DateCreated=DateTime.Now.AddDays(-30),
                    Price= 4
                },
                new Product()
                {
                    Name="Wors",
                    Category="Meat",
                    DateCreated=DateTime.Now.AddDays(-30),
                    Price= 15
                },

            };

        }
    }
}
