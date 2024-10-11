using Microsoft.Data.SqlClient;
using Project.Models.Interfaces;

namespace Project.Models.Repositories
{
    public class ReturnRepository : IReturnRepository
    {
        private readonly string _connectionString;

        public ReturnRepository()
        {

            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=myDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        public Return GetById(int id)
        {
            Return returnEntity = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Returns WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    returnEntity = new Return
                    {
                        Id = (int)reader["Id"],
                        OrderItemId = (int)reader["OrderItemId"],
                        Reason = reader["Reason"].ToString(),
                        ReturnDate = (DateTime)reader["ReturnDate"],
                        Status = reader["Status"].ToString()
                    };
                }
            }
            return returnEntity;
        }

        public List<Return> GetAll()
        {
            List<Return> returns = new List<Return>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Returns", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    returns.Add(new Return
                    {
                        Id = (int)reader["Id"],
                        OrderItemId = (int)reader["OrderItemId"],
                        Reason = reader["Reason"].ToString(),
                        ReturnDate = (DateTime)reader["ReturnDate"],
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return returns;
        }

        public void Add(Return returnEntity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Returns (OrderItemId, Reason, ReturnDate, Status) VALUES (@OrderItemId, @Reason, @ReturnDate, @Status)", conn);
                cmd.Parameters.AddWithValue("@OrderItemId", returnEntity.OrderItemId);
                cmd.Parameters.AddWithValue("@Reason", returnEntity.Reason);
                cmd.Parameters.AddWithValue("@ReturnDate", returnEntity.ReturnDate);
                cmd.Parameters.AddWithValue("@Status", returnEntity.Status);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Return returnEntity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Returns SET OrderItemId = @OrderItemId, Reason = @Reason, ReturnDate = @ReturnDate, Status = @Status WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", returnEntity.Id);
                cmd.Parameters.AddWithValue("@OrderItemId", returnEntity.OrderItemId);
                cmd.Parameters.AddWithValue("@Reason", returnEntity.Reason);
                cmd.Parameters.AddWithValue("@ReturnDate", returnEntity.ReturnDate);
                cmd.Parameters.AddWithValue("@Status", returnEntity.Status);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Returns WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}
