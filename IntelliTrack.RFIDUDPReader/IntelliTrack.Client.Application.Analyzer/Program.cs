using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IntelliTrack.Client.Application
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string [] Args)
    {
/*#if(aDEBUG)
      Win32.AllocConsole();
#endif*/
      try
      {
        if (ValidacionSeguridad.Instance.UsuarioHabilitado)
        {
          SplashScreen.SplashScreen.ShowSplashScreen();
          //System.Drawing.Html.HtmlRenderer.References.Add(typeof(IntelliTrack.Client.Application.HTMLFormatting.HtmlStyle).Assembly);
          System.Windows.Forms.Application.EnableVisualStyles();
          //System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
          SplashScreen.SplashScreen.SetStatus("Cargando ventana principal");
          System.AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
          System.Windows.Forms.Application.Run(new Form1());
        }
      }
      catch (Exception ex)
      {
        SplashScreen.SplashScreen.CloseForm();
        Logging.logError.Error(ex.Message, ex);
#if(DEBUG)
        MessageBox.Show(ex.Message);
#endif
      }
      finally
      {
/*#if(DEBUG)
        Win32.FreeConsole();
#endif*/
      }
    }

    static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {
      IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.Stop();
    }
  }

/*
#if (DEBUG)
  public class Win32
  {
    [DllImport("kernel32.dll")]
    public static extern Boolean AllocConsole();
    [DllImport("kernel32.dll")]
    public static extern Boolean FreeConsole();
  }
#endif
*/
}