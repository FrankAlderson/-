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
    public partial class Izmenit_studenti_ne_ugltu : Form
    {

        SqlConnection sqlConnection;
        public Izmenit_studenti_ne_ugltu()
        {
            InitializeComponent();
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
                SqlCommand command15 = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
                command15.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
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
                SqlCommand command16 = new SqlCommand("SELECT [Дата рождения] FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
                command16.Parameters.AddWithValue("Номер_студента", textBoxNomer.Text);
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

        private async void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand("UPDATE Студенты_не_УГЛТУ SET [Супруг/Супруга] = @Supr WHERE Номер_студента=@Nomer", sqlConnection);

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
                    int nomerInst = 0;
                    int nomerGrup = 0;
                    int nomerKurs = 0;

                    string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                    sqlConnection = new SqlConnection(connectionString);

                    await sqlConnection.OpenAsync();

                    SqlDataReader sqlReader1 = null;

                    SqlCommand command1 = new SqlCommand("SELECT Номер_универститета FROM Универститеты WHERE Универститет = @НомерУнив", sqlConnection);
                    command1.Parameters.AddWithValue("НомерУнив", textBoxInst.Text);

                    try
                    {
                        sqlReader1 = await command1.ExecuteReaderAsync();
                        await sqlReader1.ReadAsync();
                        nomerInst = Convert.ToInt32(sqlReader1["Номер_универститета"]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (sqlReader1 != null)
                            sqlReader1.Close();
                    }
                    await command1.ExecuteNonQueryAsync();


                    SqlDataReader sqlReader2 = null;
                    SqlCommand command2 = new SqlCommand("SELECT Номер_группы FROM Группы_не_УГЛТУ WHERE Группа = @НомерГр", sqlConnection);
                    command2.Parameters.AddWithValue("НомерГр", textBoxGrup.Text);

                    try
                    {
                        sqlReader2 = await command2.ExecuteReaderAsync();
                        await sqlReader2.ReadAsync();
                        nomerGrup = Convert.ToInt32(sqlReader2["Номер_группы"]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (sqlReader2 != null)
                            sqlReader2.Close();
                    }
                    await command2.ExecuteNonQueryAsync();


                    SqlDataReader sqlReader3 = null;
                    SqlCommand command3 = new SqlCommand("SELECT Номер_курса FROM Курсы WHERE Курс = @НомерКурс", sqlConnection);
                    command3.Parameters.AddWithValue("НомерКурс", textBoxKurs.Text);

                    try
                    {
                        sqlReader3 = await command3.ExecuteReaderAsync();
                        await sqlReader3.ReadAsync();
                        nomerKurs = Convert.ToInt32(sqlReader3["Номер_курса"]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (sqlReader3 != null)
                            sqlReader3.Close();
                    }
                    await command3.ExecuteNonQueryAsync();


                    SqlCommand command = new SqlCommand("UPDATE Студенты_не_УГЛТУ SET Фамилия = @Fam, Имя = @Imya, Отчество = @Otch, Универститет = @nomerInst, Группа = @nomerGr, Курс = @nomerKurs, [Дети] = @Deti WHERE Номер_студента=@Nomer", sqlConnection);
                    command.Parameters.AddWithValue("Nomer", textBoxNomer.Text);
                    command.Parameters.AddWithValue("Fam", textBoxFam.Text);
                    command.Parameters.AddWithValue("Imya", textBoxImya.Text);
                    command.Parameters.AddWithValue("Otch", textBoxOtch.Text);
                    command.Parameters.AddWithValue("nomerInst", nomerInst);
                    command.Parameters.AddWithValue("nomerGr", nomerGrup);
                    command.Parameters.AddWithValue("nomerKurs", nomerKurs);

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


                    MessageBox.Show("Данные успешно изменены", "Студенты", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    DialogResult result3 = MessageBox.Show("Очистить форму?", "Студенты", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
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
                        textBoxInst.Text = "";
                        textBoxKurs.Text = "";
                        textBoxGrup.Text = "";
                        textBoxSex.Text = "";
                        textBoxDataRozhd.Text = "";
                    }
                    this.TopMost = true;
                }
            }
        }

        private void Izmenit_studenti_ne_ugltu_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.GruppiNeUgltu". При необходимости она может быть перемещена или удалена.
            this.gruppiNeUgltuTableAdapter.Fill(this.diplomDataSet.GruppiNeUgltu);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Kursi". При необходимости она может быть перемещена или удалена.
            this.kursiTableAdapter.Fill(this.diplomDataSet.Kursi);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Univeri". При необходимости она может быть перемещена или удалена.
            this.univeriTableAdapter.Fill(this.diplomDataSet.Univeri);

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


            SqlCommand command2 = new SqlCommand("UPDATE Студенты_не_УГЛТУ SET [Супруг/Супруга] = @Supr WHERE Номер_студента=@Nomer", sqlConnection);

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
