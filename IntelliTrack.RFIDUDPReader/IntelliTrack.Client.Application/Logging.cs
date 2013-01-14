using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Client.Application
{
  public static class Logging
  {
    public static log4net.ILog logError = log4net.LogManager.GetLogger("ErrorLogger");
    public static log4net.ILog logInfo = log4net.LogManager.GetLogger("Eventos");
  }
}
