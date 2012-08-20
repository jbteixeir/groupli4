using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdAnalyser.Camada_de_Dados.Classes;

namespace ETdAnalyser.Camada_de_Interface
{
    public partial class InterfaceAssociacaoZonaColuna : Form
    {
        private delegate void eventoEventHandler(object sender, EventArgs e);
        private static event eventoEventHandler done_action;

        private List<Zona> zonas;
        private List<int> assiciacao;
        private Dictionary<int, ComboBox> combos;
        private Dictionary<object, ErrorProvider> erros;
        private Dictionary<int, Zona> resultado;

        public InterfaceAssociacaoZonaColuna(object _zonas, object _associacao)
        {
            InitializeComponent();

            zonas = (List<Zona>)_zonas;
            assiciacao = (List<int>)_associacao;
            erros = new Dictionary<object, ErrorProvider>();
            combos = new Dictionary<int, ComboBox>();

            done_action += new eventoEventHandler(
               Camada_de_Interface.InterfaceImporterMatching.reencMapeamento);

            init();
        }

        public static void main(object zonas, object associacao)
        {
            InterfaceAssociacaoZonaColuna azc = new InterfaceAssociacaoZonaColuna(zonas, associacao);
            azc.ShowDialog();
        }

        private void init()
        {
            for (int i = 0; i < assiciacao.Count; i++)
                adiciona_panel(i);
            
        }

        private void adiciona_panel(int i)
        {
            Panel p = new System.Windows.Forms.Panel();
            p.TabIndex = i;
            p.AutoSize = true;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            p.Dock = DockStyle.Top;
            panel1.Controls.Add(p);
            panel1.Controls.SetChildIndex(p, i);

            Label l = new System.Windows.Forms.Label();
            l.AutoSize = true;
            l.Text = "Valor: " + assiciacao[i].ToString();
            l.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l.Location = new System.Drawing.Point(7, 7);
            p.Controls.Add(l);

            Label l3 = new System.Windows.Forms.Label();
            l3.AutoSize = true;
            l3.Text = "Zona:";
            l3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l3.Location = new System.Drawing.Point(100, 7);
            p.Controls.Add(l3);

            ComboBox cb = new ComboBox();
            cb.AutoSize = true;
            cb.Items.AddRange(getNomesZonas().ToArray());
            cb.SelectedIndex = 0;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Location = new System.Drawing.Point(150, 7);
            cb.Click += ComboBoxClick;
            cb.KeyPress += ComboBoxKeyPressed;
            p.Controls.Add(cb);
            combos.Add(i, cb);
        }

        private bool verifica_erros()
        {
            bool continua = true;
            for (int i = 0 ; i < assiciacao.Count ; i++)
                for (int j = i+1 ; j < assiciacao.Count ; j++)
                    if (combos[i].SelectedIndex.Equals(combos[j].SelectedIndex))
                    {
                        if (!erros.Keys.Contains(combos[i]))
                        {
                            ErrorProvider err = new ErrorProvider();
                            err.Icon = global::ETdAnalyser.Properties.Resources.notification_warning_ico;
                            err.SetError(combos[i], "Vários códigos associado a " + combos[i].SelectedIndex + ".");
                            
                            erros.Add(combos[i], err);
                        }
                        if (!erros.Keys.Contains(combos[j]))
                        {
                            ErrorProvider err = new ErrorProvider();
                            err.Icon = global::ETdAnalyser.Properties.Resources.notification_warning_ico;
                            err.SetError(combos[j], "Vários códigos associado a " + combos[i].SelectedIndex + ".");

                            erros.Add(combos[j], err);
                        }
                        continua = false;
                    }
            return continua;
        }

        private List<string> getNomesZonas()
        {
            List<string> nomes = new List<string>();
            foreach (Zona z in zonas)
                nomes.Add(z.Nome);
            return nomes;
        }

        private void ContinuarClick(object sender, EventArgs e)
        {
            // se nao tiver erros
            if (verifica_erros())
            {
                resultado = new Dictionary<int, Zona>();
                for (int i = 0; i < assiciacao.Count; i++)
                    resultado.Add(assiciacao.ElementAt(i), zonas.ElementAt(combos[i].SelectedIndex));
                done_action(resultado, new EventArgs());
                endFrame();
            }
        }

        private void VoltarClick(object sender, EventArgs e)
        {
            endFrame();
        }

        private void endFrame()
        {
            Dispose();
            Close();
        }

        private void ComboBoxClick(object sender, EventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                string erro = erros[sender].GetError((ComboBox)sender);

                ErrorProvider err = erros[sender];
                err.Clear();
                erros.Remove(sender);

                for (int i = 0 ; i < erros.Keys.Count ; i++)
                    if (erro.Equals(erros[erros.Keys.ElementAt(i)].GetError((ComboBox)erros.Keys.ElementAt(i))))
                    {
                        err = erros[erros.Keys.ElementAt(i)];
                        err.Clear();
                        erros.Remove(erros.Keys.ElementAt(i));
                    }
            }
        }

        private void ComboBoxKeyPressed(object sender, EventArgs e)
        {
            if (erros.Keys.Contains(sender))
            {
                string erro = erros[sender].GetError((ComboBox)sender);

                ErrorProvider err = erros[sender];
                err.Clear();
                erros.Remove(sender);

                for (int i = 0; i < erros.Keys.Count; i++)
                    if (erro.Equals(erros[erros.Keys.ElementAt(i)].GetError((ComboBox)erros.Keys.ElementAt(i))))
                    {
                        err = erros[erros.Keys.ElementAt(i)];
                        err.Clear();
                        erros.Remove(erros.Keys.ElementAt(i));
                    }
            }
        }
    }
}
