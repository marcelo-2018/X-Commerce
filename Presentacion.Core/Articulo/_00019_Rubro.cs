using System.Windows.Forms;
using IServicio.Rubro;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Articulo
{
    public partial class _00019_Rubro : FormConsulta
    {
        private readonly IRubroServicio _rubroServicio;

        public _00019_Rubro(IRubroServicio rubroServicio)
        {
            InitializeComponent();

            _rubroServicio = rubroServicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _rubroServicio.Obtener(cadenaBuscar);

            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv); // Pongo Invisible las Columnas

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = @"Descripción";

            dgv.Columns["ActivarLimiteVentaStr"].Visible = true;
            dgv.Columns["ActivarLimiteVentaStr"].Width = 60;
            dgv.Columns["ActivarLimiteVentaStr"].HeaderText = @"Restriccion cantidad";
            dgv.Columns["ActivarLimiteVentaStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["LimiteVenta"].Visible = true;
            dgv.Columns["LimiteVenta"].Width = 70;
            dgv.Columns["LimiteVenta"].HeaderText = @"Cantidad Maxima";

            dgv.Columns["ActivarHoraVentaStr"].Visible = true;
            dgv.Columns["ActivarHoraVentaStr"].Width = 60;
            dgv.Columns["ActivarHoraVentaStr"].HeaderText = @"Restriccion hora";
            dgv.Columns["ActivarHoraVentaStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["HoraLimiteVentaDesdeStr"].Visible = true;
            dgv.Columns["HoraLimiteVentaDesdeStr"].Width = 150;
            dgv.Columns["HoraLimiteVentaDesdeStr"].HeaderText = @"Hora Desde";

            dgv.Columns["HoraLimiteVentaHastaStr"].Visible = true;
            dgv.Columns["HoraLimiteVentaHastaStr"].Width = 150;
            dgv.Columns["HoraLimiteVentaHastaStr"].HeaderText = @"Hora Hasta";

            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 100;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";
            dgv.Columns["EliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var formulario = new _00020_Abm_Rubro(tipoOperacion, id);
            formulario.ShowDialog();

            return formulario.RealizoAlgunaOperacion;
        }
    }
}
