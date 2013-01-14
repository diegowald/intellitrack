using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Client.Application.Imaging
{
  public class ComplexMap
  {
    protected UDP.UDPHandler udpHandler;

    protected SharpMap.Map _BaseMap;
    protected SharpMap.Map _OverviewMap;
    protected SharpMap.Map _RealTimeMap;

    private System.Drawing.Image _baseImage;
    private System.Drawing.Image _OverviewImage;
    private System.Drawing.Image _RealTimeImage;

    private System.Drawing.Size _MapSize;
    private System.Drawing.Size _OverviewSize;

    private SharpMap.Geometries.BoundingBox _MapArea;

    private bool BaseMapRedrawn;
    private bool OverviewMapRedrawn;
    private bool RealTimeMapRedrawn;

    private System.Collections.Generic.Dictionary<string, SharpMap.Layers.ILayer> Layers_;
    private System.Collections.Generic.List<string> RealTimeLayers_;

    private System.Collections.Generic.List<frmBaseDockingForm> forms_;

    private SharpMap.Geometries.Point CoordinateToLaunch;

    System.Timers.Timer timer;
    System.Timers.Timer TimerReloadFromDB;

    int RefreshIntevalMinutes;
    //private static IntelliTrack.Service.Engine.ServerEngine engine;

    private volatile bool _MapLoaded;
    public ComplexMap()
    {
      try
      {
        //engine = new IntelliTrack.Service.Engine.ServerEngine();
        //engine.Init();
        _MapSize.Width = 300;
        _MapSize.Height = 300;
        _OverviewSize.Width = 50;
        _OverviewSize.Height = 50;
        _BaseMap = new SharpMap.Map(_MapSize);
        _OverviewMap = new SharpMap.Map(_OverviewSize);
        _RealTimeMap = new SharpMap.Map(_MapSize);
        BaseMapRedrawn = false;
        RealTimeMapRedrawn = false;
        
        udpHandler = new IntelliTrack.Client.Application.UDP.UDPHandler();
        udpHandler.UDPArrivedData += new IntelliTrack.Client.Application.UDP.UDPHandler.UDPDataArrived(UDPArrivedData);
        
        Layers_ = new Dictionary<string, SharpMap.Layers.ILayer>();
        RealTimeLayers_ = new List<string>();
        _MapLoaded = false;
        //System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(LoadInformation));
        forms_ = new List<frmBaseDockingForm>();
        //t.Start();
        LoadInformation();
        timer = new System.Timers.Timer(10000);
        /*timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);*/

        double IntervaloMilisegundosReloadFromDB = ObtenerIntervaloReloadDB();
        if (IntervaloMilisegundosReloadFromDB > 0)
        {
          TimerReloadFromDB = new System.Timers.Timer(IntervaloMilisegundosReloadFromDB);
          TimerReloadFromDB.Elapsed += new System.Timers.ElapsedEventHandler(TimerReloadFromDB_Elapsed);
        }


        //timer.Enabled = true;
        //engine.Start();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        System.Diagnostics.Debug.WriteLine(ex.StackTrace);
      }
    }

    void TimerReloadFromDB_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      // Para cada realtimelayer, hago un reload de sus datos internos.
      foreach (string layerName in RealTimeLayers_)
      {
        SharpMap.Layers.VectorLayer layer = Layers_[layerName] as SharpMap.Layers.VectorLayer;
        if (layer != null)
        {
          SharpMap.Data.Providers.MemoryDataProviderBase provider = layer.DataSource as SharpMap.Data.Providers.MemoryDataProviderBase;
          provider.ReloadFromDB(RefreshIntevalMinutes);
        }
      }
      IntelliTrack.Data.TractoresDataPoint.RefreshTagsFromDatabase(RefreshIntevalMinutes);
      RefreshViews();
    }

    private double ObtenerIntervaloReloadDB()
    {
      int Minutos = 0;
      string Intervalo = System.Configuration.ConfigurationManager.AppSettings["MinutesBetweenDBRefresh"];

      if (!int.TryParse(Intervalo, out Minutos))
      {
        Minutos = 10;
      }
      else
      {
        // Valido que sea un nro positivo
        if (Minutos < 0)
          Minutos = 10;
        // Si es cero esta funcion se deshabilita.
      }
      RefreshIntevalMinutes=Minutos;
      return Minutos * 60.0 * 1000.0;
    }

    public void StartTimer()
    {
      if (!timer.Enabled)
        timer.Enabled = true;

      if ((TimerReloadFromDB != null) && (!TimerReloadFromDB.Enabled))
        TimerReloadFromDB.Enabled = true;

      if (udpHandler != null)
        udpHandler.Start();
    }

    void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("ComplexMap.Timer_Elapsedf");
      if (_MapLoaded)
      {
        this.RefreshViews();
      }
    }

    public void Stop()
    {
      if ((timer != null) && (timer.Enabled))
        timer.Enabled = false;

      if ((TimerReloadFromDB != null) && (TimerReloadFromDB.Enabled))
        TimerReloadFromDB.Enabled = false;

      if (udpHandler != null)
        udpHandler.Stop();
    }

    private void LoadInformation()
    {
      LoadLayers();
      LoadBaseMap();
      LoadRealTimeMap();
      LoadOverviewMap();
      _MapLoaded = true;
      RefreshViews();
      //((IntelliTrack.Client.HistoricDBLayer.GPSDataSource)((SharpMap.Layers.VectorLayer)Layers_["GPSData"]).DataSource).EnableRefresh = true;
      //engine.Start();
      //udpHandler.Start();
    }

    /**
     *  Esta funcion recarga el contenido del layer desde la base de datos.
     **/
    public void ReloadDatabaseLayerInformation()
    {
      foreach (string layerName in RealTimeLayers_)
      {
        if (RealTimeLayers_.Contains(layerName))
        {
          ((Layers_[layerName] as SharpMap.Layers.VectorLayer).DataSource as SharpMap.Data.Providers.MemoryDataProviderBase).LoadFromDB();
        }
      }
    }

    public void ReloadDatabaseLayers()
    {
      // Debo buscar las distintas categorias, obteniendolas de la tabla Categorias
      string AppDBConnection = ValidacionSeguridad.Instance.GetApplicationConnectionString();
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppDBConnection);

      string sql = "SELECT * from CATEGORIAS Where CAT_ID <> 99 ";
      switch (ValidacionSeguridad.Instance.CategoriaUsuario)
      {
        case 99:
          break;
        default:
          sql += " AND CAT_ID IN (3, ";
          sql += ValidacionSeguridad.Instance.CategoriaUsuario.ToString();
          sql += ")";
          break;
      }

      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sql, conn);

      System.Data.DataSet ds = new System.Data.DataSet();
      da.Fill(ds);

      // Creo un diccionario con los layers de tiempo real que van a permanecer (true) o se borran (false)
      System.Collections.Generic.Dictionary<string, bool> NewRealTimeLayers = new Dictionary<string, bool>();
      foreach (string layerName in RealTimeLayers_)
      {
        // En un principio todos se borraran
        NewRealTimeLayers.Add(layerName, false);
      }


      foreach (System.Data.DataRow row in ds.Tables[0].Rows)
      {
        SharpMap.Layers.VectorLayer layer;
        SharpMap.Layers.LabelLayer labelLayer;
        string LayerName = row["CAT_DESCRIPCION"].ToString().Trim();
        if (RealTimeLayers_.Contains(LayerName))
        {
          NewRealTimeLayers[LayerName] = true;
          NewRealTimeLayers["label" + LayerName] = true;
        }
        else
        {
          // Es un layer nuevo, lo creo
          CreateLayer(row, out layer, out labelLayer);
          if ((labelLayer != null) && (layer != null))
          {
            Layers_[layer.LayerName] = layer;
            Layers_[labelLayer.LayerName] = labelLayer;
            RealTimeLayers_.Add(layer.LayerName);
            RealTimeLayers_.Add(labelLayer.LayerName);
          }
        }
      }

      // Ahora recorro el dictionario y borro los layers que estan en false
      foreach (string layerName in NewRealTimeLayers.Keys)
      {
        if (!NewRealTimeLayers[layerName])
        {
          // aca lo debo borrar
          Layers_.Remove(layerName);
          Layers_.Remove("label" + layerName);
          RealTimeLayers_.Remove(layerName);
          RealTimeLayers_.Remove("label" + layerName);
        }
      }

      ReloadRealTimeMap();
    }

    /*private void LoadDatabaseLayers()
    {
      // Debo buscar las distintas categorias, obteniendolas de la tabla Categorias
      string AppDBConnection = ValidacionSeguridad.Instance.GetApplicationConnectionString();
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppDBConnection);

      string sql = "SELECT * from CATEGORIAS Where CAT_ID <> 99 ";
      switch (ValidacionSeguridad.Instance.CategoriaUsuario)
      {
        case 99:
          break;
        default:
          sql += " AND CAT_ID IN (3, ";
          sql += ValidacionSeguridad.Instance.CategoriaUsuario.ToString();
          sql += ")";
          break;
      }

      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sql, conn);

      System.Data.DataSet ds = new System.Data.DataSet();
      da.Fill(ds);

      SharpMap.Layers.VectorLayer layer;
      SharpMap.Layers.LabelLayer labelLayer;
      foreach (System.Data.DataRow row in ds.Tables[0].Rows)
      {
        CreateLayer(row, out layer, out labelLayer);
        if ((labelLayer != null) && (layer != null))
        {
          Layers_[layer.LayerName] = layer;
          Layers_[labelLayer.LayerName] = labelLayer;
          RealTimeLayers_.Add(layer.LayerName);
          RealTimeLayers_.Add(labelLayer.LayerName);
        }
      }
    }*/

    private void LoadDatabaseLayers()
    {
      SharpMap.Layers.LabelLayer labelLayer;
      SharpMap.Layers.VectorLayer vectorLayer;

      string s1 = ValidacionSeguridad.Instance.GetApplicationConnectionString();
      System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(s1);
      string s2 = "SELECT * from CATEGORIAS Where CAT_ID <> 99 ";
      int i1 = ValidacionSeguridad.Instance.CategoriaUsuario;
      if (i1 != 99)
      {
        s2 += " AND CAT_ID IN (3, ";
        int i2 = ValidacionSeguridad.Instance.CategoriaUsuario;
        s2 += i2.ToString();
        s2 += ")";
      }
      System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(s2, sqlConnection);
      System.Data.DataSet dataSet = new System.Data.DataSet();
      sqlDataAdapter.Fill(dataSet);
      foreach (System.Data.DataRow dataRow in dataSet.Tables[0].Rows)
      {
        CreateLayer(dataRow, out vectorLayer, out labelLayer);
        if ((labelLayer != null) && (vectorLayer != null))
        {
          Layers_[vectorLayer.LayerName] = vectorLayer;
          Layers_[labelLayer.LayerName] = labelLayer;
          RealTimeLayers_.Add(vectorLayer.LayerName);
          RealTimeLayers_.Add(labelLayer.LayerName);
        }
      }
    }


    private string GetSymbolName(string categoryName)
    {
      return "Simbolo_" + categoryName;
    }

    private void CreateLayer(System.Data.DataRow row, out SharpMap.Layers.VectorLayer layer, out SharpMap.Layers.LabelLayer labelLayer)
    {
      layer = new SharpMap.Layers.VectorLayer(row["CAT_DESCRIPCION"].ToString().Trim());
      //labelLayer = new SharpMap.Layers.HtmlLabelLayer("label"+row["CAT_DESCRIPCION"].ToString().Trim());
      labelLayer = new SharpMap.Layers.LabelLayer("label" + row["CAT_DESCRIPCION"].ToString().Trim());
      string symbolName = GetSymbolName(layer.LayerName);
      switch (layer.LayerName)
      {
        case "Transporte Caña":
        case "Cañeros":
          {
            SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.TransporteDataPoint> dsource;
            dsource = new SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.TransporteDataPoint>();
            dsource.LayerName = layer.LayerName;
            layer.DataSource = dsource;

            IntelliTrack.Data.TransporteDataPoint.OnEntradaAlFrente += new IntelliTrack.Data.TransporteDataPoint.EntradaAFrente(TransporteDataPoint_OnEntradaAlFrente);
            IntelliTrack.Data.TransporteDataPoint.OnSalidaAlFrente += new IntelliTrack.Data.TransporteDataPoint.SalidaDeFrente(TransporteDataPoint_OnSalidaAlFrente);
            //layer.CoordinateTransformation = Transformation;
            dsource.LoadFromDB();

            if (System.Configuration.ConfigurationManager.AppSettings[GetSymbolName("Cañeros")] == "NSEO")
              layer.Theme = new SharpMap.Rendering.Thematics.CustomTheme(TransporteCañaStyle);
            else
              layer.Style = LoadStyle(GetFileNameFromConfig(GetSymbolName("Cañeros")));

            if (udpHandler != null)
              udpHandler.AddDataProvider(layer.LayerName, dsource);
            SetLabelStyle(labelLayer);
            labelLayer.LabelStringDelegate = GetLabelMethodTransporteCaña;
            labelLayer.DataSource = dsource;
            break;
          }
        case "Regadores":
          {
            SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.RegadorDataPoint> dsource;
            dsource = new SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.RegadorDataPoint>();
            dsource.LayerName = layer.LayerName;
            layer.DataSource = dsource;
            //layer.CoordinateTransformation = Transformation;
            dsource.LoadFromDB();
            if (System.Configuration.ConfigurationManager.AppSettings[symbolName] == "NSEO")
              layer.Theme = new SharpMap.Rendering.Thematics.CustomTheme(RegadoresStyle);
            else
              layer.Style = LoadStyle(GetFileNameFromConfig(symbolName));

            if (udpHandler != null)
              udpHandler.AddDataProvider(layer.LayerName, dsource);
            SetLabelStyle(labelLayer);
            labelLayer.LabelStringDelegate = GetLabelMethodRegadores;
            labelLayer.DataSource = dsource;
            break;
          }
        case "Frentes":
          {
            SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.FrenteDataPoint> dsource;
            dsource = new SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.FrenteDataPoint>();
            dsource.LayerName = layer.LayerName;
            layer.DataSource = dsource;
            //layer.CoordinateTransformation = Transformation;

            if (System.Configuration.ConfigurationManager.AppSettings[symbolName] == "NSEO")
              layer.Theme = new SharpMap.Rendering.Thematics.CustomTheme(FrentesStyle);
            else
              layer.Style = LoadStyle(GetFileNameFromConfig(symbolName));

            layer.Style.Line.Color = System.Drawing.Color.DeepSkyBlue;
            layer.Style.Fill = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(30, System.Drawing.Color.ForestGreen));
            dsource.LoadFromDB();
            dsource.LoadCoordinatesFromDB();
            if (udpHandler != null)
              udpHandler.AddDataProvider(layer.LayerName, dsource);
            SetLabelStyle(labelLayer);
            labelLayer.LabelStringDelegate = GetLabelMethodFrentes;
            labelLayer.DataSource = dsource;
            break;
          }
        case "Tractores":
          {
            SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.TractoresDataPoint> dsource;
            dsource = new SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.TractoresDataPoint>();
            dsource.LayerName = layer.LayerName;
            layer.DataSource = dsource;
            //layer.CoordinateTransformation = Transformation;
            dsource.LoadFromDB();
            if (System.Configuration.ConfigurationManager.AppSettings[symbolName] == "NSEO")
              layer.Theme = new SharpMap.Rendering.Thematics.CustomTheme(TractoresStyle);
            else
              layer.Style = LoadStyle(GetFileNameFromConfig(symbolName));

            if (udpHandler != null)
              udpHandler.AddDataProvider(layer.LayerName, dsource);
            SetLabelStyle(labelLayer);
            labelLayer.LabelStringDelegate = GetLabelMethodTractores;
            labelLayer.DataSource = dsource;
            break;
          }
        case "Cosechadoras":
          {
            SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.CosechadorasDataPoint> dsource;
            dsource = new SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.CosechadorasDataPoint>();
            dsource.LayerName = layer.LayerName;
            layer.DataSource = dsource;
            //layer.CoordinateTransformation = Transformation;
            dsource.LoadFromDB();
            if (System.Configuration.ConfigurationManager.AppSettings[symbolName] == "NSEO")
              layer.Theme = new SharpMap.Rendering.Thematics.CustomTheme(CosechadorasStyle);
            else
              layer.Style = LoadStyle(GetFileNameFromConfig(symbolName));

            if (udpHandler != null)
              udpHandler.AddDataProvider(layer.LayerName, dsource);
            SetLabelStyle(labelLayer);
            labelLayer.LabelStringDelegate = GetLabelMethodCosechadoras;
            labelLayer.DataSource = dsource;
            break;
          }
        default:
          {
            SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.GenericDataPoint> dsource;
            dsource = new SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.GenericDataPoint>();
            dsource.LayerName = layer.LayerName;
            layer.DataSource = dsource;
            //layer.CoordinateTransformation = Transformation;
            dsource.LoadFromDB();
            if (System.Configuration.ConfigurationManager.AppSettings[symbolName] == "NSEO")
              layer.Theme = new SharpMap.Rendering.Thematics.CustomTheme(GenericoStyle);
            else
              layer.Style = LoadStyle(GetFileNameFromConfig(symbolName));

            if (udpHandler != null)
              udpHandler.AddDataProvider(layer.LayerName, dsource);
            SetLabelStyle(labelLayer);

            labelLayer.LabelStringDelegate = GetLabelMethodGenerico;
            labelLayer.DataSource = dsource;
            break;
          }
      }
    }

    private void SetLabelStyle(SharpMap.Layers.LabelLayer labelLayer)
    {
      labelLayer.Style.VerticalAlignment = SharpMap.Styles.LabelStyle.VerticalAlignmentEnum.Bottom;
      labelLayer.Style.HorizontalAlignment = SharpMap.Styles.LabelStyle.HorizontalAlignmentEnum.Center;
      labelLayer.Style.CollisionDetection = true;
      labelLayer.Style.Offset = new System.Drawing.PointF(0, 10);
      labelLayer.Style.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
      //labelLayer.Style.Halo = System.Drawing.Pens.Yellow;
    }

    void TransporteDataPoint_OnSalidaAlFrente(string TagEquipo, string CodFrente, string CodTransporteCamion)
    {
      System.Diagnostics.Debug.WriteLine("Salida: Equipo: " + TagEquipo + " Frente: " + CodFrente + " Camion: " + CodTransporteCamion);
      if (!_MapLoaded)
        return;

      if (!ValidacionSeguridad.Instance.Write2DB)
        return;

      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ValidacionSeguridad.Instance.GetApplicationConnectionString());
      try
      {
        conn.Open();

        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("STP_MOVIMIENTOS_FRENTES", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@TAGEQUIPO", TagEquipo);
        cmd.Parameters.AddWithValue("@CODTFRENTE", CodFrente);
        cmd.Parameters.AddWithValue("@CODTCAMION", CodTransporteCamion);
        cmd.Parameters.AddWithValue("@SENTIDO", "S");


        cmd.ExecuteNonQuery();
        conn.Close();
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
      }
      finally
      {
        conn.Close();
      }
    }


    void TransporteDataPoint_OnEntradaAlFrente(string TagEquipo, string CodFrente, string CodTransporteCamion)
    {
      System.Diagnostics.Debug.WriteLine("Entrada: Equipo: " + TagEquipo + " Frente: " + CodFrente + " Camion: " + CodTransporteCamion);
      if (!_MapLoaded)
        return;

      if (!ValidacionSeguridad.Instance.Write2DB)
        return;

      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ValidacionSeguridad.Instance.GetApplicationConnectionString());
      try
      {
        conn.Open();

        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("STP_MOVIMIENTOS_FRENTES", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@TAGEQUIPO", TagEquipo);
        cmd.Parameters.AddWithValue("@CODTFRENTE", CodFrente);
        cmd.Parameters.AddWithValue("@CODTCAMION", CodTransporteCamion);
        cmd.Parameters.AddWithValue("@SENTIDO", "E");


        cmd.ExecuteNonQuery();
        conn.Close();
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
      }
      finally
      {
        conn.Close();
      }
    }


    /// <summary>
    /// Creates a UTM projection for the northern
    /// hemisphere based on the WGS84 datum
    /// </summary>
    /// <param name="utmZone">Utm Zone</param>
    /// <returns>Projection</returns>
    private SharpMap.CoordinateSystems.IProjectedCoordinateSystem CreateUtmProjection(int utmZone)
    {
      SharpMap.CoordinateSystems.CoordinateSystemFactory cFac =
            new SharpMap.CoordinateSystems.CoordinateSystemFactory();
      //Create geographic coordinate system based on the WGS84 datum
      SharpMap.CoordinateSystems.IEllipsoid ellipsoid = cFac.CreateFlattenedSphere("WGS 84",
                 6378137, 298.257223563, SharpMap.CoordinateSystems.LinearUnit.Metre);
      SharpMap.CoordinateSystems.IHorizontalDatum datum = cFac.CreateHorizontalDatum("WGS_1984",
                           SharpMap.CoordinateSystems.DatumType.HD_Geocentric, ellipsoid, null);
      SharpMap.CoordinateSystems.IGeographicCoordinateSystem gcs = cFac.CreateGeographicCoordinateSystem(
                           "WGS 84", SharpMap.CoordinateSystems.AngularUnit.Degrees, datum,
                           SharpMap.CoordinateSystems.PrimeMeridian.Greenwich,
                           new SharpMap.CoordinateSystems.AxisInfo("Lon", SharpMap.CoordinateSystems.AxisOrientationEnum.East),
                           new SharpMap.CoordinateSystems.AxisInfo("Lat", SharpMap.CoordinateSystems.AxisOrientationEnum.North));
      //Create UTM projection
      List<SharpMap.CoordinateSystems.ProjectionParameter> parameters = new List<SharpMap.CoordinateSystems.ProjectionParameter>(5);
      parameters.Add(new SharpMap.CoordinateSystems.ProjectionParameter("latitude_of_origin", 0));
      parameters.Add(new SharpMap.CoordinateSystems.ProjectionParameter("central_meridian", -183 + 6 * utmZone));
      parameters.Add(new SharpMap.CoordinateSystems.ProjectionParameter("scale_factor", 0.9996));
      parameters.Add(new SharpMap.CoordinateSystems.ProjectionParameter("false_easting", 500000));
      parameters.Add(new SharpMap.CoordinateSystems.ProjectionParameter("false_northing", 0.0));
      SharpMap.CoordinateSystems.IProjection projection = cFac.CreateProjection(
      "Transverse Mercator", "Transverse_Mercator", parameters);
      return cFac.CreateProjectedCoordinateSystem(
               "WGS 84 / UTM zone " + utmZone.ToString() + "N", gcs,
      projection, SharpMap.CoordinateSystems.LinearUnit.Metre,
      new SharpMap.CoordinateSystems.AxisInfo("East", SharpMap.CoordinateSystems.AxisOrientationEnum.East),
      new SharpMap.CoordinateSystems.AxisInfo("North", SharpMap.CoordinateSystems.AxisOrientationEnum.North));
    }

    private SharpMap.CoordinateSystems.Transformations.ICoordinateTransformation Transformation_ = null;
    private SharpMap.CoordinateSystems.Transformations.ICoordinateTransformation Transformation1
    {
      get
      {
        if (Transformation_ == null)
        {
          //Create zone UTM 32N projection
          SharpMap.CoordinateSystems.IProjectedCoordinateSystem utmProj = CreateUtmProjection(32);
          //Create geographic coordinate system (lets just reuse the CS from the projection)
          SharpMap.CoordinateSystems.IGeographicCoordinateSystem geoCS = utmProj.GeographicCoordinateSystem;
          //Create transformation
          SharpMap.CoordinateSystems.Transformations.CoordinateTransformationFactory ctFac =
              new SharpMap.CoordinateSystems.Transformations.CoordinateTransformationFactory();
          SharpMap.CoordinateSystems.Transformations.ICoordinateTransformation transform =
             ctFac.CreateFromCoordinateSystems(geoCS, utmProj);
          //Apply transformation to a vectorlayer
          Transformation_ = transform;
        }
        return Transformation_;
      }
    }


    private void LoadLayers()
    {
      SharpMap.Layers.VectorLayer layer;

      LoadDatabaseLayers();
      #region Vector Layers
      // Vector Layers
      layer = new SharpMap.Layers.VectorLayer("caminos");
      layer.DataSource = new SharpMap.Data.Providers.ShapeFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\mapas\\caminos.shp", true);
      layer.Style.Line = System.Drawing.Pens.Black;
      layer.Theme = new SharpMap.Rendering.Thematics.CustomTheme(CaminosStyle);
      layer.LayerRendered += layer_LayerRendered;
      //layer.CoordinateTransformation = Transformation;
      Layers_["caminos"] = layer;

      layer = new SharpMap.Layers.VectorLayer("canales");
      layer.DataSource = new SharpMap.Data.Providers.ShapeFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\mapas\\canales.shp", true);
      layer.Style.Line = System.Drawing.Pens.Blue;
      layer.LayerRendered += layer_LayerRendered;
      //layer.CoordinateTransformation = Transformation;
      Layers_["canales"] = layer;

      layer = new SharpMap.Layers.VectorLayer("drenajes");
      layer.DataSource = new SharpMap.Data.Providers.ShapeFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\mapas\\drenajes.shp", true);
      layer.Style.Line = System.Drawing.Pens.BlueViolet;
      layer.LayerRendered += layer_LayerRendered;
      //layer.CoordinateTransformation = Transformation;
      Layers_["drenajes"] = layer;

      layer = new SharpMap.Layers.VectorLayer("jujuy");
      layer.DataSource = new SharpMap.Data.Providers.ShapeFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\mapas\\jujuy.shp", true);
      layer.Style.Fill = System.Drawing.Brushes.LightYellow;
      layer.LayerRendered += layer_LayerRendered;
      //layer.CoordinateTransformation = Transformation;
      Layers_["jujuy"] = layer;

      layer = new SharpMap.Layers.VectorLayer("rios");
      layer.DataSource = new SharpMap.Data.Providers.ShapeFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\mapas\\rios.shp", true);
      layer.Style.Line.Color = System.Drawing.Color.Turquoise;
      layer.LayerRendered += layer_LayerRendered;
      //layer.CoordinateTransformation = Transformation;
      Layers_["rios"] = layer;

      layer = new SharpMap.Layers.VectorLayer("salta");
      layer.DataSource = new SharpMap.Data.Providers.ShapeFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\mapas\\salta.shp", true);
      layer.Style.Fill = System.Drawing.Brushes.LightYellow;
      layer.LayerRendered += layer_LayerRendered;
      //layer.CoordinateTransformation = Transformation;
      Layers_["salta"] = layer;

      layer = new SharpMap.Layers.VectorLayer("Referencias");
      layer.DataSource = new SharpMap.Data.Providers.ShapeFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\mapas\\Referencias.shp", true);
      layer.Style.Fill = System.Drawing.Brushes.LightYellow;
      layer.Theme = new SharpMap.Rendering.Thematics.CustomTheme(ReferenciasStyle);
      layer.LayerRendered += layer_LayerRendered;
      //layer.CoordinateTransformation = Transformation;
      Layers_["Referencias"] = layer;

      #endregion
      #region Label Layers
      // Vector Layers
      SharpMap.Layers.LabelLayer label = new SharpMap.Layers.LabelLayer("LabelCaminos");
      label.DataSource = (Layers_["caminos"] as SharpMap.Layers.VectorLayer).DataSource;
      label.LayerRendered += layer_LayerRendered;
      label.LabelStringDelegate = GetLabelMethodCaminos;
      label.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
      label.Style.Font = new System.Drawing.Font("Arial", 8);
      label.Style.ForeColor = System.Drawing.Color.DarkBlue;
      //layer.CoordinateTransformation = Transformation;
      Layers_["LabelCaminos"] = label;

      label = new SharpMap.Layers.LabelLayer("LabelReferencias");
      //label = new SharpMap.Layers.LabelLayer("LabelReferencias");
      label.DataSource = (Layers_["Referencias"] as SharpMap.Layers.VectorLayer).DataSource;
      label.LayerRendered += layer_LayerRendered;
      label.LabelStringDelegate = GetLabelMethodReferencias;
      label.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
      label.Style.Font = new System.Drawing.Font("Arial", 8);
      label.Style.ForeColor = System.Drawing.Color.Crimson;
      //label.LabelColumn = "TYPE";
      Layers_["LabelReferencias"] = label;

      #endregion

      //LoadRasterLayers();
    }

    /*private void LoadRasterLayers()
    {
      SharpMap.Layers.GdalRasterLayer layer = new SharpMap.Layers.GdalRasterLayer("Satelital", @"C:\data\bluemarble.ecw");
      Layers_["Satelital"] = label;
    }*/

    void ComplexMap_OnDirty()
    {
      RealTimeMapRedrawn = false;
      RefreshViews();
    }


    void ComplexMap_OnReloadingData()
    {
      RealTimeMapRedrawn = false;
      RefreshViews();
    }


    public delegate void MapRendering();
    public event MapRendering OnMapRendering;

    public delegate void MapRendered();
    public event MapRendered OnMapRendered;

    public delegate void OverviewMapRendering();
    public event OverviewMapRendering OnOverviewMapRendering;

    public delegate void OverviewMapRendered();
    public event OverviewMapRendered OnOverviewMapRendered;

    public delegate void LayerRenderedHandler(string Map, string Layer);
    public event LayerRenderedHandler OnLayerRendered;


    private string _RenderingMap = "";
    void layer_LayerRendered(SharpMap.Layers.Layer layer, System.Drawing.Graphics g)
    {
      if (OnLayerRendered != null)
        OnLayerRendered(_RenderingMap, layer.LayerName);
    }

    private void ReloadRealTimeMap()
    {
      _RealTimeMap.Layers.Clear();
      foreach (string layername in RealTimeLayers_)
      {
        _RealTimeMap.Layers.Add(Layers_[layername]);
      }
    }

    private void LoadRealTimeMap()
    {
      ReloadRealTimeMap();
      _RealTimeMap.BackColor = System.Drawing.Color.Transparent;
      _RealTimeMap.ZoomToBox(Layers_["caminos"].Envelope);
    }

    private void LoadBaseMap()
    {
      _BaseMap.Layers.Add(Layers_["jujuy"]);
      _BaseMap.Layers.Add(Layers_["salta"]);
      _BaseMap.Layers.Add(Layers_["rios"]);
      _BaseMap.Layers.Add(Layers_["canales"]);
      _BaseMap.Layers.Add(Layers_["drenajes"]);
      _BaseMap.Layers.Add(Layers_["caminos"]);
      _BaseMap.Layers.Add(Layers_["Referencias"]);
      _BaseMap.Layers.Add(Layers_["LabelCaminos"]);
      _BaseMap.Layers.Add(Layers_["LabelReferencias"]);
      //_BaseMap.Layers.Add(Layers_["referencias"]);
      // Realtime layer
      //foreach (IntelliTrack.Service.Writer.IWriter writer in engine.Writers)
      //{
      //  if (writer is IntelliTrack.GIS.GPSLayer.AbstractRealTimeDataSource)
      //  {
      //    _BaseMap.Layers.Add(Layers_[writer.WriterName]);
      //    break;
      //  }

      //  if (writer is IntelliTrack.GIS.GPSLayer.InMemoryDataSource)
      //  {
      //    _BaseMap.Layers.Add(Layers_[writer.WriterName]);
      //    break;
      //  }
      //}

      _BaseMap.BackColor = System.Drawing.Color.Tomato;
      _BaseMap.ZoomToBox(Layers_["caminos"].Envelope);


    }

    private void LoadOverviewMap()
    {
      _OverviewMap.Layers.Add(Layers_["jujuy"]);
      _OverviewMap.Layers.Add(Layers_["salta"]);
      _OverviewMap.Layers.Add(Layers_["caminos"]);
      _OverviewMap.BackColor = System.Drawing.Color.LightGray;
      _OverviewMap.ZoomToExtents();
    }

    public System.Drawing.Size MapSize
    {
      get
      {
        return _MapSize;
      }
      set
      {
        _MapSize = value;
        _BaseMap.Size = _MapSize;
        _RealTimeMap.Size = _MapSize;
        _baseImage = null;
        _RealTimeImage = null;
        BaseMapRedrawn = false;
        OverviewMapRedrawn = false;
        RealTimeMapRedrawn = false;
        _MapArea = _BaseMap.Envelope;

        if (!value.IsEmpty)
          RefreshViews();
      }
    }

    public System.Drawing.Size OverviewSize
    {
      get
      {
        return _OverviewSize;
      }
      set
      {
        _OverviewSize = value;
        _OverviewMap.Size = value;

        if (_MapLoaded)
          _OverviewMap.ZoomToBox(_OverviewMap.GetExtents());
        OverviewMapRedrawn = false;
      }
    }

    public System.Drawing.Image GetOverViewMap()
    {
      if (!_MapLoaded)
        return null;

      if (OnOverviewMapRendering != null)
        OnOverviewMapRendering();

      _RenderingMap = "Overview Map";

      if ((MapSize.Width == 0) || (MapSize.Height == 0))
        return null;

      System.Drawing.Image img = new System.Drawing.Bitmap(MapSize.Width, MapSize.Height);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);

      try
      {
        if (!OverviewMapRedrawn)
        {
          _OverviewImage = _OverviewMap.GetMap();
          OverviewMapRedrawn = true;
        }

        g.PageUnit = System.Drawing.GraphicsUnit.Pixel;
        if (_baseImage != null)
          g.DrawImage(_OverviewImage, 0, 0);

        System.Drawing.PointF topleft = _OverviewMap.WorldToImage(new SharpMap.Geometries.Point(_MapArea.Left, _MapArea.Top));
        System.Drawing.PointF bottomright = _OverviewMap.WorldToImage(new SharpMap.Geometries.Point(_MapArea.Right, _MapArea.Bottom));

        int width = (int)(bottomright.X - topleft.X);
        int height = (int)(bottomright.Y - topleft.Y);
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle((int)topleft.X, (int)topleft.Y, width, height);

        g.DrawRectangle(System.Drawing.Pens.Red, rect);
      }
      catch (InvalidOperationException ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        g.DrawLine(System.Drawing.Pens.Aqua, 0, 0, 100, 100);
        g.DrawString(ex.Message,
          new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
          System.Drawing.Brushes.Tomato,
          new System.Drawing.PointF(100, 100));
      }
      g.Dispose();
      _RenderingMap = "";
      if (OnOverviewMapRendered != null)
        OnOverviewMapRendered();
      return img;
    }

    public System.Drawing.Image GetMap()
    {
      System.Diagnostics.Debug.WriteLine("ComplexMap.GetMap");
      if (!_MapLoaded)
        return null;
      if (OnMapRendering != null)
        OnMapRendering();
      if ((MapSize.Width == 0) || (MapSize.Height == 0))
        return null;
      System.Drawing.Image img = null;
      try
      {
        _RenderingMap = "Map";
        img = new System.Drawing.Bitmap(MapSize.Width, MapSize.Height);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);


        try
        {
          if (!BaseMapRedrawn)
          {
            try
            {
              _baseImage = _BaseMap.GetMap();
              BaseMapRedrawn = true;
            }
            catch (Exception ex)
            {
              IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
              BaseMapRedrawn = false;
            }
          }

          if (!RealTimeMapRedrawn)
          {
              try
              {
                  _RealTimeImage = _RealTimeMap.GetMap();
                  RealTimeMapRedrawn = true;
              }
              catch (Exception ex)
              {
                IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
                  RealTimeMapRedrawn = false;
                  _RealTimeImage = null;
              }
          }


          g.PageUnit = System.Drawing.GraphicsUnit.Pixel;
          if (_baseImage != null)
            g.DrawImage(_baseImage, 0, 0);
          if (_RealTimeImage != null)
            g.DrawImage(_RealTimeImage, 0, 0);
        }
        catch (InvalidOperationException ex)
        {
          IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
          g.DrawLine(System.Drawing.Pens.Aqua, 0, 0, 100, 100);
          g.DrawString(ex.Message,
            new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
            System.Drawing.Brushes.Tomato,
            new System.Drawing.PointF(100, 100));

        }
        g.Dispose();
        _RenderingMap = "";
        if (OnMapRendered != null)
          OnMapRendered();
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
      }
      return img;
    }

    public System.Drawing.Color BackColor
    {
      get
      {
        return _BaseMap.BackColor;
      }
      set
      {
        _BaseMap.BackColor = value;
        BaseMapRedrawn = false;
      }
    }

    public SharpMap.Geometries.BoundingBox GetExtents()
    {
      /*
      SharpMap.Geometries.BoundingBox bboxBase = _BaseMap.GetExtents();
      SharpMap.Geometries.BoundingBox bboxTraking = _TrackingMap.GetExtents();
      return bboxBase.Join(bboxTraking);
       * */
      if (!_MapLoaded)
        return null;
      return Layers_["caminos"].Envelope;
    }

    public SharpMap.Geometries.Point ImageToWorld(System.Drawing.PointF point)
    {
      return _BaseMap.ImageToWorld(point);
    }

    public void Pan(SharpMap.Geometries.Point p0, SharpMap.Geometries.Point p1)
    {
      _BaseMap.Center += (p0 - p1);
      _RealTimeMap.Center = _BaseMap.Center;
      _MapArea = _BaseMap.Envelope;
      BaseMapRedrawn = false;
      OverviewMapRedrawn = false;
      RealTimeMapRedrawn = false;
      RefreshViews();
    }

    public void ZoomToBox(SharpMap.Geometries.BoundingBox bbox)
    {
      _BaseMap.ZoomToBox(bbox);
      _RealTimeMap.ZoomToBox(bbox);
      _MapArea = bbox;
      BaseMapRedrawn = false;
      OverviewMapRedrawn = false;
      RealTimeMapRedrawn = false;
      RefreshViews();
    }

    protected void ZoomFactor(double factor)
    {
      _BaseMap.Zoom *= factor;

      _RealTimeMap.ZoomToBox(_BaseMap.Envelope);
      _MapArea = _BaseMap.Envelope;

      BaseMapRedrawn = false;
      OverviewMapRedrawn = false;
      RealTimeMapRedrawn = false;
      RefreshViews();
    }

    public void ZoomIn()
    {
      ZoomFactor(0.5);
    }

    public void ZoomOut()
    {
      ZoomFactor(2);
    }

    public void ZoomToExtents()
    {
      ZoomToBox(GetExtents());
    }

    public void ZoomToElement(string layerName, string IDVehicle)
    {
      SharpMap.Geometries.Geometry geometry = ((Layers_[layerName] as SharpMap.Layers.VectorLayer).DataSource as SharpMap.Data.Providers.MemoryDataProviderBase).GetGeometryByVehicleID(IDVehicle);
      ZoomToElement(geometry);
    }
    public void ZoomToElement(SharpMap.Geometries.Geometry Element)
    {
      if (Element == null)
        return;

      SharpMap.Geometries.Point point = Element.GetBoundingBox().GetCentroid().AsPoint();
      if ((point.X == 0.0) && (point.Y == 0.0))
        return;
      _BaseMap.Center = Element.GetBoundingBox().GetCentroid();
      _RealTimeMap.ZoomToBox(_BaseMap.Envelope);
      _MapArea = _BaseMap.Envelope;
      BaseMapRedrawn = false;
      OverviewMapRedrawn = false;
      RealTimeMapRedrawn = false;
      RefreshViews();
    }

    public void ZoomToElement(string layerName, uint IDElement)
    {
      // private System.Collections.Generic.Dictionary<string, SharpMap.Layers.ILayer> Layers_;
      // private System.Collections.Generic.List<string> RealTimeLayers_;
      SharpMap.Geometries.Geometry geo = (Layers_[layerName] as SharpMap.Layers.VectorLayer).DataSource.GetGeometryByID(IDElement);

      if (geo == null)
        return;

      SharpMap.Geometries.Point pt = geo.GetBoundingBox().GetCentroid().AsPoint();
      if ((pt.X == 0) && (pt.Y == 0))
        return;
      _BaseMap.Center = geo.GetBoundingBox().GetCentroid();
      
      _RealTimeMap.ZoomToBox(_BaseMap.Envelope);
      _MapArea = _BaseMap.Envelope;

      BaseMapRedrawn = false;
      OverviewMapRedrawn = false;
      RealTimeMapRedrawn = false;
      RefreshViews();
    }

    //public double TrackingRefreshInterval
    //{
    //  get
    //  {
    //    SharpMap.Layers.VectorLayer layer = (SharpMap.Layers.VectorLayer)_TrackingMap.GetLayerByName("GPSData");
    //    IntelliTrack.Client.HistoricDBLayer.GPSDataSource DataSource = (IntelliTrack.Client.HistoricDBLayer.GPSDataSource)layer.DataSource;
    //    return DataSource.RefreshTime;
    //  }
    //  set
    //  {
    //    SharpMap.Layers.VectorLayer layer = (SharpMap.Layers.VectorLayer)_TrackingMap.GetLayerByName("GPSData");
    //    IntelliTrack.Client.HistoricDBLayer.GPSDataSource DataSource = (IntelliTrack.Client.HistoricDBLayer.GPSDataSource)layer.DataSource;
    //    DataSource.RefreshTime = value;
    //  }
    //}

    private static int refresh = 0;

    private void RefreshViews()
    {
      System.Diagnostics.Debug.WriteLine("ComplexMap.RefreshViews");
      if (_MapLoaded)
      {
        for (int i = 0; i < forms_.Count; i++)
        {
          forms_[i].RefreshMe();
        }
        refresh++;
      }
    }

    public void AddForm(frmBaseDockingForm form)
    {
      forms_.Add(form);
    }

    public int LayerCount
    {
      get
      {
        if (_MapLoaded)
          return Layers_.Count;
        else
          return 0;
      }
    }

    public SharpMap.Data.FeatureDataSet SelectElements(SharpMap.Geometries.BoundingBox bbox, string layername)
    {
      if (!_MapLoaded)
        return null;
      SharpMap.Data.FeatureDataSet fds = new SharpMap.Data.FeatureDataSet();
      SharpMap.Layers.VectorLayer l = (SharpMap.Layers.VectorLayer)Layers_[layername];
      l.DataSource.Open();
      if (layername == "Frentes")
      {
        (l.DataSource as SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.FrenteDataPoint>).ExecuteIntersectionQuery2(bbox.GetCentroid(), fds);
      }
      else
      {
        l.DataSource.ExecuteIntersectionQuery(bbox, fds);
      }
      l.DataSource.Close();
      return fds;
    }

    public SharpMap.Data.FeatureDataSet SelectElements(SharpMap.Geometries.Point point, string layername)
    {
      return SelectElements(point.GetBoundingBox(), layername);
    }

    public delegate void DataArrived(string FromIP, string message);

    public DataArrived UDPDataArrived;

    protected void UDPArrivedData(string FromIP, string message)
    {
      this.RealTimeMapRedrawn = false;
      if (UDPDataArrived != null)
        UDPDataArrived(FromIP, message);
    }

    public System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.IProvider> GetRealTimeProviders()
    {
      System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.IProvider> prvds = new Dictionary<string, SharpMap.Data.Providers.IProvider>();
      foreach (string layername in RealTimeLayers_)
      {
          if (Layers_[layername] is SharpMap.Layers.VectorLayer)
              prvds.Add(layername, (Layers_[layername] as SharpMap.Layers.VectorLayer).DataSource);
          else if (Layers_[layername] is SharpMap.Layers.LabelLayer)
              prvds.Add(layername, (Layers_[layername] as SharpMap.Layers.LabelLayer).DataSource);
      }
      return prvds;
    }

      public System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.IProvider> GetRealTimeLayerProviders()
      {
          System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.IProvider> prvds = new Dictionary<string, SharpMap.Data.Providers.IProvider>();
          foreach (string layername in RealTimeLayers_)
          {
              if (Layers_[layername] is SharpMap.Layers.VectorLayer)
                  prvds.Add(layername, (Layers_[layername] as SharpMap.Layers.VectorLayer).DataSource);
          }
          return prvds;
      }


    private SharpMap.Styles.VectorStyle CaminosStyle(SharpMap.Data.FeatureDataRow row)
    {
      SharpMap.Styles.VectorStyle style = new SharpMap.Styles.VectorStyle();
      switch (row["TYPE"].ToString())
      {
        case "Camino Principal":
          style.Line.Color = System.Drawing.Color.Brown;
          break;
        case "Camino Secundario":
          style.Line.Color = System.Drawing.Color.DarkGray;
          break;
        case "Ruta Provincial":
          style.Line.Color = System.Drawing.Color.Orange;
          break;
        case "Ruta Nacional":
          style.Line.Color = System.Drawing.Color.Yellow;
          style.Line.Width = 2;
          break;
        case "Sin Especificar":
        default:
          style.Line.Color = System.Drawing.Color.Black;
          break;
      }
      return style;
    }

    private string GetFileNameFromConfig(string parameterName)
    {
      string filename = System.Configuration.ConfigurationManager.AppSettings[parameterName];
      if (filename == null)
        return "";
      if (filename.Trim().Length > 0)
        return AppDomain.CurrentDomain.BaseDirectory + filename;
      else
        return "";
    }

    private SharpMap.Styles.VectorStyle ReferenciasStyle(SharpMap.Data.FeatureDataRow row)
    {
      SharpMap.Styles.VectorStyle style;
      switch (row["TYPE"].ToString())
      {
        case "Cargadero":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cargadero"));
          break;
        case "Cruce":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cruce"));
          break;
        case "Canchon":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Canchon"));
          break;
        case "Puente":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Puente"));
          break;
        case "Acceso":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Acceso"));
          break;
        case "Proveedor":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Proveedor"));
          break;
        default:
          {
            style = new SharpMap.Styles.VectorStyle();
            style.Line.Color = System.Drawing.Color.Black;
          }
          break;
      }
      return style;
    }

    private SharpMap.Styles.VectorStyle LoadStyle(string filename)
    {
      SharpMap.Styles.VectorStyle style = new SharpMap.Styles.VectorStyle();
      if (System.IO.File.Exists(filename))
      {
        string fileExtension = System.IO.Path.GetExtension(filename).ToLower();
        switch (fileExtension)
        {
          case ".png":
          case ".jpg":
          case ".gif":
          case ".bmp":
            style.Symbol = new System.Drawing.Bitmap(filename, true);
            break;
          case ".ico":
            System.Drawing.Icon icon = new System.Drawing.Icon(filename);
            style.Symbol = icon.ToBitmap();
            break;
          default:
            style.Line.Color = System.Drawing.Color.Black;
            break;
        }
      }
      else
      {
        style.Line.Color = System.Drawing.Color.Black;
      }
      return style;
    }

    private SharpMap.Styles.VectorStyle TransporteCañaStyle(SharpMap.Data.FeatureDataRow row)
    {
      SharpMap.Styles.VectorStyle style = null;
      switch (row["Curso8Rumbos"].ToString())
      {
        case "N":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cañeros_N"));
          break;
        case "NE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cañeros_NE"));
          break;
        case "E":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cañeros_NE"));
          break;
        case "SE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cañeros_SE"));
          break;
        case "S":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cañeros_S"));
          break;
        case "SO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cañeros_SO"));
          break;
        case "O":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cañeros_O"));
          break;
        case "NO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cañeros_NO"));
          break;
        default:
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cañeros_default"));
          break;
      }
      //style.SymbolOffset = new System.Drawing.PointF(-style.Symbol.Width / 2, -style.Symbol.Height / 2);
      return style;
    }

    private SharpMap.Styles.VectorStyle RegadoresStyle(SharpMap.Data.FeatureDataRow row)
    {
      SharpMap.Styles.VectorStyle style = null;
      switch (row["Curso8Rumbos"].ToString())
      {
        case "N":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Regadores_N"));
          break;
        case "NE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Regadores_NE"));
          break;
        case "E":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Regadores_E"));
          break;
        case "SE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Regadores_SE"));
          break;
        case "S":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Regadores_S"));
          break;
        case "SO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Regadores_SO"));
          break;
        case "O":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Regadores_O"));
          break;
        case "NO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Regadores_NO"));
          break;
        default:
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Regadores_default"));
          break;
      }
      return style;
    }

    private SharpMap.Styles.VectorStyle FrentesStyle(SharpMap.Data.FeatureDataRow row)
    {
      SharpMap.Styles.VectorStyle style = null;
      switch (row["Curso8Rumbos"].ToString())
      {
        case "N":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Frentes_N"));
          break;
        case "NE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Frentes_NE"));
          break;
        case "E":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Frentes_E"));
          break;
        case "SE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Frentes_SE"));
          break;
        case "S":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Frentes_S"));
          break;
        case "SO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Frentes_SO"));
          break;
        case "O":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Frentes_O"));
          break;
        case "NO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Frentes_NO"));
          break;
        default:
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Frentes_default"));
          break;
      }
      return style;
    }
    private SharpMap.Styles.VectorStyle TractoresStyle(SharpMap.Data.FeatureDataRow row)
    {
      SharpMap.Styles.VectorStyle style = null;
      switch (row["Curso8Rumbos"].ToString())
      {
        case "N":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Tractores_N"));
          break;
        case "NE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Tractores_NE"));
          break;
        case "E":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Tractores_E"));
          break;
        case "SE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Tractores_SE"));
          break;
        case "S":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Tractores_S"));
          break;
        case "SO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Tractores_SO"));
          break;
        case "O":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Tractores_O"));
          break;
        case "NO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Tractores_NO"));
          break;
        default:
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Tractores_default"));
          break;
      }
      return style;
    }

    private SharpMap.Styles.VectorStyle CosechadorasStyle(SharpMap.Data.FeatureDataRow row)
    {
      SharpMap.Styles.VectorStyle style = null;
      switch (row["Curso8Rumbos"].ToString())
      {
        case "N":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cosechadoras_N"));
          break;
        case "NE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cosechadoras_NE"));
          break;
        case "E":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cosechadoras_E"));
          break;
        case "SE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cosechadoras_SE"));
          break;
        case "S":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cosechadoras_S"));
          break;
        case "SO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cosechadoras_SO"));
          break;
        case "O":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cosechadoras_O"));
          break;
        case "NO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cosechadoras_NO"));
          break;
        default:
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Cosechadoras_default"));
          break;
      }
      return style;
    }

    private SharpMap.Styles.VectorStyle GenericoStyle(SharpMap.Data.FeatureDataRow row)
    {
      SharpMap.Styles.VectorStyle style = null;
      switch (row["Curso8Rumbos"].ToString())
      {
        case "N":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Generico_N"));
          break;
        case "NE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Generico_NE"));
          break;
        case "E":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Generico_E"));
          break;
        case "SE":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Generico_SE"));
          break;
        case "S":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Generico_S"));
          break;
        case "SO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Generico_SO"));
          break;
        case "O":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Generico_O"));
          break;
        case "NO":
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Generico_NO"));
          break;
        default:
          style = LoadStyle(GetFileNameFromConfig("Simbolo_Generico_default"));
          break;
      }
      return style;
    }

    public string GetLabelMethodCaminos(SharpMap.Data.FeatureDataRow fdr)
    {
      return fdr["TYPE"].ToString() + " " + fdr["NAME"].ToString();
    }

    public string GetLabelMethodReferencias(SharpMap.Data.FeatureDataRow fdr)
    {
      return fdr["NAME"].ToString();
    }

    public string GetLabelMethodTransporteCaña(SharpMap.Data.FeatureDataRow fdr)
    {
      string s = "C. " + fdr["Camión"];
      if (fdr["Equipo"].ToString() != "")
        s += " E. " + fdr["Equipo"];
      return s;
    }

    public string GetLabelMethodRegadores(SharpMap.Data.FeatureDataRow fdr)
    {
      string s = "R. " + fdr["Camión"];
      return s;
    }

    public string GetLabelMethodFrentes(SharpMap.Data.FeatureDataRow fdr)
    {
      string s = "F. " + fdr["Frente"];
      return s;
    }

    public string GetLabelMethodTractores(SharpMap.Data.FeatureDataRow fdr)
    {
      string s = "T. " + fdr["ID"];
      return s;
    }

    public string GetLabelMethodCosechadoras(SharpMap.Data.FeatureDataRow fdr)
    {
      string s = "Cosech: " + fdr["ID"];
      return s;
    }

    public string GetLabelMethodGenerico(SharpMap.Data.FeatureDataRow fdr)
    {
      string s = "M.: " + fdr["ID"];
      return s;
    }


    internal string GetFunctionValue(SharpMap.Geometries.Point p0)
    {
      // Aca esta funcion ejecuta el stored procedure para obtener mas 
      // informacion del elemento sobre el que esta parado.
      // Dadas las coordenadas, se obtienen los elementos.

      SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.TransporteDataPoint> dsource = null;

      if (Layers_.ContainsKey("Transporte Caña"))
      {
        dsource = (Layers_["Transporte Caña"] as SharpMap.Layers.VectorLayer).DataSource as SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.TransporteDataPoint>;
      }
      else if (Layers_.ContainsKey("Cañeros"))
      {
        dsource = (Layers_["Cañeros"] as SharpMap.Layers.VectorLayer).DataSource as SharpMap.Data.Providers.MemoryDataProvider<IntelliTrack.Data.TransporteDataPoint>;
      }

      SharpMap.Data.FeatureDataSet fds = new SharpMap.Data.FeatureDataSet();
      if (dsource != null)
        dsource.ExecuteIntersectionQuery2(p0, fds);

      if (fds.Tables.Count > 0)
      {
        if (fds.Tables[0].Count > 0)
        {
          return this.GetFunctionValue(fds.Tables[0].Rows[0]["Transponder"].ToString());
        }
      }
      return "";
    }

    internal string GetFunctionValue(string TransponderID)
    {
      // Aca esta funcion ejecuta el stored procedure para obtener mas 
      // informacion del elemento sobre el que esta parado.
      // Dadas las coordenadas, se obtienen los elementos.

      // Aca obtengo la informacion del primer elemento seleccionado
      string resultado = "";

      string sql =
          " DECLARE	@return_value int; " +
          " EXEC	@return_value = [dbo].[STP_INFORMACION_ADICIONAL] " +
          "		@TRANS = N'" + TransponderID + "'; " +
          " SELECT	'Return Value' = @return_value ";

      string AppDBConnection = ValidacionSeguridad.Instance.GetApplicationConnectionString();
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppDBConnection);

      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sql, conn);

      System.Data.DataSet ds = new System.Data.DataSet();
      da.Fill(ds);

      conn.Close();

      try
      {
        resultado = ds.Tables[0].Rows[0]["Informacion_Adicional"].ToString();
      }
      catch (Exception)
      {
        resultado = "";
      }
      return resultado;      
    }

    private string TransponderToSetInformation;

    internal void SetElementToLaunch(string layerName, string IDVehicle)
    {
      if (layerName != "" && IDVehicle != "")
      {
        TransponderToSetInformation = ((Layers_[layerName] as SharpMap.Layers.VectorLayer).DataSource as SharpMap.Data.Providers.MemoryDataProviderBase).GetTransponderByVehicleID(IDVehicle);
        SharpMap.Geometries.Geometry geometry = ((Layers_[layerName] as SharpMap.Layers.VectorLayer).DataSource as SharpMap.Data.Providers.MemoryDataProviderBase).GetGeometryByVehicleID(IDVehicle);
        CoordinateToLaunch = geometry == null ? null : geometry.GetBoundingBox().GetCentroid();
        return;
      }
      CoordinateToLaunch = null;
      TransponderToSetInformation = "";
    }

    internal void LaunchInBrowser()
    {
      if (CoordinateToLaunch != null)
      {
        string s = ValidacionSeguridad.Instance.URL;
        double d1 = CoordinateToLaunch.Y;
        s = s.Replace("%lat%", d1.ToString());
        double d2 = CoordinateToLaunch.X;
        s = s.Replace("%lon%", d2.ToString());
        System.Diagnostics.Process.Start(s);
      }
    }

    internal void ShowInfoElementInDialog()
    {
      if (CoordinateToLaunch != null)
      {
        string s = GetFunctionValue(TransponderToSetInformation);
        System.Windows.Forms.MessageBox.Show(s, "Informaci\u00F3n\uFFFD", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
      }
    }
  }
}
