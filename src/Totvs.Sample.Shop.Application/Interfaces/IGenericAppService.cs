using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Dto;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Domain.Services;
using Totvs.Sample.Shop.Dto.RelationshipValidationRule;

namespace Totvs.Sample.Shop.Application.Services.Interfaces
{
    public interface IGenericAppService<Dto>
    {
        bool DoInitialAppServiceOperations(
            IDto dto,
            string dataType,
            List<(string, Domain.GlobalizationKey)> requiredProperties = null);

        int CheckAndHandleRelationshipRules(
            IDto dto,
            List<RelationshipValidationRuleDto> relationshipValidationRules,
            string dataType
        );
    }
}