using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    /// <summary>
    /// Clase Cuenta 
    /// </summary>
    public class Cuenta
    {
        /// <summary>
        /// Propiedad Id, clave primaria
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Propiedad Tipo de Cuenta
        /// </summary>
        [Required]
        [DataType("char")]
        [MaxLength(1)]
        public string Tipo { get; set; }
        
        /// <summary>
        /// Propiedad Numero de Cuenta
        /// </summary>
        [Required]
        [MaxLength(16)]
        [DataType("varchar(16)")]
        [RegularExpression("^[0-9]{16}", ErrorMessage = "El número de cuenta solo debe contener números")]
        public string Numero { get; set; }

        /// <summary>
        /// Propiedad Saldo de Cuenta
        /// </summary>
        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; }

        /// <summary>
        /// Propiedad CreadoEn de Cuenta
        /// </summary>
        public DateTime CreadoEn { get; set; }

        /// <summary>
        /// Propiedad ActualizadoEn de Cuenta
        /// </summary>
        public DateTime ActualizadoEn { get; set; }

        /// <summary>
        /// Propiedad de Navegacion Cliente de Cuenta
        /// </summary>
        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        /// <summary>
        /// Propiedad Foranea IdCliente de Cuenta
        /// </summary>
        [Required]
        public Guid IdCliente { get; set; }

        /// <summary>
        /// Propiedad de Navegacion Movimientos de Cuenta
        /// </summary>
        public ICollection<Movimiento> Movimientos{ get; set; }
    }
}