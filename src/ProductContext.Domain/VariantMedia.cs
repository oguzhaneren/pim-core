using ProductContext.Domain.Abstract;

namespace ProductContext.Domain
{
    public class VariantMedia
        : Entity<int>
    {
        public string Path { get; }
        public VariantMediaType MediaType { get; }

        public VariantMedia(int id, string path)
        {
            Id = id;
            Path = path;
            MediaType = VariantMediaType.Image;
        }
    }
}