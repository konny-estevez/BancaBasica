using System.Threading.Tasks;
using AccesoDatos.Generico;
using AccesoDatos.Interfaces;
using Entidades;
using LogicaNegocio;
using Moq;
using PruebasUnitarias.Fakers;
using PruebasUnitarias.MockRepositorios;

namespace PruebasUnitarias
{
    public class BancaBasicaUnitTest
    {
        protected Mock<IUnidadTrabajo> UnidadTrabajoMock { get; private set; }
        protected Mock<IRepositorio<Cliente>> RepositorioClientesMock { get; private set; }
        protected Mock<IRepositorio<Cuenta>> RepositorioCuentasMock { get; private set; }
        protected Mock<IRepositorio<Movimiento>> RepositorioMovimientosMock { get; private set; }
        protected Mock<IRepositorio<UsuarioAplicacion>> RepositorioUsuariosAplicacionMock { get; private set; }
        
        
        protected IClientes NegocioClientes { get; private set; }
        protected ICuentas NegocioCuentas { get; private set; }
        protected IMovimientos NegocioMovimientos { get; private set; }
        protected IEstadosCuenta NegocioEstadosCuenta { get; private set; }
        protected IUsuariosAplicacion NegocioUsuariosAplicacion { get; private set; }

        public BancaBasicaUnitTest()
        {
            UnidadTrabajoMock = new Mock<IUnidadTrabajo>();
            UnidadTrabajoMock.Setup(x => x.Aceptar());

            RepositorioClientesMock = MockBancaBasicaRepositorio<Cliente>.Setup(new ClientesFaker());
            RepositorioCuentasMock = MockBancaBasicaRepositorio<Cuenta>.Setup(new CuentasFaker());
            RepositorioMovimientosMock = MockBancaBasicaRepositorio<Movimiento>.Setup(new MovimientosFaker());
            RepositorioUsuariosAplicacionMock = MockBancaBasicaRepositorio<UsuarioAplicacion>.Setup(new UsuarioAplicacionFaker());

        }
    }
}
