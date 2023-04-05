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
            Load_Form(this, EventArgs.Empty);
            UpdateDirectoryPath();
        }

        private void LoadPdfFiles()
        {
            // Check whether the directory path exists
            if (!Directory.Exists(directoryPath))
            {
                // Handle the case where the directory does not exist
                // For example, you could show an error message and exit the method
                MessageBox.Show($"Directory '{directoryPath}' does not exist.");
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

            // Set the default selected username to "Dave" if it exists in the combo box,
            // otherwise set it to the first username in the combo box
            string defaultUsername = ConfigurationManager.AppSettings["DefaultUsername"];
            if (cboUsername.Items.Count > 0)
            {
                if (cboUsername.Items.Cast<Users>().Any(item => item.Username == defaultUsername))
                {
                    cboUsername.SelectedItem = cboUsername.Items.Cast<Users>().First(item => item.Username == defaultUsername);
                }
                else
                {
                    cboUsername.SelectedIndex = 0;
                }
            }
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
                        MessageBox.Show($"Directory '{defaultDirectoryPath}' does not exist.");
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
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Directory path updated for user '{username}'");
                    }
                    else
                    {
                        MessageBox.Show($"Failed to update directory path for user '{username}'");
                    }
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
    }
}
