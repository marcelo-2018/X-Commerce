using System;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicio.Configuracion;
using IServicios.Caja;

namespace Presentacion.Core.Caja
{
    public partial class _00039_AperturaCaja : Form
    {
        private readonly ICajaServicio _cajaServicio;
        private readonly IConfiguracionServicio _configuracionServicio;

        public bool CajaAbierta { get; set; }

        public _00039_AperturaCaja(ICajaServicio cajaServicio, IConfiguracionServicio configuracionServicio)
        {
            InitializeComponent();

            _cajaServicio = cajaServicio;
            _configuracionServicio = configuracionServicio;

            DoubleBuffered = true;
        }

        private void _00039_AperturaCaja_Load(object sender, System.EventArgs e)
        {
            var config = _configuracionServicio.Obtener();

            if (config.IngresoManualCajaInicial)
            {
                nudMonto.Value = 0;
                nudMonto.Select(0, nudMonto.Text.Length);
                nudMonto.Focus();
            }
            else
            {
                var ultimoValor = _cajaServicio.ObtenerMontoCajaAnterior(Identidad.UsuarioId);

                nudMonto.Value = ultimoValor;
                nudMonto.Select(0, nudMonto.Text.Length);
                nudMonto.Focus();
            }
        }

        private void btnEjecutar_Click(object sender, System.EventArgs e)
        {
            try
            {
                _cajaServicio.Abrir(Identidad.UsuarioId, nudMonto.Value, DateTime.Now);
                MessageBox.Show("Los datos se grabaron correctamente.");
                CajaAbierta = false;
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            nudMonto.Value = 0;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            CajaAbierta = false;
            this.Close();
        }
    }
}
