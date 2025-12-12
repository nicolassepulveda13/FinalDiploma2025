using SportsbookPatterns.BLL.Services;

namespace DiplomaFinal.Forms
{
    public partial class FrmVisitor : Form
    {
        private readonly ReporteService _reporteService;
        private readonly TransaccionService _transaccionService;

        public FrmVisitor(ReporteService reporteService, TransaccionService transaccionService)
        {
            InitializeComponent();
            _reporteService = reporteService;
            _transaccionService = transaccionService;
            CargarTiposTransaccion();
        }

        private void CargarTiposTransaccion()
        {
            cmbTipoTransaccion.Items.Clear();
            cmbTipoTransaccion.Items.Add("Apuesta");
            cmbTipoTransaccion.Items.Add("Retiro");
            cmbTipoTransaccion.Items.Add("Depósito");
            if (cmbTipoTransaccion.Items.Count > 0)
                cmbTipoTransaccion.SelectedIndex = 0;
        }

        private void btnReporteImpuestos_Click(object sender, EventArgs e)
        {
            try
            {
                var resultado = _reporteService.GenerarReporteImpuestos();
                txtResultado.Text = $"Reporte de Impuestos\r\n\r\n{resultado}";
                var datos = new List<object> { new { Tipo = "Impuestos", Resultado = resultado.ToString() } };
                dgvResultados.DataSource = datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReporteComisiones_Click(object sender, EventArgs e)
        {
            try
            {
                var resultado = _reporteService.GenerarReporteComisiones();
                txtResultado.Text = $"Reporte de Comisiones\r\n\r\n{resultado}";
                var datos = new List<object> { new { Tipo = "Comisiones", Resultado = resultado.ToString() } };
                dgvResultados.DataSource = datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReporteGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                var (resultadoImpuestos, resultadoComisiones) = _reporteService.GenerarReporteGeneral();

                txtResultado.Text = $"=== REPORTE GENERAL ===\r\n\r\n";
                txtResultado.Text += $"IMPUESTOS:\r\n{resultadoImpuestos}\r\n\r\n";
                txtResultado.Text += $"COMISIONES:\r\n{resultadoComisiones}";

                var datos = new List<object> { 
                    new { Tipo = "Impuestos", Resultado = resultadoImpuestos.ToString() }, 
                    new { Tipo = "Comisiones", Resultado = resultadoComisiones.ToString() } 
                };
                dgvResultados.DataSource = datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarTransaccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTipoTransaccion.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un tipo de transacción.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
                {
                    MessageBox.Show("Ingrese un monto válido mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tipo = cmbTipoTransaccion.SelectedItem.ToString() ?? "";
                int transaccionId = 0;

                switch (tipo)
                {
                    case "Apuesta":
                        transaccionId = _transaccionService.CrearApuesta(monto);
                        break;
                    case "Retiro":
                        transaccionId = _transaccionService.CrearRetiro(monto);
                        break;
                    case "Depósito":
                        transaccionId = _transaccionService.CrearDeposito(monto);
                        break;
                }

                MessageBox.Show($"Transacción {tipo} creada exitosamente. ID: {transaccionId}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMonto.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

