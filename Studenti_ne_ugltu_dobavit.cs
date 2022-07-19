using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DIPLOM_V2
{
    public partial class Studenti_ne_ugltu_dobavit : Form
    {
        SqlConnection sqlConnection;
        public Studenti_ne_ugltu_dobavit()
        {
            InitializeComponent();
        }

        private void Suprugi_proverka(object sender, EventArgs e)
        {
            if (checkBoxSupr.Checked == true)
            {
                groupBoxSupr.Visible = true;
            }
            else
            {
                groupBoxSupr.Visible = false;
            }
        }

        private void Suprugi_F_M(object sender, EventArgs e)
        {
            if (comboBoxSex.SelectedIndex == 0)
            {
                checkBoxSupr.Text = "Супруга";
                groupBoxSupr.Text = "Супруга";
            }
            else if (comboBoxSex.SelectedIndex == 1)
            {
                checkBoxSupr.Text = "Супруг";
                groupBoxSupr.Text = "Супруг";
            }
        }
        private void Studenti_ne_ugltu_dobavit_Load(object sender, EventArgs e)
        {

            
            if (comboBoxSex.SelectedIndex == 0)
            {
                checkBoxSupr.Text = "Супруга";
                groupBoxSupr.Text = "Супруга";

            }
            else if (comboBoxSex.SelectedIndex == 1)
            {
                checkBoxSupr.Text = "Супруг";
                groupBoxSupr.Text = "Супруг";
            }

            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Группы_не_УГЛТУ". При необходимости она может быть перемещена или удалена.
            this.группы_не_УГЛТУTableAdapter.Fill(this.diplomDataSet.Группы_не_УГЛТУ);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Универститеты". При необходимости она может быть перемещена или удалена.
            this.универститетыTableAdapter.Fill(this.diplomDataSet.Универститеты);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Курсы". При необходимости она может быть перемещена или удалена.
            this.курсыTableAdapter.Fill(this.diplomDataSet.Курсы);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Пол". При необходимости она может быть перемещена или удалена.
            this.полTableAdapter.Fill(this.diplomDataSet.Пол);


        }

        private async void buttonInsert_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            int nomeruniv;
            int nomergrup;
            int nomerkurs;
            int sex;
            int supr;


            DateTime dateRozhd = dateTimePickerRozhd.Value.Date;
            DateTime dateRozhdSupr = dateRozhSupr.Value.Date;



            SqlCommand command = new SqlCommand("INSERT INTO [Студенты_не_УГЛТУ] (Фамилия, Имя, Отчество, [Дата рождения], Универститет, Группа, Курс, Пол, [Супруг/Супруга], Дети) VALUES (@Фамилия, @Имя, @Отчество, @Дата_рождения, @Универститет, @Группа, @Курс, @Пол, @Cупруги, @Дети)", sqlConnection);

            command.Parameters.AddWithValue("Фамилия", textBoxFam.Text);
            command.Parameters.AddWithValue("Имя", textBoxImya.Text);
            command.Parameters.AddWithValue("Отчество", textBoxOtch.Text);
            command.Parameters.AddWithValue("Дата_рождения", dateRozhd);

            nomeruniv = comboBoxInst.SelectedIndex + 1;
            command.Parameters.AddWithValue("Универститет", nomeruniv);

            nomergrup = comboBoxGrup.SelectedIndex + 1;
            command.Parameters.AddWithValue("Группа", nomergrup);

            nomerkurs = comboBoxKurs.SelectedIndex + 1;
            command.Parameters.AddWithValue("Курс", nomerkurs);

            sex = comboBoxSex.SelectedIndex + 1;
            command.Parameters.AddWithValue("Пол", sex);

           if (groupBoxSupr.Visible == false)
            {
                command.Parameters.AddWithValue("Cупруги", DBNull.Value);
            }
            else if (groupBoxSupr.Visible == true)
            {

                SqlDataReader sqlReader = null;

                SqlCommand command2 = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE ([Фамилия] = @ФамилияСупр) AND ([Имя] = @ИмяСупр) AND ([Отчество] = @ОтчествоСупр)", sqlConnection);

                command2.Parameters.AddWithValue("ФамилияСупр", textBoxFamSupr.Text);
                command2.Parameters.AddWithValue("ИмяСупр", textBoxImyaSupr.Text);
                command2.Parameters.AddWithValue("ОтчествоСупр", textBoxOtchSupr.Text);

                sqlReader = await command2.ExecuteReaderAsync();

                await sqlReader.ReadAsync();


                supr = Convert.ToInt32(sqlReader["Номер_супруга"]);
                command.Parameters.AddWithValue("Cупруги", supr);
                sqlReader.Close();
                await command2.ExecuteNonQueryAsync();
            }

            if (checkBoxDeti.Checked == false)
            {
                command.Parameters.AddWithValue("Дети", 0);
            }
            else
            {
                command.Parameters.AddWithValue("Дети", 1);
            }

            await command.ExecuteNonQueryAsync();

            MessageBox.Show("Данные успешно внесены", "Студенты", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            DialogResult result = MessageBox.Show("Очистить форму?", "Студенты", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                textBoxFam.Text = "";
                textBoxImya.Text = "";
                textBoxOtch.Text = "";
                textBoxImyaSupr.Text = "";
                textBoxFamSupr.Text = "";
                textBoxOtchSupr.Text = "";
                checkBoxDeti.Checked = false;
                checkBoxSupr.Checked = false;
                comboBoxGrup.SelectedIndex = 0;
                comboBoxInst.SelectedIndex = 0;
                comboBoxKurs.SelectedIndex = 0;
            }
            this.TopMost = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            DateTime dateRozhdSupr = dateRozhSupr.Value.Date;

            SqlCommand command = new SqlCommand("INSERT INTO [Супруги] (Фамилия, Имя, Отчество, [Дата рождения]) VALUES (@ФамилияСупр, @ИмяСупр, @ОтчествоСупр, @Дата_рожденияСупр)", sqlConnection);

            command.Parameters.AddWithValue("ФамилияСупр", textBoxFamSupr.Text);
            command.Parameters.AddWithValue("ИмяСупр", textBoxImyaSupr.Text);
            command.Parameters.AddWithValue("ОтчествоСупр", textBoxOtchSupr.Text);
            command.Parameters.AddWithValue("Дата_рожденияСупр", dateRozhdSupr);

            await command.ExecuteNonQueryAsync();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
