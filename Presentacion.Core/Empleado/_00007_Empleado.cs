using System.Windows.Forms;
using IServicio.Persona;
using IServicio.Persona.DTOs;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Empleado
{
    public partial class _00007_Empleado : FormConsulta
    {
        private readonly IEmpleadoServicio _empleadoServicio;

        public _00007_Empleado(IEmpleadoServicio empleadoServicio)
        {
            InitializeComponent();

            _empleadoServicio = empleadoServicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            var datos = _empleadoServicio.Obtener(typeof(EmpleadoDto),
                !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            dgv.DataSource = datos;

            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Legajo"].Visible = true;
            dgv.Columns["Foto"].Visible = false;

            dgv.Columns["ApyNom"].Visible = true;
            dgv.Columns["ApyNom"].HeaderText = "Apellido y Nombre";
            dgv.Columns["ApyNom"].Width = 300;
            dgv.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["Dni"].Visible = true;
            dgv.Columns["Dni"].HeaderText = "DNI";

            dgv.Columns["Direccion"].Visible = true;
            dgv.Columns["Direccion"].HeaderText = "Dirección";

            dgv.Columns["Telefono"].Visible = true;

            dgv.Columns["Mail"].Visible = true;
            dgv.Columns["Mail"].HeaderText = "Correo Electronico";
            dgv.Columns["Mail"].Width = 200;

            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 100;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";
            dgv.Columns["EliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var formulario = new _00008_Abm_Empleado(tipoOperacion, id);

            formulario.ShowDialog();

            return formulario.RealizoAlgunaOperacion;
        }
    }
}
