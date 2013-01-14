using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Client.Application.UDP
{
  public class DataParser
  {

    private string dataPacket;
    private System.Collections.Generic.Dictionary<string, object> ParsedData_;
    public bool ParseData(string rawData, ref System.Collections.Generic.Dictionary<string, object> ParsedData)
    {
      dataPacket = rawData;
      if (ParseData())
      {
        ParsedData = ParsedData_;
        return true;
      }
      else
      {
        ParsedData = null;
        return false;
      }
    }

    private bool ParseData()
    {

      string[] words = dataPacket.Split(',');

      // Sample Sentence:
      // //$PRAVE,0002,0001,3308.9037,-11713.1167,165919,2,9,184,25,12.7,0,-95,0,0,,*76
      //   $PRAVE,0002,0001,-2349.4763,-6447.3949,151140,1,5,447,31,12.9,0,-56,0,0,,*64
      // Sample Sentence:
      // //$PRAVE,0002,0001,3308.9037,-11713.1167,165919,2,9,184,25,12.7,0,-95,0,0,,*76,$RFID,xxxxx,xx
      //   $PRAVE,0002,0001,-2349.4763,-6447.3949,151140,1,5,447,31,12.9,0,-56,0,0,,*64

      //$PRAVE

      switch (words[0].Trim().ToUpper())
      {
        case "$PRAVE":
          {
            return ParsePRAVEData(words);
          }
        case "$RFID":
          {
            return ParseRFIDData(words);
          }
        case "$GPRMC":
          {
            return ParseGPRMCData(words);
          }
        default:
          return false;
      }
    }

    private bool ParsePRAVEData(string[] words)
    {
      // //$PRAVE,0002,0001,3308.9037,-11713.1167,165919,2,9,184,25,12.7,0,-95,0,0,,*76,$RFID,xxxxx,xx
      //   $PRAVE,0002,0001,-2349.4763,-6447.3949,151140,1,5,447,31,12.9,0,-56,0,0,,*64
      //   $PRAVE,0006,0001,-2349.4716,-6447.4031,182800,1,9,465,46,12.9,0,-64,0,0,,*6D
      //                                                          T, V  ,IO,RS,Sp,H,Al,CH  
      string fieldname;
      ParsedData_ = new Dictionary<string, object>();
      for (int i = 0; i < words.Length; i++)
      {
        switch (i)
        {
          case 0:
            fieldname = "Command";
            break;
          case 1://                    From ID  The ID of the transponder that transmitted its position over the air. It is a decimal number, 0 – 9999.
            fieldname = "FromID";
            break;
          case 2://                     To ID   The ID that this position report was sent to. It is a decimal number, 0 To – 9999.
            fieldname = "ToID";
            break;
          case 3://                    Latitude dddmm.mmmm format. It is signed. + is north, - is south. No sign means north. Note: typically there are 4 decimal places, but as few as 0 decimal places are possible. Null field if no GPS lock.
            fieldname = "Latitude";
            break;
          case 4://     Longitude    dddmm.mmmm format. It is signed. + is east, - is west. No sign means east. Note: typically there are 4 decimal places, but as few as 0 decimal places are possible. Null field if no GPS lock.
            fieldname = "Longitude";
            break;
          case 5://     UTC time     The UTC time at the time the transmission was made. Hhmmss format. Null field if no GPS lock.
            fieldname = "UTCTime";
            break;
          case 6://     GPS Status    0=not valid position. 1=GPS locked and valid position.
            fieldname = "GPSStatus";
            break;
          case 7://    Num Satellites The number of satellites in view
            fieldname = "NumSatellites";
            break;
          case 8://       Altitude    The latitude in meters. Null field if no GPS lock.
            fieldname = "Altitude";
            break;
          case 9://     Temperature   The internal temperature of the RV-M7 in degrees C. Typically this is 5-20 degrees above ambient.
            fieldname = "Temperature";
            break;
          case 10://       Voltage     Input voltage to the device that sent this position.
            fieldname = "Voltage";
            break;
          case 11://      IO status    A decimal number representing the binary inputs.
            fieldname = "IOStatus";
            break;
          case 12://        RSSI       The signal-strength of this message as measured by the receiver, in dBm. Note, if the message went through a repeater, it is the signal lever of the repeated message.
            fieldname = "RSSI";
            break;
          case 13://        Speed      The speed of the device in km/hour, 0-255
            fieldname = "Speed";
            break;
          case 14://       Heading     The heading of the device 0-360 degrees
            fieldname = "Heading";
            break;
          case 15://        Alerts     Alert codes for alerts currently indicated in the device. NULL means no alerts. “P” means a proximity alert.
            fieldname = "Alerts";
            break;
          case 16://        Spare      A spare field. May be used for UTC date in the future. Typically NULL.
            /*fieldname = "Spare";
            break;
          case 17:*///           *       The “*” NMEA end-of-message identifier.
            //case 18://      Checksum     The NMEA 0183 checksum.
            fieldname = "Checksum";
            break;
          case 18:
            fieldname = "RFID";
            break;
          //$RFID,03,0000000009B87987,0C<CR><LF>
          //$RFID,06,0000000009B8CDE3,0C<CR><LF>
          //$RFID, ID DE LA CAJA, NUMERO DEL TAG, TIPO DE TAG
          case 19:
            fieldname = "Id. Caja";
            break;
          case 20:
            fieldname = "Número TAG";
            break;
          case 21:
            fieldname = "Tipo TAG";
            break;
          default:
            fieldname = "";
            break;
        }
        if (fieldname != "")
          ParsedData_[fieldname] = words[i];
      }
      return VerifyCheckSum();
    }

    private bool ParseRFIDData(string[] words)
    {
      //$RFID,03,0000000009B87987,0C<CR><LF>
      //$RFID,06,0000000009B8CDE3,0C<CR><LF>
      //$RFID, ID DE LA CAJA, NUMERO DEL TAG, TIPO DE TAG
      string fieldname;
      ParsedData_ = new Dictionary<string, object>();
      for (int i = 0; i < words.Length - 1; i++)
      {
        switch (i)
        {
          case 0: 
            fieldname = "Command";
            break;
          case 1:
            fieldname = "Id. Caja";
            break;
          case 2:
            fieldname = "Número TAG";
            break;
          case 3:
            fieldname = "Tipo TAG";
            break;
          default:
            fieldname = "";
            break;
        }
        if (fieldname != "")
          ParsedData_[fieldname] = words[i];
      }
      return ParsedData_.Count == 3 ? true : false;
    }


    private bool ParseGPRMCData(string[] words)
    {
      // $GPRMC,081836,A,3751.65,S,14507.36,E,000.0,360.0,130998,011.3,E*62
      // $GPRMC,225446,A,4916.45,N,12311.12,W,000.5,054.7,191194,020.3,E*68
      /*
       *   225446       Time of fix 22:54:46 UTC
           A            Navigation receiver warning A = OK, V = warning
           4916.45,N    Latitude 49 deg. 16.45 min North
           12311.12,W   Longitude 123 deg. 11.12 min West
           000.5        Speed over ground, Knots
           054.7        Course Made Good, True
           191194       Date of fix  19 November 1994
           020.3,E      Magnetic variation 20.3 deg East
           *68          mandatory checksum
       */

      
      string fieldname;
      ParsedData_ = new Dictionary<string, object>();
      for (int i = 0; i < words.Length - 1; i++)
      {
        switch (i)
        {
          case 0:
            fieldname = "Command";
            break;
          case 1:
            fieldname = "UTCTIME";
            break;
          case 2:
            fieldname = "RECWARN";
            break;
          case 3:
            fieldname = "Latitude";
            break;
          case 4:
            fieldname = "LatitudeLetter";
            break;
          case 5:
            fieldname = "Longitude";
            break;
          case 6:
            fieldname = "LongitudeLetter";
            break;
          case 7:
            fieldname = "Speed";
            break;
          case 8:
            fieldname = "Course";
            break;
          case 9:
            fieldname = "DateFix";
            break;
          case 10:
            fieldname = "MagneticVariation";
            break;
          case 11:
            fieldname = "checksum";
            break;
          default:
            fieldname = "";
            break;
        }
        if (fieldname != "")
          ParsedData_[fieldname] = words[i];
      }
      return ParsedData_.Count == 11 ? true : false;
    }

    private string Dec2Hex(byte dec)
    {
      return dec.ToString("X");
    }

    private byte Hex2Dec(string hex, bool throwException)
    {
      byte valor = 0;
      if (byte.TryParse(hex, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out valor))
        return valor;
      else
      {
        if (throwException)
          throw new InvalidCastException("No se puede Transformar '" + hex + "' a decimal");
        else
          return 0;
      }
    }

    private bool VerifyCheckSum()
    {
      try
      {
        if (ParsedData_.ContainsKey("Checksum"))
        {
          byte ChecksumData = Hex2Dec((ParsedData_["Checksum"] as string).Replace("*", ""), true);
          int CalculatedCheckSum = 0;
          foreach (byte c in dataPacket)
          {
            if ((char)c == '$')
              continue;
            if ((char)c == '*')
              break;
            CalculatedCheckSum ^= c;
          }

          /*int checksum = 0;
          for (inti = 0; i < stringToCalculateTheChecksumOver.length; i++)
          {
            checksum ^= Convert.ToByte(sentence[i]);
          }*/

          return (ChecksumData == CalculatedCheckSum);
          //CheckSum(dataPacket.ToCharArray(), true);
          //dataOK = true;
        }
        else
        {
          return false;
        }
      }
      catch (System.Collections.Generic.KeyNotFoundException ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        // No existe el campo Checksum... por lo tanto no hay que 
        // comprobar
        return true;
      }
      catch (Exception ex)
      {
        IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        return false;
        //throw ex;
      }
    }
  }
}
