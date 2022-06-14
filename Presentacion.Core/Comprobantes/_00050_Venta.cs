using IServicio.Configuracion;
using IServicio.ListaPrecio;
using IServicio.Persona;
using IServicios.PuestoDeTrabajo;
using PresentacionBase.Formularios;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicio.Articulo;
using IServicio.Configuracion.DTOs;
using IServicio.ListaPrecio.DTOs;
using IServicio.Persona.DTOs;
using IServicio.Rubro.DTOs;
using IServicios.Articulo.DTOs;
using IServicios.Comprobante.DTOs;
using IServicios.Contador;
using IServicios.Factura;
using IServicios.Factura.DTOs;
using Presentacion.Core.Articulo;
using Presentacion.Core.Cliente;
using Presentacion.Core.Comprobantes.Clases;
using Presentacion.Core.Empleado;
using Presentacion.Core.FormaPago;
using StructureMap;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.Comprobantes
{
    public partial class _00050_Venta : FormBase
    {
        private ClienteDto _clienteSeleccionado;
        private ConfiguracionDto _configuracion;
        private EmpleadoDto _vendedorSeleccionado;
        private FacturaView _factura;
        private ArticuloVentaDto _articuloSeleccionado;
        private ItemView _itemSeleccionado;
        private RubroDto _rubroActual;

        private bool _permiteAgregarPorCantidad;
        private bool _articuloConPrecioAlternativo;
        private bool _autorizaPermisoListaPrecio;
        private bool _ingresoPorCodigoBascula;
        private bool _cambiarCantidadConErrorPorValidacion;

        private readonly IConfiguracionServicio _configuracionServicio;
        private readonly IClienteServicio _clienteServicio;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IListaPrecioServicio _listaPrecioServicio;
        private readonly IPuestoDeTrabajoServicio _puestoDeTrabajo;
        private readonly IContadorServicio _contadorServicio;
        private readonly IArticuloServicio _articuloServicio;
        private readonly IFacturaServicio _facturaServicio;

        public _00050_Venta(IConfiguracionServicio configuracionServicio,
            IClienteServicio clienteServicio,
            IEmpleadoServicio empleadoServicio,
            IListaPrecioServicio listaPrecioServicio,
            IPuestoDeTrabajoServicio puestoDeTrabajo,
            IContadorServicio contadorServicio,
            IArticuloServicio articuloServicio, 
            IFacturaServicio facturaServicio)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            _configuracionServicio = configuracionServicio;
            _clienteServicio = clienteServicio;
            _empleadoServicio = empleadoServicio;
            _listaPrecioServicio = listaPrecioServicio;
            _puestoDeTrabajo = puestoDeTrabajo;
            _contadorServicio = contadorServicio;
            _articuloServicio = articuloServicio;
            _facturaServicio = facturaServicio;


            txtCodigo.KeyPress += delegate(object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoLetras(sender, args);

            };

            _clienteSeleccionado = null;
            _vendedorSeleccionado = null;
            _articuloSeleccionado = null;
            _itemSeleccionado = null;
            _rubroActual = null;

            _permiteAgregarPorCantidad = false;
            _articuloConPrecioAlternativo = false;
            _autorizaPermisoListaPrecio = false;
            _ingresoPorCodigoBascula = false;
            _cambiarCantidadConErrorPorValidacion = false;

            _factura = new FacturaView();

            _configuracion = _configuracionServicio.Obtener();

            if (_configuracion == null)
            {
                MessageBox.Show("Previamente cargue la configuracion del sistema");
                Close();
            }
        }

        private void _00050_Venta_Load(object sender, System.EventArgs e)
        {
            txtDescripcion.Enabled = false;
            txtPrecioUnitario.Enabled = false;
            nudCantidad.Enabled = false;

            CargarCabecera();
            CargarCuerpo();
            CargarPie();
        }

        private void CargarCabecera()
        {
            lblFechaActual.Text = DateTime.Today.ToShortDateString();


            // ==================     Cliente    ===================//

            _clienteSeleccionado = ObtenerConsumidorFinal();

            AsignarDatosCliente(_clienteSeleccionado);

            // =============   Tipo de Comprobante    ==============//

            PoblarComboBox(cmbTipoComprobante, Enum.GetValues(typeof(TipoComprobante)));
            cmbTipoComprobante.SelectedItem = TipoComprobante.B; // Por defecto

            //================================================================================================//

            PoblarComboBox(cmbPuestoVenta, _puestoDeTrabajo.Obtener(string.Empty, false),
                "Descripcion", "Id");

            //================================================================================================//

            PoblarComboBox(cmbListaPrecio, _listaPrecioServicio.Obtener(string.Empty, false),
                "Descripcion", "Id");

            cmbListaPrecio.SelectedValue = _configuracion.ListaPrecioPorDefectoId;

            //================================================================================================//

            //var vendedor = ObtenerVendedorPorDefecto();

            _vendedorSeleccionado = ObtenerVendedorPorDefecto();

            AsignarDatosVendedor(_vendedorSeleccionado);

        }

        private void CargarCuerpo()
        {
            dgvGrilla.DataSource = _factura.Items.ToList();

            FormatearGrilla(dgvGrilla);

            var ultimoItem = _factura.Items.LastOrDefault();

            if (ultimoItem == null)
            {
                lblDescripcion.Text = string.Empty;
                lblPrecioPorCantidad.Text = string.Empty;
            }
            else
            {
                lblDescripcion.Text = ultimoItem.Descripcion;
                lblPrecioPorCantidad.Text = $"{ultimoItem.Cantidad} X {ultimoItem.Precio} = {ultimoItem.SubTotal}";
            }

        }

        private void CargarPie()
        {
            txtSubTotal.Text = _factura.SubTotalStr;
            nudDescuento.Value = _factura.Descuento;
            txtTotal.Text = _factura.TotalStr;
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["CodigoBarra"].Visible = true;
            dgv.Columns["CodigoBarra"].Width = 100;
            dgv.Columns["CodigoBarra"].HeaderText = "Código";
            dgv.Columns["CodigoBarra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].HeaderText = "Articulo";
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["Iva"].Visible = true;
            dgv.Columns["Iva"].Width = 100;
            dgv.Columns["Iva"].HeaderText = "Iva";
            dgv.Columns["Iva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns["PrecioStr"].Visible = true;
            dgv.Columns["PrecioStr"].Width = 120;
            dgv.Columns["PrecioStr"].HeaderText = "Precio";
            dgv.Columns["PrecioStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns["Cantidad"].Visible = true;
            dgv.Columns["Cantidad"].Width = 120;
            dgv.Columns["Cantidad"].HeaderText = "Cantidad";
            dgv.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["SubTotalStr"].Visible = true;
            dgv.Columns["SubTotalStr"].Width = 120;
            dgv.Columns["SubTotalStr"].HeaderText = "Sub-Total";
            dgv.Columns["SubTotalStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            var fLoockUpCliente = ObjectFactory.GetInstance<ClienteLookUp>();
            fLoockUpCliente.ShowDialog();

            if ((ClienteDto) fLoockUpCliente.EntidadSeleccionada != null)
            {
                _clienteSeleccionado = (ClienteDto) fLoockUpCliente.EntidadSeleccionada;

                AsignarDatosCliente((ClienteDto) fLoockUpCliente.EntidadSeleccionada);
            }
            else
            {
                AsignarDatosCliente(ObtenerConsumidorFinal());
            }
        }

        private void btnBuscarVendedor_Click(object sender, EventArgs e)
        {
            var fLookUpVendedor = ObjectFactory.GetInstance<EmpleadoLookUp>();
            fLookUpVendedor.ShowDialog();

            if ((EmpleadoDto) fLookUpVendedor.EntidadSeleccionada != null)
            {
                _vendedorSeleccionado = (EmpleadoDto) fLookUpVendedor.EntidadSeleccionada;
                AsignarDatosVendedor((EmpleadoDto) fLookUpVendedor.EntidadSeleccionada);
            }
            else
            {
                AsignarDatosVendedor(ObtenerVendedorPorDefecto());
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (txtCodigo.Text.Contains("*"))
                    {
                        if (AsignarArticuloAlternativo(txtCodigo.Text))
                        {
                            btnAgregar.PerformClick();
                            return;
                        }
                    }

                    if (txtCodigo.Text.Length == 13)
                    {
                        if (_configuracion.ActivarBascula
                            && _configuracion.CodigoBascula == txtCodigo.Text.Substring(0, 4))
                        {
                            if (AsignarArticuloPorBascula(txtCodigo.Text))
                            {
                                btnAgregar.PerformClick();
                                return;
                            }
                        }
                        else
                        {
                            _articuloSeleccionado = _articuloServicio.ObtenerPorCodigo(txtCodigo.Text,
                                (long)cmbListaPrecio.SelectedValue,
                                _configuracion.DepositoVentaId);
                        }
                    }
                    else
                    {
                        _articuloSeleccionado = _articuloServicio.ObtenerPorCodigo(txtCodigo.Text,
                            (long)cmbListaPrecio.SelectedValue,
                            _configuracion.DepositoVentaId);
                    }

                    if (_articuloSeleccionado != null)
                    {
                        if (_permiteAgregarPorCantidad)
                        {
                            txtCodigo.Text = _articuloSeleccionado.CodigoBarra;
                            txtDescripcion.Text = _articuloSeleccionado.Descripcion;
                            txtPrecioUnitario.Text = _articuloSeleccionado.PrecioStr;
                            nudCantidad.Focus();
                            nudCantidad.Select(0, nudCantidad.Text.Length);
                            return;
                        }
                        else
                        {
                            btnAgregar.PerformClick();
                        }
                    }
                    else
                    {
                        LimpiarParaNuevoItem();
                    }
                }
            }

            e.Handled = false;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                // F5
                case 116:
                    _permiteAgregarPorCantidad = !_permiteAgregarPorCantidad;
                    nudCantidad.Enabled = _permiteAgregarPorCantidad;
                    break;
                // F8
                case 119:

                    var lookUpArticulo = new ArticuloLookUp((long) cmbListaPrecio.SelectedValue);
                    lookUpArticulo.ShowDialog();

                    if (lookUpArticulo.EntidadSeleccionada != null)
                    {
                        _articuloSeleccionado = (ArticuloVentaDto) lookUpArticulo.EntidadSeleccionada;

                        if (_permiteAgregarPorCantidad)
                        {
                            txtCodigo.Text = _articuloSeleccionado.CodigoBarra;
                            txtDescripcion.Text = _articuloSeleccionado.Descripcion;
                            txtPrecioUnitario.Text = _articuloSeleccionado.PrecioStr;
                            nudCantidad.Focus();
                            nudCantidad.Select(0, nudCantidad.Text.Length);
                            return;
                        }
                        else
                        {
                            btnAgregar.PerformClick();
                            LimpiarParaNuevoItem();
                        }
                    }
                    else
                    {
                        LimpiarParaNuevoItem();
                    }

                    break;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        { 
            if (_articuloSeleccionado != null)
            {
                var listaPrecioSeleccionada = (ListaPrecioDto)cmbListaPrecio.SelectedItem;

                if (listaPrecioSeleccionada.NecesitaAutorizacion)
                {
                    if (!_autorizaPermisoListaPrecio)
                    {
                        var fAutorizacion = ObjectFactory.GetInstance<AutorizacionListaPrecio>();
                        fAutorizacion.ShowDialog();

                        if (!fAutorizacion.PermisoAutorizado) return;

                        _autorizaPermisoListaPrecio = fAutorizacion.PermisoAutorizado;
                        AgregarItem(_articuloSeleccionado, (long)cmbListaPrecio.SelectedValue, nudCantidad.Value, _rubroActual);
                    }
                    else
                    {
                        AgregarItem(_articuloSeleccionado, (long)cmbListaPrecio.SelectedValue, nudCantidad.Value, _rubroActual);
                    }
                }
                else
                {
                    AgregarItem(_articuloSeleccionado, (long)cmbListaPrecio.SelectedValue, nudCantidad.Value, _rubroActual);
                }
            }

            LimpiarParaNuevoItem();
            CargarCuerpo();
            CargarPie();
        }

        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount > 0)
            {
                _itemSeleccionado = (ItemView) dgvGrilla.Rows[e.RowIndex].DataBoundItem;
            }
            else
            {
                _itemSeleccionado = null;
            }
        }

        private void dgvGrilla_DoubleClick(object sender, EventArgs e)
        {
            if (dgvGrilla.RowCount <= 0) return;

            var respaldoItem = _itemSeleccionado;
            var cantidadRespaldo = _itemSeleccionado.Cantidad;

            if (respaldoItem.IngresoPorBascula || respaldoItem.EsArticuloAlternativo)
            {
                MessageBox.Show("No se permite cambiar la cantidad.");
                return;
            }

            var cambiarCantidadItem = new CambiarCantidad(_itemSeleccionado);
            cambiarCantidadItem.ShowDialog();

            if (cambiarCantidadItem.Item != null)
            {
                var item = _factura.Items.FirstOrDefault(x => x.Id == cambiarCantidadItem.Item.Id);
                _factura.Items.Remove(item);

                if (cambiarCantidadItem.Item.Cantidad > 0)
                {
                    _articuloSeleccionado = _articuloServicio
                        .ObtenerPorCodigo(_itemSeleccionado.CodigoBarra, _itemSeleccionado.ListaPrecioId, _configuracion.DepositoVentaId);

                    nudCantidad.Value = cambiarCantidadItem.Item.Cantidad;

                    btnAgregar.PerformClick();

                    if (_cambiarCantidadConErrorPorValidacion)
                    {
                        respaldoItem.Cantidad = cantidadRespaldo;
                        _factura.Items.Add(respaldoItem);
                        _cambiarCantidadConErrorPorValidacion = false;
                    }
                }
            }

            LimpiarParaNuevoItem();
            CargarCuerpo();
            CargarPie();
        }

        private void btnEliminarItem_Click(object sender, EventArgs e)
        {
            if (dgvGrilla.RowCount <= 0) return;

            if (MessageBox.Show($"¿Esta seguro de Eliminar el Item {_itemSeleccionado.Descripcion}?", "Ateción",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                _factura.Items.Remove(_itemSeleccionado);
                LimpiarParaNuevoItem();
                CargarCuerpo();
                CargarPie();
            }
        }

        //===================================================================================================================//
        //==========================================    Metodos Privados    =================================================//
        //===================================================================================================================//

        private void AsignarDatosVendedor(EmpleadoDto vendedor)
        {
            txtVendedor.Text = vendedor.ApyNom;
        }

        private ClienteDto ObtenerConsumidorFinal()
        {
            return (ClienteDto)_clienteServicio.Obtener(typeof(ClienteDto),
                Aplicacion.Constantes.Cliente.ConsumidorFinal, false).FirstOrDefault();
        }

        private EmpleadoDto ObtenerVendedorPorDefecto()
        {
            return (EmpleadoDto)_empleadoServicio.Obtener(typeof(EmpleadoDto), Identidad.EmpleadoId);
        }

        private void AsignarDatosCliente(ClienteDto cliente)
        {
            txtDniCliente.Text = cliente.Dni;
            txtCliente.Text = cliente.ApyNom;
            txtDomicilioCliente.Text = cliente.Direccion;
            txtCondicionIvaCliente.Text = cliente.CondicionIva;
            txtTelefonoCliente.Text = cliente.Telefono;
        }

        private void AgregarItem(ArticuloVentaDto articulo, long listaPrecioId, decimal cantidad, RubroDto rubro)
        {
            // Limite de Venta por cantidad
            if (articulo.TieneRestriccionPorCantidad)
            {
                var totalArticulosItems = _factura.Items
                    .Where(x => x.ArticuloId == articulo.Id)
                    .Sum(x => x.Cantidad);

                if (cantidad + totalArticulosItems > articulo.Limite)
                {
                    _cambiarCantidadConErrorPorValidacion = true;

                    var mensajeLimiteVenta = $"El articulo {articulo.Descripcion.ToUpper()} tiene una restricción"
                                  + Environment.NewLine
                                  + $"de Venta por una Cantidad Maxima de {articulo.Limite}.";

                    MessageBox.Show(mensajeLimiteVenta, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            

            if (articulo.TieneRestriccionHorario)
            {
                if (VerificarLimiteHorarioVenta(articulo.HoraDesde, articulo.HoraHasta))
                {
                    _cambiarCantidadConErrorPorValidacion = true;

                    var mensajeLimiteHorario = $"El articulo {articulo.Descripcion.ToUpper()} tiene una restricción"
                                             + Environment.NewLine
                                             + $"de Venta por horario entre {articulo.HoraDesde.ToShortTimeString()} hasta {articulo.HoraHasta.ToShortTimeString()}.";

                    MessageBox.Show(mensajeLimiteHorario, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            if (!articulo.PermiteStockNegativo)
            {
                if (!VerificarStock(articulo, nudCantidad.Value))
                {
                    _cambiarCantidadConErrorPorValidacion = true;

                    var mensajeStock = $"No hay Stock suficiente para el articulo {articulo.Descripcion.ToUpper()}"
                                       + Environment.NewLine
                                       + $"Stock Actual disponible: {articulo.Stock}.";

                    MessageBox.Show(mensajeStock, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            if (_articuloConPrecioAlternativo || _ingresoPorCodigoBascula)
            {
                _factura.Items.Add(AsignarDatosItem(articulo, listaPrecioId, cantidad));
            }
            else
            {
                if (_configuracion.UnificarRenglonesIngresarMismoProducto)
                {
                    var item = _factura.Items.FirstOrDefault(x => x.ArticuloId == articulo.Id
                                                                  && x.ListaPrecioId == listaPrecioId
                                                                  && !x.EsArticuloAlternativo 
                                                                  && !x.IngresoPorBascula);

                    if (item == null || item.EsArticuloAlternativo || item.IngresoPorBascula) // Primera vez por ingresar
                    {
                        _factura.Items.Add(AsignarDatosItem(articulo, listaPrecioId, cantidad));
                    }
                    else
                    {
                        item.Cantidad += cantidad;
                    }
                }
                else
                {
                    _factura.Items.Add(AsignarDatosItem(articulo, listaPrecioId, cantidad));
                }
            }
        }

        private ItemView AsignarDatosItem(ArticuloVentaDto articulo, long listaPrecioId, decimal cantidad)
        {
            _factura.ContadorItem++;

            return new ItemView
            {
                Id = _factura.ContadorItem,
                Descripcion = articulo.Descripcion,
                Iva = articulo.Iva,
                Precio = articulo.Precio,
                CodigoBarra = articulo.CodigoBarra,
                Cantidad = cantidad,
                ListaPrecioId = listaPrecioId,
                ArticuloId = articulo.Id,
                EsArticuloAlternativo = _articuloConPrecioAlternativo,
                IngresoPorBascula = _ingresoPorCodigoBascula
            };
        }

        private bool VerificarStock(ArticuloVentaDto articulo, decimal cantidad)
        {
            var totalArticulosItems = _factura.Items
                .Where(x => x.ArticuloId == articulo.Id)
                .Sum(x => x.Cantidad);

            return totalArticulosItems + cantidad <= articulo.Stock;
        }

        private bool VerificarLimiteHorarioVenta(DateTime limiteHoraDesde, DateTime limiteHoraHasta)
        {
            var _horaDesdeSistema = DateTime.Now.Hour;
            var _minutoDesdeSistema = DateTime.Now.Minute;

            var _horaDesdeInicioDia = 0;
            var _minutoDesdeInicioDia = 0;

            var _horaDesdeFinDia = 23;
            var _minutoDesdeFinDia = 59;

            if (limiteHoraDesde <= limiteHoraHasta) // Mismo dia
            {
                //return _horaDesdeSistema >= limiteHoraDesde.Hour && _minutoDesdeSistema >= limiteHoraDesde.Minute
                //                                                 && _horaDesdeSistema <= limiteHoraHasta.Hour
                //                                                 && _minutoDesdeSistema <= limiteHoraHasta.Minute;

                if (_horaDesdeSistema >= limiteHoraDesde.Hour && _minutoDesdeSistema >= limiteHoraDesde.Minute)
                {
                    if (_horaDesdeSistema < limiteHoraHasta.Hour)
                    {
                        return true;
                    }
                    else if(_horaDesdeSistema == limiteHoraHasta.Hour && _minutoDesdeSistema <= limiteHoraHasta.Minute)
                    {
                        return true;
                    }
                }
            }
            else // Dias diferentes -> Ej: 23:00 hasta 06:00 AM
            {
                if (_horaDesdeSistema >= limiteHoraDesde.Hour)
                {
                    // Rango 1
                    return _horaDesdeSistema >= limiteHoraDesde.Hour && _minutoDesdeSistema >= limiteHoraDesde.Minute
                                                                     && _horaDesdeSistema <= _horaDesdeFinDia
                                                                     && _minutoDesdeSistema <= _minutoDesdeFinDia;
                }
                else
                {
                    // Rango 2

                    if (_horaDesdeSistema >= _horaDesdeInicioDia && _minutoDesdeSistema >= _minutoDesdeInicioDia)
                    {
                        if (_horaDesdeSistema < limiteHoraHasta.Hour)
                        {
                            return true;
                        }
                        else if(_horaDesdeSistema == limiteHoraHasta.Hour && _minutoDesdeSistema <= limiteHoraHasta.Minute)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        private void nudCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAgregar.PerformClick();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var mensajeCancelar = "Hay articulos cargados en la lista"
                                  + Environment.NewLine
                                  + "Desea Cancelar la Venta";

            if (MessageBox.Show(mensajeCancelar, "Atención",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                LimpiarParaNuevaFactura();
            }
        }

        //==========================================================================================================//
        //=========================================    Metodos Privados    =========================================//
        //==========================================================================================================//

        private bool AsignarArticuloPorBascula(string codigoBascula)
        {
            decimal _precioBascula = 0;
            decimal _pesoBascula = 0;

            _ingresoPorCodigoBascula = true;

            int.TryParse(codigoBascula.Substring(4, 3), out int codigoArticulo);

            var precioPesoArticulo = codigoBascula.Substring(7, 5);

            if (_configuracion.EsImpresionPorPrecio)
            {
                if (!decimal.TryParse(precioPesoArticulo.Insert(3, ","), NumberStyles.Number,
                    new CultureInfo("es-Ar"), out _precioBascula))
                {
                    return false;
                }
            }
            else
            {
                if (!decimal.TryParse(precioPesoArticulo.Insert(2, ","), NumberStyles.Number,
                    new CultureInfo("es-Ar"), out _pesoBascula))
                {
                    return false;
                }
            }

            _articuloSeleccionado = _articuloServicio.ObtenerPorCodigo(codigoArticulo.ToString()
                , (long)cmbListaPrecio.SelectedValue, _configuracion.DepositoVentaId);

            if (_articuloSeleccionado != null)
            {
                if (_configuracion.EsImpresionPorPrecio)
                {
                    _articuloSeleccionado.Precio = _precioBascula;
                }
                else
                {
                    nudCantidad.Value = _pesoBascula;
                }
            }

            return false;
        }

        private bool AsignarArticuloAlternativo(string codigo)
        {
            _articuloConPrecioAlternativo = true;

            if (codigo.Length < 3)
                return false;

            var codigoArticulo = codigo.Substring(0, codigo.IndexOf('*'));

            if (!string.IsNullOrEmpty(codigoArticulo))
            {
                _articuloSeleccionado = _articuloServicio.ObtenerPorCodigo(codigoArticulo,
                    (long) cmbListaPrecio.SelectedValue, _configuracion.DepositoVentaId);

                if (_articuloSeleccionado != null)
                {
                    var precioAlternativo = codigo.Substring(codigo.IndexOf('*') + 1);

                    if (!string.IsNullOrEmpty(precioAlternativo))
                    {
                        if (decimal.TryParse(precioAlternativo, out decimal precioNuevo))
                        {
                            _articuloSeleccionado.Precio = precioNuevo;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return false;
        }

        private void LimpiarParaNuevoItem()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtPrecioUnitario.Clear();
            nudCantidad.Value = 1;
            nudCantidad.Enabled = false;
            _permiteAgregarPorCantidad = false;
            _articuloConPrecioAlternativo = false;
            _ingresoPorCodigoBascula = false;
            _articuloSeleccionado = null;
            txtCodigo.Focus();
        }

        private void LimpiarParaNuevaFactura()
        {
            _factura = new FacturaView();
            CargarCabecera();
            CargarCuerpo();
            CargarPie();
            LimpiarParaNuevoItem();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            // Si el puesto de trabajo esta separado o no 

            _factura.Cliente = _clienteSeleccionado;
            _factura.Vendedor = _vendedorSeleccionado;
            _factura.TipoComprobante = (TipoComprobante) cmbTipoComprobante.SelectedItem;
            _factura.PuntoVentaId = (long) cmbPuestoVenta.SelectedValue;
            _factura.UsuarioId = Identidad.UsuarioId;

            if (_configuracion.PuestoCajaSeparado)
            {
                try
                {
                    var nuevoComprobante = new FacturaDto()
                    {
                        EmpleadoId = _factura.Vendedor.Id,
                        TipoComprobante = _factura.TipoComprobante,
                        Fecha = DateTime.Now,
                        Descuento = _factura.Descuento,
                        SubTotal = _factura.SubTotal,
                        Iva21 = 0,
                        Iva105 = 0,
                        Total = _factura.Total,
                        UsuarioId = _factura.UsuarioId,
                        ClienteId = _factura.Cliente.Id,
                        Estado = Estado.Pendiente,
                        PuestoTrabajoId = _factura.PuntoVentaId,
                        VieneVentas = true,
                        Eliminado = false,
                    };

                    foreach (var item in _factura.Items)
                    {
                        nuevoComprobante.Items.Add(new DetalleComprobanteDto
                        {
                            Cantidad = item.Cantidad,
                            Precio = item.Precio,
                            Descripcion = item.Descripcion,
                            SubTotal = item.SubTotal,
                            Iva = item.Iva,
                            ArticuloId = item.ArticuloId,
                            Codigo = item.CodigoBarra,
                            Eliminado = false,
                        });
                    }

                    _facturaServicio.Insertar(nuevoComprobante);

                    MessageBox.Show("Los datos se grabaron correctamente");
                    LimpiarParaNuevaFactura();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else
            {
                // Forma Pago

                var fFormaDePago = new _00044_FormaPago(_factura);
                fFormaDePago.ShowDialog();

                if (fFormaDePago.RealizoVenta)
                {
                    LimpiarParaNuevaFactura();
                    txtCodigo.Focus();
                }
            }
        }
    }
}
