using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionWeb.Models
{
    public class ReporteViewModel
    {
        [Required]
        public DateTime FechaDesde { get; set; }

        [Required]
        public DateTime FechaHasta { get; set; }

        [Required]
        public Guid IdCliente { get; set; }
        public IEnumerable<EstadoCuenta> EstadosCuenta { get; set; }
        public string Cliente { get; internal set; }

    }
}
