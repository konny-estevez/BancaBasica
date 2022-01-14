using AccesoDatos.Generico;
using AccesoDatos.Interfaces;
using Entidades;

namespace AccesoDatos.Repositorios
{
    public class RepositorioCuentas : Repositorio<Cuenta>, IRepositorioCuentas
    {
        private readonly ApplicationDbContext _contexto;

        public RepositorioCuentas(ApplicationDbContext contexto) :base(contexto)
        {
            _contexto = contexto;
        }
    }
}
