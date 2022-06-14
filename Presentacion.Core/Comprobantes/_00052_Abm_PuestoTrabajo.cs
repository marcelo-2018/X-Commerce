using System.Windows.Forms;
using IServicios.PuestoDeTrabajo;
using IServicios.PuestoDeTrabajo.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.Comprobantes
{
    public partial class _00052_Abm_PuestoTrabajo : FormAbm
    {
        private readonly IPuestoDeTrabajoServicio _puestoDeTrabajoServicio;

        public _00052_Abm_PuestoTrabajo(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _puestoDeTrabajoServicio = ObjectFactory.GetInstance<IPuestoDeTrabajoServicio>();

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
                var resultado = (PuestoDeTrabajoDto)_puestoDeTrabajoServicio.Obtener(entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                txtCodigo.Text = resultado.Codigo.ToString();
                txtDescripcion.Text = resultado.Descripcion;

                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);
            }
            else
            {
                btnEjecutar.Text = "Nuevo";

                txtCodigo.Text = _puestoDeTrabajoServicio.ObtenerSiguienteCodigo().ToString();
            }
        }

        public override bool VerificarDatosObligatorios()
        {
            return !string.IsNullOrEmpty(txtDescripcion.Text);
        }

        public override void EjecutarPostLimpieza()
        {
            txtCodigo.Text = _puestoDeTrabajoServicio.ObtenerSiguienteCodigo().ToString();
        }

        public override bool VerificarSiExiste(long? id = null)
        {
            return _puestoDeTrabajoServicio.VerificarSiExiste(txtDescripcion.Text, id);
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegsitro = new PuestoDeTrabajoDto();

            int codigo;
            int.TryParse(txtCodigo.Text, out codigo);

            nuevoRegsitro.Codigo = codigo;
            nuevoRegsitro.Descripcion = txtDescripcion.Text;
            nuevoRegsitro.Eliminado = false;

            _puestoDeTrabajoServicio.Insertar(nuevoRegsitro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new PuestoDeTrabajoDto();

            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Descripcion = txtDescripcion.Text;
            modificarRegistro.Eliminado = false;

            _puestoDeTrabajoServicio.Modificar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _puestoDeTrabajoServicio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);

            txtDescripcion.Focus();
        }
    }
}
