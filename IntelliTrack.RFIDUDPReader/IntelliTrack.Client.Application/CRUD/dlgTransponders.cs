using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application
{
  public partial class dlgTransponders : Form
  {
    IntelliTrack.Client.Application.CRUD.CRUDDataSet ds = null;
    public dlgTransponders()
    {
      InitializeComponent();
      ds = new IntelliTrack.Client.Application.CRUD.CRUDDataSet();      
    }

    private void dlgTransponders_Load(object sender, EventArgs e)
    {
      // TODO: esta línea de código carga datos en la tabla 'cRUDDataSet.TRANSPONDERS' Puede moverla o quitarla según sea necesario.
      this.tRANSPONDERSTableAdapter.Fill(this.cRUDDataSet.TRANSPONDERS);
      // TODO: esta línea de código carga datos en la tabla 'cRUDDataSet.TRANSPONDERS' Puede moverla o quitarla según sea necesario.
    }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    {
      if (dataGridView1.SelectedRows.Count > 0)
      {
        txtTransponder.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        txtCategoria.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        txtVehiculo.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "S")
        {
          chkActivo.Checked = true;
        }
        else
        {
          chkActivo.Checked = false;
        }

        txtUsuario.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        txtUltimaModificacion.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
      }
    }

    private void Salir_Click(object sender, EventArgs e)
    {
      Hide();
    }

    private void ABM(IntelliTrack.Client.Application.CRUD.ABM type, IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow row)
    {
      IntelliTrack.Client.Application.CRUD.dlgABMTransponder dlg = new IntelliTrack.Client.Application.CRUD.dlgABMTransponder(type, row);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.TRANSPONDERSTableAdapter da = new IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.TRANSPONDERSTableAdapter();
        //da.Update(ds);
        switch (type)
        {
          case IntelliTrack.Client.Application.CRUD.ABM.ALTA:
            ds.TRANSPONDERS.AddTRANSPONDERSRow(row);
            da.Update(row);
            break;
          case IntelliTrack.Client.Application.CRUD.ABM.MODIFICACION:
            da.Update(row);
            break;
          case IntelliTrack.Client.Application.CRUD.ABM.BAJA:
            {
              da.Delete(row.TRA_ID);
              row.Delete();
              //ds.CATEGORIAS.RemoveCATEGORIASRow(row);
            }
            break;
          default:
            break;
        }
        try
        {
          //IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.CATEGORIASTableAdapter da = new IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.CATEGORIASTableAdapter();
          //da.Update(ds);
          //da.Delete(
          ds.AcceptChanges();
          this.tRANSPONDERSTableAdapter.Fill(this.cRUDDataSet.TRANSPONDERS);
          dataGridView1.Refresh();
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine(ex.Message);
        }
      }
    }

    private void Nuevo_Click(object sender, EventArgs e)
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow row = ds.TRANSPONDERS.NewTRANSPONDERSRow();

      row.TRA_FECHA_MOD = System.DateTime.Now;
      row.TRA_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
      row.TRA_ACTIVO = "S";

      ABM(IntelliTrack.Client.Application.CRUD.ABM.ALTA, row);
    }


    private void Modificar_Click(object sender, EventArgs e)
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow row = (this.dataGridView1.CurrentRow.DataBoundItem as System.Data.DataRowView).Row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow;
      ABM(IntelliTrack.Client.Application.CRUD.ABM.MODIFICACION, row);
    }

    private void Eliminar_Click(object sender, EventArgs e)
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow row = (this.dataGridView1.CurrentRow.DataBoundItem as System.Data.DataRowView).Row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TRANSPONDERSRow;
      ABM(IntelliTrack.Client.Application.CRUD.ABM.BAJA, row);
    }

    private void Actualizar_Click(object sender, EventArgs e)
    {
      this.tRANSPONDERSTableAdapter.Fill(this.cRUDDataSet.TRANSPONDERS);
    }
  }
}