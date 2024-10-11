using System;
using System.Collections.Generic;
using Project.Models.Entities;
using Project.Models.Interfaces;

public class AdminRepositoryDecorator : IAdminRepository
{
    private readonly IAdminRepository _adminRepository;

    public AdminRepositoryDecorator(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public Admin GetById(int id)
    {
        Console.WriteLine($"Getting Admin by Id: {id}");
        return _adminRepository.GetById(id);
    }

    public List<Admin> GetAll()
    {
        Console.WriteLine("Getting all Admins");
        return _adminRepository.GetAll();
    }

    public void Add(Admin admin)
    {
        Console.WriteLine($"Adding Admin: {admin.Username}");
        _adminRepository.Add(admin);
    }

    public void Update(Admin admin)
    {
        Console.WriteLine($"Updating Admin: {admin.Username}");
        _adminRepository.Update(admin);
    }
}
