using ProductContext.Domain.Abstract;

namespace ProductContext.Domain.Lookup
{
    public class Brand
        : AggregateRootEntity<int>
    {
        public string Name { get; private set; }

        public Brand(int id,string name)
        {
            Id = id;
            Name = name;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}