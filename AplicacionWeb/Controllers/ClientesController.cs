using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entidades;
using LogicaNegocio;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AplicacionWeb.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN")]
    public class ClientesController : Controller
    {
        private readonly IClientes _logicaNegocio;
        private readonly IUsuariosAplicacion _logicaNegocioUsuarios;

        public ClientesController(IClientes logicaNegocio, IUsuariosAplicacion logicaNegocioUsuarios)
        {
            _logicaNegocio = logicaNegocio;
            _logicaNegocioUsuarios = logicaNegocioUsuarios;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _logicaNegocio.ObtenerLista());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await _logicaNegocio.Obtener(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombres,Direccion,Telefono,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _logicaNegocio.Crear(cliente);
                if (resultado != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el cliente.");
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Usuarios = ObtenerListaUsuarios();
            var cliente = await _logicaNegocio.Obtener(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombres,Direccion,Telefono,Email,CreadoEn,NombreUsuario")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var resultado = _logicaNegocio.Actualizar(cliente, out string error);
                if (resultado != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar el cliente. " + error);
                }
            }
            ViewBag.Usuarios = ObtenerListaUsuarios();
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _logicaNegocio.Obtener(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
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
                ModelState.AddModelError(string.Empty, "Error al eliminar el cliente.");
                return View();
            }
        }

        private dynamic ObtenerListaUsuarios()
        {
            var usuarios = _logicaNegocioUsuarios.ObtenerListaSimple().Result.ToList();
            return usuarios.OrderBy(x => x.NombreUsuario).Select(x => new SelectListItem
            {
                Value = x.NombreUsuario,
                Text = x.NombreUsuario
            });
        }
    }
}
