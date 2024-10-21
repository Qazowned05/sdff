using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_final_Rivera_Paz_Diego_Eduardo.Models
{
    public class Articulo
    {
        public int IdArticulo { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int Stock { get; set; }

        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [StringLength(50)]
        public string Imagen { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }
    }
}