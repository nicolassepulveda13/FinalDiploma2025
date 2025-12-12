using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.Strategy;
using SportsbookPatterns.DAL.Abstraccion;

namespace DiplomaFinal.Forms
{
    public partial class FrmStrategy : Form
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ICuentaUsuarioRepository _cuentaRepo;
        private readonly ITransaccionRepository _transaccionRepo;
        private readonly ITipoTransaccionRepository _tipoRepo;
        private readonly ProcesadorDeTransacciones _procesador;
        private CuentaUsuario? _cuentaActual;
        private Random _random = new Random();

        public FrmStrategy(
            IUsuarioRepository usuarioRepo,
            ICuentaUsuarioRepository cuentaRepo,
            ITransaccionRepository transaccionRepo,
            ITipoTransaccionRepository tipoRepo,
            ProcesadorDeTransacciones procesador)
        {
            InitializeComponent();
            _usuarioRepo = usuarioRepo;
            _cuentaRepo = cuentaRepo;
            _transaccionRepo = transaccionRepo;
            _tipoRepo = tipoRepo;
            _procesador = procesador;
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            var usuarios = _usuarioRepo.GetAll();
            cmbUsuarios.DataSource = usuarios;
            cmbUsuarios.DisplayMember = "Nombre";
            cmbUsuarios.ValueMember = "UsuarioId";
        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedValue != null)
            {
                int usuarioId = (int)cmbUsuarios.SelectedValue;
                _cuentaActual = _cuentaRepo.GetByUsuarioId(usuarioId);
                if (_cuentaActual != null)
                {
                    lblSaldoActual.Text = $"Saldo: ${_cuentaActual.Saldo:F2}";
                    CargarTransacciones();
                }
            }
        }

        private void btnApostar_Click(object sender, EventArgs e)
        {
            if (_cuentaActual == null)
            {
                MessageBox.Show("Debe seleccionar un usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtMontoApuesta.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int resultado = _random.Next(1, 4);
            string tipoOperacion;
            IOperacion estrategia;

            if (resultado == 1)
            {
                tipoOperacion = "Gana";
                var tipoCredito = _tipoRepo.GetByCodigo("Credito");
                estrategia = new OperacionCredito();
                lblResultado.Text = "Resultado: ¡Ganaste!";
                lblResultado.ForeColor = Color.Green;
            }
            else if (resultado == 2)
            {
                tipoOperacion = "Pierde";
                var tipoDebito = _tipoRepo.GetByCodigo("Debito");
                estrategia = new OperacionDebito();
                lblResultado.Text = "Resultado: Perdiste";
                lblResultado.ForeColor = Color.Red;
            }
            else
            {
                tipoOperacion = "Vuelve a intentar";
                var tipoCancelacion = _tipoRepo.GetByCodigo("Cancelacion");
                estrategia = new OperacionCancelacion();
                lblResultado.Text = "Resultado: Vuelve a intentar";
                lblResultado.ForeColor = Color.Orange;
            }

            try
            {
                var tipoTransaccion = _tipoRepo.GetByCodigo(tipoOperacion == "Gana" ? "Credito" : tipoOperacion == "Pierde" ? "Debito" : "Cancelacion");
                if (tipoTransaccion == null)
                    throw new Exception("Tipo de transacción no encontrado.");

                var transaccion = new Transaccion
                {
                    CuentaId = _cuentaActual.CuentaId,
                    TipoTransaccionId = tipoTransaccion.TipoTransaccionId,
                    Monto = monto,
                    FechaTransaccion = DateTime.Now,
                    Descripcion = $"Apuesta - {tipoOperacion}",
                    Exitoso = tipoOperacion != "Vuelve a intentar"
                };

                _procesador.SetStrategy(estrategia);
                var resultadoTransaccion = _procesador.ProcessTransaction(transaccion);

                MessageBox.Show(resultadoTransaccion.Mensaje, tipoOperacion, MessageBoxButtons.OK, 
                    resultadoTransaccion.Exitoso ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                _cuentaActual = _cuentaRepo.GetById(_cuentaActual.CuentaId);
                if (_cuentaActual != null)
                {
                    lblSaldoActual.Text = $"Saldo: ${_cuentaActual.Saldo:F2}";
                }
                CargarTransacciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarTransacciones()
        {
            if (_cuentaActual != null)
            {
                var transacciones = _transaccionRepo.GetByCuentaId(_cuentaActual.CuentaId);
                dgvTransacciones.DataSource = transacciones;
            }
        }
    }
}

