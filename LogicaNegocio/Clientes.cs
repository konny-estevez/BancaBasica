using AccesoDatos.Interfaces;
using Entidades;
using Entidades.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Clientes : IClientes
    {
        private readonly IRepositorioClientes _repositorio;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public Clientes(IRepositorioClientes repositorioClientes, IUnidadTrabajo unidadTrabajo)
        {
            _repositorio = repositorioClientes;
            _unidadTrabajo = unidadTrabajo;
        }

        public async Task<IEnumerable<Cliente>> ObtenerLista()
        {
            return _repositorio.ObtenerLista();
        }

        public async Task<Cliente> Obtener(Guid id)
        {
            return _repositorio.Obtener(id);
        }

        public async Task<IEnumerable<ClienteSimple>> ObtenerListaSimple(string nombreUsuario = null)
        {
            var resultado = _repositorio.ObtenerLista(string.IsNullOrEmpty(nombreUsuario) ? null : x => x.NombreUsuario.Equals(nombreUsuario));
            return resultado.Select(x => new ClienteSimple
                {
                    Id = x.Id,
                    Nombres = x.Nombres,
                });
        }

        public async Task<Cliente> Crear(Cliente nuevoCliente)
        {
            nuevoCliente.Id = new Guid();
            nuevoCliente.CreadoEn = DateTime.Now;
            nuevoCliente.ActualizadoEn = DateTime.Now;
            var resultado = _repositorio.Agregar(nuevoCliente);
            _unidadTrabajo.Aceptar();
            return resultado;
        }

        public Cliente Actualizar(Cliente actualizadoCliente, out string error)
        {
            error = string.Empty;
            var cliente = Obtener(actualizadoCliente.Id).Result;
            if (cliente == null)
            {
                error = "Cliente no existente.";
                return null;
            }
            var usuarioUsado = _repositorio.ObtenerLista(x => x.Id != actualizadoCliente.Id && 
                x.NombreUsuario.Equals(actualizadoCliente.NombreUsuario));
            if (usuarioUsado.Any())
            {
                error = "El nombre de usuario se encuentra usado por otro cliente.";
                return null;
            }
            cliente.ActualizadoEn = DateTime.Now;
            cliente.Direccion = actualizadoCliente.Direccion;
            cliente.Email = actualizadoCliente.Email;
            cliente.Nombres = actualizadoCliente.Nombres;
            cliente.Telefono = actualizadoCliente.Telefono;
            cliente.NombreUsuario = actualizadoCliente.NombreUsuario;
            var resultado = _repositorio.Actualizar(cliente);
            _unidadTrabajo.Aceptar();
            return resultado;
        }

        public async Task<bool> Eliminar(Guid id)
        {
            var cliente = await Obtener(id);
            if (cliente == null)
            {
                return false;
            }
            var resultado =_repositorio.Eliminar(id);
            _unidadTrabajo.Aceptar();
            return resultado;
        }
    }
}
