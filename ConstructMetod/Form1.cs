using System;
using System.IO;
using System.Windows.Forms;

namespace ConstructMetod
{
    public partial class Form1 : Form


    {
        string searchMain = "ConstructMetod";


        public Form1()
        {
            InitializeComponent();
        }

        public String FileNameDialog()
        {
            DialogResult dr = new DialogResult();
            fileNameDialog frm2 = new fileNameDialog();
            dr = frm2.ShowDialog();
            string name = "";
            if (dr == DialogResult.OK)
            {
                name = frm2.richTextBox1.Text.ToString();
            }
            else if (dr == DialogResult.Cancel)
            {
                frm2.Close();
            }
            return name;
        }

        public void loadDir(string path, ListBox onelistBox1)
        {
            try
            {
                onelistBox1.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo crrDir in dirs)
                { onelistBox1.Items.Add(crrDir); }
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo crrfile in files)
                { onelistBox1.Items.Add(crrfile); }
            }
            catch (Exception ms) { }
        }

        public void createWorkPanel(ListBox mlistBox, string name, string search)
        {
            using (StreamReader sr = new StreamReader(Path.Combine(search, mlistBox.SelectedItem.ToString()), System.Text.Encoding.Default))
            {


                TabPage tp = new TabPage();

                tp.Text = name;
                tp.Width = tabControl1.Width - 10;
                tp.Height = tabControl1.Height - 10;

                RichTextBox TB = new RichTextBox();
                TB.Name = "rtb name";
                TB.Dock = DockStyle.Fill;


                TB.Text = sr.ReadToEnd();
                Console.Write(sr.ReadToEnd());
                tp.Controls.Add(TB);
                tabControl2.TabPages.Add(tp);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadDir(searchMain, listBox1);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"a:\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|Allfiles(*.*) | *.* ";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {


                            TabPage tp = new TabPage();

                            tp.Text = openFileDialog1.FileName;
                            tp.Width = tabControl1.Width - 10;
                            tp.Height = tabControl1.Height - 10;

                            RichTextBox TB = new RichTextBox();
                            TB.Name = "rtb name";
                            TB.Dock = DockStyle.Fill;
                            TB.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);

                            tp.Controls.Add(TB);
                            tabControl2.TabPages.Add(tp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk: " + ex.Message);
                }
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                listBox1.ContextMenuStrip = contextMenuStrip1;
                int index = listBox1.IndexFromPoint(e.X, e.Y);
                if (index != -1)
                {
                    listBox1.SetSelected(index, true);
                }
            }
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // поиск слеша для удаления если он есть в конце пути
                if (searchMain[searchMain.Length - 1] == '\\')
                {
                    searchMain = searchMain.Remove(searchMain.Length - 1, 1);

                    //удаляем пока не встретиться другой слэш
                    while (searchMain[searchMain.Length - 1] != '\\' || searchMain == "")
                    {
                        searchMain = searchMain.Remove(searchMain.Length - 1, 1);
                    }
                }
                //иначе удаляем пока не встретиься слэш 

                else if (searchMain[searchMain.Length - 1] != '\\')
                {
                    while (searchMain[searchMain.Length - 1] != '\\')
                    {
                        searchMain = searchMain.Remove(searchMain.Length - 1, 1);
                    }
                }
            }
            catch (IndexOutOfRangeException index)
            {
                loadDir(searchMain = "ConstructMetod", listBox1);
            }

            loadDir(searchMain, listBox1);
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //добавление в строку пути название папки   

            if (listBox1.SelectedItem != null)
            {
                if (Path.GetExtension(Path.Combine(searchMain, listBox1.SelectedItem.ToString())) == "")
                {
                    searchMain = Path.Combine(searchMain + "\\" + listBox1.SelectedItem.ToString());
                    loadDir(searchMain, listBox1);
                }
                else
                {
                    createWorkPanel(listBox1, listBox1.SelectedItem.ToString(), searchMain);
                }
            }
        }

        private void создатьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderName = FileNameDialog();
            DirectoryInfo directoryInfo = new DirectoryInfo(searchMain + "\\" + folderName);
            if (!directoryInfo.Exists)
            {
                if (folderName != "")
                {
                    directoryInfo.Create();

                }
                loadDir(searchMain, listBox1);
            }
        }

        private void создатьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string fileName = FileNameDialog();
            FileInfo fnf = new FileInfo(searchMain + "\\" + fileName + ".txt");
            if (!fnf.Exists)
            {
                if (fileName != "")
                {
                    StreamWriter a = new StreamWriter(File.Create(searchMain + "\\" + fileName + ".txt"));
                    // createWorkPanel(listBox1,namen, searchMain);
                    Console.WriteLine(searchMain + "\\" + fileName + ".txt");
                    a.Close();
                }
            }

            loadDir(searchMain, listBox1);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fileDelete = new FileInfo(Path.Combine(searchMain, listBox1.SelectedItem.ToString()));
            DirectoryInfo dirDelete = new DirectoryInfo(Path.Combine(searchMain, listBox1.SelectedItem.ToString()));
            if (Path.GetExtension(Path.Combine(searchMain, listBox1.SelectedItem.ToString())) == "")
            {
                if (dirDelete.Exists)
                {
                    dirDelete.Delete(true);
                }
            }
                if (Path.GetExtension(Path.Combine(searchMain, listBox1.SelectedItem.ToString())) != "")
                {
                    if (fileDelete.Exists)
                    {
                        File.Delete(Path.Combine(searchMain, listBox1.SelectedItem.ToString()));
                    }
                }
                loadDir(searchMain, listBox1);
            
        }
    }
}
