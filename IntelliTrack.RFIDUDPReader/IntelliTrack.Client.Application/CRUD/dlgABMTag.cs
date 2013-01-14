using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application.CRUD
{
  public partial class dlgABMTag : /*Form */ IntelliTrack.Client.Application.CRUD.dlgABMBase
  {

    private void cATEGORIASBindingNavigatorSaveItem_Click(object sender, EventArgs e)
    {
      this.Validate();
      this.cATEGORIASBindingSource.EndEdit();
      this.cATEGORIASTableAdapter.Update(this.cRUDDataSet.CATEGORIAS);

    }
    
    public dlgABMTag(IntelliTrack.Client.Application.CRUD.ABM ABMType, IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow row)
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
    }

    protected override void CargarDatosEnPantalla()
    {
      if (_CRUDType == ABM.BAJA)
      {
        this.groupBox2.Enabled = false;
      }
      this.cATEGORIASTableAdapter.Fill(this.cRUDDataSet.CATEGORIAS);
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow;

      try
      {
        txtCodigo.Text = row.RFI_ID.ToString();
      }
      catch (Exception ex)
      {
        txtCodigo.Text = "";
      }

      try
      {
        txtTAG.Text = row.RFI_TAG;
      }
      catch (Exception ex)
      {
        txtTAG.Text = "";
      }

      try
      {
        cboCategorias.SelectedValue = row.RFI_CATEGORIA;
      }
      catch (Exception ex)
      {
      }


      try
      {
        txtVehiculo.Text = row.RFI_VEHICULO;
      }
      catch (Exception ex)
      {
        txtVehiculo.Text = "";
      }


      if ((row.RFI_ACTIVO == "S") || (row.RFI_ACTIVO == ""))
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
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow;

      row.RFI_CATEGORIA = (short)cboCategorias.SelectedValue;

      row.RFI_TAG = this.txtTAG.Text;

      row.RFI_VEHICULO = txtVehiculo.Text;

      if (rbSi.Checked)
      {
        row.RFI_ACTIVO = "S";
      }
      else
      {
        row.RFI_ACTIVO = "N";
      }

      row.RFID_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
    }

    protected override void SetUpFechaAltaYUsuario()
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow;
      row.RFID_FECHA_ACT = System.DateTime.Now;
      row.RFID_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
    }

    protected override void SetupFechaModificacionYUsuario()
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow;
      row.RFID_FECHA_ACT = System.DateTime.Now;
      row.RFID_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
    }
  }
}