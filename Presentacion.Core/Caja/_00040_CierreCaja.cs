using PresentacionBase.Formularios;
using System;
using System.Linq;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicios.Caja;
using IServicios.Caja.DTOs;
using StructureMap;

namespace Presentacion.Core.Caja
{
    public partial class _00040_CierreCaja : FormBase
    {
        private readonly long _cajaId;

        private readonly ICajaServicio _cajaServicio;

        private CajaDto _caja;

        public bool CajaCerrada { get; set; }

        public _00040_CierreCaja(long cajaId)
        {
            InitializeComponent();

            _cajaId = cajaId;

            _cajaServicio = ObjectFactory.GetInstance<ICajaServicio>();

            CargarDatos(_cajaId);

            lblFecha.Text = _caja.FechaAperturaStr;
        }

        private void CargarDatos(long cajaId)
        {
            _caja = _cajaServicio.Obtener(cajaId);

            if (_caja == null)
            {
                MessageBox.Show("Ocurrio un error al obtener la caja");
                Close();
            }

            txtCajaInicial.Text = _caja.MontoAperturaStr;

            var efectivo = _caja.Detalles.Where(x => x.TipoPago == TipoPago.Efectivo)
                .Sum(x => x.Monto);

            var cheque = _caja.Detalles.Where(x => x.TipoPago == TipoPago.Cheque)
                .Sum(x => x.Monto)
                .ToString("C");

            var tarjeta = _caja.Detalles.Where(x => x.TipoPago == TipoPago.Tarjeta)
                .Sum(x => x.Monto)
                .ToString("C");

            var ctacte = _caja.Detalles.Where(x => x.TipoPago == TipoPago.CtaCte)
                .Sum(x => x.Monto)
                .ToString("C");

            

            nudTotalEfectivo.Value = efectivo;
            txtVentas.Text = efectivo.ToString("C");

            txtCheque.Text = cheque;
            txtTarjeta.Text = tarjeta;
            txtCtaCte.Text = ctacte;
        }

        private void btnVerDetalleVenta_Click(object sender, EventArgs e)
        {
            var fVerComprobantesCaja = new _00045_VerComprobantesCaja(_caja.Comprobantes);
            fVerComprobantesCaja.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            try
            {
                var fecha = DateTime.Now;

                _cajaServicio.Cerrar(_caja, Identidad.UsuarioId, nudTotalEfectivo.Value, fecha);

                MessageBox.Show("Los datos se grabaron correctamente.");

                CajaCerrada = true;
                Close();
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                throw new Exception("Ocurrio un error grave al grabar la Factura");
            }
        }
    }
}
