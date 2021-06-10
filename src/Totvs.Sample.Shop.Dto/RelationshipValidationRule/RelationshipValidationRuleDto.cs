namespace Totvs.Sample.Shop.Dto.RelationshipValidationRule
{
    public class RelationshipValidationRuleDto
    {
        public string relatedKey { get; set; }
        public string relatedDataType { get; set; }
        public dynamic relatedEntity { get; set; }
        public string errorGlobalizationKey { get; set; }
        public bool allowNull { get; set; }
    }
}