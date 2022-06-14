using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IServicio.Persona;
using IServicio.Persona.DTOs;
using PresentacionBase.Formularios;

namespace Presentacion.Core.Cliente
{
    public partial class _00009_Cliente : FormConsulta
    {
        private readonly IClienteServicio _clienteServicio;

        public _00009_Cliente(IClienteServicio clienteServicio)
        {
            InitializeComponent();

            _clienteServicio = clienteServicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            var clientes = (List<ClienteDto>)_clienteServicio
                .Obtener(typeof(ClienteDto), !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty, false);

            // Sacamos el registro de consumidor final para que no se pueda operar con el 
            dgv.DataSource = clientes.Where(x => x.Dni != Aplicacion.Constantes.Cliente.ConsumidorFinal).ToList();

            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["ApyNom"].Visible = true;
            dgv.Columns["ApyNom"].HeaderText = "Apellido y Nombre";
            dgv.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["Dni"].Visible = true;
            dgv.Columns["Dni"].HeaderText = "DNI";

            dgv.Columns["Telefono"].Visible = true;

            dgv.Columns["Mail"].Visible = true;
            dgv.Columns["Mail"].HeaderText = "Correo Electronico";
            dgv.Columns["Mail"].Width = 200;

            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 100;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";
            dgv.Columns["EliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var formulario = new _00010_Abm_Cliente(tipoOperacion, id);

            formulario.ShowDialog();

            return formulario.RealizoAlgunaOperacion;
        }
    }
}
