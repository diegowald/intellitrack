namespace CreadorXYZ
{
  partial class Form1
  {
    /// <summary>
    /// Variable del diseñador requerida.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Limpiar los recursos que se estén utilizando.
    /// </summary>
    /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
    /// el contenido del método con el editor de código.
    /// </summary>
    private void InitializeComponent()
    {
      this.dtSinCodificar = new System.Windows.Forms.DateTimePicker();
      this.Codificar = new System.Windows.Forms.Button();
      this.Decodificar = new System.Windows.Forms.Button();
      this.txtCodificado = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // dtSinCodificar
      // 
      this.dtSinCodificar.Location = new System.Drawing.Point(34, 23);
      this.dtSinCodificar.Name = "dtSinCodificar";
      this.dtSinCodificar.Size = new System.Drawing.Size(259, 20);
      this.dtSinCodificar.TabIndex = 0;
      // 
      // Codificar
      // 
      this.Codificar.Location = new System.Drawing.Point(33, 65);
      this.Codificar.Name = "Codificar";
      this.Codificar.Size = new System.Drawing.Size(116, 26);
      this.Codificar.TabIndex = 1;
      this.Codificar.Text = "Codificar";
      this.Codificar.UseVisualStyleBackColor = true;
      this.Codificar.Click += new System.EventHandler(this.Codificar_Click);
      // 
      // Decodificar
      // 
      this.Decodificar.Location = new System.Drawing.Point(168, 65);
      this.Decodificar.Name = "Decodificar";
      this.Decodificar.Size = new System.Drawing.Size(116, 26);
      this.Decodificar.TabIndex = 2;
      this.Decodificar.Text = "Decodificar";
      this.Decodificar.UseVisualStyleBackColor = true;
      this.Decodificar.Click += new System.EventHandler(this.Decodificar_Click);
      // 
      // txtCodificado
      // 
      this.txtCodificado.Location = new System.Drawing.Point(33, 110);
      this.txtCodificado.Name = "txtCodificado";
      this.txtCodificado.Size = new System.Drawing.Size(277, 20);
      this.txtCodificado.TabIndex = 3;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(403, 180);
      this.Controls.Add(this.txtCodificado);
      this.Controls.Add(this.Decodificar);
      this.Controls.Add(this.Codificar);
      this.Controls.Add(this.dtSinCodificar);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DateTimePicker dtSinCodificar;
    private System.Windows.Forms.Button Codificar;
    private System.Windows.Forms.Button Decodificar;
    private System.Windows.Forms.TextBox txtCodificado;
  }
}

