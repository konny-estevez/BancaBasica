using Entidades;

namespace PruebasUnitarias.Fakers
{
    public class MovimientosFaker : FakerBase<Movimiento>
    {
        static MovimientosFaker()
        {
            Faker.RuleFor(x => x.Id, f => f.Random.Guid());
        }
    }
}
