using ProductContext.Domain.Abstract;

namespace ProductContext.Domain
{
    public class VariantMediaType
        : Enumeration<VariantMediaType>
    {
        public static readonly VariantMediaType Image = new VariantMediaType(0, "Image");
        public static readonly VariantMediaType Video = new VariantMediaType(1, "Video");
        public static readonly VariantMediaType Content = new VariantMediaType(2, "Content");
        
        public VariantMediaType(int value, string name)
            : base(value, name)
        {
        }
    }
}