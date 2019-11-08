using System.Collections.Generic;
using System.Linq;
using ProductContext.Domain.Abstract;

namespace ProductContext.Domain.Lookup
{
    public class CategoryAttribute
        : ExplicitValueObject<CategoryAttribute>
    {
        private readonly ISet<CategoryAttributeValue> _values = new HashSet<CategoryAttributeValue>();

        public int AttributeId { get; }
        public bool AllowCustom { get; }
        public bool Required { get; }
        public bool Slicer { get; }
        public bool Varianter { get; }
        public bool RequiredForApprove { get; }
        public bool IsSearchable { get; }
        public int DisplayTypeId { get; }

        public IEnumerable<CategoryAttributeValue> Values
        {
            get => _values;
        }

        public CategoryAttribute(int attributeId, string name, bool allowCustom, bool required, bool slicer, bool varianter, bool requiredForApprove, bool isSearchable, int displayTypeId)
        {
            AllowCustom = allowCustom;
            Required = required;
            Slicer = slicer;
            Varianter = varianter;
            RequiredForApprove = requiredForApprove;
            IsSearchable = isSearchable;
            DisplayTypeId = displayTypeId;
        }

        internal void AddAttributeValue(AttributeValue attributeValue)
        {
            _values.Add(new CategoryAttributeValue(attributeValue.Id, attributeValue.Name, attributeValue.IsSearchable, attributeValue.SortSequence));
        }

        public Product.ProductAttribute CreateProductAttributeByValueId(int attributeValueId)
        {
            var attributeValue = _values.FirstOrDefault(x => x.Id == attributeValueId);
            if (attributeValue == null)
            {
                throw new DomainException($"{attributeValueId} attribute value not associated with category attribute of {AttributeId}");
            }

            return new Product.ProductAttribute(AttributeId, attributeValue.Id, null);
        }

        public Product.ProductAttribute CreateProductAttributeByCustomValue(string customValue)
        {
            if (AllowCustom == false)
            {
                throw new DomainException("Custom value cannot allowed for category attribute!");
            }

            return new Product.ProductAttribute(AttributeId, null, customValue);
        }

        public override IEnumerable<object> GetEqualityFieldValues()
        {
            yield return AttributeId;
        }
    }
}