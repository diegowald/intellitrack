using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.RFIDUDPReader
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    protected IntelliTrackUDPClient client;

    private void MainForm_Load(object sender, EventArgs e)
    {
      client  =new IntelliTrackUDPClient();
      client.ClientPort = 0;
      client.Encode = EncodingType.ASCII;
      client.Protocol = System.Net.Sockets.ProtocolType.Udp;
      client.OnAfterReceive += new IntelliTrackUDPClient.AfterReceive(client_OnAfterReceive);
    }


    private void button1_Click(object sender, EventArgs e)
    {
      int port = 0;
      if (int.TryParse(maskedTextBox1.Text, out port))
        client.ClientPort= port;
      string RemoteIP = textBox2.Text;
      int RemotePort = 0;

      if ((RemoteIP != "") && (int.TryParse(textBox3.Text, out RemotePort)))
      {
        client.Server = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(RemoteIP), RemotePort);
      }
      client.Start();
      button2.Enabled=true;
      button1.Enabled=false;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      client.Stop();
      button1.Enabled = true;
      button2.Enabled = false;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      client.Stop();
      button1.Enabled = true;
      button2.Enabled = false;
      client = new IntelliTrackUDPClient();
      client.ClientPort = 8088;
      client.Encode = EncodingType.ASCII;
      client.Protocol = System.Net.Sockets.ProtocolType.Udp;
    }

    
    void client_OnAfterReceive()
    {
      if (this.InvokeRequired)
        this.Invoke(new _DisplayMessage(ref DisplayMessage));
      else
        DisplayMessage();
    }

    private delegate void _DisplayMessage();

    private void DisplayMessage()
    {
      this.textBox1.Text += client.Message + '\n';
    }


  }
}