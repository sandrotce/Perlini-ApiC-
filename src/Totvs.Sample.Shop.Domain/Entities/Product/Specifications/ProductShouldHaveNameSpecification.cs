using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace Totvs.Sample.Shop.Domain.Entities.Specifications
{
    public class ProductShouldHaveNameSpecification : Specification<Product>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Product.Error.ProductShouldHaveName;

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Name);
        }
    }
}
