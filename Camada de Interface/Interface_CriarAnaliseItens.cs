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
                n.Value = new decimal(0.333);
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
                n2.Value = new decimal(0.333);
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
                n3.Value = new decimal(0.333);
                n3.Location = new System.Drawing.Point(167, y);
                y += 40;
                p.Controls.Add(n3);

                Label l5 = new System.Windows.Forms.Label();
                l5.Width = 150;
                l5.Text = "Escalas";
                l5.Location = new System.Drawing.Point(7, y);
                y += 25;
                p.Controls.Add(l5);

                Label l6 = new System.Windows.Forms.Label();
                l6.Width = 70;
                l6.Text = "Vermelho";
                l6.Location = new System.Drawing.Point(7, y);
                y += 25;
                p.Controls.Add(l6);

                TextBox b1 = new System.Windows.Forms.TextBox();
                b1.Height = 20;
                b1.Width = 40;
                b1.Text = "0";
                b1.Location = new System.Drawing.Point(75, y);
                p.Controls.Add(b1);

                Label l7 = new System.Windows.Forms.Label();
                l7.Width = 150;
                l7.Text = "to";
                l7.Location = new System.Drawing.Point(120, y);
                p.Controls.Add(l7);

                TextBox b2 = new System.Windows.Forms.TextBox();
                b2.Height = 20;
                b2.Width = 40;
                b2.Text = "1";
                b2.Location = new System.Drawing.Point(140, y);
                y += 25;
                p.Controls.Add(b2);

                Label l8 = new System.Windows.Forms.Label();
                l8.Width = 70;
                l8.Text = "Laranja";
                l8.Location = new System.Drawing.Point(7, y);
                y += 25;
                p.Controls.Add(l8);

                TextBox b3 = new System.Windows.Forms.TextBox();
                b3.Height = 20;
                b3.Width = 40;
                b3.Text = "1";
                b3.Location = new System.Drawing.Point(75, y);
                p.Controls.Add(b3);

                Label l9 = new System.Windows.Forms.Label();
                l9.Width = 150;
                l9.Text = "to";
                l9.Location = new System.Drawing.Point(120, y);
                p.Controls.Add(l9);

                TextBox b4 = new System.Windows.Forms.TextBox();
                b4.Height = 20;
                b4.Width = 40;
                b4.Text = "2";
                b4.Location = new System.Drawing.Point(140, y);
                y += 25;
                p.Controls.Add(b4);

                Label l10 = new System.Windows.Forms.Label();
                l10.Width = 70;
                l10.Text = "Amarelo";
                l10.Location = new System.Drawing.Point(7, y);
                y += 25;
                p.Controls.Add(l10);

                TextBox b5 = new System.Windows.Forms.TextBox();
                b5.Height = 20;
                b5.Width = 40;
                b5.Text = "2";
                b5.Location = new System.Drawing.Point(75, y);
                p.Controls.Add(b5);

                Label l11 = new System.Windows.Forms.Label();
                l11.Width = 150;
                l11.Text = "to";
                l11.Location = new System.Drawing.Point(120, y);
                p.Controls.Add(l11);

                TextBox b6 = new System.Windows.Forms.TextBox();
                b6.Height = 20;
                b6.Width = 40;
                b6.Text = "3";
                b6.Location = new System.Drawing.Point(140, y);
                y += 25;
                p.Controls.Add(b6);

                Label l12 = new System.Windows.Forms.Label();
                l12.Width = 70;
                l12.Text = "Verde Lima";
                l12.Location = new System.Drawing.Point(7, y);
                y += 25;
                p.Controls.Add(l12);

                TextBox b7 = new System.Windows.Forms.TextBox();
                b7.Height = 20;
                b7.Width = 40;
                b7.Text = "3";
                b7.Location = new System.Drawing.Point(75, y);
                p.Controls.Add(b7);

                Label l13 = new System.Windows.Forms.Label();
                l13.Width = 150;
                l13.Text = "to";
                l13.Location = new System.Drawing.Point(120, y);
                p.Controls.Add(l13);

                TextBox b8 = new System.Windows.Forms.TextBox();
                b8.Height = 20;
                b8.Width = 40;
                b8.Text = "4";
                b8.Location = new System.Drawing.Point(140, y);
                y += 25;
                p.Controls.Add(b8);

                Label l14 = new System.Windows.Forms.Label();
                l14.Width = 70;
                l14.Text = "Verde";
                l14.Location = new System.Drawing.Point(7, y);
                y += 25;
                p.Controls.Add(l14);

                TextBox b9 = new System.Windows.Forms.TextBox();
                b9.Height = 20;
                b9.Width = 40;
                b9.Text = "4";
                b9.Location = new System.Drawing.Point(75, y);
                p.Controls.Add(b7);

                Label l15 = new System.Windows.Forms.Label();
                l15.Width = 150;
                l15.Text = "to";
                l15.Location = new System.Drawing.Point(120, y);
                p.Controls.Add(l15);

                TextBox b10 = new System.Windows.Forms.TextBox();
                b10.Height = 20;
                b10.Width = 40;
                b10.Text = "5";
                b10.Location = new System.Drawing.Point(140, y);
                y += 25;
                p.Controls.Add(b10);

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
