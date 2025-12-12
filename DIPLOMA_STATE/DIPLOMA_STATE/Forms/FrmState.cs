using SportsbookPatterns.BE;
using SportsbookPatterns.BLL.Services;
using SportsbookPatterns.BLL.State;

namespace DIPLOMA_STATE.Forms
{
    public partial class FrmState : Form
    {
        private readonly UsuarioService _usuarioService;
        private readonly CuentaUsuarioService _cuentaService;
        private CuentaContext? _context;

        public FrmState(
            UsuarioService usuarioService,
            CuentaUsuarioService cuentaService)
        {
            InitializeComponent();
            _usuarioService = usuarioService;
            _cuentaService = cuentaService;
            
            btnApostar.Enabled = false;
            btnRetirar.Enabled = false;
            btnDepositar.Enabled = false;
            
            CargarUsuarios();
            CargarEstados();
            LimpiarVista();
        }

        private void CargarUsuarios()
        {
            var usuarios = _usuarioService.GetAll();
            cmbUsuarios.DataSource = null;
            cmbUsuarios.Items.Clear();
            
            if (usuarios != null && usuarios.Count > 0)
            {
                cmbUsuarios.DataSource = usuarios;
                cmbUsuarios.DisplayMember = "NombreCompleto";
                cmbUsuarios.ValueMember = "UsuarioId";
                cmbUsuarios.SelectedIndex = -1;
            }
        }

        private void CargarEstados()
        {
            var estados = _cuentaService.GetAllEstados();
            cmbEstados.DataSource = null;
            cmbEstados.Items.Clear();
            
            if (estados != null && estados.Count > 0)
            {
                cmbEstados.DataSource = estados;
                cmbEstados.DisplayMember = "CodigoEstado";
                cmbEstados.ValueMember = "EstadoCuentaId";
                cmbEstados.SelectedIndex = -1;
            }
        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedIndex < 0)
            {
                _context = null;
                LimpiarVista();
                return;
            }

            try
            {
                int usuarioId;
                
                if (cmbUsuarios.SelectedItem is Usuario usuario)
                {
                    usuarioId = usuario.UsuarioId;
                }
                else if (cmbUsuarios.SelectedValue is int id)
                {
                    usuarioId = id;
                }
                else if (cmbUsuarios.SelectedValue != null)
                {
                    usuarioId = Convert.ToInt32(cmbUsuarios.SelectedValue);
                }
                else
                {
                    LimpiarVista();
                    return;
                }

                var cuenta = _cuentaService.GetCuentaByUsuarioId(usuarioId);
                
                if (cuenta != null)
                {
                    _context = _cuentaService.CrearContext(cuenta);
                    ActualizarVista();
                }
                else
                {
                    MessageBox.Show("El usuario seleccionado no tiene cuenta asociada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarVista();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la cuenta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarVista();
            }
        }

        private void cmbEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_context == null || cmbEstados.SelectedIndex < 0)
                return;

            try
            {
                int estadoId;
                
                if (cmbEstados.SelectedItem is EstadoCuenta estadoItem)
                {
                    estadoId = estadoItem.EstadoCuentaId;
                }
                else if (cmbEstados.SelectedValue is int id)
                {
                    estadoId = id;
                }
                else if (cmbEstados.SelectedValue != null)
                {
                    estadoId = Convert.ToInt32(cmbEstados.SelectedValue);
                }
                else
                {
                    return;
                }

                var estado = _cuentaService.GetAllEstados().FirstOrDefault(e => e.EstadoCuentaId == estadoId);
                if (estado != null && _context != null)
                {
                    _context.CambiarEstado(estado.CodigoEstado);
                    ActualizarVista();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar el estado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarVista()
        {
            if (_context != null)
            {
                var cuenta = _cuentaService.GetCuentaByUsuarioId(_context.Cuenta.UsuarioId);
                if (cuenta == null)
                {
                    LimpiarVista();
                    return;
                }

                _context = _cuentaService.CrearContext(cuenta);
                
                lblSaldoActual.Text = $"Saldo: ${_context.Cuenta.Saldo:F2}";
                
                string nombreEstado = _context.EstadoActual.GetNombreEstado();
                bool puedeApostar = nombreEstado == "Activa";
                bool puedeRetirar = nombreEstado == "Activa";
                bool puedeDepositar = nombreEstado != "Bloqueada";

                btnApostar.Enabled = puedeApostar;
                btnRetirar.Enabled = puedeRetirar;
                btnDepositar.Enabled = puedeDepositar;

                var estadoBD = _cuentaService.GetAllEstados().FirstOrDefault(e => e.EstadoCuentaId == _context.Cuenta.EstadoCuentaId);
                if (estadoBD != null)
                {
                    cmbEstados.SelectedIndexChanged -= cmbEstados_SelectedIndexChanged;
                    try
                    {
                        cmbEstados.SelectedValue = estadoBD.EstadoCuentaId;
                    }
                    finally
                    {
                        cmbEstados.SelectedIndexChanged += cmbEstados_SelectedIndexChanged;
                    }
                }

                CargarHistorial();
            }
        }

        private void LimpiarVista()
        {
            lblSaldoActual.Text = "Saldo: $0.00";
            btnApostar.Enabled = false;
            btnRetirar.Enabled = false;
            btnDepositar.Enabled = false;
            cmbEstados.SelectedIndex = -1;
            dgvHistorial.DataSource = null;
            txtMonto.Clear();
        }

        private void btnApostar_Click(object sender, EventArgs e)
        {
            if (_context == null)
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
                _context.Apostar(monto);
                MessageBox.Show("Apuesta realizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarVista();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            if (_context == null)
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
                _context.Retirar(monto);
                MessageBox.Show("Retiro realizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarVista();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDepositar_Click(object sender, EventArgs e)
        {
            if (_context == null)
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
                _context.Depositar(monto);
                MessageBox.Show("Depósito realizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarVista();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHistorial()
        {
            if (_context != null)
            {
                var transacciones = _cuentaService.GetTransaccionesByCuenta(_context.Cuenta.CuentaId);
                dgvHistorial.DataSource = transacciones;
            }
        }
    }
}

