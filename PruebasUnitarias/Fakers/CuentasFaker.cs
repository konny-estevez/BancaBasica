using Entidades;

namespace PruebasUnitarias.Fakers
{
    public class CuentasFaker : FakerBase<Cuenta>
    {
        static CuentasFaker()
        {
            Faker.RuleFor(x => x.Id, f => f.Random.Guid());
        }
    }
}
