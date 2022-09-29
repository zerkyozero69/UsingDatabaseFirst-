using System;
using System.Collections.Generic;

namespace UsingDatabaseFirst.Models.db
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double? ProductCost { get; set; }
        public double? ProductPrice { get; set; }
        public int? CategoryId { get; set; }
        public string ProductTypeId { get; set; }
        public string SupplierId { get; set; }
        public string SerialNumber { get; set; }
        public string UnitId { get; set; }
        public double? ProductInStock { get; set; }
        public double? ProductInStockWithUnit { get; set; }
        public double? ProductInOrder { get; set; }
        public double? ProductInOrderWithUnit { get; set; }
        public bool ProductStatus { get; set; }

        public virtual Category Category { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
