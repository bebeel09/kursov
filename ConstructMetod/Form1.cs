using System;
using System.IO;
using System.Windows.Forms;


namespace ConstructMetod
{
    public partial class Form1 : Form


    {
        string searchMain = "ConstructMetod";
        int iPage = -1;
        

        public Form1()
        {
            InitializeComponent();
            listBox1.ContextMenuStrip = contextMenuStrip1;
            tabControl2.ContextMenuStrip = contextMenuStrip2;
            
        }

        public String FileNameDialog(int numberOperation)
        {
            string name = "";
            string tochka = "";
            int kostil = 0;
            DialogResult dr = new DialogResult();
            fileNameDialog frm2 = new fileNameDialog();
            switch(numberOperation)
            {
                case 1: // операция папки
                    frm2.textPresent.Text = "Введите название папки:";
                    frm2.formatPreaent.Visible = false;
                    frm2.expansion.Visible = false;
                    tochka = "";
                    kostil = 1;
                    break;
                case 2: //операция файла
                    frm2.textPresent.Text = "Введите название файла:";
                    frm2.formatPreaent.Visible = true;
                    frm2.expansion.Visible = true;
                    tochka = ".";
                    kostil = 2;
                    break;
            }
            dr = frm2.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (kostil == 1) {
                   
                    name = frm2.fileOrFolder.Text.ToString();
                    Console.WriteLine(name+"2");
                }
                else if (kostil==2){
                    name = frm2.fileOrFolder.Text.ToString() + tochka.ToString() + frm2.expansion.Text.ToString();
                    Console.WriteLine(name);
                }
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

        public void createWorkPanel(ListBox mlistBox, string search)
        {  
                TabPage tp = new TabPage();
                tp.Name = mlistBox.SelectedItem.ToString();
                tp.Text = mlistBox.SelectedItem.ToString();
                tp.Width = tabControl1.Width - 10;
                tp.Height = tabControl1.Height - 10;

                RichTextBox RTB = new RichTextBox();
                RTB.Name = (Path.Combine(search, mlistBox.SelectedItem.ToString()));

                RTB.Dock = DockStyle.Fill;
                try
                {
                    RTB.LoadFile(Path.Combine(search, mlistBox.SelectedItem.ToString()), RichTextBoxStreamType.RichText);
                }
                catch (Exception ert) { }
                // RTB.Text = sr.ReadToEnd();

                tp.Controls.Add(RTB);
                tabControl2.TabPages.Add(tp);
            

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
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|Allfiles(*.*) | *.* ";
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
            { //проверка на расширение выбранного объекта
                if (Path.GetExtension(Path.Combine(searchMain, listBox1.SelectedItem.ToString())) == "")
                {
                  
                    searchMain = Path.Combine(searchMain + "\\" + listBox1.SelectedItem.ToString());
                    loadDir(searchMain, listBox1);
                }
                else
                {
                  //  Console.WriteLine(Path.GetExtension(Path.Combine(searchMain, listBox1.SelectedItem.ToString())));
                    bool controlName = false; //хранит данные о том нашлось ли такое же имя?
                    //цикл проверки вкладки с таким же именем как и с открываемым файлом
                    for (int i = 0; i < tabControl2.TabPages.Count; i++)
                    {       
                        if (tabControl2.TabPages[i].Name == listBox1.SelectedItem.ToString())
                        {
                            controlName = !controlName;
                            tabControl2.SelectedIndex=i;
                            break;
                        }
                    }
                    if (controlName == false)
                    {
                        createWorkPanel(listBox1, searchMain);
                    }
                }
            }
        }

        private void создатьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderName = FileNameDialog(1);
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

            string fileName = FileNameDialog(2);
            FileInfo fnf = new FileInfo(searchMain + "\\" + fileName);
            if (!fnf.Exists)
            {
                if (fileName != "")
                {
                    StreamWriter a = new StreamWriter(File.Create(searchMain + "\\" + fileName));
                    // createWorkPanel(listBox1,namen, searchMain);
                    Console.WriteLine(searchMain + "\\" + fileName);
                    a.Close();
                }
            }

            loadDir(searchMain, listBox1);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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
            }
            catch(Exception noneElement) {

            }
            loadDir(searchMain, listBox1);

        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
           System.Drawing.Point pt = tabControl2.PointToClient(Cursor.Position);
            iPage = -1;
            for (int i = 0; i < tabControl2.TabCount; i++)
            {
                if (tabControl2.GetTabRect(i).Contains(pt))
                    iPage = i;
            }          
        }

        private void Закрыть_ВкладкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl2.TabPages.Remove(tabControl2.TabPages[iPage]);
        }

        private void Сохранить_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rull_RTB = (RichTextBox)tabControl2.TabPages[iPage].Controls[0];
            Console.WriteLine(searchMain + "\\" + rull_RTB.Name);
            rull_RTB.SaveFile(rull_RTB.Name);



        }
    }
}
