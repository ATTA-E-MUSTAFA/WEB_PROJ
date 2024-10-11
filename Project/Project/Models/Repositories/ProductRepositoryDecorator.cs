using System;
using System.Collections.Generic;
using Project.Models.Entities;
using Project.Models.Interfaces;

public class ProductRepositoryDecorator : IProductRepository
{
    private readonly IProductRepository _productRepository;

    public ProductRepositoryDecorator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Product GetById(int id)
    {
        Console.WriteLine($"Getting Product by Id: {id}");
        return _productRepository.GetById(id);
    }

    public void Add(Product product)
    {
        Console.WriteLine($"Adding Product: {product.Name}");
        _productRepository.Add(product);
    }

    public void Update(Product product)
    {
        Console.WriteLine($"Updating Product: {product.Name}");
        _productRepository.Update(product);
    }

    public void Delete(int id)
    {
        Console.WriteLine($"Deleting Product with Id: {id}");
        _productRepository.Delete(id);
    }

    public List<Product> GetAll()
    {
        Console.WriteLine("Getting all Products");
        return _productRepository.GetAll();
    }
}
