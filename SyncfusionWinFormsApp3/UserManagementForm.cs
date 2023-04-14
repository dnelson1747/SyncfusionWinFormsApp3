using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyncfusionWinFormsApp3.ReceiptApp;

namespace SyncfusionWinFormsApp3
{
    public partial class UserManagementForm : Form
    {
        public event EventHandler UserSaved;
        private string connectionString;
        public string NewUsername { get; private set; }
        public bool SetAsDefault { get; private set; }
        


        public UserManagementForm(string connectionString, List<Users> users, Users selectedUser, Action loadUsernames)

        {
            InitializeComponent();
            this.connectionString = connectionString;
            
            cboUsernames.Items.AddRange(users.ToArray());
            cboUsernames.SelectedItem = selectedUser;

        }

        private void InitializeComponent()
        {
            lblUsername = new Label();
            chkDefault = new CheckBox();
            btnSave = new Button();
            btnDelete = new Button();
            cboUsernames = new ComboBox();
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
            btnSave.Click += btnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(201, 134);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(88, 28);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // cboUsernames
            // 
            cboUsernames.FormattingEnabled = true;
            cboUsernames.Location = new Point(139, 49);
            cboUsernames.Name = "cboUsernames";
            cboUsernames.Size = new Size(207, 23);
            cboUsernames.TabIndex = 5;
            // 
            // UserManagementForm
            // 
            ClientSize = new Size(425, 256);
            Controls.Add(cboUsernames);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(chkDefault);
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
        private CheckBox chkDefault;
        private Button btnSave;
        private ComboBox cboUsernames;
        private Button btnDelete;

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newUsername = cboUsernames.Text;

            if (!string.IsNullOrEmpty(newUsername))
            {
                bool setAsDefault = chkDefault.Checked;
                bool userExists = false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the user exists in the database
                    string query = $"SELECT COUNT(*) FROM Users WHERE Username = '{newUsername}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    int userCount = (int)command.ExecuteScalar();

                    if (userCount == 0)
                    {
                        // Add the new user to the database
                        query = $"INSERT INTO Users (Username) VALUES ('{newUsername}')";
                        command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        userExists = true;
                    }
                }

                if (setAsDefault)
                {
                    // Update the app.config with the new default username
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["DefaultUsername"].Value = newUsername;
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }

                this.DialogResult = DialogResult.OK; // Close the form and return DialogResult.OK to the calling form
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Users selectedUser = cboUsernames.SelectedItem as Users;
            if (selectedUser != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"DELETE FROM Users WHERE UserID = {selectedUser.UserID}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }

                cboUsernames.Items.Remove(selectedUser);

                UserSaved?.Invoke(this, EventArgs.Empty);


                this.DialogResult = DialogResult.OK; // Move this line here
                
            }
        }

    }
}


