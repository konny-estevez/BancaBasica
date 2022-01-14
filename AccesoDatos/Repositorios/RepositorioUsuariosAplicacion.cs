using AccesoDatos.Generico;
using AccesoDatos.Interfaces;
using Entidades;

namespace AccesoDatos.Repositorios
{
    public class RepositorioUsuariosAplicacion : Repositorio<UsuarioAplicacion>, IRepositorioUsuariosAplicacion
    {
        private readonly ApplicationDbContext _contexto;

        public RepositorioUsuariosAplicacion(ApplicationDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
