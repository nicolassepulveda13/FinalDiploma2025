namespace DiplomaFinal.Forms
{
    partial class FrmVisitor
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnReporteImpuestos;
        private Button btnReporteComisiones;
        private Button btnReporteGeneral;
        private TextBox txtResultado;
        private DataGridView dgvResultados;
        private Label lblTitulo;
        private Label lblTipoTransaccion;
        private ComboBox cmbTipoTransaccion;
        private Label lblMonto;
        private TextBox txtMonto;
        private Button btnGuardarTransaccion;
        private GroupBox grpCrearTransaccion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnReporteImpuestos = new Button();
            this.btnReporteComisiones = new Button();
            this.btnReporteGeneral = new Button();
            this.txtResultado = new TextBox();
            this.dgvResultados = new DataGridView();
            this.lblTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();

            this.grpCrearTransaccion = new GroupBox();
            this.lblTipoTransaccion = new Label();
            this.cmbTipoTransaccion = new ComboBox();
            this.lblMonto = new Label();
            this.txtMonto = new TextBox();
            this.btnGuardarTransaccion = new Button();

            this.lblTitulo = new Label();
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.lblTitulo.Location = new Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Crear Transacciones";
            this.lblTitulo.Size = new Size(200, 20);

            this.grpCrearTransaccion.Location = new Point(20, 50);
            this.grpCrearTransaccion.Size = new Size(490, 100);
            this.grpCrearTransaccion.Text = "Nueva Transacción";
            this.grpCrearTransaccion.TabStop = false;

            this.lblTipoTransaccion.AutoSize = true;
            this.lblTipoTransaccion.Location = new Point(15, 25);
            this.lblTipoTransaccion.Name = "lblTipoTransaccion";
            this.lblTipoTransaccion.Text = "Tipo:";
            this.lblTipoTransaccion.Size = new Size(35, 15);

            this.cmbTipoTransaccion.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipoTransaccion.Location = new Point(60, 22);
            this.cmbTipoTransaccion.Name = "cmbTipoTransaccion";
            this.cmbTipoTransaccion.Size = new Size(150, 23);

            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new Point(230, 25);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Text = "Monto:";
            this.lblMonto.Size = new Size(45, 15);

            this.txtMonto.Location = new Point(280, 22);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new Size(100, 23);

            this.btnGuardarTransaccion.Location = new Point(390, 21);
            this.btnGuardarTransaccion.Name = "btnGuardarTransaccion";
            this.btnGuardarTransaccion.Size = new Size(85, 25);
            this.btnGuardarTransaccion.Text = "Guardar";
            this.btnGuardarTransaccion.UseVisualStyleBackColor = true;
            this.btnGuardarTransaccion.Click += new EventHandler(this.btnGuardarTransaccion_Click);

            this.grpCrearTransaccion.Controls.Add(this.lblTipoTransaccion);
            this.grpCrearTransaccion.Controls.Add(this.cmbTipoTransaccion);
            this.grpCrearTransaccion.Controls.Add(this.lblMonto);
            this.grpCrearTransaccion.Controls.Add(this.txtMonto);
            this.grpCrearTransaccion.Controls.Add(this.btnGuardarTransaccion);

            this.btnReporteImpuestos.Location = new Point(20, 160);
            this.btnReporteImpuestos.Name = "btnReporteImpuestos";
            this.btnReporteImpuestos.Size = new Size(150, 35);
            this.btnReporteImpuestos.Text = "Reporte Impuestos";
            this.btnReporteImpuestos.UseVisualStyleBackColor = true;
            this.btnReporteImpuestos.Click += new EventHandler(this.btnReporteImpuestos_Click);

            this.btnReporteComisiones.Location = new Point(190, 160);
            this.btnReporteComisiones.Name = "btnReporteComisiones";
            this.btnReporteComisiones.Size = new Size(150, 35);
            this.btnReporteComisiones.Text = "Reporte Comisiones";
            this.btnReporteComisiones.UseVisualStyleBackColor = true;
            this.btnReporteComisiones.Click += new EventHandler(this.btnReporteComisiones_Click);

            this.btnReporteGeneral.Location = new Point(360, 160);
            this.btnReporteGeneral.Name = "btnReporteGeneral";
            this.btnReporteGeneral.Size = new Size(150, 35);
            this.btnReporteGeneral.Text = "Reporte General";
            this.btnReporteGeneral.UseVisualStyleBackColor = true;
            this.btnReporteGeneral.Click += new EventHandler(this.btnReporteGeneral_Click);

            this.txtResultado.Location = new Point(20, 210);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.ScrollBars = ScrollBars.Vertical;
            this.txtResultado.Size = new Size(490, 200);
            this.txtResultado.Font = new Font("Consolas", 9F);

            this.dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultados.Location = new Point(20, 420);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.Size = new Size(600, 200);
            this.dgvResultados.ReadOnly = true;

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(640, 640);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.grpCrearTransaccion);
            this.Controls.Add(this.btnReporteImpuestos);
            this.Controls.Add(this.btnReporteComisiones);
            this.Controls.Add(this.btnReporteGeneral);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.dgvResultados);
            this.Name = "FrmVisitor";
            this.Text = "Patrón Visitor - Generación de Reportes";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

