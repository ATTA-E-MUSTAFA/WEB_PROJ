namespace Project.Models.Repositories
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using Project.Models.Entities;
    using Project.Models.Interfaces;

    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly string _connectionString;

        public ProductReviewRepository()
        {

            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=myDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        }

        public ProductReview GetById(int id)
        {
            ProductReview productReview = null;
            string query = "SELECT * FROM ProductReviews WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    productReview = new ProductReview
                    {
                        Id = (int)reader["Id"],
                        ProductId = (int)reader["ProductId"],
                        UserId = (int)reader["UserId"],
                        Comment = reader["Comment"].ToString(),  // Changed from ReviewText to Comment
                        Rating = (int)reader["Rating"],
                        ReviewDate = (DateTime)reader["ReviewDate"]
                    };
                }
            }

            return productReview;
        }

        public List<ProductReview> GetByProductId(int productId)
        {
            List<ProductReview> productReviews = new List<ProductReview>();
            string query = "SELECT * FROM ProductReviews WHERE ProductId = @ProductId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", productId);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    productReviews.Add(new ProductReview
                    {
                        Id = (int)reader["Id"],
                        ProductId = (int)reader["ProductId"],
                        UserId = (int)reader["UserId"],
                        Comment = reader["Comment"].ToString(),  // Changed from ReviewText to Comment
                        Rating = (int)reader["Rating"],
                        ReviewDate = (DateTime)reader["ReviewDate"]
                    });
                }
            }

            return productReviews;
        }

        public List<ProductReview> GetAll()
        {
            List<ProductReview> productReviews = new List<ProductReview>();
            string query = "SELECT * FROM ProductReviews";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    productReviews.Add(new ProductReview
                    {
                        Id = (int)reader["Id"],
                        ProductId = (int)reader["ProductId"],
                        UserId = (int)reader["UserId"],
                        Comment = reader["Comment"].ToString(),  // Changed from ReviewText to Comment
                        Rating = (int)reader["Rating"],
                        ReviewDate = (DateTime)reader["ReviewDate"]
                    });
                }
            }

            return productReviews;
        }

        public void Add(ProductReview productReview)
        {
            string query = "INSERT INTO ProductReviews (ProductId, UserId, Comment, Rating, ReviewDate) " +
                           "VALUES (@ProductId, @UserId, @Comment, @Rating, @ReviewDate)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", productReview.ProductId);
                command.Parameters.AddWithValue("@UserId", productReview.UserId);
                command.Parameters.AddWithValue("@Comment", productReview.Comment); // Changed from ReviewText to Comment
                command.Parameters.AddWithValue("@Rating", productReview.Rating);
                command.Parameters.AddWithValue("@ReviewDate", productReview.ReviewDate);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(ProductReview productReview)
        {
            string query = "UPDATE ProductReviews SET ProductId = @ProductId, UserId = @UserId, " +
                           "Comment = @Comment, Rating = @Rating, ReviewDate = @ReviewDate WHERE Id = @Id"; // Changed from ReviewText to Comment

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", productReview.Id);
                command.Parameters.AddWithValue("@ProductId", productReview.ProductId);
                command.Parameters.AddWithValue("@UserId", productReview.UserId);
                command.Parameters.AddWithValue("@Comment", productReview.Comment); // Changed from ReviewText to Comment
                command.Parameters.AddWithValue("@Rating", productReview.Rating);
                command.Parameters.AddWithValue("@ReviewDate", productReview.ReviewDate);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM ProductReviews WHERE Id = @Id";

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
