using AccesoDatos.Generico;
using AccesoDatos.Interfaces;
using Entidades;

namespace AccesoDatos.Repositorios
{
    public class RepositorioClientes : Repositorio<Cliente>, IRepositorioClientes
    {
        private readonly ApplicationDbContext _contexto;

        public RepositorioClientes(ApplicationDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}