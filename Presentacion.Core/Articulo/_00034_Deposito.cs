using System.Windows.Forms;
using IServicio.Deposito;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Articulo
{
    public partial class _00034_Deposito : FormConsulta
    {
        private readonly IDepositoSevicio _depositoSevicio;

        public _00034_Deposito(IDepositoSevicio depositoSevicio)
        {
            InitializeComponent();

            _depositoSevicio = depositoSevicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _depositoSevicio
                .Obtener(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv); // Pongo Invisible las Columnas

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = @"Descripción";
            dgv.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;

            dgv.Columns["Ubicacion"].Visible = true;
            dgv.Columns["Ubicacion"].HeaderText = @"Ubicacion";
            dgv.Columns["Ubicacion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;

            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 100;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";
            dgv.Columns["EliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["EliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;
        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var formulario = new _00035_Abm_Deposito(tipoOperacion, id);

            formulario.ShowDialog();

            return formulario.RealizoAlgunaOperacion;
        }
    }
}
