using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Project.Models.Entities;
using Project.Models.Interfaces;

namespace Project.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository()
        {

            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=myDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        }

        public Product GetById(int id)
        {
            Product product = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            ImageUrl = reader["ImageUrl"].ToString(),
                            CategoryId = (int)reader["CategoryId"]
                        };
                    }
                }
            }

            return product;
        }

        public void Add(Product product)
        {
         
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    Console.WriteLine("Entered");
                    var command = new SqlCommand("INSERT INTO Products (Name, Description, Price, ImageUrl, CategoryId) VALUES (@Name, @Description, @Price, @ImageUrl, @CategoryId)", connection);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@ImageUrl", product.ImageUrl ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    Console.WriteLine(product.ImageUrl);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Log exception details or throw to be handled further up
                throw new Exception("There was an error adding the product to the database.", ex);
            }
        }

        public void Update(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, ImageUrl = @ImageUrl, CategoryId = @CategoryId WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@ImageUrl", product.ImageUrl ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Products WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Products", connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            ImageUrl = reader["ImageUrl"].ToString(),
                            CategoryId = (int)reader["CategoryId"]
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }
    }

}
