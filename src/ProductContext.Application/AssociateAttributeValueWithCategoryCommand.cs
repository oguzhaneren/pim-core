using ProductContext.Application.Exceptions;

namespace ProductContext.Application
{
    public class AssociateAttributeValueWithCategoryCommand
    {
        public int CategoryId { get; set; }
        public int AttributeId { get; set; }
        public int AttributeValueId { get; set; }
    }
}