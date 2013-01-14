using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UDP_IT
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      //if (ValidacionSeguridad.Instance.UsuarioHabilitado)
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
      }
      //ValidacionSeguridad.DestroySingleton();
    }
  }
}
