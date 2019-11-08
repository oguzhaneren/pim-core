using System.Collections.Generic;

namespace ProductContext.Application
{
    public class CreateProductCommand
    {
        public int CategoryId { get; set; }
        public long ProductId { get; set; }
        public int BrandId { get; set; }
        public int BusinessUnitId { get; set; }
        
        public List<Attribute> Attributes { get; set; }

        public CreateProductCommand()
        {
            Attributes = new List<Attribute>();
        }    

        public class Attribute
        {
            public int Id { get; set; }
            public int? ValueId { get; set; }
            public string CustomValue { get; set; }
        }
    }
}