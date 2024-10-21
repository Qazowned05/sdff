using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_final_Rivera_Paz_Diego_Eduardo.Models
{
    public class Persona
    {
        public int IdPersona { get; set; }  
        public string TipoPersona { get; set; }  
        public string Nombre { get; set; } 
        public string TipoDocumento { get; set; }  
        public string NumDocumento { get; set; } 
        public string Direccion { get; set; } 
        public string Telefono { get; set; }  
        public string Email { get; set; } 
    }
}