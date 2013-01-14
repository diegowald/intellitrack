using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application.CRUD
{
  public enum ABM
  {
    ALTA = 0,
    BAJA,
    MODIFICACION
  };

  public abstract partial class dlgABMBase : Form
  {

    protected ABM _CRUDType;
    protected System.Data.DataRow _row;
    public dlgABMBase(ABM CRUDType, System.Data.DataRow row)
    {
      InitializeComponent();
      _CRUDType = CRUDType;
      _row = row;

    }

  


    private void dlgABMBase_Load(object sender, EventArgs e)
    {
      switch (_CRUDType)
      {
        case ABM.ALTA:
          {
            Text = "Nuevo";
            tbSalir.Visible = true;
            tbGuardar.Visible = true;
            tbCancelar.Visible = true;
            tbEliminar.Visible = false;
          }
          break;
        case ABM.BAJA:
          {
            Text = "Eliminar";
            tbSalir.Visible = true;
            tbGuardar.Visible = false;
            tbCancelar.Visible = false;
            tbEliminar.Visible = true;
          }
          break;
        case ABM.MODIFICACION:
          {
            Text = "Modificar";
            tbSalir.Visible = true;
            tbGuardar.Visible = true;
            tbCancelar.Visible = false;
            tbEliminar.Visible = false;
          }
          break;
      }
      CargarDatosEnPantalla();
    }

    private void tbSalir_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Hide();
    }

    private void tbGuardar_Click(object sender, EventArgs e)
    {
      if (ValidarInformacion())
      {
        switch (_CRUDType)
        {
          case ABM.ALTA:
            {
              SetUpFechaAltaYUsuario();
            }
            break;
          case ABM.MODIFICACION:
            {
              SetupFechaModificacionYUsuario();
            }
            break;
          default:
            break;
        }
        GuardarDatosADataRow();
        DialogResult = DialogResult.OK;
        Hide();
      }
    }

    private void tbCancelar_Click(object sender, EventArgs e)
    {
      ClearData();
    }

    private void tbEliminar_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Hide();
    }

    protected virtual bool ValidarInformacion()
    {
      return false;
    }

    protected virtual void ClearData()
    {
    }

    protected virtual void CargarDatosEnPantalla()
    {
    }

    protected virtual void GuardarDatosADataRow()
    {
    }

    protected abstract void SetUpFechaAltaYUsuario();
    protected abstract void SetupFechaModificacionYUsuario();
    
    
  }
}