using Entidades;
using Entidades.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public interface IClientes
    {
        Task<IEnumerable<Cliente>> ObtenerLista();

        Task<Cliente> Obtener(Guid id);

        Task<IEnumerable<ClienteSimple>> ObtenerListaSimple(string nombreUsuario = null);

        Task<Cliente> Crear(Cliente nuevoCliente);

        Cliente Actualizar(Cliente actualizadoCliente, out string error);

        Task<bool> Eliminar(Guid id);
    }
}