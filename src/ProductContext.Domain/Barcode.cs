using System;
using ProductContext.Domain.Abstract;

namespace ProductContext.Domain
{
    public class Barcode : IEquatable<Barcode>
    {
        private readonly string _value;

        public Barcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode?.Trim()))
            {
                throw new DomainException("invalid barcode");
            }
            
            // some barcode validates
            _value = barcode?.Trim();
        }

        public bool Equals(Barcode other)
        {
            throw new NotImplementedException();
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

            return Equals((Barcode) obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Barcode left, Barcode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Barcode left, Barcode right)
        {
            return !Equals(left, right);
        }
    }
}