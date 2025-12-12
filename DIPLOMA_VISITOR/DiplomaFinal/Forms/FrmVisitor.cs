using SportsbookPatterns.BLL.Services;

namespace DiplomaFinal.Forms
{
    public partial class FrmVisitor : Form
    {
        private readonly ReporteService _reporteService;

        public FrmVisitor(ReporteService reporteService)
        {
            InitializeComponent();
            _reporteService = reporteService;
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
    }
}

