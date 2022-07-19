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
    public partial class Prozhivaushie : Form
    {
        SqlConnection sqlConnection;
        public Prozhivaushie()
        {
            InitializeComponent();
        }

        private async void Prozhivaushie_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * From ObsSU", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];


            SqlDataAdapter dataAdapter2 = new SqlDataAdapter("Select * From Obsh_S", sqlConnection);

            DataSet dataSet2 = new DataSet();

            dataAdapter2.Fill(dataSet2);

            dataGridView2.DataSource = dataSet2.Tables[0];


            SqlDataAdapter dataAdapter3 = new SqlDataAdapter("Select * From Obsh_sotr", sqlConnection);

            DataSet dataSet3 = new DataSet();

            dataAdapter3.Fill(dataSet3);

            dataGridView3.DataSource = dataSet3.Tables[0];


            SqlDataAdapter dataAdapter4 = new SqlDataAdapter("Select * From Obsh_supr", sqlConnection);

            DataSet dataSet4 = new DataSet();

            dataAdapter4.Fill(dataSet4);

            dataGridView4.DataSource = dataSet4.Tables[0];
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Фамилия LIKE '%{textBox6.Text}%'";

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Имя LIKE '%{textBox5.Text}%'";

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Отчество LIKE '%{textBox4.Text}%'";

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Фамилия LIKE '%{textBox9.Text}%'";

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Имя LIKE '%{textBox8.Text}%'";

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Отчество LIKE '%{textBox7.Text}%'";

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Фамилия LIKE '%{textBox12.Text}%'";

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Имя LIKE '%{textBox11.Text}%'";

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = $"Отчество LIKE '%{textBox10.Text}%'";

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (textBox18.Text != "")
            {
                int nomer = Convert.ToInt32(textBox18.Text);

                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();

                SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select * From ObsSU WHERE [Номер_студента] = {nomer} ", sqlConnection);

                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox13.Text != "")
            {
                int nomer = Convert.ToInt32(textBox13.Text);

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select * From Obsh_S WHERE [Номер_студента] = {nomer} ", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView2.DataSource = dataSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (textBox14.Text != "")
            {

                int nomer = Convert.ToInt32(textBox14.Text);

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select * From Obsh_sotr WHERE [Номер_сотрудника] = {nomer} ", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView3.DataSource = dataSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }

        private async void button3_Click(object sender, EventArgs e)

        {
            if (textBox15.Text != "")
            {
                int nomer = Convert.ToInt32(textBox15.Text);

            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select * From Obsh_supr WHERE [Номер_супруга] = {nomer} ", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView4.DataSource = dataSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Поле номер не заполнено");
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * From ObsSU", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];


            SqlDataAdapter dataAdapter2 = new SqlDataAdapter("Select * From Obsh_S", sqlConnection);

            DataSet dataSet2 = new DataSet();

            dataAdapter2.Fill(dataSet2);

            dataGridView2.DataSource = dataSet2.Tables[0];


            SqlDataAdapter dataAdapter3 = new SqlDataAdapter("Select * From Obsh_sotr", sqlConnection);

            DataSet dataSet3 = new DataSet();

            dataAdapter3.Fill(dataSet3);

            dataGridView3.DataSource = dataSet3.Tables[0];


            SqlDataAdapter dataAdapter4 = new SqlDataAdapter("Select * From Obsh_supr", sqlConnection);

            DataSet dataSet4 = new DataSet();

            dataAdapter4.Fill(dataSet4);

            dataGridView4.DataSource = dataSet4.Tables[0];
        }
    }
}
