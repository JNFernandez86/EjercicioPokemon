namespace ejemplos_ado_net
{
    partial class frmAgregarElementos
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
            this.lblNuevoElemento = new System.Windows.Forms.Label();
            this.txtNuevoElemento = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNuevoElemento
            // 
            this.lblNuevoElemento.AutoSize = true;
            this.lblNuevoElemento.Location = new System.Drawing.Point(12, 30);
            this.lblNuevoElemento.Name = "lblNuevoElemento";
            this.lblNuevoElemento.Size = new System.Drawing.Size(134, 20);
            this.lblNuevoElemento.TabIndex = 0;
            this.lblNuevoElemento.Text = "Nuevo Elemento: ";
            // 
            // txtNuevoElemento
            // 
            this.txtNuevoElemento.Location = new System.Drawing.Point(162, 27);
            this.txtNuevoElemento.Name = "txtNuevoElemento";
            this.txtNuevoElemento.Size = new System.Drawing.Size(187, 26);
            this.txtNuevoElemento.TabIndex = 1;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(162, 86);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(87, 35);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(263, 87);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 34);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmAgregarElementos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 148);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtNuevoElemento);
            this.Controls.Add(this.lblNuevoElemento);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAgregarElementos";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar Elementos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAgregarElementos_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNuevoElemento;
        private System.Windows.Forms.TextBox txtNuevoElemento;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelar;
    }
}