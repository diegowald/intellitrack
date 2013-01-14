namespace IntelliTrack.Client.Application
{
  partial class MapForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.ZoomWindow = new System.Windows.Forms.ToolStripButton();
      this.ZoomIn = new System.Windows.Forms.ToolStripButton();
      this.ZoomOut = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.Pan = new System.Windows.Forms.ToolStripButton();
      this.btnLaunchInBrowser = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.Info = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.RedrawProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.lblInformation = new System.Windows.Forms.Label();
      this.htmlLabel1 = new System.Windows.Forms.Label();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomWindow,
            this.ZoomIn,
            this.ZoomOut,
            this.toolStripSeparator1,
            this.Pan,
            this.btnLaunchInBrowser,
            this.toolStripSeparator3,
            this.Info,
            this.toolStripSeparator2,
            this.RedrawProgressBar});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(292, 25);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // ZoomWindow
      // 
      this.ZoomWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ZoomWindow.Image = ((System.Drawing.Image)(resources.GetObject("ZoomWindow.Image")));
      this.ZoomWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ZoomWindow.Name = "ZoomWindow";
      this.ZoomWindow.Size = new System.Drawing.Size(23, 22);
      this.ZoomWindow.Text = "toolStripButton1";
      this.ZoomWindow.ToolTipText = "Zoom Window";
      this.ZoomWindow.Click += new System.EventHandler(this.ZoomWindow_Click);
      // 
      // ZoomIn
      // 
      this.ZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ZoomIn.Image")));
      this.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ZoomIn.Name = "ZoomIn";
      this.ZoomIn.Size = new System.Drawing.Size(23, 22);
      this.ZoomIn.Text = "toolStripButton2";
      this.ZoomIn.ToolTipText = "Zoom In";
      this.ZoomIn.Click += new System.EventHandler(this.ZoomIn_Click);
      // 
      // ZoomOut
      // 
      this.ZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOut.Image")));
      this.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ZoomOut.Name = "ZoomOut";
      this.ZoomOut.Size = new System.Drawing.Size(23, 22);
      this.ZoomOut.Text = "toolStripButton3";
      this.ZoomOut.ToolTipText = "Zoom out";
      this.ZoomOut.Click += new System.EventHandler(this.ZoomOut_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // Pan
      // 
      this.Pan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.Pan.Image = ((System.Drawing.Image)(resources.GetObject("Pan.Image")));
      this.Pan.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.Pan.Name = "Pan";
      this.Pan.Size = new System.Drawing.Size(23, 22);
      this.Pan.Text = "toolStripButton1";
      this.Pan.ToolTipText = "Pan";
      this.Pan.Click += new System.EventHandler(this.Pan_Click);
      // 
      // btnLaunchInBrowser
      // 
      this.btnLaunchInBrowser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnLaunchInBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btnLaunchInBrowser.Image")));
      this.btnLaunchInBrowser.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnLaunchInBrowser.Name = "btnLaunchInBrowser";
      this.btnLaunchInBrowser.Size = new System.Drawing.Size(23, 22);
      this.btnLaunchInBrowser.Text = "toolStripButton1";
      this.btnLaunchInBrowser.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      this.toolStripSeparator3.Visible = false;
      // 
      // Info
      // 
      this.Info.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.Info.Image = ((System.Drawing.Image)(resources.GetObject("Info.Image")));
      this.Info.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.Info.Name = "Info";
      this.Info.Size = new System.Drawing.Size(23, 22);
      this.Info.Text = "toolStripButton1";
      this.Info.Visible = false;
      this.Info.Click += new System.EventHandler(this.Info_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // RedrawProgressBar
      // 
      this.RedrawProgressBar.Name = "RedrawProgressBar";
      this.RedrawProgressBar.Size = new System.Drawing.Size(100, 22);
      this.RedrawProgressBar.Visible = false;
      // 
      // lblInformation
      // 
      this.lblInformation.AutoSize = true;
      this.lblInformation.Location = new System.Drawing.Point(74, 126);
      this.lblInformation.Name = "lblInformation";
      this.lblInformation.Size = new System.Drawing.Size(69, 13);
      this.lblInformation.TabIndex = 1;
      this.lblInformation.Text = "lblInformation";
      // 
      // htmlLabel1
      // 
      this.htmlLabel1.AutoSize = true;
      this.htmlLabel1.BackColor = System.Drawing.SystemColors.Info;
      this.htmlLabel1.ForeColor = System.Drawing.SystemColors.InfoText;
      this.htmlLabel1.Location = new System.Drawing.Point(24, 42);
      this.htmlLabel1.Name = "htmlLabel1";
      this.htmlLabel1.Size = new System.Drawing.Size(0, 13);
      this.htmlLabel1.TabIndex = 3;
      this.htmlLabel1.Visible = false;
      // 
      // MapForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.ClientSize = new System.Drawing.Size(292, 266);
      this.Controls.Add(this.htmlLabel1);
      this.Controls.Add(this.lblInformation);
      this.Controls.Add(this.toolStrip1);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "MapForm";
      this.Text = "Mapa";
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapForm_MouseUp);
      this.SizeChanged += new System.EventHandler(this.MapForm_SizeChanged);
      this.MouseEnter += new System.EventHandler(this.MapForm_MouseEnter);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapForm_MouseDown);
      this.MouseLeave += new System.EventHandler(this.MapForm_MouseLeave);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapForm_FormClosing);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapForm_MouseMove);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }




    void MapForm_MouseLeave(object sender, System.EventArgs e)
    {
      htmlLabel1.Visible = false;
      if (_CommandAction == CommandActions.ELEMENT_INFORMATION)
        _CommandAction = CommandActions.NO_ACTION;
    }

    void MapForm_MouseEnter(object sender, System.EventArgs e)
    {
      htmlLabel1.Visible = true;
      htmlLabel1.Text = "";
      if (_CommandAction == CommandActions.NO_ACTION)
        _CommandAction = CommandActions.ELEMENT_INFORMATION;
    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton ZoomWindow;
    private System.Windows.Forms.ToolStripButton ZoomIn;
    private System.Windows.Forms.ToolStripButton ZoomOut;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton Pan;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripProgressBar RedrawProgressBar;
    private System.Windows.Forms.Label lblInformation;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton Info;
    //private System.Windows.Forms.HtmlLabel htmlLabel1;
    private System.Windows.Forms.Label htmlLabel1;
    private System.Windows.Forms.ToolStripButton btnLaunchInBrowser;
    
  }
}