namespace IntelliTrack.Client.Application.CRUD
{
  partial class dlgABMBase
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgABMBase));
      this.tbControl = new System.Windows.Forms.ToolStrip();
      this.tbSalir = new System.Windows.Forms.ToolStripButton();
      this.tbGuardar = new System.Windows.Forms.ToolStripButton();
      this.tbCancelar = new System.Windows.Forms.ToolStripButton();
      this.tbEliminar = new System.Windows.Forms.ToolStripButton();
      this.tbControl.SuspendLayout();
      this.SuspendLayout();
      // 
      // tbControl
      // 
      this.tbControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbSalir,
            this.tbGuardar,
            this.tbCancelar,
            this.tbEliminar});
      this.tbControl.Location = new System.Drawing.Point(0, 0);
      this.tbControl.Name = "tbControl";
      this.tbControl.Size = new System.Drawing.Size(292, 25);
      this.tbControl.TabIndex = 0;
      this.tbControl.Text = "Comandos";
      // 
      // tbSalir
      // 
      this.tbSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbSalir.Image = ((System.Drawing.Image)(resources.GetObject("tbSalir.Image")));
      this.tbSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbSalir.Name = "tbSalir";
      this.tbSalir.Size = new System.Drawing.Size(23, 22);
      this.tbSalir.Text = "Salir";
      this.tbSalir.Click += new System.EventHandler(this.tbSalir_Click);
      // 
      // tbGuardar
      // 
      this.tbGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbGuardar.Image = ((System.Drawing.Image)(resources.GetObject("tbGuardar.Image")));
      this.tbGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbGuardar.Name = "tbGuardar";
      this.tbGuardar.Size = new System.Drawing.Size(23, 22);
      this.tbGuardar.Text = "Guardar cambios";
      this.tbGuardar.Click += new System.EventHandler(this.tbGuardar_Click);
      // 
      // tbCancelar
      // 
      this.tbCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbCancelar.Image = ((System.Drawing.Image)(resources.GetObject("tbCancelar.Image")));
      this.tbCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbCancelar.Name = "tbCancelar";
      this.tbCancelar.Size = new System.Drawing.Size(23, 22);
      this.tbCancelar.Text = "Cancelar";
      this.tbCancelar.Click += new System.EventHandler(this.tbCancelar_Click);
      // 
      // tbEliminar
      // 
      this.tbEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbEliminar.Image = ((System.Drawing.Image)(resources.GetObject("tbEliminar.Image")));
      this.tbEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbEliminar.Name = "tbEliminar";
      this.tbEliminar.Size = new System.Drawing.Size(23, 22);
      this.tbEliminar.Text = "Eliminar";
      this.tbEliminar.Click += new System.EventHandler(this.tbEliminar_Click);
      // 
      // dlgABMBase
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 266);
      this.ControlBox = false;
      this.Controls.Add(this.tbControl);
      this.Name = "dlgABMBase";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "dlgABMBase";
      this.Load += new System.EventHandler(this.dlgABMBase_Load);
      this.tbControl.ResumeLayout(false);
      this.tbControl.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip tbControl;
    private System.Windows.Forms.ToolStripButton tbSalir;
    private System.Windows.Forms.ToolStripButton tbGuardar;
    private System.Windows.Forms.ToolStripButton tbCancelar;
    private System.Windows.Forms.ToolStripButton tbEliminar;
  }
}