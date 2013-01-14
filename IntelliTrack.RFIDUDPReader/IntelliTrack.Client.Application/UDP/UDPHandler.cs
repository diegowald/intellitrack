using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace IntelliTrack.Client.Application.UDP
{
  public class UDPHandler
  {
    private System.Collections.Generic.Dictionary<string, string> ParsedData;
    private DataParser Parser;

    private IntelliTrack.UDP.UDPClientThread udpThread;

    public UDPHandler()
    {
      Parser = new DataParser();
      InitializeData();
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

    protected void DataArrived(string FromIP, string message)
    {
      System.Diagnostics.Debug.WriteLine("UDPHandler.DataArrived");
        try
        {
            // Aca debo parsear la informacion
            SharpMap.Data.Providers.MemoryDataProviderBase dataProvider = null;
            if (Parser.ParseData(message, ref ParsedData))
            {
                if (ParsedData.Count >= 15)
                {
                    // Aca busco el layer al que pertenece el elemento
                    dataProvider = GetDataProvider(ParsedData["FromID"]);
                }
                else if (ParsedData.Count == 3)
                {
                  dataProvider = GetDataProvider("00" + ParsedData["Id. Caja"]);
                }
                else if (ParsedData.Count == 11)
                {
                  /*
                  int Anio;
                  int.TryParse(ParsedData["DateFix"].Substring(4, 2), out Anio);
                  Anio += 2000;

                  int Mes;
                  int.TryParse(ParsedData["DateFix"].Substring(2, 2), out Mes);

                  int Dia;
                  int.TryParse(ParsedData["DateFix"].Substring(0, 2), out Dia);

                  int Hora;
                  int.TryParse(ParsedData["UTCTIME"].Substring(0, 2), out Hora);

                  int Minuto;
                  int.TryParse(ParsedData["UTCTIME"].Substring(2, 2), out Minuto);

                  int Segundo;
                  int.TryParse(ParsedData["UTCTIME"].Substring(4, 2), out Segundo);

                  System.DateTime dt = new DateTime(Anio, Mes, Dia, Hora, Minuto, Segundo, 0);
                  Checker.SetEfemerides(dt);
                  */
                  UTCTime time = new UTCTime(ParsedData["DateFix"], ParsedData["UTCTIME"]);
                  Checker.SetEfemerides(time);
                }

                if (dataProvider != null)
                {
                    dataProvider.UpdateInformation(ParsedData);
                }
                // Actualizo el elemento

                // Actualizo los campos del elemento.

                // La informacion es correcta
                // Obtengo el DataSource que hay que actualizar
            }
            // Por algun motivo no se pudo actualizar la informacion.
            if (UDPArrivedData != null)
                UDPArrivedData(FromIP, message);
        }
        catch (Exception ex)
        {
          IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        }
    }

    public delegate void UDPDataArrived(string FromIP, string message);

    public UDPDataArrived UDPArrivedData;

    private void InitializeData()
    {
      string IP = System.Configuration.ConfigurationManager.AppSettings["IPBase"];
      if ((IP == null) || (IP == ""))
        IP = "10.2.40.20";

      Int32 port ;
      if (!Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["PortBase"], out port))
        port = 1420;

      //udpThread = new IntelliTrack.UDP.UDPClientThread(IP, port, new IntelliTrack.UDP.UDPClientThread.DataArrived(DataArrived));
      udpThread = new IntelliTrack.UDP.UDPClientThread(IP, port);
      /*      Que datos debo obtener aca?
              1) Cantidad de Transponders a leer
            2.0) Para i = 1 to CantTransponders
            2.1) Leer Transponder_i
            2.2) Leer TransponderType_i

     
            En esta parte deberemos leer los parametros de la base de datos...
       */
    }

      public void Start()
      {
          udpThread.Start(new IntelliTrack.UDP.UDPClientThread.DataArrived(DataArrived));
      }

    public void Stop()
    {
      udpThread.CloseAndAbort();
    }

      public void AddDataProvider(string DataProviderName, SharpMap.Data.Providers.MemoryDataProviderBase provider)
      {
          if (Providers==null)
              Providers = new Dictionary<string,SharpMap.Data.Providers.MemoryDataProviderBase>();
          Providers[DataProviderName]=provider;
      }

    //Debo tener un dictionary que vincule el DataProvider con TransponderID del GPS
    protected System.Collections.Generic.Dictionary<string, string> GPSDataProviderName;
    protected System.Collections.Generic.Dictionary<string, SharpMap.Data.Providers.MemoryDataProviderBase> Providers;

    private SharpMap.Data.Providers.MemoryDataProviderBase GetDataProvider(string TransponderID)
    {
      if (Providers != null)
      {
        foreach (string key in Providers.Keys)
        {
          SharpMap.Data.Providers.MemoryDataProviderBase provider = Providers[key];
          if (provider.ExistsTransponderID(TransponderID))
            return provider;
        }
      }
      return null;
    }
  }
}
