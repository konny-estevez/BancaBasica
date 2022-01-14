using System.Threading;
using System.Threading.Tasks;
using AccesoDatos.Generico;
using Moq;
using PruebasUnitarias.Fakers;

namespace PruebasUnitarias.MockRepositorios
{
    public static class MockBancaBasicaRepositorio<TEntity> where TEntity : class, new()
    {
        public static Mock<IRepositorio<TEntity>> Setup(FakerBase<TEntity> faker, int records = 100000)
        {
            var mockRepository = new Mock<IRepositorio<TEntity>>();
            /*mockRepository.Setup(x => x.ObtenerPrimeroPredeterminado(It.IsAny<Expression<Func<TEntity, bool>>>(),
                It.IsAny<Expression<Func<TEntity, object>>[]>()))
                .Returns((Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes) =>
                    predicate == null ? Task.FromResult(faker.GenerateRandomData(records).FirstOrDefault()) : Task.FromResult(faker.GenerateRandomData(records).Where(predicate.Compile()).FirstOrDefault())
                );
            mockRepository.Setup(x => x.Obtener(It.IsAny<Expression<Func<TEntity, bool>>>()).
                .Returns((Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes) =>
                    predicate == null ? Task.FromResult(faker.GenerateRandomData(records).AsEnumerable()) : Task.FromResult(faker.GenerateRandomData(records).Where(predicate.Compile()).AsEnumerable())
                );
            mockRepository.Setup(x => x.ObtenerLista(It.IsAny<Expression<Func<TEntity, bool>>>(),
                It.IsAny<Expression<Func<TEntity, object>>[]>()))
                .Returns((Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes) =>
                    predicate == null ? faker.GenerateRandomData(records).AsQueryable() : faker.GenerateRandomData(records).Where(predicate.Compile()).AsQueryable()
                );*/
            mockRepository.Setup(x => x.Agregar(It.IsAny<TEntity>()));
            mockRepository.Setup(x => x.Actualizar(It.IsAny<TEntity>()));
            mockRepository.Setup(x => x.Eliminar(It.IsAny<TEntity>()));

            return mockRepository;
        }
    }
}
