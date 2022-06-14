﻿using System.Windows.Forms;
using IServicios.Tarjeta;
using PresentacionBase.Formularios;

namespace Presentacion.Core.FormaPago
{
    public partial class _00045_Tarjeta : FormConsulta
    {
        private readonly ITarjetaServicio _tarjetaServicio;

        public _00045_Tarjeta(ITarjetaServicio tarjetaServicio)
        {
            InitializeComponent();

            _tarjetaServicio = tarjetaServicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _tarjetaServicio.Obtener(cadenaBuscar);

            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = "Tarjeta";
            dgv.Columns["Descripcion"].DisplayIndex = 0;

            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 100;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";
            dgv.Columns["EliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["EliminadoStr"].DisplayIndex = 1;
        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var form = new _00046_Abm_tarjeta(tipoOperacion, id);
            form.ShowDialog();
            return form.RealizoAlgunaOperacion;
        }
    }
}
