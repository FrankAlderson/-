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
    public partial class Visilenie_studenti_ne_ugltu : Form
    {

        SqlConnection sqlConnection;
        public Visilenie_studenti_ne_ugltu()
        {
            InitializeComponent();
            groupBoxDopInfo.Visible = false;
            groupBox1.Location = new Point(79, 164);
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

                SqlCommand command = new SqlCommand("SELECT Номер_студента FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
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
        private void visibledopinfi(object sender, EventArgs e)
        {
            if (checkBoxDopInfo.Checked == true)
            {
                groupBoxDopInfo.Visible = true;
                groupBox1.Location = new Point(74, 321);

            }
            else
            {
                groupBoxDopInfo.Visible = false;
                groupBox1.Location = new Point(79, 164);
            }
        }
        async void naitinomer()
        {
            textBoxFam.Text = "";
            textBoxImya.Text = "";
            textBoxOtch.Text = "";
            textBoxImyaSupr.Text = "";
            textBoxFamSupr.Text = "";
            textBoxOtchSupr.Text = "";
            checkBoxDeti.Checked = false;
            checkBoxDopInfo.Checked = false;
            textBoxInst.Text = "";
            textBoxKurs.Text = "";
            textBoxGrup.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBoxSex.Text = "";
            textBoxDataRozhd.Text = "";

            Boolean deti;

            string daterozhd;
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;

            //Вывод фамилии
            SqlCommand command = new SqlCommand("SELECT Фамилия FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
            command.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxFam.Text = Convert.ToString(sqlReader["Фамилия"]);
            sqlReader.Close();
            await command.ExecuteNonQueryAsync();

            //Вывод имени
            SqlCommand command2 = new SqlCommand("SELECT Имя FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
            command2.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command2.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxImya.Text = Convert.ToString(sqlReader["Имя"]);
            sqlReader.Close();
            await command2.ExecuteNonQueryAsync();

            //Вывод отчества
            SqlCommand command3 = new SqlCommand("SELECT Отчество FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
            command3.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command3.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxOtch.Text = Convert.ToString(sqlReader["Отчество"]);
            sqlReader.Close();
            await command3.ExecuteNonQueryAsync();

            //Вывод даты рождения
            SqlCommand command4 = new SqlCommand("SELECT [Дата рождения] FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
            command4.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command4.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            daterozhd = Convert.ToString(sqlReader["Дата рождения"]);
            textBoxDataRozhd.Text = daterozhd.Remove(daterozhd.Length - 7);
            sqlReader.Close();
            await command4.ExecuteNonQueryAsync();

            //Вывод пола
            SqlCommand command5 = new SqlCommand("SELECT Пол FROM Пол WHERE Номер_пола = (Select Пол From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
            command5.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command5.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxSex.Text = Convert.ToString(sqlReader["Пол"]);
            sqlReader.Close();
            await command5.ExecuteNonQueryAsync();
            if (textBoxSex.Text == "Мужчина")
            {
                groupBoxSupr.Text = "Супруга";
            }
            else if (textBoxSex.Text == "Женщина")
            {
                groupBoxSupr.Text = "Супруг";
            }

            //Вывод университета
            SqlCommand command6 = new SqlCommand("SELECT Универститет FROM Универститеты WHERE Номер_универститета = (Select Универститет From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
            command6.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command6.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxInst.Text = Convert.ToString(sqlReader["Универститет"]);
            sqlReader.Close();
            await command6.ExecuteNonQueryAsync();

            //Вывод группы
            SqlCommand command7 = new SqlCommand("SELECT Группа FROM Группы_не_УГЛТУ WHERE Номер_группы = (Select Группа From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
            command7.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command7.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxGrup.Text = Convert.ToString(sqlReader["Группа"]);
            sqlReader.Close();
            await command7.ExecuteNonQueryAsync();

            //Вывод курса
            SqlCommand command8 = new SqlCommand("SELECT Курс FROM Курсы WHERE Номер_курса = (Select Курс From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
            command8.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
            sqlReader = await command8.ExecuteReaderAsync();
            await sqlReader.ReadAsync();
            textBoxKurs.Text = Convert.ToString(sqlReader["Курс"]);
            sqlReader.Close();
            await command8.ExecuteNonQueryAsync();

            //Вывод детей
            SqlCommand command10 = new SqlCommand("SELECT Дети FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер_студента", sqlConnection);
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

            //Вывод фамилии супр
            try
            {
                SqlCommand command12 = new SqlCommand("SELECT Фамилия FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
                command12.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
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
                SqlCommand command13 = new SqlCommand("SELECT Имя FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
                command13.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
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
                SqlCommand command14 = new SqlCommand("SELECT Отчество FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where Номер_студента = @Номер_студента)", sqlConnection);
                command14.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
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
                SqlCommand command14 = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
                command14.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                sqlReader = await command14.ExecuteReaderAsync();
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

            // Вывод номера общ

            try
            {
                SqlCommand command16 = new SqlCommand("SELECT Номер_общежития FROM Общежития WHERE Номер_студента_не_углту = @Номер_студента", sqlConnection);
                command16.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                sqlReader = await command16.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBox1.Text = Convert.ToString(sqlReader["Номер_общежития"]);
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

            // Вывод блока

            try
            {
                SqlCommand command17 = new SqlCommand("SELECT Блок FROM Общежития WHERE Номер_студента_не_углту = @Номер_студента", sqlConnection);
                command17.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                sqlReader = await command17.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBox2.Text = Convert.ToString(sqlReader["Блок"]);
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

            // Вывод комнаты

            try
            {
                SqlCommand command18 = new SqlCommand("SELECT Комната FROM Общежития WHERE Номер_студента_не_углту = @Номер_студента", sqlConnection);
                command18.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                sqlReader = await command18.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBox3.Text = Convert.ToString(sqlReader["Комната"]);
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

            // Вывод места

            try
            {
                SqlCommand command19 = new SqlCommand("SELECT Место FROM Общежития WHERE Номер_студента_не_углту = @Номер_студента", sqlConnection);
                command19.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
                sqlReader = await command19.ExecuteReaderAsync();
                while (sqlReader.Read())
                {
                    textBox4.Text = Convert.ToString(sqlReader["Место"]);
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

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxNomer.Text = "";
            textBoxFam.Text = "";
            textBoxImya.Text = "";
            textBoxOtch.Text = "";
            textBoxImyaSupr.Text = "";
            textBoxFamSupr.Text = "";
            textBoxOtchSupr.Text = "";
            checkBoxDeti.Checked = false;
            checkBoxDopInfo.Checked = false;
            textBoxInst.Text = "";
            textBoxKurs.Text = "";
            textBoxGrup.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBoxSex.Text = "";
            textBoxDataRozhd.Text = "";
        }

        private async void button2_Click(object sender, EventArgs e)

        {
            DialogResult result = MessageBox.Show("Вы точно хотите произвести выселение?", "Выселение", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("Обратного пути нет", "Выселение", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (result2 == DialogResult.Yes)
                {
                    string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                    sqlConnection = new SqlConnection(connectionString);

                    await sqlConnection.OpenAsync();

                    SqlCommand command = new SqlCommand("UPDATE Общежития SET Номер_студента_не_углту = 0, Дата_заселения = NULL, Статус = 'Свободно', Дата_выселения = NULL WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
                    command.Parameters.AddWithValue("Obsh", textBox1.Text);
                    command.Parameters.AddWithValue("Blok", textBox2.Text);
                    command.Parameters.AddWithValue("Komnata", textBox3.Text);
                    command.Parameters.AddWithValue("Mesto", textBox4.Text);
                    await command.ExecuteNonQueryAsync();

                    if (textBoxFamSupr.Text != "")
                    {
                        SqlCommand command2 = new SqlCommand("UPDATE Общежития SET Номер_супруга = 0, Дата_заселения = NULL, Статус = 'Свободно', Дата_выселения = NULL WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
                        command2.Parameters.AddWithValue("Obsh", textBox1.Text);
                        command2.Parameters.AddWithValue("Blok", textBox2.Text);
                        command2.Parameters.AddWithValue("Komnata", textBox3.Text);
                        command2.Parameters.AddWithValue("Mesto", Convert.ToString(Convert.ToInt32(textBox4.Text) + 1));
                        await command2.ExecuteNonQueryAsync();
                    }
                    if (checkBoxDeti.Checked == true)
                    {
                        SqlCommand command3 = new SqlCommand("UPDATE Общежития SET Номер_супруга = 0, Дата_заселения = NULL, Статус = 'Свободно', Дата_выселения = NULL WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
                        command3.Parameters.AddWithValue("Obsh", textBox1.Text);
                        command3.Parameters.AddWithValue("Blok", textBox2.Text);
                        command3.Parameters.AddWithValue("Komnata", textBox3.Text);
                        command3.Parameters.AddWithValue("Mesto", Convert.ToString(Convert.ToInt32(textBox4.Text) + 1));
                        await command3.ExecuteNonQueryAsync();
                    }

                    MessageBox.Show("Данные успешно удалены", "Выселение", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    DialogResult result3 = MessageBox.Show("Очистить форму?", "Выселение", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
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
                        checkBoxDopInfo.Checked = false;
                        textBoxInst.Text = "";
                        textBoxKurs.Text = "";
                        textBoxGrup.Text = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBoxSex.Text = "";
                        textBoxDataRozhd.Text = "";
                    }
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)

        {
            DialogResult result = MessageBox.Show("Вы точно хотите произвести выселение?", "Выселение", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
           MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("Обратного пути нет", "Выселение", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (result2 == DialogResult.Yes)
                {
                    string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                    sqlConnection = new SqlConnection(connectionString);

                    await sqlConnection.OpenAsync();

                    SqlCommand command2 = new SqlCommand("UPDATE Общежития SET Номер_супруга = 0, Дата_заселения = NULL, Статус = 'Свободно', Дата_выселения = NULL WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
                    command2.Parameters.AddWithValue("Obsh", textBox1.Text);
                    command2.Parameters.AddWithValue("Blok", textBox2.Text);
                    command2.Parameters.AddWithValue("Komnata", textBox3.Text);
                    command2.Parameters.AddWithValue("Mesto", Convert.ToString(Convert.ToInt32(textBox4.Text) + 1));
                    await command2.ExecuteNonQueryAsync();

                    MessageBox.Show("Данные успешно удалены", "Выселение", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    DialogResult result3 = MessageBox.Show("Очистить форму?", "Выселение", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
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
                        checkBoxDopInfo.Checked = false;
                        textBoxInst.Text = "";
                        textBoxKurs.Text = "";
                        textBoxGrup.Text = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBoxSex.Text = "";
                        textBoxDataRozhd.Text = "";
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
