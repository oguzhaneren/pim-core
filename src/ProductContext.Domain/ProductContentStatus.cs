using ProductContext.Domain.Abstract;

namespace ProductContext.Domain
{
    public class ProductContentStatus
        : Enumeration<ProductContentStatus>
    {
        public static readonly ProductContentStatus Draft = new ProductContentStatus(0, "Draft");
        public static readonly ProductContentStatus DataEntry = new ProductContentStatus(1, "DataEntry");
        public static readonly ProductContentStatus Approved = new ProductContentStatus(2, "Approved");
        
        public ProductContentStatus(int value, string name)
            : base(value, name)
        {
        }
    }
}