using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdA.Camada_de_Negócio;

namespace ETdA.Camada_de_Interface
{
    public partial class Interface_CriarAnaliseItens : Form
    {
        Dictionary<string, string> defaults;
        Dictionary<string, string> alls;

        public Interface_CriarAnaliseItens()
        {
            InitializeComponent();

            defaults = GestaodeAnalises.getItensDefault();
            int i = 0;
            foreach (string s in defaults.Values)
            {
                checkedListBox1.Items.Add(s);
                checkedListBox1.SetItemChecked(i++, true);
            }

            ponderacao();
        }

        private void ponderacao()
        {
            panel1.AutoScroll = true;

            int yy = 7,y;
            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                y = 7;
                Panel p = new System.Windows.Forms.Panel();
                p.Height = 150;
                p.Width = 320;

                Label l = new System.Windows.Forms.Label();
                l.Width = 250;
                l.Text = checkedListBox1.Items[i].ToString();
                l.Location = new System.Drawing.Point(7, y);
                y += 30;
                p.Controls.Add(l);

                Label l2 = new System.Windows.Forms.Label();
                l2.Width = 150;
                l2.Text = "Ponderacao do Analista:";
                l2.Location = new System.Drawing.Point(7, y);
                p.Controls.Add(l2);

                NumericUpDown n = new System.Windows.Forms.NumericUpDown();
                n.Increment = new Decimal(0.1);
                n.Maximum = 1;
                n.DecimalPlaces = 3;
                n.Location = new System.Drawing.Point(167, y);
                y += 30;
                p.Controls.Add(n);

                Label l3 = new System.Windows.Forms.Label();
                l3.Width = 150;
                l3.Text = "Ponderacao do Profissional:";
                l3.Location = new System.Drawing.Point(7, y);
                p.Controls.Add(l3);

                NumericUpDown n2 = new System.Windows.Forms.NumericUpDown();
                n2.Increment = new Decimal(0.1);
                n2.Maximum = 1;
                n2.DecimalPlaces = 3;
                n2.Location = new System.Drawing.Point(167, y);
                y += 30;
                p.Controls.Add(n2);

                Label l4 = new System.Windows.Forms.Label();
                l4.Width = 150;
                l4.Text = "Ponderacao do Cliente:";
                l4.Location = new System.Drawing.Point(7, y);
                p.Controls.Add(l4);

                NumericUpDown n3 = new System.Windows.Forms.NumericUpDown();
                n3.Increment = new Decimal(0.1);
                n3.Maximum = 1;
                n3.DecimalPlaces = 3;
                n3.Location = new System.Drawing.Point(167, y);
                p.Controls.Add(n3);

                p.Location = new System.Drawing.Point(7, yy);
                yy += 150;
                panel1.Controls.Add(p);

            }
        }

        public static void main()
        {
            Interface_CriarAnaliseItens icai = new Interface_CriarAnaliseItens();
            icai.Visible = true;
        }
    }
}
