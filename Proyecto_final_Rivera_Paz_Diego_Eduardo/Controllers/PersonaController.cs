using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Proyecto_final_Rivera_Paz_Diego_Eduardo.Models;

namespace Proyecto_final_Rivera_Paz_Diego_Eduardo.Controllers
{
    public class PersonaController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TiendaDB"].ConnectionString;

        public ActionResult Index()
        {
            List<Persona> personas = new List<Persona>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarTodo", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    personas.Add(new Persona
                    {
                        IdPersona = (int)reader["idpersona"],
                        TipoPersona = reader["tipo_persona"].ToString(),
                        Nombre = reader["nombre"].ToString(),
                        TipoDocumento = reader["tipo_documento"].ToString(),
                        NumDocumento = reader["num_documento"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Email = reader["email"].ToString()
                    });
                }
            }
            return View(personas);
        }

        public ActionResult ConsultarTodo()
        {
            return Index();
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(Persona persona)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("Insertar", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@tipo_persona", persona.TipoPersona);
                    command.Parameters.AddWithValue("@nombre", persona.Nombre);
                    command.Parameters.AddWithValue("@tipo_documento", persona.TipoDocumento);
                    command.Parameters.AddWithValue("@num_documento", persona.NumDocumento);
                    command.Parameters.AddWithValue("@direccion", persona.Direccion);
                    command.Parameters.AddWithValue("@telefono", persona.Telefono);
                    command.Parameters.AddWithValue("@email", persona.Email);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        public ActionResult Eliminar(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("Eliminar", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idpersona", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult ConsultaInd(int id)
        {
            Persona persona = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarPorId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idpersona", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    persona = new Persona
                    {
                        IdPersona = (int)reader["idpersona"],
                        TipoPersona = reader["tipo_persona"].ToString(),
                        Nombre = reader["nombre"].ToString(),
                        TipoDocumento = reader["tipo_documento"].ToString(),
                        NumDocumento = reader["num_documento"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Email = reader["email"].ToString()
                    };
                }
            }
            return View(persona);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Persona persona = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarPorId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idpersona", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    persona = new Persona
                    {
                        IdPersona = (int)reader["idpersona"],
                        TipoPersona = reader["tipo_persona"].ToString(),
                        Nombre = reader["nombre"].ToString(),
                        TipoDocumento = reader["tipo_documento"].ToString(),
                        NumDocumento = reader["num_documento"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Email = reader["email"].ToString()
                    };
                }
            }
            return View(persona);
        }

        [HttpPost]
        public ActionResult Editar(Persona persona)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("Editar", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@idpersona", persona.IdPersona);
                    command.Parameters.AddWithValue("@tipo_persona", persona.TipoPersona);
                    command.Parameters.AddWithValue("@nombre", persona.Nombre);
                    command.Parameters.AddWithValue("@tipo_documento", persona.TipoDocumento);
                    command.Parameters.AddWithValue("@num_documento", persona.NumDocumento);
                    command.Parameters.AddWithValue("@direccion", persona.Direccion);
                    command.Parameters.AddWithValue("@telefono", persona.Telefono);
                    command.Parameters.AddWithValue("@email", persona.Email);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            return View(persona);
        }
    }
}
