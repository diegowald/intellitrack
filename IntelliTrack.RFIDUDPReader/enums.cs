namespace IntelliTrack.RFIDUDPReader
{

  // Various encoding types for data
  public enum EncodingType
  {
    Default = 0,
    ASCII = 1,
    Unicode = 2,
    UTF7 = 3,
    UTF8 = 4
  };

  // Custom exceptions
  public class BadEncodingException : System.Exception
  {
    public BadEncodingException()
      : base("The encoding type selected is not valid.")
    {
    }

    public BadEncodingException(string Message)
      : base(Message)
    {
    }
  }

  public class ProtocolNotSupportedException : System.Exception
  {
    public ProtocolNotSupportedException()
      : base("The protocol selected is not supported.")
    {
    }

    public ProtocolNotSupportedException(string Message)
      : base(Message)
    {
    }
  }
}