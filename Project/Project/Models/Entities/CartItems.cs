namespace Project.Models.Entities
{
    public class CartItems
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? UserId { get; set; }
        public Product? Product { get; set; }
    }
}
