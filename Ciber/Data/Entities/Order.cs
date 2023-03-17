namespace Ciber.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}
