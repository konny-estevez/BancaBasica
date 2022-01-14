using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AccesoDatos.Generico
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly DbSet<T> _datos;

        public Repositorio(DbContext contexto)
        {
            _datos = contexto.Set<T>();
        }

        public T Actualizar(T entidad)
        {
            try { 
            return _datos.Update(entidad).Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
         }

        public T Agregar(T entidad)
        {
            return _datos.Add(entidad).Entity;
        }

        public bool Eliminar(Guid id)
        {
            return _datos.Remove(Obtener(id)).State == EntityState.Deleted;
        }

        public bool Eliminar(T entidad)
        {
            return _datos.Remove(entidad).State == EntityState.Deleted;
        }

        public T Obtener(Guid id)
        {
            return _datos.Find(id);
        }

        public IEnumerable<T> ObtenerLista(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> ordenamiento = null, string incluirPropiedades = null)
        {
            IQueryable<T> consulta = _datos;
            if (filtro != null)
            {
                consulta = consulta.Where(filtro);
            }
            if (!string.IsNullOrEmpty(incluirPropiedades))
            {
                var propiedades = incluirPropiedades.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                propiedades.ForEach(x => consulta = consulta.Include(x));
            }
            if (ordenamiento != null)
            {
                return ordenamiento(consulta).ToList();
            }

            return consulta.ToList();
        }

        public T ObtenerPrimeroPredeterminado(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null)
        {
            IQueryable<T> consulta = _datos;
            if (filtro != null)
            {
                consulta = consulta.Where(filtro);
            }
            if (!string.IsNullOrEmpty(incluirPropiedades))
            {
                var propiedades = incluirPropiedades.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                propiedades.ForEach(x => consulta = consulta.Include(x));
            }

            return consulta.FirstOrDefault();
        }
    }
}
