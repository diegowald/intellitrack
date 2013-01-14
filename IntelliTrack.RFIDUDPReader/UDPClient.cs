using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace IntelliTrack.RFIDUDPReader
{
  public partial class IntelliTrackUDPClient : Component
  {
    public IntelliTrackUDPClient()
    {
      InitializeComponent();
    }

    public IntelliTrackUDPClient(IContainer container)
    {
      container.Add(this);

      InitializeComponent();
    }

    // Default Property values
    private ProtocolType _Protocol = ProtocolType.Udp;
    private Thread _ThreadReceive;
    private int _ClientPort = 0;
    private string _Message = "";
    private EncodingType _Encode = EncodingType.Default;
    private System.Net.Sockets.UdpClient _UDPClient;
    private IPEndPoint _Server = new IPEndPoint(IPAddress.Any, 0);
    private int _BytesReceived = 0;

    public delegate void BeforeReceive();
    [Browsable(true), Category("UDPReader"), Description("This event is fired right before an inbound message is received.")]
    public event BeforeReceive OnBeforeReceive;


    public delegate void AfterReceive();
    [Browsable(true), Category("UDPReader"), Description("This event is fired immediatley after an inbound message is received.")]
    public event AfterReceive OnAfterReceive;

    [Browsable(true), Category("UDPReader"), Description("The encoding to use with received messages.")]
    public EncodingType Encode
    {
      get
      {
        return _Encode;
      }
      set
      {
        _Encode = value;
      }
    }


    [Browsable(true), Category("UDPReader"), Description("The number of bytes received by the client in the most recent message.")]
    public int BytesReceived
    {
      get
      {
        return _BytesReceived;
      }
    }

    [Browsable(true), Category("UDPReader"), Description("Read Only.  The message received by the client.")]
    public string Message
    {
      get
      {
        return _Message;
      }
    }

    [Browsable(true), Category("UDPReader"), Description("The server IPEndPoint.")]
    public IPEndPoint Server
    {
      get
      {
        return _Server;
      }
      set
      {
        _Server = value;
      }
    }

    [Browsable(true), Category("UDPReader"), Description("The client port to check for inbound data.")]
    public int ClientPort
    {
      get
      {
        return _ClientPort;
      }
      set
      {
        _ClientPort = value;
      }
    }

    [Browsable(true), Category("UDPReader"), Description("The client protocol to use. Currently only UDP is supported.")]
    public ProtocolType Protocol
    {
      get
      {
        return _Protocol;
      }
      set
      {
        _Protocol = value;
      }
    }

    public void Receive()
    {
      // Clear the Message property
      _Message = "";
      // Raise the BeforeReceive event
      if (OnBeforeReceive != null)
        OnBeforeReceive();
      // Receive our UDP data
      byte[] _data;
      try
      {
        switch (Protocol)
        {
          case ProtocolType.Udp:
            {
              _data = _UDPClient.Receive(ref _Server);
              _BytesReceived = _data.Length;
              break;
            }
          default:
            throw new ProtocolNotSupportedException();
        }
      }
      catch (System.Exception ex)
      {
        throw (ex);
      }
      finally
      {
        // The thread finished blocking, and ended, so we start again
        InitializeThread();
      }
      // Encode the data per the Encode property
      string _strdata;
      switch (Encode)
      {
        case EncodingType.Default:
          _strdata = System.Text.Encoding.Default.GetString(_data);
          break;
        case EncodingType.ASCII:
          _strdata = System.Text.Encoding.ASCII.GetString(_data);
          break;
        case EncodingType.Unicode:
          _strdata = System.Text.Encoding.Unicode.GetString(_data);
          break;
        case EncodingType.UTF7:
          _strdata = System.Text.Encoding.UTF7.GetString(_data);
          break;
        case EncodingType.UTF8:
          _strdata = System.Text.Encoding.UTF8.GetString(_data);
          break;
        default:
          throw new BadEncodingException();
      }
      // Set the message
      _Message = _strdata;
      // Raise the AfterReceive event
      if (OnAfterReceive != null)
        OnAfterReceive();
    }

    private void InitializeClient()
    {
      // Configure a UDPClient
      switch (Protocol)
      {

        case ProtocolType.Udp:
          {
            if (_UDPClient == null)
            {
              _UDPClient = new UdpClient(ClientPort);
            }
            break;
          }
        default:
          throw new ProtocolNotSupportedException();
      }
    }

    private void InitializeThread()
    {
      // Start a worker thread
      try
      {
        _ThreadReceive = new Thread(Receive);
        _ThreadReceive.Start();
      }
      catch (System.Exception ex)
      {
        throw ex;
      }
    }

    public void Start()
    {
      // Initialize the Client and the Thread
      InitializeClient();
      InitializeThread();
    }

    public void Stop()
    {
      // Close the UDPClient and stop the worker thread
      try
      {
        // Suspend the thread and then abort it.  Keeps it from
        // continuing to try to process anything further while
        // it winds down.
        _ThreadReceive.Suspend();
        _ThreadReceive.Abort();
        if (_UDPClient != null)
        {
          //Close the UDPClient and then force it to Nothing
          _UDPClient.Close();
          _UDPClient = null;
        }
      }
      catch (System.Exception ex)
      {
        throw ex;
      }
    }

  }
}
