using Project.Models.Entities;

namespace Project.Models.Interfaces
{

    public interface IOrderRepository 
    {
        Order GetById(int id);
        List<Order> GetAll();
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }

}
