namespace Project.Models.Interfaces
{
    public interface IReturnRepository
    {
        Return GetById(int id);
        List<Return> GetAll();
        void Add(Return returnEntity);
        void Update(Return returnEntity);
        void Delete(int id);
    }

}
