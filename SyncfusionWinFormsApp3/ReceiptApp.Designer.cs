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
            lblEvent = new Label();
            bannerTextProvider1 = new Syncfusion.Windows.Forms.BannerTextProvider(components);
            btnEditUser = new Button();
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
            btnSavedDirectory.Text = "Saved Directory";
            btnSavedDirectory.UseVisualStyleBackColor = true;
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
            cboEvents.TabIndex = 9;
            cboEvents.Text = "Empty";
            // 
            // lblEvent
            // 
            lblEvent.AutoSize = true;
            lblEvent.Location = new Point(930, 489);
            lblEvent.Name = "lblEvent";
            lblEvent.Size = new Size(36, 15);
            lblEvent.TabIndex = 10;
            lblEvent.Text = "Event";
            // 
            // btnEditUser
            // 
            btnEditUser.Location = new Point(165, 22);
            btnEditUser.Name = "btnEditUser";
            btnEditUser.Size = new Size(78, 25);
            btnEditUser.TabIndex = 12;
            btnEditUser.Text = "Edit User";
            btnEditUser.UseVisualStyleBackColor = true;
            btnEditUser.Click += btnEditUser_Click;
            // 
            // ReceiptApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1127, 832);
            Controls.Add(btnEditUser);
            Controls.Add(lblEvent);
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
        private Label lblEvent;
        private Syncfusion.Windows.Forms.BannerTextProvider bannerTextProvider1;
        private Button btnEditUser;
    }
}

