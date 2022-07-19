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
    public partial class Izmenit_sotrudniki : Form
    {
        SqlConnection sqlConnection;
        public Izmenit_sotrudniki()
        {
            InitializeComponent();
        }

        async void naitinomer()
        {
            textBoxFamSupr.Text = "";
            textBoxImyaSupr.Text = "";
            textBoxOtchSupr.Text = "";
            textBoxNomerSupr.Text = "";

            Boolean deti;

            string daterozhd;

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;

            //Вывод фамилии
            SqlCommand command = new SqlCommand("SELECT Фамилия FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
            command.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
            sqlReader = await command.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxFam.Text = Convert.ToString(sqlReader["Фамилия"]);
            sqlReader.Close();
            await command.ExecuteNonQueryAsync();

            //Вывод имени
            SqlCommand command2 = new SqlCommand("SELECT Имя FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
            command2.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
            sqlReader = await command2.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxImya.Text = Convert.ToString(sqlReader["Имя"]);
            sqlReader.Close();
            await command2.ExecuteNonQueryAsync();

            //Вывод отчества
            SqlCommand command3 = new SqlCommand("SELECT Отчество FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
            command3.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
            sqlReader = await command3.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxOtch.Text = Convert.ToString(sqlReader["Отчество"]);
            sqlReader.Close();
            await command3.ExecuteNonQueryAsync();

            //Вывод даты рождения
            SqlCommand command4 = new SqlCommand("SELECT [Дата рождения] FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
            command4.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
            sqlReader = await command4.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            daterozhd = Convert.ToString(sqlReader["Дата рождения"]);
            textBoxDataRozhd.Text = daterozhd.Remove(daterozhd.Length - 7);
            sqlReader.Close();
            await command4.ExecuteNonQueryAsync();

            //Вывод детей
            SqlCommand command10 = new SqlCommand("SELECT Дети FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
            command10.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
            sqlReader = await command10.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            deti = Convert.ToBoolean(sqlReader["Дети"]);
            if (deti == false)
                checkBoxDeti.Checked = false;
            else if
                (deti == true)
                checkBoxDeti.Checked = true;
            sqlReader.Close();
            await command10.ExecuteNonQueryAsync();

            //Вывод фамилии супр
            try
            {
                SqlCommand command12 = new SqlCommand("SELECT Фамилия FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Сотрудники Where Номер_сотрудника = @Номер_сотрудника)", sqlConnection);
                command12.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
                sqlReader = await command12.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBoxFamSupr.Text = Convert.ToString(sqlReader["Фамилия"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }

            //Вывод имени супр
            try
            {
                SqlCommand command13 = new SqlCommand("SELECT Имя FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Сотрудники Where Номер_сотрудника = @Номер_сотрудника)", sqlConnection);
                command13.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
                sqlReader = await command13.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBoxImyaSupr.Text = Convert.ToString(sqlReader["Имя"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }

            //Вывод отчества супр
            try
            {
                SqlCommand command14 = new SqlCommand("SELECT Отчество FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Сотрудники Where Номер_сотрудника = @Номер_сотрудника)", sqlConnection);
                command14.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
                sqlReader = await command14.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBoxOtchSupr.Text = Convert.ToString(sqlReader["Отчество"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }

            //Вывод номера супр
            try
            {
                SqlCommand command15 = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Сотрудники Where Номер_сотрудника = @Номер_сотрудника)", sqlConnection);
                command15.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
                sqlReader = await command15.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBoxNomerSupr.Text = Convert.ToString(sqlReader["Номер_супруга"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }

            // дата рожд супр

            try
            {
                SqlCommand command16 = new SqlCommand("SELECT [Дата рождения] FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Сотрудники Where [Номер_сотрудника] = @Номер_сотрудника)", sqlConnection);
                command16.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
                sqlReader = await command16.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    dateTimePicker1.Value = Convert.ToDateTime(sqlReader["Дата рождения"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBoxNomer.Text != "")
            {
                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                string nomer;

                await sqlConnection.OpenAsync();

                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand("SELECT Номер_сотрудника FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
                command.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);

                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    await sqlReader.ReadAsync();
                    nomer = Convert.ToString(sqlReader["Номер_сотрудника"]);
                    sqlReader.Close();
                    naitinomer();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand("UPDATE Сотрудники SET [Супруг/Супруга] = @Supr WHERE Номер_сотрудника=@Nomer", sqlConnection);

            command.Parameters.AddWithValue("Nomer", textBoxNomer.Text);
            command.Parameters.AddWithValue("Supr", DBNull.Value);


            MessageBox.Show("Вы успешно разведены!", "Поздравляем", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            await command.ExecuteNonQueryAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите произвести изменения?", "Изменение", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
           MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("Обратного пути нет", "Изменение", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (result2 == DialogResult.Yes)
                {
                    string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                    sqlConnection = new SqlConnection(connectionString);

                    await sqlConnection.OpenAsync();

                    SqlCommand command = new SqlCommand("UPDATE Сотрудники SET Фамилия = @Fam, Имя = @Imya, Отчество = @Otch, [Дети] = @Deti WHERE Номер_сотрудника=@Nomer", sqlConnection);
                    command.Parameters.AddWithValue("Nomer", textBoxNomer.Text);
                    command.Parameters.AddWithValue("Fam", textBoxFam.Text);
                    command.Parameters.AddWithValue("Imya", textBoxImya.Text);
                    command.Parameters.AddWithValue("Otch", textBoxOtch.Text);

                    if (checkBoxDeti.Checked == false)
                    {
                        command.Parameters.AddWithValue("Deti", 0);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("Deti", 1);
                    }

                    await command.ExecuteNonQueryAsync();

                    SqlCommand command5 = new SqlCommand("UPDATE Супруги SET Фамилия = @FamS, Имя = @ImyaS, Отчество = @OtchS WHERE Номер_супруга=@NomerS", sqlConnection);
                    command5.Parameters.AddWithValue("NomerS", textBoxNomerSupr.Text);
                    command5.Parameters.AddWithValue("FamS", textBoxFamSupr.Text);
                    command5.Parameters.AddWithValue("ImyaS", textBoxImyaSupr.Text);
                    command5.Parameters.AddWithValue("OtchS", textBoxOtchSupr.Text);
                    await command5.ExecuteNonQueryAsync();


                    MessageBox.Show("Данные успешно изменены", "Сотрудники", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    DialogResult result3 = MessageBox.Show("Очистить форму?", "Студенты УГЛТУ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                       MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    if (result == DialogResult.Yes)
                    {
                        textBoxNomer.Text = "";
                        textBoxFam.Text = "";
                        textBoxImya.Text = "";
                        textBoxOtch.Text = "";
                        textBoxNomerSupr.Text = "";
                        textBoxImyaSupr.Text = "";
                        textBoxFamSupr.Text = "";
                        textBoxOtchSupr.Text = "";
                        checkBoxDeti.Checked = false;
                        textBoxDataRozhd.Text = "";
                    }
                    this.TopMost = true;
                }
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            int nomerSupr = 0;

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlReader = null;

            await sqlConnection.OpenAsync();

            DateTime dateRozhdSupr = dateTimePicker3.Value.Date;

            SqlCommand command = new SqlCommand("INSERT INTO [Супруги] (Фамилия, Имя, Отчество, [Дата рождения]) VALUES (@ФамилияСупр, @ИмяСупр, @ОтчествоСупр, @Дата_рожденияСупр)", sqlConnection);

            command.Parameters.AddWithValue("ФамилияСупр", textBox5.Text);
            command.Parameters.AddWithValue("ИмяСупр", textBox6.Text);
            command.Parameters.AddWithValue("ОтчествоСупр", textBox7.Text);
            command.Parameters.AddWithValue("Дата_рожденияСупр", dateRozhdSupr);

            await command.ExecuteNonQueryAsync();



            try
            {
                SqlCommand command3 = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE Фамилия = @ФамилияСупр AND Имя = @ИмяСупр AND Отчество=@ОтчествоСупр", sqlConnection);
                command3.Parameters.AddWithValue("ФамилияСупр", textBox5.Text);
                command3.Parameters.AddWithValue("ИмяСупр", textBox6.Text);
                command3.Parameters.AddWithValue("ОтчествоСупр", textBox7.Text);


                sqlReader = await command3.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    nomerSupr = Convert.ToInt32(sqlReader["Номер_супруга"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }


            SqlCommand command2 = new SqlCommand("UPDATE Сотрудники SET [Супруг/Супруга] = @Supr WHERE Номер_сотрудника=@Nomer", sqlConnection);

            command2.Parameters.AddWithValue("Nomer", textBoxNomer.Text);
            command2.Parameters.AddWithValue("Supr", nomerSupr);


            MessageBox.Show("Вы создали ячейку общества!", "Поздравляем", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            await command2.ExecuteNonQueryAsync();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
