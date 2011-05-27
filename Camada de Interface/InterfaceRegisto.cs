using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETdA.Camada_de_Interface
{
    public partial class InterfaceRegisto : Form
    {
        public InterfaceRegisto()
        {
            InitializeComponent();
        }

        private void RegistarActionPerformed(object sender, EventArgs e)
        {
            String cont = "abcdefghijklmnopqrstuvwxyz" + 
                          "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + 
                          "0123456789" + 
                          "_-";

            string username = textBox1.Text;
            String pw1 = textBox2.Text;
            String pw2 = textBox3.Text;

            bool found = true;
            for ( int i = 0 ; i < username.Length && found; i++ )
                found = cont.Contains(username[i]);

            if (username == "" || !found )
                errorProvider1.SetError(this.textBox1, "Username Invalido");
            else
            {
                for (int i = 0; i < pw1.Length && found; i++)
                    found = cont.Contains(pw1[i]);

                if (pw1 == "" || !found)
                    errorProvider2.SetError(this.textBox2, "Password Invalida");
                else if (pw1 != pw2)
                    errorProvider3.SetError(this.textBox3, "Passwords Não Combinam");
                else
                    regista_analista(username, pw1);
            }
        }

        private void KeuPressActionPerformed(object sender, KeyPressEventArgs e)
        {
            TextBox t = (TextBox)sender;

            if (t.Name == "textBox1")
                errorProvider1.Clear();
            else if (t.Name == "textBox2")
                errorProvider2.Clear();
            else
                errorProvider3.Clear();
        }

        private void CancelarActionPerformed(object sender, EventArgs e)
        {
            endFrame();
        }

        private void regista_analista(String usr, String pw)
        {
            //TODO
        }

        private void endFrame()
        {
            Dispose();
            Close();
        }

        public static void main()
        {
            InterfaceRegisto ir = new InterfaceRegisto();
            ir.Visible = true;
        }
    }
}
