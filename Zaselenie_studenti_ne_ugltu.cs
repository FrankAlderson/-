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
using Word = Microsoft.Office.Interop.Word;
using Point = System.Drawing.Point;

namespace DIPLOM_V2
{
    public partial class Zaselenie_studenti_ne_ugltu : Form
    {
        SqlConnection sqlConnection;
        SqlConnection sqlConnection2;

        public Zaselenie_studenti_ne_ugltu()
        {
            InitializeComponent();
            groupBoxDopInfo.Visible = false;
            tableLayoutPanel1.Location = new Point(79, 155);
            groupBox1.Location = new Point(79, 410);
        }

        Word.Application word;
        Word.Document doc;
        Word.Range r;
        Word.Application word1;
        Word.Document doc1;
        Word.Range r1;

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
                tableLayoutPanel1.Location = new Point(79, 325);
                groupBox1.Location = new Point(79, 575);

            }
            else
            {
                groupBoxDopInfo.Visible = false;
                tableLayoutPanel1.Location = new Point(79, 160);
                groupBox1.Location = new Point(79, 410);
            }
        }
        async void naitinomer()
        {
            textBoxFamSupr.Text = "";
            textBoxImyaSupr.Text = "";
            textBoxOtchSupr.Text = "";

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
                SqlCommand command14 = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Студенты_не_УГЛТУ Where [Номер_студента] = @Номер_студента)", sqlConnection);
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

        private async void button3_Click(object sender, EventArgs e)
        {
            List<int> nomera = new List<int>();
            List<int> nomera2 = new List<int>();

            string connectionString2 = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection2 = new SqlConnection(connectionString2);

            SqlDataReader sqlRead = null;

            await sqlConnection2.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT Номер_студента_не_углту FROM Общежития WHERE Номер_студента_не_углту = @Номер_студента_не_углту", sqlConnection2);
                com.Parameters.AddWithValue("Номер_студента_не_углту", textBoxNomer.Text);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera2.Add(Convert.ToInt32(sqlRead["Номер_студента_не_углту"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlRead != null && !sqlRead.IsClosed)
                {
                    sqlRead.Close();
                }
            }

            if (nomera2.Count == 1)
            {
                MessageBox.Show("Данный студент уже проживает в общежитии");
            }

            else if (nomera2.Count == 0)
            {

                if (checkBoxDeti.Checked == true || textBoxFamSupr.Text != "")
                {
                    listView1.Items.Clear();

                    string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                    sqlConnection = new SqlConnection(connectionString);

                    await sqlConnection.OpenAsync();

                    SqlDataReader dataReader = null;

                    try
                    {
                        SqlCommand sqlCommand2 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 1 AND (Блок = 5 or Блок = 6 or Блок = 7) AND Статус = 'Свободно'", sqlConnection);

                        dataReader = sqlCommand2.ExecuteReader();
                        ListViewItem item = null;
                        while (dataReader.Read())
                        {
                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                            listView1.Items.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (dataReader != null && !dataReader.IsClosed)
                        {
                            dataReader.Close();
                        }
                    }
                }
                else if (checkBoxDeti.Checked == false && textBoxFamSupr.Text == "")
                {
                    listView1.Items.Clear();

                    string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                    sqlConnection = new SqlConnection(connectionString);

                    await sqlConnection.OpenAsync();

                    SqlDataReader dataReader = null;

                    SqlDataReader sqlReader = null;

                    string gruppa;
                    string kurs;
                    string nomer1;
                    string nomer2;
                    string nomer3;
                    int qwe;

                if (textBoxSex.Text == "Мужчина")
                {
                    if (textBoxInst.Text == "Университет 1")
                    {
                        if (textBoxGrup.Text == "Группа 1 1 1")
                        {
                            try
                            {
                                SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 1 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command1.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand5.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 1 2")
                        {
                            try
                            {
                                SqlCommand command6 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 1 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command6.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command7 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command7.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command7.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command7.ExecuteNonQueryAsync();

                                SqlCommand command8 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command8.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command8.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command8.ExecuteNonQueryAsync();

                                SqlCommand command9 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command9.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command9.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command9.ExecuteNonQueryAsync();

                                SqlCommand command10 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command10.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command10.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command10.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand11 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand11.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand11.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand11.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand11.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 1 3")
                        {
                            try
                            {
                                SqlCommand command12 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 1 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command12.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command13 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command13.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command13.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command13.ExecuteNonQueryAsync();

                                SqlCommand command14 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command14.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command14.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command14.ExecuteNonQueryAsync();

                                SqlCommand command15 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command15.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command15.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command15.ExecuteNonQueryAsync();

                                SqlCommand command16 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command16.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command16.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command16.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand17 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand17.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand17.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand17.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand17.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                            listView1.Items.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 1 4")
                        {
                            try
                            {
                                SqlCommand command18 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 1 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command18.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command19 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command19.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command19.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command19.ExecuteNonQueryAsync();

                                SqlCommand command20 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command20.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command20.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command20.ExecuteNonQueryAsync();

                                SqlCommand command21 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command21.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command21.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command21.ExecuteNonQueryAsync();

                                SqlCommand command22 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command22.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command22.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command22.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand23 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand23.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand23.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand23.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand23.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 1 5")
                        {
                            try
                            {
                                SqlCommand command24 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 1 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command24.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command25 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command25.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command25.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command25.ExecuteNonQueryAsync();

                                SqlCommand command26 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command26.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command26.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command26.ExecuteNonQueryAsync();

                                SqlCommand command27 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command27.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command27.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command27.ExecuteNonQueryAsync();

                                SqlCommand command28 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command28.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command28.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command28.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand29 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand29.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand29.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand29.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand29.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 1 2 1")
                        {

                            try
                            {
                                SqlCommand command31 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 2 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command31.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command32 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command32.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command32.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command32.ExecuteNonQueryAsync();

                                SqlCommand command33 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command33.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command33.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command33.ExecuteNonQueryAsync();

                                SqlCommand command34 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command34.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command34.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command34.ExecuteNonQueryAsync();

                                SqlCommand command35 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command35.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command35.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command35.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand36 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand36.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand36.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand36.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand36.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 2 2")

                        {
                            try
                            {
                                SqlCommand command37 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 2 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command37.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command38 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command38.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command38.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command38.ExecuteNonQueryAsync();

                                SqlCommand command39 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command39.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command39.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command39.ExecuteNonQueryAsync();

                                SqlCommand command40 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command40.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command40.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command40.ExecuteNonQueryAsync();

                                SqlCommand command41 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command41.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command41.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command41.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand42 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand42.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand42.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand42.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand42.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 2 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 2 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command43 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command43.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command43.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command43.ExecuteNonQueryAsync();

                                SqlCommand command44 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command44.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command44.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command44.ExecuteNonQueryAsync();

                                SqlCommand command45 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command45.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command45.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command45.ExecuteNonQueryAsync();

                                SqlCommand command46 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command46.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command46.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command46.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand47 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand47.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand47.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand47.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand47.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 2 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 2 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command48 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command48.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command48.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command48.ExecuteNonQueryAsync();

                                SqlCommand command49 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command49.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command49.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command49.ExecuteNonQueryAsync();

                                SqlCommand command50 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command50.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command50.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command50.ExecuteNonQueryAsync();

                                SqlCommand command51 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command51.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command51.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command51.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand52 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand52.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand52.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand52.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand52.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 2 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 2 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command53 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command53.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command53.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command53.ExecuteNonQueryAsync();

                                SqlCommand command54 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command54.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command54.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command54.ExecuteNonQueryAsync();

                                SqlCommand command55 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command55.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command55.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command55.ExecuteNonQueryAsync();

                                SqlCommand command56 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command56.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command56.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command56.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand57 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand57.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand57.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand57.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand57.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 1 3 1")
                        {

                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 3 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command59 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command59.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command59.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command59.ExecuteNonQueryAsync();

                                SqlCommand command60 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command60.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command60.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command60.ExecuteNonQueryAsync();

                                SqlCommand command61 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command61.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command61.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command61.ExecuteNonQueryAsync();

                                SqlCommand command62 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command62.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command62.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command62.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand63 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand63.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand63.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand63.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand63.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 3 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 3 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command64 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command64.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command64.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command64.ExecuteNonQueryAsync();

                                SqlCommand command65 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command65.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command65.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command65.ExecuteNonQueryAsync();

                                SqlCommand command66 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command66.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command66.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command66.ExecuteNonQueryAsync();

                                SqlCommand command67 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command67.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command67.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command67.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand68 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand68.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand68.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand68.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand68.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 3 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 3 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command69 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command69.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command69.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command69.ExecuteNonQueryAsync();

                                SqlCommand command70 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command70.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command70.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command70.ExecuteNonQueryAsync();

                                SqlCommand command71 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command71.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command71.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command71.ExecuteNonQueryAsync();

                                SqlCommand command72 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command72.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command72.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command72.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand73 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand73.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand73.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand73.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand73.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 3 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 3 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command74 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command74.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command74.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command74.ExecuteNonQueryAsync();

                                SqlCommand command75 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command75.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command75.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command75.ExecuteNonQueryAsync();

                                SqlCommand command76 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command76.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command76.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command76.ExecuteNonQueryAsync();

                                SqlCommand command77 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command77.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command77.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command77.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand78 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand78.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand78.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand78.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand78.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 3 5")
                        {
                            try
                            {
                                SqlCommand command79 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 3 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command79.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command80 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command80.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command80.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command80.ExecuteNonQueryAsync();

                                SqlCommand command81 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command81.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command81.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command81.ExecuteNonQueryAsync();

                                SqlCommand command82 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command82.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command82.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command82.ExecuteNonQueryAsync();

                                SqlCommand command83 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command83.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command83.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command83.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand84 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand84.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand84.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand84.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand84.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 1 4 1")
                        {

                            try
                            {
                                SqlCommand command86 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 4 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command86.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command87 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command87.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command87.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command87.ExecuteNonQueryAsync();

                                SqlCommand command88 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command88.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command88.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command88.ExecuteNonQueryAsync();

                                SqlCommand command89 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command89.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command89.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command89.ExecuteNonQueryAsync();

                                SqlCommand command90 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command90.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command90.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command90.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand91 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand91.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand91.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand91.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand91.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 4 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 4 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 4 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 4 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 4 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 4 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 4 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 4 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 1; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 1 5 1")
                        {

                            try
                            {
                                SqlCommand command86 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 5 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command86.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command87 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command87.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command87.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command87.ExecuteNonQueryAsync();

                                SqlCommand command88 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command88.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command88.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command88.ExecuteNonQueryAsync();

                                SqlCommand command89 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command89.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command89.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command89.ExecuteNonQueryAsync();

                                SqlCommand command90 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command90.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command90.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command90.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand91 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand91.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand91.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand91.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand91.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 5 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 5 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 5 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 5 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 5 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 5 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 5 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 5 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 1; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }

                        if (listView1.Items.Count == 0)
                        {
                            if (textBoxKurs.Text == "1 курс")
                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '1 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "2 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '2 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "3 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '3 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "4 курс")
                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '4 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "5 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '5 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }

                            if (listView1.Items.Count == 0)
                            {
                                for (int i = -1; i < nomera.Count(); i++)
                                {
                                    DialogResult result = MessageBox.Show("Комната не найдена, вывести свободные комнаты четвертого общежития?", "Студенты не УГЛТУ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                                    if (result == DialogResult.Yes)
                                    {
                                        sqlConnection = new SqlConnection(connectionString);
                                        await sqlConnection.OpenAsync();
                                        try
                                        {
                                            SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE  Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно'", sqlConnection);

                                            dataReader = sqlCommand.ExecuteReader();
                                            ListViewItem item = null;
                                            while (dataReader.Read())
                                            {
                                                item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                                listView1.Items.Add(item);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                        finally
                                        {
                                            if (dataReader != null && !dataReader.IsClosed)
                                            {
                                                dataReader.Close();
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    else if (textBoxInst.Text == "Университет 2")
                    {
                        if (textBoxGrup.Text == "Группа 2 1 1")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 1 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 1 2")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 1 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 1 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 1 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                            listView1.Items.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 1 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 1 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 1 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 1 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 2 2 1")
                        {

                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 2 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 2 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 2 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 2 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 2 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 2 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 2 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 2 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 2 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 2 3 1")
                        {

                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 3 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 3 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 3 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 3 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 3 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 3 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 3 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 3 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 3 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 2 4 1")
                        {

                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 4 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 4 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 4 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 4 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 4 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 4 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 4 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 4 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 4 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 1; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 2 5 1")
                        {

                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 5 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 5 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 5 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 5 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 5 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 5 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 5 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 5 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 5 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }

                        if (listView1.Items.Count == 0)
                        {
                            if (textBoxKurs.Text == "1 курс")
                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '1 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "2 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '2 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "3 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '3 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "4 курс")
                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '4 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "5 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '5 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }

                            if (listView1.Items.Count == 0)
                            {
                                for (int i = -1; i < nomera.Count(); i++)
                                {
                                    DialogResult result = MessageBox.Show("Комната не найдена, вывести свободные комнаты четвертого общежития?", "Студенты не УГЛТУ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                                    if (result == DialogResult.Yes)
                                    {
                                        sqlConnection = new SqlConnection(connectionString);
                                        await sqlConnection.OpenAsync();
                                        try
                                        {
                                            SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE  Номер_общежития = 4 AND Блок%2!=0 AND Статус = 'Свободно'", sqlConnection);

                                            dataReader = sqlCommand.ExecuteReader();
                                            ListViewItem item = null;
                                            while (dataReader.Read())
                                            {
                                                item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                                listView1.Items.Add(item);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                        finally
                                        {
                                            if (dataReader != null && !dataReader.IsClosed)
                                            {
                                                dataReader.Close();
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                else if (textBoxSex.Text == "Женщина")

                {
                    if (textBoxInst.Text == "Университет 1")
                    {
                        if (textBoxGrup.Text == "Группа 1 1 1")
                        {
                            try
                            {
                                SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 1 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command1.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand5.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 1 2")
                        {
                            try
                            {
                                SqlCommand command6 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 1 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command6.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command7 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command7.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command7.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command7.ExecuteNonQueryAsync();

                                SqlCommand command8 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command8.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command8.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command8.ExecuteNonQueryAsync();

                                SqlCommand command9 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command9.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command9.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command9.ExecuteNonQueryAsync();

                                SqlCommand command10 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command10.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command10.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command10.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand11 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand11.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand11.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand11.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand11.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 1 3")
                        {
                            try
                            {
                                SqlCommand command12 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 1 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command12.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command13 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command13.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command13.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command13.ExecuteNonQueryAsync();

                                SqlCommand command14 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command14.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command14.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command14.ExecuteNonQueryAsync();

                                SqlCommand command15 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command15.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command15.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command15.ExecuteNonQueryAsync();

                                SqlCommand command16 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command16.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command16.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command16.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand17 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand17.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand17.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand17.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand17.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                            listView1.Items.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 1 4")
                        {
                            try
                            {
                                SqlCommand command18 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 1 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command18.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command19 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command19.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command19.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command19.ExecuteNonQueryAsync();

                                SqlCommand command20 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command20.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command20.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command20.ExecuteNonQueryAsync();

                                SqlCommand command21 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command21.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command21.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command21.ExecuteNonQueryAsync();

                                SqlCommand command22 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command22.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command22.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command22.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand23 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand23.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand23.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand23.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand23.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 1 5")
                        {
                            try
                            {
                                SqlCommand command24 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 1 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command24.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command25 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command25.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command25.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command25.ExecuteNonQueryAsync();

                                SqlCommand command26 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command26.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command26.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command26.ExecuteNonQueryAsync();

                                SqlCommand command27 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command27.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command27.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command27.ExecuteNonQueryAsync();

                                SqlCommand command28 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command28.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command28.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command28.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand29 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand29.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand29.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand29.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand29.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 1 2 1")
                        {

                            try
                            {
                                SqlCommand command31 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 2 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command31.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command32 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command32.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command32.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command32.ExecuteNonQueryAsync();

                                SqlCommand command33 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command33.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command33.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command33.ExecuteNonQueryAsync();

                                SqlCommand command34 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command34.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command34.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command34.ExecuteNonQueryAsync();

                                SqlCommand command35 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command35.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command35.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command35.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand36 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand36.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand36.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand36.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand36.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 2 2")

                        {
                            try
                            {
                                SqlCommand command37 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 2 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command37.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command38 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command38.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command38.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command38.ExecuteNonQueryAsync();

                                SqlCommand command39 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command39.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command39.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command39.ExecuteNonQueryAsync();

                                SqlCommand command40 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command40.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command40.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command40.ExecuteNonQueryAsync();

                                SqlCommand command41 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command41.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command41.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command41.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand42 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand42.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand42.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand42.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand42.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 2 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 2 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command43 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command43.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command43.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command43.ExecuteNonQueryAsync();

                                SqlCommand command44 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command44.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command44.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command44.ExecuteNonQueryAsync();

                                SqlCommand command45 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command45.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command45.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command45.ExecuteNonQueryAsync();

                                SqlCommand command46 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command46.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command46.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command46.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand47 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand47.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand47.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand47.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand47.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 2 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 2 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command48 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command48.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command48.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command48.ExecuteNonQueryAsync();

                                SqlCommand command49 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command49.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command49.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command49.ExecuteNonQueryAsync();

                                SqlCommand command50 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command50.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command50.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command50.ExecuteNonQueryAsync();

                                SqlCommand command51 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command51.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command51.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command51.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand52 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand52.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand52.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand52.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand52.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 2 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 2 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command53 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command53.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command53.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command53.ExecuteNonQueryAsync();

                                SqlCommand command54 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command54.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command54.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command54.ExecuteNonQueryAsync();

                                SqlCommand command55 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command55.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command55.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command55.ExecuteNonQueryAsync();

                                SqlCommand command56 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command56.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command56.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command56.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand57 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand57.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand57.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand57.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand57.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 1 3 1")
                        {

                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 3 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command59 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command59.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command59.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command59.ExecuteNonQueryAsync();

                                SqlCommand command60 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command60.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command60.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command60.ExecuteNonQueryAsync();

                                SqlCommand command61 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command61.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command61.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command61.ExecuteNonQueryAsync();

                                SqlCommand command62 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command62.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command62.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command62.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand63 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand63.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand63.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand63.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand63.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 3 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 3 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command64 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command64.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command64.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command64.ExecuteNonQueryAsync();

                                SqlCommand command65 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command65.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command65.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command65.ExecuteNonQueryAsync();

                                SqlCommand command66 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command66.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command66.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command66.ExecuteNonQueryAsync();

                                SqlCommand command67 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command67.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command67.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command67.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand68 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand68.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand68.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand68.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand68.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 3 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 3 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command69 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command69.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command69.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command69.ExecuteNonQueryAsync();

                                SqlCommand command70 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command70.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command70.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command70.ExecuteNonQueryAsync();

                                SqlCommand command71 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command71.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command71.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command71.ExecuteNonQueryAsync();

                                SqlCommand command72 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command72.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command72.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command72.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand73 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand73.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand73.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand73.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand73.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 3 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 3 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command74 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command74.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command74.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command74.ExecuteNonQueryAsync();

                                SqlCommand command75 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command75.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command75.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command75.ExecuteNonQueryAsync();

                                SqlCommand command76 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command76.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command76.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command76.ExecuteNonQueryAsync();

                                SqlCommand command77 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command77.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command77.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command77.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand78 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand78.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand78.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand78.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand78.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 3 5")
                        {
                            try
                            {
                                SqlCommand command79 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 3 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command79.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command80 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command80.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command80.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command80.ExecuteNonQueryAsync();

                                SqlCommand command81 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command81.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command81.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command81.ExecuteNonQueryAsync();

                                SqlCommand command82 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command82.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command82.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command82.ExecuteNonQueryAsync();

                                SqlCommand command83 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command83.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command83.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command83.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand84 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand84.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand84.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand84.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand84.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 1 4 1")
                        {

                            try
                            {
                                SqlCommand command86 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 4 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command86.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command87 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command87.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command87.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command87.ExecuteNonQueryAsync();

                                SqlCommand command88 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command88.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command88.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command88.ExecuteNonQueryAsync();

                                SqlCommand command89 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command89.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command89.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command89.ExecuteNonQueryAsync();

                                SqlCommand command90 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command90.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command90.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command90.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand91 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand91.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand91.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand91.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand91.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 4 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 4 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 4 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 4 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 4 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 4 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 4 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 4 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 1; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 1 5 1")
                        {

                            try
                            {
                                SqlCommand command86 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 5 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command86.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command87 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command87.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command87.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command87.ExecuteNonQueryAsync();

                                SqlCommand command88 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command88.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command88.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command88.ExecuteNonQueryAsync();

                                SqlCommand command89 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command89.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command89.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command89.ExecuteNonQueryAsync();

                                SqlCommand command90 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command90.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command90.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command90.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand91 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand91.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand91.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand91.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand91.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 5 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 5 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 5 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 5 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 5 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 5 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 1 5 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 1 5 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 1; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }

                        if (listView1.Items.Count == 0)
                        {
                            if (textBoxKurs.Text == "1 курс")
                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '1 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "2 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '2 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "3 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '3 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "4 курс")
                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '4 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "5 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '5 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }

                            if (listView1.Items.Count == 0)
                            {
                                for (int i = -1; i < nomera.Count(); i++)
                                {
                                    DialogResult result = MessageBox.Show("Комната не найдена, вывести свободные комнаты четвертого общежития?", "Студенты не УГЛТУ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                                    if (result == DialogResult.Yes)
                                    {
                                        sqlConnection = new SqlConnection(connectionString);
                                        await sqlConnection.OpenAsync();
                                        try
                                        {
                                            SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE  Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно'", sqlConnection);

                                            dataReader = sqlCommand.ExecuteReader();
                                            ListViewItem item = null;
                                            while (dataReader.Read())
                                            {
                                                item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                                listView1.Items.Add(item);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                        finally
                                        {
                                            if (dataReader != null && !dataReader.IsClosed)
                                            {
                                                dataReader.Close();
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    else if (textBoxInst.Text == "Университет 2")
                    {
                        if (textBoxGrup.Text == "Группа 2 1 1")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 1 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 1 2")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 1 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 1 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 1 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                            listView1.Items.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 1 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 1 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 1 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 1 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 2 2 1")
                        {

                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 2 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 2 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 2 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 2 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 2 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 2 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 2 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 2 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 2 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 2 3 1")
                        {

                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 3 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 3 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 3 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 3 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 3 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 3 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 3 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 3 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 3 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 2 4 1")
                        {

                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 4 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 4 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 4 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 4 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 4 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 4 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 4 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 4 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 4 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 1; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();


                        }
                        else if (textBoxGrup.Text == "Группа 2 5 1")
                        {

                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 5 1') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {

                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 5 2")

                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 5 2') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 5 3")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 5 3') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 5 4")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 5 4') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                    nomera.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }
                        else if (textBoxGrup.Text == "Группа 2 5 5")
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Группы_не_УГЛТУ ON dbo.Студенты_не_УГЛТУ.Группа = dbo.Группы_не_УГЛТУ.Номер_группы WHERE (dbo.Группы_не_УГЛТУ.Группа = 'Группа 2 5 5') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null && !dataReader.IsClosed)
                                {
                                    dataReader.Close();
                                }
                            }
                            for (int i = 0; i < nomera.Count(); i++)
                            {
                                SqlCommand command2 = new SqlCommand("select Группа From Группы_не_УГЛТУ where Номер_группы = (select Группа from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                command2.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command2.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                gruppa = Convert.ToString(sqlReader["Группа"]);
                                sqlReader.Close();
                                await command2.ExecuteNonQueryAsync();

                                SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command3.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command3.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                sqlReader.Close();
                                await command3.ExecuteNonQueryAsync();

                                SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command4.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command4.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer2 = Convert.ToString(sqlReader["Блок"]);
                                sqlReader.Close();
                                await command4.ExecuteNonQueryAsync();

                                SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                command5.Parameters.AddWithValue("Номер", nomera[i]);
                                sqlReader = await command5.ExecuteReaderAsync();
                                await sqlReader.ReadAsync();
                                nomer3 = Convert.ToString(sqlReader["Комната"]);
                                sqlReader.Close();
                                await command5.ExecuteNonQueryAsync();

                                try
                                {
                                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("Общага", nomer1);
                                    sqlCommand.Parameters.AddWithValue("Блок", nomer2);
                                    sqlCommand.Parameters.AddWithValue("Комната", nomer3);

                                    dataReader = sqlCommand.ExecuteReader();
                                    ListViewItem item = null;

                                    while (dataReader.Read())
                                    {
                                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                        listView1.Items.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            nomera.Clear();
                        }

                        if (listView1.Items.Count == 0)
                        {
                            if (textBoxKurs.Text == "1 курс")
                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '1 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "2 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '2 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "3 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '3 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "4 курс")
                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '4 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }
                            else if (textBoxKurs.Text == "5 курс")

                            {
                                try
                                {
                                    SqlCommand command1 = new SqlCommand("SELECT Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента INNER JOIN dbo.Курсы ON dbo.Студенты_не_УГЛТУ.Курс = dbo.Курсы.Номер_курса WHERE (dbo.Курсы.Курс = '5 курс') AND (dbo.Общежития.Номер_общежития = 4) AND (dbo.Общежития.Статус = 'Занято')", sqlConnection);
                                    dataReader = command1.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        nomera.Add(Convert.ToInt32(dataReader["Номер_студента_не_углту"]));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (dataReader != null && !dataReader.IsClosed)
                                    {
                                        dataReader.Close();
                                    }
                                }
                                for (int i = 0; i < nomera.Count(); i++)
                                {

                                    SqlCommand command2 = new SqlCommand("select Курс From Курсы where Номер_курса where Номер_курса = (select Курс from Студенты_УГЛТУ where Номер_студента = @Номер)", sqlConnection);
                                    command2.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command2.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    gruppa = Convert.ToString(sqlReader["Курс"]);
                                    sqlReader.Close();
                                    await command2.ExecuteNonQueryAsync();

                                    SqlCommand command3 = new SqlCommand("select Номер_общежития From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command3.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command3.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer1 = Convert.ToString(sqlReader["Номер_общежития"]);
                                    sqlReader.Close();
                                    await command3.ExecuteNonQueryAsync();

                                    SqlCommand command4 = new SqlCommand("select Блок From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command4.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command4.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer2 = Convert.ToString(sqlReader["Блок"]);
                                    sqlReader.Close();
                                    await command4.ExecuteNonQueryAsync();

                                    SqlCommand command5 = new SqlCommand("select Комната From Общежития where Номер_студента_не_углту = @Номер", sqlConnection);
                                    command5.Parameters.AddWithValue("Номер", nomera[i]);
                                    sqlReader = await command5.ExecuteReaderAsync();
                                    await sqlReader.ReadAsync();
                                    nomer3 = Convert.ToString(sqlReader["Комната"]);
                                    sqlReader.Close();
                                    await command5.ExecuteNonQueryAsync();

                                    try
                                    {
                                        SqlCommand sqlCommand5 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = @Общага AND Блок = @Блок AND Комната = @Комната AND Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно' ", sqlConnection);
                                        sqlCommand5.Parameters.AddWithValue("Общага", nomer1);
                                        sqlCommand5.Parameters.AddWithValue("Блок", nomer2);
                                        sqlCommand5.Parameters.AddWithValue("Комната", nomer3);

                                        dataReader = sqlCommand5.ExecuteReader();
                                        ListViewItem item = null;

                                        while (dataReader.Read())
                                        {
                                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                            listView1.Items.Add(item);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        if (dataReader != null && !dataReader.IsClosed)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                nomera.Clear();
                            }

                            if (listView1.Items.Count == 0)
                            {
                                for (int i = -1; i < nomera.Count(); i++)
                                {
                                    DialogResult result = MessageBox.Show("Комната не найдена, вывести свободные комнаты четвертого общежития?", "Студенты не УГЛТУ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                                    if (result == DialogResult.Yes)
                                    {
                                        sqlConnection = new SqlConnection(connectionString);
                                        await sqlConnection.OpenAsync();
                                        try
                                        {
                                            SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE  Номер_общежития = 4 AND Блок%2=0 AND Статус = 'Свободно'", sqlConnection);

                                            dataReader = sqlCommand.ExecuteReader();
                                            ListViewItem item = null;
                                            while (dataReader.Read())
                                            {
                                                item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                                                listView1.Items.Add(item);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                        finally
                                        {
                                            if (dataReader != null && !dataReader.IsClosed)
                                            {
                                                dataReader.Close();
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

            }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (checkBoxDeti.Checked == true)
            {
                DateTime dateZ = dateTimePicker1.Value.Date;
                DateTime dateV = dateTimePicker2.Value.Date;

                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand("UPDATE Общежития SET Номер_студента_не_углту = @Nomer, Дата_заселения = @DateZ, Статус = 'Занято', Дата_выселения = @DateV WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
                command.Parameters.AddWithValue("Nomer", textBoxNomer.Text);
                command.Parameters.AddWithValue("DateZ", dateZ);
                command.Parameters.AddWithValue("DateV", dateV);
                command.Parameters.AddWithValue("Obsh", NomerObsh.Text);
                command.Parameters.AddWithValue("Blok", Blok.Text);
                command.Parameters.AddWithValue("Komnata", Komnata.Text);
                command.Parameters.AddWithValue("Mesto", Mesto.Text);
                command.Parameters.AddWithValue("Mesto", Convert.ToInt32(Mesto.Text) + 1);
                await command.ExecuteNonQueryAsync();

              
            }
            else if (textBoxFamSupr.Text != "")

            {
                SqlDataReader sqlReader = null;

                int nomersupr;
                DateTime dateZ = dateTimePicker1.Value.Date;
                DateTime dateV = dateTimePicker2.Value.Date;

                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();

                SqlCommand command1 = new SqlCommand("select [Супруг/Супруга] from  dbo.Студенты_не_УГЛТУ WHERE Номер_студента = @Nomer2", sqlConnection);
                command1.Parameters.AddWithValue("Nomer2", textBoxNomer.Text);
                sqlReader = await command1.ExecuteReaderAsync();
                await sqlReader.ReadAsync();
                nomersupr = Convert.ToInt32(sqlReader["Супруг/Супруга"]);
                sqlReader.Close();
                await command1.ExecuteNonQueryAsync();

                SqlCommand command = new SqlCommand("UPDATE Общежития SET Номер_студента_не_углту = @Nomer, Дата_заселения = @DateZ, Статус = 'Занято', Дата_выселения = @DateV WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
                command.Parameters.AddWithValue("Nomer", textBoxNomer.Text);
                command.Parameters.AddWithValue("DateZ", dateZ);
                command.Parameters.AddWithValue("DateV", dateV);
                command.Parameters.AddWithValue("Obsh", NomerObsh.Text);
                command.Parameters.AddWithValue("Blok", Blok.Text);
                command.Parameters.AddWithValue("Komnata", Komnata.Text);
                command.Parameters.AddWithValue("Mesto", Mesto.Text);
                await command.ExecuteNonQueryAsync();


                SqlCommand command2 = new SqlCommand("UPDATE Общежития SET Дата_заселения = @DateZ, Статус = 'Занято', Номер_супруга = @NomerSupr, Дата_выселения = @DateV WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
                command2.Parameters.AddWithValue("DateZ", dateZ);
                command2.Parameters.AddWithValue("DateV", dateV);
                command2.Parameters.AddWithValue("Obsh", NomerObsh.Text);
                command2.Parameters.AddWithValue("Blok", Blok.Text);
                command2.Parameters.AddWithValue("Komnata", Komnata.Text);
                command2.Parameters.AddWithValue("Mesto", Convert.ToInt32(Mesto.Text) + 1);
                command2.Parameters.AddWithValue("NomerSupr", nomersupr);
                await command2.ExecuteNonQueryAsync();

            }
            else if (checkBoxDeti.Checked == false && textBoxFamSupr.Text == "")
            {
                DateTime dateZ = dateTimePicker1.Value.Date;
                DateTime dateV = dateTimePicker2.Value.Date;

                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand("UPDATE Общежития SET Номер_студента_не_углту = @Nomer, Дата_заселения = @DateZ, Статус = 'Занято', Дата_выселения = @DateV WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
                command.Parameters.AddWithValue("Nomer", textBoxNomer.Text);
                command.Parameters.AddWithValue("DateZ", dateZ);
                command.Parameters.AddWithValue("DateV", dateV);
                command.Parameters.AddWithValue("Obsh", NomerObsh.Text);
                command.Parameters.AddWithValue("Blok", Blok.Text);
                command.Parameters.AddWithValue("Komnata", Komnata.Text);
                command.Parameters.AddWithValue("Mesto", Mesto.Text);
                await command.ExecuteNonQueryAsync();
            }
            
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                word = new Word.Application();
                word.Visible = true;
                doc = word.Documents.Add();
                Word.Selection currentSelection = word.Application.Selection;

                string text;
                int cur_pos;
                string day = DateTime.Now.ToLongDateString();

                text = "ДОГОВОР №";
                currentSelection.TypeText(text + textBoxNomer.Text);
                cur_pos = text.Length + textBoxNomer.Text.Length;
                r = doc.Range(0, cur_pos);

                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 12;
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                currentSelection.TypeParagraph();
                cur_pos = cur_pos + 1;

                text = "г. Екатеринбург                                                                                                                                       ";
                currentSelection.TypeText(text + day);
                r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + day.Length + 1;
                currentSelection.TypeParagraph();

                text = "Федеральное государственное бюджетное образовательное учреждение высшего образования «Уральский государственный лесотехнический университет», именуемый в дальнейшем \"Наймодатель\", в лице проректора по РИК Ибатуллина Ш.Ш., действующего на основании доверенности от 25.05.2020 № 34, с одной стороны, и обучающийся ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length;

                text = textBoxFam.Text + " " + textBoxImya.Text + " " + textBoxOtch.Text;
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Font.Name = "Times New Roman";
                r.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;

                text = " именуемый в дальнейшем \"Наниматель\", с другой стороны, заключили настоящий договор (далее договор) о нижеследующем:";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Font.Name = "Times New Roman";
                r.Font.Underline = Word.WdUnderline.wdUnderlineNone;
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "1.	ПРЕДМЕТ ДОГОВОРА";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 12;
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                cur_pos = cur_pos + text.Length;
                currentSelection.TypeParagraph();

                text = "1.1. Наймодатель предоставляет Нанимателю во временное пользование койко-место в студенческом общежитии, находящемся по адресу: г. Екатеринбург, Сибирский тракт д.35б Общежитие - " + NomerObsh.Text + ", Блок - " + Blok.Text + ", Комната - " + Komnata.Text + ", Место - " + Mesto.Text + " (далее – жилое помещение). Основанием для заключения настоящего договора является зачисление Нанимателя для обучения в образовательное учреждение высшего образования (или профессиональное).";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "1.2. Срок найма устанавливается: с момента заключения настоящего договора и на весь период обучения, либо до момента окончании обучения, отчисления обучающегося.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "1.3. Настоящий договор является основанием для заселения Нанимателя в жилое помещение, указанное в п.1.1. настоящего договора.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "1.4. При заселении в жилое помещение Нанимателю предоставляются инвентарь и иное имущество во временное пользование согласно требованиям действующего законодательства.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.	ПРАВА И ОБЯЗАННОСТИ СТОРОН";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 12;
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1 Наниматель обязуется:";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.1. Соблюдать Жилищное законодательство РФ, Правила внутреннего распорядка в студенческом общежитии, Правила техники безопасности, Правила пожарной безопасности, выполнять условия настоящего договора, приказы и распоряжения ректора, распоряжения проректора и директора студенческого городка, требования иных локальных актов УГЛТУ.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.2.  Принимать посетителей в отведенное администрацией время с 08.00 до 23.00 часов.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.3. Своевременно вносить плату за проживание в общежитии и за все виды дополнительных платных услуг (если они предоставляются Нанимателю, и он ими пользуется).";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.4. Соблюдать чистоту и порядок в жилых помещениях и местах общего пользования; производить уборку в закрепленном жилом помещении ежедневно, влажную уборку помещения не реже одного раза в неделю, а на кухне – по установленному графику дежурств. Один раз в месяц участвовать в проводимом в общежитии «Санитарном Дне» с проведением генеральной уборки всех помещений (обметание стен и потолков, мытье полов, панелей, окон и дверей, оборудования, мебели, очистка от пыли и грязи отопительных приборов, светильников и т.д.).";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.5. Строго соблюдать инструкции по пользованию бытовыми электроприборами.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.6. Бережно относиться к помещениям, оборудованию и инвентарю в общежитии. Устранять за свой счет повреждения жилого помещения, мебели, а также производить замену поврежденного санитарно-технического и иного оборудования, вызванного его неправильной эксплуатацией или намеренной порчей.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.7.  Экономно расходовать электроэнергию и воду. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.8. Обеспечить возможность осмотра жилой комнаты, где предоставлено койко-место, администрацией УГЛТУ в любое время, с целью контроля за соблюдением выполнения условий настоящего договора, соблюдения Правил внутреннего распорядка в студенческом общежитии, Правил пожарной безопасности, Правил техники безопасности, сохранности выданного имущества, для проведения профилактических и других видов работ.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.9. При замене дверного замка в комнате или использовании дубликата ключа заведующего общежитием, сдать(вернуть) дубликат ключа от занимаемого жилого помещения заведующему общежитием не позднее 2 рабочих дней с момента вселения (получения дубликата ключа).";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.10. Соблюдать требования морально-этических норм поведения при проживании в общежитии, поддерживать атмосферу доброжелательности и взаимного уважения, не допускать конфликтных ситуаций по отношению к другим нанимателям и работникам общежития.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.11.  Зарегистрироваться в паспортном столе, по месту пребывания в общежитии не позднее 3 (трех) дней с момента заключения настоящего договора.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.12. Своевременно информировать администрацию общежития о неудовлетворительном самочувствии для принятия своевременных мер по предупреждению распространения инфекционных заболеваний.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.13.  Освобождать занимаемое помещение на время ремонтных работ, карантина, работ по дезинфекции и дератизации, и пр. в установленные локальными актами сроки.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.14. Нести полную ответственность за действия и поведение приглашенных в общежитие гостей.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.15. При отчислении из образовательного учреждения (в том числе и по его окончании), при уходе в академический отпуск, при расторжении договора, освободить занимаемое жилое помещение в течение 3-х рабочих дней с даты отчисления, издания приказа о предоставлении академического отпуска и т.д.). Обязанность по освобождению помещения считается Нанимателем исполненной после прекращения пользования жилым помещением, сдачи заведующему общежитием полученного инвентаря, постельных принадлежностей, ключа от жилого помещения, пропуска для входа в общежитие.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.16. Не переоборудовать и не производить перепланировку помещений;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.17. Не переселяться самовольно из одной комнаты в другую, не переносить, не выносить имущество, принадлежащие Наймодателю, из одной комнаты в другую, не устанавливать мебель (диваны, кресла, кровати, серванты и т.д.), не принадлежащую Наймодателю;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.18. Не использовать в жилом помещении энергоемкие электрические приборы (обогреватели, калориферы, электроплиты, нагреватели, скороварки, иные приборы мощностью более 0,6 кВт);";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.19. Не выполнять в помещении работы или не совершать другие действия, создающие повышенный шум и вибрацию, нарушающие нормальные условия проживания в других жилых помещениях (с 23.00 до 07.00 пользование телевизорами, радиоприемниками, магнитофонами и другими громкоговорящими устройствами допускается лишь при условии уменьшения их слышимости до степени, не нарушающей покоя проживающих);";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.20. Не использовать неисправные и самодельные электрические приборы, и приборы, не имеющие маркировки завода-изготовителя;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.21. Не готовить пищу в занимаемом жилом помещении (приготовление пищи допускается только в специально предназначенных для этого кухнях);";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.22. Не наклеивать на стены (кроме специально отведенных для этой цели мест) объявления, расписания, листовки   и т.п.;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.23. Не проводить посторонних лиц в общежитие в нарушение установленного порядка, не оставлять их на ночь, не предоставлять жилую площадь для проживания другим лицам, в том числе проживающим в других комнатах общежития (нахождение в общежитии посторонних лиц (не проживающих в данном общежитии) с 23-00 до 08.00 часов запрещается);";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.24. Не организовывать в общежитии азартные игры и не принимать в них участие;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.25. Не выбрасывать из окон и балконов мусор и посторонние предметы, не засорять и не захламлять мусором и бытовыми отходами места общего пользования, а также прилегающую к общежитию территорию;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.26. Не проходить в общежитие и не находится на территории УГЛТУ в состоянии алкогольного, наркотического, токсического опьянения, приносить в общежитие, хранить, употреблять, распространять наркотические вещества, спиртные напитки (в том числе пиво и другие слабоалкогольные напитки);";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.27 Не курить табачные изделия (в т.ч. электронные сигареты, испарители, кальяны) в помещении общежития и на территории УГЛТУ (кроме специально отведенных мест для курения); ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.28. Не хранить, не применять и не распространять легковоспламеняющиеся вещества, использовать в помещениях источники открытого огня;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.29. Не содержать в общежитии домашних животных.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.30. Не использовать занимаемое помещение в коммерческих целях.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.1.31. При выезде из общежития на каникулярный или иной длительный период более 30 дней сдать заведующему общежитием полученный инвентарь, постельные принадлежности, ключи от жилого помещения.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.2. Наниматель имеет право:";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.2.1. В любое время расторгнуть настоящий договор, письменно предупредив об этом Наймодателя за 10 дней.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.2.2. Принимать добровольное участие в работах по поддержанию чистоты в общежитии, на прилегающей к общежитию территории и ее благоустройстве.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.3. Наймодатель обязуется:";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.3.1. Осуществлять поселение Нанимателя в общежитие в соответствии с действующим законодательством.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.3.2. Осуществлять контроль за соблюдением Нанимателем правил проживания и пользования общежитием.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.3.3. Предоставить в личное пользование Нанимателю постельные принадлежности и обеспечить замену постельного белья не реже 1 раза в 7 дней.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.4. Наймодатель имеет право:";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.4.1. Досрочно расторгнуть настоящий договор в случаях, предусмотренных настоящим договором, нормативными актами УГЛТУ и действующим законодательством РФ.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.4.2. Применять меры дисциплинарной ответственности (вплоть до отчисления из образовательного учреждения) в случае нарушения Нанимателем Правил внутреннего распорядка, Правил пожарной безопасности, Положения о студенческом общежитии. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.5. За сохранность документов, денег и ценных вещей Нанимателя Наймодатель ответственности не несет.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "2.6. Дополнительно к Правилам внутреннего распорядка, к Положению о студенческом общежитии УГЛТУ Наймодатель может издавать приказы, распоряжения, нормативные акты, направленные на обеспечение безопасности обучающихся и их проживание в общежитиях, обязательные для исполнения Нанимателем с момента размещения указанных актов на сайте, информационном стенде Наймодателя.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.	ПОРЯДОК РАСЧЕТОВ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 12;
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.1. Плата за проживание в общежитии производится в размере, определенном приказом Наймодателя на текущий учебный год. Поселение производится только при предоставлении квитанции об оплате за проживание.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.2. Плата за проживание в общежитии на момент заключения договора составляет за один месяц:";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- в отапливаемый период _______________ рублей за один месяц;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- в неотапливаемый период _____________ рублей за один месяц;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Плата за проживание включает в себя плату за пользование жилым помещением в размере __________________ и плату за коммунальные услуги. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Расчетный период платы за проживание в общежитии  - месяц.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.3.В течение срока действия настоящего договора плата за проживание в общежитии может быть пересмотрена университетом в одностороннем порядке в связи с изменением тарифов и нормативов. Размер платы за проживание в общежитии утверждается приказом ректора, который доводится до сведения Нанимателя в наглядной и доступной форме путем размещения на информационных стендах в студенческом общежитии и (или) размещения на сайте университета. Несвоевременное ознакомление Нанимателя с информацией об изменении стоимости или реквизитов Наймодателя не является основанием для исполнения настоящего договора по ранее установленным стоимости или реквизитам. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.4. Оплата за проживание в общежитии производится после подписания настоящего договора Нанимателем.  Наниматель производит оплату за проживание в общежитии ежемесячно до двадцать пятого числа месяца, предшествующего месяцу, за который вносится плата. Плата за проживание в общежитии может быть внесена вперед сразу за несколько расчетных периодов.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.5. После внесения платы за проживание в общежитии Наниматель в течение 3 (трех) рабочих дней обязан предоставить заведующему общежитием копии платежных документов об оплате. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.6. Плата за проживание в общежитии вносится Нанимателем за период с 01 сентября текущего года и по 31 августа следующего года или до даты освобождения Нанимателем занимаемого жилого помещения согласно п.2.1.15 настоящего договора. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.7. При проживании Нанимателя в общежитии в каникулярный период, на время учебной и/или производственной практики, сдачи-пересдачи экзаменов, каникул, предоставляемых после прохождения итоговой аттестации, плата за проживание в общежитии производится в полном объеме за все время проживания. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.8. При выезде Нанимателя на каникулярный период без освобождения Нанимателем занимаемого жилого помещения, при условии сдачи Нанимателем заведующему общежитием полученного инвентаря, постельных принадлежностей, ключей от жилого помещения (п.2.1.31 настоящего договора), плата за коммунальные услуги за этот период не взимается на основании письменного заявления Нанимателя. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.9. В случае предоставления Нанимателю академического отпуска, а также в случае направления Нанимателя на обучение в другую образовательную организацию за пределами г. Екатеринбурга на срок более 2 месяцев (программы двойного диплома, встроенное обучение, сетевое обучение, академическая мобильность и т.п.) действие договора приостанавливается на время нахождения Нанимателя в академическом отпуске или обучения в другой образовательной организации. Наниматель обязан освободить жилое помещение в течение 3-х рабочих дней с даты наступления соответствующего основания (издания приказа о предоставлении отпуска, направления на стажировку и т.д.). Перерасчет платы за проживание производится по письменному заявлению Нанимателя с полученной отметкой от заведующего общежитием, подтверждающей факт сдачи инвентаря, постельных принадлежностей, ключей от жилого помещения с даты сдачи Нанимателем инвентаря. Перерасчет производится с 1 числа месяца, следующего за месяцем предоставления заявления, согласованного в установленном порядке. В случае, когда Наниматель документально подтвердил невозможность освобождения жилого помещения на время академического отпуска (например, в случае, когда академический отпуск предоставлен в связи с необходимостью длительного лечения и само лечение производится в г. Екатеринбурге), Наниматель производит оплату всего периода проживания в общежитии во время академического отпуска. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "3.10. При выезде Нанимателя на иной (кроме каникулярного) длительный период более одного месяца на основании приказа ректора или распорядительного документа Минобрнауки РФ(ограничительные меры, подготовка и проведение спортивных и культурных мероприятий федерального или регионального уровня), без освобождения Нанимателем занимаемого жилого помещения, внесенная им вперед плата за проживание в общежитии, при условии сдачи Нанимателем заведующему общежитием полученного инвентаря, постельных принадлежностей, ключей от жилого помещения (п.2.1.31 настоящего договора), может быть пересчитана. Указанный перерасчет производится по письменному заявлению Нанимателя с полученной отметкой от заведующего общежитием, подтверждающей факт сдачи инвентаря, постельных принадлежностей, ключей от жилого помещения. Перерасчет производится с 1 числа месяца, следующего за месяцем предоставления заявления, согласованного в установленном порядке. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "4.	РАСТОРЖЕНИЕ ДОГОВОРА";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 12;
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "4.1.  Наймодатель вправе досрочно расторгнуть настоящий договор в порядке, установленном действующим законодательством, в случаях:";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- использовании Нанимателем жилого помещения не по назначению; ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- разрушения или повреждения помещений общежития Нанимателем или лицами, за действия которых они отвечают;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- отказа Нанимателя от регистрации по месту пребывания;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- систематического (2 и более раза) нарушения Правил внутреннего распорядка, Положения о студенческом общежитии, Правил пожарной безопасности, Санитарных норм и правил, приказов, распоряжений, нормативных актов УГЛТУ, неисполнения Нанимателем обязанностей, установленных договором, при нарушении прав и законных интересов соседей, которое делает невозможным проживание в одном помещении;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- отсутствия Нанимателя в общежитии без письменного предупреждения более двух месяцев;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- появления и нахождения в общежитии, на территории УГЛТУ, в состоянии алкогольного или наркотического опьянения, хранение, употребление, распространения спиртных напитков, курения табачных изделий, электронных сигарет, испарителей, кальянов;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "-  хранения, распространения, употребления наркотических средств;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- хранения в общежитии взрывчатых, химически опасных, токсических веществ, холодного, огнестрельного, газового, травматического оружия;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- по другим основаниям, предусмотренным нормативными актами УГЛТУ и законодательством РФ.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "4.2.  Действие настоящего договора прекращается (договор досрочно расторгается):";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "-  при отчислении Нанимателя из образовательного учреждения;";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "- в связи с отказом Нанимателя от проживания в общежитии на основании его личного заявления на имя ректора.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "4.3. Возврат денежных средств при досрочном расторжении настоящего договора производится по письменному заявлению Нанимателя с полученной отметкой от заведующего общежитием подтверждающей факт освобождения помещения с даты освобождения Нанимателем занимаемого им по настоящему договору жилого помещения.  Перерасчет производится с 1 числа месяца, следующего за месяцем предоставления заявления, согласованного в установленном порядке. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "4.4. Расторжение настоящего договора влечет за собой выселение Нанимателя без предоставления другого жилого помещения. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "4.5 В случае переселения из одного общежития в другое договор найма жилого помещения расторгается   и   заключается новый договор. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "5.	ПРОЧИЕ УСЛОВИЯ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 12;
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "5.1. Настоящий договор вступает в силу с момента его подписания обеими сторонами. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "5.2. К отношениям сторон, прямо не урегулированным в договоре, применяются положения действующего законодательства, локальных нормативных актов УГЛТУ. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "5.3. В случае нарушения Нанимателем срока внесения платы за проживание Наймодатель вправе требовать уплаты пени в размере 1/300 действующей ставки рефинансирования ЦБ РФ от не выплаченных в срок сумм за каждый день просрочки начиная со следующего дня после наступления установленного законодательством РФ срока оплаты по день фактической выплаты включительно.";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "5.4. В случае причинения вреда имуществу Наймодателя или третьим лицам Наниматель несет полную материальную ответственность. Стоимость поврежденного, уничтоженного имущества компенсируется в размере рыночной цены аналогичного имущества или в натуре. С даты возмещения вреда право собственности на возвратные материалы (остатки) уничтоженного имущества переходят к Нанимателю. ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 10;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "6.	РЕКВИЗИТЫ И ПОДПИСИ СТОРОН";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 12;
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                r = doc.Range(cur_pos, cur_pos);
                Word.Table t = doc.Tables.Add(r, 1, 2);
                t.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                t.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                text = "Наймодатель";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "ФГБОУ ВО «Уральский";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "государственный лесотехнический ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "университет»";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "620100 г. Екатеринбург, Сибирский трак, 37";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "ИНН 6662000973 КПП 668501001";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Получатель УФК по Свердловской ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "области (УГЛТУ л/сч. 20626Х45000)";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "БИК 046577001";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Банк Уральское ГУ Банка России";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = " р/с 40501810100002000002";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "ОКТМО  65701000";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Код дохода 00000000000000000130";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                currentSelection.TypeParagraph();

                text = "СОГЛАСОВАНО:";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Второй отдел: (только для юношей)";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Паспортный стол:";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Наймодатель";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                currentSelection.MoveRight();

                text = "Наймодатель";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 1;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Ф. " + textBoxFam.Text;
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;

                currentSelection.TypeParagraph();

                text = "И. " + textBoxImya.Text;
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                currentSelection.TypeParagraph();

                text = "О. " + textBoxOtch.Text;
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                currentSelection.TypeParagraph();

                text = "паспорт";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "серия                   №";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                currentSelection.TypeParagraph();
                currentSelection.TypeParagraph();

                text = "выдан";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Дата выдачи паспорта";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "ИНН";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Адрес регистрации по месту жительства: ";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Телефон";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "Наниматель";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();

                text = "_______________________________";
                currentSelection.TypeText(text);
                r = doc.Range(cur_pos, cur_pos + text.Length + 1);
                r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                r.Bold = 0;
                r.Font.Name = "Times New Roman";
                r.Font.Size = 14;
                cur_pos = cur_pos + text.Length + 1;
                currentSelection.TypeParagraph();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                word.Quit();

            }
            finally
            {
                word.Quit();
                word = null;
                doc = null;

            }

            if (textBoxFamSupr.Text != "")
            {

                try
                {
                    word1 = new Word.Application();
                    word1.Visible = true;
                    doc1 = word1.Documents.Add();
                    Word.Selection currentSelection1 = word1.Application.Selection;

                    string text1;
                    int cur_pos1;
                    string day1 = DateTime.Now.ToLongDateString();

                    text1 = "ДОГОВОР №";
                    currentSelection1.TypeText(text1 + label20.Text);
                    cur_pos1 = text1.Length + label20.Text.Length;
                    r1 = doc1.Range(0, cur_pos1);

                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 12;
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection1.TypeParagraph();
                    cur_pos1 = cur_pos1 + 1;

                    text1 = "г. Екатеринбург                                                                                                                                       ";
                    currentSelection1.TypeText(text1 + day1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + day1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + day1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Федеральное государственное бюджетное образовательное учреждение высшего образования «Уральский государственный лесотехнический университет», именуемый в дальнейшем \"Наймодатель\", в лице проректора по РИК Ибатуллина Ш.Ш., действующего на основании доверенности от 25.05.2020 № 34, с одной стороны, и обучающийся ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length;

                    text1 = textBoxFamSupr.Text + " " + textBoxImyaSupr.Text + " " + textBoxOtchSupr.Text;
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;

                    text1 = " именуемый в дальнейшем \"Наниматель\", с другой стороны, заключили настоящий договор (далее договор) о нижеследующем:";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Underline = Word.WdUnderline.wdUnderlineNone;
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "1.	ПРЕДМЕТ ДОГОВОРА";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 12;
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cur_pos1 = cur_pos1 + text1.Length;
                    currentSelection1.TypeParagraph();

                    text1 = "1.1. Наймодатель предоставляет Нанимателю во временное пользование койко-место в студенческом общежитии, находящемся по адресу: г. Екатеринбург, Сибирский тракт д.35б Общежитие - " + NomerObsh.Text + ", Блок - " + Blok.Text + ", Комната - " + Komnata.Text + ", Место - " + Convert.ToString(Convert.ToInt32(Mesto.Text) + 1) + " (далее – жилое помещение). Основанием для заключения настоящего договора является зачисление Нанимателя для обучения в образовательное учреждение высшего образования (или профессиональное).";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "1.2. Срок найма устанавливается: с момента заключения настоящего договора и на весь период обучения, либо до момента окончании обучения, отчисления обучающегося.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "1.3. Настоящий договор является основанием для заселения Нанимателя в жилое помещение, указанное в п.1.1. настоящего договора.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "1.4. При заселении в жилое помещение Нанимателю предоставляются инвентарь и иное имущество во временное пользование согласно требованиям действующего законодательства.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.	ПРАВА И ОБЯЗАННОСТИ СТОРОН";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 12;
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1 Наниматель обязуется:";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.1. Соблюдать Жилищное законодательство РФ, Правила внутреннего распорядка в студенческом общежитии, Правила техники безопасности, Правила пожарной безопасности, выполнять условия настоящего договора, приказы и распоряжения ректора, распоряжения проректора и директора студенческого городка, требования иных локальных актов УГЛТУ.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.2.  Принимать посетителей в отведенное администрацией время с 08.00 до 23.00 часов.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.3. Своевременно вносить плату за проживание в общежитии и за все виды дополнительных платных услуг (если они предоставляются Нанимателю, и он ими пользуется).";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.4. Соблюдать чистоту и порядок в жилых помещениях и местах общего пользования; производить уборку в закрепленном жилом помещении ежедневно, влажную уборку помещения не реже одного раза в неделю, а на кухне – по установленному графику дежурств. Один раз в месяц участвовать в проводимом в общежитии «Санитарном Дне» с проведением генеральной уборки всех помещений (обметание стен и потолков, мытье полов, панелей, окон и дверей, оборудования, мебели, очистка от пыли и грязи отопительных приборов, светильников и т.д.).";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.5. Строго соблюдать инструкции по пользованию бытовыми электроприборами.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.6. Бережно относиться к помещениям, оборудованию и инвентарю в общежитии. Устранять за свой счет повреждения жилого помещения, мебели, а также производить замену поврежденного санитарно-технического и иного оборудования, вызванного его неправильной эксплуатацией или намеренной порчей.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.7.  Экономно расходовать электроэнергию и воду. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.8. Обеспечить возможность осмотра жилой комнаты, где предоставлено койко-место, администрацией УГЛТУ в любое время, с целью контроля за соблюдением выполнения условий настоящего договора, соблюдения Правил внутреннего распорядка в студенческом общежитии, Правил пожарной безопасности, Правил техники безопасности, сохранности выданного имущества, для проведения профилактических и других видов работ.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.9. При замене дверного замка в комнате или использовании дубликата ключа заведующего общежитием, сдать(вернуть) дубликат ключа от занимаемого жилого помещения заведующему общежитием не позднее 2 рабочих дней с момента вселения (получения дубликата ключа).";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.10. Соблюдать требования морально-этических норм поведения при проживании в общежитии, поддерживать атмосферу доброжелательности и взаимного уважения, не допускать конфликтных ситуаций по отношению к другим нанимателям и работникам общежития.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.11.  Зарегистрироваться в паспортном столе, по месту пребывания в общежитии не позднее 3 (трех) дней с момента заключения настоящего договора.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.12. Своевременно информировать администрацию общежития о неудовлетворительном самочувствии для принятия своевременных мер по предупреждению распространения инфекционных заболеваний.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.13.  Освобождать занимаемое помещение на время ремонтных работ, карантина, работ по дезинфекции и дератизации, и пр. в установленные локальными актами сроки.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.14. Нести полную ответственность за действия и поведение приглашенных в общежитие гостей.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.15. При отчислении из образовательного учреждения (в том числе и по его окончании), при уходе в академический отпуск, при расторжении договора, освободить занимаемое жилое помещение в течение 3-х рабочих дней с даты отчисления, издания приказа о предоставлении академического отпуска и т.д.). Обязанность по освобождению помещения считается Нанимателем исполненной после прекращения пользования жилым помещением, сдачи заведующему общежитием полученного инвентаря, постельных принадлежностей, ключа от жилого помещения, пропуска для входа в общежитие.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.16. Не переоборудовать и не производить перепланировку помещений;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.17. Не переселяться самовольно из одной комнаты в другую, не переносить, не выносить имущество, принадлежащие Наймодателю, из одной комнаты в другую, не устанавливать мебель (диваны, кресла, кровати, серванты и т.д.), не принадлежащую Наймодателю;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.18. Не использовать в жилом помещении энергоемкие электрические приборы (обогреватели, калориферы, электроплиты, нагреватели, скороварки, иные приборы мощностью более 0,6 кВт);";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.19. Не выполнять в помещении работы или не совершать другие действия, создающие повышенный шум и вибрацию, нарушающие нормальные условия проживания в других жилых помещениях (с 23.00 до 07.00 пользование телевизорами, радиоприемниками, магнитофонами и другими громкоговорящими устройствами допускается лишь при условии уменьшения их слышимости до степени, не нарушающей покоя проживающих);";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.20. Не использовать неисправные и самодельные электрические приборы, и приборы, не имеющие маркировки завода-изготовителя;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.21. Не готовить пищу в занимаемом жилом помещении (приготовление пищи допускается только в специально предназначенных для этого кухнях);";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.22. Не наклеивать на стены (кроме специально отведенных для этой цели мест) объявления, расписания, листовки   и т.п.;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.23. Не проводить посторонних лиц в общежитие в нарушение установленного порядка, не оставлять их на ночь, не предоставлять жилую площадь для проживания другим лицам, в том числе проживающим в других комнатах общежития (нахождение в общежитии посторонних лиц (не проживающих в данном общежитии) с 23-00 до 08.00 часов запрещается);";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.24. Не организовывать в общежитии азартные игры и не принимать в них участие;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.25. Не выбрасывать из окон и балконов мусор и посторонние предметы, не засорять и не захламлять мусором и бытовыми отходами места общего пользования, а также прилегающую к общежитию территорию;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.26. Не проходить в общежитие и не находится на территории УГЛТУ в состоянии алкогольного, наркотического, токсического опьянения, приносить в общежитие, хранить, употреблять, распространять наркотические вещества, спиртные напитки (в том числе пиво и другие слабоалкогольные напитки);";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.27 Не курить табачные изделия (в т.ч. электронные сигареты, испарители, кальяны) в помещении общежития и на территории УГЛТУ (кроме специально отведенных мест для курения); ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.28. Не хранить, не применять и не распространять легковоспламеняющиеся вещества, использовать в помещениях источники открытого огня;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.29. Не содержать в общежитии домашних животных.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.30. Не использовать занимаемое помещение в коммерческих целях.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.1.31. При выезде из общежития на каникулярный или иной длительный период более 30 дней сдать заведующему общежитием полученный инвентарь, постельные принадлежности, ключи от жилого помещения.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.2. Наниматель имеет право:";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.2.1. В любое время расторгнуть настоящий договор, письменно предупредив об этом Наймодателя за 10 дней.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.2.2. Принимать добровольное участие в работах по поддержанию чистоты в общежитии, на прилегающей к общежитию территории и ее благоустройстве.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.3. Наймодатель обязуется:";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.3.1. Осуществлять поселение Нанимателя в общежитие в соответствии с действующим законодательством.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.3.2. Осуществлять контроль за соблюдением Нанимателем правил проживания и пользования общежитием.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.3.3. Предоставить в личное пользование Нанимателю постельные принадлежности и обеспечить замену постельного белья не реже 1 раза в 7 дней.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.4. Наймодатель имеет право:";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.4.1. Досрочно расторгнуть настоящий договор в случаях, предусмотренных настоящим договором, нормативными актами УГЛТУ и действующим законодательством РФ.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.4.2. Применять меры дисциплинарной ответственности (вплоть до отчисления из образовательного учреждения) в случае нарушения Нанимателем Правил внутреннего распорядка, Правил пожарной безопасности, Положения о студенческом общежитии. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.5. За сохранность документов, денег и ценных вещей Нанимателя Наймодатель ответственности не несет.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "2.6. Дополнительно к Правилам внутреннего распорядка, к Положению о студенческом общежитии УГЛТУ Наймодатель может издавать приказы, распоряжения, нормативные акты, направленные на обеспечение безопасности обучающихся и их проживание в общежитиях, обязательные для исполнения Нанимателем с момента размещения указанных актов на сайте, информационном стенде Наймодателя.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.	ПОРЯДОК РАСЧЕТОВ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 12;
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.1. Плата за проживание в общежитии производится в размере, определенном приказом Наймодателя на текущий учебный год. Поселение производится только при предоставлении квитанции об оплате за проживание.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.2. Плата за проживание в общежитии на момент заключения договора составляет за один месяц:";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- в отапливаемый период _______________ рублей за один месяц;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- в неотапливаемый период _____________ рублей за один месяц;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Плата за проживание включает в себя плату за пользование жилым помещением в размере __________________ и плату за коммунальные услуги. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Расчетный период платы за проживание в общежитии  - месяц.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.3.В течение срока действия настоящего договора плата за проживание в общежитии может быть пересмотрена университетом в одностороннем порядке в связи с изменением тарифов и нормативов. Размер платы за проживание в общежитии утверждается приказом ректора, который доводится до сведения Нанимателя в наглядной и доступной форме путем размещения на информационных стендах в студенческом общежитии и (или) размещения на сайте университета. Несвоевременное ознакомление Нанимателя с информацией об изменении стоимости или реквизитов Наймодателя не является основанием для исполнения настоящего договора по ранее установленным стоимости или реквизитам. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.4. Оплата за проживание в общежитии производится после подписания настоящего договора Нанимателем.  Наниматель производит оплату за проживание в общежитии ежемесячно до двадцать пятого числа месяца, предшествующего месяцу, за который вносится плата. Плата за проживание в общежитии может быть внесена вперед сразу за несколько расчетных периодов.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.5. После внесения платы за проживание в общежитии Наниматель в течение 3 (трех) рабочих дней обязан предоставить заведующему общежитием копии платежных документов об оплате. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.6. Плата за проживание в общежитии вносится Нанимателем за период с 01 сентября текущего года и по 31 августа следующего года или до даты освобождения Нанимателем занимаемого жилого помещения согласно п.2.1.15 настоящего договора. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.7. При проживании Нанимателя в общежитии в каникулярный период, на время учебной и/или производственной практики, сдачи-пересдачи экзаменов, каникул, предоставляемых после прохождения итоговой аттестации, плата за проживание в общежитии производится в полном объеме за все время проживания. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.8. При выезде Нанимателя на каникулярный период без освобождения Нанимателем занимаемого жилого помещения, при условии сдачи Нанимателем заведующему общежитием полученного инвентаря, постельных принадлежностей, ключей от жилого помещения (п.2.1.31 настоящего договора), плата за коммунальные услуги за этот период не взимается на основании письменного заявления Нанимателя. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.9. В случае предоставления Нанимателю академического отпуска, а также в случае направления Нанимателя на обучение в другую образовательную организацию за пределами г. Екатеринбурга на срок более 2 месяцев (программы двойного диплома, встроенное обучение, сетевое обучение, академическая мобильность и т.п.) действие договора приостанавливается на время нахождения Нанимателя в академическом отпуске или обучения в другой образовательной организации. Наниматель обязан освободить жилое помещение в течение 3-х рабочих дней с даты наступления соответствующего основания (издания приказа о предоставлении отпуска, направления на стажировку и т.д.). Перерасчет платы за проживание производится по письменному заявлению Нанимателя с полученной отметкой от заведующего общежитием, подтверждающей факт сдачи инвентаря, постельных принадлежностей, ключей от жилого помещения с даты сдачи Нанимателем инвентаря. Перерасчет производится с 1 числа месяца, следующего за месяцем предоставления заявления, согласованного в установленном порядке. В случае, когда Наниматель документально подтвердил невозможность освобождения жилого помещения на время академического отпуска (например, в случае, когда академический отпуск предоставлен в связи с необходимостью длительного лечения и само лечение производится в г. Екатеринбурге), Наниматель производит оплату всего периода проживания в общежитии во время академического отпуска. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "3.10. При выезде Нанимателя на иной (кроме каникулярного) длительный период более одного месяца на основании приказа ректора или распорядительного документа Минобрнауки РФ(ограничительные меры, подготовка и проведение спортивных и культурных мероприятий федерального или регионального уровня), без освобождения Нанимателем занимаемого жилого помещения, внесенная им вперед плата за проживание в общежитии, при условии сдачи Нанимателем заведующему общежитием полученного инвентаря, постельных принадлежностей, ключей от жилого помещения (п.2.1.31 настоящего договора), может быть пересчитана. Указанный перерасчет производится по письменному заявлению Нанимателя с полученной отметкой от заведующего общежитием, подтверждающей факт сдачи инвентаря, постельных принадлежностей, ключей от жилого помещения. Перерасчет производится с 1 числа месяца, следующего за месяцем предоставления заявления, согласованного в установленном порядке. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "4.	РАСТОРЖЕНИЕ ДОГОВОРА";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 12;
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "4.1.  Наймодатель вправе досрочно расторгнуть настоящий договор в порядке, установленном действующим законодательством, в случаях:";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- использовании Нанимателем жилого помещения не по назначению; ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- разрушения или повреждения помещений общежития Нанимателем или лицами, за действия которых они отвечают;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- отказа Нанимателя от регистрации по месту пребывания;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- систематического (2 и более раза) нарушения Правил внутреннего распорядка, Положения о студенческом общежитии, Правил пожарной безопасности, Санитарных норм и правил, приказов, распоряжений, нормативных актов УГЛТУ, неисполнения Нанимателем обязанностей, установленных договором, при нарушении прав и законных интересов соседей, которое делает невозможным проживание в одном помещении;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- отсутствия Нанимателя в общежитии без письменного предупреждения более двух месяцев;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- появления и нахождения в общежитии, на территории УГЛТУ, в состоянии алкогольного или наркотического опьянения, хранение, употребление, распространения спиртных напитков, курения табачных изделий, электронных сигарет, испарителей, кальянов;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "-  хранения, распространения, употребления наркотических средств;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- хранения в общежитии взрывчатых, химически опасных, токсических веществ, холодного, огнестрельного, газового, травматического оружия;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- по другим основаниям, предусмотренным нормативными актами УГЛТУ и законодательством РФ.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "4.2.  Действие настоящего договора прекращается (договор досрочно расторгается):";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "-  при отчислении Нанимателя из образовательного учреждения;";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "- в связи с отказом Нанимателя от проживания в общежитии на основании его личного заявления на имя ректора.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "4.3. Возврат денежных средств при досрочном расторжении настоящего договора производится по письменному заявлению Нанимателя с полученной отметкой от заведующего общежитием подтверждающей факт освобождения помещения с даты освобождения Нанимателем занимаемого им по настоящему договору жилого помещения.  Перерасчет производится с 1 числа месяца, следующего за месяцем предоставления заявления, согласованного в установленном порядке. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "4.4. Расторжение настоящего договора влечет за собой выселение Нанимателя без предоставления другого жилого помещения. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "4.5 В случае переселения из одного общежития в другое договор найма жилого помещения расторгается   и   заключается новый договор. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "5.	ПРОЧИЕ УСЛОВИЯ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 12;
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "5.1. Настоящий договор вступает в силу с момента его подписания обеими сторонами. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "5.2. К отношениям сторон, прямо не урегулированным в договоре, применяются положения действующего законодательства, локальных нормативных актов УГЛТУ. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "5.3. В случае нарушения Нанимателем срока внесения платы за проживание Наймодатель вправе требовать уплаты пени в размере 1/300 действующей ставки рефинансирования ЦБ РФ от не выплаченных в срок сумм за каждый день просрочки начиная со следующего дня после наступления установленного законодательством РФ срока оплаты по день фактической выплаты включительно.";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "5.4. В случае причинения вреда имуществу Наймодателя или третьим лицам Наниматель несет полную материальную ответственность. Стоимость поврежденного, уничтоженного имущества компенсируется в размере рыночной цены аналогичного имущества или в натуре. С даты возмещения вреда право собственности на возвратные материалы (остатки) уничтоженного имущества переходят к Нанимателю. ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 10;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "6.	РЕКВИЗИТЫ И ПОДПИСИ СТОРОН";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 12;
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    r1 = doc1.Range(cur_pos1, cur_pos1);
                    Word.Table t1 = doc1.Tables.Add(r1, 1, 2);
                    t1.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    t1.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                    text1 = "Наймодатель";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "ФГБОУ ВО «Уральский";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "государственный лесотехнический ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "университет»";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "620100 г. Екатеринбург, Сибирский трак, 37";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "ИНН 6662000973 КПП 668501001";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Получатель УФК по Свердловской ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "области (УГЛТУ л/сч. 20626Х45000)";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "БИК 046577001";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Банк Уральское ГУ Банка России";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = " р/с 40501810100002000002";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "ОКТМО  65701000";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Код дохода 00000000000000000130";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    currentSelection1.TypeParagraph();

                    text1 = "СОГЛАСОВАНО:";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Второй отдел: (только для юношей)";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Паспортный стол:";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Наймодатель";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    currentSelection1.MoveRight();

                    text1 = "Наймодатель";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 1;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Ф. " + textBoxFamSupr.Text;
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;

                    currentSelection1.TypeParagraph();

                    text1 = "И. " + textBoxImyaSupr.Text;
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    currentSelection1.TypeParagraph();

                    text1 = "О. " + textBoxOtchSupr.Text;
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    currentSelection1.TypeParagraph();

                    text1 = "паспорт";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "серия                   №";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    currentSelection1.TypeParagraph();
                    currentSelection1.TypeParagraph();

                    text1 = "выдан";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Дата выдачи паспорта";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "ИНН";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Адрес регистрации по месту жительства: ";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Телефон";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "Наниматель";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                    text1 = "_______________________________";
                    currentSelection1.TypeText(text1);
                    r1 = doc1.Range(cur_pos1, cur_pos1 + text1.Length + 1);
                    r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    r1.Bold = 0;
                    r1.Font.Name = "Times New Roman";
                    r1.Font.Size = 14;
                    cur_pos1 = cur_pos1 + text1.Length + 1;
                    currentSelection1.TypeParagraph();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    word1.Quit();
                }
                finally
                {
                    word1.Quit();
                    word1 = null;
                    doc1 = null;

                }

                MessageBox.Show("Данные успешно внесены", "Заселение", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                DialogResult result = MessageBox.Show("Очистить форму?", "Заселение", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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
                    listView1.Items.Clear();
                    NomerObsh.Text = "";
                    Blok.Text = "";
                    Komnata.Text = "";
                    Mesto.Text = "";
                    textBoxSex.Text = "";
                    textBoxDataRozhd.Text = "";
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();

                SqlDataReader dataReader = null;

                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 4 AND Статус = 'Свободно'", sqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;
                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_общежития"]), Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
                        listView1.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader != null && !dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            
        }
    }

}
