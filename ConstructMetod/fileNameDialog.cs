
using System.Windows.Forms;

namespace ConstructMetod
{
    public partial class fileNameDialog : Form
    {
        public fileNameDialog()
        {
            InitializeComponent();
            expansion.SelectedIndex = 0;
            label2.Visible = true;
        }

        //Выводит сообщения пользвоателю о пустом значении поля
        private void fileOrFolder_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
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

        //запрет на ввод определённых симоволов
        private void fileOrFolder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar=='[' | e.KeyChar == ']'| e.KeyChar == '{'| e.KeyChar == '}'| e.KeyChar == '.'| e.KeyChar == '\\'| e.KeyChar == '/'| e.KeyChar == '|')
            {
                toolTip1.Show("Запрещён ввод символов '[]{}/|\\.'",fileOrFolder);
                e.Handled = true;
            }    
        }

    }
}
