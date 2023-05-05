using Syncfusion.Pdf.Interactive;
using Syncfusion.Styles;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SyncfusionWinFormsApp3
{
    public partial class ReceiptApp : Form
    {
        private string directoryPath;
        private string outputDirectoryPath;
        private string connectionString = "Data Source=sphq-op-djn\\sqlexpress;Initial Catalog=Apps;Integrated Security=True";
        private string[] pdfFiles;
        private decimal currentPdfIndex = 0;
        private decimal pdfIncrement = 0.001m;
        private int currentBatchNumber;
        private string DBfilePath = @"C:\Downloads\";
        private string userID;
        
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
            currentBatchNumber = DisplayBatchNumber();


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
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).TextChanged += textBox_TextChanged;
                    ((TextBox)control).Enter += textBox_Enter;
                    ((TextBox)control).Leave += textBox_Leave;
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).Enter += comboBox_Enter;
                    ((ComboBox)control).Leave += comboBox_Leave;
                }
                else if (control is DateTimePicker)
                {
                    ((DateTimePicker)control).Enter += dateTimePicker_Enter;
                    ((DateTimePicker)control).Leave += dateTimePicker_Leave;
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView.SelectedItems.Count > 0)
            {
                string selectedFile = Path.Combine(directoryPath, listView.SelectedItems[0].Text);
                pdfViewerControl1.Load(selectedFile);

                // Set currentPdfIndex to the selected item's index
                currentPdfIndex = listView.SelectedIndices[0];

                // Check if the selected file has data in the ReceiptData table
                Events selectedEvent = cboEvents.SelectedItem as Events;
                int eventID = selectedEvent.EventID;
                string selectedPdf = listView.SelectedItems[0].Text;

                // Get batch ID from the batch ID label
                int? batchID = null;
                var batchParts = lblBatchID.Text.Split(' ');
                if (batchParts.Length >= 3 && int.TryParse(batchParts[2], out int parsedBatchID))
                {
                    batchID = parsedBatchID;
                }

                if (RecordExists(eventID, selectedPdf, currentBatchNumber, batchID))
                {
                    LoadFormData(eventID, selectedPdf, currentBatchNumber, batchID);
                }
                else
                {
                    ClearFormFields();
                }
                
                lblPdfIndex.Text = $"PDF Index: {currentPdfIndex + pdfIncrement}";              

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
                string query = $"SELECT DefaultDirectoryPath, OutputDirectoryPath, UserID FROM Users WHERE Username = '{selectedUsername}'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    directoryPath = !reader.IsDBNull(0) ? reader.GetString(0) : null;
                    outputDirectoryPath = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    userID = reader.GetInt32(2).ToString();
                }
            }

            // Update the labels with the new directory paths
            lblDirectoryPath.Text = directoryPath;
            lblSavedDirectory.Text = outputDirectoryPath;
            lblUserID.Text = userID;


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

                // Turn all labels black except lblDate
                foreach (Control control in this.Controls)
                {
                    if (control is Label && control.Name != "lblDate")
                    {
                        control.ForeColor = Color.Black;
                    }
                }
                ClearFormFields();
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

                // Turn all labels black except lblDate
                foreach (Control control in this.Controls)
                {
                    if (control is Label && control.Name != "lblDate")
                    {
                        control.ForeColor = Color.Black;                        
                    }
                }
                ClearFormFields();
            }
        }
        private void SaveFormData(int batchID)
        {
            // Disable the listView1.SelectedIndexChanged event
            listView.SelectedIndexChanged -= listView1_SelectedIndexChanged;

            if (AreFormFieldsEmpty())
            {
                return; // Exit the method if the form fields are empty
            }

            int eventID = ((Events)cboEvents.SelectedItem).EventID;

            string pdfFileName = Path.GetFileNameWithoutExtension(listView.SelectedItems[0].Text);

            pdfFileName = pdfFileName.Replace(" ", ""); // remove spaces

            string filePath = Path.Combine(DBfilePath, $"{pdfFileName}-{batchID}.pdf");

            int userID = int.Parse(lblUserID.Text);

            string selectedPdf = listView.SelectedItems[0].Text;

            decimal pdfIndex = decimal.Parse(lblPdfIndex.Text.Split(' ')[2]);

            bool recordExists = RecordExists(eventID, selectedPdf, currentBatchNumber, batchID);

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
            
            int dataSaved = 0; 

            if (amount > 0)
            {
                dataSaved = 2; // Data saved with amount > 0
            }

            else
            {
                dataSaved = 1; // Data saved no amount
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command;

                if (recordExists)
                {
                    // UPDATE statement
                    string updateQuery = "UPDATE ReceiptData SET Date = @Date, Vendor = @Vendor, Description = @Description, Category = @Category, Labor = @Labor, Rent = @Rent, Audio = @Audio, Video = @Video, Amount = @Amount, FilePath = @FilePath, DataSaved = @DataSaved WHERE EventID = @EventID AND PdfFileName = @PdfFileName AND BatchID = @BatchID";
                    command = new SqlCommand(updateQuery, connection);
                }
                else
                {
                    // INSERT statement
                    string insertQuery = "INSERT INTO ReceiptData (EventID, Date, Vendor, Description, Category, Labor, Rent, Audio, Video, Amount, PdfFileName, DataSaved, BatchID, PdfIndex, FilePath, UserID) VALUES (@EventID, @Date, @Vendor, @Description, @Category, @Labor, @Rent, @Audio, @Video, @Amount, @PdfFileName, @DataSaved, @BatchID, @PdfIndex, @FilePath, @UserID)";
                    command = new SqlCommand(insertQuery, connection);
                }

                // Common parameters
                command.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
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
                command.Parameters.AddWithValue("@BatchID", batchID);
                command.Parameters.AddWithValue("@PdfIndex", pdfIndex);
                command.Parameters.AddWithValue("@FilePath", filePath);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@DataSaved", dataSaved);

                //Console.WriteLine("batchID: " + batchID); // Add this line to print the batch ID value
                command.ExecuteNonQuery();
            }


            // Change the font color of the ListView item
            int selectedIndex = listView.SelectedIndices[0];
            UpdateListViewItemColor(listView.Items[selectedIndex], dataSaved);

            // Disable the listView1.SelectedIndexChanged event to prevent re-triggering
            listView.SelectedIndexChanged -= listView1_SelectedIndexChanged;

            // Change the tag of the ListView item to true to indicate that data has been saved
            listView.Items[selectedIndex].Tag = true;

            // Enable the listView1.SelectedIndexChanged event
            listView.SelectedIndexChanged += listView1_SelectedIndexChanged;
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
            // Turn all labels black except lblDate
            foreach (Control control in this.Controls)
            {
                if (control is Label && control.Name != "lblDate")
                {
                    control.ForeColor = Color.Black;
                }
            }
        }

        private void btnSaveNext_Click(object sender, EventArgs e)
        {
            // Retrieve the batch ID
            int? batchID = GetBatchID();

            // Display an error message if the batch ID is null
            if (!batchID.HasValue)
            {
                MessageBox.Show("Error: Unable to retrieve batch ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save the form data
            SaveFormData(batchID.Value);

            // Go to the next PDF
            BtnNext_Click(sender, e);

            // Clear the form fields
            ClearFormFields();

            // Turn all labels black except lblDate
            foreach (Control control in this.Controls)
            {
                if (control is Label && control.Name != "lblDate")
                {
                    control.ForeColor = Color.Black;
                }
            }
            
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
        private void LoadFormData(int eventID, string selectedPdf, int currentBatchNumber, int? batchID = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string checkQuery = "SELECT Date, Vendor, Description, Category, Labor, Rent, Audio, Video, Amount " +
                    "FROM ReceiptData rd " +
                    "JOIN ReceiptBatch rb ON rd.BatchID = rb.BatchID " +
                    "WHERE EventID = @EventID AND BatchNumber = @currentBatchNumber AND DataSaved >= 1 and PdfFileName = @PdfFileName";

                if (batchID.HasValue)
                {
                    checkQuery += " AND rd.BatchID = @BatchID";
                }


                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@EventID", eventID);
                checkCommand.Parameters.AddWithValue("@currentBatchNumber", currentBatchNumber);
                checkCommand.Parameters.AddWithValue("@PdfFileName", selectedPdf);

                if (batchID.HasValue)
                {
                    checkCommand.Parameters.AddWithValue("@BatchID", batchID.Value);
                }

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

        private bool RecordExists(int eventID, string selectedPdf, int currentBatchNumber, int? batchID = null)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) from ReceiptData rd join ReceiptBatch rb on rd.BatchID = rb.BatchID WHERE EventID = @EventID AND DataSaved >= 1 and BatchNumber = @currentBatchNumber and PdfFileName = @PdfFileName";

                if (batchID.HasValue)
                {
                    query += " AND rb.BatchID = @BatchID";
                }

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EventID", eventID);
                //command.Parameters.AddWithValue("@PdfFileName", fileName);
                command.Parameters.AddWithValue("@PdfFileName", selectedPdf);
                command.Parameters.AddWithValue("@currentBatchNumber", currentBatchNumber);


                if (batchID.HasValue)
                {
                    command.Parameters.AddWithValue("@BatchID", batchID.Value);
                }

                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    exists = true;
                }
                Console.WriteLine($"Loaded data for event {eventID}, filename {selectedPdf}, batch {batchID}, batchNumber {currentBatchNumber} ");

            }

            return exists;
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

            lblBatchNumber.Text = "Batch #: " + (lastBatchNumber + 1);

            return lastBatchNumber + 1;
        }

        private int GetBatchID()
        {
            int newBatchID = 0;
            

            // Check if batchNumber or batchTime is null or empty
            if (string.IsNullOrEmpty(lblBatchNumber.Text) || string.IsNullOrEmpty(lblBatchTime.Text))
            {
                return newBatchID;
            }

            string batchNumberStr = lblBatchNumber.Text.Split(' ')[2];
            int batchNumber = int.Parse(batchNumberStr.Split('.')[0]);            
            string batchTime = lblBatchTime.Text;

            // Remove the "Batch Time: " prefix from batchTime string
            batchTime = batchTime.Replace("Batch Time: ", "");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the batch exists in the database
                string selectQuery = "SELECT BatchID FROM ReceiptBatch WHERE BatchNumber = @BatchNumber AND Time = @BatchTime";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@BatchNumber", batchNumber);
                selectCommand.Parameters.AddWithValue("@BatchTime", batchTime);
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        newBatchID = reader.GetInt32(reader.GetOrdinal("BatchID"));
                        
                    }
                }

                if (newBatchID == 0)
                {
                    // Batch does not exist, insert it into the database and return the new BatchID
                    string insertQuery = "INSERT INTO ReceiptBatch (BatchNumber, Time) VALUES (@BatchNumber, @BatchTime); SELECT CAST(scope_identity() AS int)";

                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@BatchNumber", batchNumber);
                    insertCommand.Parameters.AddWithValue("@BatchTime", batchTime);                    

                    object insertResult = insertCommand.ExecuteScalar();
                    if (insertResult != null && insertResult != DBNull.Value)
                    {
                        newBatchID = Convert.ToInt32(insertResult);

                        // Update the Time column with the time value
                        string updateTimeQuery = "UPDATE ReceiptBatch SET Time = @Time WHERE BatchID = @BatchID";
                        SqlCommand updateTimeCommand = new SqlCommand(updateTimeQuery, connection);
                        updateTimeCommand.Parameters.AddWithValue("@BatchID", newBatchID);
                        updateTimeCommand.Parameters.AddWithValue("@Time", batchTime);                        
                        updateTimeCommand.ExecuteNonQuery();

                        // Store the new BatchID in the lblBatchID label
                        lblBatchID.Text = $"Batch ID: {newBatchID}";
                    }
                }
            }

            return newBatchID;
        }



        private void btnSaveRepeat_Click(object sender, EventArgs e)
        {
            // Retrieve the batch ID
            int? batchID = GetBatchID();

            // Display an error message if the batch ID is null
            if (!batchID.HasValue)
            {
                MessageBox.Show("Error: Unable to retrieve batch ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save the form data
            SaveFormData(batchID.Value);

            // Populate the next form fields with values from existing form fields
            PopulateFormFields();

            // Turn all labels black except lblDate
            foreach (Control control in this.Controls)
            {
                if (control is Label && control.Name != "lblDate")
                {
                    control.ForeColor = Color.Black;
                }
            }
        }

        private void PopulateFormFields()
        {
            // Read data from your form fields and store their values to variables
            DateTime date = dateTimePicker1.Value;
            string vendor = txtVendor.Text;
            string description = txtDescription.Text;
            string category = txtCategory.Text;
            decimal.TryParse(txtLabor.Text, out decimal labor);
            decimal.TryParse(txtRent.Text, out decimal rent);
            decimal.TryParse(txtAudio.Text, out decimal audio);
            decimal.TryParse(txtVideo.Text, out decimal video);
            decimal.TryParse(txtAmount.Text, out decimal amount);

            BtnNext_Click(this, EventArgs.Empty);

            // Populate the form fields with the values from the stored variables
            dateTimePicker1.Value = date;
            txtVendor.Text = vendor;
            txtDescription.Text = description;
            txtCategory.Text = category;
            txtLabor.Text = labor.ToString();
            txtRent.Text = rent.ToString();
            txtAudio.Text = audio.ToString();
            txtVideo.Text = video.ToString();
            txtAmount.Text = amount.ToString();
        }

        private void btnSubmitBatch_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the batch ID
                int? batchID = GetBatchID();

                // Save the form data
                SaveFormData(batchID.Value);

                // Get the list of PDF files for the batch
                List<string> pdfList = GetPdfListForBatch((int)batchID);

                // Check if the saved directory exists and create it if necessary
                if (!string.IsNullOrEmpty(lblSavedDirectory.Text) && !Directory.Exists(lblSavedDirectory.Text))
                {
                    Directory.CreateDirectory(lblSavedDirectory.Text);
                }

                // Copy the selected PDF files to the fixed directory and move them to the saved directory, if specified
                foreach (string pdfPath in pdfList)
                {
                    string sourceFilePath = Path.Combine(lblDirectoryPath.Text, Path.GetFileName(pdfPath));

                    // Check if the source file exists before performing copy and move operations
                    if (File.Exists(sourceFilePath))
                    {
                        // Get the PDF file name
                        string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);

                        // Create a unique file path for the copy of the PDF file
                        string uniqueFilePath = Path.Combine(DBfilePath, pdfFileName + "-" + batchID.Value.ToString() + ".pdf");

                        // Copy the PDF file to the unique file path
                        File.Copy(sourceFilePath, uniqueFilePath, true);

                        // Move the PDF file to the saved directory, if specified
                        if (!string.IsNullOrEmpty(lblSavedDirectory.Text))
                        {
                            string savedFilePath = Path.Combine(lblSavedDirectory.Text, pdfFileName + ".pdf");
                            File.Move(sourceFilePath, savedFilePath);
                        }
                    }
                }

                listView.SelectedItems.Clear();
                listView.Items.Clear();
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


                // Display a message box indicating success
                MessageBox.Show("Batch submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                ClearFormFields();

                // Increment the pdfIncrement by 0.0001                
                pdfIncrement += 0.001m;

                // Update lblPdfIndex
                lblPdfIndex.Text = $"PDF Index: {currentPdfIndex + pdfIncrement}";                

                // Color the remaining PDF files
                ColorRemainingPdfFiles((int)batchID);

                listView1_SelectedIndexChanged(sender, new EventArgs());

            }
            catch (Exception ex)
            {
                // handle the exception here
                MessageBox.Show($"An Submit Batch error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> GetPdfListForBatch(int batchID)
        {
            List<string> pdfList = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT PdfFileName FROM ReceiptData WHERE BatchID=@batchID AND DataSaved=2", conn))
                    {
                        cmd.Parameters.AddWithValue("@batchID", batchID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pdfList.Add(reader.GetString(0));
                            }
                        }
                    }

                    // Update DataSaved to 3 where Amount is not greater than 0
                    using (SqlCommand cmdUpdate = new SqlCommand("UPDATE ReceiptData SET DataSaved=3, PdfIndex=.5001 WHERE BatchID=@batchID AND DataSaved=1 AND Amount <= 0", conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@batchID", batchID);
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An PDF List error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return pdfList;
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            // Define a dictionary of textbox names and their corresponding label names
            Dictionary<string, string> textBoxLabelPairs = new Dictionary<string, string>()
            {
                { "txtAmount", "lblAmount" },
                { "txtVendor", "lblVendor" },
                { "txtDescription", "lblDescription" },
                { "txtCategory", "lblCategory" },
                { "txtLabor", "lblLabor" },
                { "txtRent", "lblRent" },
                { "txtAudio", "lblAudio" },
                { "txtVideo", "lblVideo" },
                { "cboEvents", "lblEvents" },
                { "dateTimePicker1", "lblDate" },
            };

            // Loop through the pairs and set the font color for each label
            foreach (KeyValuePair<string, string> pair in textBoxLabelPairs)
            {
                Control[] textBoxMatches = this.Controls.Find(pair.Key, true);
                Control[] labelMatches = this.Controls.Find(pair.Value, true);

                if (textBoxMatches.Length > 0 && labelMatches.Length > 0 && textBoxMatches[0] is TextBox && labelMatches[0] is Label)
                {
                    TextBox textBox = (TextBox)textBoxMatches[0];
                    Label label = (Label)labelMatches[0];

                    if (textBox == sender)
                    {
                        label.ForeColor = Color.Red;
                    }
                    else
                    {
                        label.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            TextBox currTextBox = (TextBox)sender;
            Label currLabel = (Label)this.Controls.Find("lbl" + currTextBox.Name.Substring(3), true).FirstOrDefault();

            if (currLabel != null)
            {
                currLabel.ForeColor = Color.Red;
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox currTextBox = (TextBox)sender;
            Label currLabel = (Label)this.Controls.Find("lbl" + currTextBox.Name.Substring(3), true).FirstOrDefault();

            if (currLabel != null)
            {
                currLabel.ForeColor = Color.Black;
            }
        }
        private void dateTimePicker_Enter(object sender, EventArgs e)
        {
            Label currLabel = (Label)this.Controls.Find("lblDate", true)[0];
            currLabel.ForeColor = Color.Red;
        }


        private void dateTimePicker_Leave(object sender, EventArgs e)
        {
            Label currLabel = (Label)this.Controls.Find("lblDate", true)[0];
            currLabel.ForeColor = Color.Black;
        }

        private void comboBox_Enter(object sender, EventArgs e)
        {
            ComboBox currComboBox = (ComboBox)sender;
            Label currLabel = (Label)this.Controls.Find("lbl" + currComboBox.Name.Substring(3), true)[0];
            currLabel.ForeColor = Color.Red;
        }

        private void comboBox_Leave(object sender, EventArgs e)
        {
            ComboBox currComboBox = (ComboBox)sender;
            Label currLabel = (Label)this.Controls.Find("lbl" + currComboBox.Name.Substring(3), true)[0];
            currLabel.ForeColor = Color.Black;
        }

        private bool AreFormFieldsEmpty()
        {
            return string.IsNullOrWhiteSpace(txtVendor.Text) &&
                   string.IsNullOrWhiteSpace(txtDescription.Text) &&
                   string.IsNullOrWhiteSpace(txtCategory.Text) &&
                   string.IsNullOrWhiteSpace(txtLabor.Text) &&
                   string.IsNullOrWhiteSpace(txtRent.Text) &&
                   string.IsNullOrWhiteSpace(txtAudio.Text) &&
                   string.IsNullOrWhiteSpace(txtVideo.Text) &&
                   string.IsNullOrWhiteSpace(txtAmount.Text);
        }

        private void UpdateListViewItemColor(ListViewItem item, int dataSaved)
        {
            if (item == null || item.SubItems.Count == 0)
            {
                return; // Exit the method if the item is null or has no subitems
            }

            int index = item.SubItems.Count - 1; // Get the second last subitem
            ListViewItem.ListViewSubItem subItem = item.SubItems[index];

            if (dataSaved == 2)
            {
                item.ForeColor = Color.Green;
            }
            else
            {
                item.ForeColor = Color.Orange;
            }

        }
        private void ColorRemainingPdfFiles(int batchID)
        {
   
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT PdfFileName FROM ReceiptData WHERE BatchID=@batchID AND DataSaved=3";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@batchID", batchID);

                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string pdfFileName = reader.GetString(0);

                        foreach (ListViewItem item in listView.Items)
                        {
                            if (item.Text == pdfFileName)
                            {
                                item.ForeColor = Color.Orange;
                            }
                        }
                        // Turn all labels black except lblDate
                        foreach (Control control in this.Controls)
                        {
                            if (control is Label && control.Name != "lblDate")
                            {
                                control.ForeColor = Color.Black;
                            }
                        }
                    }
                }
            }
        }



    }
}
