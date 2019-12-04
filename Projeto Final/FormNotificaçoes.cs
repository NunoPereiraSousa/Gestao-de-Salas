using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Final
{
    public partial class FormNotificaçoes : Form
    {
        string user;
        int perfil;

        public FormNotificaçoes(string user, int perfil)
        {
            InitializeComponent();

            this.FormBorderStyle = 0;

            this.user = user;
            this.perfil = perfil;


            /* O administrador pode definir o username */
            if (perfil == 0)
            {
                DocenteTextBox.Enabled = true;
            }
            else
            {
                DocenteTextBox.Enabled = false;
                DocenteTextBox.Text = user;
            }

            /* Carregar comboBoxs */
            string fichSalas = "salas.txt";
            if (File.Exists(fichSalas))
            {
                StreamReader sr = File.OpenText(fichSalas);
                string ln = "";
                while ((ln = sr.ReadLine()) != null)
                {
                    SalaComboBox.Items.Add(ln);
                }
                sr.Close();
            }

            string fichAssuntos = "assuntos.txt";
            if (File.Exists(fichAssuntos))
            {
                StreamReader sr = File.OpenText(fichAssuntos);
                string ln = "";
                while ((ln = sr.ReadLine()) != null)
                {
                    string[] split = ln.Split(';');
                    AssuntoComboBox.Items.Add(split[1]);
                }
                sr.Close();
            }

            /* Mensagem de boas-vindas */
            string mensagem = metodos.GerarBoasVindas(user);
            StatusLabel.Text = mensagem;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fichNotif = "notificaçoes.txt";

            /* Verificar se os campos de entrada estao preenchidos */
            if (SalaComboBox.Text != SalaComboBox.Items[0].ToString())
            {
                StatusLabel.Text = "Nao foi indicada uma sala.";
            }
            else if (SalaComboBox.Text != SalaComboBox.Items[0].ToString())
            {
                StatusLabel.Text = "Nao foi indicado nenhum assunto.";
            }
            else if (SalaComboBox.Text != SalaComboBox.Items[0].ToString())
            {
                StatusLabel.Text = "Por favor, especifique o seu problema.";
            }


            StreamWriter sw;

            if (!File.Exists(fichNotif))
            {
                sw = File.CreateText(fichNotif);
            }
            else
            {
                sw = File.AppendText(fichNotif);
            }

            /* Dados a armazenar no ficheiro (excepto variavel global user) */
            string sala = SalaComboBox.Text;
            string comentario = ComentarioTextBox.Text;
            string assunto = AssuntoComboBox.Text;
            string data = DateTime.Now.ToString("yyyy/MM/dd");
            string hora = DateTime.Now.ToString("HH:mm");
            string estado = "Pendente";

            /* A alterar posteriormente pelos Servicos de Informatica */
            string resposta = "N/a";

            sw.WriteLine(user + ';' + sala + ';' + assunto + ';' + comentario + ';' + data + ';' + hora + ';' + estado + ';' + resposta);

            sw.Close();

            StatusLabel.Text = "Notificacao enviada com sucesso.";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            variaveis.CurrentForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

            InitialForm ini = new InitialForm();
            ini.Show();
        }

        private void ExitRegisterButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RetrocederButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            /* Para forcar refresh no form anterior NAO usar variavel CurrentForm */
            if (perfil == 2)
            {
                MenuProfessor mp = new MenuProfessor(user, perfil);
                mp.Show();
            }
            else
            {
                Admin adm = new Admin(user, perfil);
                adm.Show();
            }
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            this.Close();

            InitialForm ini = new InitialForm();
            ini.Show();
        }

        private void ExitAppButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
