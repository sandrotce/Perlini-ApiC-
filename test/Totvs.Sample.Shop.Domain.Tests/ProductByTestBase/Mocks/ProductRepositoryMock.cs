using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Notifications;

namespace Totvs.Sample.Shop.Domain.Tests.ProductByTestBase.Mocks
{
    public class ProductRepositoryMock : IProductRepository
    {
        private readonly INotificationHandler notificationHandler;

        public static string productCode = "123";

        private List<Product> list = new List<Product>();

        public ProductRepositoryMock(INotificationHandler notificationHandler)
        {
            this.notificationHandler = notificationHandler;

            list.Add(Product.Create(notificationHandler).WithCode(productCode).WithName("Test Product 123").WithIsActive(true).Build());
            list.Add(Product.Create(notificationHandler).WithCode(new Random().Next(1, 1000).ToString()).WithName("Test Product X").WithIsActive(true).Build());
            list.Add(Product.Create(notificationHandler).WithCode(new Random().Next(1, 1000).ToString()).WithName("Test Product Y").WithIsActive(true).Build());
        }

        public Task<Product> InsertProductAndGetIdAsync(Product product)
        {
            list.Add(product);

            return product.AsTask();
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            list.RemoveAll(c => c.Code == product.Code);
            list.Add(product);

            return product.AsTask();
        }

        public Task<Product> GetProductByCode(string code)
        {
            var product = list.Find(c => c.Code == code);

            return product.AsTask();
        }

        public Task<Product> GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
