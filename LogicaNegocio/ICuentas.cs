using Entidades;
using Entidades.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public interface ICuentas
    {
        Task<IEnumerable<Cuenta>> ObtenerLista(string nombreUsuario = null);

        Task<Cuenta> Obtener(Guid id);

        Task<IEnumerable<CuentaSimple>> ObtenerListaSimple(Guid? idCliente);

        Cuenta Crear(Cuenta nuevaCuenta, out string error);

        Cuenta Actualizar(Cuenta actualizadaCuenta, out string error);

        Task<bool> Eliminar(Guid id);
    }
}