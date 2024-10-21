using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_final_Rivera_Paz_Diego_Eduardo.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
    }
}