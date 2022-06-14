using System.Windows.Forms;
using IServicio.Rubro;
using IServicio.Rubro.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.Articulo
{
    public partial class _00020_Abm_Rubro : FormAbm
    {
        private readonly IRubroServicio _rubroServicio;

        public _00020_Abm_Rubro(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _rubroServicio = ObjectFactory.GetInstance<IRubroServicio>();

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
                var resultado = (RubroDto)_rubroServicio.Obtener(entidadId.Value);

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
            return _rubroServicio.VerificarSiExiste(txtDescripcion.Text, id);
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegistro = new RubroDto();

            nuevoRegistro.Descripcion = txtDescripcion.Text;
            nuevoRegistro.ActivarLimiteVenta = chkActivarLimite.Checked;
            nuevoRegistro.LimiteVenta = nudLimiteVenta.Value;
            nuevoRegistro.ActivarHoraVenta = chkActivarHoraVenta.Checked;
            nuevoRegistro.HoraLimiteVentaDesde = dtpHoraDesde.Value;
            nuevoRegistro.HoraLimiteVentaHasta = dtpHoraHasta.Value;
            nuevoRegistro.Eliminado = false;

            _rubroServicio.Insertar(nuevoRegistro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new RubroDto();

            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Descripcion = txtDescripcion.Text;
            modificarRegistro.ActivarLimiteVenta = chkActivarLimite.Checked;
            modificarRegistro.LimiteVenta = nudLimiteVenta.Value;
            modificarRegistro.ActivarHoraVenta = chkActivarHoraVenta.Checked;
            modificarRegistro.HoraLimiteVentaDesde = dtpHoraDesde.Value;
            modificarRegistro.HoraLimiteVentaHasta = dtpHoraHasta.Value;
            modificarRegistro.Eliminado = false;

            _rubroServicio.Modificar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _rubroServicio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);

            txtDescripcion.Focus();
        }

        private void chkActivarHoraVenta_CheckedChanged(object sender, System.EventArgs e)
        {
            dtpHoraDesde.Enabled = chkActivarHoraVenta.Checked;
            dtpHoraHasta.Enabled = chkActivarHoraVenta.Checked;
        }

        private void chkActivarLimite_CheckedChanged(object sender, System.EventArgs e)
        {
            nudLimiteVenta.Enabled = chkActivarLimite.Checked;
        }
    }
}
