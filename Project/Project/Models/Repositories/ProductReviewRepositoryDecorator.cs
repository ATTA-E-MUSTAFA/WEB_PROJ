using System;
using System.Collections.Generic;
using Project.Models.Entities;
using Project.Models.Interfaces;

namespace Project.Models.Repositories
{
    public class ProductReviewRepositoryDecorator : IProductReviewRepository
    {
        private readonly IProductReviewRepository _decoratedRepository;

        public ProductReviewRepositoryDecorator(IProductReviewRepository decoratedRepository)
        {
            _decoratedRepository = decoratedRepository;
        }

        public ProductReview GetById(int id)
        {
            Console.WriteLine($"Fetching ProductReview with Id = {id}");
            return _decoratedRepository.GetById(id);
        }


        public List<ProductReview> GetByProductId(int id)
        {
            Console.WriteLine("Fetching specific ProductReviews");
            return _decoratedRepository.GetByProductId(id);
        }
        public List<ProductReview> GetAll()
        {
            Console.WriteLine("Fetching all ProductReviews");
            return _decoratedRepository.GetAll();
        }

        public void Add(ProductReview productReview)
        {
            _decoratedRepository.Add(productReview);
        }

        public void Update(ProductReview productReview)
        {
            Console.WriteLine($"Updating ProductReview with Id = {productReview.Id}");
            _decoratedRepository.Update(productReview);
        }

        public void Delete(int id)
        {
            Console.WriteLine($"Deleting ProductReview with Id = {id}");
            _decoratedRepository.Delete(id);
        }
    }
}
