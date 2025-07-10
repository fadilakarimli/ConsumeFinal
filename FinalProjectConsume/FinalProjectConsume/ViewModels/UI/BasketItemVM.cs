namespace FinalProjectConsume.ViewModels.UI
{
    public class BasketItemVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
