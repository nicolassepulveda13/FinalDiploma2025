namespace DiplomaFinal.Forms
{
    partial class FrmStrategy
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbUsuarios;
        private Label lblSaldoActual;
        private TextBox txtMontoApuesta;
        private Button btnApostar;
        private Label lblResultado;
        private DataGridView dgvTransacciones;
        private Label lblUsuario;
        private Label lblMonto;

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
            this.cmbUsuarios = new ComboBox();
            this.lblSaldoActual = new Label();
            this.txtMontoApuesta = new TextBox();
            this.btnApostar = new Button();
            this.lblResultado = new Label();
            this.dgvTransacciones = new DataGridView();
            this.lblUsuario = new Label();
            this.lblMonto = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransacciones)).BeginInit();
            this.SuspendLayout();

            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new Point(20, 20);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuario:";

            this.cmbUsuarios.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbUsuarios.FormattingEnabled = true;
            this.cmbUsuarios.Location = new Point(100, 17);
            this.cmbUsuarios.Name = "cmbUsuarios";
            this.cmbUsuarios.Size = new Size(200, 23);
            this.cmbUsuarios.SelectedIndexChanged += new EventHandler(this.cmbUsuarios_SelectedIndexChanged);

            this.lblSaldoActual.AutoSize = true;
            this.lblSaldoActual.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblSaldoActual.Location = new Point(320, 20);
            this.lblSaldoActual.Name = "lblSaldoActual";
            this.lblSaldoActual.Text = "Saldo: $0.00";

            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new Point(20, 60);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Text = "Monto Apuesta:";

            this.txtMontoApuesta.Location = new Point(130, 57);
            this.txtMontoApuesta.Name = "txtMontoApuesta";
            this.txtMontoApuesta.Size = new Size(100, 23);

            this.btnApostar.Location = new Point(250, 55);
            this.btnApostar.Name = "btnApostar";
            this.btnApostar.Size = new Size(100, 30);
            this.btnApostar.Text = "Apostar";
            this.btnApostar.UseVisualStyleBackColor = true;
            this.btnApostar.Click += new EventHandler(this.btnApostar_Click);

            this.lblResultado.AutoSize = true;
            this.lblResultado.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblResultado.Location = new Point(20, 100);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Text = "Resultado: -";
            this.lblResultado.Size = new Size(300, 20);

            this.dgvTransacciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransacciones.Location = new Point(20, 140);
            this.dgvTransacciones.Name = "dgvTransacciones";
            this.dgvTransacciones.Size = new Size(600, 250);
            this.dgvTransacciones.ReadOnly = true;

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(640, 420);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.cmbUsuarios);
            this.Controls.Add(this.lblSaldoActual);
            this.Controls.Add(this.lblMonto);
            this.Controls.Add(this.txtMontoApuesta);
            this.Controls.Add(this.btnApostar);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.dgvTransacciones);
            this.Name = "FrmStrategy";
            this.Text = "Patrón Strategy - Sistema de Apuestas";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransacciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

