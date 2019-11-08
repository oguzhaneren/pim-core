namespace ProductContext.Messages
{
    public class ProductCreated
    {
        public long ProductId { get; }
        public long CategoryId { get; }

        public ProductCreated(long productId, long categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }
    }
}