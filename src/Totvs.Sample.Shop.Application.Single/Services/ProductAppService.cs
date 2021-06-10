using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Notifications;
using Totvs.Sample.Shop.Application.Single.Interfaces;
using Totvs.Sample.Shop.Application.Services.Interfaces;
using Totvs.Sample.Shop.Domain;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Domain.Interfaces.Repositories;
using Totvs.Sample.Shop.Domain.Interfaces.Services;
using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.Product;
using Totvs.Sample.Shop.Infra.ReadInterfaces;

namespace Totvs.Sample.Shop.Application.Single.Services
{
    public class ProductAppService : SingleAppService<ProductDto>, IProductAppService
    {
        private readonly IProductDomainService _domainService;
        private readonly IProductReadRepository _readRepository;
        private readonly INotificationHandler notificationHandler;
        private readonly IProductRepository _ProductDomainRepository;
        private readonly IProductCreator ProductCreator;
        private readonly IPriceAppService PriceAppService;
        private readonly IGenericAppService<ProductDto> genericAppService;
        public ProductAppService(IProductDomainService domainService,
            IProductReadRepository readRepository,
            IProductRepository ProductDomainRepository,
            INotificationHandler notificationHandler,
            IProductCreator ProductCreator,
            IPriceAppService PriceAppService,
            IGenericAppService<ProductDto> genericAppService) : base(notificationHandler, genericAppService, "Product")
        {
            _domainService = domainService;
            _readRepository = readRepository;
            _ProductDomainRepository = ProductDomainRepository;
            this.ProductCreator = ProductCreator;
            this.notificationHandler = notificationHandler;
            this.PriceAppService = PriceAppService;
            this.genericAppService = genericAppService;
        }

        public override async Task<(int httpStatus, dynamic businessObj)> Upsert(ProductDto dto)
        {
            genericAppService.DoInitialAppServiceOperations(
                dto,
                dataType,
                SetDtoRequiredProperties(dto));

            if (Notification.HasNotification())
                return (400, null);

            Product entityDomain = await _ProductDomainRepository.GetProductByCode(dto.Code);

            var response = new ProductResponseDto();

            if (entityDomain == null)
            {
                //caso não exista registro, proceder com o Insert
                var insertProductBuilder = ProductCreator.ConstructNew(dto);
                var ProductInsert = await _domainService.InsertProductAsync(insertProductBuilder);

                if (Notification.HasNotification())
                    return (400, null);

                response = ProductInsert.MapTo<ProductResponseDto>();
                return (201, response);
            }
            else
            {
                //caso contrário, proceder com o Update
                var updateProductBuilder = ProductCreator.ConstructExisting(dto, entityDomain);
                var ProductUpdate = await _domainService.UpdateProductAsync(updateProductBuilder);

                if (Notification.HasNotification())
                    return (400, null);

                response = ProductUpdate.MapTo<ProductResponseDto>();
                return (200, response);
            }
        }

        public async Task<ProductDto> UpdateProductAsync(Guid id, ProductDto dto)
        {
            if (!ValidateDtoAndId(dto, id))
                return null;

            if (dto.Name.IsNullOrEmpty())
            {
                notificationHandler.DefaultBuilder
                    .AsSpecification()
                    .WithMessage(Domain.Constants.LocalizationSourceName, Domain.GlobalizationKey.InvalidProductName)
                    .WithMessageFormat(dto.Code)
                    .Raise();

                return null;
            }

            var entityDomain = await _ProductDomainRepository.GetProduct(id);

            if (entityDomain == null)
                return null;

            var updateProductBuilder = Product.Create(Notification, entityDomain)
                .WithLastChange(DateTime.Now)
                .WithName(dto.Name)
                .WithIsActive(dto.IsActive);

            var productUpdate = await _domainService.UpdateProductAsync(updateProductBuilder);

            if (Notification.HasNotification())
                return dto;

            return productUpdate.MapTo<ProductDto>();
        }

        public async Task<ProductResponseDto> GetProductAsync(DefaultRequestDto id)
        {
            if (!ValidateRequestDto(id) || !ValidateId<Guid>(id.Id))
                return null;

            var entity = await _readRepository.GetProductAsync(id);

            return entity.MapTo<ProductResponseDto>();
        }

        public async Task<IListDto<ProductResponseDto>> GetAllProductAsync(ProductRequestAllDto request)
        {
            return await _readRepository.GetAllProductsAsync(request);
        }

        private static List<(string, GlobalizationKey)> SetDtoRequiredProperties(ProductDto dto)
        {
            List<(string, Domain.GlobalizationKey)> requiredProperties = new List<(string, Domain.GlobalizationKey)>();

            requiredProperties.Add((dto.Code, Domain.GlobalizationKey.InvalidProductCode));
            requiredProperties.Add((dto.Name, Domain.GlobalizationKey.InvalidProductName));

            return requiredProperties;
        }
    }
}