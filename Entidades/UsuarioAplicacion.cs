using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class UsuarioAplicacion : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        [DataType("varchar(100)")]
        public string Nombres { get; set; }
    }
}
