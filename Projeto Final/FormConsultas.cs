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
    public partial class FormConsultas : Form
    {
        string user;
        int perfil;

        public static string file = "salas.txt";
        public string caminho = @"salas\";

        public FormConsultas(string user, int perfil)
        {
            InitializeComponent();

            this.FormBorderStyle = 0;

            this.user = user;
            this.perfil = perfil;

            string[] importar = File.ReadAllLines(file);

            for (int i = 0; i < importar.Length; i++)
            {
                listBox1.Items.Add(importar[i]);
            }

            /* Mensagem de boas-vindas */
            string mensagem = metodos.GerarBoasVindas(user);
            StatusLabel.Text = mensagem;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string ficheiro = caminho + listBox1.SelectedItem + ".txt";

            if (File.Exists(ficheiro))
            {
                string[] read = File.ReadAllLines(ficheiro);

                dataGridView1.Rows.Clear();

                for (int i = 0; i < read.Length; i++)
                {
                    string[] split = read[i].Split(';');

                    string software = split[0];
                    string data = split[1];
                    string hora = split[2];
                    string tipoLicença = split[3];

                    dataGridView1.Rows.Add(software, data, hora, tipoLicença);
                }
            }
            else
            {
                MessageBox.Show("Não existem dados a apresentar.");
            }
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
