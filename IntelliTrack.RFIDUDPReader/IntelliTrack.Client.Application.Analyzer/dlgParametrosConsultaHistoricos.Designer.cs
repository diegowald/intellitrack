namespace IntelliTrack.Client.Application
{
  partial class dlgParametrosConsultaHistoricos
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      btnOK = new System.Windows.Forms.Button();
      btnCancel = new System.Windows.Forms.Button();
      label1 = new System.Windows.Forms.Label();
      dtFechaInicio = new System.Windows.Forms.DateTimePicker();
      dtFechaFin = new System.Windows.Forms.DateTimePicker();
      label2 = new System.Windows.Forms.Label();
      InputVelocidadMinima = new System.Windows.Forms.NumericUpDown();
      label7 = new System.Windows.Forms.Label();
      InputVelocidadMaxima = new System.Windows.Forms.NumericUpDown();
      InputMuestrasPorSegundo = new System.Windows.Forms.NumericUpDown();
      label8 = new System.Windows.Forms.Label();
      label9 = new System.Windows.Forms.Label();
      InputQueryTimeOut = new System.Windows.Forms.NumericUpDown();
      label3 = new System.Windows.Forms.Label();
      label4 = new System.Windows.Forms.Label();
      label5 = new System.Windows.Forms.Label();
      label10 = new System.Windows.Forms.Label();
      label11 = new System.Windows.Forms.Label();
      label12 = new System.Windows.Forms.Label();
      chkGenerarReporte = new System.Windows.Forms.CheckBox();
      cboCategoria = new System.Windows.Forms.ComboBox();
      txtVehiculo = new System.Windows.Forms.TextBox();
      txtTransponder = new System.Windows.Forms.TextBox();
      txtPunto = new System.Windows.Forms.TextBox();
      txtCamino = new System.Windows.Forms.TextBox();
      txtArea = new System.Windows.Forms.TextBox();
      chkVelocidad = new System.Windows.Forms.CheckBox();
      label6 = new System.Windows.Forms.Label();
      InputVelocidadMinima.BeginInit();
      InputVelocidadMaxima.BeginInit();
      InputMuestrasPorSegundo.BeginInit();
      InputQueryTimeOut.BeginInit();
      SuspendLayout();
      btnOK.Location = new System.Drawing.Point(199, 317);
      btnOK.Name = "btnOK";
      btnOK.Size = new System.Drawing.Size(63, 26);
      btnOK.TabIndex = 26;
      btnOK.Text = "OK";
      btnOK.UseVisualStyleBackColor = true;
      btnOK.Click += new System.EventHandler(btnOK_Click);
      btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      btnCancel.Location = new System.Drawing.Point(272, 317);
      btnCancel.Name = "btnCancel";
      btnCancel.Size = new System.Drawing.Size(63, 26);
      btnCancel.TabIndex = 27;
      btnCancel.Text = "Cancelar";
      btnCancel.UseVisualStyleBackColor = true;
      label1.AutoSize = true;
      label1.Location = new System.Drawing.Point(19, 16);
      label1.Name = "label1";
      label1.Size = new System.Drawing.Size(38, 13);
      label1.TabIndex = 0;
      label1.Text = "Desde";
      dtFechaInicio.AllowDrop = true;
      dtFechaInicio.CustomFormat = "dd/MM/yyyy HH:mm:ss";
      dtFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      dtFechaInicio.Location = new System.Drawing.Point(92, 12);
      dtFechaInicio.Name = "dtFechaInicio";
      dtFechaInicio.Size = new System.Drawing.Size(244, 20);
      dtFechaInicio.TabIndex = 1;
      dtFechaFin.AllowDrop = true;
      dtFechaFin.CustomFormat = "dd/MM/yyyy HH:mm:ss";
      dtFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      dtFechaFin.Location = new System.Drawing.Point(92, 36);
      dtFechaFin.Name = "dtFechaFin";
      dtFechaFin.Size = new System.Drawing.Size(244, 20);
      dtFechaFin.TabIndex = 3;
      label2.AutoSize = true;
      label2.Location = new System.Drawing.Point(19, 40);
      label2.Name = "label2";
      label2.Size = new System.Drawing.Size(35, 13);
      label2.TabIndex = 2;
      label2.Text = "Hasta";
      InputVelocidadMinima.Location = new System.Drawing.Point(127, 209);
      int[] iArr1 = new int[4];
      iArr1[0] = 200;
      InputVelocidadMinima.Maximum = new System.Decimal(iArr1);
      InputVelocidadMinima.Name = "InputVelocidadMinima";
      InputVelocidadMinima.Size = new System.Drawing.Size(53, 20);
      InputVelocidadMinima.TabIndex = 17;
      label7.AutoSize = true;
      label7.Location = new System.Drawing.Point(186, 211);
      label7.Name = "label7";
      label7.Size = new System.Drawing.Size(12, 13);
      label7.TabIndex = 18;
      label7.Text = "y";
      InputVelocidadMaxima.Location = new System.Drawing.Point(204, 209);
      int[] iArr2 = new int[4];
      iArr2[0] = 200;
      InputVelocidadMaxima.Maximum = new System.Decimal(iArr2);
      InputVelocidadMaxima.Name = "InputVelocidadMaxima";
      InputVelocidadMaxima.Size = new System.Drawing.Size(53, 20);
      InputVelocidadMaxima.TabIndex = 19;
      InputMuestrasPorSegundo.Location = new System.Drawing.Point(135, 243);
      int[] iArr3 = new int[4];
      iArr3[0] = 1;
      InputMuestrasPorSegundo.Minimum = new System.Decimal(iArr3);
      InputMuestrasPorSegundo.Name = "InputMuestrasPorSegundo";
      InputMuestrasPorSegundo.Size = new System.Drawing.Size(53, 20);
      InputMuestrasPorSegundo.TabIndex = 22;
      int[] iArr4 = new int[4];
      iArr4[0] = 1;
      InputMuestrasPorSegundo.Value = new System.Decimal(iArr4);
      label8.AutoSize = true;
      label8.Location = new System.Drawing.Point(17, 245);
      label8.Name = "label8";
      label8.Size = new System.Drawing.Size(112, 13);
      label8.TabIndex = 21;
      label8.Text = "Muestras por segundo";
      label9.AutoSize = true;
      label9.Location = new System.Drawing.Point(16, 266);
      label9.Name = "label9";
      label9.Size = new System.Drawing.Size(197, 13);
      label9.TabIndex = 23;
      label9.Text = "Tiempo m\u00E1ximo de espera para consulta";
      InputQueryTimeOut.Location = new System.Drawing.Point(214, 264);
      int[] iArr5 = new int[4];
      iArr5[0] = 600;
      InputQueryTimeOut.Maximum = new System.Decimal(iArr5);
      int[] iArr6 = new int[4];
      iArr6[0] = 1;
      InputQueryTimeOut.Minimum = new System.Decimal(iArr6);
      InputQueryTimeOut.Name = "InputQueryTimeOut";
      InputQueryTimeOut.Size = new System.Drawing.Size(53, 20);
      InputQueryTimeOut.TabIndex = 24;
      int[] iArr7 = new int[4];
      iArr7[0] = 1;
      InputQueryTimeOut.Value = new System.Decimal(iArr7);
      label3.AutoSize = true;
      label3.Location = new System.Drawing.Point(19, 64);
      label3.Name = "label3";
      label3.Size = new System.Drawing.Size(54, 13);
      label3.TabIndex = 4;
      label3.Text = "Categor\u00EDa";
      label4.AutoSize = true;
      label4.Location = new System.Drawing.Point(19, 89);
      label4.Name = "label4";
      label4.Size = new System.Drawing.Size(50, 13);
      label4.TabIndex = 6;
      label4.Text = "Veh\u00EDculo";
      label5.AutoSize = true;
      label5.Location = new System.Drawing.Point(19, 113);
      label5.Name = "label5";
      label5.Size = new System.Drawing.Size(67, 13);
      label5.TabIndex = 8;
      label5.Text = "Transponder";
      label10.AutoSize = true;
      label10.Location = new System.Drawing.Point(19, 137);
      label10.Name = "label10";
      label10.Size = new System.Drawing.Size(35, 13);
      label10.TabIndex = 10;
      label10.Text = "Punto";
      label11.AutoSize = true;
      label11.Location = new System.Drawing.Point(19, 161);
      label11.Name = "label11";
      label11.Size = new System.Drawing.Size(42, 13);
      label11.TabIndex = 12;
      label11.Text = "Camino";
      label12.AutoSize = true;
      label12.Location = new System.Drawing.Point(19, 185);
      label12.Name = "label12";
      label12.Size = new System.Drawing.Size(29, 13);
      label12.TabIndex = 14;
      label12.Text = "Area";
      chkGenerarReporte.AutoSize = true;
      chkGenerarReporte.Location = new System.Drawing.Point(16, 290);
      chkGenerarReporte.Name = "chkGenerarReporte";
      chkGenerarReporte.Size = new System.Drawing.Size(100, 17);
      chkGenerarReporte.TabIndex = 25;
      chkGenerarReporte.Text = "Generar reporte";
      chkGenerarReporte.UseVisualStyleBackColor = true;
      cboCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      cboCategoria.FormattingEnabled = true;
      cboCategoria.Location = new System.Drawing.Point(92, 60);
      cboCategoria.Name = "cboCategoria";
      cboCategoria.Size = new System.Drawing.Size(244, 21);
      cboCategoria.TabIndex = 5;
      txtVehiculo.Location = new System.Drawing.Point(92, 85);
      txtVehiculo.Name = "txtVehiculo";
      txtVehiculo.Size = new System.Drawing.Size(244, 20);
      txtVehiculo.TabIndex = 7;
      txtTransponder.Location = new System.Drawing.Point(92, 109);
      txtTransponder.Name = "txtTransponder";
      txtTransponder.Size = new System.Drawing.Size(244, 20);
      txtTransponder.TabIndex = 9;
      txtPunto.Location = new System.Drawing.Point(92, 133);
      txtPunto.Name = "txtPunto";
      txtPunto.Size = new System.Drawing.Size(244, 20);
      txtPunto.TabIndex = 11;
      txtCamino.Location = new System.Drawing.Point(92, 157);
      txtCamino.Name = "txtCamino";
      txtCamino.Size = new System.Drawing.Size(244, 20);
      txtCamino.TabIndex = 13;
      txtArea.Location = new System.Drawing.Point(92, 181);
      txtArea.Name = "txtArea";
      txtArea.Size = new System.Drawing.Size(244, 20);
      txtArea.TabIndex = 15;
      chkVelocidad.AutoSize = true;
      chkVelocidad.Location = new System.Drawing.Point(22, 210);
      chkVelocidad.Name = "chkVelocidad";
      chkVelocidad.Size = new System.Drawing.Size(100, 17);
      chkVelocidad.TabIndex = 16;
      chkVelocidad.Text = "Velocidad entre";
      chkVelocidad.UseVisualStyleBackColor = true;
      chkVelocidad.CheckedChanged += new System.EventHandler(chkVelocidad_CheckedChanged);
      label6.AutoSize = true;
      label6.Location = new System.Drawing.Point(263, 211);
      label6.Name = "label6";
      label6.Size = new System.Drawing.Size(32, 13);
      label6.TabIndex = 20;
      label6.Text = "km/h";
      AcceptButton = btnOK;
      AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      CancelButton = btnCancel;
      ClientSize = new System.Drawing.Size(346, 351);
      ControlBox = false;
      Controls.Add(label6);
      Controls.Add(chkVelocidad);
      Controls.Add(txtArea);
      Controls.Add(txtCamino);
      Controls.Add(txtPunto);
      Controls.Add(txtTransponder);
      Controls.Add(txtVehiculo);
      Controls.Add(cboCategoria);
      Controls.Add(chkGenerarReporte);
      Controls.Add(label12);
      Controls.Add(label11);
      Controls.Add(label10);
      Controls.Add(label5);
      Controls.Add(label4);
      Controls.Add(label3);
      Controls.Add(InputQueryTimeOut);
      Controls.Add(label9);
      Controls.Add(label8);
      Controls.Add(InputMuestrasPorSegundo);
      Controls.Add(InputVelocidadMaxima);
      Controls.Add(label7);
      Controls.Add(InputVelocidadMinima);
      Controls.Add(dtFechaFin);
      Controls.Add(label2);
      Controls.Add(dtFechaInicio);
      Controls.Add(label1);
      Controls.Add(btnCancel);
      Controls.Add(btnOK);
      Name = "dlgParametrosConsultaHistoricos";
      ShowInTaskbar = false;
      StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      Text = "Filtro de selecci\u00F3n";
      Load += new System.EventHandler(dlgParametrosConsultaHistoricos_Load);
      InputVelocidadMinima.EndInit();
      InputVelocidadMaxima.EndInit();
      InputMuestrasPorSegundo.EndInit();
      InputQueryTimeOut.EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion


    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.ComboBox cboCategoria;
    private System.Windows.Forms.CheckBox chkGenerarReporte;
    private System.Windows.Forms.CheckBox chkVelocidad;
    private System.Windows.Forms.DateTimePicker dtFechaFin;
    private System.Windows.Forms.DateTimePicker dtFechaInicio;
    private System.Windows.Forms.NumericUpDown InputMuestrasPorSegundo;
    private System.Windows.Forms.NumericUpDown InputQueryTimeOut;
    private System.Windows.Forms.NumericUpDown InputVelocidadMaxima;
    private System.Windows.Forms.NumericUpDown InputVelocidadMinima;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtArea;
    private System.Windows.Forms.TextBox txtCamino;
    private System.Windows.Forms.TextBox txtPunto;
    private System.Windows.Forms.TextBox txtTransponder;
    private System.Windows.Forms.TextBox txtVehiculo;
  }
}