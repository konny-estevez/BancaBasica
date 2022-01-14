using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    /// <summary>
    /// Clase Movimiento
    /// </summary>
    public class Movimiento
    {
        /// <summary>
        /// Propiedad Id, clave primaria
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Propiedad EsCredito de Movimiento
        /// </summary>
        [Required]
        public bool EsCredito { get; set; }

        /// <summary>
        /// Propiedad Fecha de Movimiento
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Propiedad Descripcion de Movimiento
        /// </summary>
        [Required]
        [MinLength(3), MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Propiedad Valor de Movimiento
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        /// <summary>
        /// Propiedad ActualizadoEn de Movimiento
        /// </summary>
        public DateTime ActualizadoEn { get; set; }

        /// <summary>
        /// Propiedad de Nevegacion Cuenta de Movimiento
        /// </summary>
        [ForeignKey("IdCuenta")]
        public Cuenta Cuenta { get; set; }

        /// <summary>
        /// Propiedad Foranea IdCuenta de Movimiento
        /// </summary>
        [Required]
        public Guid IdCuenta { get; set; }
    }
}
