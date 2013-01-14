using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace IntelliTrack.UDP
{
  public class UDPClientThread
  {
    public static log4net.ILog logError = log4net.LogManager.GetLogger("ErrorLogger");

    // Esta clase contiene el Thread y el UDPClient de cada conexion
    protected UdpClient udpClient_;
    protected Thread thread_;
    protected string IP_;
    protected Int32 port_;

    public UdpClient udpClient
    {
      get
      {
        return udpClient_;
      }
    }

    public Thread thread
    {
      get
      {
        return thread_;
      }
    }

    public string IP
    {
      get
      {
        return IP_;
      }
    }

    public Int32 port
    {
      get
      {
        return port_;
      }
    }


    public UDPClientThread(string IP, Int32 port)
    {
      System.Diagnostics.Debug.WriteLine("UDPClientThread");
      IP_ = IP;
      port_ = port;
      try
      {
        udpClient_ = new UdpClient(port_);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        System.Diagnostics.Debug.WriteLine(ex.StackTrace);
        Environment.Exit(-1);
      }
    }

    /*public UDPClientThread(string IP, Int32 port, DataArrived showMessageDelegate)
    {
      IP_ = IP;
      port_ = port;
      udpClient_ = new UdpClient(port_);
      Start(showMessageDelegate);
    }*/

    public delegate void DataArrived(string FromIP, string message);

    public DataArrived myDelegate;

    public void Start(DataArrived showMessageDelegate)
    {
      string szData = "\r\n";
      byte[] byData = System.Text.Encoding.ASCII.GetBytes(szData);

      myDelegate = new DataArrived(showMessageDelegate);
      thread_ = new Thread(new ThreadStart(ReceiveMessage));
      thread.IsBackground = true;

      IPAddress ip = IPAddress.Parse(IP_);
      IPEndPoint ipEndPoint = new IPEndPoint(ip, port_);

      udpClient.Send(byData, byData.Length, ipEndPoint);

      thread.Start();
    }

    private string infoRecibida = "";
    private void ReceiveMessage()
    {
        // DIW
        
      /*for (int i = 1; i < 10000; i++)
      {
        infoRecibida = "$PRAVE,0002,0001,3308.9037,-11713.1167,165919,2,9,184,25,12.7,0,-95,0,0,,*76";
        myDelegate(IP_, infoRecibida);
        System.Threading.Thread.Sleep(500);
      }*/
        // /DIW
      while (true)
      {
        /* La informacion llega de a rafagas de bytes,
         * por lo que es necesario verificar que se recibe informacion, 
         * luego añadirla a la lista de caracteres a procesar,
         * detectar el fin de linea \r\n y recien ahi enviar el primer renglon 
         * para procesamiento de la informacion en la aplicacion.
         * Luego se quita del buffer la informacion enviada y se repite el proceso...
         */
        try
        {
          IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, port);
          byte[] content = udpClient.Receive(ref remoteIPEndPoint);
          if (content.Length > 0)
          {
            infoRecibida += Encoding.ASCII.GetString(content);
            string[] lineas = infoRecibida.Split(new string[] { "\r\n", }, StringSplitOptions.None);
            if (lineas.Length > 1)
            {
              infoRecibida = "";
              string[] Comandos = lineas[0].Split('$');
              for (int i = 0; i < Comandos.Length - 1; i++)
              {
                if (Comandos[i].Length > 0)
                {
                  if (myDelegate != null)
                    myDelegate(IP_, "$" + Comandos[i]);
                  //myDelegate(IP_, lineas[0]);
                }
              }
              infoRecibida += "$" + Comandos[Comandos.Length - 1];
              infoRecibida += lineas[1];
            }
          }
        } catch(Exception ex)
        {
          logError.Error(ex.Message, ex);
        }
      }
    }

    public void CloseAndAbort()
    {
      udpClient_.Close();
      if (thread != null)
      {
        thread_.Abort();
      }
    }

  }
}
