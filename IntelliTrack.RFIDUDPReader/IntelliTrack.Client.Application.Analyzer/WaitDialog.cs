using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application
{
  public partial class WaitDialog : Form
  {
    public WaitDialog()
    {
      InitializeComponent();
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      if (_da != null)
      {
        if (_ds == null)
        {
          _ds = new DataSet();
        }
        _da.Fill(_ds);
      }
    }

    private void WaitDialog_Load(object sender, EventArgs e)
    {
      backgroundWorker1.RunWorkerAsync();
    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      SetFinished();
      Hide();
    }

    private System.Data.SqlClient.SqlDataAdapter _da = null;
    public System.Data.SqlClient.SqlDataAdapter da
    {
      get
      {
        return _da;
      }
      set
      {
        _da = value;
      }
    }

    private System.Data.DataSet _ds = null;
    public System.Data.DataSet ds
    {
      get
      {
        return _ds;
      }
      set
      {
        _ds = value;
      }
    }

    private object thislock = new object();
    private bool _Finished = false;
    private void SetFinished()
    {
      lock (thislock)
      {
        _Finished = true;
      }
    }

    public bool Finished
    {
      get
      {
        lock (thislock)
        {
          return _Finished;
        }
      }
    }
  }
}