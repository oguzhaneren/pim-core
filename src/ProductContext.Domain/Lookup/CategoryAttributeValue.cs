using System.Collections.Generic;
using ProductContext.Domain.Abstract;

namespace ProductContext.Domain.Lookup
{
    public class CategoryAttributeValue
        : ExplicitValueObject<CategoryAttributeValue>
    {
        public int Id { get; }
        public string Name { get; }
        public bool IsSearchable { get; }
        public int SortSequence { get; }

        internal CategoryAttributeValue(int id, string name, bool isSearchable, int sortSequence)
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