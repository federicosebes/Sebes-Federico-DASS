namespace TpCrudSql
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.DtaInfo = new System.Windows.Forms.DataGridView();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnNuevoAuto = new System.Windows.Forms.Button();
            this.BtnSalir = new System.Windows.Forms.Button();
            this.BtnEditar = new System.Windows.Forms.Button();
            this.BtnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DtaInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // DtaInfo
            // 
            this.DtaInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DtaInfo.Location = new System.Drawing.Point(1, 1);
            this.DtaInfo.Name = "DtaInfo";
            this.DtaInfo.Size = new System.Drawing.Size(436, 466);
            this.DtaInfo.TabIndex = 0;
            this.DtaInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtaInfo_CellContentClick);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(466, 402);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(129, 23);
            this.BtnRefresh.TabIndex = 1;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnNuevoAuto
            // 
            this.BtnNuevoAuto.Location = new System.Drawing.Point(466, 315);
            this.BtnNuevoAuto.Name = "BtnNuevoAuto";
            this.BtnNuevoAuto.Size = new System.Drawing.Size(129, 23);
            this.BtnNuevoAuto.TabIndex = 2;
            this.BtnNuevoAuto.Text = "Agregar  nuevo auto";
            this.BtnNuevoAuto.UseVisualStyleBackColor = true;
            this.BtnNuevoAuto.Click += new System.EventHandler(this.BtnNuevoAuto_Click);
            // 
            // BtnSalir
            // 
            this.BtnSalir.Location = new System.Drawing.Point(466, 431);
            this.BtnSalir.Name = "BtnSalir";
            this.BtnSalir.Size = new System.Drawing.Size(129, 25);
            this.BtnSalir.TabIndex = 3;
            this.BtnSalir.Text = "Salir";
            this.BtnSalir.UseVisualStyleBackColor = true;
            this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // BtnEditar
            // 
            this.BtnEditar.Location = new System.Drawing.Point(466, 344);
            this.BtnEditar.Name = "BtnEditar";
            this.BtnEditar.Size = new System.Drawing.Size(129, 23);
            this.BtnEditar.TabIndex = 4;
            this.BtnEditar.Text = "Editar Auto";
            this.BtnEditar.UseVisualStyleBackColor = true;
            this.BtnEditar.Click += new System.EventHandler(this.BtnEditar_Click);
            // 
            // BtnEliminar
            // 
            this.BtnEliminar.Location = new System.Drawing.Point(466, 373);
            this.BtnEliminar.Name = "BtnEliminar";
            this.BtnEliminar.Size = new System.Drawing.Size(129, 23);
            this.BtnEliminar.TabIndex = 5;
            this.BtnEliminar.Text = "Eliminar Auto";
            this.BtnEliminar.UseVisualStyleBackColor = true;
            this.BtnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 468);
            this.Controls.Add(this.BtnEliminar);
            this.Controls.Add(this.BtnEditar);
            this.Controls.Add(this.BtnSalir);
            this.Controls.Add(this.BtnNuevoAuto);
            this.Controls.Add(this.BtnRefresh);
            this.Controls.Add(this.DtaInfo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DtaInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DtaInfo;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnNuevoAuto;
        private System.Windows.Forms.Button BtnSalir;
        private System.Windows.Forms.Button BtnEditar;
        private System.Windows.Forms.Button BtnEliminar;
    }
}

