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
  public partial class MapForm : /*Form  */frmBaseDockingForm
  {
    private Form _parent;
    private Image mBackgroundImage;
    private Cursor ZoomWindowCursor = null;
    private Cursor PanCursor = null;

    public MapForm(Form parent)
    {
      ptOriginal = new System.Drawing.Point();
      ptLast = new System.Drawing.Point();
      InitializeComponent();
      Singleton<ComplexMap>.Instance.OnMapRendering += new ComplexMap.MapRendering(Instance_OnMapRendering);
      Singleton<ComplexMap>.Instance.OnMapRendered += new ComplexMap.MapRendered(Instance_OnMapRendered);
      Singleton<ComplexMap>.Instance.OnLayerRendered += new ComplexMap.LayerRenderedHandler(Instance_OnLayerRendered);
      Singleton<ComplexMap>.Instance.UDPDataArrived += new ComplexMap.DataArrived(DataArrived);
      Singleton<ComplexMap>.Instance.OnHistoryQueryPosition += new IntelliTrack.Client.Application.Imaging.ComplexMap.HistoryQueryPosition(OnHistoryQueryPosition);
      Singleton<ComplexMap>.Instance.OnHistoryQueryStatus += new IntelliTrack.Client.Application.Imaging.ComplexMap.HistoryQueryStatus(OnHistoryQueryStatus);

      lblInformation.Visible = false;
      lblInformation.Text = "";
      _parent = parent;
      this.MouseWheel += new MouseEventHandler(MapForm_MouseWheel);
      ZoomWindowCursor = new Cursor("boxsel.cur");
      PanCursor = new Cursor("Pan.cur");
      btnLaunchInBrowser.Visible = (ValidacionSeguridad.Instance.URL != null) && ValidacionSeguridad.Instance.URL != "";
    }

    public void MapForm_MouseWheel(object sender, MouseEventArgs e)
    {
      if (e.Delta > 0)
        Singleton<ComplexMap>.Instance.ZoomIn();
      else
        Singleton<ComplexMap>.Instance.ZoomOut();
    }

      void DataArrived(string FromIP, string message)
      {
          if (InvokeRequired)
          {
              try
              {
                  if (!this.IsDisposed)
                      Invoke(new MethodInvoker(
                        delegate()
                        {
                            DataArrived(FromIP, message);
                        }));
              }
              catch (ObjectDisposedException ex)
              {
                IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
                  //MessageBox.Show(ex.Message);
              }
          }
          else
          {
              this.RefreshMe();
          }
      }

    void Instance_OnLayerRendered(string Map, string Layer)
    {
      if (InvokeRequired)
      {
        Invoke(new MethodInvoker(
          delegate()
          {
            Instance_OnLayerRendered(Map, Layer);
          }));
      }
      else
      {
          if (Map == "Map")
              lblInformation.Text = Layer;
          if (RedrawProgressBar.Visible)
              RedrawProgressBar.PerformStep();
      }
    }

    void Instance_OnMapRendered()
    {
      if (InvokeRequired)
      {
        Invoke(new MethodInvoker(Instance_OnMapRendered));
      }
      else
      {
        lblInformation.Visible = false;
        RedrawProgressBar.Visible = false;
      }
    }

    void Instance_OnMapRendering()
    {
      if (InvokeRequired)
      {
        try
        {
          Invoke(new MethodInvoker(Instance_OnMapRendering));
        }
        catch (Exception ex)
        {
          IntelliTrack.Client.Application.Logging.logError.Error(ex.Message, ex);
        }
      }
      else
      {
        lblInformation.Visible = true;
        lblInformation.Text = "";
        RedrawProgressBar.Maximum = Singleton<ComplexMap>.Instance.LayerCount;
        if (RedrawProgressBar.Maximum != 0)
        {
          RedrawProgressBar.Minimum = 0;
          RedrawProgressBar.Value = 1;
          RedrawProgressBar.Visible = true;
        }
      }
    }

    private void ComposeImage()
    {
      if (DesignMode)
        return;

      if (Size.Width == 0)
      {
        return;
      }

      mBackgroundImage = Singleton<ComplexMap>.Instance.GetMap();
      this.BackgroundImage = mBackgroundImage;
    }


    private void MapForm_SizeChanged(object sender, EventArgs e)
    {
      if (DesignMode)
        return;

      Singleton<ComplexMap>.Instance.MapSize = this.Size;
    }

    static int refreshcounter = 0;
    public override void RefreshMe()
    {
      refreshcounter++;
      System.Diagnostics.Debug.WriteLine("Refresh Nro. " + refreshcounter.ToString());
      ComposeImage();
    }

    #region Toolbar Handling

    Point ptOriginal = new Point();
    Point ptLast = new Point();

    private bool _MousePressed = false;

    private enum CommandActions
    {
      NO_ACTION,
      ZOOM_WINDOW,
/*      ZOOM_IN,
      ZOOM_OUT,*/
      PAN,
      INFORMATION
    };

    CommandActions _CommandAction = CommandActions.NO_ACTION;

    private void ZoomWindow_Click(object sender, EventArgs e)
    {
      if (_MousePressed)
        return;
      _CommandAction = CommandActions.ZOOM_WINDOW;
    }

    private void ZoomIn_Click(object sender, EventArgs e)
    {
      Singleton<ComplexMap>.Instance.ZoomIn();
      /*if (_MousePressed)
        return;
      _CommandAction = CommandActions.ZOOM_IN;*/
    }

    private void ZoomOut_Click(object sender, EventArgs e)
    {
      Singleton<ComplexMap>.Instance.ZoomOut();
      /*if (_MousePressed)
        return;
      _CommandAction = CommandActions.ZOOM_OUT;*/
    }

    private void Pan_Click(object sender, EventArgs e)
    {
      if (_MousePressed)
        return;

      _CommandAction = CommandActions.PAN;

    }


    private void MapForm_MouseDown(object sender, MouseEventArgs e)
    {
      _MousePressed = true;
      if (e.Button == MouseButtons.Middle)
      {
        _CommandAction = CommandActions.PAN;
        Cursor.Current = Cursors.NoMove2D;
      }

      ptOriginal.X = e.X;
      ptOriginal.Y = e.Y;
      ptLast.X = -1;
      ptLast.Y = -1;
    }


    // Convert and normalize the points and draw the reversible frame.
    private void MyDrawReversibleRectangle(Point p1, Point p2, bool DrawRectangle)
    {
      Rectangle rc = new Rectangle();

      // Convert the points to screen coordinates.
      p1 = PointToScreen(p1);
      p2 = PointToScreen(p2);
      // Normalize the rectangle.
      if (p1.X < p2.X)
      {
        rc.X = p1.X;
        rc.Width = p2.X - p1.X;
      }
      else
      {
        rc.X = p2.X;
        rc.Width = p1.X - p2.X;
      }
      if (p1.Y < p2.Y)
      {
        rc.Y = p1.Y;
        rc.Height = p2.Y - p1.Y;
      }
      else
      {
        rc.Y = p2.Y;
        rc.Height = p1.Y - p2.Y;
      }
      // Draw the reversible frame.
      if (DrawRectangle)
      {
        ControlPaint.DrawReversibleFrame(rc, Color.Red, FrameStyle.Dashed);
      }
      else
      {
        Image img = new System.Drawing.Bitmap(Width, Height);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);

        g.PageUnit = System.Drawing.GraphicsUnit.Pixel;
        if (mBackgroundImage != null)
        {
          g.FillRectangle(System.Drawing.Brushes.DarkGray, 0, 0, Width, Height);
          g.DrawImage(mBackgroundImage, p2.X - p1.X, p2.Y - p1.Y);
        }

        BackgroundImage = img;
        g.Dispose();
      }
    }


    private void MapForm_MouseMove(object sender, MouseEventArgs e)
    {
      Point ptCurrent = new Point(e.X, e.Y);
      switch (_CommandAction)
      {
        case CommandActions.PAN:
          Cursor.Current = PanCursor;
          break;
        case CommandActions.ZOOM_WINDOW:
          Cursor.Current = ZoomWindowCursor;
          break;
        default:
          Cursor.Current = Cursors.Default;
          break;
      }

      if (_MousePressed && _CommandAction != CommandActions.NO_ACTION)
      {

        // If we have drawn previously, draw again in
        // that spot to remove the lines.
        if (ptLast.X != -1)
        {
          MyDrawReversibleRectangle(ptOriginal, ptLast, _CommandAction == CommandActions.ZOOM_WINDOW);
        }
        // Update last point.
        ptLast = ptCurrent;
        // Draw new lines.
        MyDrawReversibleRectangle(ptOriginal, ptCurrent, _CommandAction == CommandActions.ZOOM_WINDOW);
      }
    }

    private void MapForm_MouseUp(object sender, MouseEventArgs e)
    {
      // Set internal flag to know we no longer "have the mouse".
      if (_MousePressed)
      {
        // If we have drawn previously, draw again in that spot
        // to remove the lines.
        if (ptLast.X != -1)
        {
          Point ptCurrent = new Point(e.X, e.Y);
          MyDrawReversibleRectangle(ptOriginal, ptLast, _CommandAction==CommandActions.ZOOM_WINDOW);
        }
          
        ProcessCommand();

        // Set flags to know that there is no "previous" line to reverse.
        ptLast.X = -1;
        ptLast.Y = -1;
        ptOriginal.X = -1;
        ptOriginal.Y = -1;
      }
      _MousePressed = false;
      Cursor.Current = Cursors.Arrow;
    }

    #endregion

    private void ProcessCommand()
    {
      switch (_CommandAction)
      {
        case CommandActions.INFORMATION:
          {
            SharpMap.Geometries.Point p0 = Singleton<ComplexMap>.Instance.ImageToWorld(ptOriginal);
            SharpMap.Geometries.Point p1 = Singleton<ComplexMap>.Instance.ImageToWorld(ptLast);
            List<SharpMap.Geometries.BoundingBox> lst = new List<SharpMap.Geometries.BoundingBox>();
            lst.Add(p0.GetBoundingBox());
            lst.Add(p1.GetBoundingBox());
            SharpMap.Geometries.BoundingBox bbox = new SharpMap.Geometries.BoundingBox(lst);

            SharpMap.Data.FeatureDataSet fds = Singleton<ComplexMap>.Instance.SelectElements(bbox, "caminos");
            fds.Tables[0].TableName = "Caminos";
            SharpMap.Data.FeatureDataSet fds2 = Singleton<ComplexMap>.Instance.SelectElements(bbox, "Referencias");
            fds2.Tables[0].TableName = "Referencias";

            ((Form1)_parent).elementInfo.ClearData();
            ((Form1)_parent).elementInfo.SetData(fds.Tables[0]);
            ((Form1)_parent).elementInfo.SetData(fds2.Tables[0]);

          }
          break;
        case CommandActions.PAN:
          {
            SharpMap.Geometries.Point p0 = Singleton<ComplexMap>.Instance.ImageToWorld(ptOriginal);
            SharpMap.Geometries.Point p1 = Singleton<ComplexMap>.Instance.ImageToWorld(ptLast);
            Singleton<ComplexMap>.Instance.Pan(p0, p1);
          }
          break;
        /*case CommandActions.ZOOM_IN:
          {
            SharpMap.Geometries.Point p = Singleton<ComplexMap>.Instance.ImageToWorld(ptLast);
            Singleton<ComplexMap>.Instance.ZoomIn();
          }
          break;*/
        /*case CommandActions.ZOOM_OUT:
          {
            SharpMap.Geometries.Point p = Singleton<ComplexMap>.Instance.ImageToWorld(ptLast);
            Singleton<ComplexMap>.Instance.ZoomOut();
          }
          break;*/
        case CommandActions.ZOOM_WINDOW:
          {
            SharpMap.Geometries.Point p0 = Singleton<ComplexMap>.Instance.ImageToWorld(ptOriginal);
            SharpMap.Geometries.Point p1 = Singleton<ComplexMap>.Instance.ImageToWorld(ptLast);
            List<SharpMap.Geometries.BoundingBox> lst = new List<SharpMap.Geometries.BoundingBox>();
            lst.Add(p0.GetBoundingBox());
            lst.Add(p1.GetBoundingBox());
            SharpMap.Geometries.BoundingBox bbox = new SharpMap.Geometries.BoundingBox(lst);
            Singleton<ComplexMap>.Instance.ZoomToBox(bbox);
          }
          break;
        default:
          break;
      }
      //_CommandAction = CommandActions.NO_ACTION;
    }

    private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      Singleton<ComplexMap>.Instance.Stop();
    }

    private void MapForm_Scroll(object sender, ScrollEventArgs e)
    {

    }

    void btnConfiguracionConsultaHistorica_Click(object sender, System.EventArgs e)
    {
      Singleton<ComplexMap>.Instance.SetupHistoricDataHandler();
    }

    void btnDetener_Click(object sender, System.EventArgs e)
    {
      Singleton<ComplexMap>.Instance.StopHistoricDataHandler();
      // o stop??
    }

    void btnPausa_Click(object sender, System.EventArgs e)
    {
      Singleton<ComplexMap>.Instance.PauseHistoricDataHandler();
    }

    void btnEjecutar_Click(object sender, System.EventArgs e)
    {
      Singleton<ComplexMap>.Instance.StartHistoricDataHandler();
    }

    void btnInicio_Click(object sender, System.EventArgs e)
    {
      Singleton<ComplexMap>.Instance.RewindHistoricDataHandler();
    }

    private void udRegistrosPorSegundo_ValueChanged(object sender, System.EventArgs e)
    {
      Singleton<ComplexMap>.Instance.SetMuestrasPorSegundo((int)(1000M) / Convert.ToInt32(udRegistrosPorSegundo.Value));
    }

    private void trackPosicion_Scroll(object sender, System.EventArgs e)
    {
      if (!trackFromAnimation)
        Singleton<ComplexMap>.Instance.SetHistoricDataHandlerPosition(trackPosicion.Value);
    }

        private void Info_Click(object sender, System.EventArgs e)
        {
          Singleton<ComplexMap>.Instance.ShowInfoElementInDialog();
        }

    private void OnHistoryQueryPosition(int Position, System.DateTime DatePresented)
    {
      if (InvokeRequired)
      {
        try
        {
          if (!IsDisposed)
          {
            Invoke(new MethodInvoker(
              delegate()
              {
                OnHistoryQueryPosition(Position, DatePresented);
              }));
          }
        }
        catch (System.ObjectDisposedException e)
        {
          IntelliTrack.Client.Application.Logging.logError.Error(e.Message, e);
        }
      }
      else
      {
        trackFromAnimation = true;
        trackPosicion.Value = Position;
        if (DatePresented == System.DateTime.MinValue)
          lblPosicion.Text = "";
        else
          lblPosicion.Text = DatePresented.ToString("dd/MM/yyyy HH:mm:ss");
        trackFromAnimation = false;
      }
    }

    private IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS QueryStatus;
    private void OnHistoryQueryStatus(IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS CurrentStatus)
    {
      udRegistrosPorSegundo.Value = 1000 / Singleton<ComplexMap>.Instance.GetMuestrasPorSegundo();
      QueryStatus = CurrentStatus;
      lblEstado.Text = Status2String(CurrentStatus);
      if (CurrentStatus == IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS.RUNNING)
        trackPosicion.SetRange(0, Singleton<ComplexMap>.Instance.GetHistoryRecordCount());
    }

    
        private string Status2String(IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS Status)
        {
            string s = "";
            switch (Status)
            {
                case IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS.QUERYING:
                    s = "Consultando";
                    break;

                case IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS.RUNNING:
                    s = "Reproduciendo";
                    break;

                case IntelliTrack.Client.Application.Dataset.DataRetriever.QUERYSTATUS.STOPPED:
                    s = "Detenido";
                    break;

                default:
                    s = "";
                    break;
            }
            return s;
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
          Singleton<ComplexMap>.Instance.LaunchInBrowser();
        }

    } // class MapForm

}
