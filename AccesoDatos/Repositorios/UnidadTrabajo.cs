using AccesoDatos.Interfaces;

namespace AccesoDatos.Repositorios
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _contexto;

        public UnidadTrabajo(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public void Aceptar()
        {
            _contexto.SaveChanges();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
