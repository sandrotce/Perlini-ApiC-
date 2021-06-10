using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Notifications;
using Totvs.Sample.Shop.Application.Services;
using Totvs.Sample.Shop.Application.Services.Interfaces;
using Totvs.Sample.Shop.Domain;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Dto.RelationshipValidationRule;
using Totvs.Sample.Shop.Infra.ReadInterfaces;

namespace Totvs.Sample.Shop.Application.Services
{
    public class GenericAppService<Dto> : ApplicationService, IGenericAppService<Dto>
    {
        private readonly INotificationHandler notificationHandler;

        public GenericAppService(
            INotificationHandler notificationHandler) : base(notificationHandler)
        {
            this.notificationHandler = notificationHandler;
        }

        /// <summary>
        /// Logs, Check Configuration and validate required properties
        /// </summary>
        /// <param name="httpStatus"></param>
        /// <param name="domainFromValidations"></param>
        /// <returns>Error status if it should stop</returns>
        public bool DoInitialAppServiceOperations(
            IDto dto,
            string dataType,
            List<(string, Domain.GlobalizationKey)> requiredProperties = null
        )
        {
            Log.Information("Message Received. DataType: {1}", dataType);
            Log.Debug("Body of Received Message: Body: {0}", JsonConvert.SerializeObject(dto));

            if (!ValidateDto<Dto>(dto))
                return false;

            CheckRequiredProperties(dto, requiredProperties);

            if (Notification.HasNotification())
                return false;

            return true;
        }

        public int CheckAndHandleRelationshipRules(
            IDto dto,
            List<RelationshipValidationRuleDto> relationshipValidationRules,
            string dataType
        )
        {
            if (relationshipValidationRules != null)
            {
                foreach (var validationRule in relationshipValidationRules)
                {
                    if (!(validationRule.allowNull && validationRule.relatedKey == null))
                    {
                        GlobalizationKey globalizationKey = validationRule.errorGlobalizationKey.ToEnum<GlobalizationKey>();

                        if (validationRule.relatedEntity == null)
                        {
                            notificationHandler.DefaultBuilder
                                .AsSpecification()
                                .WithMessage(Domain.Constants.LocalizationSourceName, globalizationKey)
                                .Raise();
                        }
                    }
                }
            }

            return (0);
        }


        private void CheckRequiredProperties(IDto dto, List<(string propertyValue, Domain.GlobalizationKey errorGlobalizationKey)> requiredProperties)
        {
            if (requiredProperties != null)
            {
                foreach (var requiredProperty in requiredProperties)
                {
                    if (requiredProperty.propertyValue.IsNullOrEmpty())
                    {
                        notificationHandler.DefaultBuilder
                            .AsSpecification()
                            .WithMessage(Domain.Constants.LocalizationSourceName, requiredProperty.errorGlobalizationKey)
                            .Raise();

                        break;
                    }
                }
            }
        }
    }
}