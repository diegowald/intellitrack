namespace IntelliTrack.Client.Application
{
  partial class dlgCategorias
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgCategorias));
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.cATIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cATDESCRIPCIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cATEDITABLEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cATUSUARIODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cATFECHAMODDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cATEGORIASBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.cRUDDataSet = new IntelliTrack.Client.Application.CRUD.CRUDDataSet();
      this.label1 = new System.Windows.Forms.Label();
      this.txtCodigo = new System.Windows.Forms.TextBox();
      this.cATEGORIASTableAdapter = new IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.CATEGORIASTableAdapter();
      this.label2 = new System.Windows.Forms.Label();
      this.txtDescripcion = new System.Windows.Forms.TextBox();
      this.rbEditable = new System.Windows.Forms.RadioButton();
      this.rbNoEditable = new System.Windows.Forms.RadioButton();
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
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cATEGORIASBindingSource)).BeginInit();
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
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cATIDDataGridViewTextBoxColumn,
            this.cATDESCRIPCIONDataGridViewTextBoxColumn,
            this.cATEDITABLEDataGridViewTextBoxColumn,
            this.cATUSUARIODataGridViewTextBoxColumn,
            this.cATFECHAMODDataGridViewTextBoxColumn});
      this.dataGridView1.DataSource = this.cATEGORIASBindingSource;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
      this.dataGridView1.Location = new System.Drawing.Point(14, 104);
      this.dataGridView1.MultiSelect = false;
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView1.Size = new System.Drawing.Size(471, 186);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
      // 
      // cATIDDataGridViewTextBoxColumn
      // 
      this.cATIDDataGridViewTextBoxColumn.DataPropertyName = "CAT_ID";
      this.cATIDDataGridViewTextBoxColumn.HeaderText = "CAT_ID";
      this.cATIDDataGridViewTextBoxColumn.Name = "cATIDDataGridViewTextBoxColumn";
      this.cATIDDataGridViewTextBoxColumn.ReadOnly = true;
      this.cATIDDataGridViewTextBoxColumn.Visible = false;
      // 
      // cATDESCRIPCIONDataGridViewTextBoxColumn
      // 
      this.cATDESCRIPCIONDataGridViewTextBoxColumn.DataPropertyName = "CAT_DESCRIPCION";
      this.cATDESCRIPCIONDataGridViewTextBoxColumn.HeaderText = "CAT_DESCRIPCION";
      this.cATDESCRIPCIONDataGridViewTextBoxColumn.Name = "cATDESCRIPCIONDataGridViewTextBoxColumn";
      this.cATDESCRIPCIONDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // cATEDITABLEDataGridViewTextBoxColumn
      // 
      this.cATEDITABLEDataGridViewTextBoxColumn.DataPropertyName = "CAT_EDITABLE";
      this.cATEDITABLEDataGridViewTextBoxColumn.HeaderText = "CAT_EDITABLE";
      this.cATEDITABLEDataGridViewTextBoxColumn.Name = "cATEDITABLEDataGridViewTextBoxColumn";
      this.cATEDITABLEDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // cATUSUARIODataGridViewTextBoxColumn
      // 
      this.cATUSUARIODataGridViewTextBoxColumn.DataPropertyName = "CAT_USUARIO";
      this.cATUSUARIODataGridViewTextBoxColumn.HeaderText = "CAT_USUARIO";
      this.cATUSUARIODataGridViewTextBoxColumn.Name = "cATUSUARIODataGridViewTextBoxColumn";
      this.cATUSUARIODataGridViewTextBoxColumn.ReadOnly = true;
      this.cATUSUARIODataGridViewTextBoxColumn.Visible = false;
      // 
      // cATFECHAMODDataGridViewTextBoxColumn
      // 
      this.cATFECHAMODDataGridViewTextBoxColumn.DataPropertyName = "CAT_FECHA_MOD";
      this.cATFECHAMODDataGridViewTextBoxColumn.HeaderText = "CAT_FECHA_MOD";
      this.cATFECHAMODDataGridViewTextBoxColumn.Name = "cATFECHAMODDataGridViewTextBoxColumn";
      this.cATFECHAMODDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // cATEGORIASBindingSource
      // 
      this.cATEGORIASBindingSource.DataMember = "CATEGORIAS";
      this.cATEGORIASBindingSource.DataSource = this.cRUDDataSet;
      // 
      // cRUDDataSet
      // 
      this.cRUDDataSet.DataSetName = "CRUDDataSet";
      this.cRUDDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Enabled = false;
      this.label1.Location = new System.Drawing.Point(11, 32);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(40, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Código";
      // 
      // txtCodigo
      // 
      this.txtCodigo.Enabled = false;
      this.txtCodigo.Location = new System.Drawing.Point(58, 28);
      this.txtCodigo.Name = "txtCodigo";
      this.txtCodigo.Size = new System.Drawing.Size(77, 20);
      this.txtCodigo.TabIndex = 2;
      // 
      // cATEGORIASTableAdapter
      // 
      this.cATEGORIASTableAdapter.ClearBeforeFill = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Enabled = false;
      this.label2.Location = new System.Drawing.Point(143, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(63, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Descripción";
      // 
      // txtDescripcion
      // 
      this.txtDescripcion.Enabled = false;
      this.txtDescripcion.Location = new System.Drawing.Point(212, 28);
      this.txtDescripcion.Name = "txtDescripcion";
      this.txtDescripcion.Size = new System.Drawing.Size(273, 20);
      this.txtDescripcion.TabIndex = 4;
      // 
      // rbEditable
      // 
      this.rbEditable.AutoSize = true;
      this.rbEditable.Enabled = false;
      this.rbEditable.Location = new System.Drawing.Point(58, 54);
      this.rbEditable.Name = "rbEditable";
      this.rbEditable.Size = new System.Drawing.Size(63, 17);
      this.rbEditable.TabIndex = 5;
      this.rbEditable.TabStop = true;
      this.rbEditable.Text = "Editable";
      this.rbEditable.UseVisualStyleBackColor = true;
      // 
      // rbNoEditable
      // 
      this.rbNoEditable.AutoSize = true;
      this.rbNoEditable.Enabled = false;
      this.rbNoEditable.Location = new System.Drawing.Point(127, 54);
      this.rbNoEditable.Name = "rbNoEditable";
      this.rbNoEditable.Size = new System.Drawing.Size(79, 17);
      this.rbNoEditable.TabIndex = 6;
      this.rbNoEditable.TabStop = true;
      this.rbNoEditable.Text = "No editable";
      this.rbNoEditable.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Enabled = false;
      this.label3.Location = new System.Drawing.Point(20, 82);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(43, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Usuario";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Enabled = false;
      this.label4.Location = new System.Drawing.Point(209, 81);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(98, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Ultima modificación";
      // 
      // txtUsuario
      // 
      this.txtUsuario.Enabled = false;
      this.txtUsuario.Location = new System.Drawing.Point(63, 78);
      this.txtUsuario.Name = "txtUsuario";
      this.txtUsuario.Size = new System.Drawing.Size(121, 20);
      this.txtUsuario.TabIndex = 9;
      // 
      // txtUltimaModificacion
      // 
      this.txtUltimaModificacion.Enabled = false;
      this.txtUltimaModificacion.Location = new System.Drawing.Point(302, 78);
      this.txtUltimaModificacion.Name = "txtUltimaModificacion";
      this.txtUltimaModificacion.Size = new System.Drawing.Size(181, 20);
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
      this.toolStrip1.Size = new System.Drawing.Size(495, 25);
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
      // dlgCategorias
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(495, 302);
      this.ControlBox = false;
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.txtUltimaModificacion);
      this.Controls.Add(this.txtUsuario);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.rbNoEditable);
      this.Controls.Add(this.rbEditable);
      this.Controls.Add(this.txtDescripcion);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtCodigo);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dataGridView1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "dlgCategorias";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Categorias";
      this.Load += new System.EventHandler(this.dlgCategorias_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cATEGORIASBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cRUDDataSet)).EndInit();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtCodigo;
    private IntelliTrack.Client.Application.CRUD.CRUDDataSet cRUDDataSet;
    private System.Windows.Forms.BindingSource cATEGORIASBindingSource;
    private IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.CATEGORIASTableAdapter cATEGORIASTableAdapter;
    private System.Windows.Forms.DataGridViewTextBoxColumn cATIDDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn cATDESCRIPCIONDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn cATEDITABLEDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn cATUSUARIODataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn cATFECHAMODDataGridViewTextBoxColumn;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtDescripcion;
    private System.Windows.Forms.RadioButton rbEditable;
    private System.Windows.Forms.RadioButton rbNoEditable;
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

  }
}