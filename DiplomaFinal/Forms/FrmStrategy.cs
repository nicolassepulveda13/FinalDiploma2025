using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.Services;
using SportsbookPatterns.BLL.State;
using SportsbookPatterns.BLL.Strategy;

namespace DiplomaFinal.Forms
{
    public partial class FrmStrategy : Form
    {
        private readonly UsuarioService _usuarioService;
        private readonly CuentaUsuarioService _cuentaService;
        private readonly ApuestaService _apuestaService;
        private CuentaUsuario? _cuentaActual;
        private Random _random = new Random();

        public FrmStrategy(
            UsuarioService usuarioService,
            CuentaUsuarioService cuentaService,
            ApuestaService apuestaService)
        {
            InitializeComponent();
            _usuarioService = usuarioService;
            _cuentaService = cuentaService;
            _apuestaService = apuestaService;
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            var usuarios = _usuarioService.GetAll();
            cmbUsuarios.DataSource = usuarios;
            cmbUsuarios.DisplayMember = "Nombre";
            cmbUsuarios.ValueMember = "UsuarioId";
        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedValue != null)
            {
                int usuarioId = (int)cmbUsuarios.SelectedValue;
                _cuentaActual = _cuentaService.GetCuentaByUsuarioId(usuarioId);
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

            if (resultado == 1)
            {
                tipoOperacion = "Gana";
                lblResultado.Text = "Resultado: ¡Ganaste!";
                lblResultado.ForeColor = Color.Green;
            }
            else if (resultado == 2)
            {
                tipoOperacion = "Pierde";
                lblResultado.Text = "Resultado: Perdiste";
                lblResultado.ForeColor = Color.Red;
            }
            else
            {
                tipoOperacion = "Vuelve a intentar";
                lblResultado.Text = "Resultado: Vuelve a intentar";
                lblResultado.ForeColor = Color.Orange;
            }

            try
            {
                var resultadoTransaccion = _apuestaService.ProcesarApuesta(_cuentaActual, monto, tipoOperacion);

                MessageBox.Show(resultadoTransaccion.Mensaje, tipoOperacion, MessageBoxButtons.OK, 
                    resultadoTransaccion.Exitoso ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                _cuentaActual = _cuentaService.GetCuentaByUsuarioId(_cuentaActual.UsuarioId);
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
                var transacciones = _cuentaService.GetTransaccionesByCuenta(_cuentaActual.CuentaId);
                dgvTransacciones.DataSource = transacciones;
            }
        }
    }
}

