using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Final
{
    public partial class Admin : Form
    {
        string user;
        int perfil;

        public Admin(string user, int perfil)
        {
            InitializeComponent();

            this.FormBorderStyle = 0;

            this.user = user;
            this.perfil = perfil;

            string mensagem = metodos.GerarBoasVindas(user);
            toolStripStatusLabel1.Text = mensagem;
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            this.Close();

            InitialForm ini = new InitialForm();
            ini.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormNotificaçoes fn = new FormNotificaçoes(user, perfil);
            fn.Show();
        }

        private void GerirSoftwareButton_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormGerirSoftware fgs = new FormGerirSoftware(user, perfil);
            fgs.Show();
        }

        private void ConsultasButton_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormConsultas fc = new FormConsultas(user, perfil);
            fc.Show();
        }

        private void TopicosDeAssuntoButton_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormTopicosDeAssunto fta = new FormTopicosDeAssunto(user, perfil);
            fta.Show();
        }

        private void GestaoDeSalasButton_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormGestaoDeSalas fgda = new FormGestaoDeSalas(user, perfil);
            fgda.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormPerfis fp = new FormPerfis(user);
            fp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            FormEstadoDoPedido fp = new FormEstadoDoPedido(user, perfil);
            fp.Show();
        }

        private void ExitAppButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
