namespace Presentacion.Core.Cliente
{
    partial class _00034_ClienteCtaCte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSeparador = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRealizarPago = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelarPago = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.txtCelular = new System.Windows.Forms.TextBox();
            this.lblCelular = new System.Windows.Forms.Label();
            this.txtApyNom = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pnlCriterioBusqueda = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.pnlTotalDeuda = new System.Windows.Forms.Panel();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dgvGrilla = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlCriterioBusqueda.SuspendLayout();
            this.pnlTotalDeuda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrilla)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSeparador
            // 
            this.pnlSeparador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.pnlSeparador.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparador.Location = new System.Drawing.Point(0, 74);
            this.pnlSeparador.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSeparador.Name = "pnlSeparador";
            this.pnlSeparador.Size = new System.Drawing.Size(978, 6);
            this.pnlSeparador.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.SlateGray;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRealizarPago,
            this.btnSalir,
            this.btnCancelarPago,
            this.toolStripSeparator1,
            this.btnActualizar,
            this.btnImprimir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toolStrip1.Size = new System.Drawing.Size(978, 74);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRealizarPago
            // 
            this.btnRealizarPago.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnRealizarPago.Image = global::Presentacion.Core.Properties.Resources.Pagar;
            this.btnRealizarPago.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRealizarPago.Name = "btnRealizarPago";
            this.btnRealizarPago.Size = new System.Drawing.Size(128, 59);
            this.btnRealizarPago.Text = "Realizar Pagos";
            this.btnRealizarPago.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRealizarPago.Click += new System.EventHandler(this.btnRealizarPago_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSalir.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSalir.Image = global::Presentacion.Core.Properties.Resources.Salir;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 59);
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCancelarPago
            // 
            this.btnCancelarPago.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancelarPago.Image = global::Presentacion.Core.Properties.Resources.cerrarCaja;
            this.btnCancelarPago.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelarPago.Name = "btnCancelarPago";
            this.btnCancelarPago.Size = new System.Drawing.Size(127, 59);
            this.btnCancelarPago.Text = "Cancelar Pago";
            this.btnCancelarPago.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelarPago.Click += new System.EventHandler(this.btnCancelarPago_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 64);
            // 
            // btnActualizar
            // 
            this.btnActualizar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnActualizar.Image = global::Presentacion.Core.Properties.Resources.Actualizar;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(92, 59);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnActualizar.ToolTipText = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnImprimir.Image = global::Presentacion.Core.Properties.Resources.imprimir;
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(203, 59);
            this.btnImprimir.Text = "Imprimir Estado Cuenta";
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImprimir.ToolTipText = "Actualizar";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Silver;
            this.groupBox1.Controls.Add(this.txtDni);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnBuscarCliente);
            this.groupBox1.Controls.Add(this.txtCelular);
            this.groupBox1.Controls.Add(this.lblCelular);
            this.groupBox1.Controls.Add(this.txtApyNom);
            this.groupBox1.Controls.Add(this.lblApellido);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 80);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(978, 134);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[ Datos del Cliente ]";
            // 
            // txtDni
            // 
            this.txtDni.Enabled = false;
            this.txtDni.Location = new System.Drawing.Point(238, 78);
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(178, 30);
            this.txtDni.TabIndex = 130;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 25);
            this.label3.TabIndex = 131;
            this.label3.Text = "D.N.I.";
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarCliente.Location = new System.Drawing.Point(874, 37);
            this.btnBuscarCliente.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(50, 38);
            this.btnBuscarCliente.TabIndex = 129;
            this.btnBuscarCliente.Text = "...";
            this.btnBuscarCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBuscarCliente.UseVisualStyleBackColor = true;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // txtCelular
            // 
            this.txtCelular.Enabled = false;
            this.txtCelular.Location = new System.Drawing.Point(561, 78);
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.Size = new System.Drawing.Size(178, 30);
            this.txtCelular.TabIndex = 121;
            // 
            // lblCelular
            // 
            this.lblCelular.AutoSize = true;
            this.lblCelular.Location = new System.Drawing.Point(480, 83);
            this.lblCelular.Name = "lblCelular";
            this.lblCelular.Size = new System.Drawing.Size(74, 25);
            this.lblCelular.TabIndex = 126;
            this.lblCelular.Text = "Celular";
            // 
            // txtApyNom
            // 
            this.txtApyNom.Enabled = false;
            this.txtApyNom.Location = new System.Drawing.Point(238, 38);
            this.txtApyNom.Name = "txtApyNom";
            this.txtApyNom.Size = new System.Drawing.Size(626, 30);
            this.txtApyNom.TabIndex = 114;
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(52, 45);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(171, 25);
            this.lblApellido.TabIndex = 116;
            this.lblApellido.Text = "Apellido y Nombre";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 214);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 6);
            this.panel1.TabIndex = 7;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Location = new System.Drawing.Point(0, 833);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(978, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pnlCriterioBusqueda
            // 
            this.pnlCriterioBusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlCriterioBusqueda.Controls.Add(this.btnBuscar);
            this.pnlCriterioBusqueda.Controls.Add(this.label2);
            this.pnlCriterioBusqueda.Controls.Add(this.label1);
            this.pnlCriterioBusqueda.Controls.Add(this.dtpFechaHasta);
            this.pnlCriterioBusqueda.Controls.Add(this.dtpFechaDesde);
            this.pnlCriterioBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriterioBusqueda.Location = new System.Drawing.Point(0, 220);
            this.pnlCriterioBusqueda.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlCriterioBusqueda.Name = "pnlCriterioBusqueda";
            this.pnlCriterioBusqueda.Size = new System.Drawing.Size(978, 68);
            this.pnlCriterioBusqueda.TabIndex = 9;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(849, 14);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(123, 40);
            this.btnBuscar.TabIndex = 130;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(552, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 22);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fecha hasta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(254, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 22);
            this.label1.TabIndex = 8;
            this.label1.Text = "Fecha desde";
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaHasta.Location = new System.Drawing.Point(672, 18);
            this.dtpFechaHasta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(157, 28);
            this.dtpFechaHasta.TabIndex = 7;
            this.dtpFechaHasta.ValueChanged += new System.EventHandler(this.dtpFechaHasta_ValueChanged);
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaDesde.Location = new System.Drawing.Point(380, 18);
            this.dtpFechaDesde.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(157, 28);
            this.dtpFechaDesde.TabIndex = 6;
            this.dtpFechaDesde.ValueChanged += new System.EventHandler(this.dtpFechaDesde_ValueChanged);
            // 
            // pnlTotalDeuda
            // 
            this.pnlTotalDeuda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlTotalDeuda.Controls.Add(this.txtTotal);
            this.pnlTotalDeuda.Controls.Add(this.lblTotal);
            this.pnlTotalDeuda.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotalDeuda.Location = new System.Drawing.Point(0, 756);
            this.pnlTotalDeuda.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlTotalDeuda.Name = "pnlTotalDeuda";
            this.pnlTotalDeuda.Size = new System.Drawing.Size(978, 77);
            this.pnlTotalDeuda.TabIndex = 10;
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(605, 9);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(361, 53);
            this.txtTotal.TabIndex = 3;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(449, 17);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(138, 40);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "TOTAL";
            // 
            // dgvGrilla
            // 
            this.dgvGrilla.AllowUserToAddRows = false;
            this.dgvGrilla.AllowUserToDeleteRows = false;
            this.dgvGrilla.BackgroundColor = System.Drawing.Color.White;
            this.dgvGrilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrilla.Location = new System.Drawing.Point(0, 288);
            this.dgvGrilla.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvGrilla.MultiSelect = false;
            this.dgvGrilla.Name = "dgvGrilla";
            this.dgvGrilla.ReadOnly = true;
            this.dgvGrilla.RowHeadersVisible = false;
            this.dgvGrilla.RowHeadersWidth = 62;
            this.dgvGrilla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrilla.Size = new System.Drawing.Size(978, 468);
            this.dgvGrilla.TabIndex = 11;
            // 
            // _00034_ClienteCtaCte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 855);
            this.Controls.Add(this.dgvGrilla);
            this.Controls.Add(this.pnlTotalDeuda);
            this.Controls.Add(this.pnlCriterioBusqueda);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlSeparador);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 911);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 911);
            this.Name = "_00034_ClienteCtaCte";
            this.Text = "Cuenta Corriente de Clientes";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlCriterioBusqueda.ResumeLayout(false);
            this.pnlCriterioBusqueda.PerformLayout();
            this.pnlTotalDeuda.ResumeLayout(false);
            this.pnlTotalDeuda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrilla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSeparador;
        private System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton btnRealizarPago;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripButton btnCancelarPago;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.TextBox txtCelular;
        private System.Windows.Forms.Label lblCelular;
        private System.Windows.Forms.TextBox txtApyNom;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnlCriterioBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Panel pnlTotalDeuda;
        private System.Windows.Forms.DataGridView dgvGrilla;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblTotal;
    }
}