using Syncfusion.Pdf.Interactive;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace SyncfusionWinFormsApp3
{
    public partial class ReceiptApp : Form
    {
        private string directoryPath;
        private string outputDirectoryPath;
        private string connectionString = "Data Source=sphq-op-djn\\sqlexpress;Initial Catalog=Apps;Integrated Security=True";
        private string[] pdfFiles;
        private int currentPdfIndex = -1;

        public ReceiptApp()
        {

            InitializeComponent();
            cboEvents.SelectedIndexChanged += CboEvents_SelectedIndexChanged;
            btnPrevious.Click += BtnPrevious_Click;
            btnNext.Click += BtnNext_Click;
            txtLabor.KeyPress += NumericTextBox_KeyPress;
            txtRent.KeyPress += NumericTextBox_KeyPress;
            txtAudio.KeyPress += NumericTextBox_KeyPress;
            txtVideo.KeyPress += NumericTextBox_KeyPress;
            txtAmount.KeyPress += NumericTextBox_KeyPress;

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
            pdfFiles = Directory.GetFiles(directoryPath, "*.pdf");

            foreach (string pdfFile in pdfFiles)
            {
                ListViewItem item = new ListViewItem(Path.GetFileName(pdfFile));
                item.Tag = false; // false means data has not been entered yet
                listView.Items.Add(item);

            }

            // Select the first item in the ListView
            if (listView.Items.Count > 0)
            {
                listView.Items[0].Selected = true;
                listView.Items[0].Focused = true;
                listView.Select();
                currentPdfIndex = 0;
            }
        }


        private void LoadUsernames()
        {
            cboUsername.DrawMode = DrawMode.OwnerDrawFixed;
            cboUsername.SelectedIndexChanged += CboUsername_SelectedIndexChanged;
            cboUsername.Items.Clear();
            int batchID = DisplayBatchNumber();


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
                    // Check if the user is already in the combo box
                    if (!cboUsername.Items.Cast<Users>().Any(item => item.Username == username))
                    {
                        cboUsername.Items.Add(new Users(userID, username));
                    }
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

                // Check if the selected file has data in the ReceiptData table
                Events selectedEvent = cboEvents.SelectedItem as Events;
                int eventID = selectedEvent.EventID;
                if (RecordExists(eventID, selectedFile))
                {
                    // Load the form data for the selected file
                    LoadFormData(eventID, selectedFile);
                }
                currentPdfIndex = Array.IndexOf(pdfFiles, selectedFile);
                lblPdfIndex.Text = $"PDF Index: {currentPdfIndex += 1}";
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
            lblSavedDirectory.Text = outputDirectoryPath;

            // Check if a username was selected
            if (!string.IsNullOrEmpty(cboUsername.Text))
            {
                // Update the user's default directory path in the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string username = cboUsername.Text;
                    string query = $"UPDATE Users SET DefaultDirectoryPath = '{directoryPath}', OutputDirectoryPath = '{outputDirectoryPath}' WHERE Username = '{username}'";

                    SqlCommand command = new SqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();
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
                string query = "SELECT EventID, EventName, Address, City, State, Zip FROM Events";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int eventID = reader.GetInt32(0);
                    string eventName = reader.GetString(1);
                    string address = reader.GetString(2);
                    string city = reader.GetString(3);
                    string state = reader.GetString(4);
                    string zip = reader.GetString(5);
                    cboEvents.Items.Add(new Events { EventID = eventID, EventName = eventName, Address = address, City = city, State = state, Zip = zip });
                }
            }

            // Select the first event in the combo box
            if (cboEvents.Items.Count > 0)
            {
                cboEvents.SelectedIndex = 0;
                Events selectedEvent = cboEvents.SelectedItem as Events;
                lblAddress.Text = selectedEvent.Address;
                lblCity.Text = selectedEvent.City;
                lblState.Text = selectedEvent.State;
                lblZip.Text = selectedEvent.Zip;
            }
        }


        private void btnEditUser_Click(object sender, EventArgs e)
        {
            List<Users> users = cboUsername.Items.Cast<Users>().ToList();
            Users selectedUser = cboUsername.SelectedItem as Users;

            using (UserManagementForm userManagementForm = new UserManagementForm(connectionString, users, selectedUser, LoadUsernames))
            {
                // Subscribe to the UserSaved event
                userManagementForm.UserSaved += (s, ev) =>
                {
                    //MessageBox.Show("UserSaved event received, calling LoadUsernames()...");
                    LoadUsernames();
                };

                if (userManagementForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUsernames();
                    string newUsername = userManagementForm.NewUsername;
                    bool setAsDefault = userManagementForm.SetAsDefault;

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

        private void btnSavedDirectory_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                outputDirectoryPath = folderDialog.SelectedPath;
                UpdateDirectoryPath();
                Load_Form(this, EventArgs.Empty);
            }
        }


        private void CboEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEvents.SelectedIndex >= 0)
            {
                Events selectedEvent = cboEvents.SelectedItem as Events;
                lblAddress.Text = selectedEvent.Address;
                lblCity.Text = selectedEvent.City;
                lblState.Text = selectedEvent.State;
                lblZip.Text = selectedEvent.Zip;
            }
        }
        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count > 0 && listView.SelectedIndices[0] > 0)
            {
                int previousIndex = listView.SelectedIndices[0] - 1;
                listView.Items[listView.SelectedIndices[0]].Selected = false;
                listView.Items[previousIndex].Selected = true;
                listView.Items[previousIndex].Focused = true;
                listView.EnsureVisible(previousIndex);
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count > 0 && listView.SelectedIndices[0] < listView.Items.Count - 1)
            {
                int nextIndex = listView.SelectedIndices[0] + 1;
                listView.Items[listView.SelectedIndices[0]].Selected = false;
                listView.Items[nextIndex].Selected = true;
                listView.Items[nextIndex].Focused = true;
                listView.EnsureVisible(nextIndex);
            }
        }
        private void SaveFormData()
        {
            int eventID = ((Events)cboEvents.SelectedItem).EventID;

            // Get the selected PDF file name
            string selectedPdf = listView.SelectedItems[0].Text;

            bool recordExists = RecordExists(eventID, selectedPdf);

            if (recordExists)
            {
                LoadFormData(eventID, selectedPdf);
            }
            else
            {
                // Read data from your form fields, e.g.:
                DateTime date = dateTimePicker1.Value;
                string vendor = txtVendor.Text;
                string description = txtDescription.Text;
                string category = txtCategory.Text;
                decimal.TryParse(txtLabor.Text, out decimal labor);
                decimal.TryParse(txtRent.Text, out decimal rent);
                decimal.TryParse(txtAudio.Text, out decimal audio);
                decimal.TryParse(txtVideo.Text, out decimal video);
                decimal.TryParse(txtAmount.Text, out decimal amount);

                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string uniqueFilePath = $"C:\\Downloads\\{selectedPdf}_{timeStamp}.pdf";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;

                    if (recordExists)
                    {
                        // UPDATE statement
                        string updateQuery = "UPDATE ReceiptData SET Labor = @Labor, Rent = @Rent, Audio = @Audio, Video = @Video, Amount = @Amount, DataSaved = 1 WHERE EventID = @EventID AND PdfFileName = @PdfFileName";
                        command = new SqlCommand(updateQuery, connection);
                    }
                    else
                    {
                        // INSERT statement
                        string insertQuery = "INSERT INTO ReceiptData (EventID, Date, Vendor, Description, Category, Labor, Rent, Audio, Video, Amount, PdfFileName, DataSaved) VALUES (@EventID, @Date, @Vendor, @Description, @Category, @Labor, @Rent, @Audio, @Video, @Amount, @PdfFileName, 1)";
                        command = new SqlCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                    }

                    // Common parameters
                    command.Parameters.AddWithValue("@EventID", eventID);
                    command.Parameters.AddWithValue("@Vendor", txtVendor.Text);
                    command.Parameters.AddWithValue("@Description", txtDescription.Text);
                    command.Parameters.AddWithValue("@Category", txtCategory.Text);
                    command.Parameters.AddWithValue("@Labor", labor);
                    command.Parameters.AddWithValue("@Rent", rent);
                    command.Parameters.AddWithValue("@Audio", audio);
                    command.Parameters.AddWithValue("@Video", video);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@PdfFileName", selectedPdf);
                    command.Parameters.AddWithValue("@FilePath", uniqueFilePath);

                    command.ExecuteNonQuery();
                }

                int selectedIndex = listView.SelectedIndices[0];

                listView.Items[selectedIndex].ForeColor = Color.Green;
                listView.Items[selectedIndex].Tag = true;

                ClearFormFields();
            }
        }


        private void ClearFormFields()
        {
            dateTimePicker1.Value = DateTime.Now;
            txtVendor.Clear();
            txtDescription.Clear();
            txtCategory.Clear();
            txtLabor.Clear();
            txtRent.Clear();
            txtAudio.Clear();
            txtVideo.Clear();
            txtAmount.Clear();
        }

        private void btnSaveNext_Click(object sender, EventArgs e)
        {
            // Save the form data
            SaveFormData();

            // Go to the next PDF
            BtnNext_Click(sender, e);

            // Clear the form fields
            ClearFormFields();
        }
        private void NumericTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)

        {
            // Allow only digits, a single decimal point, and the backspace key
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }
        private void LoadFormData(int eventID, string selectedPdf)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string checkQuery = "SELECT Date, Vendor, Description, Category, Labor, Rent, Audio, Video, Amount FROM ReceiptData WHERE EventID = @EventID AND PdfFileName = @PdfFileName AND DataSaved = 1";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@EventID", eventID);
                checkCommand.Parameters.AddWithValue("@PdfFileName", selectedPdf);

                using (SqlDataReader reader = checkCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dateTimePicker1.Value = reader.GetDateTime(0);
                            txtVendor.Text = reader.GetString(1);
                            txtDescription.Text = reader.GetString(2);
                            txtCategory.Text = reader.GetString(3);
                            txtLabor.Text = reader.GetDecimal(4).ToString();
                            txtRent.Text = reader.GetDecimal(5).ToString();
                            txtAudio.Text = reader.GetDecimal(6).ToString();
                            txtVideo.Text = reader.GetDecimal(7).ToString();
                            txtAmount.Text = reader.GetDecimal(8).ToString();
                        }
                    }
                }
            }
        }
        private bool RecordExists(int eventID, string selectedPdf)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string checkQuery = "SELECT COUNT(*) FROM ReceiptData WHERE EventID = @EventID AND PdfFileName = @PdfFileName AND DataSaved = 1";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@EventID", eventID);
                checkCommand.Parameters.AddWithValue("@PdfFileName", selectedPdf);

                int count = (int)checkCommand.ExecuteScalar();
                return count > 0;
            }
        }
        private int DisplayBatchNumber()
        {
            int lastBatchNumber = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MAX(BatchNumber) FROM ReceiptBatch";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    lastBatchNumber = reader.GetInt32(0);
                    string batchTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
                    lblBatchTime.Text = "Batch Time: " + batchTime;
                }
                else
                {
                    lblBatchTime.Text = "Batch Time: N/A";
                }
            }

            lblBatchID.Text = "Batch #: " + (lastBatchNumber + 1);

            return lastBatchNumber + 1;
        }


    }
}
