using System;
using System.Data;
using System.Configuration;
using System.Web;

namespace IntellTrack.UDPRFReader
{
  public class UDPReader
  {
    public delegate void DataArrived(string FromIP, string data);
    public event DataArrived DataArrivedHandler;

    public UDPReader()
    {
    }

    public void Run()
    {
      for (int i = 1; i < 100000; i++)
      {
        if (DataArrivedHandler != null)
          DataArrivedHandler("0.0.0.0", i.ToString());
      }
    }
  }
}
