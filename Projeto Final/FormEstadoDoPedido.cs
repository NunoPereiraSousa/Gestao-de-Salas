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
    public partial class FormEstadoDoPedido : Form
    {
        string user;
        int perfil;
        
        string fichNotif = "notificaçoes.txt";

        public FormEstadoDoPedido(string user, int perfil)
        {
            InitializeComponent();

            this.FormBorderStyle = 0;

            this.user = user;
            this.perfil = perfil;
            
            if (perfil == 2)
            {
                dataGridView1.Columns[0].Visible = false;
                label4.Visible = false;
                label7.Visible = false;
            }
            else if(perfil == 1)
            {
                label3.Visible = false;
            }
            else
            {
                label4.Visible = false;
                label7.Visible = false;
                label3.Text = "Administrador";
                label3.Location = new Point(50, 46);
            }


            /* Carregar comboBoxs */
            string fichSalas = "salas.txt";
            if (File.Exists(fichSalas))
            {
                StreamReader sr = File.OpenText(fichSalas);
                string ln = "";
                while ((ln = sr.ReadLine()) != null)
                {
                    SalasComboBox.Items.Add(ln);
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
                    string[] dados = ln.Split(';');
                    AssuntoComboBox.Items.Add(dados[1]);
                }
                sr.Close();
            }

            /* Mensagem de boas-vindas */
            string mensagem = metodos.GerarBoasVindas(user);
            toolStripStatusLabel1.Text = mensagem;
        }

        private void PesquisaBtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            if (File.Exists(fichNotif))
            {
                string[] lerNotif = File.ReadAllLines(fichNotif);

                StreamWriter sw = File.CreateText("temp.txt");
                for (int i = 0; i < lerNotif.Length; i++)
                {
                    string[] dados = lerNotif[i].Split(';');

                    /*
                     * Indices:
                     * 0. user              4. data
                     * 1. sala              5. hora
                     * 2. assunto           6. estado
                     * 3. comentario        7. resposta
                     * 
                     */

                    if ((dados[0] == user && perfil == 2) || perfil != 2)
                    {
                        if ((checkBox1.Checked == true && checkBox2.Checked == false && dados[6] == "Pendente") 
                            || (checkBox1.Checked == false && checkBox2.Checked == true && dados[6] == "Concluido")
                            || (checkBox1.Checked == true && checkBox2.Checked == true))
                        {
                                /* Ambas as comboBox NAO aplicarem filtros */
                            if ((SalasComboBox.Text == SalasComboBox.Items[0].ToString() && AssuntoComboBox.Text == AssuntoComboBox.Items[0].ToString())
                                /* Aplicar apenas filtro de salas */
                                || (SalasComboBox.Text != SalasComboBox.Items[0].ToString() && dados[1] == SalasComboBox.Text) && (AssuntoComboBox.Text == AssuntoComboBox.Items[0].ToString())
                                /* Aplicar apenas filtro de assuntos */
                                || (AssuntoComboBox.Text != AssuntoComboBox.Items[0].ToString() && dados[2] == AssuntoComboBox.Text) && (SalasComboBox.Text == SalasComboBox.Items[0].ToString())
                                /* Ambas as comboBox aplicarem filtros */
                                || (SalasComboBox.Text != SalasComboBox.Items[0].ToString() && dados[1] == SalasComboBox.Text) && (AssuntoComboBox.Text != AssuntoComboBox.Items[0].ToString() && dados[2] == AssuntoComboBox.Text))
                            {

                                dataGridView1.Rows.Insert(0, dados[0], dados[6], dados[2], dados[1], dados[4], dados[5]);

                                sw.WriteLine(dados[0] + ';' + dados[1] + ';' + dados[2] + ';' + dados[3] + ';' + dados[4] + ';' + dados[5] + ';' + dados[6] + ';' + dados[7] + ';' + i);
                            }
                        }
                    }
                }
                sw.Close();
            }
        }

        private void ExitRegisterButton_Click(object sender, EventArgs e)
        {
            File.Delete("temp.txt");

            Application.Exit();
        }

        private void RetrocederButton_Click(object sender, EventArgs e)
        {
            File.Delete("temp.txt");

            this.Hide();

            /* Para forcar refresh no form anterior NAO usar variavel CurrentForm */
            if (perfil == 2)
            {
                MenuProfessor mp = new MenuProfessor(user, perfil);
                mp.Show();
            }
            else if (perfil == 1)
            {
                ServiçosDeInformatica sdi = new ServiçosDeInformatica(user, perfil);
                sdi.Show();
            }
            else
            {
                Admin adm = new Admin(user, perfil);
                adm.Show();
            }
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            File.Delete("temp.txt");

            this.Close();

            InitialForm ini = new InitialForm();
            ini.Show();
        }
        
        /* NAO apagar */
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void VerDetalhesButton_Click(object sender, EventArgs e)
        {
            try
            {
                int indice = dataGridView1.CurrentCell.RowIndex;

                string[] temp = File.ReadAllLines("temp.txt");
                string[] ut = temp[indice].Split(';');

                FormRespostasNotificaçoes frn = new FormRespostasNotificaçoes(user, perfil, ut[0], ut[1], ut[2], ut[3], ut[4], ut[5], ut[6], ut[7], ut[8]);
                frn.Show();
            }
            catch
            {
                toolStripStatusLabel1.Text = "Nao foi possivel encontrar a notificacao";
            }
        }

        private void ExitAppButton_Click(object sender, EventArgs e)
        {
            File.Delete("temp.txt");

            Application.Exit();
        }
    }
}
