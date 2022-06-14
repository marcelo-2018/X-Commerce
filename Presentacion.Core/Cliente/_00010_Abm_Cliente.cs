using System.Linq;
using System.Windows.Forms;
using IServicio.CondicionIva.DTOs;
using IServicio.Departamento;
using IServicio.Localidad;
using IServicio.Persona;
using IServicio.Persona.DTOs;
using IServicio.Provincia;
using Presentacion.Core.CondicionIva;
using Presentacion.Core.Departamento;
using Presentacion.Core.Localidad;
using Presentacion.Core.Provincia;
using PresentacionBase.Formularios;
using StructureMap;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.Cliente
{
    public partial class _00010_Abm_Cliente : FormAbm
    {
        private readonly IClienteServicio _clienteServicio;
        private readonly IProvinciaServicio _provinciaServicio;
        private readonly IDepartamentoServicio _departamentoServicio;
        private readonly ILocalidadServicio _localidadServicio;
        private readonly ICondicionIvaServicio _condicionIvaServicio;

        public _00010_Abm_Cliente(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _clienteServicio = ObjectFactory.GetInstance<IClienteServicio>();
            _provinciaServicio = ObjectFactory.GetInstance<IProvinciaServicio>();
            _departamentoServicio = ObjectFactory.GetInstance<IDepartamentoServicio>();
            _localidadServicio = ObjectFactory.GetInstance<ILocalidadServicio>();
            _condicionIvaServicio = ObjectFactory.GetInstance<ICondicionIvaServicio>();

            var provincias = _provinciaServicio.Obtener(string.Empty, false);

            PoblarComboBox(cmbProvincia, provincias, "Descripcion", "Id");

            if (provincias.Any())
            {
                var departamentos = _departamentoServicio.ObtenerPorProvincia(provincias.FirstOrDefault().Id);

                PoblarComboBox(cmbDepartamento, departamentos
                    , "Descripcion", "Id");

                if (departamentos.Any())
                {
                    PoblarComboBox(cmbLocalidad,
                        _localidadServicio.ObtenerPorDepartamento(departamentos.FirstOrDefault().Id), "Descripcion",
                        "Id");
                }
            }

            AsignarEvento_EnterLeave(this);

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
            var provincias = _provinciaServicio.Obtener(string.Empty);

            PoblarComboBox(cmbProvincia,
                provincias,
                "Descripcion",
                "Id");

            var condicionIva = _condicionIvaServicio.Obtener(string.Empty);

            PoblarComboBox(cmbCondicionIva, condicionIva, "Descripcion", "Id");

            base.CargarDatos(entidadId);

            if (entidadId.HasValue)
            {
                var resultado = (ClienteDto)_clienteServicio.Obtener(typeof(ClienteDto), entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                txtApellido.Text = resultado.Apellido;
                txtNombre.Text = resultado.Nombre;
                txtDni.Text = resultado.Dni;
                txtTelefono.Text = resultado.Telefono;
                txtDomicilio.Text = resultado.Direccion;
                cmbProvincia.SelectedValue = resultado.ProvinciaId;
                cmbDepartamento.SelectedValue = resultado.DepartamentoId;
                cmbLocalidad.SelectedValue = resultado.LocalidadId;
                txtMail.Text = resultado.Mail;
                cmbCondicionIva.SelectedValue = resultado.CondicionIvaId;
                chkActivarCuentaCorriente.Checked = resultado.ActivarCtaCte;
                chkLimiteCompra.Checked = resultado.TieneLimiteCompra;
                nudLimiteCompra.Value = resultado.MontoMaximoCtaCte;


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

                PoblarComboBox(cmbCondicionIva, condicionIva, "Descripcion", "Id");

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

            if (cmbCondicionIva.Items.Count <= 0) return false;

            return true;
        }

        public override bool VerificarSiExiste(long? id = null)
        {
            return _clienteServicio.VerificarSiExiste(txtDni.Text, id);
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegistro = new ClienteDto();

            nuevoRegistro.Apellido = txtApellido.Text;
            nuevoRegistro.Nombre = txtNombre.Text;
            nuevoRegistro.Dni = txtDni.Text;
            nuevoRegistro.Telefono = txtTelefono.Text;
            nuevoRegistro.Direccion = txtDomicilio.Text;
            nuevoRegistro.ProvinciaId = (long)cmbProvincia.SelectedValue;
            nuevoRegistro.LocalidadId = (long)cmbLocalidad.SelectedValue;
            nuevoRegistro.DepartamentoId = (long)cmbDepartamento.SelectedValue;
            nuevoRegistro.Mail = txtMail.Text;
            nuevoRegistro.Eliminado = false;
            nuevoRegistro.ActivarCtaCte = chkActivarCuentaCorriente.Checked;
            nuevoRegistro.TieneLimiteCompra = chkLimiteCompra.Checked;
            nuevoRegistro.MontoMaximoCtaCte = nudLimiteCompra.Value;
            nuevoRegistro.CondicionIvaId = (long)cmbCondicionIva.SelectedValue;

            _clienteServicio.Insertar(nuevoRegistro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new ClienteDto();

            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Apellido = txtApellido.Text;
            modificarRegistro.Nombre = txtNombre.Text;
            modificarRegistro.Dni = txtDni.Text;
            modificarRegistro.Telefono = txtTelefono.Text;
            modificarRegistro.Direccion = txtDomicilio.Text;
            modificarRegistro.ProvinciaId = (long)cmbProvincia.SelectedValue;
            modificarRegistro.LocalidadId = (long)cmbLocalidad.SelectedValue;
            modificarRegistro.DepartamentoId = (long)cmbDepartamento.SelectedValue;
            modificarRegistro.Mail = txtMail.Text;
            modificarRegistro.Eliminado = false;
            modificarRegistro.ActivarCtaCte = chkActivarCuentaCorriente.Checked;
            modificarRegistro.TieneLimiteCompra = chkLimiteCompra.Checked;
            modificarRegistro.MontoMaximoCtaCte = nudLimiteCompra.Value;
            modificarRegistro.CondicionIvaId = (long)cmbCondicionIva.SelectedValue;

            _clienteServicio.Modificar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _clienteServicio.Eliminar(typeof(ClienteDto), EntidadId.Value);
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

        private void btnNuevaCondicionIva_Click(object sender, System.EventArgs e)
        {
            var formulario = new _00014_Abm_CondicionIva(TipoOperacion.Nuevo);
            formulario.ShowDialog();

            if (formulario.RealizoAlgunaOperacion)
            {
                var condicionIva =
                    _condicionIvaServicio.Obtener((long)cmbCondicionIva.SelectedValue);

                PoblarComboBox(cmbCondicionIva,
                    condicionIva,
                    "Descripcion",
                    "Id");
            }
        }

        private void chkLimiteCompra_CheckedChanged(object sender, System.EventArgs e)
        {
            nudLimiteCompra.Enabled = chkLimiteCompra.Checked;
        }

        private void chkActivarCuentaCorriente_CheckedChanged_1(object sender, System.EventArgs e)
        {
            chkLimiteCompra.Enabled = chkActivarCuentaCorriente.Checked;
        }
    }
}
