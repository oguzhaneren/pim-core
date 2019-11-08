using System.Collections.Generic;
using ProductContext.Domain.Abstract;

namespace ProductContext.Domain
{
    public class ProductContent
        : Entity<long>
    {
        private readonly ISet<Variant> _variants = new HashSet<Variant>();
        public long ProductId { get; }
        public string Name { get; }
        public string Description { get; }
        public ProductContentStatus Status { get; private set; }

        public IEnumerable<Variant> Variants => _variants;

        internal ProductContent(in long id, in long productId, string name, string description)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Description = description;
            Status = ProductContentStatus.Draft;
        }

        public void Approve()
        {
            Status = ProductContentStatus.Approved;
        }

        public Variant AddVariant(in long variantId, Barcode barcode)
        {
            var variant = new Variant(variantId, Id, ProductId, barcode);
            _variants.Add(variant);
            return variant;
        }
    }
}