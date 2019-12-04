using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Final
{
    public partial class InitialForm : Form
    {
        public InitialForm()
        {
            InitializeComponent();

            this.FormBorderStyle = 0;

            string fichUsers = "users.txt";
            if (!File.Exists(fichUsers))
            {
                StreamWriter sw = File.CreateText(fichUsers);
                sw.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserTextBox.Text = "";
            PwTextBox.Text = "";

            variaveis.CurrentForm = ActiveForm;
            this.Hide();

            RegisterForm rf = new RegisterForm();
            rf.Show();
        }

        private void ExitRegisterButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string user = UserTextBox.Text;
            string pw = PwTextBox.Text;

            Users login = new Users(user, pw);

            int sessao = login.Login();

            string titLgnFailed = "Oops! O login falhou.";
            if (sessao >= 0)
            {
                variaveis.CurrentForm = Form.ActiveForm;
                this.Hide();

                login.Sessao(sessao);
            }
            else if (sessao == -1)
            {
                MessageBox.Show("A password está errada.", titLgnFailed);
            }
            else if (sessao == -2)
            {
                MessageBox.Show("A conta a que pretende aceder encontra-se inativa.", titLgnFailed);
            }
            else if (user == "" || pw == "")
            {
                MessageBox.Show("Preencha os campos.", titLgnFailed);
            }
            else
            {
                MessageBox.Show("A conta nao existe.", titLgnFailed);
            }
        }
    }
}
