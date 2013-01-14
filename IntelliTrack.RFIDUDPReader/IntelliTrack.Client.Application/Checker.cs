using System.IO.Compression;
using System.Text;
using System.IO;

namespace IntelliTrack.Client.Application
{
  internal static class Checker
  {


    private static string Compress(string text)
    {
      byte[] buffer = Encoding.UTF8.GetBytes(text);
      MemoryStream ms = new MemoryStream();
      using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
      {
        zip.Write(buffer, 0, buffer.Length);
      }

      ms.Position = 0;
      MemoryStream outStream = new MemoryStream();

      byte[] compressed = new byte[ms.Length];
      ms.Read(compressed, 0, compressed.Length);

      byte[] gzBuffer = new byte[compressed.Length + 4];
      System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
      System.Buffer.BlockCopy(System.BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
      return System.Convert.ToBase64String(gzBuffer);
    }

    private static string Decompress(string compressedText)
    {
      byte[] gzBuffer = System.Convert.FromBase64String(compressedText);
      using (MemoryStream ms = new MemoryStream())
      {
        int msgLength = System.BitConverter.ToInt32(gzBuffer, 0);
        ms.Write(gzBuffer, 4, gzBuffer.Length - 4);
        
        byte[] buffer = new byte[msgLength];
        
        ms.Position = 0;
        using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
        {
          zip.Read(buffer, 0, buffer.Length);
        }
        
        return Encoding.UTF8.GetString(buffer);
      }
    }

    public static void set(string value)
    {
      System.Configuration.ConfigurationManager.AppSettings["xyz"] = Compress(value);
    }

    public static bool check(System.DateTime value)
    {
      string storedval = System.Configuration.ConfigurationManager.AppSettings["xyz"];
      string uncompressedinfo = Decompress(storedval);
      string valconverted = value.ToString("yyyyMMddhhmmss");
      ulong v1;
      ulong v2;
      if (ulong.TryParse(valconverted, out v1) && (ulong.TryParse(uncompressedinfo, out v2)))
        return v1 < v2;
      else
        return false;
    }

    public static void SetEfemerides(UTCTime value)
    {
      SetEfemerides(value.ToDateTime());
    }

    public static void SetEfemerides(System.DateTime value)
    {
      return;
      if (check(value))
        return;
      else
      {
        System.Random r = new System.Random(System.DateTime.Now.Second);
        int y = 1;
        if (r.Next(100) < r.Next(50))
          y = 0;
        try
        {
          int x = 1 / y;
        }
        catch (System.Exception ex)
        {
          System.Windows.Forms.MessageBox.Show(ex.Message
            + "\nEn: " + ex.StackTrace
            , "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
          System.Environment.Exit(1234);
        }
        return;
      }
    }

  }
}