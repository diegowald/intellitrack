using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Service.Common
{
  public class Singleton<T> where T : new()
  {
    public Singleton()
    {
    }

    //private static T _Instance;// = default(T);

    private static object l = new object();

    public static T Instance
    {
      get
      {
        try
        {
          lock (l)
          {
            T instance = SingletonCreator.instance;
            return instance;
          }
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine(ex);
          return default(T);
        }
      }
    }

    class SingletonCreator
    {
      static SingletonCreator()
      {
      }

      internal static readonly T instance = new T();
    }
  }
}
