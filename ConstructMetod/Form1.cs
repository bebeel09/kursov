﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Text;







namespace ConstructMetod
{
    public partial class Form1 : Form

    {
        string searchMain = "Catalog"; // путь чтения и загрузки файлов
        protected string homeSearch = "Catalog";
        protected int iPage = -1; //номер вкладки к которой обращаемся
        InstalledFontCollection font;//коллекция шрифтов предостовляемые системой



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

        //метод передающий все шрифты установленные на ПК, а так же задёт размеры
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

        //Метод для вызова окна в котором задаётся название папки или файла. Возврашает название
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

        public bool SaveOrNoDialog(string nameFile)
        {
            bool resultReturn = false;          
            SaveOrNo formSave = new SaveOrNo();
            DialogResult result = new DialogResult();
            if (nameFile.Length != 0)
            {
                string corName = onlyNameFile(nameFile);
                formSave.label1.Text = $"Сохранить файл: {corName}";
            }
            result = formSave.ShowDialog();

            if (result == DialogResult.OK)
            {
                resultReturn = true;
            }

            return resultReturn;
        }

        public string onlyNameFile(string searhName)
        {
            string only_NameFile = searhName;
            int lastSlech = searhName.LastIndexOf('\\');

            if (only_NameFile[only_NameFile.Length - 1] == '\\')
            {
                only_NameFile = only_NameFile.Remove(searchMain.Length - 1, 1);
            }

            only_NameFile = only_NameFile.Remove(0,lastSlech+1);

            return only_NameFile;
        }

        //правильно ли задано название?
        public bool correctName(string name)
        {
            bool transact = true;
            string correctName = name.Trim();
            int len_fileName = correctName.Length;
            char charT;

            for (int i = 0; i < len_fileName; i++)
            {
                charT = name[i];

                if (charT == '[' | charT == ']' | charT == '{' | charT == '}' | charT == '.' | charT == '\\' | charT == '/' | charT == '|')
                {

                    MessageBox.Show("Присутствуют недопустимые символы'[ ]{ }. \\/|'. Операция была отменена!");
                    transact = false;
                    break;
                }
            }
            return transact;
        }

        //загрузка директории со всеми файлами
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

        //создание панели вывода содержания файла
        public void createWorkPanel(ListBox mlistBox, string search)
        {
            //new Tab Page создаём вкладку
            TabPage tp = new TabPage();
            tp.Name = mlistBox.SelectedItem.ToString();
            tp.Text = mlistBox.SelectedItem.ToString();
            tp.Width = tabControl1.Width - 10;
            tp.Height = tabControl1.Height - 10;

            //new RichTextBox окно для ввода текста в вкладке
            RichTextBox RTB = new RichTextBox();
            RTB.Name = (Path.Combine(search, mlistBox.SelectedItem.ToString()));
            RTB.Font = new System.Drawing.Font("Times New Roman", 24);
            RTB.Dock = DockStyle.Fill;
            RTB.Click += new System.EventHandler(this.RTB_Click);
            RTB.ContextMenuStrip = contextMenuStrip3;
            RTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RTB_KeyDown);
            RTB.MouseEnter += new EventHandler(this.RTB_MouseEnter);
            RTB.MouseLeave += new EventHandler(this.RTB_MouseLeave);


            try
            {
                RTB.LoadFile(Path.Combine(search, mlistBox.SelectedItem.ToString()), RichTextBoxStreamType.RichText);
            }
            catch (Exception ert) { }

            //добавляем компонент richTextBox в tabPage
            tp.Controls.Add(RTB);
            tabControl2.TabPages.Add(tp);
            iPage = tabControl2.SelectedIndex;


        }

        private void RTB_MouseLeave(object sender, EventArgs e)
        {
            statusLabel1.Text = "";
        }

        private void RTB_MouseEnter(object sender, EventArgs e)
        {
            RichTextBox RTBthis = (RichTextBox)sender;
            statusLabel1.Text = RTBthis.Name;
        }

        //горячие клавишы
        private void RTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                сохранитьToolStripMenuItem_Click(sender, e);
            }
            loadDir(searchMain,listBox1);
        }

        //вывод заданного шрифта и размера выдиления
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

        //выполняется при запуске программы. Проверка корневого каталога. Загрузка директории
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dirExcept = new DirectoryInfo(homeSearch);
                dirExcept.GetDirectories();
            }
            catch (DirectoryNotFoundException noneDir)
            {
                statusLabel1.Text = "Отсутствие корневого каталога. Каталог создан";
                DirectoryInfo mainDir = new DirectoryInfo(homeSearch);
                mainDir.Create();
            }
            loadDir(searchMain, listBox1);
        }

        //выход из программы
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Открыть диалоговое меню для выбора файла находящийся за границе видемости
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

        //Выбор соответствующего элемента при нажатии правй клавиши мыши
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

        //Перемещение на дерикторию выше
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

        //Открытие файла или папки по двойному нажатию
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
                        statusLabel1.Text = $"Создана папка с именем: {folderName}";
                    }
                }
                else
                {
                    MessageBox.Show("Папка с таким именем уже существует");
                }
            }
            loadDir(searchMain, listBox1);
        }

        private void создатьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = FileNameDialog(2);
            /// входная строка имеет в себе ещё и расширение файла, и проверку не пройдёт
            /// отсекаем часть с расширением фаила, и полученную строку передаём в метод проверки корректности

            if (fileName.Length != 0)
            {
                bool control = correctName(fileName.Substring(0, fileName.Length - 4));

                if (control == true)
                {
                    FileInfo fnf = new FileInfo(searchMain + "\\" + fileName);
                    if (!fnf.Exists)
                    {
                        if (fileName != "")
                        {
                            StreamWriter a = new StreamWriter(File.Create(searchMain + "\\" + fileName));
                            a.Close();
                            statusLabel1.Text = $"Создан файл с именем: {fileName}";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Файл с таким именем уже существует.");
                    }
                }
            }
            loadDir(searchMain, listBox1);
        }

        //удаляет в зависимости был ли это файл или папка
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

        //проверяет над какой вкладкой находится курсор во время открытия контекстного меню 
        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Drawing.Point pt = tabControl2.PointToClient(Cursor.Position);
            iPage = -1;

            for (int i = 0; i < tabControl2.TabCount; i++)
            {
                if (tabControl2.GetTabRect(i).Contains(pt))
                    iPage = i;//передаём номер вкладки над которой находится курсор
            }
        }

        private void Закрыть_ВкладкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl2.TabCount != 0)
            {
                RichTextBox RTB = (RichTextBox)tabControl2.TabPages[iPage].Controls[0];
                bool saveFile = SaveOrNoDialog(RTB.Name);
                if (saveFile == true)
                {

                    tabControl2.TabPages.Remove(tabControl2.TabPages[iPage]);

                }
            }
        }

        //производит сохранение файла в действующей вкладке
        private void Сохранить_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rull_RTB = (RichTextBox)tabControl2.TabPages[iPage].Controls[0];
            rull_RTB.SaveFile(rull_RTB.Name);
            loadDir(searchMain,listBox1);
        }

        //Выравние произвдимое к выделенной области текста
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

        //вернуться в корневую дерикторию
        private void homeButton_Click(object sender, EventArgs e)
        {
            loadDir("ConstructMetod", listBox1);
            searchMain = "ConstructMetod";
        }

        //Открыть диалоговое окно задания шрифтов
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
                    rull_RTB.SelectionColor = fontDialog1.Color;
                }
            }
        }

        //синхронизирует выбор ширфта в главной форме с диалоговым окном шрифтов
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

        //открывает список коллекции при вводе текста 
        private void fontBox_TextUpdate(object sender, EventArgs e)
        {
            fontBox.DroppedDown = true;
            fontBox_SelectedIndexChanged(fontBox, e);
        }

        //синхронизация размера шрифта с окном выбора шрифтов
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

        //выкидвыает список коллекции при вводе размера шрифта в соответствующий элемент
        private void sizeFontBox_TextUpdate(object sender, EventArgs e)
        {
            sizeFontBox_SelectedIndexChanged(sizeFontBox, e);
        }

        //Открывает проводник путь которого указывает в директорию хранения всей файлов и папок
        private void открытьКорневуюПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start("explorer", homeSearch);

        }

        //Элементы контекстного меню в поле редактриования текста 
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox RTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
            RTB.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox RTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
            RTB.Copy();
            statusLabel1.Text = "Скопировано в буфер обмена";
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox RTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
            RTB.Paste();
        }

        private void задатьШрифтИРазмерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialogButton_Click(sender, e);
        }

        private void поЛевомуКраюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alignLeft_Click(sender, e);
        }

        private void поПравомуКраюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alignRight_Click(sender, e);
        }

        private void поЦентруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlignCenter_Click(sender, e);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rull_RTB = (RichTextBox)tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0];
            rull_RTB.SaveFile(rull_RTB.Name);
            statusLabel1.Text = $"Файл {rull_RTB.Name} успешно сохранён";
            loadDir(searchMain,listBox1);
        }

        //выводит окно с информацией о разработчике
        private void оРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creatorInf infRazrab = new creatorInf();
            infRazrab.Show();
        }

        //Перед закрытием формы предлагает сохранить все вкладки
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            int countTabPage = tabControl2.TabCount;
            if (countTabPage > 0)
            {
                bool saveFile = SaveOrNoDialog("");
                while (tabControl2.TabCount != 0)
                {
                    RichTextBox richTextBox = (RichTextBox)tabControl2.TabPages[tabControl2.TabCount - 1].Controls[0];

                    if (saveFile == true)
                    {
                        richTextBox.SaveFile(richTextBox.Name);
                    }

                    tabControl2.TabPages.Remove(tabControl2.TabPages[tabControl2.TabCount - 1]);
                }
            }
        }

        private void сохранитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = tabControl2.TabCount;
            while (i != 0)
            {
                RichTextBox richTextBox = (RichTextBox)tabControl2.TabPages[i - 1].Controls[0];
                richTextBox.SaveFile(richTextBox.Name);
                i--;
                statusLabel1.Text = "Все файлы успешно сохранены";
            }

            loadDir(searchMain,listBox1);
        }
    }
}

