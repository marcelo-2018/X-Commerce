using System;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicios.Caja;
using IServicios.Caja.DTOs;
using PresentacionBase.Formularios;
using StructureMap;

namespace Presentacion.Core.Caja
{
    public partial class _00038_Caja : FormBase
    {
        private readonly ICajaServicio _cajaServicio;

        private CajaDto _cajaSeleccionada;

        private long _usuarioActualId;

        public _00038_Caja(ICajaServicio cajaServicio)
        {
            InitializeComponent();

            _cajaServicio = cajaServicio;

            _cajaSeleccionada = null;

            _usuarioActualId = Identidad.UsuarioId;
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            // 1 - Verificar si esta o no abierta la caja para el usuario login

            if (!_cajaServicio.VerificarSiExisteCajaAbierta(_usuarioActualId))
            {
                // 2 - Configuracion 2.1 Manual o 2.2 Auto

                var fAbrirCaja = ObjectFactory.GetInstance<_00039_AperturaCaja>();
                fAbrirCaja.ShowDialog();

                ActualizarDatos(string.Empty, false, DateTime.Today, DateTime.Today);
            }
            else
            {
                MessageBox.Show(
                    $"Ya se encuentra abierta una caja para el usuario {Identidad.Apellido} {Identidad.Nombre}");
            }
        }

        private void ActualizarDatos(string cadenaBuscar, bool filtroPorFecha, DateTime fechaDesde, DateTime fechaHasta)
        {
            dgvGrilla.DataSource = _cajaServicio.Obtener(cadenaBuscar, filtroPorFecha, fechaDesde, fechaHasta);

            FormatearGrilla(dgvGrilla);
        }

        private void chkRangoFecha_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaDesde.Enabled = chkRangoFecha.Checked;
            dtpFechaHasta.Enabled = chkRangoFecha.Checked;

            if (chkRangoFecha.Checked)
            {
                dtpFechaDesde.Value = DateTime.Now;
                dtpFechaHasta.Value = DateTime.Now;
            }
        }

        private void dtpFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            dtpFechaHasta.MinDate = dtpFechaDesde.Value;

            if (dtpFechaDesde.Value > dtpFechaDesde.Value)
            {
                dtpFechaHasta.Value = dtpFechaDesde.Value;
            }

            //dtpFechaHasta.Value = dtpFechaDesde.Value;
            //dtpFechaHasta.MinDate = dtpFechaDesde.Value;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(!string.IsNullOrEmpty(txtBuscar.Text) ? txtBuscar.Text : string.Empty,
                chkRangoFecha.Checked, dtpFechaDesde.Value, dtpFechaHasta.Value);
        }

        private void _00038_Caja_Load(object sender, EventArgs e)
        {
            dtpFechaDesde.Value = DateTime.Today;
            dtpFechaHasta.Value = DateTime.Today;
            txtBuscar.Clear();

            ActualizarDatos(!string.IsNullOrEmpty(txtBuscar.Text) ? txtBuscar.Text : string.Empty,
                chkRangoFecha.Checked, dtpFechaDesde.Value, dtpFechaHasta.Value);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["UsuarioApertura"].Visible = true;
            dgv.Columns["UsuarioApertura"].Width = 150;
            dgv.Columns["UsuarioApertura"].HeaderText = @"Usuario Apertura";
            dgv.Columns["UsuarioApertura"].DisplayIndex = 0;

            dgv.Columns["FechaAperturaStr"].Visible = true;
            dgv.Columns["FechaAperturaStr"].Width = 60;
            dgv.Columns["FechaAperturaStr"].HeaderText = @"Fecha Apertura";
            dgv.Columns["FechaAperturaStr"].DisplayIndex = 1;

            dgv.Columns["MontoApertura"].Visible = true;
            dgv.Columns["MontoApertura"].Width = 60;
            dgv.Columns["MontoApertura"].HeaderText = @"Monto Apertura";
            dgv.Columns["MontoApertura"].DisplayIndex = 2;

            //------------------------------------------------------------------------------------//

            dgv.Columns["UsuarioCierre"].Visible = true;
            dgv.Columns["UsuarioCierre"].Width = 150;
            dgv.Columns["UsuarioCierre"].HeaderText = @"Usuario Cierre";
            dgv.Columns["UsuarioCierre"].DisplayIndex = 3;

            dgv.Columns["FechaCierreStr"].Visible = true;
            dgv.Columns["FechaCierreStr"].Width = 60;
            dgv.Columns["FechaCierreStr"].HeaderText = @"Fecha Cierre";
            dgv.Columns["FechaCierreStr"].DisplayIndex = 4;

            dgv.Columns["MontoCierreStr"].Visible = true;
            dgv.Columns["MontoCierreStr"].Width = 60;
            dgv.Columns["MontoCierreStr"].HeaderText = @"Monto Cierre";
            dgv.Columns["MontoCierreStr"].DisplayIndex = 5;

            //------------------------------------------------------------------------------------//

            dgv.Columns["TotalEntradaEfectivoStr"].Visible = true;
            dgv.Columns["TotalEntradaEfectivoStr"].Width = 60;
            dgv.Columns["TotalEntradaEfectivoStr"].HeaderText = @"Efectivo Entrada";
            dgv.Columns["TotalEntradaEfectivoStr"].DisplayIndex = 6;

            dgv.Columns["TotalEntradaTarjetaStr"].Visible = true;
            dgv.Columns["TotalEntradaTarjetaStr"].Width = 60;
            dgv.Columns["TotalEntradaTarjetaStr"].HeaderText = @"Tarjeta Entrada";
            dgv.Columns["TotalEntradaTarjetaStr"].DisplayIndex = 7;

            dgv.Columns["TotalEntradaChequeStr"].Visible = true;
            dgv.Columns["TotalEntradaChequeStr"].Width = 60;
            dgv.Columns["TotalEntradaChequeStr"].HeaderText = @"Cheque Entrada";
            dgv.Columns["TotalEntradaChequeStr"].DisplayIndex = 8;

            dgv.Columns["TotalEntradaCtaCteStr"].Visible = true;
            dgv.Columns["TotalEntradaCtaCteStr"].Width = 60;
            dgv.Columns["TotalEntradaCtaCteStr"].HeaderText = @"Cta Cte Entrada";
            dgv.Columns["TotalEntradaCtaCteStr"].DisplayIndex = 9;

            //------------------------------------------------------------------------------------//

            dgv.Columns["TotalSalidaEfectivoStr"].Visible = true;
            dgv.Columns["TotalSalidaEfectivoStr"].Width = 60;
            dgv.Columns["TotalSalidaEfectivoStr"].HeaderText = @"Efectivo Salida";
            dgv.Columns["TotalSalidaEfectivoStr"].DisplayIndex = 10;

            dgv.Columns["TotalSalidaTarjetaStr"].Visible = true;
            dgv.Columns["TotalSalidaTarjetaStr"].Width = 60;
            dgv.Columns["TotalSalidaTarjetaStr"].HeaderText = @"Tarjeta Salida";
            dgv.Columns["TotalSalidaTarjetaStr"].DisplayIndex = 11;

            dgv.Columns["TotalSalidaChequeStr"].Visible = true;
            dgv.Columns["TotalSalidaChequeStr"].Width = 60;
            dgv.Columns["TotalSalidaChequeStr"].HeaderText = @"Cheque Salida";
            dgv.Columns["TotalSalidaChequeStr"].DisplayIndex = 12;

            dgv.Columns["TotalSalidaCtaCteStr"].Visible = true;
            dgv.Columns["TotalSalidaCtaCteStr"].Width = 60;
            dgv.Columns["TotalSalidaCtaCteStr"].HeaderText = @"Cta Cte Salida";
            dgv.Columns["TotalSalidaCtaCteStr"].DisplayIndex = 13;
        }

        private void btnCierreCaja_Click(object sender, EventArgs e)
        {
            var fCierreCaja = new _00040_CierreCaja(_cajaSeleccionada.Id);
            fCierreCaja.ShowDialog();

            if (fCierreCaja.CajaCerrada)
            {
                _usuarioActualId = 0;
            }

            ActualizarDatos(string.Empty, chkRangoFecha.Checked, dtpFechaDesde.Value, dtpFechaHasta.Value);
        }

        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount <= 0)
            {
                _cajaSeleccionada = null;

                return;
            }

            _cajaSeleccionada = (CajaDto)dgvGrilla.Rows[e.RowIndex].DataBoundItem;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
