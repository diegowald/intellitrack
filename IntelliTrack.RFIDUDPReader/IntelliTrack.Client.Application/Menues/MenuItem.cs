using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Client.Application.Menues
{
  public class MenuItem
  {

    private string mDescripcion1;
    private string mDescripcion2;
    private string mDescripcion3;
    private bool mEsContenedor;
    private string mFunctionToInvoke;
    private int mMenuNumero;
    private string mMenuPosicion;

    public string Descripcion1
    {
      get
      {
        return mDescripcion1;
      }
    }

    public string Descripcion2
    {
      get
      {
        return mDescripcion2;
      }
    }

    public string Descripcion3
    {
      get
      {
        return mDescripcion3;
      }
    }

    public bool EsContenedor
    {
      get
      {
        return mEsContenedor;
      }
    }

    public string FunctionToInvoke
    {
      get
      {
        return mFunctionToInvoke;
      }
    }

    public int MenuNumero
    {
      get
      {
        return mMenuNumero;
      }
    }

    public string MenuPosicion
    {
      get
      {
        return mMenuPosicion;
      }
    }

    public MenuItem(System.Data.DataRow dr)
    {
      mMenuNumero = (int)dr["MEN_NUMERO"];
      mMenuPosicion = (string)dr["MEN_POSICION"];
      mDescripcion1 = (string)dr["MEN_DESCRIPCION_1"];
      mDescripcion2 = (string)dr["MEN_DESCRIPCION_2"];
      mDescripcion3 = (string)dr["MEN_DESCRIPCION_3"];
      mFunctionToInvoke = (string)dr["MEN_ID"];
      mEsContenedor = ((string)dr["MEN_ES_CONTENEDOR"]) == "S" ? true : false;
    }

  } // class MenuItem
}
