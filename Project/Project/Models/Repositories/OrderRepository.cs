using Project.Models.Entities;
using Project.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=myDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    public Order GetById(int id)
    {
        Order order = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT Id, OrderDate, UserId FROM Orders WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                order = new Order
                {
                    Id = (int)reader["Id"],
                    OrderDate = (DateTime)reader["OrderDate"],
                    UserId = reader["UserId"].ToString()
                };
            }
        }
        return order;
    }

    public List<Order> GetAll()
    {
        List<Order> orders = new List<Order>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT Id, OrderDate, UserId FROM Orders";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new Order
                {
                    Id = (int)reader["Id"],
                    OrderDate = (DateTime)reader["OrderDate"],
                    UserId = reader["UserId"].ToString()
                });
            }
        }
        return orders;
    }

    public void Add(Order order)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Orders (OrderDate, UserId) VALUES (@OrderDate, @UserId)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            command.Parameters.AddWithValue("@UserId", order.UserId);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Update(Order order)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Orders SET OrderDate = @OrderDate, UserId = @UserId WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", order.Id);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            command.Parameters.AddWithValue("@UserId", order.UserId);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM Orders WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
