namespace IntelliTrack.Client.Application
{
  partial class ElementInformation
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
      tabControl1 = new System.Windows.Forms.TabControl();
      SuspendLayout();
      tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      tabControl1.Location = new System.Drawing.Point(0, 0);
      tabControl1.Name = "tabControl1";
      tabControl1.SelectedIndex = 0;
      tabControl1.Size = new System.Drawing.Size(341, 225);
      tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
      tabControl1.TabIndex = 1;
      AutoScaleDimensions = new System.Drawing.SizeF(7.0F, 11.0F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(341, 225);
      Controls.Add(tabControl1);
      Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      Name = "ElementInformation";
      TabText = "ElementInformation";
      Text = "ElementInformation";
      ResumeLayout(false);
    }

 

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
   
  }
}