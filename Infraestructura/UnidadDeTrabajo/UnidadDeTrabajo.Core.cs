using Dominio.Entidades;
using Dominio.Repositorio;
using Infraestructura.Repositorio;

namespace Infraestructura.UnidadDeTrabajo
{
    public partial class UnidadDeTrabajo
    {
        // ============================================================================================================ //

        private IRepositorio<Proveedor> proveedorRepositorio;

        public IRepositorio<Proveedor> ProveedorRepositorio => proveedorRepositorio
                                                               ?? (proveedorRepositorio =
                                                                   new Repositorio<Proveedor>(_context));

        // ============================================================================================================ //

        private IEmpleadoRepositorio empleadoRepositorio;

        public IEmpleadoRepositorio EmpleadoRepositorio => empleadoRepositorio
                                                           ?? (empleadoRepositorio = 
                                                               new EmpleadoRepositorio(_context));

        // ============================================================================================================ //

        private IClienteRepositorio clienteRepositorio;

        public IClienteRepositorio ClienteRepositorio => clienteRepositorio
                                                         ?? (clienteRepositorio =
                                                             new ClienteRepositorio(_context));

        // ============================================================================================================ //

        private IRepositorio<Configuracion> configuracionRepositorio;

        public IRepositorio<Configuracion> ConfiguracionRepositorio => configuracionRepositorio
                                                                       ?? (configuracionRepositorio =
                                                                           new Repositorio<Configuracion>(_context));

        // ============================================================================================================ //

        private IRepositorio<ListaPrecio> listaPrecioRepositorio;

        public IRepositorio<ListaPrecio> ListaPrecioRepositorio => listaPrecioRepositorio
                                                                   ?? (listaPrecioRepositorio =
                                                                       new Repositorio<ListaPrecio>(_context));

        // ============================================================================================================ //

        private IRepositorio<Articulo> articuloRepositorio;

        public IRepositorio<Articulo> ArticuloRepositorio => articuloRepositorio
                                                             ?? (articuloRepositorio =
                                                                 new Repositorio<Articulo>(_context));

        // ============================================================================================================ //

        private IRepositorio<PuestoTrabajo> puestoTrabajoRepositorio;

        public IRepositorio<PuestoTrabajo> PuestoTrabajoRepositorio => puestoTrabajoRepositorio
                                                                       ?? (puestoTrabajoRepositorio =
                                                                           new Repositorio<PuestoTrabajo>(_context));

        // ============================================================================================================ //

        private IRepositorio<Stock> stockRepositorio;

        public IRepositorio<Stock> StockRepositorio => stockRepositorio
                                                               ?? (stockRepositorio =
                                                                   new Repositorio<Stock>(_context));

        // ============================================================================================================ //

        private IRepositorio<Precio> precioRepositorio;

        public IRepositorio<Precio> PrecioRepositorio => precioRepositorio
                                                         ?? (precioRepositorio =
                                                             new Repositorio<Precio>(_context));

        // ============================================================================================================ //

        private IRepositorio<Contador> contadoroRepositorio;

        public IRepositorio<Contador> ContadorRepositorio => contadoroRepositorio
                                                             ?? (contadoroRepositorio =
                                                                 new Repositorio<Contador>(_context));

        // ============================================================================================================ //

        private IRepositorio<Caja> cajaRepositorio;

        public IRepositorio<Caja> CajaRepositorio => cajaRepositorio
                                                     ?? (cajaRepositorio =
                                                         new Repositorio<Caja>(_context));

        // ============================================================================================================ //

        private IRepositorio<DetalleCaja> detalleCajaRepositorio;

        public IRepositorio<DetalleCaja> DetalleCajaRepositorio => detalleCajaRepositorio
                                                                ?? (detalleCajaRepositorio =
                                                                    new Repositorio<DetalleCaja>(_context));

        // ============================================================================================================ //

        private IRepositorio<Banco> bancoRepositorio;

        public IRepositorio<Banco> BancoRepositorio => bancoRepositorio
                                                       ?? (bancoRepositorio =
                                                           new Repositorio<Banco>(_context));

        // ============================================================================================================ //

        private IRepositorio<Tarjeta> tarjetaRepositorio;

        public IRepositorio<Tarjeta> TarjetaRepositorio => tarjetaRepositorio
                                                           ?? (tarjetaRepositorio =
                                                               new Repositorio<Tarjeta>(_context));

        // ============================================================================================================ //

        private IFacturaRepositorio facturaRepositorio;

        public IFacturaRepositorio FacturaRepositorio => facturaRepositorio
                                                         ?? (facturaRepositorio =
                                                             new FacturaRepositorio(_context));

        // ============================================================================================================ //

        private ICuentaCorrienteRepositorio cuentaCorrienteRepositorio;

        public ICuentaCorrienteRepositorio CuentaCorrienteRepositorio => cuentaCorrienteRepositorio
                                                                 ?? (cuentaCorrienteRepositorio =
                                                                     new CuentaCorrienteRepositorio(_context));

        // ============================================================================================================ //

        private ICtaCteClienteRepositorio ctaCteClienteRepositorio;

        public ICtaCteClienteRepositorio CtaCteClienteRepositorio => ctaCteClienteRepositorio
                                                                     ?? (ctaCteClienteRepositorio =
                                                                         new CtaCteClienteRepositorio(_context));

        // ============================================================================================================ //

        private IRepositorio<BajaArticulo> bajaArticuloRepositorio;

        public IRepositorio<BajaArticulo> BajaArticuloRepositorio => bajaArticuloRepositorio
                                                                     ?? (bajaArticuloRepositorio =
                                                                         new Repositorio<BajaArticulo>(_context));

        // ============================================================================================================ //

        private IRepositorio<MotivoBaja> motivoBajaRepositorio;

        public IRepositorio<MotivoBaja> MotivoBajaRepositorio => motivoBajaRepositorio
                                                                 ?? (motivoBajaRepositorio =
                                                                     new Repositorio<MotivoBaja>(_context));

        // ============================================================================================================ //
    }
}
