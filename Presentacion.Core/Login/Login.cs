using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicio.Seguridad;
using PresentacionBase.Formularios;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;
using Color = System.Drawing.Color;

namespace Presentacion.Core.Login
{
    public partial class Login : FormBase
    {
        private bool _puedeAcceder = false;

        private readonly ISeguridadServicio _seguridadServicio;

        public bool PuedeAcceder => _puedeAcceder;

        public Login(ISeguridadServicio seguridadServicio)
        {
            InitializeComponent();

            _seguridadServicio = seguridadServicio;

            _puedeAcceder = false;

            //txtUsuario.Enter += Control_Enter;
            //txtPassword.Enter += Control_Enter;

            //txtUsuario.Leave += Control_Leave;
            //txtPassword.Leave += Control_Leave;

            //AsignarEvento_EnterLeave(this);

            txtPassword.UseSystemPasswordChar = true;

            txtUsuario.Text = "admin";
            txtPassword.Text = "admin";

            txtUsuario.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
            };
        }

        //Muestra el password
        private void imgOjo_MouseDown(object sender, MouseEventArgs e)
        {
            //txtPassword.PasswordChar = char.MinValue;
            txtPassword.UseSystemPasswordChar = false;
        }

        //Oculta el password
        private void imgOjo_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            //txtPassword.PasswordChar = '*';
        }

        private void btnAcceder_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUsuario.Text))
                {
                    MessageBox.Show("Por favor ingrese un Usuario");
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Por favor ingrese una contraseña");
                    return;
                }

                if (UsuarioAdmin.Usuario == txtUsuario.Text && UsuarioAdmin.Password == txtPassword.Text)
                {
                    Identidad.Apellido = "Administrador";
                    _puedeAcceder = true;
                    this.Close();
                    return;
                }

                var puedeAcceder = _seguridadServicio.VerificarAcceso(txtUsuario.Text, txtPassword.Text);

                if (puedeAcceder)
                {
                    var usuarioLogin = _seguridadServicio.ObtenerUsuarioLogin(txtUsuario.Text);

                    if (usuarioLogin == null)
                    {
                        MessageBox.Show("Ocurrio un Error al Obtener el Usuario");
                        txtPassword.Clear();
                        txtPassword.Focus();
                        return;
                    }

                    if (usuarioLogin.EstaBloqueado)
                    {
                        MessageBox.Show($"El usuario {usuarioLogin.ApyNomEmpleado} esta BLOQUEADO.");
                        txtUsuario.Clear();
                        txtPassword.Clear();
                        txtUsuario.Focus();

                        return;
                    }

                    // Datos del Empleado
                    Identidad.EmpleadoId = usuarioLogin.EmpleadoId;
                    Identidad.Apellido = usuarioLogin.ApellidoEmpleado;
                    Identidad.Nombre = usuarioLogin.NombreEmpleado;
                    Identidad.Foto = usuarioLogin.FotoEmpleado;

                    // Datos del Usuario
                    Identidad.UsuarioId = usuarioLogin.Id;
                    Identidad.Usuario = usuarioLogin.NombreUsuario;

                    _puedeAcceder = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El Usuario o la Contraseña son Incorrectas.");
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Atención", MessageBoxButtons.OK);
                this.txtPassword.Clear();
                this.txtPassword.Focus();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea salir del Sistema",
                "Atención",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                _puedeAcceder = false;
                Application.Exit();
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "USUARIO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            //if (txtUsuario.Text == "")
            //{
            //    txtUsuario.Text = "USUARIO";
            //    txtUsuario.ForeColor = Color.DimGray;
            //}
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                //txtPassword.Text = "";
                //txtPassword.ForeColor = Color.LightGray;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                //txtPassword.Text = "CLAVE";
                //txtPassword.ForeColor = Color.DimGray;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void imgOjo_Click(object sender, EventArgs e)
        {

        }
    }
}
