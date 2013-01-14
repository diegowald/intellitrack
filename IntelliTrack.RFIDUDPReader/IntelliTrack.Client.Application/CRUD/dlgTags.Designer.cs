namespace IntelliTrack.Client.Application
{
  partial class dlgTags
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgTags));
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.rFITAGDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.rFICATEGORIADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.rFIVEHICULODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.rFIACTIVODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.rFIDUSUARIODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.rFIDFECHAACTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tAGSBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
      this.tAGSTableAdapter = new IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.TAGSTableAdapter();
      this.label1 = new System.Windows.Forms.Label();
      this.txtTAG = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtCategoria = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtVehiculo = new System.Windows.Forms.TextBox();
      this.chkActivo = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tAGSBindingSource)).BeginInit();
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
            this.rFITAGDataGridViewTextBoxColumn,
            this.rFICATEGORIADataGridViewTextBoxColumn,
            this.rFIVEHICULODataGridViewTextBoxColumn,
            this.rFIACTIVODataGridViewTextBoxColumn,
            this.rFIDUSUARIODataGridViewTextBoxColumn,
            this.rFIDFECHAACTDataGridViewTextBoxColumn});
      this.dataGridView1.DataSource = this.tAGSBindingSource;
      this.dataGridView1.Location = new System.Drawing.Point(23, 119);
      this.dataGridView1.MultiSelect = false;
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView1.Size = new System.Drawing.Size(471, 173);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
      // 
      // rFITAGDataGridViewTextBoxColumn
      // 
      this.rFITAGDataGridViewTextBoxColumn.DataPropertyName = "RFI_TAG";
      this.rFITAGDataGridViewTextBoxColumn.HeaderText = "RFI_TAG";
      this.rFITAGDataGridViewTextBoxColumn.Name = "rFITAGDataGridViewTextBoxColumn";
      this.rFITAGDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // rFICATEGORIADataGridViewTextBoxColumn
      // 
      this.rFICATEGORIADataGridViewTextBoxColumn.DataPropertyName = "RFI_CATEGORIA";
      this.rFICATEGORIADataGridViewTextBoxColumn.HeaderText = "RFI_CATEGORIA";
      this.rFICATEGORIADataGridViewTextBoxColumn.Name = "rFICATEGORIADataGridViewTextBoxColumn";
      this.rFICATEGORIADataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // rFIVEHICULODataGridViewTextBoxColumn
      // 
      this.rFIVEHICULODataGridViewTextBoxColumn.DataPropertyName = "RFI_VEHICULO";
      this.rFIVEHICULODataGridViewTextBoxColumn.HeaderText = "RFI_VEHICULO";
      this.rFIVEHICULODataGridViewTextBoxColumn.Name = "rFIVEHICULODataGridViewTextBoxColumn";
      this.rFIVEHICULODataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // rFIACTIVODataGridViewTextBoxColumn
      // 
      this.rFIACTIVODataGridViewTextBoxColumn.DataPropertyName = "RFI_ACTIVO";
      this.rFIACTIVODataGridViewTextBoxColumn.HeaderText = "RFI_ACTIVO";
      this.rFIACTIVODataGridViewTextBoxColumn.Name = "rFIACTIVODataGridViewTextBoxColumn";
      this.rFIACTIVODataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // rFIDUSUARIODataGridViewTextBoxColumn
      // 
      this.rFIDUSUARIODataGridViewTextBoxColumn.DataPropertyName = "RFID_USUARIO";
      this.rFIDUSUARIODataGridViewTextBoxColumn.HeaderText = "RFID_USUARIO";
      this.rFIDUSUARIODataGridViewTextBoxColumn.Name = "rFIDUSUARIODataGridViewTextBoxColumn";
      this.rFIDUSUARIODataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // rFIDFECHAACTDataGridViewTextBoxColumn
      // 
      this.rFIDFECHAACTDataGridViewTextBoxColumn.DataPropertyName = "RFID_FECHA_ACT";
      this.rFIDFECHAACTDataGridViewTextBoxColumn.HeaderText = "RFID_FECHA_ACT";
      this.rFIDFECHAACTDataGridViewTextBoxColumn.Name = "rFIDFECHAACTDataGridViewTextBoxColumn";
      this.rFIDFECHAACTDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // tAGSBindingSource
      // 
      this.tAGSBindingSource.DataMember = "TAGS";
      this.tAGSBindingSource.DataSource = this.cRUDDataSet;
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
      this.toolStrip1.Size = new System.Drawing.Size(508, 25);
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
      // tAGSTableAdapter
      // 
      this.tAGSTableAdapter.ClearBeforeFill = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(29, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "TAG";
      // 
      // txtTAG
      // 
      this.txtTAG.Location = new System.Drawing.Point(55, 29);
      this.txtTAG.Name = "txtTAG";
      this.txtTAG.Size = new System.Drawing.Size(100, 20);
      this.txtTAG.TabIndex = 13;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(177, 35);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(54, 13);
      this.label2.TabIndex = 14;
      this.label2.Text = "Categoría";
      // 
      // txtCategoria
      // 
      this.txtCategoria.Location = new System.Drawing.Point(234, 35);
      this.txtCategoria.Name = "txtCategoria";
      this.txtCategoria.Size = new System.Drawing.Size(81, 20);
      this.txtCategoria.TabIndex = 15;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(23, 62);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(50, 13);
      this.label5.TabIndex = 16;
      this.label5.Text = "Vehículo";
      // 
      // txtVehiculo
      // 
      this.txtVehiculo.Location = new System.Drawing.Point(80, 63);
      this.txtVehiculo.Name = "txtVehiculo";
      this.txtVehiculo.Size = new System.Drawing.Size(100, 20);
      this.txtVehiculo.TabIndex = 17;
      // 
      // chkActivo
      // 
      this.chkActivo.AutoSize = true;
      this.chkActivo.Location = new System.Drawing.Point(234, 66);
      this.chkActivo.Name = "chkActivo";
      this.chkActivo.Size = new System.Drawing.Size(56, 17);
      this.chkActivo.TabIndex = 18;
      this.chkActivo.Text = "Activo";
      this.chkActivo.UseVisualStyleBackColor = true;
      // 
      // dlgTags
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(508, 302);
      this.ControlBox = false;
      this.Controls.Add(this.chkActivo);
      this.Controls.Add(this.txtVehiculo);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtCategoria);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtTAG);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.txtUltimaModificacion);
      this.Controls.Add(this.txtUsuario);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.dataGridView1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "dlgTags";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Tags";
      this.Load += new System.EventHandler(this.dlgTags_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tAGSBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cRUDDataSet)).EndInit();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
    private IntelliTrack.Client.Application.CRUD.CRUDDataSet cRUDDataSet;
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
    private System.Windows.Forms.BindingSource tAGSBindingSource;
    private IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.TAGSTableAdapter tAGSTableAdapter;
    private System.Windows.Forms.DataGridViewTextBoxColumn rFITAGDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn rFICATEGORIADataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn rFIVEHICULODataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn rFIACTIVODataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn rFIDUSUARIODataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn rFIDFECHAACTDataGridViewTextBoxColumn;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtTAG;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtCategoria;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtVehiculo;
    private System.Windows.Forms.CheckBox chkActivo;

  }
}