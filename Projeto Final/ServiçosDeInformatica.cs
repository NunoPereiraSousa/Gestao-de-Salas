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
    public partial class ServiçosDeInformatica : Form
    {
        string user;
        int perfil;

        public ServiçosDeInformatica(string user, int perfil)
        {
            InitializeComponent();

            this.FormBorderStyle = 0;

            this.user = user;
            this.perfil = perfil;

            /* Mensagem de boas-vindas */
            string mensagem = metodos.GerarBoasVindas(user);
            groupBox1.Text = mensagem;

            int notif = HaverNotif();
            if (notif != -1 && notif != 0)
            {
                textBox1.Text = notif.ToString();
            }
            else
            {
                label1.Visible = false;
                textBox1.Visible = false;
                label5.Visible = false;

                label6.Visible = true;
                button1.Text = "Ver historico";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormGerirSoftware gs = new FormGerirSoftware(user, perfil);
            gs.Show();
        }

        private void GestaoDeSalasButton_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormGestaoDeSalas gds = new FormGestaoDeSalas(user, perfil);
            gds.Show();
        }

        private void TopicosDeAssuntoButton_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormTopicosDeAssunto tda = new FormTopicosDeAssunto(user, perfil);
            tda.Show();
        }

        private void ConsultasButton_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormConsultas c = new FormConsultas(user, perfil);
            c.Show();
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            this.Close();

            InitialForm ini = new InitialForm();
            ini.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormEstadoDoPedido rn = new FormEstadoDoPedido(user, perfil);
            rn.Show();
        }

        private void ExitRegisterButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                    if (dados[6] == "Pendente")
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
