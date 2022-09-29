using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsingDatabaseFirst.ViewModels
{
    public class ProductViewModel
    { 
        public string ProductName { get; set; }
        public double? ProductPrice { get; set; }
        public int? CategoryId { get; set; }
        public string SupplierId { get; set; }
        public bool ProductStatus { get; set; }
    }
}
