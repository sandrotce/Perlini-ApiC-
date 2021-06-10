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
using Totvs.Sample.Shop.Dto.Price;
using Totvs.Sample.Shop.Dto.RelationshipValidationRule;
using Totvs.Sample.Shop.Infra.ReadInterfaces;

namespace Totvs.Sample.Shop.Application.Single.Services
{
    public class PriceAppService : SingleAppService<PriceDto>, IPriceAppService
    {
        private readonly IPriceDomainService _domainService;
        private readonly IPriceReadRepository _readRepository;
        private readonly IProductReadRepository _readRepositoryProduct;
        private readonly INotificationHandler notificationHandler;
        private readonly IPriceRepository _priceDomainRepository;
        private readonly IProductRepository _productDomainRepository;
        private readonly IPriceCreator priceCreator;
        private readonly IGenericAppService<PriceDto> genericAppService;

        public PriceAppService(IPriceDomainService domainService,
            IPriceReadRepository readRepository,
            IProductReadRepository readRepositoryProduct,
            IPriceRepository priceDomainRepository,
            IProductRepository productDomainRepository,
            INotificationHandler notificationHandler,
            IPriceCreator priceCreator,
            IGenericAppService<PriceDto> genericAppService) : base(notificationHandler, genericAppService, "Price")
        {
            _domainService = domainService;
            _readRepository = readRepository;
            _readRepositoryProduct = readRepositoryProduct;
            _priceDomainRepository = priceDomainRepository;
            _productDomainRepository = productDomainRepository;
            this.priceCreator = priceCreator;
            this.notificationHandler = notificationHandler;
            this.genericAppService = genericAppService;
        }

        public override async Task<(int httpStatus, dynamic businessObj)> Upsert(PriceDto dto)
        {
            genericAppService.DoInitialAppServiceOperations(
                dto,
                dataType,
                SetDtoRequiredProperties(dto));

            if (Notification.HasNotification())
                return (400, null);

            Product productDomain = await _productDomainRepository.GetProductByCode(dto.ProductCode);

            var result = genericAppService.CheckAndHandleRelationshipRules(
                dto,
                SetRelationshipRules(dto, productDomain),
                dataType);

            if (Notification.HasNotification())
                return (400, null);

            Price entityDomain = await _priceDomainRepository.GetPriceByProductCode(dto.ProductCode);

            var response = new PriceResponseDto();

            if (entityDomain == null)
            {
                var insertPriceBuilder = priceCreator.ConstructNew(dto, productDomain.Id);
                var priceInsert = await _domainService.InsertPriceAsync(insertPriceBuilder);

                if (Notification.HasNotification())
                    return (400, null);

                response = priceInsert.MapTo<PriceResponseDto>();
                return (201, response);
            }
            else
            {
                var updatePriceBuilder = priceCreator.ConstructExisting(dto, entityDomain);
                var PriceUpdate = await _domainService.UpdatePriceAsync(updatePriceBuilder);

                if (Notification.HasNotification())
                    return (400, null);

                response = PriceUpdate.MapTo<PriceResponseDto>();
                return (200, response);
            }
        }

        public async Task<PriceDto> UpdatePriceAsync(Guid id, PriceDto dto)
        {
            if (!ValidateDtoAndId(dto, id))
                return null;

            var entityDomain = await _priceDomainRepository.GetPrice(id);

            if (entityDomain == null)
                return null;

            var updatePriceBuilder = Price.Create(Notification, entityDomain)
                .WithLastChange(DateTime.Now)
                .WithStartDate(dto.StartDate)
                .WithEndDate(dto.EndDate)
                .WithIsActive(dto.IsActive);

            var priceUpdate = await _domainService.UpdatePriceAsync(updatePriceBuilder);

            if (Notification.HasNotification())
                return null;

            return priceUpdate.MapTo<PriceDto>();
        }

        public async Task<PriceResponseDto> GetPriceAsync(DefaultRequestDto id)
        {
            if (!ValidateRequestDto(id) || !ValidateId<Guid>(id.Id))
                return null;

            var entity = await _readRepository.GetPriceAsync(id);

            var priceResponseDto = entity.MapTo<PriceResponseDto>();
            
            return priceResponseDto;
        }

        public async Task<IListDto<PriceResponseDto>> GetAllPriceAsync(PriceRequestAllDto request)
        {
            return await _readRepository.GetAllPricesAsync(request);
        }

        private static List<(string, GlobalizationKey)> SetDtoRequiredProperties(PriceDto dto)
        {
            List<(string, Domain.GlobalizationKey)> requiredProperties = new List<(string, Domain.GlobalizationKey)>();

            requiredProperties.Add((dto.ProductCode, Domain.GlobalizationKey.InvalidProductCode));

            return requiredProperties;
        }

        private List<RelationshipValidationRuleDto> SetRelationshipRules(PriceDto dto, Product Product)
        {
            List<RelationshipValidationRuleDto> relationshipValidationRules = new List<RelationshipValidationRuleDto>();
            relationshipValidationRules.Add(new RelationshipValidationRuleDto()
            {
                relatedEntity = Product,
                relatedKey = dto.ProductCode,
                relatedDataType = "Product",
                allowNull = false,
                errorGlobalizationKey = Domain.GlobalizationKey.ProductNotFound.ToString()
            });

            return relationshipValidationRules;
        }
    }
}