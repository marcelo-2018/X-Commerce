using System;
using System.Windows.Forms;
using IServicio.Seguridad;

namespace Presentacion.Core.Comprobantes.Clases
{
    public partial class AutorizacionListaPrecio : Form
    {
        private readonly ISeguridadServicio _seguridad;

        private bool _tienePermiso;
        public bool PermisoAutorizado => _tienePermiso;

        public AutorizacionListaPrecio(ISeguridadServicio seguridad)
        {
            InitializeComponent();

            _seguridad = seguridad;
            _tienePermiso = false;

            txtPassword.UseSystemPasswordChar = true;
        }

        private void imgOjo_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void imgOjo_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnAcceder_Click(object sender, System.EventArgs e)
        {
            try
            {
                _tienePermiso = _seguridad.VerificarAcceso(txtUsuario.Text, txtPassword.Text);

                if (_tienePermiso)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El usuario o el Password son Icorrectos");
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            _tienePermiso = false;
            Close();
        }
    }
}
