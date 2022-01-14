using AccesoDatos.Generico;
using AccesoDatos.Interfaces;
using Entidades;

namespace AccesoDatos.Repositorios
{
    public class RepositorioMovimientos : Repositorio<Movimiento>, IRepositorioMovimientos
    {
        private readonly ApplicationDbContext _contexto;

        public RepositorioMovimientos(ApplicationDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
