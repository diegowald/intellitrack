using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application
{
  public partial class dlgCategorias : Form
  {
    IntelliTrack.Client.Application.CRUD.CRUDDataSet ds = null;
    public dlgCategorias()
    {
      InitializeComponent();
      ds = new IntelliTrack.Client.Application.CRUD.CRUDDataSet();      
    }

    private void dlgCategorias_Load(object sender, EventArgs e)
    {
      // TODO: This line of code loads data into the 'cRUDDataSet.CATEGORIAS' table. You can move, or remove it, as needed.
      this.cATEGORIASTableAdapter.Fill(this.cRUDDataSet.CATEGORIAS);
    }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    {
      if (dataGridView1.SelectedRows.Count > 0)
      {
        txtCodigo.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        txtDescripcion.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "S")
        {
          rbEditable.Checked = true;
          rbNoEditable.Checked = false;
        }
        else
        {
          rbEditable.Checked = false;
          rbNoEditable.Checked = true;
        }
        txtUsuario.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        txtUltimaModificacion.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
      }
    }

    private void Salir_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Hide();
    }

  
    private void ABM(IntelliTrack.Client.Application.CRUD.ABM type, IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow row)
    {
      IntelliTrack.Client.Application.CRUD.dlgABMCategoria dlg = new IntelliTrack.Client.Application.CRUD.dlgABMCategoria(type, row);
      


      if (dlg.ShowDialog() == DialogResult.OK)
      {
        IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.CATEGORIASTableAdapter da = new IntelliTrack.Client.Application.CRUD.CRUDDataSetTableAdapters.CATEGORIASTableAdapter();
        //da.Update(ds);
        switch (type)
        {
          case IntelliTrack.Client.Application.CRUD.ABM.ALTA:
            ds.CATEGORIAS.AddCATEGORIASRow(row);
            da.Update(row);
            break;
          case IntelliTrack.Client.Application.CRUD.ABM.MODIFICACION:
            da.Update(row);
            break;
          case IntelliTrack.Client.Application.CRUD.ABM.BAJA:
            {
              da.Delete(row.CAT_ID);
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
          this.cATEGORIASTableAdapter.Fill(this.cRUDDataSet.CATEGORIAS);
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
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow row = ds.CATEGORIAS.NewCATEGORIASRow();

      /*row.CAT_ID = (short)IntelliTrack.Client.Application.CRUD.Utils.CalculateNewID(
        this.cATEGORIASTableAdapter.GetData(), row.Table.Columns["CAT_ID"]);*/

      row.CAT_FECHA_MOD = System.DateTime.Now;
      row.CAT_USUARIO = ValidacionSeguridad.Instance.NombreUsuario;

      ABM(IntelliTrack.Client.Application.CRUD.ABM.ALTA, row);
    }


    private void Modificar_Click(object sender, EventArgs e)
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow row = (this.dataGridView1.CurrentRow.DataBoundItem as System.Data.DataRowView).Row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow;
      ABM(IntelliTrack.Client.Application.CRUD.ABM.MODIFICACION, row);
    }

    private void Eliminar_Click(object sender, EventArgs e)
    {
      IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow row = (this.dataGridView1.CurrentRow.DataBoundItem as System.Data.DataRowView).Row as IntelliTrack.Client.Application.CRUD.CRUDDataSet.CATEGORIASRow;
      ABM(IntelliTrack.Client.Application.CRUD.ABM.BAJA, row);
    }

    private void Actualizar_Click(object sender, EventArgs e)
    {
      this.cATEGORIASTableAdapter.Fill(this.cRUDDataSet.CATEGORIAS);
    }


  }
}