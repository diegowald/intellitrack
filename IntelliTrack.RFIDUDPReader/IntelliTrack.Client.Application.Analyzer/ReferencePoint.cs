using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Client.Application
{
  public class ReferencePoint
  {
    protected double lat;
    protected double lon;
    protected bool _IsDefined;

    private ReferencePoint()
    {
      if (System.Configuration.ConfigurationManager.AppSettings["CenterLatitude"] == "" || System.Configuration.ConfigurationManager.AppSettings["CenterLongitude"] == "")
      {
        _IsDefined = false;
      }
      else
      {
        IntelliTrack.Number.DDMMdotMMMM nro = new IntelliTrack.Number.DDMMdotMMMM(System.Configuration.ConfigurationManager.AppSettings["CenterLatitude"]);
        lat = nro.GetValue(0);
        nro = new IntelliTrack.Number.DDMMdotMMMM(System.Configuration.ConfigurationManager.AppSettings["CenterLongitude"]);
        lon = nro.GetValue(0);
        _IsDefined = true;
      }
    }

    public double DistGrados(double Lat, double Lon)
    {
      if (_IsDefined)
      {
        return System.Math.Sqrt(System.Math.Pow(lat - Lat, 2.0) + System.Math.Pow(lon - Lon, 2.0));
      }
      else
        return double.NaN;
    }


    private static ReferencePoint Instance_=null;
    public static ReferencePoint Instance
    {
      get
      {
        if (Instance_ == null)
        {
          Instance_ = new ReferencePoint();
        }
        return Instance_;
      }
    }
  }
}
