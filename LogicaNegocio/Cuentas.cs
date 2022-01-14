using AccesoDatos.Interfaces;
using Entidades;
using Entidades.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Cuentas : ICuentas
    {
        private readonly IRepositorioCuentas _repositorio;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IRepositorioClientes _repositorioClientes;

        public Cuentas(IRepositorioCuentas repositorioCuentas, IUnidadTrabajo unidadTrabajo, IRepositorioClientes repositorioClientes)
        {
            _repositorio = repositorioCuentas;
            _unidadTrabajo = unidadTrabajo;
            _repositorioClientes = repositorioClientes;
        }

        public async Task<IEnumerable<Cuenta>> ObtenerLista(string nombreUsuario = null)
        {
            if (string.IsNullOrEmpty(nombreUsuario))
                return _repositorio.ObtenerLista(null, null, "Cliente");
            else
            {
                var cliente = _repositorioClientes.ObtenerPrimeroPredeterminado(x => x.NombreUsuario.Equals(nombreUsuario));
                if (cliente != null)
                {
                    return _repositorio.ObtenerLista(x => x.IdCliente == cliente.Id, null, "Cliente");
                }
                return new List<Cuenta>();
            }
        }

        public async Task<Cuenta> Obtener(Guid id)
        {
            return _repositorio.ObtenerPrimeroPredeterminado(x => x.Id == id, "Cliente");
        }

        public async Task<IEnumerable<CuentaSimple>> ObtenerListaSimple(Guid? idCliente)
        {
            IEnumerable<Cuenta> cuentas;
            if (idCliente.HasValue)
            {
                cuentas = _repositorio.ObtenerLista(x => x.Cliente.Id == idCliente, null, "Cliente");
            }
            else
            {
                cuentas = _repositorio.ObtenerLista(null, null, "Cliente");
            }

            return cuentas.Select(x => new CuentaSimple
            {
                Id = x.Id,
                Numero = x.Numero,
                Nombres = x.Cliente.Nombres,
            });
        }

        public Cuenta Crear(Cuenta nuevaCuenta, out string error)
        {
            error = string.Empty;
            if (_repositorio.ObtenerLista(x => x.Numero.Equals(nuevaCuenta.Numero)).Any())
            {
                error = "Cuenta ya existe, no se puede duplicar.";
                return null;
            }
            nuevaCuenta.Id = new Guid();
            nuevaCuenta.CreadoEn = DateTime.Now;
            nuevaCuenta.ActualizadoEn = DateTime.Now;
            var resultado = _repositorio.Agregar(nuevaCuenta);
            _unidadTrabajo.Aceptar();
            return resultado;
        }

        public Cuenta Actualizar(Cuenta actualizadaCuenta, out string error)
        {
            error = string.Empty;
            var cuenta = Obtener(actualizadaCuenta.Id).Result;
            if (cuenta == null)
            {
                error = "Cuenta inexistente, no se puede actualizar.";
                return null;
            }
            if (_repositorio.ObtenerLista(x => x.Id != actualizadaCuenta.Id && x.Numero.Equals(actualizadaCuenta.Numero)).Any())
            {
                error = "Cuenta ya existe, no se puede duplicar.";
                return null;
            }
            cuenta.ActualizadoEn = DateTime.Now;
            cuenta.Numero = actualizadaCuenta.Numero;
            cuenta.Saldo = actualizadaCuenta.Saldo;
            cuenta.Tipo = actualizadaCuenta.Tipo;
            var resultado = _repositorio.Actualizar(cuenta);
            _unidadTrabajo.Aceptar();
            return resultado;
        }

        public async Task<bool> Eliminar(Guid id)
        {
            var cuenta = await Obtener(id);
            if (cuenta == null)
            {
                return false;
            }
            var resultado = _repositorio.Eliminar(id);
            _unidadTrabajo.Aceptar();
            return resultado;
        }
    }
}
