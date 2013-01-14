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
  public partial class OverviewMap : frmBaseDockingForm 
  {
    private Form parentForm;
    public OverviewMap(Form parent)
    {
      InitializeComponent();
      parentForm = parent;
    }

    private void ComposeImage()
    {
      if (Size.Width == 0)
      {
        return;
      }
      this.BackgroundImage = Singleton<ComplexMap>.Instance.GetOverViewMap();
    }

    private void OverviewMap_SizeChanged(object sender, EventArgs e)
    {
      Singleton<ComplexMap>.Instance.OverviewSize = this.Size;
      RefreshMe();
    }

    public override void RefreshMe()
    {
      ComposeImage();
    }

    private void OverviewMap_ResizeEnd(object sender, EventArgs e)
    {
      RefreshMe();
    }
  }
}