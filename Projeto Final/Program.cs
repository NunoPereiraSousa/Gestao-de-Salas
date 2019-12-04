using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Final
{
    public class variaveis
    {
        public static Form CurrentForm = Form.ActiveForm;
    }

    public class metodos
    {
        public static string GerarBoasVindas(string user)
        {
            string data = DateTime.Now.ToString("dd-MM-yyyy");
            string hora = DateTime.Now.ToString("HH:mm");

            string boasVindas;
            int h = DateTime.Now.Hour;
            if (h < 5 || h > 19)
            {
                boasVindas = "Boa noite";
            }
            else if (h < 11)
            {
                boasVindas = "Bom dia";
            }
            else
            {
                boasVindas = "Boa tarde";
            }

            string mensagem = boasVindas + ", " + user + "! " + data + " às " + hora;

            return mensagem;
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InitialForm());
        }
    }
}
