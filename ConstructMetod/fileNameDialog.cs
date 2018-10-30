using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConstructMetod
{
    public partial class fileNameDialog : Form
    {
        public fileNameDialog()
        {
            InitializeComponent();
            expansion.SelectedIndex = 0;
        }

        

        private void fileOrFolder_KeyUp(object sender, KeyEventArgs e)
        {
            if (fileOrFolder.Text != "")
            {
                label2.Visible = false;
                button1.Enabled = true;
            }
            else
            {
                label2.Visible = true;
                button1.Enabled = false;
            }
        }

        private void fileNameDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
