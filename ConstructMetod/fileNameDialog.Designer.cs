namespace ConstructMetod
{
    partial class fileNameDialog
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
            this.components = new System.ComponentModel.Container();
            this.textPresent = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.expansion = new System.Windows.Forms.ComboBox();
            this.fileOrFolder = new System.Windows.Forms.TextBox();
            this.formatPreaent = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // textPresent
            // 
            this.textPresent.AutoSize = true;
            this.textPresent.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textPresent.Location = new System.Drawing.Point(12, 48);
            this.textPresent.Name = "textPresent";
            this.textPresent.Size = new System.Drawing.Size(254, 17);
            this.textPresent.TabIndex = 5;
            this.textPresent.Text = "Введите название создаваемого файла:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(337, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 33);
            this.button1.TabIndex = 4;
            this.button1.Text = "Готово";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(241, 168);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // expansion
            // 
            this.expansion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.expansion.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.expansion.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.expansion.FormattingEnabled = true;
            this.expansion.Items.AddRange(new object[] {
            "txt",
            "rtf"});
            this.expansion.Location = new System.Drawing.Point(337, 69);
            this.expansion.MaxDropDownItems = 4;
            this.expansion.Name = "expansion";
            this.expansion.Size = new System.Drawing.Size(86, 29);
            this.expansion.TabIndex = 2;
            // 
            // fileOrFolder
            // 
            this.fileOrFolder.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileOrFolder.Location = new System.Drawing.Point(15, 69);
            this.fileOrFolder.Name = "fileOrFolder";
            this.fileOrFolder.Size = new System.Drawing.Size(309, 29);
            this.fileOrFolder.TabIndex = 1;
            this.toolTip1.SetToolTip(this.fileOrFolder, "Запрещён ввод символов \'[ ] { } / | \\ . \'");
            this.fileOrFolder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fileOrFolder_KeyPress);
            this.fileOrFolder.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fileOrFolder_KeyUp);
            // 
            // formatPreaent
            // 
            this.formatPreaent.AutoSize = true;
            this.formatPreaent.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.formatPreaent.Location = new System.Drawing.Point(334, 48);
            this.formatPreaent.Name = "formatPreaent";
            this.formatPreaent.Size = new System.Drawing.Size(87, 17);
            this.formatPreaent.TabIndex = 6;
            this.formatPreaent.Text = "Расширение:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Не все поля были заполнены!";
            this.label2.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            // 
            // fileNameDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 213);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.formatPreaent);
            this.Controls.Add(this.fileOrFolder);
            this.Controls.Add(this.expansion);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textPresent);
            this.Name = "fileNameDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Dialog panel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label textPresent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox fileOrFolder;
        public System.Windows.Forms.ComboBox expansion;
        public System.Windows.Forms.Label formatPreaent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}