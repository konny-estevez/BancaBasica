using AplicacionWeb.Models;
using LogicaNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AplicacionWeb.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN")]
    public class ReportesController : Controller
    {
        private readonly IEstadosCuenta _logicaNegocio;
        private readonly IClientes _logicaNegocioClientes;
        public ReportesController(IEstadosCuenta logicaNegocio, IClientes logicaNegocioClientes)
        {
            _logicaNegocio = logicaNegocio;
            _logicaNegocioClientes = logicaNegocioClientes;
        }

        // GET: ReportesController
        public ActionResult Index()
        {
            var modelo = new ReporteViewModel { FechaHasta = DateTime.Now, FechaDesde = new DateTime(DateTime.Today.Year, 1,1) };
            ViewBag.Clientes = ObtenerListaClientes();
            return View("SearchReportView", modelo);
        }

        // GET: ReportesController/Details/5
        [Route("Reportes/find=ByFechaBetween")]
        public ActionResult Search([FromQuery]DateTime from, [FromQuery]DateTime to, [FromQuery]string idClient)
        {
            if (ModelState.IsValid)
            {
                TempData["FechaDesde"] = from;
                TempData["FechaHasta"] = to;
                TempData["IdCliente"] = idClient;
                return RedirectToAction(nameof(Report));
            }
            ViewBag.Clientes = ObtenerListaClientes();
            return View("SearchReportView");
        }

        public ActionResult Report()
        {
            var fechaDesde = Convert.ToDateTime(TempData["FechaDesde"]);
            var fechaHasta = Convert.ToDateTime(TempData["FechaHasta"]);
            var idCliente = TempData["IdCliente"]?.ToString();

            if (string.IsNullOrEmpty(idCliente))
            {
                return RedirectToAction(nameof(Index));
            }
            var reporte = new ReporteViewModel
            {
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                EstadosCuenta = _logicaNegocio.ObtenerLista(fechaDesde, fechaHasta, idCliente, out string nombres, out string error),
                Cliente = nombres,
                IdCliente = Guid.Parse(idCliente),
            };
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return View("ReportView", reporte);
        }

        public ActionResult ByFechaBetween([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] string idClient)
        {
            var reporte = new ReporteViewModel
            {
                FechaDesde = from,
                FechaHasta = to,
                EstadosCuenta = _logicaNegocio.ObtenerLista(from, to, idClient, out string nombres, out string error),
                Cliente = nombres,
                IdCliente = Guid.Parse(idClient),
            };
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return View("ReportView", reporte);
        }

        private IEnumerable<SelectListItem> ObtenerListaClientes()
        {
            var clientes = _logicaNegocioClientes.ObtenerListaSimple().Result.ToList();
            return clientes.OrderBy(x => x.Nombres).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nombres
            });
        }
    }
}
