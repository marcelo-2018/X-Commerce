using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IServicios.Comprobante.DTOs;
using IServicios.Factura;
using PresentacionBase.Formularios;

namespace Presentacion.Core.FormaPago
{
    public partial class _00049_CobroDiferido : FormBase
    {
        private readonly IFacturaServicio _facturaServicio;

        private ComprobantePendienteDto comprobanteSeleccionado;

        public _00049_CobroDiferido(IFacturaServicio facturaServicio)
        {
            InitializeComponent();

            _facturaServicio = facturaServicio;

            comprobanteSeleccionado = null;

            dgvGrillaPedientePago.DataSource = new List<ComprobantePendienteDto>();
            FormatearGrilla(dgvGrillaPedientePago);

            dgvGrillaDetalleComprobante.DataSource = new List<DetallePendienteDto>();
            FormatearGrillaDetalle(dgvGrillaDetalleComprobante);

            // Libreria para que refresque cada 60 seg la grilla
            // con las facturas que estan pendientes de pago.
            //Observable.Interval(TimeSpan.FromSeconds(15))
            //    .ObserveOn(DispatcherScheduler.Current)
            //    .Subscribe(_ => { CargarDatos(); });

            CargarDatos();
        }

        private void CargarDatos()
        {
            dgvGrillaPedientePago.DataSource = null;
            dgvGrillaPedientePago.DataSource = _facturaServicio.ObtenerPendientesPago();

            FormatearGrilla(dgvGrillaPedientePago);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Numero"].Visible = true;
            dgv.Columns["Numero"].Width = 100;
            dgv.Columns["Numero"].HeaderText = @"Nro Comprobante";
            dgv.Columns["Numero"].DisplayIndex = 0;

            dgv.Columns["ClienteApyNom"].Visible = true;
            dgv.Columns["ClienteApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["ClienteApyNom"].HeaderText = @"Cliente";
            dgv.Columns["ClienteApyNom"].DisplayIndex = 1;

            dgv.Columns["MontoPagarStr"].Visible = true;
            dgv.Columns["MontoPagarStr"].Width = 150;
            dgv.Columns["MontoPagarStr"].HeaderText = @"Total";
            dgv.Columns["MontoPagarStr"].DisplayIndex = 2;
        }

        private void dgvGrillaPedientePago_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrillaPedientePago.RowCount <= 0)
            {
                comprobanteSeleccionado = null;
                return;
            }

            comprobanteSeleccionado = (ComprobantePendienteDto) dgvGrillaPedientePago.Rows[e.RowIndex].DataBoundItem;

            if (comprobanteSeleccionado == null) return;

            nudTotal.Value = comprobanteSeleccionado.MontoPagar;

            dgvGrillaDetalleComprobante.DataSource = null;
            dgvGrillaDetalleComprobante.DataSource = comprobanteSeleccionado.Items.ToList();

            FormatearGrillaDetalle(dgvGrillaDetalleComprobante);
        }

        private void FormatearGrillaDetalle(DataGridView dgv)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].Visible = false;
            }

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = @"Articulo";
            dgv.Columns["Descripcion"].DisplayIndex = 0;

            dgv.Columns["PrecioStr"].Visible = true;
            dgv.Columns["PrecioStr"].Width = 100;
            dgv.Columns["PrecioStr"].HeaderText = @"Precio";
            dgv.Columns["PrecioStr"].DisplayIndex = 1;

            dgv.Columns["Cantidad"].Visible = true;
            dgv.Columns["Cantidad"].Width = 100;
            dgv.Columns["Cantidad"].HeaderText = @"Cantidad";
            dgv.Columns["Cantidad"].DisplayIndex = 2;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvGrillaPedientePago_DoubleClick(object sender, EventArgs e)
        {
            var fFormaDePago = new _00044_FormaPago(comprobanteSeleccionado);
            fFormaDePago.ShowDialog();

            if (fFormaDePago.RealizoVenta)
            {
                //MessageBox.Show("Los datos se grabaron correctamente");
            }
        }
    }
}
