using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace IntelliTrack.Client.Application.Reportes
{
  /// <summary>
  /// Summary description for Regadores.
  /// </summary>
  public partial class Regadores : DataDynamics.ActiveReports.ActiveReport3
  {

    public Regadores()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
    }


    /// nuevo
    /// 

    internal delegate void DoCalcularTiemposTotales(string IDVehiculo, out int TiempoTotalSegundosDetenido, out int TiempoTotalSegundosCarga, out int TiempoTotalSegundosTransito);



    internal event IntelliTrack.Client.Application.Reportes.Regadores.DoCalcularTiemposTotales OnCalcularTiemposTotales;

    private void CalcularTiemposTotales(string IDVehiculo, out int TiempoTotalSegundosDetenido, out int TiempoTotalSegundosCarga, out int TiempoTotalSegundosTransito)
    {
      int i1 = 0, i2 = 0, i3 = 0;
      if (OnCalcularTiemposTotales != null)
        OnCalcularTiemposTotales(IDVehiculo, out i1, out i3, out i2);
      TiempoTotalSegundosDetenido = i1;
      TiempoTotalSegundosCarga = i3;
      TiempoTotalSegundosTransito = i2;
    }

    private void detail_Format(object sender, System.EventArgs e)
    {
      System.Drawing.Color color;

      switch (txtEstado.Text)
      {
        case "Detenido":
          color = System.Drawing.Color.DarkRed;
          HacerCampoVisible(txtTiempoDetenido1);
          break;
        case "Transito":
          color = System.Drawing.Color.DarkBlue;
          HacerCampoVisible(txtTiempoTransito1);
          break;
        case "Carga":
          color = System.Drawing.Color.Green;
          HacerCampoVisible(txtTiempoCarga1);
          break;
        default:
          color = System.Drawing.Color.Black;
          break;
      }

      txtTiempoCarga1.ForeColor = color;
      txtTiempoDetenido1.ForeColor = color;
      txtTiempoTransito1.ForeColor = color;
      txtEstado.ForeColor = color;
      txtDIA_HORA1.ForeColor = color;
      txtDIA_HORA_Final1.ForeColor = color;
      txtReferenciaSalida1.ForeColor = color;
      txtReferenciaCaminos1.ForeColor = color;
      txtReferenciaLlegada1.ForeColor = color;
      txtVelocidadMaxima1.ForeColor = color;
      txtVelocidadPromedio1.ForeColor = color;
      txtDistancia.ForeColor = color;
    }

    private void groupFooter1_Format(object sender, System.EventArgs e)
    {
      int i1 = 0, i2 = 0, i3 = 0;
      CalcularTiemposTotales(txtIDElemento1.Text, out i1, out i2, out i3);
      txtTiempoTotalCarga.Text = SegundosAHHMMSS(i2);
      txtTiempoTotalDetenido.Text = SegundosAHHMMSS(i1);
      txtTempoTotalTransito.Text = SegundosAHHMMSS(i3);
    }

    private void HacerCampoVisible(DataDynamics.ActiveReports.TextBox ControlAHacerVisible)
    {
      txtTiempoCarga1.Visible = txtTiempoCarga1 == ControlAHacerVisible ? true : false;
      txtTiempoTransito1.Visible = txtTiempoTransito1 == ControlAHacerVisible ? true : false;
      txtTiempoDetenido1.Visible = txtTiempoDetenido1 == ControlAHacerVisible ? true : false;
    }



    private string SegundosAHHMMSS(int segundos)
    {
      string s1 = "";
      System.TimeSpan timeSpan = new System.TimeSpan(0, 0, segundos);
      if (timeSpan.Days > 0)
      {
        int i1 = timeSpan.Days;
        s1 = i1.ToString() + ".";
      }
      string s2 = s1;
      string[] sArr = new string[6];
      sArr[0] = s2;
      int i2 = timeSpan.Hours;
      sArr[1] = i2.ToString("00");
      sArr[2] = ":";
      int i3 = timeSpan.Minutes;
      sArr[3] = i3.ToString("00");
      sArr[4] = ":";
      int i4 = timeSpan.Seconds;
      sArr[5] = i4.ToString("00");
      return System.String.Concat(sArr);
    }


  } // class Regadores

}

