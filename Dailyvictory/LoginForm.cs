using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dailyvictory
{
    public partial class LoginForm : Form
    {
        //FORMS CONTROLS INITIALIZE
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
            private void btnLogin_Click(object sender, EventArgs e)
        {
            //  LOGIN CHECK
            if (txtUsername.Text == "admin" && txtPassword.Text == "1234")
            { 
                // SUCCESSFUL LOGIN
                Dashboard d = new Dashboard();
                d.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }
        // TO EXIT THE APPLICATION
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
