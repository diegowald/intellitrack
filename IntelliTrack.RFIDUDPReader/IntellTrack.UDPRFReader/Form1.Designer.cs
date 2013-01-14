namespace IntellTrack.UDPRFReader
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
      this.lblDataArrived = new System.Windows.Forms.Label();
      this.lblStatus = new System.Windows.Forms.Label();
      this.lbData = new System.Windows.Forms.ListBox();
      this.SuspendLayout();
      // 
      // lblDataArrived
      // 
      this.lblDataArrived.AutoSize = true;
      this.lblDataArrived.Location = new System.Drawing.Point(12, 9);
      this.lblDataArrived.Name = "lblDataArrived";
      this.lblDataArrived.Size = new System.Drawing.Size(66, 13);
      this.lblDataArrived.TabIndex = 0;
      this.lblDataArrived.Text = "Data Arrived";
      // 
      // lblStatus
      // 
      this.lblStatus.AutoSize = true;
      this.lblStatus.Location = new System.Drawing.Point(97, 9);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(22, 13);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "OK";
      // 
      // lbData
      // 
      this.lbData.FormattingEnabled = true;
      this.lbData.Location = new System.Drawing.Point(12, 32);
      this.lbData.Name = "lbData";
      this.lbData.Size = new System.Drawing.Size(367, 199);
      this.lbData.TabIndex = 2;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(391, 246);
      this.Controls.Add(this.lbData);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.lblDataArrived);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblDataArrived;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.ListBox lbData;
  }
}

