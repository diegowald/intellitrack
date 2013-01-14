using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Client.Application
{
  public class UTCTime
  {
    protected static volatile int Anio;
    protected static volatile int Mes;
    protected static volatile int Dia;
    protected int Hora;
    protected int Minuto;
    protected int Segundo;

    public UTCTime()
    {
    }

    public UTCTime(string time)
    {
      Hora = 0;
      Minuto = 0;
      Segundo = 0;
      int aux;
      if (int.TryParse(time, out aux))
      {
        if (aux > 235959)
          return;
        Segundo = aux % 100;
        aux = aux / 100;
        Minuto = aux % 100;
        aux = aux / 100;
        Hora = aux;
      }
    }

    public UTCTime(string date, string time)
    {
      int.TryParse(date.Substring(4, 2), out Anio);
      Anio += 2000;

      int.TryParse(date.Substring(2, 2), out Mes);

      int.TryParse(date.Substring(0, 2), out Dia);

      int.TryParse(time.Substring(0, 2), out Hora);

      int.TryParse(time.Substring(2, 2), out Minuto);

      int.TryParse(time.Substring(4, 2), out Segundo);
    }

    public UTCTime(int hora, int minuto, int segundo)
    {
      Hora = hora;
      Minuto = minuto;
      Segundo = segundo;
    }

    public UTCTime(int anio, int mes, int dia, int hora, int minuto, int segundo)
    {
      Anio = anio;
      Mes = mes;
      Dia = dia;
      Hora = hora;
      Minuto = minuto;
      Segundo = segundo;
    }

    public override string ToString()
    {
      return Dia.ToString("00") + "/" + Mes.ToString("00") + "/" + Anio.ToString("0000") + " " +
          Hora.ToString("00") + ":" + Minuto.ToString("00") + ":" + Segundo.ToString("00");
    }

    public DateTime ToDateTime()
    {
      if (Anio != 0)
        return new DateTime(Anio, Mes, Dia, Hora, Minuto, Segundo, 0, DateTimeKind.Utc);
      else
      {
        try
        {
          DateTime dt = new DateTime(DateTime.Today.ToUniversalTime().Year,
            DateTime.Today.ToUniversalTime().Month,
            DateTime.Today.ToUniversalTime().Day,
            Hora, Minuto, Segundo, 0, DateTimeKind.Utc);
          return dt;
        }
        catch (Exception ex)
        {
          return DateTime.Today.ToUniversalTime();
        }
      }
    }

    public string ToSmallDateTime()
    {
      return Anio.ToString() + "-" + Mes.ToString() + "-" + Dia.ToString() + " " + Hora.ToString() + ":" + Minuto.ToString() + ":00";
    }
  }
}
