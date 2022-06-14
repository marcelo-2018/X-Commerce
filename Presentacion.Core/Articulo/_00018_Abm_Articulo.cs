﻿using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicio.Articulo;
using IServicio.Articulo.DTOs;
using IServicio.Iva;
using IServicio.Marca;
using IServicio.Rubro;
using IServicio.UnidadMedida;
using IServicios.Articulo.DTOs;
using Presentacion.Core.Properties;
using PresentacionBase.Formularios;
using StructureMap;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.Articulo
{
    public partial class _00018_Abm_Articulo : FormAbm
    {
        private readonly IArticuloServicio _articuloServicio;
        private readonly IMarcaServicio _marcaServicio;
        private readonly IRubroServicio _rubroServicio;
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;
        private readonly IIvaServicio _ivaServicio;

        public _00018_Abm_Articulo(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _articuloServicio = ObjectFactory.GetInstance<IArticuloServicio>();
            _marcaServicio = ObjectFactory.GetInstance<IMarcaServicio>();
            _rubroServicio = ObjectFactory.GetInstance<IRubroServicio>();
            _unidadMedidaServicio = ObjectFactory.GetInstance<IUnidadMedidaServicio>();
            _ivaServicio = ObjectFactory.GetInstance<IIvaServicio>();

            imgFoto.Image = Resources.ImagenProductoPorDefecto;

            PoblarComboBox(cmbMarca, _marcaServicio.Obtener(string.Empty, false), "Descripcion", "Id");
            PoblarComboBox(cmbRubro, _rubroServicio.Obtener(string.Empty, false), "Descripcion", "Id");
            PoblarComboBox(cmbUnidad, _unidadMedidaServicio.Obtener(string.Empty, false), "Descripcion", "Id");
            PoblarComboBox(cmbIva, _ivaServicio.Obtener(string.Empty, false), "Descripcion", "Id");

            AsignarEvento_EnterLeave(this);

            txtcodigoBarra.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoLetras(sender, args);
            };

            txtDescripcion.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
            };

            txtAbreviatura.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
                NoSimbolos(sender, args);
                NoNumeros(sender, args);
            };

            txtDetalle.KeyPress += delegate (object sender, KeyPressEventArgs args)
            {
                NoInyeccion(sender, args);
            };
        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);

            if (entidadId.HasValue)
            {
                groupPrecio.Enabled = false;
                nudStock.Enabled = false;

                var resultado = (ArticuloDto)_articuloServicio.Obtener(entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                // ==================================================== //
                // ===============     Datos del Articulo    ========== //
                // ==================================================== //

                txtCodigo.Text = resultado.Codigo.ToString();
                txtcodigoBarra.Text = resultado.CodigoBarra;
                txtDescripcion.Text = resultado.Descripcion;
                txtAbreviatura.Text = resultado.Abreviatura;
                txtDetalle.Text = resultado.Detalle;
                txtUbicacion.Text = resultado.Ubicacion;
                cmbMarca.SelectedValue = resultado.MarcaId;
                cmbRubro.SelectedValue = resultado.RubroId;
                cmbUnidad.SelectedValue = resultado.UnidadMedidaId;
                cmbIva.SelectedValue = resultado.IvaId;

                // ==================================================== //
                // ===============     Datos del Generales   ========== //
                // ==================================================== //

                nudStockMin.Value = resultado.StockMinimo;
                chkDescontarStock.Checked = resultado.DescuentaStock;
                chkPermitirStockNeg.Checked = resultado.PermiteStockNegativo;
                chkActivarLimite.Checked = resultado.ActivarLimiteVenta;
                nudLimiteVenta.Value = resultado.LimiteVenta;
                chkActivarHoraVenta.Checked = resultado.ActivarHoraVenta;
                dtpHoraDesde.Value = resultado.HoraLimiteVentaDesde;
                dtpHoraHasta.Value = resultado.HoraLimiteVentaHasta;

                // ==================================================== //
                // ===============     Foto del Articulo    ========== //
                // ==================================================== //

                imgFoto.Image = Imagen.ConvertirImagen(resultado.Foto);

                if (TipoOperacion == TipoOperacion.Eliminar)                    
                    DesactivarControles(this);
            }
            else // Nuevo
            {
                btnEjecutar.Text = "Grabar";
                LimpiarControles(this);

                txtCodigo.Enabled = false;
                txtCodigo.Text = _articuloServicio.ObtenerSiguienteNroCodigo().ToString();
            }
        }

        public override void EjecutarPostLimpieza()
        {
            txtCodigo.Text = _articuloServicio.ObtenerSiguienteNroCodigo().ToString();
        }

        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text)) return false;

            if (string.IsNullOrEmpty(txtcodigoBarra.Text)) return false;

            if (string.IsNullOrEmpty(txtDescripcion.Text)) return false;

            if (cmbMarca.Items.Count <= 0) return false;

            if (cmbRubro.Items.Count <= 0) return false;

            if (cmbUnidad.Items.Count <= 0) return false;

            if (cmbIva.Items.Count <= 0) return false;

            return true; 
        }

        public override bool VerificarSiExiste(long? id = null)
        {
            return _articuloServicio.VerificarSiExiste(txtDescripcion.Text, id);
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegistro = new ArticuloCrudDto
            {
                Codigo = int.Parse(txtCodigo.Text),
                CodigoBarra = txtcodigoBarra.Text,
                Descripcion = txtDescripcion.Text,
                Abreviatura = txtAbreviatura.Text,
                Detalle = txtDetalle.Text,
                Ubicacion = txtUbicacion.Text,
                MarcaId = (long)cmbMarca.SelectedValue,
                RubroId = (long)cmbRubro.SelectedValue,
                UnidadMedidaId = (long)cmbUnidad.SelectedValue,
                IvaId = (long)cmbIva.SelectedValue,
                PrecioCosto = nudPrecioCosto.Value,
                //------------------------------------------------//
                StockActual = nudStock.Value,
                StockMinimo = nudStockMin.Value,
                DescuentaStock = chkDescontarStock.Checked,
                PermiteStockNegativo = chkPermitirStockNeg.Checked,
                ActivarLimiteVenta = chkActivarLimite.Checked,
                LimiteVenta = nudLimiteVenta.Value,
                ActivarHoraVenta = chkActivarHoraVenta.Checked,
                HoraLimiteVentaDesde = dtpHoraDesde.Value,
                HoraLimiteVentaHasta = dtpHoraHasta.Value,
                //------------------------------------------------//
                Foto = Imagen.ConvertirImagen(imgFoto.Image),
                Eliminado = false
            };

            _articuloServicio.Insertar(nuevoRegistro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new ArticuloCrudDto
            {
                Id = EntidadId.Value,
                Codigo = int.Parse(txtCodigo.Text),
                CodigoBarra = txtcodigoBarra.Text,
                Descripcion = txtDescripcion.Text,
                Abreviatura = txtAbreviatura.Text,
                Detalle = txtDetalle.Text,
                Ubicacion = txtUbicacion.Text,
                MarcaId = (long)cmbMarca.SelectedValue,
                RubroId = (long)cmbRubro.SelectedValue,
                UnidadMedidaId = (long)cmbUnidad.SelectedValue,
                IvaId = (long)cmbIva.SelectedValue,
                PrecioCosto = nudPrecioCosto.Value,
                //------------------------------------------------//
                StockActual = nudStock.Value,
                StockMinimo = nudStockMin.Value,
                DescuentaStock = chkDescontarStock.Checked,
                PermiteStockNegativo = chkPermitirStockNeg.Checked,
                ActivarLimiteVenta = chkActivarLimite.Checked,
                LimiteVenta = nudLimiteVenta.Value,
                ActivarHoraVenta = chkActivarHoraVenta.Checked,
                HoraLimiteVentaDesde = dtpHoraDesde.Value,
                HoraLimiteVentaHasta = dtpHoraHasta.Value,
                //------------------------------------------------//
                Foto = Imagen.ConvertirImagen(imgFoto.Image),
                Eliminado = false

            };

            _articuloServicio.Modificar(modificarRegistro);
        }


        public override void EjecutarComandoEliminar()
        {
            _articuloServicio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);

            txtCodigo.Text = _articuloServicio.ObtenerSiguienteNroCodigo().ToString();
            txtcodigoBarra.Focus();
        }

        private void btnNuevaMarca_Click(object sender, System.EventArgs e)
        {
            var fNuevaMarca = new _00022_Abm_Marca(TipoOperacion.Nuevo);
            fNuevaMarca.ShowDialog();
            if (fNuevaMarca.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbMarca, _marcaServicio.Obtener(string.Empty, false));
            }
        }

        private void btnNuevoRubro_Click(object sender, System.EventArgs e)
        {
            var fNuevoRubro = new _00020_Abm_Rubro(TipoOperacion.Nuevo);
            fNuevoRubro.ShowDialog();
            if (fNuevoRubro.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbRubro, _rubroServicio.Obtener(string.Empty, false));
            }
        }

        private void btnNuevaUnidad_Click(object sender, System.EventArgs e)
        {
            var fNuevaUnidad = new _00024_Abm_UnidadDeMedida(TipoOperacion.Nuevo);
            fNuevaUnidad.ShowDialog();
            if (fNuevaUnidad.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbUnidad, _unidadMedidaServicio.Obtener(string.Empty, false));
            }
        }

        private void btnNuevoIva_Click(object sender, System.EventArgs e)
        {
            var fNuevaIva = new _00026_Abm_Iva(TipoOperacion.Nuevo);
            fNuevaIva.ShowDialog();
            if (fNuevaIva.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbIva, _ivaServicio.Obtener(string.Empty, false));
            }
        }

        private void btnAgregarImagen_Click(object sender, System.EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                imgFoto.Image = string.IsNullOrEmpty(openFile.FileName)
                    ? Resources.ImagenProductoPorDefecto
                    : Image.FromFile(openFile.FileName);
            }
            else
            {
                imgFoto.Image = Resources.ImagenProductoPorDefecto;
            }
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
