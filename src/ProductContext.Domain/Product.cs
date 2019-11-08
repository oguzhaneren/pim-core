using System.Collections.Generic;
using ProductContext.Messages;
using ProductContext.Domain.Abstract;
using ProductContext.Domain.Lookup;

namespace ProductContext.Domain
{
    public class Product
        : AggregateRootEntity<long>
    {
        private readonly ISet<ProductAttribute> _attributes = new HashSet<ProductAttribute>();
        private readonly ISet<Variant> _variants = new HashSet<Variant>();

        public int BusinessUnitId { get; }
        public int CategoryId { get; }
        public int BrandId { get; }

        public IEnumerable<ProductAttribute> Attributes => _attributes;

        internal Product(in long productId, in int brandId, in int businessUnitId, in int categoryId, IEnumerable<ProductAttribute> attributes)
        {
            Id = productId;
            BrandId = brandId;
            BusinessUnitId = businessUnitId;
            CategoryId = categoryId;
            foreach (var attribute in attributes)
            {
                _attributes.Add(attribute);
            }

            RaiseEvent(new ProductCreated(productId, categoryId));
        }

        public class ProductAttribute
            : ExplicitValueObject<ProductAttribute>
        {
            public int Id { get; }
            public int? ValueId { get; }
            public string CustomValue { get; }

            internal ProductAttribute(int id, int? valueId, string customValue)
            {
                Id = id;
                ValueId = valueId;
                CustomValue = customValue;
            }

            public override IEnumerable<object> GetEqualityFieldValues()
            {
                yield return Id;
                yield return ValueId;
                yield return CustomValue;
            }
        }
    }
}