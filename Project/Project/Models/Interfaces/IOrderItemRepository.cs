namespace Project.Models.Interfaces
{
    public interface IOrderItemRepository
    {
        OrderItem GetById(int id);
        List<OrderItem> GetAll();
        void Add(OrderItem orderItem);
        void Update(OrderItem orderItem);
        void Delete(int id);
    }

}
