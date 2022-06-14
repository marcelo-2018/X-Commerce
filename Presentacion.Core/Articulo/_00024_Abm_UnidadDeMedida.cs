using System.Windows.Forms;
using IServicio.UnidadMedida;
using IServicio.UnidadMedida.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.Articulo
{
    public partial class _00024_Abm_UnidadDeMedida : FormAbm
    {
        private readonly IUnidadMedidaServicio _unidadDeMedidaServicio;

        public _00024_Abm_UnidadDeMedida(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _unidadDeMedidaServicio = ObjectFactory.GetInstance<IUnidadMedidaServicio>();

            AsignarEvento_EnterLeave(this);

            txtDescripcion.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoNumeros(sender, args);
            };
        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);

            if (entidadId.HasValue)
            {
                var resultado = (UnidadMedidaDto)_unidadDeMedidaServicio.Obtener(entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                txtDescripcion.Text = resultado.Descripcion;

                if(TipoOperacion == TipoOperacion.Eliminar)
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
            return _unidadDeMedidaServicio.VerificarSiExiste(txtDescripcion.Text, id);
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegsitro = new UnidadMedidaDto();

            nuevoRegsitro.Descripcion = txtDescripcion.Text;
            nuevoRegsitro.Eliminado = false;

            _unidadDeMedidaServicio.Insertar(nuevoRegsitro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new UnidadMedidaDto();

            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Descripcion = txtDescripcion.Text;
            modificarRegistro.Eliminado = false;

            _unidadDeMedidaServicio.Modificar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _unidadDeMedidaServicio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);

            txtDescripcion.Focus();
        }
    }
}
