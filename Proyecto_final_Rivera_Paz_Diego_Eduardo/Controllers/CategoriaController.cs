using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Proyecto_final_Rivera_Paz_Diego_Eduardo.Models;

namespace Proyecto_final_Rivera_Paz_Diego_Eduardo.Controllers
{
    public class CategoriaController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TiendaDB"].ConnectionString;

        public ActionResult ListaCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarTodoCategoria", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    categorias.Add(new Categoria
                    {
                        IdCategoria = (int)reader["idcategoria"],
                        Nombre = reader["nombre"].ToString(),
                        Descripcion = reader["descripcion"].ToString(),
                        Condicion = (bool)reader["condicion"]
                    });
                }
            }
            return View(categorias);
        }

        public ActionResult ConsultarTodoCategoria()
        {
            return ListaCategorias();
        }

        public ActionResult ConsultarPorIDCategoria(int id)
        {
            Categoria categoria = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarPorIdCategoria", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idcategoria", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    categoria = new Categoria
                    {
                        IdCategoria = (int)reader["idcategoria"],
                        Nombre = reader["nombre"].ToString(),
                        Descripcion = reader["descripcion"].ToString(),
                        Condicion = (bool)reader["condicion"]
                    };
                }
            }
            return View(categoria);
        }

        public ActionResult AgregarCategoria()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarCategoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("InsertarCategoria", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@nombre", categoria.Nombre);
                    command.Parameters.AddWithValue("@descripcion", categoria.Descripcion);
                    command.Parameters.AddWithValue("@condicion", categoria.Condicion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("ListaCategorias");
            }
            return View(categoria);
        }

        public ActionResult EditarCategoria(int id)
        {
            Categoria categoria = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarPorIdCategoria", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idcategoria", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    categoria = new Categoria
                    {
                        IdCategoria = (int)reader["idcategoria"],
                        Nombre = reader["nombre"].ToString(),
                        Descripcion = reader["descripcion"].ToString(),
                        Condicion = (bool)reader["condicion"]
                    };
                }
            }
            return View(categoria);
        }

        [HttpPost]
        public ActionResult EditarCategoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("EditarCategoria", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@idcategoria", categoria.IdCategoria);
                    command.Parameters.AddWithValue("@nombre", categoria.Nombre);
                    command.Parameters.AddWithValue("@descripcion", categoria.Descripcion);
                    command.Parameters.AddWithValue("@condicion", categoria.Condicion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("ListaCategorias");
            }
            return View(categoria);
        }

        public ActionResult EliminarCategoria(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("EliminarCategoria", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idcategoria", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            return RedirectToAction("ListaCategorias");
        }
    }
}
