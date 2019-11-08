using System.Collections.Generic;
using System.Linq;
using ProductContext.Domain.Abstract;

namespace ProductContext.Domain.Lookup
{
    public class Attribute
        : AggregateRootEntity<int>
    {
        private readonly ISet<AttributeValue> _values = new HashSet<AttributeValue>();
        public string Name { get; }
        public string Type { get; }
        public int TypeId { get; }
        public int SortSequence { get; }

        public IEnumerable<AttributeValue> Values
        {
            get => _values;
        }

        public Attribute(int id, string name, string type, int typeId, int sortSequence)
        {
            Id = id;
            Name = name;
            Type = type;
            TypeId = typeId;
            SortSequence = sortSequence;
        }

        public void AddValue(AttributeValue value)
        {
            _values.Add(value);
        }

        public void RemoveValue(AttributeValue value)
        {
            _values.Remove(value);
        }

        public AttributeValue GetValue(int attributeValueId)
        {
            return _values.FirstOrDefault(x => x.Id == attributeValueId) ?? throw new DomainException($"{attributeValueId} not found in attribute value list!");
        }
        
        public CategoryAttribute ToCategoryAttribute()
        {
            return new CategoryAttribute(Id, Name, true, true, true, true, true, true, 1);
        }

       
    }
}