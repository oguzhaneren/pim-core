using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProductContext.Application.Exceptions;
using ProductContext.Application.Ports;
using ProductContext.Application.Ports.Persistence;
using ProductContext.Domain;
using ProductContext.Domain.Lookup;

namespace ProductContext.Application
{
    public class CreateProductCommandHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IBusinessUnitRepository _businessUnitRepository;

        public CreateProductCommandHandler(IProductRepository productRepository,
            ICategoryRepository categoryRepository, IBrandRepository brandRepository, IBusinessUnitRepository businessUnitRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _businessUnitRepository = businessUnitRepository ?? throw new ArgumentNullException(nameof(businessUnitRepository));
        }

        public async Task Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.Load(command.CategoryId, cancellationToken);
            var brand = await _brandRepository.Load(command.BrandId, cancellationToken);
            var businessUnit = await _businessUnitRepository.Load(command.BusinessUnitId, cancellationToken);

            var productAttributes = command.Attributes.Select(a => BuildProductAttribute(category, a)).ToArray();
            var product = category.CreateProduct(command.ProductId, brand, businessUnit, productAttributes);
            await _productRepository.Save(product, cancellationToken);
        }

        private Product.ProductAttribute BuildProductAttribute(Category category, CreateProductCommand.Attribute attribute)
        {
            var categoryAttribute = category.GetCategoryAttribute(attribute.Id);
            return attribute.ValueId.HasValue
                ? categoryAttribute.CreateProductAttributeByValueId(attribute.ValueId.Value)
                : categoryAttribute.CreateProductAttributeByCustomValue(attribute.CustomValue);
        }
    }
}