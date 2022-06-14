using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicio.Departamento;
using IServicio.Localidad;
using IServicio.Persona;
using IServicio.Persona.DTOs;
using IServicio.Provincia;
using Presentacion.Core.Departamento;
using Presentacion.Core.Localidad;
using Presentacion.Core.Properties;
using Presentacion.Core.Provincia;
using PresentacionBase.Formularios;
using StructureMap;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.Empleado
{
    public partial class _00008_Abm_Empleado : FormAbm
    {
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IProvinciaServicio _provinciaServicio;
        private readonly IDepartamentoServicio _departamentoServicio;
        private readonly ILocalidadServicio _localidadServicio;

        private EmpleadoDto empleado;

        public _00008_Abm_Empleado(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _empleadoServicio = ObjectFactory.GetInstance<IEmpleadoServicio>();
            _provinciaServicio = ObjectFactory.GetInstance<IProvinciaServicio>();
            _departamentoServicio = ObjectFactory.GetInstance<IDepartamentoServicio>();
            _localidadServicio = ObjectFactory.GetInstance<ILocalidadServicio>();

            AsignarEvento_EnterLeave(this);

            txtLegajo.KeyPress += delegate(object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtApellido.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoNumeros(sender, args);
            };

            txtNombre.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoNumeros(sender, args);
            };

            txtDni.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtTelefono.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtDomicilio.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
            };

            txtMail.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
            };
        }

        public override void CargarDatos(long? entidadId)
        {
            empleado = new EmpleadoDto();

            imgFoto.Image = Imagen.ImagenEmpleadoPorDefecto;

            var provincias = _provinciaServicio.Obtener(string.Empty);

            PoblarComboBox(cmbProvincia,
                provincias,
                "Descripcion",
                "Id");

            base.CargarDatos(entidadId);

            if (entidadId.HasValue)
            {
                var resultado = (EmpleadoDto)_empleadoServicio.Obtener(typeof(EmpleadoDto), entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                txtLegajo.Text = resultado.Legajo.ToString();
                txtApellido.Text = resultado.Apellido;
                txtNombre.Text = resultado.Nombre;
                txtDni.Text = resultado.Dni;
                txtTelefono.Text = resultado.Telefono;
                txtDomicilio.Text = resultado.Direccion;
                cmbProvincia.SelectedValue = resultado.ProvinciaId;
                cmbDepartamento.SelectedValue = resultado.DepartamentoId;
                cmbLocalidad.SelectedValue = resultado.LocalidadId;
                txtMail.Text = resultado.Mail;
                imgFoto.Image = Imagen.ConvertirImagen(resultado.Foto);

                if (provincias.Any())
                {
                    var departamentos = _departamentoServicio
                        .ObtenerPorProvincia((long)cmbProvincia.SelectedValue);

                    PoblarComboBox(cmbDepartamento,
                        departamentos,
                        "Descripcion",
                        "Id");

                    if (departamentos.Any())
                    {
                        var localidades =
                            _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue);

                        PoblarComboBox(cmbLocalidad,
                            localidades,
                            "Descripcion",
                            "Id");
                    }
                }

                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);
            }
            else
            { 
                btnEjecutar.Text = "Nuevo";

                if (provincias.Any())
                {
                    var departamentos = _departamentoServicio
                        .ObtenerPorProvincia((long)cmbProvincia.SelectedValue);

                    PoblarComboBox(cmbDepartamento,
                        departamentos,
                        "Descripcion",
                        "Id");

                    if (departamentos.Any())
                    {
                        var localidades =
                            _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue);

                        PoblarComboBox(cmbLocalidad,
                            localidades,
                            "Descripcion",
                            "Id");
                    }
                }

                txtLegajo.Text = _empleadoServicio.ObtenerSiguienteLegajo().ToString();
            }
        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtApellido.Text)) return false;

            if (string.IsNullOrEmpty(txtNombre.Text)) return false;

            if (string.IsNullOrEmpty(txtDni.Text)) return false;

            if (string.IsNullOrEmpty(txtTelefono.Text)) return false;

            if (string.IsNullOrEmpty(txtDomicilio.Text)) return false;

            if (string.IsNullOrEmpty(txtMail.Text)) return false;

            if (cmbProvincia.Items.Count <= 0) return false;

            if (cmbDepartamento.Items.Count <= 0) return false;

            if (cmbLocalidad.Items.Count <= 0) return false;

            return true;
        }

        public override bool VerificarSiExiste(long? id = null)
        {
            return _empleadoServicio.VerificarSiExiste(txtLegajo.Text, id);
        }

        public override void EjecutarPostLimpieza()
        {
            txtLegajo.Text = _empleadoServicio.ObtenerSiguienteLegajo().ToString();
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegistro = new EmpleadoDto();

            int legajo = 0 ;
            int.TryParse(txtLegajo.Text, out legajo);

            nuevoRegistro.Legajo = legajo;
            nuevoRegistro.Apellido = txtApellido.Text;
            nuevoRegistro.Nombre = txtNombre.Text;
            nuevoRegistro.Dni = txtDni.Text;
            nuevoRegistro.Telefono = txtTelefono.Text;
            nuevoRegistro.Direccion = txtDomicilio.Text;
            nuevoRegistro.ProvinciaId = (long)cmbProvincia.SelectedValue;
            nuevoRegistro.LocalidadId = (long) cmbLocalidad.SelectedValue;
            nuevoRegistro.DepartamentoId = (long) cmbDepartamento.SelectedValue;
            nuevoRegistro.Mail = txtMail.Text;
            nuevoRegistro.Foto = Imagen.ConvertirImagen(this.imgFoto.Image);
            nuevoRegistro.Eliminado = false;

            _empleadoServicio.Insertar(nuevoRegistro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new EmpleadoDto();

            int legajo = 0;
            int.TryParse(txtLegajo.Text, out legajo);

            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Legajo = legajo;
            modificarRegistro.Apellido = txtApellido.Text;
            modificarRegistro.Nombre = txtNombre.Text;
            modificarRegistro.Dni = txtDni.Text;
            modificarRegistro.Telefono = txtTelefono.Text;
            modificarRegistro.Direccion = txtDomicilio.Text;
            modificarRegistro.ProvinciaId = (long)cmbProvincia.SelectedValue;
            modificarRegistro.LocalidadId = (long)cmbLocalidad.SelectedValue;
            modificarRegistro.DepartamentoId = (long)cmbDepartamento.SelectedValue;
            modificarRegistro.Mail = txtMail.Text;
            modificarRegistro.Foto = Imagen.ConvertirImagen(this.imgFoto.Image);
            modificarRegistro.Eliminado = false;

            _empleadoServicio.Modificar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _empleadoServicio.Eliminar(typeof(EmpleadoDto), EntidadId.Value);
        }

        public override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);

            txtApellido.Focus();
        }

        private void cmbProvincia_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cmbProvincia.Items.Count <= 0) return;

            PoblarComboBox(cmbDepartamento,
                _departamentoServicio.ObtenerPorProvincia((long)cmbProvincia.SelectedValue), "Descripcion", "Id");

            if (cmbDepartamento.Items.Count <= 0) return;

            PoblarComboBox(cmbLocalidad,
                _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue), "Descripcion", "Id");
        }

        private void cmbDepartamento_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cmbDepartamento.Items.Count <= 0) return;

            PoblarComboBox(cmbLocalidad,
                _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue), "Descripcion", "Id");
        }

        private void btnNuevaProvincia_Click(object sender, System.EventArgs e)
        {
            var formulario = new _00002_Abm_Provincia(TipoOperacion.Nuevo);
            formulario.ShowDialog();

            if (formulario.RealizoAlgunaOperacion)
            {
                var provincias = _provinciaServicio.Obtener(string.Empty);

                PoblarComboBox(cmbProvincia,
                    provincias,
                    "Descripcion",
                    "Id");
            }
        }

        private void btnNuevoDepartamento_Click(object sender, System.EventArgs e)
        {
            var formulario = new _00004_Abm_Departamento(TipoOperacion.Nuevo);
            formulario.ShowDialog();

            if (formulario.RealizoAlgunaOperacion)
            {
                var departamentos = _departamentoServicio
                    .ObtenerPorProvincia((long)cmbProvincia.SelectedValue);

                PoblarComboBox(cmbDepartamento,
                    departamentos,
                    "Descripcion",
                    "Id");
            }
        }

        private void btnNuevaLocalidad_Click(object sender, System.EventArgs e)
        {
            var formulario = new _00006_AbmLocalidad(TipoOperacion.Nuevo);
            formulario.ShowDialog();

            if (formulario.RealizoAlgunaOperacion)
            {
                var localidades =
                    _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue);

                PoblarComboBox(cmbLocalidad,
                    localidades,
                    "Descripcion",
                    "Id");
            }
        }

        private void btnImagen_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                imgFoto.Image = string.IsNullOrEmpty(openFile.FileName)
                    ? Imagen.ImagenEmpleadoPorDefecto
                    : Image.FromFile(openFile.FileName);
            }
        }
    }
}
