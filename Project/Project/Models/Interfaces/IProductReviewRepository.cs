namespace Project.Models.Interfaces
{
    using Project.Models.Entities;
    public interface IProductReviewRepository
    {
        ProductReview GetById(int id);
        List<ProductReview> GetAll();
        void Add(ProductReview productReview);
        void Update(ProductReview productReview);
        void Delete(int id);
        List<ProductReview> GetByProductId(int id);
    }

}
