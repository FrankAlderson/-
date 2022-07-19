
namespace DIPLOM_V2
{
    partial class Dolzhniki
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
            this.button3 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.курсыTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.КурсыTableAdapter();
            this.полBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.diplomDataSet = new DIPLOM_V2.DiplomDataSet();
            this.универститетыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.курсыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.универститетыTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.УниверститетыTableAdapter();
            this.институты_УГЛТУTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.Институты_УГЛТУTableAdapter();
            this.su_s_suprTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.Su_s_suprTableAdapter();
            this.su_s_suprBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableAdapterManager = new DIPLOM_V2.DiplomDataSetTableAdapters.TableAdapterManager();
            this.sU_bez_suprTableAdapter = new DIPLOM_V2.DiplomDataSetTableAdapters.SU_bez_suprTableAdapter();
            this.sU_bez_suprBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label26 = new System.Windows.Forms.Label();
            this.институтыУГЛТУBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label27 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.полBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diplomDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.универститетыBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.su_s_suprBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sU_bez_suprBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.институтыУГЛТУBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1490, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 87;
            this.button3.Text = "Обновить";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView6);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.dataGridView5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(836, 472);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Сотрудники";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView6
            // 
            this.dataGridView6.AllowUserToAddRows = false;
            this.dataGridView6.AllowUserToDeleteRows = false;
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView6.Location = new System.Drawing.Point(0, 251);
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.ReadOnly = true;
            this.dataGridView6.Size = new System.Drawing.Size(836, 221);
            this.dataGridView6.TabIndex = 89;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(377, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "Сотрудники которым осталось проживать в общежитии меньше 30 дней";
            // 
            // dataGridView5
            // 
            this.dataGridView5.AllowUserToAddRows = false;
            this.dataGridView5.AllowUserToDeleteRows = false;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView5.Location = new System.Drawing.Point(0, 0);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.ReadOnly = true;
            this.dataGridView5.Size = new System.Drawing.Size(836, 207);
            this.dataGridView5.TabIndex = 0;
            // 
            // курсыTableAdapter
            // 
            this.курсыTableAdapter.ClearBeforeFill = true;
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
            // универститетыBindingSource
            // 
            this.универститетыBindingSource.DataMember = "Универститеты";
            this.универститетыBindingSource.DataSource = this.diplomDataSet;
            // 
            // курсыBindingSource
            // 
            this.курсыBindingSource.DataMember = "Курсы";
            this.курсыBindingSource.DataSource = this.diplomDataSet;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1487, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 64;
            this.button1.Text = "Обновить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(1183, 310);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(99, 13);
            this.label25.TabIndex = 59;
            this.label25.Text = "Отчество  супруга";
            // 
            // универститетыTableAdapter
            // 
            this.универститетыTableAdapter.ClearBeforeFill = true;
            // 
            // институты_УГЛТУTableAdapter
            // 
            this.институты_УГЛТУTableAdapter.ClearBeforeFill = true;
            // 
            // su_s_suprTableAdapter
            // 
            this.su_s_suprTableAdapter.ClearBeforeFill = true;
            // 
            // su_s_suprBindingSource
            // 
            this.su_s_suprBindingSource.DataMember = "Su_s_supr";
            this.su_s_suprBindingSource.DataSource = this.diplomDataSet;
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
            // sU_bez_suprTableAdapter
            // 
            this.sU_bez_suprTableAdapter.ClearBeforeFill = true;
            // 
            // sU_bez_suprBindingSource
            // 
            this.sU_bez_suprBindingSource.DataMember = "SU_bez_supr";
            this.sU_bez_suprBindingSource.DataSource = this.diplomDataSet;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(1051, 310);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(74, 13);
            this.label26.TabIndex = 57;
            this.label26.Text = "Имя  супруга";
            // 
            // институтыУГЛТУBindingSource
            // 
            this.институтыУГЛТУBindingSource.DataMember = "Институты_УГЛТУ";
            this.институтыУГЛТУBindingSource.DataSource = this.diplomDataSet;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(922, 310);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(98, 13);
            this.label27.TabIndex = 55;
            this.label27.Text = "Фамилия супруга";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(844, 498);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(836, 472);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Студенты УГЛТУ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Студенты которым осталось проживать в общежитии меньше 30 дней";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView2.Location = new System.Drawing.Point(3, 244);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(830, 225);
            this.dataGridView2.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(830, 201);
            this.dataGridView1.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView4);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label25);
            this.tabPage2.Controls.Add(this.label26);
            this.tabPage2.Controls.Add(this.label27);
            this.tabPage2.Controls.Add(this.dataGridView3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(836, 472);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Студенты университетов";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView4.Location = new System.Drawing.Point(3, 257);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.Size = new System.Drawing.Size(830, 212);
            this.dataGridView4.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Студенты которым осталось проживать в общежитии меньше 30 дней";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView3.Location = new System.Drawing.Point(3, 3);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.Size = new System.Drawing.Size(830, 218);
            this.dataGridView3.TabIndex = 35;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(777, 537);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Закрыть";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Dolzhniki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 572);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tabControl1);
            this.Name = "Dolzhniki";
            this.Text = "Дней до выселения";
            this.Load += new System.EventHandler(this.Dolzhniki_Load);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.полBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diplomDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.универститетыBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.курсыBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.su_s_suprBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sU_bez_suprBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.институтыУГЛТУBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView5;
        private DiplomDataSetTableAdapters.КурсыTableAdapter курсыTableAdapter;
        private System.Windows.Forms.BindingSource полBindingSource;
        private DiplomDataSet diplomDataSet;
        private System.Windows.Forms.BindingSource универститетыBindingSource;
        private System.Windows.Forms.BindingSource курсыBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label25;
        private DiplomDataSetTableAdapters.УниверститетыTableAdapter универститетыTableAdapter;
        private DiplomDataSetTableAdapters.Институты_УГЛТУTableAdapter институты_УГЛТУTableAdapter;
        private DiplomDataSetTableAdapters.Su_s_suprTableAdapter su_s_suprTableAdapter;
        private System.Windows.Forms.BindingSource su_s_suprBindingSource;
        private DiplomDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private DiplomDataSetTableAdapters.SU_bez_suprTableAdapter sU_bez_suprTableAdapter;
        private System.Windows.Forms.BindingSource sU_bez_suprBindingSource;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.BindingSource институтыУГЛТУBindingSource;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView6;
        private System.Windows.Forms.Label label3;
    }
}