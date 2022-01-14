using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entidades;
using LogicaNegocio;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace AplicacionWeb.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN, ROLE_USER")]
    public class MovimientosController : Controller
    {
        private readonly IMovimientos _logicaNegocio;
        private readonly IClientes _logicaNegocioClientes;
        private readonly ICuentas _logicaNegocioCuentas;

        public MovimientosController(IMovimientos logicaNegocio, IClientes logicaNegocioClientes, ICuentas logicaNegocioCuentas)
        {
            _logicaNegocio = logicaNegocio;
            _logicaNegocioClientes = logicaNegocioClientes;
            _logicaNegocioCuentas = logicaNegocioCuentas;
        }

        // GET: Movimientos
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("ROLE_ADMIN"))
                return View(await _logicaNegocio.ObtenerLista());
            else
                return View(await _logicaNegocio.ObtenerLista(User.Identity.Name));
        }

        // GET: Movimientos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _logicaNegocio.Obtener(id.Value);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // GET: Movimientos/Create
        public IActionResult Create()
        {
            ViewBag.Cuentas = ObtenerListaCuentas();
            var modelo = new Movimiento() { Fecha = DateTime.Now };
            return View(modelo);
        }

        // POST: Movimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EsCredito,Fecha,Descripcion,Valor,IdCuenta")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                var resultado = _logicaNegocio.Crear(movimiento, out string error);
                if (resultado)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el movimiento. " + error);
                }
            }
            ViewBag.Cuentas = ObtenerListaCuentas();
            return View(movimiento);
        }

        // GET: Movimientos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _logicaNegocio.Obtener(id.Value);
            if (movimiento == null)
            {
                return NotFound();
            }
            return View(movimiento);
        }

        // POST: Movimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,EsCredito,Fecha,Descripcion,Valor,IdCuenta")] Movimiento movimiento)
        {
            if (id != movimiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var resultado = _logicaNegocio.Actualizar(movimiento, out string error);
                if (resultado)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar el movimiento. " + error);
                    return View(movimiento);
                }
            }
            return View(movimiento);
        }

        // GET: Movimientos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _logicaNegocio.Obtener(id.Value);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var resultado = _logicaNegocio.Eliminar(id, out string error);
            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al eliminar el movimiento. " + error);
                return View();
            }
        }

        private IEnumerable<SelectListItem> ObtenerListaCuentas()
        {
            //IEnumerable<CuentaSimple> cuentas;
            Guid? idCliente = null;
            if (User.IsInRole("ROLE_USER"))
            {
                idCliente = _logicaNegocioClientes.ObtenerListaSimple(User.Identity.Name).Result.Select(x => x.Id).FirstOrDefault();
            }
            var cuentas = _logicaNegocioCuentas.ObtenerListaSimple(idCliente).Result.ToList();
            return cuentas.OrderBy(x => x.Numero).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.Numero} - {x.Nombres}"
            });
        }
    }
}
