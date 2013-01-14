using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application
{
  public partial class dlgTags : Form
  {
    IntelliTrack.Client.Application.CRUD.CRUDDataSet ds = null;
    public dlgTags()
    {
      InitializeComponent();
      ds = new IntelliTrack.Client.Application.CRUD.CRUDDataSet();      
    }

    private void dlgTags_Load(object sender, EventArgs e)
    {
      // TODO: This line of code loads data into the 'cRUDDataSet.TAGS' table. You can move, or remove it, as needed.
      this.tAGSTableAdapter.Fill(this.cRUDDataSet.TAGS);
    }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    {
      if (dataGridView1.SelectedRows.Count > 0)
      {
        txtTAG.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        txtCategoria.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        txtVehiculo.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "S")
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

    private void ABM(IntelliTrack.Client.Application.CRUD.ABM type, IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow row)
    {
      IntelliTrack.Client.Application.CRUD.dlgABMTag dlg = new IntelliTrack.Client.Application.CRUD.dlgABMTag(type, row);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.TAGSTableAdapter da = new IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.TAGSTableAdapter();
        //da.Update(ds);
        switch (type)
        {
          case IntelliTrack.Client.Application.CRUD.ABM.ALTA:
            ds.TAGS.AddTAGSRow(row);
            da.Update(row);
            break;
          case IntelliTrack.Client.Application.CRUD.ABM.MODIFICACION:
              da.Update(row);
            break;
          case IntelliTrack.Client.Application.CRUD.ABM.BAJA:
            {
              da.Delete(row.RFI_ID);
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
          this.tAGSTableAdapter.Fill(this.cRUDDataSet.TAGS);
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
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow row = ds.TAGS.NewTAGSRow();

      row.RFID_FECHA_ACT = System.DateTime.Now;
      row.RFID_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;
      row.RFI_ACTIVO = "S";

      ABM(IntelliTrack.Client.Application.CRUD.ABM.ALTA, row);
    }


    private void Modificar_Click(object sender, EventArgs e)
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow row = (this.dataGridView1.CurrentRow.DataBoundItem as System.Data.DataRowView).Row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow;
      ABM(IntelliTrack.Client.Application.CRUD.ABM.MODIFICACION, row);
    }

    private void Eliminar_Click(object sender, EventArgs e)
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow row = (this.dataGridView1.CurrentRow.DataBoundItem as System.Data.DataRowView).Row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.TAGSRow;
      ABM(IntelliTrack.Client.Application.CRUD.ABM.BAJA, row);
    }

    private void Actualizar_Click(object sender, EventArgs e)
    {
      this.tAGSTableAdapter.Fill(this.cRUDDataSet.TAGS);
    }
  }
}