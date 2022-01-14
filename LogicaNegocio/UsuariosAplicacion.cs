using AccesoDatos.Interfaces;
using Entidades.DTO;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private readonly IRepositorioUsuariosAplicacion _repositorioUsuariosAplicacion;
        private readonly UserManager<IdentityUser> _userManager;

        public UsuariosAplicacion(IRepositorioUsuariosAplicacion repositorioUsuariosAplicacion, UserManager<IdentityUser> userManager)
        {
            _repositorioUsuariosAplicacion = repositorioUsuariosAplicacion;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UsuarioSimple>> ObtenerListaSimple()
        {
            var resultado = new List<UsuarioSimple>();
            var usuarios = _repositorioUsuariosAplicacion.ObtenerLista();

            foreach (var item in usuarios)
            {
                if (!await _userManager.IsInRoleAsync(item, "ROLE_ADMIN"))
                {
                    resultado.Add(new UsuarioSimple { Id = item.Id, NombreUsuario = item.UserName });
                }
            }
            return resultado;
        }
    }
}
