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
    public partial class Ydalenie : Form
    {
        SqlConnection sqlConnection;
        public Ydalenie()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        async void naitinomer()
        {
            textBoxFamSupr.Text = "";
            textBoxImyaSupr.Text = "";
            textBoxOtchSupr.Text = "";

            Boolean fizotkl;
            Boolean deti;

            string daterozhd;
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;

            //Вывод фамилии
            SqlCommand command = new SqlCommand("SELECT Фамилия FROM Студенты_УГЛТУ WHERE [Номер_студента] = @Номер_студента", sqlConnection);
            command.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxFam.Text = Convert.ToString(sqlReader["Фамилия"]);
            sqlReader.Close();
            await command.ExecuteNonQueryAsync();

            //Вывод имени
            SqlCommand command2 = new SqlCommand("SELECT Имя FROM Студенты_УГЛТУ WHERE [Номер_студента] = @Номер_студента", sqlConnection);
            command2.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command2.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxImya.Text = Convert.ToString(sqlReader["Имя"]);
            sqlReader.Close();
            await command2.ExecuteNonQueryAsync();

            //Вывод отчества
            SqlCommand command3 = new SqlCommand("SELECT Отчество FROM Студенты_УГЛТУ WHERE [Номер_студента] = @Номер_студента", sqlConnection);
            command3.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command3.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxOtch.Text = Convert.ToString(sqlReader["Отчество"]);
            sqlReader.Close();
            await command3.ExecuteNonQueryAsync();

            //Вывод даты рождения
            SqlCommand command4 = new SqlCommand("SELECT [Дата рождения] FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
            command4.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command4.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            daterozhd = Convert.ToString(sqlReader["Дата рождения"]);
            textBoxDataRozhd.Text = daterozhd.Remove(daterozhd.Length - 7);
            sqlReader.Close();
            await command4.ExecuteNonQueryAsync();

            //Вывод пола
            try
            {
                SqlCommand command5 = new SqlCommand("SELECT Пол FROM Пол WHERE Номер_пола = (Select Пол From Студенты_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
                command5.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                sqlReader = await command5.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBoxSex.Text = Convert.ToString(sqlReader["Пол"]);
                    if (textBoxSex.Text == "Мужчина")
                    {
                        groupBoxSupr.Text = "Супруга";
                    }
                    else if (textBoxSex.Text == "Женщина")
                    {
                        groupBoxSupr.Text = "Супруг";
                    }
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

            //Вывод института
            SqlCommand command6 = new SqlCommand("SELECT Институт FROM Институты_УГЛТУ WHERE Номер_института = (Select Институт From Студенты_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
            command6.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command6.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxInst.Text = Convert.ToString(sqlReader["Институт"]);
            sqlReader.Close();
            await command6.ExecuteNonQueryAsync();

            //Вывод группы
            SqlCommand command7 = new SqlCommand("SELECT Группа FROM Группы WHERE Номер_группы = (Select Группа From Студенты_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
            command7.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command7.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxGrup.Text = Convert.ToString(sqlReader["Группа"]);
            sqlReader.Close();
            await command7.ExecuteNonQueryAsync();

            //Вывод курса
            SqlCommand command8 = new SqlCommand("SELECT Курс FROM Курсы WHERE Номер_курса = (Select Курс From Студенты_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
            command8.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command8.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxKurs.Text = Convert.ToString(sqlReader["Курс"]);
            sqlReader.Close();
            await command8.ExecuteNonQueryAsync();

            //Вывод отклонений
            SqlCommand command9 = new SqlCommand("SELECT [Физ.Отклонения] FROM Студенты_УГЛТУ WHERE [Номер_студента] = @Номер_студента", sqlConnection);
            command9.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command9.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            fizotkl = Convert.ToBoolean(sqlReader["Физ.Отклонения"]);
            if (fizotkl == false)
                checkBoxFizOtkl.Checked = false;
            else if
                (fizotkl == true)
                checkBoxFizOtkl.Checked = true;
            sqlReader.Close();
            await command9.ExecuteNonQueryAsync();

            //Вывод детей
            SqlCommand command10 = new SqlCommand("SELECT Дети FROM Студенты_УГЛТУ WHERE [Номер_студента] = @Номер_студента", sqlConnection);
            command10.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
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

            //Вывод рейтинга
            SqlCommand command11 = new SqlCommand("SELECT Рейтинг FROM Студенты_УГЛТУ WHERE [Номер_студента] = @Номер_студента", sqlConnection);
            command11.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command11.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxRating.Text = Convert.ToString(sqlReader["Рейтинг"]);
            sqlReader.Close();
            await command11.ExecuteNonQueryAsync();

            //Вывод фамилии супр
            try
            {
                SqlCommand command12 = new SqlCommand("SELECT Фамилия FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
                command12.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                sqlReader = await command12.ExecuteReaderAsync();
                ListViewItem item = null;
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
                SqlCommand command13 = new SqlCommand("SELECT Имя FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
                command13.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                sqlReader = await command13.ExecuteReaderAsync();
                ListViewItem item = null;
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
                SqlCommand command14 = new SqlCommand("SELECT Отчество FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
                command14.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                sqlReader = await command14.ExecuteReaderAsync();
                ListViewItem item = null;
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
                SqlCommand command14 = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
                command14.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                sqlReader = await command14.ExecuteReaderAsync();
                ListViewItem item = null;
                while (sqlReader.Read())
                {
                    label20.Text = Convert.ToString(sqlReader["Номер_супруга"]);
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

                SqlCommand command = new SqlCommand("SELECT [Номер_студента] FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
                command.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);

                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    await sqlReader.ReadAsync();
                    nomer = Convert.ToString(sqlReader["Номер_студента"]);
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

        private async void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите произвести удаление?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("Обратного пути нет", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (result2 == DialogResult.Yes)
                {
                    SqlCommand command2 = new SqlCommand("DELETE FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
                    command2.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                    await command2.ExecuteNonQueryAsync();

                    MessageBox.Show("Данные успешно изменены", "Студенты", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    DialogResult result3 = MessageBox.Show("Очистить форму?", "Студенты УГЛТУ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    if (result3 == DialogResult.Yes)
                    {
                        textBoxNomer.Text = "";
                        textBoxFam.Text = "";
                        textBoxImya.Text = "";
                        textBoxOtch.Text = "";
                        textBoxImyaSupr.Text = "";
                        textBoxFamSupr.Text = "";
                        textBoxOtchSupr.Text = "";
                        checkBoxDeti.Checked = false;
                        textBoxInst.Text = "";
                        textBoxKurs.Text = "";
                        textBoxGrup.Text = "";
                        textBoxSex.Text = "";
                        checkBoxFizOtkl.Checked = false;
                        textBoxRating.Text = "0";
                    }
                }
            }
        }

        async void naitinomer2()
        {
            textBox4.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";

            Boolean deti;

            string daterozhd;
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;

            //Вывод фамилии
            SqlCommand command = new SqlCommand("SELECT Фамилия FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
            command.Parameters.AddWithValue("Номер_студента", textBox12.Text);
            sqlReader = await command.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox11.Text = Convert.ToString(sqlReader["Фамилия"]);
            sqlReader.Close();
            await command.ExecuteNonQueryAsync();

            //Вывод имени
            SqlCommand command2 = new SqlCommand("SELECT Имя FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
            command2.Parameters.AddWithValue("Номер_студента", textBox12.Text);
            sqlReader = await command2.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox10.Text = Convert.ToString(sqlReader["Имя"]);
            sqlReader.Close();
            await command2.ExecuteNonQueryAsync();

            //Вывод отчества
            SqlCommand command3 = new SqlCommand("SELECT Отчество FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
            command3.Parameters.AddWithValue("Номер_студента", textBox12.Text);
            sqlReader = await command3.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox9.Text = Convert.ToString(sqlReader["Отчество"]);
            sqlReader.Close();
            await command3.ExecuteNonQueryAsync();

            //Вывод даты рождения
            SqlCommand command4 = new SqlCommand("SELECT [Дата рождения] FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
            command4.Parameters.AddWithValue("Номер_студента", textBox12.Text);
            sqlReader = await command4.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            daterozhd = Convert.ToString(sqlReader["Дата рождения"]);
            textBox1.Text = daterozhd.Remove(daterozhd.Length - 7);
            sqlReader.Close();
            await command4.ExecuteNonQueryAsync();

            //Вывод пола
            SqlCommand command5 = new SqlCommand("SELECT Пол FROM Пол WHERE Номер_пола = (Select Пол From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
            command5.Parameters.AddWithValue("Номер_студента", textBox12.Text);
            sqlReader = await command5.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox8.Text = Convert.ToString(sqlReader["Пол"]);
            sqlReader.Close();
            await command5.ExecuteNonQueryAsync();
            if (textBox8.Text == "Мужчина")
            {
                groupBox2.Text = "Супруга";
            }
            else if (textBox8.Text == "Женщина")
            {
                groupBox2.Text = "Супруг";
            }

            //Вывод университета
            SqlCommand command6 = new SqlCommand("SELECT Универститет FROM Универститеты WHERE Номер_универститета = (Select Универститет From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
            command6.Parameters.AddWithValue("Номер_студента", textBox12.Text);
            sqlReader = await command6.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox7.Text = Convert.ToString(sqlReader["Универститет"]);
            sqlReader.Close();
            await command6.ExecuteNonQueryAsync();

            //Вывод группы
            SqlCommand command7 = new SqlCommand("SELECT Группа FROM Группы_не_УГЛТУ WHERE Номер_группы = (Select Группа From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
            command7.Parameters.AddWithValue("Номер_студента", textBox12.Text);
            sqlReader = await command7.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox6.Text = Convert.ToString(sqlReader["Группа"]);
            sqlReader.Close();
            await command7.ExecuteNonQueryAsync();

            //Вывод курса
            SqlCommand command8 = new SqlCommand("SELECT Курс FROM Курсы WHERE Номер_курса = (Select Курс From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
            command8.Parameters.AddWithValue("Номер_студента", textBox12.Text);
            sqlReader = await command8.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox5.Text = Convert.ToString(sqlReader["Курс"]);
            sqlReader.Close();
            await command8.ExecuteNonQueryAsync();

            //Вывод детей
            SqlCommand command10 = new SqlCommand("SELECT Дети FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
            command10.Parameters.AddWithValue("Номер_студента", textBox12.Text);
            sqlReader = await command10.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            deti = Convert.ToBoolean(sqlReader["Дети"]);
            if (deti == false)
                checkBox1.Checked = false;
            else if
                (deti == true)
                checkBox1.Checked = true;
            sqlReader.Close();
            await command10.ExecuteNonQueryAsync();

            //Вывод фамилии супр
            try
            {
                SqlCommand command12 = new SqlCommand("SELECT Фамилия FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
                command12.Parameters.AddWithValue("Номер_студента", textBox12.Text);
                sqlReader = await command12.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBox4.Text = Convert.ToString(sqlReader["Фамилия"]);
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
                SqlCommand command13 = new SqlCommand("SELECT Имя FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
                command13.Parameters.AddWithValue("Номер_студента", textBox12.Text);
                sqlReader = await command13.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBox3.Text = Convert.ToString(sqlReader["Имя"]);
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
                SqlCommand command14 = new SqlCommand("SELECT Отчество FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
                command14.Parameters.AddWithValue("Номер_студента", textBox12.Text);
                sqlReader = await command14.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBox2.Text = Convert.ToString(sqlReader["Отчество"]);
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
                SqlCommand command14 = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
                command14.Parameters.AddWithValue("Номер_студента", textBox12.Text);
                sqlReader = await command14.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    label14.Text = Convert.ToString(sqlReader["Номер_супруга"]);
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

        private async void button3_Click(object sender, EventArgs e)
        {
            if (textBoxNomer.Text != "")
            {
                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                string nomer;

                await sqlConnection.OpenAsync();

                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand("SELECT Номер_студента FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
                command.Parameters.AddWithValue("Номер_студента", textBox12.Text);

                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    await sqlReader.ReadAsync();
                    nomer = Convert.ToString(sqlReader["Номер_студента"]);
                    sqlReader.Close();
                    naitinomer2();

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

        private async void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите произвести удаление?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("Обратного пути нет", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (result2 == DialogResult.Yes)
                {
                    SqlCommand command2 = new SqlCommand("DELETE FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
                    command2.Parameters.AddWithValue("Номер_студента", textBox12.Text);
                    await command2.ExecuteNonQueryAsync();

                    MessageBox.Show("Данные успешно изменены", "Студенты", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    DialogResult result3 = MessageBox.Show("Очистить форму?", "Студенты УГЛТУ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    if (result3 == DialogResult.Yes)
                    {
                        textBox12.Text = "";
                        textBox11.Text = "";
                        textBox10.Text = "";
                        textBox9.Text = "";
                        textBox8.Text = "";
                        textBox7.Text = "";
                        textBox6.Text = "";
                        checkBox1.Checked = false;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }
                }
            }
        }


        async void naitinomer3()
        {
            textBox16.Text = "";
            textBox15.Text = "";
            textBox14.Text = "";

            Boolean deti;

            string daterozhd;

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;

            //Вывод фамилии
            SqlCommand command = new SqlCommand("SELECT Фамилия FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
            command.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);
            sqlReader = await command.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox19.Text = Convert.ToString(sqlReader["Фамилия"]);
            sqlReader.Close();
            await command.ExecuteNonQueryAsync();

            //Вывод имени
            SqlCommand command2 = new SqlCommand("SELECT Имя FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
            command2.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);
            sqlReader = await command2.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox18.Text = Convert.ToString(sqlReader["Имя"]);
            sqlReader.Close();
            await command2.ExecuteNonQueryAsync();

            //Вывод отчества
            SqlCommand command3 = new SqlCommand("SELECT Отчество FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
            command3.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);
            sqlReader = await command3.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox17.Text = Convert.ToString(sqlReader["Отчество"]);
            sqlReader.Close();
            await command3.ExecuteNonQueryAsync();

            //Вывод даты рождения
            SqlCommand command4 = new SqlCommand("SELECT [Дата рождения] FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
            command4.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);
            sqlReader = await command4.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            daterozhd = Convert.ToString(sqlReader["Дата рождения"]);
            textBox13.Text = daterozhd.Remove(daterozhd.Length - 7);
            sqlReader.Close();
            await command4.ExecuteNonQueryAsync();

            //Вывод детей
            SqlCommand command10 = new SqlCommand("SELECT Дети FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
            command10.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);
            sqlReader = await command10.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            deti = Convert.ToBoolean(sqlReader["Дети"]);
            if (deti == false)
                checkBox2.Checked = false;
            else if
                (deti == true)
                checkBox2.Checked = true;
            sqlReader.Close();
            await command10.ExecuteNonQueryAsync();

            //Вывод фамилии супр
            try
            {
                SqlCommand command12 = new SqlCommand("SELECT Фамилия FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Сотрудники Where Номер_сотрудника = @Номер_сотрудника)", sqlConnection);
                command12.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);
                sqlReader = await command12.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBox16.Text = Convert.ToString(sqlReader["Фамилия"]);
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
                command13.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);
                sqlReader = await command13.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBox15.Text = Convert.ToString(sqlReader["Имя"]);
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
                command14.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);
                sqlReader = await command14.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBox14.Text = Convert.ToString(sqlReader["Отчество"]);
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
                SqlCommand command14 = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Сотрудники Where Номер_сотрудника = @Номер_сотрудника)", sqlConnection);
                command14.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);
                sqlReader = await command14.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    label28.Text = Convert.ToString(sqlReader["Номер_супруга"]);
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

        private async void button5_Click(object sender, EventArgs e)
        {
            if (textBoxNomer.Text != "")
            {
                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                string nomer;

                await sqlConnection.OpenAsync();

                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand("SELECT Номер_сотрудника FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
                command.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);

                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    await sqlReader.ReadAsync();
                    nomer = Convert.ToString(sqlReader["Номер_сотрудника"]);
                    sqlReader.Close();
                    naitinomer3();

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

        private async void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите произвести удаление?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
           MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("Обратного пути нет", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (result2 == DialogResult.Yes)
                {
                    SqlCommand command100 = new SqlCommand("DELETE FROM Сотрудники WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection);
                    command100.Parameters.AddWithValue("Номер_сотрудника", textBox20.Text);
                    await command100.ExecuteNonQueryAsync();

                    MessageBox.Show("Данные успешно изменены", "Сотрудники", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    DialogResult result3 = MessageBox.Show("Очистить форму?", "Сотрудники", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    if (result3 == DialogResult.Yes)
                    {
                        textBox20.Text = "";
                        textBox19.Text = "";
                        textBox18.Text = "";
                        textBox17.Text = "";
                        textBox16.Text = "";
                        textBox15.Text = "";
                        textBox14.Text = "";
                        checkBox2.Checked = false;
                        textBox12.Text = "";
                        textBox13.Text = "";
                       
                    }
                }
            }
        }

        async void naitinomer4()
        {
            textBox16.Text = "";
            textBox15.Text = "";
            textBox14.Text = "";

            string daterozhd;

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;

            //Вывод фамилии
            SqlCommand command = new SqlCommand("SELECT Фамилия FROM Супруги WHERE Номер_супруга = @Номер_супруга", sqlConnection);
            command.Parameters.AddWithValue("Номер_супруга", textBox24.Text);
            sqlReader = await command.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox23.Text = Convert.ToString(sqlReader["Фамилия"]);
            sqlReader.Close();
            await command.ExecuteNonQueryAsync();

            //Вывод имени
            SqlCommand command2 = new SqlCommand("SELECT Имя FROM Супруги WHERE Номер_супруга = @Номер_супруга", sqlConnection);
            command2.Parameters.AddWithValue("Номер_супруга", textBox24.Text);
            sqlReader = await command2.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox22.Text = Convert.ToString(sqlReader["Имя"]);
            sqlReader.Close();
            await command2.ExecuteNonQueryAsync();

            //Вывод отчества
            SqlCommand command3 = new SqlCommand("SELECT Отчество FROM Супруги WHERE Номер_супруга = @Номер_супруга", sqlConnection);
            command3.Parameters.AddWithValue("Номер_супруга", textBox24.Text);
            sqlReader = await command3.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBox21.Text = Convert.ToString(sqlReader["Отчество"]);
            sqlReader.Close();
            await command3.ExecuteNonQueryAsync();

            //Вывод даты рождения
            SqlCommand command4 = new SqlCommand("SELECT [Дата рождения] FROM Супруги WHERE Номер_супруга = @Номер_супруга", sqlConnection);
            command4.Parameters.AddWithValue("Номер_супруга", textBox24.Text);
            sqlReader = await command4.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            daterozhd = Convert.ToString(sqlReader["Дата рождения"]);
            textBox25.Text = daterozhd.Remove(daterozhd.Length - 7);
            sqlReader.Close();
            await command4.ExecuteNonQueryAsync();
        }


        private async void button7_Click(object sender, EventArgs e)
        {
            if (textBoxNomer.Text != "")
            {
                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                string nomer;

                await sqlConnection.OpenAsync();

                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE Номер_супруга = @Номер_супруга", sqlConnection);
                command.Parameters.AddWithValue("Номер_супруга", textBox24.Text);

                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    await sqlReader.ReadAsync();
                    nomer = Convert.ToString(sqlReader["Номер_супруга"]);
                    sqlReader.Close();
                    naitinomer4();

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

        private async void button8_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите произвести удаление?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("Обратного пути нет", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (result2 == DialogResult.Yes)
                {
                    SqlCommand command2 = new SqlCommand("DELETE FROM Супруги WHERE Номер_супруга = @Номер_супруга", sqlConnection);
                    command2.Parameters.AddWithValue("Номер_супруга", textBox24.Text);
                    await command2.ExecuteNonQueryAsync();

                    MessageBox.Show("Данные успешно изменены", "Супруги", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    DialogResult result3 = MessageBox.Show("Очистить форму?", "Супруги", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    if (result3 == DialogResult.Yes)
                    {
                        textBox24.Text = "";
                        textBox23.Text = "";
                        textBox22.Text = "";
                        textBox21.Text = "";
                        textBox25.Text = "";
                    }
                }
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            if (textBoxNomer.Text != "")
            {
                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);


                await sqlConnection.OpenAsync();

                DialogResult result = MessageBox.Show("Вы точно хотите произвести удаление?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
                {
                    DialogResult result2 = MessageBox.Show("Обратного пути нет", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (result2 == DialogResult.Yes)
                    {
                        SqlCommand command101 = new SqlCommand("UPDATE Общежития SET Номер_студента_углту = 0, Номер_студента_не_углту = 0, Номер_сотрудника = 0, Номер_супруга = 0, Дата_заселения = NULL, Дата_выселения = NULL, Статус = 'Свобдно'  WHERE Номер_студента_углту = @Номер or Номер_студента_не_углту = @Номер or Номер_сотрудника = @Номер or Номер_супруга = @Номер", sqlConnection);
                        command101.Parameters.AddWithValue("Номер", textBox26.Text);
                        await command101.ExecuteNonQueryAsync();

                        MessageBox.Show("Данные успешно изменены", "Общежития", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                        DialogResult result3 = MessageBox.Show("Очистить форму?", "Общежития", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        if (result3 == DialogResult.Yes)
                        {
                            textBox26.Text = "";
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }
    }
}
