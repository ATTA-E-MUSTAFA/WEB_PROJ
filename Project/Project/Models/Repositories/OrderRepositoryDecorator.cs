using System;
using System.Collections.Generic;
using Project.Models.Entities;
using Project.Models.Interfaces;

public class OrderRepositoryDecorator : IOrderRepository
{
    private readonly IOrderRepository _orderRepository;

    public OrderRepositoryDecorator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Order GetById(int id)
    {
        Console.WriteLine($"Getting Order by Id: {id}");
        return _orderRepository.GetById(id);
    }

    public List<Order> GetAll()
    {
        Console.WriteLine("Getting all Orders");
        return _orderRepository.GetAll();
    }

    public void Add(Order order)
    {
        Console.WriteLine($"Adding Order with Id: {order.Id}");
        _orderRepository.Add(order);
    }

    public void Update(Order order)
    {
        Console.WriteLine($"Updating Order with Id: {order.Id}");
        _orderRepository.Update(order);
    }

    public void Delete(int id)
    {
        Console.WriteLine($"Deleting Order with Id: {id}");
        _orderRepository.Delete(id);
    }
}
