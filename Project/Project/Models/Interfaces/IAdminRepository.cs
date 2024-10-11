using Project.Models.Entities;

namespace Project.Models.Interfaces
{
    public interface IAdminRepository 
    {
        Admin GetById(int id);
        List<Admin> GetAll();
        void Update(Admin user);
        void Add(Admin user);
    }
}
