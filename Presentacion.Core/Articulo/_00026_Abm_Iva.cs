using System.Windows.Forms;
using IServicio.Iva;
using IServicio.Iva.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.Articulo
{
    public partial class _00026_Abm_Iva : FormAbm
    {
        private readonly IIvaServicio _ivaServicio;

        public _00026_Abm_Iva(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _ivaServicio = ObjectFactory.GetInstance<IIvaServicio>();

            AsignarEvento_EnterLeave(this);

            txtDescripcion.KeyPress += delegate (object sender, KeyPressEventArgs args)
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
                var resultado = (IvaDto)_ivaServicio.Obtener(entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                txtDescripcion.Text = resultado.Descripcion;

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
            return _ivaServicio.VerificarSiExiste(txtDescripcion.Text, id);
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegsitro = new IvaDto();

            nuevoRegsitro.Descripcion = txtDescripcion.Text;
            nuevoRegsitro.Porcentaje = nudPorcentaje.Value;
            nuevoRegsitro.Eliminado = false;

            _ivaServicio.Insertar(nuevoRegsitro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new IvaDto();

            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Descripcion = txtDescripcion.Text;
            modificarRegistro.Porcentaje = nudPorcentaje.Value;
            modificarRegistro.Eliminado = false;

            _ivaServicio.Modificar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _ivaServicio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);

            txtDescripcion.Focus();
        }
    }
}
