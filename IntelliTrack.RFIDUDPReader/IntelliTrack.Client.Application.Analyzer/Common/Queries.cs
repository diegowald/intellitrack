using System.Data;
using System.Data.SqlClient;

namespace IntelliTrack.Client.Application.Common
{

  internal static class Queries
  {

    public static System.Data.DataSet GetCategorias()
    {
      string s1 = ValidacionSeguridad.Instance.GetApplicationConnectionString();
      System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(s1);
      string s2 = "SELECT * from CATEGORIAS Where CAT_ID <> 99 ";
      int i1 = ValidacionSeguridad.Instance.CategoriaUsuario;
      if (i1 != 99)
      {
        s2 += " AND CAT_ID IN (3, ";
        int i2 = ValidacionSeguridad.Instance.CategoriaUsuario;
        s2 += i2.ToString();
        s2 += ")";
      }
      System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(s2, sqlConnection);
      System.Data.DataSet dataSet = new System.Data.DataSet();
      sqlDataAdapter.Fill(dataSet);
      sqlConnection.Close();
      return dataSet;
    }

  } // class Queries

}
