using System.Windows.Forms;
using IServicio.Articulo.DTOs;
using IServicio.ListaPrecio;
using IServicio.ListaPrecio.DTOs;
using IServicios.Articulo.DTOs;
using IServicios.BajaArticulo;
using IServicios.MotivoBaja;
using Presentacion.Core.Properties;
using PresentacionBase.Formularios;
using StructureMap;

namespace Presentacion.Core.Articulo
{
    public partial class _00030_Abm_BajaArticulos : FormAbm
    {
        private readonly IBajaArticuloServicio _bajaArticuloServicio;
        private readonly IMotivoBajaServicio _motivoBajaServicio;
        private readonly IListaPrecioServicio _listaPrecioServicio;

        private ListaPrecioDto _listaPrecioActual;
        private ArticuloDto _articuloSeleccionado;

        private long articuloId;

        public _00030_Abm_BajaArticulos(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _bajaArticuloServicio = ObjectFactory.GetInstance<IBajaArticuloServicio>();
            _motivoBajaServicio = ObjectFactory.GetInstance<IMotivoBajaServicio>();
            _listaPrecioServicio = ObjectFactory.GetInstance<IListaPrecioServicio>();

            _articuloSeleccionado = null;

            imgFotoArticulo.Image = Resources.ImagenProductoPorDefecto;

            PoblarComboBox(cmbMotivoBaja,
                _motivoBajaServicio.Obtener(string.Empty),
                "Descripcion",
                "Id");

            PoblarComboBox(cmbListaPrecio, _listaPrecioServicio.Obtener(string.Empty, false),
                "Descripcion", "Id");
        }

        public override void CargarDatos(long? entidadId)
        {
            if (entidadId.HasValue)
            {
                var resultado = _bajaArticuloServicio.Obtener(entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrió un problema al obtener la baja de articulos", "Atención");
                    Close();
                }
            }
            else
            {
                LimpiarControles(this);
            }
        }

        private void btnBuscarArticulo_Click_1(object sender, System.EventArgs e)
        {
            var lookUpArticulo = new ArticuloLookUp((long)cmbListaPrecio.SelectedValue);
            lookUpArticulo.ShowDialog();

            if (lookUpArticulo.EntidadSeleccionada != null)
            {
                _articuloSeleccionado = (ArticuloDto) lookUpArticulo.EntidadSeleccionada;

                articuloId = lookUpArticulo.ArticuloSeleccionado.Id;
                txtArticulo.Text = lookUpArticulo.ArticuloSeleccionado.Descripcion;
                nudStockActual.Value = lookUpArticulo.ArticuloSeleccionado.StockActual;
            }
        }

        private void btnNuevoMotivoBaja_Click_1(object sender, System.EventArgs e)
        {
            var fNuevoMotivoBaja = new _00028_Abm_MotivoBaja(TipoOperacion.Nuevo);
            fNuevoMotivoBaja.ShowDialog();
            if (fNuevoMotivoBaja.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbMotivoBaja, _motivoBajaServicio.Obtener(string.Empty), "Descripcion", "Id");
            }
        }

        private void btnAgregarImagen_Click_1(object sender, System.EventArgs e)
        {
            //if (openFile.ShowDialog() == DialogResult.OK)
            //{
            //    imgFotoArticulo.Image = string.IsNullOrEmpty(openFile.FileName)
            //        ? Resources.ImagenProductoPorDefecto
            //        : Image.FromFile(openFile.FileName);
            //}
            //else
            //{
            //    imgFotoArticulo.Image = Resources.ImagenProductoPorDefecto;
            //}
        }
    }
}
