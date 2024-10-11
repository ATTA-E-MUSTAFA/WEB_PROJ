using Project.Models.Entities;
namespace Project.Models.Interfaces
{
    
    

    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetById(int id);
        void Add(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }

}
