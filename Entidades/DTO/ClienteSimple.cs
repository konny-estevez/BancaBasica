using System;

namespace Entidades.DTO
{
    /// <summary>
    /// DTO de Cliente para DropDownList
    /// </summary>
    public class ClienteSimple
    {
        /// <summary>
        /// Propiedad Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Propiedad Nombres
        /// </summary>
        public string Nombres { get; set; }
    }
}
