using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Notifications;
using Tnf.Repositories.Uow;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Domain.Interfaces.Repositories;
using Totvs.Sample.Shop.Domain.Interfaces.Services;

namespace Totvs.Sample.Shop.Domain.Services {
    public class PriceDomainService : DomainService, IPriceDomainService
    {
        private delegate Task<Price> RepositoryCommand (Price Price);
        private readonly IUnitOfWorkManager unitOfWorkManager;
        private readonly IPriceRepository _PriceRepository;

        public PriceDomainService (
            IUnitOfWorkManager unitOfWorkManager,
            IPriceRepository repository,
            INotificationHandler notificationHandler) : base (notificationHandler)
        {
            this.unitOfWorkManager = unitOfWorkManager;
            _PriceRepository = repository;
        }

        public async Task<Price> InsertPriceAsync (Price.IPriceBuilder builder)
        {
            RepositoryCommand insertFunction = _PriceRepository.InsertPriceAndGetIdAsync;
            return await HandleTransaction (builder, insertFunction);
        }

        public async Task<Price> UpdatePriceAsync (Price.IPriceBuilder builder)
        {
            RepositoryCommand updateFunction = _PriceRepository.UpdatePriceAsync;
            return await HandleTransaction (builder, updateFunction);
        }

        private async Task<Price> HandleTransaction (Price.IPriceBuilder builder, RepositoryCommand command)
        {
            if (builder == null)
            {
                Notification.RaiseError (Constants.LocalizationSourceName, Error.DomainServiceOnUpdateNullBuilderError);
                return default (Price);
            }

            var price = builder.Build();

            if (Notification.HasNotification())
                return default(Price);

            using (var uow = unitOfWorkManager.Begin())
            {
                price = await command(price);

                await uow.CompleteAsync();
            }

            return price;
        }
    }
}