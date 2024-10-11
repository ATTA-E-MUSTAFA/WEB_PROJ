namespace Project.Models.Repositories
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using Project.Models.Entities;
    using Project.Models.Interfaces;

    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly string _connectionString;

        public OrderItemRepository()
        {

            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=myDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        public OrderItem GetById(int id)
        {
            OrderItem orderItem = null;
            string query = "SELECT * FROM OrderItems WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    orderItem = new OrderItem
                    {
                        Id = (int)reader["Id"],
                        OrderId = (int)reader["OrderId"],
                        ProductId = (int)reader["ProductId"],
                        Quantity = (int)reader["Quantity"],
                        Price = (decimal)reader["Price"]
                    };
                }
            }

            return orderItem;
        }

        public List<OrderItem> GetByOrderId(int orderId)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            string query = "SELECT * FROM OrderItems WHERE OrderId = @OrderId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderId", orderId);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orderItems.Add(new OrderItem
                    {
                        Id = (int)reader["Id"],
                        OrderId = (int)reader["OrderId"],
                        ProductId = (int)reader["ProductId"],
                        Quantity = (int)reader["Quantity"],
                        Price = (decimal)reader["Price"]
                    });
                }
            }

            return orderItems;
        }

        public List<OrderItem> GetAll()
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            string query = "SELECT * FROM OrderItems";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orderItems.Add(new OrderItem
                    {
                        Id = (int)reader["Id"],
                        OrderId = (int)reader["OrderId"],
                        ProductId = (int)reader["ProductId"],
                        Quantity = (int)reader["Quantity"],
                        Price = (decimal)reader["Price"]
                    });
                }
            }

            return orderItems;
        }

        public void Add(OrderItem orderItem)
        {
            string query = "INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price) " +
                           "VALUES (@OrderId, @ProductId, @Quantity, @Price)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderId", orderItem.OrderId);
                command.Parameters.AddWithValue("@ProductId", orderItem.ProductId);
                command.Parameters.AddWithValue("@Quantity", orderItem.Quantity);
                command.Parameters.AddWithValue("@Price", orderItem.Price);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(OrderItem orderItem)
        {
            string query = "UPDATE OrderItems SET OrderId = @OrderId, ProductId = @ProductId, " +
                           "Quantity = @Quantity, Price = @Price WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", orderItem.Id);
                command.Parameters.AddWithValue("@OrderId", orderItem.OrderId);
                command.Parameters.AddWithValue("@ProductId", orderItem.ProductId);
                command.Parameters.AddWithValue("@Quantity", orderItem.Quantity);
                command.Parameters.AddWithValue("@Price", orderItem.Price);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM OrderItems WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
