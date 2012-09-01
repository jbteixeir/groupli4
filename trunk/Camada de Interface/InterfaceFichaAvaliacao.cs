using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdAnalyser.Camada_de_Negócio;

namespace ETdAnalyser.CamadaInterface
{
    public partial class InterfaceFichaAvaliacao : Form
    {
        public InterfaceFichaAvaliacao(long codigoProjecto, long codigoAnalise)
        {
            InitializeComponent();
            FichaAvaliacao.ScriptErrorsSuppressed = true;
            FichaAvaliacao.Url = new System.Uri("http://" + GestaodeAnalistas.nomeServidorWeb() + ":" + GestaodeAnalistas.portaServidorWeb() + "/ETdAnalyser/Default.aspx?form=FA&usr=" + 
                CamadaDados.ETdA.ETdA.Username + "&anl=" + codigoAnalise + "&prj" +
                    "=" + codigoProjecto + "&adminmode=true", System.UriKind.Absolute);
        }
    }
}
