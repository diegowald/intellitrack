namespace IntelliTrack.Client.Application.CRUD
{
  partial class dlgABMTransponder
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtCodigo = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.cboCategorias = new System.Windows.Forms.ComboBox();
      this.cATEGORIASBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.cRUDDataSet = new IntelliTrack.Client.Application.CRUD.CRUDDataSet();
      this.rbNo = new System.Windows.Forms.RadioButton();
      this.rbSi = new System.Windows.Forms.RadioButton();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtTransponder = new System.Windows.Forms.TextBox();
      this.txtVehiculo = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.Descripción = new System.Windows.Forms.Label();
      this.cATEGORIASTableAdapter = new IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.CATEGORIASTableAdapter();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.cATEGORIASBindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cRUDDataSet)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtCodigo);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Location = new System.Drawing.Point(2, 33);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(288, 54);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Claves";
      // 
      // txtCodigo
      // 
      this.txtCodigo.Location = new System.Drawing.Point(68, 19);
      this.txtCodigo.Name = "txtCodigo";
      this.txtCodigo.Size = new System.Drawing.Size(194, 20);
      this.txtCodigo.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(16, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(40, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Código";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.cboCategorias);
      this.groupBox2.Controls.Add(this.rbNo);
      this.groupBox2.Controls.Add(this.rbSi);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.txtTransponder);
      this.groupBox2.Controls.Add(this.txtVehiculo);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.Descripción);
      this.groupBox2.Location = new System.Drawing.Point(2, 93);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(288, 131);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Generales";
      // 
      // cboCategorias
      // 
      this.cboCategorias.DataSource = this.cATEGORIASBindingSource;
      this.cboCategorias.DisplayMember = "CAT_DESCRIPCION";
      this.cboCategorias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboCategorias.FormattingEnabled = true;
      this.cboCategorias.Location = new System.Drawing.Point(74, 42);
      this.cboCategorias.Name = "cboCategorias";
      this.cboCategorias.Size = new System.Drawing.Size(188, 21);
      this.cboCategorias.TabIndex = 3;
      this.cboCategorias.ValueMember = "CAT_ID";
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
      // rbNo
      // 
      this.rbNo.AutoSize = true;
      this.rbNo.Location = new System.Drawing.Point(116, 101);
      this.rbNo.Name = "rbNo";
      this.rbNo.Size = new System.Drawing.Size(39, 17);
      this.rbNo.TabIndex = 6;
      this.rbNo.TabStop = true;
      this.rbNo.Text = "No";
      this.rbNo.UseVisualStyleBackColor = true;
      // 
      // rbSi
      // 
      this.rbSi.AutoSize = true;
      this.rbSi.Location = new System.Drawing.Point(74, 101);
      this.rbSi.Name = "rbSi";
      this.rbSi.Size = new System.Drawing.Size(36, 17);
      this.rbSi.TabIndex = 5;
      this.rbSi.TabStop = true;
      this.rbSi.Text = "Sí";
      this.rbSi.UseVisualStyleBackColor = true;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(10, 103);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(37, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Activo";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(7, 18);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(67, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Transponder";
      // 
      // txtTransponder
      // 
      this.txtTransponder.Location = new System.Drawing.Point(74, 15);
      this.txtTransponder.Name = "txtTransponder";
      this.txtTransponder.Size = new System.Drawing.Size(188, 20);
      this.txtTransponder.TabIndex = 2;
      // 
      // txtVehiculo
      // 
      this.txtVehiculo.Location = new System.Drawing.Point(74, 72);
      this.txtVehiculo.Name = "txtVehiculo";
      this.txtVehiculo.Size = new System.Drawing.Size(188, 20);
      this.txtVehiculo.TabIndex = 4;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 75);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(50, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Vehículo";
      // 
      // Descripción
      // 
      this.Descripción.AutoSize = true;
      this.Descripción.Location = new System.Drawing.Point(6, 46);
      this.Descripción.Name = "Descripción";
      this.Descripción.Size = new System.Drawing.Size(54, 13);
      this.Descripción.TabIndex = 3;
      this.Descripción.Text = "Categoría";
      // 
      // cATEGORIASTableAdapter
      // 
      this.cATEGORIASTableAdapter.ClearBeforeFill = true;
      // 
      // dlgABMTransponder
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 230);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Name = "dlgABMTransponder";
      this.Text = "dlgABMTransponder";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.cATEGORIASBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cRUDDataSet)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox txtCodigo;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label Descripción;
    private System.Windows.Forms.TextBox txtVehiculo;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtTransponder;
    private System.Windows.Forms.RadioButton rbNo;
    private System.Windows.Forms.RadioButton rbSi;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cboCategorias;
    private CRUDDataSet cRUDDataSet;
    private System.Windows.Forms.BindingSource cATEGORIASBindingSource;
    private IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.CATEGORIASTableAdapter cATEGORIASTableAdapter;


  }
}