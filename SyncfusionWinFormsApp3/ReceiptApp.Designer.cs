namespace SyncfusionWinFormsApp3
{
    partial class ReceiptApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings messageBoxSettings1 = new Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings();
            Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings pdfViewerPrinterSettings1 = new Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiptApp));
            Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings textSearchSettings1 = new Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings();
            imageListAdv1 = new Syncfusion.Windows.Forms.Tools.ImageListAdv(components);
            listView = new ListView();
            pdfViewerControl1 = new Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl();
            lblDirectoryPath = new Label();
            btnDirectoryPath = new Button();
            btnSavedDirectory = new Button();
            lblSavedDirectory = new Label();
            cboUsername = new ComboBox();
            lblUser = new Label();
            cboEvents = new ComboBox();
            lblEvents = new Label();
            bannerTextProvider1 = new Syncfusion.Windows.Forms.BannerTextProvider(components);
            btnEditUser = new Button();
            lblAmount = new Label();
            txtAmount = new TextBox();
            lblCity = new Label();
            lblState = new Label();
            lblZip = new Label();
            lblAddress = new Label();
            txtVendor = new TextBox();
            lblVendor = new Label();
            txtCategory = new TextBox();
            lblCategory = new Label();
            txtDescription = new TextBox();
            lblDescription = new Label();
            dateTimePicker1 = new DateTimePicker();
            lblDate = new Label();
            lblAudio = new Label();
            txtAudio = new TextBox();
            txtVideo = new TextBox();
            lblVideo = new Label();
            txtRent = new TextBox();
            lblRent = new Label();
            txtLabor = new TextBox();
            lblLabor = new Label();
            btnPrevious = new Button();
            btnNext = new Button();
            btnSaveNext = new Button();
            btnSaveRepeat = new Button();
            btnSubmitBatch = new Button();
            lblPdfIndex = new Label();
            lblBatchTime = new Label();
            lblBatchNumber = new Label();
            lblBatchID = new Label();
            lblUserID = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // listView
            // 
            listView.Location = new Point(861, 68);
            listView.Name = "listView";
            listView.Size = new Size(232, 408);
            listView.TabIndex = 0;
            listView.UseCompatibleStateImageBehavior = false;
            listView.ItemSelectionChanged += listView1_ItemSelectionChanged;
            // 
            // pdfViewerControl1
            // 
            pdfViewerControl1.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.SelectTool;
            pdfViewerControl1.EnableContextMenu = true;
            pdfViewerControl1.EnableNotificationBar = true;
            pdfViewerControl1.HorizontalScrollOffset = 0;
            pdfViewerControl1.IsBookmarkEnabled = true;
            pdfViewerControl1.IsTextSearchEnabled = true;
            pdfViewerControl1.IsTextSelectionEnabled = true;
            pdfViewerControl1.Location = new Point(21, 68);
            messageBoxSettings1.EnableNotification = true;
            pdfViewerControl1.MessageBoxSettings = messageBoxSettings1;
            pdfViewerControl1.MinimumZoomPercentage = 50;
            pdfViewerControl1.Name = "pdfViewerControl1";
            pdfViewerControl1.PageBorderThickness = 1;
            pdfViewerPrinterSettings1.Copies = 1;
            pdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings1.PrintLocation = (PointF)resources.GetObject("pdfViewerPrinterSettings1.PrintLocation");
            pdfViewerPrinterSettings1.ShowPrintStatusDialog = true;
            pdfViewerControl1.PrinterSettings = pdfViewerPrinterSettings1;
            pdfViewerControl1.ReferencePath = null;
            pdfViewerControl1.ScrollDisplacementValue = 0;
            pdfViewerControl1.ShowHorizontalScrollBar = true;
            pdfViewerControl1.ShowToolBar = true;
            pdfViewerControl1.ShowVerticalScrollBar = true;
            pdfViewerControl1.Size = new Size(817, 548);
            pdfViewerControl1.SpaceBetweenPages = 8;
            pdfViewerControl1.TabIndex = 1;
            pdfViewerControl1.Text = "pdfViewerControl1";
            textSearchSettings1.CurrentInstanceColor = Color.FromArgb(127, 255, 171, 64);
            textSearchSettings1.HighlightAllInstance = true;
            textSearchSettings1.OtherInstanceColor = Color.FromArgb(127, 254, 255, 0);
            pdfViewerControl1.TextSearchSettings = textSearchSettings1;
            pdfViewerControl1.ThemeName = "Default";
            pdfViewerControl1.VerticalScrollOffset = 0;
            pdfViewerControl1.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default;
            pdfViewerControl1.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
            // 
            // lblDirectoryPath
            // 
            lblDirectoryPath.AutoSize = true;
            lblDirectoryPath.Location = new Point(351, 31);
            lblDirectoryPath.Name = "lblDirectoryPath";
            lblDirectoryPath.Size = new Size(24, 15);
            lblDirectoryPath.TabIndex = 3;
            lblDirectoryPath.Text = "NA";
            // 
            // btnDirectoryPath
            // 
            btnDirectoryPath.Location = new Point(277, 23);
            btnDirectoryPath.Name = "btnDirectoryPath";
            btnDirectoryPath.Size = new Size(68, 24);
            btnDirectoryPath.TabIndex = 4;
            btnDirectoryPath.TabStop = false;
            btnDirectoryPath.Text = "Directory";
            btnDirectoryPath.UseVisualStyleBackColor = true;
            btnDirectoryPath.Click += btnDirectoryPath_Click;
            // 
            // btnSavedDirectory
            // 
            btnSavedDirectory.Location = new Point(682, 26);
            btnSavedDirectory.Name = "btnSavedDirectory";
            btnSavedDirectory.Size = new Size(68, 24);
            btnSavedDirectory.TabIndex = 6;
            btnSavedDirectory.TabStop = false;
            btnSavedDirectory.Text = "Saved Directory";
            btnSavedDirectory.UseVisualStyleBackColor = true;
            btnSavedDirectory.Click += btnSavedDirectory_Click;
            // 
            // lblSavedDirectory
            // 
            lblSavedDirectory.AutoSize = true;
            lblSavedDirectory.Location = new Point(756, 31);
            lblSavedDirectory.Name = "lblSavedDirectory";
            lblSavedDirectory.Size = new Size(24, 15);
            lblSavedDirectory.TabIndex = 5;
            lblSavedDirectory.Text = "NA";
            // 
            // cboUsername
            // 
            cboUsername.FormattingEnabled = true;
            cboUsername.Location = new Point(21, 23);
            cboUsername.Name = "cboUsername";
            cboUsername.Size = new Size(138, 23);
            cboUsername.TabIndex = 7;
            cboUsername.TabStop = false;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(21, 5);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(30, 15);
            lblUser.TabIndex = 8;
            lblUser.Text = "User";
            // 
            // cboEvents
            // 
            cboEvents.FormattingEnabled = true;
            cboEvents.Location = new Point(972, 486);
            cboEvents.Name = "cboEvents";
            cboEvents.Size = new Size(121, 23);
            cboEvents.TabIndex = 14;
            cboEvents.Text = "Empty";
            // 
            // lblEvents
            // 
            lblEvents.AutoSize = true;
            lblEvents.Location = new Point(930, 489);
            lblEvents.Name = "lblEvents";
            lblEvents.Size = new Size(36, 15);
            lblEvents.TabIndex = 10;
            lblEvents.Text = "Event";
            // 
            // btnEditUser
            // 
            btnEditUser.Location = new Point(165, 22);
            btnEditUser.Name = "btnEditUser";
            btnEditUser.Size = new Size(78, 25);
            btnEditUser.TabIndex = 12;
            btnEditUser.TabStop = false;
            btnEditUser.Text = "Edit User";
            btnEditUser.UseVisualStyleBackColor = true;
            btnEditUser.Click += btnEditUser_Click;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Location = new Point(549, 758);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(51, 15);
            lblAmount.TabIndex = 13;
            lblAmount.Text = "Amount";
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(606, 755);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(123, 23);
            txtAmount.TabIndex = 9;
            // 
            // lblCity
            // 
            lblCity.AutoSize = true;
            lblCity.Location = new Point(882, 543);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(28, 15);
            lblCity.TabIndex = 15;
            lblCity.Text = "City";
            // 
            // lblState
            // 
            lblState.AutoSize = true;
            lblState.Location = new Point(991, 543);
            lblState.Name = "lblState";
            lblState.Size = new Size(33, 15);
            lblState.TabIndex = 16;
            lblState.Text = "State";
            // 
            // lblZip
            // 
            lblZip.AutoSize = true;
            lblZip.Location = new Point(1059, 543);
            lblZip.Name = "lblZip";
            lblZip.Size = new Size(24, 15);
            lblZip.TabIndex = 17;
            lblZip.Text = "Zip";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(882, 518);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(49, 15);
            lblAddress.TabIndex = 18;
            lblAddress.Text = "Address";
            // 
            // txtVendor
            // 
            txtVendor.Location = new Point(384, 665);
            txtVendor.Name = "txtVendor";
            txtVendor.Size = new Size(123, 23);
            txtVendor.TabIndex = 2;
            // 
            // lblVendor
            // 
            lblVendor.AutoSize = true;
            lblVendor.Location = new Point(327, 668);
            lblVendor.Name = "lblVendor";
            lblVendor.Size = new Size(44, 15);
            lblVendor.TabIndex = 19;
            lblVendor.Text = "Vendor";
            // 
            // txtCategory
            // 
            txtCategory.Location = new Point(120, 710);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(123, 23);
            txtCategory.TabIndex = 4;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(63, 713);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(55, 15);
            lblCategory.TabIndex = 21;
            lblCategory.Text = "Category";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(606, 665);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(123, 23);
            txtDescription.TabIndex = 3;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(533, 668);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 23;
            lblDescription.Text = "Description";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(99, 665);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(144, 23);
            dateTimePicker1.TabIndex = 1;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(62, 671);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(31, 15);
            lblDate.TabIndex = 26;
            lblDate.Text = "Date";
            // 
            // lblAudio
            // 
            lblAudio.AutoSize = true;
            lblAudio.Location = new Point(65, 755);
            lblAudio.Name = "lblAudio";
            lblAudio.Size = new Size(39, 15);
            lblAudio.TabIndex = 27;
            lblAudio.Text = "Audio";
            // 
            // txtAudio
            // 
            txtAudio.Location = new Point(122, 752);
            txtAudio.Name = "txtAudio";
            txtAudio.Size = new Size(123, 23);
            txtAudio.TabIndex = 7;
            // 
            // txtVideo
            // 
            txtVideo.Location = new Point(384, 755);
            txtVideo.Name = "txtVideo";
            txtVideo.Size = new Size(123, 23);
            txtVideo.TabIndex = 8;
            // 
            // lblVideo
            // 
            lblVideo.AutoSize = true;
            lblVideo.Location = new Point(327, 758);
            lblVideo.Name = "lblVideo";
            lblVideo.Size = new Size(37, 15);
            lblVideo.TabIndex = 29;
            lblVideo.Text = "Video";
            // 
            // txtRent
            // 
            txtRent.Location = new Point(606, 713);
            txtRent.Name = "txtRent";
            txtRent.Size = new Size(123, 23);
            txtRent.TabIndex = 6;
            // 
            // lblRent
            // 
            lblRent.AutoSize = true;
            lblRent.Location = new Point(549, 716);
            lblRent.Name = "lblRent";
            lblRent.Size = new Size(31, 15);
            lblRent.TabIndex = 31;
            lblRent.Text = "Rent";
            // 
            // txtLabor
            // 
            txtLabor.Location = new Point(384, 710);
            txtLabor.Name = "txtLabor";
            txtLabor.Size = new Size(123, 23);
            txtLabor.TabIndex = 5;
            // 
            // lblLabor
            // 
            lblLabor.AutoSize = true;
            lblLabor.Location = new Point(327, 713);
            lblLabor.Name = "lblLabor";
            lblLabor.Size = new Size(37, 15);
            lblLabor.TabIndex = 33;
            lblLabor.Text = "Labor";
            // 
            // btnPrevious
            // 
            btnPrevious.Location = new Point(65, 803);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(94, 27);
            btnPrevious.TabIndex = 10;
            btnPrevious.Text = "Previous";
            btnPrevious.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(192, 803);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(96, 27);
            btnNext.TabIndex = 11;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            // 
            // btnSaveNext
            // 
            btnSaveNext.Location = new Point(384, 803);
            btnSaveNext.Name = "btnSaveNext";
            btnSaveNext.Size = new Size(107, 27);
            btnSaveNext.TabIndex = 12;
            btnSaveNext.Text = "Save and Next";
            btnSaveNext.UseVisualStyleBackColor = true;
            btnSaveNext.Click += btnSaveNext_Click;
            // 
            // btnSaveRepeat
            // 
            btnSaveRepeat.Location = new Point(523, 803);
            btnSaveRepeat.Name = "btnSaveRepeat";
            btnSaveRepeat.Size = new Size(111, 27);
            btnSaveRepeat.TabIndex = 13;
            btnSaveRepeat.Text = "Save and Repeat";
            btnSaveRepeat.UseVisualStyleBackColor = true;
            btnSaveRepeat.Click += btnSaveRepeat_Click;
            // 
            // btnSubmitBatch
            // 
            btnSubmitBatch.Location = new Point(882, 817);
            btnSubmitBatch.Name = "btnSubmitBatch";
            btnSubmitBatch.Size = new Size(111, 27);
            btnSubmitBatch.TabIndex = 39;
            btnSubmitBatch.TabStop = false;
            btnSubmitBatch.Text = "Submit Batch";
            btnSubmitBatch.UseVisualStyleBackColor = true;
            btnSubmitBatch.Click += btnSubmitBatch_Click;
            // 
            // lblPdfIndex
            // 
            lblPdfIndex.AutoSize = true;
            lblPdfIndex.Location = new Point(882, 710);
            lblPdfIndex.Name = "lblPdfIndex";
            lblPdfIndex.Size = new Size(36, 15);
            lblPdfIndex.TabIndex = 40;
            lblPdfIndex.Text = "Index";
            // 
            // lblBatchTime
            // 
            lblBatchTime.AutoSize = true;
            lblBatchTime.Location = new Point(882, 787);
            lblBatchTime.Name = "lblBatchTime";
            lblBatchTime.Size = new Size(66, 15);
            lblBatchTime.TabIndex = 41;
            lblBatchTime.Text = "Batch Time";
            // 
            // lblBatchNumber
            // 
            lblBatchNumber.AutoSize = true;
            lblBatchNumber.Location = new Point(882, 763);
            lblBatchNumber.Name = "lblBatchNumber";
            lblBatchNumber.Size = new Size(84, 15);
            lblBatchNumber.TabIndex = 42;
            lblBatchNumber.Text = "Batch Number";
            // 
            // lblBatchID
            // 
            lblBatchID.AutoSize = true;
            lblBatchID.Location = new Point(882, 732);
            lblBatchID.Name = "lblBatchID";
            lblBatchID.Size = new Size(51, 15);
            lblBatchID.TabIndex = 43;
            lblBatchID.Text = "Batch ID";
            // 
            // lblUserID
            // 
            lblUserID.AutoSize = true;
            lblUserID.Location = new Point(1042, 710);
            lblUserID.Name = "lblUserID";
            lblUserID.Size = new Size(41, 15);
            lblUserID.TabIndex = 44;
            lblUserID.Text = "UserID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(995, 710);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 45;
            label1.Text = "UserID: ";
            // 
            // ReceiptApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1127, 882);
            Controls.Add(label1);
            Controls.Add(lblUserID);
            Controls.Add(lblBatchID);
            Controls.Add(lblBatchNumber);
            Controls.Add(lblBatchTime);
            Controls.Add(lblPdfIndex);
            Controls.Add(btnSubmitBatch);
            Controls.Add(btnSaveRepeat);
            Controls.Add(btnSaveNext);
            Controls.Add(btnNext);
            Controls.Add(btnPrevious);
            Controls.Add(txtLabor);
            Controls.Add(lblLabor);
            Controls.Add(txtRent);
            Controls.Add(lblRent);
            Controls.Add(txtVideo);
            Controls.Add(lblVideo);
            Controls.Add(txtAudio);
            Controls.Add(lblAudio);
            Controls.Add(lblDate);
            Controls.Add(dateTimePicker1);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(txtCategory);
            Controls.Add(lblCategory);
            Controls.Add(txtVendor);
            Controls.Add(lblVendor);
            Controls.Add(lblAddress);
            Controls.Add(lblZip);
            Controls.Add(lblState);
            Controls.Add(lblCity);
            Controls.Add(txtAmount);
            Controls.Add(lblAmount);
            Controls.Add(btnEditUser);
            Controls.Add(lblEvents);
            Controls.Add(cboEvents);
            Controls.Add(lblUser);
            Controls.Add(cboUsername);
            Controls.Add(btnSavedDirectory);
            Controls.Add(lblSavedDirectory);
            Controls.Add(btnDirectoryPath);
            Controls.Add(lblDirectoryPath);
            Controls.Add(pdfViewerControl1);
            Controls.Add(listView);
            Name = "ReceiptApp";
            Text = "Receipt App";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.ImageListAdv imageListAdv1;
        private ListView listView;
        private Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl pdfViewerControl1;
        private Label lblDirectoryPath;
        private Button btnDirectoryPath;
        private Button btnSavedDirectory;
        private Label lblSavedDirectory;
        private ComboBox cboUsername;
        private Label lblUser;
        private ComboBox cboEvents;
        private Label lblEvents;
        private Syncfusion.Windows.Forms.BannerTextProvider bannerTextProvider1;
        private Button btnEditUser;
        private Label lblAmount;
        private TextBox txtAmount;
        private Label lblCity;
        private Label lblState;
        private Label lblZip;
        private Label lblAddress;
        private TextBox txtVendor;
        private Label lblVendor;
        private TextBox txtCategory;
        private Label lblCategory;
        private TextBox txtDescription;
        private Label lblDescription;
        private DateTimePicker dateTimePicker1;
        private Label lblDate;
        private Label lblAudio;
        private TextBox txtAudio;
        private TextBox txtVideo;
        private Label lblVideo;
        private TextBox txtRent;
        private Label lblRent;
        private TextBox txtLabor;
        private Label lblLabor;
        private Button btnPrevious;
        private Button btnNext;
        private Button btnSaveNext;
        private Button btnSaveRepeat;
        private Button btnSubmitBatch;
        private Label lblPdfIndex;
        private Label lblBatchTime;
        private Label lblBatchNumber;
        private Label lblBatchID;
        private Label lblUserID;
        private Label label1;
    }
}

