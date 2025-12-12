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
            lblTitulo = new Label();
            btnState = new Button();
            btnStrategy = new Button();
            btnVisitor = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(100, 30);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(355, 26);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Sistema de Apuestas - Patrones";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnState
            // 
            btnState.Location = new Point(150, 100);
            btnState.Name = "btnState";
            btnState.Size = new Size(200, 50);
            btnState.TabIndex = 1;
            btnState.Text = "Patrón State";
            btnState.UseVisualStyleBackColor = true;
            btnState.Click += btnState_Click;
            // 
            // btnStrategy
            // 
            btnStrategy.Location = new Point(150, 170);
            btnStrategy.Name = "btnStrategy";
            btnStrategy.Size = new Size(200, 50);
            btnStrategy.TabIndex = 2;
            btnStrategy.Text = "Patrón Strategy";
            btnStrategy.UseVisualStyleBackColor = true;
            btnStrategy.Click += btnStrategy_Click;
            // 
            // btnVisitor
            // 
            btnVisitor.Location = new Point(150, 240);
            btnVisitor.Name = "btnVisitor";
            btnVisitor.Size = new Size(200, 50);
            btnVisitor.TabIndex = 3;
            btnVisitor.Text = "Patrón Visitor";
            btnVisitor.UseVisualStyleBackColor = true;
            btnVisitor.Click += btnVisitor_Click;
            // 
            // FrmMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 350);
            Controls.Add(lblTitulo);
            Controls.Add(btnState);
            Controls.Add(btnStrategy);
            Controls.Add(btnVisitor);
            Name = "FrmMenuPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menú Principal";
            Load += FrmMenuPrincipal_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

