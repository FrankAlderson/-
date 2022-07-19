
namespace DIPLOM_V2
{
    partial class Studenti_ugltu_dobavit
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxFam = new System.Windows.Forms.TextBox();
            this.textBoxImya = new System.Windows.Forms.TextBox();
            this.textBoxOtch = new System.Windows.Forms.TextBox();
            this.dateTimePickerRozhd = new System.Windows.Forms.DateTimePicker();
            this.checkBoxFizOtkl = new System.Windows.Forms.CheckBox();
            this.textBoxRating = new System.Windows.Forms.TextBox();
            this.checkBoxDeti = new System.Windows.Forms.CheckBox();
            this.comboBoxSex = new System.Windows.Forms.ComboBox();
            this.полBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.diplomDataSet = new DIPLOM_V2.DiplomDataSet();
            this.полTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.ПолTableAdapter();
            this.comboBoxInst = new System.Windows.Forms.ComboBox();
            this.институтыУГЛТУBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.институты_УГЛТУTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.Институты_УГЛТУTableAdapter();
            this.comboBoxGrup = new System.Windows.Forms.ComboBox();
            this.группыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.группыTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.ГруппыTableAdapter();
            this.comboBoxKurs = new System.Windows.Forms.ComboBox();
            this.курсыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.курсыTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.КурсыTableAdapter();
            this.labelFam = new System.Windows.Forms.Label();
            this.labelImya = new System.Windows.Forms.Label();
            this.labelOtch = new System.Windows.Forms.Label();
            this.labelRozhd = new System.Windows.Forms.Label();
            this.labelRating = new System.Windows.Forms.Label();
            this.labelSex = new System.Windows.Forms.Label();
            this.labelInst = new System.Windows.Forms.Label();
            this.labelGrup = new System.Windows.Forms.Label();
            this.labelKurs = new System.Windows.Forms.Label();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.checkBoxSupr = new System.Windows.Forms.CheckBox();
            this.groupBoxSupr = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dateRozhSupr = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOtchSupr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxImyaSupr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFamSupr = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.fKСтудентыУГЛТУКурсыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.студенты_УГЛТУTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.Студенты_УГЛТУTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.полBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diplomDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.институтыУГЛТУBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.группыBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource)).BeginInit();
            this.groupBoxSupr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKСтудентыУГЛТУКурсыBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxFam
            // 
            this.textBoxFam.Location = new System.Drawing.Point(12, 30);
            this.textBoxFam.Name = "textBoxFam";
            this.textBoxFam.Size = new System.Drawing.Size(100, 20);
            this.textBoxFam.TabIndex = 0;
            // 
            // textBoxImya
            // 
            this.textBoxImya.Location = new System.Drawing.Point(152, 30);
            this.textBoxImya.Name = "textBoxImya";
            this.textBoxImya.Size = new System.Drawing.Size(100, 20);
            this.textBoxImya.TabIndex = 1;
            // 
            // textBoxOtch
            // 
            this.textBoxOtch.Location = new System.Drawing.Point(288, 30);
            this.textBoxOtch.Name = "textBoxOtch";
            this.textBoxOtch.Size = new System.Drawing.Size(100, 20);
            this.textBoxOtch.TabIndex = 2;
            // 
            // dateTimePickerRozhd
            // 
            this.dateTimePickerRozhd.CustomFormat = "";
            this.dateTimePickerRozhd.Location = new System.Drawing.Point(12, 102);
            this.dateTimePickerRozhd.Name = "dateTimePickerRozhd";
            this.dateTimePickerRozhd.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerRozhd.TabIndex = 3;
            // 
            // checkBoxFizOtkl
            // 
            this.checkBoxFizOtkl.AutoSize = true;
            this.checkBoxFizOtkl.Location = new System.Drawing.Point(12, 139);
            this.checkBoxFizOtkl.Name = "checkBoxFizOtkl";
            this.checkBoxFizOtkl.Size = new System.Drawing.Size(116, 17);
            this.checkBoxFizOtkl.TabIndex = 4;
            this.checkBoxFizOtkl.Text = "Физ. Отклонения";
            this.checkBoxFizOtkl.UseVisualStyleBackColor = true;
            // 
            // textBoxRating
            // 
            this.textBoxRating.Location = new System.Drawing.Point(288, 102);
            this.textBoxRating.Name = "textBoxRating";
            this.textBoxRating.Size = new System.Drawing.Size(100, 20);
            this.textBoxRating.TabIndex = 5;
            // 
            // checkBoxDeti
            // 
            this.checkBoxDeti.AutoSize = true;
            this.checkBoxDeti.Location = new System.Drawing.Point(12, 162);
            this.checkBoxDeti.Name = "checkBoxDeti";
            this.checkBoxDeti.Size = new System.Drawing.Size(52, 17);
            this.checkBoxDeti.TabIndex = 7;
            this.checkBoxDeti.Text = "Дети";
            this.checkBoxDeti.UseVisualStyleBackColor = true;
            // 
            // comboBoxSex
            // 
            this.comboBoxSex.DataSource = this.полBindingSource;
            this.comboBoxSex.DisplayMember = "Пол";
            this.comboBoxSex.FormattingEnabled = true;
            this.comboBoxSex.Location = new System.Drawing.Point(417, 29);
            this.comboBoxSex.Name = "comboBoxSex";
            this.comboBoxSex.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSex.TabIndex = 8;
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
            // полTableAdapter
            // 
            this.полTableAdapter.ClearBeforeFill = true;
            // 
            // comboBoxInst
            // 
            this.comboBoxInst.DataSource = this.институтыУГЛТУBindingSource;
            this.comboBoxInst.DisplayMember = "Институт";
            this.comboBoxInst.FormattingEnabled = true;
            this.comboBoxInst.Location = new System.Drawing.Point(12, 264);
            this.comboBoxInst.Name = "comboBoxInst";
            this.comboBoxInst.Size = new System.Drawing.Size(121, 21);
            this.comboBoxInst.TabIndex = 9;
            this.comboBoxInst.ValueMember = "Институт";
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
            this.comboBoxGrup.DataSource = this.группыBindingSource;
            this.comboBoxGrup.DisplayMember = "Группа";
            this.comboBoxGrup.FormattingEnabled = true;
            this.comboBoxGrup.Location = new System.Drawing.Point(266, 264);
            this.comboBoxGrup.Name = "comboBoxGrup";
            this.comboBoxGrup.Size = new System.Drawing.Size(121, 21);
            this.comboBoxGrup.TabIndex = 10;
            this.comboBoxGrup.ValueMember = "Группа";
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
            // comboBoxKurs
            // 
            this.comboBoxKurs.DataSource = this.курсыBindingSource;
            this.comboBoxKurs.DisplayMember = "Курс";
            this.comboBoxKurs.FormattingEnabled = true;
            this.comboBoxKurs.Location = new System.Drawing.Point(139, 264);
            this.comboBoxKurs.Name = "comboBoxKurs";
            this.comboBoxKurs.Size = new System.Drawing.Size(121, 21);
            this.comboBoxKurs.TabIndex = 11;
            // 
            // курсыBindingSource
            // 
            this.курсыBindingSource.DataMember = "Курсы";
            this.курсыBindingSource.DataSource = this.diplomDataSet;
            // 
            // курсыTableAdapter
            // 
            this.курсыTableAdapter.ClearBeforeFill = true;
            // 
            // labelFam
            // 
            this.labelFam.AutoSize = true;
            this.labelFam.Location = new System.Drawing.Point(9, 14);
            this.labelFam.Name = "labelFam";
            this.labelFam.Size = new System.Drawing.Size(56, 13);
            this.labelFam.TabIndex = 12;
            this.labelFam.Text = "Фамилия";
            // 
            // labelImya
            // 
            this.labelImya.AutoSize = true;
            this.labelImya.Location = new System.Drawing.Point(152, 13);
            this.labelImya.Name = "labelImya";
            this.labelImya.Size = new System.Drawing.Size(29, 13);
            this.labelImya.TabIndex = 13;
            this.labelImya.Text = "Имя";
            // 
            // labelOtch
            // 
            this.labelOtch.AutoSize = true;
            this.labelOtch.Location = new System.Drawing.Point(288, 11);
            this.labelOtch.Name = "labelOtch";
            this.labelOtch.Size = new System.Drawing.Size(54, 13);
            this.labelOtch.TabIndex = 14;
            this.labelOtch.Text = "Отчество";
            // 
            // labelRozhd
            // 
            this.labelRozhd.AutoSize = true;
            this.labelRozhd.Location = new System.Drawing.Point(12, 83);
            this.labelRozhd.Name = "labelRozhd";
            this.labelRozhd.Size = new System.Drawing.Size(86, 13);
            this.labelRozhd.TabIndex = 15;
            this.labelRozhd.Text = "Дата рождения";
            // 
            // labelRating
            // 
            this.labelRating.AutoSize = true;
            this.labelRating.Location = new System.Drawing.Point(285, 86);
            this.labelRating.Name = "labelRating";
            this.labelRating.Size = new System.Drawing.Size(48, 13);
            this.labelRating.TabIndex = 17;
            this.labelRating.Text = "Рейтинг";
            // 
            // labelSex
            // 
            this.labelSex.AutoSize = true;
            this.labelSex.Location = new System.Drawing.Point(417, 10);
            this.labelSex.Name = "labelSex";
            this.labelSex.Size = new System.Drawing.Size(27, 13);
            this.labelSex.TabIndex = 20;
            this.labelSex.Text = "Пол";
            // 
            // labelInst
            // 
            this.labelInst.AutoSize = true;
            this.labelInst.Location = new System.Drawing.Point(12, 245);
            this.labelInst.Name = "labelInst";
            this.labelInst.Size = new System.Drawing.Size(53, 13);
            this.labelInst.TabIndex = 21;
            this.labelInst.Text = "Институт";
            // 
            // labelGrup
            // 
            this.labelGrup.AutoSize = true;
            this.labelGrup.Location = new System.Drawing.Point(266, 245);
            this.labelGrup.Name = "labelGrup";
            this.labelGrup.Size = new System.Drawing.Size(42, 13);
            this.labelGrup.TabIndex = 22;
            this.labelGrup.Text = "Группа";
            // 
            // labelKurs
            // 
            this.labelKurs.AutoSize = true;
            this.labelKurs.Location = new System.Drawing.Point(139, 244);
            this.labelKurs.Name = "labelKurs";
            this.labelKurs.Size = new System.Drawing.Size(31, 13);
            this.labelKurs.TabIndex = 23;
            this.labelKurs.Text = "Курс";
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(12, 305);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(75, 23);
            this.buttonInsert.TabIndex = 24;
            this.buttonInsert.Text = "Добавить";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // checkBoxSupr
            // 
            this.checkBoxSupr.AutoSize = true;
            this.checkBoxSupr.Location = new System.Drawing.Point(12, 185);
            this.checkBoxSupr.Name = "checkBoxSupr";
            this.checkBoxSupr.Size = new System.Drawing.Size(66, 17);
            this.checkBoxSupr.TabIndex = 29;
            this.checkBoxSupr.Text = "Супруга";
            this.checkBoxSupr.UseVisualStyleBackColor = true;
            this.checkBoxSupr.CheckedChanged += new System.EventHandler(this.Suprugi_proverka);
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
            this.groupBoxSupr.Location = new System.Drawing.Point(186, 129);
            this.groupBoxSupr.Name = "groupBoxSupr";
            this.groupBoxSupr.Size = new System.Drawing.Size(459, 99);
            this.groupBoxSupr.TabIndex = 30;
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
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(545, 305);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(100, 23);
            this.button10.TabIndex = 31;
            this.button10.Text = "Закрыть";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // fKСтудентыУГЛТУКурсыBindingSource
            // 
            this.fKСтудентыУГЛТУКурсыBindingSource.DataMember = "FK_Студенты_УГЛТУ_Курсы";
            this.fKСтудентыУГЛТУКурсыBindingSource.DataSource = this.курсыBindingSource;
            // 
            // студенты_УГЛТУTableAdapter
            // 
            this.студенты_УГЛТУTableAdapter.ClearBeforeFill = true;
            // 
            // Studenti_ugltu_dobavit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 339);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.groupBoxSupr);
            this.Controls.Add(this.checkBoxSupr);
            this.Controls.Add(this.buttonInsert);
            this.Controls.Add(this.labelKurs);
            this.Controls.Add(this.labelGrup);
            this.Controls.Add(this.labelInst);
            this.Controls.Add(this.labelSex);
            this.Controls.Add(this.labelRating);
            this.Controls.Add(this.labelRozhd);
            this.Controls.Add(this.labelOtch);
            this.Controls.Add(this.labelImya);
            this.Controls.Add(this.labelFam);
            this.Controls.Add(this.comboBoxKurs);
            this.Controls.Add(this.comboBoxGrup);
            this.Controls.Add(this.comboBoxInst);
            this.Controls.Add(this.comboBoxSex);
            this.Controls.Add(this.checkBoxDeti);
            this.Controls.Add(this.textBoxRating);
            this.Controls.Add(this.checkBoxFizOtkl);
            this.Controls.Add(this.dateTimePickerRozhd);
            this.Controls.Add(this.textBoxOtch);
            this.Controls.Add(this.textBoxImya);
            this.Controls.Add(this.textBoxFam);
            this.Name = "Studenti_ugltu_dobavit";
            this.Text = "Студенты УГЛТУ";
            this.Load += new System.EventHandler(this.Studenti_ugltu_dobavit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.полBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diplomDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.институтыУГЛТУBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.группыBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource)).EndInit();
            this.groupBoxSupr.ResumeLayout(false);
            this.groupBoxSupr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKСтудентыУГЛТУКурсыBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFam;
        private System.Windows.Forms.TextBox textBoxImya;
        private System.Windows.Forms.TextBox textBoxOtch;
        private System.Windows.Forms.DateTimePicker dateTimePickerRozhd;
        private System.Windows.Forms.CheckBox checkBoxFizOtkl;
        private System.Windows.Forms.TextBox textBoxRating;
        private System.Windows.Forms.CheckBox checkBoxDeti;
        private System.Windows.Forms.ComboBox comboBoxSex;
        private DiplomDataSet diplomDataSet;
        private System.Windows.Forms.BindingSource полBindingSource;
        private DiplomDataSetTableAdapters.ПолTableAdapter полTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxInst;
        private System.Windows.Forms.BindingSource институтыУГЛТУBindingSource;
        private DiplomDataSetTableAdapters.Институты_УГЛТУTableAdapter институты_УГЛТУTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxGrup;
        private System.Windows.Forms.BindingSource группыBindingSource;
        private DiplomDataSetTableAdapters.ГруппыTableAdapter группыTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxKurs;
        private System.Windows.Forms.BindingSource курсыBindingSource;
        private DiplomDataSetTableAdapters.КурсыTableAdapter курсыTableAdapter;
        private System.Windows.Forms.Label labelFam;
        private System.Windows.Forms.Label labelImya;
        private System.Windows.Forms.Label labelOtch;
        private System.Windows.Forms.Label labelRozhd;
        private System.Windows.Forms.Label labelRating;
        private System.Windows.Forms.Label labelSex;
        private System.Windows.Forms.Label labelInst;
        private System.Windows.Forms.Label labelGrup;
        private System.Windows.Forms.Label labelKurs;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.CheckBox checkBoxSupr;
        private System.Windows.Forms.GroupBox groupBoxSupr;
        private System.Windows.Forms.DateTimePicker dateRozhSupr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOtchSupr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxImyaSupr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFamSupr;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.BindingSource fKСтудентыУГЛТУКурсыBindingSource;
        private DiplomDataSetTableAdapters.Студенты_УГЛТУTableAdapter студенты_УГЛТУTableAdapter;
    }
}

