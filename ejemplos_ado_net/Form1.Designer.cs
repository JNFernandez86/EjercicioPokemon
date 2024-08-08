namespace ejemplos_ado_net
{
    partial class frmElementos
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
            this.dgvElementos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvElementos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvElementos
            // 
            this.dgvElementos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvElementos.Location = new System.Drawing.Point(60, 49);
            this.dgvElementos.Name = "dgvElementos";
            this.dgvElementos.Size = new System.Drawing.Size(231, 283);
            this.dgvElementos.TabIndex = 0;
            // 
            // frmElementos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 404);
            this.Controls.Add(this.dgvElementos);
            this.Name = "frmElementos";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmElementos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvElementos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvElementos;
    }
}