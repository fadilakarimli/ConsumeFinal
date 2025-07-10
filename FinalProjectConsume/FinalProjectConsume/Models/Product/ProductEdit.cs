namespace FinalProjectConsume.Models.Product
{
    public class ProductEdit
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
    }
}
