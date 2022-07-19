using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIPLOM_V2
{
    public partial class Glavnaya_forma : Form
    {
        public Glavnaya_forma()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Studenti_ugltu_dobavit newForm = new Studenti_ugltu_dobavit();
            newForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Studenti_ne_ugltu_dobavit newForm = new Studenti_ne_ugltu_dobavit();
            newForm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Sotrudniki_dobavit newForm = new Sotrudniki_dobavit();
            newForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Zaselenie_studenti_ugltu newForm = new Zaselenie_studenti_ugltu();
            newForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Zaselenie_studenti_ne_ugltu newForm = new Zaselenie_studenti_ne_ugltu();
            newForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Zaselenie_sotrudniki newForm = new Zaselenie_sotrudniki();
            newForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Izmenit_studenti_ugltu newForm = new Izmenit_studenti_ugltu();
            newForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Izmenit_studenti_ne_ugltu newForm = new Izmenit_studenti_ne_ugltu();
            newForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Izmenit_sotrudniki newForm = new Izmenit_sotrudniki();
            newForm.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Vivod_info newForm = new Vivod_info();
            newForm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Ydalenie newForm = new Ydalenie();
            newForm.Show();
        }

        private void Glavnaya_forma_Load(object sender, EventArgs e)
        {
            Аuthorization newForm = new Аuthorization();
            this.Hide();
            newForm.ShowDialog();
          
        }

        private void button15_Click(object sender, EventArgs e)
        {
           Spisok newForm = new Spisok();
            newForm.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Prozhivaushie newForm = new Prozhivaushie();
            newForm.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Prikazi newForm = new Prikazi();
            newForm.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Dolzhniki newForm = new Dolzhniki();
            newForm.Show();
        }
    }
}
