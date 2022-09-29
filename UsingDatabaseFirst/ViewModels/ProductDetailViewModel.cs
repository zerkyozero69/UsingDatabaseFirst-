namespace UsingDatabaseFirst.ViewModels
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double? ProductPrice { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
    }
}