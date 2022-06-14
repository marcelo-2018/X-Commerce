﻿using System.Windows.Forms;
using IServicio.UnidadMedida;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Articulo
{
    public partial class _00023_UnidadDeMedida : FormConsulta
    {
        private readonly IUnidadMedidaServicio _unidadDeMedidaServicio;

        public _00023_UnidadDeMedida(IUnidadMedidaServicio UnidadDeMedidaServicio)
        {
            InitializeComponent();

            _unidadDeMedidaServicio = UnidadDeMedidaServicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _unidadDeMedidaServicio.Obtener(cadenaBuscar);

            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv); // Pongo Invisible las Columnas

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = @"Descripción";

            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 100;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";
            dgv.Columns["EliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var formulario = new _00024_Abm_UnidadDeMedida(tipoOperacion, id);

            formulario.ShowDialog();

            return formulario.RealizoAlgunaOperacion;
        }
    }
}
