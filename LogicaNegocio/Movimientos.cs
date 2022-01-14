using AccesoDatos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Movimientos : IMovimientos
    {
        private readonly IRepositorioMovimientos _repositorio;
        private readonly IRepositorioCuentas _repositorioCuentas;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IRepositorioClientes _repositorioClientes;

        public Movimientos(IRepositorioMovimientos repositorioMovimientos, IRepositorioCuentas repositorioCuentas, IUnidadTrabajo unidadTrabajo, 
            IRepositorioClientes repositorioClientes)
        {
            _repositorio = repositorioMovimientos;
            _repositorioCuentas = repositorioCuentas;
            _unidadTrabajo = unidadTrabajo;
            _repositorioClientes = repositorioClientes;
        }

        public async Task<IEnumerable<Movimiento>> ObtenerLista(string nombreUsuario = null)
        {
            if (string.IsNullOrEmpty(nombreUsuario))
                return _repositorio.ObtenerLista(null, null, "Cuenta");
            else
            {
                var cliente = _repositorioClientes.ObtenerPrimeroPredeterminado(x => x.NombreUsuario.Equals(nombreUsuario));
                if (cliente != null)
                {
                    var idCuentas =_repositorioCuentas.ObtenerLista(x => x.IdCliente == cliente.Id).Select(x => x.Id);
                    if (idCuentas != null)
                    {
                        return _repositorio.ObtenerLista(x => idCuentas.Contains(x.IdCuenta), null, "Cuenta");
                    }
                }
                return new List<Movimiento>();
            }
        }

        public async Task<Movimiento> Obtener(Guid id)
        {
            return _repositorio.Obtener(id);
        }

        public bool Crear(Movimiento nuevoMovimiento, out string error)
        {
            error = string.Empty;
            var cuenta = _repositorioCuentas.Obtener(nuevoMovimiento.IdCuenta);
            if (cuenta == null)
            {
                error = "Cuenta no existe.";
                return false;
            }
            if (!nuevoMovimiento.EsCredito)
            {
                if (cuenta.Saldo - nuevoMovimiento.Valor < 0)
                {
                    error = "Saldo insufuciente para la operación.";
                    return false;
                }
            }
            cuenta.Saldo = nuevoMovimiento.EsCredito ? cuenta.Saldo += nuevoMovimiento.Valor : cuenta.Saldo -= nuevoMovimiento.Valor;
            cuenta.ActualizadoEn = DateTime.Now;
            _repositorioCuentas.Actualizar(cuenta);
            nuevoMovimiento.Id = new Guid();
            _repositorio.Agregar(nuevoMovimiento);
            _unidadTrabajo.Aceptar();
            return true;
        }

        public bool Actualizar(Movimiento actualizadoMovimiento, out string error)
        {
            error = string.Empty;
            var movimiento = Obtener(actualizadoMovimiento.Id).Result;
            if (movimiento == null)
            {
                error = "No existe el movimiento.";
                return false;
            }
            var cuenta = _repositorioCuentas.Obtener(actualizadoMovimiento.IdCuenta);
            var saldoNuevo = cuenta.Saldo;
            if (!actualizadoMovimiento.EsCredito)
            {
                if (movimiento.EsCredito)
                {
                    saldoNuevo = cuenta.Saldo - movimiento.Valor - actualizadoMovimiento.Valor;
                }
                else if (movimiento.Valor != actualizadoMovimiento.Valor)
                {
                    saldoNuevo = cuenta.Saldo + movimiento.Valor - actualizadoMovimiento.Valor;
                }
            }
            else
            {
                if (movimiento.EsCredito)
                {
                    saldoNuevo = cuenta.Saldo - movimiento.Valor + actualizadoMovimiento.Valor;
                }
                else if (movimiento.Valor != actualizadoMovimiento.Valor)
                {
                    saldoNuevo = cuenta.Saldo + movimiento.Valor + actualizadoMovimiento.Valor;
                }
                else
                {
                    saldoNuevo = cuenta.Saldo + movimiento.Valor + actualizadoMovimiento.Valor;
                }
            }
            if (saldoNuevo < 0)
            {
                error = "Saldo insufuciente para la operación.";
                return false;
            }
            cuenta.Saldo = saldoNuevo;
            cuenta.ActualizadoEn = DateTime.Now;
            _repositorioCuentas.Actualizar(cuenta);
            movimiento.ActualizadoEn = DateTime.Now;
            movimiento.Descripcion = actualizadoMovimiento.Descripcion;
            movimiento.EsCredito = actualizadoMovimiento.EsCredito;
            movimiento.Fecha = actualizadoMovimiento.Fecha;
            movimiento.Valor = actualizadoMovimiento.Valor;
            _repositorio.Actualizar(movimiento);
            _unidadTrabajo.Aceptar();
            return true;
        }

        public bool Eliminar(Guid id, out string error)
        {
            error = string.Empty;
            var movimiento = Obtener(id).Result;
            if (movimiento == null)
            {
                error = "No existe el movimiento.";
                return false;
            }
            var cuenta = _repositorioCuentas.Obtener(movimiento.IdCuenta);
            if (movimiento.EsCredito)
            {
                if (cuenta.Saldo - movimiento.Valor < 0)
                {
                    error = "No se puede realizar para la eliminación del movimiento.";
                    return false;
                }
            }
            cuenta.Saldo = !movimiento.EsCredito ? cuenta.Saldo += movimiento.Valor : cuenta.Saldo -= movimiento.Valor;
            cuenta.ActualizadoEn = DateTime.Now;
            _repositorioCuentas.Actualizar(cuenta);
            var resultado = _repositorio.Eliminar(id);
            _unidadTrabajo.Aceptar();
            return resultado;
        }
    }
}
