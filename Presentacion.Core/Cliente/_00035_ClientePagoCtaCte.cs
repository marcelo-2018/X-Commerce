using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Aplicacion.Constantes;
using IServicio.Persona.DTOs;
using IServicios.Comprobante.DTOs;
using IServicios.CuentaCorriente;
using PresentacionBase.Formularios;
using StructureMap;

namespace Presentacion.Core.Cliente
{
    public partial class _00035_ClientePagoCtaCte : FormBase
    {
        private readonly ClienteDto _cliente;
        private ICuentaCorrienteServicio _cuentaCorrienteServicio;

        public bool RealizarPago { get; set; }

        public _00035_ClientePagoCtaCte(ClienteDto cliente)
        {
            InitializeComponent();

            _cliente = cliente;

            _cuentaCorrienteServicio = ObjectFactory.GetInstance<ICuentaCorrienteServicio>();

            RealizarPago = false;
        }

        private void _00035_ClientePagoCtaCte_Load(object sender, System.EventArgs e)
        {
            if (_cliente != null)
            {
                var deuda = _cuentaCorrienteServicio.ObtenerDeudaCliente(_cliente.Id);

                nudMontoDeuda.Value = deuda >= 0 ? deuda : deuda * -1;

                nudMontoPagar.Select(0, nudMontoPagar.Text.Length);

                nudMontoPagar.Focus();
            }
        }

        private void btnSalir_Click(object sender, System.EventArgs e)
        {
            RealizarPago = false;
            Close();
        }

        private void btnLimpiar_Click(object sender, System.EventArgs e)
        {
            nudMontoPagar.Value = 0;
            nudMontoPagar.Select(0, nudMontoPagar.Text.Length);
            nudMontoPagar.Focus();
        }

        private void btnPagar_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (nudMontoPagar.Value > 0)
                {
                    if (nudMontoPagar.Value > nudMontoDeuda.Value)
                    {
                        var mensaje = "El monto que esta pagando es Mayor al monto adeudado" + Environment.NewLine;

                        if (MessageBox.Show(mensaje, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                            == DialogResult.Cancel)
                            return;
                    }

                    var comprobanteNuevo = new CtaCteComprobanteDto
                    {
                        ClienteId = _cliente.Id,
                        Descuento = 0,
                        SubTotal = nudMontoPagar.Value,
                        Total = nudMontoPagar.Value,
                        EmpleadoId = Identidad.EmpleadoId,
                        UsuarioId = Identidad.UsuarioId,
                        Fecha = DateTime.Now,
                        Iva105 = 0,
                        Iva21 = 0,
                        TipoComprobante = TipoComprobante.CuentaCorriente,
                        FormasDePagos = new List<FormaPagoDto>(),
                        Items = new List<DetalleComprobanteDto>(),
                        Eliminado = false
                    };

                    comprobanteNuevo.FormasDePagos.Add(new FormaPagoCtaCteDto
                    {
                        ClienteId = _cliente.Id,
                        Monto = nudMontoPagar.Value,
                        TipoPago = TipoPago.CtaCte,
                        Eliminado = false
                    });

                    _cuentaCorrienteServicio.Pagar(comprobanteNuevo);

                    MessageBox.Show("Los datos se grabaron Correctamente");
                    Close();

                }
                else
                {
                    MessageBox.Show("Por favor ingrese un monto mayor a Cero.");
                    nudMontoPagar.Select(0, nudMontoPagar.Text.Length);
                    nudMontoPagar.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un Error al realizar el Pago");
                Close();
            }
        }
    }
}
