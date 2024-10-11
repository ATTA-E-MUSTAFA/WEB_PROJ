using Project.Models.Entities;

namespace Project.Models.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        public User GetByGuid(string userIdGuid);
        List<User> GetAll(); 
        void Update(User user);
        void Delete(int id);
        void Add(User user);
    }
}
