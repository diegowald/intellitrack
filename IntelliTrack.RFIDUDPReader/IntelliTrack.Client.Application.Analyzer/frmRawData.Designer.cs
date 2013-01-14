namespace IntelliTrack.Client.Application
{
  partial class frmRawData
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
      this.dgRawData = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.dgRawData)).BeginInit();
      this.SuspendLayout();
      // 
      // dgRawData
      // 
      this.dgRawData.AllowUserToAddRows = false;
      this.dgRawData.AllowUserToDeleteRows = false;
      this.dgRawData.AllowUserToResizeRows = false;
      this.dgRawData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgRawData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgRawData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.dgRawData.Location = new System.Drawing.Point(0, 0);
      this.dgRawData.MultiSelect = false;
      this.dgRawData.Name = "dgRawData";
      this.dgRawData.ReadOnly = true;
      this.dgRawData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgRawData.Size = new System.Drawing.Size(292, 266);
      this.dgRawData.TabIndex = 0;
      // 
      // frmRawData
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 266);
      this.Controls.Add(this.dgRawData);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "frmRawData";
      this.Text = "frmRawData";
      ((System.ComponentModel.ISupportInitialize)(this.dgRawData)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dgRawData;
  }
}