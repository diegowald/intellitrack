using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace UDP_IT
{
  public partial class Form1 : Form
  {

    //public delegate void ShowMessage(string message);
    //public ShowMessage myDelegate;
    //Int32 port = 1420;
    //UdpClient udpClient = new UdpClient(1420);
    //Thread thread;
    System.Collections.Generic.Dictionary<string, IntelliTrack.UDP.UDPClientThread> UDPThreads;

    public Form1()
    {
      InitializeComponent();

      UDPThreads = new Dictionary<string, IntelliTrack.UDP.UDPClientThread>();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      // Aca debo leer la cantidad de RFs a monitorear y configurarlos.

      UDPThreads.Add("UDP1", new IntelliTrack.UDP.UDPClientThread("192.168.1.20", 1420));//, ShowMessageMethod));

      UDPThreads.Add("LEDE1", new IntelliTrack.UDP.UDPClientThread("10.2.40.21", 1421));//, ShowMessageMethod));
      UDPThreads.Add("LEDE2", new IntelliTrack.UDP.UDPClientThread("10.2.40.22", 1422));//, ShowMessageMethod));
      UDPThreads.Add("LEDE3", new IntelliTrack.UDP.UDPClientThread("10.2.40.23", 1423));//, ShowMessageMethod));
      UDPThreads.Add("LEDE4", new IntelliTrack.UDP.UDPClientThread("10.2.40.24", 1424));//, ShowMessageMethod));
      UDPThreads.Add("LEDE5", new IntelliTrack.UDP.UDPClientThread("10.2.40.25", 1425));//, ShowMessageMethod));
      UDPThreads.Add("LEDE6", new IntelliTrack.UDP.UDPClientThread("10.2.40.26", 1426));//, ShowMessageMethod));
      UDPThreads.Add("LEDE7", new IntelliTrack.UDP.UDPClientThread("10.2.40.27", 1427));//, ShowMessageMethod));
      
      StartReading();
    }

    private void StartReading()
    {
      foreach (IntelliTrack.UDP.UDPClientThread th in UDPThreads.Values)
      {
        th.Start(ShowMessageMethod);
      }
    }

    private void ShowMessageMethod(string FromIP, string message)
    {
        if (this.txtTerminal.InvokeRequired)
        {
          IntelliTrack.UDP.UDPClientThread.DataArrived d = new IntelliTrack.UDP.UDPClientThread.DataArrived(ShowMessageMethod);
            this.Invoke(d, new object[] { FromIP, message });
            return;
        }

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
      txtTerminal.SelectedText = FromIP + ": " + message + "\r\n";
      txtTerminal.SelectionStart = txtTerminal.Text.Length;

    }

    private void btnSalir_Click(object sender, EventArgs e)
    {
      foreach (IntelliTrack.UDP.UDPClientThread udp in UDPThreads.Values)
      {
        udp.CloseAndAbort();
      }
      this.Close();
    }
  }
}
