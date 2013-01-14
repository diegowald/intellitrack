using System;
using System.Text;
using System.Configuration;


public class ValidacionSeguridad
{
  private bool? UsuarioHabilitado_;
  protected string UserName;
  protected string NroSistema;
  protected string ServerSeguridad;
  protected string UserSeguridad;
  protected string PasswordSeguridad;
  protected string ServerAplicacion;
  protected string UserAplicacion;
  protected string PasswordAplicacion;
  protected System.Collections.Generic.Dictionary<string, string> Menues_;
  private ValidacionSeguridad()
  {
    //1.	Obtener el usuario de Red (vbScript)
    UserName = Environment.UserName;

    //2.	Obtener desde un archivo de configuración los siguientes parámetros:
    //a.	Número de sistema
    //b.	Nombre del servidor, usuario y clave de conexión a la DB de Seguridad 
    //c.	Nombre del servidor, usuario y clave de conexión a la DB de la Aplicación.
    NroSistema = ConfigurationManager.AppSettings["NumeroSistema"];

    ServerSeguridad = ConfigurationManager.AppSettings["ServerSeguridad"];
    UserSeguridad = ConfigurationManager.AppSettings["UserSeguridad"];
    PasswordSeguridad = ConfigurationManager.AppSettings["PasswordSeguridad"];

    ServerAplicacion = ConfigurationManager.AppSettings["ServerAplicacion"];
    UserAplicacion = ConfigurationManager.AppSettings["UserAplicacion"];
    PasswordAplicacion = ConfigurationManager.AppSettings["PasswordAplicacion"];

    return;
    //6.	Si el usuario no existe, habilitar una pantalla para poder ingresar un nuevo usuario y password.

    //7.	Con los valores de usuario y clave ingresados por el usuario se debe  ejecutar el SP: SP_VALIDAR_USUARIO_SO  que valida si el usuario y clave ingresado son validos, si es valido se debe volver a los puntos 4 y 5.


    //8.	Existe una opción de menú: Cerrar sesión <Nombre de usuario>, que es general para todos los sistemas y la cual no tiene restricciones de permisos. Esta opción permite ingresar al sistema con un nuevo usuario se deben seguir los pasos a partir del punto 6

  }

  ~ValidacionSeguridad()
  {
  }

  private void LeerParametros()
  {
    //5.	Con la función dbo.SF_VALOR_PARAMETRO se recupera los valores de parametrización, tanto generales, de sistema como de usuario. De acuerdo a la cantidad de parámetros que se le pase a la función devolverá parámetros Generales, del sistemas o del usuario:
    //  @IdParametro varchar(100),
    //  @IdPortal int = Null,
    //  @IdSistema int = Null,
    //  @IdUsuario char(20) = Null

    //Estos parámetros se los crean y consultan desde el sistema de seguridad y se almacenan en las siguientes tablas.

    //i.	PARAMETROS_GENERALES
    //ii.	PARAMETROS_SISTEMA
    //iii.	PARAMETROS_USUARIO 

  }


  private bool ExisteUsuarioENDBSeguridad(string UserName)
  {
    try
    {
      string strConnection = GetSecurityConnectionString();
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strConnection);
      conn.Open();

      string cmd = "select dbo.SF_ES_USUARIO_RECONOCIDO('" + UserName + "')";
      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd, conn);

      System.Data.DataSet ds = new System.Data.DataSet();
      da.Fill(ds);

      return (bool)ds.Tables[0].Rows[0][0];
    }
    catch (Exception)
    {
      return false;
    }
  }



  private string GetSecurityConnectionString()
  {
    return "Data Source=" + ServerAplicacion +
          ";Initial Catalog=EframeWorkIT;Persist Security Info=True;User ID=" + UserSeguridad
        + "; pwd=" + PasswordSeguridad;
  }


  private void ObtenerItemsMenu()
  {
    try
    {
      string strConnection = GetSecurityConnectionString();
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strConnection);
      conn.Open();

      string cmd = "select * from sf_menu(NULL, " + NroSistema + ", '" + UserName + "')";
      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd, conn);

      System.Data.DataSet ds = new System.Data.DataSet();
      da.Fill(ds);

      System.Collections.Generic.Dictionary<string, string> menues = new System.Collections.Generic.Dictionary<string, string>();
      foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
      {
        menues[dr["MEN_POSICION"].ToString()] = dr["MEN_ID"].ToString();
      }
      Menues_ = menues;
    }
    catch (Exception)
    {
      Menues_.Clear();
    }
  }

  private static ValidacionSeguridad instance = null;
  public static ValidacionSeguridad Instance
  {
    get
    {
      if (instance == null)
      {
        instance = new ValidacionSeguridad();
      }
      return instance;
    }
  }

  public static void DestroySingleton()
  {
    instance = null;
  }

  public bool UsuarioHabilitado
  {
    get
    {
      if (!UsuarioHabilitado_.HasValue)
      {
        //3.	Validar existencia del usuario en la DB de seguridad Eframework (dbo.SF_ES_USUARIO_RECONOCIDO)
        if (!ExisteUsuarioENDBSeguridad(UserName))
        {
          UsuarioHabilitado_ = false;
          //throw new NotSupportedException("Usuario Invalido");
        }
        else
        {
          UsuarioHabilitado_ = true;
          ObtenerItemsMenu();
          LeerParametros();
        }
      }
      return UsuarioHabilitado_.Value;
    }
  }

  public System.Collections.Generic.Dictionary<string, string> Menues
  {
    get
    {
      return Menues_;
    }
  }
}