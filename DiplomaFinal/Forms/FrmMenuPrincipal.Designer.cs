namespace DiplomaFinal.Forms
{
    partial class FrmMenuPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnState;
        private Button btnStrategy;
        private Button btnVisitor;
        private Label lblTitulo;

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
            this.lblTitulo = new Label();
            this.btnState = new Button();
            this.btnStrategy = new Button();
            this.btnVisitor = new Button();
            this.SuspendLayout();

            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitulo.Location = new Point(100, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(300, 26);
            this.lblTitulo.Text = "Sistema de Apuestas - Patrones";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            this.btnState.Location = new Point(150, 100);
            this.btnState.Name = "btnState";
            this.btnState.Size = new Size(200, 50);
            this.btnState.Text = "Patrón State";
            this.btnState.UseVisualStyleBackColor = true;
            this.btnState.Click += new EventHandler(this.btnState_Click);

            this.btnStrategy.Location = new Point(150, 170);
            this.btnStrategy.Name = "btnStrategy";
            this.btnStrategy.Size = new Size(200, 50);
            this.btnStrategy.Text = "Patrón Strategy";
            this.btnStrategy.UseVisualStyleBackColor = true;
            this.btnStrategy.Click += new EventHandler(this.btnStrategy_Click);

            this.btnVisitor.Location = new Point(150, 240);
            this.btnVisitor.Name = "btnVisitor";
            this.btnVisitor.Size = new Size(200, 50);
            this.btnVisitor.Text = "Patrón Visitor";
            this.btnVisitor.UseVisualStyleBackColor = true;
            this.btnVisitor.Click += new EventHandler(this.btnVisitor_Click);

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 350);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnState);
            this.Controls.Add(this.btnStrategy);
            this.Controls.Add(this.btnVisitor);
            this.Name = "FrmMenuPrincipal";
            this.Text = "Menú Principal";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

