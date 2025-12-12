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

            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.lblTitulo.Location = new Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Generar Reportes";
            this.lblTitulo.Size = new Size(200, 20);

            this.btnReporteImpuestos.Location = new Point(20, 60);
            this.btnReporteImpuestos.Name = "btnReporteImpuestos";
            this.btnReporteImpuestos.Size = new Size(150, 35);
            this.btnReporteImpuestos.Text = "Reporte Impuestos";
            this.btnReporteImpuestos.UseVisualStyleBackColor = true;
            this.btnReporteImpuestos.Click += new EventHandler(this.btnReporteImpuestos_Click);

            this.btnReporteComisiones.Location = new Point(190, 60);
            this.btnReporteComisiones.Name = "btnReporteComisiones";
            this.btnReporteComisiones.Size = new Size(150, 35);
            this.btnReporteComisiones.Text = "Reporte Comisiones";
            this.btnReporteComisiones.UseVisualStyleBackColor = true;
            this.btnReporteComisiones.Click += new EventHandler(this.btnReporteComisiones_Click);

            this.btnReporteGeneral.Location = new Point(360, 60);
            this.btnReporteGeneral.Name = "btnReporteGeneral";
            this.btnReporteGeneral.Size = new Size(150, 35);
            this.btnReporteGeneral.Text = "Reporte General";
            this.btnReporteGeneral.UseVisualStyleBackColor = true;
            this.btnReporteGeneral.Click += new EventHandler(this.btnReporteGeneral_Click);

            this.txtResultado.Location = new Point(20, 110);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.ScrollBars = ScrollBars.Vertical;
            this.txtResultado.Size = new Size(490, 200);
            this.txtResultado.Font = new Font("Consolas", 9F);

            this.dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultados.Location = new Point(20, 320);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.Size = new Size(600, 200);
            this.dgvResultados.ReadOnly = true;

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(640, 540);
            this.Controls.Add(this.lblTitulo);
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

