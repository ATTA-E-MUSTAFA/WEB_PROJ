namespace Project.Models.Repositories
{
    using System;
    using System.Collections.Generic;
    using Project.Models.Entities;
    using Project.Models.Interfaces;

    public class OrderItemRepositoryDecorator : IOrderItemRepository
    {
        private readonly IOrderItemRepository _decoratedRepository;

        public OrderItemRepositoryDecorator(IOrderItemRepository decoratedRepository)
        {
            _decoratedRepository = decoratedRepository;
        }

        public OrderItem GetById(int id)
        {
            Console.WriteLine($"Fetching OrderItem with Id = {id}");
            return _decoratedRepository.GetById(id);
        }


        public List<OrderItem> GetAll()
        {
            Console.WriteLine("Fetching all OrderItems");
            return _decoratedRepository.GetAll();
        }

        public void Add(OrderItem orderItem)
        {
            Console.WriteLine($"Adding OrderItem: OrderId = {orderItem.OrderId}, ProductId = {orderItem.ProductId}");
            _decoratedRepository.Add(orderItem);
        }

        public void Update(OrderItem orderItem)
        {
            Console.WriteLine($"Updating OrderItem with Id = {orderItem.Id}");
            _decoratedRepository.Update(orderItem);
        }

        public void Delete(int id)
        {
            Console.WriteLine($"Deleting OrderItem with Id = {id}");
            _decoratedRepository.Delete(id);
        }
    }
}
