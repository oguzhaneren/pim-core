using System.Collections.Generic;
using System.Linq;
using ProductContext.Domain.Abstract;

namespace ProductContext.Domain
{
    public class Variant
    {
        public long Id { get; }
        private readonly ISet<VariantMedia> _mediaList = new HashSet<VariantMedia>();
        public long ContentId { get; }
        public long ProductId { get; }
        public Barcode Barcode { get; }

        public ISet<VariantMedia> MediaList => _mediaList;

        internal Variant(long id, long contentId, long productId, Barcode barcode)
        {
            Id = id;
            ContentId = contentId;
            ProductId = productId;
            Barcode = barcode;
        }

        internal void AddMedia(string path)
        {
            var id = _mediaList.OrderByDescending(x => x.Id).FirstOrDefault()?.Id;
            var media = new VariantMedia(id.GetValueOrDefault(0) + 1, path);
            _mediaList.Add(media);
        }

        protected bool Equals(Variant other)
        {
            return Id == other.Id || Equals(Barcode, other.Barcode);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Variant) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ (Barcode != null ? Barcode.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Variant left, Variant right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Variant left, Variant right)
        {
            return !Equals(left, right);
        }
    }
}