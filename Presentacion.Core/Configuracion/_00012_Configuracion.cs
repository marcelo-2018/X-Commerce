﻿using System;
using System.Linq;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicio.Configuracion;
using IServicio.Configuracion.DTOs;
using IServicio.Departamento;
using IServicio.Deposito;
using IServicio.ListaPrecio;
using IServicio.Localidad;
using IServicio.Provincia;
using IServicios.DepositoVenta;
using Presentacion.Core.Articulo;
using Presentacion.Core.Departamento;
using Presentacion.Core.Localidad;
using Presentacion.Core.Provincia;
using PresentacionBase.Formularios;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;


namespace Presentacion.Core.Configuracion
{
    public partial class _00012_Configuracion : FormBase
    {
        private readonly IProvinciaServicio _provinciaServicio;
        private readonly IDepartamentoServicio _departamentoServicio;
        private readonly ILocalidadServicio _localidadServicio;
        private readonly IListaPrecioServicio _listaPrecioServicio;
        private readonly IConfiguracionServicio _configuracionServicio;
        private readonly IDepositoSevicio _depositoSevicio;

        private ConfiguracionDto configuracion;

        public _00012_Configuracion(IProvinciaServicio provinciaServicio,
            IDepartamentoServicio departamentoServicio,
            ILocalidadServicio localidadServicio,
            IListaPrecioServicio listaPrecioServicio,
            IConfiguracionServicio configuracionServicio,
            IDepositoSevicio depositoSevicio)
        {
            InitializeComponent();

            _provinciaServicio = provinciaServicio;
            _departamentoServicio = departamentoServicio;
            _localidadServicio = localidadServicio;
            _listaPrecioServicio = listaPrecioServicio;
            _configuracionServicio = configuracionServicio;
            _depositoSevicio = depositoSevicio;

            var depositos = _depositoSevicio.Obtener(string.Empty, false);

            PoblarComboBox(cmbTipoPagoCompraPorDefecto, Enum.GetValues(typeof(TipoPago)));

            PoblarComboBox(cmbTipoPagoPorDefecto, Enum.GetValues(typeof(TipoPago)));

            PoblarComboBox(cmbListaPrecioDefecto,
                _listaPrecioServicio.Obtener(string.Empty, false),
                "Descripcion",
                "Id");

            PoblarComboBox(cmbDeposito, depositos,
                "Descripcion"
                , "Id");

            PoblarComboBox(cmbDepositoVenta, depositos,
                "Descripcion", "Id");

            AsignarEvento_EnterLeave(this);

            CargarValidaciones();
        }

        private void CargarValidaciones()
        {
            txtCUIL.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtTelefono.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtCelular.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };
        }

        private void _00012_Configuracion_Load(object sender, System.EventArgs e)
        {
            configuracion = _configuracionServicio.Obtener();

            if (configuracion != null)
            {
                // Modificar
                configuracion.EsPrimeraVez = false;

                // ================================================= //
                // ==========    Datos de la Empresa       ========= //
                // ================================================= //

                txtRazonSocial.Text = configuracion.RazonSocial;
                txtNombreFantasia.Text = configuracion.NombreFantasia;
                txtCUIL.Text = configuracion.Cuit;
                txtTelefono.Text = configuracion.Telefono;
                txtCelular.Text = configuracion.Celular;
                txtDireccion.Text = configuracion.Direccion;

                PoblarComboBox(cmbProvincia, _provinciaServicio
                        .Obtener(string.Empty, false),
                    "Descripcion", "Id");

                cmbProvincia.SelectedValue = configuracion.ProvinciaId;

                PoblarComboBox(cmbDepartamento,
                    _departamentoServicio.ObtenerPorProvincia(configuracion.ProvinciaId),
                    "Descripcion"
                    , "Id");

                cmbDepartamento.SelectedValue = configuracion.DepartamentoId;

                PoblarComboBox(cmbLocalidad,
                    _localidadServicio.ObtenerPorDepartamento(configuracion.DepartamentoId)
                    , "Descripcion"
                    , "Id");

                cmbLocalidad.SelectedValue = configuracion.LocalidadId;

                txtEmail.Text = configuracion.Email;

                // ================================================= //
                // ==========    Datos del  Stock          ========= //
                // ================================================= //

                chkFacturaDescuentaStock.Checked = configuracion.FacturaDescuentaStock;
                chkPresupuestoDescuentaStock.Checked = configuracion.PresupuestoDescuentaStock;
                chkRemitoDescuentaStock.Checked = configuracion.RemitoDescuentaStock;
                chkActualizaCostoDesdeCompra.Checked = configuracion.ActualizaCostoDesdeCompra;
                chkModificaPrevioVentaDesdeCompra.Checked = configuracion.ModificaPrecioVentaDesdeCompra;
                cmbTipoPagoCompraPorDefecto.SelectedItem = configuracion.TipoFormaPagoPorDefectoCompra;
                cmbDeposito.SelectedValue = configuracion.DepositoStockId;

                // ================================================= //
                // ==========    Datos de la Venta         ========= //
                // ================================================= //

                txtObservacionFactura.Text = configuracion.ObservacionEnPieFactura;
                cmbListaPrecioDefecto.SelectedValue = configuracion.ListaPrecioPorDefectoId;
                chkRenglonesFactura.Checked = configuracion.UnificarRenglonesIngresarMismoProducto;
                cmbTipoPagoPorDefecto.SelectedItem = configuracion.TipoFormaPagoPorDefectoVenta;
                chkActivarBascula.Checked = configuracion.ActivarBascula;
                txtCodigoBascula.Text = configuracion.CodigoBascula;
                cmbDepositoVenta.SelectedItem = configuracion.DepositoVentaId;

                // ================================================= //
                // ==========    Datos de la Caja          ========= //
                // ================================================= //

                if (configuracion.IngresoManualCajaInicial)
                {
                    rdbIngresoManualCaja.Checked = true;
                }
                else
                {
                    rdbIngresoPorCierreDelDIaAnterior.Checked = true;
                }

                chkPuestoSeparado.Checked = configuracion.PuestoCajaSeparado;
                chkRetiroDineroCaja.Checked = configuracion.ActivarRetiroDeCaja;
                nudMontoMaximo.Value = configuracion.MontoMaximoRetiroCaja;

                // ================================================= //
                // ==========    Datos de la Caja          ========= //
                // ================================================= //

                chkActivarBascula.Checked = configuracion.ActivarBascula;
                txtCodigoBascula.Text = configuracion.CodigoBascula;
                if (configuracion.EsImpresionPorPrecio)
                {
                    rdbEtiquetaPorPrecio.Checked = true;
                }
                else
                {
                    rdbEtiquetaPorPeso.Checked = true;
                }
            }
            else
            {
                // Nuevo
                btnEjecutar.Text = "Grabar";

                configuracion = new ConfiguracionDto();
                configuracion.EsPrimeraVez = true;

                LimpiarControles(this);

                var provincias = _provinciaServicio.Obtener(string.Empty, false);

                PoblarComboBox(cmbProvincia,
                    provincias,
                    "Descripcion",
                    "Id");

                var deposito = _depositoSevicio.Obtener(string.Empty, false);

                PoblarComboBox(cmbDeposito, deposito, "Descripcion", "Id");

                if (provincias.Any())
                {
                    var departamentos = _departamentoServicio
                        .ObtenerPorProvincia((long)cmbProvincia.SelectedValue);

                    PoblarComboBox(cmbDepartamento,
                        departamentos,
                        "Descripcion",
                        "Id");

                    if (departamentos.Any())
                    {
                        var localidades =
                            _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue);

                        PoblarComboBox(cmbLocalidad,
                            localidades,
                            "Descripcion",
                            "Id");
                    }

                    txtRazonSocial.Focus();
                }
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (VerificarDatosObligatorios())
            {
                // ================================================= //
                // ==========    Datos de la Empresa       ========= //
                // ================================================= //

                configuracion.RazonSocial = txtRazonSocial.Text;
                configuracion.NombreFantasia = txtNombreFantasia.Text;
                configuracion.Cuit = txtCUIL.Text;
                configuracion.Telefono = txtTelefono.Text;
                configuracion.Celular = txtCelular.Text;
                configuracion.Direccion = txtDireccion.Text;
                configuracion.ProvinciaId = (long) cmbProvincia.SelectedValue;
                configuracion.DepartamentoId = (long) cmbDepartamento.SelectedValue;
                configuracion.LocalidadId = (long)cmbLocalidad.SelectedValue;
                configuracion.Email = txtEmail.Text;

                // ================================================= //
                // ==========    Datos del  Stock          ========= //
                // ================================================= //

                configuracion.FacturaDescuentaStock = chkFacturaDescuentaStock.Checked;
                configuracion.PresupuestoDescuentaStock = chkPresupuestoDescuentaStock.Checked;
                configuracion.RemitoDescuentaStock = chkRemitoDescuentaStock.Checked;
                configuracion.ActualizaCostoDesdeCompra = chkActualizaCostoDesdeCompra.Checked;
                configuracion.ModificaPrecioVentaDesdeCompra = chkModificaPrevioVentaDesdeCompra.Checked;
                configuracion.TipoFormaPagoPorDefectoCompra = (TipoPago)cmbTipoPagoCompraPorDefecto.SelectedItem;
                configuracion.DepositoStockId = (long)cmbDeposito.SelectedValue;

                // ================================================= //
                // ==========    Datos de la Venta         ========= //
                // ================================================= //

                configuracion.ObservacionEnPieFactura = txtObservacionFactura.Text;
                configuracion.ListaPrecioPorDefectoId = (long)cmbListaPrecioDefecto.SelectedValue;
                configuracion.UnificarRenglonesIngresarMismoProducto = chkRenglonesFactura.Checked;
                configuracion.TipoFormaPagoPorDefectoVenta = (TipoPago)cmbTipoPagoPorDefecto.SelectedItem;
                configuracion.DepositoVentaId = (long)cmbDepositoVenta.SelectedValue;

                // ================================================= //
                // ==========    Datos de la Caja          ========= //
                // ================================================= //

                configuracion.IngresoManualCajaInicial = rdbIngresoManualCaja.Checked;

                configuracion.PuestoCajaSeparado = chkPuestoSeparado.Checked;
                configuracion.ActivarRetiroDeCaja = chkRetiroDineroCaja.Checked;
                configuracion.MontoMaximoRetiroCaja = nudMontoMaximo.Value;

                // ================================================= //
                // ==========             Bascula          ========= //
                // ================================================= //

                configuracion.ActivarBascula = chkActivarBascula.Checked;
                configuracion.CodigoBascula = txtCodigoBascula.Text.PadLeft(4, '0');
                configuracion.EsImpresionPorPrecio = rdbEtiquetaPorPrecio.Checked;

                _configuracionServicio.Grabar(configuracion);

                MessageBox.Show("Los Datos se grabaron correctamente");
                Close();
            }
            else
            {
                MessageBox.Show("Por favor ingrese los datos Obligatorios");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea limpiar los datos cargados", "Atención", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                LimpiarControles(this);
            }
        }

        private bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtRazonSocial.Text)) return false;

            if (string.IsNullOrEmpty(txtCUIL.Text)) return false;

            if (string.IsNullOrEmpty(txtDireccion.Text)) return false;

            if (cmbLocalidad.Items.Count <= 0) return false;

            if (cmbListaPrecioDefecto.Items.Count <= 0) return false;

            return true;
        }

        private void cmbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbProvincia.Items.Count <= 0) return;

            PoblarComboBox(cmbDepartamento,
                _departamentoServicio.ObtenerPorProvincia((long)cmbProvincia.SelectedValue), "Descripcion", "Id");

            if (cmbDepartamento.Items.Count <= 0) return;

            PoblarComboBox(cmbLocalidad,
                _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue), "Descripcion", "Id");
        }

        private void cmbDepartamento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbDepartamento.Items.Count <= 0) return;

            PoblarComboBox(cmbLocalidad,
                _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue), "Descripcion", "Id");
        }

        private void btnNuevaListaPrecios_Click(object sender, EventArgs e)
        {
            var form = new _00033_Abm_ListaPrecio(TipoOperacion.Nuevo);
            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbListaPrecioDefecto, _listaPrecioServicio
                    .Obtener(string.Empty), "Descripcion", "Id");
            }
        }

        private void btnNuevaProvincia_Click(object sender, EventArgs e)
        {
            var form = new _00002_Abm_Provincia(TipoOperacion.Nuevo);
            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbProvincia,
                    _provinciaServicio.Obtener(string.Empty),
                    "Descripcion",
                    "Id");

                PoblarComboBox(cmbDepartamento,
                    _departamentoServicio
                        .ObtenerPorProvincia((long)cmbProvincia.SelectedValue),
                    "Descripcion",
                    "Id");

                if (cmbDepartamento.Items.Count <= 0) return;

                PoblarComboBox(cmbLocalidad,
                    _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue),
                    "Descripcion",
                    "Id");
            }
        }

        private void btnNuevoDepartamento_Click(object sender, EventArgs e)
        {
            var form = new _00004_Abm_Departamento(TipoOperacion.Nuevo);
            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbDepartamento,
                    _departamentoServicio
                        .ObtenerPorProvincia((long)cmbProvincia.SelectedValue),
                    "Descripcion",
                    "Id");

                if (cmbDepartamento.Items.Count <= 0) return;

                PoblarComboBox(cmbLocalidad,
                    _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue),
                    "Descripcion",
                    "Id");
            }
        }

        private void btnNuevaLocalidad_Click(object sender, EventArgs e)
        {
            var form = new _00006_AbmLocalidad(TipoOperacion.Nuevo);
            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbLocalidad,
                    _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue),
                    "Descripcion",
                    "Id");
            }
        }

        private void btnNuevoDeposito_Click(object sender, EventArgs e)
        {
            var form = new _00035_Abm_Deposito(TipoOperacion.Nuevo);

            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbDepositoVenta,
                    _depositoSevicio.Obtener(string.Empty, false),
                    "Descripcion",
                    "Id");

                PoblarComboBox(cmbDeposito,
                    _depositoSevicio.Obtener(string.Empty, false),
                    "Descripcion",
                    "Id");
            }
        }

        private void btnNuevoDepositoVenta_Click(object sender, EventArgs e)
        {
            var form = new _00035_Abm_Deposito(TipoOperacion.Nuevo);

            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbDepositoVenta,
                    _depositoSevicio.Obtener(string.Empty, false),
                    "Descripcion",
                    "Id");

                PoblarComboBox(cmbDeposito,
                    _depositoSevicio.Obtener(string.Empty, false),
                    "Descripcion",
                    "Id");
            }
        }

        private void chkActivarBascula_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivarBascula.Checked == false)
            {
                rdbEtiquetaPorPeso.Enabled = false;
                rdbEtiquetaPorPrecio.Enabled = false;
                txtCodigoBascula.Enabled = false;
                txtCodigoBascula.Clear();
            }
            else
            {
                rdbEtiquetaPorPeso.Enabled = true;
                rdbEtiquetaPorPrecio.Enabled = true;
                txtCodigoBascula.Enabled = true;
            }
        }

        private void chkRetiroDineroCaja_CheckedChanged(object sender, EventArgs e)
        {
            nudMontoMaximo.Enabled = chkRetiroDineroCaja.Checked;
        }
    }
}
