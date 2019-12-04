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
    public partial class FormGestaoDeSalas : Form
    {
        string user;
        int perfil;
        
        string file = "salas.txt";

        public FormGestaoDeSalas(string user, int perfil)
        {
            InitializeComponent();

            this.FormBorderStyle = 0;

            this.user = user;
            this.perfil = perfil;

            if (File.Exists(file))
            {
                string[] importar = File.ReadAllLines(file);

                for (int i = 0; i < importar.Length; i++)
                {
                    listBox1.Items.Add(importar[i]);
                }
            }
            else
            {
                StreamWriter sw = File.CreateText(file);
                sw.Close();
            }

            /* Mensagem de boas-vindas */
            string mensagem = metodos.GerarBoasVindas(user);
            StatusLabel.Text = mensagem;
        }

        private void AdicionarButton_Click(object sender, EventArgs e)
        {
            string sala = SalaTextBox.Text;

            if (sala == "")
            {
                StatusLabel.Text = "Escreva um sala para adicionar a lista";
            }
            else if (sala.Length < 4)
            {
                StatusLabel.Text = "A sala tem de ter um bloco identificador e tres numeros.";
            }
            else if (!char.IsLetter(sala[0]))
            {
                StatusLabel.Text = "A letra do bloco nao foi identificada corretamente.";
            }
            else if (!char.IsDigit(sala[1]) || !char.IsDigit(sala[2]) || !char.IsDigit(sala[3]))
            {
                StatusLabel.Text = "A sala nao corresponde a um numero.";
            }
            else
            {
                bool aSalaExiste = false;
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (sala == listBox1.Items[i].ToString())
                    {
                        aSalaExiste = true;
                        StatusLabel.Text = "A sala já está na lista.";
                        break;
                    }
                }

                if (!aSalaExiste)
                {
                    listBox1.Items.Add(sala);
                    sala = "";

                    StatusLabel.Text = "Sala adicionada a lista com sucesso.";
                }
            }
        }

        private void RemoverButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
            SalaTextBox.Text = "";
            
            StatusLabel.Text = "Sala removida da lista com sucesso.";
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            StreamWriter sw = File.CreateText(file); ;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                sw.WriteLine(listBox1.Items[i]);
            }
            sw.Close();
            
            StatusLabel.Text = "Salas guardadas em ficheiro com sucesso.";
        }

        private void RetrocederButton_Click(object sender, EventArgs e)
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

        private void ExitAppButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
