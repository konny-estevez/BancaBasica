using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AccesoDatos.Generico
{
    public interface IRepositorio<T> where T : class
    {
        T Obtener(Guid id);

        IEnumerable<T> ObtenerLista(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> ordenamiento = null, string incluirPropiedades = null);

        T ObtenerPrimeroPredeterminado(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null);

        T Agregar(T entidad);
        T Actualizar(T entidad);

        bool Eliminar(Guid id);

        bool Eliminar(T entidad);

    }
}
