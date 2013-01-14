using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.RFIDUDPReader
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}
