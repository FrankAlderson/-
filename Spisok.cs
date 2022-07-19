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
    public partial class Spisok : Form
    {
        SqlConnection sqlConnection;
        public Spisok()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void Spisok_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Универститеты". При необходимости она может быть перемещена или удалена.
            this.универститетыTableAdapter.Fill(this.diplomDataSet.Универститеты);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Курсы". При необходимости она может быть перемещена или удалена.
            this.курсыTableAdapter.Fill(this.diplomDataSet.Курсы);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Пол". При необходимости она может быть перемещена или удалена.
            this.полTableAdapter.Fill(this.diplomDataSet.Пол);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Институты_УГЛТУ". При необходимости она может быть перемещена или удалена.
            this.институты_УГЛТУTableAdapter.Fill(this.diplomDataSet.Институты_УГЛТУ);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.Su_s_supr". При необходимости она может быть перемещена или удалена.
            this.su_s_suprTableAdapter.Fill(this.diplomDataSet.Su_s_supr);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplomDataSet.SU_bez_supr". При необходимости она может быть перемещена или удалена.
            this.sU_bez_suprTableAdapter.Fill(this.diplomDataSet.SU_bez_supr);

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * From SU_bez_supr", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];

            SqlDataAdapter dataAdapter2 = new SqlDataAdapter("Select * From Su_s_supr", sqlConnection);

            DataSet dataSet2 = new DataSet();

            dataAdapter2.Fill(dataSet2);

            dataGridView2.DataSource = dataSet2.Tables[0];



            SqlDataAdapter dataAdapter3 = new SqlDataAdapter("Select * From S_bez_s", sqlConnection);

            DataSet dataSet3 = new DataSet();

            dataAdapter3.Fill(dataSet3);

            dataGridView3.DataSource = dataSet3.Tables[0];

            SqlDataAdapter dataAdapter4 = new SqlDataAdapter("Select * From S_s_s", sqlConnection);

            DataSet dataSet4 = new DataSet();

            dataAdapter4.Fill(dataSet4);

            dataGridView4.DataSource = dataSet4.Tables[0];



            SqlDataAdapter dataAdapter5 = new SqlDataAdapter("Select * From Sotr", sqlConnection);

            DataSet dataSet5 = new DataSet();

            dataAdapter5.Fill(dataSet5);

            dataGridView5.DataSource = dataSet5.Tables[0];

            SqlDataAdapter dataAdapter6 = new SqlDataAdapter("Select * From Sotr_s_s", sqlConnection);

            DataSet dataSet6 = new DataSet();

            dataAdapter6.Fill(dataSet6);

            dataGridView6.DataSource = dataSet6.Tables[0];



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Фамилия LIKE '%{textBox1.Text}%'";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Имя LIKE '%{textBox2.Text}%'";

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Отчество LIKE '%{textBox3.Text}%'";

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Группа LIKE '%{textBox5.Text}%'";

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Фамилия LIKE '%{textBox13.Text}%'";

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Имя LIKE '%{textBox12.Text}%'";

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Отчество LIKE '%{textBox11.Text}%'";

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Группа LIKE '%{textBox9.Text}%'";

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"[Фамилия супруга] LIKE '%{textBox16.Text}%'";

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"[Имя супруга] LIKE '%{textBox15.Text}%'";

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"[Отчество супруга] LIKE '%{textBox14.Text}%'";

        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Фамилия LIKE '%{textBox24.Text}%'";

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Имя LIKE '%{textBox23.Text}%'";

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Отчество LIKE '%{textBox22.Text}%'";

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Группа LIKE '%{textBox20.Text}%'";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Пол = 'Мужчина'";
                    break;
                case 1:
                    (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Пол = 'Женщина'";
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlDataAdapter dataAdapter3 = new SqlDataAdapter("Select * From S_bez_s", sqlConnection);

            DataSet dataSet3 = new DataSet();

            dataAdapter3.Fill(dataSet3);

            dataGridView3.DataSource = dataSet3.Tables[0];

            SqlDataAdapter dataAdapter4 = new SqlDataAdapter("Select * From S_s_s", sqlConnection);

            DataSet dataSet4 = new DataSet();

            dataAdapter4.Fill(dataSet4);

            dataGridView4.DataSource = dataSet4.Tables[0];
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '1 курс'";
                    break;
                case 1:
                    (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '2 курс'";
                    break;
                case 2:
                    (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '3 курс'";
                    break;
                case 3:
                    (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '4 курс'";
                    break;
                case 4:
                    (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '5 курс'";
                    break;

            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Курс LIKE '%{comboBox2.Text}%'";

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Универститет = 'Университет 1'";
                    break;
                case 1:
                    (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Универститет = 'Университет 2'";
                    break;
            }

           }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Универститет = 'Университет 1'";
                    break;
                case 1:
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Универститет = 'Университет 2'";
                    break;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox5.SelectedIndex)
            {
                case 0:
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '1 курс'";
                    break;
                case 1:
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '2 курс'";
                    break;
                case 2:
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '3 курс'";
                    break;
                case 3:
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '4 курс'";
                    break;
                case 4:
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '5 курс'";
                    break;

            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox6.SelectedIndex)
            {
                case 0:
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Пол = 'Мужчина'";
                    break;
                case 1:
                    (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Пол = 'Женщина'";
                    break;
            }
        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Фамилия LIKE '%{textBox34.Text}%'";

        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Имя LIKE '%{textBox33.Text}%'";

        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Отчество LIKE '%{textBox32.Text}%'";

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Группа LIKE '%{textBox30.Text}%'";

        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"[Фамилия супруга] LIKE '%{textBox28.Text}%'";

        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"[Имя супруга] LIKE '%{textBox27.Text}%'";

        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"[Отчество супруга] LIKE '%{textBox26.Text}%'";

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox7.SelectedIndex)
            {
                case 0:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Институт = 'Институт 1'";
                    break;
                case 1:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Институт = 'Институт 2'";
                    break;
                case 2:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Институт = 'Институт 3'";
                    break;
                case 3:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Институт = 'Институт 4'";
                    break;
                case 4:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Институт = 'Институт 5'";
                    break;

            }

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox10.SelectedIndex)
            {
                case 0:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Институт = 'Институт 1'";
                    break;
                case 1:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Институт = 'Институт 2'";
                    break;
                case 2:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Институт = 'Институт 3'";
                    break;
                case 3:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Институт = 'Институт 4'";
                    break;
                case 4:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Институт = 'Институт 5'";
                    break;

            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox8.SelectedIndex)
            {
                case 0:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '1 курс'";
                    break;
                case 1:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '2 курс'";
                    break;
                case 2:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '3 курс'";
                    break;
                case 3:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '4 курс'";
                    break;
                case 4:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '5 курс'";
                    break;

            }
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox11.SelectedIndex)
            {
                case 0:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '1 курс'";
                    break;
                case 1:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '2 курс'";
                    break;
                case 2:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '3 курс'";
                    break;
                case 3:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '4 курс'";
                    break;
                case 4:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Курс = '5 курс'";
                    break;

            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox9.SelectedIndex)
            {
                case 0:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Пол = 'Мужчина'";
                    break;
                case 1:
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Пол = 'Женщина'";
                    break;
            }
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox12.SelectedIndex)
            {
                case 0:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Пол = 'Мужчина'";
                    break;
                case 1:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Пол = 'Женщина'";
                    break;
            }
        }

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Курс LIKE '%{comboBox5.Text}%'";

        }

        private void comboBox7_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Институт LIKE '%{comboBox7.Text}%'";

        }

        private void comboBox8_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Курс LIKE '%{comboBox8.Text}%'";

        }

        private void comboBox10_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Институт LIKE '%{comboBox10.Text}%'";

        }

        private void comboBox11_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Курс LIKE '%{comboBox11.Text}%'";

        }

        private void button2_Click(object sender, EventArgs e)
        {

            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * From SU_bez_supr", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];

            SqlDataAdapter dataAdapter2 = new SqlDataAdapter("Select * From Su_s_supr", sqlConnection);

            DataSet dataSet2 = new DataSet();

            dataAdapter2.Fill(dataSet2);

            dataGridView2.DataSource = dataSet2.Tables[0];
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            (dataGridView5.DataSource as DataTable).DefaultView.RowFilter = $"Фамилия LIKE '%{textBox7.Text}%'";

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            (dataGridView5.DataSource as DataTable).DefaultView.RowFilter = $"Имя LIKE '%{textBox6.Text}%'";

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            (dataGridView5.DataSource as DataTable).DefaultView.RowFilter = $"Отчество LIKE '%{textBox4.Text}%'";

        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Фамилия LIKE '%{textBox25.Text}%'";

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Имя LIKE '%{textBox21.Text}%'";

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Отчество LIKE '%{textBox19.Text}%'";

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"[Фамилия супруга] LIKE '%{textBox17.Text}%'";

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"[Имя супруга] LIKE '%{textBox10.Text}%'";

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"[Отчество супруга] LIKE '%{textBox8.Text}%'";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter5 = new SqlDataAdapter("Select * From Sotr", sqlConnection);

            DataSet dataSet5 = new DataSet();

            dataAdapter5.Fill(dataSet5);

            dataGridView5.DataSource = dataSet5.Tables[0];

            SqlDataAdapter dataAdapter6 = new SqlDataAdapter("Select * From Sotr_s_s", sqlConnection);

            DataSet dataSet6 = new DataSet();

            dataAdapter6.Fill(dataSet6);

            dataGridView6.DataSource = dataSet6.Tables[0];
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (textBox18.Text != "")
            { 
            int nomer = Convert.ToInt32(textBox18.Text);

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync(); 

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select * From SU_bez_supr WHERE [Номер_студента] = {nomer} ", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (textBox29.Text != "")
            { 
            int nomer = Convert.ToInt32(textBox29.Text);

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select * From Su_s_supr WHERE [Номер_студента] = {nomer} ", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView2.DataSource = dataSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            if (textBox31.Text != "")
            {
                int nomer = Convert.ToInt32(textBox31.Text);

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select * From S_bez_s WHERE [Номер_студента] = {nomer} ", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView3.DataSource = dataSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            if (textBox35.Text != "")
            {
                int nomer = Convert.ToInt32(textBox35.Text);

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select * From S_s_s WHERE [Номер_студента] = {nomer} ", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView4.DataSource = dataSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            if (textBox36.Text != "")
            {
                int nomer = Convert.ToInt32(textBox36.Text);

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select * From Sotr WHERE [Номер_сотрудника] = {nomer} ", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView5.DataSource = dataSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            if (textBox37.Text != "")
            {
                int nomer = Convert.ToInt32(textBox37.Text);

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select * From Sotr_s_s WHERE [Номер_сотрудника] = {nomer} ", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView6.DataSource = dataSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }
    }
}
