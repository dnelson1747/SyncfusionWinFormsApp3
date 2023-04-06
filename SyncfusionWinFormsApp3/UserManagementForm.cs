using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncfusionWinFormsApp3
{
    public partial class UserManagementForm : Form
    {
        public string NewUsername { get; private set; }
        public bool SetAsDefault { get; private set; }

        public UserManagementForm()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            NewUsername = txtUsername.Text;
            SetAsDefault = chkDefault.Checked;
            DialogResult = DialogResult.OK;
        }

        private void InitializeComponent()
        {
            lblUsername = new Label();
            txtUsername = new TextBox();
            chkDefault = new CheckBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(66, 54);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(60, 15);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(144, 51);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(187, 23);
            txtUsername.TabIndex = 1;
            // 
            // chkDefault
            // 
            chkDefault.AutoSize = true;
            chkDefault.Location = new Point(144, 80);
            chkDefault.Name = "chkDefault";
            chkDefault.Size = new Size(64, 19);
            chkDefault.TabIndex = 2;
            chkDefault.Text = "Default";
            chkDefault.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(107, 134);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 28);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(201, 134);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 28);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // UserManagementForm
            // 
            ClientSize = new Size(425, 256);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(chkDefault);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Name = "UserManagementForm";
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private Label lblUsername;
        private TextBox txtUsername;
        private CheckBox chkDefault;
        private Button btnSave;
        private Button btnCancel;
    }

}
