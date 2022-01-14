using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionWeb.Models
{
    public class InicioSesionModel
    {
        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        [Display(Name = "Recordarme?")]
        public bool Recordar { get; set; }

        public string ReturnUrl { get; set; }
    }
}
