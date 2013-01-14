namespace IntelliTrack.Client.Application
{
  partial class Eventos
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
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.txtTerminal = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridView1
      // 
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.Location = new System.Drawing.Point(0, 0);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new System.Drawing.Size(292, 266);
      this.dataGridView1.TabIndex = 0;
      // 
      // txtTerminal
      // 
      this.txtTerminal.BackColor = System.Drawing.Color.Black;
      this.txtTerminal.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtTerminal.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtTerminal.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtTerminal.ForeColor = System.Drawing.Color.Lime;
      this.txtTerminal.Location = new System.Drawing.Point(0, 0);
      this.txtTerminal.Multiline = true;
      this.txtTerminal.Name = "txtTerminal";
      this.txtTerminal.ReadOnly = true;
      this.txtTerminal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtTerminal.Size = new System.Drawing.Size(292, 266);
      this.txtTerminal.TabIndex = 1;
      // 
      // Eventos
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 266);
      this.Controls.Add(this.txtTerminal);
      this.Controls.Add(this.dataGridView1);
      //this.Name = "Eventos";
      //this.TabText = "Eventos";
      //this.Text = "Eventos";
      this.Name = "Terminal";
      this.TabText = "Terminal";
      this.Text = "Terminal";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.TextBox txtTerminal;
  }
}