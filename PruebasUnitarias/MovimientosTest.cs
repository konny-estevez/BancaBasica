using Entidades;
using LogicaNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class MovimientosTest
    {
        private Movimientos logicaNegocio = new Movimientos(null, null, null, null);

        [TestMethod]
        public void CrearMovimiento_CuentaNoEncontrada()
        {
            List<Cuenta> cuentas = new List<Cuenta>();
            var movimiento = new Movimiento
            {
                ActualizadoEn = DateTime.Now,
                Descripcion = "Deposito",
                EsCredito = true,
                Fecha = DateTime.Now,
                IdCuenta = new Guid(),
                Valor = 100,
            };
            var errorEsperado = "Cuenta no existe.";
            Assert.AreEqual(logicaNegocio.Crear(movimiento, out string error), false);
            Assert.AreEqual(errorEsperado, error);
        }
    }
}
