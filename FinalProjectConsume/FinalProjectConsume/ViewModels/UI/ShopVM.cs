using FinalProjectConsume.Models.Product;

namespace FinalProjectConsume.ViewModels.UI
{
    public class ShopVM
    {
        public IEnumerable<Product> Products { get; set; }
        public string? SearchTerm { get; set; }
    }
}
