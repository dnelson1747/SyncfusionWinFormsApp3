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
            Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings messageBoxSettings2 = new Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings();
            Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings pdfViewerPrinterSettings2 = new Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiptApp));
            Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings textSearchSettings2 = new Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings();
            imageListAdv1 = new Syncfusion.Windows.Forms.Tools.ImageListAdv(components);
            listView = new ListView();
            pdfViewerControl1 = new Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl();
            lblDirectoryPath = new Label();
            btnDirectoryPath = new Button();
            btnSavedDirectory = new Button();
            lblSavedDirectory = new Label();
            cboUsername = new ComboBox();
            lblUser = new Label();
            cboEvent = new ComboBox();
            lblEvent = new Label();
            SuspendLayout();
            // 
            // listView
            // 
            listView.Location = new Point(12, 52);
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
            pdfViewerControl1.Location = new Point(250, 51);
            messageBoxSettings2.EnableNotification = true;
            pdfViewerControl1.MessageBoxSettings = messageBoxSettings2;
            pdfViewerControl1.MinimumZoomPercentage = 50;
            pdfViewerControl1.Name = "pdfViewerControl1";
            pdfViewerControl1.PageBorderThickness = 1;
            pdfViewerPrinterSettings2.Copies = 1;
            pdfViewerPrinterSettings2.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings2.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings2.PrintLocation = (PointF)resources.GetObject("pdfViewerPrinterSettings2.PrintLocation");
            pdfViewerPrinterSettings2.ShowPrintStatusDialog = true;
            pdfViewerControl1.PrinterSettings = pdfViewerPrinterSettings2;
            pdfViewerControl1.ReferencePath = null;
            pdfViewerControl1.ScrollDisplacementValue = 0;
            pdfViewerControl1.ShowHorizontalScrollBar = true;
            pdfViewerControl1.ShowToolBar = true;
            pdfViewerControl1.ShowVerticalScrollBar = true;
            pdfViewerControl1.Size = new Size(848, 588);
            pdfViewerControl1.SpaceBetweenPages = 8;
            pdfViewerControl1.TabIndex = 1;
            pdfViewerControl1.Text = "pdfViewerControl1";
            textSearchSettings2.CurrentInstanceColor = Color.FromArgb(127, 255, 171, 64);
            textSearchSettings2.HighlightAllInstance = true;
            textSearchSettings2.OtherInstanceColor = Color.FromArgb(127, 254, 255, 0);
            pdfViewerControl1.TextSearchSettings = textSearchSettings2;
            pdfViewerControl1.ThemeName = "Default";
            pdfViewerControl1.VerticalScrollOffset = 0;
            pdfViewerControl1.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default;
            pdfViewerControl1.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
            // 
            // lblDirectoryPath
            // 
            lblDirectoryPath.AutoSize = true;
            lblDirectoryPath.Location = new Point(95, 27);
            lblDirectoryPath.Name = "lblDirectoryPath";
            lblDirectoryPath.Size = new Size(24, 15);
            lblDirectoryPath.TabIndex = 3;
            lblDirectoryPath.Text = "NA";
            // 
            // btnDirectoryPath
            // 
            btnDirectoryPath.Location = new Point(21, 22);
            btnDirectoryPath.Name = "btnDirectoryPath";
            btnDirectoryPath.Size = new Size(68, 24);
            btnDirectoryPath.TabIndex = 4;
            btnDirectoryPath.Text = "Directory";
            btnDirectoryPath.UseVisualStyleBackColor = true;
            btnDirectoryPath.Click += btnDirectoryPath_Click;
            // 
            // btnSavedDirectory
            // 
            btnSavedDirectory.Location = new Point(537, 18);
            btnSavedDirectory.Name = "btnSavedDirectory";
            btnSavedDirectory.Size = new Size(68, 24);
            btnSavedDirectory.TabIndex = 6;
            btnSavedDirectory.Text = "Saved Directory";
            btnSavedDirectory.UseVisualStyleBackColor = true;
            // 
            // lblSavedDirectory
            // 
            lblSavedDirectory.AutoSize = true;
            lblSavedDirectory.Location = new Point(611, 23);
            lblSavedDirectory.Name = "lblSavedDirectory";
            lblSavedDirectory.Size = new Size(24, 15);
            lblSavedDirectory.TabIndex = 5;
            lblSavedDirectory.Text = "NA";
            // 
            // cboUsername
            // 
            cboUsername.FormattingEnabled = true;
            cboUsername.Location = new Point(960, 20);
            cboUsername.Name = "cboUsername";
            cboUsername.Size = new Size(138, 23);
            cboUsername.TabIndex = 7;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(924, 23);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(30, 15);
            lblUser.TabIndex = 8;
            lblUser.Text = "User";
            // 
            // cboEvent
            // 
            cboEvent.FormattingEnabled = true;
            cboEvent.Location = new Point(83, 497);
            cboEvent.Name = "cboEvent";
            cboEvent.Size = new Size(121, 23);
            cboEvent.TabIndex = 9;
            // 
            // lblEvent
            // 
            lblEvent.AutoSize = true;
            lblEvent.Location = new Point(41, 500);
            lblEvent.Name = "lblEvent";
            lblEvent.Size = new Size(36, 15);
            lblEvent.TabIndex = 10;
            lblEvent.Text = "Event";
            // 
            // ReceiptApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1127, 832);
            Controls.Add(lblEvent);
            Controls.Add(cboEvent);
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
        private ComboBox cboEvent;
        private Label lblEvent;
    }
}

