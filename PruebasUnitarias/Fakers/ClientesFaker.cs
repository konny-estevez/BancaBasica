using Entidades;

namespace PruebasUnitarias.Fakers
{
    public class ClientesFaker : FakerBase<Cliente>
    {
        static ClientesFaker()
        {
            Faker.RuleFor(x => x.Id, f => f.Random.Guid());
        }
    }
}
