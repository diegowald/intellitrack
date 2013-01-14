using System;
using System.Text;
using System.Configuration;


public class ValidacionSeguridad
{

  private const string Aplicacion = "analisis";

  private enum TiposParametros
  {
    ValorTexto,
    ValorEntero,
    ValorDecimal,
    ValorLogico,
    ValorFechaHora
  };

  private bool UsuarioHabilitado_;
  protected string UserName;
  protected int NroSistema;
  protected string ServerSeguridad;
  protected string UserSeguridad;
  protected string PasswordSeguridad;
  protected string DatabaseSeguridad;
  protected string ServerAplicacion;
  protected string UserAplicacion;
  protected string PasswordAplicacion;
  protected string DatabaseAplicacion;
  private string URL_;

  protected System.Collections.Generic.Dictionary<string, IntelliTrack.Client.Application.Menues.MenuItem> Menues_;
  private ValidacionSeguridad()
  {
    //ValidacionCompleja();
    ValidacionSencilla();
  }

  private void ValidacionCompleja()
  {
    Write2DB_ = false;
    CategoriaUsuario_ = -1;
    //1.	Obtener el usuario de Red (vbScript)
    UserName = Environment.UserName;
    //2.	Obtener desde un archivo de configuración los siguientes parámetros:
    //a.	Número de sistema
    //b.	Nombre del servidor, usuario y clave de conexión a la DB de Seguridad 
    //c.	Nombre del servidor, usuario y clave de conexión a la DB de la Aplicación.
    if (!int.TryParse(ConfigurationManager.AppSettings["NumeroSistema"], out NroSistema))
    {
      NroSistema = -1;
      throw new ConfigurationErrorsException("Config error: NumeroSistema debe ser numerico");
    }

    ServerSeguridad = ConfigurationManager.AppSettings["ServerSeguridad"];
    UserSeguridad = ConfigurationManager.AppSettings["UserSeguridad"];
    PasswordSeguridad = ConfigurationManager.AppSettings["PasswordSeguridad"];
    DatabaseSeguridad = ConfigurationManager.AppSettings["DatabaseSeguridad"];

    ServerAplicacion = ConfigurationManager.AppSettings["ServerAplicacion"];
    UserAplicacion = ConfigurationManager.AppSettings["UserAplicacion"];
    PasswordAplicacion = ConfigurationManager.AppSettings["PasswordAplicacion"];
    DatabaseAplicacion = ConfigurationManager.AppSettings["DatabaseAplicacion"];

    bool UsuarioValidado = false;
    if (EsServidorActivo())
    {
      Write2DB_ = true;
      UsuarioValidado = true;
      CategoriaUsuario_ = 99;
      UsuarioHabilitado_ = true;
    }
    else
    {
      if (ExisteUsuarioENDBSeguridad(UserName))
        UsuarioValidado = true;
      else
      {
        UsuarioValidado = IntentarDesdeLogin();
      }
    }
    if (!UsuarioHabilitado_)
    {
      if (UsuarioValidado)
      {
        if (AccesoAplicacion())
        {
          UsuarioHabilitado_ = true;
        }
        else
        {
          UsuarioHabilitado_ = false;
          System.Windows.Forms.MessageBox.Show(
            "El usuario " + UserName +
            " no tiene definido el atributo 'Acceso a la aplicación'. " +
            "\r\n" +
            "Comuníquese con el administrador.",
            "Ingreso de usuario", System.Windows.Forms.MessageBoxButtons.OK,
             System.Windows.Forms.MessageBoxIcon.Exclamation);
        }
      }
      else
      {
        UsuarioHabilitado_ = false;
        throw new NotSupportedException("Usuario Invalido");
      }


    }

    if (UsuarioHabilitado)
    {
      ObtenerItemsMenu();
      LeerParametros();
    }
    string s = "Write2DB: " + Write2DB_.ToString() + "\n\r" +
  " Usuario Habilitado: " + UsuarioHabilitado_.ToString() + "\n\r" +
  " Categoria: " + CategoriaUsuario_.ToString() + "\n\r" +
  " Nombre Usuario: " + UserName + "\n\r" +
  " Nombre Maquina: " + Environment.MachineName + "\n\r" +
  " Nombre Server: " + (string)LeerParametro("Server", TiposParametros.ValorTexto, false);

    System.Windows.Forms.MessageBox.Show(s);
  }

  private void ValidacionSencilla()
  {
    Write2DB_ = false;
    CategoriaUsuario_ = -1;
    //1.	Obtener el usuario de Red (vbScript)
    //2.	Obtener desde un archivo de configuración los siguientes parámetros:
    //a.	Número de sistema
    //b.	Nombre del servidor, usuario y clave de conexión a la DB de Seguridad 
    //c.	Nombre del servidor, usuario y clave de conexión a la DB de la Aplicación.
    if (!int.TryParse(ConfigurationManager.AppSettings["NumeroSistema"], out NroSistema))
    {
      NroSistema = -1;
      throw new ConfigurationErrorsException("Config error: NumeroSistema debe ser numerico");
    }

    ServerSeguridad = ConfigurationManager.AppSettings["ServerSeguridad"];
    UserSeguridad = ConfigurationManager.AppSettings["UserSeguridad"];
    PasswordSeguridad = ConfigurationManager.AppSettings["PasswordSeguridad"];
    DatabaseSeguridad = ConfigurationManager.AppSettings["DatabaseSeguridad"];

    ServerAplicacion = ConfigurationManager.AppSettings["ServerAplicacion"];
    UserAplicacion = ConfigurationManager.AppSettings["UserAplicacion"];
    PasswordAplicacion = ConfigurationManager.AppSettings["PasswordAplicacion"];
    DatabaseAplicacion = ConfigurationManager.AppSettings["DatabaseAplicacion"];

    bool UsuarioValidado = false;
    if (EsServidorActivo())
    {
      Write2DB_ = true;
      UsuarioValidado = true;
      CategoriaUsuario_ = 99;
      UsuarioHabilitado_ = true;
    }
    else
    {
      UsuarioValidado = IntentarDesdeLogin();
    }
    if (!UsuarioHabilitado_)
    {
      if (UsuarioValidado)
      {
        if (AccesoAplicacion())
        {
          UsuarioHabilitado_ = true;
        }
        else
        {
          UsuarioHabilitado_ = false;
          System.Windows.Forms.MessageBox.Show(
            "El usuario " + UserName +
            " no tiene definido el atributo 'Acceso a la aplicación'. " +
            "\r\n" +
            "Comuníquese con el administrador.",
            "Ingreso de usuario", System.Windows.Forms.MessageBoxButtons.OK,
             System.Windows.Forms.MessageBoxIcon.Exclamation);
        }
      }
      else
      {
        UsuarioHabilitado_ = false;
        throw new NotSupportedException("Usuario Invalido");
      }


    }

    if (UsuarioHabilitado)
    {
      ObtenerItemsMenu();
      LeerParametros();
    }
    /*    string s = "Write2DB: " + Write2DB_.ToString() + "\n\r" +
      " Usuario Habilitado: " + UsuarioHabilitado_.ToString() + "\n\r" +
      " Categoria: " + CategoriaUsuario_.ToString() + "\n\r" +
      " Nombre Usuario: " + UserName + "\n\r" +
      " Nombre Maquina: " + Environment.MachineName + "\n\r" +
      " Nombre Server: " + (string)LeerParametro("Server", TiposParametros.ValorTexto, false);

        System.Windows.Forms.MessageBox.Show(s);*/
  }

  private bool EsServidorActivo()
  {
    ///Diego
    //return true;
    ///Diego
    string ServerName = (string)LeerParametro("Server", TiposParametros.ValorTexto, false);
    string CurrentMachineName = System.Environment.MachineName;
    return ServerName.Trim().ToLower() == CurrentMachineName.Trim().ToLower();
  }

  private bool AccesoAplicacion()
  {
    if (Write2DB_)
      return true;

    string Valor = (string)LeerParametro("acceso_aplicacion", TiposParametros.ValorTexto, true);
    bool resultado = false;
    if (Valor == null)
      return false;
    switch (Valor.ToLower())
    {
      case "monitoreo":
        resultado = false;
        break;
      case "analisis":
        resultado = true;
        break;
      case "todos":
        resultado = true;
        break;
      default:
        resultado = false;
        break;
    }
    return resultado;
  }

  private bool IntentarDesdeLogin()
  {
    bool validado = false;

    IntelliTrack.Client.Application.dlgLogin dlglogin = new IntelliTrack.Client.Application.dlgLogin();
    bool reintentar = true;
    while (reintentar)
    {
      if (dlglogin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        // El usuario ingreso un user y password, se intentara validar con este usuario
        if (IntentarValidarUsuario(dlglogin.UserName, dlglogin.Password))
        {
          validado = true;
          UserName = dlglogin.UserName;
          reintentar = false;
        }
        else
        {
          reintentar = true;
          System.Windows.Forms.MessageBox.Show("Usuario o clave inválido",
            "Ingreso al sistema",
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Exclamation);
        }
      }
      else
      {
        // El usuario pulso cancelar... no esta habilitada la sesion.
        // Se debe cerrar el programa.
        reintentar = false;
      }
    }
    return validado;
  }

  private bool IntentarValidarUsuario(string UserName, string Password)
  {
    // Hasta el dia 18 de abril de 2011 int ResultadoParaUsuarioValidado = -1;
    // A partir de esa fecha, int ResultadoParaUsuarioValidado = 1;
    int ResultadoParaUsuarioValidado = 1;
    string strConnection = GetSecurityConnectionString();
    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strConnection);
    bool resultado = false;
    try
    {
      conn.Open();

      string cmdText = "DECLARE @ret int;";
      cmdText += " exec @ret = SP_VALIDAR_USUARIO_SO @idUsuario, @Contraseña, @IntegracionOS;";
      cmdText += "SELECT @ret;";
      System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(cmdText, conn);
      cmd.CommandType = System.Data.CommandType.Text;
      cmd.Parameters.AddWithValue("@idUsuario", UserName);
      cmd.Parameters.AddWithValue("@Contraseña", Password);
      cmd.Parameters.AddWithValue("@IntegracionOS", "NW");
      object res = cmd.ExecuteScalar();
      if (res == null)
        resultado = false;
      else
        resultado = ((int)res == ResultadoParaUsuarioValidado ? true : false);
    }
    catch (Exception ex)
    {
      IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
      resultado = false;
    }
    finally
    {
      conn.Close();
    }
    return resultado;
  }

  private object GetValor(System.Data.DataRow dr, string tipoParametro)
  {
    if (dr[tipoParametro] is System.DBNull)
      return null;
    else
      return dr[tipoParametro];
  }

  private object LeerParametro(string Parametro, TiposParametros tipoParametro, bool IsUserParameter)
  {
    object Valor = null;
    string strConnection = GetSecurityConnectionString();

    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strConnection);
    try
    {
      conn.Open();

      string cmdText = "";
      System.Data.SqlClient.SqlCommand cmd = null;
      if (IsUserParameter)
      {
        cmdText = "select * from SF_VALOR_PARAMETRO(@IdParametro, null, @IdSistema, @IdUsuario)";
        cmd = new System.Data.SqlClient.SqlCommand(cmdText, conn);
        cmd.Parameters.AddWithValue("@IdUsuario", UserName);
      }
      else
      {
        cmdText = "select * from SF_VALOR_PARAMETRO(@IdParametro, null, @IdSistema, null)";
        cmd = new System.Data.SqlClient.SqlCommand(cmdText, conn);
      }
      cmd.CommandType = System.Data.CommandType.Text;
      cmd.Parameters.AddWithValue("@IdParametro", Parametro);
      cmd.Parameters.AddWithValue("@IDSistema", NroSistema);

      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);


      System.Data.DataSet ds = new System.Data.DataSet();
      da.Fill(ds);
      if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
      {
        switch (tipoParametro)
        {
          case TiposParametros.ValorDecimal:
            Valor = GetValor(ds.Tables[0].Rows[0], "ValorDecimal");
            break;
          case TiposParametros.ValorEntero:
            Valor = GetValor(ds.Tables[0].Rows[0], "ValorEntero");
            break;
          case TiposParametros.ValorFechaHora:
            Valor = GetValor(ds.Tables[0].Rows[0], "ValorFechaHora");
            break;
          case TiposParametros.ValorLogico:
            Valor = GetValor(ds.Tables[0].Rows[0], "ValorLogico");
            break;
          case TiposParametros.ValorTexto:
            Valor = GetValor(ds.Tables[0].Rows[0], "ValorTexto");
            break;
          default:
            Valor = null;
            break;
        }
      }
    }
    catch (Exception ex)
    {
      IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
      Valor = null;
    }
    finally
    {
      conn.Close();
    }
    return Valor;
  }

  private void LeerParametros()
  {
    // Obtengo la categoria del usuario
    if (CategoriaUsuario_ == -1)
      CategoriaUsuario_ = (int)LeerParametro("categoria", TiposParametros.ValorEntero, true);
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
      conn.Close();
      return (bool)ds.Tables[0].Rows[0][0];
    }
    catch (Exception)
    {
      return false;
    }
  }



  public string GetSecurityConnectionString()
  {
    ///diego
    //return @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\EframeWorkIT.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
    //return @"Data Source=xp_vmware;Initial Catalog=EFrameworkit;Integrated Security=True";
    ///diego
    return "Data Source=" + ServerSeguridad +
          ";Initial Catalog=" + DatabaseSeguridad +
          ";Persist Security Info=True;User ID=" + UserSeguridad
        + "; pwd=" + PasswordSeguridad;
  }

  public string GetApplicationConnectionString()
  {
    ///diego
    //return @"Data Source=PC-BHB-02\SQLEXPRESS;Initial Catalog=SIG;Integrated Security=True";
    //return @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\SIGAnalisis1.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
    //return @"Data Source=pc-bhb-02\sqlexpress;Initial Catalog=SIG;Integrated Security=True;Network Library=dbnmpntw";
    //return @"Data Source=xp_vmware;Initial Catalog=SIGAnalisis1;Integrated Security=True";
    ///diego
    return "Data Source=" + ServerAplicacion +
         ";Initial Catalog=" + DatabaseAplicacion +
          ";Persist Security Info=True;User ID=" + UserAplicacion
       + "; pwd=" + PasswordAplicacion;
  }

  //private void ObtenerItemsMenu()
  //{
  //  string strConnection = GetSecurityConnectionString();
  //  System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strConnection);
  //  Menues_ = new System.Collections.Generic.Dictionary<string, IntelliTrack.Client.Application.Menues.MenuItem>();
  //  try
  //  {
  //    conn.Open();

  //    string cmd = "select * from sf_menu(NULL, " + NroSistema + ", '" + UserName + "')";
  //    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd, conn);

  //    System.Data.DataSet ds = new System.Data.DataSet();
  //    da.Fill(ds);

  //    System.Collections.Generic.Dictionary<string, IntelliTrack.Client.Application.Menues.MenuItem> menues = new System.Collections.Generic.Dictionary<string, IntelliTrack.Client.Application.Menues.MenuItem>();
  //    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
  //    {
  //      IntelliTrack.Client.Application.Menues.MenuItem menu = new IntelliTrack.Client.Application.Menues.MenuItem(dr);
  //      menues[dr["MEN_POSICION"].ToString()] = menu;
  //    }
  //    Menues_ = menues;
  //  }
  //  catch (Exception)
  //  {
  //    Menues_.Clear();
  //  }
  //  finally
  //  {
  //    conn.Close();
  //  }
  //}

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

  public bool UsuarioHabilitado
  {
    get
    {
      return UsuarioHabilitado_;
    }
  }

  public string NombreUsuario
  {
    get
    {
      return UserName;
    }
  }

  public System.Collections.Generic.Dictionary<string, IntelliTrack.Client.Application.Menues.MenuItem> Menues
  {
    get
    {
      return Menues_;
    }
  }

  public int NumeroSistema
  {
    get
    {
      return NroSistema;
    }
  }

  private bool Write2DB_;
  public bool Write2DB
  {
    get
    {
      return Write2DB_;
    }
    set
    {
      Write2DB_ = value;
    }
  }

  private int CategoriaUsuario_;
  public int CategoriaUsuario
  {
    get
    {
      return CategoriaUsuario_;
    }
  }

  public static string TipoAplicacion
  {
    get
    {
      return Aplicacion;
    }
  }

  public string URL
  {
    get
    {
      return URL_;
    }
  }

  private void ObtenerItemsMenu()
  {
    string s1 = GetSecurityConnectionString();
    System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(s1);
    Menues_ = new System.Collections.Generic.Dictionary<string, IntelliTrack.Client.Application.Menues.MenuItem>();
    try
    {
      sqlConnection.Open();
      string s2 = "select * from sf_menu(NULL, " + NroSistema + ", '" + UserName + "') ORDER BY MEN_POSICION";
      System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(s2, sqlConnection);
      System.Data.DataSet dataSet = new System.Data.DataSet();
      sqlDataAdapter.Fill(dataSet);
      System.Collections.Generic.Dictionary<string, IntelliTrack.Client.Application.Menues.MenuItem> dictionary = new System.Collections.Generic.Dictionary<string, IntelliTrack.Client.Application.Menues.MenuItem>();
      foreach (System.Data.DataRow dataRow in dataSet.Tables[0].Rows)
      {
        IntelliTrack.Client.Application.Menues.MenuItem menuItem = new IntelliTrack.Client.Application.Menues.MenuItem(dataRow);
        dictionary[dataRow["MEN_NUMERO"].ToString()] = menuItem;
      }
      Menues_ = dictionary;
    }
    catch (System.Data.SqlClient.SqlException e)
    {
      System.Windows.Forms.MessageBox.Show("Error de conexion a la base de datos.", "Error", System.Windows.Forms.MessageBoxButtons.OK);
      IntelliTrack.Client.Application.Logging.logError.Error("Error al intentar acceder a la base de datos de seguridad", e);
    }
    catch (System.Exception)
    {
      Menues_.Clear();
    }
    finally
    {
      sqlConnection.Close();
    }
  }

} // class ValidacionSeguridad


