using System.Windows.Forms;
using IServicio.Deposito;
using IServicio.Deposito.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.Articulo
{
    public partial class _00035_Abm_Deposito : FormAbm
    {
        private readonly IDepositoSevicio _depositoSevicio;

        public _00035_Abm_Deposito(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _depositoSevicio = ObjectFactory.GetInstance<IDepositoSevicio>();

            AsignarEvento_EnterLeave(this);

            CargarValidaciones();
        }

        private void CargarValidaciones()
        {
            txtDescripcion.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoNumeros(sender, args);
            };

            txtUbicacion.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
            };
        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);

            if (entidadId.HasValue)
            {
                var resultado = (DepositoDto) _depositoSevicio.Obtener(entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                txtDescripcion.Text = resultado.Descripcion;
                txtUbicacion.Text = resultado.Ubicacion;

                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);
            }
            else
            {
                btnEjecutar.Text = "Nuevo";
            }
        }

        public override bool VerificarDatosObligatorios()
        {
            return !string.IsNullOrEmpty(txtDescripcion.Text);
        }

        public override bool VerificarSiExiste(long? id = null)
        {
            return _depositoSevicio.VerificarSiExiste(txtDescripcion.Text, id);
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegsitro = new DepositoDto();

            nuevoRegsitro.Descripcion = txtDescripcion.Text;
            nuevoRegsitro.Ubicacion = txtUbicacion.Text;
            nuevoRegsitro.Eliminado = false;

            _depositoSevicio.Insertar(nuevoRegsitro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new DepositoDto();

            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Descripcion = txtDescripcion.Text;
            modificarRegistro.Ubicacion = txtUbicacion.Text;
            modificarRegistro.Eliminado = false;

            _depositoSevicio.Modificar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _depositoSevicio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);

            txtDescripcion.Focus();
        }
    }
}
