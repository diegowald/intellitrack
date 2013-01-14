using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CreadorXYZ
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Codificar_Click(object sender, EventArgs e)
    {
      txtCodificado.Text = IntelliTrack.Client.Application.Checker.Compress(
        dtSinCodificar.Value.ToString("yyyyMMddhhmmss"));
    }

    private void Decodificar_Click(object sender, EventArgs e)
    {
      if (this.txtCodificado.Text.Length != 0)
      {
        dtSinCodificar.Value =
        DateTime.ParseExact(IntelliTrack.Client.Application.Checker.Decompress(
          txtCodificado.Text), "yyyyMMddhhmmss",
          System.Globalization.CultureInfo.InvariantCulture);
      }
    }
  }
}