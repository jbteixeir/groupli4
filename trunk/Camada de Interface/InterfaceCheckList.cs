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
    public partial class InterfaceCheckList : Form
    {
        public InterfaceCheckList(long codigoProjecto, long codigoAnalise)
        {
            InitializeComponent();
            CheckList.ScriptErrorsSuppressed = true;
            CheckList.Url = new System.Uri("http://" + GestaodeAnalistas.nomeServidorWeb() + ":" + GestaodeAnalistas.portaServidorWeb() + "/ETdAnalyser/Default.aspx?form=CL&usr=" +
                CamadaDados.ETdA.ETdA.Username + "&anl=" + codigoAnalise + "&prj" +
                    "=" + codigoProjecto + "&adminmode=true", System.UriKind.Absolute);
        }

        private void Interface_CheckList_Load(object sender, EventArgs e)
        {

        }

        private void CheckList_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
