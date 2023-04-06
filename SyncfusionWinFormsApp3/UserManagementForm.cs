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

        //public UserManagementForm()
        //{
        //    InitializeComponent();
        //}

        //private void btnAddUser_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtUsername.Text))
        //    {
        //        MessageBox.Show("Please enter a username.");
        //        return;
        //    }

        //    NewUsername = txtUsername.Text;
        //    SetAsDefault = chkSetAsDefault.Checked;
        //    DialogResult = DialogResult.OK;
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }

}
