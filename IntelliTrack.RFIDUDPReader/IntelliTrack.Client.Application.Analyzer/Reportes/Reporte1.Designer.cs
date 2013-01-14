namespace IntelliTrack.Client.Application.Reportes
{
  /// <summary>
  /// Summary description for Reporte1.
  /// </summary>
  partial class Reporte1
  {
    private DataDynamics.ActiveReports.PageHeader pageHeader;
    private DataDynamics.ActiveReports.Detail detail;
    private DataDynamics.ActiveReports.PageFooter pageFooter;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
      }
      base.Dispose(disposing);
    }

    #region ActiveReport Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      DataDynamics.ActiveReports.DataSources.SqlDBDataSource sqlDBDataSource1 = new DataDynamics.ActiveReports.DataSources.SqlDBDataSource();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reporte1));
      this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
      this.ID = new DataDynamics.ActiveReports.Label();
      this.label1 = new DataDynamics.ActiveReports.Label();
      this.label2 = new DataDynamics.ActiveReports.Label();
      this.label3 = new DataDynamics.ActiveReports.Label();
      this.label4 = new DataDynamics.ActiveReports.Label();
      this.detail = new DataDynamics.ActiveReports.Detail();
      this.txtIDTransponder1 = new DataDynamics.ActiveReports.TextBox();
      this.txtDIA_HORA1 = new DataDynamics.ActiveReports.TextBox();
      this.txtVelocidad1 = new DataDynamics.ActiveReports.TextBox();
      this.txtCurso1 = new DataDynamics.ActiveReports.TextBox();
      this.txtSENTIDO1 = new DataDynamics.ActiveReports.TextBox();
      this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
      ((System.ComponentModel.ISupportInitialize)(this.ID)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtIDTransponder1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtDIA_HORA1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtVelocidad1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtCurso1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtSENTIDO1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // pageHeader
      // 
      this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.ID,
            this.label1,
            this.label2,
            this.label3,
            this.label4});
      this.pageHeader.Height = 0.25F;
      this.pageHeader.Name = "pageHeader";
      // 
      // ID
      // 
      this.ID.Border.BottomColor = System.Drawing.Color.Black;
      this.ID.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.ID.Border.LeftColor = System.Drawing.Color.Black;
      this.ID.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.ID.Border.RightColor = System.Drawing.Color.Black;
      this.ID.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.ID.Border.TopColor = System.Drawing.Color.Black;
      this.ID.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.ID.Height = 0.2395833F;
      this.ID.HyperLink = null;
      this.ID.Left = 0.07291666F;
      this.ID.Name = "ID";
      this.ID.Style = "";
      this.ID.Text = "ID";
      this.ID.Top = 0.02083333F;
      this.ID.Width = 0.8541667F;
      // 
      // label1
      // 
      this.label1.Border.BottomColor = System.Drawing.Color.Black;
      this.label1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label1.Border.LeftColor = System.Drawing.Color.Black;
      this.label1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label1.Border.RightColor = System.Drawing.Color.Black;
      this.label1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label1.Border.TopColor = System.Drawing.Color.Black;
      this.label1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label1.Height = 0.21875F;
      this.label1.HyperLink = null;
      this.label1.Left = 1.03125F;
      this.label1.Name = "label1";
      this.label1.Style = "";
      this.label1.Text = "Fecha";
      this.label1.Top = 0.02083333F;
      this.label1.Width = 2.0625F;
      // 
      // label2
      // 
      this.label2.Border.BottomColor = System.Drawing.Color.Black;
      this.label2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label2.Border.LeftColor = System.Drawing.Color.Black;
      this.label2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label2.Border.RightColor = System.Drawing.Color.Black;
      this.label2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label2.Border.TopColor = System.Drawing.Color.Black;
      this.label2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label2.Height = 0.1979167F;
      this.label2.HyperLink = null;
      this.label2.Left = 3.197917F;
      this.label2.Name = "label2";
      this.label2.Style = "";
      this.label2.Text = "Velocidad";
      this.label2.Top = 0.04166667F;
      this.label2.Width = 0.9270833F;
      // 
      // label3
      // 
      this.label3.Border.BottomColor = System.Drawing.Color.Black;
      this.label3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label3.Border.LeftColor = System.Drawing.Color.Black;
      this.label3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label3.Border.RightColor = System.Drawing.Color.Black;
      this.label3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label3.Border.TopColor = System.Drawing.Color.Black;
      this.label3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label3.Height = 0.21875F;
      this.label3.HyperLink = null;
      this.label3.Left = 4.25F;
      this.label3.Name = "label3";
      this.label3.Style = "";
      this.label3.Text = "Curso";
      this.label3.Top = 0.04166667F;
      this.label3.Width = 0.9895833F;
      // 
      // label4
      // 
      this.label4.Border.BottomColor = System.Drawing.Color.Black;
      this.label4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label4.Border.LeftColor = System.Drawing.Color.Black;
      this.label4.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label4.Border.RightColor = System.Drawing.Color.Black;
      this.label4.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label4.Border.TopColor = System.Drawing.Color.Black;
      this.label4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.label4.Height = 0.1979167F;
      this.label4.HyperLink = null;
      this.label4.Left = 5.385417F;
      this.label4.Name = "label4";
      this.label4.Style = "";
      this.label4.Text = "Sentido";
      this.label4.Top = 0.04166667F;
      this.label4.Width = 0.9375F;
      // 
      // detail
      // 
      this.detail.ColumnSpacing = 0F;
      this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtIDTransponder1,
            this.txtDIA_HORA1,
            this.txtVelocidad1,
            this.txtCurso1,
            this.txtSENTIDO1});
      this.detail.Height = 0.2083333F;
      this.detail.Name = "detail";
      // 
      // txtIDTransponder1
      // 
      this.txtIDTransponder1.Border.BottomColor = System.Drawing.Color.Black;
      this.txtIDTransponder1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtIDTransponder1.Border.LeftColor = System.Drawing.Color.Black;
      this.txtIDTransponder1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtIDTransponder1.Border.RightColor = System.Drawing.Color.Black;
      this.txtIDTransponder1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtIDTransponder1.Border.TopColor = System.Drawing.Color.Black;
      this.txtIDTransponder1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtIDTransponder1.DataField = "IDTransponder";
      this.txtIDTransponder1.Height = 0.2F;
      this.txtIDTransponder1.Left = 0.0246063F;
      this.txtIDTransponder1.Name = "txtIDTransponder1";
      this.txtIDTransponder1.Style = "";
      this.txtIDTransponder1.Text = "txtIDTransponder1";
      this.txtIDTransponder1.Top = 0F;
      this.txtIDTransponder1.Width = 1F;
      // 
      // txtDIA_HORA1
      // 
      this.txtDIA_HORA1.Border.BottomColor = System.Drawing.Color.Black;
      this.txtDIA_HORA1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtDIA_HORA1.Border.LeftColor = System.Drawing.Color.Black;
      this.txtDIA_HORA1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtDIA_HORA1.Border.RightColor = System.Drawing.Color.Black;
      this.txtDIA_HORA1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtDIA_HORA1.Border.TopColor = System.Drawing.Color.Black;
      this.txtDIA_HORA1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtDIA_HORA1.DataField = "DIA_HORA";
      this.txtDIA_HORA1.Height = 0.1968504F;
      this.txtDIA_HORA1.Left = 1.058071F;
      this.txtDIA_HORA1.Name = "txtDIA_HORA1";
      this.txtDIA_HORA1.Style = "";
      this.txtDIA_HORA1.Text = "txtDIA_HORA1";
      this.txtDIA_HORA1.Top = 0F;
      this.txtDIA_HORA1.Width = 2.042323F;
      // 
      // txtVelocidad1
      // 
      this.txtVelocidad1.Border.BottomColor = System.Drawing.Color.Black;
      this.txtVelocidad1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtVelocidad1.Border.LeftColor = System.Drawing.Color.Black;
      this.txtVelocidad1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtVelocidad1.Border.RightColor = System.Drawing.Color.Black;
      this.txtVelocidad1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtVelocidad1.Border.TopColor = System.Drawing.Color.Black;
      this.txtVelocidad1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtVelocidad1.DataField = "Velocidad";
      this.txtVelocidad1.Height = 0.2F;
      this.txtVelocidad1.Left = 3.272638F;
      this.txtVelocidad1.Name = "txtVelocidad1";
      this.txtVelocidad1.Style = "";
      this.txtVelocidad1.Text = "txtVelocidad1";
      this.txtVelocidad1.Top = 0F;
      this.txtVelocidad1.Width = 1F;
      // 
      // txtCurso1
      // 
      this.txtCurso1.Border.BottomColor = System.Drawing.Color.Black;
      this.txtCurso1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtCurso1.Border.LeftColor = System.Drawing.Color.Black;
      this.txtCurso1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtCurso1.Border.RightColor = System.Drawing.Color.Black;
      this.txtCurso1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtCurso1.Border.TopColor = System.Drawing.Color.Black;
      this.txtCurso1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtCurso1.DataField = "Curso";
      this.txtCurso1.Height = 0.2F;
      this.txtCurso1.Left = 4.330709F;
      this.txtCurso1.Name = "txtCurso1";
      this.txtCurso1.Style = "";
      this.txtCurso1.Text = "txtCurso1";
      this.txtCurso1.Top = 0F;
      this.txtCurso1.Width = 1F;
      // 
      // txtSENTIDO1
      // 
      this.txtSENTIDO1.Border.BottomColor = System.Drawing.Color.Black;
      this.txtSENTIDO1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtSENTIDO1.Border.LeftColor = System.Drawing.Color.Black;
      this.txtSENTIDO1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtSENTIDO1.Border.RightColor = System.Drawing.Color.Black;
      this.txtSENTIDO1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtSENTIDO1.Border.TopColor = System.Drawing.Color.Black;
      this.txtSENTIDO1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
      this.txtSENTIDO1.DataField = "SENTIDO";
      this.txtSENTIDO1.Height = 0.2F;
      this.txtSENTIDO1.Left = 5.38878F;
      this.txtSENTIDO1.Name = "txtSENTIDO1";
      this.txtSENTIDO1.Style = "";
      this.txtSENTIDO1.Text = "txtSENTIDO1";
      this.txtSENTIDO1.Top = 0F;
      this.txtSENTIDO1.Width = 1F;
      // 
      // pageFooter
      // 
      this.pageFooter.Height = 0.25F;
      this.pageFooter.Name = "pageFooter";
      // 
      // Reporte1
      // 
      this.MasterReport = false;
      sqlDBDataSource1.CommandTimeout = 60;
      sqlDBDataSource1.ConnectionString = "data source=PC-BHB-02\\SQLEXPRESS;initial catalog=SIGAnalisis1;integrated security" +
          "=SSPI;persist security info=False";
      sqlDBDataSource1.SQL = resources.GetString("sqlDBDataSource1.SQL");
      this.DataSource = sqlDBDataSource1;
      this.PageSettings.PaperHeight = 11F;
      this.PageSettings.PaperWidth = 8.5F;
      this.Sections.Add(this.pageHeader);
      this.Sections.Add(this.detail);
      this.Sections.Add(this.pageFooter);
      this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                  "l; font-size: 10pt; color: Black; ", "Normal"));
      this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"));
      this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                  "lic; ", "Heading2", "Normal"));
      this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"));
      ((System.ComponentModel.ISupportInitialize)(this.ID)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtIDTransponder1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtDIA_HORA1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtVelocidad1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtCurso1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtSENTIDO1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }
    #endregion

    private DataDynamics.ActiveReports.TextBox txtIDTransponder1;
    private DataDynamics.ActiveReports.TextBox txtDIA_HORA1;
    private DataDynamics.ActiveReports.TextBox txtVelocidad1;
    private DataDynamics.ActiveReports.TextBox txtCurso1;
    private DataDynamics.ActiveReports.TextBox txtSENTIDO1;
    private DataDynamics.ActiveReports.Label ID;
    private DataDynamics.ActiveReports.Label label1;
    private DataDynamics.ActiveReports.Label label2;
    private DataDynamics.ActiveReports.Label label3;
    private DataDynamics.ActiveReports.Label label4;
  }
}
