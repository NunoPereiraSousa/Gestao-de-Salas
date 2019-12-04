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
    public partial class FormTopicosDeAssunto : Form
    {
        string user;
        int perfil;

        string caminho = "assuntos.txt";
        string temp = "temp.txt";

        public FormTopicosDeAssunto(string user, int perfil)
        {
            InitializeComponent();

            this.FormBorderStyle = 0;

            this.user = user;
            this.perfil = perfil;

            if (File.Exists(caminho))
            {
                AtualizarListBox();
            }
            else
            {
                StreamWriter sw = File.CreateText(temp);
                sw.Close();
            }

            /* Mensagem de boas-vindas */
            string mensagem = metodos.GerarBoasVindas(user);
            toolStripStatusLabel1.Text = mensagem;
        }

        private void AdicionarButton_Click(object sender, EventArgs e)
        {
            if (!TextBoxVazia())
            {
                string lerTxt = AssuntoTextBox.Text;

                bool serAssuntoRepetido = false;
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    /* Retirar o numero do assunto */
                    string txtAssunto = listBox1.Items[i].ToString().Remove(0, 4);
                    if (lerTxt == txtAssunto)
                    {
                        serAssuntoRepetido = true;
                        toolStripStatusLabel1.Text = "O assunto que tentou adicionar ja existe.";
                        break;
                    }
                }

                if (!serAssuntoRepetido)
                {
                    int numAssunto = listBox1.Items.Count + 1;
                    string strNumAssunto = numAssunto.ToString("00");

                    string novoAssunto = strNumAssunto + ";" + lerTxt;

                    StreamWriter sw = File.AppendText(temp);
                    sw.WriteLine(novoAssunto);
                    sw.Close();

                    listBox1.Items.Add(numAssunto.ToString("00") + ". " + lerTxt);

                    toolStripStatusLabel1.Text = "Assunto adicionado a lista com sucesso!";
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "O campo de assunto esta vazio!";
            }
        }

        /*
        private void RetrocederButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            variaveis.CurrentForm.Show();
        }
        */

        /*
        private void LogOutButton_Click(object sender, EventArgs e)
        {
            this.Close();

            InitialForm ini = new InitialForm();
            ini.Show();
        }
        */

        private void RetrocederButton_Click_1(object sender, EventArgs e)
        {
            File.Delete(temp);

            this.Hide();
            variaveis.CurrentForm.Show();
        }

        private void LogOutButton_Click_1(object sender, EventArgs e)
        {
            File.Delete(temp);

            this.Close();

            InitialForm ini = new InitialForm();
            ini.Show();
        }

        private void RemoverButton_Click(object sender, EventArgs e)
        {
            if (!TextBoxVazia())
            {
                try
                {
                    int indice = listBox1.SelectedIndex;
                    listBox1.Items.RemoveAt(indice);

                    if (!File.Exists(temp))
                    {
                        File.Copy(caminho, temp, true);
                    }
                    string[] lerTemp = File.ReadAllLines(temp);
                    StreamWriter sw = File.CreateText("temp2.txt");
                    for (int i = 0; i < lerTemp.Length; i++)
                    {
                        if (i < indice)
                        {
                            sw.WriteLine(lerTemp[i]);
                        }
                        else if (i > indice)
                        {
                            lerTemp[i] = lerTemp[i].Remove(0, 3);
                            string novoIndice = i.ToString("00");
                            string novaLinha = novoIndice + ';' + lerTemp[i];
                            sw.WriteLine(novaLinha);
                        }
                    }
                    sw.Close();

                    File.Delete(temp);
                    File.Move("temp2.txt", temp);

                    AtualizarListBox();

                    toolStripStatusLabel1.Text = "Assunto removido da lista com sucesso!";
                }
                catch
                {
                    toolStripStatusLabel1.Text = "Nao tem nenhum assunto selecionado.";
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "O campo de assunto esta vazio!";
            }
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(temp))
            {
                File.Delete(caminho);
                File.Move(temp, caminho);

                toolStripStatusLabel1.Text = "Assuntos gravados em ficheiro com sucesso!";
            }
        }

        private void AtualizarListBox()
        {
            listBox1.Items.Clear();

            if (!File.Exists(temp))
            {
                File.Copy(caminho, temp, true);
            }
            int contLinhas = 1;
            StreamReader sr = File.OpenText(temp);
            string ln = "";
            while ((ln = sr.ReadLine()) != null)
            {
                ln = ln.Remove(0, 3);
                string novoIndice = contLinhas.ToString("00");
                string novaLinha = novoIndice + ". " + ln;
                listBox1.Items.Add(novaLinha);

                contLinhas++;
            }
            sr.Close();
        }

        private bool TextBoxVazia()
        {
            if (AssuntoTextBox.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ExitAppButton_Click(object sender, EventArgs e)
        {
            File.Delete(temp);

            Application.Exit();
        }
    }
}
