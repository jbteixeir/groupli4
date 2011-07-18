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
    public partial class Interface_GestaoFormulariosOnline : Form
    {
        private long codAnalise;
        private object itens;
        private object zonas;
        private bool fa;
        private bool qt;

        public Interface_GestaoFormulariosOnline(long codAnalise, object itens, object zonas)
        {
            this.codAnalise = codAnalise;
            this.itens = itens;
            this.zonas = zonas;
            InitializeComponent();

            fa = GestaodeRespostas.isFAcreated(codAnalise);
            qt = GestaodeRespostas.isQTcreated(codAnalise);

            if (fa)
                toolStripStatusLabel2.Image = global::ETdA.Properties.Resources._1309271487_notification_done;
            else
                toolStripStatusLabel2.Image = global::ETdA.Properties.Resources._1309271507_notification_remove;
            if (qt)
                toolStripStatusLabel4.Image = global::ETdA.Properties.Resources._1309271487_notification_done;
            else
                toolStripStatusLabel4.Image = global::ETdA.Properties.Resources._1309271507_notification_remove;
        }

        public static void main(long codAnalise, object itens, object zonas)
        {
            Interface_GestaoFormulariosOnline i = new Interface_GestaoFormulariosOnline(codAnalise,itens, zonas);
            i.Visible = true;
        }

        private void end_Frame()
        {
            Dispose();
            Close();
        }

        private void OKActioonPerformed(object sender, EventArgs e)
        {
            end_Frame();
        }

        private void MouseEnterAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Underline);
            t.BackColor = Color.LightGray;
        }

        private void MouseLeaveAction(object sender, EventArgs e)
        {
            Label t = (Label)sender;
            t.Font = new Font(t.Font, FontStyle.Regular);
            t.BackColor = Color.Empty;
        }

        private void FA_ActionPerformed(object sender, EventArgs e)
        {
            if (fa)
            {
                if (MessageBox.Show("Já adicionou as perguntas para a ficha de avaliação.\nTem a certeza que pretende editar?",
                    "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!GestaodeRespostas.canEditFA(codAnalise))
                        MessageBox.Show("Não é possível editar, porque já foram adcionadas respostas ou porque o Website está online.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        Interface_Perguntas.main(codAnalise, itens);
                }
            }
            else
            {
                Interface_Perguntas.main(codAnalise, itens);
            }
        }

        private void QT_ActionPerformed(object sender, EventArgs e)
        {
            if (qt)
            {
                if (MessageBox.Show("Já adicionou as perguntas para o questionário.\nTem a certeza que pretende editar?", 
                    "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!GestaodeRespostas.canEditQT(codAnalise))
                        MessageBox.Show("Não é possível editar, porque já foram adcionadas respostas ou porque o Website está online.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        Interface_PerguntasQT.main(codAnalise, itens, zonas);
                }
            }
            else
                Interface_PerguntasQT.main(codAnalise, itens, zonas);
        }
    }
}
