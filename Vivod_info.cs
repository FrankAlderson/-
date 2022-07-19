using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DIPLOM_V2
{
    public partial class Vivod_info : Form
    {

        SqlConnection sqlConnection;

        public Vivod_info()
        {
            InitializeComponent();
        }

        async void info_po_obsh ()
        


            {
                listView1.Items.Clear();

                string connectionString = @"Data Source=COMPUTER\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";

                sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();


                // первое общ

                SqlDataReader dataReader = null;
                SqlCommand sqlCommand3 = new SqlCommand("SELECT Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 1 AND Статус = 'Свободно'", sqlConnection);
                try
                {
                    dataReader = sqlCommand3.ExecuteReader();
                    ListViewItem item = null;
                    while (dataReader.Read())
                    {
                        item = new ListViewItem(new string[] { Convert.ToString(dataReader["Блок"]), Convert.ToString(dataReader["Комната"]), Convert.ToString(dataReader["Место"]) });
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

                SqlDataReader dataReader1 = null;
                SqlCommand sqlCommand4 = new SqlCommand("SELECT Блок, Комната, Место, Номер_студента_углту FROM View_1_1", sqlConnection);
                try
                {
                    dataReader1 = sqlCommand4.ExecuteReader();

                    ListViewItem item2 = null;

                    while (dataReader1.Read())
                    {
                        item2 = new ListViewItem(new string[] { Convert.ToString(dataReader1["Блок"]), Convert.ToString(dataReader1["Комната"]), Convert.ToString(dataReader1["Место"]), Convert.ToString(dataReader1["Номер_студента_углту"]) });
                        listView2.Items.Add(item2);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader1 != null && !dataReader1.IsClosed)
                    {
                        dataReader1.Close();
                    }
                }

                SqlDataReader dataReader2 = null;
                SqlCommand sqlCommand5 = new SqlCommand("SELECT Блок, Комната, Место, Номер_студента_не_углту FROM View_1_2", sqlConnection);
                try
                {
                    dataReader2 = sqlCommand5.ExecuteReader();

                    ListViewItem item3 = null;

                    while (dataReader2.Read())
                    {
                        item3 = new ListViewItem(new string[] { Convert.ToString(dataReader2["Блок"]), Convert.ToString(dataReader2["Комната"]), Convert.ToString(dataReader2["Место"]), Convert.ToString(dataReader2["Номер_студента_не_углту"]) });
                        listView2.Items.Add(item3);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader2 != null && !dataReader2.IsClosed)
                    {
                        dataReader2.Close();
                    }
                }

                SqlDataReader dataReader3 = null;
                SqlCommand sqlCommand6 = new SqlCommand("SELECT Блок, Комната, Место, Номер_сотрудника FROM View_1_3", sqlConnection);
                try
                {
                    dataReader3 = sqlCommand6.ExecuteReader();

                    ListViewItem item4 = null;

                    while (dataReader3.Read())
                    {
                        item4 = new ListViewItem(new string[] { Convert.ToString(dataReader3["Блок"]), Convert.ToString(dataReader3["Комната"]), Convert.ToString(dataReader3["Место"]), Convert.ToString(dataReader3["Номер_сотрудника"]) });
                        listView2.Items.Add(item4);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader3 != null && !dataReader3.IsClosed)
                    {
                        dataReader3.Close();
                    }
                }

                SqlDataReader dataReader4 = null;
                SqlCommand sqlCommand7 = new SqlCommand("SELECT Блок, Комната, Место, Номер_супруга FROM View_1_4", sqlConnection);
                try
                {
                    dataReader4 = sqlCommand7.ExecuteReader();

                    ListViewItem item5 = null;

                    while (dataReader4.Read())
                    {
                        item5 = new ListViewItem(new string[] { Convert.ToString(dataReader4["Блок"]), Convert.ToString(dataReader4["Комната"]), Convert.ToString(dataReader4["Место"]), Convert.ToString(dataReader4["Номер_супруга"]) });
                        listView2.Items.Add(item5);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader4 != null && !dataReader4.IsClosed)
                    {
                        dataReader4.Close();
                    }
                }

                // второе общ

                SqlDataReader dataReader5 = null;
                SqlCommand sqlCommand8 = new SqlCommand("SELECT Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 2 AND Статус = 'Свободно'", sqlConnection);
                try
                {
                    dataReader5 = sqlCommand8.ExecuteReader();
                    ListViewItem item6 = null;
                    while (dataReader5.Read())
                    {
                        item6 = new ListViewItem(new string[] { Convert.ToString(dataReader5["Блок"]), Convert.ToString(dataReader5["Комната"]), Convert.ToString(dataReader5["Место"]) });
                        listView3.Items.Add(item6);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader5 != null && !dataReader5.IsClosed)
                    {
                        dataReader5.Close();
                    }
                }

                SqlDataReader dataReader6 = null;
                SqlCommand sqlCommand9 = new SqlCommand("SELECT Блок, Комната, Место, Номер_студента_углту FROM View_2_1", sqlConnection);
                try
                {
                    dataReader6 = sqlCommand9.ExecuteReader();

                    ListViewItem item7 = null;

                    while (dataReader6.Read())
                    {
                        item7 = new ListViewItem(new string[] { Convert.ToString(dataReader6["Блок"]), Convert.ToString(dataReader6["Комната"]), Convert.ToString(dataReader6["Место"]), Convert.ToString(dataReader6["Номер_студента_углту"]) });
                        listView4.Items.Add(item7);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader6 != null && !dataReader6.IsClosed)
                    {
                        dataReader6.Close();
                    }
                }

                SqlDataReader dataReader7 = null;
                SqlCommand sqlCommand10 = new SqlCommand("SELECT Блок, Комната, Место, Номер_супруга FROM View_2_2", sqlConnection);
                try
                {
                    dataReader7 = sqlCommand10.ExecuteReader();

                    ListViewItem item8 = null;

                    while (dataReader7.Read())
                    {
                        item8 = new ListViewItem(new string[] { Convert.ToString(dataReader7["Блок"]), Convert.ToString(dataReader7["Комната"]), Convert.ToString(dataReader7["Место"]), Convert.ToString(dataReader7["Номер_супруга"]) });
                        listView4.Items.Add(item8);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader7 != null && !dataReader7.IsClosed)
                    {
                        dataReader7.Close();
                    }
                }

                // третье общ

                SqlDataReader dataReader10 = null;
                SqlCommand sqlCommand13 = new SqlCommand("SELECT Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 3 AND Статус = 'Свободно'", sqlConnection);
                try
                {
                    dataReader10 = sqlCommand13.ExecuteReader();
                    ListViewItem item11 = null;
                    while (dataReader10.Read())
                    {
                        item11 = new ListViewItem(new string[] { Convert.ToString(dataReader10["Блок"]), Convert.ToString(dataReader10["Комната"]), Convert.ToString(dataReader10["Место"]) });
                        listView5.Items.Add(item11);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader10 != null && !dataReader10.IsClosed)
                    {
                        dataReader10.Close();
                    }
                }

                SqlDataReader dataReader8 = null;
                SqlCommand sqlCommand11 = new SqlCommand("SELECT Блок, Комната, Место, Номер_студента_углту FROM View_3_1", sqlConnection);
                try
                {
                    dataReader8 = sqlCommand11.ExecuteReader();

                    ListViewItem item9 = null;

                    while (dataReader8.Read())
                    {
                        item9 = new ListViewItem(new string[] { Convert.ToString(dataReader8["Блок"]), Convert.ToString(dataReader8["Комната"]), Convert.ToString(dataReader8["Место"]), Convert.ToString(dataReader8["Номер_студента_углту"]) });
                        listView6.Items.Add(item9);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader8 != null && !dataReader8.IsClosed)
                    {
                        dataReader8.Close();
                    }
                }

                SqlDataReader dataReader9 = null;
                SqlCommand sqlCommand12 = new SqlCommand("SELECT Блок, Комната, Место, Номер_супруга FROM View_3_2", sqlConnection);
                try
                {
                    dataReader9 = sqlCommand12.ExecuteReader();

                    ListViewItem item10 = null;

                    while (dataReader9.Read())
                    {
                        item10 = new ListViewItem(new string[] { Convert.ToString(dataReader9["Блок"]), Convert.ToString(dataReader9["Комната"]), Convert.ToString(dataReader9["Место"]), Convert.ToString(dataReader9["Номер_супруга"]) });
                        listView6.Items.Add(item10);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader9 != null && !dataReader9.IsClosed)
                    {
                        dataReader9.Close();
                    }
                }

                // четвертое общ

                SqlDataReader dataReader11 = null;
                SqlCommand sqlCommand14 = new SqlCommand("SELECT Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 4 AND Статус = 'Свободно'", sqlConnection);
                try
                {
                    dataReader11 = sqlCommand14.ExecuteReader();
                    ListViewItem item12 = null;
                    while (dataReader11.Read())
                    {
                        item12 = new ListViewItem(new string[] { Convert.ToString(dataReader11["Блок"]), Convert.ToString(dataReader11["Комната"]), Convert.ToString(dataReader11["Место"]) });
                        listView7.Items.Add(item12);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader11 != null && !dataReader11.IsClosed)
                    {
                        dataReader11.Close();
                    }
                }

                SqlDataReader dataReader12 = null;
                SqlCommand sqlCommand15 = new SqlCommand("SELECT Блок, Комната, Место, Номер_студента_не_углту FROM View_4_1", sqlConnection);
                try
                {
                    dataReader12 = sqlCommand15.ExecuteReader();

                    ListViewItem item13 = null;

                    while (dataReader12.Read())
                    {
                        item13 = new ListViewItem(new string[] { Convert.ToString(dataReader12["Блок"]), Convert.ToString(dataReader12["Комната"]), Convert.ToString(dataReader12["Место"]), Convert.ToString(dataReader12["Номер_студента_не_углту"]) });
                        listView8.Items.Add(item13);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader12 != null && !dataReader12.IsClosed)
                    {
                        dataReader12.Close();
                    }
                }

                SqlDataReader dataReader13 = null;
                SqlCommand sqlCommand16 = new SqlCommand("SELECT Блок, Комната, Место, Номер_супруга FROM View_4_2", sqlConnection);
                try
                {
                    dataReader13 = sqlCommand16.ExecuteReader();

                    ListViewItem item14 = null;

                    while (dataReader13.Read())
                    {
                        item14 = new ListViewItem(new string[] { Convert.ToString(dataReader13["Блок"]), Convert.ToString(dataReader13["Комната"]), Convert.ToString(dataReader13["Место"]), Convert.ToString(dataReader13["Номер_супруга"]) });
                        listView8.Items.Add(item14);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader13 != null && !dataReader13.IsClosed)
                    {
                        dataReader13.Close();
                    }
                }

                // пятое общ

                SqlDataReader dataReader14 = null;
                SqlCommand sqlCommand17 = new SqlCommand("SELECT Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 5 AND Статус = 'Свободно'", sqlConnection);
                try
                {
                    dataReader14 = sqlCommand17.ExecuteReader();
                    ListViewItem item15 = null;
                    while (dataReader14.Read())
                    {
                        item15 = new ListViewItem(new string[] { Convert.ToString(dataReader14["Блок"]), Convert.ToString(dataReader14["Комната"]), Convert.ToString(dataReader14["Место"]) });
                        listView9.Items.Add(item15);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader14 != null && !dataReader14.IsClosed)
                    {
                        dataReader14.Close();
                    }
                }

                SqlDataReader dataReader15 = null;
                SqlCommand sqlCommand18 = new SqlCommand("SELECT Блок, Комната, Место, Номер_студента_углту FROM View_5_1", sqlConnection);
                try
                {
                    dataReader15 = sqlCommand18.ExecuteReader();

                    ListViewItem item16 = null;

                    while (dataReader15.Read())
                    {
                        item16 = new ListViewItem(new string[] { Convert.ToString(dataReader15["Блок"]), Convert.ToString(dataReader15["Комната"]), Convert.ToString(dataReader15["Место"]), Convert.ToString(dataReader15["Номер_студента_углту"]) });
                        listView10.Items.Add(item16);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader15 != null && !dataReader15.IsClosed)
                    {
                        dataReader15.Close();
                    }
                }

                SqlDataReader dataReader16 = null;
                SqlCommand sqlCommand19 = new SqlCommand("SELECT Блок, Комната, Место, Номер_супруга FROM View_5_2", sqlConnection);
                try
                {
                    dataReader16 = sqlCommand19.ExecuteReader();

                    ListViewItem item17 = null;

                    while (dataReader16.Read())
                    {
                        item17 = new ListViewItem(new string[] { Convert.ToString(dataReader16["Блок"]), Convert.ToString(dataReader16["Комната"]), Convert.ToString(dataReader16["Место"]), Convert.ToString(dataReader16["Номер_супруга"]) });
                        listView10.Items.Add(item17);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader16 != null && !dataReader16.IsClosed)
                    {
                        dataReader16.Close();
                    }
                }

                // шестое общ

                SqlDataReader dataReader17 = null;
                SqlCommand sqlCommand20 = new SqlCommand("SELECT Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 6 AND Статус = 'Свободно'", sqlConnection);
                try
                {
                    dataReader17 = sqlCommand20.ExecuteReader();
                    ListViewItem item18 = null;
                    while (dataReader17.Read())
                    {
                        item18 = new ListViewItem(new string[] { Convert.ToString(dataReader17["Блок"]), Convert.ToString(dataReader17["Комната"]), Convert.ToString(dataReader17["Место"]) });
                        listView11.Items.Add(item18);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader17 != null && !dataReader17.IsClosed)
                    {
                        dataReader17.Close();
                    }
                }

                SqlDataReader dataReader18 = null;
                SqlCommand sqlCommand21 = new SqlCommand("SELECT Блок, Комната, Место, Номер_студента_углту FROM View_6_1", sqlConnection);
                try
                {
                    dataReader18 = sqlCommand21.ExecuteReader();

                    ListViewItem item19 = null;

                    while (dataReader18.Read())
                    {
                        item19 = new ListViewItem(new string[] { Convert.ToString(dataReader18["Блок"]), Convert.ToString(dataReader18["Комната"]), Convert.ToString(dataReader18["Место"]), Convert.ToString(dataReader18["Номер_студента_углту"]) });
                        listView12.Items.Add(item19);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader18 != null && !dataReader18.IsClosed)
                    {
                        dataReader18.Close();
                    }
                }

                SqlDataReader dataReader19 = null;
                SqlCommand sqlCommand22 = new SqlCommand("SELECT Блок, Комната, Место, Номер_супруга FROM View_6_2", sqlConnection);
                try
                {
                    dataReader19 = sqlCommand22.ExecuteReader();

                    ListViewItem item20 = null;

                    while (dataReader19.Read())
                    {
                        item20 = new ListViewItem(new string[] { Convert.ToString(dataReader19["Блок"]), Convert.ToString(dataReader19["Комната"]), Convert.ToString(dataReader19["Место"]), Convert.ToString(dataReader19["Номер_супруга"]) });
                        listView12.Items.Add(item20);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader19 != null && !dataReader19.IsClosed)
                    {
                        dataReader19.Close();
                    }
                }

                // седьмое общ

                SqlDataReader dataReader20 = null;
                SqlCommand sqlCommand23 = new SqlCommand("SELECT Блок, Комната, Место FROM Общежития WHERE Номер_общежития = 7 AND Статус = 'Свободно'", sqlConnection);
                try
                {
                    dataReader20 = sqlCommand23.ExecuteReader();
                    ListViewItem item21 = null;
                    while (dataReader20.Read())
                    {
                        item21 = new ListViewItem(new string[] { Convert.ToString(dataReader20["Блок"]), Convert.ToString(dataReader20["Комната"]), Convert.ToString(dataReader20["Место"]) });
                        listView13.Items.Add(item21);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader20 != null && !dataReader20.IsClosed)
                    {
                        dataReader20.Close();
                    }
                }

                SqlDataReader dataReader21 = null;
                SqlCommand sqlCommand24 = new SqlCommand("SELECT Блок, Комната, Место, Номер_студента_углту FROM View_7_1", sqlConnection);
                try
                {
                    dataReader21 = sqlCommand24.ExecuteReader();

                    ListViewItem item22 = null;

                    while (dataReader21.Read())
                    {
                        item22 = new ListViewItem(new string[] { Convert.ToString(dataReader21["Блок"]), Convert.ToString(dataReader21["Комната"]), Convert.ToString(dataReader21["Место"]), Convert.ToString(dataReader21["Номер_студента_углту"]) });
                        listView14.Items.Add(item22);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader21 != null && !dataReader21.IsClosed)
                    {
                        dataReader21.Close();
                    }
                }

                SqlDataReader dataReader22 = null;
                SqlCommand sqlCommand25 = new SqlCommand("SELECT Блок, Комната, Место, Номер_супруга FROM View_7_2", sqlConnection);
                try
                {
                    dataReader22 = sqlCommand25.ExecuteReader();

                    ListViewItem item23 = null;

                    while (dataReader22.Read())
                    {
                        item23 = new ListViewItem(new string[] { Convert.ToString(dataReader22["Блок"]), Convert.ToString(dataReader22["Комната"]), Convert.ToString(dataReader22["Место"]), Convert.ToString(dataReader22["Номер_супруга"]) });
                        listView14.Items.Add(item23);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (dataReader22 != null && !dataReader22.IsClosed)
                    {
                        dataReader22.Close();
                    }
                }

            }

        private async void Vivod_info_Load(object sender, EventArgs e)
        {
            info_po_obsh();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
