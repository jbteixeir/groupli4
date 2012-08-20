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
    public partial class InterfaceFichaAvaliacao : Form
    {
        public InterfaceFichaAvaliacao(long codProjecto, long codAnalise)
        {
            InitializeComponent();
            FichaAvaliacao.ScriptErrorsSuppressed = true;
            FichaAvaliacao.Url = new System.Uri("http://" + GestaodeAnalistas.nomeServidorWeb() + ":" + GestaodeAnalistas.portaServidorWeb() + "/ETdA/Default.aspx?form=FA&usr=" + 
                Camada_de_Dados.ETdA.ETdA.Username + "&anl=" + codAnalise + "&prj" +
                    "=" + codProjecto + "&adminmode=true", System.UriKind.Absolute);
        }
    }
}
