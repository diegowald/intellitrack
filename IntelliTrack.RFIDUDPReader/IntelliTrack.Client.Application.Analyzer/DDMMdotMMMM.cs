using System;
using System.Collections.Generic;
using System.Text;


namespace IntelliTrack.Number
{
  public class DDMMdotMMMM 
  {
    // private members
    private double _value;

    // public constructors
    public DDMMdotMMMM()
    {
      _value = 0;
   } 

    public DDMMdotMMMM(string s)
    {
      // Format
      // -DDDMM.MMMMM 
      double value = 0;
      int signo = 0;
      int grados = 0;
      double minutos = 0;
      /*string aux = s.Replace(".", ",");
      if (System.Double.TryParse(aux, out value))*/
      if (System.Double.TryParse(s, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out value))
      {
        signo = System.Math.Sign(value);
        value = System.Math.Abs(value);
        grados = (int)System.Math.Floor(value / 100);
        minutos = value - grados * 100.0;
        _value = signo * (grados + minutos / 60);
      }
      else
      {
        throw new NotFiniteNumberException("Invalid number");
      }
    }

    public DDMMdotMMMM(double degrees)
    {
      _value = degrees;
    }

    public DDMMdotMMMM(double degrees, double minutes, double seconds)
    {
      _value = (((System.Math.Abs(seconds) / 60) + System.Math.Abs(minutes)) / 60 + System.Math.Abs(degrees)) * System.Math.Sign(degrees);
    }

    // operator overloading
    public static implicit operator DDMMdotMMMM(double val)
    {
      return new DDMMdotMMMM(val);
    }

    public static implicit operator DDMMdotMMMM(string val)
    {
      return new DDMMdotMMMM(val);
    }

    public static implicit operator double(DDMMdotMMMM val)
    {
      return val._value;
    }

    public static implicit operator string(DDMMdotMMMM val)
    {
      int signo = System.Math.Sign(val._value);
      double grados = System.Math.Floor(System.Math.Abs(val._value/100));
      double minutos = System.Math.Abs( val._value) - grados * 100;
      double resultado = signo * (grados * 100 + minutos * 60);
      return resultado.ToString();
    }

    /// <summary>
    /// Esta funcion transforma el valor a la siguiente forma:
    /// +-gg° MM' ss.sss" 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      double aux = _value;
      int signo = System.Math.Sign(_value);
      aux = System.Math.Abs(aux);
      double grados = System.Math.Floor(aux);
      aux -= grados;
      aux *= 100;
      double minutos = System.Math.Floor(aux);
      aux -= minutos;
      aux *= 100;
      string s = "";
      s = signo >= 0 ? "" : "-";
      s += grados.ToString() + "° ";
      s += minutos.ToString() + "' ";
      s += aux.ToString("0.00") + "\"";
      return s;
    }

    #region ICloneable Members

    public object Clone()
    {
      return new DDMMdotMMMM(_value);
    }

    #endregion

    #region IDataType Members

    // operator overloading
    public void setValue(double val, int pos)
    {
      if (pos == 0)
      {
        _value = val;
      }
      else
      {
        throw new ArgumentOutOfRangeException("pos debe ser 0");
      }
    }

    public void setValue(string val, int pos)
    {
      if (pos != 0)
      {
        throw new ArgumentOutOfRangeException("pos debe ser 0");
      }
      string s = (val.Length == 0 ? "0" : val);
      // Format
      // -DDDMM.MMMMM 
      double value = 0;
      int signo = 0;
      int grados = 0;
      double minutos = 0;
      
      System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
      if (System.Double.TryParse(s, System.Globalization.NumberStyles.Float, culture.NumberFormat, out value))
      {
        signo = System.Math.Sign(value);
        value = System.Math.Abs(value);
        grados = (int)System.Math.Floor(value / 100);
        minutos = value - grados * 100.0;
        _value = signo * (grados + minutos / 60);
      }
      else
      {
        throw new NotFiniteNumberException("Invalid number");
      }
    }

    public double GetValue(int pos)
    {
      if (pos == 0)
      {
        return _value;
      }
      else
      {
        throw new ArgumentOutOfRangeException("pos debe ser 0");
      }
    }

#endregion
  }
}
