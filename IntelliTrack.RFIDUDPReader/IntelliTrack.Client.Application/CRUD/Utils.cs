using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Client.Application.CRUD
{
  public static class Utils
  {
    public static int CalculateNewID(System.Data.DataTable table, System.Data.DataColumn dc)
    {
      object res = table.Compute("Max(" + dc.ColumnName + ")", "");
      if (res != null)
      {
        int maxValue = (short) res + 0;
        return maxValue + 1;
      }
      else
        return 1;
      /*        
            if (_row.Table.Rows.Count == 0)
            {
              return 1;
            }
            else
            {
              System.Data.DataView dv = _row.Table.DefaultView;
              dv.Sort = dc.ColumnName + ", DESC";
              return ((int)dv[0][dc.ColumnName]) + 1;
            }
       */
    }
  }
}
