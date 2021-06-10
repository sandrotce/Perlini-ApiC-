using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace Totvs.Sample.Shop.Domain.Entities.Specifications
{
    public class PriceShouldHaveValueSpecification : Specification<Price>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Price.Error.PriceShouldHaveValue;

        public override Expression<Func<Price, bool>> ToExpression()
        {
            return (p) => p.Value > 0;
        }
    }
}
