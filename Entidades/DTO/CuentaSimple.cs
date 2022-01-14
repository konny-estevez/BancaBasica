using System;

namespace Entidades.DTO
{
    /// <summary>
    /// DTO de Cuenta para DropDownList
    /// </summary>
    public class CuentaSimple
    {
        /// <summary>
        /// Propiedad Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Propiedad Numero de Cuenta
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Propiedad Nombres
        /// </summary>
        public string Nombres { get; set; }
    }
}
