using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Mvc;
using Proyecto_final_Rivera_Paz_Diego_Eduardo.Models;

namespace Proyecto_final_Rivera_Paz_Diego_Eduardo.Controllers
{
    public class ArticuloController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TiendaDB"].ConnectionString;

        public ActionResult ListaArticulos(string txtNombre = "", int? idCategoria = null)
        {
            Debug.WriteLine("Param nombre: " + txtNombre);
            List<Articulo> articulos = new List<Articulo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("BuscarArticulos", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@nombreArticulo", txtNombre);
                command.Parameters.AddWithValue("@idcategoria", idCategoria);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    articulos.Add(new Articulo
                    {
                        IdArticulo = (int)reader["idarticulo"],
                        IdCategoria = (int)reader["idcategoria"],
                        Codigo = reader["codigo"].ToString(),
                        Nombre = reader["nombre_articulo"].ToString(),
                        Stock = (int)reader["stock"],
                        Descripcion = reader["descripcion"].ToString(),
                        Imagen = reader["imagen"].ToString(),
                        Estado = reader["estado"].ToString()
                    });
                }
            }
            ViewBag.Categorias = ObtenerCategorias();
            return View(articulos);
        }

        public ActionResult ConsultarTodoArticulo()
        {
            return ListaArticulos();
        }

        public ActionResult AgregarArticulo()
        {
            ViewBag.Categorias = ObtenerCategorias();
            return View();
        }

        [HttpPost]
        public ActionResult AgregarArticulo(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("InsertarArticulo", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@idcategoria", articulo.IdCategoria);
                    command.Parameters.AddWithValue("@codigo", articulo.Codigo);
                    command.Parameters.AddWithValue("@nombre", articulo.Nombre);
                    command.Parameters.AddWithValue("@stock", articulo.Stock);
                    command.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
                    command.Parameters.AddWithValue("@imagen", articulo.Imagen);
                    command.Parameters.AddWithValue("@estado", articulo.Estado);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("ListaArticulos");
            }

            ViewBag.Categorias = ObtenerCategorias();
            return View(articulo);
        }

        private List<Categoria> ObtenerCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT IdCategoria, Nombre FROM Categoria", connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(new Categoria
                        {
                            IdCategoria = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }
            return categorias;
        }

        public ActionResult EditarArticulo(int id)
        {
            Articulo articulo = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarPorIdArticulo", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idarticulo", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    articulo = new Articulo
                    {
                        IdArticulo = (int)reader["idarticulo"],
                        IdCategoria = (int)reader["idcategoria"],
                        Codigo = reader["codigo"].ToString(),
                        Nombre = reader["nombre"].ToString(),
                        Stock = (int)reader["stock"],
                        Descripcion = reader["descripcion"].ToString(),
                        Imagen = reader["imagen"].ToString(),
                        Estado = reader["estado"].ToString()
                    };
                }
            }

            ViewBag.Categorias = ObtenerCategorias();
            return View(articulo);
        }

        [HttpPost]
        public ActionResult EditarArticulo(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("EditarArticulo", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@idarticulo", articulo.IdArticulo);
                    command.Parameters.AddWithValue("@idcategoria", articulo.IdCategoria);
                    command.Parameters.AddWithValue("@codigo", articulo.Codigo);
                    command.Parameters.AddWithValue("@nombre", articulo.Nombre);
                    command.Parameters.AddWithValue("@stock", articulo.Stock);
                    command.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
                    command.Parameters.AddWithValue("@imagen", articulo.Imagen);
                    command.Parameters.AddWithValue("@estado", articulo.Estado);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("ListaArticulos");
            }

            ViewBag.Categorias = ObtenerCategorias();
            return View(articulo);
        }

        public ActionResult EliminarArticulo(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("EliminarArticulo", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idarticulo", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            return RedirectToAction("ListaArticulos");
        }
    }
}
