namespace Presentacion.Core.Articulo
{
    partial class _00026_Abm_Iva
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.nudPorcentaje = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudPorcentaje)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 202);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(183, 197);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(430, 30);
            this.txtDescripcion.TabIndex = 8;
            // 
            // nudPorcentaje
            // 
            this.nudPorcentaje.DecimalPlaces = 2;
            this.nudPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPorcentaje.Location = new System.Drawing.Point(183, 252);
            this.nudPorcentaje.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPorcentaje.Name = "nudPorcentaje";
            this.nudPorcentaje.Size = new System.Drawing.Size(191, 30);
            this.nudPorcentaje.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(54, 250);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Porcentaje";
            // 
            // _00026_Abm_Iva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 322);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudPorcentaje);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDescripcion);
            this.Margin = new System.Windows.Forms.Padding(9, 12, 9, 12);
            this.MaximumSize = new System.Drawing.Size(712, 378);
            this.MinimumSize = new System.Drawing.Size(712, 378);
            this.Name = "_00026_Abm_Iva";
            this.Text = "IVA (Alta, Baja y Modificación)";
            this.Controls.SetChildIndex(this.txtDescripcion, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.nudPorcentaje, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudPorcentaje)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.NumericUpDown nudPorcentaje;
        private System.Windows.Forms.Label label2;
    }
}