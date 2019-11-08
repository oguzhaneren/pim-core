using System.Collections.Generic;
using ProductContext.Domain.Abstract;

namespace ProductContext.Domain.Lookup
{
    public class AttributeValue
        : ExplicitValueObject<AttributeValue>
    {
        public int Id { get; }
        public string Name { get; }
        public bool IsSearchable { get; }
        public int SortSequence { get; }

        public AttributeValue(int id, string name, bool isSearchable, int sortSequence)
        {
            Id = id;
            Name = name;
            IsSearchable = isSearchable;
            SortSequence = sortSequence;
        }

        public override IEnumerable<object> GetEqualityFieldValues()
        {
            yield return Id;
        }
    }
}