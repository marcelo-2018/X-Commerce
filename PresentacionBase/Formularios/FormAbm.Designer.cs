namespace PresentacionBase.Formularios
{
    partial class FormAbm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbm));
            this.pnlSeparador = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.btnLimpiar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSeparador
            // 
            this.pnlSeparador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.pnlSeparador.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparador.Location = new System.Drawing.Point(0, 74);
            this.pnlSeparador.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSeparador.Name = "pnlSeparador";
            this.pnlSeparador.Size = new System.Drawing.Size(722, 6);
            this.pnlSeparador.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.SlateGray;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEjecutar,
            this.btnLimpiar,
            this.btnSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toolStrip1.Size = new System.Drawing.Size(722, 74);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnEjecutar.Image = global::PresentacionBase.Properties.Resources.guardar;
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(69, 59);
            this.btnEjecutar.Text = "Grabar";
            this.btnEjecutar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnLimpiar.Image = global::PresentacionBase.Properties.Resources.Limpiar;
            this.btnLimpiar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(74, 59);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSalir.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSalir.Image = global::PresentacionBase.Properties.Resources.Salir;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 59);
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FormAbm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 494);
            this.Controls.Add(this.pnlSeparador);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormAbm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSeparador;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnLimpiar;
        private System.Windows.Forms.ToolStripButton btnSalir;
        protected System.Windows.Forms.ToolStripButton btnEjecutar;
    }
}