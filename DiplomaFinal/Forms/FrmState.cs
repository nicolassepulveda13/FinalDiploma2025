using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.Services;
using SportsbookPatterns.BLL.State;

namespace DiplomaFinal.Forms
{
    public partial class FrmState : Form
    {
        private readonly UsuarioService _usuarioService;
        private readonly CuentaUsuarioService _cuentaService;
        private CuentaUsuario? _cuentaActual;
        private IEstadoCuenta? _estadoActual;

        public FrmState(
            UsuarioService usuarioService,
            CuentaUsuarioService cuentaService)
        {
            InitializeComponent();
            _usuarioService = usuarioService;
            _cuentaService = cuentaService;
            CargarUsuarios();
            CargarEstados();
        }

        private void CargarUsuarios()
        {
            var usuarios = _usuarioService.GetAll();
            cmbUsuarios.DataSource = usuarios;
            cmbUsuarios.DisplayMember = "Nombre";
            cmbUsuarios.ValueMember = "UsuarioId";
        }

        private void CargarEstados()
        {
            var estados = _cuentaService.GetAllEstados();
            cmbEstados.DataSource = estados;
            cmbEstados.DisplayMember = "CodigoEstado";
            cmbEstados.ValueMember = "EstadoCuentaId";
        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedValue != null)
            {
                int usuarioId = (int)cmbUsuarios.SelectedValue;
                _cuentaActual = _cuentaService.GetCuentaByUsuarioId(usuarioId);
                if (_cuentaActual != null)
                {
                    ActualizarVista();
                }
            }
        }

        private void cmbEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cuentaActual != null && cmbEstados.SelectedValue != null)
            {
                var estado = _cuentaService.GetAllEstados().FirstOrDefault(e => e.EstadoCuentaId == (int)cmbEstados.SelectedValue);
                if (estado != null)
                {
                    _cuentaService.CambiarEstado(_cuentaActual, estado.CodigoEstado);
                    ActualizarVista();
                }
            }
        }

        private void ActualizarVista()
        {
            if (_cuentaActual != null)
            {
                lblSaldoActual.Text = $"Saldo: ${_cuentaActual.Saldo:F2}";
                _estadoActual = _cuentaService.ObtenerEstado(_cuentaActual);
                
                bool puedeApostar = _estadoActual.GetNombreEstado() == "ActivaConFondos";
                bool puedeRetirar = _estadoActual.GetNombreEstado() == "ActivaConFondos";
                bool puedeDepositar = _estadoActual.GetNombreEstado() != "Bloqueada";

                btnApostar.Enabled = puedeApostar;
                btnRetirar.Enabled = puedeRetirar;
                btnDepositar.Enabled = puedeDepositar;
            }
        }

        private void btnApostar_Click(object sender, EventArgs e)
        {
            if (_cuentaActual == null || _estadoActual == null)
            {
                MessageBox.Show("Debe seleccionar un usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _cuentaService.Apostar(_cuentaActual, monto);
                MessageBox.Show("Apuesta realizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cuentaActual = _cuentaService.GetCuentaByUsuarioId(_cuentaActual.UsuarioId);
                ActualizarVista();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            if (_cuentaActual == null || _estadoActual == null)
            {
                MessageBox.Show("Debe seleccionar un usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _cuentaService.Retirar(_cuentaActual, monto);
                MessageBox.Show("Retiro realizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cuentaActual = _cuentaService.GetCuentaByUsuarioId(_cuentaActual.UsuarioId);
                ActualizarVista();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDepositar_Click(object sender, EventArgs e)
        {
            if (_cuentaActual == null || _estadoActual == null)
            {
                MessageBox.Show("Debe seleccionar un usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _cuentaService.Depositar(_cuentaActual, monto);
                MessageBox.Show("Depósito realizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cuentaActual = _cuentaService.GetCuentaByUsuarioId(_cuentaActual.UsuarioId);
                ActualizarVista();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHistorial()
        {
            if (_cuentaActual != null)
            {
                var transacciones = _cuentaService.GetTransaccionesByCuenta(_cuentaActual.CuentaId);
                dgvHistorial.DataSource = transacciones;
            }
        }
    }
}

