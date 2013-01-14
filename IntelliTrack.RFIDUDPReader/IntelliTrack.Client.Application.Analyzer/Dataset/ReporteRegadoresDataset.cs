using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Client.Application.Dataset
{
  public class ReporteRegadoresDataset
  {

    #region Atributos internos

    private int UmbralCarga;
    private int UmbralDetenido;
    private int UmbralTransito;
    private System.Collections.Generic.List<string> IDsValidos;
    System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<ReportElement>> info;

    #endregion



    private int GetConfigurationParameter(string ParameterName, int DefaultValue)
    {
      string value = System.Configuration.ConfigurationManager.AppSettings[ParameterName];
      int valor = 0;
      if (!int.TryParse(value, out valor))
      {
        valor = DefaultValue;
      }
      valor = valor <= 0 ? DefaultValue : valor;
      return valor;
    }

    public ReporteRegadoresDataset()
    {
      info = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<IntelliTrack.Client.Application.Dataset.ReporteRegadoresDataset.ReportElement>>();
      UmbralCarga = GetConfigurationParameter("UmbralCargaSegundos", -2147483648);
      UmbralDetenido = GetConfigurationParameter("UmbralDetenidoSegundos", -2147483648);
      UmbralTransito = GetConfigurationParameter("UmbralTransitoSegundos", -2147483648);
      IntelliTrack.Client.Application.Dataset.ReporteRegadoresDataset._PuntosCarga = GetPuntosCarga();
    }

    private List<string> GetIdsValidos()
    {
      return IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.GetIDsRegadores();
    }

    public void Process(System.Data.DataSet ds)
    {
      // Proceso la informacion
      
      foreach (System.Data.DataRow row in ds.Tables[0].Rows)
      {
        ProcessRow(row);
      }
      // Finalizo los registros finales (para que tengan todos la fecha y hora final.
      if (ds.Tables[0].Rows.Count > 0)
      {
        FinalizarInformacion(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]);
      }
      // Elimino los registros cuyo tiempo es menor al umbral definido.
      EliminarRegistrosConDuracionMenorAUmbral();
    }


    private void EliminarRegistrosConDuracionMenorAUmbral(System.Collections.Generic.List<ReportElement> lista)
    {
      System.Collections.Generic.List<int> IDsARemover = new List<int>();

      for (int i = 0; i < lista.Count; i++)
      {
        ReportElement rpt = lista[i];

        int Umbral = int.MaxValue;
        switch (rpt.EstadoInterno)
        {
          case ReportElement.EstadosPosibles.Carga:
            Umbral = UmbralCarga;
            break;
          case ReportElement.EstadosPosibles.Detenido:
            Umbral = UmbralDetenido;
            break;
          case ReportElement.EstadosPosibles.Transito:
            Umbral = UmbralTransito;
            break;
          default:
            break;
        }
        if (rpt.Duracion.Value < Umbral)
        {
          IDsARemover.Add(i);
          if (i > 0)
            lista[i - 1].Finalizar(rpt.DIA_HORA_Final);
        }
      }

      for (int i = IDsARemover.Count - 1; i >= 0; i--)
      {
        lista.RemoveAt(IDsARemover[i]);
      }
    }

    private void EliminarRegistrosConDuracionMenorAUmbral()
    {
      System.Collections.Generic.List<string> ClavesAEliminar = new List<string>();
      foreach (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<ReportElement>> pair in info)
      {
        EliminarRegistrosConDuracionMenorAUmbral(pair.Value);
        if (pair.Value.Count == 0)
        {
          ClavesAEliminar.Add(pair.Key);
        }
      }
      foreach (string ClaveAEliminar in ClavesAEliminar)
      {
        info.Remove(ClaveAEliminar);
      }
    }

    private void FinalizarInformacion(System.Data.DataRow row)
    {
      System.DateTime? FechaHoraFinal = DateTimeNOMilliseconds((System.DateTime)row["DIA_HORA"]);

      foreach (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<ReportElement>> pair in info)
      {
        pair.Value[pair.Value.Count - 1].Finalizar(FechaHoraFinal);
      }
    }


    private void ProcessRow(System.Data.DataRow row)
    {
      // Obtengo el ID del vehiculo
      string ID = row["IDElemento"].ToString();

      if (EsIDValido(ID))
      {
        if (!info.ContainsKey(ID))
          info[ID] = new List<ReportElement>();

        System.Collections.Generic.List<ReportElement> lista = info[ID];
        AddToList(lista, row);
      }
    }

    /// <summary>
    /// Esta funcion determina si el ID del vehiculo corresponde a 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private bool EsIDValido(string ID)
    {
      return true;
      return (IDsValidos.Count == 0) ? true : IDsValidos.Contains(ID);
    }

    private void AddToList(List<ReportElement> lista, System.Data.DataRow row)
    {
      ReportElement last = null;

      ReportElement rpt = new ReportElement();

      rpt.ID = (long)row["ID"];
      rpt.IDElemento = row["IDElemento"].ToString();
      rpt.IDTransponder = row["IDTransponder"].ToString();
      rpt.Latitud = (double)row["Latitud"];
      rpt.Longitud = (double)row["Longitud"];
      rpt.NOM_AREA = row["NOM_AREA"].ToString();
      rpt.NOM_CAMINO = row["NOM_CAMINO"].ToString();
      rpt.NOM_FRENTE = row["NOM_FRENTE"].ToString();
      rpt.NOM_PUNTO = row["NOM_PUNTO"].ToString();
      rpt.SENTIDO = row["SENTIDO"].ToString();
      rpt.TAG_EQUIPO = row["TAG_EQUIPO"].ToString();
      rpt.TRANSP_TEMP = (short)row["TRANSP_TEMP"];
      rpt.TRANSP_VOLT = (double)row["TRANSP_VOLT"];
      rpt.Velocidad = (double)row["Velocidad"];
      rpt.DIA_HORA = DateTimeNOMilliseconds((System.DateTime)row["DIA_HORA"]);
      rpt.Categoria = (short)row["Categoria"];
      rpt.Equipo = row["Equipo"].ToString();
      rpt.Curso = (double)row["Curso"];


      // Determino el estado en funcion de la velocidad

      rpt.DeterminarEstado();
      rpt.ActualizarCamposEnFuncionEstado();

      if (lista.Count > 0)
        last = lista[lista.Count - 1];
      else
        last = null;

      if (last == null)
      {
        lista.Add(rpt);
        return;
      }
      else
      {
        if (last.Estado == rpt.Estado)
        {
          // Los estados son iguales, por lo tanto añado a este elemento
          // la informacion de este registro.
          last.AddInformation(rpt);
        }
        else
        {
          // Los estados son distintos. Finalizo la informacion del ultimo 
          // elemento y añado el rpt a la lista.
          last.Finalizar(rpt);
          lista.Add(rpt);
        }
      }

      // Obtengo el ultimo elemento de la lista.
      if (lista.Count > 0)
      {
        last = lista[lista.Count - 1];

        // aca debo determinar si la fila continua permaneciendo en el estado igual al anterior.
        // si es asi ->

        if (last.Velocidad != rpt.Velocidad)
        {
          if ((last.Velocidad != 0) && (rpt.Velocidad != 0))
          {

          }
          else
          {
            last.DIA_HORA_Final = rpt.DIA_HORA;
            last = null;
          }
        }
        // si no -> seteo la fecha de fin del estado anterior y add una nueva fila
      }

      if (last == null)
      {
        lista.Add(rpt);
      }
    }

    public System.Data.DataSet DataSet
    {
      get
      {
        return CalculateDataSet();
      }
    }

    private System.Data.DataSet CrearDataSet()
    {
      System.Data.DataSet ds = new System.Data.DataSet();

      System.Data.DataTable dt = new System.Data.DataTable();

      dt.Columns.Add("ID", typeof(long));
      dt.Columns.Add("IDTransponder", typeof(string));
      dt.Columns.Add("IDElemento", typeof(string));
      dt.Columns.Add("Categoria", typeof(short));
      dt.Columns.Add("Equipo", typeof(string));
      dt.Columns.Add("DIA_HORA", typeof(System.DateTime));
      dt.Columns.Add("DIA_HORA_Final", typeof(System.DateTime));
      dt.Columns.Add("Duracion", typeof(int));
      dt.Columns.Add("Latitud", typeof(double));
      dt.Columns.Add("Longitud", typeof(double));
      dt.Columns.Add("Velocidad", typeof(double));
      dt.Columns.Add("Curso", typeof(double));
      dt.Columns.Add("NOM_PUNTO", typeof(string));
      dt.Columns.Add("NOM_CAMINO", typeof(string));
      dt.Columns.Add("NOM_AREA", typeof(string));
      dt.Columns.Add("NOM_FRENTE", typeof(string));
      dt.Columns.Add("TAG_EQUIPO", typeof(string));
      dt.Columns.Add("TRANSP_VOLT", typeof(double));
      dt.Columns.Add("TRANSP_TEMP", typeof(short));
      dt.Columns.Add("SENTIDO", typeof(string));
      dt.Columns.Add("TiempoCarga", typeof(TimeSpan));
      dt.Columns.Add("TiempoCargaSegundos", typeof(int));
      //dt.Columns.Add("TiempoRiego", typeof(TimeSpan));
      //dt.Columns.Add("TiempoImproductivo", typeof(TimeSpan));
      dt.Columns.Add("TiempoDetenido", typeof(TimeSpan));
      dt.Columns.Add("TiempoDetenidoSegundos", typeof(int));
      dt.Columns.Add("TiempoTransito", typeof(TimeSpan));
      dt.Columns.Add("TiempoTransitoSegundos", typeof(int));
      dt.Columns.Add("Estado", typeof(string));
      dt.Columns.Add("VelocidadMaxima", typeof(double));
      dt.Columns.Add("VelocidadPromedio", typeof(double));
      dt.Columns.Add("ReferenciaSalida", typeof(string));
      dt.Columns.Add("ReferenciaCaminos", typeof(string));
      dt.Columns.Add("ReferenciaLlegada", typeof(string));
      dt.Columns.Add("DistanciaRecorrida", typeof(double));

      ds.Tables.Add(dt);

      return ds;
    }

    private System.Data.DataSet CalculateDataSet()
    {
      System.Data.DataSet ds = CrearDataSet();
      CargarDataset(ds);
      /*ds.WriteXml("c:/regadores.xml", System.Data.XmlWriteMode.WriteSchema);
      ds.WriteXmlSchema("c:/regadores2.xml");*/
      return ds;
    }

    private void CargarDataset(System.Data.DataSet ds)
    {
      foreach (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<IntelliTrack.Client.Application.Dataset.ReporteRegadoresDataset.ReportElement>> pair in info)
      {
        foreach (ReportElement element in pair.Value)
        {
          System.Data.DataRow dataRow = ds.Tables[0].NewRow();
          if(element.ID.HasValue)
            dataRow["ID"] = element.ID;
          dataRow["IDTransponder"] = element.IDTransponder;
          dataRow["IDElemento"] = element.IDElemento;
          dataRow["Categoria"] = element.Categoria;
          dataRow["Equipo"] = element.Equipo;
          if (element.DIA_HORA.HasValue)
            dataRow["DIA_HORA"] = element.DIA_HORA;
          if(element.DIA_HORA_Final.HasValue)
            dataRow["DIA_HORA_Final"] = element.DIA_HORA_Final;
          if (element.Duracion.HasValue)
            dataRow["Duracion"] = element.Duracion;
          dataRow["Latitud"] = element.Latitud;
          dataRow["Longitud"] = element.Longitud;
          dataRow["Velocidad"] = element.Velocidad;
          dataRow["Curso"] = element.Curso;
          dataRow["NOM_PUNTO"] = element.NOM_PUNTO;
          dataRow["NOM_CAMINO"] = element.NOM_CAMINO;
          dataRow["NOM_AREA"] = element.NOM_AREA;
          dataRow["NOM_FRENTE"] = element.NOM_FRENTE;
          dataRow["TAG_EQUIPO"] = element.TAG_EQUIPO;
          dataRow["TRANSP_VOLT"] = element.TRANSP_VOLT;
          dataRow["TRANSP_TEMP"] = element.TRANSP_TEMP;
          dataRow["SENTIDO"] = element.SENTIDO;
          dataRow["TiempoCarga"] = element.TiempoCarga;
          dataRow["TiempoCargaSegundos"] = (int)element.TiempoCarga.TotalSeconds;
          dataRow["TiempoDetenido"] = element.TiempoDetenido;
          dataRow["TiempoDetenidoSegundos"] = (int)element.TiempoDetenido.TotalSeconds;
          dataRow["TiempoTransito"] = element.TiempoTransito;
          dataRow["TiempoTransitoSegundos"] = (int)element.TiempoTransito.TotalSeconds;
          dataRow["Estado"] = element.Estado;
          dataRow["VelocidadMaxima"] = element.VelocidadMaxima;
          dataRow["VelocidadPromedio"] = element.VelocidadPromedio;
          dataRow["ReferenciaSalida"] = element.ReferenciaSalida;
          dataRow["ReferenciaCaminos"] = element.ReferenciaCaminos;
          dataRow["ReferenciaLlegada"] = element.ReferenciaLlegada;
          dataRow["DistanciaRecorrida"] = element.DistanciaRecorrida;
          ds.Tables[0].Rows.Add(dataRow);
        }
      }
      ds.AcceptChanges();
    }
    private class ReportElement
    {
      private long? _ID;
      private string _IDTransponder;
      private string _IDElemento;
      private short? _Categoria;
      private string _Equipo;
      private System.DateTime? _DIA_HORA;
      private System.DateTime? _DIA_HORA_Final;
      private double _Latitud;
      private double _Longitud;
      private double? _Velocidad;
      private double? _Curso;
      private string _NOM_PUNTO;
      private string _NOM_CAMINO;
      private string _NOM_AREA;
      private string _NOM_FRENTE;
      private string _TAG_EQUIPO;
      private double? _TRANSP_VOLT;
      private short? _TRANSP_TEMP;
      private string _SENTIDO;

      private TimeSpan _TiempoCarga;
      //private TimeSpan _TiempoRiego;
      //private TimeSpan _TiempoImproductivo;
      private TimeSpan _TiempoDetenido;
      private TimeSpan _TiempoTransito;
      public enum EstadosPosibles
      {
        Carga,
        //Riego,
        //Improductivo,
        Detenido,
        Transito
      }
      private EstadosPosibles _Estado;
      private double _VelocidadMaxima;
      private double _VelocidadPromedio;

      private string _ReferenciaSalida;
      private string _ReferenciaCaminos;
      private string _ReferenciaLlegada;

      private double _DistanciaRecorrida;

      private SharpMap.Geometries.Point UltimoPunto;
      private SharpMap.Geometries.Point PuntoActual;

      public long? ID
      {
        get
        {
          return _ID;
        }
        set
        {
          _ID = value;
        }
      }

      public string IDTransponder
      {
        get
        {
          return _IDTransponder;
        }
        set
        {
          _IDTransponder = value;
        }
      }

      public string IDElemento
      {
        get
        {
          return _IDElemento;
        }
        set
        {
          _IDElemento = value;
        }
      }

      public short? Categoria
      {
        get
        {
          return _Categoria;
        }
        set
        {
          _Categoria = value;
        }
      }

      public string Equipo
      {
        get
        {
          return _Equipo;
        }
        set
        {
          _Equipo = value;
        }
      }

      public System.DateTime? DIA_HORA
      {
        get
        {
          return _DIA_HORA;
        }
        set
        {
          _DIA_HORA = value;
        }
      }

      public System.DateTime? DIA_HORA_Final
      {
        get
        {
          return _DIA_HORA_Final;
        }
        set
        {
          _DIA_HORA_Final = value;
        }
      }

      public int? Duracion
      {
        get
        {
          if (_DIA_HORA_Final.HasValue && _DIA_HORA.HasValue)
          {
            TimeSpan dur = _DIA_HORA_Final.Value - _DIA_HORA.Value;
            return (int)dur.TotalSeconds;
          }
          else
          {
            return null;
          }
        }
      }

      public TimeSpan TiempoCarga
      {
        get
        {
          return _TiempoCarga;
        }
      }

      /*public int TiempoRiego
      {
        get
        {
          return _TiempoRiego;
        }
      }*/

      /*public int TiempoImproductivo
      {
        get
        {
          return _TiempoImproductivo;
        }
      }*/

      public TimeSpan TiempoDetenido
      {
        get
        {
          return _TiempoDetenido;
        }
      }

      public TimeSpan TiempoTransito
      {
        get
        {
          return _TiempoTransito;
        }
      }

      public EstadosPosibles EstadoInterno
      {
        get
        {
          return _Estado;
        }
      }

      public String Estado
      {
        get
        {
          string estado = "";
          switch (_Estado)
          {
            case EstadosPosibles.Carga:
              estado = "Carga";
              break;
            case EstadosPosibles.Detenido:
              estado = "Detenido";
              break;
            //case EstadosPosibles.Improductivo:
            //  estado = "Improductivo";
            //  break;
            //case EstadosPosibles.Riego:
            //  estado = "Riego";
            //  break;
            case EstadosPosibles.Transito:
            default:
              estado = "Transito";
              break;
          }
          return estado;
        }
      }

      public double Latitud
      {
        get
        {
          return _Latitud;
        }
        set
        {
          _Latitud = value;
        }
      }

      public double Longitud
      {
        get
        {
          return _Longitud;
        }
        set
        {
          _Longitud = value;
        }
      }

      public double? Velocidad
      {
        get
        {
          return _Velocidad;
        }
        set
        {
          _Velocidad = value;
        }
      }

      public double VelocidadMaxima
      {
        get
        {
          return _VelocidadMaxima;
        }
      }

      public double VelocidadPromedio
      {
        get
        {
          return _VelocidadPromedio;
        }
      }

      public double? Curso
      {
        get
        {
          return _Curso;
        }
        set
        {
          _Curso = value;
        }
      }

      public string NOM_PUNTO
      {
        get
        {
          return _NOM_PUNTO;
        }
        set
        {
          _NOM_PUNTO = value;
        }
      }

      public string NOM_CAMINO
      {
        get
        {
          return _NOM_CAMINO;
        }
        set
        {
          _NOM_CAMINO = value;
        }
      }

      public string NOM_AREA
      {
        get
        {
          return _NOM_AREA;
        }
        set
        {
          _NOM_AREA = value;
        }
      }

      public string NOM_FRENTE
      {
        get
        {
          return _NOM_FRENTE;
        }
        set
        {
          _NOM_FRENTE = value;
        }
      }

      public string TAG_EQUIPO
      {
        get
        {
          return _TAG_EQUIPO;
        }
        set
        {
          _TAG_EQUIPO = value;
        }
      }

      public double? TRANSP_VOLT
      {
        get
        {
          return _TRANSP_VOLT;
        }
        set
        {
          _TRANSP_VOLT = value;
        }
      }

      public short? TRANSP_TEMP
      {
        get
        {
          return _TRANSP_TEMP;
        }
        set
        {
          _TRANSP_TEMP = value;
        }
      }

      public string SENTIDO
      {
        get
        {
          return _SENTIDO;
        }
        set
        {
          _SENTIDO = value;
        }
      }

      public string ReferenciaSalida
      {
        get
        {
          return _ReferenciaSalida;
        }
      }

      public string ReferenciaCaminos
      {
        get
        {
          return _ReferenciaCaminos;
        }
      }

      public string ReferenciaLlegada
      {
        get
        {
          return _ReferenciaLlegada;
        }
      }

      public double DistanciaRecorrida
      {
        get
        {
          return _DistanciaRecorrida;
        }
      }

      internal void DeterminarEstado()
      {
        if (_Velocidad == 0)
        {
          if (DetenidoEnPuntoRiego())
            this._Estado = EstadosPosibles.Carga;
          else
            this._Estado = EstadosPosibles.Detenido;
        }
        else
        {
          // TODO: como saber si esta regando, en transporte volciendo o al pedo???
          this._Estado = EstadosPosibles.Transito;
        }
      }


      private bool DetenidoEnPuntoRiego()
      {
        return _PuntosCarga.Contains(_NOM_PUNTO);
      }


      internal void ActualizarCamposEnFuncionEstado()
      {
        switch (this._Estado)
        {
          case EstadosPosibles.Carga:
          case EstadosPosibles.Detenido:
            this._ReferenciaCaminos = "";
            this._ReferenciaSalida = _NOM_PUNTO;
            this._ReferenciaLlegada = "";
            break;
          //case EstadosPosibles.Improductivo:
          //case EstadosPosibles.Riego:
          case EstadosPosibles.Transito:
            {
              _ReferenciaCaminos = _NOM_CAMINO;
              _ReferenciaSalida = _NOM_PUNTO;
              if (_Velocidad.HasValue)
              {
                _VelocidadMaxima = _Velocidad.Value;
                _VelocidadPromedio = _Velocidad.Value;
              }
              else
              {
                _VelocidadMaxima = 0;
                _VelocidadPromedio = 0;
              }
            }
            break;
          default:
            break;
        }
      }

      private SharpMap.Geometries.Point ProyectarPunto(double longitud, double latitud)
      {
        // Aca deberia proyectar desde lat/lon a UTM.
        SharpMap.CoordinateSystems.IProjectedCoordinateSystem UTM = CreateUTMProjection(1);
        
        //Create geographic coordinate system (lets just reuse the CS from the projection)
        SharpMap.CoordinateSystems.IGeographicCoordinateSystem geoCS = UTM.GeographicCoordinateSystem;
        //Create transformation
        SharpMap.CoordinateSystems.Transformations.CoordinateTransformationFactory ctFac = new SharpMap.CoordinateSystems.Transformations.CoordinateTransformationFactory();
        SharpMap.CoordinateSystems.Transformations.ICoordinateTransformation transform = ctFac.CreateFromCoordinateSystems(geoCS, UTM);
        
        //Apply transformation to a vectorlayer
        SharpMap.Geometries.Point pt = SharpMap.CoordinateSystems.Transformations.GeometryTransform.TransformPoint(new SharpMap.Geometries.Point(longitud, latitud), transform.MathTransform);

        return pt;
      }

      private SharpMap.CoordinateSystems.IProjectedCoordinateSystem CreateUTMProjection(int utmZone)
      {
        SharpMap.CoordinateSystems.CoordinateSystemFactory cFac = new SharpMap.CoordinateSystems.CoordinateSystemFactory();

        SharpMap.CoordinateSystems.IEllipsoid ellipsoid = cFac.CreateFlattenedSphere("WGS 84", 6378137, 298.257223563, SharpMap.CoordinateSystems.LinearUnit.Metre);
        SharpMap.CoordinateSystems.IHorizontalDatum datum = cFac.CreateHorizontalDatum("WGS_1984", SharpMap.CoordinateSystems.DatumType.HD_Geocentric, ellipsoid, null);
        SharpMap.CoordinateSystems.IGeographicCoordinateSystem gcs = cFac.CreateGeographicCoordinateSystem("WGS 84", SharpMap.CoordinateSystems.AngularUnit.Degrees, datum, SharpMap.CoordinateSystems.PrimeMeridian.Greenwich,
          new SharpMap.CoordinateSystems.AxisInfo("Lon", SharpMap.CoordinateSystems.AxisOrientationEnum.East),
          new SharpMap.CoordinateSystems.AxisInfo("Lat", SharpMap.CoordinateSystems.AxisOrientationEnum.North));

        //Create UTM projection
        List<SharpMap.CoordinateSystems.ProjectionParameter> parameters = new List<SharpMap.CoordinateSystems.ProjectionParameter>(5);
        parameters.Add(new SharpMap.CoordinateSystems.ProjectionParameter("latitude_of_origin", 0));
        parameters.Add(new SharpMap.CoordinateSystems.ProjectionParameter("central_meridian", -183 + 6 * utmZone));
        parameters.Add(new SharpMap.CoordinateSystems.ProjectionParameter("scale_factor", 0.9996));
        parameters.Add(new SharpMap.CoordinateSystems.ProjectionParameter("false_easting", 500000));
        parameters.Add(new SharpMap.CoordinateSystems.ProjectionParameter("false_northing", 0.0));
        SharpMap.CoordinateSystems.IProjection projection = cFac.CreateProjection("Transverse Mercator", "Transverse_Mercator", parameters);
        return cFac.CreateProjectedCoordinateSystem( "WGS 84 / UTM zone " + utmZone.ToString() + "N", gcs,
          projection, SharpMap.CoordinateSystems.LinearUnit.Metre,
          new SharpMap.CoordinateSystems.AxisInfo("East", SharpMap.CoordinateSystems.AxisOrientationEnum.East),
          new SharpMap.CoordinateSystems.AxisInfo("North", SharpMap.CoordinateSystems.AxisOrientationEnum.North));
      }

      private void ActualizarDistanciaRecorrida()
      {
        _DistanciaRecorrida += CalcularDistanciaSegmento(UltimoPunto, PuntoActual);
        UltimoPunto = PuntoActual;
      }

      private double CalcularDistanciaSegmento(SharpMap.Geometries.Point UltimoPunto, SharpMap.Geometries.Point PuntoActual)
      {
        if (UltimoPunto == null)
          return 0;
        else
        {
          return Math.Sqrt(Math.Pow(UltimoPunto.X - PuntoActual.X, 2) + Math.Pow(UltimoPunto.Y - PuntoActual.Y, 2)) / 1000;
        }
      }


      internal void Finalizar(System.DateTime? FechaFinal)
      {
        _DIA_HORA_Final = FechaFinal;
        CalcularValoresFinales();
      }

      internal void Finalizar(ReportElement rpt)
      {
        Finalizar(rpt._DIA_HORA);
      }

      private void CalcularValoresFinales()
      {
        System.TimeSpan duracion = (DIA_HORA_Final - DIA_HORA).Value;
        //string tiempo = (Math.Floor(duracion.TotalHours)).ToString("00") + ":" + duracion.Minutes.ToString("00") + ":" + duracion.Seconds.ToString("00");
        switch (_Estado)
        {
          case EstadosPosibles.Carga:
            {
              _TiempoCarga = duracion;
            }
            break;
          case EstadosPosibles.Detenido:
            {
              _TiempoDetenido = duracion;
            }
            break;
          /*case EstadosPosibles.Improductivo:
            {
              _TiempoImproductivo = duracion.Seconds;
            }
            break;*/
          /*case EstadosPosibles.Riego:
            {
              _TiempoRiego = duracion.Seconds;
            }
            break;*/
          case EstadosPosibles.Transito:
            {
              _TiempoTransito = duracion;
            }
            break;
          default:
            break;
        }
      }


      internal void AddInformation(ReportElement rpt)
      {
        // la idea es aproximar el esquema de velocidades por trapecios.
        if (rpt._VelocidadMaxima > _VelocidadMaxima)
        {
          _VelocidadMaxima = rpt._VelocidadMaxima;
        }
        if (_Estado == EstadosPosibles.Transito)
        {
          PuntoActual = ProyectarPunto(rpt.Longitud, rpt.Latitud);
          ActualizarDistanciaRecorrida();
          /*System.Diagnostics.Debug.WriteLine(this.PuntoActual);
          System.Diagnostics.Debug.WriteLine(this.UltimoPunto);
          System.Diagnostics.Debug.WriteLine(this.DistanciaRecorrida);*/
        }
      }

      public ReportElement()
      {
        UltimoPunto = null;
        PuntoActual = null;
      }
    } // class ReportElement


    private static System.Collections.Generic.List<string> _PuntosCarga;

    private System.DateTime DateTimeNOMilliseconds(System.DateTime fechaOriginal)
    {
      return new System.DateTime(fechaOriginal.Year, fechaOriginal.Month, fechaOriginal.Day, fechaOriginal.Hour, fechaOriginal.Minute, fechaOriginal.Second);
    }

    private System.Collections.Generic.List<string> GetPuntosCarga()
    {
      return IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.GetPuntosCargaRegadores();
    }
  } // class ReporteRegadoresDataset

}
