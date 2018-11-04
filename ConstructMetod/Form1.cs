using System;
using System.IO;
using System.Windows.Forms;

using System.Drawing.Text;



namespace ConstructMetod
{
    public partial class Form1 : Form

    {
        string searchMain = "ConstructMetod"; // путь чтения и загрузки файлов
        protected string homeSearch = "ConstructMetod";
        protected int iPage = -1; //номер вкладки к которой обращаемся
        InstalledFontCollection font;//для коллекции шрифтов предостовляемые системой


        public Form1()
        {
            InitializeComponent();
            listBox1.ContextMenuStrip = contextMenuStrip1;
            tabControl2.ContextMenuStrip = contextMenuStrip2;
            System.Drawing.Font font2 = new System.Drawing.Font("Times New Roman", 12);//начальный шрифт в диалоге шрифтов
            fontDialog1.Font = font2;
            fontAndSizePapam();
            fontBox.Text = fontDialog1.Font.Name.ToString();
            sizeFontBox.Text = fontDialog1.Font.Size.ToString();
        }

        public void fontAndSizePapam()
        {
            this.font = new InstalledFontCollection();
            int count = font.Families.Length;

            //Заполняем fontBox именами шрифтов
            for (int j = 0; j < count; ++j)
            {
                fontBox.Items.Add(font.Families[j].Name);
            }
            //размеры шрифтов
            for (int i = 1; i < 100; i++)
            {
                sizeFontBox.Items.Add(i);
            }

        }

        public String FileNameDialog(int numberOperation)
        {
            string name = "";
            string tochka = "";
            int kostil = 0;
            DialogResult dr = new DialogResult();
            fileNameDialog frm2 = new fileNameDialog();
            switch (numberOperation)
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
                if (kostil == 1)
                {

                    name = frm2.fileOrFolder.Text.ToString();
                    Console.WriteLine(name + "2");
                }
                else if (kostil == 2)
                {
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

        public bool correctName(string name)
        {
            bool transact = true;
            int len_fileName = name.Length;
            char charT;
            for (int i = 0; i < len_fileName; i++)
            {
                charT = name[i];
                if (charT == '[' || charT == ']' || charT == '{' || charT == '}' || charT == '.' || charT == '\\' || charT == '/' || charT == '|')
                {
                    MessageBox.Show("Присутствуют недопустимые символы'[ ]{ }. \\/|'. Операция была отменена!");
                    transact = false;
                    break;
                }
                Console.WriteLine(charT);
            }
            return transact;
        }

        public void loadDir(string path, ListBox onelistBox1)
        {

            onelistBox1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo crrDir in dirs)
            {

                onelistBox1.Items.Add(crrDir);

            }
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo crrfile in files)
            {
                onelistBox1.Items.Add(crrfile);
            }


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
            RTB.Font = new System.Drawing.Font("Times New Roman", 24);
            RTB.Click += new System.EventHandler(this.RTB_Click);



            RTB.Dock = DockStyle.Fill;
            try
            {
                RTB.LoadFile(Path.Combine(search, mlistBox.SelectedItem.ToString()), RichTextBoxStreamType.RichText);
            }
            catch (Exception ert) { }
            // RTB.Text = sr.ReadToEnd();

            tp.Controls.Add(RTB);
            tabControl2.TabPages.Add(tp);
            iPage = tabControl2.SelectedIndex;


        }

        private void RTB_Click(object sender, EventArgs e)
        {
            try
            {
                RichTextBox RTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
                fontBox.Text = RTB.SelectionFont.Name;
                sizeFontBox.Text = RTB.SelectionFont.Size.ToString();
            }
            catch (NullReferenceException error)
            {
                fontBox.Text = "";
                sizeFontBox.Text = "";
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
                            tabControl2.SelectedIndex = i;
                            iPage = i;
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
            bool transact = correctName(folderName);
            if (transact == true)
            {
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
            loadDir(searchMain, listBox1);
        }

        private void создатьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = FileNameDialog(2);
            try
            {
                FileInfo fnf = new FileInfo(searchMain + "\\" + fileName);
                if (!fnf.Exists)
                {
                    if (fileName != "")
                    {
                        StreamWriter a = new StreamWriter(File.Create(searchMain + "\\" + fileName));

                        a.Close();
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Присутствуют недопустимые символы'[ ]{ }. \\/|'. Операция была отменена!");

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
            catch (Exception noneElement)
            {

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
            try
            {
                tabControl2.TabPages.Remove(tabControl2.TabPages[iPage]);
            }
            catch (ArgumentOutOfRangeException ex) { }

        }

        private void Сохранить_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rull_RTB = (RichTextBox)tabControl2.TabPages[iPage].Controls[0];
            Console.WriteLine(searchMain + "\\" + rull_RTB.Name);
            rull_RTB.SaveFile(rull_RTB.Name);
        }

        private void AlignCenter_Click(object sender, EventArgs e)
        {
            if (tabControl2.TabPages.Count > 0)
            {
                iPage = tabControl2.SelectedIndex;
                RichTextBox alignRTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
                alignRTB.SelectionAlignment = HorizontalAlignment.Center;
            }
        }

        private void alignRight_Click(object sender, EventArgs e)
        {
            if (tabControl2.TabPages.Count > 0)
            {
                iPage = tabControl2.SelectedIndex;
                RichTextBox alignRTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
                alignRTB.SelectionAlignment = HorizontalAlignment.Right;
            }
        }

        private void alignLeft_Click(object sender, EventArgs e)
        {
            if (tabControl2.TabPages.Count > 0)
            {
                RichTextBox alignRTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
                alignRTB.SelectionAlignment = HorizontalAlignment.Left;
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            loadDir("ConstructMetod", listBox1);
            searchMain = "ConstructMetod";
        }

        private void fontDialogButton_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                fontBox.Text = fontDialog1.Font.Name;
                sizeFontBox.Text = fontDialog1.Font.SizeInPoints.ToString();
                if (tabControl2.TabPages.Count > 0)
                {
                    RichTextBox rull_RTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
                    rull_RTB.SelectionFont = fontDialog1.Font;
                }
            }
        }

        private void fontBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int sizeFont = int.Parse(sizeFontBox.Text);
                System.Drawing.Font font3 = new System.Drawing.Font(fontBox.Text, sizeFont);
                fontDialog1.Font = font3;
                RichTextBox RTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
                RTB.SelectionFont = fontDialog1.Font;

            }
            catch (Exception args) { }
        }

        private void fontBox_TextUpdate(object sender, EventArgs e)
        {
            fontBox.DroppedDown = true;
            fontBox_SelectedIndexChanged(fontBox, e);
        }

        private void sizeFontBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int sizeFont = int.Parse(sizeFontBox.Text);
                System.Drawing.Font font3 = new System.Drawing.Font(fontBox.Text, sizeFont);
                fontDialog1.Font = font3;
                RichTextBox RTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
                RTB.SelectionFont = fontDialog1.Font;
            }
            catch (Exception asdfg) { }
        }

        private void sizeFontBox_TextUpdate(object sender, EventArgs e)
        {
            sizeFontBox_SelectedIndexChanged(sizeFontBox, e);
        }

        private void открытьКорневуюПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start("explorer", homeSearch);

        }

    }
}
