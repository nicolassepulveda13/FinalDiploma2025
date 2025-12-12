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
            var serviceProvider = Program.ServiceProvider;
            var frmState = serviceProvider.GetRequiredService<FrmState>();
            frmState.ShowDialog();
        }

        private void btnStrategy_Click(object sender, EventArgs e)
        {
            var serviceProvider = Program.ServiceProvider;
            var frmStrategy = serviceProvider.GetRequiredService<FrmStrategy>();
            frmStrategy.ShowDialog();
        }

        private void btnVisitor_Click(object sender, EventArgs e)
        {
            var serviceProvider = Program.ServiceProvider;
            var frmVisitor = serviceProvider.GetRequiredService<FrmVisitor>();
            frmVisitor.ShowDialog();
        }
    }
}

