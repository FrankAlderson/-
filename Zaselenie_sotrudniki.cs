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
    public partial class Zaselenie_sotrudniki : Form
    {
        SqlConnection sqlConnection;
        SqlConnection sqlConnection2;

        public Zaselenie_sotrudniki()
        {
            InitializeComponent();
            groupBoxDopInfo.Visible = false;
            tableLayoutPanel1.Location = new Point(66, 155);
            groupBox1.Location = new Point(66, 400);
        }

        Word.Application word;
        Word.Document doc;
        Word.Range r;
        Word.Application word1;
        Word.Document doc1;
        Word.Range r1;

        private void visibledopinfi(object sender, EventArgs e)
        {
            if (checkBoxDopInfo.Checked == true)
            {
                groupBoxDopInfo.Visible = true;
                tableLayoutPanel1.Location = new Point(66, 274);
                groupBox1.Location = new Point(66, 513);

            }
            else
            {
                groupBoxDopInfo.Visible = false;
                tableLayoutPanel1.Location = new Point(66, 155);
                groupBox1.Location = new Point(66, 400);
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
                SqlCommand command13 = new SqlCommand("SELECT Имя FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Сотрудники Where Номер_сотрудника = @Номер_сотрудника)", sqlConnection);
                command13.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
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
                SqlCommand command14 = new SqlCommand("SELECT Отчество FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Сотрудники Where Номер_сотрудника = @Номер_сотрудника)", sqlConnection);
                command14.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
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
                SqlCommand command14 = new SqlCommand("SELECT Номер_супруга FROM Супруги WHERE Номер_супруга = (Select [Супруг/Супруга] From Сотрудники Where Номер_сотрудника = @Номер_сотрудника)", sqlConnection);
                command14.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);
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



            List<int> nomera2 = new List<int>();

            string connectionString2 = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection2 = new SqlConnection(connectionString2);

            SqlDataReader sqlRead = null;

            await sqlConnection2.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT Номер_сотрудника FROM Общежития WHERE Номер_сотрудника = @Номер_сотрудника", sqlConnection2);
                com.Parameters.AddWithValue("Номер_сотрудника", textBoxNomer.Text);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera2.Add(Convert.ToInt32(sqlRead["Номер_сотрудника"]));
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
                MessageBox.Show("Данный сотрудник уже проживает в общежитии");
            }

            else if (nomera2.Count == 0)
            {
                listView1.Items.Clear();

                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();

                SqlDataReader dataReader = null;

                try
                {
                    SqlCommand sqlCommand3 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 1 AND (Блок = 10 or Блок = 11 or Блок = 12) AND Статус = 'Свободно'", sqlConnection);

                    dataReader = sqlCommand3.ExecuteReader();
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
        private async void button2_Click(object sender, EventArgs e)
        {
            if (checkBoxDeti.Checked == true)
            {
                DateTime dateZ = dateTimePicker1.Value.Date;
                DateTime dateV = dateTimePicker2.Value.Date;

                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand("UPDATE Общежития SET Номер_сотрудника = @Nomer, Дата_заселения = @DateZ, Статус = 'Занято', Дата_выселения = @DateV WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
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

                SqlCommand command1 = new SqlCommand("select [Супруг/Супруга] from  dbo.Сотрудники WHERE Номер_сотрудника = @Nomer2", sqlConnection);
                command1.Parameters.AddWithValue("Nomer2", textBoxNomer.Text);
                sqlReader = await command1.ExecuteReaderAsync();
                await sqlReader.ReadAsync();
                nomersupr = Convert.ToInt32(sqlReader["Супруг/Супруга"]);
                sqlReader.Close();
                await command1.ExecuteNonQueryAsync();

                SqlCommand command = new SqlCommand("UPDATE Общежития SET Номер_сотрудника = @Nomer, Дата_заселения = @DateZ, Статус = 'Занято', Дата_выселения = @DateV WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
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

                SqlCommand command = new SqlCommand("UPDATE Общежития SET Номер_сотрудника = @Nomer, Дата_заселения = @DateZ, Статус = 'Занято', Дата_выселения = @DateV WHERE Номер_общежития = @Obsh AND Блок = @Blok AND Комната = @Komnata AND Место = @Mesto", sqlConnection);
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
                listView1.Items.Clear();
                NomerObsh.Text = "";
                Blok.Text = "";
                Komnata.Text = "";
                Mesto.Text = "";
                textBoxDataRozhd.Text = "";
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
                SqlCommand sqlCommand3 = new SqlCommand("SELECT Номер_общежития, Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 1 AND (Блок = 10 or Блок = 11 or Блок = 12) AND Статус = 'Свободно'", sqlConnection);

                dataReader = sqlCommand3.ExecuteReader();
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
