using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using Project.Models.Repositories;
using System;

namespace ProjectWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        // Constructor injection of IAdminRepository
        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var users = _adminRepository.GetAll();
            Admin user = null;

            foreach (var u in users)
            {
                if (u.Username == username && u.Password == password)
                {
                    user = u;
                    break;
                }
            }

            if (user == null)
            {
                Console.WriteLine("empty");
            }

            if (user != null)
            {
                // Set cookie
                Response.Cookies.Append("username", username, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    HttpOnly = true,
                    Secure = true
                });
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("username");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            // Check if user is logged in
            var isLoggedIn = Request.Cookies["username"] != null;
            ViewData["IsLoggedIn"] = isLoggedIn;
            ViewData["Username"] = isLoggedIn ? Request.Cookies["username"] : string.Empty;
            return View();
        }
    }
}


//using Microsoft.Data.SqlClient;
//using System.Data.SqlTypes;

//public class GenericRepository<TEntity> where TEntity : class
//{
//    protected readonly string _connectionString;
//    public GenericRepository(string connectionString)
//    {
//        _connectionString = connectionString;
//    }
//    public bool Add(TEntity entity)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();
//            var tableName = typeof(TEntity).Name;
//            var properties = typeof(TEntity).GetProperties();
//            var columnNames = string.Join(",", properties.Select(p => p.Name));
//            var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));
//            var query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames});";
//            using (var command = new SqlCommand(query, connection))
//            {
//                foreach (var property in properties)
//                {
//                    var value = property.GetValue(entity);
//                    // DateTime validation for SQL Server range
//                    if (value is DateTime dateTimeValue)
//                    {
//                        if (dateTimeValue < SqlDateTime.MinValue.Value || dateTimeValue >
//                        SqlDateTime.MaxValue.Value)
// }
//                    {
//                    }
//                    Console.WriteLine("DATE OVERFLOW GENERIC REPO");
//                    command.Parameters.AddWithValue("@" + property.Name, value ??
//                    DBNull.Value);
//                }
//            }
//        }
//        return command.ExecuteNonQuery() > 0;
//    }
//    public TEntity GetById(string id)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();
//            var tableName = typeof(TEntity).Name;
//            var primaryKey = "Id";
//            var query = $"SELECT * FROM {tableName} WHERE {primaryKey} = @Id;";
//            using (var command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@Id", id);
//                var reader = command.ExecuteReader();
//                if (reader.Read())
//                {
//                    return MapReaderToObject(reader);
//                }
//                return null;
//            }
//        }
//    }
//    public IEnumerable<TEntity> GetAll()
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();
//            var tableName = typeof(TEntity).Name;
//            var query = $"SELECT * FROM {tableName};";
//            using (var command = new SqlCommand(query, connection))
//            {
//                var reader = command.ExecuteReader();
//                var entities = new List<TEntity>();
//                while (reader.Read())
//                {
//                    entities.Add(MapReaderToObject(reader));
//                }
//                return entities;
//            }
//        }
//    }
//    public bool Update(TEntity entity)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();
//            var tableName = typeof(TEntity).Name;
//            var primaryKey = "Id";
//            var properties = typeof(TEntity).GetProperties().Where(p => p.Name != primaryKey);
//            var setClause = string.Join(",", properties.Select(p => $"{p.Name} = @{p.Name}"));
//            var query = $"UPDATE {tableName} SET {setClause} WHERE {primaryKey} =
// @{ primaryKey}; ";
//        using (var command = new SqlCommand(query, connection))
//            {
//                foreach (var property in properties)
//                {
//                    command.Parameters.AddWithValue("@" + property.Name,
//                    property.GetValue(entity));
//                }
//                command.Parameters.AddWithValue("@" + primaryKey,
//                typeof(TEntity).GetProperty(primaryKey).GetValue(entity));
//                return command.ExecuteNonQuery() > 0;
//            }
//        }
//    }
//    public bool Delete(string id)
//    {
//        using (var connection = new SqlConnection(_connectionString))
//        {
//            connection.Open();
//            var tableName = typeof(TEntity).Name;
//            var primaryKey = "Id";
//            var query = $"DELETE FROM {tableName} WHERE {primaryKey} = @Id;";
//            using (var command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@Id", id);
//                return command.ExecuteNonQuery() > 0;
//            }
//        }
//    }
//    private TEntity MapReaderToObject(SqlDataReader reader)
//    {
//        var entity = Activator.CreateInstance<TEntity>();
//        foreach (var property in typeof(TEntity).GetProperties())
//        {
//            if (property.Name != "Id")
//            {
//                property.SetValue(entity, reader[property.Name]);
//            }
//        }
//        return entity;
//    }