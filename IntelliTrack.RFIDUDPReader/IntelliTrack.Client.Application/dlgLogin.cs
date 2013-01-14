using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliTrack.Client.Application
{
  public partial class dlgLogin : Form
  {
    public dlgLogin()
    {
      InitializeComponent();
    }

    public string UserName
    {
      get
      {
        return txtUsuario.Text;
      }
    }

    public string Password
    {
      get
      {
        return txtPassword.Text;
      }
    }
  }
}