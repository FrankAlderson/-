
namespace DIPLOM_V2
{
    partial class Izmenit_studenti_ne_ugltu
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
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.univeriDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.univeriBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.diplomDataSet = new DIPLOM_V2.DiplomDataSet();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.kursiDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kursiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gruppiNeUgltuDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gruppiNeUgltuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxDataRozhd = new System.Windows.Forms.TextBox();
            this.checkBoxDeti = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxNomerSupr = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxOtchSupr = new System.Windows.Forms.TextBox();
            this.textBoxImyaSupr = new System.Windows.Forms.TextBox();
            this.textBoxFamSupr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxKurs = new System.Windows.Forms.TextBox();
            this.textBoxGrup = new System.Windows.Forms.TextBox();
            this.textBoxInst = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSex = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOtch = new System.Windows.Forms.TextBox();
            this.textBoxImya = new System.Windows.Forms.TextBox();
            this.textBoxFam = new System.Windows.Forms.TextBox();
            this.textBoxNomer = new System.Windows.Forms.TextBox();
            this.univeriTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.UniveriTableAdapter();
            this.tableAdapterManager = new DIPLOM_V2.DiplomDataSetTableAdapters.TableAdapterManager();
            this.kursiTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.KursiTableAdapter();
            this.gruppiNeUgltuTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.GruppiNeUgltuTableAdapter();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBoxSupr = new System.Windows.Forms.GroupBox();
            this.button10 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.univeriDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.univeriBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diplomDataSet)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kursiDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kursiBindingSource)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gruppiNeUgltuDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gruppiNeUgltuBindingSource)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBoxSupr.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 537);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 67;
            this.button2.Text = "Изменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(416, 89);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(306, 321);
            this.tabControl1.TabIndex = 66;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.univeriDataGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(298, 295);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Список  универститетов";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // univeriDataGridView
            // 
            this.univeriDataGridView.AutoGenerateColumns = false;
            this.univeriDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.univeriDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.univeriDataGridView.DataSource = this.univeriBindingSource;
            this.univeriDataGridView.Location = new System.Drawing.Point(6, 6);
            this.univeriDataGridView.Name = "univeriDataGridView";
            this.univeriDataGridView.Size = new System.Drawing.Size(289, 283);
            this.univeriDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Универститет";
            this.dataGridViewTextBoxColumn1.HeaderText = "Универститет";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // univeriBindingSource
            // 
            this.univeriBindingSource.DataMember = "Univeri";
            this.univeriBindingSource.DataSource = this.diplomDataSet;
            // 
            // diplomDataSet
            // 
            this.diplomDataSet.DataSetName = "DiplomDataSet";
            this.diplomDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.kursiDataGridView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(298, 295);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Список курсов";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // kursiDataGridView
            // 
            this.kursiDataGridView.AutoGenerateColumns = false;
            this.kursiDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kursiDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2});
            this.kursiDataGridView.DataSource = this.kursiBindingSource;
            this.kursiDataGridView.Location = new System.Drawing.Point(6, 6);
            this.kursiDataGridView.Name = "kursiDataGridView";
            this.kursiDataGridView.Size = new System.Drawing.Size(289, 286);
            this.kursiDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Курс";
            this.dataGridViewTextBoxColumn2.HeaderText = "Курс";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // kursiBindingSource
            // 
            this.kursiBindingSource.DataMember = "Kursi";
            this.kursiBindingSource.DataSource = this.diplomDataSet;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.gruppiNeUgltuDataGridView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(298, 295);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Список групп";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gruppiNeUgltuDataGridView
            // 
            this.gruppiNeUgltuDataGridView.AutoGenerateColumns = false;
            this.gruppiNeUgltuDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gruppiNeUgltuDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3});
            this.gruppiNeUgltuDataGridView.DataSource = this.gruppiNeUgltuBindingSource;
            this.gruppiNeUgltuDataGridView.Location = new System.Drawing.Point(6, 6);
            this.gruppiNeUgltuDataGridView.Name = "gruppiNeUgltuDataGridView";
            this.gruppiNeUgltuDataGridView.Size = new System.Drawing.Size(289, 283);
            this.gruppiNeUgltuDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Группа";
            this.dataGridViewTextBoxColumn3.HeaderText = "Группа";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // gruppiNeUgltuBindingSource
            // 
            this.gruppiNeUgltuBindingSource.DataMember = "GruppiNeUgltu";
            this.gruppiNeUgltuBindingSource.DataSource = this.diplomDataSet;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 65;
            this.label13.Text = "Дата рождения";
            // 
            // textBoxDataRozhd
            // 
            this.textBoxDataRozhd.Location = new System.Drawing.Point(104, 136);
            this.textBoxDataRozhd.Name = "textBoxDataRozhd";
            this.textBoxDataRozhd.Size = new System.Drawing.Size(100, 20);
            this.textBoxDataRozhd.TabIndex = 64;
            // 
            // checkBoxDeti
            // 
            this.checkBoxDeti.AutoSize = true;
            this.checkBoxDeti.Location = new System.Drawing.Point(12, 226);
            this.checkBoxDeti.Name = "checkBoxDeti";
            this.checkBoxDeti.Size = new System.Drawing.Size(52, 17);
            this.checkBoxDeti.TabIndex = 61;
            this.checkBoxDeti.Text = "Дети";
            this.checkBoxDeti.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 189);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(215, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "Развод";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 13);
            this.label14.TabIndex = 19;
            this.label14.Text = "Номер";
            // 
            // textBoxNomerSupr
            // 
            this.textBoxNomerSupr.Location = new System.Drawing.Point(103, 23);
            this.textBoxNomerSupr.Name = "textBoxNomerSupr";
            this.textBoxNomerSupr.Size = new System.Drawing.Size(125, 20);
            this.textBoxNomerSupr.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Отчество";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 89);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Имя";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Фамилия";
            // 
            // textBoxOtchSupr
            // 
            this.textBoxOtchSupr.Location = new System.Drawing.Point(104, 123);
            this.textBoxOtchSupr.Name = "textBoxOtchSupr";
            this.textBoxOtchSupr.Size = new System.Drawing.Size(125, 20);
            this.textBoxOtchSupr.TabIndex = 2;
            // 
            // textBoxImyaSupr
            // 
            this.textBoxImyaSupr.Location = new System.Drawing.Point(103, 89);
            this.textBoxImyaSupr.Name = "textBoxImyaSupr";
            this.textBoxImyaSupr.Size = new System.Drawing.Size(125, 20);
            this.textBoxImyaSupr.TabIndex = 1;
            // 
            // textBoxFamSupr
            // 
            this.textBoxFamSupr.Location = new System.Drawing.Point(103, 58);
            this.textBoxFamSupr.Name = "textBoxFamSupr";
            this.textBoxFamSupr.Size = new System.Drawing.Size(125, 20);
            this.textBoxFamSupr.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(119, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 58;
            this.label8.Text = "Курс";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(225, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 57;
            this.label7.Text = "Группа";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "Институт";
            // 
            // textBoxKurs
            // 
            this.textBoxKurs.Location = new System.Drawing.Point(119, 191);
            this.textBoxKurs.Name = "textBoxKurs";
            this.textBoxKurs.Size = new System.Drawing.Size(100, 20);
            this.textBoxKurs.TabIndex = 55;
            // 
            // textBoxGrup
            // 
            this.textBoxGrup.Location = new System.Drawing.Point(225, 191);
            this.textBoxGrup.Name = "textBoxGrup";
            this.textBoxGrup.Size = new System.Drawing.Size(100, 20);
            this.textBoxGrup.TabIndex = 54;
            // 
            // textBoxInst
            // 
            this.textBoxInst.Location = new System.Drawing.Point(12, 191);
            this.textBoxInst.Name = "textBoxInst";
            this.textBoxInst.Size = new System.Drawing.Size(100, 20);
            this.textBoxInst.TabIndex = 53;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(216, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Пол";
            // 
            // textBoxSex
            // 
            this.textBoxSex.Location = new System.Drawing.Point(249, 136);
            this.textBoxSex.Name = "textBoxSex";
            this.textBoxSex.Size = new System.Drawing.Size(100, 20);
            this.textBoxSex.TabIndex = 51;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(147, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 50;
            this.button1.Text = "Поиск";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(283, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Отчество";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 48;
            this.label3.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Фамилия";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Номер студента";
            // 
            // textBoxOtch
            // 
            this.textBoxOtch.Location = new System.Drawing.Point(283, 89);
            this.textBoxOtch.Name = "textBoxOtch";
            this.textBoxOtch.Size = new System.Drawing.Size(100, 20);
            this.textBoxOtch.TabIndex = 45;
            // 
            // textBoxImya
            // 
            this.textBoxImya.Location = new System.Drawing.Point(147, 89);
            this.textBoxImya.Name = "textBoxImya";
            this.textBoxImya.Size = new System.Drawing.Size(100, 20);
            this.textBoxImya.TabIndex = 44;
            // 
            // textBoxFam
            // 
            this.textBoxFam.Location = new System.Drawing.Point(12, 90);
            this.textBoxFam.Name = "textBoxFam";
            this.textBoxFam.Size = new System.Drawing.Size(100, 20);
            this.textBoxFam.TabIndex = 43;
            // 
            // textBoxNomer
            // 
            this.textBoxNomer.Location = new System.Drawing.Point(12, 29);
            this.textBoxNomer.Name = "textBoxNomer";
            this.textBoxNomer.Size = new System.Drawing.Size(100, 20);
            this.textBoxNomer.TabIndex = 42;
            // 
            // univeriTableAdapter
            // 
            this.univeriTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.LogPassTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = DIPLOM_V2.DiplomDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.Группы_не_УГЛТУTableAdapter = null;
            this.tableAdapterManager.ГруппыTableAdapter = null;
            this.tableAdapterManager.Институты_УГЛТУTableAdapter = null;
            this.tableAdapterManager.КурсыTableAdapter = null;
            this.tableAdapterManager.ОбщежитияTableAdapter = null;
            this.tableAdapterManager.ПолTableAdapter = null;
            this.tableAdapterManager.СотрудникиTableAdapter = null;
            this.tableAdapterManager.Студенты_не_УГЛТУTableAdapter = null;
            this.tableAdapterManager.Студенты_УГЛТУTableAdapter = null;
            this.tableAdapterManager.СупругиTableAdapter = null;
            this.tableAdapterManager.УниверститетыTableAdapter = null;
            // 
            // kursiTableAdapter
            // 
            this.kursiTableAdapter.ClearBeforeFill = true;
            // 
            // gruppiNeUgltuTableAdapter
            // 
            this.gruppiNeUgltuTableAdapter.ClearBeforeFill = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(6, 19);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(262, 256);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.button3);
            this.tabPage4.Controls.Add(this.textBoxNomerSupr);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.dateTimePicker1);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.textBoxFamSupr);
            this.tabPage4.Controls.Add(this.textBoxOtchSupr);
            this.tabPage4.Controls.Add(this.textBoxImyaSupr);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(254, 230);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Развод";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(103, 154);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(125, 20);
            this.dateTimePicker1.TabIndex = 43;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 154);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 13);
            this.label15.TabIndex = 42;
            this.label15.Text = "Дата рождения";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dateTimePicker3);
            this.tabPage5.Controls.Add(this.textBox5);
            this.tabPage5.Controls.Add(this.label19);
            this.tabPage5.Controls.Add(this.textBox6);
            this.tabPage5.Controls.Add(this.textBox7);
            this.tabPage5.Controls.Add(this.button5);
            this.tabPage5.Controls.Add(this.label20);
            this.tabPage5.Controls.Add(this.label21);
            this.tabPage5.Controls.Add(this.label22);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(254, 230);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Брак";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Location = new System.Drawing.Point(112, 139);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(125, 20);
            this.dateTimePicker3.TabIndex = 54;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(112, 36);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(125, 20);
            this.textBox5.TabIndex = 44;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(19, 139);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(86, 13);
            this.label19.TabIndex = 53;
            this.label19.Text = "Дата рождения";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(112, 67);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(125, 20);
            this.textBox6.TabIndex = 45;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(113, 101);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(125, 20);
            this.textBox7.TabIndex = 46;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(22, 170);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(215, 23);
            this.button5.TabIndex = 52;
            this.button5.Text = "Брак";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(19, 39);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 13);
            this.label20.TabIndex = 47;
            this.label20.Text = "Фамилия";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(19, 70);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 13);
            this.label21.TabIndex = 48;
            this.label21.Text = "Имя";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(19, 104);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 13);
            this.label22.TabIndex = 49;
            this.label22.Text = "Отчество";
            // 
            // groupBoxSupr
            // 
            this.groupBoxSupr.Controls.Add(this.tabControl2);
            this.groupBoxSupr.Location = new System.Drawing.Point(12, 249);
            this.groupBoxSupr.Name = "groupBoxSupr";
            this.groupBoxSupr.Size = new System.Drawing.Size(275, 283);
            this.groupBoxSupr.TabIndex = 59;
            this.groupBoxSupr.TabStop = false;
            this.groupBoxSupr.Text = "Супруга";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(615, 537);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(100, 23);
            this.button10.TabIndex = 68;
            this.button10.Text = "Закрыть";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // Izmenit_studenti_ne_ugltu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 571);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBoxDataRozhd);
            this.Controls.Add(this.checkBoxDeti);
            this.Controls.Add(this.groupBoxSupr);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxKurs);
            this.Controls.Add(this.textBoxGrup);
            this.Controls.Add(this.textBoxInst);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxSex);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOtch);
            this.Controls.Add(this.textBoxImya);
            this.Controls.Add(this.textBoxFam);
            this.Controls.Add(this.textBoxNomer);
            this.Name = "Izmenit_studenti_ne_ugltu";
            this.Text = "Изменить студенты не УГЛТУ";
            this.Load += new System.EventHandler(this.Izmenit_studenti_ne_ugltu_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.univeriDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.univeriBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diplomDataSet)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kursiDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kursiBindingSource)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gruppiNeUgltuDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gruppiNeUgltuBindingSource)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBoxSupr.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxDataRozhd;
        private System.Windows.Forms.CheckBox checkBoxDeti;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxNomerSupr;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxOtchSupr;
        private System.Windows.Forms.TextBox textBoxImyaSupr;
        private System.Windows.Forms.TextBox textBoxFamSupr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxKurs;
        private System.Windows.Forms.TextBox textBoxGrup;
        private System.Windows.Forms.TextBox textBoxInst;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSex;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOtch;
        private System.Windows.Forms.TextBox textBoxImya;
        private System.Windows.Forms.TextBox textBoxFam;
        private System.Windows.Forms.TextBox textBoxNomer;
        private DiplomDataSet diplomDataSet;
        private System.Windows.Forms.BindingSource univeriBindingSource;
        private DiplomDataSetTableAdapters.UniveriTableAdapter univeriTableAdapter;
        private DiplomDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView univeriDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource kursiBindingSource;
        private DiplomDataSetTableAdapters.KursiTableAdapter kursiTableAdapter;
        private System.Windows.Forms.DataGridView kursiDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.BindingSource gruppiNeUgltuBindingSource;
        private DiplomDataSetTableAdapters.GruppiNeUgltuTableAdapter gruppiNeUgltuTableAdapter;
        private System.Windows.Forms.DataGridView gruppiNeUgltuDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBoxSupr;
        private System.Windows.Forms.Button button10;
    }
}