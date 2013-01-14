using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application
{
  public partial class ElementInformation : /*Form*/ frmBaseDockingForm
  {

    private System.Collections.Generic.List<string> tabPageNames;
    private Form parentForm;

    private System.Collections.Generic.Dictionary<string, SharpMap.Data.FeatureDataTable> DataSources;

    public ElementInformation(Form parent)
    {
      InitializeComponent();
      parentForm = parent;
      DataSources = new Dictionary<string, SharpMap.Data.FeatureDataTable>();
      tabPageNames = new List<string>();
    }

    public override void RefreshMe()
    {
    }

    
    private void CreateTab(SharpMap.Data.FeatureDataTable featureDataTable)
    {
      tabControl1.TabPages.Add(featureDataTable.TableName, featureDataTable.TableName);
      tabPageNames.Add(featureDataTable.TableName);
      
      System.Windows.Forms.DataGridView dv = new DataGridView();
      dv.AllowUserToAddRows = false;
      dv.AllowUserToDeleteRows = false;
      dv.AllowUserToOrderColumns = true;
      dv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dv.Font = this.Font;
      dv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dv_DataBindingComplete);
      dv.Dock = System.Windows.Forms.DockStyle.Fill;
      dv.Location = new System.Drawing.Point(3, 3);
      dv.Name = "dvSelection";
      dv.ReadOnly = true;
      dv.Size = new System.Drawing.Size(278, 234);
      dv.TabIndex = 1;
      dv.VirtualMode = true;
      dv.CellValueNeeded += new DataGridViewCellValueEventHandler(dv_CellValueNeeded);
      dv.CellDoubleClick += new DataGridViewCellEventHandler(dv_CellDoubleClick);
      dv.CellClick += new DataGridViewCellEventHandler(dv_CellClick);
      dv.AllowUserToAddRows = false;
      dv.AllowUserToDeleteRows = false;
      dv.AllowUserToOrderColumns = true;
      dv.AllowUserToResizeColumns = true;
      dv.AllowUserToResizeRows = true;
      dv.MultiSelect = false;
      dv.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(dv_CellToolTipTextNeeded);
      this.tabControl1.TabPages[featureDataTable.TableName].Controls.Add(dv);
      //dv.DataSource = featureDataTable;
      DataSources[featureDataTable.TableName] = featureDataTable;
      SetupRowsAndColumns(dv, featureDataTable);
      dv.Refresh();
    }

    void dv_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine(e.ColumnIndex);
      System.Diagnostics.Debug.WriteLine(e.RowIndex);
      if (e.RowIndex >= 0)
      {
        string TransponderID = (tabControl1.SelectedTab.Controls[0] as System.Windows.Forms.DataGridView)
        .Rows[e.RowIndex].Cells["Transponder"].Value.ToString();
        e.ToolTipText = IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.GetFunctionValue(TransponderID);
      }
      else
      {
        e.ToolTipText = "";
      }
    }



    void dv_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex == -1) // El usuario hizo click en el encabezado...
      {
        DataGridView dv = this.tabControl1.SelectedTab.Controls["dvSelection"] as DataGridView;
        switch (dv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection)
        {
          case SortOrder.Ascending:
            dv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            break;
          case SortOrder.Descending:
            dv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.None;
            break;
          default:
            dv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            break;
        }
        SharpMap.Data.FeatureDataTable fdt = DataSources[this.tabControl1.SelectedTab.Text];
        SortDataTable(fdt, dv);
        dv.Invalidate();
      }
    }

    private void SetupRowsAndColumns(DataGridView dv, System.Data.DataTable dt)
    {
      if (dv.RowCount != dt.Rows.Count)
        dv.RowCount = dt.Rows.Count;
      dv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
      if (dv.ColumnCount != dt.Columns.Count)
      {
        dv.ColumnCount = dt.Columns.Count;

        for (int i = 0; i < dt.Columns.Count; i++)
        {
          System.Data.DataColumn col = dt.Columns[i];
          dv.Columns[i].HeaderText = col.ColumnName;
          dv.Columns[i].Name = col.ColumnName;
          dv.Columns[i].ReadOnly = true;
          dv.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
          dv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

          //dv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
        OcultarColumnas(dv, dt.TableName);
      }
    }

    void dv_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
      try
      {
        TabPage tp = (sender as DataGridView).Parent as TabPage;
        string DataSourceName = tp.Text;
        if (DataSources.ContainsKey(DataSourceName))
        {
          System.Data.DataTable dt = (DataSources[DataSourceName] as System.Data.DataTable).DefaultView.ToTable();
          e.Value = dt. Rows[e.RowIndex].ItemArray[e.ColumnIndex];
        }
        else
          e.Value = "";
      }
      catch (System.Collections.Generic.KeyNotFoundException ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        // No existe la tabla. Se ignora.
      }
    }

    private void UpdateTable(SharpMap.Data.FeatureDataTable fdt)
    {      string tabName = fdt.TableName;
      
      DataSources[tabName] = fdt;
      DataGridView dv = ((DataGridView)tabControl1.TabPages[fdt.TableName].Controls[0]);
      SortDataTable(fdt, dv);
      SetupRowsAndColumns(dv, fdt);
      dv.Refresh();
    }

    private static void SortDataTable(SharpMap.Data.FeatureDataTable fdt, DataGridView dv)
    {
      string sort = "";
      for (int i = 0; i < dv.ColumnCount; i++)
      {
        switch (dv.Columns[i].HeaderCell.SortGlyphDirection)
        {
          case SortOrder.Ascending:
            //sort += " " + dv.Columns[i].HeaderText + " ASC, ";
            sort += " " + dv.Columns[i].Name + " ASC, ";
            break;
          case SortOrder.Descending:
            //sort += " " + dv.Columns[i].HeaderText + " DESC, ";
            sort += " " + dv.Columns[i].Name + " DESC, ";
            break;
          default:
            break;
        }
      }
      if (sort.Length > 0)
      {
        sort = sort.Substring(0, sort.Length - 2);
      }
      fdt.DefaultView.Sort = sort;
    }



    private void OcultarColumnas(System.Windows.Forms.DataGridView dv, string tabName)
    {
      if (dv.Columns.Count == 0)
        return;
      for (int i = 0; i < dv.Columns.Count; i++)
      {
        dv.Columns[i].Visible = false;
      }
      string s = tabName;
      if ((tabName != null) && s == "Frentes")
      {
        HacerColumnaVisible(dv, "Camión", "Vehículo");
        HacerColumnaVisible(dv, "Día");
        HacerColumnaVisible(dv, "Hora");
        HacerColumnaVisible(dv, "Velocidad");
        HacerColumnaVisible(dv, "Dirección");
        HacerColumnaVisible(dv, "Punto de Referencia");
        HacerColumnaVisible(dv, "Camino de Referencia");
        HacerColumnaVisible(dv, "Temperatura");
        HacerColumnaVisible(dv, "Area", "Area de Referencia");
        HacerColumnaVisible(dv, "Frente", "Vehículo");
        HacerColumnaVisible(dv, "Tensión");
        HacerColumnaVisible(dv, "Transponder");
        HacerColumnaVisible(dv, "lon");
        HacerColumnaVisible(dv, "lat");
        HacerColumnaVisible(dv, "Curso8Rumbos", "Curso");
        return;
      }
      HacerColumnaVisible(dv, "Camión", "Vehículo");
      HacerColumnaVisible(dv, "Día");
      HacerColumnaVisible(dv, "Hora");
      HacerColumnaVisible(dv, "Velocidad");
      HacerColumnaVisible(dv, "Dirección");
      HacerColumnaVisible(dv, "Punto de Referencia");
      HacerColumnaVisible(dv, "Camino de Referencia");
      HacerColumnaVisible(dv, "Temperatura");
      HacerColumnaVisible(dv, "Area", "Area de Referencia");
      HacerColumnaVisible(dv, "Frente");
      HacerColumnaVisible(dv, "Tensión");
      HacerColumnaVisible(dv, "Transponder");
      HacerColumnaVisible(dv, "lon");
      HacerColumnaVisible(dv, "lat");
      HacerColumnaVisible(dv, "Curso8Rumbos", "Curso");
    }

    private void HacerColumnaVisible(System.Windows.Forms.DataGridView dv, string ColumnName)
    {
      HacerColumnaVisible(dv, ColumnName, "");
    }

    private void HacerColumnaVisible(System.Windows.Forms.DataGridView dv, string ColumnName, string HeaderText)
    {
      if (dv.Columns.Contains(ColumnName))
      {
        dv.Columns[ColumnName].Visible = true;
        if (HeaderText != "")
          dv.Columns[ColumnName].HeaderText = HeaderText;
      }
    }


    void dv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      /*
        DataGridView dv = this.tabControl1.SelectedTab.Controls["dvSelection"] as DataGridView;
      uint IDElement = 0;
      if (e.RowIndex >= 0)
      {
        if (uint.TryParse(dv.Rows[e.RowIndex].Cells[0].Value.ToString(), out IDElement))
        {
          IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.ZoomToElement(
            this.tabControl1.SelectedTab.Text, IDElement);
        }
      }
      */
      System.Windows.Forms.DataGridView dataGridView = tabControl1.SelectedTab.Controls["dvSelection"] as System.Windows.Forms.DataGridView;
      if (e.RowIndex >= 0)
      {
        string s = dataGridView.Rows[e.RowIndex].Cells["Camión"].Value.ToString();
        if (s != "")
          IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.ZoomToElement(tabControl1.SelectedTab.Text, s);
      }
    }

    void dv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
      System.Windows.Forms.DataGridView dv = sender as System.Windows.Forms.DataGridView;
      foreach(System.Windows.Forms.DataGridViewColumn col in dv.Columns)
        {
          col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
    

    public void SetData(SharpMap.Data.FeatureDataSet fds)
    {
      tabControl1.TabPages.Clear();
      foreach(SharpMap.Data.FeatureDataTable fdt in fds.Tables)
      {
        CreateTab(fdt);
      }
    }

    public void SetData(SharpMap.Data.FeatureDataTable fdt)
    {
      if (this.tabControl1.TabPages[fdt.TableName] == null)
      {
        CreateTab(fdt);
      }
      else
      {
        UpdateTable(fdt);
      }
    }

    public void ClearData()
    {
      tabControl1.TabPages.Clear();
    }

    protected override string GetPersistString()
    {
      // Add extra information into the persist string for this document
      // so that it is available when deserialized.
      return GetType().ToString() + "," + this.Text; //+ FileName + "," + Text;
    }

    internal void RefreshTabs(Dictionary<string, SharpMap.Data.Providers.IProvider> providers)
    {
      System.Collections.Generic.Dictionary<string, bool> tabs = new Dictionary<string, bool>();

      foreach (string tabname in tabPageNames)
      {
        tabs.Add(tabname, false);
      }

      foreach (System.Collections.Generic.KeyValuePair<string, SharpMap.Data.Providers.IProvider> pair in providers)
      {
        SharpMap.Data.FeatureDataSet fds = new SharpMap.Data.FeatureDataSet();
        pair.Value.ExecuteIntersectionQuery(pair.Value.GetExtents(), fds);
        fds.Tables[0].TableName = pair.Key;
        if (tabs.ContainsKey(pair.Key))
        {
          tabs[pair.Key] = true;
        }
        SetData(fds.Tables[0]);
      }

      foreach (System.Collections.Generic.KeyValuePair<string, bool> pair in tabs)
      {
        if (!pair.Value)
        {
          // debo borrar el tab
          tabControl1.TabPages.RemoveByKey(pair.Key);
        }
      }
    }

  }
}