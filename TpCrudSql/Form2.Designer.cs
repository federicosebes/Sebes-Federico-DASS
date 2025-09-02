namespace TpCrudSql
{
    partial class Form2
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
            this.LblModelo = new System.Windows.Forms.Label();
            this.LblFecha = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEdad = new System.Windows.Forms.TextBox();
            this.BtnAgregar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblModelo
            // 
            this.LblModelo.AutoSize = true;
            this.LblModelo.Location = new System.Drawing.Point(12, 20);
            this.LblModelo.Name = "LblModelo";
            this.LblModelo.Size = new System.Drawing.Size(42, 13);
            this.LblModelo.TabIndex = 0;
            this.LblModelo.Text = "Modelo";
            // 
            // LblFecha
            // 
            this.LblFecha.AutoSize = true;
            this.LblFecha.Location = new System.Drawing.Point(12, 48);
            this.LblFecha.Name = "LblFecha";
            this.LblFecha.Size = new System.Drawing.Size(26, 13);
            this.LblFecha.TabIndex = 1;
            this.LblFecha.Text = "Año";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(71, 17);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 2;
            // 
            // txtEdad
            // 
            this.txtEdad.Location = new System.Drawing.Point(71, 45);
            this.txtEdad.Name = "txtEdad";
            this.txtEdad.Size = new System.Drawing.Size(100, 20);
            this.txtEdad.TabIndex = 3;
            // 
            // BtnAgregar
            // 
            this.BtnAgregar.Location = new System.Drawing.Point(12, 83);
            this.BtnAgregar.Name = "BtnAgregar";
            this.BtnAgregar.Size = new System.Drawing.Size(159, 30);
            this.BtnAgregar.TabIndex = 4;
            this.BtnAgregar.Text = "Agregar";
            this.BtnAgregar.UseVisualStyleBackColor = true;
            this.BtnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 145);
            this.Controls.Add(this.BtnAgregar);
            this.Controls.Add(this.txtEdad);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.LblFecha);
            this.Controls.Add(this.LblModelo);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblModelo;
        private System.Windows.Forms.Label LblFecha;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.Button BtnAgregar;
    }
}