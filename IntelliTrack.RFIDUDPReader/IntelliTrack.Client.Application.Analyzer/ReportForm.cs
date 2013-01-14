using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application
{
  public partial class ReportForm : /*Form  */frmBaseDockingForm
  {
    private Form _parent;
    private DataDynamics.ActiveReports.ActiveReport3 _reporte;
    private string _SQL;

    public ReportForm(Form parent, DataDynamics.ActiveReports.ActiveReport3 rpt)
    {
      InitializeComponent();
      _parent = parent;
      _reporte = rpt;

      if (rpt is IntelliTrack.Client.Application.Reportes.Reporte1)
      {
        IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.SetHistoricDataHandlerReportForm(this);
      }
      else if (rpt is IntelliTrack.Client.Application.Reportes.Regadores)
      {
        IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.SetHistoricDataHandlerReporteRegadoresForm(this);
        (rpt as IntelliTrack.Client.Application.Reportes.Regadores).OnCalcularTiemposTotales += new IntelliTrack.Client.Application.Reportes.Regadores.DoCalcularTiemposTotales(ReportForm_OnCalcularTiemposTotales);

      }
      HideOnClose = true;
    }


    public string SQL
    {
      get
      {
        return _SQL;
      }
      set
      {
        _SQL = SQL;
      }
    }

    public override void RefreshMe()
    {
      //base.RefreshMe();
    }



    ///nuevo
    ///


    private System.Data.DataSet Data;


    private void OcultarSiEstaVacio(System.Data.DataSet ds)
    {
      bool flag = (ds != null) && (ds.Tables[0].Rows.Count > 0);
      if (flag)
      {
        Show();
        return;
      }
      Hide();
    }

    public void ReloadData(System.Data.DataSet ds, System.DateTime FechaDesde, System.DateTime FechaHasta, double VelocidadDesde, double VelocidadHasta)
    {
      _reporte.DataSource = ds;
      Data = ds;
      _reporte.DataMember = ds.Tables[0].TableName;
      viewer1.Document = _reporte.Document;
      if ((_reporte as IntelliTrack.Client.Application.Reportes.Regadores) != null)
      {
        (_reporte as IntelliTrack.Client.Application.Reportes.Regadores).FechaDesde.Value = FechaDesde.ToString("dd/MM/yyyy hh:mm:ss");
        (_reporte as IntelliTrack.Client.Application.Reportes.Regadores).FechaHasta.Value = FechaHasta.ToString("dd/MM/yyyy hh:mm:ss");
        (_reporte as IntelliTrack.Client.Application.Reportes.Regadores).VelocidadDesde.Value = VelocidadDesde.ToString();
        (_reporte as IntelliTrack.Client.Application.Reportes.Regadores).VelocidadHasta.Value = VelocidadHasta.ToString();
      }
      _reporte.Run();
      OcultarSiEstaVacio(ds);
    }

    public void ReloadData(System.Data.DataSet ds)
    {
      System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("Es");
      Data = ds;
      if ((ds != null) && (ds.Tables.Count > 0))
      {
        _reporte.DataSource = ds;
        _reporte.DataMember = ds.Tables[0].TableName;
      }
      else
      {
        _reporte.DataSource = null;
      }
      viewer1.Document = _reporte.Document;
      _reporte.Run();
      OcultarSiEstaVacio(ds);
    }

    public void ReloadData()
    {
      DataDynamics.ActiveReports.DataSources.SqlDBDataSource sqlDBDataSource = _reporte.DataSource as DataDynamics.ActiveReports.DataSources.SqlDBDataSource;
      sqlDBDataSource.ConnectionString = ValidacionSeguridad.Instance.GetApplicationConnectionString();
      sqlDBDataSource.SQL = SQL;
      viewer1.Document = _reporte.Document;
      _reporte.Run(true);
    }

    private void ReportForm_OnCalcularTiemposTotales(string IDVehiculo, out int TiempoTotalSegundosDetenido, out int TiempoTotalSegundosCarga, out int TiempoTotalSegundosTransito)
    {
      try
      {
        if (Data != null)
        {
          object obj = Data.Tables[0].Compute("Sum(TiempoCargaSegundos)", "IDElemento = '" + IDVehiculo + "'");
          TiempoTotalSegundosCarga = System.Convert.ToInt32((long)obj);
          obj = Data.Tables[0].Compute("Sum(TiempoDetenidoSegundos)", "IDElemento = '" + IDVehiculo + "'");
          TiempoTotalSegundosDetenido = System.Convert.ToInt32((long)obj);
          obj = Data.Tables[0].Compute("Sum(TiempoTransitoSegundos)", "IDElemento = '" + IDVehiculo + "'");
          TiempoTotalSegundosTransito = System.Convert.ToInt32((long)obj);
        }
        else
        {
          TiempoTotalSegundosCarga = 0;
          TiempoTotalSegundosDetenido = 0;
          TiempoTotalSegundosTransito = 0;
        }
      }
      catch (System.Exception)
      {
        TiempoTotalSegundosCarga = 0;
        TiempoTotalSegundosDetenido = 0;
        TiempoTotalSegundosTransito = 0;
      }
    }


  } // class ReportForm

}
