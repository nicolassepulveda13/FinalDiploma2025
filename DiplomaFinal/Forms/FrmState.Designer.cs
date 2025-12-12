namespace DiplomaFinal.Forms
{
    partial class FrmState
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbUsuarios;
        private ComboBox cmbEstados;
        private Label lblSaldoActual;
        private Button btnApostar;
        private Button btnRetirar;
        private Button btnDepositar;
        private TextBox txtMonto;
        private DataGridView dgvHistorial;
        private Label lblUsuario;
        private Label lblEstado;
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
            this.cmbEstados = new ComboBox();
            this.lblSaldoActual = new Label();
            this.btnApostar = new Button();
            this.btnRetirar = new Button();
            this.btnDepositar = new Button();
            this.txtMonto = new TextBox();
            this.dgvHistorial = new DataGridView();
            this.lblUsuario = new Label();
            this.lblEstado = new Label();
            this.lblMonto = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
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

            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new Point(20, 60);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Text = "Estado:";

            this.cmbEstados.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbEstados.FormattingEnabled = true;
            this.cmbEstados.Location = new Point(100, 57);
            this.cmbEstados.Name = "cmbEstados";
            this.cmbEstados.Size = new Size(200, 23);
            this.cmbEstados.SelectedIndexChanged += new EventHandler(this.cmbEstados_SelectedIndexChanged);

            this.lblSaldoActual.AutoSize = true;
            this.lblSaldoActual.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblSaldoActual.Location = new Point(320, 20);
            this.lblSaldoActual.Name = "lblSaldoActual";
            this.lblSaldoActual.Text = "Saldo: $0.00";

            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new Point(20, 100);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Text = "Monto:";

            this.txtMonto.Location = new Point(100, 97);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new Size(100, 23);

            this.btnApostar.Location = new Point(20, 140);
            this.btnApostar.Name = "btnApostar";
            this.btnApostar.Size = new Size(100, 30);
            this.btnApostar.Text = "Apostar";
            this.btnApostar.UseVisualStyleBackColor = true;
            this.btnApostar.Click += new EventHandler(this.btnApostar_Click);

            this.btnRetirar.Location = new Point(130, 140);
            this.btnRetirar.Name = "btnRetirar";
            this.btnRetirar.Size = new Size(100, 30);
            this.btnRetirar.Text = "Retirar";
            this.btnRetirar.UseVisualStyleBackColor = true;
            this.btnRetirar.Click += new EventHandler(this.btnRetirar_Click);

            this.btnDepositar.Location = new Point(240, 140);
            this.btnDepositar.Name = "btnDepositar";
            this.btnDepositar.Size = new Size(100, 30);
            this.btnDepositar.Text = "Depositar";
            this.btnDepositar.UseVisualStyleBackColor = true;
            this.btnDepositar.Click += new EventHandler(this.btnDepositar_Click);

            this.dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.Location = new Point(20, 190);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.Size = new Size(600, 200);
            this.dgvHistorial.ReadOnly = true;

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(640, 420);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.cmbUsuarios);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.cmbEstados);
            this.Controls.Add(this.lblSaldoActual);
            this.Controls.Add(this.lblMonto);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.btnApostar);
            this.Controls.Add(this.btnRetirar);
            this.Controls.Add(this.btnDepositar);
            this.Controls.Add(this.dgvHistorial);
            this.Name = "FrmState";
            this.Text = "Patrón State - Gestión de Estados";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

