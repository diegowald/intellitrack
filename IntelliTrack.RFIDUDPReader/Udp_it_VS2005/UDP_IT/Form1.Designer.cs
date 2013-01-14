namespace UDP_IT
{
  partial class Form1
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.txtTerminal = new System.Windows.Forms.TextBox();
      this.btnSalir = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // txtTerminal
      // 
      this.txtTerminal.BackColor = System.Drawing.Color.Black;
      this.txtTerminal.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtTerminal.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtTerminal.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtTerminal.ForeColor = System.Drawing.Color.Lime;
      this.txtTerminal.Location = new System.Drawing.Point(0, 0);
      this.txtTerminal.Multiline = true;
      this.txtTerminal.Name = "txtTerminal";
      this.txtTerminal.ReadOnly = true;
      this.txtTerminal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtTerminal.Size = new System.Drawing.Size(583, 251);
      this.txtTerminal.TabIndex = 0;
      // 
      // btnSalir
      // 
      this.btnSalir.Location = new System.Drawing.Point(495, 257);
      this.btnSalir.Name = "btnSalir";
      this.btnSalir.Size = new System.Drawing.Size(74, 23);
      this.btnSalir.TabIndex = 1;
      this.btnSalir.Text = "Salir";
      this.btnSalir.UseVisualStyleBackColor = true;
      this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(581, 287);
      this.Controls.Add(this.btnSalir);
      this.Controls.Add(this.txtTerminal);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "UDP Test - IntelliTrack";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtTerminal;
    private System.Windows.Forms.Button btnSalir;
  }
}

