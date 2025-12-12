using Microsoft.Extensions.DependencyInjection;
using SportsbookPatterns.BLL.State;
using SportsbookPatterns.BLL.Strategy;
using SportsbookPatterns.BLL.Visitor;
using SportsbookPatterns.DAL.Abstraccion;

namespace DiplomaFinal.Forms
{
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnState_Click(object sender, EventArgs e)
        {
            if (Program.ServiceProvider == null) return;
            var frmState = Program.ServiceProvider.GetRequiredService<FrmState>();
            frmState.ShowDialog();
        }

        private void btnStrategy_Click(object sender, EventArgs e)
        {
            if (Program.ServiceProvider == null) return;
            var frmStrategy = Program.ServiceProvider.GetRequiredService<FrmStrategy>();
            frmStrategy.ShowDialog();
        }

        private void btnVisitor_Click(object sender, EventArgs e)
        {
            if (Program.ServiceProvider == null) return;
            var frmVisitor = Program.ServiceProvider.GetRequiredService<FrmVisitor>();
            frmVisitor.ShowDialog();
        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}

