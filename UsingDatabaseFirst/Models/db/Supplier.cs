using System;
using System.Collections.Generic;

namespace UsingDatabaseFirst.Models.db
{
    public partial class Supplier
    {
        public Supplier()
        {
            Product = new HashSet<Product>();
        }

        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string Telephone { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
