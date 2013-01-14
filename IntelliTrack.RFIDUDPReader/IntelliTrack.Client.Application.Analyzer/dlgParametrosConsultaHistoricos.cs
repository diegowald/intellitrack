using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application
{
  public partial class dlgParametrosConsultaHistoricos : Form
  {
    public int QueryTimeOut
    {
      get
      {
        return (int)InputQueryTimeOut.Value;
      }
      set
      {
        InputQueryTimeOut.Value = (decimal)value;
      }
    }




        public string Area
        {
            get
            {
                return txtArea.Text;
            }
            set
            {
                txtArea.Text = value;
            }
        }

        public string Camino
        {
            get
            {
                return txtCamino.Text;
            }
            set
            {
                txtCamino.Text = value;
            }
        }

        public int CategoriaSelected
        {
            get
            {
                return (short)cboCategoria.SelectedValue;
            }
            set
            {
                if (value != 99)
                    cboCategoria.SelectedValue = value;
            }
        }

        public System.DateTime FechaFin
        {
            get
            {
                return dtFechaFin.Value;
            }
            set
            {
                dtFechaFin.Value = value;
            }
        }

        public System.DateTime FechaInicio
        {
            get
            {
                return dtFechaInicio.Value;
            }
            set
            {
                dtFechaInicio.Value = value;
            }
        }

        public bool FiltroPorVelocidad
        {
            get
            {
                return chkVelocidad.Checked;
            }
            set
            {
                chkVelocidad.Checked = value;
                InputVelocidadMaxima.Enabled = value;
                InputVelocidadMinima.Enabled = value;
            }
        }

        public bool GenerarReporte
        {
            get
            {
                return chkGenerarReporte.Checked;
            }
            set
            {
                chkGenerarReporte.Checked = value;
            }
        }

        public int MuestrasPorSegundo
        {
            get
            {
                return (int)InputMuestrasPorSegundo.Value;
            }
            set
            {
                InputMuestrasPorSegundo.Value = value;
            }
        }

        public string Punto
        {
            get
            {
                return txtPunto.Text;
            }
            set
            {
                txtPunto.Text = value;
            }
        }

        public string Transponder
        {
            get
            {
                return txtTransponder.Text;
            }
            set
            {
                txtTransponder.Text = value;
            }
        }

        public string Vehiculo
        {
            get
            {
                return txtVehiculo.Text;
            }
            set
            {
                txtVehiculo.Text = value;
            }
        }

        public int VelocidadMaxima
        {
            get
            {
                return (int)InputVelocidadMaxima.Value;
            }
            set
            {
                InputVelocidadMaxima.Value = value;
            }
        }

        public int VelocidadMinima
        {
            get
            {
                return (int)InputVelocidadMinima.Value;
            }
            set
            {
                InputVelocidadMinima.Value = value;
            }
        }

        public dlgParametrosConsultaHistoricos()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (InputVelocidadMinima.Value > InputVelocidadMaxima.Value)
            {
                System.Windows.Forms.MessageBox.Show("La velocidad M\u00EDnima debe ser menor o igual a la velocidad M\u00E1xima", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                InputVelocidadMinima.Value = InputVelocidadMaxima.Value;
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void chkVelocidad_CheckedChanged(object sender, System.EventArgs e)
        {
            InputVelocidadMaxima.Enabled = chkVelocidad.Checked;
            InputVelocidadMinima.Enabled = chkVelocidad.Checked;
        }

        private void dlgParametrosConsultaHistoricos_Load(object sender, System.EventArgs e)
        {
            LlenarCategorias();
        }


        private void LlenarCategorias()
        {
            cboCategoria.Items.Clear();
            System.Data.DataSet dataSet = IntelliTrack.Client.Application.Common.Queries.GetCategorias();
            cboCategoria.DataSource = dataSet.Tables[0];
            cboCategoria.DisplayMember = "CAT_DESCRIPCION";
            cboCategoria.ValueMember = "CAT_ID";
        }


    } // class dlgParametrosConsultaHistoricos

}
