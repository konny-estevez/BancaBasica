using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    /// <summary>
    /// Clase Cliente
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Propiedad Id, clave primaria
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Propiedad Nombres de Cliente
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Nombres { get; set; }

        /// <summary>
        /// Propiedad Direccion de Cliente
        /// </summary>
        [Required]
        [MaxLength(250)]
        [Column(TypeName = "varchar(250)")]
        public string Direccion { get; set; }

        /// <summary>
        /// Propiedad Telefono de Cliente
        /// </summary>
        [Required]
        [MaxLength(10)]
        [Phone]
        [Column(TypeName = "varchar(10)")]
        public string Telefono { get; set; }

        /// <summary>
        /// Propiedad Email de Cliente
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        [Column(TypeName = "varchar(150)")]
        public string Email { get; set; }

        /// <summary>
        /// Propiedad CreadoEn de Cliente
        /// </summary>
        public DateTime CreadoEn { get; set; }

        /// <summary>
        /// Propiedad Actualizado de Cliente
        /// </summary>
        public DateTime ActualizadoEn { get; set; }

        /// <summary>
        /// Propiedad NombreUsuario de Cliente
        /// </summary>
        [MaxLength(100)]
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Propiedad de Navegacion Cuentas de Cliente
        /// </summary>
        public ICollection<Cuenta> Cuentas { get; set; }
    }
}
