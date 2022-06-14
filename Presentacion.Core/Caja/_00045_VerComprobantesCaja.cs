using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IServicios.Caja.DTOs;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Caja
{
    public partial class _00045_VerComprobantesCaja : FormBase
    {
        public _00045_VerComprobantesCaja(List<ComprobanteCajaDto> comprobantes)
        {
            InitializeComponent();

            dgvGrilla.DataSource = comprobantes.ToList();

            FormatearGrilla(dgvGrilla);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Vendedor"].Visible = true;
            dgv.Columns["Vendedor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Vendedor"].HeaderText = @"Vendedor";

            dgv.Columns["Numero"].Visible = true;
            dgv.Columns["Numero"].Width = 60;
            dgv.Columns["Numero"].HeaderText = "Nro";
            dgv.Columns["Numero"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["FechaStr"].Visible = true;
            dgv.Columns["FechaStr"].Width = 100;
            dgv.Columns["FechaStr"].HeaderText = "Fecha";
            dgv.Columns["FechaStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["TotalStr"].Visible = true;
            dgv.Columns["TotalStr"].Width = 60;
            dgv.Columns["TotalStr"].HeaderText = "Total";
            dgv.Columns["TotalStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
    }
}
