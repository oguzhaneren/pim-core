using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductContext.Domain.Abstract
{
    public abstract class ExplicitValueObject<T> : IEquatable<T> where T : ExplicitValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as T;

            return Equals(other);
        }

        public abstract IEnumerable<object> GetEqualityFieldValues();

        public override int GetHashCode()
        {
            var startValue = 17;
            var multiplier = 59;

            return GetEqualityFieldValues()
                .Where(value => value != null)
                .Aggregate(
                    startValue,
                    (current, value) => current * multiplier + value.GetHashCode());
        }

        public virtual bool Equals(T other)
        {
            if (other == null)
                return false;

            var t = GetType();
            var otherType = other.GetType();

            if (t != otherType)
                return false;

            var fieldsValues = GetEqualityFieldValues().ToArray();
            var fieldsValuesOfOther = other.GetEqualityFieldValues().ToArray();
            for (int i = 0; i < fieldsValues.Length; i++)
            {
                var value1 = fieldsValuesOfOther[i];
                var value2 = fieldsValues[i];

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }
            

            return true;
        }

       

        public static bool operator ==(ExplicitValueObject<T> x, ExplicitValueObject<T> y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }

            return x.Equals(y);
        }

        public static bool operator !=(ExplicitValueObject<T> x, ExplicitValueObject<T> y)
        {
            return !(x == y);
        }
    }
}