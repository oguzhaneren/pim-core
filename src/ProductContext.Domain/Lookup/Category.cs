using System;
using System.Collections.Generic;
using System.Linq;
using ProductContext.Domain.Abstract;

namespace ProductContext.Domain.Lookup
{
    public class Category
        : AggregateRootEntity<int>
    {
        private readonly ISet<CategoryAttribute> _attributes = new HashSet<CategoryAttribute>();

        public string Name { get; private set; }
        public int VatRate { get; private set; }
        public int Installment { get; private set; }
        public bool IsPassive { get; private set; }
        public int? ParentId { get; private set; }
        public string HierarchyPath { get; private set; }
        public decimal? Commission { get; private set; }
        public string UxLayout { get; private set; }
        public int? Maturity { get; private set; }

        public IEnumerable<CategoryAttribute> Attributes
        {
            get => _attributes;
        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void ChangeParent(Category category)
        {
            ParentId = category?.Id;
            HierarchyPath = BuildHierarchyPath();
        }

        private string BuildHierarchyPath()
        {
            return "";
        }

        public void ChangeUxLayout(string uxLayout)
        {
            UxLayout = uxLayout;
        }

        public void ChangeMaturity(int? maturity)
        {
            Maturity = maturity;
        }

        public void ChangeCommission(decimal? commission)
        {
            Commission = commission;
        }

        public void ToPassive()
        {
            IsPassive = true;
        }

        public void ChangeInstallment(int installment)
        {
            Installment = installment;
        }

        public void ChangeVatRate(int vatRate)
        {
            VatRate = vatRate;
        }

        public void AssociateWithAttribute(Attribute attribute)
        {
        }

        public CategoryAttribute GetCategoryAttribute(int attributeId)
        {
            return FindCategoryAttribute(attributeId) ?? throw new DomainException($"{attributeId} attribute not associated with category!");
        }

        public Product CreateProduct(long productId, Brand brand, BusinessUnit businessUnit,Product.ProductAttribute[] productAttributes)
        {
            return new Product(productId, brand.Id, businessUnit.Id, Id, productAttributes);
        }

        public CategoryAttribute FindCategoryAttribute(int attributeId)
        {
            return _attributes.FirstOrDefault(a => a.AttributeId == attributeId);
        }

        public void AssociateWithAttributeValue(int attributeId, AttributeValue attributeValue)
        {
            GetCategoryAttribute(attributeId).AddAttributeValue(attributeValue);
        }
    }
}