namespace IntelliTrack.Client.Application.CRUD
{
  partial class dlgABMCategoria
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtCodigo = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.Descripción = new System.Windows.Forms.Label();
      this.rbNo = new System.Windows.Forms.RadioButton();
      this.rbSi = new System.Windows.Forms.RadioButton();
      this.txtDescripcion = new System.Windows.Forms.TextBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
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
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.Descripción);
      this.groupBox2.Controls.Add(this.rbNo);
      this.groupBox2.Controls.Add(this.rbSi);
      this.groupBox2.Controls.Add(this.txtDescripcion);
      this.groupBox2.Location = new System.Drawing.Point(2, 93);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(288, 92);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Generales";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(9, 64);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(45, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Editable";
      // 
      // Descripción
      // 
      this.Descripción.AutoSize = true;
      this.Descripción.Location = new System.Drawing.Point(10, 27);
      this.Descripción.Name = "Descripción";
      this.Descripción.Size = new System.Drawing.Size(63, 13);
      this.Descripción.TabIndex = 3;
      this.Descripción.Text = "Descripción";
      // 
      // rbNo
      // 
      this.rbNo.AutoSize = true;
      this.rbNo.Location = new System.Drawing.Point(133, 62);
      this.rbNo.Name = "rbNo";
      this.rbNo.Size = new System.Drawing.Size(39, 17);
      this.rbNo.TabIndex = 4;
      this.rbNo.TabStop = true;
      this.rbNo.Text = "No";
      this.rbNo.UseVisualStyleBackColor = true;
      // 
      // rbSi
      // 
      this.rbSi.AutoSize = true;
      this.rbSi.Location = new System.Drawing.Point(91, 62);
      this.rbSi.Name = "rbSi";
      this.rbSi.Size = new System.Drawing.Size(36, 17);
      this.rbSi.TabIndex = 3;
      this.rbSi.TabStop = true;
      this.rbSi.Text = "Sí";
      this.rbSi.UseVisualStyleBackColor = true;
      // 
      // txtDescripcion
      // 
      this.txtDescripcion.Location = new System.Drawing.Point(73, 19);
      this.txtDescripcion.Name = "txtDescripcion";
      this.txtDescripcion.Size = new System.Drawing.Size(188, 20);
      this.txtDescripcion.TabIndex = 2;
      // 
      // dlgABMCategoria
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 188);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Name = "dlgABMCategoria";
      this.Text = "dlgABMCategoria";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox txtCodigo;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label Descripción;
    private System.Windows.Forms.RadioButton rbNo;
    private System.Windows.Forms.RadioButton rbSi;
    private System.Windows.Forms.TextBox txtDescripcion;


  }
}