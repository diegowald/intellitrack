using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using IntelliTrack.Service.Common;
using IntelliTrack.Client.Application.Imaging;

namespace IntelliTrack.Client.Application
{
  public partial class frmBaseDockingForm : /*Form*/ WeifenLuo.WinFormsUI.Docking.DockContent 
  {
    public frmBaseDockingForm()
    {
      InitializeComponent();
      if (Singleton<ComplexMap>.Instance != null)
        Singleton<ComplexMap>.Instance.AddForm(this);
    }

    public virtual void RefreshMe()
    {
      throw new NotImplementedException();
    }

  }
}