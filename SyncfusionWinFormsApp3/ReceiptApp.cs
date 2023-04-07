using Syncfusion.Windows.Forms.PdfViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncfusionWinFormsApp3;


namespace SyncfusionWinFormsApp3
{
    public partial class ReceiptApp : Form
    {
        private string directoryPath;
        private string outputDirectoryPath;
        private string connectionString = "Data Source=sphq-op-djn\\sqlexpress;Initial Catalog=Apps;Integrated Security=True";


        public ReceiptApp()
        {

            InitializeComponent();

            // Set the ListView control to Details view mode
            listView.View = View.Details;

            // Add a column to the ListView control to display the file names
            listView.Columns.Add("File Name", 300);

            LoadUsernames();
            LoadPdfFiles();
            LoadEvents();
            Load_Form(this, EventArgs.Empty);
            UpdateDirectoryPath();

            cboUsername.DrawItem += CboUsername_DrawItem;
            this.Controls.Add(cboUsername);
        }


        private void LoadPdfFiles()
        {
            // Check whether the directory path exists
            if (!Directory.Exists(directoryPath))
            {

                Console.WriteLine($"Directory '{directoryPath}' does not exist.");
                return;
            }

            // Use the directory path to load PDF files from the directory
            string[] pdfFiles = Directory.GetFiles(directoryPath, "*.pdf");

            foreach (string pdfFile in pdfFiles)
            {
                // Add the file name to the ListView control
                ListViewItem item = new ListViewItem(Path.GetFileName(pdfFile));

                // Add the item to the ListView control
                listView.Items.Add(item);
            }
        }

        private void LoadUsernames()
        {
            cboUsername.DrawMode = DrawMode.OwnerDrawFixed;
            cboUsername.SelectedIndexChanged += CboUsername_SelectedIndexChanged;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UserID, Username FROM Users";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int userID = reader.GetInt32(0);
                    string username = reader.GetString(1);
                    cboUsername.Items.Add(new Users(userID, username));
                }
            }

            // otherwise set it to the first username in the combo box
            string defaultUsername = ConfigurationManager.AppSettings["DefaultUsername"];
            if (cboUsername.Items.Count > 0)
            {
                if (cboUsername.Items.Cast<Users>().Any(item => item.Username == defaultUsername))
                {
                    cboUsername.SelectedItem = cboUsername.Items.Cast<Users>().First(item => item.Username == defaultUsername);
                    lblUser.Text = "Default User";
                    lblUser.ForeColor = Color.Green;
                }
                else
                {
                    cboUsername.SelectedIndex = 0;
                }
            }
            cboUsername.DrawItem += CboUsername_DrawItem;
        }


        private void Load_Form(object sender, EventArgs e)
        {

            // Load the PDF files from the directory path for the selected username
            string selectedUsername = cboUsername.SelectedItem?.ToString();


            if (!string.IsNullOrEmpty(selectedUsername))
            {
                // Retrieve the default directory path for the selected username from the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT DefaultDirectoryPath FROM Users WHERE Username = '{selectedUsername}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    string defaultDirectoryPath = command.ExecuteScalar()?.ToString() ?? "unknown";

                    if (Directory.Exists(defaultDirectoryPath))
                    {
                        directoryPath = defaultDirectoryPath;
                        LoadPdfFiles();
                    }
                    else
                    {
                        Console.WriteLine($"Directory '{defaultDirectoryPath}' does not exist.");
                    }
                }

                if (string.IsNullOrEmpty(directoryPath))
                {
                    // Handle the case where the directory path is null or empty                
                    Console.WriteLine("Directory path is null or empty.");
                    return;
                }

                // Check whether the directory path exists
                if (!Directory.Exists(directoryPath))
                {
                    // Handle the case where the directory does not exist                
                    Console.WriteLine($"Directory '{directoryPath}' does not exist.");
                    return;
                }

                // Load the PDF files from the directory
                string[] pdfFiles = Directory.GetFiles(directoryPath, "*.pdf");

                if (pdfFiles.Length > 0)
                {
                    string firstPdfFile = pdfFiles[0];
                    string fullPath = Path.Combine(directoryPath, firstPdfFile);
                    pdfViewerControl1.Load(fullPath);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                string selectedFile = Path.Combine(directoryPath, listView.SelectedItems[0].Text);
                pdfViewerControl1.Load(selectedFile);
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                listView1_SelectedIndexChanged(sender, e);
            }
        }

        private void UpdateDirectoryPath()
        {
            lblDirectoryPath.Text = directoryPath;

            // Check if a username was selected
            if (!string.IsNullOrEmpty(cboUsername.Text))
            {
                // Update the user's default directory path in the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string username = cboUsername.Text;
                    string query = $"UPDATE Users SET DefaultDirectoryPath = '{directoryPath}' WHERE Username = '{username}'";

                    SqlCommand command = new SqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();
                    //if (rowsAffected > 0)
                    //{
                    //    Console.WriteLine($"Directory path updated for user '{username}'");
                    //}
                    //else
                    //{
                    //    Console.WriteLine($"Failed to update directory path for user '{username}'");
                    //}
                }
            }
        }

        private void btnDirectoryPath_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                directoryPath = folderDialog.SelectedPath;
                listView.Items.Clear();
                LoadPdfFiles();
                UpdateDirectoryPath();
                Load_Form(this, EventArgs.Empty);
            }
        }

        public void LoadEvents()
        {

            // Load the events from the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT EventID, EventName FROM Events";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int eventID = reader.GetInt32(0);
                    string eventName = reader.GetString(1);
                    cboEvents.Items.Add(new Events { EventID = eventID, EventName = eventName });
                }
            }

            // Select the first event in the combo box
            if (cboEvents.Items.Count > 0)
            {
                cboEvents.SelectedIndex = 0;
            }
        }


        private void btnEditUser_Click(object sender, EventArgs e)
        {
            using (UserManagementForm userManagementForm = new UserManagementForm())
            {
                if (userManagementForm.ShowDialog() == DialogResult.OK)
                {
                    string newUsername = userManagementForm.NewUsername;
                    bool setAsDefault = userManagementForm.SetAsDefault;

                    // Check if the user exists in the database
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"SELECT COUNT(*) FROM Users WHERE Username = '{newUsername}'";
                        SqlCommand command = new SqlCommand(query, connection);
                        int userCount = (int)command.ExecuteScalar();

                        if (userCount == 0)
                        {
                            // Add the new user to the database
                            query = $"INSERT INTO Users (Username) VALUES ('{newUsername}')";
                            command = new SqlCommand(query, connection);
                            command.ExecuteNonQuery();

                            // Add the new user to the combo box
                            int userID = GetUserId(newUsername);
                            cboUsername.Items.Add(new Users(userID, newUsername));
                        }
                    }

                    if (setAsDefault)
                    {
                        // Update the app.config with the new default username
                        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        config.AppSettings.Settings["DefaultUsername"].Value = newUsername;
                        config.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");

                        // Update the combo box selection
                        cboUsername.SelectedItem = cboUsername.Items.Cast<Users>().First(item => item.Username == newUsername);
                    }
                }
            }
        }

        private int GetUserId(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT UserID FROM Users WHERE Username = '{username}'";
                SqlCommand command = new SqlCommand(query, connection);
                int userId = (int)command.ExecuteScalar();
                return userId;
            }
        }
        private void CboUsername_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            e.DrawBackground();

            Users user = (Users)cboUsername.Items[e.Index];
            string defaultUsername = ConfigurationManager.AppSettings["DefaultUsername"];
            Brush brush = (user.Username == defaultUsername) ? Brushes.Aqua : Brushes.Black;

            e.Graphics.DrawString(user.ToString(), e.Font, brush, e.Bounds);

            e.DrawFocusRectangle();
        }
        private void CboUsername_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUsername = cboUsername.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedUsername)) return;

            string defaultUsername = ConfigurationManager.AppSettings["DefaultUsername"];
            if (selectedUsername == defaultUsername)
            {
                lblUser.Text = "Default User";
                lblUser.ForeColor = Color.Green;
            }
            else
            {
                lblUser.Text = "User";
                lblUser.ForeColor = Color.Black;
            }

            // Retrieve the directoryPath and outputDirectoryPath for the selected user from the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT DefaultDirectoryPath, OutputDirectoryPath FROM Users WHERE Username = '{selectedUsername}'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    directoryPath = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                    outputDirectoryPath = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                }
            }

            // Update the labels with the new directory paths
            lblDirectoryPath.Text = directoryPath;
            lblSavedDirectory.Text = outputDirectoryPath;

            // Load the PDF files for the newly selected user
            listView.Items.Clear();
            if (!string.IsNullOrEmpty(directoryPath))
            {
                LoadPdfFiles();
            }
        }


    }
}
