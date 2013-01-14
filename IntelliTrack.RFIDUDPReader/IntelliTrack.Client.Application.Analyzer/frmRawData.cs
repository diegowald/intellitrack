using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application
{
  public partial class frmRawData : /*Form  */frmBaseDockingForm
  {

    private Form _parent;

    public frmRawData(Form parent)
    {
      InitializeComponent();
      _parent = parent;

      IntelliTrack.Service.Common.Singleton<IntelliTrack.Client.Application.Imaging.ComplexMap>.Instance.SetHistoricDataHandlerRawDataForm(this);

      dgRawData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgRawData.MultiSelect = false;
    }

    public System.Data.DataSet DataSource
    {
      get
      {
        return dgRawData.DataSource as System.Data.DataSet;
      }
      set
      {
        dgRawData.DataSource = value;
        dgRawData.DataMember = value.Tables[0].TableName;
        if (dgRawData.Columns.Contains("DIA_HORA"))
          dgRawData.Columns["DIA_HORA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
      }
    }

    public int SelectedElement
    {
      set
      {
        /*dgRawData.Rows[value].Selected = true;
        dgRawData.FirstDisplayedScrollingRowIndex = value;
        //dgRawData.Refresh();*/
        try
        {
          dgRawData.CurrentCell = dgRawData.Rows[value].Cells[0];
        }
        catch (Exception ex)
        {
        }
      }
    }

    public override void RefreshMe()
    {
      //base.RefreshMe();
    }



    internal void ExportarDataSetAPortapapeles()
    {
      dgRawData.MultiSelect = true;
      dgRawData.SelectAll();
      System.Windows.Forms.DataObject dataObject = dgRawData.GetClipboardContent();
      System.Windows.Forms.Clipboard.SetDataObject(dataObject, true);
      dgRawData.ClearSelection();
      dgRawData.MultiSelect = false;
    }


  } // class frmRawData

}
