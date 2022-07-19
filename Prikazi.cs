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


namespace DIPLOM_V2
{
    public partial class Prikazi : Form
    {
        SqlConnection sqlConnection;
        public Prikazi()
        {
            InitializeComponent();
        }

        Word.Application word;
        Word.Document doc;
        Word.Range r;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            List<int> nomera = new List<int>();

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlRead = null;

            SqlDataReader dataReader = null;
            SqlDataReader dataReader2 = null;

            await sqlConnection.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_студента_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_УГЛТУ ON dbo.Общежития.Номер_студента_углту = dbo.Студенты_УГЛТУ.Номер_студента WHERE (dbo.Студенты_УГЛТУ.Приказ = N'Нет') AND (dbo.Студенты_УГЛТУ.Институт = 1) AND (dbo.Студенты_УГЛТУ.[Супруг/Супруга] IS NULL) AND (dbo.Студенты_УГЛТУ.Дети = 0)", sqlConnection);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera.Add(Convert.ToInt32(sqlRead["Номер_студента_углту"]));
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

            for (int i = 0; i < nomera.Count(); i++)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_студента, Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;

                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_студента"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
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

            if (nomera.Count == 0)
            {
                MessageBox.Show("К сожалению для приказа нет людей");
            }
            else
            {
                DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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

                    text = "Министерство науки и высшего образования Российской Федерации";
                    cur_pos = text.Length;
                    currentSelection.TypeText(text);
                    r = doc.Range(0, cur_pos);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + 1;

                    text = "Федеральное государственное бюджетное";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "образовательное учреждение высшего образования";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "«Уральский государственный лесотехнический университет»";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "(УГЛТУ)";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    cur_pos = cur_pos + text.Length + 2;
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();

                    text = "ПРИКАЗ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "___________________________                                                  №___________________________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "г. Екатеринбург";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "О поселении студентов Института 1";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "в общежития студгородка УГЛТУ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "ПРИКАЗЫВАЮ:";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "Поселить в общежитие №2 на 2020-2021 учебный год с ";
                    currentSelection.TypeText(text + day);
                    r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    cur_pos = cur_pos + text.Length + day.Length + 1;

                    text = " в счет мест Института 1 следующих студентов, с обязательным заключением договора найма жилого помещения в общежитии.";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "Группа    П.П.   Фамилия Имя Отчество";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    string gruppa;
                    string fam;
                    string im;
                    string ot;
                    int q = 1;

                    for (int i = 0; i < nomera.Count(); i++)
                    {

                        SqlCommand sqlCommand = new SqlCommand("SELECT Группа FROM Группы WHERE Номер_группы = (SELECT Группа FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер)", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader = await sqlCommand.ExecuteReaderAsync();
                        await dataReader.ReadAsync();
                        gruppa = Convert.ToString(dataReader["Группа"]);
                        dataReader.Close();
                        await sqlCommand.ExecuteNonQueryAsync();

                        SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                        sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                        await dataReader2.ReadAsync();
                        fam = Convert.ToString(dataReader2["Фамилия"]);
                        im = Convert.ToString(dataReader2["Имя"]);
                        ot = Convert.ToString(dataReader2["Отчество"]);
                        dataReader2.Close();
                        await sqlCommand2.ExecuteNonQueryAsync();

                        q = q + i;

                        text = gruppa + "  " + "№" + q + "  " + fam + " " + im + " " + ot;

                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                      

                    }
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();
                    text = "        Ректор                                          ______________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                   

                    for (int i = 0; i < nomera.Count(); i++)
                    {
                        string daytext = Convert.ToString(day);

                        SqlCommand command = new SqlCommand("UPDATE Студенты_УГЛТУ SET Приказ = @day WHERE Номер_студента=@Номер", sqlConnection);
                        command.Parameters.AddWithValue("Номер", nomera[i]);
                        command.Parameters.AddWithValue("day", daytext);
                        await command.ExecuteNonQueryAsync();

                    }

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
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)

        {
            listView1.Items.Clear();

            List<int> nomera = new List<int>();

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlRead = null;

            SqlDataReader dataReader = null;
            SqlDataReader dataReader2 = null;

            await sqlConnection.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_студента_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_УГЛТУ ON dbo.Общежития.Номер_студента_углту = dbo.Студенты_УГЛТУ.Номер_студента WHERE (dbo.Студенты_УГЛТУ.Приказ = N'Нет') AND (dbo.Студенты_УГЛТУ.Институт = 2) AND (dbo.Студенты_УГЛТУ.[Супруг/Супруга] IS NULL) AND (dbo.Студенты_УГЛТУ.Дети = 0)", sqlConnection);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera.Add(Convert.ToInt32(sqlRead["Номер_студента_углту"]));
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

            for (int i = 0; i < nomera.Count(); i++)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_студента, Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;

                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_студента"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
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

            if (nomera.Count == 0)
            {
                MessageBox.Show("К сожалению для приказа нет людей");
            }
            else
            {
                DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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

                        text = "Министерство науки и высшего образования Российской Федерации";
                        cur_pos = text.Length;
                        currentSelection.TypeText(text);
                        r = doc.Range(0, cur_pos);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + 1;

                        text = "Федеральное государственное бюджетное";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "образовательное учреждение высшего образования";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "«Уральский государственный лесотехнический университет»";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "(УГЛТУ)";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                        cur_pos = cur_pos + text.Length + 2;
                        currentSelection.TypeParagraph();
                        currentSelection.TypeParagraph();

                        text = "ПРИКАЗ";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "___________________________                                                  №___________________________";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 0;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "г. Екатеринбург";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "О поселении студентов Института 2";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 10;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "в общежития студгородка УГЛТУ";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 10;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "ПРИКАЗЫВАЮ:";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "Поселить в общежитие №3 на 2020-2021 учебный год с ";
                        currentSelection.TypeText(text + day);
                        r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                        r.Bold = 0;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        cur_pos = cur_pos + text.Length + day.Length + 1;

                        text = " в счет мест Института 2 следующих студентов, с обязательным заключением договора найма жилого помещения в общежитии.";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "Группа    П.П.   Фамилия Имя Отчество";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        string gruppa;
                        string fam;
                        string im;
                        string ot;
                        int q = 1;

                        for (int i = 0; i < nomera.Count(); i++)
                        {

                            SqlCommand sqlCommand = new SqlCommand("SELECT Группа FROM Группы WHERE Номер_группы = (SELECT Группа FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер)", sqlConnection);
                            sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);
                            dataReader = await sqlCommand.ExecuteReaderAsync();
                            await dataReader.ReadAsync();
                            gruppa = Convert.ToString(dataReader["Группа"]);
                            dataReader.Close();
                            await sqlCommand.ExecuteNonQueryAsync();

                            SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                            sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                            dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                            await dataReader2.ReadAsync();
                            fam = Convert.ToString(dataReader2["Фамилия"]);
                            im = Convert.ToString(dataReader2["Имя"]);
                            ot = Convert.ToString(dataReader2["Отчество"]);
                            dataReader2.Close();
                            await sqlCommand2.ExecuteNonQueryAsync();

                            q = q + i;

                            text = gruppa + "  " + "№" + q + "  " + fam + " " + im + " " + ot;

                            currentSelection.TypeText(text);
                            r = doc.Range(cur_pos, cur_pos + text.Length);
                            r.Font.Name = "Times New Roman";
                            r.Font.Size = 12;
                            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            currentSelection.TypeParagraph();
                            cur_pos = cur_pos + text.Length + 1;



                        }
                        currentSelection.TypeParagraph();
                        currentSelection.TypeParagraph();
                        text = "        Ректор                                          ______________";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;



                        for (int i = 0; i < nomera.Count(); i++)
                        {
                            string daytext = Convert.ToString(day);

                            SqlCommand command = new SqlCommand("UPDATE Студенты_УГЛТУ SET Приказ = @day WHERE Номер_студента=@Номер", sqlConnection);
                            command.Parameters.AddWithValue("Номер", nomera[i]);
                            command.Parameters.AddWithValue("day", daytext);
                            await command.ExecuteNonQueryAsync();

                        }

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
                }
            }
        }

        private async void button4_Click(object sender, EventArgs e)
       

            {
                listView1.Items.Clear();

                List<int> nomera = new List<int>();

                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                SqlDataReader sqlRead = null;

                SqlDataReader dataReader = null;
                SqlDataReader dataReader2 = null;

                await sqlConnection.OpenAsync();

                try
                {
                    SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_студента_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_УГЛТУ ON dbo.Общежития.Номер_студента_углту = dbo.Студенты_УГЛТУ.Номер_студента WHERE (dbo.Студенты_УГЛТУ.Приказ = N'Нет') AND (dbo.Студенты_УГЛТУ.Институт = 3) AND (dbo.Студенты_УГЛТУ.[Супруг/Супруга] IS NULL) AND (dbo.Студенты_УГЛТУ.Дети = 0)", sqlConnection);

                    sqlRead = await com.ExecuteReaderAsync();
                    while (sqlRead.Read())
                    {

                        nomera.Add(Convert.ToInt32(sqlRead["Номер_студента_углту"]));
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

                for (int i = 0; i < nomera.Count(); i++)
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand("SELECT Номер_студента, Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                        dataReader = sqlCommand.ExecuteReader();
                        ListViewItem item = null;

                        while (dataReader.Read())
                        {
                            item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_студента"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
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

                if (nomera.Count == 0)
                {
                    MessageBox.Show("К сожалению для приказа нет студентов");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    if (result == DialogResult.Yes)
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

                        text = "Министерство науки и высшего образования Российской Федерации";
                        cur_pos = text.Length;
                        currentSelection.TypeText(text);
                        r = doc.Range(0, cur_pos);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + 1;

                        text = "Федеральное государственное бюджетное";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "образовательное учреждение высшего образования";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "«Уральский государственный лесотехнический университет»";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "(УГЛТУ)";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                        cur_pos = cur_pos + text.Length + 2;
                        currentSelection.TypeParagraph();
                        currentSelection.TypeParagraph();

                        text = "ПРИКАЗ";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "___________________________                                                  №___________________________";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 0;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "г. Екатеринбург";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "О поселении студентов Института 3";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 10;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "в общежития студгородка УГЛТУ";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 10;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "ПРИКАЗЫВАЮ:";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "Поселить в общежитие №5 на 2020-2021 учебный год с ";
                        currentSelection.TypeText(text + day);
                        r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                        r.Bold = 0;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        cur_pos = cur_pos + text.Length + day.Length + 1;

                        text = " в счет мест Института 3 следующих студентов, с обязательным заключением договора найма жилого помещения в общежитии.";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "Группа    П.П.   Фамилия Имя Отчество";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        string gruppa;
                        string fam;
                        string im;
                        string ot;
                        int q = 1;

                        for (int i = 0; i < nomera.Count(); i++)
                        {

                            SqlCommand sqlCommand = new SqlCommand("SELECT Группа FROM Группы WHERE Номер_группы = (SELECT Группа FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер)", sqlConnection);
                            sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);
                            dataReader = await sqlCommand.ExecuteReaderAsync();
                            await dataReader.ReadAsync();
                            gruppa = Convert.ToString(dataReader["Группа"]);
                            dataReader.Close();
                            await sqlCommand.ExecuteNonQueryAsync();

                            SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                            sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                            dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                            await dataReader2.ReadAsync();
                            fam = Convert.ToString(dataReader2["Фамилия"]);
                            im = Convert.ToString(dataReader2["Имя"]);
                            ot = Convert.ToString(dataReader2["Отчество"]);
                            dataReader2.Close();
                            await sqlCommand2.ExecuteNonQueryAsync();

                            q = q + i;

                            text = gruppa + "  " + "№" + q + "  " + fam + " " + im + " " + ot;

                            currentSelection.TypeText(text);
                            r = doc.Range(cur_pos, cur_pos + text.Length);
                            r.Font.Name = "Times New Roman";
                            r.Font.Size = 12;
                            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            currentSelection.TypeParagraph();
                            cur_pos = cur_pos + text.Length + 1;



                        }
                        currentSelection.TypeParagraph();
                        currentSelection.TypeParagraph();
                        text = "        Ректор                                          ______________";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                    

                        for (int i = 0; i < nomera.Count(); i++)
                        {
                            string daytext = Convert.ToString(day);

                            SqlCommand command = new SqlCommand("UPDATE Студенты_УГЛТУ SET Приказ = @day WHERE Номер_студента=@Номер", sqlConnection);
                            command.Parameters.AddWithValue("Номер", nomera[i]);
                            command.Parameters.AddWithValue("day", daytext);
                            await command.ExecuteNonQueryAsync();

                        }
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
                }
                }
            }

        private async void button5_Click(object sender, EventArgs e)

        {
            listView1.Items.Clear();

            List<int> nomera = new List<int>();

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlRead = null;

            SqlDataReader dataReader = null;
            SqlDataReader dataReader2 = null;

            await sqlConnection.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_студента_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_УГЛТУ ON dbo.Общежития.Номер_студента_углту = dbo.Студенты_УГЛТУ.Номер_студента WHERE (dbo.Студенты_УГЛТУ.Приказ = N'Нет') AND (dbo.Студенты_УГЛТУ.Институт = 4) AND (dbo.Студенты_УГЛТУ.[Супруг/Супруга] IS NULL) AND (dbo.Студенты_УГЛТУ.Дети = 0)", sqlConnection);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera.Add(Convert.ToInt32(sqlRead["Номер_студента_углту"]));
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

            for (int i = 0; i < nomera.Count(); i++)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_студента, Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;

                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_студента"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
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

            if (nomera.Count == 0)
            {
                MessageBox.Show("К сожалению для приказа нет студентов");
            }
            else
            {
                DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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

                    text = "Министерство науки и высшего образования Российской Федерации";
                    cur_pos = text.Length;
                    currentSelection.TypeText(text);
                    r = doc.Range(0, cur_pos);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + 1;

                    text = "Федеральное государственное бюджетное";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "образовательное учреждение высшего образования";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "«Уральский государственный лесотехнический университет»";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "(УГЛТУ)";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    cur_pos = cur_pos + text.Length + 2;
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();

                    text = "ПРИКАЗ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "___________________________                                                  №___________________________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "г. Екатеринбург";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "О поселении студентов Института 4";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "в общежития студгородка УГЛТУ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "ПРИКАЗЫВАЮ:";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "Поселить в общежитие №6 на 2020-2021 учебный год с ";
                    currentSelection.TypeText(text + day);
                    r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    cur_pos = cur_pos + text.Length + day.Length + 1;

                    text = " в счет мест Института 4 следующих студентов, с обязательным заключением договора найма жилого помещения в общежитии.";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "Группа    П.П.   Фамилия Имя Отчество";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    string gruppa;
                    string fam;
                    string im;
                    string ot;
                    int q = 1;

                    for (int i = 0; i < nomera.Count(); i++)
                    {

                        SqlCommand sqlCommand = new SqlCommand("SELECT Группа FROM Группы WHERE Номер_группы = (SELECT Группа FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер)", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader = await sqlCommand.ExecuteReaderAsync();
                        await dataReader.ReadAsync();
                        gruppa = Convert.ToString(dataReader["Группа"]);
                        dataReader.Close();
                        await sqlCommand.ExecuteNonQueryAsync();

                        SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                        sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                        await dataReader2.ReadAsync();
                        fam = Convert.ToString(dataReader2["Фамилия"]);
                        im = Convert.ToString(dataReader2["Имя"]);
                        ot = Convert.ToString(dataReader2["Отчество"]);
                        dataReader2.Close();
                        await sqlCommand2.ExecuteNonQueryAsync();

                        q = q + i;

                        text = gruppa + "  " + "№" + q + "  " + fam + " " + im + " " + ot;

                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;



                    }
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();
                    text = "        Ректор                                          ______________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;



                    for (int i = 0; i < nomera.Count(); i++)
                    {
                        string daytext = Convert.ToString(day);

                        SqlCommand command = new SqlCommand("UPDATE Студенты_УГЛТУ SET Приказ = @day WHERE Номер_студента=@Номер", sqlConnection);
                        command.Parameters.AddWithValue("Номер", nomera[i]);
                        command.Parameters.AddWithValue("day", daytext);
                        await command.ExecuteNonQueryAsync();

                    }

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
                }
            }
        }

        private async void button6_Click(object sender, EventArgs e)

        {
            listView1.Items.Clear();

            List<int> nomera = new List<int>();

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlRead = null;

            SqlDataReader dataReader = null;
            SqlDataReader dataReader2 = null;

            await sqlConnection.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_студента_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_УГЛТУ ON dbo.Общежития.Номер_студента_углту = dbo.Студенты_УГЛТУ.Номер_студента WHERE (dbo.Студенты_УГЛТУ.Приказ = N'Нет') AND (dbo.Студенты_УГЛТУ.Институт = 5) AND (dbo.Студенты_УГЛТУ.[Супруг/Супруга] IS NULL) AND (dbo.Студенты_УГЛТУ.Дети = 0)", sqlConnection);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera.Add(Convert.ToInt32(sqlRead["Номер_студента_углту"]));
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

            for (int i = 0; i < nomera.Count(); i++)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_студента, Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;

                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_студента"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
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

            if (nomera.Count == 0)
            {
                MessageBox.Show("К сожалению для приказа нет студентов");
            }
            else
            {
                DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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

                    text = "Министерство науки и высшего образования Российской Федерации";
                    cur_pos = text.Length;
                    currentSelection.TypeText(text);
                    r = doc.Range(0, cur_pos);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + 1;

                    text = "Федеральное государственное бюджетное";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "образовательное учреждение высшего образования";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "«Уральский государственный лесотехнический университет»";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "(УГЛТУ)";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    cur_pos = cur_pos + text.Length + 2;
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();

                    text = "ПРИКАЗ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "___________________________                                                  №___________________________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "г. Екатеринбург";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "О поселении студентов Института 5";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "в общежития студгородка УГЛТУ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "ПРИКАЗЫВАЮ:";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "Поселить в общежитие №7 на 2020-2021 учебный год с ";
                    currentSelection.TypeText(text + day);
                    r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    cur_pos = cur_pos + text.Length + day.Length + 1;

                    text = " в счет мест Института 5 следующих студентов, с обязательным заключением договора найма жилого помещения в общежитии.";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "Группа    П.П.   Фамилия Имя Отчество";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    string gruppa;
                    string fam;
                    string im;
                    string ot;
                    int q = 1;

                    for (int i = 0; i < nomera.Count(); i++)
                    {

                        SqlCommand sqlCommand = new SqlCommand("SELECT Группа FROM Группы WHERE Номер_группы = (SELECT Группа FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер)", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader = await sqlCommand.ExecuteReaderAsync();
                        await dataReader.ReadAsync();
                        gruppa = Convert.ToString(dataReader["Группа"]);
                        dataReader.Close();
                        await sqlCommand.ExecuteNonQueryAsync();

                        SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                        sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                        await dataReader2.ReadAsync();
                        fam = Convert.ToString(dataReader2["Фамилия"]);
                        im = Convert.ToString(dataReader2["Имя"]);
                        ot = Convert.ToString(dataReader2["Отчество"]);
                        dataReader2.Close();
                        await sqlCommand2.ExecuteNonQueryAsync();

                        q = q + i;

                        text = gruppa + "  " + "№" + q + "  " + fam + " " + im + " " + ot;

                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;



                    }
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();
                    text = "        Ректор                                          ______________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;


                    for (int i = 0; i < nomera.Count(); i++)
                    {
                        string daytext = Convert.ToString(day);

                        SqlCommand command = new SqlCommand("UPDATE Студенты_УГЛТУ SET Приказ = @day WHERE Номер_студента=@Номер", sqlConnection);
                        command.Parameters.AddWithValue("Номер", nomera[i]);
                        command.Parameters.AddWithValue("day", daytext);
                        await command.ExecuteNonQueryAsync();

                    }

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
                }
            }
        }

        private async void button7_Click(object sender, EventArgs e)

        {
            listView1.Items.Clear();

            List<int> nomera = new List<int>();

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlRead = null;

            SqlDataReader dataReader = null;
            SqlDataReader dataReader2 = null;

            await sqlConnection.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_студента_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_УГЛТУ ON dbo.Общежития.Номер_студента_углту = dbo.Студенты_УГЛТУ.Номер_студента WHERE (dbo.Студенты_УГЛТУ.Приказ = N'Нет')", sqlConnection);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera.Add(Convert.ToInt32(sqlRead["Номер_студента_углту"]));
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

            for (int i = 0; i < nomera.Count(); i++)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_студента, Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;

                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_студента"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
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

            if (nomera.Count == 0)
            {
                MessageBox.Show("К сожалению для приказа нет студентов");
            }
            else
            {
                DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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

                    text = "Министерство науки и высшего образования Российской Федерации";
                    cur_pos = text.Length;
                    currentSelection.TypeText(text);
                    r = doc.Range(0, cur_pos);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + 1;

                    text = "Федеральное государственное бюджетное";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "образовательное учреждение высшего образования";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "«Уральский государственный лесотехнический университет»";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "(УГЛТУ)";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    cur_pos = cur_pos + text.Length + 2;
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();

                    text = "ПРИКАЗ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "___________________________                                                  №___________________________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "г. Екатеринбург";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "О поселении студентов";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "в общежития студгородка УГЛТУ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "ПРИКАЗЫВАЮ:";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "Поселить в общежитие №1 на 2020-2021 учебный год с ";
                    currentSelection.TypeText(text + day);
                    r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    cur_pos = cur_pos + text.Length + day.Length + 1;

                    text = " в счет мест университета следующих студентов, с обязательным заключением договора найма жилого помещения в общежитии.";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "Группа    П.П.   Фамилия Имя Отчество";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    string gruppa;
                    string fam;
                    string im;
                    string ot;
                    int q = 1;

                    for (int i = 0; i < nomera.Count(); i++)
                    {

                        SqlCommand sqlCommand = new SqlCommand("SELECT Группа FROM Группы WHERE Номер_группы = (SELECT Группа FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер)", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader = await sqlCommand.ExecuteReaderAsync();
                        await dataReader.ReadAsync();
                        gruppa = Convert.ToString(dataReader["Группа"]);
                        dataReader.Close();
                        await sqlCommand.ExecuteNonQueryAsync();

                        SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Студенты_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                        sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                        await dataReader2.ReadAsync();
                        fam = Convert.ToString(dataReader2["Фамилия"]);
                        im = Convert.ToString(dataReader2["Имя"]);
                        ot = Convert.ToString(dataReader2["Отчество"]);
                        dataReader2.Close();
                        await sqlCommand2.ExecuteNonQueryAsync();

                        q = q + i;

                        text = gruppa + "  " + "№" + q + "  " + fam + " " + im + " " + ot;

                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;



                    }
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();
                    text = "        Ректор                                          ______________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                  

                    for (int i = 0; i < nomera.Count(); i++)
                    {
                        string daytext = Convert.ToString(day);

                        SqlCommand command = new SqlCommand("UPDATE Студенты_УГЛТУ SET Приказ = @day WHERE Номер_студента=@Номер", sqlConnection);
                        command.Parameters.AddWithValue("Номер", nomera[i]);
                        command.Parameters.AddWithValue("day", daytext);
                        await command.ExecuteNonQueryAsync();

                    }
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
                }
            }
        }

        private async void button13_Click(object sender, EventArgs e)

        {
            listView2.Items.Clear();

            List<int> nomera = new List<int>();

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlRead = null;

            SqlDataReader dataReader = null;
            SqlDataReader dataReader2 = null;

            await sqlConnection.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента WHERE (dbo.Студенты_не_УГЛТУ.Приказ = N'Нет') AND (dbo.Студенты_не_УГЛТУ.Универститет = 1) AND (dbo.Студенты_не_УГЛТУ.[Супруг/Супруга] IS NULL) AND (dbo.Студенты_не_УГЛТУ.Дети = 0)", sqlConnection);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera.Add(Convert.ToInt32(sqlRead["Номер_студента_не_углту"]));
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

            for (int i = 0; i < nomera.Count(); i++)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_студента, Фамилия, Имя, Отчество FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;

                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_студента"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
                        listView2.Items.Add(item);
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

            if (nomera.Count == 0)
            {
                MessageBox.Show("К сожалению для приказа нет студентов");
            }
            else
            {
                DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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

                    text = "Министерство науки и высшего образования Российской Федерации";
                    cur_pos = text.Length;
                    currentSelection.TypeText(text);
                    r = doc.Range(0, cur_pos);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + 1;

                    text = "Федеральное государственное бюджетное";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "образовательное учреждение высшего образования";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "«Уральский государственный лесотехнический университет»";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "(УГЛТУ)";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    cur_pos = cur_pos + text.Length + 2;
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();

                    text = "ПРИКАЗ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "___________________________                                                  №___________________________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "г. Екатеринбург";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "О поселении студентов Университет 1";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "в общежития студгородка УГЛТУ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "ПРИКАЗЫВАЮ:";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "Поселить в общежитие №4 на 2020-2021 учебный год с ";
                    currentSelection.TypeText(text + day);
                    r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    cur_pos = cur_pos + text.Length + day.Length + 1;

                    text = " в счет мест Университета  1 следующих студентов, с обязательным заключением договора найма жилого помещения в общежитии.";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "Группа    П.П.   Фамилия Имя Отчество";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    string gruppa;
                    string fam;
                    string im;
                    string ot;
                    int q = 1;

                    for (int i = 0; i < nomera.Count(); i++)
                    {

                        SqlCommand sqlCommand = new SqlCommand("SELECT Универститет FROM Универститеты WHERE Номер_универститета = (SELECT Универститет FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер)", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader = await sqlCommand.ExecuteReaderAsync();
                        await dataReader.ReadAsync();
                        gruppa = Convert.ToString(dataReader["Универститет"]);
                        dataReader.Close();
                        await sqlCommand.ExecuteNonQueryAsync();

                        SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                        sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                        await dataReader2.ReadAsync();
                        fam = Convert.ToString(dataReader2["Фамилия"]);
                        im = Convert.ToString(dataReader2["Имя"]);
                        ot = Convert.ToString(dataReader2["Отчество"]);
                        dataReader2.Close();
                        await sqlCommand2.ExecuteNonQueryAsync();

                        q = q + i;

                        text = gruppa + "  " + "№" + q + "  " + fam + " " + im + " " + ot;

                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;



                    }
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();
                    text = "        Ректор                                          ______________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                   

                    for (int i = 0; i < nomera.Count(); i++)
                    {
                        string daytext = Convert.ToString(day);

                        SqlCommand command = new SqlCommand("UPDATE Студенты_не_УГЛТУ SET Приказ = @day WHERE Номер_студента=@Номер", sqlConnection);
                        command.Parameters.AddWithValue("Номер", nomera[i]);
                        command.Parameters.AddWithValue("day", daytext);
                        await command.ExecuteNonQueryAsync();

                    }
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
                }
            }
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();

            List<int> nomera = new List<int>();

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlRead = null;

            SqlDataReader dataReader = null;
            SqlDataReader dataReader2 = null;

            await sqlConnection.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента WHERE (dbo.Студенты_не_УГЛТУ.Приказ = N'Нет') AND (dbo.Студенты_не_УГЛТУ.Универститет = 2) AND (dbo.Студенты_не_УГЛТУ.[Супруг/Супруга] IS NULL) AND (dbo.Студенты_не_УГЛТУ.Дети = 0)", sqlConnection);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera.Add(Convert.ToInt32(sqlRead["Номер_студента_не_углту"]));
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

            for (int i = 0; i < nomera.Count(); i++)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_студента, Фамилия, Имя, Отчество FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;

                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_студента"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
                        listView2.Items.Add(item);
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

            if (nomera.Count == 0)
            {
                MessageBox.Show("К сожалению для приказа нет студентов");
            }
            else
            {
                DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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

                    text = "Министерство науки и высшего образования Российской Федерации";
                    cur_pos = text.Length;
                    currentSelection.TypeText(text);
                    r = doc.Range(0, cur_pos);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + 1;

                    text = "Федеральное государственное бюджетное";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "образовательное учреждение высшего образования";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "«Уральский государственный лесотехнический университет»";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "(УГЛТУ)";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    cur_pos = cur_pos + text.Length + 2;
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();

                    text = "ПРИКАЗ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "___________________________                                                  №___________________________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "г. Екатеринбург";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "О поселении студентов Университет 2";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "в общежития студгородка УГЛТУ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "ПРИКАЗЫВАЮ:";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "Поселить в общежитие №4 на 2020-2021 учебный год с ";
                    currentSelection.TypeText(text + day);
                    r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    cur_pos = cur_pos + text.Length + day.Length + 1;

                    text = " в счет мест Университета 2 следующих студентов, с обязательным заключением договора найма жилого помещения в общежитии.";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "Группа    П.П.   Фамилия Имя Отчество";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    string gruppa;
                    string fam;
                    string im;
                    string ot;
                    int q = 1;

                    for (int i = 0; i < nomera.Count(); i++)
                    {

                        SqlCommand sqlCommand = new SqlCommand("SELECT Универститет FROM Универститеты WHERE Номер_универститета = (SELECT Универститет FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер)", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader = await sqlCommand.ExecuteReaderAsync();
                        await dataReader.ReadAsync();
                        gruppa = Convert.ToString(dataReader["Универститет"]);
                        dataReader.Close();
                        await sqlCommand.ExecuteNonQueryAsync();

                        SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                        sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                        await dataReader2.ReadAsync();
                        fam = Convert.ToString(dataReader2["Фамилия"]);
                        im = Convert.ToString(dataReader2["Имя"]);
                        ot = Convert.ToString(dataReader2["Отчество"]);
                        dataReader2.Close();
                        await sqlCommand2.ExecuteNonQueryAsync();

                        q = q + i;

                        text = gruppa + "  " + "№" + q + "  " + fam + " " + im + " " + ot;

                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;



                    }
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();
                    text = "        Ректор                                          ______________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;


                    for (int i = 0; i < nomera.Count(); i++)
                    {
                        string daytext = Convert.ToString(day);

                        SqlCommand command = new SqlCommand("UPDATE Студенты_не_УГЛТУ SET Приказ = @day WHERE Номер_студента=@Номер", sqlConnection);
                        command.Parameters.AddWithValue("Номер", nomera[i]);
                        command.Parameters.AddWithValue("day", daytext);
                        await command.ExecuteNonQueryAsync();

                    }
                    
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
                }
            }
        }

        private async void button8_Click(object sender, EventArgs e)

        {
            listView2.Items.Clear();

            List<int> nomera = new List<int>();

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlRead = null;

            SqlDataReader dataReader = null;
            SqlDataReader dataReader2 = null;

            await sqlConnection.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_студента_не_углту FROM dbo.Общежития INNER JOIN dbo.Студенты_не_УГЛТУ ON dbo.Общежития.Номер_студента_не_углту = dbo.Студенты_не_УГЛТУ.Номер_студента WHERE (dbo.Студенты_не_УГЛТУ.Приказ = N'Нет')", sqlConnection);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera.Add(Convert.ToInt32(sqlRead["Номер_студента_не_углту"]));
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

            for (int i = 0; i < nomera.Count(); i++)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_студента, Фамилия, Имя, Отчество FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;

                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_студента"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
                        listView2.Items.Add(item);
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

            if (nomera.Count == 0)
            {
                MessageBox.Show("К сожалению для приказа нет студентов");
            }
            else
            {
                DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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

                    text = "Министерство науки и высшего образования Российской Федерации";
                    cur_pos = text.Length;
                    currentSelection.TypeText(text);
                    r = doc.Range(0, cur_pos);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + 1;

                    text = "Федеральное государственное бюджетное";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "образовательное учреждение высшего образования";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "«Уральский государственный лесотехнический университет»";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "(УГЛТУ)";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    cur_pos = cur_pos + text.Length + 2;
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();

                    text = "ПРИКАЗ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "___________________________                                                  №___________________________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "г. Екатеринбург";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "О поселении студентов";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "в общежития студгородка УГЛТУ";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 10;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "ПРИКАЗЫВАЮ:";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Bold = 1;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;


                    text = "Поселить в общежитие №1 на 2020-2021 учебный год с ";
                    currentSelection.TypeText(text + day);
                    r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                    r.Bold = 0;
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    cur_pos = cur_pos + text.Length + day.Length + 1;

                    text = " в счет мест университета следующих студентов, с обязательным заключением договора найма жилого помещения в общежитии.";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    text = "Группа    П.П.   Фамилия Имя Отчество";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentSelection.TypeParagraph();
                    cur_pos = cur_pos + text.Length + 1;

                    string gruppa;
                    string fam;
                    string im;
                    string ot;
                    int q = 1;

                    for (int i = 0; i < nomera.Count(); i++)
                    {

                        SqlCommand sqlCommand = new SqlCommand("SELECT Универститет FROM Универститеты WHERE Номер_универститета = (SELECT Универститет FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер)", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader = await sqlCommand.ExecuteReaderAsync();
                        await dataReader.ReadAsync();
                        gruppa = Convert.ToString(dataReader["Универститет"]);
                        dataReader.Close();
                        await sqlCommand.ExecuteNonQueryAsync();

                        SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Студенты_не_УГЛТУ WHERE Номер_студента = @Номер", sqlConnection);
                        sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                        dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                        await dataReader2.ReadAsync();
                        fam = Convert.ToString(dataReader2["Фамилия"]);
                        im = Convert.ToString(dataReader2["Имя"]);
                        ot = Convert.ToString(dataReader2["Отчество"]);
                        dataReader2.Close();
                        await sqlCommand2.ExecuteNonQueryAsync();

                        q = q + i;

                        text = gruppa + "  " + "№" + q + "  " + fam + " " + im + " " + ot;

                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;



                    }
                    currentSelection.TypeParagraph();
                    currentSelection.TypeParagraph();
                    text = "        Ректор                                          ______________";
                    currentSelection.TypeText(text);
                    r = doc.Range(cur_pos, cur_pos + text.Length);
                    r.Font.Name = "Times New Roman";
                    r.Font.Size = 12;
                    r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;


                   

                    for (int i = 0; i < nomera.Count(); i++)
                    {
                        string daytext = Convert.ToString(day);

                        SqlCommand command = new SqlCommand("UPDATE Студенты_не_УГЛТУ SET Приказ = @day WHERE Номер_студента=@Номер", sqlConnection);
                        command.Parameters.AddWithValue("Номер", nomera[i]);
                        command.Parameters.AddWithValue("day", daytext);
                        await command.ExecuteNonQueryAsync();

                    }
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
                }
            }
        }

        private async void button11_Click(object sender, EventArgs e)


        {
            listView3.Items.Clear();

            List<int> nomera = new List<int>();

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlRead = null;

            SqlDataReader dataReader = null;
            SqlDataReader dataReader2 = null;

            await sqlConnection.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_сотрудника FROM dbo.Общежития INNER JOIN dbo.Сотрудники ON dbo.Общежития.Номер_сотрудника = dbo.Сотрудники.Номер_сотрудника WHERE (dbo.Сотрудники.Приказ = N'Нет')", sqlConnection);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera.Add(Convert.ToInt32(sqlRead["Номер_сотрудника"]));
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

            for (int i = 0; i < nomera.Count(); i++)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_сотрудника, Фамилия, Имя, Отчество FROM Сотрудники WHERE Номер_сотрудника = @Номер", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;

                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_сотрудника"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
                        listView3.Items.Add(item);
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

            if (nomera.Count == 0)
            {
                MessageBox.Show("К сожалению для приказа нет сотрудников");
            }
            else
            {
                DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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

                        text = "Министерство науки и высшего образования Российской Федерации";
                        cur_pos = text.Length;
                        currentSelection.TypeText(text);
                        r = doc.Range(0, cur_pos);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + 1;

                        text = "Федеральное государственное бюджетное";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "образовательное учреждение высшего образования";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "«Уральский государственный лесотехнический университет»";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "(УГЛТУ)";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                        cur_pos = cur_pos + text.Length + 2;
                        currentSelection.TypeParagraph();
                        currentSelection.TypeParagraph();

                        text = "ПРИКАЗ";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "___________________________                                                  №___________________________";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 0;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "г. Екатеринбург";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "О поселении сотрудников УГЛТУ";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 10;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "в общежития студгородка УГЛТУ";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 10;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "ПРИКАЗЫВАЮ:";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "Поселить в общежитие №1 на 2020-2021 учебный год с ";
                        currentSelection.TypeText(text + day);
                        r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                        r.Bold = 0;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        cur_pos = cur_pos + text.Length + day.Length + 1;

                        text = " в счет мест УГЛТУ следующих сотрудников, с обязательным заключением договора найма жилого помещения в общежитии.";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "П.П.   Фамилия Имя Отчество";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        string fam;
                        string im;
                        string ot;
                        int q = 1;

                        for (int i = 0; i < nomera.Count(); i++)
                        {

                            

                            SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Сотрудники WHERE Номер_сотрудника = @Номер", sqlConnection);
                            sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                            dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                            await dataReader2.ReadAsync();
                            fam = Convert.ToString(dataReader2["Фамилия"]);
                            im = Convert.ToString(dataReader2["Имя"]);
                            ot = Convert.ToString(dataReader2["Отчество"]);
                            dataReader2.Close();
                            await sqlCommand2.ExecuteNonQueryAsync();

                            q = q + i;

                            text = "№" + q + "  " + fam + " " + im + " " + ot;

                            currentSelection.TypeText(text);
                            r = doc.Range(cur_pos, cur_pos + text.Length);
                            r.Font.Name = "Times New Roman";
                            r.Font.Size = 12;
                            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            currentSelection.TypeParagraph();
                            cur_pos = cur_pos + text.Length + 1;



                        }
                        currentSelection.TypeParagraph();
                        currentSelection.TypeParagraph();
                        text = "        Ректор                                          ______________";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;



                        for (int i = 0; i < nomera.Count(); i++)
                        {
                            string daytext = Convert.ToString(day);

                            SqlCommand command = new SqlCommand("UPDATE Сотрудники SET Приказ = @day WHERE Номер_сотрудника=@Номер", sqlConnection);
                            command.Parameters.AddWithValue("Номер", nomera[i]);
                            command.Parameters.AddWithValue("day", daytext);
                            await command.ExecuteNonQueryAsync();

                        }
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
                }
            }
        }

        private async void button9_Click(object sender, EventArgs e)



        {
            listView3.Items.Clear();

            List<int> nomera = new List<int>();

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            SqlDataReader sqlRead = null;

            SqlDataReader dataReader = null;
            SqlDataReader dataReader2 = null;

            await sqlConnection.OpenAsync();

            try
            {
                SqlCommand com = new SqlCommand("SELECT dbo.Общежития.Номер_супруга FROM dbo.Общежития INNER JOIN dbo.Супруги ON dbo.Общежития.Номер_супруга = dbo.Супруги.Номер_супруга WHERE (dbo.Супруги.Приказ = N'Нет')", sqlConnection);

                sqlRead = await com.ExecuteReaderAsync();
                while (sqlRead.Read())
                {

                    nomera.Add(Convert.ToInt32(sqlRead["Номер_супруга"]));
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

            for (int i = 0; i < nomera.Count(); i++)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT Номер_супруга, Фамилия, Имя, Отчество FROM Супруги WHERE Номер_супруга = @Номер", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("Номер", nomera[i]);

                    dataReader = sqlCommand.ExecuteReader();
                    ListViewItem item = null;

                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Номер_супруга"]), Convert.ToString(dataReader["Фамилия"]), Convert.ToString(dataReader["Имя"]), Convert.ToString(dataReader["Отчество"]) });
                        listView3.Items.Add(item);
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

            if (nomera.Count == 0)
            {
                MessageBox.Show("К сожалению для приказа нет людей");
            }
            else
            {
                DialogResult result = MessageBox.Show("Распечатать приказ", "Приказ", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
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

                        text = "Министерство науки и высшего образования Российской Федерации";
                        cur_pos = text.Length;
                        currentSelection.TypeText(text);
                        r = doc.Range(0, cur_pos);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + 1;

                        text = "Федеральное государственное бюджетное";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "образовательное учреждение высшего образования";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "«Уральский государственный лесотехнический университет»";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "(УГЛТУ)";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                        cur_pos = cur_pos + text.Length + 2;
                        currentSelection.TypeParagraph();
                        currentSelection.TypeParagraph();

                        text = "ПРИКАЗ";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "___________________________                                                  №___________________________";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 0;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "г. Екатеринбург";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "О поселении";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 10;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "в общежития студгородка УГЛТУ";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 10;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "ПРИКАЗЫВАЮ:";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Bold = 1;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;


                        text = "Поселить в общежитие №1 на 2020-2021 учебный год с ";
                        currentSelection.TypeText(text + day);
                        r = doc.Range(cur_pos, cur_pos + text.Length + day.Length);
                        r.Bold = 0;
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        cur_pos = cur_pos + text.Length + day.Length + 1;

                        text = " в счет мест УГЛТУ следующих человек, с обязательным заключением договора найма жилого помещения в общежитии.";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        text = "П.П.   Фамилия Имя Отчество";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        currentSelection.TypeParagraph();
                        cur_pos = cur_pos + text.Length + 1;

                        string fam;
                        string im;
                        string ot;
                        int q = 1;

                        for (int i = 0; i < nomera.Count(); i++)
                        {



                            SqlCommand sqlCommand2 = new SqlCommand("SELECT Фамилия, Имя, Отчество FROM Супруги WHERE Номер_супруга = @Номер", sqlConnection);
                            sqlCommand2.Parameters.AddWithValue("Номер", nomera[i]);
                            dataReader2 = await sqlCommand2.ExecuteReaderAsync();
                            await dataReader2.ReadAsync();
                            fam = Convert.ToString(dataReader2["Фамилия"]);
                            im = Convert.ToString(dataReader2["Имя"]);
                            ot = Convert.ToString(dataReader2["Отчество"]);
                            dataReader2.Close();
                            await sqlCommand2.ExecuteNonQueryAsync();

                            q = q + i;

                            text = "№" + q + "  " + fam + " " + im + " " + ot;

                            currentSelection.TypeText(text);
                            r = doc.Range(cur_pos, cur_pos + text.Length);
                            r.Font.Name = "Times New Roman";
                            r.Font.Size = 12;
                            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            currentSelection.TypeParagraph();
                            cur_pos = cur_pos + text.Length + 1;



                        }
                        currentSelection.TypeParagraph();
                        currentSelection.TypeParagraph();
                        text = "        Ректор                                          ______________";
                        currentSelection.TypeText(text);
                        r = doc.Range(cur_pos, cur_pos + text.Length);
                        r.Font.Name = "Times New Roman";
                        r.Font.Size = 12;
                        r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;



                        for (int i = 0; i < nomera.Count(); i++)
                        {
                            string daytext = Convert.ToString(day);

                            SqlCommand command = new SqlCommand("UPDATE Супруги SET Приказ = @day WHERE Номер_супруга=@Номер", sqlConnection);
                            command.Parameters.AddWithValue("Номер", nomera[i]);
                            command.Parameters.AddWithValue("day", daytext);
                            await command.ExecuteNonQueryAsync();

                        }
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
                }
            }
        }

        private void Prikazi_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Приказ_супр". При необходимости она может быть перемещена или удалена.
            this.приказ_супрTableAdapter.Fill(this.diplomDataSet.Приказ_супр);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Приказ_сотр". При необходимости она может быть перемещена или удалена.
            this.приказ_сотрTableAdapter.Fill(this.diplomDataSet.Приказ_сотр);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Сотрудники". При необходимости она может быть перемещена или удалена.
            this.сотрудникиTableAdapter.Fill(this.diplomDataSet.Сотрудники);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Приказ_студ_не_углту". При необходимости она может быть перемещена или удалена.
            this.приказ_студ_не_углтуTableAdapter.Fill(this.diplomDataSet.Приказ_студ_не_углту);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Приказ_студ_углту". При необходимости она может быть перемещена или удалена.
            this.приказ_студ_углтуTableAdapter.Fill(this.diplomDataSet.Приказ_студ_углту);

        }
    }
}
