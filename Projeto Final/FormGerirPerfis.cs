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
    public partial class FormGerirPerfis : Form
    {
        string user;

        string fileUsers = "users.txt";

        public FormGerirPerfis(string user)
        {
            InitializeComponent();

            this.user = user;

            this.FormBorderStyle = 0;

            AtualizarDataGrid();

            string mensagem = metodos.GerarBoasVindas(user);
            toolStripStatusLabel1.Text = mensagem;
        }

        private void GestaoDeSalasButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            variaveis.CurrentForm.Show();
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            this.Close();

            InitialForm ini = new InitialForm();
            ini.Show();
        }

        private void AlterarCorpoDocenteButton_Click(object sender, EventArgs e)
        {
            string radio;
            if (SIRadioButton.Checked == true)
            {
                radio = "1";
            }
            else
            {
                radio = "2";
            }

            int indice = dataGridView1.CurrentCell.RowIndex;

            string[] read = File.ReadAllLines(fileUsers);
            string[] ut = read[indice].Split(';');
            
            if (ut[3] == radio)
            {
                toolStripStatusLabel1.Text = "O perfil do utilizador ja corresponde ao perfil selecionado.";
            }
            else
            {
                string ln = ut[0] + ';' + ut[1] + ';' + ut[2] + ';' + radio + ';' + ut[4];

                ModFich(indice, ln);
                toolStripStatusLabel1.Text = "Perfil alterado com sucesso.";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string procura = textBox5.Text;

            if (procura != "")
            {
                dataGridView1.Rows.Clear();

                StreamReader sr = File.OpenText(fileUsers);
                string ln = "";
                while ((ln = sr.ReadLine()) != null)
                {
                    string[] split = ln.Split(';');

                    if (procura.StartsWith("u:") && split[0].ToLower().Contains(procura.Substring(2, procura.Length - 2).ToLower())
                        || procura.StartsWith("m:") && split[1].ToLower().Contains(procura.Substring(2, procura.Length - 2).ToLower())
                        || procura.StartsWith("p:") && split[3].ToLower().Contains(procura.Substring(2, procura.Length - 2).ToLower())
                        || procura.StartsWith("e:") && split[4].ToLower().Contains(procura.Substring(2, procura.Length - 2).ToLower()))
                    {
                        dataGridView1.Rows.Add(split[0], split[1], SetPerfil(split[3]), SetEstado(split[4]));
                    }
                }
                sr.Close();
            }
            else
            {
                AtualizarDataGrid();
            }
        }

        private void AtualizarDataGrid()
        {
            dataGridView1.Rows.Clear();

            StreamReader sr = File.OpenText(fileUsers);
            string ln = "";
            while ((ln = sr.ReadLine()) != null)
            {
                string[] split = ln.Split(';');

                string userName = split[0];
                string userMail = split[1];
                string userPerfil = SetPerfil(split[3]);
                string userEstado = SetEstado(split[4]);

                /* Nao mostrar a palavra-chave */

                dataGridView1.Rows.Add(userName, userMail, userPerfil, userEstado);
            }
            sr.Close();
        }

        private void ModFich(int indice, string ln)
        {
            string[] read = File.ReadAllLines(fileUsers);

            string temp = "temp.txt";
            StreamWriter sw = File.CreateText(temp);

            for (int i = 0; i < read.Length; i++)
            {
                string[] split = read[i].Split(';');

                if (i == indice)
                {
                    sw.WriteLine(ln);
                }
                else
                {
                    sw.WriteLine(split[0] + ';' + split[1] + ';' + split[2] + ';' + split[3] + ';' + split[4]);
                }
            }
            sw.Close();

            File.Delete(fileUsers);
            File.Move(temp, fileUsers);

            AtualizarDataGrid();
        }

        private void AlterarEstadoButton_Click(object sender, EventArgs e)
        {
            string radio;
            if (AtivoRadioButton.Checked == true)
            {
                radio = "1";
            }
            else
            {
                radio = "0";
            }

            int indice = dataGridView1.CurrentCell.RowIndex;

            string[] read = File.ReadAllLines(fileUsers);
            string[] ut = read[indice].Split(';');
            
            if (ut[4] == radio)
            {
                toolStripStatusLabel1.Text = "O estado selecionado corresponde ao estado atual da conta.";
            }
            else
            {
                string ln = ut[0] + ';' + ut[1] + ';' + ut[2] + ';' + ut[3] + ';' + radio;

                ModFich(indice, ln);
                toolStripStatusLabel1.Text = "Estado da conta alterado com sucesso.";
            }
        }

        private void AlterarEmailButton_Click(object sender, EventArgs e)
        {
            string novoEmail = textBox3.Text;

            if (!RegisterForm.FormatoEmail(novoEmail))
            {
                toolStripStatusLabel1.Text = "O endereco nao se encontra no formato correto.";
            }
            else
            {
                int indice = dataGridView1.CurrentCell.RowIndex;

                string[] read = File.ReadAllLines(fileUsers);
                string[] ut = read[indice].Split(';');

                bool emailEmUso = false;
                for (int i = 0; i < read.Length; i++)
                {
                    string[] split = read[i].Split(';');
                    if (split[1].ToLower() == novoEmail.ToLower())
                    {
                        emailEmUso = true;
                        break;
                    }
                }

                if (emailEmUso)
                {
                    toolStripStatusLabel1.Text = "O endereco ja se encontra em uso.";
                }
                else if (ut[1] == novoEmail)
                {
                    toolStripStatusLabel1.Text = "O endereco ja pertence ao utilizador selecionado.";
                }
                else
                {
                    string ln = ut[0] + ';' + novoEmail + ';' + ut[2] + ';' + ut[3] + ';' + ut[4];

                    ModFich(indice, ln);
                    toolStripStatusLabel1.Text = "Email alterado com sucesso.";
                }
            }
        }

        private void AlterarPwButton_Click(object sender, EventArgs e)
        {
            string novaPw = textBox4.Text;
            
            if (novaPw.Length < 8)
            {
                toolStripStatusLabel1.Text = "A password tem de ter no minimo 8 caracteres.";
            }
            else if (RegisterForm.CharEspeciais(novaPw))
            {
                toolStripStatusLabel1.Text = "A password contem caracteres especiais.";
            }
            else
            {
                int indice = dataGridView1.CurrentCell.RowIndex;

                string[] read = File.ReadAllLines(fileUsers);
                string[] ut = read[indice].Split(';');

                string ln = ut[0] + ';' + ut[1] + ';' + novaPw + ';' + ut[3] + ';' + ut[4];

                ModFich(indice, ln);
                toolStripStatusLabel1.Text = "Password alterada com sucesso.";
            }
        }

        private string SetPerfil(string perfil)
        {
            if (perfil == "2")
            {
                return "Professor";
            }
            else
            {
                return "Servicos Inf.";
            }
        }

        private string SetEstado(string estado)
        {
            if (estado == "1")
            {
                return "Ativo";
            }
            else
            {
                return "Inativo";
            }
        }

        private void ExitAppButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
