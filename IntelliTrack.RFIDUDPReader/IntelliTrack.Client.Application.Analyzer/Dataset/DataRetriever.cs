using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Client.Application.Dataset
{
  public class DataRetriever
  {

    private System.Collections.Generic.Dictionary<string, object> ParsedData;

    private System.Collections.Generic.Dictionary<string, bool> _FiltroCañeros;
    private System.Collections.Generic.Dictionary<string, bool> _FiltroRegadores;
    private System.Collections.Generic.Dictionary<string, bool> _FiltroFrentes;

    private System.Timers.Timer timer;
    private bool _IsStarted;
    /* Propiedades */
    // Parametros:
    private DateTime? FechaInicioConsulta_;
    private DateTime? FechaFinConsulta_;
    private string IDElemento_;
    private string IDTransponder_;
    private int? Categoria_;
    private int AnimationSpeedMiliSeconds_;

    private int VelocidadMinima_;
    private int VelocidadMaxima_;

    private System.Data.DataSet dsData;
    private int CurrentPointer;

    private IntelliTrack.Client.Application.ReportForm _reporte;
    private IntelliTrack.Client.Application.ReportForm _reporteRegadores;
    private IntelliTrack.Client.Application.frmRawData _rawDataForm;
    private int QueryTimeOut_;


    public DataRetriever()
    {
      timer = new System.Timers.Timer();
      timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
      ParsedData = new System.Collections.Generic.Dictionary<string, object>();
      _IsStarted = false;
      VelocidadMinima_ = 0;
      VelocidadMaxima_ = 200;
      QueryTimeOut_ = 30;
      _reporte = null;
      _reporteRegadores = null;
      _rawDataForm = null;
      Categoria_ = ValidacionSeguridad.Instance.CategoriaUsuario;
      Vehiculo_ = "";
      Transponder_ = "";
      Punto_ = "";
      Camino_ = "";
      Area_ = "";
      FiltroPorVelocidad_ = false;
    }

    void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      //aca de alguna manera hay que transformar el datarow seleccionado a ParsedData 
      //no creo que sea dificil, pero este es el punto critico de la aplicacion....
      if (dsData == null)
        return;
      if (dsData.Tables.Count == 0)
        return;
      if (dsData.Tables[0].Rows.Count == 0)
        return;
      if (CurrentPointer == dsData.Tables[0].Rows.Count)
        return;

      System.Data.DataRow dataRow = dsData.Tables[0].Rows[CurrentPointer];
      ParsedData["Command"]= "$HISTORY";
      ParsedData["ID"]= dataRow["ID"];
      ParsedData["IDTransponder"]= dataRow["IDTransponder"];
      ParsedData["IDElemento"]= dataRow["IDElemento"];
      ParsedData["Categoria"]= dataRow["Categoria"];
      ParsedData["Equipo"]= dataRow["Equipo"];
      ParsedData["DIA_HORA"]= dataRow["DIA_HORA"];
      ParsedData["Latitud"]= dataRow["Latitud"];
      ParsedData["Longitud"]= dataRow["Longitud"];
      ParsedData["Velocidad"]= dataRow["Velocidad"];
      ParsedData["Curso"]= dataRow["Curso"];
      ParsedData["NOM_PUNTO"]= dataRow["NOM_PUNTO"];
      ParsedData["NOM_CAMINO"]= dataRow["NOM_CAMINO"];
      ParsedData["NOM_AREA"]= dataRow["NOM_AREA"];
      ParsedData["NOM_FRENTE"]= dataRow["NOM_FRENTE"];
      ParsedData["TAG_EQUIPO"]= dataRow["TAG_EQUIPO"];
      ParsedData["TRANSP_VOLT"]= dataRow["TRANSP_VOLT"];
      ParsedData["TRANSP_TEMP"]= dataRow["TRANSP_TEMP"];
      ParsedData["SENTIDO"]= dataRow["SENTIDO"];
      SharpMap.Data.Providers.MemoryDataProviderBase memoryDataProviderBase = null;
      memoryDataProviderBase = GetDataProvider((short)dataRow["Categoria"]);
      EliminarElementoAnterior(ParsedData);
      if (memoryDataProviderBase != null)
        memoryDataProviderBase.UpdateInformation(ParsedData);
      if (UDPArrivedData != null)
        UDPArrivedData("", BuildStringMessage(ParsedData));
      if (_rawDataForm != null)
        _rawDataForm.SelectedElement = CurrentPointer;
      FireQueryPosition();
      CurrentPointer++;
    }

    private System.Data.DataRow GetParameter(string IDParametro, int? IDPortal, int? IDSistema, string IDUsuario)
    {
      // Aca se lee la informacion de la base de datos
      // y se preparan los layers
      string connStr = ValidacionSeguridad.Instance.GetSecurityConnectionString();
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr);
      conn.Open();

      System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand
        ("SELECT * FROM dbo.SF_VALOR_PARAMETRO(@IDParametro, @IDPortal, @IDSistema, @IDUsuario)", conn);

      System.Data.SqlClient.SqlParameter prm = new System.Data.SqlClient.SqlParameter("@IDParametro", System.Data.SqlDbType.VarChar, 100);
      prm.Value = IDParametro;
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@IDPortal", System.Data.SqlDbType.Int);
      if (IDPortal.HasValue)
      {
        prm.Value = IDPortal.Value;
      }
      else
      {
        prm.Value = null;
      }
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@IDSistema", System.Data.SqlDbType.Int);
      if (IDSistema.HasValue)
      {
        prm.Value = IDSistema.Value;
      }
      else
      {
        prm.Value = null;
      }
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@IDUsuario", System.Data.SqlDbType.VarChar);
      if (IDUsuario != null)
      {
        prm.Value = IDUsuario;
      }
      else
      {
        prm.Value = null;
      }
      cmd.Parameters.Add(prm);

      //     IdParametro, Alcance, ValorTexto, ValorEntero, ValorDecimal, ValorLogico, ValorFechaHora
      cmd.CommandType = System.Data.CommandType.Text;
      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);

      System.Data.DataSet ds = new System.Data.DataSet();
      da.Fill(ds);

      conn.Close();

      return ds.Tables[0].Rows[0];
      //return resultado;
    }

    /* Esta funcion de abajo se deberia reemplazar por 
     * una llamada a un timer */


    public delegate void UDPDataArrived(string FromIP, string message);

    public UDPDataArrived UDPArrivedData;


    private void ExecuteQuery(/*bool ShowWindow*/)
    {
      if (dsData != null)
      {
        dsData = null;
      }

      dsData = new System.Data.DataSet();
      dsData.ReadXmlSchema("c:\\data.xsd");
      dsData.ReadXml("c:\\data.xml");

      if (_reporte != null)
      {
        //        _reporte.SQL = SQL;
        //        _reporte.ReloadData();
        _reporte.ReloadData(dsData);
      }

      if (_reporteRegadores != null)
      {
        ReporteRegadoresDataset rep = new ReporteRegadoresDataset();
        rep.Process(dsData);
        _reporteRegadores.ReloadData(rep.DataSet);
      }

      if (_rawDataForm != null)
      {
        _rawDataForm.DataSource = dsData;
      }
    }

    private string BuildTransponderSQLFilter(string FieldName)
    {
      // La idea es armar un string de la siguiente manera 
      // FieldName IN ('...', '...',......)
      string clause = "";
      foreach (string key in _FiltroCañeros.Keys)
      {
        if (_FiltroCañeros[key])
        {
          clause += key + ", ";
        }
      }
      foreach (string key in _FiltroFrentes.Keys)
      {
        if (_FiltroFrentes[key])
        {
          clause += key + ", ";
        }
      }
      foreach (string key in _FiltroRegadores.Keys)
      {
        if (_FiltroRegadores[key])
        {
          clause += key + ", ";
        }
      }
      if (clause.Length > 2)
      {
        clause = " " + FieldName + " IN (" + clause.Substring(0, clause.Length - 2) + ")";
      }
      return clause;
    }

    public void AddDataProvider(string DataProviderName, SharpMap.Data.Providers.MemoryDataProviderBase provider)
    {
      if (Providers == null)
        Providers = new Dictionary<string, SharpMap.Data.Providers.MemoryDataProviderBase>();
      Providers[DataProviderName] = provider;
    }

    //Debo tener un dictionary que vincule el DataProvider con TransponderID del GPS
    protected System.Collections.Generic.Dictionary<string, string> GPSDataProviderName;
    protected System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.MemoryDataProviderBase> Providers;


    // Definicion de las propiedades.
    public DateTime? FechaInicioConsulta
    {
      get
      {
        return FechaInicioConsulta_;
      }
      set
      {
        FechaInicioConsulta_ = value;
      }
    }


    public DateTime? FechaFinConsulta
    {
      get
      {
        return FechaFinConsulta_;
      }
      set
      {
        FechaFinConsulta_ = value;
      }
    }

    public string IDElemento
    {
      get
      {
        return IDElemento_;
      }
      set
      {
        IDElemento_ = value;
      }
    }

    public string IDTransponder
    {
      get
      {
        return IDTransponder_;
      }
      set
      {
        IDTransponder_ = value;
      }
    }

    public int? Categoria
    {
      get
      {
        return Categoria_;
      }
      set
      {
        Categoria_ = value;
      }
    }

    private void LlenarLista(SharpMap.Data.Providers.IProvider iProvider, System.Collections.Generic.Dictionary<string, bool> Lista)
    {
      SharpMap.Data.FeatureDataSet fds = new SharpMap.Data.FeatureDataSet();
      iProvider.ExecuteIntersectionQuery(iProvider.GetExtents(), fds);
      foreach (System.Data.DataRow row in (fds.Tables[0] as System.Data.DataTable).Rows)
      {
        Lista[row["Transponder"].ToString()] = true;
      }
    }

    private void LlenarLista(System.Collections.Generic.Dictionary<string, bool> ListaOrigen, System.Collections.Generic.Dictionary<string, bool> Lista)
    {
      foreach (string s in ListaOrigen.Keys)
      {
        Lista[s] = ListaOrigen[s];
      }
    }

    public bool IsStarted
    {
      get
      {
        return _IsStarted;
      }
    }


    public int VelocidadMinima
    {
      get
      {
        return VelocidadMinima_;
      }
      set
      {
        VelocidadMinima_ = value;
      }
    }

    public int VelocidadMaxima
    {
      get
      {
        return VelocidadMaxima_;
      }
      set
      {
        VelocidadMaxima_ = value;
      }
    }

    public int QueryTimeOut
    {
      get
      {
        return QueryTimeOut_;
      }
      set
      {
        QueryTimeOut_ = value;
      }
    }

    public IntelliTrack.Client.Application.ReportForm Reporte
    {
      get
      {
        return _reporte;
      }
      set
      {
        _reporte = value;
      }
    }

    public IntelliTrack.Client.Application.ReportForm ReporteRegadores
    {
      get
      {
        return _reporteRegadores;
      }
      set
      {
        _reporteRegadores = value;
      }
    }


    public IntelliTrack.Client.Application.frmRawData RawDataForm
    {
      get
      {
        return _rawDataForm;
      }
      set
      {
        _rawDataForm = value;
      }
    }



    internal enum QUERYSTATUS
    {
      NONE,
      QUERYING,
      STOPPED,
      RUNNING
    }

    internal delegate void QueryPosition(int Position, System.DateTime DatePresented);
    internal delegate void QueryStatus(IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS CurrentStatus);

    public string Area_;
    public string Camino_;
    private bool FiltroPorVelocidad_;
    private bool GenerarReporte_;

    public string Punto_;
    public string Transponder_;
    public string Vehiculo_;

    internal event IntelliTrack.Client.Application.Dataset.DataRetriever.QueryPosition OnQueryPosition;
    internal event IntelliTrack.Client.Application.Dataset.DataRetriever.QueryStatus OnQueryStatus;

    public int AnimationSpeedMiliSeconds
    {
      get
      {
        return AnimationSpeedMiliSeconds_;
      }
      set
      {
        AnimationSpeedMiliSeconds_ = value;
        timer.Interval = (double)value;
      }
    }

    public string Area
    {
      get
      {
        return Area_;
      }
      set
      {
        Area_ = value;
      }
    }

    public string Camino
    {
      get
      {
        return Camino_;
      }
      set
      {
        Camino_ = value;
      }
    }

    public bool FiltroPorVelocidad
    {
      get
      {
        return FiltroPorVelocidad_;
      }
      set
      {
        FiltroPorVelocidad_ = value;
      }
    }

    public bool GenerarReporte
    {
      get
      {
        return GenerarReporte_;
      }
      set
      {
        GenerarReporte_ = value;
      }
    }

    public string Punto
    {
      get
      {
        return Punto_;
      }
      set
      {
        Punto_ = value;
      }
    }

    public string Transponder
    {
      get
      {
        return Transponder_;
      }
      set
      {
        Transponder_ = value;
      }
    }

    public string Vehiculo
    {
      get
      {
        return Vehiculo_;
      }
      set
      {
        Vehiculo_ = value;
      }
    }

    private string BuildStringMessage(System.Collections.Generic.Dictionary<string, object> ParsedData)
    {
      string s = "";
      foreach (System.Collections.Generic.KeyValuePair<string, object> pair in ParsedData)
      {
        s = s + pair.Value.ToString() + ",";
      }
      return s.Substring(0, s.Length - 1);
    }

    private void ClearProviderGPSs()
    {
      if (Providers != null)
      {
        foreach (System.Collections.Generic.KeyValuePair<string, SharpMap.Data.Providers.MemoryDataProviderBase> pair in Providers)
        {
          pair.Value.ClearAll();
        }
      }
    }

    private void EliminarElementoAnterior(System.Collections.Generic.Dictionary<string, object> ParsedData)
    {
      if (Providers != null)
      {
        foreach (System.Collections.Generic.KeyValuePair<string, SharpMap.Data.Providers.MemoryDataProviderBase> pair in Providers)
        {
          pair.Value.EliminarXORByTransponderIDVehicleID((string)ParsedData["IDTransponder"], (string)ParsedData["IDElemento"]);
        }
      }
    }

    private void ExecuteQuery(bool ShowWindow)
    {
      if (dsData != null)
        dsData = null;
      string s1 = " exec STP_OBTENER_MOVIMIENTOS @FechaInicio, @FechaFin, @VelocidadMinima, @VelocidadMaxima, @Categoria, @IDTransponder, @NumCamion, @PUNTO, @CAMINO, @AREA";
      string s2 = ValidacionSeguridad.Instance.GetApplicationConnectionString();
      System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(s2);
      sqlConnection.Open();
      System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(s1, sqlConnection);
      sqlCommand.CommandTimeout = QueryTimeOut_;
      sqlCommand.Parameters.AddWithValue("@FechaInicio", FechaInicioConsulta_);
      sqlCommand.Parameters.AddWithValue("@FechaFin", FechaFinConsulta_);
      if (FiltroPorVelocidad)
      {
        sqlCommand.Parameters.AddWithValue("@VelocidadMinima", VelocidadMinima);
        sqlCommand.Parameters.AddWithValue("@VelocidadMaxima", VelocidadMaxima);
      }
      else
      {
        sqlCommand.Parameters.AddWithValue("@VelocidadMinima", System.Data.SqlTypes.SqlDouble.Null);
        sqlCommand.Parameters.AddWithValue("@VelocidadMaxima", System.Data.SqlTypes.SqlDouble.Null);
      }
      sqlCommand.Parameters.AddWithValue("@Categoria", Categoria_.HasValue ? (short)Categoria_.Value : System.Data.SqlTypes.SqlInt16.Null);
      sqlCommand.Parameters.AddWithValue("@IDTransponder", (IDTransponder_ != null) && IDTransponder_ != "" ? IDTransponder_ : System.Data.SqlTypes.SqlString.Null);
      sqlCommand.Parameters.AddWithValue("@NumCamion", (Vehiculo_ != null) && Vehiculo_ != "" ? Vehiculo_ : System.Data.SqlTypes.SqlString.Null);
      sqlCommand.Parameters.AddWithValue("@PUNTO", (Punto_ != null) && Punto_ != "" ? Punto_ : System.Data.SqlTypes.SqlString.Null);
      sqlCommand.Parameters.AddWithValue("@CAMINO", (Camino_ != null) && Camino_ != "" ? Camino_ : System.Data.SqlTypes.SqlString.Null);
      sqlCommand.Parameters.AddWithValue("@AREA", (Area_ != null) && Area_ != "" ? Area_ : System.Data.SqlTypes.SqlString.Null);
      sqlCommand.CommandType = System.Data.CommandType.Text;
      System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(sqlCommand);
      dsData = new System.Data.DataSet();
      CurrentPointer = 0;
      FireQueryStatus(IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS.QUERYING);
      FireQueryPosition();
      try
      {
        if (!ShowWindow)
        {
          sqlDataAdapter.Fill(dsData);
        }
        else
        {
          IntelliTrack.Client.Application.WaitDialog waitDialog = new IntelliTrack.Client.Application.WaitDialog();
          waitDialog.da = sqlDataAdapter;
          waitDialog.ShowDialog();
          dsData = waitDialog.ds;
        }
      }
      catch (System.Data.SqlClient.SqlException)
      {
        if (System.Windows.Forms.MessageBox.Show("La obtenci\u00F3n de datos ha superado el tiempo m\u00E1ximo establecido. Restringa los par\u00E1metros o incremente el tiempo m\u00E1ximo de espera. Desea modificarlo ahora?", "Tiempo m\u00E1ximo", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
          Setup();
      }
      sqlConnection.Close();
      if (GenerarReporte)
      {
        if (_reporte != null)
          _reporte.ReloadData(dsData);
        if (_reporteRegadores != null)
        {
          IntelliTrack.Client.Application.Dataset.ReporteRegadoresDataset reporteRegadoresDataset = new IntelliTrack.Client.Application.Dataset.ReporteRegadoresDataset();
          reporteRegadoresDataset.Process(dsData);
          _reporteRegadores.ReloadData(reporteRegadoresDataset.DataSet, FechaInicioConsulta_.Value, FechaFinConsulta_.Value, (double)VelocidadMinima, (double)VelocidadMaxima);
        }
      }
      else if (_reporteRegadores != null)
      {
        _reporteRegadores.ReloadData(null);
      }
      if (_rawDataForm != null)
        _rawDataForm.DataSource = dsData;
      FireQueryStatus(IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS.STOPPED);
    }

    private void FireQueryPosition()
    {
      if (OnQueryPosition != null)
      {
        if (CurrentPointer == 0)
        {
          OnQueryPosition(CurrentPointer, System.DateTime.MinValue);
          return;
        }
        OnQueryPosition(CurrentPointer, (System.DateTime)dsData.Tables[0].Rows[CurrentPointer]["DIA_HORA"]);
      }
    }

    private void FireQueryStatus(IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS CurrentStatus)
    {
      if (OnQueryStatus != null)
        OnQueryStatus(CurrentStatus);
    }

    private SharpMap.Data.Providers.MemoryDataProviderBase GetDataProvider(string TransponderID)
    {
      SharpMap.Data.Providers.MemoryDataProviderBase memoryDataProviderBase2;

      if (Providers != null)
      {
        foreach (string prov in Providers.Keys)
        {
          SharpMap.Data.Providers.MemoryDataProviderBase memoryDataProviderBase1 = Providers[prov];
          if (memoryDataProviderBase1.ExistsTransponderID(TransponderID))
          {
            memoryDataProviderBase2 = memoryDataProviderBase1;
            return memoryDataProviderBase2;
          }
        }
      }
      return null;
    }

    private SharpMap.Data.Providers.MemoryDataProviderBase GetDataProvider(short IDCategoria)
    {
      SharpMap.Data.Providers.MemoryDataProviderBase memoryDataProviderBase;

      if (Providers != null)
      {
        foreach (System.Collections.Generic.KeyValuePair<string, SharpMap.Data.Providers.MemoryDataProviderBase> pair in Providers)
        {
          if (pair.Value.IDCategoria == IDCategoria)
          {
            memoryDataProviderBase = pair.Value;
            return memoryDataProviderBase;
          }
        }
      }
      return null;
    }


    internal int GetRecordCount()
    {
      if (dsData == null)
        return 0;
      if (dsData.Tables.Count == 0)
        return 0;
      return dsData.Tables[0].Rows.Count;
    }

    public void InitializeData()
    {
      AnimationSpeedMiliSeconds_ = 1000;
      System.DateTime dateTime = System.DateTime.Today;
      FechaInicioConsulta_ = new System.Nullable<System.DateTime>(dateTime.AddDays(-1.0));
      FechaFinConsulta_ = new System.Nullable<System.DateTime>(System.DateTime.Now);
    }

    public void Pause()
    {
      timer.Stop();
      _IsStarted = false;
      FireQueryStatus(IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS.STOPPED);
    }

    public void Rewind()
    {
      if (IsStarted)
        Stop();
      CurrentPointer = 0;
      FireQueryPosition();
    }

    internal void SetFilters(System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.IProvider> providers)
    {
      Providers = new System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.MemoryDataProviderBase>();
      foreach (System.Collections.Generic.KeyValuePair<string, SharpMap.Data.Providers.IProvider> pair in providers)
      {
        Providers[pair.Key] = pair.Value as SharpMap.Data.Providers.MemoryDataProviderBase;
      }
    }

    public void SetPosition(int NewPosition)
    {
      if (dsData == null)
        return;
      if (dsData.Tables.Count == 0)
        return;
      CurrentPointer = NewPosition > dsData.Tables[0].Rows.Count ? (NewPosition < 0 ? 0 : dsData.Tables[0].Rows.Count) : NewPosition;
      ClearProviderGPSs();
    }

    public void Setup()
    {
      IntelliTrack.Client.Application.dlgParametrosConsultaHistoricos dlgParametrosConsultaHistoricos = new IntelliTrack.Client.Application.dlgParametrosConsultaHistoricos();
      dlgParametrosConsultaHistoricos.FechaInicio = FechaInicioConsulta_.Value;;
      dlgParametrosConsultaHistoricos.FechaFin = FechaFinConsulta.Value;
      dlgParametrosConsultaHistoricos.VelocidadMinima = VelocidadMinima;
      dlgParametrosConsultaHistoricos.VelocidadMaxima = VelocidadMaxima;
      dlgParametrosConsultaHistoricos.CategoriaSelected = Categoria_.Value;
      dlgParametrosConsultaHistoricos.Vehiculo = Vehiculo;
      dlgParametrosConsultaHistoricos.Transponder = Transponder;
      dlgParametrosConsultaHistoricos.Punto = Punto;
      dlgParametrosConsultaHistoricos.Camino = Camino;
      dlgParametrosConsultaHistoricos.Area = Area;
      dlgParametrosConsultaHistoricos.GenerarReporte = GenerarReporte;
      dlgParametrosConsultaHistoricos.MuestrasPorSegundo = 1000 / AnimationSpeedMiliSeconds_;
      dlgParametrosConsultaHistoricos.QueryTimeOut = QueryTimeOut;
      dlgParametrosConsultaHistoricos.FiltroPorVelocidad = FiltroPorVelocidad;
      if (dlgParametrosConsultaHistoricos.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        FechaInicioConsulta = new System.Nullable<System.DateTime>(dlgParametrosConsultaHistoricos.FechaInicio);
        FechaFinConsulta = new System.Nullable<System.DateTime>(dlgParametrosConsultaHistoricos.FechaFin);
        VelocidadMinima = dlgParametrosConsultaHistoricos.VelocidadMinima;
        VelocidadMaxima = dlgParametrosConsultaHistoricos.VelocidadMaxima;
        Categoria = new System.Nullable<int>(dlgParametrosConsultaHistoricos.CategoriaSelected);
        Vehiculo = dlgParametrosConsultaHistoricos.Vehiculo;
        Transponder = dlgParametrosConsultaHistoricos.Transponder;
        Punto = dlgParametrosConsultaHistoricos.Punto;
        Camino = dlgParametrosConsultaHistoricos.Camino;
        Area = dlgParametrosConsultaHistoricos.Area;
        AnimationSpeedMiliSeconds = 1000 / dlgParametrosConsultaHistoricos.MuestrasPorSegundo;
        QueryTimeOut = dlgParametrosConsultaHistoricos.QueryTimeOut;
        GenerarReporte = dlgParametrosConsultaHistoricos.GenerarReporte;
        FiltroPorVelocidad = dlgParametrosConsultaHistoricos.FiltroPorVelocidad;
        ClearProviderGPSs();
        ExecuteQuery(true);
      }
    }

    public void Start()
    {
      try
      {
        timer.Enabled = true;
        timer.Interval = (double)AnimationSpeedMiliSeconds_ * 1.0;
        timer.Start();
        _IsStarted = true;
        FireQueryStatus(IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS.RUNNING);
      }
      catch (System.Exception)
      {
        _IsStarted = false;
      }
    }

    public void Stop()
    {
      timer.Stop();
      CurrentPointer = 0;
      _IsStarted = false;
      FireQueryStatus(IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS.STOPPED);
      FireQueryPosition();
    }


  } // class DataRetriever

}
