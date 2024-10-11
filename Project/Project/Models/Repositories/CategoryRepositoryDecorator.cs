using System;
using System.Collections.Generic;
using Project.Models.Entities;
using Project.Models.Interfaces;

public class CategoryRepositoryDecorator : ICategoryRepository
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryRepositoryDecorator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public List<Category> GetAllCategories()
    {
        Console.WriteLine("Getting all Categories");
        return _categoryRepository.GetAllCategories();
    }

    public Category GetById(int id)
    {
        Console.WriteLine($"Getting Category by Id: {id}");
        return _categoryRepository.GetById(id);
    }

    public void Add(Category category)
    {
        Console.WriteLine($"Adding Category: {category.Name}");
        _categoryRepository.Add(category);
    }

    public void UpdateCategory(Category category)
    {
        Console.WriteLine($"Updating Category: {category.Name}");
        _categoryRepository.UpdateCategory(category);
    }

    public void DeleteCategory(int id)
    {
        Console.WriteLine($"Deleting Category with Id: {id}");
        _categoryRepository.DeleteCategory(id);
    }
}
