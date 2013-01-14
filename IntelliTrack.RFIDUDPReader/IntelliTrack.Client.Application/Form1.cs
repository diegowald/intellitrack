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
      AplicarRestriccionEnMenues();
    }

    private void AplicarRestriccionEnMenu(string MenuKey, ToolStripMenuItem menu)
    {
      menu.Visible = ValidacionSeguridad.Instance.Menues.ContainsKey(MenuKey);
    }

    private void AplicarRestriccionEnMenues()
    {
      ///diego
      /*
      ///diego
      AplicarRestriccionEnMenu("003", infraestructuraToolStripMenuItem);
      /*AplicarRestriccionEnMenu("003001", categoríasToolStripMenuItem);
      AplicarRestriccionEnMenu("003002", tagsToolStripMenuItem);
      AplicarRestriccionEnMenu("003003", transpondersToolStripMenuItem);*/

      ///diego
      //infraestructuraToolStripMenuItem.Visible = true;
      ///diego
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
      if (menuItem.FunctionToInvoke.ToLower() != null)
      {
        if (s == "abmcategorias") goto label_1;
        if (s == "abmtags") goto label_2;
        if (s == "abmtransponders") goto label_3;
        if (s == "saliraplicacion") goto label_4;
        return;
      label_1:
        ABMCategorias();
        return;
      label_2:
        ABMTags();
        return;
      label_3:
        ABMTransponders();
        return;
      label_4:
        SalirAplicacion();
      }
    }

    private void ABMCategorias()
    {
      IntelliTrack.Client.Application.dlgCategorias dlgCategorias = new IntelliTrack.Client.Application.dlgCategorias();
      if (dlgCategorias.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.ReloadDatabaseLayers();
        ReloadTabsRealTimeLayers();
      }
    }

    private void ABMTags()
    {
      IntelliTrack.Client.Application.dlgTags dlgTags = new IntelliTrack.Client.Application.dlgTags();
      if (dlgTags.ShowDialog() == System.Windows.Forms.DialogResult.OK)
         IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.ReloadDatabaseLayerInformation();
    }

    private void ABMTransponders()
    {
      IntelliTrack.Client.Application.dlgTransponders dlgTransponders = new IntelliTrack.Client.Application.dlgTransponders();
      if (dlgTransponders.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.ReloadDatabaseLayerInformation();
        ReloadTabsRealTimeLayers();
      }
    }

    private void SalirAplicacion()
    {
      //dockPanel.SaveAsXml("c:\\dock.xml");
      System.Windows.Forms.Application.Exit();
    }

    private string ObtenerBarraTitulo()
    {
      string s = "IntelliTrack";
      if (ValidacionSeguridad.Instance.Write2DB)
        s += " Server";
      else
        s += " Cliente";

      string aux = System.Reflection.Assembly.GetExecutingAssembly().FullName;
      string[] aux2=aux.Split(',');
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


    private int nCount = 0;

    public MapForm frmMap = null;
    public Eventos eventosFrm = null;
    public OverviewMap Overviewmap = null;
    public ElementInformation elementInfo = null;
    public ElementInformation RealtimeInfo = null;

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
      /*frmMap.Text = "Form " + nCount.ToString();*/

      nCount++;

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
        }
        else
        {
          frmMap.Show(dockPanel);
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

      ReloadTabsRealTimeLayers();
      IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.StartTimer();

      //Aca voy a activar o desactivar los items del menu, en funcion de la seguridad
      elementInfo.Hide();
    }

    private void ReloadTabsRealTimeLayers()
    {
      System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.IProvider> providers = IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.GetRealTimeLayerProviders();
      RealtimeInfo.RefreshTabs(providers);
      foreach (System.Collections.Generic.KeyValuePair<string, SharpMap.Data.Providers.IProvider> pair in providers)
      {
        ((SharpMap.Data.Providers.MemoryDataProviderBase)pair.Value).OnDataUpdated += new SharpMap.Data.Providers.MemoryDataProviderBase.DataUpdated(Form1_OnDataUpdated);
      }
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
      else if (persistString.Substring(0,typeof(ElementInformation).ToString().Length)  == typeof(ElementInformation).ToString())
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

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      dockPanel.SaveAsXml("c:\\dock.xml");
      System.Windows.Forms.Application.Exit();
    }

    private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
    {
      dlgCategorias dlg = new dlgCategorias();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        // Aca debo refrescar los tabs y los databaselayers.
        //Refresh de algo
        IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.ReloadDatabaseLayers();
        ReloadTabsRealTimeLayers();
        //System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.IProvider> providers = IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.GetRealTimeLayerProviders();
      }
    }

    private void transpondersToolStripMenuItem_Click(object sender, EventArgs e)
    {
      dlgTransponders dlg = new dlgTransponders();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.ReloadDatabaseLayerInformation();
      }
    }

    private void tagsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      dlgTags dlg = new dlgTags();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.ReloadDatabaseLayerInformation();
      }
    }

  }
}