using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ProductContext.Domain.Abstract
{
    [Serializable]
    [DebuggerDisplay("{Name} - {Value}")]
    public abstract class Enumeration<TEnumeration> : Enumeration<TEnumeration, int>
        where TEnumeration : Enumeration<TEnumeration>
    {
        protected Enumeration(int value, string name)
            : base(value, name)
        {
        }

        public static TEnumeration FromValueOrThrow(int? value)
        {
            if (!value.HasValue)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return FromValue(value.Value);
        }

        public static TEnumeration FromInt32(int value)
        {
            return FromValue(value);
        }

        public static bool TryFromInt32(int listItemValue, out TEnumeration result)
        {
            return TryParse(listItemValue, out result);
        }

        public static implicit operator int?(Enumeration<TEnumeration> obj)
        {
            return obj?.Value;
        }

        public static implicit operator int(Enumeration<TEnumeration> obj)
        {
            return obj.Value;
        }
    }

    public interface IEnumeration<out TValue>
    {
        TValue Value { get; }
        string Name { get; }
    }


    [Serializable]
    [DebuggerDisplay("{Name} - {Value}")]
    [DataContract]
    public abstract class Enumeration<TEnumeration, TValue> : IComparable<TEnumeration>, IEquatable<TEnumeration>, IEnumeration<TValue>
        where TEnumeration : Enumeration<TEnumeration, TValue>
        where TValue : IComparable
    {
        readonly string _name;
        readonly TValue _value;

        private static readonly Lazy<TEnumeration[]> Enumerations = new Lazy<TEnumeration[]>(GetEnumerations);

        protected Enumeration(TValue value, string name)
        {
            _value = value;
            _name = name;
        }

        [DataMember(Order = 1)]
        public TValue Value
        {
            get { return _value; }
        }

        [DataMember(Order = 0)] public string Name => _name;

        public int CompareTo(TEnumeration other)
        {
            return Value.CompareTo(other.Value);
        }

        public sealed override string ToString()
        {
            return Name;
        }

        public static TEnumeration[] GetAll()
        {
            return Enumerations.Value;
        }

        private static TEnumeration[] GetEnumerations()
        {
            Type enumerationType = typeof(TEnumeration);
            return enumerationType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
                .Select(info => info.GetValue(null))
                .Cast<TEnumeration>()
                .ToArray();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TEnumeration);
        }

        public bool Equals(TEnumeration other)
        {
            return other != null && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
        {
            return !Equals(left, right);
        }

        public static TEnumeration FromValue(TValue value)
        {
            return Parse(value, "value", item => item.Value.Equals(value));
        }

        public static TEnumeration Parse(string name)
        {
            return Parse(name, "name", item => item.Name == name);
        }

        static bool TryParse(Func<TEnumeration, bool> predicate, out TEnumeration result)
        {
            result = GetAll().FirstOrDefault(predicate);
            return result != null;
        }

        private static TEnumeration Parse(object value, string description, Func<TEnumeration, bool> predicate)
        {
            TEnumeration result;

            if (!TryParse(predicate, out result))
            {
                string message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(TEnumeration));
                throw new ArgumentException(message, "value");
            }

            return result;
        }

        public static bool TryParse(TValue value, out TEnumeration result)
        {
            return TryParse(e => e.Value.Equals(value), out result);
        }

        public static bool TryParse(string name, out TEnumeration result)
        {
            return TryParse(e => e.Name == name, out result);
        }
    }
}