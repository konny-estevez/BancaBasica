using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    /// <summary>
    /// Clase EstadoCuenta
    /// </summary>
    public class EstadoCuenta
    {
        /// <summary>
        /// Propiedad Id Cuenta
        /// </summary>
        public Guid IdCuenta { get; set; }

        /// <summary>
        /// Propiedad NumeroCuenta de EstadoCuenta
        /// </summary>
        public string NumeroCuenta { get; set; }

        /// <summary>
        /// Propiedad FechaCorte de EstadoCuenta
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime FechaCorte { get; set; }

        /// <summary>
        /// Propiedad SaldoFinal de EstadoCuenta
        /// </summary>
        public decimal SaldoFinal { get; set; }

        /// <summary>
        /// Propiedad Creditos de EstadoCuenta
        /// </summary>
        public int Creditos { get; set; }

        /// <summary>
        /// Propiedad TotalCreditos de EstadoCuenta
        /// </summary>
        public decimal TotalCreditos { get; set; }

        /// <summary>
        /// Propiedad Debitos de EstadoCuenta
        /// </summary>
        public int Debitos { get; set; }

        /// <summary>
        /// Propiedad TotalDebitos de EstadoCuenta
        /// </summary>
        public decimal TotalDebitos { get; set; }
    }
}
