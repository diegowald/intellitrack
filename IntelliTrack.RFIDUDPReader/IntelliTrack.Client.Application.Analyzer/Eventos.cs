using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application
{
  public partial class Eventos : frmBaseDockingForm
  {

    

    private Form parentForm;
    public Eventos(Form parent)
    {
      InitializeComponent();
      parentForm = parent;
      IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.UDPDataArrived += new IntelliTrack.Client.Application.Imaging.ComplexMap.DataArrived(DataArrived);
    }

    protected void DataArrived(string FromIP, string message)
    {
     
      if (this.txtTerminal.InvokeRequired)
      {
        IntelliTrack.UDP.UDPClientThread.DataArrived d = new IntelliTrack.UDP.UDPClientThread.DataArrived(DataArrived);
        this.Invoke(d, new object[] { FromIP, message });
        return;
      }

      if (Logging.logInfo.IsInfoEnabled)
        Logging.logInfo.Info(message);

      const int MAXTERMSIZE = 16000;
      int TermSize;

      TermSize = txtTerminal.Text.Length;
      if (TermSize > MAXTERMSIZE)
      {
        txtTerminal.Text = txtTerminal.Text.Substring(0, 4097);
        TermSize = txtTerminal.Text.Length;
      }
      txtTerminal.SelectionStart = TermSize;
      //txtTerminal.Text += message;
      //txtTerminal.SelectedText = FromIP + ": " + message + "\r\n";
      // se elimina el $PRAVE, porque no es necesario presentar esta informacion.
      txtTerminal.SelectedText = message.Replace("$PRAVE,", "$TRACK,") + "\r\n";
      txtTerminal.SelectionStart = txtTerminal.Text.Length;
    }

    public override void RefreshMe()
    {
    }
  }
}