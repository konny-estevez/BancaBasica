using Entidades;

namespace PruebasUnitarias.Fakers
{
    public class UsuarioAplicacionFaker : FakerBase<UsuarioAplicacion>
    {
        static UsuarioAplicacionFaker()
        {
            Faker.RuleFor(x => x.Id, f => f.Random.Guid().ToString());
        }
    }
}
