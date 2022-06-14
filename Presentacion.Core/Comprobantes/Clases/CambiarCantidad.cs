using System.Windows.Forms;

namespace Presentacion.Core.Comprobantes.Clases
{
    public partial class CambiarCantidad : Form
    {
        private ItemView _itemSeleccionado;

        public ItemView Item => _itemSeleccionado;

        public CambiarCantidad(ItemView item)
        {
            InitializeComponent();

            _itemSeleccionado = item;
        }

        private void CambiarCantidad_Load(object sender, System.EventArgs e)
        {
            if (_itemSeleccionado == null)
            {
                MessageBox.Show("Ocurrio un Error al Obtener el Articulo");
                Close();
            }

            lblArticulo.Text = _itemSeleccionado.Descripcion;
            nudCantidad.Value = _itemSeleccionado.Cantidad;
            nudCantidad.Select(0, nudCantidad.Text.Length);
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            _itemSeleccionado = null;
            this.Close();
        }

        private void btnAceptar_Click(object sender, System.EventArgs e)
        {
            _itemSeleccionado.Cantidad = nudCantidad.Value;
            Close();
        }
    }
}
