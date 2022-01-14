using Entidades.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public interface IUsuariosAplicacion
    {
        Task<IEnumerable<UsuarioSimple>> ObtenerListaSimple();
    }
}