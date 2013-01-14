using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IntelliTrack.RFIDUDPReader
{
  using System;
  using System.Net;
  using System.Net.Sockets;
  using System.Text;

  public class UdpClientMultiRecv
  {
    public static void Main()
    {
      UdpClient sock = new UdpClient(9050);
      Console.WriteLine("Ready to receive...");

      sock.JoinMulticastGroup(IPAddress.Parse("192.100.0.1"), 50);
      IPEndPoint iep = new IPEndPoint(IPAddress.Any, 0);
      byte[] data = sock.Receive(ref iep);
      string stringData = Encoding.ASCII.GetString(data, 0, data.Length);
      Console.WriteLine("received: {0}  from: {1}", stringData, iep.ToString());
      sock.Close();
    }
  }
}