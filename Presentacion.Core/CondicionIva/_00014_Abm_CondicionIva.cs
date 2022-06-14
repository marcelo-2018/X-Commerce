using System.Windows.Forms;
using IServicio.CondicionIva.DTOs;
using IServicio.Departamento;
using PresentacionBase.Formularios;
using StructureMap;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.CondicionIva
{
    public partial class _00014_Abm_CondicionIva : FormAbm
    {
        private readonly ICondicionIvaServicio _condicionIvaServicio;

        public _00014_Abm_CondicionIva(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _condicionIvaServicio = ObjectFactory.GetInstance<ICondicionIvaServicio>();

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
                var resultado = (CondicionIvaDto) _condicionIvaServicio.Obtener(entidadId.Value);

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
            return _condicionIvaServicio.VerificarSiExiste(txtDescripcion.Text, id);
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegsitro = new CondicionIvaDto();

            nuevoRegsitro.Descripcion = txtDescripcion.Text;
            nuevoRegsitro.Eliminado = false;

            _condicionIvaServicio.Insertar(nuevoRegsitro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new CondicionIvaDto();

            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Descripcion = txtDescripcion.Text;
            modificarRegistro.Eliminado = false;

            _condicionIvaServicio.Modificar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _condicionIvaServicio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);

            txtDescripcion.Focus();
        }
    }
}
