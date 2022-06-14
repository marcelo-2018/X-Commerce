using System.Windows.Forms;
using IServicios.PuestoDeTrabajo;
using Presentacion.Core.CondicionIva;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Comprobantes
{
    public partial class _00051_PuestoTrabajo : FormConsulta
    {
        private readonly IPuestoDeTrabajoServicio _puestoDeTrabajoServicio;

        public _00051_PuestoTrabajo(IPuestoDeTrabajoServicio puestoDeTrabajoServicio)
        {
            InitializeComponent();

            _puestoDeTrabajoServicio = puestoDeTrabajoServicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _puestoDeTrabajoServicio.Obtener(cadenaBuscar);

            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv); // Pongo Invisible las Columnas

            dgv.Columns["Codigo"].Visible = true;
            dgv.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Codigo"].HeaderText = @"Código";

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
            var formulario = new _00052_Abm_PuestoTrabajo(tipoOperacion, id);

            formulario.ShowDialog();

            return formulario.RealizoAlgunaOperacion;
        }
    }
}
