using System;
using System.Collections.Generic;
using Project.Models.Entities;
using Project.Models.Interfaces;

public class UserRepositoryDecorator : IUserRepository
{
    private readonly IUserRepository _userRepository;

    public UserRepositoryDecorator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetById(int id)
    {
        Console.WriteLine($"Getting User by Id: {id}");
        return _userRepository.GetById(id);
    }
    public User GetByGuid(string userIdGuid)
    {
        Console.WriteLine($"Getting User by Id: {userIdGuid}");
        return _userRepository.GetByGuid(userIdGuid);
    }
    public List<User> GetAll()
    {
        Console.WriteLine("Getting all Users");
        return _userRepository.GetAll();
    }

    public void Add(User user)
    {
        Console.WriteLine($"Adding User: {user.Username}");
        _userRepository.Add(user);
    }

    public void Update(User user)
    {
        Console.WriteLine($"Updating User: {user.Username}");
        _userRepository.Update(user);
    }

    public void Delete(int id)
    {
        Console.WriteLine($"Deleting User with Id: {id}");
        _userRepository.Delete(id);
    }
}
