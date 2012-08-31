using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETdAnalyser.Camada_de_Negócio;

namespace ETdAnalyser.Camada_de_Interface
{
    public partial class InterfaceQuestionario : Form
    {
        public InterfaceQuestionario(long codigoProjecto, long codigoAnalise)
        {
            InitializeComponent();
            Questionario.ScriptErrorsSuppressed = true;
            Questionario.Url = new System.Uri("http://" + GestaodeAnalistas.nomeServidorWeb() + ":" + GestaodeAnalistas.portaServidorWeb() + "/ETdAnalyser/Default.aspx?form=QT&usr=" + 
                Camada_de_Dados.ETdA.ETdA.Username + "&anl=" + codigoAnalise + "&prj" +
                    "=" + codigoProjecto + "&adminmode=true", System.UriKind.Absolute);
        }
    }
}
