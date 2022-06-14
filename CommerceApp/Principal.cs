using System;
using System.Windows.Forms;
using Presentacion.Core.Articulo;
using Presentacion.Core.Cliente;
using Presentacion.Core.Comprobantes;
using Presentacion.Core.CondicionIva;
using Presentacion.Core.Configuracion;
using Presentacion.Core.Departamento;
using Presentacion.Core.Empleado;
using Presentacion.Core.Localidad;
using Presentacion.Core.Provincia;
using Presentacion.Core.Usuario;
using PresentacionBase.Formularios;
using StructureMap;
using Presentacion.Core.Login;
using System.Runtime.InteropServices;
using System.Threading;
using Aplicacion.Constantes;
using IServicio.Articulo;
using IServicio.Configuracion;
using IServicio.Persona;
using IServicios.Caja;
using Presentacion.Core.Caja;
using Presentacion.Core.FormaPago;

namespace CommerceApp
{
    public partial class Principal : Form
    {
        delegate void TiempoDelegado();
        private Thread hiloReloj;

        private readonly IClienteServicio _clienteServicio;
        private readonly IArticuloServicio _articuloServicio;
        private readonly ICajaServicio _cajaServicio;
        private readonly IConfiguracionServicio _configuracionServicio;

        public Principal(IClienteServicio clienteServicio,
            IArticuloServicio articuloServicio,
            ICajaServicio cajaServicio,
            IConfiguracionServicio configuracionServicio)
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;

            _articuloServicio = articuloServicio;
            _cajaServicio = cajaServicio;
            _configuracionServicio = configuracionServicio;
            _clienteServicio = clienteServicio;
            _configuracionServicio = configuracionServicio;

            hiloReloj = new Thread(Tiempo);
            hiloReloj.Start();

            lblApellido.Text = $"{Identidad.Apellido}";
            lblNombre.Text = $"{Identidad.Nombre}";

            imgUsuarioLogin.Image = Imagen.ConvertirImagen(Identidad.Foto);

            imgUsuarioLogin.Visible = Identidad.Apellido != "Administrador";
        }

        public void CambiarTiempo()
        {
            if (this.InvokeRequired)
            {
                var delegado = new TiempoDelegado(CambiarTiempo);
                this.Invoke(delegado);
            }
            else
            {
                lblHora.Text =
                    $@"{DateTime.Now.Hour:00}" +
                    $@":{DateTime.Now.Minute:00}" +
                    $@":{DateTime.Now.Second:00}";

                lblFecha.Text = DateTime.Now.ToLongDateString();

                imgUsuarioLogin.Image = Imagen.ConvertirImagen(Identidad.Foto);
            }
        }

        private void Tiempo()
        {
            Thread.Sleep(100);
            CambiarTiempo();
            Tiempo();
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            hiloReloj.Abort();
        }


        private void consultaDeUnidadDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00023_UnidadDeMedida>().ShowDialog();
        }

        private void nuevaUnidadDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fNuevaUnidadDeMedida = new _00024_Abm_UnidadDeMedida(TipoOperacion.Nuevo);
            fNuevaUnidadDeMedida.ShowDialog();
        }

        private void consultaDeMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00021_Marca>().ShowDialog();
        }

        private void nuevaMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fNuevaMarca = new _00022_Abm_Marca(TipoOperacion.Nuevo);
            fNuevaMarca.ShowDialog();
        }

        private void consultaDeRubroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00019_Rubro>().ShowDialog();
        }

        private void nuevoRubroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fNuevoRubro = new _00020_Abm_Rubro(TipoOperacion.Nuevo);
            fNuevoRubro.ShowDialog();
        }

        private void consultaDeIvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00025_Iva>().ShowDialog();
        }

        private void nuevoIvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fNuevoIva = new _00026_Abm_Iva(TipoOperacion.Nuevo);
            fNuevoIva.ShowDialog();
        }

        private void consultaDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00009_Cliente>().ShowDialog();
        }

        private void nuevoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fNuevoCliente = new _00010_Abm_Cliente(TipoOperacion.Nuevo);
            fNuevoCliente.ShowDialog();
        }

        private void artículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00017_Articulo>().ShowDialog();
        }

        private void nuevoArticuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fNuevoArticulo = new _00018_Abm_Articulo(TipoOperacion.Nuevo);
            fNuevoArticulo.ShowDialog();
        }

        private void consultaDeEmpleadoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00007_Empleado>().ShowDialog();
        }

        private void nuevoEmpleadoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var fNuevoEmpleado = new _00008_Abm_Empleado(TipoOperacion.Nuevo);
            fNuevoEmpleado.ShowDialog();
        }

        private void consultaListaDePreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00032_ListaPrecio>().ShowDialog();
        }

        private void nuevaListaDePreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00033_Abm_ListaPrecio(TipoOperacion.Nuevo).ShowDialog();
        }

        private void consultaDeProvinciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00001_Provincia>().ShowDialog();
        }

        private void nuevaProvinciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00002_Abm_Provincia(TipoOperacion.Nuevo).ShowDialog();
        }

        private void consultaDeDepartamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00003_Departamento>().ShowDialog();
        }

        private void nuevoDepartamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00004_Abm_Departamento(TipoOperacion.Nuevo).ShowDialog();
        }

        private void consultaDeLocalidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00005_Localidad>().ShowDialog();
        }

        private void nuevaLocalidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00006_AbmLocalidad(TipoOperacion.Nuevo).ShowDialog();
        }

        private void consultaCondicionIvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00013_CondicionIva>().ShowDialog();
        }

        private void nuevaCondicionIvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00014_Abm_CondicionIva(TipoOperacion.Nuevo).ShowDialog();
        }

        private void consultaDeDepositoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00034_Deposito>().ShowDialog();
        }

        private void nuevoDepositoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00035_Abm_Deposito(TipoOperacion.Nuevo).ShowDialog();
        }

        private void consultaPuestoDeTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00051_PuestoTrabajo>().ShowDialog();
        }

        private void nuevoPuestoDeTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00052_Abm_PuestoTrabajo(TipoOperacion.Nuevo).ShowDialog();
        }

        private void configuracionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00012_Configuracion>().ShowDialog();
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00011_Usuario>().ShowDialog();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Identidad.Limpiar();

            lblNombre.Text = string.Empty;
            lblApellido.Text = string.Empty;
            imgUsuarioLogin.Image = null;
            imgUsuarioLogin.Visible = false;

            var fLogin = ObjectFactory.GetInstance<Login>();
            fLogin.ShowDialog();

            if (!fLogin.PuedeAcceder)
            {
                Application.Exit();
            }
            else
            {
                imgUsuarioLogin.Visible = true;
                lblApellido.Text = $"{Identidad.Apellido}";
                lblNombre.Text = $"{Identidad.Nombre}";
                this.imgUsuarioLogin.Image = Imagen.ConvertirImagen(Identidad.Foto);
            }
        }

        private void salirDelSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿ Desea salir del Sistema ?",
                "Atención",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        //Mover el form tocando el panel 

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void pnlTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnArticulo_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00017_Articulo>().ShowDialog();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            if (!Identidad.EsAdministrador)
            {
                if (_cajaServicio.VerificarSiExisteCajaAbierta(Identidad.UsuarioId))
                {
                    ObjectFactory.GetInstance<_00050_Venta>().Show();
                }
                else
                {
                    if (MessageBox.Show("La caja aun no fue abierta. ¿Desea hacerlo ahora?", "Atención",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        var fCaja = ObjectFactory.GetInstance<_00039_AperturaCaja>();
                        fCaja.ShowDialog();

                        if (fCaja.CajaAbierta)
                        {
                            ObjectFactory.GetInstance<_00050_Venta>().Show();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Acceso Denegado");
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00009_Cliente>().ShowDialog();
        }

        private void btnEmpleado_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00007_Empleado>().ShowDialog();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00053_Compra>().ShowDialog();
        }

        private void actualizarPreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00031_ActualizarPrecios>().ShowDialog();
        }

        private void consultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00038_Caja>().ShowDialog();
        }

        private void abrirCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00039_AperturaCaja>().ShowDialog();

            //if (_cajaServicio.VerificarSiExisteCajaAbierta(Identidad.UsuarioId))
            //{
            //    ObjectFactory.GetInstance<_00039_AperturaCaja>().ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show(
            //        $"Ya se encuentra abierta una caja para el usuario {Identidad.Apellido} {Identidad.Nombre}");
            //}
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00038_Caja>().ShowDialog();
        }

        private void consultaBancoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00047_Banco>().ShowDialog();
        }

        private void nuevoBancoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00048_Abm_Banco(TipoOperacion.Nuevo).ShowDialog();
        }

        private void consultaTarjetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00045_Tarjeta>().ShowDialog();
        }

        private void nuevaTarjetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00046_Abm_tarjeta(TipoOperacion.Nuevo).ShowDialog();
        }

        private void cobroDePendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Identidad.EsAdministrador)
            {
                ObjectFactory.GetInstance<_00049_CobroDiferido>().Show();
            }
            else
            {
                MessageBox.Show("Acceso Denegado");
            }
        }

        private void cuentaCorrienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00034_ClienteCtaCte>().ShowDialog();
        }

        private void consultaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00029_BajaDeArticulos>().ShowDialog();
        }

        private void nuevaBajaDeArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00030_Abm_BajaArticulos(TipoOperacion.Nuevo).ShowDialog();
        }

        private void consultaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ObjectFactory.GetInstance<_00027_MotivoBaja>().ShowDialog();
        }

        private void nuevoMotivoDeBajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new _00028_Abm_MotivoBaja(TipoOperacion.Nuevo).ShowDialog();
        }
    }
}