using System;
using System.Collections.Generic;
using Project.Models.Entities;
using Project.Models.Interfaces;

namespace Project.Models.Repositories
{
    public class ReturnRepositoryDecorator : IReturnRepository
    {
        private readonly IReturnRepository _decoratedRepository;

        public ReturnRepositoryDecorator(IReturnRepository decoratedRepository)
        {
            _decoratedRepository = decoratedRepository;
        }

        public Return GetById(int id)
        {
            Console.WriteLine($"Fetching Return with Id = {id}");
            return _decoratedRepository.GetById(id);
        }

        public List<Return> GetAll()
        {
            Console.WriteLine("Fetching all Returns");
            return _decoratedRepository.GetAll();
        }

        public void Add(Return returnEntity)
        {
            Console.WriteLine($"Adding Return: OrderItemId = {returnEntity.OrderItemId}, Reason = {returnEntity.Reason}");
            _decoratedRepository.Add(returnEntity);
        }

        public void Update(Return returnEntity)
        {
            Console.WriteLine($"Updating Return with Id = {returnEntity.Id}");
            _decoratedRepository.Update(returnEntity);
        }

        public void Delete(int id)
        {
            Console.WriteLine($"Deleting Return with Id = {id}");
            _decoratedRepository.Delete(id);
        }
    }
}
