using System;
using System.Collections.Generic;
using System.Text;


namespace IntelliTrack.Data
{

  public abstract class DataPoint
  {
    public double lon;
    public double lat;
    public uint ID;
    public string TransponderID;
    public int CategoryID;
    public string Designacion;

    public string Camino;
    public string TipoCamino;

    public string Referencia;
    public string TipoReferencia;

    public IntelliTrack.Client.Application.UTCTime UTCTime;
    public int GPSStatus;//     GPS Status    0=not valid position. 1=GPS locked and valid position.
    public int NumSatellites;//    Num Satellites The number of satellites in view
    public int Altitude; //       Altitude    The latitude in meters. Null field if no GPS lock.
    public double Temperature;//     Temperature   The internal temperature of the RV-M7 in degrees C. Typically this is 5-20 degrees above ambient.
    public double Voltage;//       Voltage     Input voltage to the device that sent this position.
    public int IOStatus; //      IO status    A decimal number representing the binary inputs.
    public double RSSI;//        RSSI       The signal-strength of this message as measured by the receiver, in dBm. Note, if the message went through a repeater, it is the signal lever of the repeated message.
    public double Speed;//        Speed      The speed of the device in km/hour, 0-255
    public int Heading;//       Heading     The heading of the device 0-360 degrees
    public string Alerts; //        Alerts     Alert codes for alerts currently indicated in the device. NULL means no alerts. �P� means a proximity alert.
    public string Spare;//        Spare      A spare field. May be used for UTC date in the future. Typically NULL.

    public string Curso; // N, NNE, NE, ENE, E, ESE, SE, SSE, S...
    public string Curso8Rumbos; // N, NE, E, SE, S, SO, O, NO
    public string Command;

    public abstract void CalculateFields();

    public virtual SharpMap.Geometries.Geometry GetGeometry()
    {
      SharpMap.Geometries.Point pt = new SharpMap.Geometries.Point(lon, lat);
      return pt;
    }


    public virtual bool ParseData(Dictionary<string, string> ParsedData)
    {
      try
      {
        Command = ParsedData["Command"];
        if (ParsedData.ContainsKey("Longitude"))
        {
          IntelliTrack.Number.DDMMdotMMMM nro = new IntelliTrack.Number.DDMMdotMMMM(ParsedData["Longitude"]);
          lon = nro;
          nro = new IntelliTrack.Number.DDMMdotMMMM(ParsedData["Latitude"]);
          lat = nro;
          /*        lon = -65.18144610691931;
                  lat = -23.525701107943384;

                  lon = -64.788240910399281;
                  lat = -23.736674068068311;*/
          /// BAHIA
          //lat -= -14.958049000000003;
          //lon -= 2.4813246666666586;
          /// /BAHIA

          TransponderID = ParsedData["FromID"];

          UTCTime = new IntelliTrack.Client.Application.UTCTime(ParsedData["UTCTime"]);
          //UTCTime = new IntelliTrack.Client.Application.UTCTime();
          int aux;
          if (int.TryParse(ParsedData["GPSStatus"], out aux))
          {
            GPSStatus = aux;
          }
          if (int.TryParse(ParsedData["NumSatellites"], out aux))
            NumSatellites = aux;

          if (int.TryParse(ParsedData["Altitude"], out aux))
            Altitude = aux;

          double aux2;
          if (double.TryParse(ParsedData["Temperature"], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out aux2))
            Temperature = aux2;

          if (double.TryParse(ParsedData["Voltage"], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out aux2))
            Voltage = aux2;

          if (int.TryParse(ParsedData["IOStatus"], out aux))
            IOStatus = aux;

          if (double.TryParse(ParsedData["RSSI"], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out aux2))
            RSSI = aux2;

          if (int.TryParse(ParsedData["Speed"], out aux))
            Speed = aux;

          if (int.TryParse(ParsedData["Heading"], out aux))
            Heading = aux;

          Alerts = ParsedData["Alerts"];

          return true;
        }
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
      }
      return false;
    }

    public virtual bool UpdateFields()
    {
      try
      {
        /// BAHIA
        //Random r = new Random();

        // Aca modifico con unos Delta los valores de lat y lon
        //lon += (1 - r.NextDouble()) / 10000;
        //lat += (1 - r.NextDouble()) / 10000;
        //Heading = r.Next(0, 360);

        /// /BAHIA

        // Aca dadas las coordenadas obtengo informacion de los siguientes layers_
        SharpMap.Geometries.Point pt = new SharpMap.Geometries.Point(lon, lat);

        SharpMap.Data.FeatureDataSet fdsCaminos = IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.SelectElements(pt, "caminos");
        if (fdsCaminos.Tables[0].Count > 0)
        {
          Camino = fdsCaminos.Tables[0][0]["NAME"].ToString();
          TipoCamino = fdsCaminos.Tables[0][0]["TYPE"].ToString();
        }
        else
        {
          Camino = "S/D";
          TipoCamino = "S/D";
        }

        SharpMap.Data.FeatureDataSet fdsReferencias = IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.SelectElements(pt, "Referencias");
        if (fdsReferencias.Tables[0].Count > 0)
        {
          Referencia = fdsReferencias.Tables[0][0]["NAME"].ToString();
          TipoReferencia = fdsReferencias.Tables[0][0]["TYPE"].ToString();
        }
        else
        {
          Referencia = "S/D";
          TipoReferencia = "S/D";
        }

        Curso = Heading2Curso();
        Curso8Rumbos = Heading2Curso8Rumbos();

        return true;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    protected string Heading2Curso8Rumbos()
    {
      if ((337.5 <= Heading) && (Heading <= 360))
      {
        if (Speed > 0)
          return "N";
        else
          return "";
      }
      else if ((0 <= Heading) && (Heading <= 22.5))
        return "N";
      else if ((22.5 <= Heading) && (67.5 <= Heading))
        return "NE";
      else if ((67.5 <= Heading) && (112.5 <= Heading))
        return "E";
      else if ((112.5 <= Heading) && (157.5 <= Heading))
        return "SE";
      else if ((157.5 <= Heading) && (202.5 <= Heading))
        return "S";
      else if ((202.5 <= Heading) && (247.5 <= Heading))
        return "SO";
      else if ((247.5 <= Heading) && (292.5 <= Heading))
        return "O";
      else //if ((292.5 <= Heading) && (337.5 <= Heading))
        return "NO";
    }

    protected string Heading2Curso()
    {
      if ((348.75 <= Heading) && (Heading <= 360))
      {
        if (Speed > 0)
          return "N";
        else
          return "";
      }
      else if ((0 <= Heading) && (Heading <= 11.25))
        return "N";
      else if ((11.26 <= Heading) && (Heading <= 33.75))
        return "NNE";
      else if ((33.76 <= Heading) && (56.25 <= Heading))
        return "NE";
      else if ((56.26 <= Heading) && (78.75 <= Heading))
        return "ENE";
      else if ((78.76 <= Heading) && (101.25 <= Heading))
        return "E";
      else if ((101.26 <= Heading) && (123.75 <= Heading))
        return "ESE";
      else if ((123.76 <= Heading) && (146.25 <= Heading))
        return "SE";
      else if ((146.26 <= Heading) && (168.75 <= Heading))
        return "SSE";
      else if ((168.76 <= Heading) && (191.25 <= Heading))
        return "S";
      else if ((191.26 <= Heading) && (213.75 <= Heading))
        return "SSO";
      else if ((213.76 <= Heading) && (236.25 <= Heading))
        return "SO";
      else if ((236.26 <= Heading) && (258.75 <= Heading))
        return "OSO";
      else if ((258.76 <= Heading) && (281.25 <= Heading))
        return "O";
      else if ((281.26 <= Heading) && (303.75 <= Heading))
        return "ONO";
      else if ((303.76 <= Heading) && (326.25 <= Heading))
        return "NO";
      else //if ((326.76<=Heading)&&(348.75<=Heading))
        return "NNO";
    }

    /*
     * ALTER PROCEDURE DBO.STP_MOVIMIENTOS 
    @TRANSP_CAMION   VARCHAR(8),
    @DIA_HORA      SMALLDATETIME,
    @LATITUD         FLOAT,
    @LONGITUD      FLOAT,
    @VELOCIDAD    FLOAT,
    @CURSO            FLOAT,
    @TRANSP_VOLT        FLOAT,
    @TRANSP_TEMP SMALLINT,


    @NOM_PUNTO          NVARCHAR(50),
    @NOM_CAMINO          NVARCHAR(50),
    @NOM_AREA              NVARCHAR(50),
    @NOM_FRENTE         NVARCHAR(50),
    @CAMION                    NVARCHAR(4),
    @EQUIPO                    NVARCHAR(4),
    @TAG_EQUIPO           VARCHAR(16),
    @RADIO_COBERTURA        INT
    */
    protected virtual void AddFields(System.Data.SqlClient.SqlCommand cmd)
    {
      if (cmd == null)
        throw new NotSupportedException("cmd no debe ser nulo");

      System.Data.SqlClient.SqlParameter prm = new System.Data.SqlClient.SqlParameter("@TRANSP_CAMION", System.Data.SqlDbType.VarChar, 8);
      prm.Value = TransponderID;
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@DIA_HORA", System.Data.SqlDbType.DateTime);// System.Data.SqlDbType.SmallDateTime);
      if (UTCTime == null)
        prm.Value = System.Data.SqlTypes.SqlDateTime.Null;
      else
        prm.Value = UTCTime.ToDateTime().ToLocalTime();
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@LATITUD", System.Data.SqlDbType.Float);
      prm.Value = lat;
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@LONGITUD", System.Data.SqlDbType.Float);
      prm.Value = lon;
      cmd.Parameters.Add(prm);


      prm = new System.Data.SqlClient.SqlParameter("@VELOCIDAD", System.Data.SqlDbType.Float);
      prm.Value = Speed;
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@CURSO", System.Data.SqlDbType.Float);
      prm.Value = Heading;
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@TRANSP_VOLT", System.Data.SqlDbType.Float);
      prm.Value = Voltage;
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@TRANSP_TEMP", System.Data.SqlDbType.SmallInt);
      prm.Value = Temperature;
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@NOM_PUNTO", System.Data.SqlDbType.VarChar, 50);
      prm.Value = Referencia;
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@NOM_CAMINO", System.Data.SqlDbType.VarChar, 50);
      prm.Value = Camino;
      cmd.Parameters.Add(prm);


    }

    public virtual bool Equals(string FieldName, string Value)
    {
      switch (FieldName)
      {
        case "lon":
          return lon == double.Parse(Value);
          break;
        case "lat":
          return lat == double.Parse(Value);
          break;
        case "ID":
          return ID == uint.Parse(Value);
          break;
        case "TransponderID":
          return TransponderID == Value;
          break;
        case "CategoryID":
          return CategoryID == int.Parse(Value);
          break;
        case "Designacion":
          return Designacion == Value;
          break;
        case "Camino":
          return Camino == Value;
          break;
        case "TipoCamino":
          return TipoCamino == Value;
          break;
        case "Referencia":
          return Referencia == Value;
          break;
        case "TipoReferencia":
          return TipoReferencia == Value;
          break;
        //          case "        //public IntelliTrack.Client.Application.UTCTime UTCTime;
        case "GPSStatus":
          return GPSStatus == int.Parse(Value);
          break;
        case "NumSatellites":
          return NumSatellites == int.Parse(Value);
          break;
        case "Altitude":
          return Altitude == int.Parse(Value);
          break;
        case "Temperature":
          return Temperature == double.Parse(Value);
          break;
        case "Voltage":
          return Voltage == double.Parse(Value);
          break;
        case "IOStatus":
          return IOStatus == int.Parse(Value);
          break;
        case "RSSI":
          return RSSI == double.Parse(Value);
          break;
        case "Speed":
          return Speed == double.Parse(Value);
          break;
        case "Heading":
          return Heading == int.Parse(Value);
          break;
        case "Alerts":
          return Alerts == Value;
          break;
        case "Spare":
          return Spare == Value;
          break;
        case "Curso":
          return Curso == Value;
          break;
        case "Curso8Rumbos":
          return Curso8Rumbos == Value;
          break;
        case "Command":
          return Command == Value;
        default:
          break;
      }
      return false;
    }

    public virtual bool SaveData()
    {
      if (ValidacionSeguridad.Instance.Write2DB)
      {
        try
        {
          System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ValidacionSeguridad.Instance.GetApplicationConnectionString());
          conn.Open();

          System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("STP_MOVIMIENTOS", conn);

          cmd.CommandType = System.Data.CommandType.StoredProcedure;
          AddFields(cmd);

          int rowsaffected = cmd.ExecuteNonQuery();

          conn.Close();
        }
        catch (Exception ex)
        {
          IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
          return false;
        }
      }
      return true;
    }

    public virtual void SetFieldOrder(ref SharpMap.Data.FeatureDataTable fdt)
    {
      fdt.Columns["D�a"].SetOrdinal(0);
      fdt.Columns["Hora"].SetOrdinal(1);
      fdt.Columns["Velocidad"].SetOrdinal(2);
      fdt.Columns["Punto de Referencia"].SetOrdinal(3);
      fdt.Columns["Camino de Referencia"].SetOrdinal(4);
      fdt.Columns["Curso8Rumbos"].SetOrdinal(5);
      fdt.Columns["lon"].SetOrdinal(6);
      fdt.Columns["lat"].SetOrdinal(7);
      fdt.Columns["Transponder"].SetOrdinal(8);
      fdt.Columns["Temperatura"].SetOrdinal(9);
      fdt.Columns["Tensi\u00F3n"].SetOrdinal(10);
      fdt.Columns["ID"].SetOrdinal(11);
      fdt.Columns["Curso"].SetOrdinal(12);
    }


    public virtual void LoadDataFromDatarow(System.Data.DataRow row)
    {
      TransponderID = row["TRA_COD_TRANSP"].ToString();
      int id;
      if (int.TryParse(row["TRA_COD_CATEGORIA"].ToString(), out id))
        CategoryID = id;
      Designacion = row["TRA_VEHICULO"].ToString();
    }

    public override bool Equals(object obj)
    {
      return this.TransponderID == ((DataPoint)obj).TransponderID;
    }

    public virtual void AddFieldsToTable(ref SharpMap.Data.FeatureDataTable fdt)
    {
      fdt.Columns.Add("ID", typeof(uint));


      //- D�a (mm/dd/aaaa Ej.: 22/01/2010)
      //- Hora (hh:mm:ss Ej.: 09:05:45)
      fdt.Columns.Add("D�a", typeof(string));
      fdt.Columns.Add("Hora", typeof(string));

      //- Velocidad (000 km/h Ej: 56 km/h)
      fdt.Columns.Add("Velocidad", typeof(string));

      //- Curso (nnn � Ej; N-NNE-NE-ENE-E-ESE-SE.SSE-S�ver abajo c�digo� cada 22,5 grados un dato)
      fdt.Columns.Add("Curso", typeof(string));

      //- Punto Referencia (string)
      fdt.Columns.Add("Punto de Referencia", typeof(string));
      //- Camino Referencia (string)
      fdt.Columns.Add("Camino de Referencia", typeof(string));

      //- Temperatura (nn Ej.: 25 �C)
      fdt.Columns.Add("Temperatura", typeof(double));

      //- Tensi�n (nn,n Ej.: 14,4 v)
      fdt.Columns.Add("Tensi�n", typeof(double));

      fdt.Columns.Add("Transponder", typeof(string));


      fdt.Columns.Add("lon", typeof(string));
      fdt.Columns.Add("lat", typeof(string));
      fdt.Columns.Add("Curso8Rumbos", typeof(string));
    }

    public virtual SharpMap.Data.FeatureDataRow GetFeature(SharpMap.Data.FeatureDataTable fdt)
    {
      SharpMap.Data.FeatureDataRow fdr = fdt.NewRow();
      fdr["ID"] = ID;

      //- D�a (mm/dd/aaaa Ej.: 22/01/2010)
      //- Hora (hh:mm:ss Ej.: 09:05:45)
      if (UTCTime != null)
      {
        fdr["D�a"] = this.UTCTime.ToDateTime().ToLocalTime().ToString("dd-MM-yyyy");
        fdr["Hora"] = this.UTCTime.ToDateTime().ToLocalTime().ToString("HH:mm:ss");
      }
      else
      {
        fdr["D�a"] = "S/D";
        fdr["Hora"] = "S/D";
      }

      //- Velocidad (000 km/h Ej: 56 km/h)
      fdr["Velocidad"] = String.Format("{0:0.0 km/h}", this.Speed);
      //- Curso (nnn � Ej; N-NNE-NE-ENE-E-ESE-SE.SSE-S�ver abajo c�digo� cada 22,5 grados un dato)
      fdr["Curso"] = this.Curso;
      fdr["Curso8Rumbos"] = this.Curso8Rumbos;



      fdr["Transponder"] = this.TransponderID;

      //- Punto Referencia (string)
      fdr["Punto de Referencia"] = this.Referencia;
      //- Camino Referencia (string)
      fdr["Camino de Referencia"] = this.Camino;


      //- Temperatura (nn Ej.: 25 �C)
      fdr["Temperatura"] = this.Temperature;

      //- Tensi�n (nn,n Ej.: 14,4 v)
      fdr["Tensi�n"] = this.Voltage;

      IntelliTrack.Number.DDMMdotMMMM aux = new IntelliTrack.Number.DDMMdotMMMM(this.lon);
      fdr["lon"] = aux.ToString();
      aux = new IntelliTrack.Number.DDMMdotMMMM(this.lat);
      fdr["lat"] = aux.ToString();
      return fdr;
    }

    public virtual void CopyFrom(object data, bool UpdateCoordinates)
    {
      DataPoint theData = data as DataPoint;
      Command = theData.Command;
      if (UpdateCoordinates)
      {
        lon = theData.lon;
        lat = theData.lat;
      }

      /*
      ID = theData.ID;
      /*
      TransponderID = theData.TransponderID;
      CategoryID = theData.CategoryID;
      Designacion = theData.Designacion;

      Camino = theData.Camino;
      TipoCamino = theData.TipoCamino;

      Referencia = theData.Referencia;
      TipoReferencia = theData.TipoReferencia;
      */
      UTCTime = theData.UTCTime;
      GPSStatus = theData.GPSStatus;
      NumSatellites = theData.NumSatellites;
      Altitude = theData.Altitude;
      Temperature = theData.Temperature;
      Voltage = theData.Voltage;
      IOStatus = theData.IOStatus;
      RSSI = theData.RSSI;
      Speed = theData.Speed;
      Heading = theData.Heading;
      Alerts = theData.Alerts;
      Spare = theData.Spare;
      Curso = theData.Curso;
      Curso8Rumbos = theData.Curso8Rumbos;
    }
  }

  public class RegadorDataPoint : TractoresDataPoint
  {
    public RegadorDataPoint()
    {
      ID = maxID;
      maxID++;
    }

    public override void AddFieldsToTable(ref SharpMap.Data.FeatureDataTable fdt)
    {
      base.AddFieldsToTable(ref fdt);
      /*fdt.Columns["Cami�n"].SetOrdinal(1);
      fdt.Columns["D�a"].SetOrdinal(2);
      fdt.Columns["Hora"].SetOrdinal(3);
      fdt.Columns["Velocidad"].SetOrdinal(4);
      fdt.Columns["Direcci�n"].SetOrdinal(5);
       * */



      fdt.Columns["Transponder"].SetOrdinal(1);
      fdt.Columns["Equipo"].SetOrdinal(2);
      //fdt.Columns["NumeroTag"].SetOrdinal(3);
      fdt.Columns["Curso8Rumbos"].SetOrdinal(3);
      fdt.Columns["lon"].SetOrdinal(4);
      fdt.Columns["lat"].SetOrdinal(5);
      fdt.Columns["Temperatura"].SetOrdinal(6);
      fdt.Columns["Tensi�n"].SetOrdinal(7);
    }

    protected override void AddFields(System.Data.SqlClient.SqlCommand cmd)
    {
      base.AddFields(cmd);
    }

    public RegadorDataPoint(Dictionary<string, string> ParsedData)
    {
      throw new NotImplementedException("RegadorDataPoint");
    }

    public override bool ParseData(Dictionary<string, string> ParsedData)
    {
      try
      {
        bool res = base.ParseData(ParsedData);
        // Aca va el parsing propio de la clase
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    public override void CalculateFields()
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public override SharpMap.Geometries.Geometry GetGeometry()
    {
      return new SharpMap.Geometries.Point(lon, lat);
    }

    public override bool SaveData()
    {
      bool res = base.SaveData();
      return res;
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return base.ToString();
    }

    public override bool UpdateFields()
    {
      try
      {
        bool res = base.UpdateFields();
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    private static uint maxID = 0;

    public override void LoadDataFromDatarow(System.Data.DataRow row)
    {
      base.LoadDataFromDatarow(row);
    }
  }

  public class FrenteDataPoint : DataPoint
  {
    public FrenteDataPoint(Dictionary<string, string> ParsedData)
    {
      throw new NotImplementedException("FrenteDataPoint");
    }

    public FrenteDataPoint()
    {
      ID = maxID;
      maxID++;
    }
    public override void SetFieldOrder(ref SharpMap.Data.FeatureDataTable fdt)
    {
      fdt.Columns["Frente"].SetOrdinal(0);
      fdt.Columns["D�a"].SetOrdinal(1);
      fdt.Columns["Hora"].SetOrdinal(2);
      fdt.Columns["Velocidad"].SetOrdinal(3);
      fdt.Columns["Punto de Referencia"].SetOrdinal(4);
      fdt.Columns["Camino de Referencia"].SetOrdinal(5);
      fdt.Columns["Curso8Rumbos"].SetOrdinal(6);
      fdt.Columns["lon"].SetOrdinal(7);
      fdt.Columns["lat"].SetOrdinal(8);
      fdt.Columns["Transponder"].SetOrdinal(9);
      fdt.Columns["Temperatura"].SetOrdinal(10);
      fdt.Columns["Tensi�n"].SetOrdinal(11);
      fdt.Columns["ID"].SetOrdinal(12);
      fdt.Columns["Curso"].SetOrdinal(13);
    }

    public override bool Equals(string FieldName, string Value)
    {
      switch (FieldName)
      {
        case "radius":
          return radius == double.Parse(Value);
          break;
        case "Designacion":
          return Designacion == Value;
          break;
        default:
          return base.Equals(FieldName, Value);
          break;
      }
    }


    public override bool ParseData(Dictionary<string, string> ParsedData)
    {
      try
      {
        bool res = base.ParseData(ParsedData);
        // Aca va el parsing propio de la clase
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    public double radius;

    public override void CalculateFields()
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public override SharpMap.Geometries.Geometry GetGeometry()
    {
      return new SharpMap.Geometries.GISCircle(lon, lat, radius);
    }

    protected override void AddFields(System.Data.SqlClient.SqlCommand cmd)
    {
      base.AddFields(cmd);
      System.Data.SqlClient.SqlParameter prm = null;

      prm = new System.Data.SqlClient.SqlParameter("@NOM_FRENTE", System.Data.SqlDbType.VarChar, 50);
      prm.Value = (Designacion == null ? "" : Designacion);
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@RADIO_COBERTURA", System.Data.SqlDbType.Int);
      prm.Value = this.radius;
      cmd.Parameters.Add(prm);
    }

    public override bool SaveData()
    {
      bool res = base.SaveData();
      return res;
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return base.ToString();
    }

    public override bool UpdateFields()
    {
      try
      {
        bool res = base.UpdateFields();
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    public override void AddFieldsToTable(ref SharpMap.Data.FeatureDataTable fdt)
    {
      base.AddFieldsToTable(ref fdt);
      fdt.Columns.Add("Radius", typeof(double));
      fdt.Columns.Add("Frente", typeof(string));

      fdt.Columns["Transponder"].SetOrdinal(1);
      fdt.Columns["lon"].SetOrdinal(2);
      fdt.Columns["lat"].SetOrdinal(3);
      fdt.Columns["Radius"].SetOrdinal(4);
    }

    public override SharpMap.Data.FeatureDataRow GetFeature(SharpMap.Data.FeatureDataTable fdt)
    {
      SharpMap.Data.FeatureDataRow fdr = base.GetFeature(fdt);
      fdr["Radius"] = radius;
      fdr["Frente"] = Designacion;
      return fdr;
    }


    private static uint maxID = 0;

    private static double RadioPredeterminado = -1.0;
    public override void LoadDataFromDatarow(System.Data.DataRow row)
    {
      base.LoadDataFromDatarow(row);

      if (RadioPredeterminado == -1.0)
      {
        if (double.TryParse(System.Configuration.ConfigurationManager.AppSettings["RadioFrente"], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out radius))
        {
          RadioPredeterminado = radius; // Este valor deberia venir de la base de datos... O al menos de la configuracion
          if (RadioPredeterminado <= 0)
            RadioPredeterminado = 300;
        }
        else
          RadioPredeterminado = 300;
      }
      radius = RadioPredeterminado;
    }
  }

  public class TractoresDataPoint : DataPoint
  {


    protected static System.Collections.Generic.Dictionary<string, string> _Tags = null;
    // estos serian los campos opcionales...
    public string Area;
    public string Frente;
    //public string Camion;
    //public string Equipo;
    public string Equipo; // Equipo que esta llevando el tractor.
    public int RadioCobertura;

    public double oldDist;
    public string Direccion;

    public string Camion
    {
      get
      {
        return Designacion;
      }
      set
      {
        Designacion = value;
      }
    }

    public TractoresDataPoint()
    {
      ID = maxID;
      maxID++;
      if (_Tags == null)
        LoadTagsFromDatabase();
    }

    private void LoadTagsFromDatabase()
    {
      _Tags = new Dictionary<string, string>();

      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ValidacionSeguridad.Instance.GetApplicationConnectionString());
      conn.Open();

      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(
        "      SELECT     * "
        + " FROM         TAGS "
        + " WHERE rfi_activo = 'S'"
      , conn);
      System.Data.DataSet ds = new System.Data.DataSet();

      da.Fill(ds);

      foreach (System.Data.DataRow row in ds.Tables[0].Rows)
      {
        _Tags[row["RFI_TAG"].ToString()] = row["RFI_VEHICULO"].ToString();
      }
      conn.Close();
    }

    public override void SetFieldOrder(ref SharpMap.Data.FeatureDataTable fdt)
    {
      fdt.Columns["Cami�n"].SetOrdinal(0);
      fdt.Columns["D�a"].SetOrdinal(1);
      fdt.Columns["Hora"].SetOrdinal(2);
      fdt.Columns["Velocidad"].SetOrdinal(3);
      fdt.Columns["Frente"].SetOrdinal(4);
      fdt.Columns["Punto de Referencia"].SetOrdinal(5);
      fdt.Columns["Camino de Referencia"].SetOrdinal(6);
      fdt.Columns["Area"].SetOrdinal(7);
      fdt.Columns["Direcci�n"].SetOrdinal(8);
      fdt.Columns["Curso8Rumbos"].SetOrdinal(9);
      fdt.Columns["lon"].SetOrdinal(10);
      fdt.Columns["lat"].SetOrdinal(11);
      fdt.Columns["Transponder"].SetOrdinal(12);
      fdt.Columns["Temperatura"].SetOrdinal(13);
      fdt.Columns["Tensi�n"].SetOrdinal(14);
      fdt.Columns["ID"].SetOrdinal(15);
      fdt.Columns["Curso"].SetOrdinal(16);
      fdt.Columns["Equipo"].SetOrdinal(17);
      fdt.Columns["Radio de Cobertura"].SetOrdinal(18);
    }

    protected void LoadTagFromDatabase(string RFID)
    {
      if (_Tags == null)
        return;
      if (RFID.Trim() == "")
        return;
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ValidacionSeguridad.Instance.GetApplicationConnectionString());
      conn.Open();

      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(
        "      SELECT     * "
        + " FROM         TAGS "
        + " WHERE rfi_activo = 'S' "
        + " AND RFI_TAG = '" + RFID + "'"
      , conn);
      System.Data.DataSet ds = new System.Data.DataSet();

      da.Fill(ds);
      if (ds.Tables[0].Rows.Count > 0)
      {
        foreach (System.Data.DataRow row in ds.Tables[0].Rows)
        {
          _Tags[row["RFI_TAG"].ToString()] = row["RFI_VEHICULO"].ToString();
        }
      }
      conn.Close();
    }

    public static void RefreshTagsFromDatabase(int IntevalMinutes)
    {
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ValidacionSeguridad.Instance.GetApplicationConnectionString());
      conn.Open();

      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(
        "      SELECT     * "
        + " FROM         TAGS "
        + " WHERE "
        + " RFID_fecha_ACT > dateadd(mi, " + IntevalMinutes.ToString() + ", getdate()) "
      , conn);

      System.Data.DataSet ds = new System.Data.DataSet();

      da.Fill(ds);
      if (ds.Tables[0].Rows.Count > 0)
      {
        foreach (System.Data.DataRow row in ds.Tables[0].Rows)
        {
          if (row["RFI_ACTIVO"].ToString() == "N")
          {
            if (_Tags.ContainsKey(row["RFI_TAG"].ToString()))
            {
              _Tags.Remove(row["RFI_TAG"].ToString());
            }
          }
          else
          {
            _Tags[row["RFI_TAG"].ToString()] = row["RFI_VEHICULO"].ToString();
          }
        }
      }
      conn.Close();
    }

    public override void CalculateFields()
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public override SharpMap.Geometries.Geometry GetGeometry()
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public override bool ParseData(Dictionary<string, string> ParsedData)
    {
      try
      {
        bool res = base.ParseData(ParsedData);
        // Aca va el parsing propio de la clase
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    public override bool UpdateFields()
    {
      if (Command == "$PRAVE")
      {
        try
        {
          // Aca dadas las coordenadas obtengo informacion de los siguientes layers_
          /// BAHIA 
          //Random r = new Random();
          //lon += r.NextDouble() / 100;
          /// /BAHIA

          SharpMap.Geometries.Point pt = new SharpMap.Geometries.Point(lon, lat);


          bool res = base.UpdateFields();
          //    public string Area;



          // Aca calculo la distancia al punto de referencia de Ledesma
          double newDist = IntelliTrack.Client.Application.ReferencePoint.Instance.DistGrados(this.lat, this.lon);
          if (oldDist > newDist)
          {
            // acercando
            Direccion = "V";
          }
          else if (oldDist < newDist)
          {
            // alejando
            Direccion = "I";
          }
          else if (newDist == double.NaN)
          {
            // Indefinido
            Direccion = "S/D";
          }
          else
          {
            //igual
            if (Speed == 0)
              Direccion = "D";
          }
          oldDist = newDist;

          //Camion = "";
          //Equipo = "";
          //TagEquipo = "";
          //RadioCobertura = 0;

          return res;
        }
        catch (Exception ex)
        {
          IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
          return false;
        }
      }
      else
      {
        return true;
      }
    }


    public override void AddFieldsToTable(ref SharpMap.Data.FeatureDataTable fdt)
    {
      base.AddFieldsToTable(ref fdt);
      //fdt.Columns.Add("Equipo", typeof(string));
      //fdt.Columns["Equipo"].SetOrdinal(1);

      fdt.Columns.Add("Area", typeof(string));
      fdt.Columns["Area"].SetOrdinal(8);

      fdt.Columns.Add("Frente", typeof(string));
      fdt.Columns["Frente"].SetOrdinal(9);

      fdt.Columns.Add("Cami�n", typeof(string));
      fdt.Columns["Cami�n"].SetOrdinal(1);

      fdt.Columns.Add("Equipo", typeof(string));
      fdt.Columns["Equipo"].SetOrdinal(2);

      fdt.Columns.Add("Radio de Cobertura", typeof(int));

      fdt.Columns.Add("Direcci�n", typeof(string));
      fdt.Columns["Direcci�n"].SetOrdinal(6);
    }

    protected override void AddFields(System.Data.SqlClient.SqlCommand cmd)
    {
      base.AddFields(cmd);
      System.Data.SqlClient.SqlParameter prm = null;

      prm = new System.Data.SqlClient.SqlParameter("@NOM_AREA", System.Data.SqlDbType.VarChar, 50);
      prm.Value = (Area == null ? "" : Area);
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@NOM_FRENTE", System.Data.SqlDbType.VarChar, 50);
      prm.Value = (Frente == null ? "" : Frente);
      cmd.Parameters.Add(prm);


      prm = new System.Data.SqlClient.SqlParameter("@CAMION", System.Data.SqlDbType.VarChar, 4);
      prm.Value = (Camion == null ? "" : Camion);
      cmd.Parameters.Add(prm);


      prm = new System.Data.SqlClient.SqlParameter("@EQUIPO", System.Data.SqlDbType.VarChar, 4);
      prm.Value = (Equipo == null ? "" : Equipo);
      cmd.Parameters.Add(prm);


      prm = new System.Data.SqlClient.SqlParameter("@RADIO_COBERTURA", System.Data.SqlDbType.Int);
      prm.Value = RadioCobertura;
      cmd.Parameters.Add(prm);

      prm = new System.Data.SqlClient.SqlParameter("@SENTIDO", System.Data.SqlDbType.Char, 1);
      if (Direccion == null)
        Direccion = "";
      switch (Direccion.ToUpper())
      {
        case "I":
        case "D":
        case "V":
          prm.Value = Direccion;
          break;
        default:
          prm.Value = "";
          break;
      }
      cmd.Parameters.Add(prm);
    }

    public override SharpMap.Data.FeatureDataRow GetFeature(SharpMap.Data.FeatureDataTable fdt)
    {
      SharpMap.Data.FeatureDataRow fdr = base.GetFeature(fdt);
      fdr["Area"] = Area;
      fdr["Frente"] = Frente;
      fdr["Cami�n"] = Camion;
      //fdr["Equipo"] = Equipo;
      fdr["Equipo"] = Equipo;
      fdr["Radio de Cobertura"] = RadioCobertura;
      fdr["Direcci�n"] = Direccion;
      return fdr;
    }

    public override bool SaveData()
    {
      bool res = base.SaveData();
      return res;
    }

    private static uint maxID = 0;
    public override void LoadDataFromDatarow(System.Data.DataRow row)
    {
      base.LoadDataFromDatarow(row);
    }

    public override void CopyFrom(object data, bool UpdateCoordinates)
    {
      base.CopyFrom(data, UpdateCoordinates);
      TractoresDataPoint theData = data as TractoresDataPoint;
      /*Area = theData.Area;
      Frente = theData.Frente;
      Camion = theData.Camion;
      Equipo = theData.Equipo;
      TagEquipo = theData.TagEquipo;
      RadioCobertura = theData.RadioCobertura;
       */
    }
  }

  public class CosechadorasDataPoint : DataPoint
  {
    public CosechadorasDataPoint()
    {
      ID = maxID;
      maxID++;
    }

    public override void CalculateFields()
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public override SharpMap.Geometries.Geometry GetGeometry()
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public override bool ParseData(Dictionary<string, string> ParsedData)
    {
      try
      {
        bool res = base.ParseData(ParsedData);
        // Aca va el parsing propio de la clase
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    public override bool UpdateFields()
    {
      try
      {
        bool res = base.UpdateFields();
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    public override bool SaveData()
    {
      bool res = base.SaveData();
      return res;
    }

    private static uint maxID = 0;
    public override void LoadDataFromDatarow(System.Data.DataRow row)
    {
      base.LoadDataFromDatarow(row);
    }
  }

  public class UniversalDataPoint : DataPoint
  {

    public UniversalDataPoint()
    {
      ID = maxID;
      maxID++;
    }

    public override void CalculateFields()
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public override SharpMap.Geometries.Geometry GetGeometry()
    {
      //throw new Exception("The method or operation is not implemented.");
      return null;
    }

    public override bool ParseData(Dictionary<string, string> ParsedData)
    {
      try
      {
        bool res = base.ParseData(ParsedData);
        // Aca va el parsing propio de la clase
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    public override bool UpdateFields()
    {
      try
      {
        bool res = base.UpdateFields();
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    public override bool SaveData()
    {
      bool res = base.SaveData();
      return res;
    }

    private static uint maxID = 0;
    public override void LoadDataFromDatarow(System.Data.DataRow row)
    {
      base.LoadDataFromDatarow(row);
    }
  }

  public class TransporteDataPoint : TractoresDataPoint
  {

    public delegate void EntradaAFrente(string Equipo, string CodFrente, string CodTransporteCamion);
    public delegate void SalidaDeFrente(string Equipo, string CodFrente, string CodTransporteCamion);

    public static event EntradaAFrente OnEntradaAlFrente;
    public static event SalidaDeFrente OnSalidaAlFrente;


    public string IDCaja;
    public string NumeroTAGCompleto;
    public string NumeroTAG;
    public string TipoTAG;


    public override void CopyFrom(object data, bool UpdateCoordinates)
    {
      if (UpdateCoordinates)
        base.CopyFrom(data, UpdateCoordinates);
      TransporteDataPoint theData = data as TransporteDataPoint;
      Command = theData.Command;
      IDCaja = theData.IDCaja;
      NumeroTAGCompleto = theData.NumeroTAGCompleto;
      NumeroTAG = theData.NumeroTAG;
      TipoTAG = theData.TipoTAG;
    }

    public override bool ParseData(Dictionary<string, string> ParsedData)
    {
      try
      {
        bool res = base.ParseData(ParsedData);

        // Aca va el parsing propio de la clase
        try
        {
          if (ParsedData["Command"] == "$RFID")
          {
            TransponderID = "00" + ParsedData["Id. Caja"];
            IDCaja = ParsedData["Id. Caja"];
            NumeroTAGCompleto = ParsedData["N�mero TAG"];
            if (NumeroTAGCompleto.Length == 16)
            {
              if (NumeroTAGCompleto == "FFFFFFFFFFFFFFFF")
              {
                // Esta es una lectura sin informacion
                NumeroTAG = "";
              }
              else
                NumeroTAG = NumeroTAGCompleto.Substring(8);
            }
            else
              NumeroTAG = "";
          }
          res = true;
        }
        catch (Exception ex)
        {
          IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
          res = false;
        }
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    public TransporteDataPoint()
      : base()
    {
      ID = maxID;
      maxID++;
    }

    public TransporteDataPoint(Dictionary<string, string> ParsedData)
    {
      throw new NotImplementedException("TransporteDataPoint");
    }

    public override void CalculateFields()
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public override SharpMap.Geometries.Geometry GetGeometry()
    {
      return new SharpMap.Geometries.Point(lon, lat);
    }

    public override void AddFieldsToTable(ref SharpMap.Data.FeatureDataTable fdt)
    {
      base.AddFieldsToTable(ref fdt);
      fdt.Columns.Add("Id. Caja", typeof(string));
      fdt.Columns.Add("N�mero TAG", typeof(string));
      fdt.Columns.Add("Tipo TAG", typeof(string));

      fdt.Columns["Temperatura"].SetOrdinal(7);
      fdt.Columns["Tensi�n"].SetOrdinal(8);
      fdt.Columns["Transponder"].SetOrdinal(1);
      fdt.Columns["lon"].SetOrdinal(5);
      fdt.Columns["lat"].SetOrdinal(6);
      fdt.Columns["Curso8Rumbos"].SetOrdinal(4);
      fdt.Columns["Id. Caja"].SetOrdinal(2);
      fdt.Columns["N�mero TAG"].SetOrdinal(3);
    }

    protected override void AddFields(System.Data.SqlClient.SqlCommand cmd)
    {
      try
      {
        base.AddFields(cmd);
        System.Data.SqlClient.SqlParameter prm = null;

        prm = new System.Data.SqlClient.SqlParameter("@TAG_EQUIPO", System.Data.SqlDbType.VarChar, 16);
        if ((NumeroTAG == null) || (NumeroTAG == ""))
          prm.Value = "";
        else
          prm.Value = NumeroTAG;//.Substring(0, 8);
        cmd.Parameters.Add(prm);
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
      }
    }

    public override SharpMap.Data.FeatureDataRow GetFeature(SharpMap.Data.FeatureDataTable fdt)
    {
      SharpMap.Data.FeatureDataRow fdr = base.GetFeature(fdt);
      fdr["Id. Caja"] = IDCaja;
      fdr["N�mero TAG"] = NumeroTAG;
      fdr["Tipo TAG"] = TipoTAG;
      return fdr;
    }

    public override bool SaveData()
    {
      /*bool res = true;
      if (Command == "$RFID")
        res = base.SaveData();
      return res;*/
      return base.SaveData();
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return base.ToString();
    }

    public override bool UpdateFields()
    {
      try
      {
        if (NumeroTAG != null)
        {
          if (!_Tags.ContainsKey(NumeroTAG))
            LoadTagFromDatabase(NumeroTAG);

          if (_Tags.ContainsKey(NumeroTAG))
            Equipo = _Tags[NumeroTAG].Trim();
          else
            Equipo = "";
        }
        bool res = base.UpdateFields();

        if (Command == "$PRAVE")
        {
          SharpMap.Geometries.Point pt = new SharpMap.Geometries.Point(lon, lat);
          SharpMap.Data.FeatureDataSet fdsFrentes = IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.SelectElements(pt, "Frentes");
          //aca debo considerar que puedo tener una lista de frentes...
          string FrenteNuevo = "";
          if (fdsFrentes.Tables[0].Count > 0)
          {
            FrenteNuevo = DataTable2String(fdsFrentes.Tables[0]);
          }
          else
          {
            FrenteNuevo = "";
          }

          if (FrenteNuevo != Frente)
          {
            System.Diagnostics.Debug.WriteLine("Analizando");
            AnalizarEntradasYSalidasFrentes(Frente, FrenteNuevo);
            System.Diagnostics.Debug.WriteLine("Fin analisis");
            Frente = FrenteNuevo;
          }
        }
        return res;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
      }
    }

    private string DataTable2String(System.Data.DataTable dt)
    {
      string aux = "";
      foreach (System.Data.DataRow dr in dt.Rows)
      {
        aux += dr["Frente"].ToString() + ",";
      }
      if (aux.Length > 0)
        aux = aux.Substring(0, aux.Length - 1);
      return aux;
    }



    private static uint maxID = 0;

    public override void LoadDataFromDatarow(System.Data.DataRow row)
    {
      base.LoadDataFromDatarow(row);
      //throw new Exception("The method or operation is not implemented.");

      /*TRA_ACTIVO
      TRA_USUARIO
      TRA_FECHA_MOD
      CAT_DESCRIPCION
      TRA_COD_CATEGORIA
      */

    }
    private static int counter = 0;
    private void AnalizarEntradasYSalidasFrentes(string FrenteViejo, string FrenteNuevo)
    {
      try
      {
        string[] FrentesViejos = null;
        if (FrenteViejo != null)
          FrentesViejos = FrenteViejo.Split(',');

        string[] FrentesNuevos = null;
        if (FrenteNuevo != null)
          FrentesNuevos = FrenteNuevo.Split(',');

        List<string> Salidas = new List<string>();
        List<string> Entradas = new List<string>();

        if (FrentesViejos != null)
        {
          for (int i = 0; i < FrentesViejos.Length; i++)
          {
            Salidas.Add(FrentesViejos[i]);
          }
        }

        if (FrentesNuevos != null)
        {
          for (int i = 0; i < FrentesNuevos.Length; i++)
          {
            Entradas.Add(FrentesNuevos[i]);
          }
        }

        // Ahora voy recorriendo los frentes nuevos y voy eliminando 
        // en ambas listas los repetidos, ya que significa que el movil permanece 
        // en ese frente
        foreach (string frente in FrentesNuevos)
        {
          if (Salidas.Contains(frente))
          {
            Salidas.Remove(frente);
            Entradas.Remove(frente);
          }
        }

        // Recorro la lista de frentes en los que se acaba de entrar
        if (OnEntradaAlFrente != null)
        {
          foreach (string frente in Entradas)
          {
            if ((frente != null) && (frente != ""))
              OnEntradaAlFrente(Equipo, frente, Camion);
          }
        }

        // Recorro la listas de frentes de los que se salio
        if (OnSalidaAlFrente != null)
        {
          foreach (string frente in Salidas)
          {
            if ((frente != "") && (frente != ""))
              OnSalidaAlFrente(Equipo, frente, Camion);
          }
        }
        System.Diagnostics.Debug.WriteLine(
          "Camion: " + Camion +
          " FrenteViejo: " + FrenteViejo +
          " FrenteNuevo: " + FrenteNuevo +
          " counter: " + counter.ToString());
        counter++;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
      }
    }

  }

  public class GenericDataPoint : DataPoint
  {
    public override void CalculateFields()
    {
    }
  }

}

namespace SharpMap.Data.Providers
{
  public abstract class MemoryDataProviderBase
  {
    public abstract bool UpdateInformation(System.Collections.Generic.Dictionary<string, string> ParsedData);
    public abstract void LoadFromDB();
    public abstract bool LoadTransponderIDFromDB(string TransponderID);
    public abstract void ReloadFromDB(int IntervalMinutes);
    public abstract bool ExistsTransponderID(string TransponderID);
    public abstract SharpMap.Geometries.Geometry GetGeometryByVehicleID(string VehicleID);
    public abstract string GetTransponderByVehicleID(string VehicleID);
    public delegate void DataUpdated();
    public event DataUpdated OnDataUpdated;
    public virtual void FireOnDataUpdated()
    {
      if (OnDataUpdated != null)
        OnDataUpdated();
    }
    public abstract string LayerName { get;set;}

    protected short IDCategoria_;

    public short IDCategoria
    {
      get
      {
        return IDCategoria_;
      }
      set
      {
        IDCategoria_ = value;
      }
    }

    public abstract void ClearAll();
    public abstract void EliminarXORByTransponderIDVehicleID(string IDTransponder, string IDVehicle);
    public abstract SharpMap.Geometries.Geometry GetGeometryByID(uint oid);

    public abstract void LoadCoordinatesFromDB();


  } // class MemoryDataProviderBase



  public class MemoryDataProvider<T> : MemoryDataProviderBase, SharpMap.Data.Providers.IProvider, IDisposable where T : IntelliTrack.Data.DataPoint, new()
  {
    private string LayerName_;

    #region Constructor

    public MemoryDataProvider()
    {
      info = new Dictionary<uint, T>();
    }

    #endregion

    #region Protected member variables

    protected System.Collections.Generic.Dictionary<uint, T> info;

    #endregion

    #region IProvider Members

    public List<SharpMap.Geometries.Geometry> GetGeometriesInView(SharpMap.Geometries.BoundingBox bbox)
    {
      List<SharpMap.Geometries.Geometry> features = new List<SharpMap.Geometries.Geometry>();

      foreach (T data in info.Values)
      {
        SharpMap.Geometries.Geometry geom = data.GetGeometry(); //new SharpMap.Geometries.Point(data.lon, data.lat);
        features.Add(geom);
      }

      return features;
    }

    public List<uint> GetObjectIDsInView(SharpMap.Geometries.BoundingBox bbox)
    {
      List<uint> IDs = new List<uint>();
      foreach (T data in info.Values)
      {
        IDs.Add(data.ID);
      }
      return IDs;
    }

    public override SharpMap.Geometries.Geometry GetGeometryByID(uint oid)
    {
      foreach (T data in info.Values)
      {
        if (data.ID == oid)
          return data.GetGeometry();
      }
      return null;
    }

    public void ExecuteIntersectionQuery(SharpMap.Geometries.Geometry geom, FeatureDataSet ds)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    /*private SharpMap.Data.FeatureDataTable baseTable;

    private void CreateBaseTable()
    {
      baseTable = new SharpMap.Data.FeatureDataTable();
      T ele
      foreach (DbaseField dbf in DbaseColumns)
        baseTable.Columns.Add(dbf.ColumnName, dbf.DataType);
    }*/

    public void ExecuteIntersectionQuery(SharpMap.Geometries.BoundingBox box, FeatureDataSet ds)
    {
      SharpMap.Data.FeatureDataTable baseTable = new SharpMap.Data.FeatureDataTable();
      baseTable.TableName = "Tabla1";
      bool CreateTable = true;

      foreach (T element in info.Values)
      {
        if (CreateTable)
        {
          element.AddFieldsToTable(ref baseTable);
          element.SetFieldOrder(ref baseTable);
          CreateTable = false;
        }
        if (box.Contains(new SharpMap.Geometries.Point(element.lon, element.lat)))
        {
          SharpMap.Data.FeatureDataRow fdr = element.GetFeature(baseTable);
          fdr.Geometry = element.GetGeometry();
          baseTable.AddRow(fdr);
        }
      }
      ds.Tables.Add(baseTable);
    }


    public void ExecuteIntersectionQuery2(SharpMap.Geometries.Point pt, FeatureDataSet ds)
    {
      SharpMap.Data.FeatureDataTable baseTable = new SharpMap.Data.FeatureDataTable();
      baseTable.TableName = "Tabla1";
      bool CreateTable = true;

      foreach (T element in info.Values)
      {
        if (element is IntelliTrack.Data.FrenteDataPoint)
        {
          if (CreateTable)
          {
            (element as IntelliTrack.Data.FrenteDataPoint).AddFieldsToTable(ref baseTable);
            (element as IntelliTrack.Data.FrenteDataPoint).SetFieldOrder(ref baseTable);
            CreateTable = false;
          }
          SharpMap.Geometries.GISCircle gc = new SharpMap.Geometries.GISCircle(element.lon, element.lat, (element as IntelliTrack.Data.FrenteDataPoint).radius);
          if (gc.Contains(pt))
          {
            SharpMap.Data.FeatureDataRow fdr = (element as IntelliTrack.Data.FrenteDataPoint).GetFeature(baseTable);
            fdr.Geometry = element.GetGeometry();
            baseTable.AddRow(fdr);
          }
        }
      }
      ds.Tables.Add(baseTable);
    }


    public int GetFeatureCount()
    {
      return info.Count;
    }

    public SharpMap.Data.FeatureDataRow GetFeature(uint RowID)
    {
      return GetFeature(RowID, null);
    }

    /// <summary>
    /// Gets a datarow from the datasource at the specified index belonging to the specified datatable
    /// </summary>
    /// <param name="RowID"></param>
    /// <param name="dt">Datatable to feature should belong to.</param>
    /// <returns></returns>
    public SharpMap.Data.FeatureDataRow GetFeature(uint RowID, FeatureDataTable dt)
    {
      T element = info[RowID];
      return element.GetFeature(dt);
    }

    public SharpMap.Geometries.BoundingBox GetExtents()
    {
      SharpMap.Geometries.BoundingBox bbox = null;
      foreach (T data in info.Values)
      {
        SharpMap.Geometries.Geometry geom = data.GetGeometry(); //new SharpMap.Geometries.Point(data.lon, data.lat);
        if ((bbox == null) && (geom != null))
        {
          bbox = geom.GetBoundingBox();
        }
        else
        {
          bbox = bbox.Join(geom.GetBoundingBox());
        }
      }

      return bbox;
    }

    public string ConnectionID
    {
      get { throw new Exception("The method or operation is not implemented."); }
    }

    public void Open()
    {
      // No es necesario abrir la base
    }

    public void Close()
    {
      // No es necesario abrir la base
    }

    public bool IsOpen
    {
      get
      {
        return true;
      }
    }

    private int SRID_;
    public int SRID
    {
      get
      {
        return SRID_;
      }
      set
      {
        SRID_ = value;
      }
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
      throw new Exception("The method or operation is not implemented.");
    }

    #endregion

    #region IDisposable Members

    void IDisposable.Dispose()
    {
      throw new Exception("The method or operation is not implemented.");
    }

    #endregion

    #region Implementacion clase base
    public override bool UpdateInformation(Dictionary<string, string> ParsedData)
    {
      // Construyo un elemento en funcion de la informacion Parseada
      T Element = new T();
      if (Element.ParseData(ParsedData))
      {
        // Chequeo si este elemento existe en el layer
        if (ParsedData["Command"] == "$RFID")
          Element = GetElement(Element, false, false);
        else
          Element = GetElement(Element, true, true);
        Element.UpdateFields();
        Element.SaveData();
        FireOnDataUpdated();
        return true;
      }
      return false;
    }

    private T GetElement(T Element, bool createNewIfNotExists, bool UpdateCoordinates)
    {
      foreach (T data in info.Values)
      {
        if (data.Equals(Element))
        {
          data.CopyFrom(Element as T, UpdateCoordinates);
          return data;
        }
      }
      if (createNewIfNotExists)
      {
        Element.ID = (uint)(info.Count + 1);
        info.Add(Element.ID, Element);
      }
      return Element;
    }

    #endregion

    private T GetElementByTransponderID(string TransponderID)
    {
      foreach (uint ID in info.Keys)
      {
        T element = info[ID];
        if (element.TransponderID == TransponderID)
          return element;
      }
      return null;
    }



    public override void ReloadFromDB(int IntervalMinutes)
    {
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ValidacionSeguridad.Instance.GetApplicationConnectionString());
      conn.Open();

      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(
        " SELECT     TRANSPONDERS.TRA_COD_TRANSP, TRANSPONDERS.TRA_VEHICULO, TRANSPONDERS.TRA_ACTIVO, TRANSPONDERS.TRA_USUARIO, " +
        " TRANSPONDERS.TRA_FECHA_MOD, TRA_COD_CATEGORIA, TRA_ACTIVO " +
        " FROM         CATEGORIAS INNER JOIN " +
        " TRANSPONDERS ON CATEGORIAS.CAT_ID = TRANSPONDERS.TRA_COD_CATEGORIA " +
        " WHERE     (CATEGORIAS.CAT_DESCRIPCION = '" + LayerName_ + "') " +
        " AND TRA_VEHICULO <> 'N/A' " +
        " AND TRA_FECHA_MOD is not NULL" +
        " and tra_fecha_mod > dateadd(mi, " + IntervalMinutes.ToString() + ", getdate()) " +
        " ORDER BY TRANSPONDERS.TRA_VEHICULO "
        , conn);
      System.Data.DataSet ds = new System.Data.DataSet();

      da.Fill(ds);

      foreach (System.Data.DataRow row in ds.Tables[0].Rows)
      {
        string Activo = row["TRA_ACTIVO"].ToString();
        T element = GetElementByTransponderID(row["TRA_COD_TRANSP"].ToString());
        switch (Activo)
        {
          case "S":
            {
              if (element == null)
              {
                element = new T();
                element.LoadDataFromDatarow(row);
                info.Add(element.ID, element);
              }
              else
              {     // Update informacion
                element.LoadDataFromDatarow(row);
                info[element.ID] = element;
              }
            }
            break;
          case "N":
            info.Remove(element.ID);
            break;
        }
      }
      if (ds.Tables[0].Rows.Count > 0)
        FireOnDataUpdated();
      conn.Close();
    }

    public override void LoadFromDB()
    {
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ValidacionSeguridad.Instance.GetApplicationConnectionString());
      conn.Open();

      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(
        "      SELECT     TRANSPONDERS.TRA_COD_TRANSP, TRANSPONDERS.TRA_VEHICULO, TRANSPONDERS.TRA_ACTIVO, TRANSPONDERS.TRA_USUARIO, "
        + " TRANSPONDERS.TRA_FECHA_MOD, TRA_COD_CATEGORIA "
        + " FROM         CATEGORIAS INNER JOIN "
        + " TRANSPONDERS ON CATEGORIAS.CAT_ID = TRANSPONDERS.TRA_COD_CATEGORIA "
        + " WHERE     (CATEGORIAS.CAT_DESCRIPCION = '" + LayerName_ + "') "
        + " AND tra_activo = 'S' "
        + " AND TRA_VEHICULO <> 'N/A' "
      , conn);
      System.Data.DataSet ds = new System.Data.DataSet();

      da.Fill(ds);

      foreach (System.Data.DataRow row in ds.Tables[0].Rows)
      {
        T element = new T();
        element.LoadDataFromDatarow(row);
        info.Add(element.ID, element);
      }
      conn.Close();
    }

    public override bool LoadTransponderIDFromDB(string TransponderID)
    {
      bool resultado = false;
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ValidacionSeguridad.Instance.GetApplicationConnectionString());
      conn.Open();

      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(
        "      SELECT     TRANSPONDERS.TRA_COD_TRANSP, TRANSPONDERS.TRA_VEHICULO, TRANSPONDERS.TRA_ACTIVO, TRANSPONDERS.TRA_USUARIO, "
        + " TRANSPONDERS.TRA_FECHA_MOD, TRA_COD_CATEGORIA "
        + " FROM         CATEGORIAS INNER JOIN "
        + " TRANSPONDERS ON CATEGORIAS.CAT_ID = TRANSPONDERS.TRA_COD_CATEGORIA "
        + " WHERE     (CATEGORIAS.CAT_DESCRIPCION = '" + LayerName_ + "') "
        + " AND tra_activo = 'S' "
        + " AND TRA_VEHICULO <> 'N/A' "
        + " AND TRA_COD_TRANSP = '" + TransponderID + "' "
      , conn);
      System.Data.DataSet ds = new System.Data.DataSet();

      da.Fill(ds);

      if (ds.Tables[0].Rows.Count > 0)
      {
        foreach (System.Data.DataRow row in ds.Tables[0].Rows)
        {
          T element = new T();
          element.LoadDataFromDatarow(row);
          info.Add(element.ID, element);
        }
        resultado = true;
      }
      conn.Close();
      return resultado;
    }

    public override bool ExistsTransponderID(string TransponderID)
    {
      foreach (T element in info.Values)
      {
        if (element.TransponderID == TransponderID)
          return true;
      }
      return LoadTransponderIDFromDB(TransponderID);
    }

    public override string LayerName
    {
      get
      {
        return LayerName_;
      }
      set
      {
        LayerName_ = value;
      }
    }

    internal List<string> GetVehiculesIDs()
    {
      System.Collections.Generic.List<string> IDs = new List<string>();
      foreach (System.Collections.Generic.KeyValuePair<uint, T> pair in info)
      {
        IDs.Add((pair.Value as IntelliTrack.Data.DataPoint).Designacion);
      }
      return IDs;
    }


    private T GetElementByField(string FieldName, string Value)
    {
      foreach (T element in info.Values)
      {
        if (element.Equals(FieldName, Value))
          return element;
      }
      return null;
    }



    public override void ClearAll()
    {
      info.Clear();
    }

    public override void EliminarXORByTransponderIDVehicleID(string IDTransponder, string IDVehicle)
    {
      System.Collections.Generic.List<uint> list = new System.Collections.Generic.List<uint>();
      foreach (uint key in info.Keys)
      {
        if (info[key].TransponderID.Equals(IDTransponder) ^ info[key].Designacion.Equals(IDVehicle))
          list.Add(key);
      }
    }

    public override SharpMap.Geometries.Geometry GetGeometryByVehicleID(string VehicleID)
    {
      SharpMap.Geometries.Geometry geometry;

      foreach (T element in info.Values)
      {
        if (element.Designacion == VehicleID)
          return element.GetGeometry();
      }
      return null;
    }

    public override string GetTransponderByVehicleID(string VehicleID)
    {
      string s;

      foreach (T element in info.Values)
      {
        if (element.Designacion == VehicleID)
          return element.TransponderID;
      }
      return "";
    }

    public override void LoadCoordinatesFromDB()
    {
      System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(ValidacionSeguridad.Instance.GetApplicationConnectionString());
      sqlConnection.Open();
      System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(" DECLARE @return_value int  EXEC @return_value = [dbo].[STP_ULTIMOS_MOVIMIENTOS_FRENTES]  SELECT 'Return Value' = @return_value ", sqlConnection);
      System.Data.DataSet dataSet = new System.Data.DataSet();
      sqlDataAdapter.Fill(dataSet);
      foreach (System.Data.DataRow dataRow in dataSet.Tables[0].Rows)
      {
        T t = GetElementByField("Designacion", dataRow["MFC_NOM_FRENTE"].ToString());
        if (t != null)
        {
          t.lat = (double)dataRow["Latitud"];
          t.lon = (double)dataRow["Longitud"];
        }
      }
      sqlConnection.Close();
    }
  } // class MemoryDataProvider

}
