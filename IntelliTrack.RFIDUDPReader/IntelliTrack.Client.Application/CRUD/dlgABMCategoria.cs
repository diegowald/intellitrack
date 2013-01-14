using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application.CRUD
{
  public partial class dlgABMCategoria : /*Form */  IntelliTrack.Client.Application.CRUD.dlgABMBase
  {
    public dlgABMCategoria(IntelliTrack.Client.Application.CRUD.ABM ABMType, IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow row)
      : base(ABMType, row)
    {
      InitializeComponent();
      groupBox1.Enabled = false;
    }
    
    protected override bool ValidarInformacion()
    {
      return true;
      return false;
    }

    protected override void ClearData()
    {
      txtDescripcion.Text = "";
    }

    protected override void CargarDatosEnPantalla()
    {
      if (_CRUDType == ABM.BAJA)
      {
        this.groupBox2.Enabled = false;
      }

      IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow;

      try
      {
        txtCodigo.Text = row.CAT_ID.ToString();
      }
      catch (Exception)
      {
        txtCodigo.Text = "";
      }
      try
      {
        txtDescripcion.Text = row.CAT_DESCRIPCION;
      }
      catch (Exception)
      {
        txtDescripcion.Text = "";
      }
      if ((row.IsCAT_EDITABLENull()) || (row.CAT_EDITABLE == "S"))
      {
        rbSi.Checked = true;
        rbNo.Checked = false;
      }
      else
      {
        rbSi.Checked = false;
        rbNo.Checked = true;
      }
    }

    protected override void GuardarDatosADataRow()
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow;

      row.CAT_DESCRIPCION = txtDescripcion.Text;
      if (rbSi.Checked)
      {
        row.CAT_EDITABLE = "S";
      }
      else
      {
        row.CAT_EDITABLE = "N";
      }
      row.CAT_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
    }

    protected override void SetUpFechaAltaYUsuario()
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow;
      row.CAT_FECHA_MOD = System.DateTime.Now;
      row.CAT_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
    }

    protected override void SetupFechaModificacionYUsuario()
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow;
      row.CAT_FECHA_MOD = System.DateTime.Now;
      row.CAT_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
    }
  }
}