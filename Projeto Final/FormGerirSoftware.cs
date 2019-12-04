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
    public partial class FormGerirSoftware : Form
    {
        string user;
        int perfil;

        public static string file = "salas.txt";
        public string caminho = @"salas\";

        public FormGerirSoftware(string user, int perfil)
        {
            InitializeComponent();

            this.FormBorderStyle = 0;

            this.user = user;
            this.perfil = perfil;

            string[] importar = File.ReadAllLines(file);

            for (int i = 0; i < importar.Length; i++)
            {
                SalaComboBox.Items.Add(importar[i]);
            }

            if (!Directory.Exists("salas"))
            {
                Directory.CreateDirectory("salas");
            }

            /* Mensagem de boas-vindas */
            string mensagem = metodos.GerarBoasVindas(user);
            toolStripStatusLabel1.Text = mensagem;
        }

        private void RegistarButton_Click(object sender, EventArgs e)
        {
            string ficheiro = caminho + SalaComboBox.Text + ".txt";

            StreamWriter sw;

            if (!File.Exists(ficheiro))
            {
                sw = File.CreateText(ficheiro);
            }
            else
            {
                sw = File.AppendText(ficheiro);
            }

            string software = SoftwareTextBox.Text;
            string data = DataTimePicker.Value.ToString("dd-MM-yyyy");
            string hora = HoraTimePicker.Value.ToString("HH:mm:ss");
            string licenca = LicençaComboBox.Text;

            string registo = software + ";" + data + ";" + hora + ";" + licenca;

            sw.WriteLine(registo);
            sw.Close();

            listBox1.Items.Add(registo);

            toolStripStatusLabel1.Text = "Registo concluido com sucesso";
        }

        private void SalaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Directory.Exists("salas"))
            {
                string[] salas = Directory.GetFiles("salas");
                for (int i = 0; i < salas.Length; i++)
                {
                    SalaComboBox.Items.Add(salas[i]);
                }
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
