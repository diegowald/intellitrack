using System.IO.Compression;
using System.Text;
using System.IO;

namespace IntelliTrack.Client.Application
{
  internal static class Checker
  {


    public static string Compress(string text)
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

    public static string Decompress(string compressedText)
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

  }
}