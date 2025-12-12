using SportsbookPatterns.BE;
using SportsbookPatterns.BE.Visitor;
using SportsbookPatterns.BLL.Visitor;
using SportsbookPatterns.DAL.Abstraccion;

namespace DiplomaFinal.Forms
{
    public partial class FrmVisitor : Form
    {
        private readonly ITransaccionRepository _transaccionRepo;
        private readonly ITransaccionApuestaRepository _apuestaRepo;
        private readonly ITransaccionRetiroRepository _retiroRepo;
        private readonly ITransaccionDepositoRepository _depositoRepo;

        public FrmVisitor(
            ITransaccionRepository transaccionRepo,
            ITransaccionApuestaRepository apuestaRepo,
            ITransaccionRetiroRepository retiroRepo,
            ITransaccionDepositoRepository depositoRepo)
        {
            InitializeComponent();
            _transaccionRepo = transaccionRepo;
            _apuestaRepo = apuestaRepo;
            _retiroRepo = retiroRepo;
            _depositoRepo = depositoRepo;
        }

        private void btnReporteImpuestos_Click(object sender, EventArgs e)
        {
            try
            {
                var visitor = new CalculadoraImpuestosVisitor();
                ProcesarReporte(visitor, "Reporte de Impuestos");
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
                var visitor = new GeneradorComisionesVisitor();
                ProcesarReporte(visitor, "Reporte de Comisiones");
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
                var visitorImpuestos = new CalculadoraImpuestosVisitor();
                var visitorComisiones = new GeneradorComisionesVisitor();

                var transacciones = _transaccionRepo.GetAll();
                var elementos = new List<ITransaccionVisitable>();

                foreach (var transaccion in transacciones)
                {
                    var apuesta = _apuestaRepo.GetByTransaccionId(transaccion.TransaccionId);
                    if (apuesta != null)
                    {
                        apuesta.Transaccion = transaccion;
                        elementos.Add(apuesta);
                        continue;
                    }

                    var retiro = _retiroRepo.GetByTransaccionId(transaccion.TransaccionId);
                    if (retiro != null)
                    {
                        retiro.Transaccion = transaccion;
                        elementos.Add(retiro);
                        continue;
                    }

                    var deposito = _depositoRepo.GetByTransaccionId(transaccion.TransaccionId);
                    if (deposito != null)
                    {
                        deposito.Transaccion = transaccion;
                        elementos.Add(deposito);
                    }
                }

                foreach (var elemento in elementos)
                {
                    elemento.Accept(visitorImpuestos);
                    elemento.Accept(visitorComisiones);
                }

                var resultadoImpuestos = visitorImpuestos.GetResultado();
                var resultadoComisiones = visitorComisiones.GetResultado();

                txtResultado.Text = $"=== REPORTE GENERAL ===\r\n\r\n";
                txtResultado.Text += $"IMPUESTOS:\r\n{resultadoImpuestos}\r\n\r\n";
                txtResultado.Text += $"COMISIONES:\r\n{resultadoComisiones}";

                var datos = new List<object> { new { Tipo = "Impuestos", Resultado = resultadoImpuestos.ToString() }, 
                    new { Tipo = "Comisiones", Resultado = resultadoComisiones.ToString() } };
                dgvResultados.DataSource = datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcesarReporte(ITransaccionVisitor visitor, string titulo)
        {
            var transacciones = _transaccionRepo.GetAll();
            var elementos = new List<ITransaccionVisitable>();

            foreach (var transaccion in transacciones)
            {
                var apuesta = _apuestaRepo.GetByTransaccionId(transaccion.TransaccionId);
                if (apuesta != null)
                {
                    apuesta.Transaccion = transaccion;
                    elementos.Add(apuesta);
                    continue;
                }

                var retiro = _retiroRepo.GetByTransaccionId(transaccion.TransaccionId);
                if (retiro != null)
                {
                    retiro.Transaccion = transaccion;
                    elementos.Add(retiro);
                    continue;
                }

                var deposito = _depositoRepo.GetByTransaccionId(transaccion.TransaccionId);
                if (deposito != null)
                {
                    deposito.Transaccion = transaccion;
                    elementos.Add(deposito);
                }
            }

            foreach (var elemento in elementos)
            {
                elemento.Accept(visitor);
            }

            var resultado = visitor.GetResultado();
            txtResultado.Text = $"{titulo}\r\n\r\n{resultado}";

            var datos = new List<object> { new { Tipo = titulo, Resultado = resultado.ToString() } };
            dgvResultados.DataSource = datos;
        }
    }
}

