using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entidades;
using LogicaNegocio;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Entidades.DTO;

namespace AplicacionWeb.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN, ROLE_USER")]
    public class CuentasController : Controller
    {
        private readonly ICuentas _logicaNegocio;
        private readonly IClientes _logicaNegocioClientes;

        public CuentasController(ICuentas logicaNegocio, IClientes logicaNegocioClientes)
        {
            _logicaNegocio = logicaNegocio;
            _logicaNegocioClientes = logicaNegocioClientes;
        }

        // GET: Cuentas
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("ROLE_ADMIN"))
                return View(await _logicaNegocio.ObtenerLista());
            else
                return View(await _logicaNegocio.ObtenerLista(User.Identity.Name));
        }

        // GET: Cuentas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _logicaNegocio.Obtener(id.Value);
            if (cuenta == null)
            {
                return NotFound();
            }

            return View(cuenta);
        }

        // GET: Cuentas/Create
        public IActionResult Create()
        {
            ViewBag.Clientes = ObtenerListaClientes();
            return View(new Cuenta { Saldo = 0 });
        }

        // POST: Cuentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Numero,Saldo,IdCliente")] Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                var resultado = _logicaNegocio.Crear(cuenta, out string error);
                if (resultado != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear la cuenta. " + error);
                }
            }
            ViewBag.Clientes = ObtenerListaClientes();
            return View(cuenta);
        }

        // GET: Cuentas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _logicaNegocio.Obtener(id.Value);
            if (cuenta == null)
            {
                return NotFound();
            }

            return View(cuenta);
        }

        // POST: Cuentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Tipo,Numero,Saldo,CreadoEn,IdCliente")] Cuenta cuenta)
        {
            if (id != cuenta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var resultado = _logicaNegocio.Actualizar(cuenta, out string error);
                if (resultado != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar la cuenta." + error);
                }
            }
            cuenta.Cliente = await _logicaNegocioClientes.Obtener(cuenta.IdCliente);
            return View(cuenta);
        }

        // GET: Cuentas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _logicaNegocio.Obtener(id.Value);
            if (cuenta == null)
            {
                return NotFound();
            }

            return View(cuenta);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var resultado = await _logicaNegocio.Eliminar(id);
            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al eliminar la cuenta.");
                return View();
            }
        }

        private IEnumerable<SelectListItem> ObtenerListaClientes()
        {
            IEnumerable<ClienteSimple> clientes;
            if (User.IsInRole("ROLE_ADMIN"))
            {
                clientes = _logicaNegocioClientes.ObtenerListaSimple().Result.ToList();
            }
            else
            {
                clientes = _logicaNegocioClientes.ObtenerListaSimple(User.Identity.Name).Result.ToList();
            }
            return clientes.OrderBy(x => x.Nombres).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nombres
            });
        }
    }
}
