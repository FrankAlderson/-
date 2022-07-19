
namespace DIPLOM_V2
{
    partial class Studenti_ne_ugltu_dobavit
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
            this.groupBoxSupr = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dateRozhSupr = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOtchSupr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxImyaSupr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFamSupr = new System.Windows.Forms.TextBox();
            this.checkBoxSupr = new System.Windows.Forms.CheckBox();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.labelKurs = new System.Windows.Forms.Label();
            this.labelGrup = new System.Windows.Forms.Label();
            this.labelInst = new System.Windows.Forms.Label();
            this.labelSex = new System.Windows.Forms.Label();
            this.labelRozhd = new System.Windows.Forms.Label();
            this.labelOtch = new System.Windows.Forms.Label();
            this.labelImya = new System.Windows.Forms.Label();
            this.labelFam = new System.Windows.Forms.Label();
            this.курсыTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.КурсыTableAdapter();
            this.comboBoxSex = new System.Windows.Forms.ComboBox();
            this.полBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.diplomDataSet = new DIPLOM_V2.DiplomDataSet();
            this.checkBoxDeti = new System.Windows.Forms.CheckBox();
            this.dateTimePickerRozhd = new System.Windows.Forms.DateTimePicker();
            this.textBoxOtch = new System.Windows.Forms.TextBox();
            this.textBoxImya = new System.Windows.Forms.TextBox();
            this.textBoxFam = new System.Windows.Forms.TextBox();
            this.полTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.ПолTableAdapter();
            this.comboBoxInst = new System.Windows.Forms.ComboBox();
            this.универститетыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.институтыУГЛТУBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.институты_УГЛТУTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.Институты_УГЛТУTableAdapter();
            this.comboBoxGrup = new System.Windows.Forms.ComboBox();
            this.группынеУГЛТУBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.группыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.группыTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.ГруппыTableAdapter();
            this.курсыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.универститетыTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.УниверститетыTableAdapter();
            this.группы_не_УГЛТУTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.Группы_не_УГЛТУTableAdapter();
            this.курсыBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxKurs = new System.Windows.Forms.ComboBox();
            this.курсыBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.button10 = new System.Windows.Forms.Button();
            this.groupBoxSupr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.полBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diplomDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.универститетыBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.институтыУГЛТУBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.группынеУГЛТУBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.группыBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxSupr
            // 
            this.groupBoxSupr.Controls.Add(this.button2);
            this.groupBoxSupr.Controls.Add(this.dateRozhSupr);
            this.groupBoxSupr.Controls.Add(this.label2);
            this.groupBoxSupr.Controls.Add(this.textBoxOtchSupr);
            this.groupBoxSupr.Controls.Add(this.label3);
            this.groupBoxSupr.Controls.Add(this.textBoxImyaSupr);
            this.groupBoxSupr.Controls.Add(this.label4);
            this.groupBoxSupr.Controls.Add(this.textBoxFamSupr);
            this.groupBoxSupr.Location = new System.Drawing.Point(189, 124);
            this.groupBoxSupr.Name = "groupBoxSupr";
            this.groupBoxSupr.Size = new System.Drawing.Size(459, 99);
            this.groupBoxSupr.TabIndex = 53;
            this.groupBoxSupr.TabStop = false;
            this.groupBoxSupr.Text = "Супруга";
            this.groupBoxSupr.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 23);
            this.button2.TabIndex = 35;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateRozhSupr
            // 
            this.dateRozhSupr.Location = new System.Drawing.Point(330, 46);
            this.dateRozhSupr.Name = "dateRozhSupr";
            this.dateRozhSupr.Size = new System.Drawing.Size(122, 20);
            this.dateRozhSupr.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Отчество";
            // 
            // textBoxOtchSupr
            // 
            this.textBoxOtchSupr.Location = new System.Drawing.Point(218, 47);
            this.textBoxOtchSupr.Name = "textBoxOtchSupr";
            this.textBoxOtchSupr.Size = new System.Drawing.Size(100, 20);
            this.textBoxOtchSupr.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Имя";
            // 
            // textBoxImyaSupr
            // 
            this.textBoxImyaSupr.Location = new System.Drawing.Point(112, 47);
            this.textBoxImyaSupr.Name = "textBoxImyaSupr";
            this.textBoxImyaSupr.Size = new System.Drawing.Size(100, 20);
            this.textBoxImyaSupr.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Фамилия";
            // 
            // textBoxFamSupr
            // 
            this.textBoxFamSupr.Location = new System.Drawing.Point(6, 47);
            this.textBoxFamSupr.Name = "textBoxFamSupr";
            this.textBoxFamSupr.Size = new System.Drawing.Size(100, 20);
            this.textBoxFamSupr.TabIndex = 0;
            // 
            // checkBoxSupr
            // 
            this.checkBoxSupr.AutoSize = true;
            this.checkBoxSupr.Location = new System.Drawing.Point(15, 171);
            this.checkBoxSupr.Name = "checkBoxSupr";
            this.checkBoxSupr.Size = new System.Drawing.Size(66, 17);
            this.checkBoxSupr.TabIndex = 52;
            this.checkBoxSupr.Text = "Супруга";
            this.checkBoxSupr.UseVisualStyleBackColor = true;
            this.checkBoxSupr.CheckedChanged += new System.EventHandler(this.Suprugi_proverka);
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(15, 300);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(75, 23);
            this.buttonInsert.TabIndex = 51;
            this.buttonInsert.Text = "Добавить";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // labelKurs
            // 
            this.labelKurs.AutoSize = true;
            this.labelKurs.Location = new System.Drawing.Point(144, 239);
            this.labelKurs.Name = "labelKurs";
            this.labelKurs.Size = new System.Drawing.Size(31, 13);
            this.labelKurs.TabIndex = 50;
            this.labelKurs.Text = "Курс";
            // 
            // labelGrup
            // 
            this.labelGrup.AutoSize = true;
            this.labelGrup.Location = new System.Drawing.Point(270, 240);
            this.labelGrup.Name = "labelGrup";
            this.labelGrup.Size = new System.Drawing.Size(42, 13);
            this.labelGrup.TabIndex = 49;
            this.labelGrup.Text = "Группа";
            // 
            // labelInst
            // 
            this.labelInst.AutoSize = true;
            this.labelInst.Location = new System.Drawing.Point(15, 240);
            this.labelInst.Name = "labelInst";
            this.labelInst.Size = new System.Drawing.Size(73, 13);
            this.labelInst.TabIndex = 48;
            this.labelInst.Text = "Университет";
            // 
            // labelSex
            // 
            this.labelSex.AutoSize = true;
            this.labelSex.Location = new System.Drawing.Point(420, 5);
            this.labelSex.Name = "labelSex";
            this.labelSex.Size = new System.Drawing.Size(27, 13);
            this.labelSex.TabIndex = 47;
            this.labelSex.Text = "Пол";
            // 
            // labelRozhd
            // 
            this.labelRozhd.AutoSize = true;
            this.labelRozhd.Location = new System.Drawing.Point(15, 78);
            this.labelRozhd.Name = "labelRozhd";
            this.labelRozhd.Size = new System.Drawing.Size(86, 13);
            this.labelRozhd.TabIndex = 45;
            this.labelRozhd.Text = "Дата рождения";
            // 
            // labelOtch
            // 
            this.labelOtch.AutoSize = true;
            this.labelOtch.Location = new System.Drawing.Point(291, 6);
            this.labelOtch.Name = "labelOtch";
            this.labelOtch.Size = new System.Drawing.Size(54, 13);
            this.labelOtch.TabIndex = 44;
            this.labelOtch.Text = "Отчество";
            // 
            // labelImya
            // 
            this.labelImya.AutoSize = true;
            this.labelImya.Location = new System.Drawing.Point(155, 8);
            this.labelImya.Name = "labelImya";
            this.labelImya.Size = new System.Drawing.Size(29, 13);
            this.labelImya.TabIndex = 43;
            this.labelImya.Text = "Имя";
            // 
            // labelFam
            // 
            this.labelFam.AutoSize = true;
            this.labelFam.Location = new System.Drawing.Point(12, 9);
            this.labelFam.Name = "labelFam";
            this.labelFam.Size = new System.Drawing.Size(56, 13);
            this.labelFam.TabIndex = 42;
            this.labelFam.Text = "Фамилия";
            // 
            // курсыTableAdapter
            // 
            this.курсыTableAdapter.ClearBeforeFill = true;
            // 
            // comboBoxSex
            // 
            this.comboBoxSex.DataSource = this.полBindingSource;
            this.comboBoxSex.DisplayMember = "Пол";
            this.comboBoxSex.FormattingEnabled = true;
            this.comboBoxSex.Location = new System.Drawing.Point(420, 24);
            this.comboBoxSex.Name = "comboBoxSex";
            this.comboBoxSex.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSex.TabIndex = 38;
            this.comboBoxSex.ValueMember = "Пол";
            this.comboBoxSex.SelectedIndexChanged += new System.EventHandler(this.Suprugi_F_M);
            // 
            // полBindingSource
            // 
            this.полBindingSource.DataMember = "Пол";
            this.полBindingSource.DataSource = this.diplomDataSet;
            // 
            // diplomDataSet
            // 
            this.diplomDataSet.DataSetName = "DiplomDataSet";
            this.diplomDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // checkBoxDeti
            // 
            this.checkBoxDeti.AutoSize = true;
            this.checkBoxDeti.Location = new System.Drawing.Point(15, 148);
            this.checkBoxDeti.Name = "checkBoxDeti";
            this.checkBoxDeti.Size = new System.Drawing.Size(52, 17);
            this.checkBoxDeti.TabIndex = 37;
            this.checkBoxDeti.Text = "Дети";
            this.checkBoxDeti.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerRozhd
            // 
            this.dateTimePickerRozhd.CustomFormat = "";
            this.dateTimePickerRozhd.Location = new System.Drawing.Point(15, 97);
            this.dateTimePickerRozhd.Name = "dateTimePickerRozhd";
            this.dateTimePickerRozhd.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerRozhd.TabIndex = 34;
            // 
            // textBoxOtch
            // 
            this.textBoxOtch.Location = new System.Drawing.Point(291, 25);
            this.textBoxOtch.Name = "textBoxOtch";
            this.textBoxOtch.Size = new System.Drawing.Size(100, 20);
            this.textBoxOtch.TabIndex = 33;
            // 
            // textBoxImya
            // 
            this.textBoxImya.Location = new System.Drawing.Point(155, 25);
            this.textBoxImya.Name = "textBoxImya";
            this.textBoxImya.Size = new System.Drawing.Size(100, 20);
            this.textBoxImya.TabIndex = 32;
            // 
            // textBoxFam
            // 
            this.textBoxFam.Location = new System.Drawing.Point(15, 25);
            this.textBoxFam.Name = "textBoxFam";
            this.textBoxFam.Size = new System.Drawing.Size(100, 20);
            this.textBoxFam.TabIndex = 31;
            // 
            // полTableAdapter
            // 
            this.полTableAdapter.ClearBeforeFill = true;
            // 
            // comboBoxInst
            // 
            this.comboBoxInst.DataSource = this.универститетыBindingSource;
            this.comboBoxInst.DisplayMember = "Универститет";
            this.comboBoxInst.FormattingEnabled = true;
            this.comboBoxInst.Location = new System.Drawing.Point(15, 259);
            this.comboBoxInst.Name = "comboBoxInst";
            this.comboBoxInst.Size = new System.Drawing.Size(121, 21);
            this.comboBoxInst.TabIndex = 39;
            this.comboBoxInst.ValueMember = "Институт";
            // 
            // универститетыBindingSource
            // 
            this.универститетыBindingSource.DataMember = "Универститеты";
            this.универститетыBindingSource.DataSource = this.diplomDataSet;
            // 
            // институтыУГЛТУBindingSource
            // 
            this.институтыУГЛТУBindingSource.DataMember = "Институты_УГЛТУ";
            this.институтыУГЛТУBindingSource.DataSource = this.diplomDataSet;
            // 
            // институты_УГЛТУTableAdapter
            // 
            this.институты_УГЛТУTableAdapter.ClearBeforeFill = true;
            // 
            // comboBoxGrup
            // 
            this.comboBoxGrup.DataSource = this.группынеУГЛТУBindingSource;
            this.comboBoxGrup.DisplayMember = "Группа";
            this.comboBoxGrup.FormattingEnabled = true;
            this.comboBoxGrup.Location = new System.Drawing.Point(270, 259);
            this.comboBoxGrup.Name = "comboBoxGrup";
            this.comboBoxGrup.Size = new System.Drawing.Size(121, 21);
            this.comboBoxGrup.TabIndex = 40;
            this.comboBoxGrup.ValueMember = "Группа";
            // 
            // группынеУГЛТУBindingSource
            // 
            this.группынеУГЛТУBindingSource.DataMember = "Группы_не_УГЛТУ";
            this.группынеУГЛТУBindingSource.DataSource = this.diplomDataSet;
            // 
            // группыBindingSource
            // 
            this.группыBindingSource.DataMember = "Группы";
            this.группыBindingSource.DataSource = this.diplomDataSet;
            // 
            // группыTableAdapter
            // 
            this.группыTableAdapter.ClearBeforeFill = true;
            // 
            // курсыBindingSource
            // 
            this.курсыBindingSource.DataMember = "Курсы";
            this.курсыBindingSource.DataSource = this.diplomDataSet;
            // 
            // универститетыTableAdapter
            // 
            this.универститетыTableAdapter.ClearBeforeFill = true;
            // 
            // группы_не_УГЛТУTableAdapter
            // 
            this.группы_не_УГЛТУTableAdapter.ClearBeforeFill = true;
            // 
            // курсыBindingSource1
            // 
            this.курсыBindingSource1.DataMember = "Курсы";
            this.курсыBindingSource1.DataSource = this.diplomDataSet;
            // 
            // comboBoxKurs
            // 
            this.comboBoxKurs.DataSource = this.курсыBindingSource2;
            this.comboBoxKurs.DisplayMember = "Курс";
            this.comboBoxKurs.FormattingEnabled = true;
            this.comboBoxKurs.Location = new System.Drawing.Point(142, 259);
            this.comboBoxKurs.Name = "comboBoxKurs";
            this.comboBoxKurs.Size = new System.Drawing.Size(121, 21);
            this.comboBoxKurs.TabIndex = 54;
            this.comboBoxKurs.ValueMember = "Курс";
            // 
            // курсыBindingSource2
            // 
            this.курсыBindingSource2.DataMember = "Курсы";
            this.курсыBindingSource2.DataSource = this.diplomDataSet;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(548, 300);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(100, 23);
            this.button10.TabIndex = 55;
            this.button10.Text = "Закрыть";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // Studenti_ne_ugltu_dobavit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 331);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.comboBoxKurs);
            this.Controls.Add(this.groupBoxSupr);
            this.Controls.Add(this.checkBoxSupr);
            this.Controls.Add(this.buttonInsert);
            this.Controls.Add(this.labelKurs);
            this.Controls.Add(this.labelGrup);
            this.Controls.Add(this.labelInst);
            this.Controls.Add(this.labelSex);
            this.Controls.Add(this.labelRozhd);
            this.Controls.Add(this.labelOtch);
            this.Controls.Add(this.labelImya);
            this.Controls.Add(this.labelFam);
            this.Controls.Add(this.comboBoxSex);
            this.Controls.Add(this.checkBoxDeti);
            this.Controls.Add(this.dateTimePickerRozhd);
            this.Controls.Add(this.textBoxOtch);
            this.Controls.Add(this.textBoxImya);
            this.Controls.Add(this.textBoxFam);
            this.Controls.Add(this.comboBoxInst);
            this.Controls.Add(this.comboBoxGrup);
            this.Name = "Studenti_ne_ugltu_dobavit";
            this.Text = "Студенты не УГЛТУ";
            this.Load += new System.EventHandler(this.Studenti_ne_ugltu_dobavit_Load);
            this.groupBoxSupr.ResumeLayout(false);
            this.groupBoxSupr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.полBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diplomDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.универститетыBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.институтыУГЛТУBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.группынеУГЛТУBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.группыBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSupr;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateRozhSupr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOtchSupr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxImyaSupr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFamSupr;
        private System.Windows.Forms.CheckBox checkBoxSupr;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.Label labelKurs;
        private System.Windows.Forms.Label labelGrup;
        private System.Windows.Forms.Label labelInst;
        private System.Windows.Forms.Label labelSex;
        private System.Windows.Forms.Label labelRozhd;
        private System.Windows.Forms.Label labelOtch;
        private System.Windows.Forms.Label labelImya;
        private System.Windows.Forms.Label labelFam;
        private DiplomDataSetTableAdapters.КурсыTableAdapter курсыTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxSex;
        private System.Windows.Forms.BindingSource полBindingSource;
        private DiplomDataSet diplomDataSet;
        private System.Windows.Forms.CheckBox checkBoxDeti;
        private System.Windows.Forms.DateTimePicker dateTimePickerRozhd;
        private System.Windows.Forms.TextBox textBoxOtch;
        private System.Windows.Forms.TextBox textBoxImya;
        private System.Windows.Forms.TextBox textBoxFam;
        private DiplomDataSetTableAdapters.ПолTableAdapter полTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxInst;
        private System.Windows.Forms.BindingSource институтыУГЛТУBindingSource;
        private DiplomDataSetTableAdapters.Институты_УГЛТУTableAdapter институты_УГЛТУTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxGrup;
        private System.Windows.Forms.BindingSource группыBindingSource;
        private DiplomDataSetTableAdapters.ГруппыTableAdapter группыTableAdapter;
        private System.Windows.Forms.BindingSource курсыBindingSource;
        private System.Windows.Forms.BindingSource универститетыBindingSource;
        private DiplomDataSetTableAdapters.УниверститетыTableAdapter универститетыTableAdapter;
        private System.Windows.Forms.BindingSource группынеУГЛТУBindingSource;
        private DiplomDataSetTableAdapters.Группы_не_УГЛТУTableAdapter группы_не_УГЛТУTableAdapter;
        private System.Windows.Forms.BindingSource курсыBindingSource1;
        private System.Windows.Forms.ComboBox comboBoxKurs;
        private System.Windows.Forms.BindingSource курсыBindingSource2;
        private System.Windows.Forms.Button button10;
    }
}