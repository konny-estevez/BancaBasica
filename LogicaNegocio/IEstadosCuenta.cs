using Entidades;
using System;
using System.Collections.Generic;

namespace LogicaNegocio
{
    public interface IEstadosCuenta
    {
        IEnumerable<EstadoCuenta> ObtenerLista(DateTime fechaDesde, DateTime fechaHasta, string idCliente, out string nombres, out string error);
    }
}