using Syncfusion.Windows.Forms.PdfViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Syncfusion.Windows.Forms.Tools.Navigation.Bar;

namespace SyncfusionWinFormsApp3
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

            string directoryPath = @"C:\Users\djn\Downloads\EssentialStudio_CodeScanReports\";
            string[] pdfFiles = Directory.GetFiles(directoryPath, "*.pdf");

            // Set the ListView control to Details view mode
            listView1.View = View.Details;

            // Add a column to the ListView control to display the file names
            listView1.Columns.Add("File Name", 300);

            foreach (string pdfFile in pdfFiles)
            {
                // Add the file name to the ListView control
                ListViewItem item = new ListViewItem(Path.GetFileName(pdfFile));

                // Add the item to the ListView control
                listView1.Items.Add(item);
            }
            Form1_Load(this, EventArgs.Empty);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string directoryPath = @"C:\Users\djn\Downloads\EssentialStudio_CodeScanReports\";
            string[] pdfFiles = Directory.GetFiles(directoryPath, "*.pdf");

            if (pdfFiles.Length > 0)
            {
                string firstPdfFile = pdfFiles[0];
                string fullPath = Path.Combine(directoryPath, firstPdfFile);
                pdfViewerControl1.Load(fullPath);
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string directoryPath = @"C:\Users\djn\Downloads\EssentialStudio_CodeScanReports\";

            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string selectedFile = Path.Combine(directoryPath, listView1.SelectedItems[0].Text);
                    pdfViewerControl1.Load(selectedFile);
                }
            }

        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                listView1_SelectedIndexChanged(sender, e);
            }
        }
    }


}
