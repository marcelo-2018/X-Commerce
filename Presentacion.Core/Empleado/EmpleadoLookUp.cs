using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IServicio.Persona;
using IServicio.Persona.DTOs;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Empleado
{
    public partial class EmpleadoLookUp : FormLookUp
    {
        private readonly IEmpleadoServicio _empleadoServicio;

        public EmpleadoLookUp(IEmpleadoServicio empleadoServicio)
        {
            InitializeComponent();

            _empleadoServicio = empleadoServicio;
            EntidadSeleccionada = null;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            var empledos = (List<EmpleadoDto>) _empleadoServicio
                .Obtener(typeof(EmpleadoDto), !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty, false);

            dgv.DataSource = empledos;

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
        }
    }
}
