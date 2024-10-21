using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Proyecto_final_Rivera_Paz_Diego_Eduardo.Models;

namespace Proyecto_final_Rivera_Paz_Diego_Eduardo.Controllers
{
    public class UsersController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TiendaDB"].ConnectionString;

        public ActionResult ListaUsers()
        {
            List<Users> users = new List<Users>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarTodoUsers", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new Users
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString()
                    });
                }
            }
            return View(users);
        }
        public ActionResult ConsultarTodoUser()
        {
            return ListaUsers();
        }

        public ActionResult ConsultarPorIDUsers(int id)
        {
            Users user = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarPorIdUsers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new Users
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString()
                    };
                }
            }
            return View(user);
        }

    
        public ActionResult AgregarUser()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult AgregarUser(Users user)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("InsertarUser", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", user.Password);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("ListaUsers");
            }
            return View(user);
        }

     
        public ActionResult EditarUsuario(int id)
        {
            Users user = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarPorIdUsers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new Users
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString()
                    };
                }
            }
            return View(user);
        }

        
        [HttpPost]
        public ActionResult EditarUsuario(Users user)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("EditarUser", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", user.Id);
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", user.Password);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("ListaUsers");
            }
            return View(user);
        }

  
        public ActionResult EliminarUsuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("EliminarUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            return RedirectToAction("ListaUsers");
        }
    }
}
