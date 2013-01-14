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
      System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(IntelliTrack.Client.Application.MapForm));
      toolStrip1 = new System.Windows.Forms.ToolStrip();
      ZoomWindow = new System.Windows.Forms.ToolStripButton();
      ZoomIn = new System.Windows.Forms.ToolStripButton();
      ZoomOut = new System.Windows.Forms.ToolStripButton();
      toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      Pan = new System.Windows.Forms.ToolStripButton();
      toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      btnLaunchInBrowser = new System.Windows.Forms.ToolStripButton();
      Info = new System.Windows.Forms.ToolStripButton();
      toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      RedrawProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      btnInicio = new System.Windows.Forms.ToolStripButton();
      btnEjecutar = new System.Windows.Forms.ToolStripButton();
      btnPausa = new System.Windows.Forms.ToolStripButton();
      btnDetener = new System.Windows.Forms.ToolStripButton();
      btnConfiguracionConsultaHistorica = new System.Windows.Forms.ToolStripButton();
      lblInformation = new System.Windows.Forms.Label();
      panel1 = new System.Windows.Forms.Panel();
      udRegistrosPorSegundo = new System.Windows.Forms.NumericUpDown();
      lblPosicion = new System.Windows.Forms.Label();
      trackPosicion = new System.Windows.Forms.TrackBar();
      lblEstado = new System.Windows.Forms.Label();
      toolStrip1.SuspendLayout();
      panel1.SuspendLayout();
      udRegistrosPorSegundo.BeginInit();
      trackPosicion.BeginInit();
      SuspendLayout();
      System.Windows.Forms.ToolStripItem[] toolStripItemArr = new System.Windows.Forms.ToolStripItem[] {
                                                                                                               ZoomWindow, 
                                                                                                               ZoomIn, 
                                                                                                               ZoomOut, 
                                                                                                               toolStripSeparator1, 
                                                                                                               Pan, 
                                                                                                               toolStripSeparator3, 
                                                                                                               btnLaunchInBrowser, 
                                                                                                               Info, 
                                                                                                               toolStripSeparator2, 
                                                                                                               RedrawProgressBar, 
                                                                                                               toolStripSeparator4, 
                                                                                                               btnInicio, 
                                                                                                               btnEjecutar, 
                                                                                                               btnPausa, 
                                                                                                               btnDetener, 
                                                                                                               btnConfiguracionConsultaHistorica };
      toolStrip1.Items.AddRange(toolStripItemArr);
      toolStrip1.Location = new System.Drawing.Point(0, 0);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Size = new System.Drawing.Size(542, 25);
      toolStrip1.TabIndex = 0;
      toolStrip1.Text = "toolStrip1";
      ZoomWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      ZoomWindow.Image = (System.Drawing.Image)componentResourceManager.GetObject("ZoomWindow.Image");
      ZoomWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
      ZoomWindow.Name = "ZoomWindow";
      ZoomWindow.Size = new System.Drawing.Size(23, 22);
      ZoomWindow.Text = "toolStripButton1";
      ZoomWindow.ToolTipText = "Zoom Window";
      ZoomWindow.Click += new System.EventHandler(ZoomWindow_Click);
      ZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      ZoomIn.Image = (System.Drawing.Image)componentResourceManager.GetObject("ZoomIn.Image");
      ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
      ZoomIn.Name = "ZoomIn";
      ZoomIn.Size = new System.Drawing.Size(23, 22);
      ZoomIn.Text = "toolStripButton2";
      ZoomIn.ToolTipText = "Zoom In";
      ZoomIn.Click += new System.EventHandler(ZoomIn_Click);
      ZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      ZoomOut.Image = (System.Drawing.Image)componentResourceManager.GetObject("ZoomOut.Image");
      ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
      ZoomOut.Name = "ZoomOut";
      ZoomOut.Size = new System.Drawing.Size(23, 22);
      ZoomOut.Text = "toolStripButton3";
      ZoomOut.ToolTipText = "Zoom out";
      ZoomOut.Click += new System.EventHandler(ZoomOut_Click);
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      Pan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      Pan.Image = (System.Drawing.Image)componentResourceManager.GetObject("Pan.Image");
      Pan.ImageTransparentColor = System.Drawing.Color.Magenta;
      Pan.Name = "Pan";
      Pan.Size = new System.Drawing.Size(23, 22);
      Pan.Text = "toolStripButton1";
      Pan.ToolTipText = "Pan";
      Pan.Click += new System.EventHandler(Pan_Click);
      toolStripSeparator3.Name = "toolStripSeparator3";
      toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      toolStripSeparator3.Visible = false;
      btnLaunchInBrowser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      btnLaunchInBrowser.Image = (System.Drawing.Image)componentResourceManager.GetObject("btnLaunchInBrowser.Image");
      btnLaunchInBrowser.ImageTransparentColor = System.Drawing.Color.Magenta;
      btnLaunchInBrowser.Name = "btnLaunchInBrowser";
      btnLaunchInBrowser.Size = new System.Drawing.Size(23, 22);
      btnLaunchInBrowser.Text = "btnLaunchInBrowser";
      btnLaunchInBrowser.Click += new System.EventHandler(toolStripButton1_Click);
      Info.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      Info.Image = (System.Drawing.Image)componentResourceManager.GetObject("Info.Image");
      Info.ImageTransparentColor = System.Drawing.Color.Magenta;
      Info.Name = "Info";
      Info.Size = new System.Drawing.Size(23, 22);
      Info.Text = "toolStripButton1";
      Info.Click += new System.EventHandler(Info_Click);
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      RedrawProgressBar.Name = "RedrawProgressBar";
      RedrawProgressBar.Size = new System.Drawing.Size(100, 22);
      RedrawProgressBar.Visible = false;
      toolStripSeparator4.Name = "toolStripSeparator4";
      toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      btnInicio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      btnInicio.Image = (System.Drawing.Image)componentResourceManager.GetObject("btnInicio.Image");
      btnInicio.ImageTransparentColor = System.Drawing.Color.Magenta;
      btnInicio.Name = "btnInicio";
      btnInicio.Size = new System.Drawing.Size(23, 22);
      btnInicio.Text = "Inicio";
      btnInicio.Click += new System.EventHandler(btnInicio_Click);
      btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      btnEjecutar.Image = (System.Drawing.Image)componentResourceManager.GetObject("btnEjecutar.Image");
      btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
      btnEjecutar.Name = "btnEjecutar";
      btnEjecutar.Size = new System.Drawing.Size(23, 22);
      btnEjecutar.Text = "Ejecutar";
      btnEjecutar.Click += new System.EventHandler(btnEjecutar_Click);
      btnPausa.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      btnPausa.Image = (System.Drawing.Image)componentResourceManager.GetObject("btnPausa.Image");
      btnPausa.ImageTransparentColor = System.Drawing.Color.Magenta;
      btnPausa.Name = "btnPausa";
      btnPausa.Size = new System.Drawing.Size(23, 22);
      btnPausa.Text = "Pausa";
      btnPausa.Click += new System.EventHandler(btnPausa_Click);
      btnDetener.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      btnDetener.Image = (System.Drawing.Image)componentResourceManager.GetObject("btnDetener.Image");
      btnDetener.ImageTransparentColor = System.Drawing.Color.Magenta;
      btnDetener.Name = "btnDetener";
      btnDetener.Size = new System.Drawing.Size(23, 22);
      btnDetener.Text = "Detener";
      btnDetener.Click += new System.EventHandler(btnDetener_Click);
      btnConfiguracionConsultaHistorica.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      btnConfiguracionConsultaHistorica.Image = (System.Drawing.Image)componentResourceManager.GetObject("btnConfiguracionConsultaHistorica.Image");
      btnConfiguracionConsultaHistorica.ImageTransparentColor = System.Drawing.Color.Magenta;
      btnConfiguracionConsultaHistorica.Name = "btnConfiguracionConsultaHistorica";
      btnConfiguracionConsultaHistorica.Size = new System.Drawing.Size(23, 22);
      btnConfiguracionConsultaHistorica.Text = "Configuraci\u00F3n";
      btnConfiguracionConsultaHistorica.Click += new System.EventHandler(btnConfiguracionConsultaHistorica_Click);
      lblInformation.AutoSize = true;
      lblInformation.Location = new System.Drawing.Point(12, 45);
      lblInformation.Name = "lblInformation";
      lblInformation.Size = new System.Drawing.Size(69, 13);
      lblInformation.TabIndex = 1;
      lblInformation.Text = "lblInformation";
      panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
      panel1.Controls.Add(udRegistrosPorSegundo);
      panel1.Controls.Add(lblPosicion);
      panel1.Controls.Add(trackPosicion);
      panel1.Controls.Add(lblEstado);
      panel1.Location = new System.Drawing.Point(0, 189);
      panel1.Name = "panel1";
      panel1.Size = new System.Drawing.Size(142, 77);
      panel1.TabIndex = 2;
      udRegistrosPorSegundo.Dock = System.Windows.Forms.DockStyle.Top;
      udRegistrosPorSegundo.Location = new System.Drawing.Point(0, 54);
      int[] iArr1 = new int[4];
      iArr1[0] = 50;
      udRegistrosPorSegundo.Maximum = new System.Decimal(iArr1);
      int[] iArr2 = new int[4];
      iArr2[0] = 1;
      udRegistrosPorSegundo.Minimum = new System.Decimal(iArr2);
      udRegistrosPorSegundo.Name = "udRegistrosPorSegundo";
      udRegistrosPorSegundo.Size = new System.Drawing.Size(142, 20);
      udRegistrosPorSegundo.TabIndex = 3;
      int[] iArr3 = new int[4];
      iArr3[0] = 1;
      udRegistrosPorSegundo.Value = new System.Decimal(iArr3);
      udRegistrosPorSegundo.ValueChanged += new System.EventHandler(udRegistrosPorSegundo_ValueChanged);
      lblPosicion.Dock = System.Windows.Forms.DockStyle.Top;
      lblPosicion.Location = new System.Drawing.Point(0, 41);
      lblPosicion.Name = "lblPosicion";
      lblPosicion.Size = new System.Drawing.Size(142, 13);
      lblPosicion.TabIndex = 2;
      trackPosicion.AutoSize = false;
      trackPosicion.Dock = System.Windows.Forms.DockStyle.Top;
      trackPosicion.Location = new System.Drawing.Point(0, 13);
      trackPosicion.Maximum = 1000000;
      trackPosicion.Name = "trackPosicion";
      trackPosicion.Size = new System.Drawing.Size(142, 28);
      trackPosicion.TabIndex = 1;
      trackPosicion.TickFrequency = 0;
      trackPosicion.TickStyle = System.Windows.Forms.TickStyle.None;
      trackPosicion.Scroll += new System.EventHandler(trackPosicion_Scroll);
      lblEstado.Dock = System.Windows.Forms.DockStyle.Top;
      lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
      lblEstado.ForeColor = System.Drawing.Color.Green;
      lblEstado.Location = new System.Drawing.Point(0, 0);
      lblEstado.Name = "lblEstado";
      lblEstado.Size = new System.Drawing.Size(142, 13);
      lblEstado.TabIndex = 0;
      lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      AutoSize = true;
      BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      ClientSize = new System.Drawing.Size(542, 266);
      Controls.Add(panel1);
      Controls.Add(lblInformation);
      Controls.Add(toolStrip1);
      DoubleBuffered = true;
      Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      Name = "MapForm";
      Text = "Mapa";
      MouseUp += new System.Windows.Forms.MouseEventHandler(MapForm_MouseUp);
      Scroll += new System.Windows.Forms.ScrollEventHandler(MapForm_Scroll);
      SizeChanged += new System.EventHandler(MapForm_SizeChanged);
      MouseDown += new System.Windows.Forms.MouseEventHandler(MapForm_MouseDown);
      FormClosing += new System.Windows.Forms.FormClosingEventHandler(MapForm_FormClosing);
      MouseMove += new System.Windows.Forms.MouseEventHandler(MapForm_MouseMove);
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      panel1.ResumeLayout(false);
      udRegistrosPorSegundo.EndInit();
      trackPosicion.EndInit();
      ResumeLayout(false);
      PerformLayout();
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
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton btnInicio;
    private System.Windows.Forms.ToolStripButton btnEjecutar;
    private System.Windows.Forms.ToolStripButton btnPausa;
    private System.Windows.Forms.ToolStripButton btnDetener;
    private System.Windows.Forms.ToolStripButton btnConfiguracionConsultaHistorica;
    private System.Windows.Forms.ToolStripButton btnLaunchInBrowser;
    private System.Windows.Forms.Panel panel1; 
    private bool trackFromAnimation;
    private System.Windows.Forms.TrackBar trackPosicion;
    private System.Windows.Forms.NumericUpDown udRegistrosPorSegundo;
    private System.Windows.Forms.Label lblEstado;
    private System.Windows.Forms.Label lblPosicion;

  }
}