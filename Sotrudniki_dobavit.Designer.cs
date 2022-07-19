
namespace DIPLOM_V2
{
    partial class Sotrudniki_dobavit
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
            this.labelSex = new System.Windows.Forms.Label();
            this.labelRozhd = new System.Windows.Forms.Label();
            this.labelOtch = new System.Windows.Forms.Label();
            this.labelImya = new System.Windows.Forms.Label();
            this.labelFam = new System.Windows.Forms.Label();
            this.курсыTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.КурсыTableAdapter();
            this.comboBoxSex = new System.Windows.Forms.ComboBox();
            this.полBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.diplomDataSet = new DIPLOM_V2.DiplomDataSet();
            this.dateTimePickerRozhd = new System.Windows.Forms.DateTimePicker();
            this.textBoxOtch = new System.Windows.Forms.TextBox();
            this.textBoxImya = new System.Windows.Forms.TextBox();
            this.textBoxFam = new System.Windows.Forms.TextBox();
            this.полTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.ПолTableAdapter();
            this.институтыУГЛТУBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.институты_УГЛТУTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.Институты_УГЛТУTableAdapter();
            this.группыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.группыTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.ГруппыTableAdapter();
            this.курсыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.checkBoxDeti = new System.Windows.Forms.CheckBox();
            this.button10 = new System.Windows.Forms.Button();
            this.groupBoxSupr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.полBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diplomDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.институтыУГЛТУBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.группыBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource)).BeginInit();
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
            this.groupBoxSupr.Location = new System.Drawing.Point(15, 147);
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
            this.checkBoxSupr.Location = new System.Drawing.Point(15, 124);
            this.checkBoxSupr.Name = "checkBoxSupr";
            this.checkBoxSupr.Size = new System.Drawing.Size(66, 17);
            this.checkBoxSupr.TabIndex = 52;
            this.checkBoxSupr.Text = "Супруга";
            this.checkBoxSupr.UseVisualStyleBackColor = true;
            this.checkBoxSupr.CheckedChanged += new System.EventHandler(this.Suprugi_proverka);
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(15, 252);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(75, 23);
            this.buttonInsert.TabIndex = 51;
            this.buttonInsert.Text = "Добавить";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // labelSex
            // 
            this.labelSex.AutoSize = true;
            this.labelSex.Location = new System.Drawing.Point(169, 78);
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
            this.comboBoxSex.Location = new System.Drawing.Point(169, 97);
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
            // институтыУГЛТУBindingSource
            // 
            this.институтыУГЛТУBindingSource.DataMember = "Институты_УГЛТУ";
            this.институтыУГЛТУBindingSource.DataSource = this.diplomDataSet;
            // 
            // институты_УГЛТУTableAdapter
            // 
            this.институты_УГЛТУTableAdapter.ClearBeforeFill = true;
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
            // checkBoxDeti
            // 
            this.checkBoxDeti.AutoSize = true;
            this.checkBoxDeti.Location = new System.Drawing.Point(87, 124);
            this.checkBoxDeti.Name = "checkBoxDeti";
            this.checkBoxDeti.Size = new System.Drawing.Size(52, 17);
            this.checkBoxDeti.TabIndex = 54;
            this.checkBoxDeti.Text = "Дети";
            this.checkBoxDeti.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(374, 252);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(100, 23);
            this.button10.TabIndex = 55;
            this.button10.Text = "Закрыть";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // Sotrudniki_dobavit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 283);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.checkBoxDeti);
            this.Controls.Add(this.groupBoxSupr);
            this.Controls.Add(this.checkBoxSupr);
            this.Controls.Add(this.buttonInsert);
            this.Controls.Add(this.labelSex);
            this.Controls.Add(this.labelRozhd);
            this.Controls.Add(this.labelOtch);
            this.Controls.Add(this.labelImya);
            this.Controls.Add(this.labelFam);
            this.Controls.Add(this.comboBoxSex);
            this.Controls.Add(this.dateTimePickerRozhd);
            this.Controls.Add(this.textBoxOtch);
            this.Controls.Add(this.textBoxImya);
            this.Controls.Add(this.textBoxFam);
            this.Name = "Sotrudniki_dobavit";
            this.Text = "Сотрудники";
            this.Load += new System.EventHandler(this.Sotrudniki_dobavit_Load);
            this.groupBoxSupr.ResumeLayout(false);
            this.groupBoxSupr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.полBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diplomDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.институтыУГЛТУBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.группыBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource)).EndInit();
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
        private System.Windows.Forms.Label labelSex;
        private System.Windows.Forms.Label labelRozhd;
        private System.Windows.Forms.Label labelOtch;
        private System.Windows.Forms.Label labelImya;
        private System.Windows.Forms.Label labelFam;
        private DiplomDataSetTableAdapters.КурсыTableAdapter курсыTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxSex;
        private System.Windows.Forms.BindingSource полBindingSource;
        private DiplomDataSet diplomDataSet;
        private System.Windows.Forms.DateTimePicker dateTimePickerRozhd;
        private System.Windows.Forms.TextBox textBoxOtch;
        private System.Windows.Forms.TextBox textBoxImya;
        private System.Windows.Forms.TextBox textBoxFam;
        private DiplomDataSetTableAdapters.ПолTableAdapter полTableAdapter;
        private System.Windows.Forms.BindingSource институтыУГЛТУBindingSource;
        private DiplomDataSetTableAdapters.Институты_УГЛТУTableAdapter институты_УГЛТУTableAdapter;
        private System.Windows.Forms.BindingSource группыBindingSource;
        private DiplomDataSetTableAdapters.ГруппыTableAdapter группыTableAdapter;
        private System.Windows.Forms.BindingSource курсыBindingSource;
        private System.Windows.Forms.CheckBox checkBoxDeti;
        private System.Windows.Forms.Button button10;
    }
}