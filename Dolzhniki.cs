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
    public partial class Dolzhniki : Form
    {
        SqlConnection sqlConnection;
        public Dolzhniki()
        {
            InitializeComponent();
            //SELECT Дата_выселения, Номер_студента_углту, Номер_студента_не_углту, Номер_сотрудника, DATEDIFF(DAY, SYSDATETIME(), Дата_выселения) AS 'Дней до выселения'
            //FROM Общежития
            //WHERE Номер_студента_углту != 0 OR Номер_студента_не_углту != 0 OR Номер_сотрудника != 0
        }

        private async void Dolzhniki_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();


            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select Дата_выселения AS 'Дата выселения', [Номер_студента_углту] AS 'Номер студента', [Дней до выселения] From Выселение WHERE [Номер_студента_углту] != 0", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];


            SqlDataAdapter dataAdapter2 = new SqlDataAdapter("Select Дата_выселения AS 'Дата выселения', [Номер_студента_углту] AS 'Номер студента', [Дней до выселения] From Выселение WHERE [Номер_студента_углту] != 0 AND [Дней до выселения] < 30 ", sqlConnection);

            DataSet dataSet2 = new DataSet();

            dataAdapter2.Fill(dataSet2);

            dataGridView2.DataSource = dataSet2.Tables[0];


            SqlDataAdapter dataAdapter3 = new SqlDataAdapter("Select Дата_выселения AS 'Дата выселения', [Номер_студента_не_углту] AS 'Номер студента', [Дней до выселения] From Выселение WHERE [Номер_студента_не_углту] != 0", sqlConnection);

            DataSet dataSet3 = new DataSet();

            dataAdapter3.Fill(dataSet3);

            dataGridView3.DataSource = dataSet3.Tables[0];


            SqlDataAdapter dataAdapter4 = new SqlDataAdapter("Select Дата_выселения AS 'Дата выселения', [Номер_студента_не_углту] AS 'Номер студента', [Дней до выселения] From Выселение WHERE [Номер_студента_не_углту] != 0 AND [Дней до выселения] < 30 ", sqlConnection);

            DataSet dataSet4 = new DataSet();

            dataAdapter4.Fill(dataSet4);

            dataGridView4.DataSource = dataSet4.Tables[0];


            SqlDataAdapter dataAdapter5 = new SqlDataAdapter("Select Дата_выселения AS 'Дата выселения', [Номер_сотрудника] AS 'Номер сотрудника', [Дней до выселения] From Выселение WHERE [Номер_сотрудника] != 0", sqlConnection);

            DataSet dataSet5 = new DataSet();

            dataAdapter5.Fill(dataSet5);

            dataGridView5.DataSource = dataSet5.Tables[0];


            SqlDataAdapter dataAdapter6 = new SqlDataAdapter("Select Дата_выселения AS 'Дата выселения', [Номер_сотрудника] AS 'Номер сотрудника', [Дней до выселения] From Выселение WHERE [Номер_сотрудника] != 0 AND [Дней до выселения] < 30 ", sqlConnection);

            DataSet dataSet6 = new DataSet();

            dataAdapter6.Fill(dataSet6);

            dataGridView6.DataSource = dataSet6.Tables[0];


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
