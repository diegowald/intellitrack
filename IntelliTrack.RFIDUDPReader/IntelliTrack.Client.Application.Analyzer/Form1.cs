using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



namespace IntelliTrack.Client.Application
{
  public partial class Form1 : Form
  {

    private WeifenLuo.WinFormsUI.Docking.DeserializeDockContent m_deserializeDockContent;

    public Form1()
    {
      InitializeComponent();
      //m_solutionExplorer = new DummySolutionExplorer();
      //m_solutionExplorer.RightToLeftLayout = RightToLeftLayout;
      m_deserializeDockContent = new WeifenLuo.WinFormsUI.Docking.DeserializeDockContent(GetContentFromPersistString);
      dockPanel.MouseWheel += new MouseEventHandler(dockPanel_MouseWheel);
      this.Text = ObtenerBarraTitulo();
      CrearElementosMenu();
    }

    private void CrearElementosMenu()
    {
      menuStrip1.Items.Clear();
      Dictionary<string, IntelliTrack.Client.Application.Menues.MenuItem> menus = ValidacionSeguridad.Instance.Menues;

      foreach (KeyValuePair<string, IntelliTrack.Client.Application.Menues.MenuItem> pair in menus)
      {
        ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem();
        toolStripMenuItem1.Text = pair.Value.Descripcion2;
        toolStripMenuItem1.Tag = pair.Value;
        toolStripMenuItem1.Visible = true;
        toolStripMenuItem1.Available = true;
        if (!pair.Value.EsContenedor)
          toolStripMenuItem1.Click += new EventHandler(menuitem_Click);
        if (pair.Value.MenuPosicion.Length > 3)
        {
          ToolStripMenuItem toolStripMenuItem2 = GetMenuItemPadre(pair.Value.MenuPosicion.Substring(0, pair.Value.MenuPosicion.Length - 3));
          if (toolStripMenuItem2 != null)
            toolStripMenuItem2.DropDownItems.Add(toolStripMenuItem1);
        }
        else
        {
          menuStrip1.Items.Add(toolStripMenuItem1);
        }
      }
    }

    private string ObtenerBarraTitulo()
    {
      string s = "IntelliTrack Análisis";

      string aux = System.Reflection.Assembly.GetExecutingAssembly().FullName;
      string[] aux2 = aux.Split(',');
      aux2 = aux2[1].Split('=');

      s += " v" + aux2[1].Trim();
      return s;
    }

    void dockPanel_MouseWheel(object sender, MouseEventArgs e)
    {
      if (dockPanel.ActiveDocument != null)
      {
        (dockPanel.ActiveDocument as IntelliTrack.Client.Application.MapForm).MapForm_MouseWheel(sender, e);
      }
    }


    private void QuitarControlesAForms()
    {
      frmMap.CloseButton = false;
      eventosFrm.CloseButton = false;
      Overviewmap.CloseButton = false;
      elementInfo.CloseButton = false;
      RealtimeInfo.CloseButton = false;
      reporteRegadores.CloseButton = false;
      rawDataForm.CloseButton = false;
    }

    private int nCount = 0;

    public MapForm frmMap = null;
    public Eventos eventosFrm = null;
    public OverviewMap Overviewmap = null;
    public ElementInformation elementInfo = null;
    public ElementInformation RealtimeInfo = null;
    //public ReportForm reportForm = null;
    public ReportForm reporteRegadores = null;
    public frmRawData rawDataForm = null;

    private void Form1_Shown(object sender, EventArgs e)
    {
      SplashScreen.SplashScreen.SetStatus("Abriendo ventana de informacion de capas de tiempo real");
      RealtimeInfo = new ElementInformation(this);
      RealtimeInfo.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;//.Document;// WeifenLuo.WinFormsUI.Docking.DockState.Float;
      RealtimeInfo.Text = "EVENTOS";
      RealtimeInfo.Name = "Eventos";
      RealtimeInfo.TabText = "Eventos";
      RealtimeInfo.Text = "Eventos";
      RealtimeInfo.CloseButtonVisible = false;
      RealtimeInfo.CloseButton = false;

      /*      if (this.dockPanel.DocumentStyle == WeifenLuo.WinFormsUI.Docking.DocumentStyle.SystemMdi)
            {
              RealtimeInfo.MdiParent = this;
              RealtimeInfo.Show();
            }
            else
            {
              RealtimeInfo.Show(dockPanel);
            }
            nCount++;*/

      SplashScreen.SplashScreen.SetStatus("Abriendo Mapa");
      frmMap = new MapForm(this);
      frmMap.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
      frmMap.Text = "Mapa";
      nCount++;

      /*reportForm = new ReportForm(this);
      reportForm.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
      reportForm.Text = "Reporte";

      nCount++;*/

      SplashScreen.SplashScreen.SetStatus("Generando eventos...");
      //System.Windows.Forms.Application.DoEvents();
      eventosFrm = new Eventos(this);
      eventosFrm.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
      eventosFrm.CloseButtonVisible = false;
      eventosFrm.CloseButton = false;
      eventosFrm.Text = "Eventos";


      SplashScreen.SplashScreen.SetStatus("Abriendo vista general");
      //System.Windows.Forms.Application.DoEvents();
      Overviewmap = new OverviewMap(this);
      Overviewmap.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float;
      Overviewmap.Text = "MAPA";

      SplashScreen.SplashScreen.SetStatus("Abriendo ventana de selección");
      //System.Windows.Forms.Application.DoEvents();
      elementInfo = new ElementInformation(this);
      elementInfo.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
      elementInfo.Text = "INFORMACION";
      elementInfo.CloseButtonVisible = false;
      elementInfo.CloseButton = false;

      //System.Windows.Forms.Application.DoEvents();
      SplashScreen.SplashScreen.CloseForm();

      string configFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "dock.xml");

      if (!System.IO.File.Exists(configFile))
      {
        if (this.dockPanel.DocumentStyle == WeifenLuo.WinFormsUI.Docking.DocumentStyle.SystemMdi)
        {
          frmMap.MdiParent = this;
          frmMap.Show();
          /*reportForm.MdiParent = this;
          reportForm.Show();*/
        }
        else
        {
          frmMap.Show(dockPanel);
          //reportForm.Show(dockPanel);
        }
        eventosFrm.Show(dockPanel);
        Overviewmap.Show(dockPanel/*dockPanel.DockWindows[WeifenLuo.WinFormsUI.Docking.DockState.DockBottom]*/);
        elementInfo.Show(dockPanel);
        RealtimeInfo.Show(dockPanel);
      }
      else
      {
        dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
      }
      System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.IProvider> providers = IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.GetRealTimeLayerProviders();
      foreach (System.Collections.Generic.KeyValuePair<string, SharpMap.Data.Providers.IProvider> pair in providers)
      {
        SharpMap.Data.FeatureDataSet fds = new SharpMap.Data.FeatureDataSet();
        pair.Value.ExecuteIntersectionQuery(pair.Value.GetExtents(), fds);
        fds.Tables[0].TableName = pair.Key;
        RealtimeInfo.SetData(fds.Tables[0]);
        ((SharpMap.Data.Providers.MemoryDataProviderBase)pair.Value).OnDataUpdated += new SharpMap.Data.Providers.MemoryDataProviderBase.DataUpdated(Form1_OnDataUpdated);
      }
      IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.StartTimer();

      //      reportForm = new ReportForm(this, new IntelliTrack.Client.Application.Reportes.Reporte1());
      //      reportForm.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
      //      reportForm.Text = "Reporte";
      nCount++;
      //      reportForm.Show(dockPanel);

      reporteRegadores = new ReportForm(this, new IntelliTrack.Client.Application.Reportes.Regadores());
      reporteRegadores.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
      reporteRegadores.Text = "Reporte de Regadores";
      nCount++;
      reporteRegadores.Show(dockPanel);

      rawDataForm = new frmRawData(this);
      rawDataForm.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
      rawDataForm.Text = "Información obtenida";
      nCount++;
      rawDataForm.Show(dockPanel);

      frmMap.Activate();

      //Aca voy a activar o desactivar los items del menu, en funcion de la seguridad
      elementInfo.Hide();
      QuitarControlesAForms();
    }


    private WeifenLuo.WinFormsUI.Docking.IDockContent GetContentFromPersistString(string persistString)
    {
      if (persistString == typeof(MapForm).ToString())
      {
        return frmMap;
      }
      else if (persistString == typeof(Eventos).ToString())
      {
        return eventosFrm;
      }
      else if (persistString == typeof(OverviewMap).ToString())
      {
        return Overviewmap;
      }
      else if (persistString.Substring(0, typeof(ElementInformation).ToString().Length) == typeof(ElementInformation).ToString())
      {
        if (persistString.Substring(typeof(ElementInformation).ToString().Length + 1) == "INFORMACION")
        {
          return elementInfo;
        }
        else
        {
          return RealtimeInfo;
        }
      }
      else return null;
    }

    void Form1_OnDataUpdated()
    {
      if (RealtimeInfo.InvokeRequired)
      {
        SharpMap.Data.Providers.MemoryDataProviderBase.DataUpdated d = new SharpMap.Data.Providers.MemoryDataProviderBase.DataUpdated(Form1_OnDataUpdated);
        this.Invoke(d, null);
        return;
      }
      else
      {
        System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.IProvider> providers = IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.GetRealTimeLayerProviders(); ;
        foreach (System.Collections.Generic.KeyValuePair<string, SharpMap.Data.Providers.IProvider> pair in providers)
        {
          SharpMap.Data.FeatureDataSet fds = new SharpMap.Data.FeatureDataSet();
          pair.Value.ExecuteIntersectionQuery(pair.Value.GetExtents(), fds);
          fds.Tables[0].TableName = pair.Key;
          RealtimeInfo.SetData(fds.Tables[0]);
        }
        //IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.UDPDataArrived
      }
    }


    private void Form1_Layout(object sender, LayoutEventArgs e)
    {
      //if (m_bLayoutCalled == false)
      //{
      //  m_bLayoutCalled = true;
      //  m_dt = DateTime.Now;
      //  if (SplashScreen.SplashForm != null)
      //    SplashScreen.SplashForm.Owner = this;
      //  this.Activate();
      //  SplashScreen.CloseForm();
      //  timer1.Start();
      //}
    }




    private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
      SalirAplicacion();
    }

    private void ExportarDataSetAPortapapeles()
    {
      if (rawDataForm != null)
        rawDataForm.ExportarDataSetAPortapapeles();
    }

    
    private System.Windows.Forms.ToolStripMenuItem GetMenuItemPadre(string tag)
    {
      System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

      foreach (System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1 in menuStrip1.Items)
      {
        IntelliTrack.Client.Application.Menues.MenuItem menuItem = toolStripMenuItem1.Tag as IntelliTrack.Client.Application.Menues.MenuItem;
        if (menuItem.MenuPosicion == tag)
        {
          return toolStripMenuItem1;
        }
      }
      return null;
    }

    private void menuitem_Click(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripMenuItem toolStripMenuItem = sender as System.Windows.Forms.ToolStripMenuItem;
      IntelliTrack.Client.Application.Menues.MenuItem menuItem = toolStripMenuItem.Tag as IntelliTrack.Client.Application.Menues.MenuItem;
      string s = menuItem.FunctionToInvoke.ToLower();
      if ((menuItem.FunctionToInvoke.ToLower() != null) && s != "abmcategorias" && s != "abmtags" && s != "abmtransponders")
      {
        if (s != "saliraplicacion")
        {
          if (s == "exportardatasetaportapapeles")
            goto label_1;
          return;
        }
        SalirAplicacion();
        return;
      label_1:
        ExportarDataSetAPortapapeles();
      }
    }

    private void SalirAplicacion()
    {
      System.Windows.Forms.Application.Exit();
    }


  } // class Form1

}
