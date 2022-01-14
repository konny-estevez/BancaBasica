using AccesoDatos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicaNegocio
{
    public class EstadosCuenta : IEstadosCuenta
    {
        private readonly IRepositorioCuentas _repositorioCuentas;
        private readonly IRepositorioMovimientos _repositorioMovimientos;
        private readonly IRepositorioClientes _repositorioClientes;

        public EstadosCuenta(IRepositorioCuentas repositorioCuentas, IRepositorioMovimientos repositorioMovimientos, IRepositorioClientes repositorioClientes)
        {
            _repositorioCuentas = repositorioCuentas;
            _repositorioMovimientos = repositorioMovimientos;
            _repositorioClientes = repositorioClientes;
        }

        public IEnumerable<EstadoCuenta> ObtenerLista(DateTime fechaDesde, DateTime fechaHasta, string idCliente, out string nombres, out string error)
        {
            nombres = error = string.Empty;
            if (Guid.TryParse(idCliente, out Guid guidCliente))
            {
                var cliente = _repositorioClientes.Obtener(guidCliente);
                if (cliente == null)
                {
                    error = "No se encuentra cliente con ese Id.";
                    return new List<EstadoCuenta>();
                }
                nombres = cliente.Nombres;
                var cuentas = _repositorioCuentas.ObtenerLista(x => x.IdCliente == guidCliente);
                if (cuentas == null || !cuentas.Any())
                {
                    error = "El cliente no posee cuentas.";
                    return new List<EstadoCuenta>();
                }
                fechaHasta = fechaHasta.AddDays(1);
                var idCuentas = cuentas.Select(x => x.Id).ToList();
                var movimientos = _repositorioMovimientos.ObtenerLista(x => x.Fecha >= fechaDesde && x.Fecha < fechaHasta && idCuentas.Contains(x.IdCuenta), null, "Cuenta");
                if (movimientos == null || !movimientos.Any())
                {
                    error = "No existen movimientos en las cuentas del cliente.";
                    return _repositorioCuentas.ObtenerLista(x => x.IdCliente == guidCliente).Select(x => new EstadoCuenta
                    {
                        Creditos = 0,
                        Debitos = 0,
                        FechaCorte = DateTime.Today,
                        IdCuenta = x.Id,
                        NumeroCuenta = x.Numero,
                        SaldoFinal = x.Saldo,
                        TotalCreditos = 0,
                        TotalDebitos = 0,
                    }); ;
                }
                var resumen = from mov in movimientos
                               group mov by new { mov.IdCuenta, mov.Cuenta.Numero, mov.Cuenta.Saldo, mov.EsCredito } into cts
                               select new EstadoCuenta
                               {
                                   Creditos = cts.Key.EsCredito ? cts.Count() : 0,
                                   Debitos = cts.Key.EsCredito ? 0 : cts.Count(),
                                   FechaCorte = fechaHasta,
                                   IdCuenta = cts.Key.IdCuenta,
                                   NumeroCuenta = cts.Key.Numero,
                                   SaldoFinal = cts.Key.Saldo,
                                   TotalCreditos = cts.Key.EsCredito ? cts.Sum(x => x.Valor) : 0,
                                   TotalDebitos = cts.Key.EsCredito ? 0 : cts.Sum(x => x.Valor),
                               };
                var resultado = from ect in resumen
                                group ect by new { ect.IdCuenta, ect.FechaCorte, ect.NumeroCuenta, ect.SaldoFinal } into grp
                                select new EstadoCuenta
                                {
                                    Creditos = grp.Sum(x => x.Creditos),
                                    Debitos = grp.Sum(x => x.Debitos),
                                    FechaCorte = grp.Key.FechaCorte,
                                    IdCuenta = grp.Key.IdCuenta,
                                    NumeroCuenta = grp.Key.NumeroCuenta,
                                    SaldoFinal = grp.Key.SaldoFinal,
                                    TotalCreditos = grp.Sum(x => x.TotalCreditos),
                                    TotalDebitos = grp.Sum(x => x.TotalDebitos),
                                };
                return resultado.ToList();
            }
            else
            {
                error = "Id de cliente incorrecto.";
            }
            return null;
        }
    }
}
