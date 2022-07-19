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
    public partial class Аuthorization : Form
    {
        SqlConnection sqlConnection;

        public Аuthorization()
        {
            InitializeComponent();
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {

            if (loginField.Text != "" && passField.Text != "")
            {

                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                string log = loginField.Text;
                string pas;

                await sqlConnection.OpenAsync();


                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand("SELECT Пароль FROM LogPass WHERE Пользователь = @log", sqlConnection);

                command.Parameters.AddWithValue("log", log);

                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    await sqlReader.ReadAsync();

                    pas = Convert.ToString(sqlReader["Пароль"]);

                    if (pas == passField.Text)
                    {
                        MessageBox.Show("Вход разрешен", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                    sqlReader.Close();
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
            }
            else if (loginField.Text == "" && passField.Text != "")
            {
                MessageBox.Show("Поле логин не заполнено", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else if (passField.Text == "" && loginField.Text != "")
            {
                MessageBox.Show("Поле пароль не заполнено", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else if (loginField.Text == "" && passField.Text == "")
            {
                MessageBox.Show("Поля логин и пароль не заполнены", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
