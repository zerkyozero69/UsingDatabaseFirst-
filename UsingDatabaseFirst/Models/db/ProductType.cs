using System;
using System.Collections.Generic;

namespace UsingDatabaseFirst.Models.db
{
    public partial class ProductType
    {
        public ProductType()
        {
            Product = new HashSet<Product>();
        }

        public string ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
