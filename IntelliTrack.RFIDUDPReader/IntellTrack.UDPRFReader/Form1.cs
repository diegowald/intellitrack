using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntellTrack.UDPRFReader
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      dataReader = new UDPReader();
      dataReader.DataArrivedHandler += new UDPReader.DataArrived(dataReader_DataArrivedHandler);
    }



    protected UDPReader dataReader;
    private void Form1_Load(object sender, EventArgs e)
    {
      this.lblStatus.Visible = false;
      this.lblDataArrived.Visible = false;
      this.lbData.Items.Clear();
    }


    void dataReader_DataArrivedHandler(string FromIP, string data)
    {
      this.lblStatus.Visible = true;
      //this.lbData.Visible = false;
      this.lblDataArrived.Visible = true;
      string CodAntena = "";
      string TagEquipo = "";
      if (ParseData(FromIP, data, CodAntena, TagEquipo))
      {
        this.lblStatus.Text = "OK";
        this.lblStatus.BackColor = System.Drawing.Color.Green;
        SaveData(CodAntena, TagEquipo);
      }
      else
      {
        this.lblStatus.Text = "ERROR";
        this.lblStatus.BackColor = System.Drawing.Color.Red;
      }
    }

    private void SaveData(string CodAntena, string TagEquipo)
    {
      Save2Database.Instance.Save(CodAntena, TagEquipo);
    }

    private bool ParseData(string FromURL, string data, string CodAntena, string TagEquipo)
    {
      CodAntena = "0001";
      TagEquipo = "1234567";
      return true;
    }
  }
}