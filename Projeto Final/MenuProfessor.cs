using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Projeto_Final
{
    public partial class MenuProfessor : Form
    {
        string user;
        int perfil;

        public MenuProfessor(string user, int perfil)
        {
            InitializeComponent();

            this.FormBorderStyle = 0;

            this.user = user;
            this.perfil = perfil;

            /* Mensagem de boas-vindas */
            string mensagem = metodos.GerarBoasVindas(user);
            groupBox2.Text = mensagem;

            int notif = HaverNotif();
            if (notif != -1 && notif != 0)
            {
                textBox2.Text = notif.ToString();
            }
            else
            {
                label9.Visible = false;
                textBox2.Visible = false;
                label7.Visible = false;
                button4.Enabled = false;

                label6.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormNotificaçoes fn = new FormNotificaçoes(user, perfil);
            fn.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();

            InitialForm ini = new InitialForm();
            ini.Show();
        }

        private void ExitRegisterButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormEstadoDoPedido edp = new FormEstadoDoPedido(user, perfil);
            edp.Show();
        }

        private void ExitRegisterButton_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormEstadoDoPedido rn = new FormEstadoDoPedido(user, perfil);
            rn.Show();
        }

        private int HaverNotif()
        {
            string fichNotif = "notificaçoes.txt";

            int contPendentes = 0;
            if (!File.Exists(fichNotif))
            {
                return -1;
            }
            else
            {
                StreamReader sr = File.OpenText(fichNotif);
                string ln = "";
                while ((ln = sr.ReadLine()) != null)
                {
                    string[] dados = ln.Split(';');
                    if (dados[0] == user && dados[6] == "Pendente")
                    {
                        contPendentes++;
                    }
                }
                sr.Close();

                return contPendentes;
            }
        }
    }
}
