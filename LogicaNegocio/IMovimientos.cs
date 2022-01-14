using Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public interface IMovimientos
    {
        Task<IEnumerable<Movimiento>> ObtenerLista(string nombreUsuario = null);

        Task<Movimiento> Obtener(Guid id);

        bool Crear(Movimiento nuevoMovimiento, out string error);

        bool Actualizar(Movimiento actualizadoMovimiento, out string error);

        bool Eliminar(Guid id, out string error);
    }
}