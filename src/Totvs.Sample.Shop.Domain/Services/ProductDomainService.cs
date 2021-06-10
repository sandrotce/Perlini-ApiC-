using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Notifications;
using Tnf.Repositories.Uow;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Domain.Interfaces.Repositories;
using Totvs.Sample.Shop.Domain.Interfaces.Services;

namespace Totvs.Sample.Shop.Domain.Services {
    public class ProductDomainService : DomainService, IProductDomainService
    {
        private delegate Task<Product> RepositoryCommand (Product Product);
        private readonly IUnitOfWorkManager unitOfWorkManager;
        private readonly IProductRepository _ProductRepository;

        public ProductDomainService (
            IUnitOfWorkManager unitOfWorkManager,
            IProductRepository ProductRepository,
            INotificationHandler notificationHandler) : base (notificationHandler)
        {
            this.unitOfWorkManager = unitOfWorkManager;
            _ProductRepository = ProductRepository;
        }

        public async Task<Product> InsertProductAsync (Product.IProductBuilder builder)
        {
            RepositoryCommand insertFunction = _ProductRepository.InsertProductAndGetIdAsync;
            return await HandleTransaction (builder, insertFunction);
        }

        public async Task<Product> UpdateProductAsync (Product.IProductBuilder builder)
        {
            RepositoryCommand updateFunction = _ProductRepository.UpdateProductAsync;
            return await HandleTransaction (builder, updateFunction);
        }

        private async Task<Product> HandleTransaction (Product.IProductBuilder builder, RepositoryCommand command)
        {
            if (builder == null)
            {
                Notification.RaiseError (Constants.LocalizationSourceName, Error.DomainServiceOnInsertNullBuilderError);
                return default (Product);
            }

            var product = builder.Build ();

            if (Notification.HasNotification())
                return default (Product);

            using (var uow = unitOfWorkManager.Begin())
            {
                product = await command(product);
                await uow.CompleteAsync();
            }

            return product;
        }
    }
}