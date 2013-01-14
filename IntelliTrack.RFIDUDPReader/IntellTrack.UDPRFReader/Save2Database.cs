using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace IntellTrack.UDPRFReader
{
  public class Save2Database
  {

    private string ServerSICOP;
    private string UserSICOP;
    private string PasswordSICOP;

    private string GetConnectionString()
    {
      return "Data Source=" + ServerSICOP +
            ";Initial Catalog=SICOP;Persist Security Info=True;User ID=" + UserSICOP
          + "; pwd=" + PasswordSICOP;
    }

    public bool Save(string CodAntena, string TagEquipo)
    {
      // Aca se graba la informacion en la base de datos.

      //1. Para el caso RFID, donde del Agente está escuchando o leyendo los Data Logger cada 15 segundos se debe ejecutar la siguiente SP con los parámetros que se detallan:
      //-----------------------------------------------------------------------------------------------------------------------
      //-- Declare type parameter
      //DECLARE @RC int                                                   --Valor de retorno 0=OK
      //DECLARE @CODANTENA varchar(4)                         --Código de antena
      //DECLARE @TAGEQUIPO varchar(16)                        --Nro de Tag del equipo/camión
      //-- Set parameter values
      //SET @CODANTENA = '0001'
      //SET @TAGEQUIPO = 'A000000000000112'
      //EXEC @RC = [TARJA].[dbo].[STP_MOVIMIENTOS_PUNTOS_CONTROL] @CODANTENA, @TAGEQUIPO
      //-----------------------------------------------------------------------------------------------------------------------
      // Para los códigos de las antenas determinamos la siguiente codificación:
      //                       1 = Trapiche 1
      //2 = Trapiche 2
      //3 = Trapiche 3
      //4 = Bascula 9
      //5 = Bascula 10
      //6 = Portería Gas
      //7 = Portería Gas Interno)

      try
      {
        string strConnection = GetConnectionString();
        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strConnection);
        conn.Open();

        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.CommandText = "STP_MOVIMIENTOS_PUNTOS_CONTROL";

        System.Data.SqlClient.SqlParameter prm = new System.Data.SqlClient.SqlParameter("@CODANTENA", System.Data.SqlDbType.VarChar, 4);
        prm.Value = CodAntena;
        cmd.Parameters.Add(prm);

        prm = new System.Data.SqlClient.SqlParameter("@TAGEQUIPO", System.Data.SqlDbType.VarChar, 16);
        prm.Value = TagEquipo;
        cmd.Parameters.Add(prm);

        int rc =(int) cmd.ExecuteScalar();

        if (rc == 0)
          return true;
        else
          return false;
      }
      catch (Exception)
      {
        return false;
      }
    }

    private Save2Database()
    {
      ServerSICOP = ConfigurationManager.AppSettings["ServerSICOP"];
      UserSICOP = ConfigurationManager.AppSettings["UserSICOP"];
      PasswordSICOP = ConfigurationManager.AppSettings["PasswordSICOP"];
    }

    private static Save2Database Instance_;
    public static Save2Database Instance
    {
      get
      {
        if (Instance_ == null)
        {
          Instance_ = new Save2Database();
        }
        return Instance_;
      }
    }

  }
}
