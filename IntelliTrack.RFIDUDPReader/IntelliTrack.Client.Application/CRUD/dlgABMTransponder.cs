using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application.CRUD
{
  public partial class dlgABMTransponder : /*Form */  IntelliTrack.Client.Application.CRUD.dlgABMBase 
  {
   
    public dlgABMTransponder(IntelliTrack.Client.Application.CRUD.ABM ABMType, IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow row)
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
      cboCategorias.Text = "";
    }

    protected override void CargarDatosEnPantalla()
    {
      if (_CRUDType == ABM.BAJA)
      {
        this.groupBox2.Enabled = false;
      }
      this.cATEGORIASTableAdapter.Fill(this.cRUDDataSet.CATEGORIAS);
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow;

      try
      {
        txtCodigo.Text = row.TRA_ID.ToString();
      }
      catch (Exception ex)
      {
        txtCodigo.Text = "";
      }

      try
      {
        txtTransponder.Text = row.TRA_COD_TRANSP;
      }
      catch (Exception ex)
      {
        txtTransponder.Text = "";
      }

      try
      {
        cboCategorias.SelectedValue = row.TRA_COD_CATEGORIA;
        //cboCategorias.Text = row.TRA_COD_CATEGORIA.ToString();
      }
      catch (Exception ex)
      {
        //cboCategorias.Text = "";
      }

      try
      {
        txtVehiculo.Text = row.TRA_VEHICULO;
      }
      catch (Exception ex)
      {
        txtVehiculo.Text = "";
      }

      if ((row.TRA_ACTIVO == "S") || (row.TRA_ACTIVO == ""))
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
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow;
      
      row.TRA_COD_CATEGORIA = (short)cboCategorias.SelectedValue;

      row.TRA_COD_TRANSP = txtTransponder.Text;

      row.TRA_VEHICULO = txtVehiculo.Text;

      if (rbSi.Checked)
      {
        row.TRA_ACTIVO = "S";
      }
      else
      {
        row.TRA_ACTIVO = "N";
      }

      row.TRA_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
    }

    protected override void SetUpFechaAltaYUsuario()
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow;
      row.TRA_FECHA_MOD = System.DateTime.Now;
      row.TRA_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
    }

    protected override void SetupFechaModificacionYUsuario()
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow row = _row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow;
      row.TRA_FECHA_MOD = System.DateTime.Now;
      row.TRA_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
    }
  
  }
}