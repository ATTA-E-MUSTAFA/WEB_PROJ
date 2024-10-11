using Project.Models.Entities;

namespace Project.Models.Interfaces
{
    public interface IProductRepository 
    {
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        List<Product> GetAll();
        
    }
}

