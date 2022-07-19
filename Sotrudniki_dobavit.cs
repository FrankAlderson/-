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
    public partial class Sotrudniki_dobavit : Form
    {
        SqlConnection sqlConnection;

        public Sotrudniki_dobavit()
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

        private void Sotrudniki_dobavit_Load(object sender, EventArgs e)
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
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Пол". При необходимости она может быть перемещена или удалена.
            this.полTableAdapter.Fill(this.diplomDataSet.Пол);

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

        private async void buttonInsert_Click(object sender, EventArgs e)
        {
                         
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            int supr;

            DateTime dateRozhd = dateTimePickerRozhd.Value.Date;
            DateTime dateRozhdSupr = dateRozhSupr.Value.Date;

            SqlCommand command = new SqlCommand("INSERT INTO [Сотрудники] (Фамилия, Имя, Отчество, [Дата рождения], [Супруг/Супруга], Дети) VALUES (@Фамилия, @Имя, @Отчество, @Дата_рождения, @Cупруги, @Дети)", sqlConnection);
           
            command.Parameters.AddWithValue("Фамилия", textBoxFam.Text);
            command.Parameters.AddWithValue("Имя", textBoxImya.Text);
            command.Parameters.AddWithValue("Отчество", textBoxOtch.Text);
            command.Parameters.AddWithValue("Дата_рождения", dateRozhd);

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
           
            DialogResult result = MessageBox.Show("Очистить форму?", "Студенты УГЛТУ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
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
            }
            this.TopMost = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
