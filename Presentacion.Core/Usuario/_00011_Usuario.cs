using System;
using System.Windows.Forms;
using IServicio.Usuario;
using IServicio.Usuario.DTOs;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Usuario
{
    public partial class _00011_Usuario : FormBase
    {
        private readonly IUsuarioServicio _usuarioServicio;
        private UsuarioDto _usuarioDto;

        public _00011_Usuario(IUsuarioServicio usuarioServicio)
        {
            InitializeComponent();

            _usuarioServicio = usuarioServicio;
            _usuarioDto = null;
        }

        private void ActualizarDatos(string cadenaBuscar)
        {
            var usuario = _usuarioServicio
                .Obtener(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            dgvGrilla.DataSource = usuario;

            FormatearGrilla(dgvGrilla);
        }

        private void _00011_Usuario_Load(object sender, System.EventArgs e)
        {
            ActualizarDatos(string.Empty);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["EmpleadoId"].Visible = false;

            dgv.Columns["ApellidoEmpleado"].Visible = false;

            dgv.Columns["NombreEmpleado"].Visible = false;

            dgv.Columns["ApyNomEmpleado"].Visible = true;
            dgv.Columns["ApyNomEmpleado"].HeaderText = "Apellido y Nombre";
            dgv.Columns["ApyNomEmpleado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["NombreUsuario"].Visible = true;
            dgv.Columns["NombreUsuario"].HeaderText = "Nombre de Usuario";
            dgv.Columns["NombreUsuario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["NombreUsuario"].Width = 150;

            dgv.Columns["Password"].Visible = false;

            dgv.Columns["EstaBloqueado"].Visible = false;

            dgv.Columns["EstaBloqueadoStr"].Visible = true;
            dgv.Columns["EstaBloqueadoStr"].HeaderText = "Bloqueado";
            dgv.Columns["EstaBloqueadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["EstaBloqueadoStr"].Width = 70;

            dgv.Columns["FotoEmpleado"].Visible = false;
        }

        private void btnBuscar_Click(object sender, System.EventArgs e)
        {
            ActualizarDatos(txtBuscar.Text);
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ActualizarDatos(txtBuscar.Text);
            }
        }

        private void btnActualizar_Click(object sender, System.EventArgs e)
        {
            ActualizarDatos(string.Empty);
        }

        private void btnSalir_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount <= 0)
            {
                _usuarioDto = null;
                return;
            }

            _usuarioDto = (UsuarioDto)dgvGrilla.Rows[e.RowIndex].DataBoundItem;
        }

        private void btnNuevo_Click(object sender, System.EventArgs e)
        {
            if (_usuarioDto == null)
            {
                MessageBox.Show("Por favor seleccione un empleado.");
                return;
            }

            try
            {
                if (_usuarioDto.NombreUsuario == "NO ASIGNADO")
                {
                    _usuarioServicio.Crear(_usuarioDto.EmpleadoId, _usuarioDto.ApellidoEmpleado, _usuarioDto.NombreEmpleado);

                    ActualizarDatos(string.Empty);

                    MessageBox.Show("El usuario se creo correctamente");
                }
                else
                {
                    MessageBox.Show("El empleado ya tiene un usuario asignado");
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Atención", MessageBoxButtons.OK);
            }
        }

        private void btnBloquear_Click(object sender, EventArgs e)
        {
            if (_usuarioDto == null)
            {
                MessageBox.Show("Por favor seleccione un empleado.");
                return;
            }

            try
            {
                _usuarioServicio.Bloquear(_usuarioDto.Id);

                txtBuscar.Clear();
                txtBuscar.Focus();

                ActualizarDatos(string.Empty);

                MessageBox.Show($"El usuario {_usuarioDto.NombreUsuario} se bloqueo/desbloqueo correctamente.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Atención", MessageBoxButtons.OK);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_usuarioDto == null)
            {
                MessageBox.Show("Por favor seleccione un empleado.");
                return;
            }

            try
            {
                _usuarioServicio.ResetPassword(_usuarioDto.Id);

                txtBuscar.Clear();
                txtBuscar.Focus();

                ActualizarDatos(string.Empty);

                MessageBox.Show($"La contraseña del usuario {_usuarioDto.NombreUsuario} se reinicio correctamente.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Atención", MessageBoxButtons.OK);
            }
        }
    }
}
