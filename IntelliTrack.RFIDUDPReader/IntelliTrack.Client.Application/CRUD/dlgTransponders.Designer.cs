namespace IntelliTrack.Client.Application
{
  partial class dlgTransponders
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgTransponders));
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.tRACODTRANSPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tRACODCATEGORIADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tRAVEHICULODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tRAACTIVODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tRAUSUARIODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tRAFECHAMODDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tRANSPONDERSBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.cRUDDataSet = new IntelliTrack.Client.Application.CRUD.CRUDDataSet();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txtUsuario = new System.Windows.Forms.TextBox();
      this.txtUltimaModificacion = new System.Windows.Forms.TextBox();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.Salir = new System.Windows.Forms.ToolStripButton();
      this.Nuevo = new System.Windows.Forms.ToolStripButton();
      this.Modificar = new System.Windows.Forms.ToolStripButton();
      this.Eliminar = new System.Windows.Forms.ToolStripButton();
      this.Actualizar = new System.Windows.Forms.ToolStripButton();
      this.tRANSPONDERSTableAdapter = new IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.TRANSPONDERSTableAdapter();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.txtTransponder = new System.Windows.Forms.TextBox();
      this.txtCategoria = new System.Windows.Forms.TextBox();
      this.txtVehiculo = new System.Windows.Forms.TextBox();
      this.chkActivo = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tRANSPONDERSBindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cRUDDataSet)).BeginInit();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AllowUserToOrderColumns = true;
      this.dataGridView1.AutoGenerateColumns = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tRACODTRANSPDataGridViewTextBoxColumn,
            this.tRACODCATEGORIADataGridViewTextBoxColumn,
            this.tRAVEHICULODataGridViewTextBoxColumn,
            this.tRAACTIVODataGridViewTextBoxColumn,
            this.tRAUSUARIODataGridViewTextBoxColumn,
            this.tRAFECHAMODDataGridViewTextBoxColumn});
      this.dataGridView1.DataSource = this.tRANSPONDERSBindingSource;
      this.dataGridView1.Location = new System.Drawing.Point(23, 119);
      this.dataGridView1.MultiSelect = false;
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView1.Size = new System.Drawing.Size(471, 173);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
      // 
      // tRACODTRANSPDataGridViewTextBoxColumn
      // 
      this.tRACODTRANSPDataGridViewTextBoxColumn.DataPropertyName = "TRA_COD_TRANSP";
      this.tRACODTRANSPDataGridViewTextBoxColumn.HeaderText = "TRA_COD_TRANSP";
      this.tRACODTRANSPDataGridViewTextBoxColumn.Name = "tRACODTRANSPDataGridViewTextBoxColumn";
      this.tRACODTRANSPDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // tRACODCATEGORIADataGridViewTextBoxColumn
      // 
      this.tRACODCATEGORIADataGridViewTextBoxColumn.DataPropertyName = "TRA_COD_CATEGORIA";
      this.tRACODCATEGORIADataGridViewTextBoxColumn.HeaderText = "TRA_COD_CATEGORIA";
      this.tRACODCATEGORIADataGridViewTextBoxColumn.Name = "tRACODCATEGORIADataGridViewTextBoxColumn";
      this.tRACODCATEGORIADataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // tRAVEHICULODataGridViewTextBoxColumn
      // 
      this.tRAVEHICULODataGridViewTextBoxColumn.DataPropertyName = "TRA_VEHICULO";
      this.tRAVEHICULODataGridViewTextBoxColumn.HeaderText = "TRA_VEHICULO";
      this.tRAVEHICULODataGridViewTextBoxColumn.Name = "tRAVEHICULODataGridViewTextBoxColumn";
      this.tRAVEHICULODataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // tRAACTIVODataGridViewTextBoxColumn
      // 
      this.tRAACTIVODataGridViewTextBoxColumn.DataPropertyName = "TRA_ACTIVO";
      this.tRAACTIVODataGridViewTextBoxColumn.HeaderText = "TRA_ACTIVO";
      this.tRAACTIVODataGridViewTextBoxColumn.Name = "tRAACTIVODataGridViewTextBoxColumn";
      this.tRAACTIVODataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // tRAUSUARIODataGridViewTextBoxColumn
      // 
      this.tRAUSUARIODataGridViewTextBoxColumn.DataPropertyName = "TRA_USUARIO";
      this.tRAUSUARIODataGridViewTextBoxColumn.HeaderText = "TRA_USUARIO";
      this.tRAUSUARIODataGridViewTextBoxColumn.Name = "tRAUSUARIODataGridViewTextBoxColumn";
      this.tRAUSUARIODataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // tRAFECHAMODDataGridViewTextBoxColumn
      // 
      this.tRAFECHAMODDataGridViewTextBoxColumn.DataPropertyName = "TRA_FECHA_MOD";
      this.tRAFECHAMODDataGridViewTextBoxColumn.HeaderText = "TRA_FECHA_MOD";
      this.tRAFECHAMODDataGridViewTextBoxColumn.Name = "tRAFECHAMODDataGridViewTextBoxColumn";
      this.tRAFECHAMODDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // tRANSPONDERSBindingSource
      // 
      this.tRANSPONDERSBindingSource.DataMember = "TRANSPONDERS";
      this.tRANSPONDERSBindingSource.DataSource = this.cRUDDataSet;
      // 
      // cRUDDataSet
      // 
      this.cRUDDataSet.DataSetName = "CRUDDataSet";
      this.cRUDDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Enabled = false;
      this.label3.Location = new System.Drawing.Point(20, 96);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(43, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Usuario";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Enabled = false;
      this.label4.Location = new System.Drawing.Point(229, 93);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(98, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Ultima modificación";
      // 
      // txtUsuario
      // 
      this.txtUsuario.Enabled = false;
      this.txtUsuario.Location = new System.Drawing.Point(63, 93);
      this.txtUsuario.Name = "txtUsuario";
      this.txtUsuario.Size = new System.Drawing.Size(121, 20);
      this.txtUsuario.TabIndex = 9;
      // 
      // txtUltimaModificacion
      // 
      this.txtUltimaModificacion.Enabled = false;
      this.txtUltimaModificacion.Location = new System.Drawing.Point(333, 93);
      this.txtUltimaModificacion.Name = "txtUltimaModificacion";
      this.txtUltimaModificacion.Size = new System.Drawing.Size(137, 20);
      this.txtUltimaModificacion.TabIndex = 10;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Salir,
            this.Nuevo,
            this.Modificar,
            this.Eliminar,
            this.Actualizar});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(534, 25);
      this.toolStrip1.TabIndex = 11;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // Salir
      // 
      this.Salir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.Salir.Image = ((System.Drawing.Image)(resources.GetObject("Salir.Image")));
      this.Salir.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.Salir.Name = "Salir";
      this.Salir.Size = new System.Drawing.Size(23, 22);
      this.Salir.Text = "Salir";
      this.Salir.Click += new System.EventHandler(this.Salir_Click);
      // 
      // Nuevo
      // 
      this.Nuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.Nuevo.Image = ((System.Drawing.Image)(resources.GetObject("Nuevo.Image")));
      this.Nuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.Nuevo.Name = "Nuevo";
      this.Nuevo.Size = new System.Drawing.Size(23, 22);
      this.Nuevo.Text = "Nuevo";
      this.Nuevo.Click += new System.EventHandler(this.Nuevo_Click);
      // 
      // Modificar
      // 
      this.Modificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.Modificar.Image = ((System.Drawing.Image)(resources.GetObject("Modificar.Image")));
      this.Modificar.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.Modificar.Name = "Modificar";
      this.Modificar.Size = new System.Drawing.Size(23, 22);
      this.Modificar.Text = "Modificar";
      this.Modificar.Click += new System.EventHandler(this.Modificar_Click);
      // 
      // Eliminar
      // 
      this.Eliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.Eliminar.Image = ((System.Drawing.Image)(resources.GetObject("Eliminar.Image")));
      this.Eliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.Eliminar.Name = "Eliminar";
      this.Eliminar.Size = new System.Drawing.Size(23, 22);
      this.Eliminar.Text = "Eliminar";
      this.Eliminar.Click += new System.EventHandler(this.Eliminar_Click);
      // 
      // Actualizar
      // 
      this.Actualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.Actualizar.Image = ((System.Drawing.Image)(resources.GetObject("Actualizar.Image")));
      this.Actualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.Actualizar.Name = "Actualizar";
      this.Actualizar.Size = new System.Drawing.Size(23, 22);
      this.Actualizar.Text = "Actualizar";
      this.Actualizar.Click += new System.EventHandler(this.Actualizar_Click);
      // 
      // tRANSPONDERSTableAdapter
      // 
      this.tRANSPONDERSTableAdapter.ClearBeforeFill = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(20, 31);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(67, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "Transponder";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(253, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(54, 13);
      this.label2.TabIndex = 13;
      this.label2.Text = "Categoría";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(24, 57);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(50, 13);
      this.label5.TabIndex = 14;
      this.label5.Text = "Vehículo";
      // 
      // txtTransponder
      // 
      this.txtTransponder.Location = new System.Drawing.Point(93, 29);
      this.txtTransponder.Name = "txtTransponder";
      this.txtTransponder.Size = new System.Drawing.Size(75, 20);
      this.txtTransponder.TabIndex = 15;
      // 
      // txtCategoria
      // 
      this.txtCategoria.Location = new System.Drawing.Point(310, 36);
      this.txtCategoria.Name = "txtCategoria";
      this.txtCategoria.Size = new System.Drawing.Size(121, 20);
      this.txtCategoria.TabIndex = 16;
      // 
      // txtVehiculo
      // 
      this.txtVehiculo.Location = new System.Drawing.Point(77, 60);
      this.txtVehiculo.Name = "txtVehiculo";
      this.txtVehiculo.Size = new System.Drawing.Size(86, 20);
      this.txtVehiculo.TabIndex = 17;
      // 
      // chkActivo
      // 
      this.chkActivo.AutoSize = true;
      this.chkActivo.Location = new System.Drawing.Point(256, 62);
      this.chkActivo.Name = "chkActivo";
      this.chkActivo.Size = new System.Drawing.Size(56, 17);
      this.chkActivo.TabIndex = 18;
      this.chkActivo.Text = "Activo";
      this.chkActivo.UseVisualStyleBackColor = true;
      // 
      // dlgTransponders
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(534, 302);
      this.ControlBox = false;
      this.Controls.Add(this.chkActivo);
      this.Controls.Add(this.txtVehiculo);
      this.Controls.Add(this.txtCategoria);
      this.Controls.Add(this.txtTransponder);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.txtUltimaModificacion);
      this.Controls.Add(this.txtUsuario);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.dataGridView1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "dlgTransponders";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Transponders";
      this.Load += new System.EventHandler(this.dlgTransponders_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tRANSPONDERSBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cRUDDataSet)).EndInit();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtUsuario;
    private System.Windows.Forms.TextBox txtUltimaModificacion;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton Salir;
    private System.Windows.Forms.ToolStripButton Nuevo;
    private System.Windows.Forms.ToolStripButton Modificar;
    private System.Windows.Forms.ToolStripButton Eliminar;
    private System.Windows.Forms.ToolStripButton Actualizar;
    private IntelliTrack.Client.Application.CRUD.CRUDDataSet cRUDDataSet;
    private System.Windows.Forms.BindingSource tRANSPONDERSBindingSource;
    private IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.TRANSPONDERSTableAdapter tRANSPONDERSTableAdapter;
    private System.Windows.Forms.DataGridViewTextBoxColumn tRACODTRANSPDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn tRACODCATEGORIADataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn tRAVEHICULODataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn tRAACTIVODataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn tRAUSUARIODataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn tRAFECHAMODDataGridViewTextBoxColumn;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtTransponder;
    private System.Windows.Forms.TextBox txtCategoria;
    private System.Windows.Forms.TextBox txtVehiculo;
    private System.Windows.Forms.CheckBox chkActivo;

  }
}