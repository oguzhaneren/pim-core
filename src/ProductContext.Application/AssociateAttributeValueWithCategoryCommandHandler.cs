using System;
using System.Threading;
using System.Threading.Tasks;
using ProductContext.Application.Ports.Persistence;

namespace ProductContext.Application
{
    public class AssociateAttributeValueWithCategoryCommandHandler
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAttributeRepository _attributeRepository;

        public AssociateAttributeValueWithCategoryCommandHandler(ICategoryRepository categoryRepository, IAttributeRepository attributeRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
        }

        public async Task Handle(AssociateAttributeValueWithCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.Load(command.CategoryId, cancellationToken);
            var attribute = await _attributeRepository.Load(command.AttributeId, cancellationToken);
            
            var attributeValue = attribute.GetValue(command.AttributeValueId);
            category.AssociateWithAttributeValue(attribute.Id, attributeValue);
            
            await _categoryRepository.Save(category, cancellationToken);
        }
    }
}